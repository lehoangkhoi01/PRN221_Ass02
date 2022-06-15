using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObject.Models;
using DataAccess.Repository;
using ShoppingAssignment_SE150854.View_Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace ShoppingAssignment_SE150854.Pages.Products
{
    public class IndexModel : PageModel
    {
        private readonly IProductRepository productRepository;

        public IndexModel(IProductRepository _productRepository)
        {
            productRepository = _productRepository;
        }
        [BindProperty]
        public IList<ProductViewModel> ProductList { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }


        public void OnGetAsync()
        {
            ProductList = new List<ProductViewModel>();
            List<Product> productList = productRepository.GetProductList().ToList();
            string role = HttpContext.Session.GetString("ROLE");
            if(role == "Customer")
            {
                productList = productList.Where(p => p.ProductStatus == 1).ToList();
            }

            if (!string.IsNullOrEmpty(SearchString))
            {
                productList = productList.Where(s => s.ProductName.ToLower().Contains(SearchString.ToLower().Trim()))
                                        .ToList();
            }

            foreach (Product product in productList)
            {
                ProductViewModel productViewModel = new ProductViewModel
                {
                    ProductId = product.ProductId,
                    ProductName = product.ProductName,
                    QuantityPerUnit = product.QuantityPerUnit,
                    ProductImage = product.ProductImage,
                    UnitPrice = product.UnitPrice,
                    Category = product.Category,
                    Supplier = product.Supplier,
                    ProductStatus = bool.Parse(product.ProductStatus.ToString() == "1" ? 
                                            true.ToString() : false.ToString()),
                };
                ProductList.Add(productViewModel);
            }
        }

        public IActionResult OnGetSearch([FromForm] string searchValue)
        {
            if(!String.IsNullOrEmpty(searchValue)) {
                ProductList = ProductList.Where(p => p.ProductName.ToLower().Contains(searchValue.ToLower().Trim())).ToList();
            }
            return Page();
        }

        public IActionResult OnPostAddToCart([FromForm] int productId)
        {
            Product product = productRepository.GetProductById(productId);
            //Check validation for product
            if(product == null)
            {
                return NotFound();
            }

            if(product.QuantityPerUnit == 0)
            {
                ViewData["Messsage"] = "This product is not enough in stock.";
                return RedirectToPage();
            }

            var cart = HttpContext.Session.GetComplexData<List<CartItem>>("CART");
            var item = new CartItem
            {
                ProductId = product.ProductId,
                ProductName = product.ProductName,
                Price = (decimal)product.UnitPrice,
                Quantity = 1,
            };
            if (cart != null)
            {
                if(cart.Exists(i => i.ProductId == product.ProductId))
                {
                    // If item is already exist in cart
                    int index = cart.FindIndex(i => i.ProductId == product.ProductId);
                    if(product.QuantityPerUnit < cart[index].Quantity + 1)
                    {
                        TempData["Message"] = "The product is not enough in stock";
                    }
                    else
                    {
                        cart[index].Quantity += 1;
                        cart[index].Price += (decimal)product.UnitPrice;
                        TempData["Message"] = "Add product to cart successfully";
                    }                    
                }
                else
                {
                    cart.Add(item);
                    TempData["Message"] = "Add product to cart successfully";
                }               
                HttpContext.Session.SetComplexData("CART", cart);                
            }
            else //When cart is null -> create new cart
            {                
                var list = new List<CartItem>();
                list.Add(item);
                HttpContext.Session.SetComplexData("CART", list);
                TempData["Message"] = "Add product to cart successfully";
            }

            return RedirectToPage();
        }
    }
}

#region ExtendSession
public static class SessionExtensions
{
    public static T GetComplexData<T>(this ISession session, string key)
    {
        var data = session.GetString(key);
        if (data == null)
        {
            return default(T);
        }
        return JsonConvert.DeserializeObject<T>(data);
    }

    public static void SetComplexData(this ISession session, string key, object value)
    {
        session.SetString(key, JsonConvert.SerializeObject(value));
    }
}

#endregion
