using ImageUploader.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ImageUploader.Controllers
{
    public class HomeController : Controller
    {
        private readonly IImageRepository _repository;
        private readonly IWebHostEnvironment _hostEnvironment;

        public HomeController(IImageRepository repository, IWebHostEnvironment hostEnvironment)
        {
            _repository = repository;
            this._hostEnvironment = hostEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upload([Bind("ImageId,Title,ImageName")] Image imageModel)
        {
            if (ModelState.IsValid)
            {
                // Save image to folder
                string wwwRootPath = _hostEnvironment.WebRootPath;
                string fileName = Path.GetFileNameWithoutExtension(imageModel.ImageFile.FileName);
                string extension = Path.GetExtension(imageModel.ImageFile.FileName);
                fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                imageModel.ImageName = fileName;
                string path = Path.Combine(wwwRootPath + "/Image/", fileName);
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await imageModel.ImageFile.CopyToAsync(fileStream);
                }
                // Save Image to DB
                _repository.SaveImage(imageModel);
                return RedirectToAction(nameof(Index));
            }
            return View(imageModel);
        }
    }
}
