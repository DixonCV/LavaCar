using System;
using Microsoft.AspNetCore.Mvc;
using LavaCar.ETL;
using LavaCar.BLL;

namespace LavaCar.UIL.Controllers
{
    public class VehiculosController : Controller
    {
        private readonly VehiculoBLL _vehiculoBLL = new VehiculoBLL();

        public IActionResult Index()
        {
            return View(_vehiculoBLL.ListarVehiculos());
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            VehiculoETL vehiculo = _vehiculoBLL.ConsultarVehiculo((int)id);

            if (vehiculo == null)
            {
                return NotFound();
            }

            return View(vehiculo);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("IdVehiculo,Placa,Dueno,Marca")] VehiculoETL vehiculo)
        {
            if (ModelState.IsValid)
            {
                int vehiculoId = _vehiculoBLL.InsertarVehiculo(vehiculo);
                return RedirectToAction("Create2", "vehiculoServicios", new { id = vehiculoId });
            }
            return View(vehiculo);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            VehiculoETL vehiculo = _vehiculoBLL.ConsultarVehiculo((int)id);
            if (vehiculo == null)
            {
                return NotFound();
            }
            return View(vehiculo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("IdVehiculo,Placa,Dueno,Marca")] VehiculoETL vehiculo)
        {
            if (id != vehiculo.IdVehiculo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _vehiculoBLL.ActualizarVehiculo(vehiculo);
                }
                catch (Exception)
                {
                    throw;                    
                }
                return RedirectToAction(nameof(Index));
            }
            return View(vehiculo);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            VehiculoETL vehiculo = _vehiculoBLL.ConsultarVehiculo((int)id);
            if (vehiculo == null)
            {
                return NotFound();
            }

            return View(vehiculo);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _vehiculoBLL.EliminarVehiculo(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
