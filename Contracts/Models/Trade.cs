namespace Contracts.Models
{
    public class Trade
    {
        public string Id { get; set; }
        public DateTime Date { get; set; }
        public string Contract { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
    }
}
