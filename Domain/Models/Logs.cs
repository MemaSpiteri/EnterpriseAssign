using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Models
{
    public class Logs
    {
        [Key]
        public int Id { get; set; }
        public string EmailTo { get; set; }
        public string YourEmail { get; set; }
        public string FileLoc { get; set; }
        public string FileName { get; set; }
        public DateTime DateSent { get; set; }
        public float FileSize { get; set; }
    }
}
