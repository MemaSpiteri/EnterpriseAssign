using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class FileTransLogs
    {
        public int Id { get; set; }
        public string EmailTo { get; set; }
        public string YourEmail { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public string Password { get; set; }
    }
}
