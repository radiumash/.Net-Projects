using appSchool.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DevExpress.Web;
using DevExpress.Web.Mvc;
using System.Web.UI;
using appSchool.ViewModels;
using System.Data.SqlClient;
using System.Data;
using System.IO;

namespace appSchool.Controllers
{

    [NoCache]
    public class TeacherRegistrationController : Controller
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        private SqlConnection _mConn;
        private SqlTransaction _mTran;

        public ActionResult Index()
        {
            if (Session["UserID"] == null || (int)SubMenuModules.appTeacherDataExport == 0)
            {
                return Redirect("~/");
            }
            //UserPermission objuser = new UserPermission();
            //objuser = unitOfWork.userPermissionService.CheckUserPermissionModulewise(int.Parse(Session["UserID"].ToString()), 55, byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()));
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
            return View();
        }

        public ActionResult Registrations()
        {
            if (Session["UserID"] == null || (int)SubMenuModules.appTeacherDataExport == 0)
            {
                return Redirect("~/");
            }
            //UserPermission objuser = new UserPermission();
            //objuser = unitOfWork.userPermissionService.CheckUserPermissionModulewise(int.Parse(Session["UserID"].ToString()), 55, byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()));
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
            return PartialView("Registrations");
        }
        public ActionResult grdRegistredStudents(bool mShowFilter)
        {
            if (Session["UserID"] == null) { return Redirect("~/"); }
            ViewBag.ShowFilter = mShowFilter;
            return PartialView("GridViewPartial", unitOfWork.teacherRegistrationService.GetTeacherRegistrationDataForList(byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));
        }

        public ActionResult ListRegistrations()
        {
            if (Session["UserID"] == null) { return Redirect("~/"); }
            
            return PartialView("GridViewPartial", (new UnitOfWork()).teacherRegistrationService.GetTeacherRegistrationDataForList(byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));
        }

        public ActionResult EditUpLoadDocsbyClick(int mTeacherID)
        {
            if (Session["UserID"] == null) { return Redirect("~/"); }

            modelTeacherDocumentUploadInfo obj = unitOfWork.teacherRegistrationService.GetTeacherDocumentInfo(mTeacherID);
            return PartialView("UploadDocumentPartial", obj);
        }


        public ActionResult grdStudentsDocumentsParial(int mStudentID)
        {
            if (Session["UserID"] == null) { return Redirect("~/"); }
            return PartialView("GridDocumentListPartial", unitOfWork.studentDocumentService.GetStudentDetailListByStudentID(mStudentID));
        }

        public ActionResult RefreshRegistrationGrid(bool mShowFilter)
        {
            if (Session["UserID"] == null) { return Redirect("~/"); }
            ViewBag.ShowFilter = mShowFilter;
           // return PartialView("ListRegistrations", unitOfWork.studentRegistrationService.GetRegistrationGridData(byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));
            return PartialView("GridViewPartial", unitOfWork.studentRegistrationService.GetRegistrationGridData(byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));
        }

        //public string GetStudentFullName(int StudentID)
        //{
        //    string FullName = string.Empty;

        //    FullName = unitOfWork.studentRegistrationService.GetFullNameOfStudent(StudentID, byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()));


        //    return FullName;
        //}

        public ActionResult AddNewTeacherRegistration(Teacher obj)
        {
            if (Session["UserID"] == null) { return Redirect("~/"); }
            _mConn = DB.GetActiveConnection();
            _mTran = _mConn.BeginTransaction(IsolationLevel.Snapshot);

            if (ModelState.IsValid)
            {
                try
                {
                    obj.CompID = byte.Parse(Session["CompID"].ToString());
                    obj.BranchID = byte.Parse(Session["BranchID"].ToString());

                    unitOfWork.teacherRegistrationService.AddNewTeacherRegistrationInfo(obj, byte.Parse(Session["UserID"].ToString()), byte.Parse(Session["SessionID"].ToString()));
                    unitOfWork.Save();

                    if (obj.TeacherID > 0)
                    {

                        string mTeacherLoginID = obj.FirstName + obj.TeacherID;
                        string mPassword = "admin";
                        int i = 0;
                        i = CreateTeacherLogin(obj, mTeacherLoginID, mPassword);
                        if (i > 0)
                            _mTran.Commit();
                        else
                            _mTran.Rollback();
                    }

                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            ViewData["EditableHouse"] = obj;
            return PartialView("GridViewPartial", unitOfWork.teacherRegistrationService.GetTeacherRegistrationDataForList(byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult UpdateTeacherRegistration(modelTeacherRegistration obj)
        {
            if (Session["UserID"] == null) { return Redirect("~/"); }

            if (ModelState.IsValid)
            {
                _mConn = DB.GetActiveConnection();
                _mTran = _mConn.BeginTransaction(IsolationLevel.Snapshot);
                try
                {
                    SaveUserLogForUpdate(obj);
                    _mTran.Commit();
                    unitOfWork.teacherRegistrationService.UpdateTeacherRegistrationInfo(obj, byte.Parse(Session["SessionID"].ToString()), byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()));
                    unitOfWork.Save();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            ViewData["EditableHouse"] = obj;
            return PartialView("GridViewPartial", unitOfWork.teacherRegistrationService.GetTeacherRegistrationDataForList(byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult DeleteTeacherRegistration(modelTeacherRegistration obj)
        {
            if (Session["UserID"] == null) { return Redirect("~/"); }
            _mConn = DB.GetActiveConnection();
            _mTran = _mConn.BeginTransaction(IsolationLevel.Snapshot);
            try
            {
                SaveUserLogForDelete(obj);
                _mTran.Commit();
                //unitOfWork.teacherRegistrationService.Delete(obj);
                //unitOfWork.Save();
            }
            catch (Exception e)
            {
                ViewData["EditError"] = e.Message;
            }
            return PartialView("GridViewPartial", unitOfWork.teacherRegistrationService.GetTeacherRegistrationDataForList(byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));
        }

        public void SaveUserLogForDelete(modelTeacherRegistration obj)
        {
            SqlCommand cmdMaster = new SqlCommand("UPDATE_TeacherHistory", _mConn);
            cmdMaster.CommandType = CommandType.StoredProcedure;
            cmdMaster.Transaction = _mTran;

            cmdMaster.Parameters.AddWithValue("@TeacherID", obj.TeacherID);
            cmdMaster.Parameters.AddWithValue("@ChangeBy", int.Parse(Session["UserID"].ToString()));
            cmdMaster.Parameters.AddWithValue("@IsDeleted", true);
            cmdMaster.Parameters.AddWithValue("@CompID", byte.Parse(Session["CompID"].ToString()));
            cmdMaster.Parameters.AddWithValue("@BranchID", byte.Parse(Session["BranchID"].ToString()));

            cmdMaster.ExecuteNonQuery();

        }

        public void SaveUserLogForUpdate(modelTeacherRegistration obj)
        {

            SqlCommand cmdMaster = new SqlCommand("UPDATE_TeacherHistory", _mConn);
            cmdMaster.CommandType = CommandType.StoredProcedure;
            cmdMaster.Transaction = _mTran;

            cmdMaster.Parameters.AddWithValue("@TeacherID", obj.TeacherID);
            cmdMaster.Parameters.AddWithValue("@ChangeBy", int.Parse(Session["UserID"].ToString()));
            cmdMaster.Parameters.AddWithValue("@IsDeleted", false);
            cmdMaster.Parameters.AddWithValue("@CompID", byte.Parse(Session["CompID"].ToString()));
            cmdMaster.Parameters.AddWithValue("@BranchID", byte.Parse(Session["BranchID"].ToString()));

            cmdMaster.ExecuteNonQuery();

        }


        public JsonResult GetTeacherFullName(int mTeacherID)
        {
            string FullName = string.Empty;

            FullName = unitOfWork.teacherRegistrationService.GetFullNameOfTeacher(mTeacherID, byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()));

            return Json(new { TeacherFullName = FullName }, JsonRequestBehavior.AllowGet);
        }


        public int CreateParentLogin(StudentRegistration objnew, string mParentLoginID, string mPassword)
        {
            int i = 0;

            SqlCommand cmd = new SqlCommand("Add_ParentLogin", _mConn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Transaction = _mTran;

            SqlParameter outPutParameter = new SqlParameter();
            outPutParameter.ParameterName = "@ParentLoginID";
            outPutParameter.SqlDbType = SqlDbType.Int;
            outPutParameter.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(outPutParameter);

            cmd.Parameters.AddWithValue("@StudentID ", objnew.StudentID);
            cmd.Parameters.AddWithValue("@ParentName", objnew.FatherName);
            cmd.Parameters.AddWithValue("@ParentUserName", mParentLoginID);
            cmd.Parameters.AddWithValue("@Password", mPassword);
            cmd.Parameters.AddWithValue("@CompID", byte.Parse(Session["CompID"].ToString()));
            cmd.Parameters.AddWithValue("@BranchID", byte.Parse(Session["BranchID"].ToString()));

            cmd.ExecuteNonQuery();
            string ParentLoginID = outPutParameter.Value.ToString();

            if (ParentLoginID != string.Empty && ParentLoginID != "" && ParentLoginID != null)
            {
                i = int.Parse(ParentLoginID);
            }
           
            return i;
        }

        public int CreateTeacherLogin(Teacher objnew, string mTeacherLoginID, string mTeacherPassword)
        {
            int i = 0;

            SqlCommand cmd = new SqlCommand("Add_TeacherLogin", _mConn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Transaction = _mTran;

            SqlParameter outPutParameter = new SqlParameter();
            outPutParameter.ParameterName = "@TeacherLoginID";
            outPutParameter.SqlDbType = SqlDbType.Int;
            outPutParameter.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(outPutParameter);

            cmd.Parameters.AddWithValue("@TeacherID ", objnew.TeacherID);
            cmd.Parameters.AddWithValue("@TeacherUserName", mTeacherLoginID);
            cmd.Parameters.AddWithValue("@Password", mTeacherPassword);
            cmd.Parameters.AddWithValue("@CompID", byte.Parse(Session["CompID"].ToString()));
            cmd.Parameters.AddWithValue("@BranchID", byte.Parse(Session["BranchID"].ToString()));

            cmd.ExecuteNonQuery();
            string TeacherLoginID = outPutParameter.Value.ToString();

            if (TeacherLoginID != string.Empty && TeacherLoginID != "" && TeacherLoginID != null)
            {
                i = int.Parse(TeacherLoginID);
            }

            return i;
        }


        public int CreateStudentLogin(StudentRegistration objnew, string mStudentLoginID, string mStudentPassword, int mParentID)
        {
            int i = 0;

            SqlCommand cmd = new SqlCommand("Add_StudentLogin", _mConn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Transaction = _mTran;

            SqlParameter outPutParameter = new SqlParameter();
            outPutParameter.ParameterName = "@StudentLoginID";
            outPutParameter.SqlDbType = SqlDbType.Int;
            outPutParameter.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(outPutParameter);

            cmd.Parameters.AddWithValue("@StudentID ", objnew.StudentID);
            cmd.Parameters.AddWithValue("@StudentUserName", mStudentLoginID);
            cmd.Parameters.AddWithValue("@Password", mStudentPassword);
            cmd.Parameters.AddWithValue("@ParentID", int.Parse(Session["CompID"].ToString()));
            cmd.Parameters.AddWithValue("@CompID", byte.Parse(Session["CompID"].ToString()));
            cmd.Parameters.AddWithValue("@BranchID", byte.Parse(Session["BranchID"].ToString()));

            cmd.ExecuteNonQuery();
            string ParentLoginID = outPutParameter.Value.ToString();

            if (ParentLoginID != string.Empty && ParentLoginID != "" && ParentLoginID != null)
            {
                i = int.Parse(ParentLoginID);
            }

            return i;
        }


        [HttpPost, ValidateInput(false)]
        //public ActionResult AddNewRegistration(StudentRegistration obj)
        //{
        //    if (Session["UserID"] == null) { return Redirect("~/"); }

        //    _mConn = DB.GetActiveConnection();
        //    _mTran = _mConn.BeginTransaction(IsolationLevel.Snapshot);

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            obj.CompID   = byte.Parse(Session["CompID"].ToString());
        //            obj.BranchID = byte.Parse(Session["BranchID"].ToString());
        //            obj.UIDAdd   = byte.Parse(Session["UserID"].ToString());
        //            obj.AddDate  = DateTime.Now;
        //            obj.SessionID = int.Parse(Session["SessionID"].ToString());
        //            obj.TransportRequired = false;
        //            obj.EnableSMSFeature = false;
        //            obj.HostelRequired = false;
        //            obj.TCGiven = false;

        //            unitOfWork.studentRegistrationService.AddNewRegistrationInfo(obj);
        //            unitOfWork.Save();
        //            if (obj.StudentID != null)
        //            {

        //                unitOfWork.teacherRegistrationService.AddNewTeacherRegistrationInfo(obj, byte.Parse(Session["UserID"].ToString()), byte.Parse(Session["SessionID"].ToString()));
        //                unitOfWork.Save();

        //                int i = 0; int newID = 0;
        //                if (obj.FatherName.Length > 0)
        //                {
        //                    string mparentUserName = obj.FatherName + obj.StudentID;
        //                    string mPassword = "abcd";

        //                     i = CreateParentLogin(obj, mparentUserName, mPassword);
        //                }
        //                if (i > 0)
        //                {
        //                    string mStudentUserName = obj.FirstName + obj.LastName + obj.StudentID;
        //                    string mStudentPassword = "abcd";
        //                    newID = CreateStudentLogin(obj, mStudentUserName, mStudentPassword, i);
        //                }
                       

        //                if (newID>0 && i>0)
        //                    _mTran.Commit();
        //                else
        //                    _mTran.Rollback();

        //            }
                

        //        }
        //        catch (Exception e)
        //        {
        //            ViewData["EditError"] = e.Message;
        //        }
        //    }
        //    else
        //        ViewData["EditError"] = "Please, correct all errors.";
        //    ViewData["EditableHouse"] = obj;
        //    //return PartialView("ListRegistrations", unitOfWork.studentRegistrationService.GetRegistrationGridData(byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));

        //    return PartialView("GridViewPartial", unitOfWork.teacherRegistrationService.GetTeacherRegistrationDataForList(byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));
        //}


        public void SaveUserLogForUpdate(modelRegistration obj)
        {
            SqlCommand cmdMaster = new SqlCommand("UPDATE_StudentRegistrationHistory", _mConn);
            cmdMaster.CommandType = CommandType.StoredProcedure;
            cmdMaster.Transaction = _mTran;

            cmdMaster.Parameters.AddWithValue("@StudentID", obj.StudentID);
            cmdMaster.Parameters.AddWithValue("@ChangeBy", int.Parse(Session["UserID"].ToString()));
            cmdMaster.Parameters.AddWithValue("@IsDeleted", false);
            cmdMaster.Parameters.AddWithValue("@CompID", byte.Parse(Session["CompID"].ToString()));
            cmdMaster.Parameters.AddWithValue("@BranchID", byte.Parse(Session["BranchID"].ToString()));
            cmdMaster.ExecuteNonQuery();

        }
        public void SaveUserLogForDelete(modelRegistration obj)
        {
            SqlCommand cmdMaster = new SqlCommand("UPDATE_StudentRegistrationHistory", _mConn);
            cmdMaster.CommandType = CommandType.StoredProcedure;
            cmdMaster.Transaction = _mTran;

            cmdMaster.Parameters.AddWithValue("@StudentID", obj.StudentID);
            cmdMaster.Parameters.AddWithValue("@ChangeBy", int.Parse(Session["UserID"].ToString()));
            cmdMaster.Parameters.AddWithValue("@IsDeleted", true);
            cmdMaster.Parameters.AddWithValue("@CompID", byte.Parse(Session["CompID"].ToString()));
            cmdMaster.Parameters.AddWithValue("@BranchID", byte.Parse(Session["BranchID"].ToString()));

            cmdMaster.ExecuteNonQuery();

        }

        [HttpPost, ValidateInput(false)]
        public ActionResult UpdateRegistration(modelRegistration obj)
        {
            if (Session["UserID"] == null) { return Redirect("~/"); }
            _mConn = DB.GetActiveConnection();
            _mTran = _mConn.BeginTransaction(IsolationLevel.Snapshot);
            if (ModelState.IsValid)
            {
                try
                {

                    obj.CompID = byte.Parse(Session["CompID"].ToString());
                    obj.BranchID = byte.Parse(Session["BranchID"].ToString());
                    if (SettingMasterStaticClass._ManageHistory == true)
                    {
                        SaveUserLogForUpdate(obj);
                        _mTran.Commit();
                    }
                    unitOfWork.studentRegistrationService.UpdateRegistrationInfo(obj, byte.Parse(Session["UserID"].ToString()));
                    unitOfWork.Save();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            ViewData["EditableHouse"] = obj;
            //return PartialView("ListRegistrations", unitOfWork.studentRegistrationService.GetRegistrationGridData(byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));

            return PartialView("GridViewPartial", unitOfWork.studentRegistrationService.GetRegistrationGridData(byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult DeleteRegistration(modelRegistration obj)
        {
            if (Session["UserID"] == null) { return Redirect("~/"); }
            _mConn = DB.GetActiveConnection();
            _mTran = _mConn.BeginTransaction(IsolationLevel.Snapshot);
            try
            {
                int RowCount = unitOfWork.studentRegistrationService.CheckDelete(obj.StudentID);
                if (RowCount == 0)
                {
                    if (SettingMasterStaticClass._ManageHistory == true)
                    {
                        SaveUserLogForDelete(obj);
                        _mTran.Commit();
                    }
                }
                if (RowCount == 0)
                {
                    unitOfWork.studentRegistrationService.Delete(obj);
                    unitOfWork.Save();
                }
            }
            catch (Exception e)
            {
                ViewData["EditError"] = e.Message;
            }
            return PartialView("GridViewPartial", unitOfWork.studentRegistrationService.GetRegistrationGridData(byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));
        }
       

        [HttpPost]

        public JsonResult EditPersonaldetailbyClick(int mTeacherID)
        {
            string Displaymsg = string.Empty;
            modelTeacherPersonalInfo obj = unitOfWork.teacherRegistrationService.GetPersonalInfo(mTeacherID);



            if (obj == null)
            {
                Displaymsg = "Data Not Found";
            }
            else
            {
                Displaymsg = " ";
            }
            return new JsonResult()
            {
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                Data = new
                {
                    Resultmsg = Displaymsg,
                    ResultData = cCommon.RenderRazorViewToString("PersonalInfoPartial", obj, ControllerContext, ViewData, TempData)
                }
            };

        }


        public JsonResult UpdatePhotodetailbyClick(int mTeacherID)
        {
            string Displaymsg = string.Empty;
            modelTeacherPersonalInfo obj = unitOfWork.teacherRegistrationService.GetPersonalInfo(mTeacherID);



            if (obj == null)
            {
                Displaymsg = "Data Not Found";
            }
            else
            {
                Displaymsg = " ";
            }
            return new JsonResult()
            {
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                Data = new
                {
                    Resultmsg = Displaymsg,
                    ResultData = cCommon.RenderRazorViewToString("UploadPhotoPartial", obj, ControllerContext, ViewData, TempData)
                }
            };

        }



        //public JsonResult RefreshPersonaldetailAfterUpdate(int mTeacherID)
        //{
        //    string Displaymsg = string.Empty;

        //    List<TeacherDocumentDetail> objList = unitOfWork.teacherDocumentService.GeTeacherDetailListByTeacherID(mTeacherID);

        //    return new JsonResult()
        //    {
        //        JsonRequestBehavior = JsonRequestBehavior.AllowGet,
        //        Data = new
        //        {

        //            ResultData = cCommon.RenderRazorViewToString("GridViewPartial", objList, ControllerContext, ViewData, TempData)
        //        }
        //    };

        //}

        public JsonResult EditParentdetailbyClick(int mStudentID)
        {
            // if (Session["UserID"] == null) { return Redirect("~/"); }

            modelGuardianInfo obj = unitOfWork.studentRegistrationService.GetGuardianInfo(mStudentID);

            return new JsonResult()
            {
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                Data = new
                {
                    Resultmsg = "",
                    ResultData = cCommon.RenderRazorViewToString("GuardianPartial", obj, ControllerContext, ViewData, TempData)
                }

            };


        }


        public JsonResult EditTeacherDocsUploadbyClickButton(int mTeacherID)
         {
            // if (Session["UserID"] == null) { return Redirect("~/"); }
            modelTeacherDocumentUploadInfo obj = unitOfWork.teacherRegistrationService.GetTeacherDocumentInfo(mTeacherID);

            return new JsonResult()
            {
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                Data = new { ResultData = cCommon.RenderRazorViewToString("UploadDocumentPartial", obj, ControllerContext, ViewData, TempData) }
            };

        }

        public ActionResult PartialTeacherAchivmentEdit(int mTeacherID)
        {
            if (Session["UserID"] == null) { return Redirect("~/"); }
            ViewData["TeacherIDForTAch"] = mTeacherID;

            return PartialView("GridTeacherAchivmentData", unitOfWork.teacherAchivmentService.GetTeacherAchievmentGridData(mTeacherID));
        }

        public ActionResult UpdateTeacherAchivmentEdit(MVCxGridViewBatchUpdateValues<TeacherAchievement, int> updateValues, int mTeacherID)
        {
            if (Session["UserID"] == null) { return Redirect("~/"); }

            try
            {
                foreach (var TAch in updateValues.Insert)
                {
                    if (updateValues.IsValid(TAch))
                        InsertTAch(TAch, updateValues, mTeacherID);
                }
                foreach (var TAch in updateValues.Update)
                {
                    if (updateValues.IsValid(TAch))
                        UpdateTAch(TAch, updateValues);
                }
                foreach (var TAchID in updateValues.DeleteKeys)
                {
                    DeleteTAch(TAchID, updateValues);
                }


                //decimal FeesAmount = 0;

                //FeesAmount = unitOfWork.feesStructureDetailService.GetTotalFeesAmountByClassID(mFeesStructID, int.Parse(Session["SessionID"].ToString()));

                //FeesStructureMaster objSM = new FeesStructureMaster();
                //objSM.FeeStructAmount = FeesAmount;
                //objSM.FeeStructID = mFeesStructID;
                //unitOfWork.feesStructureMasterService.UpdateFeesAmountMaster(objSM);
                //unitOfWork.Save();


            }
            catch (Exception e)
            {

            }


            ViewData["TeacherIDForTAch"] = mTeacherID;
            return PartialView("GridTeacherAchivmentData", unitOfWork.teacherAchivmentService.GetTeacherAchievmentDetailbyTeacherID(mTeacherID));
        }

        public JsonResult EditPreviousDetail(int mStudentID)
        {
            //if (Session["UserID"] == null) { return Redirect("~/"); }
            StudentPreviousDetail obj = unitOfWork.studentPriviousDetailsService.GetStudentPriviousInfo(mStudentID);
            return new JsonResult()
            {
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                Data = new
                {
                    Resultmsg = "",
                    ResultData = cCommon.RenderRazorViewToString("StudentPriviousPartial", obj, ControllerContext, ViewData, TempData)
                }

            };

        }

        public ActionResult EditStudentBackLockSessionDetailbyClick(int mStudentID)
        {
            if (Session["UserID"] == null) { return Redirect("~/"); }
            return PartialView("ListStudentPrevioussessiondetail", unitOfWork.studentRegistrationService.GetStudentAllSessiondetailbyStudentID(mStudentID, int.Parse(Session["SessionID"].ToString()), byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));

        }

 
        public ActionResult EditUpLoadPhotobyClick(int mTeacherID)
        {
            if (Session["UserID"] == null) { return Redirect("~/"); }
            //modelTeacherPhotoUploadInfo obj = unitOfWork.teacherRegistrationService.GetTeacherPhotoInfo(mTeacherID);
            //return PartialView("UploadPhotoPartial", obj);
            modelTeacherPhotoUploadInfo obj = unitOfWork.teacherRegistrationService.GetTeacherPhotoInfo(mTeacherID);

            return new JsonResult()
            {
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                Data = new { ResultData = cCommon.RenderRazorViewToString("UploadPhotoPartial", obj, ControllerContext, ViewData, TempData) }
            };



        }
        public ActionResult UploadSignature(modelTeacherSignatureUploadInfo obj)
        {
            if (Session["UserID"] == null) { return Redirect("~/"); }
            UploadControlSignature objup = new UploadControlSignature();
            objup.EmployeeCode = obj.EmployeeCode;
            objup.TeacherID = obj.TeacherID;
            UploadControlExtension.GetUploadedFiles("ucSign", UploadControlSignature.ValidationSettings, UploadControlSignature.uc_FileUploadComplete);
            return null;
        }


        public JsonResult EditSignatureDetailbyClickButton(int mTeacherID)
        {
            // if (Session["UserID"] == null) { return Redirect("~/"); }
            modelTeacherSignatureUploadInfo obj = unitOfWork.teacherRegistrationService.GetTeacherSignatureInfo(mTeacherID);
            return new JsonResult()
            {
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                Data = new { ResultData = cCommon.RenderRazorViewToString("UploadSignaturePartial", obj, ControllerContext, ViewData, TempData) }
            };

        }

        public JsonResult EditExpertiesDetailbyClickButton(int mTeacherID)
        {
            List<TeacherExpertise> obj1 = (new TeacherExpertiseRepository()).GetTeacherExpertiseGridData(mTeacherID);
            ViewData["TeacherIDForTex"] = mTeacherID;
            return new JsonResult()
            {
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                Data = new { ResultData = cCommon.RenderRazorViewToString("GridTeacherExprtiseData", obj1, ControllerContext, ViewData, TempData) }
            };
        }


        public JsonResult EditQualificationsDetailbyClickButton(int mTeacherID)
        {
            //if (Session["UserID"] == null) { return Redirect("~/"); }

            List<TeacherQualification> obj1 = (new TeacherQualificationRepository()).GetTeacherQualificationGridData(mTeacherID);
            ViewData["TeacherIDForTQ"] = mTeacherID;
            // return PartialView("GridTeacherQualificationData", obj1);

            return new JsonResult()
            {
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                Data = new { ResultData = cCommon.RenderRazorViewToString("GridTeacherQualificationData", obj1, ControllerContext, ViewData, TempData) }
            };

        }


        public JsonResult EditSubjectExpertisDetailbyClickButton(int mTeacherID)
        {
            //if (Session["UserID"] == null) { return Redirect("~/"); }

            List<TeacherSubjectExpertise> obj1 = (new SubjectExpertiseRepository()).GetSubjectExpertiseGridData(mTeacherID);
            ViewData["TeacherIDForSbEx"] = mTeacherID;
            return new JsonResult()
            {
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                Data = new { ResultData = cCommon.RenderRazorViewToString("GridSubjectExprtiseData", obj1, ControllerContext, ViewData, TempData) }
            };

        }

        public JsonResult EditTeacherAchievementsDetailbyClickButton(int mTeacherID)
        {
            // if (Session["UserID"] == null) { return Redirect("~/"); }
            List<TeacherAchievement> obj1 = (new TeacherAchivmentRepository()).GetTeacherAchievmentGridData(mTeacherID);
            ViewData["TeacherIDForTAch"] = mTeacherID;

            return new JsonResult()
            {
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                Data = new { ResultData = cCommon.RenderRazorViewToString("GridTeacherAchivmentData", obj1, ControllerContext, ViewData, TempData) }
            };

        }


        public ActionResult TeacherSignaturedisplay(int RegID)
        {
            if (Session["UserID"] == null) { return Redirect("~/"); }

            modelTeacherSignatureUploadInfo obj = new modelTeacherSignatureUploadInfo();
            obj = unitOfWork.teacherRegistrationService.GetTeacherSignatureInfo(RegID);

            return PartialView("UploadSignatureEditPartial", obj);
        }

        [HttpPost]
        public JsonResult UpdateProfileInfo(modelTeacherPersonalInfo obj)
        {

            //string DisplayMsg = string.Empty;
            //if (ModelState.IsValid)
            //{
            //    try
            //    {
            //        unitOfWork.teacherRegistrationService.UpdatePersonalInfo(obj);
            //        unitOfWork.Save();
            //        DisplayMsg = "Update Successfully !";


            //    }
            //    catch (Exception e)
            //    {
            //        ViewData["EditError"] = e.Message;
            //        DisplayMsg = e.Message;
            //    }
            //}
            //else
            //{
            //    ViewData["EditError"] = "Please, correct all errors.";
            //    DisplayMsg = "Please, correct all errors.";
            //}
            //List<StudentRegistration> objList = unitOfWork.studentRegistrationService.GetRegistrationGridData(byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()));
            //return new JsonResult()
            //{
            //    JsonRequestBehavior = JsonRequestBehavior.AllowGet,
            //    Data = new
            //    {
            //        Resultmsg = DisplayMsg,
            //        //ResultData = cCommon.RenderRazorViewToString("ListRegistrations", objList, ControllerContext, ViewData, TempData)
            //        ResultData = cCommon.RenderRazorViewToString("GridViewPartial", objList, ControllerContext, ViewData, TempData)
            //    }
            //};


            string DisplayMsg = string.Empty;
            if (ModelState.IsValid)
            {
                try
                {
                    unitOfWork.teacherRegistrationService.UpdatePersonalInfo(obj);
                    unitOfWork.Save();
                    DisplayMsg = "Update Successfully.";
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                    DisplayMsg = e.Message;
                }
            }
            else
            {
                ViewData["EditError"] = "Please, correct all errors.";
                DisplayMsg = "Please, correct all errors.";
            }
            List<Teacher> objList = (new UnitOfWork()).teacherRegistrationService.GetTeacherRegistrationDataForList(byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()));
            return new JsonResult()
            {
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                Data = new
                {
                    ResultMsg = DisplayMsg,
                    DataResult = cCommon.RenderRazorViewToString("GridViewPartial", objList, ControllerContext,ViewData,TempData)
                }
            };

        }


        [HttpPost]
        public ActionResult UpdateProfileInfotest(modelStudentPersonalInfo obj)
        {
            //string DisplayMsg = string.Empty;
            //if (ModelState.IsValid)
            //{
            //    try
            //    {
            //        unitOfWork.studentRegistrationService.UpdatePersonalInfo(obj);
            //        unitOfWork.Save();

            //        DisplayMsg = "Update Successfully !";


            //    }
            //    catch (Exception e)
            //    {
            //        ViewData["EditError"] = e.Message;
            //        DisplayMsg = e.Message;
            //    }
            //}
            //else
            //{
            //    ViewData["EditError"] = "Please, correct all errors.";
            //    DisplayMsg = "Please, correct all errors.";
            //}
            //List<StudentRegistration> objList = unitOfWork.studentRegistrationService.GetRegistrationGridData(byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()));
            //return new JsonResult()
            //{
            //    JsonRequestBehavior = JsonRequestBehavior.AllowGet,
            //    Data = new
            //    {
            //        Resultmsg = DisplayMsg,
            //        //ResultData = cCommon.RenderRazorViewToString("ListRegistrations", objList, ControllerContext, ViewData, TempData)
            //        ResultData = cCommon.RenderRazorViewToString("GridViewPartial", objList, ControllerContext, ViewData, TempData)
            //    }
            //};

            obj.Caste = "castechange";

            //return PartialView("PersonalInfoPartial", obj);
            return View("Index");

        }




        [HttpPost]
        public JsonResult UpdatePhotoInfo(modelTeacherPersonalInfo obj)
        {

            //string DisplayMsg = string.Empty;
            //if (ModelState.IsValid)
            //{
            //    try
            //    {
            //        unitOfWork.teacherRegistrationService.UpdatePersonalInfo(obj);
            //        unitOfWork.Save();
            //        DisplayMsg = "Update Successfully !";


            //    }
            //    catch (Exception e)
            //    {
            //        ViewData["EditError"] = e.Message;
            //        DisplayMsg = e.Message;
            //    }
            //}
            //else
            //{
            //    ViewData["EditError"] = "Please, correct all errors.";
            //    DisplayMsg = "Please, correct all errors.";
            //}
            //List<StudentRegistration> objList = unitOfWork.studentRegistrationService.GetRegistrationGridData(byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()));
            //return new JsonResult()
            //{
            //    JsonRequestBehavior = JsonRequestBehavior.AllowGet,
            //    Data = new
            //    {
            //        Resultmsg = DisplayMsg,
            //        //ResultData = cCommon.RenderRazorViewToString("ListRegistrations", objList, ControllerContext, ViewData, TempData)
            //        ResultData = cCommon.RenderRazorViewToString("GridViewPartial", objList, ControllerContext, ViewData, TempData)
            //    }
            //};


            string DisplayMsg = string.Empty;
            if (ModelState.IsValid)
            {
                try
                {
                    unitOfWork.teacherRegistrationService.UpdatePersonalInfo(obj);
                    unitOfWork.Save();
                    DisplayMsg = "Update Successfully.";
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                    DisplayMsg = e.Message;
                }
            }
            else
            {
                ViewData["EditError"] = "Please, correct all errors.";
                DisplayMsg = "Please, correct all errors.";
            }
            List<Teacher> objList = (new UnitOfWork()).teacherRegistrationService.GetTeacherRegistrationDataForList(byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()));
            return new JsonResult()
            {
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                Data = new
                {
                    ResultMsg = DisplayMsg,
                    DataResult = cCommon.RenderRazorViewToString("GridViewPartial", objList, ControllerContext, ViewData, TempData)
                }
            };

        }


        [HttpPost]
        public JsonResult UpdateGuardianInfo(modelGuardianInfo obj)
        {
            //  if (Session["UserID"] == null) { return Redirect("~/"); }
            string DisplayMsg = string.Empty;
            if (ModelState.IsValid)
            {
                try
                {
                    unitOfWork.studentRegistrationService.UpdateGuardianInfo(obj);
                    unitOfWork.Save();
                    DisplayMsg = "Update Successfully !";
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                    DisplayMsg = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";

            return new JsonResult()
            {
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                Data = new
                {
                    Resultmsg = DisplayMsg,
                    ResultData = cCommon.RenderRazorViewToString("GuardianEditPartial", obj, ControllerContext, ViewData, TempData)
                }
            };

        }

        public ActionResult PartialTeacherSubjectExpertiseEdit(int mTeacherID)
        {
            if (Session["UserID"] == null) { return Redirect("~/"); }
            ViewData["TeacherIDForSbEx"] = mTeacherID;

            return PartialView("GridSubjectExprtiseData", unitOfWork.subjectExpertiseService.GetSubjectExpertiseGridData(mTeacherID));
        }
        public ActionResult UpdateTeacherSubjectExpertiseEdit(MVCxGridViewBatchUpdateValues<TeacherSubjectExpertise, int> updateValues, int mTeacherID)
        {
            if (Session["UserID"] == null) { return Redirect("~/"); }

            try
            {
                foreach (var Tsbex in updateValues.Insert)
                {
                    if (updateValues.IsValid(Tsbex))
                        InsertTsbexpertise(Tsbex, updateValues, mTeacherID);
                }
                foreach (var Tsbex in updateValues.Update)
                {
                    if (updateValues.IsValid(Tsbex))
                        UpdateTsbexpertise(Tsbex, updateValues);
                }
                foreach (var TsbexID in updateValues.DeleteKeys)
                {
                    DeleteTsbexpertise(TsbexID, updateValues);
                }


                //decimal FeesAmount = 0;

                //FeesAmount = unitOfWork.feesStructureDetailService.GetTotalFeesAmountByClassID(mFeesStructID, int.Parse(Session["SessionID"].ToString()));

                //FeesStructureMaster objSM = new FeesStructureMaster();
                //objSM.FeeStructAmount = FeesAmount;
                //objSM.FeeStructID = mFeesStructID;
                //unitOfWork.feesStructureMasterService.UpdateFeesAmountMaster(objSM);
                //unitOfWork.Save();


            }
            catch (Exception e)
            {

            }


            ViewData["TeacherIDForSbEx"] = mTeacherID;
            return PartialView("GridSubjectExprtiseData", unitOfWork.subjectExpertiseService.GetSubjectExpertiseDetailbyTeacherID(mTeacherID));
        }

        protected void UpdateTsbexpertise(TeacherSubjectExpertise Tsbex, MVCxGridViewBatchUpdateValues<TeacherSubjectExpertise, int> updateValues)
        {

            try
            {
                unitOfWork.subjectExpertiseService.UpdateSubjectExpertiseDetail(Tsbex);
                unitOfWork.Save();
            }
            catch (Exception e)
            {
                updateValues.SetErrorText(Tsbex, e.Message);
            }
        }

        protected void DeleteTsbexpertise(int Tsbex, MVCxGridViewBatchUpdateValues<TeacherSubjectExpertise, int> updateValues)
        {
            try
            {
                unitOfWork.teacherExpertiseService.Delete(Tsbex);
                unitOfWork.Save();
            }
            catch (Exception e)
            {
                updateValues.SetErrorText(Tsbex, e.Message);
            }
        }


        protected void InsertTsbexpertise(TeacherSubjectExpertise Tsbex, MVCxGridViewBatchUpdateValues<TeacherSubjectExpertise, int> updateValues, int mTeacherID)
        {
            try
            {
                Tsbex.TeacherID = mTeacherID;
                Tsbex.UIDAdd = byte.Parse(Session["UserID"].ToString());
                Tsbex.AddDate = DateTime.Now;


                unitOfWork.subjectExpertiseService.Insert(Tsbex);
                unitOfWork.Save();
            }
            catch (Exception e)
            {
                updateValues.SetErrorText(Tsbex, e.Message);
            }
        }

        public ActionResult PartialTeacherQualificationEdit(int mTeacherID)
        {
            if (Session["UserID"] == null) { return Redirect("~/"); }


            return PartialView("GridTeacherQualificationData", unitOfWork.teacherQualificationService.GetTeacherQualificationGridData(mTeacherID));
        }

        //public ActionResult PartialTeacherAchivmentEdit(int mTeacherID)
        //{
        //    if (Session["UserID"] == null) { return Redirect("~/"); }
        //    ViewData["TeacherIDForTAch"] = mTeacherID;

        //    return PartialView("GridTeacherAchivmentData", unitOfWork.teacherAchivmentService.GetTeacherAchievmentGridData(mTeacherID));
        //}

        //public ActionResult UpdateTeacherAchivmentEdit(MVCxGridViewBatchUpdateValues<TeacherAchievement, int> updateValues, int mTeacherID)
        //{
        //    if (Session["UserID"] == null) { return Redirect("~/"); }

        //    try
        //    {
        //        foreach (var TAch in updateValues.Insert)
        //        {
        //            if (updateValues.IsValid(TAch))
        //                InsertTAch(TAch, updateValues, mTeacherID);
        //        }
        //        foreach (var TAch in updateValues.Update)
        //        {
        //            if (updateValues.IsValid(TAch))
        //                UpdateTAch(TAch, updateValues);
        //        }
        //        foreach (var TAchID in updateValues.DeleteKeys)
        //        {
        //            DeleteTAch(TAchID, updateValues);
        //        }


        //        //decimal FeesAmount = 0;

        //        //FeesAmount = unitOfWork.feesStructureDetailService.GetTotalFeesAmountByClassID(mFeesStructID, int.Parse(Session["SessionID"].ToString()));

        //        //FeesStructureMaster objSM = new FeesStructureMaster();
        //        //objSM.FeeStructAmount = FeesAmount;
        //        //objSM.FeeStructID = mFeesStructID;
        //        //unitOfWork.feesStructureMasterService.UpdateFeesAmountMaster(objSM);
        //        //unitOfWork.Save();


        //    }
        //    catch (Exception e)
        //    {

        //    }


        //    ViewData["TeacherIDForTAch"] = mTeacherID;
        //    return PartialView("GridTeacherAchivmentData", unitOfWork.teacherAchivmentService.GetTeacherAchievmentDetailbyTeacherID(mTeacherID));
        //}

        protected void UpdateTAch(TeacherAchievement TAch, MVCxGridViewBatchUpdateValues<TeacherAchievement, int> updateValues)
        {
            try
            {
                unitOfWork.teacherAchivmentService.UpdateTeacherAchievementDetail(TAch);
                unitOfWork.Save();
            }
            catch (Exception e)
            {
                updateValues.SetErrorText(TAch, e.Message);
            }
        }
        protected void DeleteTAch(int TAch, MVCxGridViewBatchUpdateValues<TeacherAchievement, int> updateValues)
        {
            try
            {
                unitOfWork.teacherAchivmentService.Delete(TAch);
                unitOfWork.Save();
            }
            catch (Exception e)
            {
                updateValues.SetErrorText(TAch, e.Message);
            }
        }


        protected void InsertTAch(TeacherAchievement TAch, MVCxGridViewBatchUpdateValues<TeacherAchievement, int> updateValues, int mTeacherID)
        {
            try
            {
                TAch.TeacherID = mTeacherID;
                TAch.UIDAdd = byte.Parse(Session["UserID"].ToString());
                TAch.AddDate = DateTime.Now;


                unitOfWork.teacherAchivmentService.Insert(TAch);
                unitOfWork.Save();
            }
            catch (Exception e)
            {
                updateValues.SetErrorText(TAch, e.Message);
            }
        }
        [HttpPost]
        public JsonResult StudentPriviousInfoSaveANDUpdate(StudentPreviousDetail obj)
        {
            //if (Session["UserID"] == null) { return Redirect("~/"); }
            string DisplayMsg = string.Empty;
            if (ModelState.IsValid)
            {
                try
                {

                    int CheckRow = unitOfWork.studentPriviousDetailsService.CheckPreviousDatainTable(obj.StudentID);
                    if (CheckRow > 0)
                    {
                        obj.UIDMod = byte.Parse(Session["UserID"].ToString());
                        obj.ModDate = DateTime.Now;
                        obj.CompID = byte.Parse(Session["CompID"].ToString());
                        obj.BranchID = byte.Parse(Session["BranchID"].ToString());
                        obj.SessionID = byte.Parse(Session["SessionID"].ToString());
                        unitOfWork.studentPriviousDetailsService.UpdateStudentPriviousDetail(obj);
                        unitOfWork.Save();
                        DisplayMsg = "Update Successfully";
                    }
                    else
                    {

                        obj.CompID = byte.Parse(Session["CompID"].ToString());
                        obj.BranchID = byte.Parse(Session["BranchID"].ToString());
                        obj.SessionID = byte.Parse(Session["SessionID"].ToString());
                        obj.UIDAdd = byte.Parse(Session["UserID"].ToString());
                        obj.AddDate = DateTime.Now;
                        unitOfWork.studentPriviousDetailsService.Insert(obj);
                        unitOfWork.Save();
                    }

                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                    DisplayMsg = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";

            return new JsonResult()
            {
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                Data = new
                {
                    Resultmsg = DisplayMsg,
                    ResultData = cCommon.RenderRazorViewToString("StudentPriviousEditPartial", obj, ControllerContext, ViewData, TempData)
                }
            };
            //return PartialView("StudentPriviousEditPartial", obj);

        }


        public ActionResult CallbacksImageUpload(IEnumerable<UploadedFile> ucCallbacks)
        {
            if (Session["UserID"] == null) { return Redirect("~/"); }
            return null;
        }


        [HttpPost]
        public ActionResult UploadControlCallbackAction(modelTeacherPhotoUploadInfo obj)
        {
            if (Session["UserID"] == null) { return Redirect("~/"); }
            UploadControlForTeacher objup = new UploadControlForTeacher();
            objup.EmployeeCode = obj.EmployeeCode;
            objup.TeacherID = obj.TeacherID;
            UploadControlExtension.GetUploadedFiles("uc", UploadControlForTeacher.ValidationSettings, UploadControlForTeacher.uc_FileUploadComplete);

            //UploadControlExtension.GetUploadedFiles("ucDoc", UploadControlDocument.ValidationSettings, UploadControlDocument.uc_FileUploadComplete);
            return null;

            // return    RedirectToAction("ApplicationImageView");


        }

        [HttpPost]
        public ActionResult UploadDocument(modelTeacherDocumentUploadInfo obj)
        {
            if (Session["UserID"] == null) { return Redirect("~/"); }
            UploadTeacherControlDocument objup = new UploadTeacherControlDocument();
            //objup.mEnrollmentNo = obj.EnrollmentNo;
            objup.mTeacherID = obj.TeacherID;
            objup.mDocumentName = obj.DocumentName;
            objup.mRemark = obj.Remark;
            objup.mUidAdd = int.Parse(Session["UserID"].ToString());
            objup.mSessionID = int.Parse(Session["SessionID"].ToString());
            objup.mCompID = byte.Parse(Session["CompID"].ToString());
            objup.mBranchID = byte.Parse(Session["branchID"].ToString());

            //  string ImageName = System.IO.Path.GetFileName(file.FileName);
            //string physicalPath = Server.MapPath("~/Images/students/" + ImageName);

            //// save image in folder
            //file.SaveAs(physicalPath);
            //RenderImage ren = new RenderImage();
            //ren.UploadImageToDB(file);
            //string name = obj1.FileName.Replace(obj1.UploadedFile.FileName, obj.EnrollmentNo);
            //obj1.FileName.Replace(obj1.FileName, obj.EnrollmentNo + ".jpg");

            UploadControlExtension.GetUploadedFiles("ucDoc", UploadTeacherControlDocument.ValidationSettings, UploadTeacherControlDocument.uc_FileUploadComplete);

            return null;

            // return    RedirectToAction("ApplicationImageView");
        }




    }

    public class UploadControlForTeacher
    {
        public static string _EmployeeCode = string.Empty;
        public static int _TeacherID;

        public string EmployeeCode
        {
            set { _EmployeeCode = value; }
            get { return _EmployeeCode; }
        }
        public int TeacherID
        {
            set { _TeacherID = value; }
            get { return _TeacherID; }
        }

        public const string UploadDirectory = "~/Images/teachers/";

        public static readonly DevExpress.Web.UploadControlValidationSettings ValidationSettings = new DevExpress.Web.UploadControlValidationSettings
        {
            AllowedFileExtensions = new string[] { ".jpg", ".jpeg", ".jpe", ".gif", ".bmp", ".png", },
            MaxFileSize = 20971520,
        };

        public static void uc_FileUploadComplete(object sender, FileUploadCompleteEventArgs e)
        {
            if (e.UploadedFile.IsValid)
            {

                string name = e.UploadedFile.FileName.Replace(e.UploadedFile.FileName, _TeacherID.ToString() + ".png");
                // string name = e.UploadedFile.FileName;
                string resultFilePath = HttpContext.Current.Request.MapPath(UploadDirectory + name);
                e.UploadedFile.SaveAs(resultFilePath, true);//Code Central Mode - Uncomment This Line
                Stream strm = e.UploadedFile.PostedFile.InputStream;

                var targetFile = resultFilePath;

                appSchool.Repositories.Teacher img = new appSchool.Repositories.Teacher();
                img.TeacherID = _TeacherID;
                img.AppImageName = name;

                //byte[] imageBytes = null;
                //imageBytes = e.UploadedFile.FileBytes;

                //img.AppImage = imageBytes;

                UnitOfWork obj = new UnitOfWork();

                obj.teacherRegistrationService.UpdateTeacherPhoto(img);
                obj.Save();
                IUrlResolutionService urlResolver = sender as IUrlResolutionService;
                if (urlResolver != null)
                {
                    e.CallbackData = urlResolver.ResolveClientUrl("Successfully Uploaded. ");
                }
            }
        }





    }
    public class UploadTeacherControlDemosHelper
    {
        public static string _EnrollmentNo = string.Empty;
        public static int _StudentID;

        public string mEnrollmentNo
        {
            set { _EnrollmentNo = value; }
            get { return _EnrollmentNo; }
        }
        public int mStudentID
        {
            set { _StudentID = value; }
            get { return _StudentID; }
        }

        public const string UploadDirectory = "~/Images/students/";

        public static readonly DevExpress.Web.UploadControlValidationSettings ValidationSettings = new DevExpress.Web.UploadControlValidationSettings
        {
            AllowedFileExtensions = new string[] { ".jpg", ".jpeg", ".jpe", ".gif", ".bmp", ".png", },
            MaxFileSize = 40971520,
        };

        public static void uc_FileUploadCompleteold(object sender, FileUploadCompleteEventArgs e)
        {
            if (e.UploadedFile.IsValid)
            {


                string name = e.UploadedFile.FileName.Replace(e.UploadedFile.FileName, _StudentID.ToString() + ".png");

                string resultFilePath = HttpContext.Current.Request.MapPath(UploadDirectory + name);
                e.UploadedFile.SaveAs(resultFilePath, true);//Code Central Mode - Uncomment This Line

                Stream strm = e.UploadedFile.PostedFile.InputStream;
                //Guid uid = Guid.NewGuid();
                //if (e.UploadedFile.ContentLength <= 1048576)
                //{
                //    System.Drawing.Bitmap bmpUploadImage = new System.Drawing.Bitmap(strm);
                //    System.Drawing.Image objImage = cCommon.ScaleImage(bmpUploadImage, 150);
                //    objImage.Save(resultFilePath);

                //}
                var targetFile = resultFilePath;
                //cCommon.GenerateThumbnails(0.5, strm, targetFile);

                appSchool.Repositories.StudentRegistration img = new appSchool.Repositories.StudentRegistration();
                img.StudentID = _StudentID;
                img.AppImageName = name;

                //byte[] imageBytes = null;

                //imageBytes = e.UploadedFile.FileBytes;

                //img.AppImage = imageBytes;

                UnitOfWork obj = new UnitOfWork();

                obj.studentRegistrationService.UpdateStudentPhoto(img);
                obj.Save();

                IUrlResolutionService urlResolver = sender as IUrlResolutionService;
                if (urlResolver != null)
                {
                    e.CallbackData = urlResolver.ResolveClientUrl("Successfully Uploaded. ");
                }
            }
        }

        //public static void uc_FileUploadComplete(object sender, FileUploadCompleteEventArgs e)
        //{
        //    if (e.UploadedFile.IsValid)
        //    {


        //        string name = e.UploadedFile.FileName.Replace(e.UploadedFile.FileName, _StudentID.ToString() + ".png");

        //        string resultFilePath = HttpContext.Current.Request.MapPath(UploadDirectory + name);
        //        e.UploadedFile.SaveAs(resultFilePath, true);//Code Central Mode - Uncomment This Line

        //        Stream strm = e.UploadedFile.PostedFile.InputStream;
        //        //Guid uid = Guid.NewGuid();
        //        //if (e.UploadedFile.ContentLength <= 1048576)
        //        //{
        //        //    System.Drawing.Bitmap bmpUploadImage = new System.Drawing.Bitmap(strm);
        //        //    System.Drawing.Image objImage = cCommon.ScaleImage(bmpUploadImage, 150);
        //        //    objImage.Save(resultFilePath);

        //        //}
        //        var targetFile = resultFilePath;
        //        //cCommon.GenerateThumbnails(0.5, strm, targetFile);

        //        appSchool.Repositories.StudentRegistration img = new appSchool.Repositories.StudentRegistration();
        //        img.StudentID = _StudentID;
        //        img.AppImageName = name;

        //        //byte[] imageBytes = null;

        //        //imageBytes = e.UploadedFile.FileBytes;

        //        //img.AppImage = imageBytes;

        //        UnitOfWork obj = new UnitOfWork();

        //        obj.studentRegistrationService.UpdateStudentPhoto(img);
        //        obj.Save();

        //        IUrlResolutionService urlResolver = sender as IUrlResolutionService;
        //        if (urlResolver != null)
        //        {
        //            e.CallbackData = urlResolver.ResolveClientUrl("Successfully Uploaded. ");
        //        }
        //    }
        //}



    }

    public class UploadTeacherControlDocument
    {
        public static string _EnrollmentNo = string.Empty;
        public static int _TeacherID;
        public static string _DocumentName;
        public static string _FileName;
        public static string _Remark;
        public static int _UidAdd;
        public static int _SessionID;
        public static byte _CompID;
        public static byte _BranchID;




        public string mEnrollmentNo
        {
            set { _EnrollmentNo = value; }
            get { return _EnrollmentNo; }
        }
        public int mTeacherID
        {
            set { _TeacherID = value; }
            get { return _TeacherID; }
        }
        public string mDocumentName
        {
            set { _DocumentName = value; }
            get { return _DocumentName; }
        }

        public string mFileName
        {
            set { _FileName = value; }
            get { return _FileName; }
        }
        public string mRemark
        {
            set { _Remark = value; }
            get { return _Remark; }
        }
        public int mUidAdd
        {
            set { _UidAdd = value; }
            get { return _UidAdd; }
        }

        public int mSessionID
        {
            set { _SessionID = value; }
            get { return _SessionID; }
        }


        public byte mCompID
        {
            set { _CompID = value; }
            get { return _CompID; }
        }

        public byte mBranchID
        {
            set { _BranchID = value; }
            get { return _BranchID; }
        }


        public const string UploadDirectory = "~/Images/teacherDocuments";

        public static readonly DevExpress.Web.UploadControlValidationSettings ValidationSettings = new DevExpress.Web.UploadControlValidationSettings
        {
            AllowedFileExtensions = new string[] { ".jpg", ".jpeg", ".jpe", ".gif", ".bmp", ".png", },
            MaxFileSize = 50971520,
        };

        public static void uc_FileUploadComplete(object sender, FileUploadCompleteEventArgs e)
        {
            if (e.UploadedFile.IsValid)
            {
                //string name = e.UploadedFile.FileName.Replace(e.UploadedFile.FileName, _EnrollmentNo + ".png");
                string subfolder = _TeacherID.ToString();
                string name = e.UploadedFile.FileName;
                string path = HttpContext.Current.Request.MapPath(UploadDirectory);
                if (Directory.Exists(path))
                {
                    Directory.CreateDirectory(path + "\\" + subfolder);
                }

                string resultFilePath = path + "\\" + subfolder + "\\" + name;

                e.UploadedFile.SaveAs(resultFilePath, true);//Code Central Mode - Uncomment This Line


                Stream strm = e.UploadedFile.PostedFile.InputStream;
                //Guid uid = Guid.NewGuid();
                //if (e.UploadedFile.ContentLength <= 1048576)
                //{
                //    System.Drawing.Bitmap bmpUploadImage = new System.Drawing.Bitmap(strm);
                //    System.Drawing.Image objImage = cCommon.ScaleImage(bmpUploadImage, 700);
                //    objImage.Save(resultFilePath);

                //}


                var targetFile = resultFilePath;
                cCommon.GenerateThumbnails(0.5, strm, targetFile);

                appSchool.Repositories.TeacherDocumentDetail img = new appSchool.Repositories.TeacherDocumentDetail();
                img.TeacherID = _TeacherID;
                img.DocumentName = _DocumentName;
                img.Remark = _Remark;
                img.FileName = name;
                img.UIDAdd = _UidAdd;
                img.AddDate = DateTime.Now;
                img.SessionID = _SessionID;
                img.CompID = _CompID;
                img.BranchID = _BranchID;

                UnitOfWork obj = new UnitOfWork();

                obj.teacherDocumentService.Insert(img);
                obj.Save();

                IUrlResolutionService urlResolver = sender as IUrlResolutionService;
                if (urlResolver != null)
                {
                    e.CallbackData = urlResolver.ResolveClientUrl("Successfully Uploaded. ");
                }
            }
        }





    }


    public class UploadControlSignature
    {
        public static string _EmployeeCode = string.Empty;
        public static int _TeacherID;

        public string EmployeeCode
        {
            set { _EmployeeCode = value; }
            get { return _EmployeeCode; }
        }
        public int TeacherID
        {
            set { _TeacherID = value; }
            get { return _TeacherID; }
        }

        public const string UploadDirectory = "~/Images/Signature";

        public static readonly DevExpress.Web.UploadControlValidationSettings ValidationSettings = new DevExpress.Web.UploadControlValidationSettings
        {
            AllowedFileExtensions = new string[] { ".jpg", ".jpeg", ".jpe", ".gif", ".bmp", ".png", },
            MaxFileSize = 20971520,
        };
        private static byte[] _SignatureImg;

        public static void uc_FileUploadComplete(object sender, FileUploadCompleteEventArgs e)
        {
            if (e.UploadedFile.IsValid)
            {
                string name = e.UploadedFile.FileName.Replace(e.UploadedFile.FileName, _TeacherID.ToString() + ".png");
                string resultFilePath = HttpContext.Current.Request.MapPath(UploadDirectory + "\\" + name);
                e.UploadedFile.SaveAs(resultFilePath, true);//Code Central Mode - Uncomment This Line

                //appSchool.Repositories.Teacher img = new appSchool.Repositories.Teacher();

                //img.TeacherID = _TeacherID;
                //byte[] imageBytes = null;
                //imageBytes = e.UploadedFile.FileBytes;

                //img.SignatureImg = imageBytes;

                //UnitOfWork obj = new UnitOfWork();
                //obj.teacherRegistrationService.UpdateTeacherSignature(img);
                //obj.Save();


                IUrlResolutionService urlResolver = sender as IUrlResolutionService;
                if (urlResolver != null)
                {
                    e.CallbackData = urlResolver.ResolveClientUrl("Successfully Uploaded. ");
                }

            }
        }





    }


}

