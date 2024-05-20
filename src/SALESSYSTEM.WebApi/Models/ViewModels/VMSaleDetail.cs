using SALESSYSTEM.Domain.Entities;

namespace SALESSYSTEM.WebApi.Models.ViewModels
{
    public class VMSaleDetail
    {
        public int? IdProduct { get; set; }
        public string? ProductBrand { get; set; }
        public string? ProductDescription { get; set; }
        public string? ProductCategory { get; set; }
        public int? Quantity { get; set; }
        public string? Price { get; set; }
        public string? Total { get; set; }
    }
}
