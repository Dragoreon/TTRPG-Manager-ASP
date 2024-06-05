using System;
using System.Collections.Generic;

namespace TTRPG_Manager_ASP.Models;

public partial class Aventura
{
    public int Id { get; set; }

    public DateTime FechaCreacion { get; set; }

    public string Nombre { get; set; } = null!;

    public byte[]? Imagen { get; set; }

    public int? IdCampana { get; set; }

    public List<string>? ListaEtiquetas { get; set; }

    public bool? EnProceso { get; set; }

    public virtual ICollection<Entrada> Entrada { get; set; } = new List<Entrada>();

    public virtual Campana? IdCampanaNavigation { get; set; }

    public virtual ICollection<Personaje> IdPersonajes { get; set; } = new List<Personaje>();

    public virtual ICollection<ManualReglas> ManualReglas { get; set; } = new List<ManualReglas>();
}
