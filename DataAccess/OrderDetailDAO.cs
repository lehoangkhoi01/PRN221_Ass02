using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class OrderDetailDAO
    {
        private static OrderDetailDAO instance = null;
        private static readonly object instanceLock = new object();

        private OrderDetailDAO() { }
        public static OrderDetailDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new OrderDetailDAO();
                    }
                }
                return instance;
            }
        }
        //----------------------------------------

        public IEnumerable<OrderDetail> GetOrdersByOrderId(string orderId)
        {
            IEnumerable<OrderDetail> orderList;
            try
            {
                var dbContext = new NorthwindCopyDBContext();
                orderList = dbContext.OrderDetails.Where(o => o.OrderId == orderId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return orderList;
        }

        public void AddNewOrderDetail(IEnumerable<OrderDetail> orderDetails)
        {
            try
            {
                var dbContext = new NorthwindCopyDBContext();
                dbContext.OrderDetails.AddRange(orderDetails);
                dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
