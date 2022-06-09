//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.RazorPages;
//using Microsoft.AspNetCore.Mvc.Rendering;
//using Microsoft.EntityFrameworkCore;
//using ShoppingAssignment_SE150854.Models;

//namespace ShoppingAssignment_SE150854.Pages.Customers
//{
//    [BindProperties]
//    public class EditModel : PageModel
//    {
//        private readonly NorthwindCopyDBContext _context;

//        public EditModel(NorthwindCopyDBContext context)
//        {
//            _context = context;
//        }

//        [BindProperty]
//        public Customer Customer { get; set; }

//        public string CustomerId { get; set; }

//        [DataType(DataType.Password)]
//        [Required(ErrorMessage = "Password can not be empty")]
//        [StringLength(maximumLength: 20, 
//                        ErrorMessage = "Password's lenght must be between 6-20", 
//                        MinimumLength = 6)]
//        public string Password { get; set; }

//        [Required(ErrorMessage = "Contact name can not be empty")]
//        [Display(Name = "Contact Name")]
//        [StringLength(maximumLength: 20,
//                        MinimumLength = 2,
//                        ErrorMessage = "Contact name's length must be between 2-20 characters")]
//        public string ContactName { get; set; }


//        [Required(ErrorMessage = "Address can not be empty")]
//        [StringLength(maximumLength: 50, 
//                        ErrorMessage = "Address's length", 
//                        MinimumLength = 10)]
//        public string Address { get; set; }


//        [Required(ErrorMessage = "Phone can not be empty")]
//        [RegularExpression("[0-9]{10}", ErrorMessage = "Wrong format for phone number")]
//        public string Phone { get; set; }


//        [EmailAddress(ErrorMessage = "Wrong format for email address")]
//        [Required(ErrorMessage = "Email can not be empty")]
//        public string Email { get; set; }

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
//            else
//            {
//                CustomerId = Customer.CustomerId;
//                Password = Customer.Password;
//                ContactName = Customer.ContactName;
//                Phone = Customer.Phone;
//                Address = Customer.Address;
//                Email = Customer.Email;
//            }
//            return Page();
//        }

//        // To protect from overposting attacks, enable the specific properties you want to bind to.
//        // For more details, see https://aka.ms/RazorPagesCRUD.
//        public async Task<IActionResult> OnPostAsync()
//        {
//            if (!ModelState.IsValid)
//            {
//                return Page();
//            }
//            var customer = await _context.Customers.FirstOrDefaultAsync(m => m.CustomerId == CustomerId);
//            customer.ContactName = ContactName;
//            customer.Phone = Phone;
//            customer.Address = Address;
//            customer.Email = Email;
//            customer.Password = Password;

//            _context.Attach(customer).State = EntityState.Modified;

//            try
//            {
//                await _context.SaveChangesAsync();
//            }
//            catch (DbUpdateConcurrencyException ex)
//            {
//                if (!CustomerExists(Customer.CustomerId))
//                {
//                    return NotFound();
//                }
//                else
//                {
//                    ViewData["Message"] = ex.Message;
//                    return Page();
//                }
//            }

//            return RedirectToPage("./Index");
//        }

//        private bool CustomerExists(string id)
//        {
//            return _context.Customers.Any(e => e.CustomerId == id);
//        }
//    }
//}
