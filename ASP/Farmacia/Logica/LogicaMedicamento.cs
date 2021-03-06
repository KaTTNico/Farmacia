﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntidadesCompartidas;
using Persistencia;

namespace Logica
{
    public class LogicaMedicamento
    {
        //ALTA MEDICAMENTO
        public void AltaMedicamento(Medicamento medicamento)
        {
            try
            {
                PersistenciaMedicamento persistenciaMedicamento = new PersistenciaMedicamento();
                persistenciaMedicamento.AltaMedicamento(medicamento);
            }
            catch (Exception ex)
            { throw ex; }
        }

        //BAJA MEDICAMENTO
        public void BajaMedicamento(Medicamento medicamento)
        {
            try
            {
                PersistenciaMedicamento persistenciaMedicamento = new PersistenciaMedicamento();
                persistenciaMedicamento.BajaMedicamento(medicamento);
            }
            catch (Exception ex)
            { throw ex; }
        }

        //MODIFICAR MEDICAMENTO
        public void ModificarMedicamento(Medicamento medicamento)
        {
            try
            {
                PersistenciaMedicamento persistenciaMedicamento = new PersistenciaMedicamento();
                persistenciaMedicamento.ModificarMedicamento(medicamento);
            }
            catch (Exception ex)
            { throw ex; }
        }

        //BUSCAR MEDICAMENTO
        public Medicamento BuscarMedicamento(int codigo, string rucFarmaceutica)
        {
            try
            {
                PersistenciaMedicamento persistenciaMedicamento = new PersistenciaMedicamento();
                return persistenciaMedicamento.BuscarMedicamento(codigo, rucFarmaceutica);
            }
            catch (Exception ex)
            { throw ex; }
        }

        //LISTAR MEDICAMENTO
        public List<Medicamento> ListarMedicamento()
        {
            try
            {
                PersistenciaMedicamento persistenciaMedicamento = new PersistenciaMedicamento();
                return persistenciaMedicamento.ListarMedicamento();
            }
            catch { throw; }
        }

        //LISTAR MEDICAMENTO POR FARMACEUTICA
        public List<Medicamento> ListarMedicamentoPorFarmaceutica(Farmaceutica farmaceutica)
        {
            try
            {
                PersistenciaMedicamento persistenciaMedicamento = new PersistenciaMedicamento();
                return persistenciaMedicamento.ListarMedicamentoPorFarmaceutica(farmaceutica);
            }
            catch { throw; }
        }
    }
}
