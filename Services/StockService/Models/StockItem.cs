namespace StockService.Models
{
    public class StockItem
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string Location { get; set; }
        public int Quantity { get; set; }
    }
}
