﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Logica;
using EntidadesCompartidas;
using System.Text.RegularExpressions;

public partial class ABMEmpleados : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                //CARGAR HORAS
                for (int hora = 1; hora <= 24; hora++)
                {
                    string Hora = (hora.ToString() == "24") ? "00" : ((hora.ToString().Length == 1) ? ("0" + hora.ToString()) : hora.ToString());

                    ddlInicioJornadaHoras.Items.Add(Hora);
                    ddlFinJornadaHoras.Items.Add(Hora);
                }

                //CARGAR MINUTOS
                for (int minuto = 0; minuto <= 55; minuto += 5)
                {
                    string Minuto = (minuto.ToString().Length == 1) ? ("0" + minuto.ToString()) : minuto.ToString();
                    ddlInicioJornadaMinutos.Items.Add(Minuto.ToString());
                    ddlFinJornadaMinutos.Items.Add(Minuto.ToString());
                }

                FormularioDefault();
            }
        }
        catch (Exception ex) { lblERROR.Text = ex.Message; }
    }

    //FORMULARIO ESTADO DEFAULT
    private void FormularioDefault()
    {
        //BLOQUEAR BOTONES
        btnAlta.Enabled = false;
        btnBaja.Enabled = false;
        btnModificar.Enabled = false;
        btnCancelar.Enabled = true;
        btnBuscar.Enabled = true;

        //BLOQUEAR TEXTBOXES
        txtUsuario.Enabled = true;
        txtUsuario.Text = "";

        txtPass.Enabled = false;
        txtPass.Text = "";

        txtNombre.Enabled = false;
        txtNombre.Text = "";

        lblERROR.Text = "";

        ddlFinJornadaHoras.Enabled = false;
        ddlFinJornadaMinutos.Enabled = false;
        ddlInicioJornadaHoras.Enabled = false;
        ddlInicioJornadaMinutos.Enabled = false;
    }

    //FORMULARIO ESTADO ALTA
    private void FormularioAlta()
    {
        //BLOQUEAR BOTONES
        btnAlta.Enabled = true;
        btnBaja.Enabled = false;
        btnModificar.Enabled = false;
        btnCancelar.Enabled = true;
        btnBuscar.Enabled = true;

        //BOQUEAR TEXTBOXES
        txtUsuario.Enabled = false;
        txtPass.Enabled = true;
        txtNombre.Enabled = true;
        ddlFinJornadaHoras.Enabled = true;
        ddlFinJornadaMinutos.Enabled = true;
        ddlInicioJornadaHoras.Enabled = true;
        ddlInicioJornadaMinutos.Enabled = true;
    }

    //FORMULARIO MODIFICAR / CANCELAR
    private void FormularioModificarCancelar()
    {
        //BLOQUEAR BOTONES
        btnAlta.Enabled = false;
        btnBaja.Enabled = true;
        btnModificar.Enabled = true;
        btnCancelar.Enabled = true;
        btnBuscar.Enabled = true;

        //BOQUEAR TEXTBOXES
        txtUsuario.Enabled = false;

        txtPass.Enabled = true;
        txtPass.Text = ((Empleado)Session["Empleado"]).pPass;

        txtNombre.Enabled = true;
        txtNombre.Text = ((Empleado)Session["Empleado"]).pNombreCompleto;

        ddlFinJornadaHoras.Enabled = true;
        ddlFinJornadaHoras.SelectedValue = (Regex.Match(((Empleado)Session["Empleado"]).pFinJornadaLaboral, @"[0-9]+(?=:)")).ToString();

        ddlFinJornadaMinutos.Enabled = true;
        ddlFinJornadaMinutos.SelectedValue = (Regex.Match(((Empleado)Session["Empleado"]).pFinJornadaLaboral, @"(?<=:)[0-9]+")).ToString();

        ddlInicioJornadaHoras.Enabled = true;
        ddlInicioJornadaHoras.SelectedValue = (Regex.Match(((Empleado)Session["Empleado"]).pInicioJornadaLaboral, @"[0-9]+(?=:)")).ToString();

        ddlInicioJornadaMinutos.Enabled = true;
        ddlInicioJornadaMinutos.SelectedValue = (Regex.Match(((Empleado)Session["Empleado"]).pInicioJornadaLaboral, @"(?<=:)[0-9]+")).ToString();

    }

    protected void btnBuscar_Click(object sender, EventArgs e)
    {
        try
        {
            Logica.LogicaUsuario logicaUsuario = new Logica.LogicaUsuario();
            Session["Empleado"] = logicaUsuario.BuscarUsuario(txtUsuario.Text);

            if (!((Usuario)Session["Empleado"] is Cliente))
                if ((Usuario)Session["Empleado"] == null)
                    FormularioAlta();
                else
                    FormularioModificarCancelar();
            else
                throw new Exception("Este usuario pertenece a un cliente.");
        }
        catch (Exception ex)
        {
            lblERROR.ForeColor = System.Drawing.Color.Red;
            lblERROR.Text = ex.Message;
        }
    }
    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("DefaultEmpleado.aspx");
        }
        catch (Exception ex)
        {
            lblERROR.ForeColor = System.Drawing.Color.Red;
            lblERROR.Text = ex.Message;
        }
    }
    protected void btnLimpiar_Click(object sender, EventArgs e)
    {
        try
        {
            FormularioDefault();
        }
        catch (Exception ex)
        {
            lblERROR.ForeColor = System.Drawing.Color.Red;
            lblERROR.Text = ex.Message;
        }
    }
    protected void btnModificar_Click(object sender, EventArgs e)
    {
        try
        {
            string usuario = txtUsuario.Text;
            string pass = txtPass.Text;
            string nombre = txtNombre.Text;
            string inicioJornada = (ddlInicioJornadaHoras.SelectedItem.Text + ":" + ddlInicioJornadaMinutos.SelectedItem.Text);
            string finJornada = (ddlFinJornadaHoras.SelectedItem.Text + ":" + ddlFinJornadaMinutos.SelectedItem.Text);
            Empleado empleado = new Empleado(usuario, pass, nombre, inicioJornada, finJornada);

            Logica.LogicaUsuario logicaUsuario = new LogicaUsuario();
            logicaUsuario.ModificarUsuario(empleado);

            lblERROR.ForeColor = System.Drawing.Color.Green;
            lblERROR.Text = "Modificacion exitosa.";
        }
        catch (Exception ex)
        {
            lblERROR.ForeColor = System.Drawing.Color.Red;
            lblERROR.Text = ex.Message;
        }
    }
    protected void btnBaja_Click(object sender, EventArgs e)
    {
        try
        {
            string usuario = txtUsuario.Text;
            string pass = txtPass.Text;
            string nombre = txtNombre.Text;
            string inicioJornada = (ddlInicioJornadaHoras.SelectedItem.Text + ":" + ddlInicioJornadaMinutos.SelectedItem.Text);
            string finJornada = (ddlFinJornadaHoras.SelectedItem.Text + ":" + ddlFinJornadaMinutos.SelectedItem.Text);
            Empleado empleado = new Empleado(usuario, pass, nombre, inicioJornada, finJornada);

            Logica.LogicaUsuario logicaUsuario = new LogicaUsuario();
            logicaUsuario.BajaUsuario(empleado);

            lblERROR.ForeColor = System.Drawing.Color.Green;
            lblERROR.Text = "Baja exitosa.";
        }
        catch (Exception ex)
        {
            lblERROR.ForeColor = System.Drawing.Color.Red;
            lblERROR.Text = ex.Message;
        }
    }
    protected void btnAlta_Click(object sender, EventArgs e)
    {
        try
        {
            string usuario = txtUsuario.Text;
            string pass = txtPass.Text;
            string nombre = txtNombre.Text;
            string inicioJornada = (ddlInicioJornadaHoras.SelectedItem.Text + ":" + ddlInicioJornadaMinutos.SelectedItem.Text);
            string finJornada = (ddlFinJornadaHoras.SelectedItem.Text + ":" + ddlFinJornadaMinutos.SelectedItem.Text);
            Empleado empleado = new Empleado(usuario, pass, nombre, inicioJornada, finJornada);

            Logica.LogicaUsuario logicaUsuario = new LogicaUsuario();
            logicaUsuario.AltaUsuario(empleado);

            lblERROR.ForeColor = System.Drawing.Color.Green;
            lblERROR.Text = "Alta exitosa.";
        }
        catch (Exception ex)
        {
            lblERROR.ForeColor = System.Drawing.Color.Red;
            lblERROR.Text = ex.Message;
        }
    }
}