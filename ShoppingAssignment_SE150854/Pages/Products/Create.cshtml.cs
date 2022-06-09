﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BusinessObject.Models;
using ShoppingAssignment_SE150854.View_Models;
using DataAccess.Repository;
using ShoppingAssignment_SE150854.Utils.FileUploadService;

namespace ShoppingAssignment_SE150854.Pages.Products
{
    public class CreateModel : PageModel
    {
        private readonly IProductRepository productRepository;
        private readonly ISupplierRepository supplierRepository;
        private readonly ICategoryRepository categoryRepository;
        private readonly IFileUploadService fileUploadService;

        public CreateModel(IProductRepository _productRepository, 
                           ISupplierRepository _supplierRepository,
                           ICategoryRepository _categoryRepository,
                           IFileUploadService _fileUploadService)
        {
            productRepository = _productRepository;
            supplierRepository = _supplierRepository;
            categoryRepository = _categoryRepository;
            fileUploadService = _fileUploadService;
        }

        public IActionResult OnGet()
        {
            IEnumerable<Category> categories = categoryRepository.GetCategories();
            IEnumerable<Supplier> suppliers = supplierRepository.GetSuppliers();
            ViewData["CategoryId"] = new SelectList(categories, "CategoryId", "CategoryName");
            ViewData["SupplierId"] = new SelectList(suppliers, "SupplierId", "CompanyName");
            return Page();
        }

        [BindProperty]
        public Product Product { get; set; }
        [BindProperty]
        public ProductViewModel ProductViewModel { get; set;}

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                IEnumerable<Category> categories = categoryRepository.GetCategories();
                IEnumerable<Supplier> suppliers = supplierRepository.GetSuppliers();
                ViewData["CategoryId"] = new SelectList(categories, "CategoryId", "CategoryName");
                ViewData["SupplierId"] = new SelectList(suppliers, "SupplierId", "CompanyName");
                return Page();
            }

            var _product = productRepository.GetProductByName(ProductViewModel.ProductName);
            if (_product == null)
            {
                try
                {
                    string filePath = await fileUploadService.UploadFileAsync(ProductViewModel.ImageFile);
                    Product product = new Product
                    {
                        ProductName = ProductViewModel.ProductName,
                        CategoryId = ProductViewModel.CategoryId,
                        SupplierId = ProductViewModel.SupplierId,
                        QuantityPerUnit = ProductViewModel.QuantityPerUnit,
                        UnitPrice = ProductViewModel.UnitPrice,
                        ProductStatus = byte.Parse(ProductViewModel.ProductStatus ? 1.ToString() : 0.ToString()),
                        ProductImage = ProductViewModel.ImageFile.FileName
                    };
                    productRepository.AddProduct(product);
                }
                catch (Exception ex)
                {
                    IEnumerable<Category> categories = categoryRepository.GetCategories();
                    IEnumerable<Supplier> suppliers = supplierRepository.GetSuppliers();
                    ViewData["CategoryId"] = new SelectList(categories, "CategoryId", "CategoryName");
                    ViewData["SupplierId"] = new SelectList(suppliers, "SupplierId", "CompanyName");
                    ViewData["ErrorMessage"] = ex.Message;
                    return Page();
                }                               
            }
            else
            {
                ViewData["ErrorMessage"] = "This product is already existed.";
            }

            return RedirectToPage("./Index");
        }
    }
}
