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
    
    public partial class FeesStructureDetailHistory
    {
        public int HistoryID { get; set; }
        public int FeeStructDetailID { get; set; }
        public Nullable<int> FeeStructID { get; set; }
        public Nullable<int> FeeStructClassID { get; set; }
        public Nullable<int> FeeTermID { get; set; }
        public Nullable<int> FeeHeadID { get; set; }
        public Nullable<decimal> FeeAmount { get; set; }
        public Nullable<int> FeeStructSessionID { get; set; }
        public Nullable<byte> ChangedBy { get; set; }
        public System.DateTime ChangedDate { get; set; }
        public bool IsDeleted { get; set; }
        public byte BranchID { get; set; }
        public byte CompID { get; set; }
    }
}