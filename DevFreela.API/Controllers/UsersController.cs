using DevFreela.API.Models;
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
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            return Ok();
        }

        [HttpPost]
        public IActionResult Post([FromBody] CreateUserModel inputModel)
        {
            return CreatedAtAction(nameof(GetById), new { id = 1 }, inputModel);
        }

        [HttpPut("{id}/login")]
        public IActionResult Login([FromBody] LoginModel inputModel)
        {
            return NoContent();
        }
    }
}
