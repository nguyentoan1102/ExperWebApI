using SportsStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace SportsStore.Controllers
{
    public class ProductsController : ApiController
    {
        IRepository repository;
        public ProductsController(IRepository repon)
        {
            //repository = new ProductRepository();
            repository = repon;
        }
        public IEnumerable<Product> GetProducts() => repository.Products;
        public IHttpActionResult GetProduct(int id)
        {
            Product result = repository.Products.Where(p => p.Id == id).FirstOrDefault();

            return result == null ? (IHttpActionResult)BadRequest("No Product Found") : Ok(result);
        }

        public async Task<IHttpActionResult> PostProduct(Product product)
        {
            if (ModelState.IsValid)
            {
                await repository.SaveProductAsync(product);
                return Ok();
            }
            else
            {
                return BadRequest(ModelState);
            }

        }
        [Authorize(Roles = "Administrators")]
        public async Task DeleteProduct(int id) => await repository.DeleteProductAsync(id);
    }
}
