using Application.Interfaces;
using Domain.Interfaces;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using RestSharp;
using RestSharp.Authenticators;
using System.IO;
using MailChimp.Net.Models;
using Method = RestSharp.Method;
using System.Linq;
using Application.ViewModel;
using Microsoft.Azure.Documents;

namespace Application.Services
{
    public class FileTransferService : IFileTransferService
    {
        private IFileTransferRepository FileTransRepo;
       
        public FileTransferService(IFileTransferRepository _FileTransRepo, Logs logger)
        {
            FileTransRepo = _FileTransRepo;
            _logger = logger;
        }
        public void AddFile(FileTransfers model)
        {
            FileTransRepo.AddFile(
                new FileTransfers()
                {
                    EmailTo = model.EmailTo,
                    YourEmail = model.YourEmail,
                    Title = model.Title,
                    File = model.File,
                    Message = model.Message,
                    Password = model.Password,
                    Link = model.Link
                });

            
            

            SendSimpleMessage(model);
        }

        public static IRestResponse SendSimpleMessage(FileTransfers model)
        {
            RestClient client = new RestClient();
            client.BaseUrl = new Uri("https://api.eu.mailgun.net/v3");
            client.Authenticator =
                new HttpBasicAuthenticator("api",
                                            "a5539f015f58d7cac1056d5a0867a2c9-cac494aa-46269f71");
            RestRequest request = new RestRequest();
            request.AddParameter("domain", "sandbox730d7935b7284b36bcba9ae4c0817bb9.mailgun.org", ParameterType.UrlSegment);
            request.Resource = "{domain}/messages";
            request.AddParameter("from", model.YourEmail);
            request.AddParameter("to", model.EmailTo);
            request.AddParameter("subject", model.Title);
            request.AddParameter("text", model.Link);
            request.Method = Method.POST;
            client.Execute(request);
            return client.Execute(request);
        }

        //public void zipFileWithPass()
        //{
        //    using (FileStream zipFile = File.Open("compress_directory.zip", FileMode.Create))
        //    {
        //        using (Archive archive = new Archive(new ArchiveEntrySettings(null, new TraditionalEncryptionSettings("p@s$"))))
        //        {
        //            // Add folder to the archive
        //            DirectoryInfo corpus = new DirectoryInfo("CanterburyCorpus");
        //            archive.CreateEntries(corpus);
        //            // Create ZIP archive
        //            archive.Save(zipFile);
        //        }
        //    }

        //}

        public IQueryable<FileTransferViewModel> GetFileTransfer(string user)
        {
            //all this will be changed into 1 line with the introduction of AutoMapper

            var list = from b in FileTransRepo.GetFileTransfer(user) //List<Blog>
                       select new FileTransferViewModel()
                       {
                          Id = b.Id,
                          EmailTo = b.EmailTo,
                          YourEmail = b.YourEmail,
                          Title = b.Title,
                          Message = b.Message,
                          Link = b.Link
                       };
            return list;
        }
    }
    
}
