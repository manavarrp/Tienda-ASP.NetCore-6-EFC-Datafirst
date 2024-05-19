using System;
using System.Collections.Generic;

namespace SALESSYSTEM.Domain.Entities
{
    public partial class Configuration
    {
        public string? Resource { get; set; }
        public string? Property { get; set; }
        public string? Value { get; set; }
    }
}
