using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using LavaCar.DAL.DataContext;
using LavaCar.ETL;

namespace LavaCar.DAL
{
    public class VehiculoDAL
    {
        private readonly ServiciosContext _dataContext = new ServiciosContext();
        public List<VehiculoETL> ListarVehiculos(string terminoBusqueda = "")
        {
            try
            {
                List<VehiculoETL> listaVehiculos = new List<VehiculoETL>();
                if (!string.IsNullOrEmpty(terminoBusqueda))
                {
                    listaVehiculos = _dataContext.Vehiculos
                        .Select(fila => new VehiculoETL
                        {
                            IdVehiculo = fila.IdVehiculo,
                            Placa = fila.Placa,
                            Dueno = fila.Dueno,
                            Marca = fila.Marca
                        }).Where(v => v.Placa == terminoBusqueda).ToList();
                }
                else
                {
                    listaVehiculos = _dataContext.Vehiculos
                        .Select(fila => new VehiculoETL
                        {
                            IdVehiculo = fila.IdVehiculo,
                            Placa = fila.Placa,
                            Dueno = fila.Dueno,
                            Marca = fila.Marca
                        }).ToList();
                }
                return listaVehiculos;
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
                VehiculoETL vehiculo = _dataContext.Vehiculos
                        .Select(fila => new VehiculoETL
                        {
                            IdVehiculo = fila.IdVehiculo,
                            Placa = fila.Placa,
                            Dueno = fila.Dueno,
                            Marca = fila.Marca
                        }).FirstOrDefault(v => v.IdVehiculo == idVehiculo);

                return vehiculo;
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
                if (vehiculo != null)
                {
                    Vehiculo nuevoVehiculo = new Vehiculo()
                    {
                        Placa = vehiculo.Placa.Trim(),
                        Dueno = vehiculo.Dueno.Trim(),
                        Marca = vehiculo.Marca.Trim()
                    };

                    _dataContext.Vehiculos.Add(nuevoVehiculo);

                    if (_dataContext.Entry(nuevoVehiculo).State.Equals(EntityState.Added))
                    {
                        _dataContext.SaveChanges();

                        if (_dataContext.Entry(nuevoVehiculo).State.Equals(EntityState.Unchanged))
                        {
                            return nuevoVehiculo.IdVehiculo;
                        }
                    }

                }

                return 0;
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
                if (vehiculo != null && _dataContext.Vehiculos.Any(v => v.IdVehiculo == vehiculo.IdVehiculo))
                {
                    Vehiculo vehiculoActualizado = new Vehiculo()
                    {
                        IdVehiculo = vehiculo.IdVehiculo,
                        Placa = vehiculo.Placa.Trim(),
                        Dueno = vehiculo.Dueno.Trim(),
                        Marca = vehiculo.Marca.Trim()
                    };

                    _dataContext.Vehiculos.Update(vehiculoActualizado);
                    _dataContext.SaveChanges();

                    if (_dataContext.Entry(vehiculoActualizado).State.Equals(EntityState.Unchanged))
                    {
                        return true;
                    }

                }

                return false;
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
                Vehiculo vehiculoEliminar = _dataContext.Vehiculos.FirstOrDefault(v => v.IdVehiculo == idVehiculo);
                if (vehiculoEliminar != null)
                {
                    List<VehiculoServicio> listaVehiculoServicios = _dataContext.VehiculoServicios.Where(vs => vs.IdVehiculo == idVehiculo).ToList();
                    if (listaVehiculoServicios.Count > 0)
                    {
                        _dataContext.RemoveRange(listaVehiculoServicios);
                        _dataContext.SaveChanges();
                    }

                    _dataContext.Vehiculos.Remove(vehiculoEliminar);
                    _dataContext.SaveChanges();

                    if (_dataContext.Entry(vehiculoEliminar).State.Equals(EntityState.Deleted))
                    {
                        return true;
                    }

                }

                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
