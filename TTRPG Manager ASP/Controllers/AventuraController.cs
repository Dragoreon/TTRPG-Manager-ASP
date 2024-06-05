using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TTRPG_Manager_ASP.Models;
using TTRPG_Manager_ASP.Models.ViewModels;

namespace TTRPG_Manager_ASP.Controllers
{
    public class AventuraController : Controller
    {
        private readonly TtrpgmanagerContext _context;

        public AventuraController(TtrpgmanagerContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Método de inicio y búsqueda
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public async Task<IActionResult> Index(string search, int inProcess)
        {
            if (_context.Aventuras == null)
            {
                return Problem("No hay aventuras");
            }

            var aventuras = from m in _context.Aventuras select m;

            if (!String.IsNullOrEmpty(search)) aventuras = aventuras.Where(s => s.Nombre!.Contains(search));

            if (inProcess == 1) aventuras = aventuras.Where(s => s.EnProceso == true);
            else if (inProcess == 2) aventuras = aventuras.Where(s => s.EnProceso == false);

            return View(await aventuras.ToListAsync());
        }

        public IActionResult Create()
        {
            ViewData["Campanas"] = new SelectList(_context.Campanas, "Id", "Nombre");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AventuraViewModel model)
        {
            if (ModelState.IsValid)
            {
                var aventura = new Aventura()
                {
                    Nombre = model.Nombre,
                    IdCampana = model.IdCampana,
                    EnProceso = false,
                    FechaCreacion = DateTime.Now,
                    Imagen = null,
                    // TODO Arreglar esto
                    ListaEtiquetas = new List<string>(new List<string>().Append("Aventura").Append("Puzles").Append("Misterio")),
                };
                _context.Add(aventura);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["Campanas"] = new SelectList(_context.Campanas, "Id", "Nombre", model.IdCampana);
            return View(model);
        }
    }
}
