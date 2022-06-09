using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class SupplierDAO
    {
        private static SupplierDAO instance = null;
        private static readonly object instanceLock = new object();

        private SupplierDAO() { }
        public static SupplierDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new SupplierDAO();
                    }
                }
                return instance;
            }
        }
        //---------------------------------------------------

        public IEnumerable<Supplier> GetSuppliers()
        {
            IEnumerable<Supplier> suppliers;
            try
            {
                var dbContext = new NorthwindCopyDBContext();
                suppliers = dbContext.Suppliers.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return suppliers;
        }
    }
}
