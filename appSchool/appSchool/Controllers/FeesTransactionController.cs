using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using appSchool.ViewModels;
using appSchool.Repositories;
using DevExpress.Web.Mvc;
using DevExpress.Web;
using Newtonsoft.Json;
using System.Web.SessionState;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace appSchool.Controllers
{
    [NoCache]
    public class FeesTransactionController : Controller
    {
        //
        // GET: /FeesStructure/


        UnitOfWork unitOfWork = new UnitOfWork();
        public ActionResult Index()
        {
            if (Session["UserID"] == null || (int)SubMenuModules.appFeesTransaction == 0)
            {
                return Redirect("~/");
            }


            UserPermission objuser = new UserPermission();
            objuser = unitOfWork.userPermissionService.CheckUserPermissionModulewise(int.Parse(Session["UserID"].ToString()), 50, byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()));
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

            modelStudentFeesReceipt objFeeReceipt = unitOfWork.feesReceiptService.GetFeesReceiptDefaultData(int.Parse(Session["UserID"].ToString()), int.Parse(Session["SessionID"].ToString()), byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()));
           
            return View("Index", objFeeReceipt);
        }

        public ActionResult GetAllTermForClasswise(int studentID)
        {
            if (Session["UserID"] == null) { return Redirect("~/"); }

            return PartialView("ListTermView", unitOfWork.feesReceiptService.GetTermListForFeesReceiptStudentWise(studentID, int.Parse(Session["SessionID"].ToString()), byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));
        }

        public ActionResult GetAllHeadForTermAndStudentWise(int studentID, int termID)
        {
            if (Session["UserID"] == null) { return Redirect("~/"); }

            return PartialView("ListHeadView", unitOfWork.StudentFeesDetailService.GetStudentFeesDetailByTermIDandStudentID(studentID, termID, int.Parse(Session["SessionID"].ToString()), byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));        
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
            if(objstud != null)
            {
                 studentclass = objstud.ClassID.ToString();
                 studentenrollno = objstud.EnrollmentNo;
                 studentmobileno = objstud.SMSMobileNo;
                 studentfathername = objstud.FatherName;

                StudentFeesMaster objstudfees = unitOfWork.StudentFeesMasterService.GetStudentFeesStructByStudentId(mStudentID, int.Parse(Session["SessionID"].ToString()), int.Parse(Session["CompID"].ToString()), int.Parse(Session["BranchID"].ToString()) );
                if(objstudfees != null)
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
                    if(objstudfees.StudentClassId != null)
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

        public JsonResult GetStudentFeesFooterData(int studentID, int termID)
        {
            string ErrorMsg = string.Empty;


             var feeTermTotal = 0.0 ;
            var feeFineAmount = 0.0 ;
            var feeOtherAmount = 0.0 ;
            var feeSubTotal = 0.0 ;
            var feeFinalAmount = 0.0 ;
            var feeDiscountAmount = 0.0 ;
            var feeDiscountPercent = 0.0 ;

            List<StudentFeesDetail>  liststudfees  = unitOfWork.StudentFeesDetailService.GetStudentFeesDetailByTermIDandStudentID(studentID, termID, int.Parse(Session["SessionID"].ToString()), byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()));

            var feeTermTotals = liststudfees.Select(x => x.HeadAmount).Sum();
            var feeSubTotals = liststudfees.Select(x => x.HeadAmount).Sum();
            var feeFinalAmounts = liststudfees.Select(x => x.HeadAmount).Sum();


            foreach (StudentFeesDetail objfees in liststudfees)
            {
               
            }

            return new JsonResult
            {
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                Data = new
                {
                     feeTermTotal = feeTermTotals,
                     feeFineAmount = feeFineAmount,
                     feeOtherAmount = feeOtherAmount,
                     feeSubTotal = feeSubTotals,
                     feeFinalAmount = feeFinalAmounts,
                     feeDiscountAmount = feeDiscountAmount,
                     feeDiscountPercent = feeDiscountPercent,
                }
            };

        }

        public string CheckDuplicateReciept(int mTermID, int mStudentID)
        {
            string error=string.Empty;
            StudentFeesMaster objFeeMaster = unitOfWork.StudentFeesMasterService.GetFeesDetailForStudentFeesReceipt(mStudentID, mTermID, int.Parse(Session["SessionID"].ToString()), byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()));
            if (objFeeMaster.PaidFlag == false)
            {
                return error="Not duplicate";
            }
            else
            {
                return error = "Paid"; ;
            }

        }

        public ActionResult GetStudentDataforFeeReceipt(int mTermID,int mStudentID)
        {
            if (Session["UserID"] == null) { return Redirect("~/"); }

            StudentFeesMaster objFeeMaster = unitOfWork.StudentFeesMasterService.GetFeesDetailForStudentFeesReceipt(mStudentID, mTermID, int.Parse(Session["SessionID"].ToString()), byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()));
            if (objFeeMaster.PaidFlag == false)
            {
                StudentRegistration obj = unitOfWork.studentRegistrationService.GetByID(mStudentID);

                modelStudentFeesReceipt objFeeReceipt = new modelStudentFeesReceipt();
                objFeeReceipt.StudentID = obj.StudentID;
                objFeeReceipt.EnrollmentNo = obj.EnrollmentNo;
                objFeeReceipt.StudentName = obj.FirstName + " " + obj.LastName;
                objFeeReceipt.FatherName = obj.FatherName;
                objFeeReceipt.MobileNo = obj.SMSMobileNo;
                objFeeReceipt.ClassID = int.Parse(obj.ClassID.ToString());
                objFeeReceipt.Class = unitOfWork.studentSessionService.GetClassDescriptionByStudentIDandClassIDandSessionID(mStudentID, int.Parse(objFeeMaster.StudentClassId.ToString()), int.Parse(Session["SessionID"].ToString()), byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()));

                FeeTerm objterm = new FeeTerm();
                objterm = unitOfWork.feeTermService.GetByID(mTermID);

                if(objterm != null)
                {
                    objFeeReceipt.TermName = objterm.FeeTermType;
                    objFeeReceipt.TermId = mTermID;
                    objFeeReceipt.TermTotal = decimal.Parse(objFeeMaster.Amount.ToString());
                    if (objterm.FeeTermToDate.Date < DateTime.Now.Date)
                    {
                        objFeeReceipt.FineAmount = 100;
                    }
                    else
                    {
                        objFeeReceipt.FineAmount = 0;
                    }
                }
                
                objFeeReceipt.ReceiptNo = unitOfWork.feesReceiptService.GetReceiptNoFromSetup(int.Parse(Session["SessionID"].ToString()), byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()));
                objFeeReceipt.ReceiptDate = DateTime.Now;
                objFeeReceipt.DiscountAmount = 0;
                objFeeReceipt.DiscountPercent = 0;
                objFeeReceipt.OtherAmount = 0;
                objFeeReceipt.SubTotal = 0;
                objFeeReceipt.SubTotal = objFeeReceipt.TermTotal + objFeeReceipt.FineAmount + objFeeReceipt.OtherAmount;
                objFeeReceipt.FinalAmount = objFeeReceipt.SubTotal - objFeeReceipt.DiscountAmount;

                return PartialView("FeesTransactionView", objFeeReceipt);
            }
            else
            {
                modelStudentFeesReceipt objFeeReceipt = new modelStudentFeesReceipt();
                return PartialView("FeesTransactionView", objFeeReceipt);
            }
                      
        }

        [HttpPost]
        public ActionResult SaveFeesReceipt(modelStudentFeesReceipt obj)
        {
            if (Session["UserID"] == null) { return Redirect("~/"); }
            
            if (ModelState.IsValid)
            {
                try
                {

                    bool isDuplicate = false;
                    isDuplicate = unitOfWork.feeCollectionMasterService.CheckDuplicateData(obj.StudentID, obj.TermId, int.Parse(Session["SessionID"].ToString()), byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()));

                    if (isDuplicate == false)
                    {
                        FeesCollectionMaster objFeesCollMaster = new FeesCollectionMaster();

                        objFeesCollMaster.ReceiptNo = obj.ReceiptNo;
                        objFeesCollMaster.ReceiptDate = obj.ReceiptDate;
                        objFeesCollMaster.StudentID = obj.StudentID;
                        objFeesCollMaster.ClassID = obj.ClassID;
                        objFeesCollMaster.TermIds = obj.TermId.ToString();
                        objFeesCollMaster.Mode = obj.Mode;
                        objFeesCollMaster.BankName = obj.BankName;
                        objFeesCollMaster.BranchName = obj.BranchName;
                        objFeesCollMaster.ChequeNo = obj.ChequeNo;
                        objFeesCollMaster.ChequeDate = obj.ChequeDate;
                        objFeesCollMaster.Remark = obj.Remark;
                        objFeesCollMaster.FinalTotal = obj.FinalAmount;
                        objFeesCollMaster.FineAmount = obj.FineAmount;
                        objFeesCollMaster.OtherAmount = obj.OtherAmount;
                        objFeesCollMaster.TermTotal = obj.TermTotal;
                        objFeesCollMaster.PaidAmount = obj.FinalAmount;
                        objFeesCollMaster.DiscountType = obj.DiscountType;
                        objFeesCollMaster.DiscountPercent = obj.DiscountPercent;
                        objFeesCollMaster.DiscountAmount = obj.DiscountAmount;

                        objFeesCollMaster.SessionID = byte.Parse(Session["SessionID"].ToString());
                        objFeesCollMaster.CompID = byte.Parse(Session["CompID"].ToString());
                        objFeesCollMaster.BranchID = byte.Parse(Session["BranchID"].ToString());
                        objFeesCollMaster.UIDAdd = byte.Parse(Session["UserID"].ToString());
                        objFeesCollMaster.AddDate = DateTime.Now;

                        unitOfWork.feeCollectionMasterService.Insert(objFeesCollMaster);
                        unitOfWork.Save();

                        List<StudentFeesDetail> objListStudeFeeDetail = new List<StudentFeesDetail>();
                        objListStudeFeeDetail = unitOfWork.StudentFeesDetailService.GetStudentFeesDetailByTermIDandStudentID(int.Parse(objFeesCollMaster.StudentID.ToString()), int.Parse(objFeesCollMaster.TermID.ToString()), int.Parse(Session["SessionID"].ToString()), byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()));

                        foreach (StudentFeesDetail objdetail in objListStudeFeeDetail)
                        {
                            FeesCollectionDetail objFeeCollDetail = new FeesCollectionDetail();
                            objFeeCollDetail.ReceiptNo = objFeesCollMaster.ReceiptNo;
                            objFeeCollDetail.SessionID = byte.Parse(Session["SessionID"].ToString());
                            objFeeCollDetail.Amount = objdetail.HeadAmount;
                            objFeeCollDetail.FeesHeadID = objdetail.HeadID;
                            objFeeCollDetail.FeeTermID = objdetail.TermID;
                            objFeeCollDetail.CompID = byte.Parse(Session["CompID"].ToString());
                            objFeeCollDetail.BranchID = byte.Parse(Session["BranchID"].ToString());

                            unitOfWork.feeCollectionDetailService.Insert(objFeeCollDetail);
                            unitOfWork.Save();

                        }

                        long mReceiptNo = objFeesCollMaster.ReceiptNo + 1;

                        unitOfWork.setupService.UpdateReceiptNo(int.Parse(objFeesCollMaster.SessionID.ToString()), mReceiptNo, byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()));
                        unitOfWork.Save();
                        decimal PaidAmount = decimal.Parse(obj.FinalAmount.ToString());
                        unitOfWork.StudentFeesMasterService.UpdateFeesPaidFlagINStudentFeesMaster(int.Parse(objFeesCollMaster.StudentID.ToString()), int.Parse(objFeesCollMaster.TermID.ToString()), PaidAmount, int.Parse(objFeesCollMaster.SessionID.ToString()), byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()));
                        unitOfWork.Save();

                    }
                    else
                    {
                        ViewData["Mesage"] ="Data allready Exist";
                    }

                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            modelStudentFeesReceipt objnext = new modelStudentFeesReceipt();
            return PartialView("FeesTransactionEditView", objnext);
        }



        #region AddFeesTransactions

      
        public string GetClassDescription(string mClassSetupID)
        {

            string[] bits = mClassSetupID.Split(',');
            string mNewClassID = string.Empty;
            string ErrorMsg = string.Empty;
            string dupclass=string.Empty;
            foreach (string bit in bits)
            {
                int mFeesStructID = unitOfWork.feesStructureMasterService.GetClassExist(int.Parse(bit), int.Parse(Session["SessionID"].ToString()), byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()));
                string className = string.Empty;
                int classIDforname = int.Parse(bit);
                className = unitOfWork.classSetupService.GetClassNameByClassID(classIDforname);
                if (dupclass == string.Empty)
                    dupclass = className;
                else
                    dupclass = dupclass + "," + className;
                if (mFeesStructID > 0)
                {
                    ViewData["EditError"] = "Class " + dupclass + " All Ready Exist";
                    ErrorMsg = "Class " + dupclass + " All Ready Exist";
                }
                else
                {
                    if (mNewClassID == string.Empty)
                        mNewClassID = bit;
                    else
                        mNewClassID = mNewClassID + "," + bit;
                   // ErrorMsg = dupclass;
                }
            }
            ViewData["FeesClasses"] = mNewClassID;


           // return PartialView("ListAllFeesTerms", (new UnitOfWork().feesHeadService.GetTermHeadForFeeStructure()));

            return ErrorMsg;
        }
        public ActionResult GetAllStudentListView(string mClassesID)
        {
            int newclassID = int.Parse(mClassesID);
            ViewData["ClassSetupIDForFee"] = newclassID;
            return PartialView("GridStudentListForFee", new UnitOfWork().studentSessionService.GetStudentForFeeClasswise(newclassID, int.Parse(Session["SessionID"].ToString()), byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));
        }

        public ActionResult AllFeesTermTransaction(int RegID)
        {

            modelFeeTransaction obj = new modelFeeTransaction();

            modelFeesStructureDetail objdetail = new modelFeesStructureDetail();
            objdetail.FeeStructSessionID = 1;
            objdetail.FeeStructID = 1;
            objdetail.FeeAmount = 100;
            objdetail.FeeStructDetailID = 1;

            List<modelFeesStructureDetail> objldetail = new List<modelFeesStructureDetail>();
            objldetail.Add(objdetail);

            obj.feesStructDetail=objldetail;


            return PartialView("FeesTransactionView",obj);
        }

        public ActionResult PartialGridAllStudentForFee()
        {


            return PartialView("GridStudentListForFee", new UnitOfWork().studentSessionService.GetAllStudentForSession(int.Parse(Session["SessionID"].ToString()), byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));
        }

        public ActionResult ListPartialStudentList()
        {

            return PartialView("ListStudentGridLookupPartial", new UnitOfWork().studentSessionService.GetAllStudentForSessionNameWise(int.Parse(Session["SessionID"].ToString()), byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));
        }


        [HttpPost]
        public JsonResult SubmitFees(modelStudentFeesReceipt objpostfees)
        {

           //if (Session["UserID"] == null) { return Redirect("~/"); }

            bool result = false;
            string Errormsg = string.Empty;
            SettingMasterStaticClass._SaveSMSLog = true;

            try
            {


                string Errormsgreturn = "";
                FeesPayment objfees = new FeesPayment();

                result = objfees.AddNewFeesReceipt(objpostfees, ref Errormsgreturn);
                if (result)
                {
                    Errormsg = "Fees submit successfully , Receipt no" + Errormsgreturn;

                    if(objpostfees.ISSendSMS)
                    SendSms(false, objpostfees);
                }
                else
                    Errormsg = "Error to submit fees";
                
            }
            catch(Exception ex)
            {
                 Errormsg = "Error :" + ex.Message.ToString();

            }
            return new JsonResult()
            {
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                    Data = new
                    {
                        Resultmsg = Errormsg,
                        ResultData = RenderRazorViewToString("FeesTransactionView", objpostfees, ControllerContext, ViewData, TempData)
                    }
            };
        }

        private void SendSms(bool IsSmsinHindi, modelStudentFeesReceipt objpostfees)
        {
            string smsTemplate = string.Empty;
            DataRow dr = DB.ExecuteSingleRow("select top(1) TemplateMessage,TemplateMessageH from dbo.SMSTemplate where TemplateName='Fees Receipt' and  CompID " + objpostfees.CompID + " and  BranchID " + objpostfees.BranchID );
            if (dr != null)
            {
                if (IsSmsinHindi == false)
                    smsTemplate = dr[0].ToString();
                else
                    smsTemplate = dr[1].ToString();
                smsTemplate = smsTemplate.Replace("$Name$", objpostfees.StudentName);
                smsTemplate = smsTemplate.Replace("$Class$", objpostfees.Class);
                smsTemplate = smsTemplate.Replace("$Term$", objpostfees.TermName);
                smsTemplate = smsTemplate.Replace("$ReceiptNo$", objpostfees.ReceiptNo.ToString());
            }

            if (smsTemplate != string.Empty && objpostfees.MobileNo != string.Empty)
            {
               
                switch (objpostfees.CompID)
                {
                    case 1:

                        if (IsSmsinHindi == false)
                            unitOfWork.sendMessegeService.sendSmsNirmala(smsTemplate, objpostfees.MobileNo);
                        else
                            unitOfWork.sendMessegeService.sendSmsUnicodeNirmala(ConvertUnicodeStringToHexString(smsTemplate), objpostfees.MobileNo);
                        break;
                    case 2:
                        if (IsSmsinHindi == false)
                            unitOfWork.sendMessegeService.sendSmsStMark(smsTemplate, objpostfees.MobileNo);
                        else
                            unitOfWork.sendMessegeService.sendSmsUnicodeStMark(ConvertUnicodeStringToHexString(smsTemplate), objpostfees.MobileNo);
                        break;
                    case 4:
                        if (IsSmsinHindi == false)
                            unitOfWork.sendMessegeService.sendSmsStXviear(smsTemplate, objpostfees.MobileNo);
                        else
                            unitOfWork.sendMessegeService.sendSmsUnicodesendSmsStXviear(ConvertUnicodeStringToHexString(smsTemplate), objpostfees.MobileNo);
                        break;
                    case 5:
                        if (IsSmsinHindi == false)
                            unitOfWork.sendMessegeService.sendSmsStjoseph(smsTemplate, objpostfees.MobileNo);
                        else
                            unitOfWork.sendMessegeService.sendSmsUnicodesendSmsjoseph(ConvertUnicodeStringToHexString(smsTemplate), objpostfees.MobileNo);
                        break;
                    case 6:
                        if (IsSmsinHindi == false)
                            unitOfWork.sendMessegeService.sendSmsStlawrence(smsTemplate, objpostfees.MobileNo);
                        else
                            unitOfWork.sendMessegeService.sendSmsUnicodesendSmsStlawrence(ConvertUnicodeStringToHexString(smsTemplate), objpostfees.MobileNo);
                        break;

                }
            }
        }

        public string ConvertUnicodeStringToHexString(string unicodeString)
        {
            string hex = "";
            foreach (char c in unicodeString)
            {
                int tmp = c;
                hex += String.Format("{0:x4}", (uint)System.Convert.ToUInt32(tmp.ToString()));
            }
            return hex;
        }
        public ActionResult PartialFeeTermView(VFeesCompulsory obj, string mClasses)
        {

            return PartialView("ListAllFeesTerms", new UnitOfWork().feesHeadService.GetTermHeadForFeeStructure(mClasses, byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()), byte.Parse(Session["SessionID"].ToString())));
        }
        [ValidateInput(false)]
        public ActionResult updateFeesStructure(MVCxGridViewBatchUpdateValues<VFeesCompulsory, int> updateValues, string mClasses)
        {
            if (Session["SessionID"] == null)
            {

                return Redirect("~/");
            }

            if (mClasses == string.Empty || mClasses == "")
            {
              return PartialView("AddFeesStructure");
            }


            try
            {
                string[] bits = mClasses.Split(',');
                foreach (string bit in bits)
                {
                    FeesStructureMaster objSM = new FeesStructureMaster();
                    objSM.FeeStructSessionID = int.Parse(Session["SessionID"].ToString());
                    objSM.CompID = byte.Parse(Session["CompID"].ToString());
                    objSM.BranchID = byte.Parse(Session["BranchID"].ToString());
                    objSM.UIDAdd = byte.Parse(Session["UserID"].ToString());
                    objSM.AddDate = DateTime.Now;
                    objSM.FeesStructClassID = int.Parse(bit);

                    unitOfWork.feesStructureMasterService.Insert(objSM);
                    unitOfWork.Save();

                    int mFeesStructID = unitOfWork.feesStructureMasterService.GetFeesStructID(objSM);

                    decimal FeesAmount = 0;
       
                    foreach (var product in updateValues.Insert)
                    {
                        if (updateValues.IsValid(product))
                        {
                            FeesAmount += decimal.Parse(product.DefaultAmount.ToString());
                            InsertProduct(product, updateValues, mFeesStructID, int.Parse(bit), int.Parse(Session["SessionID"].ToString()));

                        }
                    }
                    foreach (var product in updateValues.Update)
                    {

                        if (updateValues.IsValid(product))
                        {
                            FeesAmount += decimal.Parse(product.DefaultAmount.ToString());
                            InsertProduct(product, updateValues, mFeesStructID, int.Parse(bit), int.Parse(Session["SessionID"].ToString()));
                        }
                    }

                    objSM.FeeStructAmount = FeesAmount;
                    objSM.FeeStructID = mFeesStructID;
                    unitOfWork.feesStructureMasterService.Update(objSM);
                    unitOfWork.Save();


                }
            }
            catch (Exception e)
            {

            }
            return PartialView("AddFeesStructure");
        }
        protected void InsertProduct(VFeesCompulsory product, MVCxGridViewBatchUpdateValues<VFeesCompulsory, int> updateValues,int mFeesStructID,int mFeesClassID,int mStructSessionID)
        {
            try
            {

                unitOfWork.feesStructureDetailService.InsertProduct(product,mFeesStructID,mFeesClassID,mStructSessionID,byte.Parse(Session["CompID"].ToString()),byte.Parse(Session["BranchID"].ToString()));
                unitOfWork.Save();
            }
            catch (Exception e)
            {
                updateValues.SetErrorText(product, e.Message);
            }
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

        public JsonResult GetStudentFeesData(int receiptno)
        {
            string ErrorMsg = string.Empty;


            modelStudentFeesReceipt modelStudentFeesReceipt = new modelStudentFeesReceipt();
            var objmodelStudentFeesReceipt = modelStudentFeesReceipt;


            FeesCollectionMaster objfeescollection = unitOfWork.feeCollectionMasterService.GetFeesCollectionByReceiptNo(receiptno,  int.Parse(Session["SessionID"].ToString()), int.Parse(Session["CompID"].ToString()), int.Parse(Session["BranchID"].ToString()));

            if (objfeescollection == null)
                ErrorMsg = "404";
            else
            { 

                    StudentRegistration objstud = unitOfWork.studentRegistrationService.GetProfileInfo(objfeescollection.StudentID.Value);


                    objmodelStudentFeesReceipt.ReceiptNo = objfeescollection.ReceiptNo;
                    objmodelStudentFeesReceipt.ReceiptDate = objfeescollection.ReceiptDate;
                    objmodelStudentFeesReceipt.StudentID = objfeescollection.StudentID.Value;
                    objmodelStudentFeesReceipt.EnrollmentNo = objstud.EnrollmentNo;
                    objmodelStudentFeesReceipt.StudentName = objstud.FirstName + objstud.LastName;
                    objmodelStudentFeesReceipt.FatherName = objstud.FatherName;
                    objmodelStudentFeesReceipt.MobileNo = objstud.SMSMobileNo;
                    // objmodelStudentFeesReceipt.Class = objfeescollection.ReceiptDate;
                    objmodelStudentFeesReceipt.ClassID = objfeescollection.ClassID.Value;
                    objmodelStudentFeesReceipt.ScholarNo = "";
                    if(objfeescollection.TermID.HasValue)
                    objmodelStudentFeesReceipt.TermId = objfeescollection.TermID.Value;
                    objmodelStudentFeesReceipt.TermName = objfeescollection.TermIds;
                    objmodelStudentFeesReceipt.TermTotal = objfeescollection.TermTotal;
                    objmodelStudentFeesReceipt.OtherAmount = objfeescollection.OtherAmount;
                    objmodelStudentFeesReceipt.SubTotal = objfeescollection.FinalTotal;
                    objmodelStudentFeesReceipt.FinalAmount = objfeescollection.FinalTotal;
                    objmodelStudentFeesReceipt.DiscountAmount = objfeescollection.DiscountAmount;
                    objmodelStudentFeesReceipt.DiscountPercent = objfeescollection.DiscountPercent;
                    objmodelStudentFeesReceipt.PaidAmount = objfeescollection.PaidAmount;
                    objmodelStudentFeesReceipt.DueAmount = objfeescollection.DueAmount;
                    //objmodelStudentFeesReceipt.DueTermId = objfeescollection.ReceiptDate;
                    //objmodelStudentFeesReceipt.OtherHead = objfeescollection.OtherHead;
                    objmodelStudentFeesReceipt.BankCode = objfeescollection.BankCode;
                    objmodelStudentFeesReceipt.Mode = objfeescollection.Mode;

                    objmodelStudentFeesReceipt.PayMode = objfeescollection.CounterType;
                    objmodelStudentFeesReceipt.DiscountType = objfeescollection.DiscountType;
                    objmodelStudentFeesReceipt.BankName = objfeescollection.BankName;
                    objmodelStudentFeesReceipt.BranchName = objfeescollection.BranchName;
                    objmodelStudentFeesReceipt.ChequeNo = objfeescollection.ChequeNo;
                    objmodelStudentFeesReceipt.ChequeDate = objfeescollection.ChequeDate;
                    objmodelStudentFeesReceipt.Remark = objfeescollection.Remark;
                    objmodelStudentFeesReceipt.CompID = objfeescollection.CompID;
                    objmodelStudentFeesReceipt.BranchID = objfeescollection.BranchID;
                    objmodelStudentFeesReceipt.SessionID = objfeescollection.SessionID;
                    objmodelStudentFeesReceipt.USerID = objfeescollection.UIDAdd.Value;

            }


            return new JsonResult
            {
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                Data = new
                {
                    //objmodelStudentFeesReceipt = objmodelStudentFeesReceipt,
                   
                        Resultmsg = ErrorMsg,
                        ResultData = RenderRazorViewToString("FeesTransactionEditView", objmodelStudentFeesReceipt, ControllerContext, ViewData, TempData)
                    
                }
            };

        }


        public JsonResult DeleteFeesReceipt(int receiptno)
        {
            bool result = false;
            string ErrorMsg = string.Empty;


            int sessionID = int.Parse(Session["SessionID"].ToString());
            int userID = int.Parse(Session["UserID"].ToString());

            FeesPayment objfees = new FeesPayment();

            result = objfees.DeleteFeesReceipt(receiptno, sessionID, userID);
            if (result)
                ErrorMsg = "Fees delete successfully , Receipt no" + receiptno;
            else
                ErrorMsg = "Error to delete fees"; 

            

            return new JsonResult
            {
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                Data = new
                {

                    errorMsg = ErrorMsg,
                   
                }
            };

        }

        


        #endregion



    }


    public class FeesPayment
    {

        private SqlConnection _mConn;
        private SqlTransaction _mTran;
        private string _ErrorMessage;

        public FeesPayment()
        {

        }

        public bool AddNewFeesReceipt(modelStudentFeesReceipt objfees, ref string Errormsgreturn)
        {
            bool res = false;

            
            _mConn = new SqlConnection(DB.GetConnectionString());
            Errormsgreturn = "";

            try
            {
                _mConn.Open();
                _mTran = _mConn.BeginTransaction(IsolationLevel.Snapshot);

                res = SaveMasterInfo(objfees, ref Errormsgreturn);
                //for (int i = 0; i <= _Items.Count - 1; i++)
                //{
                //    _Items[i].ReceiptNo = _ReceiptNo;
                //}
                //for (int i = 0; i <= _ItemsTerm.Count - 1; i++)
                //{
                //    _ItemsTerm[i].ReceiptNo = _ReceiptNo;
                //}
                if (res)
                {
                    _mTran.Commit();
                }
                else
                    _mTran.Rollback();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                _mConn.Dispose();
            }

            return res;
        }

        public bool SaveMasterInfo(modelStudentFeesReceipt objfees, ref string Errormsgreturn)
        {
            bool res = false;
            int rec = 0;
            Errormsgreturn = "";
            try
            {
                SqlCommand cmd = new SqlCommand("[ADD_FeesReceipt]", _mConn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Transaction = _mTran;
                SqlParameter Para1 = cmd.Parameters.Add("@ReceiptNo", SqlDbType.BigInt);
                Para1.Direction = ParameterDirection.InputOutput;
                Para1.Value = objfees.ReceiptNo;
                cmd.Parameters.Add("@SessionID", SqlDbType.TinyInt).Value = objfees.SessionID;
                cmd.Parameters.Add("@ReceiptDate", SqlDbType.SmallDateTime).Value = objfees.ReceiptDate;
                cmd.Parameters.Add("@StudentID", SqlDbType.Int).Value = objfees.StudentID;
                cmd.Parameters.Add("@ClassId", SqlDbType.Int).Value = objfees.ClassID;
                cmd.Parameters.Add("@TermID", SqlDbType.Int).Value = objfees.TermId;
                cmd.Parameters.Add("@CounterType", SqlDbType.VarChar).Value = objfees.PayMode;
                cmd.Parameters.Add("@Mode", SqlDbType.VarChar).Value = objfees.Mode;
                
                if (objfees.BankName != null)
                    cmd.Parameters.Add("@BankName", SqlDbType.VarChar).Value = objfees.BankName;
                else
                cmd.Parameters.Add("@BankName", SqlDbType.VarChar).Value = DBNull.Value;

                if (objfees.BranchName != null)
                    cmd.Parameters.Add("@BranchName", SqlDbType.VarChar).Value = objfees.BranchName;
                else
                    cmd.Parameters.Add("@BranchName", SqlDbType.VarChar).Value = DBNull.Value;

                if (objfees.BankCode != null)
                    cmd.Parameters.Add("@BankCode", SqlDbType.VarChar).Value = objfees.BankCode;
                else
                    cmd.Parameters.Add("@BankCode", SqlDbType.Int).Value = objfees.BankCode;


                if (objfees.ChequeNo != null)
                cmd.Parameters.Add("@ChequeNo", SqlDbType.VarChar).Value = objfees.ChequeNo;
                else
                    cmd.Parameters.Add("@ChequeNo", SqlDbType.SmallDateTime).Value = DBNull.Value;


                if (objfees.ChequeDate != null)
                    cmd.Parameters.Add("@ChequeDate", SqlDbType.SmallDateTime).Value = objfees.ChequeDate;
                else
                    cmd.Parameters.Add("@ChequeDate", SqlDbType.SmallDateTime).Value = DBNull.Value;

                if (objfees.Remark != null)
                    cmd.Parameters.Add("@Remark", SqlDbType.VarChar).Value = objfees.Remark;
                else
                    cmd.Parameters.Add("@Remark", SqlDbType.VarChar).Value =  DBNull.Value;

                cmd.Parameters.Add("@FinalTotal", SqlDbType.Decimal).Value = objfees.FinalAmount;
                cmd.Parameters.Add("@TermTotal", SqlDbType.Decimal).Value = objfees.TermTotal;
                cmd.Parameters.Add("@FineAmount", SqlDbType.Decimal).Value = objfees.FineAmount;
                cmd.Parameters.Add("@OtherAmount", SqlDbType.Decimal).Value = objfees.OtherAmount;
                cmd.Parameters.Add("@DiscountAmount", SqlDbType.Decimal).Value = objfees.DiscountAmount;
                if(objfees.DiscountType != null)
                cmd.Parameters.Add("@DiscountType", SqlDbType.VarChar).Value = objfees.DiscountType;
                else
                cmd.Parameters.Add("@DiscountType", SqlDbType.VarChar).Value =DBNull.Value;
                cmd.Parameters.Add("@DiscountPercent", SqlDbType.Decimal).Value = objfees.DiscountPercent;
                cmd.Parameters.Add("@UIdAdd", SqlDbType.TinyInt).Value = objfees.USerID;
                cmd.Parameters.Add("@TermIds", SqlDbType.VarChar).Value = objfees.TermId;
                cmd.Parameters.Add("@PaidAmount", SqlDbType.Decimal).Value = objfees.PaidAmount;
                cmd.Parameters.Add("@DueAmount", SqlDbType.Decimal).Value = objfees.DueAmount;
                cmd.Parameters.Add("@DueTermId", SqlDbType.VarChar).Value = objfees.DueTermId;
                cmd.Parameters.Add("@OtherHead", SqlDbType.VarChar).Value = objfees.OtherHead;
            
                cmd.Parameters.Add("@CompID", SqlDbType.TinyInt).Value = objfees.CompID;
                cmd.Parameters.Add("@BranchID", SqlDbType.TinyInt).Value = objfees.BranchID;

                int mNewValue = -1;
                rec = cmd.ExecuteNonQuery();
                int.TryParse(Para1.Value.ToString(), out mNewValue);
                if (mNewValue == -99)
                {
                    _ErrorMessage = objfees.ReceiptNo.ToString() + " is Duplicate in the database";
                    Errormsgreturn = _ErrorMessage;
                    res = false;
                }
                else
                {
                    objfees.ReceiptNo = mNewValue;
                    Errormsgreturn =  objfees.ReceiptNo.ToString();
                    res = true;
                }

            }
            catch (Exception ex)
            {

                Errormsgreturn  = "error " + ex.Message; 
                //throw ex;
            }
            return res;

        }

        public bool DeleteFeesReceipt(int receiptNo, int sessionID, int userID)
        {
            bool res = false;
            _mConn = new SqlConnection(DB.GetConnectionString());
            try
            {
                _mConn.Open();
                SqlCommand cmd = new SqlCommand("Delete_FeesReceipt", _mConn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Transaction = _mTran;
                cmd.Parameters.Add("@ReceiptNo", SqlDbType.BigInt).Value = receiptNo;
                cmd.Parameters.Add("@SessionID", SqlDbType.TinyInt).Value = sessionID;
                cmd.Parameters.Add("@UIdMod", SqlDbType.TinyInt).Value = userID;
               int rc = cmd.ExecuteNonQuery();

                if (receiptNo > 0) res = true;
            }
            catch(Exception ex)
            {
                res = false;
            }
            finally
            {
                _mConn.Dispose();
            }

            return res;
        }

    }
}
