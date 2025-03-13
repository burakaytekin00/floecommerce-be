using ECommerce.Business;
using ECommerce.Entity.DTOs;
using Microsoft.AspNetCore.Mvc;
/*
 * test buba 
 API katmanı:
Kullanıcıların veya diğer uygulamaların, iş mantığına HTTP protokolü üzerinden erişmesini sağlar.
 
 */
namespace ECommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var response = _categoryService.GetAllCategories();
            if (!response.IsSuccess)
                return BadRequest(response);
            return Ok(response);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var response = _categoryService.GetCategoryById(id);
            if (!response.IsSuccess)
                return NotFound(response);
            return Ok(response);
        }

        [HttpPost]
        public IActionResult Add([FromBody] CategoryDto categoryDto)
        {
            var response = _categoryService.AddCategory(categoryDto);
            if (!response.IsSuccess)
                return BadRequest(response);
            return CreatedAtAction(nameof(GetById), new { id = response.Data.Id }, response);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] CategoryDto categoryDto)
        {
            if (id != categoryDto.Id)
                return BadRequest();

            var response = _categoryService.UpdateCategory(categoryDto);
            if (!response.IsSuccess)
                return NotFound(response);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var response = _categoryService.DeleteCategory(id);
            if (!response.IsSuccess)
                return NotFound(response);
            return Ok(response);
        }

        [HttpPatch("{id}/status")]
        public IActionResult SetStatus(int id, [FromQuery] bool isActive)
        {
            var response = _categoryService.SetCategoryStatus(id, isActive);
            if (!response.IsSuccess)
                return NotFound(response);
            return Ok(response);
        }
    }
}
