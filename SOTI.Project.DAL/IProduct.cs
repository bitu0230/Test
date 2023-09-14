using SOTI.Project.DAL.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOTI.Project.DAL
{
    public interface IProduct
    {
        DataSet GetProduct();
        Product GetProductById(int id);
        List<Product> GetAllProducts();
        bool AddProduct( string productName, decimal unitPrice, int unitsInStock);
        bool  DeleteProduct(int id);
        bool UpdateProduct(int id,decimal? unitPrice,short? unitsInStock);

    }
}
