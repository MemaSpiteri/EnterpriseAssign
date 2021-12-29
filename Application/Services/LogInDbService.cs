using Application.Interfaces;
using Domain.Interfaces;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Services
{
    public class LogInDbService : ILogInDbService
    {
        private Logs _logger;
        private ILog LogsRepo;

        public LogInDbService(ILog _LogRepo, Logs logger)
        {
            LogsRepo = _LogRepo;
            _logger = logger;
        }

        public void AddLog(Logs log)
        {
            LogsRepo.AddLog(
                new Logs()
                {
                    EmailTo = log.EmailTo,
                    YourEmail = log.YourEmail,
                    FileName = log.FileLoc,
                    FileLoc = log.FileLoc,
                    FileSize = log.FileSize,
                    DateSent = log.DateSent
                   
                });
        }
    }
}
