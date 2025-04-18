using CoreMart.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreMart.BLL.Repository.Interface
{
    public interface IShppingCartRepository
    {
        //Task<IEnumerable<ShoppingCart>> GetAllAsync();
        //Task<ShoppingCart> GetByIdAsync(int? id);
        //Task<IEnumerable<ShoppingCart>> GetByCategoryIdAsync(int? categoryId);
        //Task AddtoCartAsync(ShoppingCart cart);
        //Task UpdatetoCartAsync(ShoppingCart cart);
        //Task DeletefromCartAsync(ShoppingCart cart);

        List<ShoppingCart> GetCartItems();
        void AddToCart(Product product);
        void RemoveFromCart(int productId);
        void ClearCart();
    }
}
