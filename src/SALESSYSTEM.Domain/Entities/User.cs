using System;
using System.Collections.Generic;

namespace SALESSYSTEM.Domain.Entities
{
    public partial class User
    {
        public User()
        {
            Sales = new HashSet<Sale>();
        }

        public int IdUser { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public int? IdRole { get; set; }
        public string? Password { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? RegistrationDate { get; set; }

        public virtual Role? IdRoleNavigation { get; set; }
        public virtual ICollection<Sale> Sales { get; set; }
    }
}