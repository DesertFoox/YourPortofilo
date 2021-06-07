using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Application.Mapping
{
    public class UploadImageUserDTO
    {
        public IFormFile Image { get; set; }
    }
}
