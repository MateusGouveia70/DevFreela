using DevFreela.Application.Services.Interfaces;
using DevFreela.Application.ViewModels;
using DevFreela.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.Services.Implementations
{
    public class SkillService : ISkillIService
    {
        private readonly DevFreelaDbContext _dbContext;

        public SkillService(DevFreelaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<SkillViewModel> GetAll()
        {
            var skill = _dbContext.Skills;

            var skillsViewModels = skill.Select(s => new SkillViewModel(
                s.Id,
                s.Description)).ToList();

            return skillsViewModels;
        }
    }
}
