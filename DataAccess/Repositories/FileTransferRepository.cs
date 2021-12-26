using DataAccess.Context;
using Domain.Interfaces;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Repositories
{
    public class FileTransferRepository : IFileTransferRepository
    {
        private FileTransferContext context;

        public FileTransferRepository(FileTransferContext _context)
        {
            context = _context;
        }
        public void AddFile(FileTransfers b)
        {
            context.FileTransfers.Add(b);
            context.SaveChanges();
        }
    }
}
