using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace appSchool.Models
{
    public class TeacherListDetail
    {

        public int TeacherID { get; set; }
        public string TeacherFullName { get; set; }
        public string Dob { get; set; }
        public string Email { get; set; }
        public string DesignationName { get; set; }
        public string TeacherImage { get; set; }
        public string TeacherImageUrl { get; set; }

    }
}