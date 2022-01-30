
using System.Threading.Tasks;
using Accounting.API.Entities;

namespace Accounting.API.Repositories.Interfaces
{
    public interface ICartRepository
    {
        Task<Cart> UpdateCart(Cart cart);
        Task<Cart> GetCartByUserName(string userName);
        Task DeleteCart(string userName);
    }
}
