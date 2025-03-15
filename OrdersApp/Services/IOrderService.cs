using OrdersApp.Entities;
using OrdersApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersApp.Services;

public interface IOrderService
{
    public Task<Order> Add(Order order);
    public Task<Order> Get(int id);
    public Task Delete(int id);
    public Task<IEnumerable<Order>> GetAll();
    public Task DeleteAll();
    public Task Update(Order order);

}
