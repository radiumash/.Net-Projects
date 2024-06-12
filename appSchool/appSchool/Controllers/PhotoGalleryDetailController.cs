using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using appSchool.Repositories;
using appSchool.Models;
using appSchool.ViewModels;
using System.IO;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using DevExpress.Web.Mvc;

namespace appSchool.Controllers
{
    public class PhotoGalleryDetailController : Controller
    {
        //
        // GET: /PhotoGallaryDetail/
        private UnitOfWork unitOfWork = new UnitOfWork();
        private SqlConnection _mConn;
        private SqlTransaction _mTran;

        public ActionResult Index()
        {
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

           
           

            return View();

           
        }


        public ActionResult PartialPhotoGalleryDetail(int mGalleryID, string mFolderName)
        {
            if (Session["UserID"] == null) { return Redirect("~/"); }

            ViewData["pGalleryID"] = mGalleryID;
            ViewData["pFolderName"] = mFolderName;
            return PartialView("ListPhotoGalleryDetail", unitOfWork.photogallerdetailService.GetPhotoGalleryDetailList(mGalleryID));
          
        }


        public JsonResult GetphotoDetail(int pGalleryID)
        {
            string ErrorMsg = string.Empty;

            bool Result = false;
            string EvenetName = string.Empty; string EveneDesc = string.Empty; string EventDate = ""; string FromDate = ""; string ToDate = "";

            DateTime mEventDate = DateTime.Now; DateTime mFromDate = DateTime.Now; DateTime mToDate = DateTime.Now;
            try
            {

                PhotoGalleryMaster objphoto = new PhotoGalleryMaster();

                objphoto = unitOfWork.PhotoGalleryMasterService.GetGallerydetail(pGalleryID);

                if (objphoto.EventName != null)
                    EvenetName = objphoto.EventName;

                if (objphoto.EventDate != null)
                {
                    mEventDate = DateTime.Parse(objphoto.EventDate.ToString());
                    EventDate = mEventDate.ToString("dd MMM yyyy");
                }

                if (objphoto.Fromdate != null)
                {
                    mFromDate = DateTime.Parse(objphoto.Fromdate.ToString());
                    FromDate = mFromDate.ToString("dd MMM yyyy");
                }
                if (objphoto.Todate != null)
                {
                    mToDate = DateTime.Parse(objphoto.Todate.ToString());
                    ToDate = mToDate.ToString("dd MMM yyyy");
                }
                if (objphoto.Remark != null)
                {
                    EveneDesc = objphoto.Remark;
                } 
           }
            catch (Exception ex)
            {
                ErrorMsg = ex.Message;
            }



            return Json(new { Result, ErrorMsg, EvenetName, EventDate, FromDate, ToDate, EveneDesc }, JsonRequestBehavior.AllowGet);

        }


        public JsonResult UpdatePhotoDetail(int pGalleryID,string pEvent,string pEventDesc,DateTime pFromDate,DateTime pToDate,DateTime pEventdate)
        {
            string ErrorMsg = string.Empty;
            
            bool Result = false;
            //_mConn = DB.GetActiveConnection();
            //_mTran = _mConn.BeginTransaction(IsolationLevel.Snapshot);

            string MainFolderName = string.Empty;
            MainFolderName = unitOfWork.PhotoGalleryMasterService.GetByID(pGalleryID).FolderName;


            Result = UpadtePhotoGalleryMaster(pGalleryID, pEvent, pEventDesc, pFromDate, pToDate, pEventdate);
            
            if (Result)
            {
                                string FolderPath = "~/Gallery";
                                int i = 1;
                                bool mResult = false;
                                _mConn = DB.GetActiveConnection();
                                _mTran = _mConn.BeginTransaction();


                                string FolderName = string.Empty;
                                FolderName = unitOfWork.PhotoGalleryMasterService.GetByID(pGalleryID).FolderName;
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
                                        string sqldelete = "delete  From PhotoGalleryDetail Where GalleryID=" + pGalleryID;
                                        SqlCommand cmddelete = new SqlCommand(sqldelete, _mConn);
                                        cmddelete.Transaction = _mTran;
                                        cmddelete.CommandType = CommandType.Text;
                                        int recdelete = cmddelete.ExecuteNonQuery();

                                        StringBuilder mQuery = new StringBuilder();
                                        mQuery.Append(" Insert INTO PhotoGalleryDetail (GalleryID, PhotoName,PhotoOrder) ");
                                        foreach (string s in FileList)
                                        {
                                            string Newfile = Path.GetFileName(s);
                                            mQuery.Append("SELECT " + pGalleryID + ",'" + Newfile + "',"+ i +"  ");
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

                                        mResult = (recs > 0) ? true : false;
                                        if (mResult)
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


                                

                                ErrorMsg = "successfully inserted";
            }
            else
            {
                //_mTran.Rollback();
                ErrorMsg = "error messg";
            }


            List<PhotoGalleryDetail> lst = unitOfWork.photogallerdetailService.GetPhotoGalleryDetailList(pGalleryID);

            ViewData["pGalleryID"] = pGalleryID;
            ViewData["pFolderName"] = MainFolderName;

            return Json(new { Result, ErrorMsg, ListData = cCommon.RenderRazorViewToString("ListPhotoGalleryDetail", lst, ControllerContext, ViewData, TempData) }, JsonRequestBehavior.AllowGet);

        }


        public JsonResult DeleteFolderDetail(int pGalleryID)
        {
            string ErrorMsg = string.Empty;
            
            bool Result = false;
            bool res = false;
            int rec = 0;

            try 
            {
            byte mCompID = byte.Parse(Session["CompID"].ToString());
            byte mBranchID=byte.Parse(Session["BranchId"].ToString());
            //SqlCommand cmd = new SqlCommand("Update_PhotoGalleryDetail", _mConn);
            SqlCommand cmd = DB.CreateCommand("[Delete_PhotoGalleryDetail]");
            //cmd.CommandType = CommandType.StoredProcedure;
            //cmd.Transaction = _mTran;
            cmd.Parameters.Add("@GalleryID", SqlDbType.Int).Value = pGalleryID;
            cmd.Parameters.Add("@BranchID", SqlDbType.VarChar).Value = mBranchID;
            cmd.Parameters.Add("@CompID", SqlDbType.VarChar).Value = mCompID;
            
            rec = DB.ExecuteNoResult(cmd);
            }
            catch(Exception ex)
            {
                ErrorMsg = ex.Message;
            }

            if (rec > 0)
            {
                res = true;
                ErrorMsg = "Delete sucessfully";
            }
            else
            {
                ErrorMsg = "Data not found";
            }


            return Json(new { ErrorMsg, Result });
        }

        public bool UpadtePhotoGalleryMaster(int pGalleryID, string pEvent, string pEventDesc, DateTime pFromDate, DateTime pToDate, DateTime pEventdate)
        {
            bool res = false;
            int rec = 0;
            //SqlCommand cmd = new SqlCommand("Update_PhotoGalleryDetail", _mConn);
            SqlCommand cmd = DB.CreateCommand("[Update_PhotoGalleryDetail]");
            //cmd.CommandType = CommandType.StoredProcedure;
            //cmd.Transaction = _mTran;
            cmd.Parameters.Add("@GalleryID", SqlDbType.Int).Value = pGalleryID;
            cmd.Parameters.Add("@EventName", SqlDbType.VarChar).Value = pEvent;
            cmd.Parameters.Add("@EventDesc", SqlDbType.VarChar).Value = pEventDesc;
            cmd.Parameters.Add("@FromDate", SqlDbType.SmallDateTime).Value = pFromDate;
            cmd.Parameters.Add("@ToDate", SqlDbType.SmallDateTime).Value = pToDate;
            cmd.Parameters.Add("@EventDate", SqlDbType.SmallDateTime).Value = pEventdate;


            rec = DB.ExecuteNoResult(cmd);

            if (rec > 0)
            {
                res = true;
            }

            return res;

        }


        public ActionResult UpdatePartialPhotoOrder(MVCxGridViewBatchUpdateValues<PhotoGalleryDetail, int> updateValues, int mGalleryID, string mFolderName)
        {
            if (Session["UserID"] == null) { return Redirect("~/"); }

            try
            {
                foreach (var product in updateValues.Update)
                {
                    if (updateValues.IsValid(product))
                        UpdateStructure(product, updateValues);
                }
            }
            catch(Exception ex)
            {

            }




            ViewData["pGalleryID"] = mGalleryID;
            ViewData["pFolderName"] = mFolderName;
            return PartialView("ListPhotoGalleryDetail", unitOfWork.photogallerdetailService.GetPhotoGalleryDetailList(mGalleryID));
        }


        protected void UpdateStructure(PhotoGalleryDetail product, MVCxGridViewBatchUpdateValues<PhotoGalleryDetail, int> updateValues)
        {
            try
            {
                unitOfWork.photogallerdetailService.UpdatephotoOrder(product, byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()));
                unitOfWork.Save();
            }
            catch (Exception e)
            {
                updateValues.SetErrorText(product, e.Message);
            }
        }


    }
}
