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
    
    public partial class Online_ZoomAttendClass
    {
        public int AttendID { get; set; }
        public string ZoomID { get; set; }
        public Nullable<System.DateTime> LoginTime { get; set; }
        public Nullable<System.DateTime> LogOutTime { get; set; }
        public Nullable<int> StudentID { get; set; }
        public string MeetingID { get; set; }
        public Nullable<System.DateTime> AddDate { get; set; }
        public Nullable<System.DateTime> ModDate { get; set; }
        public Nullable<int> BranchID { get; set; }
        public Nullable<int> SessionID { get; set; }
        public Nullable<int> CompID { get; set; }
        public string AttendFromApp { get; set; }
    }
}
