using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public class SkillSql : ISkillRepo
    {
        private readonly IAppDBContext _context;
        public SkillSql(IUserRepo repo, IAppDBContext context)
        {
            _context = context;
        }
        public Skill CreateSkill(Skill skill)
        {
            var NewSkill = _context.Skills.Add(skill);
            SaveChanges();
            return _context.Skills.Find(skill.Id);
        }
        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }
        public void DeleteSkill(Guid id)
        {
            var Skill = _context.Skills.Find(id);
            _context.Skills.Remove(Skill);
            SaveChanges();
        }
        public IEnumerable<Skill> GetUserSkill(Guid UserId)
        {
            return _context.Skills.Where(x => x.UserId == UserId).ToList();
        }

        public Skill UpdateSkill(Skill skill,Guid id)
        {
            var OldSkill = _context.Skills.FirstOrDefault(x => x.Id == id);
            OldSkill.KnowLedge = skill.KnowLedge;
            OldSkill.SkillName = skill.SkillName;
            SaveChanges();
            return OldSkill;
        }

        public Skill GetSkillById(Guid id)
        {
            return _context.Skills.FirstOrDefault(x=>x.Id == id);
        }
    }
}
