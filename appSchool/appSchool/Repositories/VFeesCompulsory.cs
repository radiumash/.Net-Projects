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
    
    public partial class VFeesCompulsory
    {
        public Nullable<long> Id { get; set; }
        public int FeeTermID { get; set; }
        public string FeeTermName { get; set; }
        public string FeeTermType { get; set; }
        public System.DateTime FeeTermFromDate { get; set; }
        public System.DateTime FeeTermToDate { get; set; }
        public int FeesHeadID { get; set; }
        public string FeesHeadName { get; set; }
        public string PrintHeadName { get; set; }
        public Nullable<bool> ISRebate { get; set; }
        public string FeesHeadType { get; set; }
        public decimal DefaultAmount { get; set; }
        public byte CompID { get; set; }
        public byte BranchID { get; set; }
        public Nullable<byte> SessionID { get; set; }
        public byte FeesHeadBranchID { get; set; }
        public byte FeesHeadCompID { get; set; }
    }
}