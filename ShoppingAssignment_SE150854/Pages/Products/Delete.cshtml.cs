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

namespace ShoppingAssignment_SE150854.Pages.Products
{
    public class DeleteModel : PageModel
    {
        private readonly IProductRepository productRepository;

        public DeleteModel(IProductRepository _productRepository)
        {
            productRepository = _productRepository;
        }

        [BindProperty]
        public Product Product { get; set; }

        public ProductViewModel productViewModel  { get; set; }

        public IActionResult OnGet(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Product = productRepository.GetProductById((int)id);

            if (Product == null)
            {
                return NotFound();
            }
            else
            {
                productViewModel = new ProductViewModel
                {
                    ProductName = Product.ProductName,
                    ProductId = Product.ProductId,
                    UnitPrice = Product.UnitPrice,
                    QuantityPerUnit = Product.QuantityPerUnit,
                    CategoryId = Product.CategoryId,
                    SupplierId = Product.SupplierId,
                    Supplier = Product.Supplier,
                    Category = Product.Category,
                    ProductStatus = bool.Parse(Product.ProductStatus == 1 ? true.ToString() : false.ToString()),
                    ProductImage = Product.ProductImage
                };
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Product product = productRepository.GetProductById((int)id);


            if (product != null)
            {
                try
                {
                    productRepository.DeleteProduct((int)id);
                }
                catch (Exception ex)
                {
                    TempData["Message"] = ex.Message;
                    return Page();
                }               
            } 
            else
            {
                return NotFound();
            }
            return RedirectToPage("./Index");
        }
    }
}
