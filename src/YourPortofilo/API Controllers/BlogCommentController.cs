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
    public class BlogCommentController : ControllerBase
    {
        private readonly IBlogCommentRepo _repository;
        private readonly IBlogRepo _blogrepo;
        private readonly IUserRepo _userrepo;
        private readonly IMapper _mapper;

        public BlogCommentController(IBlogCommentRepo repository, IUserRepo userrepo, IMapper mapper, IBlogRepo blogrepo)
        {
            _repository = repository;
            _blogrepo = blogrepo;
            _userrepo = userrepo;
            _mapper = mapper;
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetAllComments()
        {
            return Ok(new { statuscode = 200, Comment = _repository.GetAllComments() });
        }
        [Authorize]
        [HttpGet("{id}")]
        public IActionResult GetAllBlogCommentById(Guid id)
        {
            var Comment = _repository.GetBlogCommentById(id);
            if (Comment is null) return NotFound(new { statuscode = 404, message = "Blog not found" });
            return Ok(new { statuscode = 200, Blogs = Comment });
        }
        [Authorize(Roles = "Admin")]

        [HttpGet]
        public IActionResult GetUnverifiedComments()
        {
            return Ok(new { statusCode = 200, Blog = _repository.GetUnverifiedComment() });
        }
        [Authorize(Roles = "Admin")]

        [HttpGet]
        public IActionResult GetVerifiedComments()
        {
            return Ok(new { statusCode = 200, Blog = _repository.GetVerifiedComment() });
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("{id}")]
        public IActionResult GetCommentById(Guid id)
        {
            var Comment = _repository.GetCommentById(id);
            if (Comment is null) return NotFound(new { statuscode = 404, message = "Comment not found" });
            return Ok(new { statusCode = 200, Blog = _repository.GetCommentById(id) }); ;
        }

        [Authorize]
        [HttpPost]
        public IActionResult CreateComment(CreateCommentDTO BlogComment)
        {
            var User = _userrepo.GetUserById(BlogComment.UserCreatorId);
            var Blog = _blogrepo.GetBlogById(BlogComment.BlogId);
            if (User is null) return NotFound(new { statuscode = 404, message = "User not found" });
            if (Blog is null) return NotFound(new { statuscode = 404, message = "Blog not found" });
            _repository.CreateComment(_mapper.Map<BlogComment>(BlogComment));
            return Ok(new { statusCode = 200, Message = "Comment Created Successfully", Comment = BlogComment }); ;

        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public IActionResult DeleteComment(Guid id)
        {
            var Comment = _repository.GetCommentById(id);
            if (Comment is null) return NotFound(new { statuscode = 404, message = "Comment not found" });

            _repository.DeleteComment(id);
            return Ok(new { statusCode = 200, Message = "Comment Deleted Successfully" }); ;
        }
        [Authorize(Roles = "Admin")]

        [HttpPost("{id}")]
        public IActionResult VerifyComment(Guid id)
        {
            var Comment = _repository.GetCommentById(id);
            if (Comment is null) return NotFound(new { statuscode = 404, message = "Comment not found" });
            _repository.VerifyComment(id);
            return Ok(new { statusCode = 200, Message = "Comment Vrified Successfully" }); ;
        }
        [Authorize(Roles = "Admin")]
        [HttpPost("{id}")]
        public IActionResult UnVerifyComment(Guid id)
        {
            var Comment = _repository.GetCommentById(id);
            if (Comment is null) return NotFound(new { statuscode = 404, message = "Comment not found" });
            _repository.UnvirifyComment(id);
            return Ok(new { statusCode = 200, Message = "Comment UnVerified Successfully" }); ;
        }
    }
}

