namespace APIReservify.Models
{
    public interface ICitasStoreDataBaseSettings
    {
        string ReservifyCitasCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
