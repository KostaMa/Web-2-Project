using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectOther.DataAccess.IRepository;
using ProjectOther.Common.Models;

namespace ProjectOther.DataAccess.Repository
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(DataAccessContext context) : base(context) { }

        public async Task<IEnumerable<Product>> GetProductsByIds(List<int> ids)
        {
            return table.Where(p => ids.Contains(p.Id));
        }
    }
}
