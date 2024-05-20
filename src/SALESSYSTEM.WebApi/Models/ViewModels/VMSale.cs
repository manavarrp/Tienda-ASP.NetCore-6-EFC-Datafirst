using SALESSYSTEM.Domain.Entities;

namespace SALESSYSTEM.WebApi.Models.ViewModels
{
    public class VMSale
    {
        public int IdSale { get; set; }
        public string? SaleNumber { get; set; }
        public int IdDocumentTypeSale { get; set; }
        public string? DocumentTypeSale { get; set; }
        public int? IdUser { get; set; }
        public string? User {  get; set; }
        public string? ClientDocument { get; set; }
        public string? ClientName { get; set; }
        public string? SubTotal { get; set; }
        public string? TaxTotal { get; set; }
        public string? Total { get; set; }
        public string? RegistrationDate { get; set; }
        public virtual ICollection<VMSaleDetail> SaleDetails { get; set; }
    }
}
