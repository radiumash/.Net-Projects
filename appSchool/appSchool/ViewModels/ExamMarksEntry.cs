using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;

namespace appSchool.ViewModels
{
    public class ExamMarksEntry
    {
    }
    public class ExamResultMaster
    {
        #region PRIVATE DATA REGION
        private int _ExamId;
        private int _ClassId;
        private int _SubjectIdL1;
        private int _SubjectIdL2;
        private int _SubjectIdL3;
        private int _CompID;
        private int _BranchID;
        private int _SessionID;
        private int _UserID;
        private SqlConnection _mConn;
        private SqlTransaction _mTran;
        private string _ErrorMessage;
        private List<ExamResultDetail> _Items;
        private int _ExamOrder;

        #endregion
        #region PROPERTY DECLARED REGION

        public int ExamId
        {
            get { return _ExamId; }
            set { _ExamId = value; }
        }
        public int ClassId
        {
            get { return _ClassId; }
            set { _ClassId = value; }
        }
        public int SubjectIdL1
        {
            get { return _SubjectIdL1; }
            set { _SubjectIdL1 = value; }
        }
        public int SubjectIdL2
        {
            get { return _SubjectIdL2; }
            set { _SubjectIdL2 = value; }
        }
        public int SubjectIdL3
        {
            get { return _SubjectIdL3; }
            set { _SubjectIdL3 = value; }
        }

        public int CompID
        {
            get { return _CompID; }
            set { _CompID = value; }
        }
        public int BranchID
        {
            get { return _BranchID; }
            set { _BranchID = value; }
        }
        public int SessionID
        {
            get { return _SessionID; }
            set { _SessionID = value; }
        }
        public int UserID
        {
            get { return _UserID; }
            set { _UserID = value; }
        }
        public int ExamOrder
        {
            get { return _ExamOrder; }
            set { _ExamOrder = value; }
        }
       
        public string ErrorMessage
        {
            get { return _ErrorMessage; }
        }

        public List<ExamResultDetail> Items
        {
            get { return _Items; }
            set { _Items = value; }
        }

        #endregion

        #region CONSTRUCTOR DECLARED REGION
        public ExamResultMaster()
        {
            SetDefaultValue();
        }

       
        

        public ExamResultMaster(int mClassSetupId, int mSubjectID, int mExamId, int idL1, int idL2, int idL3, int min, int max, int compid, int branchid, int sessionid, int userid)
        {
            SetDefaultValue();
            _CompID = compid; _BranchID = branchid; _SessionID = sessionid; _UserID = userid;

            _Items = new List<ExamResultDetail>();
            string mSql = string.Empty;
            _ExamOrder = mExamId;

            string StudentList = RegularStudentlist(mClassSetupId, mSubjectID);

            mSql = "SELECT dbo.ExamResult.StudentID, dbo.StudentSession.RollNo, dbo.StudentRegistration.FirstName + ' ' + ISNULL(dbo.StudentRegistration.LastName, '') " +
                   "AS StudentName,dbo.ExamResult.MaxMark1, dbo.ExamResult.MinMark1, dbo.ExamResult.ObtainMarks1 " +
                   " ,dbo.ExamResult.MaxMark2, dbo.ExamResult.MinMark2, dbo.ExamResult.ObtainMarks2 " +
                   " ,dbo.ExamResult.MaxMark3, dbo.ExamResult.MinMark3, dbo.ExamResult.ObtainMarks3 " +
                   " ,dbo.ExamResult.MaxMark4, dbo.ExamResult.MinMark4, dbo.ExamResult.ObtainMarks4 " +
                   " ,dbo.ExamResult.MaxMark5, dbo.ExamResult.MinMark5, dbo.ExamResult.ObtainMarks5, " +
                   " dbo.ExamResult.IsAbsent,dbo.ExamResult.Grade,dbo.ExamResult.ObtainGrade2,dbo.ExamResult.ObtainGrade3,dbo.ExamResult.ObtainGrade4,cast(0 as bit) as IsNew FROM dbo.ExamResult INNER JOIN  dbo.StudentSession ON dbo.ExamResult.StudentID = dbo.StudentSession.StudentID AND " +
                   " dbo.ExamResult.SessionID = dbo.StudentSession.SessionID AND  dbo.ExamResult.ClassID = dbo.StudentSession.ClassID INNER JOIN  dbo.StudentRegistration ON dbo.StudentSession.StudentID = dbo.StudentRegistration.StudentID AND " +
                   " dbo.StudentSession.CompID = dbo.StudentRegistration.CompID AND dbo.StudentSession.BranchID = dbo.StudentRegistration.BranchID " +
                   " WHERE dbo.StudentSession.SessionID =" + _SessionID + " AND dbo.StudentSession.ClassSetupID=" + mClassSetupId +
                   " AND dbo.ExamResult.SubjectIDL1 =" + idL1 + " AND  dbo.ExamResult.SubjectIDL2 =" + idL2 + " AND dbo.ExamResult.SubjectIDL3 =" + idL3 + " and dbo.StudentRegistration.TCGiven=0 ";


            mSql += " Union All select dbo.studentsession.StudentId,dbo.studentsession.RollNo,StudentRegistration.FirstName + ' ' + ISNULL(StudentRegistration.LastName, '') AS StudentName," +
                  " 0 as MaxMark1,0 as MinMark1,'0' as ObtainMarks1,0 as MaxMark2,0 as MinMark2,'0' as ObtainMarks2,0 as MaxMark3,0 as MinMark3,'0' as ObtainMarks3,0 as MaxMark4,0 as MinMark4,'0' as ObtainMarks4,0 as MaxMark5,0 as MinMark5,'0' as ObtainMarks5," +
                  " 0 as IsAbsent,'' as Grade,'' as ObtainGrade2,'' as ObtainGrade3,'' as ObtainGrade4,cast(1 as Bit) as IsNew from dbo.studentsession Inner Join dbo.StudentRegistration " +
                  " ON dbo.StudentRegistration.StudentID=dbo.StudentSession.StudentID where dbo.studentsession.ClassSetupId=" +
                mClassSetupId + " and dbo.studentsession.SessionId=" + sessionid + " and dbo.studentsession.BranchID=" +
                branchid + " and dbo.studentsession.CompID=" + compid + " and  dbo.studentsession.StudentID not in(select distinct studentid from dbo.ExamResult where " +
                "  SubjectIDL1=" + idL1 + "  and SubjectIDL2 =" + idL2 + " and  CompID =" + compid + " and  BranchID = " + branchid + " and SessionID =" + _SessionID + ") and dbo.StudentRegistration.TCGiven=0  order by Rollno ";

            DataTable dt = DB.ExecuteQuery(mSql);

            foreach (DataRow dRow in dt.Rows)
            {

                ExamResultDetail objDetail = new ExamResultDetail();
                objDetail.FillObjectFromDataRowSearch(dRow, _ExamOrder,compid);

                if (objDetail.MaxMark == 0)
                {
                    objDetail.MaxMark = max;
                    objDetail.MinMark = min;
                }
                //if (objDetail.IsNew == true && !StudentList.Contains(objDetail.StudentId.ToString())) continue;

                _Items.Add(objDetail);
            }

            
        }


      
        #endregion
        #region Method DECLARED REGION

        private void SetDefaultValue()
        {
            _ExamId = _ClassId;
            _SubjectIdL1 = _SubjectIdL2 = _SubjectIdL3 = -1;
        }
        private string RegularStudentlist(int ClasssetupID, int SubjectID)
        {
            string Studentlist = string.Empty;

            string sql = "Select StudentID from SubjectOptAllot where ClassSetupID=" + ClasssetupID + " and SubjectID1=" + SubjectID + " ";
            DataTable dt = DB.ExecuteQuery(sql);

            foreach (DataRow dr in dt.Rows)
            {
                Studentlist += dr["StudentID"].ToString() + ",";
            }

            Studentlist = Studentlist.TrimEnd(',');
            return Studentlist;
        }

        public bool UpdateExamResult()
        {
            bool res = false;
            _mConn = DB.GetActiveConnection();
            _mTran = _mConn.BeginTransaction(IsolationLevel.Snapshot);
            if (_ExamOrder == 1)
            {
                res = UpdateDetailedInfo();
                res = SaveDetailedInfoNew();
            }
            else if (_ExamOrder == 2)
            {
                res = UpdateDetailedInfo2();
                res = SaveDetailedInfoNew();
            }
            else if (_ExamOrder == 3)
                res = UpdateDetailedInfo3();
            else if (_ExamOrder == 4)
                res = UpdateDetailedInfo4();

            if (res)
            {
                _mTran.Commit();
            }
            else
                _mTran.Rollback();

            return res;
        }
        public bool AddNewExamResult()
        {
            bool res = false;
            _mConn = DB.GetActiveConnection();
            _mTran = _mConn.BeginTransaction(IsolationLevel.Snapshot);

            res = SaveDetailedInfo();



            if (res)
            {
                _mTran.Commit();
            }
            else
                _mTran.Rollback();

            return res;
        }


       
        public bool SaveDetailedInfo()
        {
            int i = 1;
            bool _DetailSaved = false;

            StringBuilder mQuery = new StringBuilder();

            List<double> lstMaxobtain = new List<double>();
            double maxobtaincheck = 0.0;
            string StudentIds = "";


            mQuery.Append("INSERT INTO dbo.ExamResult(" +
                            "StudentID, SessionID, ClassID, SubjectIDL1, SubjectIDL2, SubjectIDL3, MaxMark1, MinMark1, ObtainMarks1, IsAbsent, UIDAdd, AddDate,Grade,CompId,BranchId)");



            foreach (ExamResultDetail objItem in _Items)
            {

                mQuery.Append("SELECT " + objItem.StudentId + "," + _SessionID + "," + _ClassId + "," + _SubjectIdL1.ToString() + "," + _SubjectIdL2.ToString() + "," + _SubjectIdL3.ToString() + "," +
                          objItem.MaxMark + "," + objItem.MinMark + ",'" + objItem.ObtainMarks + "'," + Convert.ToByte(objItem.IsAbsent) + "," + _UserID + ", Getdate(),'" + objItem.Grade + "'," + _CompID + "," + _BranchID);
                if (i < _Items.Count)
                {
                    mQuery.Append(" UNION ALL ");
                    i++;
                }

                StudentIds += objItem.StudentId + ",";
                if (double.TryParse(objItem.ObtainMarks, out maxobtaincheck))
                {
                    lstMaxobtain.Add(maxobtaincheck);
                }

            }


            try
            {
                lstMaxobtain.Sort();
                string Maxobtain1 = lstMaxobtain[lstMaxobtain.Count - 1].ToString("0.00");
                StudentIds = StudentIds.Trim(',');
                string sqlupdate = "Update dbo.ExamResult Set ObtainMax1 = '" + Maxobtain1 + "' where ClassId=" + _ClassId +
                    " and SubjectIDL1=" + _SubjectIdL1 + " and SubjectIDL2=" + _SubjectIdL2 + " and SubjectIDL3=" + _SubjectIdL3 +
                    " and StudentId In (" + StudentIds + ") and  CompID =" + _CompID + " and  BranchID = " + _BranchID + " and SessionID =" + _SessionID + " ";
                int j = DB.ExecuteQueryNoResult(sqlupdate);
            }
            catch (Exception ex)
            {

            }

            SqlCommand cmdDetail = new SqlCommand("Execute_SQL", _mConn);
            cmdDetail.Transaction = _mTran;
            cmdDetail.CommandType = CommandType.StoredProcedure;
            cmdDetail.Connection = _mConn;
            cmdDetail.Parameters.Add("@MQuery", SqlDbType.VarChar).Value = mQuery.ToString();
            int recs = cmdDetail.ExecuteNonQuery();
            _DetailSaved = (recs > 0) ? true : false;
            _DetailSaved = true;
            return _DetailSaved;

        }
        public bool SaveDetailedInfoNew()
        {
            int i = 1;
            bool _DetailSaved = false;
            bool _isRecord = false;
            StringBuilder mQuery = new StringBuilder();



            mQuery.Append("INSERT INTO dbo.ExamResult(" +
                                     "StudentID, SessionID, ClassID, SubjectIDL1, SubjectIDL2, SubjectIDL3, MaxMark1, MinMark1, ObtainMarks1, IsAbsent, UIDAdd, AddDate,CompId,BranchId)");

            foreach (ExamResultDetail objItem in _Items)
            {
                if (objItem.IsNew == true)
                {
                    mQuery.Append("SELECT " + objItem.StudentId + "," + _SessionID + "," + _ClassId + "," + _SubjectIdL1.ToString() + "," + _SubjectIdL2.ToString() + "," + _SubjectIdL3.ToString() + "," +
                              objItem.MaxMark + "," + objItem.MinMark + ",'" + objItem.ObtainMarks + "'," + Convert.ToByte(objItem.IsAbsent) + "," + _UserID + ", Getdate()," + _CompID + "," + _BranchID);
                    _isRecord = true;
                    if (i < _Items.Count)
                    {
                        mQuery.Append(" UNION ALL ");
                        i++;
                    }
                }



            }



            if (_isRecord == true)
            {
                SqlCommand cmdDetail = new SqlCommand("Execute_SQL", _mConn);
                cmdDetail.Transaction = _mTran;
                cmdDetail.CommandType = CommandType.StoredProcedure;
                cmdDetail.Connection = _mConn;
                if (mQuery.Length > 0) mQuery = new StringBuilder(mQuery.ToString().Substring(0, mQuery.Length - 10));
                cmdDetail.Parameters.Add("@MQuery", SqlDbType.VarChar).Value = mQuery.ToString();
                int recs = cmdDetail.ExecuteNonQuery();
            }
            _DetailSaved = true;
            return _DetailSaved;

        }
        public bool UpdateDetailedInfo()
        {

            bool _DetailSaved = false;

            StringBuilder mQuery = new StringBuilder();

            List<double> lstMaxobtain = new List<double>();
            double maxobtaincheck = 0.0;
            string StudentIds = "";
            foreach (ExamResultDetail objItem in _Items)
            {
                if (objItem.IsNew == false)
                {
                    mQuery.Append("  Update dbo.ExamResult Set MaxMark1=" + objItem.MaxMark + ",MinMark1=" + objItem.MinMark + ",ObtainMarks1='" + objItem.ObtainMarks +
                        "',Grade='" + objItem.Grade + "',IsAbsent=" + Convert.ToByte(objItem.IsAbsent) + ",UIDMod=" + _UserID + ",MODDate=GetDate() where ClassId=" + _ClassId +
                " and SubjectIDL1=" + _SubjectIdL1 + " and SubjectIDL2=" + _SubjectIdL2 + " and SubjectIDL3=" + _SubjectIdL3 + " and  CompID =" + _CompID + " and  BranchID = " + _BranchID + " and SessionID =" + _SessionID + " and StudentID=" + objItem.StudentId);
                }


                StudentIds += objItem.StudentId + ",";
                if (double.TryParse(objItem.ObtainMarks, out maxobtaincheck))
                {
                    lstMaxobtain.Add(maxobtaincheck);
                }

            }



            try
            {
                if (_CompID != 1) // not for nirmala
                {
                    lstMaxobtain.Sort();
                    string Maxobtain1 = lstMaxobtain[lstMaxobtain.Count - 1].ToString("0.00");
                    StudentIds = StudentIds.Trim(',');
                    string sqlupdate = "Update dbo.ExamResult Set ObtainMax1 = '" + Maxobtain1 + "' where ClassId=" + _ClassId +
                        " and SubjectIDL1=" + _SubjectIdL1 + " and SubjectIDL2=" + _SubjectIdL2 + " and SubjectIDL3=" + _SubjectIdL3 +
                        " and StudentId In (" + StudentIds + ") and  CompID =" + _CompID + " and  BranchID = " + _BranchID + " and SessionID =" + _SessionID + " ";
                    int j = DB.ExecuteQueryNoResult(sqlupdate);
                }
            }
            catch (Exception ex)
            {

            }



            if (mQuery.Length > 0)
            {
                SqlCommand cmdDetail = new SqlCommand("Execute_SQL", _mConn);
                cmdDetail.Transaction = _mTran;
                cmdDetail.CommandType = CommandType.StoredProcedure;
                cmdDetail.Connection = _mConn;
                cmdDetail.Parameters.Add("@MQuery", SqlDbType.VarChar).Value = mQuery.ToString();
                int recs = cmdDetail.ExecuteNonQuery();
            }
            _DetailSaved = true;
            return _DetailSaved;

        }
        public bool UpdateDetailedInfo2()
        {

            bool _DetailSaved = false;

            StringBuilder mQuery = new StringBuilder();

            List<double> lstMaxobtain = new List<double>();
            double maxobtaincheck = 0.0;
            string StudentIds = "";

            foreach (ExamResultDetail objItem in _Items)
            {

                mQuery.Append("  Update dbo.ExamResult Set MaxMark2=" + objItem.MaxMark + ",MinMark2=" + objItem.MinMark + ",ObtainMarks2='" + objItem.ObtainMarks +
                    "',ObtainGrade2='" + objItem.Grade + "',  IsAbsent=" + Convert.ToByte(objItem.IsAbsent) + ",UIDMod=" + _UserID + ",MODDate=GetDate() where ClassId=" + _ClassId +
            " and SubjectIDL1=" + _SubjectIdL1 + " and SubjectIDL2=" + _SubjectIdL2 + " and SubjectIDL3=" + _SubjectIdL3 + " and  CompID =" + _CompID + " and  BranchID = " + _BranchID + " and SessionID =" + _SessionID + " and StudentID=" + objItem.StudentId);


                StudentIds += objItem.StudentId + ",";
                if (double.TryParse(objItem.ObtainMarks, out maxobtaincheck))
                {
                    lstMaxobtain.Add(maxobtaincheck);
                }
            }

            try
            {
                if (_CompID != 1) // not for nirmala
                {
                    lstMaxobtain.Sort();
                    string Maxobtain2 = lstMaxobtain[lstMaxobtain.Count - 1].ToString("0.00");
                    StudentIds = StudentIds.Trim(',');
                    string sqlupdate = "Update dbo.ExamResult Set ObtainMax2= '" + Maxobtain2 + "' where ClassId=" + _ClassId +
                        " and SubjectIDL1=" + _SubjectIdL1 + " and SubjectIDL2=" + _SubjectIdL2 + " and SubjectIDL3=" + _SubjectIdL3 +
                        " and StudentId In (" + StudentIds + ") and  CompID =" + _CompID + " and  BranchID = " + _BranchID + " and SessionID =" + _SessionID + " ";
                    int j = DB.ExecuteQueryNoResult(sqlupdate);
                }
            }
            catch (Exception ex)
            {

            }

            SqlCommand cmdDetail = new SqlCommand("Execute_SQL", _mConn);
            cmdDetail.Transaction = _mTran;
            cmdDetail.CommandType = CommandType.StoredProcedure;
            cmdDetail.Connection = _mConn;
            cmdDetail.Parameters.Add("@MQuery", SqlDbType.VarChar).Value = mQuery.ToString();
            int recs = cmdDetail.ExecuteNonQuery();
            _DetailSaved = (recs > 0) ? true : false;
            _DetailSaved = true;
            return _DetailSaved;

        }
        public bool UpdateDetailedInfo3()
        {

            bool _DetailSaved = false;

            StringBuilder mQuery = new StringBuilder();


            List<double> lstMaxobtain = new List<double>();
            double maxobtaincheck = 0.0;
            string StudentIds = "";


            foreach (ExamResultDetail objItem in _Items)
            {

                mQuery.Append("  Update dbo.ExamResult Set MaxMark3=" + objItem.MaxMark + ",MinMark3=" + objItem.MinMark + ",ObtainMarks3='" + objItem.ObtainMarks +
                    "',ObtainGrade3='" + objItem.Grade + "',IsAbsent=" + Convert.ToByte(objItem.IsAbsent) + ",UIDMod=" + _UserID + ",MODDate=GetDate() where ClassId=" + _ClassId +
            " and SubjectIDL1=" + _SubjectIdL1 + " and SubjectIDL2=" + _SubjectIdL2 + " and SubjectIDL3=" + _SubjectIdL3 + " and  CompID =" + _CompID + " and  BranchID = " + _BranchID + " and SessionID =" + _SessionID + " and StudentID=" + objItem.StudentId);


                StudentIds += objItem.StudentId + ",";
                if (double.TryParse(objItem.ObtainMarks, out maxobtaincheck))
                {
                    lstMaxobtain.Add(maxobtaincheck);
                }
            }


            try
            {
                if (_CompID != 1) // not for nirmala
                {

                    lstMaxobtain.Sort();
                    string Maxobtain2 = lstMaxobtain[lstMaxobtain.Count - 1].ToString("0.00");
                    StudentIds = StudentIds.Trim(',');
                    string sqlupdate = "Update dbo.ExamResult Set ObtainMax3= '" + Maxobtain2 + "' where ClassId=" + _ClassId +
                        " and SubjectIDL1=" + _SubjectIdL1 + " and SubjectIDL2=" + _SubjectIdL2 + " and SubjectIDL3=" + _SubjectIdL3 +
                        " and StudentId In (" + StudentIds + ") and  CompID =" + _CompID + " and  BranchID = " + _BranchID + " and SessionID =" + _SessionID + " ";
                    int j = DB.ExecuteQueryNoResult(sqlupdate);
                }
            }
            catch (Exception ex)
            {

            }


            SqlCommand cmdDetail = new SqlCommand("Execute_SQL", _mConn);
            cmdDetail.Transaction = _mTran;
            cmdDetail.CommandType = CommandType.StoredProcedure;
            cmdDetail.Connection = _mConn;
            cmdDetail.Parameters.Add("@MQuery", SqlDbType.VarChar).Value = mQuery.ToString();
            int recs = cmdDetail.ExecuteNonQuery();
            _DetailSaved = (recs > 0) ? true : false;
            _DetailSaved = true;
            return _DetailSaved;

        }
        public bool UpdateDetailedInfo4()
        {

            bool _DetailSaved = false;

            StringBuilder mQuery = new StringBuilder();

            List<double> lstMaxobtain = new List<double>();
            double maxobtaincheck = 0.0;
            string StudentIds = "";

            foreach (ExamResultDetail objItem in _Items)
            {

                mQuery.Append("  Update dbo.ExamResult Set MaxMark4=" + objItem.MaxMark + ",MinMark4=" + objItem.MinMark + ",ObtainMarks4='" + objItem.ObtainMarks +
                    "',ObtainGrade4='" + objItem.Grade + "',IsAbsent=" + Convert.ToByte(objItem.IsAbsent) + ",UIDMod=" + _UserID + ",MODDate=GetDate() where ClassId=" + _ClassId +
            " and SubjectIDL1=" + _SubjectIdL1 + " and SubjectIDL2=" + _SubjectIdL2 + " and SubjectIDL3=" + _SubjectIdL3 + " and  CompID =" + _CompID + " and  BranchID = " + _BranchID + " and SessionID =" + _SessionID + " and StudentID=" + objItem.StudentId);


                StudentIds += objItem.StudentId + ",";
                if (double.TryParse(objItem.ObtainMarks, out maxobtaincheck))
                {
                    lstMaxobtain.Add(maxobtaincheck);
                }

            }

            try
            {
                if (_CompID != 1) // not for nirmala
                {
                    lstMaxobtain.Sort();
                    string Maxobtain1 = lstMaxobtain[lstMaxobtain.Count - 1].ToString("0.00");
                    StudentIds = StudentIds.Trim(',');
                    string sqlupdate = "Update dbo.ExamResult Set ObtainMax4 = '" + Maxobtain1 + "' where ClassId=" + _ClassId +
                        " and SubjectIDL1=" + _SubjectIdL1 + " and SubjectIDL2=" + _SubjectIdL2 + " and SubjectIDL3=" + _SubjectIdL3 +
                        " and StudentId In (" + StudentIds + ") and  CompID =" + _CompID + " and  BranchID = " + _BranchID + " and SessionID =" + _SessionID + " ";
                    int j = DB.ExecuteQueryNoResult(sqlupdate);
                }
            }
            catch (Exception ex)
            {

            }

            SqlCommand cmdDetail = new SqlCommand("Execute_SQL", _mConn);
            cmdDetail.Transaction = _mTran;
            cmdDetail.CommandType = CommandType.StoredProcedure;
            cmdDetail.Connection = _mConn;
            cmdDetail.Parameters.Add("@MQuery", SqlDbType.VarChar).Value = mQuery.ToString();
            int recs = cmdDetail.ExecuteNonQuery();
            _DetailSaved = (recs > 0) ? true : false;
            _DetailSaved = true;
            return _DetailSaved;

        }
        public bool UpdateDetailedInfo5()
        {

            bool _DetailSaved = false;

            StringBuilder mQuery = new StringBuilder();

            foreach (ExamResultDetail objItem in _Items)
            {

                mQuery.Append("  Update dbo.ExamResult Set MaxMark5=" + objItem.MaxMark + ",MinMark5=" + objItem.MinMark + ",ObtainMarks5='" + objItem.ObtainMarks +
                    "',IsAbsent=" + Convert.ToByte(objItem.IsAbsent) + ",UIDMod=" + _UserID + ",MODDate=GetDate() where ClassId=" + _ClassId +
            " and SubjectIDL1=" + _SubjectIdL1 + " and SubjectIDL2=" + _SubjectIdL2 + " and SubjectIDL3=" + _SubjectIdL3 + " and  CompID =" + _CompID + " and  BranchID = " + _BranchID + " and SessionID =" + _SessionID + " and StudentID=" + objItem.StudentId);

            }

            SqlCommand cmdDetail = new SqlCommand("Execute_SQL", _mConn);
            cmdDetail.Transaction = _mTran;
            cmdDetail.CommandType = CommandType.StoredProcedure;
            cmdDetail.Connection = _mConn;
            cmdDetail.Parameters.Add("@MQuery", SqlDbType.VarChar).Value = mQuery.ToString();
            int recs = cmdDetail.ExecuteNonQuery();
            _DetailSaved = (recs > 0) ? true : false;
            _DetailSaved = true;
            return _DetailSaved;

        }
        #endregion
    }

    public class ExamResultDetail
    {
        #region PRIVATE DATA REGION
        private int _StudentId;
        private string _StudentName = string.Empty;
        private int _RollNo;
        private int _MinMark;
        private int _MaxMark;
        private string _ObtainMarks = string.Empty;
        private bool _IsAbsent = false;
        private string _Grade = string.Empty;
        private bool _IsNew = false;


        #endregion
        #region PROPERTY DECLARED REGION

        public int StudentId
        {
            get { return _StudentId; }
            set { _StudentId = value; }
        }
        public int RollNo
        {
            get { return _RollNo; }
            set { _RollNo = value; }
        }
        public string StudentName
        {
            get { return _StudentName; }
            set { _StudentName = value; }
        }
        public int MaxMark
        {
            get { return _MaxMark; }
            set { _MaxMark = value; }
        }
        public int MinMark
        {
            get { return _MinMark; }
            set { _MinMark = value; }
        }
        public string ObtainMarks
        {
            get { return _ObtainMarks; }
            set { _ObtainMarks = value; }
        }


        public bool IsAbsent
        {
            get { return _IsAbsent; }
            set { _IsAbsent = value; }
        }

        public string Grade
        {
            get { return _Grade; }
            set { _Grade = value; }
        }
        public bool IsNew
        {
            get { return _IsNew; }
            set { _IsNew = value; }
        }
        #endregion
        #region CONSTRUCTOR DECLARED REGION
        public ExamResultDetail()
        {
            SetDefaultValue();
        }


        #endregion
        #region METHOD DECLARED REGION

        private void SetDefaultValue()
        {
            _MinMark = _MaxMark = 0;
        }
        public void FillObjectFromDataRow(DataRow dr)
        {
            if (dr == null) return;
            _StudentId = int.Parse(dr["StudentId"].ToString());
            _RollNo = int.Parse(dr["RollNo"].ToString());
            _StudentName = dr["StudentName"].ToString();
        }
        public void FillObjectFromDataRowSearch(DataRow dr, int mOrder, int mCompid)
        {
            if (dr == null) return;
            _StudentId = int.Parse(dr["StudentId"].ToString());
            _IsNew = bool.Parse(dr["IsNew"].ToString());
            _RollNo = int.Parse(dr["RollNo"].ToString());
            if (mOrder == 1)
            {
                _MinMark = int.Parse(dr["MinMark1"].ToString());
                _MaxMark = int.Parse(dr["MaxMark1"].ToString());
                _ObtainMarks = dr["ObtainMarks1"].ToString();
                _Grade = dr["Grade"].ToString();
            }
            if (mOrder == 2)
            {
                _MinMark = int.Parse(dr["MinMark2"].ToString());
                _MaxMark = int.Parse(dr["MaxMark2"].ToString());
                _ObtainMarks = dr["ObtainMarks2"].ToString();
                _Grade = dr["ObtainGrade2"].ToString();
            }
            if (mOrder == 3)
            {
                _MinMark = int.Parse(dr["MinMark3"].ToString());
                _MaxMark = int.Parse(dr["MaxMark3"].ToString());
                _ObtainMarks = dr["ObtainMarks3"].ToString();
                _Grade = dr["ObtainGrade3"].ToString();
            }
            if (mOrder == 4)
            {
                _MinMark = int.Parse(dr["MinMark4"].ToString());
                _MaxMark = int.Parse(dr["MaxMark4"].ToString());
                _ObtainMarks = dr["ObtainMarks4"].ToString();
                _Grade = dr["ObtainGrade4"].ToString();
            }
            if (mOrder == 5)
            {
                _MinMark = int.Parse(dr["MinMark5"].ToString());
                _MaxMark = int.Parse(dr["MaxMark5"].ToString());
                _ObtainMarks = dr["ObtainMarks5"].ToString();
                //_Grade = dr["ObtaiGrade5"].ToString();
            }

            if (mCompid == 7)
            {

                if (mOrder == 6)
                {
                    _MinMark = int.Parse(dr["MinMark6"].ToString());
                    _MaxMark = int.Parse(dr["MaxMark6"].ToString());
                    _ObtainMarks = dr["ObtainMarks6"].ToString();
                    //_Grade = dr["ObtaiGrade5"].ToString();
                }

                if (mOrder == 7)
                {
                    _MinMark = int.Parse(dr["MinMark7"].ToString());
                    _MaxMark = int.Parse(dr["MaxMark7"].ToString());
                    _ObtainMarks = dr["ObtainMarks7"].ToString();
                    //_Grade = dr["ObtaiGrade5"].ToString();
                }

                if (mOrder == 8)
                {
                    _MinMark = int.Parse(dr["MinMark8"].ToString());
                    _MaxMark = int.Parse(dr["MaxMark8"].ToString());
                    _ObtainMarks = dr["ObtainMarks8"].ToString();
                    //_Grade = dr["ObtaiGrade5"].ToString();
                }
            }
            // _IsAbsent = bool.Parse(dr["IsAbsent"].ToString());

            _StudentName = dr["StudentName"].ToString();
        }
        #endregion

    }
}