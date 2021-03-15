using System;
using System.Collections.Generic;
using LavaCar.DAL;
using LavaCar.ETL;

namespace LavaCar.BLL
{
    public class VehiculoBLL
    {
        private readonly VehiculoDAL _vehiculoDAL = new VehiculoDAL();

        public List<VehiculoETL> ListarVehiculos(string terminoBusqueda = "")
        {
            try
            {
                return _vehiculoDAL.ListarVehiculos(terminoBusqueda);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public VehiculoETL ConsultarVehiculo(int idVehiculo)
        {
            try
            {
                return _vehiculoDAL.ConsultarVehiculo(idVehiculo);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int InsertarVehiculo(VehiculoETL vehiculo)
        {
            try
            {
                return _vehiculoDAL.InsertarVehiculo(vehiculo);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool ActualizarVehiculo(VehiculoETL vehiculo)
        {
            try
            {
                return _vehiculoDAL.ActualizarVehiculo(vehiculo);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool EliminarVehiculo(int idVehiculo)
        {
            try
            {
                return _vehiculoDAL.EliminarVehiculo(idVehiculo);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
