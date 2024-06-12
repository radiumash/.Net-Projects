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
using appSchool.Models;
using System.IO;
using DevExpress.Web.Mvc;
using System.Text;


namespace appSchool.Controllers
{
        [NoCache]
    public class PhotoGalleryController : Controller
    {
        public static readonly object RootFolder = "~/Gallery";
        private UnitOfWork unitOfWork = new UnitOfWork();
        private SqlConnection _mConn;
        private SqlTransaction _mTran;



        public ActionResult Index()
        {
          //  ArtsFileSystemProvider obj=new ArtsFileSystemProvider(RootFolder);
            if (Session["UserID"] == null || (int)SubMenuModules.appPhotoGallery == 0)
            {
                return Redirect("~/");
            }

            //MenuID of Classes=11
            //UserPermission objuser = new UserPermission();
            //objuser = unitOfWork.userPermissionService.CheckUserPermissionModulewise(int.Parse(Session["UserID"].ToString()), 120, byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()));
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



            return View("Index",RootFolder);
        }

         [ValidateInput(false)]
        //public ActionResult PhotoGalleryPartial()
        //{
        //    //   ArtsFileSystemProvider obj=new ArtsFileSystemProvider(RootFolder);
        //    return PartialView("PhotoGalleryPartial",RootFolder);
        //}
        // public FileStreamResult DownloadImages()
        // {
        //     return FileManagerExtension.DownloadFiles(PhotoGalleryControllerFileManagerSettings.DownloadSettings, FlyerVoiceMasterControllerFileManagerSettings.Model);
        // }




         public JsonResult CreateFolderView(string PFolderName, string PEventName, string PRemark)
         {
             string ErrorMsg=string.Empty;
             string FolderPath = "~/Gallery";
             bool Result = false;
             PFolderName = PFolderName.Replace(" ", "");
             _mConn = DB.GetActiveConnection();
             _mTran = _mConn.BeginTransaction();

             string PathName = System.Web.HttpContext.Current.Request.MapPath(FolderPath);
             if (Directory.Exists(PathName))
             {
                 if (!Directory.Exists(PathName + "/" + PFolderName))
                 {
                     Directory.CreateDirectory(PathName + "\\" + PFolderName);

                     PhotoGalleryMaster objPGM = new PhotoGalleryMaster();
                     objPGM.EventName =PEventName;
                     objPGM.FolderName = PFolderName;
                     objPGM.PathName = PathName + "\\" + PFolderName;
                     objPGM.Remark = PRemark;
                    
                     Result = AddPhotoGalleryMaster(objPGM);
                     if (Result)
                     {
                         _mTran.Commit();
                         ErrorMsg += "Folder Created Successfully.";
                     }
                     else
                     {
                         _mTran.Rollback();
                         Directory.Delete(PathName+"\\"+PFolderName);
                     }

                 }
                 else
                 {
                     ErrorMsg += "Folder Allready Exist";
                 }
             }
             else
             {
                 ErrorMsg +="Gallery Folder does not Exist.";
             }

             var FileMgrData = cCommon.RenderRazorViewToString("PhotoGalleryPartial",RootFolder,ControllerContext,ViewData,TempData);

             return Json( new {Result,ErrorMsg,FileMgrData},JsonRequestBehavior.AllowGet);

         }


         public bool AddPhotoGalleryMaster(PhotoGalleryMaster obj)
         {
             bool res = false;
             SqlCommand cmd =new SqlCommand("Add_PhotoGalleryMaster",_mConn);
             cmd.CommandType = CommandType.StoredProcedure;
             cmd.Transaction = _mTran;
             SqlParameter param = cmd.Parameters.Add("@GalleryID", SqlDbType.Int);
             param.Direction = ParameterDirection.InputOutput;
             param.Value = 0;
             
             cmd.Parameters.Add("@FolderName",SqlDbType.VarChar).Value=obj.FolderName;
             cmd.Parameters.Add("@PathName", SqlDbType.VarChar).Value = obj.PathName;
             cmd.Parameters.Add("@EventName", SqlDbType.VarChar).Value = obj.EventName;
             cmd.Parameters.Add("@Remark", SqlDbType.VarChar).Value = obj.Remark;
             cmd.ExecuteNonQuery();
             int GalleryID = int.Parse(param.Value.ToString());
             if (GalleryID > 0)
             {
                 res = true;
             }

             return res;
         }


         public JsonResult UpdatePhotoDetail(int mGalleryID)
         {
             string ErrorMsg=string.Empty;
             string FolderPath = "~/Gallery";
             int i = 1;
             bool Result = false;
             _mConn = DB.GetActiveConnection();
             _mTran = _mConn.BeginTransaction();


             string FolderName = string.Empty;
             FolderName = unitOfWork.PhotoGalleryMasterService.GetByID(mGalleryID).FolderName;
             if (FolderName == string.Empty || FolderName == null)
             {
                 ErrorMsg = "Folder not found in location.";
             }

             string NewFolderName = "~/Gallery/" + FolderName;
             //List<ImageList> objlst = new List<ImageList>();
             string PathName = System.Web.HttpContext.Current.Request.MapPath(NewFolderName);
             if (Directory.Exists(PathName))
             {
                 string[] FileList = Directory.GetFiles(PathName);

                 if (FileList.Length > 0)
                 {
                     string sqldelete = "delete  From PhotoGalleryDetail Where GalleryID=" + mGalleryID;
                     SqlCommand cmddelete = new SqlCommand(sqldelete, _mConn);
                     cmddelete.Transaction = _mTran;
                     cmddelete.CommandType = CommandType.Text;
                     int recdelete = cmddelete.ExecuteNonQuery();

                     StringBuilder mQuery = new StringBuilder();
                     mQuery.Append(" Insert INTO PhotoGalleryDetail (GalleryID, PhotoName) ");
                     foreach (string s in FileList)
                     {
                         string Newfile = Path.GetFileName(s);
                         mQuery.Append("SELECT " + mGalleryID + ",'" + Newfile + "'");
                         if (i < FileList.Count())
                         {
                             mQuery.Append(" Union All ");
                             i++;
                         }

                     }

                     SqlCommand cmdDetail = new SqlCommand("Execute_SQL", _mConn);
                     cmdDetail.Transaction = _mTran;
                     cmdDetail.CommandType = CommandType.StoredProcedure;
                     cmdDetail.Parameters.Add("@MQuery", SqlDbType.VarChar).Value = mQuery.ToString();
                     int recs = cmdDetail.ExecuteNonQuery();

                     Result = (recs > 0) ? true : false;
                     if (Result)
                     {
                         _mTran.Commit();
                     }
                     else
                     {
                         _mTran.Rollback();
                     }

                     ErrorMsg = "Data inserted successfully";
                 }
                 else
                 {
                     ErrorMsg = "Images Not Found";
                 }
                               

             }
             else
             {
                 ErrorMsg = "Path not Exists.";
             }

             return Json( new {Result,ErrorMsg},JsonRequestBehavior.AllowGet);

         }




        [ValidateInput(false)]
        public ActionResult PhotoGalleryPartial()
        {
            return PartialView("PhotoGalleryPartial", PhotoGalleryControllerfileManagerGallerySettings.Model);
        }

        public FileStreamResult PhotoGalleryPartialDownload()
        {
            return FileManagerExtension.DownloadFiles(PhotoGalleryControllerfileManagerGallerySettings.DownloadSettings, PhotoGalleryControllerfileManagerGallerySettings.Model);
        }
    }
    public class PhotoGalleryControllerfileManagerGallerySettings
    {
        public const string RootFolder = @"~\Gallery";

        public static string Model { get { return RootFolder; } }
        public static DevExpress.Web.Mvc.FileManagerSettings DownloadSettings
        {
            get
            {
                var settings = new DevExpress.Web.Mvc.FileManagerSettings { Name = "fileManagerGallery" };
                settings.SettingsEditing.AllowDownload = true;
                return settings;
            }
        }
    }







    //public class FileManagerDemoHelper
    //    {
    //        static ArtsFileSystemProvider artsFileSystemProvider;

    //        public static readonly object RootFolder = "~/Gallery";
    //        public static readonly object ImagesRootFolder = "~/Gallery/Chiritsmus";
    //        public static readonly string[] AllowedFileExtensions = new string[] {
    //        ".jpg", ".jpeg", ".gif", ".rtf", ".txt", ".avi", ".png", ".mp3", ".xml", ".doc", ".pdf"
    //    };
    //        public static FileManagerFeaturesDemoOptions FeaturesDemoOptions
    //        {
    //            get
    //            {
    //                if (HttpContext.Current.Session["FeaturesDemoOptions"] == null)
    //                    HttpContext.Current.Session["FeaturesDemoOptions"] = new FileManagerFeaturesDemoOptions();
    //                return (FileManagerFeaturesDemoOptions)HttpContext.Current.Session["FeaturesDemoOptions"];
    //            }
    //            set { HttpContext.Current.Session["FeaturesDemoOptions"] = value; }
    //        }
    //        public static ArtsFileSystemProvider ArtsFileSystemProvider
    //        {
    //            get
    //            {
    //                if (artsFileSystemProvider == null)
    //                    artsFileSystemProvider = new ArtsFileSystemProvider(string.Empty);
    //                return artsFileSystemProvider;
    //            }
    //        }

    //        //public static void ValidateSiteEdit(FileManagerActionEventArgsBase e)
    //        //{
    //        //    e.Cancel = Utils.IsSiteMode;
    //        // //   e.ErrorText = Utils.GetReadOnlyMessageText();
    //        //}
    //        public static List<SelectListItem> GetSecurityRoles()
    //        {
    //            return new List<SelectListItem>() {
    //            new SelectListItem() { Text = "Default User", Value = SecurityRole.DefaultUser.ToString(), Selected = true },
    //            new SelectListItem() { Text = "Document Manager", Value = SecurityRole.DocumentManager.ToString() },
    //            new SelectListItem() { Text = "Media Moderator", Value = SecurityRole.MediaModerator.ToString() },
    //            new SelectListItem() { Text = "Administrator", Value = SecurityRole.Administrator.ToString() }
    //        };
    //        }
    //        public static List<SelectListItem> GetFileListViewModes()
    //        {
    //            return new List<SelectListItem>() {
    //            //new SelectListItem() { Text = FileListView.Thumbnails.ToString(), Value = FileListView.Thumbnails.ToString(), Selected = true },
    //            //new SelectListItem() { Text = FileListView.Details.ToString(), Value = FileListView.Details.ToString() }
    //        };
    //        }
    //    }

    //public class FileManagerFeaturesDemoOptions
    //{
    //    FileManagerSettingsEditing settingsEditing;
    //    FileManagerSettingsToolbar settingsToolbar;
    //    FileManagerSettingsFolders settingsFolders;
    //    MVCxFileManagerSettingsUpload settingsUpload;

    //    public FileManagerFeaturesDemoOptions()
    //    {
    //        this.settingsEditing = new FileManagerSettingsEditing(null)
    //        {
    //            AllowCreate = true,
    //            AllowMove = true,
    //            AllowDelete = true,
    //            AllowRename = true,
    //            AllowCopy = true
    //        };
    //        this.settingsToolbar = new FileManagerSettingsToolbar(null)
    //        {
    //            ShowPath = true,
    //            ShowRefreshButton = true,
    //            ShowFilterBox = true,
    //            ShowDownloadButton = false
    //        };
    //        this.settingsFolders = new FileManagerSettingsFolders(null)
    //        {
    //            Visible = true,
    //            EnableCallBacks = false,
    //            ShowFolderIcons = true,
    //            ShowLockedFolderIcons = true
    //        };
    //        this.settingsUpload = new MVCxFileManagerSettingsUpload();
    //        this.settingsUpload.Enabled = true;
    //        this.settingsUpload.AdvancedModeSettings.EnableMultiSelect = true;
    //    }

    //    public FileManagerSettingsEditing SettingsEditing { get { return settingsEditing; } }
    //    public FileManagerSettingsToolbar SettingsToolbar { get { return settingsToolbar; } }
    //    public FileManagerSettingsFolders SettingsFolders { get { return settingsFolders; } }
    //    public MVCxFileManagerSettingsUpload SettingsUpload { get { return settingsUpload; } }
    //}

    public class FileManagerDetailsViewModeDemoOptions
        {
            public FileManagerDetailsViewModeDemoOptions()
            {
                AllowColumnResize = true;
                AllowColumnDragDrop = true;
                AllowColumnSort = true;
                ShowHeaderFilterButton = false;
            }

            public bool AllowColumnResize { get; set; }
            public bool AllowColumnDragDrop { get; set; }
            public bool AllowColumnSort { get; set; }
            public bool ShowHeaderFilterButton { get; set; }
        }

        public enum SecurityRole { DefaultUser, DocumentManager, MediaModerator, Administrator }
}
