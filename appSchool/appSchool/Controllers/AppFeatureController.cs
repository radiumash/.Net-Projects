using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using appSchool.Repositories;
using System.Data.SqlClient;
using System.Data;
namespace appSchool.Controllers
{
    [NoCache]
    public class AppFeatureController : Controller
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

            //UserPermission objuser = new UserPermission();
            //objuser = unitOfWork.userPermissionService.CheckUserPermissionModulewise(int.Parse(Session["UserID"].ToString()), 31, byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()));
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

            modelSendSms objsmsmodel = unitOfWork.smsTemplateService.GetSendSMSModele();
            return View();
        }

        public static List<List<string>> Split(List<string> source)
        {
            return source
                .Select((x, i) => new { Index = i, Value = x })
                .GroupBy(x => x.Index / 100)
                .Select(x => x.Select(v => v.Value).ToList())
                .ToList();
        }


         public ActionResult PartialAppFeatureView()
        {
            return PartialView("GridViewPartial", new UnitOfWork().appFeatureservices.GetAppFeatureList(byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult AddNewFeature(AppFeature obj)
         {
            if (ModelState.IsValid)
            {
                try
                {
                    obj.CompID = byte.Parse(Session["CompID"].ToString());
                    obj.BranchID = byte.Parse(Session["BranchID"].ToString());
                    obj.UIDAdd = byte.Parse(Session["UserID"].ToString());
                    //obj.AddDate = DateTime.Now;

                    unitOfWork.appFeatureservices.AddNewAppModuledata(obj);
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
            return PartialView("GridViewPartial", new UnitOfWork().appFeatureservices.GetAppFeatureList(byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult UpdateAppFeature(AppFeature obj)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    //obj.UIDMod = byte.Parse(Session["UserID"].ToString());
                    //obj.ModDate = DateTime.Now;
                    unitOfWork.appFeatureservices.UpdateAppFeature(obj);
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
            return PartialView("GridViewPartial", new UnitOfWork().appFeatureservices.GetAppFeatureList(byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult DeleteAppModule(AppModule obj)
        {
            try
            {
                int RowsCount = unitOfWork.appModuleservices.CheckDelete(obj.Id);
                if (RowsCount == 0)
                {
                    unitOfWork.appModuleservices.DeleteAppModule(obj);
                    unitOfWork.Save();
                }
            }
            catch (Exception e)
            {
                ViewData["EditError"] = e.Message;
            }
            return PartialView("GridViewPartial", new UnitOfWork().appModuleservices.GetAppModuleList(byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));
        }
        public ActionResult AppFeatureGridRowChange(int RegID)
        {
            return PartialView("ClassTabs", RegID);
        }


        public ActionResult GridViewCustomActionPartial(string customAction)
        {
            if (customAction == "delete")
            {
                //SafeExecute(() => PerformDelete());
                //string vartem = 1;
            }
            return PartialAppFeatureView();
        }

    }



}



    