using System;
using System.Collections.Generic;

namespace APIReservify.Models;

public partial class Negocio
{
    public int IdNegocio { get; set; }

    public string? Categoria { get; set; }

    public string? Nombre { get; set; }

    public string? Direccion { get; set; }

    public string? HoraApertura { get; set; }

    public string? HoraCierre { get; set; }

    public string? Descripcion { get; set; }
    public string? Foto { get; set; }
}
