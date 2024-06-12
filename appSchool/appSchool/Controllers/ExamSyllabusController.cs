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
using System.Data.SqlClient;
using System.Data;

namespace appSchool.Controllers
{
    [NoCache]
    public class ExamSyllabusController : Controller
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        private SqlConnection _mConn;
        private SqlTransaction _mTran;


        public ActionResult Index()
        {
            if (Session["UserID"] == null || (int)SubMenuModules.appExamSyllabus == 0)
            {
                return Redirect("~/");
            }

            UserPermission objuser = new UserPermission();
            objuser = unitOfWork.userPermissionService.CheckUserPermissionModulewise(int.Parse(Session["UserID"].ToString()), 85, byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()));
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

            ViewData["ExamSyllabusID"] = 0;
            ViewData["ClassID_ForExamSyllabus"] = 0;
            ViewData["SubjectID_ForExamSyllabus"] = 0;
            ViewData["ExamID_ForExamSyllabus"] = 0;

            return View();
        }

        public ActionResult PartialExamSyllabusView(int pExamSyllabusID, int pClassID, int pSubjectID, int pExamID)
        {
            ViewData["ExamSyllabusID"] = pExamSyllabusID;
            ViewData["ClassID_ForExamSyllabus"] = pClassID;
            ViewData["SubjectID_ForExamSyllabus"] = pSubjectID;
            ViewData["ExamID_ForExamSyllabus"] = pExamID;

            return PartialView("ListExamSyllabus", unitOfWork.examSyllabusDetailService.GetExamSyllabusDetailListByExamSyllabusID(pExamSyllabusID,byte.Parse(Session["CompID"].ToString()),byte.Parse(Session["BranchID"].ToString())));
        }

        public ActionResult GridViewCustomActionPartial(string customAction, int pExamSyllabusID, int pClassID, int pSubjectID, int pExamID)
        {
            if (customAction == "delete")
            {
                //SafeExecute(() => PerformDelete());
                //string vartem = 1; 
            }
            return PartialExamSyllabusView(pExamSyllabusID, pClassID, pSubjectID, pExamID);
        }

        public ActionResult AddNewExamSyllabus(ExamSyllabusDetail objClass, int pExamSyllabusID)
        {
            if (ModelState.IsValid)
            {
                try
                {



                    objClass.CompID = byte.Parse(Session["CompID"].ToString());
                    objClass.BranchID = byte.Parse(Session["BranchID"].ToString());
                    //objClass.UIDAdd = byte.Parse(Session["UserID"].ToString());
                    //objClass.AddDate = DateTime.Now;


                    unitOfWork.examSyllabusDetailService.InsertExamSyllabusDetail(objClass);
                    unitOfWork.Save();

                    _mTran.Commit();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            ViewData["EditableClass"] = objClass;
            return View();
            //return PartialView("ListExamSyllabus", unitOfWork.examSyllabusDetailService.GetExamSyllabusDetailListByExamSyllabusID(pExamSyllabusID, byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));
        }

        //[HttpPost, ValidateInput(false)]
        //public ActionResult UpdateClassSyllabus(ClassSyllabusDetail objClass)
        //{
        //    _mConn = DB.GetActiveConnection();
        //    _mTran = _mConn.BeginTransaction(IsolationLevel.Snapshot);

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {

        //            unitOfWork.ClassSyllabusDetailService.UpdateClassSyllabusDetail(objClass);
        //            unitOfWork.Save();

        //            _mTran.Commit();
        //        }
        //        catch (Exception e)
        //        {
        //            ViewData["EditError"] = e.Message;
        //        }
        //    }
        //    else
        //        ViewData["EditError"] = "Please, correct all errors.";
        //    ViewData["EditableClass"] = objClass;
        //    return PartialView("ListFacultyAllotment", unitOfWork.examSyllabusDetailService.GetExamSyllabusDetailListByExamSyllabusID(pExamSyllabusID, byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));
        //}


        public List<modelSubjectOne> GetSubjectsByClassID(int mClassID)
        {
            List<modelSubjectOne> obj = new List<modelSubjectOne>();

            DataTable dt = new DataTable();

            string sql = " SELECT DISTINCT dbo.SubjectAllotment.IDL1, dbo.SubjectAllotment.ClassID, dbo.SubjectLevelOne.SubjectCodeL1, " + 
                         " dbo.SubjectLevelOne.SubjectNameL1,   dbo.SubjectAllotment.BranchID, dbo.SubjectAllotment.CompID, " +
                         " dbo.SubjectLevelOne.Order1  FROM   dbo.SubjectAllotment INNER JOIN   dbo.SubjectLevelOne ON dbo.SubjectAllotment.IDL1 = dbo.SubjectLevelOne.IdL1 " +
                         " Where dbo.SubjectAllotment.ClassID="+mClassID +" And     dbo.SubjectAllotment.BranchID="+int.Parse(Session["BranchID"].ToString()) +"AND " + 
                         " dbo.SubjectAllotment.CompID="+int.Parse(Session["CompID"].ToString());

            dt = DB.ExecuteQuery(sql);

            foreach(DataRow dr in dt.Rows)
            {
                modelSubjectOne objin = new modelSubjectOne();
                objin.SubjectID = int.Parse(dr["IDL1"].ToString());
                objin.SubjectName = dr["SubjectNameL1"].ToString();
                obj.Add(objin);
            }

            return obj;
        }



         public ActionResult GetSubjectList(string pClassID)
         {
             List<modelSubjectOne> obj = new List<modelSubjectOne>();

             obj = GetSubjectsByClassID(int.Parse(pClassID));

             return PartialView("SubjectPartialView", obj);
         }


         public ActionResult GetExamSyllabuseDetailView(string pClassID, string pSubjectID, string pExamID)
         {
             int ExamSyllabusID = 0;
             int mClassID = int.Parse(pClassID);
             int mSubjectID = int.Parse(pSubjectID);
             int mExamID = int.Parse(pExamID);

             ExamSyllabusMaster objFill = new ExamSyllabusMaster();
             objFill.ClassID = int.Parse(pClassID);
             objFill.SubjectIDL1 = mSubjectID;
             objFill.ExamID = mExamID;
             objFill.CompID = byte.Parse(Session["CompID"].ToString());
             objFill.BranchID = byte.Parse(Session["BranchID"].ToString());
             objFill.SessionID = byte.Parse(Session["SessionID"].ToString());
             objFill.UIDAdd = byte.Parse(Session["UserID"].ToString());
             objFill.AddDate = DateTime.Now;


             ExamSyllabusMaster objCheck = new ExamSyllabusMaster();
             objCheck = unitOfWork.examSyllabusMasterService.GetExamSyllabusMasterData(objFill);
             if (objCheck==null)
             {

                 unitOfWork.examSyllabusMasterService.InsertExamSyllabusMaster(objFill);
                 unitOfWork.Save();
                 ExamSyllabusID = objFill.ExamSyllabusID;
             }
             else
             {
                 objCheck.UIDMod = byte.Parse(Session["UserID"].ToString());
                 objCheck.ModDate = DateTime.Now;

                 unitOfWork.examSyllabusMasterService.UpdateExamSyllabusMaster(objCheck);
                 unitOfWork.Save();

                 ExamSyllabusID = objCheck.ExamSyllabusID;
             }

             List<vExamSyllabusDetail> obj = new List<vExamSyllabusDetail>();
             if (ExamSyllabusID > 0)
             {
                 obj = unitOfWork.examSyllabusDetailService.GetExamSyllabusDetailListByExamSyllabusID(ExamSyllabusID, byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()));
                 if (obj.Count ==0)
                 {



                     List<vClassSyllabusSubjectwise> objDetail = new List<vClassSyllabusSubjectwise>();
                     objDetail = unitOfWork.examSyllabusMasterService.GetClassSyllabusSubjectandClassWise(mClassID, mSubjectID, byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()));
                     if (objDetail != null)
                     {
                         foreach (vClassSyllabusSubjectwise objnew in objDetail)
                         {
                             ExamSyllabusDetail objExamSyllDetail = new ExamSyllabusDetail();
                             objExamSyllDetail.BranchID = byte.Parse(Session["BranchID"].ToString());
                             objExamSyllDetail.CompID = byte.Parse(Session["BranchID"].ToString());
                             objExamSyllDetail.ExamSyllabusID = ExamSyllabusID;
                             objExamSyllDetail.OrderNo = objnew.OrderNo;
                             objExamSyllDetail.SessionID = byte.Parse(Session["SessionID"].ToString());

                             unitOfWork.examSyllabusDetailService.InsertExamSyllabusDetail(objExamSyllDetail);
                             unitOfWork.Save();
                         }

                     }

                     obj = unitOfWork.examSyllabusDetailService.GetExamSyllabusDetailListByExamSyllabusID(ExamSyllabusID, byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()));

                 }


             }

             ViewData["ExamSyllabusID"] = ExamSyllabusID;
             ViewData["ClassID_ForExamSyllabus"] = pClassID;
             ViewData["SubjectID_ForExamSyllabus"] = pSubjectID;
             ViewData["ExamID_ForExamSyllabus"] = pExamID;

             return PartialView("ListExamSyllabus", obj);
         }


         public ActionResult GetExamSyllabuseDetailViewUPDATE(string pClassID, string pSubjectID, string pExamID)
         {
             int ExamSyllabusID = 0;
             int mClassID = int.Parse(pClassID);
             int mSubjectID = int.Parse(pSubjectID);
             int mExamID = int.Parse(pExamID);

             ExamSyllabusMaster objFill = new ExamSyllabusMaster();
             objFill.ClassID = int.Parse(pClassID);
             objFill.SubjectIDL1 = mSubjectID;
             objFill.ExamID = mExamID;
             objFill.CompID = byte.Parse(Session["CompID"].ToString());
             objFill.BranchID = byte.Parse(Session["BranchID"].ToString());
             objFill.SessionID = byte.Parse(Session["SessionID"].ToString());
             objFill.UIDAdd = byte.Parse(Session["UserID"].ToString());
             objFill.AddDate = DateTime.Now;


             ExamSyllabusMaster objCheck = new ExamSyllabusMaster();
             objCheck = unitOfWork.examSyllabusMasterService.GetExamSyllabusMasterData(objFill);
             if (objCheck==null)
             {

                 unitOfWork.examSyllabusMasterService.InsertExamSyllabusMaster(objFill);
                 unitOfWork.Save();
                 ExamSyllabusID = objFill.ExamSyllabusID;
             }
             else
             {
                 objCheck.UIDMod = byte.Parse(Session["UserID"].ToString());
                 objCheck.ModDate = DateTime.Now;

                 unitOfWork.examSyllabusMasterService.UpdateExamSyllabusMaster(objCheck);
                 unitOfWork.Save();

                 ExamSyllabusID = objCheck.ExamSyllabusID;
             }

             List<vExamSyllabusDetail> obj = new List<vExamSyllabusDetail>();
             if (ExamSyllabusID > 0)
             {
                 obj = unitOfWork.examSyllabusDetailService.GetExamSyllabusDetailListByExamSyllabusID(ExamSyllabusID, byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()));
                 if (obj.Count ==0)
                 {
                     
                     List<vClassSyllabusSubjectwise> objDetail = new List<vClassSyllabusSubjectwise>();
                     objDetail = unitOfWork.examSyllabusMasterService.GetClassSyllabusSubjectandClassWise(mClassID, mSubjectID, byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()));
                     if (objDetail != null)
                     {
                         foreach (vClassSyllabusSubjectwise objnew in objDetail)
                         {
                             ExamSyllabusDetail objExamSyllDetail = new ExamSyllabusDetail();
                             objExamSyllDetail.BranchID = byte.Parse(Session["BranchID"].ToString());
                             objExamSyllDetail.CompID = byte.Parse(Session["BranchID"].ToString());
                             objExamSyllDetail.ExamSyllabusID = ExamSyllabusID;
                             objExamSyllDetail.OrderNo = objnew.OrderNo;
                             objExamSyllDetail.SessionID = byte.Parse(Session["SessionID"].ToString());

                             unitOfWork.examSyllabusDetailService.InsertExamSyllabusDetail(objExamSyllDetail);
                             unitOfWork.Save();
                         }

                     }

                     obj = unitOfWork.examSyllabusDetailService.GetExamSyllabusDetailListByExamSyllabusID(ExamSyllabusID, byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()));

                 }
                 else
                 {
                     foreach (vExamSyllabusDetail objdelete in obj)
                     {
                         unitOfWork.examSyllabusDetailService.Delete(objdelete.ID);
                         unitOfWork.Save();
                     }

                     obj = null;

                     List<vClassSyllabusSubjectwise> objDetail = new List<vClassSyllabusSubjectwise>();
                     objDetail = unitOfWork.examSyllabusMasterService.GetClassSyllabusSubjectandClassWise(mClassID, mSubjectID, byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()));
                     if (objDetail != null)
                     {
                         foreach (vClassSyllabusSubjectwise objnew in objDetail)
                         {
                             ExamSyllabusDetail objExamSyllDetail = new ExamSyllabusDetail();
                             objExamSyllDetail.BranchID = byte.Parse(Session["BranchID"].ToString());
                             objExamSyllDetail.CompID = byte.Parse(Session["BranchID"].ToString());
                             objExamSyllDetail.ExamSyllabusID = ExamSyllabusID;
                             objExamSyllDetail.OrderNo = objnew.OrderNo;
                             objExamSyllDetail.SessionID = byte.Parse(Session["SessionID"].ToString());

                             unitOfWork.examSyllabusDetailService.InsertExamSyllabusDetail(objExamSyllDetail);
                             unitOfWork.Save();
                         }

                     }

                     obj = unitOfWork.examSyllabusDetailService.GetExamSyllabusDetailListByExamSyllabusID(ExamSyllabusID, byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()));
                   }

             }

             ViewData["ExamSyllabusID"] = ExamSyllabusID;
             ViewData["ClassID_ForExamSyllabus"] = pClassID;
             ViewData["SubjectID_ForExamSyllabus"] = pSubjectID;
             ViewData["ExamID_ForExamSyllabus"] = pExamID;

             return PartialView("ListExamSyllabus", obj);
         }


   

        [ValidateInput(false)]
         public ActionResult updateExamSyllabusAll(MVCxGridViewBatchUpdateValues<vExamSyllabusDetail, int> updateValues, int pExamSyllabusID, int pClassID, int pSubjectID, int pExamID)
        {
            if (pExamSyllabusID == 0 || pClassID == 0 || pSubjectID == 0 || pExamID == 0)
            {
                return PartialView("ListExamSyllabus", new UnitOfWork().examSyllabusDetailService.GetExamSyllabusDetailListByExamSyllabusID(pExamSyllabusID, byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));
            }

            ViewData["ExamSyllabusID"] = pExamSyllabusID;
            ViewData["ClassID_ForExamSyllabus"] = pClassID;
            ViewData["SubjectID_ForExamSyllabus"] = pSubjectID;
            ViewData["ExamID_ForExamSyllabus"] = pExamID;
          

            //foreach (var product in updateValues.Insert)
            //{
            //    if (updateValues.IsValid(product))
            //        InsertProduct(product, updateValues, pExamSyllabusID);
            //}
            //foreach (var product in updateValues.Update)
            //{
            //    if (updateValues.IsValid(product))
            //        UpdateProduct(product, updateValues);
            //}
            foreach (var productID in updateValues.DeleteKeys)
            {
                DeleteProduct(productID, updateValues);
            }


            return PartialView("ListExamSyllabus", new UnitOfWork().examSyllabusDetailService.GetExamSyllabusDetailListByExamSyllabusID(pExamSyllabusID, byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));
        }

     
        protected void DeleteProduct(int product, MVCxGridViewBatchUpdateValues<vExamSyllabusDetail, int> updateValues)
        {
            try
            {
                unitOfWork.examSyllabusDetailService.Delete(product);
                unitOfWork.Save();
            }
            catch (Exception e)
            {
                updateValues.SetErrorText(product, e.Message);
            }
        }
        //protected void InsertProduct(FacultyAllotmentDetail product, MVCxGridViewBatchUpdateValues<FacultyAllotmentDetail, int> updateValues, int mFacultyAllotmentID)
        //{
        //    try
        //    {


        //        product.BranchID = byte.Parse(Session["BranchID"].ToString());
        //        product.CompID = byte.Parse(Session["CompID"].ToString());
        //        product.SessionID = byte.Parse(Session["SessionID"].ToString());
        //        product.FacultyAllotmentID = mFacultyAllotmentID;

        //        unitOfWork.FacultyAllotmentDetailService.InsertFacultyAllotmentDetail(product);
        //        unitOfWork.Save();

        //    }
        //    catch (Exception e)
        //    {
        //        updateValues.SetErrorText(product, e.Message);
        //    }
        //}
     
    

    }


}



    