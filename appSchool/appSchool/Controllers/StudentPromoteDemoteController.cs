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
using Newtonsoft.Json;

namespace appSchool.Controllers
{
    //[NoCache]
    public class StudentPromoteDemoteController : Controller
    { 

        private UnitOfWork unitOfWork = new UnitOfWork();
        private SqlConnection _mConn;
        private SqlTransaction _mTran;

        public ActionResult Index()
        {
            if (Session["UserID"] == null )
            {
                return Redirect("~/");
            }

            UserPermission objuser = new UserPermission();
            objuser = unitOfWork.userPermissionService.CheckUserPermissionModulewise(int.Parse(Session["UserID"].ToString()), 19, byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()));
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

        public ActionResult PartialStudentSessionView(int mClassesID, int mClassesSetupID)
        {
            if (Session["UserID"] == null) { return Redirect("~/"); }
            ViewData["ClassID"] = mClassesID;
            ViewData["ClassesSetupID"] = mClassesSetupID;

            return PartialView("ListStudentSessionView", new UnitOfWork().studentSessionService.GetAllStudentWithTCFalsebyClassandClasssetupID(mClassesID, mClassesSetupID, int.Parse(Session["SessionID"].ToString()), byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));
        }
        public JsonResult GetSectionListView(int mClassID)
        {
            string ErrorMsg = string.Empty;
           
            var DataSectionList = string.Empty;
            if (mClassID > 0)
            {

                List<ClassSetup> objsection = new List<ClassSetup>();
                objsection = unitOfWork.classSetupService.GetAllClassNameByClassID(mClassID);
                if (objsection.Count == 0)
                {
                    ErrorMsg += " Section Not Found. ";
                   
                }
                DataSectionList = JsonConvert.SerializeObject(objsection);

            }
            else
            {
                ErrorMsg += " Please Select Class. ";
                //msgFlag = true;
            }


            return new JsonResult
            {
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                Data = new
                {
                    
                    DisplayMsg = ErrorMsg,
                    SectionList = DataSectionList,
                }
            };



        }

        public ActionResult GetAllStudentListView(int mClassesID , int mClassesSetupID)
        {
            if (Session["UserID"] == null) { return Redirect("~/"); }
            
            ViewData["ClassID"] = mClassesID;
            ViewData["ClassesSetupID"] = mClassesSetupID;

            return PartialView("ListStudentSessionView", new UnitOfWork().studentSessionService.GetAllStudentWithTCFalsebyClassandClasssetupID(mClassesID,  mClassesSetupID, int.Parse(Session["SessionID"].ToString()), byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));
        }

        public ActionResult SaveStudentPromoteDetaote(int mClassesID , int mClassesSetupID , int mToClassID , int mToClassesSetupID , string mStudentID, string mClassName, string mtoClassName, string mStudentName)
        {
            if (Session["UserID"] == null)
            {
                return Redirect("~/");
            }
            string ErrorMsg = string.Empty;
            string mResponseNextProcessMsg = string.Empty;
            int mResponseCode = 0;
            int btnattendanceenable = 1;



            try
            {
                int mSessionID = int.Parse(Session["SessionID"].ToString());
                int mCompID = int.Parse(Session["CompID"].ToString());
                int mBranchID = int.Parse(Session["BranchID"].ToString());
                int mUserID = int.Parse(Session["UserID"].ToString());

                string sql = "Select StudentID From ExamResult Where StudentID=" + mStudentID + " and CompID=" + mCompID + " And BranchID=" + mBranchID + " And SessionID=" + mSessionID + "";
                DataTable dt = DB.ExecuteQuery(sql);

                string sqlremark = "Select StudentID From ExamRemarkEntry Where StudentID=" + mStudentID + " and CompID=" + mCompID + " And BranchID=" + mBranchID + " And SessionID=" + mSessionID + "";
                DataTable dtremark = DB.ExecuteQuery(sqlremark);


                if (dt.Rows.Count > 0)
                {
                    ErrorMsg = "Exam Marks Entry create for this student first you have to delete it.";
                    mResponseCode = 3;//  Exam Marks Entry 

                }
                else if(dtremark.Rows.Count > 0)
                {
                    ErrorMsg = "Exam ReMarks Entry create for this student first you have to delete it.";
                    mResponseCode = 4;//  Exam ReMarks Entry 
                }
                else
                {
                    bool IsNewStudent = false;
                    int studentID = int.Parse(mStudentID);
                    vStudentSession Objstud = unitOfWork.studentSessionService.GetSingleStudentbyStudentID(studentID, mSessionID, mCompID, mBranchID);

                    if (Objstud != null)
                    {
                        IsNewStudent = Objstud.IsNewStudent;
                    }

                    ClassPromoteDemote objpromote = new ClassPromoteDemote();
                    objpromote.UserID = mUserID;
                    objpromote.SessionID = mSessionID;
                    objpromote.CompID = mCompID;
                    objpromote.BranchID = mBranchID;
                    objpromote.ClassID = mClassesID;
                    objpromote.ClassSetupID = mClassesSetupID;
                    objpromote.StudentID = studentID;
                    objpromote.ToClassID = mToClassID;
                    objpromote.ToClassSetupID = mToClassesSetupID;
                    objpromote.IsNewStudent = IsNewStudent;
                    objpromote.ClassName = mClassName;
                    objpromote.ToClassName = mtoClassName;
                    objpromote.StudentName = mStudentName;


                    if (objpromote.UpdateStudentClass())
                    {
                        ErrorMsg = "Student Class Modified Successfully ";
                        mResponseCode = 1;
                        bool chkbtnattendanceenable = false;
                        mResponseNextProcessMsg = objpromote.CheckAttandanceForClassPromote(out chkbtnattendanceenable);

                        if (chkbtnattendanceenable) btnattendanceenable = 1;
                    }
                    else
                        ErrorMsg = "Unable to Modify Class";
                }
                

            }
            catch (Exception ex)
            {
            }



            return new JsonResult
            {
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                Data = new
                {
                    DisplayMsg = ErrorMsg + "," + mResponseNextProcessMsg,
                    ResponseCode = mResponseCode,
                    ButtonAttendanceEnabled = btnattendanceenable

                }
            };



        }

        public ActionResult TransferStudentAttendance(int mClassesID, int mClassesSetupID, int mToClassID, int mToClassesSetupID, string mStudentID)
        {
            if (Session["UserID"] == null)
            {
                return Redirect("~/");
            }

            string ErrorMsg = string.Empty;
            int mResponseCode = 0;
            string mResponseNextProcessMsg = string.Empty;

            try
            {

                int mSessionID = int.Parse(Session["SessionID"].ToString());
                int mCompID = int.Parse(Session["CompID"].ToString());
                int mBranchID = int.Parse(Session["BranchID"].ToString());
                int mUserID = int.Parse(Session["UserID"].ToString());

                bool IsNewStudent = false;
                int studentID = int.Parse(mStudentID);
                vStudentSession Objstud = unitOfWork.studentSessionService.GetSingleStudentbyStudentID(studentID, mSessionID, mCompID, mBranchID);

                if (Objstud != null)
                {
                    IsNewStudent = Objstud.IsNewStudent;
                }

                ClassPromoteDemote objpromote = new ClassPromoteDemote();
                objpromote.UserID = mUserID;
                objpromote.SessionID = mSessionID;
                objpromote.CompID = mCompID;
                objpromote.BranchID = mBranchID;
                objpromote.ClassID = mClassesID;
                objpromote.ClassSetupID = mClassesSetupID;
                objpromote.StudentID = studentID;
                objpromote.ToClassID = mToClassID;
                objpromote.ToClassSetupID = mToClassesSetupID;
                objpromote.IsNewStudent = IsNewStudent;

                if (objpromote.UpdateStudentAttandance())
                {
                    ErrorMsg = "Student Attandance Transfered" ;
                    mResponseNextProcessMsg = objpromote.CheckFeesStructureForClassPromote();
                    mResponseCode = 1;
                }
                else
                {
                    ErrorMsg = "Unable to Modify Attandance Details " ;

                }

            }
            catch(Exception ex)
            {

            }

            return new JsonResult
            {
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                Data = new
                {

                    DisplayMsg = ErrorMsg + "," + mResponseNextProcessMsg,
                    ResponseCode = mResponseCode
                }
            };



        }

        public ActionResult DeleteFeesStructure(int mClassesID, int mClassesSetupID, int mToClassID, int mToClassesSetupID, string mStudentID, string mClassName, string mtoClassName, string mStudentName)
        {
            if (Session["UserID"] == null)
            {
                return Redirect("~/");
            }

            string ErrorMsg = string.Empty;
            int mResponseCode = 0;

            try
            {

                int mSessionID = int.Parse(Session["SessionID"].ToString());
                int mCompID = int.Parse(Session["CompID"].ToString());
                int mBranchID = int.Parse(Session["BranchID"].ToString());
                int mUserID = int.Parse(Session["UserID"].ToString());

                bool IsNewStudent = false;
                int studentID = int.Parse(mStudentID);
                vStudentSession Objstud = unitOfWork.studentSessionService.GetSingleStudentbyStudentID(studentID, mSessionID, mCompID, mBranchID);

                if (Objstud != null)
                {
                    IsNewStudent = Objstud.IsNewStudent;
                }

                ClassPromoteDemote objpromote = new ClassPromoteDemote();
                objpromote.UserID = mUserID;
                objpromote.SessionID = mSessionID;
                objpromote.CompID = mCompID;
                objpromote.BranchID = mBranchID;
                objpromote.ClassID = mClassesID;
                objpromote.ClassSetupID = mClassesSetupID;
                objpromote.StudentID = studentID;
                objpromote.ToClassID = mToClassID;
                objpromote.ToClassSetupID = mToClassesSetupID;
                objpromote.IsNewStudent = IsNewStudent;
                objpromote.ClassName = mClassName;
                objpromote.ToClassName = mtoClassName;
                objpromote.StudentName = mStudentName;

                if (objpromote.DeleteFeesStructure())
                {
                    ErrorMsg = "Student Fees Structure Deleted successfully" + objpromote.RecreateFeesStructureMessage();


                    mResponseCode = 1;
                }
                else
                {
                    ErrorMsg = "Unable to Delete Fees Structure Details ";

                    
                }

            }
            catch (Exception ex)
            {

            }

            return new JsonResult
            {
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                Data = new
                {

                    DisplayMsg = ErrorMsg,
                    ResponseCode = mResponseCode
                }
            };



        }

        public ActionResult RecreateFeesStructure(int mClassesID, int mClassesSetupID, int mToClassID, int mToClassesSetupID, string mStudentID)
        {
            if (Session["UserID"] == null)
            {
                return Redirect("~/");
            }

            string ErrorMsg = string.Empty;
            int mResponseCode = 0;

            try
            {

                int mSessionID = int.Parse(Session["SessionID"].ToString());
                int mCompID = int.Parse(Session["CompID"].ToString());
                int mBranchID = int.Parse(Session["BranchID"].ToString());
                int mUserID = int.Parse(Session["UserID"].ToString());

                bool IsNewStudent = false;
                int studentID = int.Parse(mStudentID);
                vStudentSession Objstud = unitOfWork.studentSessionService.GetSingleStudentbyStudentID(studentID, mSessionID, mCompID, mBranchID);

                if (Objstud != null)
                {
                    IsNewStudent = Objstud.IsNewStudent;
                }

                ClassPromoteDemote objpromote = new ClassPromoteDemote();
                objpromote.UserID = mUserID;
                objpromote.SessionID = mSessionID;
                objpromote.CompID = mCompID;
                objpromote.BranchID = mBranchID;
                objpromote.ClassID = mClassesID;
                objpromote.ClassSetupID = mClassesSetupID;
                objpromote.StudentID = studentID;
                objpromote.ToClassID = mToClassID;
                objpromote.ToClassSetupID = mToClassesSetupID;
                objpromote.IsNewStudent = IsNewStudent;

                if (objpromote.RecreateFeesStructure())
                {
                    ErrorMsg = "Student Fee Structure Generated Successfully";
                   
                    mResponseCode = 1;
                }
                else
                {
                    ErrorMsg = "Unable to Generated Fee Structure  ";

                    
                }

            }
            catch (Exception ex)
            {

            }

            return new JsonResult
            {
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                Data = new
                {

                    DisplayMsg = ErrorMsg,
                    ResponseCode = mResponseCode
                }
            };



        }

    }

    public class ClassPromoteDemote
    {

        public SqlConnection _mConn { get; set; }
        public SqlTransaction _mTran { get; set; }
        public string _ErrorMessage { get; set; }

        public int SessionID { get; set; }
        public int CompID { get; set; }
        public int BranchID { get; set; }
        public int UserID { get; set; }
        public int ClassID { get; set; }
        public int ClassSetupID { get; set; }
        public int StudentID { get; set; }
        public int ToClassID { get; set; }
        public int ToClassSetupID { get; set; }
        public bool IsNewStudent { get; set; }

        public int TermID { get; set; }

        public string  StudentName { get; set; }
        public string ClassName { get; set; }
        public string ToClassName { get; set; }

        public ClassPromoteDemote()
        {
            ClassName = ToClassName = string.Empty;
        }

        public bool UpdateStudentClass()
        {
            bool res = false;
            _mConn = new SqlConnection(DB.GetConnectionString());
            
            try
            {
                _mConn.Open();

                _mTran = _mConn.BeginTransaction(IsolationLevel.Snapshot);
                res = UpdateClassPromoteDemote();
                if (res)
                {
                    _mTran.Commit();
                }
                else
                    _mTran.Rollback();
            }
            catch(Exception ex)
            {
                _mTran.Rollback();
            }
            finally
            {
                _mConn.Dispose();
            }

            return res;
        }

        public bool UpdateClassPromoteDemote()
        {
            bool res = false;
            int rec = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("ClassPromoteDemote", _mConn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Transaction = _mTran;
                cmd.Parameters.Add("@StudentID", SqlDbType.Int).Value = StudentID;
                cmd.Parameters.Add("@ClassID", SqlDbType.Int).Value = ClassID;
                cmd.Parameters.Add("@ClassSetupID", SqlDbType.Int).Value = ToClassSetupID;
                cmd.Parameters.Add("@SessionID", SqlDbType.Int).Value =SessionID;
                cmd.Parameters.Add("@BranchID", SqlDbType.TinyInt).Value = BranchID;
                cmd.Parameters.Add("@CompID", SqlDbType.TinyInt).Value = CompID;

                rec = cmd.ExecuteNonQuery(); 

                res = (rec > 0) ? true : false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return res;

        }

       

        public bool UpdateStudentAttandance()
        {
            bool res = false;
            _mConn = new SqlConnection(DB.GetConnectionString());

            try
            {
                _mConn.Open();
                _mTran = _mConn.BeginTransaction(IsolationLevel.Snapshot);
                res = TransferAttandance();

                if (res)
                {
                    _mTran.Commit();
                }
                else
                {
                    _mTran.Rollback();
                }
                  
            }
            catch (Exception ex)
            {
                _mTran.Rollback();
            }
            finally
            {
                _mConn.Dispose();
            }


            return res;
        }

        public bool TransferAttandance()
        {
            bool res = false;
            int rec = 0; string _ClassDescription = string.Empty;

            string sql = "Select ClassDescription From ClassSetup Where ClassID=" + ToClassID + " and CompID=" + CompID + " And BranchID=" + BranchID + "";
            DataRow dr = DB.ExecuteSingleRow(sql);
            _ClassDescription = dr["ClassDescription"].ToString();

            try
            {
                
                SqlCommand cmd = new SqlCommand("ClassPromote_AttandanceTransfer", _mConn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Transaction = _mTran;
                cmd.Parameters.Add("@StudentID", SqlDbType.Int).Value = StudentID;
                cmd.Parameters.Add("@ClassSetupName", SqlDbType.VarChar).Value = _ClassDescription;
                cmd.Parameters.Add("@ClassSetupID", SqlDbType.Int).Value = ToClassSetupID;
                cmd.Parameters.Add("@SessionID", SqlDbType.Int).Value = SessionID;
                cmd.Parameters.Add("@BranchID", SqlDbType.TinyInt).Value = BranchID;
                cmd.Parameters.Add("@CompID", SqlDbType.TinyInt).Value = CompID;

                rec = cmd.ExecuteNonQuery();

                res = (rec > 0) ? true : false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return res;

        }

        public bool DeleteFeesStructure()
        {
            bool res = false;
          
            _mConn = new SqlConnection(DB.GetConnectionString());

            try
            {
                _mConn.Open();
                _mTran = _mConn.BeginTransaction(IsolationLevel.Snapshot);
                res = DeleteFeesStructureandRecreate();

                if (res)
                {
                    _mTran.Commit();
                }
                else
                    _mTran.Rollback();
            }
            catch(Exception ex)
            {
                
            }
            


            return res;
        }

        public bool DeleteFeesStructureandRecreate()
        {
            bool res = false;
            int rec = 0; string RecieptNo = string.Empty; int Receipt = 0;


            try
            {
                SqlCommand cmd = new SqlCommand("ClassPromoteDemote_FeesUpdate", _mConn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Transaction = _mTran;
                
                cmd.Parameters.Add("@StudentID", SqlDbType.Int).Value = StudentID;
                cmd.Parameters.Add("@ClassID", SqlDbType.Int).Value = ClassID;
                cmd.Parameters.Add("@ClassSetupID", SqlDbType.Int).Value = ClassSetupID;
                cmd.Parameters.Add("@SessionID", SqlDbType.Int).Value = SessionID;
                cmd.Parameters.Add("@BranchID", SqlDbType.TinyInt).Value = BranchID;
                cmd.Parameters.Add("@CompID", SqlDbType.TinyInt).Value = CompID;

                rec = cmd.ExecuteNonQuery();

                res = (rec > 0) ? true : false;
            }
            catch (Exception ex)
            {
                //throw ex;
            }
            return res;

        }

        public bool RecreateFeesStructure()
        {
            bool res = false;
            string sqlTerm = string.Empty;
            _mConn = new SqlConnection(DB.GetConnectionString());

            try
            {
                _mConn.Open();
                _mTran = _mConn.BeginTransaction();

            
                sqlTerm = "SELECT FeeTermID,sum(FeeAmount) as Amount FROM dbo.FeesStructureDetail where FeeStructSessionID=" +
                    SessionID + " and FeeStructClassID=" + ClassID + " group by FeeTermID ";
                DataTable dtTerm = DB.ExecuteQuery(sqlTerm);

                bool isNewStudent = false;
                foreach (DataRow dr in dtTerm.Rows)
                {
                   
                    SaveMasterInfo(ClassID, double.Parse(dr["Amount"].ToString()), int.Parse(dr["FeeTermID"].ToString()), StudentID, isNewStudent);

                }
                _mTran.Commit();
                res = true;
            }
            catch (Exception ex)
            {
                res = false;
            }
            finally
            {
                _mConn.Dispose();
            }
            return res;
        }


        public bool SaveMasterInfo(int _ClassId, double _Amount, int _TermID, int _StudentID, bool _IsNewStudent)
        {
            bool res = false;
            int rec = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("[Add_StudentFeesStructure]", _mConn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Transaction = _mTran;
                cmd.Parameters.Add("@ClassID", SqlDbType.Int).Value = _ClassId;
                cmd.Parameters.Add("@TermID", SqlDbType.Int).Value = _TermID;
                cmd.Parameters.Add("@Amount", SqlDbType.Decimal).Value = _Amount;
                cmd.Parameters.Add("@StudentID", SqlDbType.Int).Value = _StudentID;
                cmd.Parameters.Add("@IsNewStudent", SqlDbType.Bit).Value = _IsNewStudent;
                cmd.Parameters.Add("@UIdAdd", SqlDbType.TinyInt).Value = UserID;
                cmd.Parameters.Add("@CompID", SqlDbType.TinyInt).Value = CompID;
                cmd.Parameters.Add("@BranchID", SqlDbType.TinyInt).Value = BranchID;
                cmd.Parameters.Add("@SessionID", SqlDbType.TinyInt).Value = SessionID;
                rec = cmd.ExecuteNonQuery();
                if (rec > 0)
                    res = true;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return res;

        }

        public string CheckAttandanceForClassPromote(out bool chkbtnattendanceenable)
        {
            string responsemessage = string.Empty;
            chkbtnattendanceenable = false;
            DataTable _dtSource = null;
            string mQueryCheckAttandance = "Select  ClassAttendanceID,StudentID  from dbo.AttendanceStudent where StudentID=" + StudentID + " And BranchID=" + BranchID + " and CompID=" + CompID + "  ";
            _dtSource = DB.ExecuteQuery(mQueryCheckAttandance);
            if (_dtSource.Rows.Count > 0)
            {
                chkbtnattendanceenable = true;
                responsemessage = "Press TransferAttandance button to transfer attandance from Class " + ClassName + " to " + ToClassName + " ";

            }
            else
            {
                
                responsemessage = CheckFeesStructureForClassPromote();


            }

            return responsemessage;
        }

        public string CheckFeesStructureForClassPromote()
        {
            string responsemessage = string.Empty;
            DataTable _dtSource = null;
            string mQueryCheckFeesStructure = "Select  StudmasterID,StudentID  from dbo.StudentFeesMaster where StudentID=" + StudentID + " And BranchID=" + BranchID + " and CompID=" + CompID + " and SessionID=" + SessionID + "  ";
            _dtSource = DB.ExecuteQuery(mQueryCheckFeesStructure);
            if (_dtSource.Rows.Count > 0)
            {
                responsemessage = "(3)- Press DeleteFeesstructure button to delete feesstructure for " + StudentName + " ";


            }
            else
            {
                responsemessage = "AttanDance and FeesStructure is not generate for " + StudentName + ". Class Promote/Demote successfully ";
                //btnFeesTransfer.Enabled = false;
            }
            return responsemessage;
        }

        public string RecreateFeesStructureMessage()
        {
            return "Press RecreateFeesStructure button to recreate fees structure from Class " + ClassName + " to " + ToClassName + " ";

        }


    }

}



    