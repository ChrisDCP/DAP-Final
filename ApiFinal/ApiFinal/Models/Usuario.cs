using System;
using System.Collections.Generic;

namespace ApiFinal.Models;

public class Usuario
{
    public int Id { get; set; }

    public string NombreUsuario { get; set; } = null!;

    public string CorreoElectronico { get; set; } = null!;

    public string Contraseña { get; set; } = null!;
}
