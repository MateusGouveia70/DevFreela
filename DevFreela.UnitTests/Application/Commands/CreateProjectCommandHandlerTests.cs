using DevFreela.Application.Commands.CreateProject;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace DevFreela.UnitTests.Application.Commands
{
    public class CreateProjectCommandHandlerTests
    {
        [Fact]

        public async Task InputDataIsOk_Executed_ReturnProjectId()
        {
            // Arrange
            var projectReposiroty = new Mock<IProjectRepository>();

            var createProjectCommand = new CreateProjectCommand
            {
                Title = "Titudo de Teste",
                Description = "Uma descricao",
                TotalCost = 50000,
                IdClient = 1,
                IdFreelancer = 2
            };

            var createProjectCommandHandler = new CreateProjectCommandHandler(projectReposiroty.Object);

            // Act
            var id = await createProjectCommandHandler.Handle(createProjectCommand, new CancellationToken());


            // Assert
            Assert.True(id >= 0);

            projectReposiroty.Verify(pr => pr.AddAsync(It.IsAny<Project>()), Times.Once);
        }
    }
}
