using InventoryAPI.Helper;
using InventoryAPI.Interface;
using InventoryAPI.Model.Domain;
using InventoryAPI.Model.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InventoryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _repo;
        public ProductController(IProductService repo)
        {
            _repo = repo;
        }

        [HttpPost("AddNewProduct")]
        public async Task<IActionResult> AddProduct([FromBody]ProductRequestDTO _product)
        {
            try
            {
                var res = await _repo.AddProduct(_product);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("DeleteProduct")]
        public async Task<IActionResult> Delete([FromBody]Guid Id)
        {
            try
            {
                var res = await _repo.Delete(Id);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetAllProduct")]
        public async Task<IActionResult> GetAllProduct(string message)
        {
            try
            {
                var res = await _repo.GetAllProduct(message);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetProductById")]
        
        public async Task<IActionResult> GetProductById(Guid Id)
        {
            try
            {
                var res = await _repo.GetProductById(Id);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetProductQuantity")]

        public async Task<IActionResult> GetProductQuantity(Guid Id)
        {
            try
            {
                var res = await _repo.GetProductQuantity(Id);
                return Ok(res);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("UpdateProduct")]
        public async Task<IActionResult> UpdateProduct(ProductUpdateDTO product, Guid Id)
        {
            try
            {
                var res = await _repo.UpdateProduct(product, Id);
                return Ok(res);
            }
            catch( Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }









    }
}
