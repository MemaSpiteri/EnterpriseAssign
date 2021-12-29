using Domain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Context
{
    public class LogContext : IdentityDbContext
    {
        public LogContext(DbContextOptions<LogContext> options) :
         base(options)
        { }

        public DbSet<Logs> logs { get; set; }
    }
}
