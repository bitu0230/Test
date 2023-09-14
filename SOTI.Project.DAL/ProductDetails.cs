using SOTI.Project.DAL.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOTI.Project.DAL
{
    public class ProductDetails : IProduct
    {
        private SqlConnection con = null;
        private SqlDataAdapter adapter = null;

        public bool AddProduct( string productName, decimal unitPrice, int unitsInStock)
        {
            using (con = new SqlConnection(SqlConnectionStrings.GetConnectionString))
            {
                using (adapter = new SqlDataAdapter("select productName,UnitPrice,UnitsInStock from Products", con))
                {
                    using (DataSet ds = new DataSet())
                    {
                        try
                        {
                            adapter.Fill(ds, "Products");
                            SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
                            adapter.InsertCommand = builder.GetInsertCommand();
                            DataRow dr = ds.Tables[0].NewRow();
                            dr["productName"] = productName;
                            dr["unitPrice"] = unitPrice;
                            dr["unitsInStock"] = unitsInStock;

                            ds.Tables[0].Rows.Add(dr);
                            adapter.Update(ds, "Products");
                            return true;
                        }
                        catch(Exception e)
                        {
                            return false;
                        }

                    }
                }
            }
        }

        public bool DeleteProduct(int id)
        {
            using (con = new SqlConnection(SqlConnectionStrings.GetConnectionString))
            {
                using (adapter = new SqlDataAdapter("select productId,UnitPrice,UnitsInStock from Products", con))
                {
                    using (DataSet ds = new DataSet())
                    {
                        try
                        {
                            adapter.Fill(ds, "Products");
                            //adapter.FillSchema(ds.Tables["Products"], SchemaType.Source);
                            SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
                            adapter.UpdateCommand = builder.GetUpdateCommand();
                            //var product = ds.Tables[0].Rows.Find(id);
                            // use this or linq 
                            var product = ds.Tables[0].AsEnumerable().FirstOrDefault(p => p.Field<int>("productId") == id);
                            if (product != null)
                            {
                                product.Delete();// if exist delete
                                adapter.Update(ds, "Products");
                                return true;
                            }
                            return false;
                        }
                        catch (Exception)
                        {
                            return false;
                        }

                    }
                }
            }
        }

        public List<Product> GetAllProducts()
        {
            return GetProduct().Tables["Products"].AsEnumerable().Select(x => new Product
            {
                ProductName = x.Field<string>("ProductName"),
                ProductId = x.Field<int>("productId"),
                UnitsInStock = x.Field<short?>("unitsInStock"),
                UnitPrice = x.Field<decimal?>("unitPrice")
            }
                ).ToList();
        }

        public DataSet GetProduct()
        {
            using (con = new SqlConnection(SqlConnectionStrings.GetConnectionString))
            {
                using (adapter = new SqlDataAdapter ("select * from Products",con))
                {
                    using (DataSet ds= new DataSet())
                    {
                        adapter.Fill(ds,"Products");
                        return ds;
                    }
                }
            }
        }

        public Product GetProductById(int id)
        {

            return GetProduct().Tables["Products"].AsEnumerable().Select(x => new Product
            {
                ProductName = x.Field<string>("ProductName"),
                ProductId = x.Field<int>("productId"),
                UnitsInStock = x.Field<short?>("unitsInStock"),
                UnitPrice = x.Field<decimal?>("unitPrice")
            }
                ).FirstOrDefault(x => x.ProductId == id);

            // or use this 
            using (con = new SqlConnection(SqlConnectionStrings.GetConnectionString))
            {
                using (adapter = new SqlDataAdapter("select * from Products", con))
                {
                    using (DataSet ds = new DataSet())
                    {
                        //adapter.SelectCommand.Parameters.AddWithValue("@Id", id);
                        //adapter.Fill(ds, "Products");
                        //return ds;
                        // use linq
                        // return 

                       // can't return data row so dont use this way 
                        adapter.FillSchema(ds, SchemaType.Source);
                        
                    }
                }
            }
        }

        public bool UpdateProduct(int id, decimal? unitPrice, short? unitsInStock)
        {
            using (con = new SqlConnection(SqlConnectionStrings.GetConnectionString))
            {
                using (adapter = new SqlDataAdapter("select productId,UnitPrice,UnitsInStock from Products", con))
                {
                    using (DataSet ds = new DataSet())
                    {
                        try
                        {
                            adapter.Fill(ds, "Products");
                            adapter.FillSchema(ds.Tables["Products"], SchemaType.Source);
                            SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
                            adapter.UpdateCommand = builder.GetUpdateCommand();
                            var product = ds.Tables[0].Rows.Find(id); 
                            // use this or linq 
                            //var prod = ds.Tables[0].AsEnumerable().FirstOrDefault(p => p.Field<int>("productId") == id);
                            if(product!=null)
                            {
                                product.BeginEdit();
                                product["UnitPrice"] = unitPrice == null ? product["unitPrice"] : unitPrice;
                                product["unitsInStock"] = unitsInStock == null ? product["unitsInStock"] : unitsInStock;
                                product.EndEdit();
                                adapter.Update(ds, "Products");
                                return true;
                            }
                            return false;
                        }
                        catch (Exception )
                        {
                            return false;
                        }

                    }
                }
            }
        }

        
    }
}
