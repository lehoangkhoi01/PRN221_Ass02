using System.ComponentModel.DataAnnotations;

namespace ShoppingAssignment_SE150854.View_Models
{
    public class CustomerViewModel
    {
        public string CustomerId { get; set; }

        [Required(ErrorMessage = "Password can not be empty.")]
        [StringLength(50, 
                ErrorMessage = "Password's length should be between 6-50 characters",
                MinimumLength = 6)]
        public string Password { get; set; }

        [Display(Name = "Contact Name")]
        [Required(ErrorMessage = "Contact name can not be empty")]
        public string ContactName { get; set; }
        public string Address { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        [EmailAddress(ErrorMessage = "Wrong format for email address")]
        [Required(ErrorMessage = "Email can not be empty")]
        public string Email { get; set; }
    }
}
