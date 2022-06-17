using BusinessObject.Models;
using DataAccess.Repository;
using Microsoft.AspNetCore.Http;
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
        private readonly IOrderDetailRepository orderDetailRepository;
        private readonly ICustomerRepository customerRepository;

        public OrderHistoryModel(IOrderRepository orderRepository, 
                                ICustomerRepository customerRepository,
                                IOrderDetailRepository orderDetailRepository)
        {
            this.orderRepository = orderRepository;
            this.customerRepository = customerRepository;
            this.orderDetailRepository = orderDetailRepository;
        }

        [BindProperty]
        public IList<OrderViewModel> orderList { get; set; }

        [BindProperty]
        public int TotalProductSold { get; set; }

        [BindProperty]
        public int TotalOrder { get; set; }
        [BindProperty]
        public decimal TotalIncome { get; set; }

        public IActionResult OnGet()
        {
            try
            {
                List<Order> orders;
                string role = HttpContext.Session.GetString("ROLE");
                string email = HttpContext.Session.GetString("EMAIL");
                if(string.IsNullOrEmpty(role))
                {
                    return RedirectToPage("/Login");
                } 
                else if(role == "Admin")
                {
                    TotalIncome = orderRepository.GetTotalIncome();
                    TotalProductSold = orderDetailRepository.GetTotalProductSold();
                    TotalOrder = orderRepository.GetTotalOrder();
                    orders = orderRepository.GetAllOrders().ToList();
                }
                else
                {
                    Customer customer = customerRepository.GetCustomerByEmail(email);
                    orders = orderRepository.GetOrderByCustomerId(customer.CustomerId).ToList();
                }

                
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
    
        public IActionResult OnPost()
        {
            try
            {
                List<Order> orders;
                string role = HttpContext.Session.GetString("ROLE");
                string email = HttpContext.Session.GetString("EMAIL");
                if (string.IsNullOrEmpty(role))
                {
                    return RedirectToPage("/Login");
                }
                else if (role == "Admin")
                {
                    TotalIncome = orderRepository.GetTotalIncome();
                    TotalProductSold = orderDetailRepository.GetTotalProductSold();
                    TotalOrder = orderRepository.GetTotalOrder();
                    orders = orderRepository.GetAllOrders().ToList();
                }
                else
                {
                    Customer customer = customerRepository.GetCustomerByEmail(email);
                    orders = orderRepository.GetOrderByCustomerId(customer.CustomerId).ToList();
                }

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


                orderList = orderList.OrderBy(o => o.OrderDate).ToList();

            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
            }

            return Page();
        }
    }
}

