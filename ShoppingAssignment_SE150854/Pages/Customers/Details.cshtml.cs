using BusinessObject.Models;
using DataAccess.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;


namespace ShoppingAssignment_SE150854.Pages.Customers
{
    public class DetailsModel : PageModel
    {
        private readonly ICustomerRepository customerRepository;

        public DetailsModel(ICustomerRepository _customerRepository)
        {
            customerRepository = _customerRepository;
        }

        public Customer Customer { get; set; }

        public IActionResult OnGet(string id)
        {
            string role = HttpContext.Session.GetString("ROLE");
            string email = HttpContext.Session.GetString("EMAIL");
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

            Customer = customerRepository.GetCustomerById(id);

            if (Customer == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
