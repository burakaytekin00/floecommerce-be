using ECommerce.Business.Abstract;
using ECommerce.Entity.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserTypeController : ControllerBase
    {
        private readonly IUserTypeService _userTypeService;

        public UserTypeController(IUserTypeService userTypeService)
        {
            _userTypeService = userTypeService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var response = _userTypeService.GetAll();
            if (!response.IsSuccess)
                return BadRequest(response);
            return Ok(response);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var response = _userTypeService.GetById(id);
            if (!response.IsSuccess)
                return NotFound(response);
            return Ok(response);
        }

        [HttpPost]
        public IActionResult Add([FromBody] UserTypeDto userTypeDto)
        {
            var response = _userTypeService.Add(userTypeDto);
            if (!response.IsSuccess)
                return BadRequest(response);
            return CreatedAtAction(nameof(GetById), new { id = response.Data.Id }, response);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] UserTypeDto userTypeDto)
        {
            if (id != userTypeDto.Id)
                return BadRequest();

            var response = _userTypeService.Update(userTypeDto);
            if (!response.IsSuccess)
                return NotFound(response);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var response = _userTypeService.Delete(id);
            if (!response.IsSuccess)
                return NotFound(response);
            return Ok(response);
        }

        [HttpPatch("{id}/status")]
        public IActionResult SetStatus(int id, [FromQuery] bool isActive)
        {
            var response = _userTypeService.SetStatus(id, isActive);
            if (!response.IsSuccess)
                return NotFound(response);
            return Ok(response);
        }
    }
} 