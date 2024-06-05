using System;
using System.Collections.Generic;

namespace TTRPG_Manager_ASP.Models;

public partial class Sistema
{
    public string Nombre { get; set; } = null!;

    public string Edicion { get; set; } = null!;

    public DateTime FechaCreacion { get; set; }

    public string? Clase { get; set; }

    public byte[]? ListaEtiquetas { get; set; }

    public virtual ICollection<ManualReglas> ManualReglas { get; set; } = new List<ManualReglas>();
}
