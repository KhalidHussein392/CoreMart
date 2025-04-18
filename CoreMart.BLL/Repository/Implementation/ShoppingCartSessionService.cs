using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using CoreMart.BLL.Repository.Interface;
using CoreMart.DAL.Models;
using Microsoft.AspNetCore.Http;

namespace CoreMart.BLL.Repository.Implementation
{
    public class ShoppingCartSessionService : IShppingCartRepository
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private const string CartSessionKey = "CartSession";

        public ShoppingCartSessionService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public List<ShoppingCart> GetCartItems()
        {
            var session = _httpContextAccessor.HttpContext.Session;
            var cartJson = session.GetString(CartSessionKey);
            return cartJson == null
                ? new List<ShoppingCart>()
                : JsonSerializer.Deserialize<List<ShoppingCart>>(cartJson);
        }

        // إضافة منتج للسلة
        public void AddToCart(Product product)
        {
            var cart = GetCartItems();
            var item = cart.FirstOrDefault(p => p.ProductId == product.Id);

            if (item != null)
                item.Count++; 
            else
                cart.Add(new ShoppingCart
                {
                    ProductId = product.Id,
                    Name = product.Name,
                    Price = product.Price,
                    ImageURL = product.ImageURL,
                    Count = 1
                });

            SaveCart(cart);  
        }

        public void RemoveFromCart(int productId)
        {
            var cart = GetCartItems();
            var item = cart.FirstOrDefault(p => p.ProductId == productId);
            if (item != null)
            {
                cart.Remove(item);  
                SaveCart(cart); 
            }
        }

      
        public void ClearCart()
        {
            SaveCart(new List<ShoppingCart>());
        }

        
        private void SaveCart(List<ShoppingCart> cart)
        {
            var session = _httpContextAccessor.HttpContext.Session;
            session.SetString(CartSessionKey, JsonSerializer.Serialize(cart));
        }

    }
}
