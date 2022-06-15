using BusinessObject.Models;
using DataAccess.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShoppingAssignment_SE150854.View_Models;
using System.Collections.Generic;
using System.Linq;

namespace ShoppingAssignment_SE150854.Pages.Cart
{
    public class IndexModel : PageModel
    {
        private readonly IProductRepository productRepository;
        public IndexModel(IProductRepository _productRepository)
        {
            productRepository = _productRepository;
        }

        public IList<CartItem> Cart { get; set; }


        public IActionResult OnGet()
        {
            //if (HttpContext.Session.GetString("EMAIL") == null)
            //{
            //    return RedirectToPage("/Login");                
            //}

            var cart = HttpContext.Session.GetComplexData<List<CartItem>>("CART");
            if (cart != null)
            {
                Cart = cart;
            }
            return Page();
        }

        public IActionResult OnGetIncrease(int id)
        {
            //if (HttpContext.Session.GetString("EMAIL") == null)
            //{
            //    return RedirectToPage("/Login");
            //}

            var cart = HttpContext.Session.GetComplexData<List<CartItem>>("CART");
            if (cart == null)
            {
                return RedirectToPage();
            }

            CartItem cartItem = cart.FirstOrDefault(i => i.ProductId == id);
            if (cartItem == null)
            {
                return NotFound();
            }

            Product product = productRepository.GetProductById(id);
            if (product.QuantityPerUnit == 0 || product.QuantityPerUnit < cartItem.Quantity + 1)
            {
                TempData["Message"] = "This product is not enough in stock.";
            }
            else
            {
                cartItem.Quantity += 1;
                cartItem.Price += (decimal)product.UnitPrice;
                HttpContext.Session.SetComplexData("CART", cart);
            }
            return RedirectToPage();
        }

        public IActionResult OnGetDecrease(int id)
        {
            //if (HttpContext.Session.GetString("EMAIL") == null)
            //{
            //    return RedirectToPage("/Login");
            //}

            var cart = HttpContext.Session.GetComplexData<List<CartItem>>("CART");
            if (cart == null)
            {
                return RedirectToPage();
            }

            CartItem cartItem = cart.FirstOrDefault(i => i.ProductId == id);
            if (cartItem == null)
            {
                return NotFound();
            }

            Product product = productRepository.GetProductById(id);
            if (cartItem.Quantity - 1 == 0)
            {
                OnPostRemove(id);
            }
            else
            {
                cartItem.Quantity -= 1;
                cartItem.Price -= (decimal)product.UnitPrice;
                HttpContext.Session.SetComplexData("CART", cart);
            }
            return RedirectToPage();
        }

        public IActionResult OnPostRemove(int productId)
        {
            //if (HttpContext.Session.GetString("EMAIL") == null)
            //{
            //    return RedirectToPage("/Login");
            //}


            var cart = HttpContext.Session.GetComplexData<List<CartItem>>("CART");
            if (cart != null)
            {
                if (cart.Exists(i => i.ProductId == productId))
                {
                    var item = cart.FirstOrDefault(i => i.ProductId == productId);
                    cart.Remove(item);
                    if (cart.Count > 0)
                    {
                        HttpContext.Session.SetComplexData("CART", cart);
                    }
                    else
                    {
                        HttpContext.Session.Remove("CART");
                    }
                }
            }
            return RedirectToPage();
        }
    }
}
