using SALESSYSTEM.BLL.Interfaces;
using SALESSYSTEM.DAL.Interfaces;
using SALESSYSTEM.Domain.Entities;

namespace SALESSYSTEM.BLL.Implementation
{
    public class BusinessService : IBussinesService
    {
        private readonly IGenericRepository<Business> _repository;

        public BusinessService(IGenericRepository<Business> repository)
        {
            _repository = repository;
        }

        public async Task<Business> Get()
        {
            try
            {
                Business business = await _repository.Get(n => n.IdBusiness == 1);
                return business;

            }catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<Business> SaveChanges(Business entity)
        {
            try
            {
                Business business = await _repository.Get(n => n.IdBusiness == 1);

                business.DocumentNumber = entity.DocumentNumber;
                business.Name = entity.Name;
                business.Email = entity.Email;
                business.Address = entity.Address;
                business.Phone = entity.Phone;
                business.TaxPercentage = entity.TaxPercentage;
                business.CurrencySymbol = entity.CurrencySymbol;
                business.LogoName = entity.LogoName;
                business.LogoUrl = entity.LogoUrl;

                await _repository.Update(business);
                return business;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
