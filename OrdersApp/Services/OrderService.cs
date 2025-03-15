using Microsoft.EntityFrameworkCore;
using OrdersApp.Data;
using OrdersApp.Entities;

namespace OrdersApp.Services
{
    public class OrderService : IOrderService
    {
        private readonly OrdersDbContext dbContext;

        public OrderService(OrdersDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<Order> Add(Order order)
        {
            dbContext.Orders.Add(order);
            await dbContext.SaveChangesAsync();
            return order;
        }

        public async Task Delete(int id)
        {
            var order = await Get(id);
            if (order != null)
            {
                dbContext.Orders.Remove(order);
                await dbContext.SaveChangesAsync();
            }
            return;
        }

        public async Task DeleteAll()
        {
            dbContext.Orders.RemoveRange(dbContext.Orders);
            await dbContext.SaveChangesAsync();
            return;
        }

        public async Task<Order> Get(int id) => await dbContext.Orders.FirstOrDefaultAsync(o => o.Id == id);

        public async Task<IEnumerable<Order>> GetAll() => await dbContext.Orders.ToListAsync();

        public async Task Update(Order order)
        {
            dbContext.Orders.Update(order);
            await dbContext.SaveChangesAsync();
            return;
        }
    }
}
