using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using Presentation.Models;

namespace Presentation.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IFileTransferService fileTransfService;
        private IWebHostEnvironment hostEnv;

        public HomeController(ILogger<HomeController> logger,  IFileTransferService _FileTransferService,
            IWebHostEnvironment _hostEnv)
        {
            fileTransfService = _FileTransferService;
            hostEnv = _hostEnv;
            _logger = logger;
        }

       
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(FileTransfers model, IFormFile file)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (file != null)
                    {
                        //1. to generate a new unique filename
                        string newFilename = Guid.NewGuid() + Path.GetExtension(file.FileName);
                        _logger.Log(LogLevel.Information, $"New filename {newFilename} was generated for the file being uploaded by user {User.Identity.Name}");
                        //2. find what the absolute path to the folder Files is
                        //C:\Users\attar\Source\Repos\SWD62BEP2021v2\Presentation\Files\5389205C-813B-4AFA-A453-B912C30BF933.jpg

                        //hostEnv.ContentRootPath : C:\Users\attar\Source\Repos\SWD62BEP2021v2\Presentation
                        //hostEnv.WebRootPath:  C:\Users\attar\Source\Repos\SWD62BEP2021v2\Presentation\wwwroot

                        string absolutePath = hostEnv.WebRootPath + "\\Files";
                        _logger.Log(LogLevel.Information, $"{User.Identity.Name} is about to start saving file at {absolutePath}");

                        string absolutePathWithFilename = absolutePath + "\\" + newFilename;
                        model.File = "\\Files\\" + newFilename;
                        //3. do the transfer/saving of the actual physical file

                        using (FileStream fs = new FileStream(absolutePathWithFilename, FileMode.CreateNew, FileAccess.Write))
                        {
                            file.CopyTo(fs);
                            fs.Close();
                        }
                        _logger.Log(LogLevel.Information, $"{newFilename} has been saved successfully at {absolutePath}");

                   

                    using (WebClient client = new WebClient())
                    {
                        var resStr = client.UploadFile("https://file.io", @absolutePathWithFilename);
                        var jObjResult = JObject.Parse(Encoding.UTF8.GetString(resStr));
                        model.Link = (string)jObjResult["link"];
                    }

                    }
                    fileTransfService.AddFile(model);
                    ViewBag.Message = "File added successfully";
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Blog wasn't added successfully";
            }
            return View();
        }

        
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
