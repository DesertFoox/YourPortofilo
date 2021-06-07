using Application.Interfaces;
using Application.Mapping.Portofilo;
using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace YourPortofilo.API_Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PortofiloController : ControllerBase
    {
        private readonly IPortofiloRepo _repository;
        private readonly IUserRepo _userrepository;
        private readonly IMapper _mapper;
        public PortofiloController(IPortofiloRepo repository, IMapper mapper, IUserRepo userrepository)
        {
            _repository = repository;
            _mapper = mapper;
            _userrepository = userrepository;
        }

        [Authorize]
        [HttpGet("{id}")]
        public IActionResult GetPortofiloByUserId(Guid id)
        {
            var Porto = _repository.GetUserPortofiloById(id);
            if (Porto is null) return NotFound(new { statuscode = 404, message = "Portofilo Id Is Undifiend" });
            return Ok(new { statuscode = 200, PortoFilo = Porto });
        }

        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult DeletePortofiloById(Guid id)
        {
            var Porto = _repository.GetPortoFiloById(id);
            if (Porto is null) return NotFound(new { statuscode = 404, message = "Portofilo Id Is Undifiend" });

            _repository.DeletePortoFilo(Porto.Id);
            return Ok(new { statuscode = 200, message = "Portofilo Deleted Successfully" });
        }

        [Authorize]
        [HttpPut("{id}")]
        public IActionResult UpdatePortoById(Guid id, UpdatePortofiloDTO porto)
        {
            if (_repository.GetPortoFiloById(id) is null) return NotFound(new { statuscode = 404, message = "Portofilo Didnt Found" });
            var Porto = _repository.UpdatePortofilo(_mapper.Map<PortoFilo>(porto));
            return Ok(new { statuscode = 200, message = "Portofilo Updated Successfully", PortoFilo = Porto });
        }

        [Authorize]
        [HttpPost("{id}")]
        public IActionResult CreatePorto(Guid id,[FromBody] CreatePortoDTO porto)
        {
            var User = _userrepository.GetUserById(id);
            if (User is null) return BadRequest(new { statuscode = 400, message = "user id is undifiend" });
            var PortoModdel = _mapper.Map<PortoFilo>(porto);
            var Porto = _repository.CreatePortofilo(PortoModdel);
            return Ok(new { statuscode = 200, message = "Skill Successfully Created", PortoFilo = _mapper.Map<CreatePortoDTO>(Porto) });
        }

        [Authorize]
        [HttpPost("{id}")]
        public IActionResult UploadImage([FromForm] UploadImageDTO img , Guid id)
        {
            var dir = Directory.GetCurrentDirectory() + "\\wwwroot\\UserPicture";

            if (img.Image.Length > 0)
            {
                if (img.Image.FileName.EndsWith(".png") || img.Image.FileName.EndsWith(".jpg") || img.Image.FileName.EndsWith(".jpeg"))
                {
                    var NewFileName = img.Image.FileName;
                    Random RandomNumberName = new Random();
                    NewFileName = RandomNumberName.Next(1000, 10000) + "_" + img.Image.FileName;
                    if (!Directory.Exists(dir + "\\uploads\\"))
                    {
                        Directory.CreateDirectory(dir + "\\uploads\\");
                    }
                    using (FileStream filestream = System.IO.File.Create(dir + "\\uploads\\" + NewFileName))
                    {

                        img.Image.CopyTo(filestream);
                        filestream.Flush();
                        var Porto = _repository.GetPortoFiloById(id);
                        if (Porto is null) return NotFound(new { statuscode = 404,message= "Porto not found" });
                        _repository.UploadImage(id, NewFileName);
                        return Ok(new { statuscode = 200, message = "Image Uploaded Successfully" });
                    }
                }
                else
                {
                    return BadRequest(new { Statuscode = 415, Message = "UnsupportedContent,Suppoerted format is jpg png jpeg" });
                }

            }
            else
            {
                return BadRequest(new { statuscode = 400, errormessage = "Something Went Wront Please Try Again" });
            }
        }
    }
}
