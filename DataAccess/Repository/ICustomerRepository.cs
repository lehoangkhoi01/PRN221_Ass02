using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface ICustomerRepository
    {
        public Customer Login(string email, string password);
        public void Signup(Customer customer);
        public void Update(Customer customer);
        public IEnumerable<Customer> GetCustomerList();
        public Customer GetCustomerById(string id);
        public Customer GetCustomerByEmail(string email);
    }
}
