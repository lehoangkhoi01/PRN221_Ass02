using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BusinessObject.Models;
using ShoppingAssignment_SE150854.View_Models;
using DataAccess.Repository;
using System.ComponentModel.DataAnnotations;
using ShoppingAssignment_SE150854.Validation;
using Microsoft.AspNetCore.Http;
using ShoppingAssignment_SE150854.Utils.FileUploadService;

namespace ShoppingAssignment_SE150854.Pages.Products
{
    public class EditModel : PageModel
    {
        private readonly IProductRepository productRepository;
        private readonly ICategoryRepository categoryRepository;
        private readonly ISupplierRepository supplierRepository;
        private readonly IFileUploadService fileUploadService;

        public EditModel(IProductRepository _productRepository, 
                        ICategoryRepository _categoryRepository, 
                        ISupplierRepository _supplierRepository,
                        IFileUploadService _fileUploadService)
        {
            productRepository = _productRepository;
            categoryRepository = _categoryRepository;
            supplierRepository = _supplierRepository;
            fileUploadService = _fileUploadService;
        }

        [BindProperty]
        public Product Product { get; set; }

        [BindProperty]
        public ProductViewModel ProductViewModel { get; set; }

        [Display(Name = "Choose an image to upload (optional)")]
        [DataType(DataType.Upload)]
        [ProductImageValidation(ErrorMessage = "Wrong format for image")]
        public IFormFile ImageFile { get; set; }

        public IActionResult OnGet(int? id)
        {
            string role = HttpContext.Session.GetString("ROLE");
            if(string.IsNullOrEmpty(role))
            {
                return RedirectToPage("/Login");
            }
            else if(role != "Admin")
            {
                return NotFound();
            }

            if (id == null)
            {
                return NotFound();
            }

            Product product = productRepository.GetProductById((int)id);

            if (product == null)
            {
                return NotFound();
            } 
            else
            {
                ProductViewModel = new ProductViewModel
                {
                    ProductId = product.ProductId,
                    ProductName = product.ProductName,
                    ProductStatus = bool.Parse(product.ProductStatus == 1 ? true.ToString() : false.ToString()),
                    QuantityPerUnit = product.QuantityPerUnit,
                    ProductImage = product.ProductImage,
                    UnitPrice = product.UnitPrice,
                    SupplierId = product.SupplierId,
                    CategoryId = product.CategoryId,
                };
            } 

            List<Category> categories = categoryRepository.GetCategories().ToList();
            List<Supplier> suppliers = supplierRepository.GetSuppliers().ToList();

            ViewData["CategoryId"] = new SelectList(categories, "CategoryId", "CategoryName");
            ViewData["SupplierId"] = new SelectList(suppliers, "SupplierId", "CompanyName");
            return Page();
        }

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

            try
            {
                Product product = new Product
                {
                    ProductId = ProductViewModel.ProductId,
                    ProductName = ProductViewModel.ProductName,
                    CategoryId = ProductViewModel.CategoryId,
                    SupplierId = ProductViewModel.SupplierId,
                    QuantityPerUnit = ProductViewModel.QuantityPerUnit,
                    UnitPrice = ProductViewModel.UnitPrice,
                    ProductStatus = byte.Parse(ProductViewModel.ProductStatus ? 1.ToString() : 0.ToString()),
                    ProductImage = ProductViewModel.ProductImage,
                };

                if (ImageFile != null)
                {
                    await fileUploadService.UploadFileAsync(ImageFile);
                    product.ProductImage = ImageFile.FileName;
                }
                productRepository.UpdateProduct(product);
            }
            catch (Exception ex)
            {
                IEnumerable<Category> categories = categoryRepository.GetCategories();
                IEnumerable<Supplier> suppliers = supplierRepository.GetSuppliers();
                ViewData["CategoryId"] = new SelectList(categories, "CategoryId", "CategoryName");
                ViewData["SupplierId"] = new SelectList(suppliers, "SupplierId", "CompanyName");
                TempData["Message"] = ex.Message;
                return Page();
            }

            return RedirectToPage("./Index");
        }

    }
}
