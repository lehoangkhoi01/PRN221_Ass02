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
//    public class DetailsModel : PageModel
//    {
//        private readonly ShoppingAssignment_SE150854.Models.NorthwindCopyDBContext _context;

//        public DetailsModel(ShoppingAssignment_SE150854.Models.NorthwindCopyDBContext context)
//        {
//            _context = context;
//        }

//        public Customer Customer { get; set; }

//        public async Task<IActionResult> OnGetAsync(string id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }

//            Customer = await _context.Customers.FirstOrDefaultAsync(m => m.CustomerId == id);

//            if (Customer == null)
//            {
//                return NotFound();
//            }
//            return Page();
//        }
//    }
//}
