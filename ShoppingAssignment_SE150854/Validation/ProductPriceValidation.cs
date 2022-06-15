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
                if(price < 0)
                {
                    ErrorMessage = "Product's price can not be negative";
                    return false;
                }
            }
            catch (Exception ex)
            {
                result = false;
            }
            return result;
        }
    }
}
