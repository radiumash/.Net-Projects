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
    public class ExamMarkPrintController : Controller
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

               
             
                
                    //List<SubjectLevelOne> objSub1list = unitOfWork.examSetupMasterService.GetSubjectLevelOneListFromExamSetup(byte.Parse(Session["BranchID"].ToString()), byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["SessionID"].ToString()), mClassID);

                   
                    //if (objSub1list.Count==0)
                    //{
                    //    ErrorMsg += " Subject Not Found. ";
                    //    msgFlag = true;
                    //}
                    DataExamList = JsonConvert.SerializeObject(objlst);
                    DataClassSetupList = JsonConvert.SerializeObject(objClassSetup);
                    //DataSub1List = JsonConvert.SerializeObject(objSub1list);
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

        public ActionResult GetStudentListForMarkEntry(int mCLassID, int mClassSetupID,int mExamID)
        {
            if (Session["UserID"] == null)
            {
                return Redirect("~/");
            }
             
            List<ExamPrintDetail> _Items=new List<ExamPrintDetail>();
            string ErrorMsg = string.Empty;
            bool msgStatus = false;
            int ExamOrder = 0;
            int MaxMark = 0;
            int MinMark = 0;
            int CompID = int.Parse(Session["CompID"].ToString());
            int BranchID = int.Parse(Session["BranchID"].ToString());
            int SessionID = int.Parse(Session["SessionID"].ToString());
            int UserID = int.Parse(Session["UserID"].ToString());
            string sqlSubject = string.Empty;
            string subIds = string.Empty;
            string sql = string.Empty;
            string sqlMain = string.Empty;
            string Subject1 = string.Empty;
            string Subject2 = string.Empty;
            string Subject3 = string.Empty;
            string Subject4 = string.Empty;
            string Subject5 = string.Empty;
            string Subject6 = string.Empty;
            string Subject7 = string.Empty;
            string Subject8 = string.Empty;

            ViewData["ClassID"] = mCLassID;
            ViewData["ClassSetupID"] = mClassSetupID;
            ViewData["ExamID"] = mExamID;
            ViewData["ExamOrder"] = 0;
            ViewData["Subject1"] = Subject1;
            ViewData["Subject2"] = Subject2;
            ViewData["Subject3"] = Subject3;
            ViewData["Subject4"] = Subject4;
            ViewData["Subject5"] = Subject5;
            ViewData["Subject6"] = Subject6;
            ViewData["Subject7"] = Subject7;
            ViewData["Subject8"] = Subject8;
            if (CompID == 2 || CompID == 5 || CompID == 6 || CompID == 7)
            {
                sqlSubject = "SELECT TOP (100) PERCENT dbo.SubjectLevelOne.IdL1 AS ID, dbo.SubjectLevelOne.SubjectNameL1 AS Subject,dbo.ExamSetupDetail.MaxMark,dbo.ExamSetupDetail.MinMark " +
                                " FROM dbo.ExamSetupMaster INNER JOIN dbo.ExamSetupDetail ON dbo.ExamSetupMaster.ExamSetupID = dbo.ExamSetupDetail.ExamSetupID LEFT OUTER JOIN " +
                                " dbo.SubjectLevelOne ON dbo.ExamSetupDetail.SubjectIDL1 = dbo.SubjectLevelOne.IdL1 AND dbo.ExamSetupDetail.BranchID = dbo.SubjectLevelOne.BranchID AND " +
                                " dbo.ExamSetupDetail.CompID = dbo.SubjectLevelOne.CompID WHERE dbo.ExamSetupMaster.ClassID=" + mCLassID +
                                " AND dbo.ExamSetupMaster.ExamID =" + mExamID + " and ExamSetupMaster.CompID=" + CompID +
                                " and ExamSetupMaster.BranchID=" + BranchID + " and ExamSetupMaster.SessionID=" + SessionID + " and ExamSetupDetail.MarksType='Number' " +
                                " ORDER BY dbo.ExamSetupDetail.OrderNo ";

            }
            if (CompID == 1 || CompID == 4)
            {
                sqlSubject = sqlSubject = "SELECT dbo.SubjectLevelTwo.IdL2 AS ID, dbo.SubjectLevelTwo.SubjectNameL2 AS Subject,dbo.ExamSetupDetail.MaxMark,dbo.ExamSetupDetail.MinMark " +
                                    " FROM dbo.ExamSetupMaster INNER JOIN dbo.ExamSetupDetail ON dbo.ExamSetupMaster.ExamSetupID = dbo.ExamSetupDetail.ExamSetupID LEFT OUTER JOIN " +
                                    " dbo.SubjectLevelTwo ON dbo.ExamSetupDetail.SubjectIDL2 = dbo.SubjectLevelTwo.IdL2 AND dbo.ExamSetupDetail.BranchID = dbo.SubjectLevelTwo.BranchID AND " +
                                    " dbo.ExamSetupDetail.CompID = dbo.SubjectLevelTwo.CompID WHERE dbo.ExamSetupMaster.ClassID=" + mCLassID +
                                    " AND dbo.ExamSetupMaster.ExamID =" + mExamID + " and ExamSetupMaster.CompID=" + CompID +
                                    " and ExamSetupMaster.BranchID=" + BranchID + " and ExamSetupMaster.SessionID=" + SessionID + " and ExamSetupDetail.MarksType='Number' " +
                                    " ORDER BY dbo.ExamSetupDetail.OrderNo ";
            }
            DataTable dt = DB.ExecuteQuery(sqlSubject);

            //int maxmark = 0;
            //List<string> lstMin = new List<string>();
            //List<string> lstMax = new List<string>();
            //List<string> sbject = new List<string>();
            //List<string> ID = new List<string>();


            int i = 1;
            foreach (DataRow dr in dt.Rows)
            {

                subIds += "[" + dr[0].ToString() + "],";
                //sql += "[" + dr[0].ToString() + "] as '" + dr[1].ToString().Trim() + "',";
                sql += "[" + dr[0].ToString() + "] as Subject" + i + ",";
                ViewData["Subject"+i+""] = dr[1].ToString().Trim();

                ++i;
            }


            if (CompID == 1 || CompID == 4 || CompID == 5 || CompID == 6 || CompID == 7)
            {
                dt.Columns.Add("Division", typeof(string));
                dt.Columns["Division"].ReadOnly = false;

                ViewData["Division"] = "Division";
            }


            ExamPrintMaster objExam = new ExamPrintMaster(mClassSetupID, mExamID, mCLassID, CompID, BranchID, SessionID);
            List<ExamPrintDetail> objLst = objExam.Items;


            return PartialView("ListExamMarksEntry", objLst);

        }

        public ActionResult PartialStudentMarkList(int PClassID,int PClassSetupID, int PExamID, int PSubjectID1, int PSubjectID2, int PExamOrder)
        {

            if (Session["UserID"] == null) { return Redirect("~/"); }
            List<ExamPrintDetail> _Items = new List<ExamPrintDetail>();
            string ErrorMsg = string.Empty;
            bool msgStatus = false;
            int ExamOrder = 0;
            int MaxMark = 0;
            int MinMark = 0;
            int CompID = int.Parse(Session["CompID"].ToString());
            int BranchID = int.Parse(Session["BranchID"].ToString());
            int SessionID = int.Parse(Session["SessionID"].ToString());
            int UserID = int.Parse(Session["UserID"].ToString());
            string sqlSubject = string.Empty;
            string subIds = string.Empty;
            string sql = string.Empty;
            string sqlMain = string.Empty;
            string Subject1 = string.Empty;
            string Subject2 = string.Empty;
            string Subject3 = string.Empty;
            string Subject4 = string.Empty;
            string Subject5 = string.Empty;
            string Subject6 = string.Empty;
            string Subject7 = string.Empty;
            string Subject8 = string.Empty;

            ViewData["ClassID"] = PClassID;
            ViewData["ClassSetupID"] = PClassSetupID;
            ViewData["ExamID"] = PExamID;
            ViewData["ExamOrder"] = 0;
         
            if (CompID == 2 || CompID == 5 || CompID == 6 || CompID == 7)
            {
                sqlSubject = "SELECT TOP (100) PERCENT dbo.SubjectLevelOne.IdL1 AS ID, dbo.SubjectLevelOne.SubjectNameL1 AS Subject,dbo.ExamSetupDetail.MaxMark,dbo.ExamSetupDetail.MinMark " +
                                " FROM dbo.ExamSetupMaster INNER JOIN dbo.ExamSetupDetail ON dbo.ExamSetupMaster.ExamSetupID = dbo.ExamSetupDetail.ExamSetupID LEFT OUTER JOIN " +
                                " dbo.SubjectLevelOne ON dbo.ExamSetupDetail.SubjectIDL1 = dbo.SubjectLevelOne.IdL1 AND dbo.ExamSetupDetail.BranchID = dbo.SubjectLevelOne.BranchID AND " +
                                " dbo.ExamSetupDetail.CompID = dbo.SubjectLevelOne.CompID WHERE dbo.ExamSetupMaster.ClassID=" + PClassID +
                                " AND dbo.ExamSetupMaster.ExamID =" + PClassID + " and ExamSetupMaster.CompID=" + CompID +
                                " and ExamSetupMaster.BranchID=" + BranchID + " and ExamSetupMaster.SessionID=" + SessionID + " and ExamSetupDetail.MarksType='Number' " +
                                " ORDER BY dbo.ExamSetupDetail.OrderNo ";

            }
            if (CompID == 1 || CompID == 4)
            {
                sqlSubject = sqlSubject = "SELECT dbo.SubjectLevelTwo.IdL2 AS ID, dbo.SubjectLevelTwo.SubjectNameL2 AS Subject,dbo.ExamSetupDetail.MaxMark,dbo.ExamSetupDetail.MinMark " +
                                    " FROM dbo.ExamSetupMaster INNER JOIN dbo.ExamSetupDetail ON dbo.ExamSetupMaster.ExamSetupID = dbo.ExamSetupDetail.ExamSetupID LEFT OUTER JOIN " +
                                    " dbo.SubjectLevelTwo ON dbo.ExamSetupDetail.SubjectIDL2 = dbo.SubjectLevelTwo.IdL2 AND dbo.ExamSetupDetail.BranchID = dbo.SubjectLevelTwo.BranchID AND " +
                                    " dbo.ExamSetupDetail.CompID = dbo.SubjectLevelTwo.CompID WHERE dbo.ExamSetupMaster.ClassID=" + PClassID +
                                    " AND dbo.ExamSetupMaster.ExamID =" + PClassID + " and ExamSetupMaster.CompID=" + CompID +
                                    " and ExamSetupMaster.BranchID=" + BranchID + " and ExamSetupMaster.SessionID=" + SessionID + " and ExamSetupDetail.MarksType='Number' " +
                                    " ORDER BY dbo.ExamSetupDetail.OrderNo ";
            }
            DataTable dt = DB.ExecuteQuery(sqlSubject);

            //int maxmark = 0;
            //List<string> lstMin = new List<string>();
            //List<string> lstMax = new List<string>();
            //List<string> sbject = new List<string>();
            //List<string> ID = new List<string>();


            int i = 1;
            foreach (DataRow dr in dt.Rows)
            {

                subIds += "[" + dr[0].ToString() + "],";
                //sql += "[" + dr[0].ToString() + "] as '" + dr[1].ToString().Trim() + "',";
                sql += "[" + dr[0].ToString() + "] as Subject" + i + ",";
                ViewData["Subject" + i + ""] = dr[1].ToString().Trim();

                ++i;
            }




            ExamPrintMaster objExam = new ExamPrintMaster(PClassSetupID, PExamID, PClassID, CompID, BranchID, SessionID);
            List<ExamPrintDetail> objLst = objExam.Items;


            return PartialView("ListExamMarksEntry", objLst);
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
