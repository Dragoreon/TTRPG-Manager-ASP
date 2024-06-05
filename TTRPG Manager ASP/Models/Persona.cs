using System;
using System.Collections.Generic;

namespace TTRPG_Manager_ASP.Models;

public partial class Persona
{
    public int Id { get; set; }

    public string Apodo { get; set; } = null!;

    public string? Nombre { get; set; }

    public string? Descripcion { get; set; }

    public string? Objetivo { get; set; }

    public string? Contacto { get; set; }

    public byte[]? Etiquetas { get; set; }

    public virtual ICollection<Personaje> Personajes { get; set; } = new List<Personaje>();
}
