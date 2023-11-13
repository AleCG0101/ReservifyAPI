namespace APIReservify.Models
{
    public class CitasStoreDatabaseSettings : ICitasStoreDataBaseSettings
    {
        public string ReservifyCitasCollectionName { get; set; } = String.Empty;
        public string ConnectionString { get; set; } = String.Empty;
        public string DatabaseName { get; set; } = String.Empty;
    }
}
