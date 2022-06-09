using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class ProductRepository : IProductRepository
    {
        public void AddProduct(Product product) => ProductDAO.Instance.AddProduct(product);

        public Product GetProductById(int id) => ProductDAO.Instance.GetProductById(id);

        public Product GetProductByName(string name) => ProductDAO.Instance.GetProductByName(name);

        public IEnumerable<Product> GetProductList() => ProductDAO.Instance.GetProductList();

        public void UpdateProduct(Product product) => ProductDAO.Instance.UpdateProduct(product);
    }
}
