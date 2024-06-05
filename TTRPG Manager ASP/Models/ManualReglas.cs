using System;
using System.Collections.Generic;

namespace TTRPG_Manager_ASP.Models;

public partial class ManualReglas
{
    public int Id { get; set; }

    public string? Titulo { get; set; }

    public string? NombreSistema { get; set; }

    public string? EdicionSistema { get; set; }

    public DateTime FechaCreacion { get; set; }

    public string? Tipo { get; set; }

    public byte[]? Archivo { get; set; }

    public byte[]? DicBookmarks { get; set; }

    public virtual Sistema? Sistema { get; set; }

    public virtual ICollection<Aventura> Aventuras { get; set; } = new List<Aventura>();
}
