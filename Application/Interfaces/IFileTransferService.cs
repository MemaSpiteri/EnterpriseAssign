using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces
{
    public interface IFileTransferService
    {
        public void AddFile(FileTransfers model);
    }
}
