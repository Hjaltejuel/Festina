using Microsoft.AspNetCore.Http;
using Sommerhuse.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Sommerhuse.ImageHelp
{
    public class ImageWriter : IImageWriter
    {

     
        public async Task<Image> UploadImage(IFormFile file)
        {

            if (CheckIfImageFile(file))
            {
                return await WriteFile(file);
            }

            return null;

        }

        private bool CheckIfImageFile(IFormFile file)
        {
            byte[] fileBytes;
            using (var ms = new MemoryStream())
            {
                file.CopyTo(ms);
                fileBytes = ms.ToArray();
            }

            return Helper.GetImageFormat(fileBytes) != Helper.ImageFormat.unknown;
        }

        public async Task<Image> WriteFile(IFormFile file)
        {
            string fileName;
            try
            {
                var extension = "." + file.FileName.Split('.')[file.FileName.Split('.').Length - 1];
                fileName = Guid.NewGuid().ToString() + extension; //Create a new Name 
                                                                  //for the file due to security reasons.
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images\\uploads", fileName);
           
                using (var bits = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(bits);
                }
            }
            catch (Exception e)
            {
                return null;
            }
            Image image = new Image();
            image.src = "images\\uploads\\"+fileName;
            image.uploadedAt = DateTime.Now;
            return image;
        }

      
    }
}
