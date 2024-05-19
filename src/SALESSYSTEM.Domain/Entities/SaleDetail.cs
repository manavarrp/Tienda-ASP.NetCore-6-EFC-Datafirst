using System;
using System.Collections.Generic;

namespace SALESSYSTEM.Domain.Entities
{
    public partial class SaleDetail
    {
        public int IdSaleDetail { get; set; }
        public int? IdSale { get; set; }
        public int? IdProduct { get; set; }
        public string? ProductBrand { get; set; }
        public string? ProductDescription { get; set; }
        public string? ProductCategory { get; set; }
        public int? Quantity { get; set; }
        public decimal? Price { get; set; }
        public decimal? Total { get; set; }

        public virtual Sale? IdSaleNavigation { get; set; }
    }
}
