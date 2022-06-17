using BusinessObject.Models;
using DataAccess.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShoppingAssignment_SE150854.Utils;
using ShoppingAssignment_SE150854.View_Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ShoppingAssignment_SE150854.Pages.Cart
{
    public class CheckoutModel : PageModel
    {
        private readonly IOrderRepository orderRepository;
        private readonly IOrderDetailRepository orderDetailRepository;
        private readonly IProductRepository productRepository;
        private readonly ICustomerRepository customerRepository;

        public CheckoutModel(IOrderRepository orderRepository,
                            IOrderDetailRepository orderDetailRepository,
                            IProductRepository productRepository,
                            ICustomerRepository customerRepository)
        {
            this.orderRepository = orderRepository;
            this.orderDetailRepository = orderDetailRepository;
            this.productRepository = productRepository;
            this.customerRepository = customerRepository;
        }

        public IList<CartItem> Cart { get; set; }


        [Required(ErrorMessage = "Shipping address can not be empty")]
        [StringLength(maximumLength: 50,
                        MinimumLength = 6,
                        ErrorMessage = "Shipping address's length must be between 6-50 characters")]
        [BindProperty]
        [Display(Name = "Shipping address")]
        public string ShippingAddress { get; set; }

        [BindProperty]
        public decimal TotalPrice { get; set; }


        public IActionResult OnGet()
        {
            string customerEmail = HttpContext.Session.GetString("EMAIL");
            if (string.IsNullOrEmpty(customerEmail))
            {
                return RedirectToPage("/Login");
            }

            var cart = HttpContext.Session.GetComplexData<List<CartItem>>("CART");
            if (cart != null)
            {
                TotalPrice = GetTotalPrice(cart);
                Cart = cart;
            }
            else
            {
                return RedirectToPage("./Index");
            }
            return Page();
        }

        private decimal GetTotalPrice(List<CartItem> cart)
        {
            decimal result = 0;
            foreach (CartItem item in cart)
            {
                result += item.Price;
            }
            return result;
        }

        public IActionResult OnPost()
        {
            try
            {
                Customer customer;
                string customerEmail = HttpContext.Session.GetString("EMAIL");
                if (string.IsNullOrEmpty(customerEmail))
                {
                    return RedirectToPage("/Login");
                }
                else
                {
                    customer = customerRepository.GetCustomerByEmail(customerEmail);
                }


                var cart = HttpContext.Session.GetComplexData<List<CartItem>>("CART");
                if (!ModelState.IsValid)
                {

                    if (cart != null)
                    {
                        TotalPrice = GetTotalPrice(cart);
                        Cart = cart;
                    }
                    else
                    {
                        return RedirectToPage("./Index");
                    }
                    return Page();
                }

                var resultCheckProductQuantity = ProcessCheckingQuantity(cart);
                if (!String.IsNullOrEmpty(resultCheckProductQuantity))
                {
                    Cart = cart;
                    TotalPrice = GetTotalPrice(cart);
                    TempData["Message"] = resultCheckProductQuantity;
                    return Page();
                }

                Order order = new Order
                {
                    OrderId = GetRandomString.GenerateRandomString(),
                    CustomerId = customer.CustomerId,
                    Freight = TotalPrice,
                    OrderDate = DateTime.Now,
                    ShipAddress = ShippingAddress,
                };


                List<Order> orderList = orderRepository.GetAllOrders().ToList();
                while (orderList.Exists(o => o.OrderId.Equals(order.OrderId)))
                {
                    order.OrderId = GetRandomString.GenerateRandomString();
                }

                //Add order 
                orderRepository.AddNewOrder(order);
                List<OrderDetail> orderDetails = new List<OrderDetail>();
                foreach (CartItem cartItem in cart)
                {
                    OrderDetail orderDetail = new OrderDetail
                    {
                        OrderId = order.OrderId,
                        ProductId = cartItem.ProductId,
                        Quantity = (short)cartItem.Quantity,
                        UnitPrice = cartItem.Price / cartItem.Quantity,
                    };
                    orderDetails.Add(orderDetail);
                }
                if (orderDetails.Count > 0)
                {
                    //Add order details
                    orderDetailRepository.AddOrderDetail(orderDetails);
                }

                //Update product's quantity in stock
                UpdateProductQuantityStock(cart);

                //Delete cart after checkout
                HttpContext.Session.Remove("CART");
            }

            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
                return Page();
            }

            TempData["Message"] = "Order successfully";
            return RedirectToPage("/Orders/OrderHistory");
        }


        //--------------------------------------------------------------

        private void UpdateProductQuantityStock(List<CartItem> cart)
        {
            foreach (CartItem cartItem in cart)
            {
                Product product = productRepository.GetProductById(cartItem.ProductId);
                product.QuantityPerUnit -= cartItem.Quantity;
                productRepository.UpdateProduct(product);
            }
        }

        private string ProcessCheckingQuantity(List<CartItem> cart)
        {
            string result = "";
            foreach (CartItem cartItem in cart)
            {
                Product product = productRepository.GetProductById(cartItem.ProductId);
                if (product.QuantityPerUnit < cartItem.Quantity)
                {
                    result = $"The product ${product.ProductName} does not have enough quantity in stock(${product.QuantityPerUnit})";
                    break;
                }
            }
            return result;
        }


    }
}
