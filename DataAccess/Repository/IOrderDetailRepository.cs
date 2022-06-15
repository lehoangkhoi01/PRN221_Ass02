using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface IOrderDetailRepository
    {
        public IEnumerable<OrderDetail> GetOrderDetailsByOrderId(string orderId);
        public void AddOrderDetail(IEnumerable<OrderDetail> orderDetails);    
    }
}
