using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain.Interfaces
{
    public interface IFileTransferRepository
    {
        public void AddFile(FileTransfers b);

        public IQueryable<FileTransfers> GetFileTransfer(string user);
    }
}
