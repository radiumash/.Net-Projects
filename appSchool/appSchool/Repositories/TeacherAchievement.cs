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
    
    public partial class TeacherAchievement
    {
        public int ID { get; set; }
        public int TeacherID { get; set; }
        public string AchievementName { get; set; }
        public string Description { get; set; }
        public Nullable<System.DateTime> AchievementDate { get; set; }
        public Nullable<int> SessionID { get; set; }
        public Nullable<byte> UIDAdd { get; set; }
        public Nullable<System.DateTime> AddDate { get; set; }
        public Nullable<byte> UIDMod { get; set; }
        public Nullable<System.DateTime> ModDate { get; set; }
        public byte CompID { get; set; }
        public byte BranchId { get; set; }
    }
}
