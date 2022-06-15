using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        public Customer GetCustomerByEmail(string email) => CustomerDAO.Instance.GetCustomerByEmail(email);

        public Customer GetCustomerById(string id) => CustomerDAO.Instance.GetCustomerById(id);

        public IEnumerable<Customer> GetCustomerList() => CustomerDAO.Instance.GetCusomerList();

        public Customer Login(string email, string password) => CustomerDAO.Instance.Login(email, password);

        public void Signup(Customer customer) => CustomerDAO.Instance.AddNewCustomer(customer);

        public void Update(Customer customer) => CustomerDAO.Instance.UpdateCustomer(customer);
    }
}
