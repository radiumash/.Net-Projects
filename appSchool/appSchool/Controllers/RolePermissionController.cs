using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using appSchool.Repositories;
using DevExpress.Web.Mvc;
using DevExpress.Web;
using System.Web.SessionState;
using appSchool.ViewModels;

namespace appSchool.Controllers
{
    [NoCache]
    public class RolePermissionController : Controller
    {
         private UnitOfWork unitOfWork = new UnitOfWork();

         public ActionResult Index()
        {
            if (Session["UserID"] == null || (int)SubMenuModules.appUserPermission == 0)
            {
                return Redirect("~/");
            }

            //UserPermission objuser = new UserPermission();
            //objuser = unitOfWork.userPermissionService.CheckUserPermissionModulewise(int.Parse(Session["UserID"].ToString()), 73, byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()));
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
            //return PartialView("UserPermissionView");
            return View();
        }

        public ActionResult PartialRolePermission(string PRoleId)
        {
            if (Session["UserID"] == null) { return Redirect("~/"); }
            int RoleId = int.Parse(PRoleId);
            ViewData["RoleIdForPermission"] = PRoleId;

            return PartialView("ListUserPermissionView", new UnitOfWork().rolePermissionservices.GetRolePermissionByRoleIDAndModuleID(RoleId));
        }
        public ActionResult GetFeatureListByFModuleIDView(int mModuleID)
        {
            if (Session["UserID"] == null) { return Redirect("~/"); }
            List<AppFeature> obj = new List<AppFeature>();
            obj = unitOfWork.appFeatureservices.GetListByModuleId(mModuleID);

            return PartialView("FromFeaturePartial", obj);
        }

        public ActionResult GetAllRolePermission(int PFeatureID,int PModuleID,int PRoleID)
        {
            if (Session["UserID"] == null) { return Redirect("~/"); }
            //int UserID = int.Parse(mUserID);
            ViewData["RoleIdForPermission"] = PRoleID;
            ViewData["ModuleIDForPermission"] = PModuleID;
            ViewData["FeatureIDForPermission"] = PFeatureID;

            return PartialView("ListUserPermissionView", new UnitOfWork().rolePermissionservices.GetRolePermissionByRoleIDAndModuleID(PFeatureID));
        }

        public ActionResult UpdateRolePermission(int pRoleIDs, int PModuleId, int PFeatureId)
        {

            string responseMsg = string.Empty;

            try
            {

                RolePermission objPermissionList = new RolePermission();
                objPermissionList = unitOfWork.rolePermissionservices.GetRolePermissionByRoleANDModelANDFeatureID(pRoleIDs,PModuleId,PFeatureId);
                if (objPermissionList==null)
                {

                    RolePermission objuserPermission = new RolePermission();
                    objuserPermission.FeatureId = PFeatureId;
                    objuserPermission.RoleId = pRoleIDs;
                    objuserPermission.ModuleId = PModuleId;
                    objuserPermission.CanAdd = false;
                    objuserPermission.CanEdit = false;
                    objuserPermission.CanView = false;
                    objuserPermission.CompID = byte.Parse(Session["CompID"].ToString());
                    objuserPermission.BranchID = byte.Parse(Session["BranchID"].ToString());
                    unitOfWork.userPermissionService.Insert(objuserPermission);
                    unitOfWork.Save();

                    responseMsg = "Permission inserted successfully";
                }
                else
                {
                    responseMsg =  "Permission already Exist";
                    //ViewData["errorFeestructure"] = "All Ready Exist";
                }
            }
            catch (Exception e)
            {
                responseMsg = "error : " + e.Message.ToString();
            }

            return new JsonResult
            {
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                Data = new
                {
                    ResponseMessage = responseMsg,
                }
            };
        }

        public ActionResult CreateUserPermission(string PRoleId, string  mModuleID)
         {
             if (Session["UserID"] == null) { return Redirect("~/"); }
             
             int UserID = int.Parse(PRoleId);
             int ModuleID = int.Parse(mModuleID);
             ViewData["RoleIdForPermission"] = UserID;
             ViewData["ModuleIDIDForPermission"] = ModuleID;

              
             List<RolePermission> objPermissionList = new List<RolePermission>();
             try
             {
                
                     int DuplicatePermitID = unitOfWork.userPermissionService.CheckDuplicatePermission(UserID,byte.Parse(Session["CompID"].ToString()),byte.Parse(Session["BranchID"].ToString()));
                     if (DuplicatePermitID == 0)
                     {
                        
                                 List<RolePermission> objvUserPermission = unitOfWork.userPermissionService.GetAllFormNameForPermission();

                                 foreach (RolePermission objForDetailSave in objvUserPermission)
                                 {

                                     RolePermission objuserPermission = new RolePermission();
                                     objuserPermission.Id = byte.Parse(UserID.ToString());
                                     objuserPermission.ModuleId = objForDetailSave.ModuleId;
                                     objuserPermission.CanAdd= false;
                                     objuserPermission.CanEdit = false;
                                     objuserPermission.CanView = false;
                                     objuserPermission.CompID = byte.Parse(Session["CompID"].ToString());
                                     objuserPermission.BranchID = byte.Parse(Session["BranchID"].ToString());
                                     unitOfWork.userPermissionService.Insert(objuserPermission);
                                     unitOfWork.Save();

                                 }


                                 objPermissionList = unitOfWork.userPermissionService.GetUserPermissionListUserwise(UserID, byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()));

                     }
                     else
                     {

                         objPermissionList = unitOfWork.userPermissionService.GetUserPermissionListUserwise(UserID, byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()));
                         ViewData["errorFeestructure"] = "Student All Ready Exist";
                     }

           

             }
             catch (Exception e)
             {
                 ViewData["EditError"] = e.Message;
             }



             return PartialView("ListUserPermissionView",objPermissionList);
         }

         public ActionResult SetAllUserPermission(string mOption, string mUserID,string mModuleID)
         {
             if (Session["UserID"] == null) { return Redirect("~/"); }

             bool Addflag =false, Modflag=false, Delflag = false;
             string[] bits = mOption.Split(',');
             foreach (string bit in bits)
             {
                 if (bit == "Add")
                 {
                     Addflag = true;
                 }
                 if (bit == "Modify")
                 {
                     Modflag = true;
                 }
                 if (bit == "Delete")
                 {
                     Delflag = true;
                 }
             }

             int UserID = int.Parse(mUserID);
             ViewData["UserIDForPermission"] = UserID;
             int ModuleID = int.Parse(mModuleID);
             ViewData["ModuleIDIDForPermission"] = ModuleID;

            List<RolePermission> objPermissionList = new List<RolePermission>();
             try
             {

                 int DuplicatePermitID = unitOfWork.userPermissionService.CheckDuplicatePermission(UserID, byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()));
                 if (DuplicatePermitID == 0)
                 {

                     List<RolePermission> objvUserPermission = unitOfWork.userPermissionService.GetAllFormNameForPermission();

                     foreach (RolePermission objForDetailSave in objvUserPermission)
                     {

                          RolePermission objuserPermission = new RolePermission();
                         objuserPermission.Id = byte.Parse(UserID.ToString());
                         objuserPermission.ModuleId = objForDetailSave.ModuleId;
                         objuserPermission.CanAdd = Addflag;
                         objuserPermission.CanEdit = Modflag;
                         objuserPermission.CanView = Delflag;
                         objuserPermission.BranchID = byte.Parse(Session["BranchID"].ToString());
                         objuserPermission.CompID = byte.Parse(Session["CompID"].ToString());
                         unitOfWork.userPermissionService.Insert(objuserPermission);
                         unitOfWork.Save();

                     }


                     objPermissionList = unitOfWork.userPermissionService.GetUserPermissionListUserwise(UserID, byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()));

                 }
                 else
                 {
                     objPermissionList = unitOfWork.userPermissionService.GetUserPermissionListUserwise(UserID, byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()));

                     try
                     {
                         foreach (RolePermission objuserPermit in objPermissionList)
                         {

                             unitOfWork.userPermissionService.UpdateAllPermissionToUser(objuserPermit, Addflag, Modflag, Delflag,ViewBag);
                             unitOfWork.Save();
                         }
                     }
                     catch (Exception e)
                     {
                        // updateValues.SetErrorText(product, e.Message);
                     }

                     objPermissionList = unitOfWork.userPermissionService.GetUserPermissionListUserwise(UserID, byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()));
                     ViewData["errorFeestructure"] = "Student All Ready Exist";
                 }

                 //   }

             }
             catch (Exception e)
             {
                 ViewData["EditError"] = e.Message;
             }

             return PartialView("ListUserPermissionView", objPermissionList);
         }


         public ActionResult PartialUserPermissionView(int PRoleId)
         {
             if (Session["UserID"] == null) { return Redirect("~/"); }

             ViewData["RoleIdForPermission"] = PRoleId;
             return PartialView("ListUserPermissionView", new UnitOfWork().userPermissionService.GetUserPermissionListUserwise(PRoleId, byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));
         }

       
         [ValidateInput(false)]
         public ActionResult updateUserPermissionAll(MVCxGridViewBatchUpdateValues<RolePermission, int> updateValues, int PRoleId)
         {
             if (Session["UserID"] == null) { return Redirect("~/"); }

             ViewData["RoleIdForPermission"] = PRoleId;
             //foreach (var product in updateValues.Insert)
             //{
             //    if (updateValues.IsValid(product))
             //        InsertProduct(product, updateValues);
             //}
             foreach (var product in updateValues.Update)
             {
                 if (updateValues.IsValid(product))
                     UpdateProduct(product, updateValues, PRoleId);
             }
            //foreach (var productID in updateValues.DeleteKeys)
            //{
            //    DeleteProduct(productID, updateValues);
            //}

            return PartialView("ListUserPermissionView", new UnitOfWork().rolePermissionservices.GetRolePermissionByRoleIDAndModuleID(PRoleId));
        }

         protected void UpdateProduct(RolePermission product, MVCxGridViewBatchUpdateValues<RolePermission, int> updateValues,int RoleId)
         {
            
             try
             {
                 unitOfWork.userPermissionService.UpdateUserPermission(product);
                 unitOfWork.Save();
             }
             catch (Exception e)
             {
                 updateValues.SetErrorText(product, e.Message);
             }
         }
     

    }


}



    