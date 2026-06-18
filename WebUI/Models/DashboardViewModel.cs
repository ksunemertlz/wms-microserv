namespace WebUI.Models
{
    public class DashboardViewModel
    {
        public int ProductCount { get; set; }
        public int OrderCount { get; set; }
        public int LowStockCount { get; set; }
        public int TotalStockValue { get; set; }  // суммарная стоимость (если есть цена)
        public int TodayOrdersCount { get; set; }
        public List<Order> RecentOrders { get; set; } = new();
        public List<StockAlert> LowStockItems { get; set; } = new();
    }

    public class StockAlert
    {
        public string ProductName { get; set; }
        public int Quantity { get; set; }
    }
}