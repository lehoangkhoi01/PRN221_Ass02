using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessObject.Models;
using DataAccess.Repository;
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
