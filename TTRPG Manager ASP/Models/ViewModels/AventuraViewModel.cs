using System.ComponentModel.DataAnnotations;

namespace TTRPG_Manager_ASP.Models.ViewModels
{
    public class AventuraViewModel
    {
        [Required]
        public string Nombre { get; set; } = null!;

        [Required]
        [Display(Name = "Campaña")]
        public int? IdCampana { get; set; }

        public int Id { get; set; }

        [Display(Name = "Fecha de creación")]
        public DateTime FechaCreacion { get; set; }

        //public Image Imagen{ get; set; }

        [Display(Name = "En proceso")]
        public bool EnProceso { get; set; }

        public List<string>? ListaEtiquetas { get; set; }

    }
}
