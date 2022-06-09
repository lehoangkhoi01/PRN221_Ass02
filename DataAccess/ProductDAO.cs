using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class ProductDAO
    {
        private static ProductDAO instance = null;
        private static readonly object instanceLock = new object();

        private ProductDAO() { }
        public static ProductDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new ProductDAO();
                    }
                }
                return instance;
            }
        }
        //-------------------------------------------------------------

        public IEnumerable<Product> GetProductList()
        {
            IEnumerable<Product> productList;
            try
            {
                var dbContext = new NorthwindCopyDBContext();
                productList = dbContext.Products.Include(p => p.Category)
                                                .Include(p => p.Supplier)
                                                .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return productList;
        }

        public Product GetProductById(int id)
        {
            Product product;
            try
            {
                var dbContext = new NorthwindCopyDBContext();
                product = dbContext.Products.FirstOrDefault(p => p.ProductId == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return product;
        }

        public Product GetProductByName(string name)
        {
            Product product;
            try
            {
                var dbContext = new NorthwindCopyDBContext();
                product = dbContext.Products.FirstOrDefault(p => p.ProductName.ToLower().Trim() == name.ToLower().Trim());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return product;
        }

        public void AddProduct(Product product)
        {
            try
            {
                var dbContext = new NorthwindCopyDBContext();
                Product _product = dbContext.Products.FirstOrDefault(p => p.ProductName.ToLower().Trim()
                                                                            .Equals(product.ProductName.ToLower().Trim()));

                if(_product == null)
                {
                    dbContext.Products.Add(product);
                    dbContext.SaveChanges();
                }
                else
                {
                    throw new Exception("This product is already exist.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void UpdateProduct(Product product)
        {
            try
            {
                var dbContext = new NorthwindCopyDBContext();
                List<Product> productList = dbContext.Products.ToList();
                productList.Remove(product);
                if(productList.Exists(p => p.ProductName.ToLower().Trim() == product.ProductName.ToLower().Trim()))
                {
                    throw new Exception("This product is already existed.");
                } 
                else
                {
                    dbContext.Products.Update(product);
                    dbContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


    }
}
