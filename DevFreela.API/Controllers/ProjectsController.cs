using DevFreela.API.Models;
using DevFreela.Application.Commands.CreateCommentProject;
using DevFreela.Application.Commands.CreateProject;
using DevFreela.Application.Commands.DeleteProject;
using DevFreela.Application.Commands.FinishProject;
using DevFreela.Application.Commands.StartProject;
using DevFreela.Application.Commands.UpdateProject;
using DevFreela.Application.InputModels;
using DevFreela.Application.Queries.GetAllProjects;
using DevFreela.Application.Queries.GetProject;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevFreela.API.Controllers
{
    [Route("api/projects")]
    public class ProjectsController : ControllerBase
    {
        private readonly IMediator _mediator; 

        public ProjectsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // api/projects?query=net core
        [HttpGet]
        [Authorize(Roles = "client, freelancer")]
        public async Task<IActionResult> Get(string query)
        {
            var projectQuery = new GetAllProjectsQuery(query);

            var projects = await _mediator.Send(projectQuery);


            return Ok(projects);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "client, freelancer")]
        public async Task<IActionResult> GetById(int id)
        {
            var query = new GetProjectQuery(id);

            var project = await _mediator.Send(query);


            if(project == null)
            {
                return NotFound();
            }

            return Ok(project);
        }

        [HttpPost]
        [Authorize(Roles = "client")]
        public async Task<IActionResult> Post([FromBody] CreateProjectCommand command)
        {
            // var id = _projectService.Create(inputModel);

            var id = await _mediator.Send(command);

            return CreatedAtAction(nameof(GetById), new { id = id}, command);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "client")]
        public async Task<IActionResult> Put(int id, [FromBody] UpdateProjectCommand command)
        {
            if (command.Description.Length > 200)
            {
                return BadRequest();
            }

            await _mediator.Send(command);

            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "client")]
        public async Task<IActionResult> Delete(int Id)
        {
            var command = new DeleteProjectCommand(Id);

            await _mediator.Send(command);

            return NoContent();
        }
        
        // api/projects/2/comments post
        [HttpPost("{id}/comments")]
        [Authorize(Roles = "client, freelancer")]
        public async Task<IActionResult> PostComment(int id, [FromBody] CreateCommentCommand command)
        {
            await _mediator.Send(command);

            return NoContent(); 
        }

        [HttpPut("{id}/start")]
        [Authorize(Roles = "client")]
        public async Task<IActionResult> StartProject(int id)
        {
            var command = new StartProjectCommand(id);

            await _mediator.Send(command);

            return NoContent();
        }

        [HttpPut("{id}/finish")]
        [Authorize(Roles = "client")]
        public async Task<IActionResult> FinishProject(int id)
        {
            var command = new FinishProjectCommand(id);

            await _mediator.Send(command);

            return NoContent();
        }

       
    }
}
