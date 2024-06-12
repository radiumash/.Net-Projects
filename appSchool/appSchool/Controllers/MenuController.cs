using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using appSchool.Model;
using appSchool.Repositories;
using appSchool.ViewModels;

namespace appSchool.Controllers
{
    public class MenuController : Controller
    {
        private UnitOfWork unitOfWork = new UnitOfWork();

        public ActionResult Index()
        {
            if (Session["UserID"] == null || (int)SubMenuModules.appClassSetup == 0)
            {
                return Redirect("~/");
            }
            List<vHomePageUserPermission> listmodulefeature = new List<vHomePageUserPermission>();


            try
            {
                listmodulefeature = unitOfWork.homepagepermissionservice.GetAllModuleAndFeatureListByRoleID(int.Parse(Session["UserRoleID"].ToString()), byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()));
            }
            catch(Exception ex)
            {

            }
             
            return View(listmodulefeature);
            //return View();
        }


        public ActionResult GetModuleListFiler(string filtertext)
        {
            if (Session["UserID"] == null) { return Redirect("~/"); }

            List<vHomePageUserPermission> listmodulefeature = new List<vHomePageUserPermission>();

            try
            {
                //List<vUserRoleModulePermission> listmodulefeature = unitOfWork.appFeatureservices.GetAllModuleAndFeatureListByRoleID(int.Parse(Session["UserRoleID"].ToString()), byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()));
               listmodulefeature = unitOfWork.homepagepermissionservice.GetAllModuleAndFeatureListByFiltertext(int.Parse(Session["UserRoleID"].ToString()), byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()), filtertext);

            }
            catch (Exception ex)
            {

            }
            return PartialView("Index", listmodulefeature);

        }

        public ActionResult GetModuleListFilerClear()
        {
            if (Session["UserID"] == null) { return Redirect("~/"); }

            List<vHomePageUserPermission> listmodulefeature = new List<vHomePageUserPermission>();

            try
            {
                listmodulefeature = unitOfWork.homepagepermissionservice.GetAllModuleAndFeatureListByRoleID(int.Parse(Session["UserRoleID"].ToString()), byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()));
            }
            catch(Exception ex)
            {

            }
            return PartialView("Index", listmodulefeature);

        }

        public ActionResult sessionview(int id)
        {
            if (Session["UserID"] == null) { return Redirect("~/"); }

            Session["SessionID"] = id;
            Session["SessionName"] = unitOfWork.academicYearService.GetByID(id).SessionName;

            List<vHomePageUserPermission> listmodulefeature = new List<vHomePageUserPermission>();

            try
            {
                 listmodulefeature = unitOfWork.homepagepermissionservice.GetAllModuleAndFeatureListByRoleID(int.Parse(Session["UserRoleID"].ToString()), byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()));
            }
            catch (Exception ex)
            {

            }
            return View("Index", listmodulefeature);
         }

        public ActionResult CardViewPartial()
        {
            if (Session["UserID"] == null) { return Redirect("~/"); }

            List<vHomePageUserPermission> listmodulefeature = unitOfWork.homepagepermissionservice.GetAllModuleAndFeatureList(byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()));
            return PartialView("CardViewPartial", listmodulefeature);
        }

        
    }
}