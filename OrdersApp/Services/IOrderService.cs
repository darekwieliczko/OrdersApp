﻿using OrdersApp.Entities;

namespace OrdersApp.Services;

public interface IOrderService
{
    public Task<Order> Add(Order order);
    public Task<Order> Get(int id);
    public Task Delete(int id);
    public IQueryable<Order> GetAll();
    public Task DeleteAll();
    public Task<Order> Update(Order order);

}
