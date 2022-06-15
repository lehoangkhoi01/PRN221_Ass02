using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using BusinessObject.Models;
using DataAccess.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ShoppingAssignment_SE150854.Utils;

namespace ShoppingAssignment_SE150854.Pages.Customers
{
    [BindProperties]
    public class EditModel : PageModel
    {
        private readonly ICustomerRepository customerRepository;

        public EditModel(ICustomerRepository customerRepository)
        {
            this.customerRepository = customerRepository;
        }

        [BindProperty]
        public Customer Customer { get; set; }

        public string CustomerId { get; set; }

        [Required(ErrorMessage = "Please enter your current password.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }


        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        [StringLength(maximumLength: 20,
                        ErrorMessage = "Password's lenght must be between 6-20",
                        MinimumLength = 6)]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Contact name can not be empty")]
        [Display(Name = "Contact Name")]
        [StringLength(maximumLength: 20,
                        MinimumLength = 2,
                        ErrorMessage = "Contact name's length must be between 2-20 characters")]
        public string ContactName { get; set; }


        [Required(ErrorMessage = "Address can not be empty")]
        [StringLength(maximumLength: 50,
                        ErrorMessage = "Address's length",
                        MinimumLength = 10)]
        public string Address { get; set; }


        [Required(ErrorMessage = "Phone can not be empty")]
        [RegularExpression("[0-9]{10}", ErrorMessage = "Wrong format for phone number")]
        public string Phone { get; set; }


        [EmailAddress(ErrorMessage = "Wrong format for email address")]
        [Required(ErrorMessage = "Email can not be empty")]
        public string Email { get; set; }

        public IActionResult OnGet()
        {
            string email = HttpContext.Session.GetString("EMAIL");
            string role = HttpContext.Session.GetString("ROLE");
            if(string.IsNullOrEmpty(role))
            {
                return RedirectToPage("/Login");
            }
            else if(role == "Admin")
            {
                return NotFound();
            }

            Customer customer = customerRepository.GetCustomerByEmail(email);
            if (customer == null)
            {
                return NotFound();
            }

            
            CustomerId = customer.CustomerId;
            ContactName = customer.ContactName;
            Phone = customer.Phone;
            Address = customer.Address;
            Email = customer.Email;
            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                Customer currentCustomer = customerRepository.GetCustomerById(CustomerId);
                if (!string.Equals(currentCustomer.Password, HashingUtil.HashPassword(Password)))
                {
                    TempData["Message"] = "Your current password is not valid";
                    return Page();
                }

                Customer customer = new Customer
                {
                    ContactName = ContactName,
                    Phone = Phone,
                    Address = Address,
                    CustomerId = CustomerId,
                    Email = Email,
                    Password = string.IsNullOrEmpty(NewPassword) ? 
                                HashingUtil.HashPassword(Password) : HashingUtil.HashPassword(NewPassword)
                };

                customerRepository.Update(customer);
            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
                return Page();
            }

            TempData["Message"] = "Update successfully.";
            return Page();
        }

    }
}
