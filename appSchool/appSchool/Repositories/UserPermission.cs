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
    
    public partial class UserPermission
    {
        public int PermitId { get; set; }
        public Nullable<byte> UserId { get; set; }
        public Nullable<int> MenuId { get; set; }
        public bool AddP { get; set; }
        public bool ModP { get; set; }
        public bool DelP { get; set; }
        public Nullable<byte> CompID { get; set; }
        public Nullable<byte> BranchID { get; set; }
    }
}