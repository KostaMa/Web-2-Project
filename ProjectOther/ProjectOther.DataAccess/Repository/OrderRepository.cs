using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProjectOther.DataAccess.IRepository;
using ProjectOther.Common.Models;

namespace ProjectOther.DataAccess.Repository
{
    public class OrderRepository : GenericRepository<Order>, IOrederRepository
    {
        public OrderRepository(DataAccessContext context) : base(context) { }

        public async Task<Order> GetLast(int IdCustomer)
        {
            var orders = table.Where(o => o.IdCustomer == IdCustomer);
            Order order = await orders.OrderByDescending(o => o.Id).FirstOrDefaultAsync();
            return order;
        }
        public async Task<IEnumerable<Order>> GetOrdersByIdCustomer(int idPerson)
        {
            var orders = table.Where(o => o.IdCustomer == idPerson);
            return orders;
        }

        public async Task<IEnumerable<Order>> GetOrdersByIdDeliverer(int idPerson)
        {
            var orders = table.Where(o => o.IdDeliverer == idPerson);
            return orders;
        }

        public async Task<IEnumerable<Order>> GetOrderByStatus(Enums.OrderStatus status)
        {
            return table.Where(o => o.OrderStatus == status);
        }
    }
}
