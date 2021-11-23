using DevFreela.API.Models;
using DevFreela.Application.InputModels;
using DevFreela.Application.Services.Interfaces;
using DevFreela.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevFreela.API.Controllers
{
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var user = _userService.GetById(id);

            if (user == null) return NotFound(); 

            return Ok(user);
        }

        [HttpPost]
        public IActionResult Post([FromBody] UserInputModel inputModel)
        {
            var user = _userService.Create(inputModel);

            return CreatedAtAction(nameof(GetById), new { id = user }, inputModel);
        }

        [HttpPut("{id}/login")]
        public IActionResult Login([FromBody] LoginModel inputModel)
        {
            return NoContent();
        }
    }
}
