using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TTRPG_Manager_ASP.Models;
using TTRPG_Manager_ASP.Models.ViewModels;

namespace TTRPG_Manager_ASP.Controllers
{
    public class CampanaController : Controller
    {
        private readonly TtrpgmanagerContext _context;

        public CampanaController(TtrpgmanagerContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Método de inicio y búsqueda
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public async Task<IActionResult> Index(string search)
        {
            if (_context.Campanas == null)
            {
                return Problem("No hay campañas");
            }

            var campanas = from m in _context.Campanas select m;

            if (!String.IsNullOrEmpty(search))
            {
                campanas = campanas.Where(s => s.Nombre!.Contains(search));
            }

            return View(await campanas.ToListAsync());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CampanaViewModel model)
        {
            if (ModelState.IsValid)
            {
                var campana = new Campana()
                {
                    Nombre = model.Nombre,
                    EnProceso = false,
                    FechaCreacion = DateTime.Now,
                    Aventuras = new List<Aventura>(),
                };

                _context.Add(campana);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }
        public async Task<IActionResult> Create()
        {
            return View();
        }

    }
}
