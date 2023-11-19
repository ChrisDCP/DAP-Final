using System;
using System.Collections.Generic;

namespace ApiFinal.Models;

public partial class Personaje
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }

    public int? JuegoId { get; set; }

    public virtual Juego? Juego { get; set; }
}
