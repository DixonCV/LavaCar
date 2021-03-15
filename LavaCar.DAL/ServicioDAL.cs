using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using LavaCar.DAL.DataContext;
using LavaCar.ETL;

namespace LavaCar.DAL
{
    public class ServicioDAL
    {
        private readonly ServiciosContext _dataContext = new ServiciosContext();
        public List<ServicioETL> ListarServicios(string terminoBusqueda = "")
        {
            try
            {
                List<ServicioETL> listaServicios = new List<ServicioETL>();
                if (!string.IsNullOrEmpty(terminoBusqueda))
                {
                    listaServicios = _dataContext.Servicios
                        .Select(fila => new ServicioETL
                        {
                            IdServicio = fila.IdServicio,
                            Descripcion = fila.Descripcion,
                            Monto = fila.Monto
                        }).Where(v => v.Descripcion == terminoBusqueda).ToList();
                }
                else
                {
                    listaServicios = _dataContext.Servicios
                        .Select(fila => new ServicioETL
                        {
                            IdServicio = fila.IdServicio,
                            Descripcion = fila.Descripcion,
                            Monto = fila.Monto
                        }).ToList();
                }
                return listaServicios;
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
                ServicioETL servicio = _dataContext.Servicios
                        .Select(fila => new ServicioETL
                        {
                            IdServicio = fila.IdServicio,
                            Descripcion = fila.Descripcion,
                            Monto = fila.Monto
                        }).FirstOrDefault(v => v.IdServicio == idServicio);

                return servicio;
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
                if (servicio != null)
                {
                    Servicio nuevoServicio = new Servicio()
                    {
                        IdServicio = servicio.IdServicio,
                        Descripcion = servicio.Descripcion.Trim(),
                        Monto = servicio.Monto
                    };

                    _dataContext.Servicios.Add(nuevoServicio);

                    if (_dataContext.Entry(nuevoServicio).State.Equals(EntityState.Added))
                    {
                        _dataContext.SaveChanges();

                        if (_dataContext.Entry(nuevoServicio).State.Equals(EntityState.Unchanged))
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

        public bool ActualizarServicio(ServicioETL servicio)
        {
            try
            {
                if (servicio != null && _dataContext.Servicios.Any(v => v.IdServicio == servicio.IdServicio))
                {
                    Servicio servicioActualizado = new Servicio()
                    {
                        IdServicio = servicio.IdServicio,
                        Descripcion = servicio.Descripcion,
                        Monto = servicio.Monto
                    };

                    _dataContext.Servicios.Update(servicioActualizado);
                    _dataContext.SaveChanges();

                    if (_dataContext.Entry(servicioActualizado).State.Equals(EntityState.Unchanged))
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

        public bool EliminarServicio(int idServicio)
        {
            try
            {
                Servicio servicioEliminar = _dataContext.Servicios.FirstOrDefault(v => v.IdServicio == idServicio);
                if (servicioEliminar != null)
                {
                    List<VehiculoServicio> listaVehiculoServicios = _dataContext.VehiculoServicios.Where(vs => vs.IdServicio == idServicio).ToList();
                    if (listaVehiculoServicios.Count > 0)
                    {
                        _dataContext.RemoveRange(listaVehiculoServicios);
                        _dataContext.SaveChanges();
                    }

                    _dataContext.Servicios.Remove(servicioEliminar);
                    _dataContext.SaveChanges();

                    if (_dataContext.Entry(servicioEliminar).State.Equals(EntityState.Deleted))
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

        /// <summary>
        /// Método que retorna una lista con los servicios que aún no han sido asignados a un vehiculo
        /// </summary>
        /// <returns>Lista de objetos ServicioETL</returns>
        public List<ServicioETL> ObtenerListaDeServicios(int vehiculoId)
        {
            try
            {
                List<int> serviciosAsociados = _dataContext.VehiculoServicios.Where(vs => vs.IdVehiculo == vehiculoId).Select(vs => vs.IdServicio).Distinct().ToList();
                List<ServicioETL> listaServicios = _dataContext.Servicios
                    .Select(s => new ServicioETL
                    {
                        IdServicio = s.IdServicio,
                        Descripcion = s.Descripcion
                    }).Where(vs => !serviciosAsociados.Any(sa => sa == vs.IdServicio)).ToList();

                return listaServicios;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
