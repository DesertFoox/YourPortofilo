using Application.Mapping;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IBlogCommentRepo
    {
        bool SaveChanges();
          
        public IEnumerable<BlogCommentDTO> GetBlogCommentById(Guid id);
        public IEnumerable<GetAllCommentsDTO> GetAllComments();
        public IEnumerable<BlogComment> GetUnverifiedComment();
        public IEnumerable<BlogComment> GetVerifiedComment();
        public BlogComment GetCommentById(Guid id);
        public BlogComment VerifyComment(Guid id);
        public BlogComment UnvirifyComment(Guid id);
        public void DeleteComment(Guid id);
        public BlogComment UpdateBlog(BlogComment blog);
        public void CreateComment(BlogComment blog);
    }
}
