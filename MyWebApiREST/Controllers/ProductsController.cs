using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MyWebApiREST.Models;

namespace MyWebApiREST.Controllers
{
    public class ProductsController : ApiController
    {
        private NorthwindProducts dbContext = new NorthwindProducts();

        // api/products
        [HttpGet]
        public IEnumerable<Product> Get()
        {
            return dbContext.Products.ToList();
        }

        // api/products/1
        [HttpGet]
        public Product Get(int id)
        {
            return dbContext.Products.FirstOrDefault(p => p.ProductID == id);
        }

        // api/products
        [HttpPost]
        public IHttpActionResult CreateProduct([FromBody]Product product)
        {
            if (ModelState.IsValid)
            {
                dbContext.Products.Add(product);
                dbContext.SaveChanges();
                return Ok(product);
            }
            else
            {
                return BadRequest();
            }
        }

        // api/products
        [HttpPut]
        public IHttpActionResult UpdateProduct(int id, [FromBody]Product product)
        {
            if (ModelState.IsValid)
            {
                bool ProductExist = dbContext.Products.Count(p => p.ProductID == id) > 0;

                if(ProductExist)
                {
                    dbContext.Entry(product).State = System.Data.Entity.EntityState.Modified;
                    dbContext.SaveChanges();
                    return Ok(product);
                }
                else
                {
                    return NotFound();
                }
                
            }
            else
            {
                return BadRequest();
            }
        }

        // api/products/1
        [HttpDelete]
        public IHttpActionResult DeleteProduct(int id)
        {
            var prod = dbContext.Products.Find(id);

            if(prod != null)
            {
                dbContext.Products.Remove(prod);
                dbContext.SaveChanges();

                return Ok(prod);
            }
            else
            {
                return NotFound();
            }
        }


    }
}
