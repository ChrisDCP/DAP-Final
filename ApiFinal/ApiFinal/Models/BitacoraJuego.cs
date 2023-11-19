using System;
using System.Collections.Generic;

namespace ApiFinal.Models;

public partial class BitacoraJuego
{
    public int Id { get; set; }

    public string? Transaccion { get; set; }

    public string? Usuario { get; set; }

    public string Host { get; set; } = null!;

    public DateTime? FechaMod { get; set; }

    public string Tabla { get; set; } = null!;
}
