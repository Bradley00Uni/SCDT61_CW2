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

            var deliveryValues = new Dictionary<string, string>
            {
                {"firstName", order.FirstName },
                {"lastName", order.LastName },
                {"email", order.Email },
                {"addressLine1", order.AddressLine1 },
                {"addressLine2", order.AddressLine2 },
                {"postCode", order.Postcode },
                {"city", order.City },
                {"country", order.Country },
                {"orderID", order.OrderId.ToString() }
            };

            content = Newtonsoft.Json.JsonConvert.SerializeObject(deliveryValues);
            httpContent = new StringContent(content, Encoding.UTF8 ,"application/json");

            response = await client.PostAsync("https://localhost:44380/api/deliveries", httpContent);
            responseString = await response.Content.ReadAsStringAsync();

            foreach(var product in lines)
            {
                var productValues = new Dictionary<string, string>();

                productValues.Add("productName", product.Product.Description);
                productValues.Add("amount", product.Amount.ToString());
                productValues.Add("price", product.Price.ToString());
                productValues.Add("orderID", order.OrderId.ToString());

                content = Newtonsoft.Json.JsonConvert.SerializeObject(productValues);
                httpContent = new StringContent(content, Encoding.UTF8, "application/json");

                response = await client.PostAsync("https://localhost:44380/api/products", httpContent);
                responseString = await response.Content.ReadAsStringAsync();
            }

        }
    }
}
