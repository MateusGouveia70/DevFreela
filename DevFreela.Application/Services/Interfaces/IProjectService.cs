using DevFreela.Application.InputModels;
using DevFreela.Application.ViewModels;
using DevFreela.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.Services.Interfaces
{
    public interface IProjectService
    {
        List<ProjectViewModel> GetAll(string query);
        ProjectDetailsViewModel GetById(int id);
        int Create(ProjectInputModel inputModel);
        void Update(UpdateProjectInputModel inputModel);
        void CreateComment(CommentInputModel inputModel);
        void Delete(int id);
        void Start(int id);
        void Finish(int id); 


    }
}
