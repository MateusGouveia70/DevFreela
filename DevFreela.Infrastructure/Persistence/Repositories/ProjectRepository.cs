using Dapper;
using DevFreela.Core.DTOs;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Infrastructure.Persistence.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly DevFreelaDbContext _dbContext;
        private readonly string _connectionString;

        public ProjectRepository(DevFreelaDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _connectionString = configuration.GetConnectionString("DevFreelaCs");
        }


        public async Task AddCommentAsync(ProjectComment projectComment)
        {
            var comment = new ProjectComment(projectComment.Content, projectComment.IdProject, projectComment.UserId);

            await _dbContext.ProjectComments.AddAsync(comment);
            await _dbContext.SaveChangesAsync();
        }

        
        public async Task<int> AddAsync(Project project)
        {
            await _dbContext.Projects.AddAsync(project);
            await _dbContext.SaveChangesAsync();

            return project.Id;
           
        }

        public async Task<List<Project>> GetAllAsync()
        {
            return await _dbContext.Projects.ToListAsync(); 

        }

        public async Task<Project> GetDetailsByIdAsync(int id) 
        {
            var project = await _dbContext.Projects
                .Include(p => p.Cliente)
                .Include(p => p.Freelancer)
                .SingleOrDefaultAsync(p => p.Id == id);

            if (project == null) return null;

          
            return project;
        }

        public async Task<Project> GetByIdAsync(int id)
        {
            var project = await _dbContext.Projects.SingleAsync(p => p.Id == id);

            return project;
        }


        public async Task StartAsync(int id)
        {
            var project =  await _dbContext.Projects.SingleOrDefaultAsync(p => p.Id == id);

            project.Start();

            using (var sqlConnection = new SqlConnection())
            {
                sqlConnection.Open();

                var script = "UPDATE Projects SET Status = @status, StartedAt = @startedAt WHERE Id = @id";

                await sqlConnection.ExecuteAsync(script, new { Status = project.Status, project.StartedAt, id });
            }
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
