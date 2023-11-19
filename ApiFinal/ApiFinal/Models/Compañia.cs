using System;
using System.Collections.Generic;

namespace ApiFinal.Models;

public partial class Compañia
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual ICollection<Juego> Juegos { get; set; } = new List<Juego>();
}
