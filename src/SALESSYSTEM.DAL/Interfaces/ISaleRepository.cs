using SALESSYSTEM.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SALESSYSTEM.DAL.Interfaces
{
    public interface ISaleRepository : IGenericRepository<Sale>
    {
        Task<Sale> Register(Sale entity);

        Task<IEnumerable<SaleDetail>> Report(DateTime startTime, DateTime endTime);
    }
}
