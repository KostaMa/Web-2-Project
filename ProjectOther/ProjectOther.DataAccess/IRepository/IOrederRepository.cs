using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProjectOther.Common.Models;

namespace ProjectOther.DataAccess.IRepository
{
    public interface IOrederRepository
    {
        Task<IEnumerable<Order>> GetOrderByStatus(Enums.OrderStatus status);
        Task<Order> GetLast(int IdCustomer);
        Task<IEnumerable<Order>> GetOrdersByIdCustomer(int idPerson);
        Task<IEnumerable<Order>> GetOrdersByIdDeliverer(int idPerson);
    }
}
