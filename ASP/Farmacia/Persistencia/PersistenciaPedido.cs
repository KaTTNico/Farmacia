﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntidadesCompartidas;
using System.Data;
using System.Data.SqlClient;

namespace Persistencia
{
    public class PersistenciaPedido
    {
        //ALTA PEDIDO
        public void AltaPedido(Pedido pedido)
        {
            //GET CONNECTION STRING
            SqlConnection connection = new SqlConnection(Conexion.ConnectionString);

            //STORED PROCEDURE
            SqlCommand sp = new SqlCommand("AltaPedido", connection);
            sp.CommandType = CommandType.StoredProcedure;

            //PARAMETROS
            sp.Parameters.AddWithValue("@Cliente", pedido.pClienteComprador.pNombreUsuario);
            sp.Parameters.AddWithValue("@MedicamentoCodigo", pedido.pMedicamentoPedido.pCodigo);
            sp.Parameters.AddWithValue("@MedicamentoFarmaceutica", pedido.pMedicamentoPedido.pFarmaceutica.pRUC);
            sp.Parameters.AddWithValue("@CantidadMedicamento", pedido.pCantidad);
            sp.Parameters.AddWithValue("@Estado", pedido.pEstado);

            //RETORNO
            SqlParameter retorno = new SqlParameter("@retorno", SqlDbType.Int);
            retorno.Direction = ParameterDirection.ReturnValue;
            sp.Parameters.Add(retorno);

            try
            {
                connection.Open();

                //ALTA PEDIDO
                sp.ExecuteNonQuery();

                //RETORNO
                switch ((int)retorno.Value)
                {
                    case 1:
                        //EXITO
                        break;
                    //USUARIO NO EXISTE
                    case -1:
                        throw new Exception("El cliente no existe.");
                    //LA FARMACEUTICA NO EXISTE
                    case -2:
                        throw new Exception("La farmaceutica no existe.");
                    //EL MEDICAMENTO NO EXISTE
                    case -3:
                        throw new Exception("El medicamento no existe.");
                    //EXCEPCION NO CONTROLADA
                    default:
                        throw new Exception("Ha ocurrido un error vuelva a intentarlo mas tarde.");
                }
            }
            catch { throw; }

            finally { connection.Close(); }
        }

        //CAMBIAR ESTADO PEDIDO
        public void CambiarEstadoPedido(Pedido pedido)
        {
            //GET CONNECTION STRING
            SqlConnection connection = new SqlConnection(Conexion.ConnectionString);

            //STORED PROCEDURE
            SqlCommand sp = new SqlCommand("CambiarEstadoPedido", connection);
            sp.CommandType = CommandType.StoredProcedure;

            //PARAMETROS
            sp.Parameters.AddWithValue("@Numero", pedido.pNumero);
            sp.Parameters.AddWithValue("@Estado", pedido.pEstado);

            //RETORNO
            SqlParameter retorno = new SqlParameter("@retorno", SqlDbType.Int);
            retorno.Direction = ParameterDirection.ReturnValue;
            sp.Parameters.Add(retorno);

            try
            {
                connection.Open();

                //LISTAR PEDIDOS
                sp.ExecuteNonQuery();

                //RETORNO
                switch ((int)retorno.Value)
                {
                    case 1:
                        //EXITO
                        break;
                    //USUARIO NO EXISTE
                    case -1:
                        throw new Exception("El pedido no existe.");
                    //EXCEPCION NO CONTROLADA
                    default:
                        throw new Exception("Ha ocurrido un error vuelva a intentarlo mas tarde.");
                }
            }
            catch { throw; }

            finally { connection.Close(); }
        }

        //LISTAR PEDIDO POR CLIENTE EN ESTADO GENERADO
        public List<Pedido> ListarPedidoPorClienteGenerados(Cliente cliente)
        {
            //GET CONNECTION STRING 
            SqlConnection connection = new SqlConnection(Conexion.ConnectionString);

            //STORED PROCEDURE
            SqlCommand Command = new SqlCommand("ListarPedidoPorClienteGENERADOS", connection);
            Command.CommandType = CommandType.StoredProcedure;

            //PARAMETROS
            Command.Parameters.AddWithValue("@Usuario", cliente.pNombreUsuario);

            //READER
            SqlDataReader Reader;

            //PREPARAR VARIABLES
            PersistenciaMedicamento persistenciaMedicamento = new PersistenciaMedicamento();
            int Numero;
            int MedicamentoCodigo;
            string MedicamentoFarmacia;
            Medicamento medicamento = null;
            string Estado;
            int Cantidad;
            List<Pedido> List = new List<Pedido>();
            try
            {
                connection.Open();
                Reader = Command.ExecuteReader();
                while (Reader.Read())
                {
                    Numero = (int)Reader["Numero"];
                    Estado = (string)Reader["Estado"];
                    Cantidad = (int)Reader["CantidadMedicamento"];
                    MedicamentoCodigo = (int)Reader["MedicamentoCodigo"];
                    MedicamentoFarmacia = (string)Reader["MedicamentoFarmaceutica"];
                    medicamento = persistenciaMedicamento.BuscarMedicamento(MedicamentoCodigo, MedicamentoFarmacia);
                    Pedido pedido = new Pedido(Numero, cliente, medicamento, Cantidad, Estado);
                    List.Add(pedido);
                }
                Reader.Close();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error en la base de datos: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return List;
        }

        //LISTAR PEDIDO POR MEDICAMENTO EN ESTADO X
        public List<Pedido> ListarPedidoPorEstadoMedicamento(Medicamento medicamento, string Estado)
        {
            //GET CONNECTION STRING 
            SqlConnection connection = new SqlConnection(Conexion.ConnectionString);

            //STORED PROCEDURE
            SqlCommand Command = new SqlCommand("ListarPedidoPorEstadoMedicamento", connection);
            Command.CommandType = CommandType.StoredProcedure;

            //PARAMETROS
            Command.Parameters.AddWithValue("@MedicamentoCodigo", medicamento.pCodigo);
            Command.Parameters.AddWithValue("@MedicamentoFarmaceutica", medicamento.pFarmaceutica.pRUC);
            Command.Parameters.AddWithValue("@Estado", Estado);

            //READER
            SqlDataReader Reader;

            //PREPARAR VARIABLES
            PersistenciaCliente persistenciaCliente = new PersistenciaCliente();
            int Numero;
            Cliente cliente = null;
            int Cantidad;
            List<Pedido> List = new List<Pedido>();
            try
            {
                connection.Open();
                Reader = Command.ExecuteReader();
                while (Reader.Read())
                {
                    Numero = (int)Reader["Numero"];
                    Cantidad = (int)Reader["CantidadMedicamento"];
                    cliente = persistenciaCliente.BuscarCliente((string)Reader["Cliente"]);
                    Pedido pedido = new Pedido(Numero, cliente, medicamento, Cantidad, Estado);
                    List.Add(pedido);
                }
                Reader.Close();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error en la base de datos: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return List;
        }

        //LISTAR PEDIDO POR MEDICAMENTO
        public List<Pedido> ListarPedidoPorMedicamento(Medicamento medicamento)
        {
            //GET CONNECTION STRING 
            SqlConnection connection = new SqlConnection(Conexion.ConnectionString);

            //STORED PROCEDURE
            SqlCommand Command = new SqlCommand("ListarPedidoMedicamento", connection);
            Command.CommandType = CommandType.StoredProcedure;

            //PARAMETROS
            Command.Parameters.AddWithValue("@MedicamentoCodigo", medicamento.pCodigo);
            Command.Parameters.AddWithValue("@MedicamentoFarmaceutica", medicamento.pFarmaceutica.pRUC);

            //READER
            SqlDataReader Reader;

            //PREPARAR VARIABLES
            PersistenciaCliente persistenciaCliente = new PersistenciaCliente();
            int Numero;
            Cliente cliente = null;
            int Cantidad;
            string Estado;
            List<Pedido> List = new List<Pedido>();
            try
            {
                connection.Open();
                Reader = Command.ExecuteReader();
                while (Reader.Read())
                {
                    Numero = (int)Reader["Numero"];
                    Estado = (string)Reader["Estado"];
                    Cantidad = (int)Reader["CantidadMedicamento"];
                    cliente = persistenciaCliente.BuscarCliente((string)Reader["Cliente"]);
                    Pedido pedido = new Pedido(Numero, cliente, medicamento, Cantidad, Estado);
                    List.Add(pedido);
                }
                Reader.Close();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error en la base de datos: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return List;
        }

        //LISTAR PEDIDOS GENERADOS O ENVIADOS
        public List<Pedido> ListarPedidoGeneradoOEnviado()
        {
            //GET CONNECTION STRING 
            SqlConnection connection = new SqlConnection(Conexion.ConnectionString);

            //STORED PROCEDURE
            SqlCommand Command = new SqlCommand("ListarPedidoGeneradoOEnviado", connection);
            Command.CommandType = CommandType.StoredProcedure;

            //READER
            SqlDataReader Reader;

            //PREPARAR VARIABLES
            PersistenciaCliente persistenciaCliente = new PersistenciaCliente();
            PersistenciaMedicamento persistenciaMedicamento = new PersistenciaMedicamento();
            int Numero;
            Cliente cliente = null;
            int Cantidad;
            string Estado;
            Medicamento medicamento = null;
            int MedicamentoCodigo;
            string MedicamentoFarmacia;
            List<Pedido> List = new List<Pedido>();
            try
            {
                connection.Open();
                Reader = Command.ExecuteReader();
                while (Reader.Read())
                {
                    Numero = (int)Reader["Numero"];
                    Estado = (string)Reader["Estado"];
                    Cantidad = (int)Reader["CantidadMedicamento"];
                    MedicamentoCodigo = (int)Reader["MedicamentoCodigo"];
                    MedicamentoFarmacia = (string)Reader["MedicamentoFarmaceutica"];
                    medicamento = persistenciaMedicamento.BuscarMedicamento(MedicamentoCodigo, MedicamentoFarmacia);
                    cliente = persistenciaCliente.BuscarCliente((string)Reader["Cliente"]);
                    Pedido pedido = new Pedido(Numero, cliente, medicamento, Cantidad, Estado);
                    List.Add(pedido);
                }
                Reader.Close();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error en la base de datos: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return List;
        }

        //GET PEDIDO
        public Pedido BuscarPedido(int Numero)
        {
            //GET CONNECTION STRING
            SqlConnection connection = new SqlConnection(Conexion.ConnectionString);

            //STORED PROCEDURE
            SqlCommand sp = new SqlCommand("BuscarPedido", connection);
            sp.CommandType = CommandType.StoredProcedure;

            //PARAMETROS
            sp.Parameters.AddWithValue("@Numero", Numero);

            //READER
            SqlDataReader reader;

            //PREPARAR VARIABLES
            PersistenciaCliente persistenciaCliente = new PersistenciaCliente();
            PersistenciaMedicamento persistenciaMedicamento = new PersistenciaMedicamento();
            Cliente cliente;
            Medicamento medicamento;
            Pedido pedido;
            int Cantidad;
            string Estado;
            try
            {
                connection.Open();
                reader = sp.ExecuteReader();

                if (reader.Read())
                {
                    cliente = persistenciaCliente.BuscarCliente((string)reader["Cliente"]);
                    medicamento = persistenciaMedicamento.BuscarMedicamento((int)reader["MedicamentoCodigo"], (string)reader["MedicamentoFarmaceutica"]);
                    Cantidad = (int)reader["CantidadMedicamento"];
                    Estado = (string)reader["Estado"];
                    pedido = new Pedido(Numero, cliente, medicamento, Cantidad, Estado);
                    reader.Close();
                }
                else
                    return null;

                return pedido;
            }
            catch { throw; }

            finally { connection.Close(); }
        }

        //BAJA PEDIDO
        public void BajaPedido(Pedido pedido)
        {
            //GET CONNECTION STRING
            SqlConnection connection = new SqlConnection(Conexion.ConnectionString);

            //STORED PROCEDURE
            SqlCommand sp = new SqlCommand("BajaPedido", connection);
            sp.CommandType = CommandType.StoredProcedure;

            //PARAMETROS
            sp.Parameters.AddWithValue("@Numero", pedido.pNumero);

            //RETORNO
            SqlParameter retorno = new SqlParameter("@retorno", SqlDbType.Int);
            retorno.Direction = ParameterDirection.ReturnValue;
            sp.Parameters.Add(retorno);

            try
            {
                connection.Open();

                //BAJA PEDIDO
                sp.ExecuteNonQuery();

                //RETORNO
                switch ((int)retorno.Value)
                {
                    case 1:
                        //EXITO
                        break;
                    //EL PEDIDO NO EXISTE
                    case -1:
                        throw new Exception("El pedido no existe.");
                    //EXCEPCION NO CONTROLADA
                    default:
                        throw new Exception("Ha ocurrido un error vuelva a intentarlo mas tarde.");
                }
            }
            catch { throw; }

            finally { connection.Close(); }
        }
    }
}
