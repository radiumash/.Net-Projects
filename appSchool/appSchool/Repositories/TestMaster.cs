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
    
    public partial class TestMaster
    {
        public int TestID { get; set; }
        public string TestName { get; set; }
        public Nullable<int> Duration { get; set; }
        public string Description { get; set; }
        public string Topic { get; set; }
        public string MarkingSystem { get; set; }
        public byte BranchID { get; set; }
        public byte CompID { get; set; }
    }
}
