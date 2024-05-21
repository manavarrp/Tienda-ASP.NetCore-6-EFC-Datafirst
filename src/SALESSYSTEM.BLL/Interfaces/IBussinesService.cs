using SALESSYSTEM.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SALESSYSTEM.BLL.Interfaces
{
    public interface IBussinesService
    {
        Task<Business> Get();
        Task<Business> SaveChanges(Business entity);
    }
}
