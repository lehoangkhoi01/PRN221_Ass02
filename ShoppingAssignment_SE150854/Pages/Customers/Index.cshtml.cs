using System;
using System.Collections.Generic;
using System.Linq;
using BusinessObject.Models;
using DataAccess.Repository;
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

        public void OnGet()
        {
            try
            {
                Customer = customerRepository.GetCustomerList().ToList();
            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
            }            
        }
    }
}
