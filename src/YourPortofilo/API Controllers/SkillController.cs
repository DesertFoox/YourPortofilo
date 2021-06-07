using Application.Interfaces;
using Application.Mapping;
using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YourPortofilo.API_Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SkillController : ControllerBase
    {
        private readonly ISkillRepo _repository;
        private readonly IUserRepo _userrepository;
        private readonly IMapper _mapper;
        public SkillController(ISkillRepo repository, IUserRepo userrepository, IMapper mapper)
        {
            _repository = repository;
            _userrepository = userrepository;
            _mapper =mapper;
        }
        [Authorize]
        [HttpGet("{id}")]
        public IActionResult GetSkillByUserId(Guid id)
        {
            var Skill = _repository.GetUserSkill(id);
            if (Skill is null) return NotFound(new { statuscode = 404, message = "Skill Id Is Undifiend" });
            return Ok(new { statuscode = 200, Skills = Skill });
        }
        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult DeleteSkillById(Guid id)
        {
            var Skill = _repository.GetSkillById(id);
            if (Skill is null) return NotFound(new { statuscode = 404, message = "Skill Id Is Undifiend" });

            _repository.DeleteSkill(Skill.Id);
            return Ok(new { statuscode = 200, message = "Skill Deleted Successfully" });
        }
        [Authorize]
        [HttpPut("{id}")]
        public IActionResult UpdateSkillById(Guid id, UpdateSkillDTO skill)
        {
            if (_repository.GetSkillById(id) is null) return NotFound(new { statuscode = 404, message = "Skill Didnt Found" });
            var Skill = _repository.UpdateSkill(_mapper.Map<Skill>(skill),id);
            return Ok(new { statuscode = 200, message = "Skill Updated Successfully", skill = _mapper.Map<UpdateSkillDTO>(Skill) });
        }
        [Authorize]
        [HttpPost("{id}")]
        public IActionResult CreateSkill(Guid id,[FromBody] CreateSkillDTo skill)
        {
           var User =  _userrepository.GetUserById(id);
            var UserSkill = _userrepository.GetUserById(skill.UserId);

            if (User is null) return NotFound(new { statuscode = 404, message = "user id is undifiend" });
            if (UserSkill is null) return NotFound(new { statuscode = 404, message = "user id is undifiend" });

           var Skill =   _repository.CreateSkill(_mapper.Map<Skill>(skill));
            return Ok(new { statuscode = 200,message="Skill Successfully Created",Skill = _mapper.Map<CreateSkillDTo>(Skill) });
        }


    }
}
