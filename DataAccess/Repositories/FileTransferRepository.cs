using Application.ViewModel;
using DataAccess.Context;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Azure.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public IQueryable<FileTransfers> GetFileTransfer(string user)
        {
            var result = context.FileTransfers.Where(b => b.EmailTo == user || b.YourEmail == user);
            return result;
        }
    }
}
