using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProjectOther.Common.DTOs;
using ProjectOther.Common.Models;

namespace ProjectOther.Service.IService
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDTO>> GetAllProduct();
        Task<bool> AddProduct(ProductDTO dto);
        Task<bool> UpdateProduct(ProductDTO dto);
        Task<ProductDTO> GetProduct(int idProduct);
        Task<bool> DeleteProduct(int idProduct);

    }
}
