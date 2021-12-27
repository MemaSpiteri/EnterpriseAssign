using Application.Interfaces;
using Domain.Interfaces;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using RestSharp;
using RestSharp.Authenticators;

namespace Application.Services
{
    public class FileTransferService : IFileTransferService
    {
        private IFileTransferRepository FileTransRepo;
        public FileTransferService(IFileTransferRepository _FileTransRepo)
        {
            FileTransRepo = _FileTransRepo;
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
                    Password = model.Password
                });

            RestClient client = new RestClient();
            client.BaseUrl = new Uri("https://api.mailgun.net/v3");
            client.Authenticator =
                new HttpBasicAuthenticator("api",
                                            "a5539f015f58d7cac1056d5a0867a2c9-cac494aa-46269f71");
            RestRequest request = new RestRequest();
            request.AddParameter("domain", "sandbox730d7935b7284b36bcba9ae4c0817bb9.mailgun.org", ParameterType.UrlSegment);
            request.Resource = "{domain}/messages";
            request.AddParameter("from", model.YourEmail);
            request.AddParameter("to", model.EmailTo);
            request.AddParameter("to", "mema.spiteri@gmail.com");
            request.AddParameter("subject", model.Title);
            request.AddParameter("text", model.Message);
            request.Method = Method.POST;
            client.Execute(request);
        }
    }
    
}
