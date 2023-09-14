using SOTI.Project.DAL;
using SOTI.Project.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SOTI.Project.WebAPI.Controllers
{
    public class ProductsController : ApiController
    {
        private readonly IProduct product = null;
        public ProductsController()
        {
            product = new ProductDetails();
        }



        [HttpGet]
        public IHttpActionResult GetProducts()
        {
            var ds = product.GetAllProducts();
            if (ds == null)
            {
                return BadRequest();
            }
            return Ok(ds);
        }

        [HttpGet]
        public IHttpActionResult GetProductsById( int id)
        {
            Product ds = product.GetProductById(id);
            if(ds==null)
            {
                return BadRequest();
            }
            return Ok(ds);
        }

        [HttpPost]
        public IHttpActionResult AddProducts([FromBody] Product pro)
        {
            var v = product.AddProduct(pro.ProductName,pro.UnitPrice.Value,pro.UnitsInStock.Value);
            if (v)
            {
                return Ok(v);
            }
            return BadRequest();
        }


        [HttpPut]
        public IHttpActionResult Update([FromUri] int id, [FromBody] Product pro)
        {
            var v = product.UpdateProduct(id, pro.UnitPrice.Value, pro.UnitsInStock.Value);
            if (v)
            {
                return Ok(v);
            }
            return BadRequest();
        }


        [HttpDelete]
        public IHttpActionResult Delete([FromUri] int id)
        {
            var v = product.DeleteProduct(id);
            if (v)
            {
                return Ok(v);
            }
            return BadRequest();
        }

        public IHttpActionResult GetData()
        {
            return Json(new { Message = "Welcome" });
        }
    }
}
