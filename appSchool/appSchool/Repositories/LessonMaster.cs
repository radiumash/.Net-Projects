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
    
    public partial class LessonMaster
    {
        public int ID { get; set; }
        public int ClassID { get; set; }
        public Nullable<int> SubjectID { get; set; }
        public string Path { get; set; }
        public Nullable<byte> UIDAdd { get; set; }
        public Nullable<System.DateTime> AddDate { get; set; }
        public Nullable<System.DateTime> ModDate { get; set; }
        public Nullable<byte> UIDMod { get; set; }
    }
}