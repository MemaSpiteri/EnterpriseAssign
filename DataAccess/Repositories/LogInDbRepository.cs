using DataAccess.Context;
using Domain.Interfaces;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Repositories
{
    public class LogInDbRepository : ILog
    {
        private FileTransferContext context;

        public LogInDbRepository(FileTransferContext _context)
        {
            context = _context;
        }

        public void AddLog(Logs b)
        {
            context.Logs.Add(b);
            context.SaveChanges();
        }
    }
}
