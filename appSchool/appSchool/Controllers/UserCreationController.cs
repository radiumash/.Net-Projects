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

namespace appSchool.Controllers
{
    [NoCache]
    public class UserCreationController : Controller
    {
        private UnitOfWork unitOfWork = new UnitOfWork();

        public ActionResult Index()
        {
            if (Session["UserID"] == null || (int)SubMenuModules.appStudentRegistration == 0)
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
        public ActionResult grdRegistredUser()
        {
            if (Session["UserID"] == null) { return Redirect("~/"); }
            //ViewBag.ShowFilter = mShowFilter;
            return PartialView("GridViewPartial", unitOfWork.userLoginService.GetUserRegistrationGridData(byte.Parse(Session["CompID"].ToString()),byte.Parse(Session["BranchID"].ToString())));
        }

        public ActionResult GridViewCustomActionPartial(string customAction)
        {
            if (customAction == "delete")
            {
                //SafeExecute(() => PerformDelete());
                //string vartem = 1;
            }
            return grdRegistredUser();
        }

        public ActionResult RefreshRegistrationGrid(bool mShowFilter)
        {
            if (Session["UserID"] == null) { return Redirect("~/"); }
            ViewBag.ShowFilter = mShowFilter;
            return PartialView("GridViewPartial", unitOfWork.userLoginService.GetUserRegistrationGridData(byte.Parse(Session["CompID"].ToString()),byte.Parse(Session["BranchID"].ToString())));
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult AddNewUser(UserLogin obj)
        {
            if (Session["UserID"] == null) { return Redirect("~/"); }

            if (ModelState.IsValid)
            {
                try
                {
                    obj.BranchID = byte.Parse(Session["BranchID"].ToString());
                    obj.CompID = byte.Parse(Session["CompID"].ToString());
               
                    unitOfWork.userLoginService.AddNewRegistrationUser(obj);
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
            return PartialView("GridViewPartial", unitOfWork.userLoginService.GetUserRegistrationGridData(byte.Parse(Session["CompID"].ToString()),byte.Parse(Session["BranchID"].ToString())));
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult UpdateUser(UserLogin obj)
        {
            if (Session["UserID"] == null) { return Redirect("~/"); }
            if (ModelState.IsValid)
            {
                try
                {
                    unitOfWork.userLoginService.UpdateUserRegistration(obj,byte.Parse(Session["CompID"].ToString()),byte.Parse(Session["BranchID"].ToString()));
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
            return PartialView("GridViewPartial", unitOfWork.userLoginService.GetUserRegistrationGridData(byte.Parse(Session["CompID"].ToString()),byte.Parse(Session["BranchID"].ToString())));
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult DeleteUser(UserLogin obj)
        {
            if (Session["UserID"] == null) { return Redirect("~/"); }
            try
            {
                unitOfWork.userLoginService.Delete(obj);
                unitOfWork.Save();
            }
            catch (Exception e)
            {
                ViewData["EditError"] = e.Message;
            }
            return PartialView("GridViewPartial", unitOfWork.userLoginService.GetUserLoginList(byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));
        }

        public ActionResult StudentGridRowChange(int RegID)
        {
            if (Session["UserID"] == null) { return Redirect("~/"); }
            return PartialView("RegistrationTabs",RegID);
        }

     

     

       



        public ActionResult CallbacksImageUpload(IEnumerable<UploadedFile> ucCallbacks)
        {
            return null;
        }





        [HttpPost]
        public ActionResult UploadControlCallbackAction(modelUserPhotoUploadInfo obj)
        {
            if (Session["UserID"] == null) { return Redirect("~/"); }

            UploadControlUserCreation objup = new UploadControlUserCreation();
           // objup.mEnrollmentNo = obj.EnrollmentNo;
            objup.mUserID = obj.UserID;
            objup.mUserName = obj.UserName;

            //  string ImageName = System.IO.Path.GetFileName(file.FileName);
            //string physicalPath = Server.MapPath("~/Images/students/" + ImageName);

            //// save image in folder
            //file.SaveAs(physicalPath);

            //RenderImage ren = new RenderImage();
            //ren.UploadImageToDB(file);

            //string name = obj1.FileName.Replace(obj1.UploadedFile.FileName, obj.EnrollmentNo);

            //obj1.FileName.Replace(obj1.FileName, obj.EnrollmentNo + ".jpg");

           // UploadControlExtension.GetUploadedFiles("uc", UploadControlUserCreation.ValidationSettings, UploadControlUserCreation.uc_FileUploadComplete);

            return null;

           //0
            //return PartialView("UploadPhotoEditPartial", unitOfWork.userLoginService.GetByID(obj.UserName));
        }



    }

    public class UploadControlUserCreation
    {
      
        public static int _UserID;
        public static string _UserName;

        
        public int mUserID
        {
            set { _UserID = value; }
            get { return _UserID; }
        }
        public string mUserName
        {
            set { _UserName = value; }
            get { return _UserName; }
        }

        public const string UploadDirectory = "~/Images/users/";

        //public static readonly DevExpress.Web.UploadControlValidationSettings UploadControlValidationSettings = new DevExpress.Web.UploadControlValidationSettings
        //{
        //   // AllowedFileExtensions = new string[] { ".jpg", ".jpeg", ".jpe", ".gif", ".bmp", ".png", },
        //    //MaxFileSize = 20971520,
        //};

        public static void uc_FileUploadComplete(object sender, FileUploadCompleteEventArgs e)
        {
            if (e.UploadedFile.IsValid)
            {



                string name = e.UploadedFile.FileName.Replace(e.UploadedFile.FileName, _UserID + ".png");
                //  string name = e.UploadedFile.FileName;
                string resultFilePath = HttpContext.Current.Request.MapPath(UploadDirectory + name);
                e.UploadedFile.SaveAs(resultFilePath, true);//Code Central Mode - Uncomment This Line

                appSchool.Repositories.UserLogin img = new appSchool.Repositories.UserLogin();
                img.UserID = _UserID;
                img.UserName = _UserName;
                //img.AppImageType = 1;
                img.AppImageName = name;

                byte[] imageBytes = null;
                //BinaryReader reader = new BinaryReader(e.UploadedFile.InputStream);
                //imageBytes = reader.ReadBytes((int)Image.ContentLength);

                imageBytes = e.UploadedFile.FileBytes;

                img.AppImage = imageBytes;

                UnitOfWork obj = new UnitOfWork();

                obj.userLoginService.UpdateUserPhoto(img);
                obj.Save();




                IUrlResolutionService urlResolver = sender as IUrlResolutionService;
                if (urlResolver != null)
                {
                    e.CallbackData = urlResolver.ResolveClientUrl(resultFilePath);
                }
            }
        }



       

    }

}

