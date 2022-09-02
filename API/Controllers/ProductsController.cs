using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DAL.Models;
using DAL.ProductRepositories;
using System.Diagnostics;
using System.Xml.Linq;

namespace API.Controllers
{
    public class ProductsController : ApiController
    {
        //Product[] products = new Product[]
        //{
        //    new Product { Id = 1, Name = "Tomato Soup", Category = "Groceries", Price = 1 },
        //    new Product { Id = 2, Name = "Yo-yo", Category = "Toys", Price = 3.75M },
        //    new Product { Id = 3, Name = "Hammer", Category = "Hardware", Price = 16.99M }
        //};

        string _conStr = ConfigurationManager.ConnectionStrings["Demo"].ConnectionString;

        // GET api/<controller>
        public IHttpActionResult GetAllProducts()
        {

            ProductRepository repo = new ProductRepository(_conStr);

            if(repo != null)
            {
                return Ok(repo.GetAllProducts());
            }
            return NotFound();
        }

        // GET api/<controller>/5
        public IHttpActionResult GetProduct(int id)
        {

            ProductRepository repo = new ProductRepository(_conStr);
            var product = repo.GetProductById(id);
            if (product.Id == 0)
            {
                return NotFound();
            }
            return Ok(product);
        }

        // POST api/<controller>
        public IHttpActionResult Post([FromBody] ProductDbo product)
        {
            ProductRepository repo = new ProductRepository(_conStr);
            int insertedProduct = repo.PostProduct(product);
            product.Id = insertedProduct;
            if(insertedProduct == 0)
            {
                return NotFound();
            }
            return Ok(product);
        }

        // PUT api/<controller>/5
        public IHttpActionResult Put(int id, [FromBody] ProductDbo product)
        {
            ProductRepository repo = new ProductRepository(_conStr);
            int result = repo.UpdateProduct(id, product);

            if(result == 0)
            {
                return NotFound();
            }
            return Ok(result);
        }

        // DELETE api/<controller>/5
        public IHttpActionResult Delete(int id)
        {
            ProductRepository repo = new ProductRepository(_conStr);
            int result = repo.DeleteProduct(id);

            if (result == 0)
            {
                return NotFound();
            }
            return Ok(result);
        }
    }
}