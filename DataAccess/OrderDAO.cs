using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class OrderDAO
    {
        private static OrderDAO instance = null;
        private static readonly object instanceLock = new object();

        private OrderDAO() { }
        public static OrderDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new OrderDAO();
                    }
                }
                return instance;
            }
        }
        //-----------------------------------------------------------

        public IEnumerable<Order> GetOrderList()
        {
            IEnumerable<Order> orderList;
            try
            {
                var dbContext = new NorthwindCopyDBContext();
                orderList = dbContext.Orders.Include(p => p.Customer)
                                            .Include(p => p.OrderDetails)
                                                .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return orderList;
        }

        public int GetTotalOrder()
        {
            int result = 0;
            try
            {
                var dbContext = new NorthwindCopyDBContext();
                result = dbContext.Orders.Count();
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }

        public int GetTotalProductSold()
        {
            int result = 0;
            try
            {
                var dbContext = new NorthwindCopyDBContext();
                result = dbContext.Orders.Sum(o => o.OrderDetails.Sum(o => o.Quantity));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        } 

        public decimal GetTotalOrderIncome()
        {
            decimal result = 0;
            try
            {
                var dbContext = new NorthwindCopyDBContext();
                result = (decimal)dbContext.Orders.Sum(o => o.Freight);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }

        public IEnumerable<Order> GetOrdersByCustomerId(string customerId)
        {
            IEnumerable<Order> orderList;
            try
            {
                var dbContext = new NorthwindCopyDBContext();
                orderList = dbContext.Orders.Include(p => p.Customer)
                                                .Include(p => p.OrderDetails)
                                                .Where(p => p.CustomerId == customerId)
                                                .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return orderList;
        }

        public Order GetOrderById(string id)
        {
            Order order;
            try
            {
                var dbContext = new NorthwindCopyDBContext();
                order = dbContext.Orders.Include(p => p.Customer)
                                        .Include(p => p.OrderDetails)
                                        .SingleOrDefault(o => o.OrderId == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return order;
        }

        public void AddOrder(Order order)
        {
            try
            {
                var dbContext = new NorthwindCopyDBContext();
                dbContext.Orders.Add(order);
                dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Update(Order order)
        {
            try
            {
                var dbContext = new NorthwindCopyDBContext();
                dbContext.Entry<Order>(order).State = EntityState.Modified;
                dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


    }
}
