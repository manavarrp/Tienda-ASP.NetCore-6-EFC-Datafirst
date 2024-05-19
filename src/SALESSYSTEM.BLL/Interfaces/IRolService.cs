using SALESSYSTEM.Domain.Entities;

namespace SALESSYSTEM.BLL.Interfaces
{
    public interface IRolService
    {
        Task<IEnumerable<Role>> List();
    }
}
