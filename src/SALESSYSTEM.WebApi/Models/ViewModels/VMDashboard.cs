namespace SALESSYSTEM.WebApi.Models.ViewModels
{
    public class VMDashboard
    {
        public int TotalSales { get; set; }
        public string? TotalIncome { get; set; }
        public int TotalProducts { get; set; }
        public int TotalCategories { get; set; }

        public List<VMSaleWeekly> LastSaleWeekly { get; set; }
        public List<VMProductWekly> LastTopProductWekly { get; set; }
    }
}
