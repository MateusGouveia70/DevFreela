using DevFreela.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevFreela.API.Controllers
{
    [Route("api/skills")]
    public class SkillsController : ControllerBase
    {
        private readonly ISkillIService _skillService;

        public SkillsController(ISkillIService skillService)
        {
            _skillService = skillService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var skillView = _skillService.GetAll();

            return Ok();
        }
    }
}
