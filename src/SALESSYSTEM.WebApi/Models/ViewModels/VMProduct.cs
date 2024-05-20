using SALESSYSTEM.Domain.Entities;

namespace SALESSYSTEM.WebApi.Models.ViewModels
{
    public class VMProduct
    {
        public int IdProduct { get; set; }
        public string? Barcode { get; set; }
        public string? Brand { get; set; }
        public string? Description { get; set; }
        public int? IdCategory { get; set; }
        public string? NameCatgory { get; set; }
        public int? Stock { get; set; }
        public string? ImageUrl { get; set; }
        public string? Price { get; set; }
        public int? IsActive { get; set; }
    }
}
