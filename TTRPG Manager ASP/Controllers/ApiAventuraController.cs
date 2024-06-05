using Microsoft.AspNetCore.Mvc;
using TTRPG_Manager_ASP.Models;

namespace TTRPG_Manager_ASP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiAventuraController : ControllerBase
    {
        private readonly TtrpgmanagerContext _context;

        public ApiAventuraController(TtrpgmanagerContext context)
        {
            _context = context;
        }

        //public async Task<List<Aventura>> Get()
        //    => await _context.Aventuras.Include(a=>a.IdCampanaNavigation)
        //    .Select(a => new 
        //    { 
        //        Nombre = a.Nombre,
        //        Campana = a.IdCampanaNavigation,
        //    })
        //    .ToListAsync();
    }
}
