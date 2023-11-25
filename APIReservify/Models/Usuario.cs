using System;
using System.Collections.Generic;

namespace APIReservify.Models;

public partial class Usuario
{
    public int IdUsuario { get; set; }

    public int? IdNegocio { get; set; }

    public string Nombre { get; set; } = null!;

    public string Apellidos { get; set; } = null!;

    public string Correo { get; set; } = null!;

    public string Pass { get; set; } = null!;

    public string? Telefono { get; set; }
}
