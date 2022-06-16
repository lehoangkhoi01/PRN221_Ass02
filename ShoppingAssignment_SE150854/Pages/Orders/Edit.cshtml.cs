using BusinessObject.Models;
using DataAccess.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShoppingAssignment_SE150854.View_Models;
using System;

namespace ShoppingAssignment_SE150854.Pages.Orders
{
    [BindProperties]
    public class EditModel : PageModel
    {
        private readonly IOrderRepository orderRepository;

        public EditModel(IOrderRepository orderRepository)
        {
            this.orderRepository = orderRepository;
        }

        [BindProperty]
        public Order Order { get; set; }

        public IActionResult OnGet(string? id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            string role = HttpContext.Session.GetString("ROLE");
            if(string.IsNullOrEmpty(role))
            {
                return RedirectToPage("/Login");
            }
            else if(role == "Customer")
            {
                return NotFound();
            }

            try
            {
                Order = orderRepository.GetOrderById(id);
                if(Order == null)
                {
                    return NotFound();
                }


            }
            catch(Exception ex)
            {
                TempData["Message"] = ex.Message;
                return RedirectToPage("./OrderHistory");
            }

            return Page();
        }
    
        public IActionResult OnPost()
        {
            if(!string.IsNullOrEmpty(ValidateDateTime()))
            {
                TempData["Message"] = ValidateDateTime();
                return Page();
            }

            try
            {
                orderRepository.UpdateOrder(Order);
            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
                return Page();
            }

            TempData["Message"] = "Update successfully.";
            return Page();
        }

        private string ValidateDateTime()
        {
            if(Order.RequiredDate < Order.OrderDate)
            {
                return "Required date time must be greater than order date time";
            }

            if (Order.ShippedDate < Order.OrderDate)
            {
                return "Shipped date time must be greater than order date time";
            }

            return "";
        }
    }
}
