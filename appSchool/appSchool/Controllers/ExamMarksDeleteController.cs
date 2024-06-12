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


namespace appSchool.Controllers
{
        [NoCache]
    public class ExamMarksDeleteController : Controller
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        private SqlConnection _mConn;
        private SqlTransaction _mTran;

          
        public ActionResult Index()
        {
            if (Session["UserID"] == null || (int)SubMenuModules.appClasses == 0)
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


            string ispoup = "0";
            if (Request.Params["ispoup"] != null)
            {
                ispoup = Request.Params["ispoup"];

                //string mstudid = Request.Params["mstudid"];
                ViewData["ispoup"] = ispoup;

            }
            else
                ViewData["ispoup"] = null;


            return View();
        }
        public ActionResult ListPartialStudentList()
        {

            return PartialView("ListStudentGridLookupPartial", new UnitOfWork().studentSessionService.GetAllStudentForSessionNameWise(int.Parse(Session["SessionID"].ToString()), byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));
        }

        public ActionResult PartialGridClasses()
        {
            return PartialView("GridViewPartial", unitOfWork.ClassService.GetClassList(byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));
        }



    }
}
