﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using appSchool.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace appSchool.Repositories
{
    public class FeesReceiptRepository : GenericRepository<FeesReceiptRepository>
    {
        public FeesReceiptRepository() : base(new dbSchoolAppEntities()) { }
        public FeesReceiptRepository(dbSchoolAppEntities dbContext) : base(dbContext) { }



        public long GetReceiptNoFromSetup(int mSessionID ,byte mCompID, byte mBranchID)
        {
            long mReceiptNo=0;

            mReceiptNo = this.context.Setups.Where(x => x.SessionID == mSessionID && x.CompID==mCompID && x.BranchID==mBranchID ).SingleOrDefault().ReceiptNo;

            return mReceiptNo;
        }


        public List<vTermListStudentFeeMaster> GetTermListForFeesReceipt(int mStudentID, int mSessionID ,byte mCompID, byte mBranchID)
        {

            List<vTermListStudentFeeMaster> obj = new List<vTermListStudentFeeMaster>();

            obj = this.context.vTermListStudentFeeMasters.Where(x => x.StudentID == mStudentID && x.SessionID == mSessionID && x.CompID==mCompID && x.BranchID==mBranchID).ToList();

            return obj;
        }


        public List<StudentFeesMaster> GetTermListForFeesReceiptStudentWise(int mStudentID, int mSessionID, byte mCompID, byte mBranchID)
        {

            List<StudentFeesMaster> obj = new List<StudentFeesMaster>();

            obj = this.context.StudentFeesMasters.Where(x => x.StudentID == mStudentID && x.SessionID == mSessionID && x.CompID==mCompID && x.BranchID==mBranchID).ToList();

            return obj;
        }


        public modelStudentFeesReceipt GetFeesReceiptDefaultData(int mUserID,int mSessionID, byte mCompID, byte mBranchID)
        {

            modelStudentFeesReceipt objFeeReceipt = new modelStudentFeesReceipt();

            objFeeReceipt.ReceiptNo = GetReceiptNoFromSetup(mSessionID, mCompID, mBranchID);
            //objFeeReceipt.ReceiptNo = objFeeReceipt.ReceiptNo + 1;
            objFeeReceipt.ReceiptDate = DateTime.Now;
            objFeeReceipt.StudentID =0;
            objFeeReceipt.EnrollmentNo = string.Empty;
            objFeeReceipt.StudentName = string.Empty;
            objFeeReceipt.FatherName = string.Empty;
            objFeeReceipt.MobileNo = string.Empty;
            objFeeReceipt.Class = string.Empty;
            objFeeReceipt.ClassID = 0;
            objFeeReceipt.ScholarNo = string.Empty;
            objFeeReceipt.TermId = 0;
            objFeeReceipt.TermName = string.Empty;
            objFeeReceipt.TermTotal =0;
            objFeeReceipt.FineAmount =  0;
            objFeeReceipt.OtherAmount = 0;
            objFeeReceipt.SubTotal = 0;
            objFeeReceipt.FinalAmount = 0;
            objFeeReceipt.DiscountAmount = 0;
            objFeeReceipt.DiscountPercent = 0;
            objFeeReceipt.PaidAmount = 0;
            objFeeReceipt.DueAmount = 0;
            objFeeReceipt.DueTermId = 0;
            objFeeReceipt.OtherHead = 0;
            objFeeReceipt.BankCode = 0;
            objFeeReceipt.Mode =  string.Empty;

            objFeeReceipt.PayMode =  string.Empty;
            objFeeReceipt.DiscountType =  string.Empty;
            objFeeReceipt.BankName =  string.Empty;
            objFeeReceipt.BranchName =  string.Empty;
            objFeeReceipt.ChequeNo =  string.Empty;
            objFeeReceipt.ChequeDate = null;
            objFeeReceipt.Remark =  "----";
            objFeeReceipt.CompID = mCompID;
            objFeeReceipt.BranchID =mBranchID;
            objFeeReceipt.SessionID = mSessionID;
            objFeeReceipt.USerID = mUserID;
            objFeeReceipt.ISSendSMS = true;




            return objFeeReceipt;
        }





    }

    public class modelStudentFeesReceipt
    {
        public long ReceiptNo { get; set; }
        public Nullable<System.DateTime> ReceiptDate { get; set; }
        public int StudentID { get; set; }
        public string EnrollmentNo { get; set; }
        public string StudentName { get; set; }
        public string FatherName { get; set; }
        public string MobileNo { get; set; }
        public string Class { get; set; }
        public int ClassID { get; set; }
        public string ScholarNo { get; set; }
        public int TermId { get; set; }
        public string TermName { get; set; }
        public decimal?  TermTotal { get; set; }
        public decimal? FineAmount { get; set; }
        public decimal? OtherAmount { get; set; }
        public decimal? SubTotal { get; set; }
        public decimal? FinalAmount { get; set; }
        public decimal? DiscountAmount { get; set; }
        public decimal? DiscountPercent { get; set; }
        public decimal? PaidAmount { get; set; }
        public decimal? DueAmount { get; set; }
        public decimal? DueTermId { get; set; }
        public decimal? OtherHead { get; set; }
        public decimal? BankCode { get; set; }
        public string Mode { get; set; }

        public string PayMode { get; set; }
        public string DiscountType { get; set; }
        public string BankName { get; set; }
        public string BranchName { get; set; }
        public string ChequeNo { get; set; }
        public Nullable<System.DateTime> ChequeDate { get; set; }
        public string Remark { get; set; }
        public int CompID { get; set; }
        public int BranchID { get; set; }
        public int SessionID { get; set; }
        public int USerID { get; set; }

        public bool ISSendSMS { get; set; }

    }


  

    




}