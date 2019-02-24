﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ABMMedicamentos.aspx.cs"
    MasterPageFile="~/MasterPage.master" Inherits="ABMMedicamentos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!--TABLA ABM MEDICAMENTOS-->
    <table border="0" cellpadding="0" cellspacing="0">
        <strong>A.B.M MEDICAMENTOS</strong>
        <!--FORMULARIO-->
        <tr>
            <td>
                <asp:Label ID="lblCodigo" runat="server" Text="Codigo:" />
            </td>
            <td>
                <asp:TextBox ID="txtCodigo" runat="server" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblRuc" runat="server" Text="Farmaceutica:" />
            </td>
            <td>
                <asp:DropDownList ID="ddlFarmaceuticas" runat="server" />
                <asp:Button ID="btnBuscar" Text="Buscar" runat="server" OnClick="btnBuscar_Click" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblDescripcion" runat="server" Text="Descripcion:" />
            </td>
            <td>
                <asp:TextBox ID="txtDescripcion" runat="server" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblPrecio" runat="server" Text="Precio:" />
            </td>
            <td>
                <asp:TextBox ID="txtPrecio" runat="server" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblNombre" runat="server" Text="Nombre:" />
            </td>
            <td>
                <asp:TextBox ID="txtNombre" runat="server" />
            </td>
        </tr>
    </table>
    <!--ALTA BAJA MODIFICACION BOTONES-->
    <table>
        <tr>
            <td>
                <asp:Button ID="btnAlta" Text="Alta" runat="server" OnClick="btnAlta_Click" />
            </td>
            <td>
                <asp:Button ID="btnBaja" Text="Baja" runat="server" OnClick="btnBaja_Click" />
            </td>
            <td>
                <asp:Button ID="btnModificar" Text="Modifciar" runat="server" OnClick="btnModificar_Click" />
            </td>
            <td>
                <asp:Button ID="btnLimpiar" Text="Limpiar" runat="server" OnClick="btnLimpiar_Click" />
            </td>
            <td>
                <asp:Button ID="btnCancelar" Text="Cancelar" runat="server" OnClick="btnCancelar_Click" />
            </td>
        </tr>
    </table>
    <asp:Label ID="lblERROR" runat="server" Text="" />
</asp:Content>
