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
    
    public partial class Branch
    {
        public byte BranchID { get; set; }
        public byte CompID { get; set; }
        public string BranchName { get; set; }
        public string Add1 { get; set; }
        public string Add2 { get; set; }
        public string City { get; set; }
        public string Phone1 { get; set; }
        public string Phone2 { get; set; }
        public Nullable<float> GrCharge { get; set; }
        public Nullable<float> GpCharge { get; set; }
        public Nullable<bool> Stationery { get; set; }
        public Nullable<byte> RateType { get; set; }
        public string StaxNo { get; set; }
        public string PanNo { get; set; }
        public string TanNo { get; set; }
        public string DeliveryAddress { get; set; }
        public string BookingAddress { get; set; }
        public Nullable<byte> StaxBy { get; set; }
        public Nullable<bool> PFAccount { get; set; }
        public Nullable<bool> CartageAccount { get; set; }
        public Nullable<bool> BillAccount { get; set; }
        public Nullable<bool> PaidAccount { get; set; }
        public Nullable<bool> FrDueAccount { get; set; }
        public Nullable<bool> CrChAccount { get; set; }
        public Nullable<bool> FmAccount { get; set; }
        public Nullable<bool> SumAccount { get; set; }
        public Nullable<bool> CrBillAccount { get; set; }
        public Nullable<bool> PaidAcGroup { get; set; }
        public Nullable<bool> PFAcGroup { get; set; }
        public Nullable<bool> CartAcGroup { get; set; }
        public Nullable<bool> BookingAlert { get; set; }
        public Nullable<bool> DispatchAlert { get; set; }
        public Nullable<bool> UnloadingAlert { get; set; }
        public Nullable<bool> DeliveryAlert { get; set; }
        public Nullable<decimal> GrHammali { get; set; }
        public Nullable<decimal> GrCartage { get; set; }
        public Nullable<int> StationId { get; set; }
        public Nullable<decimal> OpBalance { get; set; }
        public Nullable<bool> IsStaxAll { get; set; }
        public Nullable<decimal> DVCRate { get; set; }
        public Nullable<decimal> MinDVC { get; set; }
        public Nullable<int> RefundAc { get; set; }
        public Nullable<decimal> CrRate { get; set; }
    }
}