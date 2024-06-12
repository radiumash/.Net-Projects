using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace appSchool.Models
{
    public class StudentResultstatus
    {
        public string StudentName { get; set; }
        public string RollNo { get; set; }
        public string DownloadResultLink { get; set; }
        public string ResultStatus { get; set; }
        public string Class { get; set; }
        public string ResultSession { get; set; }
        public bool IsFileExist { get; set; }
    }
}