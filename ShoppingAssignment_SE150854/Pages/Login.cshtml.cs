using BusinessObject.Models;
using DataAccess.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using ShoppingAssignment_SE150854.Utils;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ShoppingAssignment_SE150854.Pages
{
    public class LoginModel : PageModel
    {
        private readonly ICustomerRepository customerRepository;
        private readonly IConfiguration configuration;

        public LoginModel(ICustomerRepository _customerRepository, 
                            IConfiguration configuration)
        {
            customerRepository = _customerRepository;
            this.configuration = configuration;
        }

        [BindProperty]
        [Required]
        [EmailAddress(ErrorMessage = "Wrong format for email address")]
        public string Email { get; set; }

        [BindProperty]
        [Required]
        public string Password { get; set; }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                var adminEmail = configuration.GetSection("AdminAccount").GetSection("Email").Value;
                var adminPassword = configuration.GetSection("AdminAccount").GetSection("Password").Value;

                if(Email.Equals(adminEmail) && Password.Equals(adminPassword))
                {
                    HttpContext.Session.SetString("EMAIL", Email);
                    HttpContext.Session.SetString("ROLE", "Admin");
                    return RedirectToPage("./Index");
                }


                Customer customer = customerRepository.Login(Email, HashingUtil.HashPassword(Password));
                if (customer != null)
                {
                    
                    HttpContext.Session.SetString("EMAIL", customer.Email);
                    HttpContext.Session.SetString("ROLE", "Customer");
                    return RedirectToPage("./Index");
                }
                else
                {
                    ViewData["ErrorMessage"] = "Wrong email or password.";
                    return Page();
                }
            }
            else
            {
                return Page();
            }
        }
    }
}
