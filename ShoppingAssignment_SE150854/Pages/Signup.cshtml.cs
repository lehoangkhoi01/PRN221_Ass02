using DataAccess.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace ShoppingAssignment_SE150854.Pages
{
    [BindProperties]
    public class SignupModel : PageModel
    {
        private readonly ICustomerRepository customerRepostiory;

        public SignupModel(ICustomerRepository _customerRepository)
        {
            customerRepostiory = _customerRepository;
        }


        [Display(Name = "Contact name")]
        [Required]
        public string CustomerName { get; set; }

        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "Wrong format for email")]
        [Required]
        public string CustomerEmail { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }


        [StringLength(maximumLength: 100)]
        public string Address { get; set; }


        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }


        public void OnGet()
        {
        }
    }
}
