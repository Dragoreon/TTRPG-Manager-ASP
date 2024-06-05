using System;
using System.Collections.Generic;

namespace TTRPG_Manager_ASP.Models;

public partial class Entrada
{
    public int Id { get; set; }

    public DateTime FechaCreacion { get; set; }

    public string Titulo { get; set; } = null!;

    public byte[]? Contenido { get; set; }

    public string? Tipo { get; set; }

    public int? Aventura { get; set; }

    public int? EntradaPadre { get; set; }

    public virtual Aventura? AventuraNavigation { get; set; }

    public virtual Entrada? EntradaPadreNavigation { get; set; }

    public virtual ICollection<Entrada> InverseEntradaPadreNavigation { get; set; } = new List<Entrada>();

    public virtual ICollection<Partida> Partida { get; set; } = new List<Partida>();
}
