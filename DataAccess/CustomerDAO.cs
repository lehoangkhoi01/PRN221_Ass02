using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class CustomerDAO
    {
        private static CustomerDAO instance = null;
        private static readonly object instanceLock = new object();

        private CustomerDAO() { }
        public static CustomerDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new CustomerDAO();
                    }
                }
                return instance;
            }
        }
        //----------------------------------------------

        public Customer Login(string email, string password)
        {
            Customer customer = null;
            try
            {
                var dbContext = new NorthwindCopyDBContext();
                customer = dbContext.Customers.FirstOrDefault(u => u.Email == email && u.Password == password);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return customer;
        }

    }
}
