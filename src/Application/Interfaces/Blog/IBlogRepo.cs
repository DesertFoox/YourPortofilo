using Application.Mapping;
using Application.Mapping;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IBlogRepo
    {
        bool SaveChanges();
        public IEnumerable<GetAllBlogDTO> GetAllBlogs();
        public Blog GetBlogById(Guid id);
        public BlogByIdDTO GetBlogDTOById(Guid id);
        public IEnumerable<GetAllBlogDTO> GetBlogsByUserId(Guid id);
        public void DeleteBlog(Guid id);
        public void UpdateBlog(Blog blog,Guid id);
        public void CreateBlog(Blog blog);
        public void UploadImage(Guid id, string imagepath);
    }
}
