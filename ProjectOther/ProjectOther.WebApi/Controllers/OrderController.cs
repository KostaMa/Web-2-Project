using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProjectOther.Common.DTOs;
using ProjectOther.Common.Models;
using ProjectOther.Service.IService;
using ProjectOther.WebApi.Authorization;

namespace ProjectOther.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [Authorize]
        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetAllOrders()
        {
            try
            {
                IEnumerable<OrderDTO> orders = await _orderService.GetAll();
                return Ok(orders);
            }
            catch (KeyNotFoundException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception e)
            {
                return BadRequest(new { message = "Something went wrong." });
            }
        }

        [Authorize("Deliverer")]
        [HttpGet]
        [Route("complated-orders")]
        public async Task<IActionResult> GetCompletedOrders()
        {
            try
            {
                IEnumerable<OrderDTO> orders = await _orderService.GetOrdersByStatus(Enums.OrderStatus.Done);
                return Ok(orders);
            }
            catch (KeyNotFoundException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception e)
            {
                return BadRequest(new { message = "Something went wrong." });
            }
        }

        [Authorize("Customer")]
        [HttpGet]
        [Route("{idPerson}/orders")]
        public async Task<IActionResult> GetOrdersByIdCustomer(int idPerson)
        {
            try
            {
                IEnumerable<OrderDTO> orders = await _orderService.GetOrdersByIdCustomer(idPerson);
                return Ok(orders);
            }
            catch (KeyNotFoundException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception e)
            {
                return BadRequest(new { message = "Something went wrong." });
            }
        }

        [Authorize("Deliverer")]
        [HttpGet]
        [Route("{idPerson}/my-orders")]
        public async Task<IActionResult> GetOrdersByIdDeliverer(int idPerson)
        {
            try
            {
                IEnumerable<OrderDTO> orders = await _orderService.GetOrdersByIdDeliverer(idPerson);
                return Ok(orders);
            }
            catch (KeyNotFoundException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception e)
            {
                return BadRequest(new { message = "Something went wrong." });
            }
        }

        [Authorize]
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetOneOrder(int id)
        {
            try
            {
                OrderDTO dto = await _orderService.GetOne(id);
                return Ok(dto);
            }
            catch (KeyNotFoundException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception e)
            {
                return BadRequest(new { message = "Something went wrong." });
            }
        }

        [Authorize("Customer,Deliverer")]
        [HttpGet]
        [Route("{IdCustomer}/current")]
        public async Task<IActionResult> CurrentOrder(int IdCustomer)
        {
            try
            {
                OrderDTO dto = await _orderService.LastOrder(IdCustomer);
                return Ok(dto);
            }
            catch (KeyNotFoundException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception e)
            {
                return BadRequest(new { message = "Something went wrong." });
            }
        }

        [Authorize]
        [HttpGet]
        [Route("order/products/{id}")]
        public async Task<IActionResult> GetProducts(int idOrder)
        {
            try
            {
                var dto = await _orderService.GetProductsFromOrder(idOrder);
                return Ok(dto);
            }
            catch (KeyNotFoundException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception e)
            {
                return BadRequest(new { message = "Something went wrong." });
            }
        }

        [Authorize("Deliverer")]
        [HttpPost]
        [Route("accept-order")]
        public async Task<IActionResult> AcceptOrder(AcceptOrderDTO dto)
        {
            try
            {
                bool isAccepted = await _orderService.AcceptOrder(dto.IdOrder, dto.IdCustomer);
                return Ok();
            }
            catch (KeyNotFoundException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception e)
            {
                return BadRequest(new { message = "Something went wrong." });
            }
        }

        [Authorize("Customer")]
        [HttpPost]
        [Route("order")]
        public async Task<IActionResult> OrderProduct(OrderDTO dto)
        {
            try
            {
                await _orderService.AddOrder(dto);
            }
            catch (Exception e)
            {
                return BadRequest();
            }

            return Ok();
        }

        [Authorize("Customer,Deliverer")]
        [HttpGet]
        [Route("confirmation/{idOrder}")]
        public async Task<IActionResult> OrderConfirmation(int idOrder)
        {
            try
            {
                bool isConfirmed = await _orderService.OrderConfirmation(idOrder);
            }
            catch (KeyNotFoundException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception e)
            {
                return BadRequest(new { message = "Something went wrong." });
            }
            return Ok(new { message = "Order is delivered." });
        }

    }
}
