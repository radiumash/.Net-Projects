//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace appSchool.Repositories
{
    using System;
    using System.Collections.Generic;
    
    public partial class vAttendanceOnlineClass
    {
        public string EnrollmentNo { get; set; }
        public string StudentName { get; set; }
        public Nullable<int> TeacherID { get; set; }
        public bool IsPresent { get; set; }
        public string ClassDescription { get; set; }
        public string SubjectName { get; set; }
        public string PeriodName { get; set; }
        public Nullable<int> SubjectID { get; set; }
        public Nullable<int> ClassID { get; set; }
        public Nullable<int> SectionID { get; set; }
        public Nullable<System.DateTime> AttendanceDate { get; set; }
        public string SMSMobileNo { get; set; }
        public string FatherName { get; set; }
        public Nullable<int> SubjectID2 { get; set; }
        public Nullable<int> SubjectID3 { get; set; }
        public Nullable<byte> CompID { get; set; }
        public Nullable<byte> BranchID { get; set; }
        public Nullable<byte> SessionID { get; set; }
        public Nullable<int> PeriodID { get; set; }
        public long EAttendanceID { get; set; }
        public int StudentID { get; set; }
        public string SubjectNameList { get; set; }
    }
}