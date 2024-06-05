using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TTRPG_Manager_ASP.Models;

namespace TTRPG_Manager_ASP.Controllers
{
    public class SistemaController : Controller
    {
        private readonly TtrpgmanagerContext _context;

        public SistemaController(TtrpgmanagerContext context)
        {
            _context = context;
        }

        // GET: Sistema
        public async Task<IActionResult> Index()
        {
            return View(await _context.Sistemas.ToListAsync());
        }

        // GET: Sistema/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sistema = await _context.Sistemas
                .FirstOrDefaultAsync(m => m.Nombre == id);
            if (sistema == null)
            {
                return NotFound();
            }

            return View(sistema);
        }

        // GET: Sistema/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Sistema/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nombre,Edicion,Clase,ListaEtiquetas")] Sistema sistema)
        {
            if (ModelState.IsValid)
            {
                sistema.FechaCreacion = DateTime.Now;
                _context.Add(sistema);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(sistema);
        }

        // GET: Sistema/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sistema = await _context.Sistemas.FindAsync(id);
            if (sistema == null)
            {
                return NotFound();
            }
            return View(sistema);
        }

        // POST: Sistema/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Nombre,Edicion,Clase,ListaEtiquetas")] Sistema sistema)
        {
            if (id != sistema.Nombre)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sistema);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SistemaExists(sistema.Nombre))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(sistema);
        }

        // GET: Sistema/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sistema = await _context.Sistemas
                .FirstOrDefaultAsync(m => m.Nombre == id);
            if (sistema == null)
            {
                return NotFound();
            }

            return View(sistema);
        }

        // POST: Sistema/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var sistema = await _context.Sistemas.FindAsync(id);
            if (sistema != null)
            {
                _context.Sistemas.Remove(sistema);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SistemaExists(string id)
        {
            return _context.Sistemas.Any(e => e.Nombre == id);
        }
    }
}
