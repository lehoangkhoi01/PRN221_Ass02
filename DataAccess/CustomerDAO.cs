using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
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

        public IEnumerable<Customer> GetCusomerList()
        {
            IEnumerable<Customer> customerList;
            try
            {
                var dbContext = new NorthwindCopyDBContext();
                customerList = dbContext.Customers.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return customerList;
        }

        public Customer GetCustomerById(string id)
        {
            Customer customer;
            try
            {
                var dbContext = new NorthwindCopyDBContext();
                customer = dbContext.Customers.SingleOrDefault(c => c.CustomerId == id);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return customer;
        }

        public Customer GetCustomerByEmail(string email)
        {
            Customer customer;
            try
            {
                var dbContext = new NorthwindCopyDBContext();
                customer = dbContext.Customers.SingleOrDefault(c => c.Email.ToLower().Trim() == email.ToLower().Trim());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return customer;
        }


        public void AddNewCustomer(Customer customer)
        {
            try
            {
                var dbContext = new NorthwindCopyDBContext();
                dbContext.Customers.Add(customer);
                dbContext.SaveChanges();
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void UpdateCustomer(Customer customer)
        {
            try
            {
                var dbContext = new NorthwindCopyDBContext();
                if (!CheckUpdateCustomerEmail(customer))
                {
                    throw new Exception("This email is already existed.");
                }
                else
                {
                    dbContext.Entry<Customer>(customer).State = EntityState.Modified;                 
                    dbContext.SaveChanges();
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private bool CheckUpdateCustomerEmail(Customer customer)
        {
            try
            {
                var dbContext = new NorthwindCopyDBContext();
                List<Customer> customerList = dbContext.Customers.ToList();
                customerList.RemoveAll(c => c.CustomerId == customer.CustomerId);
                if (customerList.Exists(p => p.Email.ToLower().Trim() == customer.Email.ToLower().Trim()))
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return true;
        }

    }
}
