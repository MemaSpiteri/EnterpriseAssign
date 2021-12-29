using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Models
{
    public class FileTransfers
    {
        [Key]
        public int Id { get; set; }
        public string EmailTo { get; set; }
        public string YourEmail { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public string Password { get; set; }
        public string File { get; set; }
        public string Link { get; set; }
    }
}
