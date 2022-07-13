using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ProjectOther.Common.DTOs;
using ProjectOther.Common.Models;
using ProjectOther.DataAccess.IRepository;
using ProjectOther.Service.IService;

namespace ProjectOther.Service.Service
{
    public class OrderService : IOrderService
    {
        private readonly IGenericRepository<Product> _genericRepositoryProduct;
        private readonly IGenericRepository<Order> _genericRepositoryOrder;
        private readonly IGenericRepository<OrderProduct> _genericRepositoryOrderProduct;
        private readonly IOrederRepository _orderRepository;
        private readonly IProductRepository _productRepository;
        private readonly IOrderProductRepository _orderProductRepository;
        private readonly IMapper _mapper;
        public OrderService(IGenericRepository<Product> genericRepositoryProduct, IGenericRepository<Order> genericRepositoryOrder, IGenericRepository<OrderProduct> genericRepositoryOrderProduct, IOrederRepository orderRepository, IProductRepository productRepository, IOrderProductRepository orderProductRepository, IMapper mapper)
        {
            _genericRepositoryProduct = genericRepositoryProduct;
            _genericRepositoryOrder = genericRepositoryOrder;
            _genericRepositoryOrderProduct = genericRepositoryOrderProduct;
            _orderRepository = orderRepository;
            _productRepository = productRepository;
            _orderProductRepository = orderProductRepository;
            _mapper = mapper;
        }

        public async Task<bool> AcceptOrder(int id, int idPerson)
        {
            Random random = new Random();
            Order order = await _genericRepositoryOrder.GetByObject(id);
            if (order == null)
            {
                throw new KeyNotFoundException("Order does not exists.");
            }

            order.OrderStatus = Enums.OrderStatus.Accept;
            order.IdDeliverer = idPerson;
            order.DateTimeOfDelivery = DateTime.Now.AddMinutes(random.Next(2, 5));
            await _genericRepositoryOrder.Update(order);
            await _genericRepositoryOrder.Save();

            return true;
        }

        public async Task<bool> AddOrder(OrderDTO dto)
        {
            Order order = _mapper.Map<Order>(dto);
            order.OrderStatus = Enums.OrderStatus.OnHold;

            await _genericRepositoryOrder.Insert(order);
            await _genericRepositoryOrder.Save();

            Order newOrder = await _orderRepository.GetLast(dto.IdCustomer);
            if (newOrder == null)
            {
                throw new KeyNotFoundException("Order does not exists.");
            }

            foreach (ProductDTO product in dto.Products)
            {
                OrderProduct op = new OrderProduct
                {
                    IdOrder = newOrder.Id,
                    IdProduct = product.Id
                };
                await _genericRepositoryOrderProduct.Insert(op);
            }

            await _genericRepositoryOrderProduct.Save();

            return true;
        }

        public async Task<IEnumerable<OrderDTO>> GetAll()
        {
            List<Product> products = new List<Product>();
            IEnumerable<Order> orders = await _genericRepositoryOrder.GetAll();
            if (orders == null)
            {
                throw new KeyNotFoundException("Currently there is no orders.");
            }

            IEnumerable<OrderDTO> ordersDto = _mapper.Map<IEnumerable<Order>, IEnumerable<OrderDTO>>(orders);
            foreach (OrderDTO oDto in ordersDto)
            {
                IEnumerable<OrderProduct> op = await _orderProductRepository.GetByIdOrder(oDto.Id);
                foreach (OrderProduct opp in op)
                {
                    if (oDto.Id == opp.IdOrder)
                    {
                        products.Add(await _genericRepositoryProduct.GetByObject(opp.IdProduct));
                    }
                }
                oDto.Products = _mapper.Map<IEnumerable<Product>, IEnumerable<ProductDTO>>(products);
                products.Clear();
            }

            return ordersDto;
        }

        public async Task<OrderDTO> GetOne(int id)
        {
            Order order = await _genericRepositoryOrder.GetByObject(id);
            if (order == null)
            {
                throw new KeyNotFoundException("Order does not exists.");
            }

            OrderDTO dto = _mapper.Map<OrderDTO>(order);

            return dto;
        }

        public async Task<IEnumerable<ProductDTO>> GetProductsFromOrder(int idOrder)
        {
            List<ProductDTO> dto = new List<ProductDTO>();
            Order order = await _genericRepositoryOrder.GetByObject(idOrder);
            if (order == null)
            {
                throw new KeyNotFoundException("Order does not exist");
            }

            var orderProducts = await _orderProductRepository.GetByIdOrder(idOrder);
            if (order == null)
            {
                throw new KeyNotFoundException("Order does not have products.");
            }

            foreach (var op in orderProducts)
            {
                Product product = await _genericRepositoryProduct.GetByObject(op.IdProduct);
                ProductDTO pDto = _mapper.Map<ProductDTO>(product);
                dto.Add(pDto);
            }

            return dto;
        }

        public async Task<IEnumerable<OrderDTO>> GetOrdersByStatus(Enums.OrderStatus status)
        {
            List<Product> products = new List<Product>();
            IEnumerable<Order> orders = null;
            if (status == Enums.OrderStatus.Accept)
            {
                orders = await _orderRepository.GetOrderByStatus(status);
            }
            else if (status == Enums.OrderStatus.Done)
            {
                orders = await _orderRepository.GetOrderByStatus(status);
            }
            else if (status == Enums.OrderStatus.OnHold)
            {
                orders = await _orderRepository.GetOrderByStatus(status);
            }

            if (orders == null)
            {
                throw new KeyNotFoundException("Currently there is no orders.");
            }

            IEnumerable<OrderDTO> ordersDto = _mapper.Map<IEnumerable<Order>, IEnumerable<OrderDTO>>(orders);
            foreach (OrderDTO oDto in ordersDto)
            {
                IEnumerable<OrderProduct> op = await _orderProductRepository.GetByIdOrder(oDto.Id);
                foreach (OrderProduct opp in op)
                {
                    products.Add(await _genericRepositoryProduct.GetByObject(opp.IdProduct));
                }
                oDto.Products = _mapper.Map<IEnumerable<Product>, IEnumerable<ProductDTO>>(products);
            }

            return ordersDto;
        }

        public async Task<bool> OrderConfirmation(int idOrder)
        {
            Order order = await _genericRepositoryOrder.GetByObject(idOrder);
            if (order == null)
            {
                throw new KeyNotFoundException("Order does not exist.");
            }

            order.OrderStatus = Enums.OrderStatus.Done;

            await _genericRepositoryOrder.Update(order);
            await _genericRepositoryOrder.Save();

            return true;
        }

        public async Task<IEnumerable<OrderDTO>> GetOrdersByIdCustomer(int idPerson)
        {
            List<Product> products = new List<Product>();
            IEnumerable<Order> orders = await _orderRepository.GetOrdersByIdCustomer(idPerson);
            if (orders == null)
            {
                throw new KeyNotFoundException("User does not have orders.");
            }

            IEnumerable<OrderDTO> ordersDto = _mapper.Map<IEnumerable<Order>, IEnumerable<OrderDTO>>(orders);
            foreach (OrderDTO oDto in ordersDto)
            {
                IEnumerable<OrderProduct> op = await _orderProductRepository.GetByIdOrder(oDto.Id);
                foreach (OrderProduct opp in op)
                {
                    if (oDto.Id == opp.IdOrder)
                    {
                        products.Add(await _genericRepositoryProduct.GetByObject(opp.IdProduct));
                    }
                }
                oDto.Products = _mapper.Map<IEnumerable<Product>, IEnumerable<ProductDTO>>(products);
                products.Clear();
            }


            return ordersDto;
        }

        public async Task<IEnumerable<OrderDTO>> GetOrdersByIdDeliverer(int idPerson)
        {
            List<Product> products = new List<Product>();
            IEnumerable<Order> orders = await _orderRepository.GetOrdersByIdDeliverer(idPerson);
            if (orders == null)
            {
                throw new KeyNotFoundException("User does not have orders.");
            }

            IEnumerable<OrderDTO> ordersDto = _mapper.Map<IEnumerable<Order>, IEnumerable<OrderDTO>>(orders);
            foreach (OrderDTO oDto in ordersDto)
            {
                IEnumerable<OrderProduct> op = await _orderProductRepository.GetByIdOrder(oDto.Id);
                foreach (OrderProduct opp in op)
                {
                    if (oDto.Id == opp.IdOrder)
                    {
                        products.Add(await _genericRepositoryProduct.GetByObject(opp.IdProduct));
                    }
                }
                oDto.Products = _mapper.Map<IEnumerable<Product>, IEnumerable<ProductDTO>>(products);
                products.Clear();
            }


            return ordersDto;
        }

        public async Task<OrderDTO> LastOrder(int IdCustomer)
        {
            List<ProductDTO> porductDto = new List<ProductDTO>();
            Order order = await _orderRepository.GetLast(IdCustomer);
            if (order == null)
            {
                throw new KeyNotFoundException("Order does not exists.");
            }

            OrderDTO dto = _mapper.Map<OrderDTO>(order);

            var orderProducts = await _orderProductRepository.GetByIdOrder(order.Id);
            if (order == null)
            {
                throw new KeyNotFoundException("Order does not have products.");
            }

            foreach (var op in orderProducts)
            {
                Product product = await _genericRepositoryProduct.GetByObject(op.IdProduct);
                ProductDTO pDto = _mapper.Map<ProductDTO>(product);
                porductDto.Add(pDto);
            }
            dto.Products = porductDto;

            return dto;
        }
    }
}
