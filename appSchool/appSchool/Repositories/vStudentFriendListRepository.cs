using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using appSchool.ViewModels;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;


namespace appSchool.Repositories
{
    public class vStudentFriendListRepository : GenericRepository<vStudentFriendList> 
    {
        public vStudentFriendListRepository() : base(new dbSchoolAppEntities()) { }
        public vStudentFriendListRepository(dbSchoolAppEntities dbContext) : base(dbContext) { }


        public List<vStudentFriendList> GetStudentList(int mSessionID,int mClassSetUpID)
        {
            List<vStudentFriendList> objStudentlist = new List<vStudentFriendList>();
            var param = new[] { 
                           new SqlParameter("@SessionID", mSessionID),
                             new SqlParameter("@ClassSetUpID", mClassSetUpID),

                            };
            objStudentlist = this.context.Database.SqlQuery<vStudentFriendList>(
                                     "GetAllStudentsOfClass @SessionID, @ClassSetUpID",
                                      param
                             ).ToList();

            return objStudentlist;
        }

     

     








       
    }

  
}
