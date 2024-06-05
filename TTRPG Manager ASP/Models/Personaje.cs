using System;
using System.Collections.Generic;

namespace TTRPG_Manager_ASP.Models;

public partial class Personaje
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Arquetipo { get; set; } = null!;

    public bool EsJugable { get; set; }

    public string? Motivacion { get; set; }

    public byte[]? Ficha { get; set; }

    public byte[]? Imagen { get; set; }

    public byte[]? DicPuntos { get; set; }

    public int? PersonaJugadora { get; set; }

    public virtual Persona? PersonaJugadoraNavigation { get; set; }

    public virtual ICollection<Aventura> IdAventuras { get; set; } = new List<Aventura>();
}
