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
    
    public partial class RouteDetail
    {
        public int RouteDetailID { get; set; }
        public int RouteID { get; set; }
        public Nullable<int> StopId { get; set; }
        public string StopName { get; set; }
        public Nullable<int> OrderNo { get; set; }
        public byte BranchID { get; set; }
        public byte CompID { get; set; }
    }
}
