using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class OrdersData : IOrdersData
    {
        BakeryDBContext dBContext;//_dBContext;

        public OrdersData(BakeryDBContext _dBContext)
        {
            dBContext = _dBContext;
        }
        public async Task<List<Order>> GetOrders() // Changed to PascalCase: GetOrders
        {
            return await dBContext.Orders.Include(o => o.OrderItems).ToListAsync<Order>();
        }
        public async Task<Order> AddOrder(Order order) // Changed to PascalCase: AddOrder
        {
            try
            {
                await dBContext.Orders.AddAsync(order);
                await dBContext.SaveChangesAsync();
                // return order; // Return the added order
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
