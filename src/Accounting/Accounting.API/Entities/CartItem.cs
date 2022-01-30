
namespace Accounting.API.Entities
{
    public class CartItem
    {
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string BookTitle { get; set; }
        public string BookId { get; set; }
    }
}
