using System;
using Microsoft.AspNetCore.Mvc;
using LavaCar.BLL;
using LavaCar.ETL;

namespace LavaCar.UIL.Controllers
{
    public class ServiciosController : Controller
    {
        private readonly ServicioBLL _servicioBLL = new ServicioBLL();

        public IActionResult Index()
        {
            return View(_servicioBLL.ListarServicios());
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ServicioETL servicio = _servicioBLL.ConsultarServicio((int)id);
            if (servicio == null)
            {
                return NotFound();
            }

            return View(servicio);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("IdServicio,Descripcion,Monto")] ServicioETL servicio)
        {
            if (ModelState.IsValid)
            {
                _servicioBLL.InsertarServicio(servicio);
                return RedirectToAction(nameof(Index));
            }
            return View(servicio);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ServicioETL servicio = _servicioBLL.ConsultarServicio((int)id);
            if (servicio == null)
            {
                return NotFound();
            }
            return View(servicio);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("IdServicio,Descripcion,Monto")] ServicioETL servicio)
        {
            if (id != servicio.IdServicio)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _servicioBLL.ActualizarServicio(servicio);
                }
                catch (Exception)
                {
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(servicio);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var servicio = _servicioBLL.ConsultarServicio((int)id);
            if (servicio == null)
            {
                return NotFound();
            }

            return View(servicio);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _servicioBLL.EliminarServicio(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
