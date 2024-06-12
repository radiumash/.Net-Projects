using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using appSchool.ViewModels;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;


namespace appSchool.Repositories
{
    public class AssignmentRepository : GenericRepository<AssignmentMaster>
    {
        public AssignmentRepository() : base(new dbSchoolAppEntities()) { }
        public AssignmentRepository(dbSchoolAppEntities dbContext) : base(dbContext) { }

        public List<AssignmentMaster> GetStudentAssigment(int mSessionID, byte mCompID, byte mBranchID,int mClassID,int mClassSetupID,int mIsAllORSubject)
        {

            List<AssignmentMaster> objAssigmentMaster = new List<AssignmentMaster>();


            var param = new[] {
                           new SqlParameter("@SessionID", mSessionID),
                            new SqlParameter("@CompID", mCompID),
                             new SqlParameter("@BranchID", mBranchID),
                             new SqlParameter("@ClassID", mClassID),
                             new SqlParameter("@ClassSetupID",mClassSetupID),
                             new SqlParameter("@IsAllORSubject",mIsAllORSubject),
                            };

            objAssigmentMaster = this.context.Database.SqlQuery<AssignmentMaster>(
                                     "Get_OnlineAssignmentForAndroid @SessionID,@CompID,@BranchID,@ClassID,@ClassSetupID,@IsAllORSubject",
                                      param
                             ).ToList();

            return objAssigmentMaster;
        }

    }
}