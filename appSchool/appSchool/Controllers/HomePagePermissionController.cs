using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using appSchool.Repositories;
using appSchool.ViewModels;
using System.Data.SqlClient;
using System.Data;
using Newtonsoft.Json;

namespace appSchool.Controllers
{
        //[NoCache]
    public class HomePagePermissionController : Controller
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        private SqlConnection _mConn;
        private SqlTransaction _mTran;



        public ActionResult Index()
        {
            if (Session["UserID"] == null)
            {
                return Redirect("~/");
            }

            return View();
        }

        public ActionResult PartialHomePagePermissionView()
        {
            if (Session["UserID"] == null) { return Redirect("~/"); }

            return PartialView("GridViewPartial", new UnitOfWork().homepagepermissionservice.GetHomePageRolePermissionListBYFeatureOrder(byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));
        }


        
        public ActionResult GetFeatureListByModuleID(int ModuleID)
        {
            if (Session["UserID"] == null) { return Redirect("~/"); }

            string ErrorMsg = string.Empty;
            int MaxFeatureOrder = 0;
            var DataFeatureList = string.Empty;

            try
            {
                if (ModuleID > 0)
                {

                    List<vUserRoleModulePermission> objfeature = new List<vUserRoleModulePermission>();
                    objfeature = unitOfWork.appFeatureservices.GetAllModuleAndFeatureListFromRolePermission(ModuleID, byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()));


                    if (objfeature.Count == 0)
                    {
                        ErrorMsg += " Section Not Found. ";

                    }
                    DataFeatureList = JsonConvert.SerializeObject(objfeature);

                   
                    MaxFeatureOrder = unitOfWork.homepagepermissionservice.MaxFeatureOrderNo(byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()));
                    MaxFeatureOrder++;
                }
                else
                {
                    ErrorMsg += " Please Select Class. ";
                    //msgFlag = true;
                }


              
            }
            catch(Exception ex)
            {

            }

            return new JsonResult
            {
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                Data = new
                {

                    DisplayMsg = ErrorMsg,
                    MaxFeatureOrderReturn = MaxFeatureOrder,
                    FeatureList = DataFeatureList,
                }
            };


        }

        public ActionResult GetFeatureListForISOnlyModulerequired()
        {
            if (Session["UserID"] == null) { return Redirect("~/"); }

            string ErrorMsg = string.Empty;
            
            var DataFeatureList = string.Empty;

            try
            {
                

                    List<vUserRoleModulePermission> listfeature = new List<vUserRoleModulePermission>();
                    AppFeature objfeature  = unitOfWork.appFeatureservices.GetFeatureListForNonSelection(byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()));
                    if (objfeature != null)
                    {
                       listfeature.Add(new vUserRoleModulePermission { Id = 1, FMenuText = objfeature.MenuText, FeatureId = objfeature.Id, CompID = objfeature.CompID, BranchID = objfeature.BranchID });
                    }

                DataFeatureList = JsonConvert.SerializeObject(listfeature);
            }
            catch (Exception ex)
            {

            }

            return new JsonResult
            {
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                Data = new
                {

                    DisplayMsg = ErrorMsg,
                    FeatureList = DataFeatureList,
                }
            };


        }


        public ActionResult AddNewHomePagePermissionByClientSide(int RoleID, int ModuleID, int FeatureID, int FeatureOrder, bool Ismodulerequired, bool Isvisible)
        {
            if (Session["UserID"] == null)
            {
                return Redirect("~/");
            }

            string Errormsg = string.Empty;

           

            bool chkexist  = unitOfWork.homepagepermissionservice.CheckISExist(ModuleID,  FeatureID, Ismodulerequired,  byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()));
            if(chkexist)
            {
                List<HomePageRolePermission> lstHomepermit = unitOfWork.homepagepermissionservice.GetHomePageRolePermissionListBYFeatureOrder(byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()));

                Errormsg = "This entry already exist";
                return Json(new
                {
                    DataResponseMsg = Errormsg,
                    ListDataHomePagePermission = cCommon.RenderRazorViewToString("GridViewPartial", lstHomepermit, ControllerContext, ViewData, TempData)
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                HomePageRolePermission objhomepermit = new HomePageRolePermission();
                objhomepermit.RoleID = RoleID;
                objhomepermit.ModuleID = ModuleID;
                objhomepermit.FeatureID = FeatureID;
                objhomepermit.FeatureOrder = FeatureOrder;
                objhomepermit.IsOnlyModuleRequire = Ismodulerequired;
                objhomepermit.CanView = Isvisible;
                objhomepermit.CompID = byte.Parse(Session["CompID"].ToString());
                objhomepermit.BranchID = byte.Parse(Session["BranchID"].ToString());
                objhomepermit.AddDate = DateTime.Now;

                unitOfWork.homepagepermissionservice.AddNewHomePageRolePermission(objhomepermit);
                unitOfWork.Save();

                List<HomePageRolePermission> lstHomepermit = unitOfWork.homepagepermissionservice.GetHomePageRolePermissionListBYFeatureOrder(byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()));
                Errormsg = "Permission inserted successfully";

                return Json(new
                {
                   
                    DataResponseMsg = Errormsg,
                    ListDataHomePagePermission = cCommon.RenderRazorViewToString("GridViewPartial", lstHomepermit, ControllerContext, ViewData, TempData)
                }, JsonRequestBehavior.AllowGet
                );

            }
           


        }



        [HttpPost, ValidateInput(false)]
        public ActionResult AddNewHomePagePermission(HomePageRolePermission obj)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    obj.CompID = byte.Parse(Session["CompID"].ToString());
                    obj.BranchID = byte.Parse(Session["BranchID"].ToString());

                    obj.AddDate = DateTime.Now;

                    unitOfWork.homepagepermissionservice.AddNewHomePageRolePermission(obj);
                    unitOfWork.Save();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            ViewData["EditableClass"] = obj;
            return PartialHomePagePermissionView();
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult UpdateHomePagePermission(HomePageRolePermission obj)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    obj.ModDate = DateTime.Now;
                    unitOfWork.homepagepermissionservice.UpdateHomePageRolePermission(obj);
                    unitOfWork.Save();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            ViewData["EditableClass"] = obj;
            return PartialHomePagePermissionView();
        }


        [HttpPost, ValidateInput(false)]
        public ActionResult DeleteHomePagePermission(HomePageRolePermission obj)
        {
            try
            {
                int RowsCount = unitOfWork.homepagepermissionservice.CheckDelete(obj.HomePageID);
                if (RowsCount > 0)
                {
                    unitOfWork.homepagepermissionservice.DeleteHomePageRolePermission(obj);
                    unitOfWork.Save();
                }
            }
            catch (Exception e)
            {
                ViewData["EditError"] = e.Message;
            }
            return PartialHomePagePermissionView();
        }




        [ValidateAntiForgeryToken]
        public ActionResult GridViewCustomActionPartial(string customAction)
        {
            if (customAction == "delete")
            { 
                //SafeExecute(() => PerformDelete());
                //string vartem = 1;
            }
            return PartialHomePagePermissionView();
        }




    }
}
