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
    
    public partial class ExamResultHistory
    {
        public int HistoryID { get; set; }
        public int ID { get; set; }
        public int StudentID { get; set; }
        public int SessionID { get; set; }
        public Nullable<int> ClassID { get; set; }
        public int SubjectIDL1 { get; set; }
        public int SubjectIDL2 { get; set; }
        public int SubjectIDL3 { get; set; }
        public Nullable<int> MaxMark1 { get; set; }
        public Nullable<int> MinMark1 { get; set; }
        public string ObtainMarks1 { get; set; }
        public Nullable<int> MaxMark2 { get; set; }
        public Nullable<int> MinMark2 { get; set; }
        public string ObtainMarks2 { get; set; }
        public Nullable<int> MaxMark3 { get; set; }
        public Nullable<int> MinMark3 { get; set; }
        public string ObtainMarks3 { get; set; }
        public Nullable<int> MaxMark4 { get; set; }
        public Nullable<int> MinMark4 { get; set; }
        public string ObtainMarks4 { get; set; }
        public Nullable<int> MaxMark5 { get; set; }
        public Nullable<int> MinMark5 { get; set; }
        public string ObtainMarks5 { get; set; }
        public Nullable<bool> IsAbsent { get; set; }
        public string Grade { get; set; }
        public Nullable<byte> UIDAdd { get; set; }
        public Nullable<System.DateTime> AddDate { get; set; }
        public Nullable<byte> UIDMod { get; set; }
        public Nullable<System.DateTime> ModDate { get; set; }
        public Nullable<byte> ChangedBy { get; set; }
        public System.DateTime ChangedDate { get; set; }
        public bool IsDeleted { get; set; }
        public byte BranchID { get; set; }
        public byte CompID { get; set; }
    }
}
