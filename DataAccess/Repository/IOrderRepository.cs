using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface IOrderRepository
    {
        public IEnumerable<Order> GetAllOrders();
        public IEnumerable<Order> GetOrderByCustomerId(string customerId);
        public void AddNewOrder(Order order);
        public void UpdateOrder(Order order);
    }
}
