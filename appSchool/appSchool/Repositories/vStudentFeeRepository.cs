using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using appSchool.ViewModels;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;



namespace appSchool.Repositories
{
    public class vStudentFeeRepository : GenericRepository<vStudentFee>
    {
        public vStudentFeeRepository() : base(new dbSchoolAppEntities()) { }
        public vStudentFeeRepository(dbSchoolAppEntities dbContext) : base(dbContext) { }





        public List<vStudentFee> GetStudentFees(int mStudentID,int mSessionID, byte mCompID, byte mBranchID)
        {

            List<vStudentFee> objFeesReminder = new List<vStudentFee>();


            var param = new[] { 
                           new SqlParameter("@SessionID", mSessionID),
                            new SqlParameter("@CompID", mCompID),
                             new SqlParameter("@BranchID", mBranchID),
                             new SqlParameter("@StudentID", mStudentID),
                            };

            objFeesReminder = this.context.Database.SqlQuery<vStudentFee>(
                                     "Get_OnlineStudentFeeForAndroid @StudentID, @SessionID, @CompID, @BranchID ",
                                      param
                             ).ToList();

            return objFeesReminder;
        }

        public List<vTotalFeesReminder> GetTotalFeesReminderStudentwise(int mSessionID, int mStudentID, byte mCompID, byte mBranchID)
        {

            List<vTotalFeesReminder> objFeesReminder = new List<vTotalFeesReminder>();

            var param = new[] { 
                           new SqlParameter("@SessionID", mSessionID),
                           new SqlParameter("@StudentID", mStudentID),
                           new SqlParameter("@CompID", mCompID),
                           new SqlParameter("@BranchID", mBranchID),

                            };

            objFeesReminder = this.context.Database.SqlQuery<vTotalFeesReminder>(
                                     "GetTotalFeesReminder @SessionID, @StudentID, @CompID, @BranchID",
                                      param
                             ).ToList();

            return objFeesReminder;
        }




    }







}