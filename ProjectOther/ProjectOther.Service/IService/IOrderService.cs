using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProjectOther.Common.DTOs;
using ProjectOther.Common.Models;

namespace ProjectOther.Service.IService
{
    public interface IOrderService
    {
        Task<bool> AddOrder(OrderDTO dto);
        Task<IEnumerable<OrderDTO>> GetOrdersByStatus(Enums.OrderStatus status);
        Task<IEnumerable<OrderDTO>> GetAll();
        Task<OrderDTO> GetOne(int id);
        Task<bool> AcceptOrder(int id, int idPerson);
        Task<bool> OrderConfirmation(int idOrder);
        Task<IEnumerable<ProductDTO>> GetProductsFromOrder(int idOrder);
        Task<IEnumerable<OrderDTO>> GetOrdersByIdCustomer(int idPerson);
        Task<IEnumerable<OrderDTO>> GetOrdersByIdDeliverer(int idPerson);
        Task<OrderDTO> LastOrder(int IdCustomer);
    }
}
