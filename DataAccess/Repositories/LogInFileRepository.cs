﻿using DataAccess.Context;
using Domain.Interfaces;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Repositories
{
    public class LogInFileRepository : ILog
    {
        private LogContext context;

        public LogInFileRepository(LogContext _context)
        {
            context = _context;
        }

        public void AddLog(Logs b)
        {
            
        }
    }
}
