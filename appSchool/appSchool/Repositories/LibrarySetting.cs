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
    
    public partial class LibrarySetting
    {
        public int SettingId { get; set; }
        public Nullable<int> SessionId { get; set; }
        public Nullable<int> StudentQuota { get; set; }
        public Nullable<int> StaffQuota { get; set; }
        public Nullable<int> Fine { get; set; }
        public Nullable<int> DueDays { get; set; }
        public Nullable<bool> IsLumSumFine { get; set; }
        public Nullable<int> LumSumFine { get; set; }
        public byte CompID { get; set; }
        public byte BranchID { get; set; }
    }
}
