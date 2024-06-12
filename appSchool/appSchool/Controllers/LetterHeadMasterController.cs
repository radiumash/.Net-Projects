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
using DevExpress.Web.ASPxHtmlEditor;

namespace appSchool.Controllers
{
    public class LetterHeadMasterController : Controller
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        private SqlConnection _mConn;
        private SqlTransaction _mTran;


        public ActionResult Index()
        {
            if (Session["UserID"] == null || (int)SubMenuModules.appLetterHeadMaster == 0)
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
        public ActionResult FeaturesPartial()
        {
            return PartialView("HtmlEditor2");
        }
        public ActionResult HtmlImageUpload()
        {
            return PartialView("HtmlEditor2");
        }


        public ActionResult PartialGridLetterHeadMaster()
        {
            if (Session["UserID"] == null) { return Redirect("~/"); }
            //ViewBag.ShowFilter = mShowFilter;
            return PartialView("ListLetterHeadMaster", unitOfWork.letterheadmasterService.GetLetterHedMasterList(byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));
        }

        public ActionResult GridViewCustomActionPartial(string customAction)
        {
            if (customAction == "delete")
            {
                //SafeExecute(() => PerformDelete());
                //string vartem = 1;
            }
            return PartialGridLetterHeadMaster();
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult AddNewLetterHeadMaster(LetterHeadMaster objNews)
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
                    //objNews.PublishDate = DateTime.Now;
                    //objNews.AddDate = DateTime.Now;
                    //unitOfWork.letterheadmasterService.AddNewLetterHeadMaster(objNews);
                    //unitOfWork.Save();
                    AddNewLetterHead(objNews);
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please Fill all Field, & correct all errors.";
            ViewData["EditableClass"] = objNews;
            return PartialView("ListLetterHeadMaster", unitOfWork.letterheadmasterService.GetLetterHedMasterList(byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult UpdateLetterHeadMaster(LetterHeadMaster objNews)
        {
            _mConn = DB.GetActiveConnection();
            _mTran = _mConn.BeginTransaction(IsolationLevel.Snapshot);

            if (ModelState.IsValid)
            {
                try
                {

                    objNews.CompID = byte.Parse(Session["CompID"].ToString());
                    objNews.BranchID = byte.Parse(Session["BranchID"].ToString());
                    //objNews.UIDMod = byte.Parse(Session["UserID"].ToString());
                   
                    //objNews.ModDate = DateTime.Now;


                    if (SettingMasterStaticClass._ManageHistory == true)
                    {
                        SaveUserLogForUpdate(objNews);
                    }
                    //unitOfWork.letterheadmasterService.UpdateLetterHeadMaster(objNews);
                    //unitOfWork.Save();
                    UpdateLetterHead(objNews);

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
            return PartialView("ListLetterHeadMaster", unitOfWork.letterheadmasterService.GetLetterHedMasterList(byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));
        }


        [HttpPost, ValidateInput(false)]
        public ActionResult DeleteLetterHeadMaster(LetterHeadMaster objNews)
        {
            //if (Session["UserID"] == null) { return Redirect("~/"); }
            _mConn = DB.GetActiveConnection();
            _mTran = _mConn.BeginTransaction(IsolationLevel.Snapshot);

            try
            {
                int RowsCount = unitOfWork.letterheadmasterService.CheckThoughtDelete(objNews.LetterHeadID);
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
                    unitOfWork.letterheadmasterService.DeleteLetterHeadMaster(objNews);
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
            return PartialView("ListLetterHeadMaster", unitOfWork.letterheadmasterService.GetLetterHedMasterList(byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));
        }

        [HttpPost]
        public ActionResult SaveLetterHeadDescription(string mLetterHeadDesc, string mLetterheadId)
        {
            ViewBag.PostedHtml = HtmlEditorExtension.GetHtml("LetterHeadDesc");
           string value =  HtmlEditorExtension.GetHtml("LetterHeadDesc");
            LetterHeadMaster obj = new LetterHeadMaster();
            obj.CompID = byte.Parse(Session["CompID"].ToString());
            obj.BranchID = byte.Parse(Session["BranchID"].ToString());
            obj.LetterHeadDesc = value;
            obj.LetterHeadID = int.Parse(mLetterheadId.ToString());
            //objNews.UIDMod = byte.Parse(Session["UserID"].ToString());

            //objNews.ModDate = DateTime.Now;


            //if (SettingMasterStaticClass._ManageHistory == true)
            //{
            //    SaveUserLogForUpdate(obj);
            //}
            unitOfWork.letterheadmasterService.UpdateLetterHeadDescCription(obj);
            unitOfWork.Save();
            return PartialView("HtmlEditor2");
        }

        public void SaveUserLogForUpdate(LetterHeadMaster obj)
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

        public void SaveUserLogForDelete(LetterHeadMaster obj)
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


        private void UpdateLetterHead(LetterHeadMaster obj)
        {
            SqlConnection con = DB.GetActiveConnection();
            
            SqlCommand cmd = new SqlCommand("Update_LetterHeadMaster", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@LetterHeadID", SqlDbType.VarChar).Value = obj.LetterHeadID;
            cmd.Parameters.Add("@LetterHeadName", SqlDbType.VarChar).Value = obj.LetterHeadName;
            cmd.Parameters.Add("@LetterHeadDesc", SqlDbType.VarChar).Value = obj.LetterHeadDesc;
            cmd.Parameters.Add("@IsActive", SqlDbType.VarChar).Value = obj.IsActive;
            cmd.Parameters.Add("@CompID", SqlDbType.VarChar).Value = Session["CompID"];
            cmd.Parameters.Add("@BranchID", SqlDbType.VarChar).Value = Session["BranchID"];

            int i = cmd.ExecuteNonQuery();

            if (cmd.Connection.State == ConnectionState.Open)
            cmd.Connection.Close();
        }

        private void AddNewLetterHead(LetterHeadMaster obj)
        {
            try
            {
                SqlConnection con = DB.GetActiveConnection();
                int rec = 0;
                SqlCommand cmd = new SqlCommand("Add_LetterHeadMaster", con);
                cmd.CommandType = CommandType.StoredProcedure;
                

                cmd.Parameters.AddWithValue("@LetterHeadName", obj.LetterHeadName);
                cmd.Parameters.AddWithValue("@LetterHeadDesc", obj.LetterHeadDesc);
                cmd.Parameters.AddWithValue("IsActive", obj.IsActive);
                cmd.Parameters.AddWithValue("@CompID", byte.Parse(Session["CompID"].ToString()));
                cmd.Parameters.AddWithValue("@BranchID", byte.Parse(Session["BranchID"].ToString()));
                SqlParameter outPutParameter = new SqlParameter();
                outPutParameter.ParameterName = "@LetterHeadID";
                outPutParameter.SqlDbType = SqlDbType.Int;
                outPutParameter.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(outPutParameter);

                cmd.ExecuteNonQuery();
               
            }

            catch (Exception e)
            {
                ViewData["EditError"] = e.Message;
            }
        }
       
        
    }
}
