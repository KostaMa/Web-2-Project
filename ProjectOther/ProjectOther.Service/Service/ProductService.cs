using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProjectOther.DataAccess.IRepository;
using System.Linq;
using ProjectOther.Service.IService;
using ProjectOther.Common.Models;
using ProjectOther.Common.DTOs;
using AutoMapper;

namespace ProjectOther.Service.Service
{
    public class ProductService : IProductService
    {
        private readonly IGenericRepository<Product> _genericRepository;
        private readonly IMapper _mapper;
        public ProductService(IGenericRepository<Product> genericRepository, IMapper mapper)
        {
            _genericRepository = genericRepository;
            _mapper = mapper;
        }

        public async Task<bool> AddProduct(ProductDTO dto)
        {
            Product product = _mapper.Map<Product>(dto);

            await _genericRepository.Insert(product);
            await _genericRepository.Save();

            return true;
        }

        public async Task<bool> UpdateProduct(ProductDTO dto)
        {
            Product product = await _genericRepository.GetByObject(dto.Id);

            if (product == null)
            {
                throw new KeyNotFoundException("Product does not exists.");
            }

            if (dto.Name != null)
            {
                product.Name = dto.Name;
                await _genericRepository.Update(product);
            }
            if (dto.Ingredients != null)
            {
                product.Ingredients = dto.Ingredients;
                await _genericRepository.Update(product);
            }
            if (dto.Price != product.Price)
            {
                product.Price = dto.Price;
                await _genericRepository.Update(product);
            }
            await _genericRepository.Save();

            return true;
        }

        public async Task<IEnumerable<ProductDTO>> GetAllProduct()
        {
            var products = await _genericRepository.GetAll();
            if (products == null)
            {
                throw new KeyNotFoundException("No products.");
            }

            IEnumerable<ProductDTO> productsDto = _mapper.Map<IEnumerable<Product>, IEnumerable<ProductDTO>>(products);

            return productsDto;
        }

        public async Task<ProductDTO> GetProduct(int idProduct)
        {
            Product product = await _genericRepository.GetByObject(idProduct);
            if (product == null)
            {
                throw new KeyNotFoundException("Product does not exist.");
            }

            ProductDTO dto = _mapper.Map<ProductDTO>(product);

            return dto;
        }

        public async Task<bool> DeleteProduct(int idProduct)
        {
            Product product = await _genericRepository.GetByObject(idProduct);
            if (product == null)
            {
                throw new KeyNotFoundException("Product does not exist.");
            }

            await _genericRepository.Delete(product);
            await _genericRepository.Save();

            return true;
        }
    }
}
