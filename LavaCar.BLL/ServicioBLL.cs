using System;
using System.Collections.Generic;
using LavaCar.DAL;
using LavaCar.ETL;

namespace LavaCar.BLL
{
    public class ServicioBLL
    {
        private readonly ServicioDAL _servicioDAL = new ServicioDAL();
        public List<ServicioETL> ListarServicios(string terminoBusqueda = "")
        {
            try
            {
                return _servicioDAL.ListarServicios(terminoBusqueda);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ServicioETL ConsultarServicio(int idServicio)
        {
            try
            {
                return _servicioDAL.ConsultarServicio(idServicio);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool InsertarServicio(ServicioETL servicio)
        {
            try
            {
                return _servicioDAL.InsertarServicio(servicio);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool ActualizarServicio(ServicioETL servicio)
        {
            try
            {
                return _servicioDAL.ActualizarServicio(servicio);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool EliminarServicio(int idServicio)
        {
            try
            {
                return _servicioDAL.EliminarServicio(idServicio);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Método que retorna una lista con los servicios que aún no han sido asignados a un vehiculo
        /// </summary>
        /// <returns>Lista de objetos ServicioETL</returns>
        public List<ServicioETL> ObtenerListaDeServicios(int vehiculoId)
        {
            try
            {
                return _servicioDAL.ObtenerListaDeServicios(vehiculoId);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
