using BusinessObject.Models;
using DataAccess.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ShoppingAssignment_SE150854.Pages
{
    public class LoginModel : PageModel
    {
        private readonly ICustomerRepository customerRepository;

        public LoginModel(ICustomerRepository _customerRepository)
        {
            customerRepository = _customerRepository;
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
                Customer customer = customerRepository.Login(Email, Password);
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
