using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using appSchool.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace appSchool.Repositories
{
    public class StudentFeesMasterRepository: GenericRepository<StudentFeesMaster>
    {
        public StudentFeesMasterRepository() : base(new dbSchoolAppEntities()) { }
        public StudentFeesMasterRepository(dbSchoolAppEntities dbContext) : base(dbContext) { }




        public int GetFeesStructID(StudentFeesMaster obj)
        {
            int id = 0;
            //DateTime mAttendanceDate = obj.AttendanceDate.Date;
            StudentFeesMaster obj1 = this.context.StudentFeesMasters.Where(x => x.TermID == obj.TermID && x.SessionID == obj.SessionID && x.StudentClassId==obj.StudentClassId).FirstOrDefault();
            if (obj1 != null)
            {
                id = obj1.StudmasterID;
            }


            return id;
        }


        public StudentFeesMaster GetStudentFeesStructByStudentId(int studentID, int sessionID, int compID, int branchID)
        {
            StudentFeesMaster objstud = new StudentFeesMaster();

            List<StudentFeesMaster> listobjstud = this.context.StudentFeesMasters.Where(x => x.StudentID == studentID && x.SessionID == sessionID && x.CompID == compID && x.BranchID == branchID).ToList();

            foreach (StudentFeesMaster obj in listobjstud)
            {
                if(obj.PaidFlag == false)
                {
                    objstud = obj;
                    break;
                }
            }
            
            return objstud;
        }

        public int GetFeesStructIDForCheckDuplicateByStudentSession(StudentSession obj)
        {
            int id = 0;
            StudentFeesMaster obj1 = this.context.StudentFeesMasters.Where(x => x.StudentClassId == obj.ClassID && x.SessionID == obj.SessionID && x.StudentID == obj.StudentID && x.CompID==obj.CompID && x.BranchID==obj.BranchID ).FirstOrDefault();
            if (obj1 != null)
            {
                id = obj1.StudmasterID;
            }
            return id;
        }

        public StudentFeesMaster GetFeesDetailForStudentFeesReceipt(int mStudentID,int mTermID, int mSessionID, byte mCompID, byte mBranchID)
        {
            StudentFeesMaster obj = this.context.StudentFeesMasters.Where(x => x.StudentID == mStudentID && x.SessionID == mSessionID && x.TermID == mTermID  && x.CompID==mCompID && x.BranchID==mBranchID ).FirstOrDefault();
            return obj;
        }

        public void UpdateFeesPaidFlagINStudentFeesMaster(int StudentID, int TermID, decimal PaidAmount, int SessionID , byte mCompID, byte mBranchID)
        {

            StudentFeesMaster editFeeMaster = this.context.StudentFeesMasters.Where(x => x.StudentID == StudentID && x.TermID == TermID && x.SessionID == SessionID && x.CompID==mCompID && x.BranchID==mBranchID).SingleOrDefault();
            if (editFeeMaster != null)
            {

                editFeeMaster.PaidFlag = true;
                editFeeMaster.PaidAmount = PaidAmount;

                this.Update(editFeeMaster);
            }




        }

        public void UpdateTotalFeesAmount(int? StudentID, int? TermID, int? SessionID, decimal? FeesAmount)
        {

            StudentFeesMaster editFeeMaster = this.context.StudentFeesMasters.Where(x => x.StudentID == StudentID && x.TermID == TermID && x.SessionID == SessionID).SingleOrDefault();
            if (editFeeMaster != null)
            {

                editFeeMaster.Amount = FeesAmount;

                this.Update(editFeeMaster);
            }




        }

        public decimal GetTotalFeesAmountByStudentID(int studentID, int termID, int sessionID)
        {
            decimal? sumamount = 0;
            //DateTime mAttendanceDate = obj.AttendanceDate.Date;
            sumamount = this.context.StudentFeesDetails.SqlQuery("SELECT  Sum(HeadAmount) AS HeadAmount FROM  StudentFeesDetail  WHERE StudentID = " + studentID + " AND TermID = " + termID + "  and SessionId=  " + sessionID + "   ").FirstOrDefault().HeadAmount;
            decimal amount = Convert.ToDecimal(sumamount);
            return amount;
        }

        public void UpdateFeesAmountMaster(StudentFeesMaster objFSmaster)
        {
            StudentFeesMaster editFStructMaster = this.GetByID(objFSmaster.StudmasterID);
            if (editFStructMaster != null)
            {
                editFStructMaster.Amount = objFSmaster.Amount;
                //editFStructMaster.FeeHeadID = objFSDetail.FeeHeadID;
                //editFStructMaster.FeeTermID = objFSDetail.FeeTermID;
                this.Update(editFStructMaster);
            }

        }



    }



}