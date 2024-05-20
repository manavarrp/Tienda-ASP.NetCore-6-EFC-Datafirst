namespace SALESSYSTEM.WebApi.Models.ViewModels
{
    public class VMUser
    {
        public int IdUser { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public int? IdRole { get; set; }
        public string? NameRole { get; set; }
        public string? PhotoUrl { get; set; }
        public int? IsActive { get; set; }
    }
}
