using Application.Interfaces;
using Domain.Interfaces;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

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
                new Domain.Models.FileTransfers()
                {
                    //CategoryId = model.CategoryId,
                    //Name = model.Name,
                    //LogoImageUrl = model.LogoImageUrl,
                    //DateCreated = DateTime.Now,
                    //DateUpdated = DateTime.Now
                });

        }
    }
}
