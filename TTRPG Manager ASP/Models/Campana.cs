using System;
using System.Collections.Generic;

namespace TTRPG_Manager_ASP.Models;

public partial class Campana
{
    public int Id { get; set; }

    public DateTime FechaCreacion { get; set; }

    public string Nombre { get; set; } = null!;

    public bool EnProceso { get; set; }

    public virtual ICollection<Aventura> Aventuras { get; set; } = new List<Aventura>();
}
