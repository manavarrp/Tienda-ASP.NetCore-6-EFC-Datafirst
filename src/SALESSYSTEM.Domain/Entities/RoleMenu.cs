using System;
using System.Collections.Generic;

namespace SALESSYSTEM.Domain.Entities
{
    public partial class RoleMenu
    {
        public int IdRoleMenu { get; set; }
        public int? IdRole { get; set; }
        public int? IdMenu { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? RegistrationDate { get; set; }

        public virtual Menu? IdMenuNavigation { get; set; }
        public virtual Role? IdRoleNavigation { get; set; }
    }
}
