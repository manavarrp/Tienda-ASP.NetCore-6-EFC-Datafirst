using Microsoft.EntityFrameworkCore;
using SALESSYSTEM.DAL.Context;
using SALESSYSTEM.DAL.Interfaces;
using SALESSYSTEM.Domain.Entities;

namespace SALESSYSTEM.DAL.Implementation
{
    public class SaleRepository : GenericRepository<Sale>,  ISaleRepository
    {
        private readonly SALESSYSDBContext _context;

        public SaleRepository(SALESSYSDBContext context): base(context) 
        {
            _context = context;
        }

        public async Task<Sale> Register(Sale entity)
        {
            var saleGenerated = new Sale();

            using (var transaction= _context.Database.BeginTransaction())
            {
                try
                {
                    foreach (SaleDetail sd  in entity.SaleDetails) 
                    { 
                        Product product = _context.Products.Where(
                            p=>p.IdProduct == sd.IdProduct).First();

                        product.Stock = product.Stock - sd.Quantity;
                        _context.Products.Update(product);

                        await _context.SaveChangesAsync();

                        CorrelativeNumber correlative = _context.CorrelativeNumbers
                            .Where(n => n.Management == "venta").First();

                        correlative.LastNumber = correlative.LastNumber + 1;
                        correlative.LastUpdateDate = DateTime.Now;

                        string zeros = string.Concat(Enumerable.Repeat("0", correlative.DigitCount!.Value));
                        string saleNumber = zeros + correlative.LastNumber.ToString();
                        saleNumber = saleNumber.Substring(saleNumber.Length - correlative.DigitCount.Value, correlative.DigitCount.Value);

                        entity.SaleNumber = saleNumber;

                        await _context.AddAsync(entity);
                        await _context.SaveChangesAsync();

                        saleGenerated = entity;

                        transaction.Commit();


                    }

                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    throw e;
    
                }

            }
            return saleGenerated;
           

        }

        public async Task<IEnumerable<SaleDetail>> Report(DateTime startTime, DateTime endTime)
        {
            IEnumerable<SaleDetail> listResume = await _context.SaleDetails
                .Include(s => s.IdSaleNavigation)
                .ThenInclude(u =>u.IdUserNavigation)
                .Include(v => v.IdSaleNavigation)
                .ThenInclude(tsd => tsd.IdDocumentTypeSaleNavigation)
                .Where(sd =>sd.IdSaleNavigation!.RegistrationDate!.Value.Date >= startTime.Date &&
                sd.IdSaleNavigation.RegistrationDate.Value.Date <= endTime.Date)
                .ToListAsync();

            return listResume;
        }
    }
}
