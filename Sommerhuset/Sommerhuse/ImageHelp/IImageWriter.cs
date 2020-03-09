using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sommerhuse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sommerhuse.ImageHelp
{
    public interface IImageWriter
    {
        Task<Image> UploadImage(IFormFile file);
    }
}
