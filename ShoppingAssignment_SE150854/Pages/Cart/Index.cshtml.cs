//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.RazorPages;
//using ShoppingAssignment_SE150854.Models;
//using ShoppingAssignment_SE150854.View_Models;
//using System.Collections.Generic;
//using System.Linq;

//namespace ShoppingAssignment_SE150854.Pages.Cart
//{
//    public class IndexModel : PageModel
//    {
//        private readonly NorthwindCopyDBContext _context;
//        public IndexModel(NorthwindCopyDBContext context)
//        {
//            _context = context;
//        }

//        public IList<CartItem> Cart { get; set; }


//        public IActionResult OnGet()
//        {
//            //if (HttpContext.Session.GetString("EMAIL") == null)
//            //{
//            //    return RedirectToPage("/Login");                
//            //}

//            var cart = HttpContext.Session.GetComplexData<List<CartItem>>("CART");
//            if(cart != null)
//            {
//                Cart = cart;
//            }
//            return Page();
//        }

//        public IActionResult OnGetIncrease(int id)
//        {
//            //if (HttpContext.Session.GetString("EMAIL") == null)
//            //{
//            //    return RedirectToPage("/Login");
//            //}

//            var cart = HttpContext.Session.GetComplexData<List<CartItem>>("CART");
//            if(cart == null)
//            {
//                return RedirectToPage();
//            }

//            CartItem cartItem = cart.FirstOrDefault(i => i.ProductId == id);
//            if (cartItem == null)
//            {
//                return NotFound();
//            }

//            Product product = _context.Products.FirstOrDefault(i => i.ProductId == id);
//            if (product.QuantityPerUnit == 0 || product.QuantityPerUnit < cartItem.Quantity+1)
//            {
//                ViewData["Message"] = "This product is not enough in stock.";
//            }
//            else
//            {
//                cartItem.Quantity += 1;
//                HttpContext.Session.SetComplexData("CART", cart);               
//            }
//            return RedirectToPage();
//        }

//        public IActionResult OnGetDecrease(int id)
//        {
//            //if (HttpContext.Session.GetString("EMAIL") == null)
//            //{
//            //    return RedirectToPage("/Login");
//            //}

//            var cart = HttpContext.Session.GetComplexData<List<CartItem>>("CART");
//            if (cart == null)
//            {
//                return RedirectToPage();
//            }

//            CartItem cartItem = cart.FirstOrDefault(i => i.ProductId == id);
//            if (cartItem == null)
//            {
//                return NotFound();
//            }

//            Product product = _context.Products.FirstOrDefault(i => i.ProductId == id);
//            if (cartItem.Quantity - 1 == 0)
//            {
//                OnPostRemove(id);
//            }
//            else
//            {
//                cartItem.Quantity -= 1;
//                HttpContext.Session.SetComplexData("CART", cart);
//            }
//            return RedirectToPage();
//        }

//        public IActionResult OnPostRemove(int productId)
//        {
//            //if (HttpContext.Session.GetString("EMAIL") == null)
//            //{
//            //    return RedirectToPage("/Login");
//            //}


//            var cart = HttpContext.Session.GetComplexData<List<CartItem>>("CART");
//            if(cart != null)
//            {
//                if(cart.Exists(i => i.ProductId == productId))
//                {
//                    var item = cart.FirstOrDefault(i => i.ProductId == productId);
//                    cart.Remove(item);
//                    if(cart.Count > 0)
//                    {
//                        HttpContext.Session.SetComplexData("CART", cart);
//                    }
//                    else
//                    {
//                        HttpContext.Session.Remove("CART");
//                    }                   
//                }
//            }
//            return RedirectToPage();
//        }
//    }
//}
