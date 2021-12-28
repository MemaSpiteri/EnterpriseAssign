using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class Log
    {
        public int Id { get; set; }
        public string emailTo { get; set; }
        public string YourEmail { get; set; }
        public string message { get; set; }
        public DateTime DateSent { get; set; }

    }
}
