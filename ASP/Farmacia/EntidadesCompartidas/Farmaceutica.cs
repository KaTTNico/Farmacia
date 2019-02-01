﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace EntidadesCompartidas
{
    class Farmaceutica
    {
        /*ATRIBUTOS*/
        private string RUC;
        private string Nombre;
        private string CorreoElectronico;
        private string Direccion;

        /*PROPIEDADES*/
        public string pRUC
        {
            get { return RUC; }
            set
            {
                /*VERIFICAR LONGITUD*/
                if (value.Trim().Length == 13)
                {
                    /*VERIFICAR PATRON*/
                    if (!Regex.Match(value, @"[A-Za-z]").Success)
                        RUC = value.Trim();
                    else
                        throw new Exception("No se admiten letras en el RUC.");
                }
                else
                    throw new Exception("El numero RUC debe ser de 13 digitos. \nEl ingresado contiene" + value.Length + " digitos.");
            }
        }

        public string pNombre
        {
            get { return Nombre; }
            set
            {
                /*VERIFICAR LONGITUD*/
                if (value.Trim().Length <= 50)
                    Nombre = value.Trim().ToUpper();
                else
                    throw new Exception("El nombre debe contener menos de 50 caracteres.");
            }
        }

        public string pCorreoElectronico
        {
            get { return CorreoElectronico; }
            set
            {
                /*VERIFICAR LONGITUD*/
                if (value.Trim().Length <= 50)
                {
                    /*VERIFICAR PATRON*/
                    if (Regex.Match(value, @"^[_a-z0-9-]+(.[a-z0-9-]+)@[a-z0-9-]+(.[a-z0-9-]+)*(.[a-z]{2,4})$").Success)
                        CorreoElectronico = value.Trim().ToUpper();
                    else
                        throw new Exception("El correo ingresado no es correcto.");
                }
            }
        }

        public string pDireccion
        {
            get { return Direccion; }
            set
            {
                /*VERIFICAR LONGITUD*/
                if (value.Trim().Length <= 50)
                    Direccion = value.Trim().ToUpper();
                else
                    throw new Exception("La direccion debe contener menos de 50 caracteres.");
            }
        }

        /*CONSTRUCTOR*/
        public Farmaceutica(string _RUC, string _Nombre, string _CorreoElectronico, string _Direccion)
        {
            pRUC = _RUC;
            pNombre = _Nombre;
            pCorreoElectronico = _CorreoElectronico;
            pDireccion = _Direccion;
        }
    }
}
