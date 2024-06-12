using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace appSchool.Models
{
    public class MettingJoinList
    {
        public string ZoomID { get; set; }
        public string MettingID { get; set; }
        public int ClassID { get; set; }
        public int ClassSetupID { get; set; }
        public string ClassName { get; set; }
        public string MettingPassword { get; set; }
        public string ClassSetupName { get; set; }
        public string StartDate { get; set; }
        public string TeacherName { get; set; }
        public string MettingDate { get; set; }
        public string StartTIme { get; set; }
        public string EndTIme { get; set; }
        public int ShowButton { get; set; }

    }
}