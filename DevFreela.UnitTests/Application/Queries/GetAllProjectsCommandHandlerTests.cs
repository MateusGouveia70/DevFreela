using DevFreela.Application.Queries.GetAllProjects;
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

namespace DevFreela.UnitTests.Application.Queries
{
    public class GetAllProjectsCommandHandlerTests
    {
        [Fact]
        public async void ThreeProjects_Executed_ReturnThreeProjectViewModels()
        {
            // Arrange
            var projects = new List<Project>
                {
                    new Project("Nome do Teste 1", "Descricao de Teste 1", 1, 2, 10000),
                    new Project("Nome do Teste 2", "Descricao de Teste 2", 1, 2, 20000),
                    new Project("Nome do Teste 3", "Descricao de Teste 3", 1, 3, 10000)
                };

            var projectRepositoryMock = new Mock<IProjectRepository>();
            projectRepositoryMock.Setup(pr => pr.GetAllAsync().Result).Returns(projects);

            var getAllProjectsQuery = new GetAllProjectsQuery("");
            var getAllProjectsQueryHandler = new GetAllProjectsQueryHandler(projectRepositoryMock.Object);

            // Act
            var projectViewModel = await getAllProjectsQueryHandler.Handle(getAllProjectsQuery, new CancellationToken());


            // Assert
            Assert.NotNull(projectViewModel);
            Assert.NotEmpty(projectViewModel);
            Assert.Equal(projects.Count, projectViewModel.Count);

            projectRepositoryMock.Verify(pr => pr.GetAllAsync().Result, Times.Once);
        }
    }
}
