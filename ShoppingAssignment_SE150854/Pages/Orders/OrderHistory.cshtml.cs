using BusinessObject.Models;
using DataAccess.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShoppingAssignment_SE150854.View_Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ShoppingAssignment_SE150854.Pages.Orders
{
    public class OrderHistoryModel : PageModel
    {
        private readonly IOrderRepository orderRepository;

        public OrderHistoryModel(IOrderRepository orderRepository)
        {
            this.orderRepository = orderRepository;
        }

        [BindProperty]
        public IList<OrderViewModel> orderList { get; set; }

        public IActionResult OnGet()
        {
            try
            {
                List<Order> orders = orderRepository.GetAllOrders().ToList();
                orderList = new List<OrderViewModel>();
                foreach (Order order in orders)
                {
                    OrderViewModel orderViewModel = new OrderViewModel
                    {
                        OrderId = order.OrderId,
                        CustomerId = order.CustomerId,
                        Customer = order.Customer,
                        Freight = order.Freight,
                        OrderDate = order.OrderDate,
                        ShippedDate = order.ShippedDate,
                        RequiredDate = order.RequiredDate,
                        ShipAddress = order.ShipAddress,
                        OrderDetails = order.OrderDetails,
                    };
                    orderList.Add(orderViewModel);
                }
            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
            }
            return Page();
        }
    }
}
