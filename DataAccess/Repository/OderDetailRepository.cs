using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class OderDetailRepository : IOrderDetailRepository
    {
        public void AddOrderDetail(IEnumerable<OrderDetail> orderDetails) => OrderDetailDAO.Instance.AddNewOrderDetail(orderDetails);

        public IEnumerable<OrderDetail> GetOrderDetailsByOrderId(string orderId) => OrderDetailDAO.Instance.GetOrdersByOrderId(orderId);
    }
}
