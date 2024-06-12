using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using appSchool.ViewModels;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using appSchool.Models;

namespace appSchool.Repositories
{
    public class AttendanceRepository : GenericRepository<AttendanceDetail> 
    {
        public AttendanceRepository() : base(new dbSchoolAppEntities()) { }
        public AttendanceRepository(dbSchoolAppEntities dbContext) : base(dbContext) { }


        public List<AttendanceDetail> GetAttendaceList(int mSessionID,int mstudentid,int mCompID,int mBranchID)
        {
            List<AttendanceDetail> objAttendacelist = new List<AttendanceDetail>();
            var param = new[] {
                             new SqlParameter("@CompID", mCompID),
                             new SqlParameter("@BranchID", mBranchID),
                             new SqlParameter("@SessionID", mSessionID),
                             new SqlParameter("@StudentID", mstudentid),



        };
            objAttendacelist = this.context.Database.SqlQuery<AttendanceDetail>(
                                     "Get_OnlinePortalClassAttendance @CompID,@BranchID,@SessionID,@StudentID",
                                      param
                             ).ToList();

            return objAttendacelist;
        }

     

     








       
    }

  
}
