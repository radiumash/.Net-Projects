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
    public class MeetingRepository : GenericRepository<MettingJoinList> 
    {
        public MeetingRepository() : base(new dbSchoolAppEntities()) { }
        public MeetingRepository(dbSchoolAppEntities dbContext) : base(dbContext) { }


        public List<MettingJoinList> GetMeetingList(int mSessionID,int mClassSetUpID,int mClassID,int mCompID,int mBranchID)
        {
            List<MettingJoinList> objStudentlist = new List<MettingJoinList>();
            var param = new[] {
                             new SqlParameter("@CompID", mCompID),
                             new SqlParameter("@BranchID", mBranchID),
                             new SqlParameter("@SessionID", mSessionID),
                             new SqlParameter("@ClassID", mClassID),
                             new SqlParameter("@ClassSetUpID", mClassSetUpID),



        };
            objStudentlist = this.context.Database.SqlQuery<MettingJoinList>(
                                     "GET_OnlineMetting @CompID,@BranchID,@SessionID,@ClassID, @ClassSetUpID",
                                      param
                             ).ToList();

            return objStudentlist;
        }

     

     








       
    }

  
}
