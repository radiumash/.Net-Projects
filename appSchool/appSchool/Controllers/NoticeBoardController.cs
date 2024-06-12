using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using appSchool.Repositories;
using appSchool.Model;
using appSchool.ViewModels;
using System.IO;

namespace appSchool.Controllers
{
        [NoCache]
    public class NoticeBoardController : Controller
    {
         private UnitOfWork unitOfWork = new UnitOfWork();
        //
        // GET: /DesignationMaster/

        public ActionResult Index()
         {

             if (Session["UserID"] == null || (int)SubMenuModules.appNoticeBoard == 0)
            {
                return Redirect("~/");
            }

             //UserPermission objuser = new UserPermission();
             //objuser = unitOfWork.userPermissionService.CheckUserPermissionModulewise(int.Parse(Session["UserID"].ToString()), 124, byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()));
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
        public ActionResult ExternalEditFormPartial()
        {
            if (Session["UserID"] == null) { return Redirect("~/"); }
            return PartialView("ListNoticeBoard", unitOfWork.noticeBoardService.GetNoticeBoardList(byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));
        }

        public ActionResult GridViewCustomActionPartial(string customAction, int pExamSyllabusID, int pClassID, int pSubjectID, int pExamID)
        {
            if (customAction == "delete")
            {
                //SafeExecute(() => PerformDelete());
                //string vartem = 1; 
            }
            return ExternalEditFormPartial();
        }

        public ActionResult ExternalEditFormEdit(int mNoticeID)
        {
            if (Session["UserID"] == null) { return Redirect("~/"); }
            NoticeBoard editProduct = unitOfWork.noticeBoardService.GetByID(mNoticeID);
            if (editProduct == null)
            {
                editProduct = new NoticeBoard();
                editProduct.NoticeID = -1;
                editProduct.Notice = "";
                editProduct.NoticePerson = string.Empty;
            }
            return PartialView ( "TextEditingForm", editProduct);   
        }


        public ActionResult AddNoticBoardEdit(NoticeBoard objNotice)
        {

            if (Session["UserID"] == null) { return Redirect("~/"); }

            objNotice.CompID = byte.Parse(Session["CompID"].ToString());
            objNotice.BranchID = byte.Parse(Session["BranchID"].ToString());
            objNotice.UIDAdd = byte.Parse(Session["UserID"].ToString());
            unitOfWork.noticeBoardService.AddNewNotice(objNotice, byte.Parse(Session["UserID"].ToString()));
            unitOfWork.Save();
            
            return PartialView("ListNoticeBoard", unitOfWork.noticeBoardService.GetNoticeBoardList(byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));
        }


        public ActionResult UpdateNoticBoardEdit(NoticeBoard objNotice)
        {

            if (Session["UserID"] == null) { return Redirect("~/"); }

            objNotice.CompID = byte.Parse(Session["CompID"].ToString());
            objNotice.BranchID = byte.Parse(Session["BranchID"].ToString());
            objNotice.UIDAdd = byte.Parse(Session["UserID"].ToString());
            unitOfWork.noticeBoardService.UpdateNotice(objNotice, byte.Parse(Session["UserID"].ToString()));
            unitOfWork.Save();

            return PartialView("ListNoticeBoard", unitOfWork.noticeBoardService.GetNoticeBoardList(byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));
        }


        [HttpPost]
        public JsonResult ExternalEditFormEdit1(NoticeBoard objNotice)
        {
            objNotice.CompID = byte.Parse(Session["CompID"].ToString());
            objNotice.BranchID = byte.Parse(Session["BranchID"].ToString());
            string errorMsg = string.Empty;


            //if (objNotice.Notice == string.Empty || objNotice.ToDate == null || objNotice.FromDate == null)
            //{
            //    List<NoticeBoard> objList = unitOfWork.noticeBoardService.GetNoticeBoardList(byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()));
            //    return new JsonResult()
            //    {
            //        JsonRequestBehavior = JsonRequestBehavior.AllowGet,
            //        Data = new
            //        {

            //            result = "Some Field blank. Unable to Save Data.",
            //            ListData = RenderRazorViewToString("ExternalEditFormPartial", objList, ControllerContext, ViewData, TempData)

            //        }

            //    };
            //}

            if (ModelState.IsValid)
            {
                if (objNotice.NoticeID == -1)
                {

                    unitOfWork.noticeBoardService.AddNewNotice(objNotice, byte.Parse(Session["UserID"].ToString()));
                    unitOfWork.Save();
                    errorMsg = "Successfully Save.";
                }
                else
                {
                    unitOfWork.noticeBoardService.UpdateNotice(objNotice, byte.Parse(Session["UserID"].ToString()));
                    unitOfWork.Save();
                    errorMsg = "Successfully Update.";
                }

            }
            List<NoticeBoard> obj = unitOfWork.noticeBoardService.GetNoticeBoardList(byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()));

                    
                return new JsonResult()
                {
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                    Data = new {
                       
                        result =errorMsg,
                        ListData = RenderRazorViewToString("ExternalEditFormPartial", obj, ControllerContext, ViewData, TempData)
                       
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


        [HttpPost]
        public ActionResult Cancle()
        {
            if (Session["UserID"] == null) { return Redirect("~/"); }

            return PartialView("ExternalEditFormPartial", unitOfWork.noticeBoardService.GetNoticeBoardList(byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));
        
        }


       








       
    }
}



    