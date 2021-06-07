using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
namespace Application.Interfaces
{
    public interface ISkillRepo
    {
        bool SaveChanges();
        public IEnumerable<Skill> GetUserSkill(Guid UserId);
        public Skill CreateSkill(Skill skill);
        public void DeleteSkill(Guid id);
        public Skill UpdateSkill(Skill skill,Guid id);
        public Skill GetSkillById(Guid id);
    }
}
