using SALESSYSTEM.BLL.Interfaces;
using SALESSYSTEM.DAL.Interfaces;
using SALESSYSTEM.Domain.Entities;

namespace SALESSYSTEM.BLL.Implementation
{
    public class RolService : IRolService
    {
        private readonly IGenericRepository<Role> _repository;

        public RolService(IGenericRepository<Role> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Role>> List()
        {

           
                IQueryable<Role> query = await _repository.GetEntityQuery();
                return query.ToList();


        }
    }
}
