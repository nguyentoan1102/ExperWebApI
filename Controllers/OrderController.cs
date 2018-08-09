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
    public class OrderController : ApiController
    {
        IRepository reponsitory;
        public OrderController(IRepository repon)
        {
            reponsitory = repon;
        }
        [HttpGet]
        [Authorize(Roles = "Administrators")]
        public IEnumerable<Order> List()
        {
            return reponsitory.Orders;
        }
        [HttpPost]
        public async Task<IHttpActionResult> CreateOrder(Order order)
        {
            if (ModelState.IsValid)
            {
                IDictionary<int, Product> products = reponsitory.Products
               .Where(p => order.Lines.Select(ol => ol.ProductId)
               .Any(id => id == p.Id)).ToDictionary(p => p.Id);
                order.TotalCost = order.Lines.Sum(ol => ol.Count * products[ol.ProductId].Price);
                await reponsitory.SaveOrderAsync(order);
                return Ok();
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
        [HttpDelete]
        [Authorize(Roles = "Administrators")]
        public async Task DeleteOrder(int id)
        {
            await reponsitory.DeleteOrderAsync(id);
        }
    }
}

