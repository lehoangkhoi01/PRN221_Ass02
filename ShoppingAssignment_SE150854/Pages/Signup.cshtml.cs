using BusinessObject.Models;
using DataAccess.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShoppingAssignment_SE150854.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ShoppingAssignment_SE150854.Pages
{
    [BindProperties]
    public class SignupModel : PageModel
    {
        private readonly ICustomerRepository customerRepostiory;

        public SignupModel(ICustomerRepository _customerRepository)
        {
            customerRepostiory = _customerRepository;
        }


        [Display(Name = "Contact name")]
        [Required]
        public string CustomerName { get; set; }

        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "Wrong format for email")]
        [Required]
        public string CustomerEmail { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }


        [StringLength(maximumLength: 100)]
        public string Address { get; set; }


        [Required(ErrorMessage = "Phone can not be empty")]
        [RegularExpression("[0-9]{10}", ErrorMessage = "Wrong format for phone number")]
        public string PhoneNumber { get; set; }


        public IActionResult OnGet()
        {
            string role = HttpContext.Session.GetString("ROLE");
            if(!string.IsNullOrEmpty(role))
            {
                return NotFound();
            }
            return Page();
        }

        public IActionResult OnPost()
        {
            try
            {
                List<Customer> customers = customerRepostiory.GetCustomerList().ToList();
                Customer customer = new Customer
                {
                    CustomerId = GetRandomString.GenerateRandomString(6),
                    ContactName = CustomerName,
                    Address = Address,
                    Email = CustomerEmail,
                    Password = HashingUtil.HashPassword(Password),
                    Phone = PhoneNumber
                };

                while(customers.Exists(c => c.CustomerId == customer.CustomerId))
                {
                    customer.CustomerId = GetRandomString.GenerateRandomString(6);
                }
                customerRepostiory.Signup(customer);

                HttpContext.Session.SetString("EMAIL", customer.Email);
                HttpContext.Session.SetString("ROLE", "Customer");
                return RedirectToPage("./Index");
            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
                return Page();
            }
        }
    }
}
