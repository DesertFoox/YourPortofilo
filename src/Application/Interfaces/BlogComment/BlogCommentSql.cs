using Application.Mapping;
using Application;
using AutoMapper;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public class BlogCommentSql : IBlogCommentRepo
    {
        private readonly IAppDBContext _context;
        public readonly IMapper _mapper;
        public readonly IUserRepo _userrepo;

        public BlogCommentSql(IAppDBContext context, IMapper mapper , IUserRepo userrepo)
        {
            _userrepo = userrepo;
            _context = context;
            _mapper = mapper;
        }

        public void CreateComment(BlogComment blog)
        {
            _context.BlogComments.Add(blog);
            SaveChanges();
        }

        public void DeleteComment(Guid id)
        {
            var BlogComment = GetCommentById(id);
            _context.BlogComments.Remove(BlogComment);
            SaveChanges();
        }

        public BlogComment GetCommentById(Guid id)
        {
            return _context.BlogComments.Find(id);
        }
        public IEnumerable<GetAllCommentsDTO> GetAllComments()
        {
            return _context.BlogComments.Include(x => x.Blog).Select(x => new GetAllCommentsDTO
            {
                Comment = x.Comment,
                Id = x.Id,
                IsVerified = x.IsVerified,
                UserCreatorId = x.UserCreatorId,
                Blog = _mapper.Map<BlogGetAllComment>(x.Blog)
            }
            ).ToList();
        }

        public IEnumerable<BlogCommentDTO> GetBlogCommentById(Guid id)
        {
            return _context.BlogComments.Select(x=>new BlogCommentDTO
            { 
            Comment = x.Comment,
            BlogId = x.BlogId,
            Id = x.Id,
            UserName = _mapper.Map<UserNameDTO>(_userrepo.GetUserNameById(x.UserCreatorId))
            }
            ).Where(x => x.BlogId == id).ToList();
        }

        public IEnumerable<BlogComment> GetUnverifiedComment()
        {
            return _context.BlogComments.Where(x => x.IsVerified == false).ToList();
        }

        public IEnumerable<BlogComment> GetVerifiedComment()
        {
            return _context.BlogComments.Where(x => x.IsVerified == true).ToList();
        }

        public BlogComment UnvirifyComment(Guid id)
        {
            var BlogComment = _context.BlogComments.FirstOrDefault(x => x.Id == id);
            BlogComment.IsVerified = false;
            SaveChanges();
            return BlogComment;
        }

        public BlogComment UpdateBlog(BlogComment blog)
        {
            var OldBlog = _context.BlogComments.FirstOrDefault(x => x.Id == blog.Id);
            OldBlog.Comment = blog.Comment;
            SaveChanges();
            return OldBlog;
        }

        public BlogComment VerifyComment(Guid id)
        {
            var BlogComment = _context.BlogComments.FirstOrDefault(x => x.Id == id);
            BlogComment.IsVerified = true;
            SaveChanges();
            return BlogComment;
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }

    }
}
