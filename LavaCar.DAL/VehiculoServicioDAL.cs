using System;
using System.Collections.Generic;
using System.Linq;
using LavaCar.ETL;
using LavaCar.DAL.DataContext;
using Microsoft.EntityFrameworkCore;

namespace LavaCar.DAL
{
    public class VehiculoServicioDAL
    {
        private readonly ServiciosContext _dataContext = new ServiciosContext();

        public List<VehiculoServicioETL> ListarVehiculoServicios(string servicioId = "")
        {
            try
            {
                List<VehiculoServicioETL> listaVehiculoServicios = new List<VehiculoServicioETL>();
                if (!string.IsNullOrEmpty(servicioId))
                {
                    listaVehiculoServicios = _dataContext.VehiculoServicios
                        .Select(fila => new VehiculoServicioETL
                        {
                            IdVehiculoServicio = fila.IdVehiculoServicio,
                            IdServicioNavigation = new ServicioETL
                            {
                                IdServicio = fila.IdServicioNavigation.IdServicio,
                                Descripcion = fila.IdServicioNavigation.Descripcion
                            },
                            IdVehiculoNavigation = new VehiculoETL
                            {
                                IdVehiculo = fila.IdVehiculoNavigation.IdVehiculo,
                                Placa = fila.IdVehiculoNavigation.Placa
                            }
                        }).Where(v => v.IdServicioNavigation.IdServicio == Convert.ToInt32(servicioId)).ToList();
                }
                else
                {
                    listaVehiculoServicios = _dataContext.VehiculoServicios
                        .Select(fila => new VehiculoServicioETL
                        {
                            IdVehiculoServicio = fila.IdVehiculoServicio,
                            IdServicioNavigation = new ServicioETL
                            {
                                IdServicio = fila.IdServicioNavigation.IdServicio,
                                Descripcion = fila.IdServicioNavigation.Descripcion
                            },
                            IdVehiculoNavigation = new VehiculoETL
                            {
                                IdVehiculo = fila.IdVehiculoNavigation.IdVehiculo,
                                Placa = fila.IdVehiculoNavigation.Placa
                            }
                        }).ToList();
                }
                return listaVehiculoServicios;
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
                VehiculoServicioETL vehiculo = _dataContext.VehiculoServicios
                        .Select(fila => new VehiculoServicioETL
                        {
                            IdServicio = fila.IdServicioNavigation.IdServicio,
                            IdVehiculo = fila.IdVehiculoNavigation.IdVehiculo,
                            IdVehiculoServicio = fila.IdVehiculoServicio,
                            IdServicioNavigation = new ServicioETL
                            {
                                IdServicio = fila.IdServicioNavigation.IdServicio,
                                Descripcion = fila.IdServicioNavigation.Descripcion
                            },
                            IdVehiculoNavigation = new VehiculoETL
                            {
                                IdVehiculo = fila.IdVehiculoNavigation.IdVehiculo,
                                Placa = fila.IdVehiculoNavigation.Placa
                            }
                        }).FirstOrDefault(v => v.IdVehiculoServicio == idVehiculoServicio);

                return vehiculo;
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
                if (vehiculoServicio != null)
                {
                    VehiculoServicio nuevoVehiculoServicio = new VehiculoServicio()
                    {
                        IdVehiculo = vehiculoServicio.IdVehiculo,
                        IdServicio = vehiculoServicio.IdServicio
                    };

                    _dataContext.VehiculoServicios.Add(nuevoVehiculoServicio);

                    if (_dataContext.Entry(nuevoVehiculoServicio).State.Equals(EntityState.Added))
                    {
                        _dataContext.SaveChanges();

                        if (_dataContext.Entry(nuevoVehiculoServicio).State.Equals(EntityState.Unchanged))
                        {
                            return true;
                        }
                    }

                }

                return false;
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
                if (vehiculoServicio != null && _dataContext.VehiculoServicios.Any(v => v.IdVehiculoServicio == vehiculoServicio.IdVehiculoServicio))
                {
                    VehiculoServicio vehiculoServicioActualizado = new VehiculoServicio()
                    {
                        IdVehiculoServicio = vehiculoServicio.IdVehiculoServicio,
                        IdServicio = vehiculoServicio.IdServicio,
                        IdVehiculo = vehiculoServicio.IdVehiculo
                    };

                    _dataContext.VehiculoServicios.Update(vehiculoServicioActualizado);
                    _dataContext.SaveChanges();

                    if (_dataContext.Entry(vehiculoServicioActualizado).State.Equals(EntityState.Unchanged))
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

        public bool EliminarVehiculoServicio(int idVehiculoServicio)
        {
            try
            {
                VehiculoServicio vehiculoServicioEliminar = _dataContext.VehiculoServicios.FirstOrDefault(v => v.IdVehiculoServicio == idVehiculoServicio);
                if (vehiculoServicioEliminar != null)
                {

                    _dataContext.VehiculoServicios.Remove(vehiculoServicioEliminar);
                    _dataContext.SaveChanges();

                    if (_dataContext.Entry(vehiculoServicioEliminar).State.Equals(EntityState.Deleted))
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
