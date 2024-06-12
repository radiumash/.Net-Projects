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
    public class TeacherListRepository : GenericRepository<TeacherListDetail> 
    {
        public TeacherListRepository() : base(new dbSchoolAppEntities()) { }
        public TeacherListRepository(dbSchoolAppEntities dbContext) : base(dbContext) { }


        public List<TeacherListDetail> GetTeacherList(int mCompID,int mBranchID)
        {
            List<TeacherListDetail> objTeacherlist = new List<TeacherListDetail>();
            var param = new[] {
                             new SqlParameter("@CompID", mCompID),
                             new SqlParameter("@BranchID", mBranchID),



        };
            objTeacherlist = this.context.Database.SqlQuery<TeacherListDetail>(
                                     "Get_OnlineExamTeacherPhotoList @CompID,@BranchID",
                                      param
                             ).ToList();

            return objTeacherlist;
        }

     

     








       
    }

  
}
