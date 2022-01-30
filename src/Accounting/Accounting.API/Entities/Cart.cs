using System.Collections.Generic;

namespace Accounting.API.Entities
{
    public class Cart
    {
        public string UserName { get; set; }
        public List<CartItem> Items { get; set; }

        public Cart()
        {
            
        }

        public Cart(string userName)
        {
            UserName = userName;
        }

        public decimal TotalPrice
        {
            get
            {
                decimal total = 0;
                foreach (var cartItem in Items)
                {
                    total += cartItem.Price * cartItem.Quantity;
                }
                return total;
            }
        }
    }
}
