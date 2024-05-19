using System;
using System.Collections.Generic;

namespace SALESSYSTEM.Domain.Entities
{
    public partial class Business
    {
        public int IdBusiness { get; set; }
        public string? LogoUrl { get; set; }
        public string? LogoName { get; set; }
        public string? DocumentNumber { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public decimal? TaxPercentage { get; set; }
        public string? CurrencySymbol { get; set; }
    }
}
