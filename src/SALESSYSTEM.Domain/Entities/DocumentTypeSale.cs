using System;
using System.Collections.Generic;

namespace SALESSYSTEM.Domain.Entities
{
    public partial class DocumentTypeSale
    {
        public DocumentTypeSale()
        {
            Sales = new HashSet<Sale>();
        }

        public int IdDocumentTypeSale { get; set; }
        public string? Description { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? RegistrationDate { get; set; }

        public virtual ICollection<Sale> Sales { get; set; }
    }
}
