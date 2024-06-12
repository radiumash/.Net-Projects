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
    
    public partial class StudentSessionHistory
    {
        public int ID { get; set; }
        public int StudentSessionID { get; set; }
        public int StudentID { get; set; }
        public int ClassSetupID { get; set; }
        public int SessionID { get; set; }
        public int ClassID { get; set; }
        public Nullable<int> HouseID { get; set; }
        public Nullable<bool> HostelFacility { get; set; }
        public Nullable<bool> BusFacility { get; set; }
        public Nullable<int> BusID { get; set; }
        public Nullable<bool> SMSInHindi { get; set; }
        public Nullable<byte> UIDAdd { get; set; }
        public Nullable<System.DateTime> AddDate { get; set; }
        public Nullable<byte> UIDMod { get; set; }
        public Nullable<System.DateTime> ModDate { get; set; }
        public bool FeesStructStatus { get; set; }
        public Nullable<bool> IsNewStudent { get; set; }
        public Nullable<byte> ChangedBy { get; set; }
        public Nullable<System.DateTime> ChangedDate { get; set; }
        public bool IsDeleted { get; set; }
        public Nullable<int> RollNo { get; set; }
        public byte CompID { get; set; }
        public byte BranchId { get; set; }
    }
}
