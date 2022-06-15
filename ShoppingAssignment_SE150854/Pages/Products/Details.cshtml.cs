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
    public class DetailsModel : PageModel
    {
        private readonly IProductRepository productRepository;

        public DetailsModel(IProductRepository _productRepository)
        {
            productRepository = _productRepository;
        }

        public ProductViewModel Product { get; set; }

        public async Task<IActionResult> OnGet(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Product _product = productRepository.GetProductById((int)id);

            if (_product == null)
            {
                return NotFound();
            }

            Product = new ProductViewModel
            {
                ProductId = _product.ProductId,
                ProductName = _product.ProductName,
                ProductImage = _product.ProductImage,
                QuantityPerUnit = _product.QuantityPerUnit,
                UnitPrice = _product.UnitPrice,
                Category = _product.Category,
                Supplier = _product.Supplier,
                ProductStatus = bool.Parse(_product.ProductStatus == 1 ?
                                                true.ToString() : false.ToString())
            };
                 
            return Page();
        }
    }
}
