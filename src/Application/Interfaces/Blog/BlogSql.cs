using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Mapping;
using AutoMapper;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Interfaces
{
    public class BlogSql : IBlogRepo
    {
        private readonly IAppDBContext _context;
        private readonly IUserRepo _userrepo;

        private readonly IMapper _mapper;
        public BlogSql(IAppDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public void CreateBlog(Blog blog)
        {
            blog.CreateDate = DateTime.Now;
            _context.Blogs.Add(blog);
            SaveChanges();
        }

        public void DeleteBlog(Guid id)
        {
            var Blog = GetBlogById(id);
            _context.Blogs.Remove(Blog);
            SaveChanges();
        }

        public IEnumerable<GetAllBlogDTO> GetAllBlogs()
        {
            var Blogs = _context.Blogs.Include(x => x.User).Select(x => new GetAllBlogDTO
            {
                Id = x.Id,
                Title = x.Title,
                Image = x.Image,
                Description = x.Description,
                CreateDate = x.CreateDate,
                UserId = x.UserId,
                User = _mapper.Map<UserBlogViewModel>(x.User)
        }
            ).ToList();
            return Blogs;
        }

        public Blog GetBlogById(Guid id)
        {
            return _context.Blogs.Include(x => x.User).Include(x => x.Comments).FirstOrDefault(x => x.Id == id);
        }
        public BlogByIdDTO GetBlogDTOById(Guid id)
        {
            return _context.Blogs.Include(x=>x.User).Include(x=>x.Comments).Select(x => new BlogByIdDTO
            {
                Id = x.Id,
                Title = x.Title,
                Image = x.Image,
                Description = x.Description,
                CreateDate = x.CreateDate,
                User = _mapper.Map<UserBlogViewModel>(x.User)
            }
            ).FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<GetAllBlogDTO> GetBlogsByUserId(Guid id)
        {

            var Blogs = _context.Blogs.Include(x => x.User).Select(x => new GetAllBlogDTO
            {
                Id = x.Id,
                Title = x.Title,
                Image = x.Image,
                Description = x.Description,
                CreateDate = x.CreateDate,
                UserId = x.UserId,
                User = _mapper.Map<UserBlogViewModel>(x.User)
            }
            ).ToList();
            return Blogs;
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }

        public void UpdateBlog(Blog blog,Guid id)
        {
            var OldBlog = GetBlogById(id);
            OldBlog.Description = blog.Description;
            OldBlog.Title = blog.Title;
            SaveChanges();
        }

        public void UploadImage(Guid id, string imagepath)
        {
            var Blog = _context.Blogs.FirstOrDefault(x => x.Id == id);
            Blog.Image = imagepath;
            SaveChanges();
        }

      
    }
}
