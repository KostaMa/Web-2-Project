using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectOther.DataAccess.IRepository;
using ProjectOther.Common.Models;

namespace ProjectOther.DataAccess.Repository
{
    public class OrderProductRepository : GenericRepository<OrderProduct>, IOrderProductRepository
    {
        public OrderProductRepository(DataAccessContext context) : base(context) { }

        public async Task<IEnumerable<OrderProduct>> GetByIdOrder(int idOrder)
        {
            return table.Where(op => op.IdOrder == idOrder);
        }
    }
}
