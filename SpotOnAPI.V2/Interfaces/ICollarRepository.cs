using SpotOnAPI.V2.Models;

namespace SpotOnAPI.V2.Interfaces
{
    public interface ICollarRepository
    {
        Task<Collar> CreateCollar(Collar collar);
        Task<Collar> EditCollar(Collar collar);
        Task<bool> DeleteCollar(Guid id);
        Task<List<Collar>> GetCollars();
        Task<List<Collar>> GetCustomerCollars(int id);
    }
}
