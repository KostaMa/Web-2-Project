using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProjectOther.Common.DTOs;
using ProjectOther.Service.IService;
using ProjectOther.WebApi.Authorization;

namespace Project.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [Authorize("Admin")]
        [HttpPost]
        [Route("")]
        public async Task<IActionResult> AddProduct(ProductDTO dto)
        {
            try
            {
                await _productService.AddProduct(dto);
            }
            catch (KeyNotFoundException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception e)
            {
                return BadRequest(new { message = "Something went wrong." });
            }
            return Ok(new { message = "Product successfully added." });
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetAllProduct()
        {
            try
            {
                var products = await _productService.GetAllProduct();
                return Ok(products);
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

        [HttpGet]
        [Route("{idProduct}")]
        public async Task<IActionResult> GetProduct(int idProduct)
        {
            try
            {
                ProductDTO dto = await _productService.GetProduct(idProduct);
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

        [Authorize("Admin")]
        [HttpPut]
        [Route("{idProduct}")]
        public async Task<IActionResult> UpdateProduct(int idProduct, ProductDTO dto)
        {
            try
            {
                await _productService.UpdateProduct(dto);
            }
            catch (KeyNotFoundException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception e)
            {
                return BadRequest(new { message = "Something went wrong." });
            }
            return Ok(new { message = "Product successfully updated." });
        }

        [Authorize("Admin")]
        [HttpDelete]
        [Route("{idProduct}")]
        public async Task<IActionResult> DeleteProduct(int idProduct)
        {
            try
            {
                await _productService.DeleteProduct(idProduct);
            }
            catch (KeyNotFoundException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception e)
            {
                return BadRequest(new { message = "Something went wrong." });
            }
            return Ok(new { message = "Product successfully deleted." });
        }

    }
}
