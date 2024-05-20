using SALESSYSTEM.Domain.Entities;

namespace SALESSYSTEM.WebApi.Models.ViewModels
{
    public class VMMenu
    {
        public string? Description { get; set; }
        public string? Icon { get; set; }
        public string? Controller { get; set; }
        public string? ActionPage { get; set; }
        public virtual ICollection<VMMenu> SubMenus { get; set; }
    }
}
