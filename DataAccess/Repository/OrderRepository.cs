﻿using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class OrderRepository : IOrderRepository
    {
        public void AddNewOrder(Order order) => OrderDAO.Instance.AddOrder(order);  

        public IEnumerable<Order> GetAllOrders() => OrderDAO.Instance.GetOrderList();

        public IEnumerable<Order> GetOrderByCustomerId(string customerId) 
            => OrderDAO.Instance.GetOrdersByCustomerId(customerId);

        public void UpdateOrder(Order order) => OrderDAO.Instance.Update(order);
    }
}
