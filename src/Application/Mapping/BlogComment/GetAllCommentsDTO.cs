using Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mapping
{
    public class GetAllCommentsDTO
    {
        public Guid Id { get; set; }
        public Guid UserCreatorId { get; set; }
        public string Comment { get; set; }
        public bool IsVerified { get; set; }
        public BlogGetAllComment Blog { get; set; }
    }
}
