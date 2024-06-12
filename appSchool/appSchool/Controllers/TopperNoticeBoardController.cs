using appSchool.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using appSchool.ViewModels;
using System.Data.SqlClient;
using System.Data;
using System.Web.UI;
using appSchool;
using Newtonsoft.Json;



namespace appSchool.Controllers
{
        [NoCache]
    public class TopperNoticeBoardController : Controller
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        private SqlConnection _mConn;
        private SqlTransaction _mTran;


        public ActionResult Index()
        {
            if (Session["UserID"] == null || (int)SubMenuModules.appTopperNoticeBoard == 0)
            {
                return Redirect("~/");
            }

            //MenuID of Classes=11
            //UserPermission objuser = new UserPermission();
            //objuser = unitOfWork.userPermissionService.CheckUserPermissionModulewise(int.Parse(Session["UserID"].ToString()), 122, byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()));
            //if (objuser != null)
            //{
            //    PermissionFlag._AddFlag = objuser.AddP;
            //    PermissionFlag._ModFlag = objuser.ModP;
            //    PermissionFlag._DelFlag = objuser.DelP;
            //}
            //else
            //{
            //    PermissionFlag._AddFlag = false;
            //    PermissionFlag._ModFlag = false;
            //    PermissionFlag._DelFlag = false;
            //}

            ViewData["ClassIDForStudentRemark"] =0;
            //ViewData["SessionIDForStudentRemark"] = 0;
            //ViewData["YearIDForStudentRemark"] =0;
            //ViewData["SemIDForStudentRemark"] = 0;

            return View();
        }


        public ActionResult PartialGridTopperNoticeBoard(byte PClassID, byte PSessionID, int pStudStatus)
        {
            if (Session["UserID"] == null) { return Redirect("~/"); }

            if (pStudStatus == 1)
            {
                ViewData["pStudStatus"] = pStudStatus;
                return PartialView("ListTopperNoticeBoard", unitOfWork.TopperNoticeBoardService.GetOldTopperStudentList(byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));
               
            }

            else
            {
                ViewData["pStudStatus"] = pStudStatus;
                ViewData["ClassIDForStudentRemark"] = PClassID;
                ViewData["SessionIDForStudentRemark"] = PSessionID;
                //ViewData["YearIDForStudentRemark"] = PYearID;
                //ViewData["SemIDForStudentRemark"] = PSemID;

                return PartialView("ListTopperNoticeBoard", unitOfWork.studentSessionService.GetStudentDetailForRemark(PClassID, PSessionID, byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));
            }      
       }

        //public ActionResult PartialGridTopperNoticeBoard()
        //{
        //    return PartialView("ListTopperNoticeBoard", unitOfWork.studentRegistrationService.GetRegistrationGridData(byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));
        //}

        [HttpPost, ValidateInput(false)]

        public ActionResult AddNewTopperNoticeBoard(TopperNoticeBoard objTpr)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    objTpr.CompID = byte.Parse(Session["CompID"].ToString());
                    objTpr.BranchID = byte.Parse(Session["BranchID"].ToString());
                    objTpr.UIDAdd = byte.Parse(Session["UserID"].ToString());
                    objTpr.AddDate = DateTime.Now;

                    unitOfWork.TopperNoticeBoardService.AddNewTopperNoticeBoard(objTpr);
                    unitOfWork.Save();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            ViewData["EditableClass"] = objTpr;
            return PartialView("ListTopperNoticeBoard", unitOfWork.TopperNoticeBoardService.GetTopperNoticeBoardList(byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));
        }

        public ActionResult GetStudentDetailForRemark(byte pClassID)
        {

             if (Session["UserID"] == null) { return Redirect("~/"); }
            string Errormsg = string.Empty;
            bool status = false;
            byte pSessionID = byte.Parse(Session["SessionID"].ToString());

            //if (pStudStatus == 1)
            //{


            //    ViewData["pStudStatus"] = pStudStatus;




            //    List<vStudentTopperList> lst = unitOfWork.TopperNoticeBoardService.GetOldTopperStudentList(byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()));
            //     return Json(new { returnStatus = status, RetErrorMsg = Errormsg, ListData = cCommon.RenderRazorViewToString("ListTopperNoticeBoard", lst, ControllerContext, ViewData, TempData) }, JsonRequestBehavior.AllowGet);
            //     status = true;
            
            //}

            
                //ViewData["pStudStatus"] = pStudStatus;
               ViewData["ClassIDForStudentRemark"] = pClassID;
               ViewData["SessionIDForStudentRemark"] = pSessionID;
               //ViewData["YearIDForStudentRemark"] = pYearID;
               //ViewData["SemIDForStudentRemark"] = pSemID;

               List<vStudentSession> lst = unitOfWork.studentSessionService.GetStudentDetailForRemark(pClassID,pSessionID, byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()));
               if (lst == null)
               {
                status = false;
                Errormsg = "Data Not Found.";
               }
               else
               {
                status = true;
               }
               return Json(new { returnStatus = status, RetErrorMsg = Errormsg, ListData = cCommon.RenderRazorViewToString("ListTopperNoticeBoard", lst, ControllerContext, ViewData, TempData) }, JsonRequestBehavior.AllowGet);
         
           
        
        }

        public string GetStudentFullName(int StudentID)
        {
            string fullName = string.Empty;

            fullName = unitOfWork.studentSessionService.GetStudentDetailByStudentID(StudentID, byte.Parse(Session["SessionID"].ToString()), byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())).FullName;

            return fullName;
        }
        //public ActionResult GetYearList(int PMCourseID)
        //{
        //    string ErrorMsg = string.Empty;
        //    bool Status = false;
        //   // string DataList = string.Empty;
        //    var DataList = string.Empty;
        //    List<YearMaster> objlst = new List<YearMaster>();
        //    objlst = unitOfWork.MCourseService.GetYearListByMCourseID(byte.Parse(PMCourseID.ToString()), byte.Parse(Session["BranchID"].ToString()), byte.Parse(Session["CompID"].ToString()));
        //    if (objlst == null)
        //    {
        //        ErrorMsg = "Year List Not Found.";
        //        Status = true;
        //    }
        //    else
        //    {
        //        DataList = JsonConvert.SerializeObject(objlst);
        //    }

        //    return Json(new {Status,ErrorMsg,DataList },JsonRequestBehavior.AllowGet);
        //}

        public JsonResult AddTopperStudent(int pStudentID,int pClassID,string pPercantage,string pToppersType, string pRank, string pSession)
        {
            string ErrorMsg = string.Empty;
            bool Status = false;
            _mConn = DB.GetActiveConnection();
            _mTran = _mConn.BeginTransaction();

            try
            {
                    //int? pClassID = 0;
                 string FirstName = string.Empty; string mClassName = string.Empty; string LastName = string.Empty; string FatherName = string.Empty; string Gender = string.Empty; 

                 StudentRegistration objstud = new StudentRegistration();

                 objstud = unitOfWork.TopperNoticeBoardService.GetTopperStudentID(pStudentID,  byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()));

                 if (objstud.FirstName != null)
                 FirstName = objstud.FirstName;
                 if (objstud.LastName != null)
                 LastName = objstud.LastName;
                 if (objstud.Gender != null) 
                  Gender = objstud.Gender;
                 if (objstud.FatherName != null)
                 FatherName = objstud.FatherName; 

                 Class objclass = new Class();
                 objclass = unitOfWork.ClassService.GetClassName(pClassID,byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()));
                 mClassName = objclass.ClassName;
                 string ImageName = pStudentID + ".png"; 

                 string StudType = "New";

                  SqlCommand cmd = new SqlCommand("Add_TopperNoticeBoard", _mConn);
                  cmd.CommandType = CommandType.StoredProcedure;
                  cmd.Transaction = _mTran;
                  SqlParameter param = cmd.Parameters.Add("TNoticeID",SqlDbType.Int);
                  param.Direction = ParameterDirection.InputOutput;
                  param.Value = 0;
                  cmd.Parameters.Add("@StudentID",SqlDbType.Int).Value=pStudentID;
                  cmd.Parameters.Add("@Percentage", SqlDbType.VarChar).Value = pPercantage;
                  cmd.Parameters.Add("@Rank", SqlDbType.VarChar).Value = pRank;
                  cmd.Parameters.Add("@FirstName", SqlDbType.VarChar).Value = FirstName;
                  cmd.Parameters.Add("@LastName", SqlDbType.VarChar).Value = LastName;
                  cmd.Parameters.Add("@ClassID", SqlDbType.TinyInt).Value = pClassID;
                  cmd.Parameters.Add("@FatherName", SqlDbType.VarChar).Value = FatherName;
                  cmd.Parameters.Add("@Gender", SqlDbType.VarChar).Value = Gender;
                  cmd.Parameters.Add("@ClassName", SqlDbType.VarChar).Value = mClassName;
                  cmd.Parameters.Add("@Session", SqlDbType.VarChar).Value = pSession;
                  cmd.Parameters.Add("@StudentType", SqlDbType.VarChar).Value = pToppersType;
                  cmd.Parameters.Add("@AppImageName", SqlDbType.VarChar).Value = ImageName;
                  cmd.Parameters.Add("@FromDate", SqlDbType.SmallDateTime).Value = DateTime.Now;
                  cmd.Parameters.Add("@ToDate", SqlDbType.SmallDateTime).Value = DateTime.Now;
                  cmd.Parameters.Add("@UIDAdd", SqlDbType.TinyInt).Value = byte.Parse(Session["UserID"].ToString());
                  cmd.Parameters.Add("@CompID", SqlDbType.TinyInt).Value = byte.Parse(Session["CompID"].ToString());
                  cmd.Parameters.Add("@BranchID", SqlDbType.TinyInt).Value = byte.Parse(Session["BranchID"].ToString());

                  cmd.ExecuteNonQuery();
       
                  int TNoticeID = int.Parse(param.Value.ToString());
                  if (TNoticeID > 0)
                  {
                   
                    Status = true;
                    _mTran.Commit();
                    ErrorMsg = "Insert Successfully.";
                  }
                  else
                  {
                   
                    Status = false;
                    _mTran.Rollback();
                   
                  }


         


            }
            catch (Exception ex)
            {
                Status = false;
                ErrorMsg = "Try again.";


            }
            return Json(new { ErrorMsg,Status },JsonRequestBehavior.AllowGet);
        }



       
      
        public void SaveUserLogForUpdate(TopperNoticeBoard obj)
        {
            SqlCommand cmdMaster = new SqlCommand("Update_ClassHistory", _mConn);
            cmdMaster.CommandType = CommandType.StoredProcedure;
            cmdMaster.Transaction = _mTran;

            //cmdMaster.Parameters.AddWithValue("@ClassId", obj.TNoticeID);
            //cmdMaster.Parameters.AddWithValue("@ChangeBy", int.Parse(Session["UserID"].ToString()));
            //cmdMaster.Parameters.AddWithValue("@IsDeleted", false);
            //cmdMaster.Parameters.AddWithValue("@CompID", byte.Parse(Session["CompID"].ToString()));
            //cmdMaster.Parameters.AddWithValue("@BranchID", byte.Parse(Session["BranchID"].ToString()));
            //cmdMaster.ExecuteNonQuery();
            
        }
        public void SaveUserLogForDelete(TopperNoticeBoard obj)
        {
            SqlCommand cmdMaster = new SqlCommand("Update_ClassHistory", _mConn);
            cmdMaster.CommandType = CommandType.StoredProcedure;
            cmdMaster.Transaction = _mTran;

            //cmdMaster.Parameters.AddWithValue("@ClassId", obj.ClassID);
            //cmdMaster.Parameters.AddWithValue("@ChangeBy", int.Parse(Session["UserID"].ToString()));
            //cmdMaster.Parameters.AddWithValue("@IsDeleted", true);
            //cmdMaster.Parameters.AddWithValue("@CompID", byte.Parse(Session["CompID"].ToString()));
            //cmdMaster.Parameters.AddWithValue("@BranchID", byte.Parse(Session["BranchID"].ToString()));

            //cmdMaster.ExecuteNonQuery();
         
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult UpdateTopperNoticeBoard(TopperNoticeBoard objTpr)
        {
            _mConn = DB.GetActiveConnection();
            _mTran = _mConn.BeginTransaction(IsolationLevel.Snapshot);
          
            if (ModelState.IsValid)
            {
                try
                {
                    objTpr.ModDate = DateTime.Now;
                    objTpr.UIDMod = byte.Parse(Session["UserID"].ToString());

                    if (SettingMasterStaticClass._ManageHistory == true)
                    {
                        SaveUserLogForUpdate(objTpr);
                    }
                    unitOfWork.TopperNoticeBoardService.UpdateTopperNoticeBoard(objTpr);
                   unitOfWork.Save();

                   _mTran.Commit();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            ViewData["EditableClass"] = objTpr;
            return PartialView("ListTopperNoticeBoard", unitOfWork.TopperNoticeBoardService.GetTopperNoticeBoardList(byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));
        }
 
        [HttpPost, ValidateInput(false)]
        public ActionResult DeleteTopperNoticeBoard(TopperNoticeBoard objTpr)
        {
           
            _mConn = DB.GetActiveConnection();
            _mTran = _mConn.BeginTransaction(IsolationLevel.Snapshot);

            try
            {
                int RowsCount = unitOfWork.TopperNoticeBoardService.CheckClassDelete(objTpr.TNoticeID);
                if (RowsCount == 0)
                {
                    if (SettingMasterStaticClass._ManageHistory == true)
                    {
                        SaveUserLogForDelete(objTpr);
                        _mTran.Commit();
                    }
                }
                #region Deletel Old Method
                if (RowsCount == 0)
                {
                    unitOfWork.TopperNoticeBoardService.DeleteTopperNoticeBoard(objTpr);
                    unitOfWork.Save();
                }
                else
                {
                }
                #endregion

            }
            catch (Exception e)
            {
                ViewData["EditError"] = e.Message;
            }
            return PartialView("ListTopperNoticeBoard", unitOfWork.TopperNoticeBoardService.GetTopperNoticeBoardList(byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));
        }


        //public ActionResult ClassGridRowChange(int RegID)
        //{
        //    return PartialView("ClassTabs", RegID);
        //}

        

    }
}
