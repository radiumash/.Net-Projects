using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using appSchool.ViewModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System.IO;
//using DevExpress.Xpo;


namespace appSchool.Repositories
{
    public class StudentDetailsRepository : GenericRepository<vStudentSession> 
    {
        public StudentDetailsRepository() : base(new dbSchoolAppEntities()) { }
        public StudentDetailsRepository(dbSchoolAppEntities dbContext) : base(dbContext) { }
        public IEnumerable<string> GetNamesWithEnrollmentNo()
        {
            var lst = (from xx in this.context.StudentRegistrations select new{ FullName= xx.FirstName + " " + xx.LastName }).ToList();
            return (from xx in this.context.StudentRegistrations select xx.FirstName + " " + xx.LastName );
        }
       
        public vStudentSession GetStudentAllSessiondetailbyStudentID(int mStudentID, int mSessionID, byte CompID, byte mBranchID)
        {
           
            vStudentSession lst =this.context.vStudentSessions.Where(x => x.StudentID == mStudentID && x.CompID == CompID && x.SessionID == mSessionID && x.BranchID == mBranchID).FirstOrDefault(); ;
            return lst;

        }




    }
    //public static class ExtensionMethods
    //{
    //    public static bool HasFile(this HttpPostedFileBase file)
    //    {
    //        return (file != null && file.ContentLength > 0) ? true : false;
    //    }

    //}
  

}
