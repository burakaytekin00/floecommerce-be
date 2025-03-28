using ECommerce.Business;
using ECommerce.Entity.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var response = _productService.GetAll();
            if (!response.IsSuccess)
                return BadRequest(response);
            return Ok(response);
        }

        [HttpGet("GetAllByFilter")]
        public IActionResult GetAllByFilter(int categoryId)
        {
            var response = _productService.GetAllByFilter(categoryId);
            if (!response.IsSuccess)
                return BadRequest(response);
            return Ok(response);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var response = _productService.GetById(id);
            if (!response.IsSuccess)
                return NotFound(response);
            return Ok(response);
        }

        [HttpPost]
        public IActionResult Add([FromBody] ProductDto productDto)
        {
            var response = _productService.Add(productDto);
            if (!response.IsSuccess)
                return BadRequest(response);
            return CreatedAtAction(nameof(GetById), new { id = response.Data.Id }, response);
        }

        [HttpPost("Update")]
        public IActionResult Update([FromBody] ProductDto productDto)
        {
            var response = _productService.Update(productDto);
            if (!response.IsSuccess)
                return NotFound(response);
            return Ok(response);
        }

        [HttpPost("Delete")]
        public IActionResult Delete(int id)
        {
            var response = _productService.Delete(id);
            if (!response.IsSuccess)
                return NotFound(response);
            return Ok(response);
        }

        [HttpPatch("{id}/status")]
        public IActionResult SetStatus(int id, [FromQuery] bool isActive)
        {
            var response = _productService.SetProductStatus(id, isActive);
            if (!response.IsSuccess)
                return NotFound(response);
            return Ok(response);
        }
    }
}
