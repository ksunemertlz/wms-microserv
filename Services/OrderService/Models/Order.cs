namespace OrderService.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string OrderNumber { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; } 
        public int ProductId { get; set; }   
        public int Quantity { get; set; }    
    }
}