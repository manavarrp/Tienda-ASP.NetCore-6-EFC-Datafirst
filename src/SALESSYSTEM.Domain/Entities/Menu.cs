using System;
using System.Collections.Generic;

namespace SALESSYSTEM.Domain.Entities
{
    public partial class Menu
    {
        public Menu()
        {
            InverseParent = new HashSet<Menu>();
            RoleMenus = new HashSet<RoleMenu>();
        }

        public int IdMenu { get; set; }
        public string? Description { get; set; }
        public int? ParentId { get; set; }
        public string? Icon { get; set; }
        public string? Controller { get; set; }
        public string? ActionPage { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? RegistrationDate { get; set; }

        public virtual Menu? Parent { get; set; }
        public virtual ICollection<Menu> InverseParent { get; set; }
        public virtual ICollection<RoleMenu> RoleMenus { get; set; }
    }
}
