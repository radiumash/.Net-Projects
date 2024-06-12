using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using appSchool.ViewModels;
using appSchool.Repositories;
using DevExpress.Web.Mvc;
using DevExpress.Web;
using System.Web.SessionState;

namespace appSchool.Controllers
{
        [NoCache]
    public class StudentListFeesStructureController : Controller
    {
        

        UnitOfWork unitOfWork = new UnitOfWork();
        public ActionResult Index()
        {
            if (Session["UserID"] == null)
            {


                return Redirect("~/");
            }

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

      


        #region ListStudentFeesStructure

        public ActionResult StudentListFeesStructure ()
        {
            if (Session["UserID"] == null)
            {
                return Redirect("~/");
            }

            return PartialView("StudentListFeesStructure");
        }


        public ActionResult PartialGridListFeesStructure()
        {
            if (Session["UserID"] == null) { return Redirect("~/"); }
            return PartialView("GridStudentList", new UnitOfWork().vstudentListFromStudFeesStructureService.GetStudentListFromFeesStructure(int.Parse(Session["SessionID"].ToString()),byte.Parse(Session["CompID"].ToString()),byte.Parse(Session["BranchID"].ToString())));
        }




        public ActionResult GetFeesTermForListEdit(int newStudentID)
        {
            if (Session["UserID"] == null) { return Redirect("~/"); }

            int sessionID = int.Parse(Session["SessionID"].ToString());

            ViewData["FeesStructStudentID"] = newStudentID;
            return PartialView("GridFeesTermList", new UnitOfWork().vstudentListFromStudFeesStructureService.GetTermListByStudentID(newStudentID, sessionID, byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));
        }

        public ActionResult PartialGridFeesTerm(int newStudentID)
        {
            if (Session["UserID"] == null) { return Redirect("~/"); }
            ViewData["FeesStructStudentID"] = newStudentID;

            return PartialView("GridFeesTermList", unitOfWork.vstudentListFromStudFeesStructureService.GetTermListByStudentID(newStudentID, int.Parse(Session["SessionID"].ToString()), byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));
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


        public ActionResult GetFeesHeadForListEdit(int studentID, int termID, int calssID)
        {
            if (Session["UserID"] == null) { return Redirect("~/"); }
            int sessionID = int.Parse(Session["SessionID"].ToString());

            ViewData["StudentID"] = studentID;
            ViewData["TermID"] = termID;
            ViewData["ClassID"] = calssID;
            return PartialView("ListFeesStructureForEdit", unitOfWork.StudentFeesDetailService.GetStudentFeesDetailByStudentIDAndTermID(studentID, termID, int.Parse(Session["SessionID"].ToString()) , int.Parse(Session["CompID"].ToString()) , int.Parse(Session["BranchID"].ToString())));
        }

        public ActionResult PartialGridFeesHead(int studentID, int termID , int calssID)
        {
            if (Session["UserID"] == null) { return Redirect("~/"); }


            ViewData["StudentID"] = studentID;
            ViewData["TermID"] = termID;
            ViewData["ClassID"] = calssID;
            return PartialView("ListFeesStructureForEdit", unitOfWork.StudentFeesDetailService.GetStudentFeesDetailByStudentIDAndTermID(studentID, termID, int.Parse(Session["SessionID"].ToString()), int.Parse(Session["CompID"].ToString()), int.Parse(Session["BranchID"].ToString())));
        }



        public ActionResult UpdateFeesStructureEditOld(MVCxGridViewBatchUpdateValues<StudentFeesDetail, int> updateValues, int studentID, int termID)
        {
            if (Session["UserID"] == null) { return Redirect("~/"); }

            int mFeesStructID = 0;
            try
            {


                        //int mFeesStructID = ;

                        foreach (var product in updateValues.Insert)
                        {
                            if (updateValues.IsValid(product))
                                InsertStructure(product, updateValues,mFeesStructID);
                        }
                        foreach (var product in updateValues.Update)
                        {
                            //if (updateValues.IsValid(product))
                            //    UpdateStructure(product, updateValues);
                        }
                        foreach (var productID in updateValues.DeleteKeys)
                        {
                            DeleteStructure(productID, updateValues);
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
            return PartialView("ListFeesStructureForEdit",unitOfWork.feesStructureDetailService.GetFeeStructureDetailbyFeeStructID(mFeesStructID));
        }

        public ActionResult UpdateFeesStructureEdit(MVCxGridViewBatchUpdateValues<StudentFeesDetail, int> updateValues, int studentID, int termID, int calssID)
        {
            if (Session["UserID"] == null) { return Redirect("~/"); }

            int mStudMasterId = 0;
            try
            {


                //int mFeesStructID = ;

                foreach (var product in updateValues.Insert)
                {
                    if (updateValues.IsValid(product))
                    {
                        //mStudMasterId = product.StudMasterId.Value;
                        product.StudentID = studentID;
                        product.StudentClassId = calssID;
                        product.SessionId = int.Parse(Session["SessionID"].ToString());
                        product.Type = "Cr";
                        InsertStructure(product, updateValues, mStudMasterId);
                    }
                }
                foreach (var product in updateValues.Update)
                {
                    if (updateValues.IsValid(product))
                    {
                        UpdateStructure(product, updateValues);
                        mStudMasterId = product.StudMasterId.Value;
                    }
                }
                foreach (var productID in updateValues.DeleteKeys)
                {
                    mStudMasterId = productID;
                    DeleteStructure(productID, updateValues);
                }


                decimal FeesAmount = 0;

                //FeesAmount = unitOfWork.feesStructureDetailService.GetTotalFeesAmountByClassID(mFeesStructID, int.Parse(Session["SessionID"].ToString()));

                FeesAmount = unitOfWork.StudentFeesMasterService.GetTotalFeesAmountByStudentID(studentID, termID, int.Parse(Session["SessionID"].ToString()));


                StudentFeesMaster objSM = new StudentFeesMaster();
                objSM.Amount = FeesAmount;
                objSM.StudmasterID = mStudMasterId;
                unitOfWork.StudentFeesMasterService.UpdateFeesAmountMaster(objSM);
                unitOfWork.Save();


            }
            catch (Exception e)
            {

            }


            //ViewData["FeesStructID"] = mStudMasterId;
            //ViewData["FeesStructID"] = mStudMasterId;
            ViewData["StudentID"] = studentID;
            ViewData["TermID"] = termID;
            ViewData["ClassID"] = calssID;
            return PartialView("ListFeesStructureForEdit", unitOfWork.StudentFeesDetailService.GetStudentFeesDetailByStudentIDAndTermID(studentID, termID, int.Parse(Session["SessionID"].ToString()), int.Parse(Session["CompID"].ToString()), int.Parse(Session["BranchID"].ToString())));
            
        }


        //protected void UpdateStructure(vListFeesStructure product, MVCxGridViewBatchUpdateValues<vListFeesStructure, int> updateValues)
        protected void UpdateStructure(StudentFeesDetail product, MVCxGridViewBatchUpdateValues<StudentFeesDetail, int> updateValues)
        {
            try
            {
                unitOfWork.StudentFeesDetailService.UpdateStuddentFeesStructureDetailnew(product, byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()));
                 unitOfWork.Save();
            }
            catch (Exception e)
            {
                //updateValues.SetErrorText(product, e.Message);
            }
        }
        protected void DeleteStructure(int product, MVCxGridViewBatchUpdateValues<StudentFeesDetail, int> updateValues)
        {
            try
            {
                unitOfWork.StudentFeesDetailService.Delete(product);
                unitOfWork.Save();
            }
            catch (Exception e)
            {
                updateValues.SetErrorText(product, e.Message);
            }
        }
        protected void InsertStructure(StudentFeesDetail product, MVCxGridViewBatchUpdateValues<StudentFeesDetail, int> updateValues,int mfeeStructID)
        {
            try
            {
                unitOfWork.StudentFeesDetailService.InsertStuddentFeesStructureDetail(product, mfeeStructID, int.Parse(Session["SessionID"].ToString()), byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()));
                unitOfWork.Save();
            }
            catch (Exception e)
            {
                updateValues.SetErrorText(product, e.Message);
            }
        }

        #endregion


    }
}
