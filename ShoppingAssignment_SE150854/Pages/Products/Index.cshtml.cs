using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObject.Models;
using DataAccess.Repository;

namespace ShoppingAssignment_SE150854.Pages.Products
{
    public class IndexModel : PageModel
    {
        private readonly IProductRepository productRepository;

        public IndexModel(IProductRepository _productRepository)
        {
            productRepository = _productRepository;
        }

        public IList<Product> Product { get;set; }

        public void OnGetAsync()
        {
            Product = productRepository.GetProductList().ToList();
        }
    }
}
