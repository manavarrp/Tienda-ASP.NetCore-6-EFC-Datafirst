using System;
using System.Collections.Generic;

namespace SALESSYSTEM.Domain.Entities
{
    public partial class CorrelativeNumber
    {
        public int IdCorrelativeNumber { get; set; }
        public int? LastNumber { get; set; }
        public int? DigitCount { get; set; }
        public string? Management { get; set; }
        public DateTime? LastUpdateDate { get; set; }
    }
}
