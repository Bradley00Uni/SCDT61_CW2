﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineShopDeliveryAPI.Data;
using OnlineShopDeliveryAPI.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShopDeliveryAPI.Controllers
{
    [Route("api/orders")]
    [ApiController]
    public class OrderController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OrderController(ApplicationDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderModel>>> GetOrders()
        {
            return await _context.Orders.ToListAsync();
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDetailsModel>> GetOrderModel(int id)
        {
            var OrderModel = await _context.Orders.FindAsync(id);

            if (OrderModel == null)
            {
                return NotFound();
            }

            var products = await _context.Products.Where(x => x.OrderID == OrderModel.OrderId).ToListAsync();
            var deliveries = await _context.Deliveries.Where(x => x.OrderID == OrderModel.OrderId).ToListAsync();
            var delivery = deliveries.FirstOrDefault();

            return new OrderDetailsModel() { Order = OrderModel, Products = products, Delivery = delivery };
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrderModel(int id, OrderModel OrderModel)
        {
            if (id != OrderModel.OrderId)
            {
                return BadRequest();
            }

            _context.Entry(OrderModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderModelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> PostOrderModel(int id, OrderModel OrderModel)
        {
            OrderModel.OrderPlaced = System.DateTime.Now;
            _context.Orders.Add(OrderModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrderModel", new { id = OrderModel.OrderId }, OrderModel);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrderModel(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            var products = await _context.Products.Where(x => x.OrderID == order.OrderId).ToListAsync();

            if(products != null)
            {
                _context.Products.RemoveRange(products);
            }

            var deliveries = await _context.Deliveries.Where(x => x.OrderID == order.OrderId).ToListAsync();

            if(deliveries != null)
            {
                _context.Deliveries.RemoveRange(deliveries);
            }

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OrderModelExists(int id)
        {
            return _context.Orders.Any(e => e.OrderId == id);
        }
    }
}
