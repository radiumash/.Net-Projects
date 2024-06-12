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
    public class EResultController : Controller
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        private SqlConnection _mConn;
        private SqlTransaction _mTran;

        public ActionResult Index()
        {
            if (Session["UserID"] == null || (int)SubMenuModules.appRegistrationsWithTC == 0)
            {
                return Redirect("~/");
            }
            UserPermission objuser = new UserPermission();
            objuser = unitOfWork.userPermissionService.CheckUserPermissionModulewise(int.Parse(Session["UserID"].ToString()), 109, byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()));
            if (objuser != null)
            {
                PermissionFlag._AddFlag = objuser.AddP;
                PermissionFlag._ModFlag = objuser.ModP;
                PermissionFlag._DelFlag = objuser.DelP;
            }
            else
            {
                PermissionFlag._AddFlag = false;
                PermissionFlag._ModFlag = false;
                PermissionFlag._DelFlag = false;
            }
            return PartialView("Index");
        }
       


        [HttpPost, ValidateInput(false)]
     
        public void SaveUserLogForUpdate(modelRegistration obj)
        {
     
        }
        public void SaveUserLogForDelete(modelRegistration obj)
        {

        }

        [HttpPost, ValidateInput(false)]

        public ActionResult StudentGridRowChange(int RegID)
        {
            if (Session["UserID"] == null) { return Redirect("~/"); }
            return PartialView("RegistrationTabs", RegID);
        }

        [HttpPost]

        public ActionResult CallbacksImageUpload(IEnumerable<UploadedFile> ucCallbacks)
        {
            if (Session["UserID"] == null) { return Redirect("~/"); }
            return null;
        }


        public ActionResult GetStudentResult()
        {
            if (Session["UserID"] == null) { return Redirect("~/"); }

            return PartialView("ListResult", new UnitOfWork().examSetupMasterService.GetExamResult(int.Parse(Session["UserID"].ToString()), byte.Parse(Session["SessionID"].ToString()), byte.Parse(Session["BranchID"].ToString()), byte.Parse(Session["CompID"].ToString())));
        }

        [HttpPost]
        public ActionResult UploadControlCallbackAction(modelStudentPhotoUploadInfo obj)
        {
            if (Session["UserID"] == null) { return Redirect("~/"); }
            UploadControlDemosHelpers objup = new UploadControlDemosHelpers();
            objup.mEnrollmentNo = obj.EnrollmentNo;
            objup.mStudentID = obj.StudentID;
       
            return null;

        }

        [HttpPost]
        public ActionResult UploadDocument(modelStudentDocumentUploadInfo obj)
        {
 
            return null;
       
        }

        [HttpPost]
        public ActionResult StudentPriviousInfoSaveANDUpdate(StudentPreviousDetail obj)
        {
            if (Session["UserID"] == null) { return Redirect("~/"); }
            if (ModelState.IsValid)
            {
                try
                {

                    int CheckRow = unitOfWork.studentPriviousDetailsService.CheckPreviousDatainTable(obj.StudentID);
                    if (CheckRow > 0)
                    {

                        unitOfWork.studentPriviousDetailsService.UpdateStudentPriviousDetail(obj);
                        unitOfWork.Save();

                    }
                    else
                    {
                        unitOfWork.studentPriviousDetailsService.Insert(obj);
                        unitOfWork.Save();
                    }

                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("StudentPriviousEditPartial", obj);

        }



    }


}

