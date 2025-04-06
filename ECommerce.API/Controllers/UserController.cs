using ECommerce.Business.Abstract;
using ECommerce.Entity.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var response = _userService.GetAll();
            if (!response.IsSuccess)
                return BadRequest(response);
            return Ok(response);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var response = _userService.GetById(id);
            if (!response.IsSuccess)
                return NotFound(response);
            return Ok(response);
        }

        [HttpPost]
        public IActionResult Add([FromBody] UserCreateDto userCreateDto)
        {
            var response = _userService.Add(userCreateDto);
            if (!response.IsSuccess)
                return BadRequest(response);
            return CreatedAtAction(nameof(GetById), new { id = response.Data.Id }, response);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] UserDto userDto)
        {
            if (id != userDto.Id)
                return BadRequest();

            var response = _userService.Update(userDto);
            if (!response.IsSuccess)
                return NotFound(response);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var response = _userService.Delete(id);
            if (!response.IsSuccess)
                return NotFound(response);
            return Ok(response);
        }

        [HttpPatch("{id}/status")]
        public IActionResult SetStatus(int id, [FromQuery] bool isActive)
        {
            var response = _userService.SetStatus(id, isActive);
            if (!response.IsSuccess)
                return NotFound(response);
            return Ok(response);
        }
    }
} 