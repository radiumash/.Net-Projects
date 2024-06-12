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
using System.IO;
using DevExpress.Web.Mvc;
using DevExpress.Web;


namespace appSchool.Controllers
{
        [NoCache]
    public class FlyerMasterController : Controller
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        private SqlConnection _mConn;
        private SqlTransaction _mTran;


        public ActionResult Index()
        {
            if (Session["UserID"] == null || (int)SubMenuModules.appFlyer == 0)
            {
                return Redirect("~/");
            }

            //MenuID of Classes=11
            //UserPermission objuser = new UserPermission();
            //objuser = unitOfWork.userPermissionService.CheckUserPermissionModulewise(int.Parse(Session["UserID"].ToString()), 11, byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()));
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

        public ActionResult PartialGridFlyerMaster()
        {
             if (Session["UserID"] == null) { return Redirect("~/"); }
             //ViewBag.ShowFilter = mShowFilter;
             return PartialView("ListFlyerMaster", unitOfWork.flyermasterService.GetFlyerMasterList(byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));
        }

        [HttpPost, ValidateInput(false)]

        public ActionResult EventGridRowChange(int RegID)
        {
            if (Session["UserID"] == null) { return Redirect("~/"); }

            return PartialView("FlyerMasterTabs", RegID);
        }

        public ActionResult RefreshNewsEventGrid(bool mShowFilter)
        {
            if (Session["UserID"] == null) { return Redirect("~/"); }
            ViewBag.ShowFilter = mShowFilter;
            return PartialView("ListFlyerMaster", unitOfWork.NewsEventMasterService.GetNewsEventMasterList(byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));
        }


        public void SaveUserLogForUpdate(FlyerMaster obj)
        {
            SqlCommand cmdMaster = new SqlCommand("Update_ClassHistory", _mConn);
            cmdMaster.CommandType = CommandType.StoredProcedure;
            cmdMaster.Transaction = _mTran;

            //cmdMaster.Parameters.AddWithValue("@ClassId", obj.ClassID);
            //cmdMaster.Parameters.AddWithValue("@ChangeBy", int.Parse(Session["UserID"].ToString()));
            //cmdMaster.Parameters.AddWithValue("@IsDeleted", false);
            //cmdMaster.Parameters.AddWithValue("@CompID", byte.Parse(Session["CompID"].ToString()));
            //cmdMaster.Parameters.AddWithValue("@BranchID", byte.Parse(Session["BranchID"].ToString()));
            //cmdMaster.ExecuteNonQuery();
            
        }
        public void SaveUserLogForDelete(FlyerMaster obj)
        {
            SqlCommand cmdMaster = new SqlCommand("Update_ClassHistory", _mConn);
            cmdMaster.CommandType = CommandType.StoredProcedure;
            cmdMaster.Transaction = _mTran;

            //cmdMaster.Parameters.AddWithValue("@ClassId", obj.ClassID);
            //cmdMaster.Parameters.AddWithValue("@ChangeBy", int.Parse(Session["UserID"].ToString()));
            //cmdMaster.Parameters.AddWithValue("@IsDeleted", true);
            //cmdMaster.Parameters.AddWithValue("@CompID", byte.Parse(Session["CompID"].ToString()));
            //cmdMaster.Parameters.AddWithValue("@BranchID", byte.Parse(Session["BranchID"].ToString()));

            cmdMaster.ExecuteNonQuery();
         
        }

        public ActionResult GridViewCustomActionPartial(string customAction)
        {
            if (customAction == "delete")
            {
                //SafeExecute(() => PerformDelete());
                //string vartem = 1;
            }
            return PartialGridFlyerMaster();
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult AddNewFlyerMaster(FlyerMaster objNews)
        {
            _mConn = DB.GetActiveConnection();
            _mTran = _mConn.BeginTransaction(IsolationLevel.Snapshot);
            if (ModelState.IsValid)
            {
                try
                {
                    objNews.CompID = byte.Parse(Session["CompID"].ToString());
                    objNews.BranchID = byte.Parse(Session["BranchID"].ToString());
                    //objNews.UIDAdd = byte.Parse(Session["UserID"].ToString());
                    
                    
                    unitOfWork.flyermasterService.AddNewFlyerMaster(objNews);
                    unitOfWork.Save();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please Fill all Field, & correct all errors.";
            ViewData["EditableClass"] = objNews;
            return PartialView("ListFlyerMaster", unitOfWork.flyermasterService.GetFlyerMasterList(byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult UpdateFlyerMaster(FlyerMaster objNews)
        {
            _mConn = DB.GetActiveConnection();
            _mTran = _mConn.BeginTransaction(IsolationLevel.Snapshot);
          
            if (ModelState.IsValid)
            {
                try
                {

                    objNews.CompID = byte.Parse(Session["CompID"].ToString());
                    objNews.BranchID = byte.Parse(Session["BranchID"].ToString());
                    //objNews.UIDAdd = byte.Parse(Session["UserID"].ToString());
                    ////objNews.PublishDate = DateTime.Now;
                    //objNews.AddDate = DateTime.Now;
                    

                    if (SettingMasterStaticClass._ManageHistory == true)
                    {
                        SaveUserLogForUpdate(objNews);
                    }
                    unitOfWork.flyermasterService.UpdateFlyerMaster(objNews);
                   unitOfWork.Save();

                   _mTran.Commit();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please Fill all Field, & correct all errors.";
            ViewData["EditableClass"] = objNews;
            return PartialView("ListFlyerMaster", unitOfWork.flyermasterService.GetFlyerMasterList(byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));
        }
 
        [HttpPost, ValidateInput(false)]
        public ActionResult DeleteFlyerMaster(FlyerMaster objNews)
        {
            //if (Session["UserID"] == null) { return Redirect("~/"); }
            _mConn = DB.GetActiveConnection();
            _mTran = _mConn.BeginTransaction(IsolationLevel.Snapshot);

            try
            {
                int RowsCount = unitOfWork.achievementService.CheckEventDelete(objNews.FlyerID);
                if (RowsCount == 0)
                {
                    if (SettingMasterStaticClass._ManageHistory == true)
                    {
                        SaveUserLogForDelete(objNews);
                        _mTran.Commit();
                    }
                }



                #region Delete Old Method
                if (RowsCount == 0)
                {
                    unitOfWork.flyermasterService.DeleteFlyerMaster(objNews);
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
            return PartialView("ListFlyerMaster", unitOfWork.flyermasterService.GetFlyerMasterList(byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));
        }

        

        public static string RenderRazorViewToString(string viewName, object model, ControllerContext controllerContext, ViewDataDictionary viewData, TempDataDictionary tempData)
        {
            viewData.Model = model;
            using (var sw = new StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindPartialView(controllerContext, viewName);
                var viewContext = new ViewContext(controllerContext, viewResult.View, viewData, tempData, sw);
                viewResult.View.Render(viewContext, sw);
                viewResult.ViewEngine.ReleaseView(controllerContext, viewResult.View);
                return sw.GetStringBuilder().ToString();
            }
        }

        public ActionResult UploadFlyerMasterImage(modelFlyerMasterPhotoUploadInfo obj)
        {
            if (Session["UserID"] == null) { return Redirect("~/"); }
            UploadControlDemoFlyerMaster objup = new UploadControlDemoFlyerMaster();

            objup.mEventID = obj.FlyerID;

           // UploadControlExtension.GetUploadedFiles("uc", UploadControlDemoFlyerMaster.ValidationSettings, UploadControlDemoFlyerMaster.uc_FileUploadComplete);

            return null;
        }

       


        //public ActionResult ClassGridRowChange(int RegID)
        //{
        //    return PartialView("ClassTabs", RegID);
        //}

        

    }


        public class UploadControlDemoFlyerMaster
        {

            public static int _EventID;

            public int mEventID
            {
                set { _EventID = value; }
                get { return _EventID; }
            }

            public const string UploadDirectory = "~/Images/Flyer/";

            //public static readonly DevExpress.Web.UploadControlValidationSettings UploadControlValidationSettings = new DevExpress.Web.UploadControlValidationSettings
            //{
            //    AllowedFileExtensions = new string[] { ".jpg", ".jpeg", ".jpe",".png", },
            //    MaxFileSize = 20971520,
            //};

            public static void uc_FileUploadComplete(object sender, FileUploadCompleteEventArgs e)
            {
                if (e.UploadedFile.IsValid)
                {


                    string name = e.UploadedFile.FileName.Replace(e.UploadedFile.FileName, _EventID.ToString() + ".png");

                    string FilePath = HttpContext.Current.Request.MapPath(UploadDirectory + name);

                    e.UploadedFile.SaveAs(FilePath, true);//Code Central Mode - Uncomment This Line

                    Stream strm = e.UploadedFile.PostedFile.InputStream;
                    var targetFile = FilePath;
                    //cCommon.GenerateThumbnails(0.5, strm, targetFile);

                    appSchool.Repositories.FlyerMaster img = new appSchool.Repositories.FlyerMaster();
                    img.FlyerID = _EventID;
                    img.ImageName = name;


                    UnitOfWork obj = new UnitOfWork();

                    obj.flyermasterService.UpdateFlyerMasterPhoto(img);
                    obj.Save();

                    IUrlResolutionService urlResolver = sender as IUrlResolutionService;
                    if (urlResolver != null)
                    {
                        e.CallbackData = urlResolver.ResolveClientUrl("Successfully Uploaded. ");
                    }
                }
            }





        }
}
