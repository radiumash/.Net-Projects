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
    
    public partial class VehicleMaster
    {
        public int VehicleID { get; set; }
        public string VehicleNo { get; set; }
        public string EngineNo { get; set; }
        public string ChasisNo { get; set; }
        public string MfgCompany { get; set; }
        public string MfgYear { get; set; }
        public string MakeNo { get; set; }
        public string Model { get; set; }
        public string Colour { get; set; }
        public string VehicleClass { get; set; }
        public string RTOCardNo { get; set; }
        public Nullable<System.DateTime> InsuranceDate { get; set; }
        public string RTOName { get; set; }
        public Nullable<float> RTOPassedWeight { get; set; }
        public Nullable<System.DateTime> RegValidDate { get; set; }
        public Nullable<float> OpeningBalance { get; set; }
        public Nullable<int> OpeningKM { get; set; }
        public byte BranchID { get; set; }
        public byte CompID { get; set; }
        public byte SessionID { get; set; }
        public Nullable<int> UIdAdd { get; set; }
        public Nullable<System.DateTime> AddDate { get; set; }
        public Nullable<int> UIdMod { get; set; }
        public Nullable<System.DateTime> ModDate { get; set; }
        public Nullable<int> BusNo { get; set; }
    }
}
