using System;
using System.Collections.Generic;

namespace ApiFinal.Models;

public class Juego
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Descripcion { get; set; }

    public DateTime FechaLanzamiento { get; set; }

    public int CompañiaId { get; set; }

}
