using AutoMapper;
using DTOs;
using Entities;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class OrdersServices : IOrdersServices
    {
        private readonly IOrdersData ordersData;
        private readonly IMapper autoMapping;

        public OrdersServices(IOrdersData _ordersData, IMapper _autoMapping)
        {
            autoMapping = _autoMapping;
            ordersData = _ordersData;
        }

        public async Task<List<OrderDto>> GetOrders() // Changed to PascalCase: GetOrders
        {
            List<Order> orders = await ordersData.getOrders(); // Suggestion: getOrders => GetOrders (PascalCase)
            List<OrderDto> ordersDto = autoMapping.Map<List<OrderDto>>(orders);
            return ordersDto;
        }

        public async Task AddOrder(OrderDto orderDto) // Changed to PascalCase: AddOrder, //RETURN OBJECT
        {
            try
            {
                Order order = autoMapping.Map<Order>(orderDto);
                await ordersData.addOrder(order); // Suggestion: addOrder => AddOrder (PascalCase)
            }
            catch (Exception e)
            {
                throw e;
            }
        }


    }
}
