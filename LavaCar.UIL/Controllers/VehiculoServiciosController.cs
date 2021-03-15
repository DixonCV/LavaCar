using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using LavaCar.BLL;
using LavaCar.ETL;

namespace LavaCar.UIL.Controllers
{
    public class VehiculoServiciosController : Controller
    {
        private readonly ServicioBLL _servicioBLL = new ServicioBLL();
        private readonly VehiculoBLL _vehiculoBLL = new VehiculoBLL();
        private readonly VehiculoServicioBLL _vehiculoServicioBLL = new VehiculoServicioBLL();

        public IActionResult Index(string servicioId = "")
        {
            ViewData["Servicio"] = new SelectList(_servicioBLL.ListarServicios(), "IdServicio", "Descripcion");
            return View(_vehiculoServicioBLL.ListarVehiculoServicios(servicioId));
        }

        public IActionResult FiltrarPorServicio()
        {
            string id = Request.Form["IdServicio"];
            return RedirectToAction(nameof(Index), new { servicioID = id});
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            VehiculoServicioETL vehiculoServicio = _vehiculoServicioBLL.ConsultarVehiculoServicio((int)id);

            if (vehiculoServicio == null)
            {
                return NotFound();
            }

            return View(vehiculoServicio);
        }

        public IActionResult Create()
        {
            ViewData["IdServicio"] = new SelectList(_servicioBLL.ListarServicios(), "IdServicio", "Descripcion");
            ViewData["IdVehiculo"] = new SelectList(_vehiculoBLL.ListarVehiculos(), "IdVehiculo", "Placa");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("IdVehiculoServicio,IdServicio,IdVehiculo")] VehiculoServicioETL vehiculoServicio)
        {
            if (ModelState.IsValid)
            {
                _vehiculoServicioBLL.InsertarVehiculoServicio(vehiculoServicio);
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdServicio"] = new SelectList(_servicioBLL.ListarServicios(), "IdServicio", "Descripcion");
            ViewData["IdVehiculo"] = new SelectList(_vehiculoBLL.ListarVehiculos(), "IdVehiculo", "Placa");
            return View(vehiculoServicio);
        }

        public IActionResult Create2(int? id)
        {
            VehiculoETL vehiculo = _vehiculoBLL.ConsultarVehiculo((int)id);
            ViewData["Vehiculo"] = vehiculo;
            ViewData["IdServicio"] = new SelectList(_servicioBLL.ObtenerListaDeServicios((int)id), "IdServicio", "Descripcion");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create2([Bind("IdVehiculoServicio,IdServicio,IdVehiculo")] VehiculoServicioETL vehiculoServicio)
        {
            if (ModelState.IsValid)
            {
                _vehiculoServicioBLL.InsertarVehiculoServicio(vehiculoServicio);
                return RedirectToAction(nameof(Create2), new { id = vehiculoServicio.IdVehiculo });
            }
            ViewData["IdServicio"] = new SelectList(_servicioBLL.ListarServicios(), "IdServicio", "Descripcion");
            ViewData["IdVehiculo"] = new SelectList(_vehiculoBLL.ListarVehiculos(), "IdVehiculo", "Placa");
            return View(vehiculoServicio);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            VehiculoServicioETL vehiculoServicio = _vehiculoServicioBLL.ConsultarVehiculoServicio((int)id);
            if (vehiculoServicio == null)
            {
                return NotFound();
            }
            ViewData["IdServicio"] = new SelectList(_servicioBLL.ObtenerListaDeServicios(vehiculoServicio.IdVehiculoNavigation.IdVehiculo), "IdServicio", "Descripcion", vehiculoServicio.IdServicio);
            return View(vehiculoServicio);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("IdVehiculoServicio,IdServicio,IdVehiculo")] VehiculoServicioETL vehiculoServicio)
        {
            if (id != vehiculoServicio.IdVehiculoServicio)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _vehiculoServicioBLL.ActualizarVehiculoServicio(vehiculoServicio);
                }
                catch (Exception)
                {
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdServicio"] = new SelectList(_servicioBLL.ListarServicios(), "IdServicio", "Descripcion");
            return View(vehiculoServicio);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            VehiculoServicioETL vehiculoServicio = _vehiculoServicioBLL.ConsultarVehiculoServicio((int)id);
            if (vehiculoServicio == null)
            {
                return NotFound();
            }

            return View(vehiculoServicio);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _vehiculoServicioBLL.EliminarVehiculoServicio(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
