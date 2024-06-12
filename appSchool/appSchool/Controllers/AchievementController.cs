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
    public class AchievementController : Controller
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        private SqlConnection _mConn;
        private SqlTransaction _mTran;


        public ActionResult Index()
        {
            if (Session["UserID"] == null || (int)SubMenuModules.appAchievement == 0)
            {
                return Redirect("~/");
            }

            //MenuID of Classes=11
            //UserPermission objuser = new UserPermission();
            //objuser = unitOfWork.userPermissionService.CheckUserPermissionModulewise(int.Parse(Session["UserID"].ToString()), 121, byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()));
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

        public ActionResult PartialGridAchievement()
        {
             if (Session["UserID"] == null) { return Redirect("~/"); }
             //ViewBag.ShowFilter = mShowFilter;
             return PartialView("ListAchievement", unitOfWork.achievementService.GetAchievementList(byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));
        }

        public ActionResult GridViewCustomActionPartial(string customAction, int pExamSyllabusID, int pClassID, int pSubjectID, int pExamID)
        {
            if (customAction == "delete")
            {
                //SafeExecute(() => PerformDelete());
                //string vartem = 1; 
            }
            return PartialGridAchievement();
        }

        [HttpPost, ValidateInput(false)]

        public ActionResult EventGridRowChange(int RegID)
        {
            if (Session["UserID"] == null) { return Redirect("~/"); }

            return PartialView("AchievementTabs", RegID);
        }

        public ActionResult RefreshNewsEventGrid(bool mShowFilter)
        {
            if (Session["UserID"] == null) { return Redirect("~/"); }
            ViewBag.ShowFilter = mShowFilter;
            return PartialView("ListNewsEventMaster", unitOfWork.NewsEventMasterService.GetNewsEventMasterList(byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));
        }


        public void SaveUserLogForUpdate(Achievement obj)
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
        public void SaveUserLogForDelete(Achievement obj)
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
        [HttpPost, ValidateInput(false)]
        public ActionResult AddNewAchievement(Achievement objNews)
        {
            _mConn = DB.GetActiveConnection();
            _mTran = _mConn.BeginTransaction(IsolationLevel.Snapshot);
            if (ModelState.IsValid)
            {
                try
                {
                    objNews.CompID = byte.Parse(Session["CompID"].ToString());
                    objNews.BranchID = byte.Parse(Session["BranchID"].ToString());
                    objNews.UIDAdd = byte.Parse(Session["UserID"].ToString());
                    
                    objNews.AddDate = DateTime.Now;
                    unitOfWork.achievementService.AddNewAchievement(objNews);
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
            return PartialView("ListAchievement", unitOfWork.achievementService.GetAchievementList(byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult UpdateAchievement(Achievement objNews)
        {
            _mConn = DB.GetActiveConnection();
            _mTran = _mConn.BeginTransaction(IsolationLevel.Snapshot);
          
            if (ModelState.IsValid)
            {
                try
                {

                    objNews.CompID = byte.Parse(Session["CompID"].ToString());
                    objNews.BranchID = byte.Parse(Session["BranchID"].ToString());
                    objNews.UIDAdd = byte.Parse(Session["UserID"].ToString());
                    //objNews.PublishDate = DateTime.Now;
                    objNews.AddDate = DateTime.Now;
                    

                    if (SettingMasterStaticClass._ManageHistory == true)
                    {
                        SaveUserLogForUpdate(objNews);
                    }
                    unitOfWork.achievementService.UpdateAchievement(objNews);
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
            return PartialView("ListAchievement", unitOfWork.achievementService.GetAchievementList(byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));
        }
 
        [HttpPost, ValidateInput(false)]
        public ActionResult DeleteAchievement(Achievement objNews)
        {
            //if (Session["UserID"] == null) { return Redirect("~/"); }
            _mConn = DB.GetActiveConnection();
            _mTran = _mConn.BeginTransaction(IsolationLevel.Snapshot);

            try
            {
                int RowsCount = unitOfWork.achievementService.CheckEventDelete(objNews.AchievementID);
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
                    unitOfWork.achievementService.DeleteAchievement(objNews);
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
            return PartialView("ListAchievement", unitOfWork.achievementService.GetAchievementList(byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));
        }

        public ActionResult UpdateAchievementInfo(modelAchievementinfo obj)
        {
            if (Session["UserID"] == null) { return Redirect("~/"); }
            if (ModelState.IsValid)
            {
                try
                {
                    unitOfWork.achievementService.UpdateAchievementInfo(obj);
                    unitOfWork.Save();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("AchievementEditPartial", obj);

        }


        [HttpPost]
        public JsonResult AddNewsEventinfo(NewsEventMaster obj)
        {
            bool ResultFlag = false;
            string Errormsg = string.Empty;
            _mConn = DB.GetActiveConnection();
            _mTran = _mConn.BeginTransaction(IsolationLevel.Snapshot);

            if (obj.EventTitle == null || obj.CategoryType==null || obj.EFromDate==null || obj.EToDate==null || obj.EventDescription==null )
            {
                Errormsg += "Please Fill all Fields";
                List<NewsEventMaster> objlist = new List<NewsEventMaster>();
                objlist = unitOfWork.NewsEventMasterService.GetNewsEventMasterList(byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()));

                return new JsonResult()
                {
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                    Data = new
                    {
                      
                        ResultFlag=false,
                        ResultMsg = Errormsg,
                        EventData=RenderRazorViewToString("NewsEventEditPartial",obj,ControllerContext,ViewData,TempData),
                        ListData = RenderRazorViewToString("ListNewsEventMaster", objlist, ControllerContext, ViewData, TempData)
                    }
                };

            }

            if (ModelState.IsValid)
            {
                try
                {
                    int rec = 0;
                    SqlCommand cmd = new SqlCommand("Add_NewsEventMaster", _mConn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Transaction = _mTran;

                    cmd.Parameters.AddWithValue("@EventTitle", obj.EventTitle);
                    cmd.Parameters.AddWithValue("@EventDescription", obj.EventDescription);
                    cmd.Parameters.AddWithValue("@CategoryType", obj.CategoryType);
                    cmd.Parameters.AddWithValue("@EFromDate", obj.EFromDate);
                    cmd.Parameters.AddWithValue("@EToDate", obj.EToDate);
                    cmd.Parameters.AddWithValue("@UIDAdd", byte.Parse(Session["UserID"].ToString()));
                    cmd.Parameters.AddWithValue("@CompID", byte.Parse(Session["CompID"].ToString()));
                    cmd.Parameters.AddWithValue("@BranchID", byte.Parse(Session["BranchID"].ToString()));
                    SqlParameter outPutParameter = new SqlParameter();
                    outPutParameter.ParameterName = "@EventID";
                    outPutParameter.SqlDbType = SqlDbType.Int;
                    outPutParameter.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(outPutParameter);

                    
                    cmd.ExecuteNonQuery();


                    //string EventID = outPutParameter.Value.ToString();

                    //if (EventID != string.Empty && EventID != "" && EventID != null)
                    //{
                    //    rec = int.Parse(EventID);
                    //}
                   


                }

                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }

             }

            if (ResultFlag == true)
            {

                Errormsg += "Add SuccessFully";
            }
               List<NewsEventMaster> objlistNew = new List<NewsEventMaster>();
               objlistNew = unitOfWork.NewsEventMasterService.GetNewsEventMasterList(byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()));

              NewsEventMaster objnews = new NewsEventMaster();
                return new JsonResult()
                {
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                    Data = new
                    {
                        ResultFlag = false,
                        ResultMsg = Errormsg,
                        EventData = RenderRazorViewToString("NewsEventEditPartial", objnews, ControllerContext, ViewData, TempData),
                        ListData = RenderRazorViewToString("ListNewsEventMaster", objlistNew, ControllerContext, ViewData, TempData)
                    }
                };
           
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

        public ActionResult UploadAchievementImage(modelAchievementPhotoUploadInfo obj)
        {
            if (Session["UserID"] == null) { return Redirect("~/"); }
            UploadControlDemoAchievement objup = new UploadControlDemoAchievement();

            objup.mEventID = obj.AchievementID;

            //UploadControlExtension.GetUploadedFiles("uc", UploadControlDemoAchievement.ValidationSettings, UploadControlDemoAchievement.uc_FileUploadComplete);

            return null;
        }

       


        //public ActionResult ClassGridRowChange(int RegID)
        //{
        //    return PartialView("ClassTabs", RegID);
        //}

        

    }


        public class UploadControlDemoAchievement
        {

            public static int _EventID;

            public int mEventID
            {
                set { _EventID = value; }
                get { return _EventID; }
            }

            public const string UploadDirectory = "~/Images/Achievement/";

            //public static readonly DevExpress.Web.UploadControlValidationSettings UploadControlValidationSettings = new DevExpress.Web.UploadControlValidationSettings
            //{
            //   // AllowedFileExtensions = new string[] { ".jpg", ".jpeg", ".jpe",".png", },
            //    //MaxFileSize = 20971520,
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

                    appSchool.Repositories.Achievement img = new appSchool.Repositories.Achievement();
                    img.AchievementID = _EventID;
                    img.ImageName = name;


                    UnitOfWork obj = new UnitOfWork();

                    obj.achievementService.UpdateAchievementPhoto(img);
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
