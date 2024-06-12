using appSchool.Repositories;
using appSchool.ViewModels;
using DevExpress.Web.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace appSchool.Controllers
{
    [NoCache]
    public class ExamMarkEntryController : Controller
    {

        UnitOfWork unitOfWork = new UnitOfWork();
        private SqlConnection _mConn;
        private SqlTransaction _mTran;

        public ActionResult Index()
        {

            if (Session["UserID"] == null )
            {
                return Redirect("~/");
            }
            return View();
        }

        public ActionResult GetExamListView(int mClassID)
        {
            if (Session["UserID"] == null)
            {
                return Redirect("~/");
            }

            string ErrorMsg = string.Empty;
            bool msgFlag = false;
            var DataExamList =string.Empty;
            var DataClassSetupList = string.Empty;
            var DataSub1List = string.Empty;
            if (mClassID > 0)
            {
                List<ExamMaster> objlst = new List<ExamMaster>();
                objlst = unitOfWork.examSetupMasterService.GetExamListFromExamSetupMasterByClasswise(mClassID, byte.Parse(Session["SessionID"].ToString()), byte.Parse(Session["BranchID"].ToString()), byte.Parse(Session["CompID"].ToString()));
                if (objlst.Count == 0)
                {
                    ErrorMsg += " Exams Not Founds.";
                    msgFlag = true;
                }

                List<ClassSetup> objClassSetup = new List<ClassSetup>();
                objClassSetup = unitOfWork.classSetupService.GetAllClassNameByClassID(mClassID);
                if (objClassSetup.Count == 0)
                {
                    ErrorMsg += " Section Not Found. ";
                    msgFlag = true;
                }

               
             
                
                    List<SubjectLevelOne> objSub1list = unitOfWork.examSetupMasterService.GetSubjectLevelOneListFromExamSetup(byte.Parse(Session["BranchID"].ToString()), byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["SessionID"].ToString()), mClassID);

                   
                    if (objSub1list.Count==0)
                    {
                        ErrorMsg += " Subject Not Found. ";
                        msgFlag = true;
                    }
                    DataExamList = JsonConvert.SerializeObject(objlst);
                    DataClassSetupList = JsonConvert.SerializeObject(objClassSetup);
                    DataSub1List = JsonConvert.SerializeObject(objSub1list);
            }
            else
            {
                ErrorMsg += " Please Select Class. ";
                msgFlag = true;
            }


            return new JsonResult
            {
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                Data = new  { 
                    Status=msgFlag,
                    DisplayMsg=ErrorMsg,
                    ExamNameList = DataExamList,
                    ClassSetupList = DataClassSetupList  ,
                    SubjectList = DataSub1List
                  }
            };

           

        }

        public ActionResult GetSubjectLevelTwoListView(int mClassID, int mEamID, int mSubjectID1)
        {
            if (Session["UserID"] == null)
            {
                return Redirect("~/");
            }
            string ErrorMsg = string.Empty;
            bool msgFlag = false;
            var DataSub2List = string.Empty;
            if (mClassID > 0)
            {
                
                List<SubjectLevelTwo> objSub2lit = unitOfWork.examSetupMasterService.GetSubjectLevelTWoListFromExamSetup(byte.Parse(Session["BranchID"].ToString()), byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["SessionID"].ToString()), mClassID, mEamID, mSubjectID1);
                if (objSub2lit.Count == 0)
                {
                    ErrorMsg += " Subject Level Two Not Found. ";
                    msgFlag = true;
                }
               
                DataSub2List = JsonConvert.SerializeObject(objSub2lit);
            }
            else
            {
                ErrorMsg += " Please Select Class. ";
                msgFlag = true;
            }
            return new JsonResult
            {
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                Data = new
                {
                    Status = msgFlag,
                    DisplayMsg = ErrorMsg,
                    SubjectList = DataSub2List
                }
            };



        }

        public ActionResult GetStudentListForMarkEntry(int mCLassID, int mClassSetupID,int mExamID, int mSubjectID1, int mSubjectID2, int mSubjectID3)
        {
            if (Session["UserID"] == null)
            {
                return Redirect("~/");
            }

            string ErrorMsg = string.Empty;
            bool msgStatus = false;
            int ExamOrder = 0;
            int MaxMark = 0;
            int MinMark = 0;
            int CompID = int.Parse(Session["CompID"].ToString());
            int BranchID = int.Parse(Session["BranchID"].ToString());
            int SessionID = int.Parse(Session["SessionID"].ToString());
            int UserID = int.Parse(Session["UserID"].ToString());
            int _IdL1, _IdL2, _IdL3;
            _IdL1 = mSubjectID1;
            _IdL2 = mSubjectID2;
            _IdL3 = mSubjectID3;

            string MarkType = string.Empty;

            ViewData["ClassID"] = mCLassID;
            ViewData["ClassSetupID"] = mClassSetupID;
            ViewData["ExamID"] = mExamID;
            ViewData["SubjectID1"] = mSubjectID1;
            ViewData["SubjectID2"] = mSubjectID2;
            ViewData["SubjectID2"] = mSubjectID3;
            ViewData["ExamOrder"] = 0;


            string sql = "select dbo.ExamSetupMaster.ExamOrder, ExamSetupDetail.MinMark,ExamSetupDetail.MaxMark,MarksType from dbo.ExamSetupDetail INNER JOIN dbo.ExamSetupMaster " +
                  "ON dbo.ExamSetupMaster.ExamSetupId=dbo.ExamSetupDetail.ExamSetupId where ExamSetupDetail.SubjectIDL1=" + _IdL1 + " and ExamSetupDetail.SubjectIDL2=" + _IdL2 +
                  " and ExamSetupDetail.SubjectIDL3=" + _IdL3 + " and ExamSetupMaster.ClassId=" + mCLassID + " and ExamSetupMaster.ExamId=" +
                  mExamID + " and ExamSetupMaster.SessionId=" + SessionID + " AND dbo.ExamSetupMaster.CompID=" + CompID + " AND dbo.ExamSetupMaster.BranchID=" + BranchID + "";       

            DataRow dr = DB.ExecuteSingleRow(sql);


            //DataRow dr = DB.ExecuteSingleRow(sql);
            if (dr != null)
            {

              
                ExamOrder = int.Parse(dr["ExamOrder"].ToString());
                MaxMark = int.Parse(dr["MaxMark"].ToString());
                MinMark = int.Parse(dr["MinMark"].ToString());
                MarkType = dr["MarksType"].ToString();

                ViewData["ExamOrder"] = ExamOrder;

                
                ExamResultMaster objExam = new ExamResultMaster(mClassSetupID, mSubjectID1, ExamOrder, _IdL1, _IdL2, _IdL3, MinMark,  MaxMark, CompID,  BranchID,  SessionID, UserID);
                List<ExamResultDetail> objLst = objExam.Items;


                return new JsonResult()
                {
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                    Data = new
                    {
                        DisplayMsg = ErrorMsg,
                        Status = msgStatus,
                        DataList = cCommon.RenderRazorViewToString("ListExamMarksEntry", objLst, ControllerContext, ViewData, TempData)
                    }
                };
            }



            else
            {
                 List<ExamResultDetail> objList = new List<ExamResultDetail>();
                  objList = null;
                  ErrorMsg = "Data Not Found";

                  return new JsonResult()
                 {
                      JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                     Data = new
                     {
                        DisplayMsg = ErrorMsg,
                        Status = msgStatus,
                        DataList = cCommon.RenderRazorViewToString("ListExamMarksEntry", objList, ControllerContext, ViewData, TempData)
                     }
                 };


            }
        }

        public ActionResult PartialStudentMarkList(int PClassID,int PClassSetupID, int PExamID, int PSubjectID1, int PSubjectID2, int PExamOrder)
        {

            if (Session["UserID"] == null) { return Redirect("~/"); }
            int MaxMark = 0;
            int MinMark = 0;
            ViewData["ClassID"] = PClassID;
            ViewData["ClassSetupID"] = PClassSetupID;
            ViewData["ExamID"] = PExamID;
            ViewData["SubjectID1"] = PSubjectID1;
            ViewData["ExamOrder"] = PExamOrder;
            int CompID = int.Parse(Session["CompID"].ToString());
            int BranchID = int.Parse(Session["BranchID"].ToString());
            int SessionID = int.Parse(Session["SessionID"].ToString());
            int UserID = int.Parse(Session["UserID"].ToString());


            //List<vExamMarkEntry> objLst = new List<vExamMarkEntry>();
            //objLst = unitOfWork.vExamMarkEntryService.GetStudentListForMarkEntry(PClassID, PClassSetupID, PExamID, PExamOrder, MaxMark, MinMark, PSubjectID, byte.Parse(Session["UserID"].ToString()), byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()), byte.Parse(Session["SessionID"].ToString()));

            int _IdL1, _IdL2, _IdL3;
            _IdL1 = _IdL2 = _IdL3 = -1;

            ExamResultMaster objExam = new ExamResultMaster(PClassSetupID, PSubjectID1, PExamOrder, _IdL1, _IdL2, _IdL3, MinMark, MaxMark, CompID, BranchID, SessionID, UserID);
            List<ExamResultDetail> objLst = objExam.Items;


            return PartialView("GridStudentMarkList",objLst);
        }

        [ValidateInput(false)]
        public ActionResult updateStudentMarkAll(MVCxGridViewBatchUpdateValues<ExamResultDetail, int> updateValues, int PClassID, int PClassSetupID, int PExamID, int PSubjectID1, int PSubjectID2, int PExamOrder)
        {
            if (Session["UserID"] == null) { return Redirect("~/"); }
            int MaxMark = 0;
            int MinMark = 0;
            string MarkType = string.Empty;
            ViewData["ClassID"] = PClassID;
            ViewData["ClassSetupID"] = PClassSetupID;
            ViewData["ExamID"] = PExamID;
            ViewData["SubjectID1"] = PSubjectID1;
            ViewData["ExamOrder"] = PExamOrder;

            int CompID = int.Parse(Session["CompID"].ToString());
            int BranchID = int.Parse(Session["BranchID"].ToString());
            int SessionID = int.Parse(Session["SessionID"].ToString());
            int UserID = int.Parse(Session["UserID"].ToString());




            foreach (var product in updateValues.Update)
            {
                if (updateValues.IsValid(product))
                    UpdateProduct(product, updateValues,PClassID,PClassSetupID,PExamID,PSubjectID1,PExamOrder);
            }

            //List<vExamMarkEntry> objLst = new List<vExamMarkEntry>();
            //objLst = unitOfWork.vExamMarkEntryService.GetStudentListForMarkEntry(PClassID, PClassSetupID, PExamID, PExamOrder, MaxMark, MinMark, PSubjectID, byte.Parse(Session["UserID"].ToString()), byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()), byte.Parse(Session["SessionID"].ToString()));
            int _IdL1, _IdL2, _IdL3;
            _IdL1 = _IdL2 = _IdL3 = -1;

            ExamResultMaster objExam = new ExamResultMaster(PClassSetupID, PSubjectID1, PExamOrder, _IdL1, _IdL2, _IdL3, MinMark, MaxMark, CompID, BranchID, SessionID, UserID);
            List<ExamResultDetail> objLst = objExam.Items;


            return PartialView("ListExamMarksEntry", objLst);
        }

        protected void UpdateProduct(ExamResultDetail product, MVCxGridViewBatchUpdateValues<ExamResultDetail, int> updateValues,int ClassID,int ClassSetupID,int ExamID,int SubjectID,int ExamOrder)
        {
            _mConn = DB.GetActiveConnection();
            _mTran = _mConn.BeginTransaction(IsolationLevel.Snapshot);
            try
            {

                SqlCommand cmdMaster = new SqlCommand("Update_ExamResult", _mConn);
                cmdMaster.CommandType = CommandType.StoredProcedure;
                cmdMaster.Transaction = _mTran;

                cmdMaster.Parameters.AddWithValue("@StudentID", product.StudentId);
                cmdMaster.Parameters.AddWithValue("@ClassID", ClassID);
                cmdMaster.Parameters.AddWithValue("@ExamID", ExamID);
                cmdMaster.Parameters.AddWithValue("@SubjectID", SubjectID);
                cmdMaster.Parameters.AddWithValue("@ObtainMark", product.ObtainMarks);
                cmdMaster.Parameters.AddWithValue("@IsAbsent", product.IsAbsent);
                cmdMaster.Parameters.AddWithValue("@ExamOrder", ExamOrder);
                cmdMaster.Parameters.AddWithValue("@UIDMod", byte.Parse(Session["UserID"].ToString()));
                cmdMaster.Parameters.AddWithValue("@CompID", byte.Parse(Session["CompID"].ToString()));
                cmdMaster.Parameters.AddWithValue("@BranchID", byte.Parse(Session["BranchID"].ToString()));
                cmdMaster.Parameters.AddWithValue("@SessionID", byte.Parse(Session["SessionID"].ToString()));

                int i = cmdMaster.ExecuteNonQuery();
                if (i > 0)
                    _mTran.Commit();
                else
                    _mTran.Rollback();

            }
            catch (Exception e)
            {
                updateValues.SetErrorText(product, e.Message);
            }
            finally
            {
                if(_mConn  != null)
                {
                    if (_mConn.State == ConnectionState.Open)
                        _mConn.Dispose();
                }
                  
            }

        }
      

    }
}
