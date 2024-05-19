using SALESSYSTEM.Domain.Entities;

namespace SALESSYSTEM.DAL.Interfaces
{
    public interface ISaleRepository : IGenericRepository<Sale>
    {
        Task<Sale> Register(Sale entity);

        Task<IEnumerable<SaleDetail>> Report(DateTime startTime, DateTime endTime);
    }
}
