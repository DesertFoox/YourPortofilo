using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mapping
{
    public class BlogCommentDTO
    {
        public Guid Id { get; set; }
        public Guid BlogId { get; set; }
        public string Comment { get; set; }
        public UserNameDTO UserName { get; set; }

    }
}