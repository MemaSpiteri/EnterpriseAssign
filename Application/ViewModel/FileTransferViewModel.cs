using System;
using System.Collections.Generic;
using System.Text;

namespace Application.ViewModel
{
    public class FileTransferViewModel
    {
        public int Id { get; set; }
        public string EmailTo { get; set; }
        public string YourEmail { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public string Link { get; set; }
    }
}
