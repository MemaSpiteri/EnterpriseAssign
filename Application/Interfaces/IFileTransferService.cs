using Application.ViewModel;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Application.Interfaces
{
    public interface IFileTransferService
    {
        public void AddFile(FileTransfers model);

        public IQueryable<FileTransferViewModel> GetFileTransfer(string user);
    }
}
