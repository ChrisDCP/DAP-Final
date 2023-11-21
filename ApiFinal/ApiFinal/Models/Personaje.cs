using System;
using System.Collections.Generic;

namespace ApiFinal.Models;

public class Personaje
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Descripcion { get; set; }

    public int JuegoId { get; set; }

}
