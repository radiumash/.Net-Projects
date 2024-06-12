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
using DevExpress.Web.Mvc;

namespace appSchool.Controllers
{
    [NoCache]
    public class SubjectAllotmentController : Controller
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        private SqlConnection _mConn;
        private SqlTransaction _mTran;


        public ActionResult Index()
        {
            if (Session["UserID"] == null || (int)SubMenuModules.appSubjectAllotment == 0)
            {
                return Redirect("~/");
            }


            UserPermission objuser = new UserPermission();
            objuser = unitOfWork.userPermissionService.CheckUserPermissionModulewise(int.Parse(Session["UserID"].ToString()), 11, byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()));
            if (objuser != null)
            {
                PermissionFlag._AddFlag = objuser.AddP;
                PermissionFlag._ModFlag = objuser.ModP;
                PermissionFlag._DelFlag = objuser.DelP;
            }
            else
            {
                PermissionFlag._AddFlag = false;
                PermissionFlag._ModFlag = false;
                PermissionFlag._DelFlag = false;
            }



            return PartialView("Index");
        }


        public ActionResult PartialGridSubjectLevelOne(int PClassID, int mSubjectlevel)
        {
            ViewData["ClassID"] = PClassID;
            ViewData["SubjectLevel"] = mSubjectlevel;

            return PartialView("ListForSubjectlevelOne", new UnitOfWork().subjectLevelService.GetSubjectLevel1List( byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));
        }


        public ActionResult ListPartialClassSetup()
        {
            return PartialView("ListClassGridLookupPartial", unitOfWork.ClassService.GetClassList(byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));
        }

        public ActionResult PartialGridLookUpSubjectLevelOne()
        {
            return PartialView("ListSubjectOneGridLookupPartial", new UnitOfWork().subjectLevelService.GetSubjectLevel1List(byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));
        }

        public ActionResult PartialGridLookUpSubjectLevelTwo()
        {
            return PartialView("ListSubjectTwoGridLookupPartial", new UnitOfWork().subjectLevel2Service.GetSubjectLevel2List(byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));
        }

        public ActionResult PartialGridSubjectLeveTwo(int PClassID, int mSubjectlevel)
        {
            ViewData["ClassID"] = PClassID;
            ViewData["SubjectLevel"] = mSubjectlevel;
            return PartialView("ListForSubjectLevelTwo", new UnitOfWork().SubjectAllotmentService.GetSubjectlevelTwoList(byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));
        }

        public ActionResult PartialGridSubjectLevelThree(int PClassID, int mSubjectlevel)
        {
            ViewData["ClassID"] = PClassID;
            ViewData["SubjectLevel"] = mSubjectlevel;
            return PartialView("ListForSubjectLevelThree", new UnitOfWork().SubjectAllotmentService.GetSubjectlevelThreeList(byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));
        }

        public ActionResult PartialGridSubjectAllotment()
        {
           
            List<SubjectAllotment> lstSubAllot = unitOfWork.SubjectAllotmentService.GetSubjectAllotmentListByCompID(byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()));

            return PartialView("ListSubjectAllotmentView", lstSubAllot);
        }

        public JsonResult GetSubjectlevelbyClassID(string MClassID)
        {
            byte mSubjectLevel = 0;
            string mSubjectLevelName = string.Empty;
           
            ViewData["ClassIDForSubjectAllotment"] = int.Parse(MClassID);

            Class objclass = new Class();
            objclass = unitOfWork.SubjectAllotmentService.GetSubjectLevelByClassID(int.Parse(MClassID));

            if (objclass != null)
            {
                mSubjectLevel = byte.Parse(objclass.SubjectLevel.ToString());
                if (mSubjectLevel == 1)
                {
                    mSubjectLevelName = "Subject level One";
                }
                else
                {
                    if (mSubjectLevel == 2)
                    {
                        mSubjectLevelName = "Subject level Two";

                    }

                    else
                    {
                        mSubjectLevelName = "Subject Level Three";
                    }
                }
               
            }


            return Json(new { ResultSubLevel = mSubjectLevel, ResultSubLevelName = mSubjectLevelName }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ShowSubjectForClass(int PClassID, int PSubjectlevel)
        {
            string Errormsg = string.Empty;
            int  ResultSubjectLevel = 1;
            ViewData["ClassID"] = PClassID;
            ViewData["SubjectLevel"] = PSubjectlevel;



            if (PSubjectlevel == 1)
            {
                //ViewData["SubjectLevel"] = PSubjectlevel;
                ResultSubjectLevel = 1;
                List<SubjectLevelOne> lst = unitOfWork.SubjectAllotmentService.GetSubjectlevelOneListByClassID(PClassID, PSubjectlevel, byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()));

                List<SubjectAllotment> lstSubAllot = unitOfWork.SubjectAllotmentService.GetSubjectAllotmentList(PClassID, PSubjectlevel);
                
                return Json(new { DisplaySubjectLevel = ResultSubjectLevel, ListData = cCommon.RenderRazorViewToString("ListForSubjectlevelOne", lst, ControllerContext, ViewData, TempData),
                                  ListDataSuballot = cCommon.RenderRazorViewToString("ListForSubjectAllotment", lstSubAllot, ControllerContext, ViewData, TempData)
                                   }, JsonRequestBehavior.AllowGet);
    
            }

         

             else
            {
                if (PSubjectlevel == 2)
                {
                  
                    ResultSubjectLevel = 2;
                    List<vSubjectAllotmentwithIDLTwo> lst2 = unitOfWork.SubjectAllotmentService.GetSubjectlevelTwoListByClassID(PClassID, PSubjectlevel, byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()));
                    List<SubjectAllotment> lstSubAllot = unitOfWork.SubjectAllotmentService.GetSubjectAllotmentList(PClassID, PSubjectlevel);

                
                    return Json(new { DisplaySubjectLevel = ResultSubjectLevel, ListData = cCommon.RenderRazorViewToString("ListForSubjectLevelTwo", lst2, ControllerContext, ViewData, TempData),
                                      ListDataSuballot = cCommon.RenderRazorViewToString("ListForSubjectAllotment", lstSubAllot, ControllerContext, ViewData, TempData)
                                        }, JsonRequestBehavior.AllowGet);
                }

                else
                {
                  

                    ResultSubjectLevel = 3;
                    List<vSubjectAllotmentwithIDLThree> lst3 = unitOfWork.SubjectAllotmentService.GetSubjectlevelThreeListByClassID(PClassID, PSubjectlevel, byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()));
                    List<SubjectAllotment> lstSubAllot = unitOfWork.SubjectAllotmentService.GetSubjectAllotmentList(PClassID, PSubjectlevel);
                    
                    return Json(new { DisplaySubjectLevel = ResultSubjectLevel, ListData = cCommon.RenderRazorViewToString("ListForSubjectLevelThree", lst3, ControllerContext, ViewData, TempData),
                                      ListDataSuballot = cCommon.RenderRazorViewToString("ListForSubjectAllotment", lstSubAllot, ControllerContext, ViewData, TempData)
                    }, JsonRequestBehavior.AllowGet);
    
                }

            }

          
        
        
        }


        public ActionResult AddSubjectsToSubjectAllotment(string ClassIDs, string SubjectOneIDs, string SubjectTwoIDs, int Issubjectlevetwo)
        {
            if (Session["UserID"] == null )
            {
                return Redirect("~/");
            }

            string Errormsg = string.Empty;

            string[] ClassIdlist = ClassIDs.Split(',');
            string[] Subjectonedlist = SubjectOneIDs.Split(',');
            string[] Subjecttwodlist = SubjectTwoIDs.Split(',');

            byte mCompID = byte.Parse(Session["CompID"].ToString());
            byte mBranchID = byte.Parse(Session["BranchID"].ToString());

            foreach (string classid in ClassIdlist)
            {
                foreach(string subjectidone in Subjectonedlist)
                {
                    if (Issubjectlevetwo == 1)
                    {
                        foreach (string subjectid2 in Subjecttwodlist)
                        {
                            SubjectAllotment objsubjectallot = new SubjectAllotment();
                            objsubjectallot.ClassID = int.Parse(classid);
                            objsubjectallot.IDL1 = int.Parse(subjectidone);
                            objsubjectallot.IDL2 = int.Parse(subjectid2);
                            objsubjectallot.IDL3 = -1;
                            objsubjectallot.BranchID = mBranchID;
                            objsubjectallot.CompID = mCompID;

                            //InsertSubjectAllotment(objsubjectallot);
                            AddSubjectAllotment(objsubjectallot);
                        }
                       
                    }
                    else
                    {
                        SubjectAllotment objsubjectallot = new SubjectAllotment();
                        objsubjectallot.ClassID = int.Parse(classid);
                        objsubjectallot.IDL1 = int.Parse(subjectidone);
                        objsubjectallot.IDL2 = -1;
                        objsubjectallot.IDL3 = -1;
                        objsubjectallot.BranchID = mBranchID;
                        objsubjectallot.CompID = mCompID;

                        //InsertSubjectAllotment(objsubjectallot);
                        AddSubjectAllotment(objsubjectallot);
                    }
                }
            }

            List<SubjectAllotment> lstSubAllot = unitOfWork.SubjectAllotmentService.GetSubjectAllotmentListByCompID(byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()));

            return Json(new
            {
                DataResponseMsg = Errormsg,
                ListDataSuballot = cCommon.RenderRazorViewToString("ListSubjectAllotmentView", lstSubAllot, ControllerContext, ViewData, TempData)
            }, JsonRequestBehavior.AllowGet
            );


        }

        public ActionResult DeleteSubjectAlloted(string ClassIDs, string SubjectOneIDs, string SubjectTwoIDs, string SubjectThreeIDs)
        {
            if (Session["UserID"] == null)
            {
                return Redirect("~/");
            }

            string Errormsg = "";

            try
            {
                SubjectAllotment objsubjectallot = new SubjectAllotment();

                objsubjectallot.ClassID = int.Parse(ClassIDs);

                objsubjectallot.IDL1 = int.Parse(SubjectOneIDs);

                objsubjectallot.IDL2 = int.Parse(SubjectTwoIDs);

                objsubjectallot.IDL3 = int.Parse(SubjectThreeIDs);

                objsubjectallot.BranchID = byte.Parse(Session["BranchID"].ToString());

                objsubjectallot.CompID = byte.Parse(Session["CompID"].ToString());

                unitOfWork.SubjectAllotmentService.Delete(objsubjectallot);
                unitOfWork.Save();

                Errormsg = "Subject Deleted Successfully";
            }
            catch(Exception ex)
            {

            }


            List<SubjectAllotment> lstSubAllot = unitOfWork.SubjectAllotmentService.GetSubjectAllotmentListByCompID(byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()));

            return Json(new
            {
                DataResponseMsg = Errormsg,
                ListDataSuballot = cCommon.RenderRazorViewToString("ListSubjectAllotmentView", lstSubAllot, ControllerContext, ViewData, TempData)
            }, JsonRequestBehavior.AllowGet
            );

            

        }

        public ActionResult AllotmentSubjectForClass(int ClassID, int pSubjectlevel, string PSubjectlevelID)
        {
            string Errormsg = string.Empty;
            int NewID = 0;

            try
            {
                string[] StudentIDList = PSubjectlevelID.Split(',');
                 foreach (string SubjectlevelID in StudentIDList)
                 {
                     int MSubjectLevelID = int.Parse(SubjectlevelID);

                     if (pSubjectlevel == 1)
                     {


                         int Subjectlevel2 = -1;
                         int Subjectlevel3 = -1;
                         int i = int.Parse(SubjectlevelID);

                         //NewID = AddSubjectAllotment(ClassID, i, Subjectlevel2, Subjectlevel3);

                         if (NewID > 0)
                         {
                             Errormsg = "Subject Alloted";
                         }
                         else
                         {
                             Errormsg = "Already Subject Alloted For this Class";
                         }
                     }



                     else
                     {
                         if (pSubjectlevel == 2)
                         {
                             int mSubjectLevel1 = 0;
                             int mSubjectLevel3 = -1;
                             SubjectLevelTwo objsubl1 = new SubjectLevelTwo();
                             objsubl1 = unitOfWork.SubjectAllotmentService.GetSubjectlevelOneByLevelTwo(MSubjectLevelID);
                             if (objsubl1 != null)
                             {
                                 mSubjectLevel1 = objsubl1.IdL1;
                             }

                             //NewID = AddSubjectAllotment(ClassID, mSubjectLevel1, MSubjectLevelID, mSubjectLevel3);

                             if (NewID > 0)
                             {
                                 Errormsg = "Subject Alloted";
                             }
                             else
                             {
                                 Errormsg = "Already Subject Alloted For this Class";
                             }

                         }

                         else
                         {

                             int mSubjectLevel1 = 0;
                             int mSubjectLevel2 = 0;
                             SubjectLevelThree objsub3 = new SubjectLevelThree();
                             objsub3 = unitOfWork.SubjectAllotmentService.GetSubjectlevelOneAndTwoByLevelThree(MSubjectLevelID);
                             if (objsub3 != null)
                             {

                                 mSubjectLevel2 = objsub3.IdL2;
                                 SubjectLevelTwo objsubl2 = new SubjectLevelTwo();

                                 objsubl2 = unitOfWork.SubjectAllotmentService.GetSubjectlevelOneByLevelTwo(mSubjectLevel2);
                                 if (objsubl2 != null)
                                 {
                                     mSubjectLevel1 = objsubl2.IdL1;
                                 }
                             }

                             //NewID = AddSubjectAllotment(ClassID, mSubjectLevel1, mSubjectLevel2, MSubjectLevelID);

                             if (NewID > 0)
                             {
                                 Errormsg = "Subject Alloted";
                             }
                             else
                             {
                                 Errormsg = "Already Subject Alloted For this Class";
                             }
                         }

                     }


                 }   
               



            }

            catch (Exception e)
            {
                // updateValues.SetErrorText(product, e.Message);
            }

            ViewData["ClassID"]  = ClassID;
            ViewData["SubjectLevel"] = pSubjectlevel;

            List<SubjectAllotment> lst = unitOfWork.SubjectAllotmentService.GetSubjectAllotmentList(ClassID, pSubjectlevel);
            return Json(new { Displaymsg = Errormsg, ListData = cCommon.RenderRazorViewToString("ListForSubjectAllotment", lst, ControllerContext, ViewData, TempData) }, JsonRequestBehavior.AllowGet);
    
        }


        public int AddSubjectAllotment(SubjectAllotment objsub)
        {
            int Res = 0;

            SqlCommand cmd = null;
            try
            {
                cmd = new SqlCommand("Add_SubjectAllotment", DB.GetActiveConnection());
                cmd.CommandType = CommandType.StoredProcedure;


                cmd.Parameters.AddWithValue("@ClassID", objsub.ClassID);
                cmd.Parameters.AddWithValue("@IDL1", objsub.IDL1);
                cmd.Parameters.AddWithValue("@IDL2", objsub.IDL2);
                cmd.Parameters.AddWithValue("@IDL3", objsub.IDL3);
                cmd.Parameters.AddWithValue("@CompID", objsub.CompID);
                cmd.Parameters.AddWithValue("@BranchID", objsub.BranchID);

                Res = cmd.ExecuteNonQuery();

            }
            catch(Exception ex)
            {

            }
            finally
            {
               if(cmd != null)
                {
                    if (cmd.Connection.State == ConnectionState.Open)
                    {
                        cmd.Connection.Close();
                    }
                }
            }
            
            return Res;
        }



        public ActionResult UpdateSubjectAllotment(MVCxGridViewBatchUpdateValues<SubjectAllotment, int> updateValues)
        {
            if (Session["UserID"] == null) { return Redirect("~/"); }

            int mFeesStructID = 0;
            try
            {

                foreach (var product in updateValues.Insert)
                {
                    if (updateValues.IsValid(product))
                        InsertSubjectAllotment(product, updateValues);
                }
                foreach (var product in updateValues.Update)
                {
                    if (updateValues.IsValid(product))
                        UpdateSubjectAllotment(product, updateValues);
                }
                foreach (var productID in updateValues.DeleteKeys)
                {
                    DeleteSubjectAllotment(productID, updateValues);
                }


                decimal FeesAmount = 0;

                FeesAmount = unitOfWork.feesStructureDetailService.GetTotalFeesAmountByClassID(mFeesStructID, int.Parse(Session["SessionID"].ToString()));

                FeesStructureMaster objSM = new FeesStructureMaster();
                objSM.FeeStructAmount = FeesAmount;
                objSM.FeeStructID = mFeesStructID;
                unitOfWork.feesStructureMasterService.UpdateFeesAmountMaster(objSM);
                unitOfWork.Save();


            }
            catch (Exception e)
            {

            }


            ViewData["FeesStructID"] = mFeesStructID;
            return PartialView("ListFeesStructureForEdit", unitOfWork.feesStructureDetailService.GetFeeStructureDetailbyFeeStructID(mFeesStructID));
        }


        protected bool InsertSubjectAllotment(SubjectAllotment product)
        {
            bool flag = false;
            try
            {
                unitOfWork.SubjectAllotmentService.InsertSubjectAllotment(product, byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()));
                unitOfWork.Save();
                flag = true;
            }
            catch (Exception e)
            {
                flag = false;
            }
            return flag;
        }

        protected void InsertSubjectAllotment(SubjectAllotment product, MVCxGridViewBatchUpdateValues<SubjectAllotment, int> updateValues)
        {
            try
            {
                unitOfWork.SubjectAllotmentService.InsertSubjectAllotment(product, byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()));
                unitOfWork.Save();
            }
            catch (Exception e)
            {
                updateValues.SetErrorText(product, e.Message);
            }
        }

        protected void UpdateSubjectAllotment(SubjectAllotment product, MVCxGridViewBatchUpdateValues<SubjectAllotment, int> updateValues)
        {
            try
            {
                unitOfWork.SubjectAllotmentService.UpdateSubjectAllotment(product, byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()));
                unitOfWork.Save();
            }
            catch (Exception e)
            {
                //updateValues.SetErrorText(product, e.Message);
            }
        }
        protected void DeleteSubjectAllotment(int product, MVCxGridViewBatchUpdateValues<SubjectAllotment, int> updateValues)
        {
            try
            {
                unitOfWork.SubjectAllotmentService.Delete(product);
                unitOfWork.Save();
            }
            catch (Exception e)
            {
                updateValues.SetErrorText(product, e.Message);
            }
        }
       








    }


}
