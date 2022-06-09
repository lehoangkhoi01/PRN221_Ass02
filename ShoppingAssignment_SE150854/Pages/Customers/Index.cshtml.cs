//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.RazorPages;
//using Microsoft.EntityFrameworkCore;
//using ShoppingAssignment_SE150854.Models;

//namespace ShoppingAssignment_SE150854.Pages.Customers
//{
//    public class IndexModel : PageModel
//    {
//        private readonly NorthwindCopyDBContext _context;

//        public IndexModel(NorthwindCopyDBContext context)
//        {
//            _context = context;
//        }

//        public IList<Customer> Customer { get;set; }

//        public async Task OnGetAsync()
//        {
//            Customer = await _context.Customers.ToListAsync();
//        }
//    }
//}
