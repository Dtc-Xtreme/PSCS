using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PSCS.API.Models;
using PSCS.Domain;
using PSCS.Infrastructure.Repositories;

namespace PSCS.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : Controller
    {
        private readonly IOrderRepsository orderRepsository;

        public OrderController(IOrderRepsository repository)
        {
            this.orderRepsository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            IList<Order> orders = await orderRepsository.Orders.ToListAsync();
            return Ok(orders == null ? NotFound() : orders);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> FindById(int id)
        {
            Order? order = await orderRepsository.FindById(id);

            return Ok(order == null ? NotFound() : order);
        }

        [HttpPost]
        public async Task<IActionResult> Create(OrderDTO order)
        {
            Order? newOrder= null;

            if (ModelState.IsValid)
            {
                newOrder = new Order
                {
                    ZoneId = order.ZoneId
                };

                foreach(OrderLineDTO ol in order.OrderLines)
                {
                    newOrder.OrderLines.Add(
                        new OrderLine
                        {
                            ProductId = ol.ProductId,
                            Quantity = ol.Quantity,
                        }
                    );
                }
            }
            return Ok(await orderRepsository.Create(newOrder) == false ? NotFound() : newOrder);
        }

        [HttpDelete("id")]
        public async Task<IActionResult> Remove(int id)
        {
            return Ok(await orderRepsository.Remove(id) == false ? NotFound() : "Order is removed!");
        }
    }
}