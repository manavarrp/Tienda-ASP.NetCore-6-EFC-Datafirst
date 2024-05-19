using System;
using System.Collections.Generic;

namespace SALESSYSTEM.Domain.Entities
{
    public partial class Sale
    {
        public Sale()
        {
            SaleDetails = new HashSet<SaleDetail>();
        }

        public int IdSale { get; set; }
        public string? SaleNumber { get; set; }
        public int? IdDocumentTypeSale { get; set; }
        public int? IdUser { get; set; }
        public string? ClientDocument { get; set; }
        public string? ClientName { get; set; }
        public decimal? SubTotal { get; set; }
        public decimal? TaxTotal { get; set; }
        public decimal? Total { get; set; }
        public DateTime? RegistrationDate { get; set; }

        public virtual DocumentTypeSale? IdDocumentTypeSaleNavigation { get; set; }
        public virtual User? IdUserNavigation { get; set; }
        public virtual ICollection<SaleDetail> SaleDetails { get; set; }
    }
}
