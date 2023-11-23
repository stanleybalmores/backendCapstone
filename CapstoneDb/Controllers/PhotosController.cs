using CapstoneDb.Data;
using CapstoneDb.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CapstoneDb.Data;
using CapstoneDb.Models;


namespace CapstoneDb.Controllers
{
    [Route("api/photo")]
    [ApiController]
    public class PhotosController : ControllerBase
    {
        private readonly CapstoneDbContext _context;

        public PhotosController(CapstoneDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult UploadPhoto(PhotoDTO model)
        {
            if (model.PhotoFile != null && model.PhotoFile.Length > 0)
            {

                byte[] photoBytes;
                using (var memoryStream = new MemoryStream())
                {
                    model.PhotoFile.CopyTo(memoryStream);
                    photoBytes = memoryStream.ToArray();
                }


                var photo = new Photo
                {
                    Title = model.Title,
                    PhotoData = photoBytes
                };

                _context.Photos.Add(photo);
                _context.SaveChanges();



                return Ok(new { result = "added photo" });
            }

            // Handle validation errors
            ModelState.AddModelError("PhotoFile", "Please choose a valid image file.");
            Console.WriteLine("Error uploading");
            return StatusCode(500, "An error occurred while retrieving log in credentials.");
        }


    }
}
