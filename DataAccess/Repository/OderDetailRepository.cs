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
        public OrderDetail GetOrderDetailById(string orderId, int productId) => OrderDetailDAO.Instance.GetOrderDetailById(orderId, productId);
        public IEnumerable<OrderDetail> GetOrderDetailsByOrderId(string orderId) => OrderDetailDAO.Instance.GetOrderDetailsByOrderId(orderId);

        public int GetTotalProductSold() => OrderDetailDAO.Instance.GetTotalProductSold();
    }
}
