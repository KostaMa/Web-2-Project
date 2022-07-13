using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProjectOther.Common.Models;

namespace ProjectOther.DataAccess.IRepository
{
    public interface IOrderProductRepository
    {
        Task<IEnumerable<OrderProduct>> GetByIdOrder(int idOrder);
    }
}
