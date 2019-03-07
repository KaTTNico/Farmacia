﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntidadesCompartidas;
using Persistencia;

namespace Logica
{
    public class LogicaPedido
    {
        //ALTA PEDIDO
        public void AltaPedido(Pedido pedido)
        {
            try
            {
                Persistencia.PersistenciaPedido persistenciaPedido = new PersistenciaPedido();
                persistenciaPedido.AltaPedido(pedido);
            }
            catch { throw; }
        }

        //LISTAR PEDIDO
        public List<Pedido> ListarPedidosPorClienteGenerados(Cliente cliente)
        {
            try
            {
                Persistencia.PersistenciaPedido persistenciaPedido = new PersistenciaPedido();
                return persistenciaPedido.ListarPedidoPorClienteGenerados(cliente);
            }
            catch { throw; }
        }

        //LISTAR PEDIDO POR ESTADO MEDICAMENTO
        public List<Pedido> ListarPedidoPorEstadoMedicamento(Medicamento medicamento, string estado)
        {
            try
            {
                PersistenciaPedido persistenciaPedido = new PersistenciaPedido();
                return persistenciaPedido.ListarPedidoPorEstadoMedicamento(medicamento, estado);
            }
            catch { throw; }
        }

        //BUSCAR PEDIDO
        public Pedido BuscarPedido(int Numero)
        {
            try
            {
                Persistencia.PersistenciaPedido persistenciaPedido = new PersistenciaPedido();
                return persistenciaPedido.BuscarPedido(Numero);
            }
            catch { throw; }
        }

        //BAJA PEDIDO
        public void BajaPedido(Pedido pedido)
        {
            try
            {
                Persistencia.PersistenciaPedido persistenciaPedido = new PersistenciaPedido();
                persistenciaPedido.BajaPedido(pedido);
            }
            catch { throw; }
        }
    }
}
