using System.ComponentModel.DataAnnotations;
using BusinessObject.Models;
using Microsoft.AspNetCore.Http;
using ShoppingAssignment_SE150854.Validation;

namespace ShoppingAssignment_SE150854.View_Models
{
    public class ProductViewModel
    {
        [Display(Name = "Product ID")]
        public int ProductId { get; set; }


        [Display(Name = "Product's name")]
        [Required (ErrorMessage = "Product's name can not be empty")]
        [StringLength(maximumLength: 30,
                      MinimumLength = 5,
                      ErrorMessage = "Product name's lenght must be between 5-30 characters")]
        public string ProductName { get; set; }


        [Display(Name = "Supplier")]
        [Required(ErrorMessage = "Supplier can not be empty")]
        public int? SupplierId { get; set; }


        [Display(Name = "Category")]
        [Required(ErrorMessage = "Category can not be empty")]
        public int? CategoryId { get; set; }


        [Display(Name = "Quantity")]
        [RegularExpression("[0-9]{1,10}", ErrorMessage = "Wrong format for product's quantity")]
        [Required(ErrorMessage = "Quantity can not be empty")]
        public int? QuantityPerUnit { get; set; }

        [Display(Name = "Price")]
        [Required(ErrorMessage = "Price can not be empty")]
        [ProductPriceValidation (ErrorMessage = "Wrong format for product's price")]
        public decimal? UnitPrice { get; set; }


        [Display(Name = "Product's image")]
        public string ProductImage { get; set; }

        


        [Display(Name = "Status")]
        public bool ProductStatus { get; set; }

        [Display(Name = "Category")]
        public Category Category { get; set; }

        [Display(Name = "Supplier")]
        public Supplier Supplier { get; set; }
    }
}
