using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using OnlineShop2022.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;

namespace OnlineShop2022.Models
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _appDbContext;
        private readonly ShoppingCartModel _shoppingCart;
        private static readonly HttpClient client = new HttpClient();

        public OrderRepository(AppDbContext appDbContext, ShoppingCartModel shoppingCart)
        {
            _appDbContext = appDbContext;
            _shoppingCart = shoppingCart;
        }


        public async void CreateOrder(OrderModel order)
        {
            order.OrderPlaced = DateTime.Now;
            order.OrderStatus = "Processing...";

            _appDbContext.Orders.Add(order);

            await _appDbContext.SaveChangesAsync();

            var shoppingCartItems = _shoppingCart.ShoppingCartItems;

            var amountTotal = 0.0;

            foreach (var shoppingCartItem in shoppingCartItems)
            {
                var orderDetail = new OrderDetailModel()
                {
                    Amount = shoppingCartItem.Amount,
                    ProductId = shoppingCartItem.Product.Id,
                    OrderId = order.OrderId,
                    Price = shoppingCartItem.Product.Price
                };
                _appDbContext.OrderDetails.Add(orderDetail);
                amountTotal += shoppingCartItem.Product.Price;
            }
            order.OrderTotal = amountTotal;
            _appDbContext.Update(order);
            await _appDbContext.SaveChangesAsync();
            AddtoAPI(order);

        }

        private async void AddtoAPI(OrderModel order)
        {
            var lines = await _appDbContext.OrderDetails.Where(m => m.OrderId == order.OrderId).Include("Product").ToListAsync();

            var settings = new JsonSerializerSettings { DateFormatString = "yyyy-MM-ddTHH:mm:ss.fffZ" };
            var orderString = JsonConvert.SerializeObject(order.OrderPlaced, settings);

            var orderValues = new Dictionary<string, string>
            {
                { "orderId", order.OrderId.ToString() },
                { "orderTotal", order.OrderTotal.ToString() },
                { "orderStatus", order.OrderStatus }
            };
            var content = Newtonsoft.Json.JsonConvert.SerializeObject(orderValues);
            var httpContent = new StringContent(content, Encoding.UTF8 ,"application/json");

            var response = await client.PostAsync("https://localhost:44380/api/orders", httpContent);
            var responseString = await response.Content.ReadAsStringAsync();
        }
    }
}
