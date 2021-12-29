using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces;
using Application.Services;
using DataAccess.Repositories;
using Domain.Interfaces;
using Domain.Models;
using Grpc.Core;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using Presentation.Models;

namespace Presentation.Controllers
{
    public class HomeController : Controller
    {
        
        private IFileTransferService fileTransfService;
        private ILog LogInDbService;
        private IWebHostEnvironment hostEnv;

        public HomeController(IFileTransferService _FileTransferService, ILog _LogDb,
            IWebHostEnvironment _hostEnv)
        {
            fileTransfService = _FileTransferService;
            LogInDbService = _LogDb;
            hostEnv = _hostEnv;
            
        }

        [Authorize]
        public IActionResult List()
        {
            var currentUser = this.User.Identity.Name; /* ToString();*/
            var list = fileTransfService.GetFileTransfer(currentUser);

            return View(list);
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
                        Logs _logger = new Logs();
                        _logger.DateSent = DateTime.Now;

                        //1. to generate a new unique filename
                        string newFilename = Guid.NewGuid() + Path.GetExtension(file.FileName);
                        _logger.YourEmail = model.YourEmail;
                        _logger.FileName = newFilename;

                        string absolutePath = hostEnv.ContentRootPath + "\\Files";

                        string absolutePathWithFilename = absolutePath + "\\" + newFilename;
                        model.File = "\\Files\\" + newFilename;
                        //3. do the transfer/saving of the actual physical file
                        _logger.FileLoc = model.File;

                        using (FileStream fs = new FileStream(absolutePathWithFilename, FileMode.CreateNew, FileAccess.Write))
                        {
                            file.CopyTo(fs);
                            fs.Close();
                        }
                        _logger.FileSize = file.Length;

                        //creates a link for the file upload
                        model.Link = "localhost:44329/" + model.File;
                        _logger.EmailTo = model.EmailTo;
                        fileTransfService.AddFile(model);
                        LogInDbService.AddLog(_logger);
                        ViewBag.Message = "Email sent successfully";
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Email wasn't sent..";
            }
            return View();
        }

    }
}
