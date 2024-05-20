using SALESSYSTEM.Domain.Entities;

namespace SALESSYSTEM.WebApi.Models.ViewModels
{
    public class VMCategory
    {
        public int IdCategory { get; set; }
        public string? Description { get; set; }
        public int? IsActive { get; set; }
    }
}
