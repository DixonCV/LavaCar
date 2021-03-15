using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LavaCar.DAL;
using LavaCar.ETL;

namespace LavaCar.BLL
{
    public class VehiculoServicioBLL
    {
        private readonly VehiculoServicioDAL _vehiculoServicioDAL = new VehiculoServicioDAL();

        public List<VehiculoServicioETL> ListarVehiculoServicios(string servicioID = "")
        {
            try
            {
                return _vehiculoServicioDAL.ListarVehiculoServicios(servicioID);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public VehiculoServicioETL ConsultarVehiculoServicio(int idVehiculoServicio)
        {
            try
            {
                return _vehiculoServicioDAL.ConsultarVehiculoServicio(idVehiculoServicio);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool InsertarVehiculoServicio(VehiculoServicioETL vehiculoServicio)
        {
            try
            {
                return _vehiculoServicioDAL.InsertarVehiculoServicio(vehiculoServicio);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool ActualizarVehiculoServicio(VehiculoServicioETL vehiculoServicio)
        {
            try
            {
                return _vehiculoServicioDAL.ActualizarVehiculoServicio(vehiculoServicio);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool EliminarVehiculoServicio(int idVehiculoServicio)
        {
            try
            {
                return _vehiculoServicioDAL.EliminarVehiculoServicio(idVehiculoServicio);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
