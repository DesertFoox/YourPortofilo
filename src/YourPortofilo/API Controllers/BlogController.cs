using Application.Interfaces;
using Application.Mapping;
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
    public class BlogController : ControllerBase
    {
        private readonly IBlogRepo _repository;
        private readonly IMapper _mapper;
        private readonly IUserRepo _userrepo;
        public BlogController(IBlogRepo repository, IMapper mapper, IUserRepo userrepo)
        {
            _repository = repository;
            _userrepo = userrepo;
            _mapper = mapper;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult GetAllBlog()
        {
            return Ok(new { statuscode = 200, Blogs = _repository.GetAllBlogs() });
        }

        [Authorize]
        [HttpGet("{id}")]
        public IActionResult GetBlogById(Guid id)
        {
            var Blog = _repository.GetBlogDTOById(id);
            return Ok(new { statuscode = 200, Blog = Blog });
        }

        [Authorize]
        [HttpGet("{id}")]
        public IActionResult GetBlogsByUserId(Guid id)
        {
            var UserBlog = _repository.GetBlogsByUserId(id);
            return Ok(new { statuscode = 200, Blogs = UserBlog });
        }

        [Authorize]
        [HttpPost]
        public IActionResult CreateBlog(CreateBlogDTO blog)
        {
            var User = _userrepo.GetUserById(blog.UserId);
            if (String.IsNullOrWhiteSpace(blog.Image)) blog.Image = "DefaultBlogImage.png";
            if (User is null) return NotFound(new { statuscode = 404, message = "Blog not found" });
            _repository.CreateBlog(_mapper.Map<Blog>(blog));
            return Ok(new { statuscode = 200, message = "Blog Created Successfully" });
        }

        [Authorize]
        [HttpPost("{id}")]
        public IActionResult UploadBlogImage([FromForm] UploadImageDTO img, Guid id)
        {
            var dir = Directory.GetCurrentDirectory() + "\\wwwroot\\";

            if (img.Image.Length > 0)
            {
                if (img.Image.FileName.EndsWith(".png") || img.Image.FileName.EndsWith(".jpg") || img.Image.FileName.EndsWith(".jpeg"))
                {
                    var NewFileName = img.Image.FileName;
                    Random RandomNumberName = new Random();
                    if (String.IsNullOrWhiteSpace(NewFileName)|| NewFileName=="string") NewFileName = "defaultBlogImage.png";
                    else NewFileName = RandomNumberName.Next(1000, 10000) + "_" + img.Image.FileName;

                    if (!Directory.Exists(dir + "\\uploads\\UserPicture\\"))
                    {
                        Directory.CreateDirectory(dir + "\\uploads\\UserPicture\\");
                    }
                    using (FileStream filestream = System.IO.File.Create(dir + "\\uploads\\UserPicture\\" + NewFileName))
                    {

                        img.Image.CopyTo(filestream);
                        filestream.Flush();
                        var Blog = _repository.GetBlogById(id);
                        if (Blog is null) return NotFound(new { statuscode = 404, message = "Blog not found" });
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

        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult DeleteBlogById(Guid id)
        {
            var Blog = _repository.GetBlogById(id);
            if (Blog is null) return NotFound(new { statuscode = 404, message = "Blog not found" });
            _repository.DeleteBlog(id);
            return Ok(new { statuscode = 200, message="Blog Deleted Successfully" });

        }

        [Authorize]
        [HttpPut("{id}")]
        public IActionResult UpdateBlogById(Guid id, UpdateBlogDTO blog)
        {
            var Blogg = _repository.GetBlogById(id);
            if (Blogg is null) return NotFound(new { statuscode = 404, message = "Blog not found" });
            var BlogModel = _mapper.Map<Blog>(blog);
             _repository.UpdateBlog(BlogModel,id);
            return Ok(new { statuscode = 200, message = "Blog Updated Successfully" });
        }

    }
}
