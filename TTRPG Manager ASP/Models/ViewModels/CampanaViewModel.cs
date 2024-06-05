using System.ComponentModel.DataAnnotations;

namespace TTRPG_Manager_ASP.Models.ViewModels
{
    public class CampanaViewModel
    {
        [Required]
        public string Nombre { get; set; } = null!;

        public int Id { get; set; }

        [Display(Name = "Fecha de creación")]
        public DateTime FechaCreacion { get; set; }

        [Display(Name = "En proceso")]
        public bool EnProceso { get; set; }
    }
}
