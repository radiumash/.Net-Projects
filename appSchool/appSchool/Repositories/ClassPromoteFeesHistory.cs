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
    
    public partial class ClassPromoteFeesHistory
    {
        public int ID { get; set; }
        public Nullable<int> TransactionID { get; set; }
        public Nullable<int> SessionID { get; set; }
        public Nullable<long> ReceiptNo { get; set; }
        public Nullable<System.DateTime> ReceiptDate { get; set; }
        public Nullable<int> StudentID { get; set; }
        public Nullable<int> ClassID { get; set; }
        public Nullable<int> ClassSetUpID { get; set; }
        public Nullable<int> TermID { get; set; }
        public Nullable<decimal> FinalTotal { get; set; }
        public string CounterType { get; set; }
        public string Mode { get; set; }
        public string BankName { get; set; }
        public string BranchName { get; set; }
        public string ChequeNo { get; set; }
        public Nullable<System.DateTime> ChequeDate { get; set; }
        public string Remark { get; set; }
        public Nullable<long> VoucherNo { get; set; }
        public Nullable<byte> UIDAdd { get; set; }
        public Nullable<System.DateTime> AddDate { get; set; }
        public Nullable<byte> UIDMod { get; set; }
        public Nullable<System.DateTime> ModDate { get; set; }
        public Nullable<decimal> TermTotal { get; set; }
        public Nullable<decimal> FineAmount { get; set; }
        public Nullable<decimal> OtherAmount { get; set; }
        public string OtherHead { get; set; }
        public Nullable<decimal> DiscountAmount { get; set; }
        public string DiscountType { get; set; }
        public Nullable<decimal> DiscountPercent { get; set; }
        public string TermIds { get; set; }
        public Nullable<decimal> PaidAmount { get; set; }
        public Nullable<decimal> DueAmount { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<int> BankCode { get; set; }
        public byte BranchID { get; set; }
        public byte CompID { get; set; }
    }
}