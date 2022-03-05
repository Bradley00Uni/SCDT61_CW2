using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineShopDeliveryAPI.Data;
using OnlineShopDeliveryAPI.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShopDeliveryAPI.Controllers
{
    [Route("api/deliveries")]
    [ApiController]
    public class DeliveryController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DeliveryController(ApplicationDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<DeliveryModel>>> GetDeliveries()
        {
            return await _context.Deliveries.Include("Order").ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DeliveryModel>> GetDeliveryModel(int id)
        {
            var DeliveryModel = await _context.Deliveries.Include("Order").Where(x => x.DeliveryId == id).ToListAsync();

            if (DeliveryModel == null)
            {
                return NotFound();
            }

            return DeliveryModel.FirstOrDefault();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutDeliveryModel(int id, DeliveryModel DeliveryModel)
        {
            if (id != DeliveryModel.DeliveryId)
            {
                return BadRequest();
            }

            _context.Entry(DeliveryModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DeliveryModelExists(id))
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
        public async Task<IActionResult> PostDeliveryModel(int id, DeliveryModel DeliveryModel)
        {
            DeliveryModel.Order = await _context.Orders.FindAsync(DeliveryModel.OrderID);
            DeliveryModel.DeliveryId = DeliveryModel.Order.OrderId;

            _context.Deliveries.Add(DeliveryModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDeliveryModel", new { id = DeliveryModel.DeliveryId }, DeliveryModel);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDeliveryModel(int id)
        {
            var delivery = await _context.Deliveries.FindAsync(id);
            if (delivery == null)
            {
                return NotFound();
            }

            _context.Deliveries.Remove(delivery);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DeliveryModelExists(int id)
        {
            return _context.Deliveries.Any(e => e.DeliveryId == id);
        }
    }
}
