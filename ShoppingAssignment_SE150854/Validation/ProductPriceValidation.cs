using System;
using System.ComponentModel.DataAnnotations;

namespace ShoppingAssignment_SE150854.Validation
{
    public class ProductPriceValidation : ValidationAttribute
    {
        public ProductPriceValidation()
        {
            ErrorMessage = "Wrong format for product's price";
        }

        public override bool IsValid(object value)
        {
            bool result = true;
            try
            {
                decimal price = decimal.Parse(value.ToString());
            }
            catch (Exception ex)
            {
                result = false;
            }
            return result;
        }
    }
}
