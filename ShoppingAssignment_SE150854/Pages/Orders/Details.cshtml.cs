using BusinessObject.Models;
using DataAccess.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ShoppingAssignment_SE150854.Pages.Orders
{
    public class DetailsModel : PageModel
    {
        private readonly IOrderRepository orderRepository;
        private readonly ICustomerRepository customerRepository;
        private readonly IOrderDetailRepository orderDetailRepository;
        public DetailsModel(IOrderRepository orderRepository,
                            ICustomerRepository customerRepository,
                            IOrderDetailRepository orderDetailRepository)
        {
            this.orderRepository = orderRepository;
            this.customerRepository = customerRepository;
            this.orderDetailRepository = orderDetailRepository;
        }

        [BindProperty]
        public Order Order { get; set; }
        public IActionResult OnGet(string? id)
        {
            if(string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            try
            {
                //Authorize and authentication
                string email = HttpContext.Session.GetString("EMAIL");
                string role = HttpContext.Session.GetString("ROLE");
                if(string.IsNullOrEmpty(role))
                {
                    return RedirectToPage("/Login");
                }
                //else
                //{
                //    if (AuthorizeForOrderDetailPage(email, role, id) == false)
                //    {
                //        return NotFound();
                //    }
                //}
                //-----------------------------------------

                Order = orderRepository.GetOrderById(id);
                foreach (OrderDetail item in Order.OrderDetails)
                {
                    OrderDetail _orderDetail = orderDetailRepository.GetOrderDetailById(item.OrderId, item.ProductId);
                    item.Product = _orderDetail.Product;
                }

                if(Order == null)
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
                return RedirectToPage("./OrderHistory");
            }

            return Page();        
        }

        private bool AuthorizeForOrderDetailPage(string email, string role, string orderId)
        {
            if(role == "Customer")
            {
                try
                {
                    Customer customer = customerRepository.GetCustomerByEmail(email);
                    List<Order> orders = orderRepository.GetOrderByCustomerId(customer.CustomerId).ToList();
                    if (!orders.Exists(o => o.OrderId == orderId))
                    {
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }           
            return true;
        }
    }
}
