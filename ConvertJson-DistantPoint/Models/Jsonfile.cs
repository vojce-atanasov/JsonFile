using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConvertJson_DistantPoint.Models
{
    public class Jsonfile
    {
        public IFormFile FileJ { get; set; }

        public string Content { get; set; }
    }
}
