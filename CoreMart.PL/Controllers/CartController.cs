using CoreMart.BLL.Repository.Implementation;
using CoreMart.BLL.Repository.Interface;
using CoreMart.DAL.Context;
using CoreMart.DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CoreMart.PL.Controllers
{
    public class CartController : Controller
    {

        private readonly ShoppingCartSessionService _cartService;
        private readonly CoreMartDbContext _dbContext;

        public CartController(ShoppingCartSessionService cartService, CoreMartDbContext dbContext)
        {
            _cartService = cartService;
            _dbContext = dbContext;
        }

        
        public IActionResult CartIndex()
        {
            var cartItems = _cartService.GetCartItems();  
            return View(cartItems);
        }

       
        public IActionResult AddToCart(int productId)
        {
            var product = _dbContext.Products.FirstOrDefault(p => p.Id == productId);
            if (product != null)
            {
                _cartService.AddToCart(product);  
            }

            return RedirectToAction("CartIndex");  
        }

        public IActionResult RemoveFromCart(int productId)
        {
            _cartService.RemoveFromCart(productId);
            return RedirectToAction("CartIndex"); 
        }

        [HttpPost]
        public IActionResult UpdateCart(int productId, int quantity)
        {
            var cartItems = _cartService.GetCartItems();
            var item = cartItems.FirstOrDefault(p => p.ProductId == productId);

            if (item != null)
            {
                item.Count = quantity;  
                _cartService.RemoveFromCart(productId); 
                _cartService.AddToCart(new Product
                {
                    Id = productId,
                    Name = item.Name,
                    Price = item.Price,
                    ImageURL = item.ImageURL
                });  
            }

            return RedirectToAction("CartIndex"); 
        }


    }
}

