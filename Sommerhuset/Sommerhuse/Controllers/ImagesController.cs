using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sommerhuse.Data;
using Sommerhuse.ImageHelp;
using Sommerhuse.Models;

namespace Sommerhuse.Controllers
{
    [Authorize]
    [Route("[controller]")]
    public class ImagesController : Controller
    {
        private readonly SommerhusContext _context;
        private readonly IImageWriter _imageWriter;

        public ImagesController(SommerhusContext context, IImageWriter imageWriter)
        {
            _context = context;
            _imageWriter = imageWriter;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Images.ToArrayAsync());
        }

        [HttpPost]
        [Route("/UploadImage")]
        public async Task<IActionResult> UploadImage(IFormFile file)
        {
            Image image = await _imageWriter.UploadImage(file);
            if(image == null)
            {
                ViewData["message"] = "Du uploadede ikke et billede eller noget gik galt, prøv igen";
                return BadRequest();
            }
            return await PostImage(image);
        }
        // GET: api/Images/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetImage([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var image = await _context.Images.SingleOrDefaultAsync(m => m.Id == id);

            if (image == null)
            {
                return NotFound();
            }

            return Ok(image);
        }



   
        // POST: api/Images
        [HttpPost]
        public async Task<IActionResult> PostImage([FromBody] Image image)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Images.Add(image);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

    }
}