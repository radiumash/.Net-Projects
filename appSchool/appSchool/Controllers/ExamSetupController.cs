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
    public class ExamSetupController : Controller
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        private SqlConnection _mConn;
        private SqlTransaction _mTran;


        public ActionResult Index()
        {
            if (Session["UserID"] == null || (int)SubMenuModules.appExamSetup == 0)
            {
                return Redirect("~/");
            }

            UserPermission objuser = new UserPermission();
            objuser = unitOfWork.userPermissionService.CheckUserPermissionModulewise(int.Parse(Session["UserID"].ToString()), 86, byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()));
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

            ViewData["ExamID"] = 0;
            ViewData["ClassID"] = 0;
            ViewData["ExamSetupID"] = 0;


            return View("Index");
        }

        public ActionResult PartialExamSetupView(int pExamSetupID, int pClassID, int pExamID )
        {
            ViewData["ExamID"] = pExamID;
            ViewData["ClassID"] = pClassID;
            ViewData["ExamSetupID"] = pExamSetupID;

            return PartialView("ListExamSetup",unitOfWork.examSetupDetailService.GetExamSetupDetailListByExamSetupID(pExamSetupID,byte.Parse(Session["CompID"].ToString()),byte.Parse(Session["BranchID"].ToString())));
        }
       

         //public ActionResult GetClassSetupList(string pClassID)
         //{
         //    List<ClassSetup> obj = new List<ClassSetup>();

         //    obj=unitOfWork.classSetupService.GetAllClassNameByClassID(int.Parse(pClassID));

         //    return PartialView("ClassSetupPartialView", obj);
         //}

         //public string GetPreferredBookByClass(string pClassID, string pSubjectID)
         //{
         //    int mClassID = int.Parse(pClassID);
         //    int mSubjectID = int.Parse(pSubjectID);
         //    string mPreferredBook = string.Empty;
              
         //    ClassSyllabusMaster objmaster = new ClassSyllabusMaster();
         //    objmaster = unitOfWork.ClassSyllabusMasterService.GetClassSyllabusDataByClassIDANDSubjectID(mClassID, mSubjectID, byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()));
         //    if (objmaster != null)
         //    {
         //        mPreferredBook = objmaster.PreferredBooks;
         //    }

         //    return  mPreferredBook;
         //}


         public ActionResult CheckDuplicateMasterAction(string pExamID, string pClassID)
         {

            if (Session["UserID"] == null)
            {
                return Redirect("~/");
            }

             int mClassID = int.Parse(pClassID);
             int mExamID = int.Parse(pExamID);
             int RecordeFound = 0;
             int ExamSetupID = 0;
             DateTime? StartDate = null;
             DateTime? EndDate = null;
             int? ExamOrder = null;
             List<ExamSetupDetail> listexamsetup = new List<ExamSetupDetail>();

            try
            {
                ExamSetupMaster objFill = new ExamSetupMaster();
                objFill.ClassID = int.Parse(pClassID);
                objFill.ExamID = int.Parse(pExamID);
                objFill.CompID = byte.Parse(Session["CompID"].ToString());
                objFill.BranchID = byte.Parse(Session["BranchID"].ToString());
                objFill.SessionID = byte.Parse(Session["SessionID"].ToString());

                ExamSetupMaster objCheck = new ExamSetupMaster();
                objCheck = unitOfWork.examSetupMasterService.GetExamSetupMasterData(objFill);


                if (objCheck != null)
                {
                    RecordeFound = 1;

                    if (objCheck.StartDate.HasValue)
                        StartDate = objCheck.StartDate.Value;

                    if (objCheck.EndDate.HasValue)
                        StartDate = objCheck.EndDate.Value;

                    if (objCheck.ExamOrder.HasValue)
                        ExamOrder = objCheck.ExamOrder.Value;

                    ExamSetupID = objCheck.ExamSetupID;


                }

              

                listexamsetup = unitOfWork.examSetupDetailService.GetExamSetupDetailListByExamSetupID(ExamSetupID, byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()));

                if (listexamsetup == null) listexamsetup = new List<ExamSetupDetail>();


                ViewData["ExamID"] = pExamID;
                ViewData["ClassID"] = pClassID;
                ViewData["ExamSetupID"] = ExamSetupID;
            }
            catch(Exception ex)
            {

            }

           

            return new JsonResult
            {
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                Data = new
                {
                    recordFound = RecordeFound,
                    examfromDate = StartDate,
                    examtoDate = EndDate,
                    examOrder = ExamOrder,
                    ListDataExamSetup = cCommon.RenderRazorViewToString("ListExamSetup", listexamsetup, ControllerContext, ViewData, TempData)
                }
            };

         }


        public JsonResult GetStudentData(int mStudentID)
        {
            string ErrorMsg = string.Empty;

            var studentclass = string.Empty;
            var studentclassid = string.Empty;
            var studentenrollno = string.Empty;
            var studentmobileno = string.Empty;
            var studentfathername = string.Empty;
            var feestermid = string.Empty;
            var feestermname = string.Empty;

            StudentRegistration objstud = unitOfWork.studentRegistrationService.GetProfileInfo(mStudentID);
            if (objstud != null)
            {
                studentclass = objstud.ClassID.ToString();
                studentenrollno = objstud.EnrollmentNo;
                studentmobileno = objstud.SMSMobileNo;
                studentfathername = objstud.FatherName;

                StudentFeesMaster objstudfees = unitOfWork.StudentFeesMasterService.GetStudentFeesStructByStudentId(mStudentID, int.Parse(Session["SessionID"].ToString()), int.Parse(Session["CompID"].ToString()), int.Parse(Session["BranchID"].ToString()));
                if (objstudfees != null)
                {
                    if (objstudfees.StudentID != null)
                    {
                        feestermid = (objstudfees.TermID.ToString());
                        FeeTerm objterm = unitOfWork.feeTermService.GetFeeTermByTermID(int.Parse(feestermid));

                        if (objterm != null)
                        {
                            feestermname = objterm.FeeTermName;
                        }
                    }
                    if (objstudfees.StudentClassId != null)
                    {
                        studentclassid = objstudfees.StudentClassId.Value.ToString();
                    }


                }
            }

            return new JsonResult
            {
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                Data = new
                {
                    studentClass = studentclass,
                    studentclassid = studentclassid,
                    studentEnrollno = objstud.EnrollmentNo,
                    studentMobileno = objstud.SMSMobileNo,
                    studentFathername = objstud.FatherName,
                    feestermid = feestermid,
                    feestermname = feestermname
                }
            };

        }

        public ActionResult CreateOrUpdateExamSetUpint( int pClassID, int pExamID, DateTime? pStartDate, DateTime? pEndDate, int? pOrderID)
         {
             int ExamSetupID = 0;
             
             ExamSetupMaster objFill = new ExamSetupMaster();
             objFill.ClassID = pClassID;
             objFill.ExamID = pExamID;
            if(pOrderID.HasValue)
             objFill.ExamOrder = pOrderID.Value;
            if (pStartDate.HasValue)
                objFill.StartDate = pStartDate.Value;
            if (pEndDate.HasValue)
                objFill.EndDate = pEndDate.Value;
             objFill.CompID = byte.Parse(Session["CompID"].ToString());
             objFill.BranchID = byte.Parse(Session["BranchID"].ToString());
             objFill.SessionID = byte.Parse(Session["SessionID"].ToString());
             objFill.UIDAdd = byte.Parse(Session["UserID"].ToString());
             objFill.AddDate = DateTime.Now;




             ExamSetupMaster objCheck = new ExamSetupMaster();
             objCheck = unitOfWork.examSetupMasterService.GetExamSetupMasterData(objFill);
             if (objCheck==null)
             {
                 unitOfWork.examSetupMasterService.InsertExamSetupMaster(objFill);
                 unitOfWork.Save();
                 ExamSetupID = objFill.ExamSetupID;
             }
             else
             {
                 objCheck.UIDMod = byte.Parse(Session["UserID"].ToString());
                 objCheck.ModDate = DateTime.Now;

                 unitOfWork.examSetupMasterService.UpdateExamSetupMaster(objCheck);
                 unitOfWork.Save();

                 ExamSetupID = objCheck.ExamSetupID;
             }

             List<ExamSetupDetail> obj = new List<ExamSetupDetail>();
             if (ExamSetupID > 0)
             {
                 obj = unitOfWork.examSetupDetailService.GetExamSetupDetailListByExamSetupID(ExamSetupID, byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()));
                 if (obj.Count == 0)
                 {
                     List<SubjectAllotment> objSubAllot = new List<SubjectAllotment>();
                     objSubAllot = unitOfWork.examSetupDetailService.GetSubjectAllotmentListByClassID(pClassID, byte.Parse(Session["CompID"].ToString()),byte.Parse(Session["BranchID"].ToString()));
                     if (objSubAllot != null)
                     {
                        int Orderno=1;
                         foreach(SubjectAllotment objSubAllotDetail in objSubAllot)
                         {
                             ExamSetupDetail objDetail = new ExamSetupDetail();
                             objDetail.BranchID = byte.Parse(Session["BranchID"].ToString());
                             objDetail.CompID = byte.Parse(Session["CompID"].ToString());
                             objDetail.ExamSetupID = ExamSetupID;
                             objDetail.SessionId = byte.Parse(Session["SessionID"].ToString());
                             objDetail.OrderNo = Orderno;
                             objDetail.SubjectIDL1 = objSubAllotDetail.IDL1;
                             objDetail.SubjectIDL2 = objSubAllotDetail.IDL2;
                             objDetail.SubjectIDL3 = objSubAllotDetail.IDL3;

                             unitOfWork.examSetupDetailService.InsertExamSetupDetail(objDetail);
                             unitOfWork.Save();
                             Orderno++;
                         }
                       
                     }
                     obj = unitOfWork.examSetupDetailService.GetExamSetupDetailListByExamSetupID(ExamSetupID, byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()));
                 }
             }

            
             ViewData["ExamID"] = pExamID;
             ViewData["ClassID"] = pClassID;
             ViewData["ExamSetupID"] = ExamSetupID;


            return PartialView("ListExamSetup", obj);
         }


        public ActionResult UpdateMinMaxExamSetupDetail(int pClassID, int pExamID,  int pMaxmarks, int pMinmarks)
        {
            int ExamSetupID = 0;

            ExamSetupMaster objFill = new ExamSetupMaster();
            objFill.ClassID = pClassID;
            objFill.ExamID = pExamID;
            objFill.CompID = byte.Parse(Session["CompID"].ToString());
            objFill.BranchID = byte.Parse(Session["BranchID"].ToString());
            objFill.SessionID = byte.Parse(Session["SessionID"].ToString());
            objFill.UIDAdd = byte.Parse(Session["UserID"].ToString());
            objFill.AddDate = DateTime.Now;

            ExamSetupMaster objCheck = new ExamSetupMaster();
            objCheck = unitOfWork.examSetupMasterService.GetExamSetupMasterData(objFill);
            if (objCheck == null)
            {
                ExamSetupID = objFill.ExamSetupID;
            }
            else
            {
                ExamSetupID = objCheck.ExamSetupID;
            }

            List<ExamSetupDetail> obj = new List<ExamSetupDetail>();
            if (ExamSetupID > 0)
            {
                obj = unitOfWork.examSetupDetailService.GetExamSetupDetailListByExamSetupID(ExamSetupID, byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()));
                if (obj.Count > 0)
                {
                    List<SubjectAllotment> objSubAllot = new List<SubjectAllotment>();
                    objSubAllot = unitOfWork.examSetupDetailService.GetSubjectAllotmentListByClassID(pClassID, byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()));
                    if (objSubAllot != null)
                    {
                        int Orderno = 1;
                        foreach (SubjectAllotment objSubAllotDetail in objSubAllot)
                        {
                            ExamSetupDetail objDetail = new ExamSetupDetail();
                            objDetail.BranchID = objFill.BranchID;
                            objDetail.CompID = objFill.CompID;
                            objDetail.ExamSetupID = ExamSetupID;
                            objDetail.SessionId = objFill.SessionID;
                            objDetail.SubjectIDL1 = objSubAllotDetail.IDL1;
                            objDetail.SubjectIDL2 = objSubAllotDetail.IDL2;
                            objDetail.SubjectIDL3 = objSubAllotDetail.IDL3;
                            objDetail.MaxMark = pMaxmarks;
                            objDetail.MinMark = pMinmarks;

                            unitOfWork.examSetupDetailService.UpdateExamSetupMinMaxDetail(objDetail);
                            unitOfWork.Save();
                            Orderno++;
                        }

                    }
                    obj = unitOfWork.examSetupDetailService.GetExamSetupDetailListByExamSetupID(ExamSetupID, byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()));
                }
            }


            ViewData["ExamID"] = pExamID;
            ViewData["ClassID"] = pClassID;
            ViewData["ExamSetupID"] = ExamSetupID;


            return PartialView("ListExamSetup", obj);
        }

        public ActionResult GetExamSetupListForupdateData(string pClassID, string pExamID, DateTime pStartDate, DateTime pEndDate, string pOrderID)
         {
             int ExamSetupID = 0;
             int mClassID = int.Parse(pClassID);
             int mExamID = int.Parse(pExamID);
             int mOrderID = int.Parse(pOrderID);


             ExamSetupMaster objFill = new ExamSetupMaster();
             objFill.ClassID = int.Parse(pClassID);
             objFill.ExamID = int.Parse(pExamID);
             objFill.ExamOrder = int.Parse(pOrderID);
             objFill.StartDate = pStartDate;
             objFill.EndDate = pEndDate;
             objFill.CompID = byte.Parse(Session["CompID"].ToString());
             objFill.BranchID = byte.Parse(Session["BranchID"].ToString());
             objFill.SessionID = byte.Parse(Session["SessionID"].ToString());
             objFill.UIDAdd = byte.Parse(Session["UserID"].ToString());
             objFill.AddDate = DateTime.Now;

             ExamSetupMaster objCheck = new ExamSetupMaster();
             objCheck = unitOfWork.examSetupMasterService.GetExamSetupMasterData(objFill);
             if (objCheck == null)
             {
                 unitOfWork.examSetupMasterService.InsertExamSetupMaster(objFill);
                 unitOfWork.Save();
                 ExamSetupID = objFill.ExamSetupID;
             }
             else
             {
                 objCheck.UIDMod = byte.Parse(Session["UserID"].ToString());
                 objCheck.ModDate = DateTime.Now;

                 unitOfWork.examSetupMasterService.UpdateExamSetupMaster(objCheck);
                 unitOfWork.Save();

                 ExamSetupID = objCheck.ExamSetupID;
             }

             List<ExamSetupDetail> obj = new List<ExamSetupDetail>();
             if (ExamSetupID > 0)
             {
                 obj = unitOfWork.examSetupDetailService.GetExamSetupDetailListByExamSetupID(ExamSetupID, byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()));
                 if (obj.Count == 0)
                 {
                     List<SubjectAllotment> objSubAllot = new List<SubjectAllotment>();
                     objSubAllot = unitOfWork.examSetupDetailService.GetSubjectAllotmentListByClassID(mClassID, byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()));
                     if (objSubAllot != null)
                     {
                         foreach (SubjectAllotment objSubAllotDetail in objSubAllot)
                         {
                             ExamSetupDetail objDetail = new ExamSetupDetail();
                             objDetail.BranchID = byte.Parse(Session["BranchID"].ToString());
                             objDetail.CompID = byte.Parse(Session["CompID"].ToString());
                             objDetail.ExamSetupID = ExamSetupID;
                             objDetail.SessionId = byte.Parse(Session["SessionID"].ToString());
                             objDetail.SubjectIDL1 = objSubAllotDetail.IDL1;
                             objDetail.SubjectIDL2 = objSubAllotDetail.IDL2;
                             objDetail.SubjectIDL3 = objSubAllotDetail.IDL3;

                             unitOfWork.examSetupDetailService.InsertExamSetupDetail(objDetail);
                             unitOfWork.Save();
                         }
                     }
                     obj = unitOfWork.examSetupDetailService.GetExamSetupDetailListByExamSetupID(ExamSetupID, byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()));
                 }
                 else
                 {

                     foreach (ExamSetupDetail objdelete in obj)
                     {
                         unitOfWork.examSetupDetailService.Delete(objdelete.ExamSetupDetailID);
                         unitOfWork.Save();
                     }

                     obj = null;

                     List<SubjectAllotment> objSubAllot = new List<SubjectAllotment>();
                     objSubAllot = unitOfWork.examSetupDetailService.GetSubjectAllotmentListByClassID(mClassID, byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()));
                     if (objSubAllot != null)
                     {
                         foreach (SubjectAllotment objSubAllotDetail in objSubAllot)
                         {
                             ExamSetupDetail objDetail = new ExamSetupDetail();
                             objDetail.BranchID = byte.Parse(Session["BranchID"].ToString());
                             objDetail.CompID = byte.Parse(Session["CompID"].ToString());
                             objDetail.ExamSetupID = ExamSetupID;
                             objDetail.SessionId = byte.Parse(Session["SessionID"].ToString());
                             objDetail.SubjectIDL1 = objSubAllotDetail.IDL1;
                             objDetail.SubjectIDL2 = objSubAllotDetail.IDL2;
                             objDetail.SubjectIDL3 = objSubAllotDetail.IDL3;

                             unitOfWork.examSetupDetailService.InsertExamSetupDetail(objDetail);
                             unitOfWork.Save();
                         }
                     }
                     obj = unitOfWork.examSetupDetailService.GetExamSetupDetailListByExamSetupID(ExamSetupID, byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()));


                 }

             }

             ViewData["ExamSetupID"] = ExamSetupID;
             ViewData["StartDate_ForExamSetup"] = pStartDate;
             ViewData["EndDate_ForExamSetup"] = pEndDate;
             ViewData["ExamID_ForExamSetup"] = mExamID;
             ViewData["Order_ForExamSetup"] = mOrderID;
             ViewData["ClassID_ForExamSetup"] = mClassID;



             return PartialView("ListExamSetup",obj);
         }

        [ValidateInput(false)]
         public ActionResult updateExamSetupAll(MVCxGridViewBatchUpdateValues<ExamSetupDetail, int> updateValues, int pExamSetupID, int pClassID, int pExamID)
        {


            ViewData["ExamID"] = pExamID;
            ViewData["ClassID"] = pClassID;
            ViewData["ExamSetupID"] = pExamSetupID;

            if (pExamSetupID == 0)
            {
                return PartialView("ListExamSetup", new UnitOfWork().examSetupDetailService.GetExamSetupDetailListByExamSetupID(pExamSetupID, byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));
            }

          
            //foreach (var product in updateValues.Insert)
            //{
            //    if (updateValues.IsValid(product))
            //        InsertProduct(product, updateValues, pExamSetupID);
            //}
            foreach (var product in updateValues.Update)
            {
                if (updateValues.IsValid(product))
                    UpdateProduct(product, updateValues);
            }
            foreach (var productID in updateValues.DeleteKeys)
            {
                DeleteProduct(productID, updateValues);
            }

           List<ExamSetupDetail> objlist = new UnitOfWork().examSetupDetailService.GetExamSetupDetailListByExamSetupID(pExamSetupID, byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()));

            return PartialView("ListExamSetup", objlist);
        }

        protected void UpdateProduct(ExamSetupDetail product, MVCxGridViewBatchUpdateValues<ExamSetupDetail, int> updateValues)
        {
            _mConn = DB.GetActiveConnection();
            _mTran = _mConn.BeginTransaction(IsolationLevel.Snapshot);
            try
            {
                product.BranchID = byte.Parse(Session["BranchID"].ToString());
                product.CompID = byte.Parse(Session["CompID"].ToString());

                unitOfWork.examSetupDetailService.UpdateExamSetupDetail(product);
                unitOfWork.Save();
            }
            catch (Exception e)
            {
                updateValues.SetErrorText(product, e.Message);
            }
        }
        protected void DeleteProduct(int product, MVCxGridViewBatchUpdateValues<ExamSetupDetail, int> updateValues)
        {
            try
            {
                unitOfWork.examSetupDetailService.Delete(product);
                unitOfWork.Save();
            }
            catch (Exception e)
            {
                updateValues.SetErrorText(product, e.Message);
            }
        }
        protected void InsertProduct(ExamSetupDetail product, MVCxGridViewBatchUpdateValues<ExamSetupDetail, int> updateValues, int mExamSetupID)
        {
            try
            {

                product.BranchID = byte.Parse(Session["BranchID"].ToString());
                product.CompID = byte.Parse(Session["CompID"].ToString());
                product.ExamSetupID = mExamSetupID;
                unitOfWork.examSetupDetailService.InsertExamSetupDetail(product);
                unitOfWork.Save();

            }
            catch (Exception e)
            {
                updateValues.SetErrorText(product, e.Message);
            }
        }

    }


}



    