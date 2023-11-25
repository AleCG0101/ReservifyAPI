using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace APIReservify.Models
{
    public class Citas
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = String.Empty;

        [BsonElement("fecha")]
        public string Fecha { get; set; } = String.Empty;

        [BsonElement("hora")]
        public string Hora { get; set; } = String.Empty;

        [BsonElement("id_negocio")]
        public int Id_negocio { get; set; }

        [BsonElement("id_usuario")]
        public int Id_usuario { get; set; }
    }
}
