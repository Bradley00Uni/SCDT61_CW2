using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OnlineShop2022.Data;
using OnlineShop2022.Models;

namespace OnlineShop2022
{
    [Authorize(Roles = "Manager")]
    [Area("Admin")]
    public class OrderController : Controller
    {
        private readonly AppDbContext _context;
        static readonly HttpClient client = new HttpClient();

        public OrderController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Order
        public async Task<IActionResult> Index()
        {
            return View(await _context.Orders.ToListAsync());
        }

        // GET: Order/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderModel = await _context.Orders
                .FirstOrDefaultAsync(m => m.OrderId == id);
            orderModel.OrderLines = await _context.OrderDetails.Where(m => m.OrderId == orderModel.OrderId).Include("Product").ToListAsync();
            if (orderModel == null)
            {
                return NotFound();
            }

            return View(orderModel);
        }

        // GET: Order/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Order/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderId,FirstName,LastName,AddressLine1,AddressLine2,Postcode,City,Country,Email")] OrderModel orderModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(orderModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(orderModel);
        }

        // GET: Order/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderModel = await _context.Orders.FindAsync(id);
            if (orderModel == null)
            {
                return NotFound();
            }
            return View(orderModel);
        }

        // POST: Order/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrderId,FirstName,LastName,AddressLine1,AddressLine2,Postcode,City,Country,Email")] OrderModel orderModel)
        {
            if (id != orderModel.OrderId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(orderModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderModelExists(orderModel.OrderId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(orderModel);
        }

        // GET: Order/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderModel = await _context.Orders.FirstOrDefaultAsync(m => m.OrderId == id);
            HttpResponseMessage response = await client.DeleteAsync("https://onlineshopdeliveryapi20220402003022.azurewebsites.net/api/orders/" + orderModel.OrderId);
            response.EnsureSuccessStatusCode();

            if (orderModel == null)
            {
                return NotFound();
            }

            return View(orderModel);
        }

        // POST: Order/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var orderModel = await _context.Orders.FindAsync(id);
            _context.Orders.Remove(orderModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> UpdateStatus()
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync("https://onlineshopdeliveryapi20220402003022.azurewebsites.net/api/orders");
                response.EnsureSuccessStatusCode();

                List<DeliveryOrderModel> orders = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DeliveryOrderModel>>(await response.Content.ReadAsStringAsync());

                var currentOrders = await _context.Orders.ToListAsync();

                if(orders != null && currentOrders != null)
                {
                    foreach (var order in orders)
                    {
                        var i = 0;
                        if (order.OrderId == currentOrders[i].OrderId)
                        {
                            var orderModel = await _context.Orders.FindAsync(currentOrders[i].OrderId);
                            orderModel.OrderStatus = order.OrderStatus;
                            _context.Update(orderModel);
                            await _context.SaveChangesAsync();
                            break;
                        }
                        else { i++; }
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            catch (HttpRequestException e)
            {
                return NotFound(e.Message); 
            }
        }

        private bool OrderModelExists(int id)
        {
            return _context.Orders.Any(e => e.OrderId == id);
        }
    }
}
