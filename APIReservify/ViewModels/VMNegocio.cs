using APIReservify.Models;
using MongoDB.Bson.Serialization.Attributes;

namespace APIReservify.ViewModels
{
    public class VMNegocio
    {
        public partial class CrearNegocio
        {
            public int IdNegocio { get; set; }
            public int IdUsuario { get; set; }

            public string? Categoria { get; set; }

            public string? Nombre { get; set; }

            public string? Direccion { get; set; }

            public string? HoraApertura { get; set; }

            public string? HoraCierre { get; set; }

            public string? Descripcion { get; set; }
        }

        public partial class CitasUsuario
        {
            public string Id { get; set; } = String.Empty;

            public string Fecha { get; set; } = String.Empty;

            public string Hora { get; set; } = String.Empty;

            public int Id_negocio { get; set; }

            public string? NombreNegocio { get; set; } = String.Empty;
            public string? DireccionNegocio { get; set; } = String.Empty;
            public string? CategoriaNegocio { get; set; } = String.Empty;

            public int Id_usuario { get; set; }
        }

        public partial class CitasNegocio
        {
            public string Id { get; set; } = String.Empty;

            public string Fecha { get; set; } = String.Empty;

            public string Hora { get; set; } = String.Empty;

            public int Id_negocio { get; set; }

            public string? NombreUsuario { get; set; } = String.Empty;

            public int Id_usuario { get; set; }
        }

    }
}
