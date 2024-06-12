using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using appSchool.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace appSchool.Repositories
{
    public class StudentFeesDetailRepository: GenericRepository<StudentFeesDetail>
    {
        public StudentFeesDetailRepository() : base(new dbSchoolAppEntities()) { }
        public StudentFeesDetailRepository(dbSchoolAppEntities dbContext) : base(dbContext) { }


        public List<StudentFeesDetail> GetStudentFeesDetailByTermIDandStudentID(int mStudentID, int mTermID, int mSessionID,byte mCompID, byte mBranchID)
        {
            List<StudentFeesDetail> obj = new List<StudentFeesDetail>();
            obj = this.context.StudentFeesDetails.Where(x => x.StudentID ==mStudentID && x.TermID == mTermID && x.SessionId == mSessionID && x.CompID==mCompID && x.BranchID==mBranchID).ToList();
            return obj;
        }

        public List<StudentFeesDetail> GetStudentFeesDetailByStudMasterID(int mStudMasterID, int mSessionID)
        {
            List<StudentFeesDetail> obj = new List<StudentFeesDetail>();
            obj = this.context.StudentFeesDetails.Where(x => x.StudMasterId == mStudMasterID && x.SessionId == mSessionID).ToList();
            return obj;

        }

        public List<StudentFeesDetail> GetStudentFeesDetailByStudentIDAndTermID(int mstudentID, int mtermID, int msessionID, int mcomID, int mbranchID)
        {
            List<StudentFeesDetail> obj = new List<StudentFeesDetail>();
            obj = this.context.StudentFeesDetails.Where(x => x.StudentID == mstudentID && x.TermID == mtermID && x.SessionId == msessionID && x.CompID == mcomID && x.BranchID == mbranchID).ToList();
            return obj;

        }

        //public void InsertProduct(VFeesCompulsory objFSMaster, int mFeesStructID, int mfeesClassID, int mStructSessionID, byte mCompID, byte mBranchID)
        //{
        //    FeesStructureDetail editFStructDetail = new FeesStructureDetail();
        //    if (editFStructDetail != null)
        //    {
        //        editFStructDetail.FeeStructID = mFeesStructID;
        //        editFStructDetail.FeeAmount = objFSMaster.DefaultAmount;
        //        editFStructDetail.FeeHeadID = objFSMaster.FeesHeadID;
        //        editFStructDetail.FeeStructClassID = mfeesClassID;
        //        editFStructDetail.FeeStructSessionID = mStructSessionID;
        //        editFStructDetail.CompID = mCompID;
        //        editFStructDetail.BranchID = mBranchID;
        //        editFStructDetail.FeeTermID = objFSMaster.FeeTermID;
        //        this.Insert(editFStructDetail);
        //    }

        //}



        public void InsertStuddentFeesStructureDetail(StudentFeesDetail objFSDetail, int mfeeStructID, int sessionid, byte CompID, byte BranchID)
        {
            int mStudmasterID = 0;
            StudentFeesMaster objFSmaster = this.context.StudentFeesMasters.Where(x=> x.TermID == objFSDetail.TermID  && x.SessionID == objFSDetail.SessionId && x.StudentClassId == objFSDetail.StudentClassId ).FirstOrDefault();
            if (objFSmaster != null)
                mStudmasterID = objFSmaster.StudmasterID;

            StudentFeesDetail editFStructDetail = new StudentFeesDetail();
            if (editFStructDetail != null)
            {
                //editFStructDetail. = objFSMaster.FeeStructID;
                editFStructDetail.StudentID = objFSDetail.StudentID;
                editFStructDetail.TermID = objFSDetail.TermID;
                editFStructDetail.HeadID = objFSDetail.HeadID;
                editFStructDetail.Type = objFSDetail.Type;
                editFStructDetail.StudentClassId = objFSDetail.StudentClassId;
                editFStructDetail.StudMasterId = mStudmasterID;
                editFStructDetail.HeadAmount = objFSDetail.HeadAmount;
                editFStructDetail.SessionId = objFSDetail.SessionId;
                editFStructDetail.CompID = CompID;
                editFStructDetail.BranchID = BranchID;

                this.Insert(editFStructDetail);
            }

        }




        public void UpdateStuddentFeesStructureDetailnew(StudentFeesDetail objFSDetail, byte CompID, byte BranchID)
        {
            StudentFeesDetail editFStructDetail = this.GetByID(objFSDetail.StudentDetailID);
            if (editFStructDetail != null)
            {
                editFStructDetail.HeadAmount = objFSDetail.HeadAmount;
                editFStructDetail.HeadID = objFSDetail.HeadID;
                editFStructDetail.TermID = objFSDetail.TermID;
                editFStructDetail.CompID = CompID;
                editFStructDetail.BranchID = BranchID;

                this.Update(editFStructDetail);
            }

        }

        public void DeleteStuddentFeesStructureDetail(StudentFeesDetail objFSDetail)
        {
            StudentFeesDetail editSession = this.GetByID(objFSDetail.StudentDetailID);
            if (editSession != null)
            {
                this.Delete(editSession);
            }

        }

    }



}