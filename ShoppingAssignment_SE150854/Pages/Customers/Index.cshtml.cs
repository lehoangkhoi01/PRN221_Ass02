using System;
using System.Collections.Generic;
using System.Linq;
using BusinessObject.Models;
using DataAccess.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ShoppingAssignment_SE150854.Pages.Customers
{
    public class IndexModel : PageModel
    {
        private readonly ICustomerRepository customerRepository;

        public IndexModel(ICustomerRepository _customerRepository)
        {
            customerRepository = _customerRepository;
        }

        public IList<Customer> Customer { get; set; }

        public IActionResult OnGet()
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

            try
            {
                Customer = customerRepository.GetCustomerList().ToList();
            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
            }
            return Page();
        }
    }
}
