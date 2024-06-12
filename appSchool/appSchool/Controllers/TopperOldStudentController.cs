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
    public class TopperOldStudentController : Controller
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



            return View();
        }

        public ActionResult PartialGriTopperOldStudent()
        {
             if (Session["UserID"] == null) { return Redirect("~/"); }
             //ViewBag.ShowFilter = mShowFilter;
             return PartialView("ListtopperOldStudentMaster", unitOfWork.TopperNoticeBoardService.GetTopperStudentList(byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));
        }


        public ActionResult GridViewCustomActionPartial(string customAction, int pExamSyllabusID, int pClassID, int pSubjectID, int pExamID)
        {
            if (customAction == "delete")
            {
                //SafeExecute(() => PerformDelete());
                //string vartem = 1; 
            }
            return PartialGriTopperOldStudent();
        }

        [HttpPost, ValidateInput(false)]

        public ActionResult EventGridRowChange(int RegID)
        {
            if (Session["UserID"] == null) { return Redirect("~/"); }

            return PartialView("TopperOldStudentTabs", RegID);
        }

        public ActionResult RefreshNewsEventGrid(bool mShowFilter)
        {
            if (Session["UserID"] == null) { return Redirect("~/"); }
            ViewBag.ShowFilter = mShowFilter;
            return PartialView("ListtopperOldStudentMaster", unitOfWork.TopperNoticeBoardService.GetTopperStudentList(byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));
        }

        public string GenerateUniqID()
        {
            string password = string.Empty;

            var chars = "0123456789";
            var random = new Random();
            var result = new string(
                Enumerable.Repeat(chars, 6)
                          .Select(s => s[random.Next(s.Length)])
                          .ToArray());

            password = result;
            return password;

        }
     
        [HttpPost, ValidateInput(false)]
        public ActionResult AddNewTopperOldStudent(TopperNoticeBoard objNews)
        {
            _mConn = DB.GetActiveConnection();
            _mTran = _mConn.BeginTransaction(IsolationLevel.Snapshot);
            if (ModelState.IsValid)
            {
                try
                {

                    string Id = GenerateUniqID();

                    int mStudentID = int.Parse(Id.ToString());

                    objNews.StudentID = mStudentID;
                    objNews.CompID = byte.Parse(Session["CompID"].ToString());
                    objNews.BranchID = byte.Parse(Session["BranchID"].ToString());
                    objNews.UIDAdd = byte.Parse(Session["UserID"].ToString());
                   
                    objNews.AddDate = DateTime.Now;
                    unitOfWork.TopperNoticeBoardService.AddNewTopperNoticeBoard(objNews);
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
            return PartialView("ListtopperOldStudentMaster", unitOfWork.TopperNoticeBoardService.GetTopperStudentList(byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult UpdateTopperOldStudent(TopperNoticeBoard objNews)
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
                   
                    objNews.ModDate = DateTime.Now;
                    

                    if (SettingMasterStaticClass._ManageHistory == true)
                    {
                        //SaveUserLogForUpdate(objNews);
                    }
                    unitOfWork.TopperNoticeBoardService.UpdateTopperNoticeBoard(objNews);
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
            return PartialView("ListtopperOldStudentMaster", unitOfWork.TopperNoticeBoardService.GetTopperStudentList(byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));
        }
 
        [HttpPost, ValidateInput(false)]
        public ActionResult DeleteTopperOldStudent(TopperNoticeBoard objNews)
        {
           
            _mConn = DB.GetActiveConnection();
            _mTran = _mConn.BeginTransaction(IsolationLevel.Snapshot);

            try
            {
                int RowsCount = unitOfWork.ClassService.CheckClassDelete(objNews.TNoticeID);
                //if (RowsCount == 0)
                //{
                //    if (SettingMasterStaticClass._ManageHistory == true)
                //    {
                //       // SaveUserLogForDelete(objNews);
                //        _mTran.Commit();
                //    }
                //}
                #region Deletel Old Method
                unitOfWork.TopperNoticeBoardService.DeleteTopperNoticeBoard(objNews);
                unitOfWork.Save();
                //if (RowsCount == 0)
                //{
                //    unitOfWork.TopperNoticeBoardService.DeleteTopperNoticeBoard(objNews);
                //    unitOfWork.Save();
                //}
                //else
                //{
                //}
                #endregion

            }
            catch (Exception e)
            {
                ViewData["EditError"] = e.Message;
            }
            return PartialView("ListtopperOldStudentMaster", unitOfWork.TopperNoticeBoardService.GetTopperStudentList(byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));
        }

        public ActionResult UpdateTopperOldStudentInfo(modelTopperNoticeinfo obj)
        {
            if (Session["UserID"] == null) { return Redirect("~/"); }
            if (ModelState.IsValid)
            {
                try
                {
                    //unitOfWork.NewsEventMasterService.UpdateNewsEventInfo(obj);
                    unitOfWork.Save();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("NewsEventEditPartial", obj);

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


        public ActionResult UploadTopperStudentImage(modelTopperNoticeinfo obj)
        {
            if (Session["UserID"] == null) { return Redirect("~/"); }
            UploadControlDemoHelperForTopper objup = new UploadControlDemoHelperForTopper();

            objup.TNoticeID = obj.TNoticeID;

           // UploadControlExtension.GetUploadedFiles("uc", UploadControlDemoHelperForTopper.ValidationSettings, UploadControlDemoHelperForTopper.uc_FileUploadComplete);

            return null;
        }

       


        //public ActionResult ClassGridRowChange(int RegID)
        //{
        //    return PartialView("ClassTabs", RegID);
        //}

        

    }


        public class UploadControlDemoHelperForTopper
        {

            public static int _TNoticeID;

            public int TNoticeID
            {
                set { _TNoticeID = value; }
                get { return _TNoticeID; }
            }

            public const string UploadDirectory = "~/Images/TopperStudent/";

            //public static readonly DevExpress.Web.UploadControlValidationSettings UploadControlValidationSettings = new DevExpress.Web.UploadControlValidationSettings
            //{
            //    AllowedFileExtensions = new string[] { ".jpg", ".jpeg", ".jpe", ".gif", ".bmp", ".png", },
            //    MaxFileSize = 20971520,
            //};

            public static void uc_FileUploadComplete(object sender, FileUploadCompleteEventArgs e)
            {
                if (e.UploadedFile.IsValid)
                {
                    string name = e.UploadedFile.FileName.Replace(e.UploadedFile.FileName, _TNoticeID.ToString() + ".png");
                    string FilePath = HttpContext.Current.Request.MapPath(UploadDirectory + name);

                    e.UploadedFile.SaveAs(FilePath, true);//Code Central Mode - Uncomment This Line

                    Stream strm = e.UploadedFile.PostedFile.InputStream;
                    var targetFile = FilePath;
                    //cCommon.GenerateThumbnails(0.5, strm, targetFile);

                    appSchool.Repositories.TopperNoticeBoard img = new appSchool.Repositories.TopperNoticeBoard();
                    img.TNoticeID = _TNoticeID;
                    img.AppImageName = name;


                    UnitOfWork obj = new UnitOfWork();

                    obj.TopperNoticeBoardService.UpdateTopperPhoto(img);
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
