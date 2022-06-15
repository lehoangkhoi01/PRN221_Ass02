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
        public Order GetOrderById(string id);
        public void AddNewOrder(Order order);
        public void UpdateOrder(Order order);
        public int GetTotalOrder();
        public int GetTotalProductSold();
        public decimal GetTotalIncome();
    }
}
