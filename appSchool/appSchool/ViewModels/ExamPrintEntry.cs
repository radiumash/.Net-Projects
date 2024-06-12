using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;

namespace appSchool.ViewModels
{
    public class ExamPrintEntry
    {
    }
    public class ExamPrintMaster
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
        private List<ExamPrintDetail> _Items;
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

        public List<ExamPrintDetail> Items
        {
            get { return _Items; }
            set { _Items = value; }
        }

        #endregion

        #region CONSTRUCTOR DECLARED REGION
        public ExamPrintMaster()
        {
            SetDefaultValue();
        }

       
        

        public ExamPrintMaster(int mClassSetupId, int mExamId, int mCLassID, int compid, int branchid, int sessionid)
        {
            SetDefaultValue();
            _CompID = compid; _BranchID = branchid; _SessionID = sessionid;

            _Items = new List<ExamPrintDetail>();
            string mSql = string.Empty;
            string sqlSubject = string.Empty;
            string subIds = string.Empty;
            //string sql = string.Empty;
            string sqlMain = string.Empty;
            string sql = string.Empty;

            if (CompID == 2 || CompID == 5 || CompID == 6 || CompID == 7)
            {
                sqlSubject = "SELECT TOP (100) PERCENT dbo.SubjectLevelOne.IdL1 AS ID, dbo.SubjectLevelOne.SubjectNameL1 AS Subject,dbo.ExamSetupDetail.MaxMark,dbo.ExamSetupDetail.MinMark " +
                                " FROM dbo.ExamSetupMaster INNER JOIN dbo.ExamSetupDetail ON dbo.ExamSetupMaster.ExamSetupID = dbo.ExamSetupDetail.ExamSetupID LEFT OUTER JOIN " +
                                " dbo.SubjectLevelOne ON dbo.ExamSetupDetail.SubjectIDL1 = dbo.SubjectLevelOne.IdL1 AND dbo.ExamSetupDetail.BranchID = dbo.SubjectLevelOne.BranchID AND " +
                                " dbo.ExamSetupDetail.CompID = dbo.SubjectLevelOne.CompID WHERE dbo.ExamSetupMaster.ClassID=" + mCLassID +
                                " AND dbo.ExamSetupMaster.ExamID =" + mExamId + " and ExamSetupMaster.CompID=" + CompID +
                                " and ExamSetupMaster.BranchID=" + BranchID + " and ExamSetupMaster.SessionID=" + SessionID + " and ExamSetupDetail.MarksType='Number' " +
                                " ORDER BY dbo.ExamSetupDetail.OrderNo ";

            }
            if (CompID == 1 || CompID == 4)
            {
                sqlSubject = sqlSubject = "SELECT dbo.SubjectLevelTwo.IdL2 AS ID, dbo.SubjectLevelTwo.SubjectNameL2 AS Subject,dbo.ExamSetupDetail.MaxMark,dbo.ExamSetupDetail.MinMark " +
                                    " FROM dbo.ExamSetupMaster INNER JOIN dbo.ExamSetupDetail ON dbo.ExamSetupMaster.ExamSetupID = dbo.ExamSetupDetail.ExamSetupID LEFT OUTER JOIN " +
                                    " dbo.SubjectLevelTwo ON dbo.ExamSetupDetail.SubjectIDL2 = dbo.SubjectLevelTwo.IdL2 AND dbo.ExamSetupDetail.BranchID = dbo.SubjectLevelTwo.BranchID AND " +
                                    " dbo.ExamSetupDetail.CompID = dbo.SubjectLevelTwo.CompID WHERE dbo.ExamSetupMaster.ClassID=" + mCLassID +
                                    " AND dbo.ExamSetupMaster.ExamID =" + mExamId + " and ExamSetupMaster.CompID=" + CompID +
                                    " and ExamSetupMaster.BranchID=" + BranchID + " and ExamSetupMaster.SessionID=" + SessionID + " and ExamSetupDetail.MarksType='Number' " +
                                    " ORDER BY dbo.ExamSetupDetail.OrderNo ";
            }
            DataTable dt = DB.ExecuteQuery(sqlSubject);

            int maxmark = 0;
            List<string> lstMin = new List<string>();
            List<string> lstMax = new List<string>();
            List<string> sbject = new List<string>();
            List<string> ID = new List<string>();


            int i = 0;
            foreach (DataRow dr in dt.Rows)
            {
                ++i;
                subIds += "[" + dr[0].ToString() + "],";
                //sql += "[" + dr[0].ToString() + "] as '" + dr[1].ToString().Trim() + "',";
                sql += "[" + dr[0].ToString() + "] as Subject" + i + ",";
                maxmark += int.Parse(dr["MaxMark"].ToString());
                lstMin.Add(dr["MinMark"].ToString());
                lstMax.Add(dr["MaxMark"].ToString());
                sbject.Add(dr["Subject"].ToString());
                sbject.Add(dr["ID"].ToString());

              
            }

            if (subIds != string.Empty)
            {
                subIds = subIds.Substring(0, subIds.Length - 1);
                sql = sql.Substring(0, sql.Length - 1);



                int order = DB.ExecuteScalarQuery(" SELECT ExamOrder FROM dbo.ExamSetupMaster where CompId=" + CompID + " and BranchId=" + BranchID + " and ExamID=" + mExamId + " and ClassID=" + mCLassID);
                string sqlObtain = string.Empty;
                string sqlObtainName = string.Empty;
                if (order == 1)
                {
                    sqlObtain = "dbo.ExamResult.ObtainMarks1";
                    sqlObtainName = " ObtainMarks1";
                }
                if (order == 2)
                {
                    sqlObtain = "dbo.ExamResult.ObtainMarks2";
                    sqlObtainName = " ObtainMarks2";
                }
                if (order == 3)
                {
                    sqlObtain = "dbo.ExamResult.ObtainMarks3";
                    sqlObtainName = " ObtainMarks3";
                }
                if (order == 4)
                {
                    sqlObtain = "dbo.ExamResult.ObtainMarks4";
                    sqlObtainName = " ObtainMarks4";
                }




                if (CompID == 2 || CompID == 5 || CompID == 6 || CompID == 7)
                {
                    sqlMain = "SELECT RollNo,StudentName,Class,Section," + sql + " , 0.00 as Total,StudentId FROM (SELECT dbo.StudentSession.RollNo," + sqlObtain + ", " +
                          " dbo.StudentRegistration.FirstName + ' ' + ISNULL(dbo.StudentRegistration.LastName, '') AS StudentName, dbo.Class.ClassName AS Class, " +
                          " dbo.Section.SectionName AS Section,SubjectIDL1,dbo.StudentRegistration.StudentID FROM dbo.ExamResult INNER JOIN dbo.StudentRegistration ON dbo.ExamResult.StudentID = dbo.StudentRegistration.StudentID " +
                          " AND dbo.ExamResult.BranchID = dbo.StudentRegistration.BranchID AND dbo.ExamResult.CompID = dbo.StudentRegistration.CompID INNER JOIN " +
                          " dbo.StudentSession ON dbo.ExamResult.StudentID = dbo.StudentSession.StudentID AND " +
                          " dbo.ExamResult.BranchID = dbo.StudentSession.BranchID AND dbo.ExamResult.CompID = dbo.StudentSession.CompID AND dbo.ExamResult.SessionID = dbo.StudentSession.SessionID INNER JOIN " +
                          " dbo.Class ON dbo.StudentSession.ClassID = dbo.Class.ClassID INNER JOIN  dbo.ClassSetup ON dbo.StudentSession.ClassSetupID = dbo.ClassSetup.ClassSetupID " +
                          " AND dbo.StudentSession.ClassID = dbo.ClassSetup.ClassID INNER JOIN dbo.Section ON dbo.ClassSetup.SectionID = dbo.Section.SectionID " +
                          " WHERE dbo.ExamResult.ClassID =" + mCLassID + " and StudentSession.ClassSetupID=" + mClassSetupId +
                          " and ExamResult.BranchID=" + BranchID + " and ExamResult.CompID=" + CompID + " and dbo.StudentRegistration.TCGiven=0 and ExamResult.SessionID=" + SessionID +
                          " ) as s PIVOT ( max (" + sqlObtainName + ") FOR SubjectIDL1 IN (" + subIds + ") ) as pvt ";
                }
                if (CompID == 1 || CompID == 4)
                {
                    sqlMain = "SELECT RollNo,StudentName,Class,Section," + sql + "  , 0.00 as Total,StudentId FROM (SELECT dbo.StudentSession.RollNo," + sqlObtain + ", " +
                           " dbo.StudentRegistration.FirstName + ' ' + ISNULL(dbo.StudentRegistration.LastName, '') AS StudentName, dbo.Class.ClassName AS Class, " +
                           " dbo.Section.SectionName AS Section,SubjectIDL2,dbo.StudentRegistration.StudentID FROM dbo.ExamResult INNER JOIN dbo.StudentRegistration ON dbo.ExamResult.StudentID = dbo.StudentRegistration.StudentID " +
                           " AND dbo.ExamResult.BranchID = dbo.StudentRegistration.BranchID AND dbo.ExamResult.CompID = dbo.StudentRegistration.CompID INNER JOIN " +
                           " dbo.StudentSession ON dbo.ExamResult.StudentID = dbo.StudentSession.StudentID AND " +
                           " dbo.ExamResult.BranchID = dbo.StudentSession.BranchID AND dbo.ExamResult.CompID = dbo.StudentSession.CompID AND dbo.ExamResult.SessionID = dbo.StudentSession.SessionID INNER JOIN " +
                           " dbo.Class ON dbo.StudentSession.ClassID = dbo.Class.ClassID INNER JOIN  dbo.ClassSetup ON dbo.StudentSession.ClassSetupID = dbo.ClassSetup.ClassSetupID " +
                           " AND dbo.StudentSession.ClassID = dbo.ClassSetup.ClassID INNER JOIN dbo.Section ON dbo.ClassSetup.SectionID = dbo.Section.SectionID " +
                           " WHERE dbo.ExamResult.ClassID =" + mCLassID + " and StudentSession.ClassSetupID=" + mClassSetupId +
                           " and ExamResult.BranchID=" + BranchID + " and ExamResult.CompID=" + CompID + " and dbo.StudentRegistration.TCGiven=0 and ExamResult.SessionID=" + SessionID +
                           " ) as s PIVOT ( max (" + sqlObtainName + ") FOR SubjectIDL2 IN (" + subIds + ") ) as pvt ";
                }







                dt = DB.ExecuteQuery(sqlMain);
                //dt.Columns.Add("Total", typeof(double));
                //dt.Columns["Total"].ReadOnly = false;
                //dt.Columns["Total"].DefaultValue = 0.00;

                //dt.Columns.Add("Percentage", typeof(double));
                //dt.Columns["Percentage"].ReadOnly = false;
                //dt.Columns["Percentage"].DefaultValue = 0.00;

                //dt.Columns.Add("Rank", typeof(string));
                //dt.Columns["Rank"].ReadOnly = false;

                //dt.Columns.Add("IsAbsent", typeof(bool));
                //dt.Columns["IsAbsent"].ReadOnly = false;
                //dt.Columns["IsAbsent"].DefaultValue = false;

                //dt.Columns.Add("Select", typeof(bool));
                //dt.Columns["Select"].ReadOnly = false;
                //dt.Columns["Select"].DefaultValue = true;


                foreach (DataRow dRow in dt.Rows)
                {

                    ExamPrintDetail objDetail = new ExamPrintDetail();

                    objDetail.FillObjectFromDataRowSearch(dRow, order, CompID, i, maxmark);


                    //    objDetail.MaxMark = lstMin[dRow];
                     //   objDetail.MinMark = lstMax[dRow];

                    //if (objDetail.IsNew == true && !StudentList.Contains(objDetail.StudentId.ToString())) continue;

                    _Items.Add(objDetail);
                }
            }
            else {
               


            }

           



        }



        #endregion
        #region Method DECLARED REGION

        private void SetDefaultValue()
        {
            _ExamId = _ClassId;
            _SubjectIdL1 = _SubjectIdL2 = _SubjectIdL3 = -1;
        }
        private string RegularStudentlist(int ClasssetupID)
        {
            string Studentlist = string.Empty;

            string sql = "Select StudentID from SubjectOptAllot where ClassSetupID=" + ClasssetupID +  " ";
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



            foreach (ExamPrintDetail objItem in _Items)
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

            foreach (ExamPrintDetail objItem in _Items)
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
            foreach (ExamPrintDetail objItem in _Items)
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

            foreach (ExamPrintDetail objItem in _Items)
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


            foreach (ExamPrintDetail objItem in _Items)
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

            foreach (ExamPrintDetail objItem in _Items)
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

            foreach (ExamPrintDetail objItem in _Items)
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

    public class ExamPrintDetail
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
        private string _Class;
        private string _Section;
        private string _Subject1;
        private string _Subject2;
        private string _Subject3;

        private string _Subject4;
        private string _Subject5;
        private string _Subject6;
        private string _Subject7;
        private string _Subject8;
        private string _Subject9;
        private string _Subject10;
        private string _Subject11;
        private string _Subject12;
        private string _Subject13;
        private string _Subject14;
        private string _Subject15;
        private string _Subject16;
        private string _Subject17;
        private string _Subject18;
        private string _Subject19;
        private string _Subject20;
        private string _Subject21;
        private string _Subject22;
        private string _Subject23;
        private string _Subject24;
        private decimal _Total;
        private decimal _Percentage;
        private string _Rank;




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

        public string Class
        {
            get { return _Class; }
            set { _Class = value; }
        }

        public string Section
        {
            get { return _Section; }
            set { _Section = value; }
        }

        public string Subject1
        {
            get { return _Subject1; }
            set { _Subject1 = value; }
        }

        public string Subject2
        {
            get { return _Subject2; }
            set { _Subject2 = value; }
        }

        public string Subject3
        {
            get { return _Subject3; }
            set { _Subject3 = value; }
        }

        public string Subject4
        {
            get { return _Subject4; }
            set { _Subject4 = value; }
        }

        public string Subject5
        {
            get { return _Subject5; }
            set { _Subject5 = value; }
        }

        public string Subject6
        {
            get { return _Subject6; }
            set { _Subject6 = value; }
        }
        public string Subject7
        {
            get { return _Subject7; }
            set { _Subject7 = value; }
        }

        public string Subject8
        {
            get { return _Subject8; }
            set { _Subject8 = value; }
        }

        public string Subject9
        {
            get { return _Subject9; }
            set { _Subject9 = value; }
        }

        public string Subject10
        {
            get { return _Subject10; }
            set { _Subject10 = value; }
        }

        public string Subject11
        {
            get { return _Subject11; }
            set { _Subject11 = value; }
        }

        public string Subject12
        {
            get { return _Subject12; }
            set { _Subject12 = value; }
        }

        public string Subject13
        {
            get { return _Subject13; }
            set { _Subject13 = value; }
        }
        public string Subject14
        {
            get { return _Subject14; }
            set { _Subject14 = value; }
        }

        public string Subject15
        {
            get { return _Subject15; }
            set { _Subject15 = value; }
        }
        public string Subject16
        {
            get { return _Subject16; }
            set { _Subject16 = value; }
        }
        public string Subject17
        {
            get { return _Subject17; }
            set { _Subject17 = value; }
        }
        public string Subject18
        {
            get { return _Subject18; }
            set { _Subject18 = value; }
        }
        public string Subject19
        {
            get { return _Subject19; }
            set { _Subject19 = value; }
        }
        public string Subject20
        {
            get { return _Subject20; }
            set { _Subject20 = value; }
        }

        public string Subject21
        {
            get { return _Subject21; }
            set { _Subject21 = value; }
        }

        public string Subject22
        {
            get { return _Subject22; }
            set { _Subject22 = value; }
        }
        public decimal Total
        {
            get { return _Total; }
            set { _Total = value; }
        }
        public string Subject23
        {
            get { return _Subject23; }
            set { _Subject23 = value; }
        }

        public string Subject24
        {
            get { return _Subject24; }
            set { _Subject24 = value; }
        }

       
        public decimal Percentage
        {
            get { return _Percentage; }
            set { _Percentage = value; }
        }

        public string Rank
        {
            get { return _Rank; }
            set { _Rank = value; }
        }

        #endregion
        #region CONSTRUCTOR DECLARED REGION
        public ExamPrintDetail()
        {
            SetDefaultValue();
        }


        #endregion
        #region METHOD DECLARED REGION

        private void SetDefaultValue()
        {
            _MinMark = _MaxMark = 0;
            _Total = 0;
        }
        public void FillObjectFromDataRow(DataRow dr)
        {
            if (dr == null) return;
            _StudentId = int.Parse(dr["StudentId"].ToString());
            _RollNo = int.Parse(dr["RollNo"].ToString());
            _StudentName = dr["StudentName"].ToString();
        }
        public void FillObjectFromDataRowSearch(DataRow dr, int mOrder, int mCompid,int i,int maxmark)
        {
            double total, temp, max, max2, max3;
            max = 0;
            max2 = 0;
            max3 = 0;

            if (dr == null) return;
            _StudentId = int.Parse(dr["StudentId"].ToString());
            _RollNo = int.Parse(dr["RollNo"].ToString());
            _Class = dr["Class"].ToString();
            _Section = dr["Section"].ToString();
            _Total =decimal.Parse( dr["Total"].ToString());

            for (int j = 1; j <= i; j++)
            {


                if (j == 1)
                {
                    _Subject1 = dr["Subject1"].ToString();
                    if (dr["Subject1"].ToString() != "Ab" && dr["Subject1"].ToString() != "ML" && dr["Subject1"].ToString() != "N/A" && dr["Subject1"].ToString() != "L" && dr["Subject1"].ToString() != string.Empty)
                    {
                        _Total = decimal.Parse(dr["Subject1"].ToString());
                    }
                    CompaniWise(mCompid, j,dr, maxmark);
                }
                if (j == 2)
                {
                    _Subject2 = dr["Subject2"].ToString();
                    if (dr["Subject2"].ToString() != "Ab" && dr["Subject2"].ToString() != "ML" && dr["Subject2"].ToString() != "N/A" && dr["Subject2"].ToString() != "L" && dr["Subject2"].ToString() != string.Empty)
                    {
                        _Total = _Total + decimal.Parse(dr["Subject2"].ToString());
                    }
                }
                if (j == 3)
                {
                    _Subject3 = dr["Subject3"].ToString();
                    if (dr["Subject3"].ToString() != "Ab" && dr["Subject3"].ToString() != "ML" && dr["Subject3"].ToString() != "N/A" && dr["Subject3"].ToString() != "L" && dr["Subject3"].ToString() != string.Empty)
                    {
                        _Total = _Total + decimal.Parse(dr["Subject3"].ToString());
                    }
                }

                if (j == 4)
                {
                    _Subject4 = dr["Subject4"].ToString();
                    if (dr["Subject4"].ToString() != "Ab" && dr["Subject4"].ToString() != "ML" && dr["Subject4"].ToString() != "N/A" && dr["Subject4"].ToString() != "L" && dr["Subject4"].ToString() != string.Empty)
                    {
                        _Total = _Total + decimal.Parse(dr["Subject4"].ToString());
                    }
                }
                if (j == 5)
                {
                    _Subject5 = dr["Subject5"].ToString();
                    if (dr["Subject5"].ToString() != "Ab" && dr["Subject5"].ToString() != "ML" && dr["Subject5"].ToString() != "N/A" && dr["Subject5"].ToString() != "L" && dr["Subject5"].ToString() != string.Empty)
                    {
                        _Total = _Total + decimal.Parse(dr["Subject5"].ToString());
                    }
                }
                if (j == 6)
                {
                    _Subject6 = dr["Subject6"].ToString();
                    if (dr["Subject6"].ToString() != "Ab" && dr["Subject6"].ToString() != "ML" && dr["Subject6"].ToString() != "N/A" && dr["Subject6"].ToString() != "L" && dr["Subject6"].ToString() != string.Empty)
                    {
                        _Total = _Total + decimal.Parse(dr["Subject6"].ToString());
                    }

                }
                if (j == 7)
                {
                    _Subject7 = dr["Subject7"].ToString();
                    if (dr["Subject7"].ToString() != "Ab" && dr["Subject7"].ToString() != "ML" && dr["Subject7"].ToString() != "N/A" && dr["Subject7"].ToString() != "L" && dr["Subject7"].ToString() != string.Empty)
                    {
                        _Total = _Total + decimal.Parse(dr["Subject7"].ToString());
                    }
                }

                if (j == 8)
                {
                    _Subject8 = dr["Subject8"].ToString();
                    if (dr["Subject8"].ToString() != "Ab" && dr["Subject8"].ToString() != "ML" && dr["Subject8"].ToString() != "N/A" && dr["Subject8"].ToString() != "L" && dr["Subject8"].ToString() != string.Empty)
                    {
                        _Total = _Total + decimal.Parse(dr["Subject8"].ToString());
                    }
                }
                if (j == 9)
                {
                    _Subject9 = dr["Subject9"].ToString();
                    if (dr["Subject9"].ToString() != "Ab" && dr["Subject9"].ToString() != "ML" && dr["Subject9"].ToString() != "N/A" && dr["Subject9"].ToString() != "L" && dr["Subject9"].ToString() != string.Empty)
                    {
                        _Total = _Total + decimal.Parse(dr["Subject9"].ToString());
                    }
                }
                if (j == 10)
                {
                    _Subject10 = dr["Subject10"].ToString();
                    if (dr["Subject10"].ToString() != "Ab" && dr["Subject10"].ToString() != "ML" && dr["Subject10"].ToString() != "N/A" && dr["Subject10"].ToString() != "L" && dr["Subject10"].ToString() != string.Empty)
                    {
                        _Total = _Total + decimal.Parse(dr["Subject10"].ToString());
                    }
                }
                if (j == 11)
                {
                    _Subject11 = dr["Subject11"].ToString();
                    if (dr["Subject11"].ToString() != "Ab" && dr["Subject11"].ToString() != "ML" && dr["Subject11"].ToString() != "N/A" && dr["Subject11"].ToString() != "L" && dr["Subject11"].ToString() != string.Empty)
                    {
                        _Total = _Total + decimal.Parse(dr["Subject11"].ToString());
                    }
                }
                if (j == 12)
                {
                    _Subject12 = dr["Subject12"].ToString();
                    if (dr["Subject12"].ToString() != "Ab" && dr["Subject12"].ToString() != "ML" && dr["Subject12"].ToString() != "N/A" && dr["Subject12"].ToString() != "L" && dr["Subject12"].ToString() != string.Empty)
                    {
                        _Total = _Total + decimal.Parse(dr["Subject12"].ToString());
                    }
                }
                if (j == 13)
                {
                    _Subject13 = dr["Subject13"].ToString();
                    if (dr["Subject13"].ToString() != "Ab" && dr["Subject13"].ToString() != "ML" && dr["Subject13"].ToString() != "N/A" && dr["Subject13"].ToString() != "L" && dr["Subject13"].ToString() != string.Empty)
                    {
                        _Total = _Total + decimal.Parse(dr["Subject13"].ToString());
                    }
                }
                if (j == 14)
                {
                    _Subject14 = dr["Subject14"].ToString();
                    if (dr["Subject14"].ToString() != "Ab" && dr["Subject14"].ToString() != "ML" && dr["Subject14"].ToString() != "N/A" && dr["Subject14"].ToString() != "L" && dr["Subject14"].ToString() != string.Empty)
                    {
                        _Total = _Total + decimal.Parse(dr["Subject14"].ToString());
                    }
                }
                if (j == 15)
                {
                    _Subject15 = dr["Subject15"].ToString();
                    if (dr["Subject15"].ToString() != "Ab" && dr["Subject15"].ToString() != "ML" && dr["Subject15"].ToString() != "N/A" && dr["Subject15"].ToString() != "L" && dr["Subject15"].ToString() != string.Empty)
                    {
                        _Total = _Total + decimal.Parse(dr["Subject15"].ToString());
                    }
                }
                if (j == 16)
                {
                    _Subject16 = dr["Subject16"].ToString();
                    if (dr["Subject16"].ToString() != "Ab" && dr["Subject16"].ToString() != "ML" && dr["Subject16"].ToString() != "N/A" && dr["Subject16"].ToString() != "L" && dr["Subject16"].ToString() != string.Empty)
                    {
                        _Total = _Total + decimal.Parse(dr["Subject16"].ToString());
                    }
                }
                if (j == 17)
                {
                    _Subject17 = dr["Subject17"].ToString();
                    if (dr["Subject17"].ToString() != "Ab" && dr["Subject17"].ToString() != "ML" && dr["Subject17"].ToString() != "N/A" && dr["Subject17"].ToString() != "L" && dr["Subject17"].ToString() != string.Empty)
                    {
                        _Total = _Total + decimal.Parse(dr["Subject17"].ToString());
                    }
                }
                if (i == 18)
                {
                    _Subject18 = dr["Subject18"].ToString();
                    if (dr["Subject18"].ToString() != "Ab" && dr["Subject18"].ToString() != "ML" && dr["Subject18"].ToString() != "N/A" && dr["Subject18"].ToString() != "L" && dr["Subject18"].ToString() != string.Empty)
                    {
                        _Total = _Total + decimal.Parse(dr["Subject18"].ToString());
                    }
                }
                if (j == 19)
                {
                    _Subject19 = dr["Subject19"].ToString();
                    if (dr["Subject19"].ToString() != "Ab" && dr["Subject19"].ToString() != "ML" && dr["Subject19"].ToString() != "N/A" && dr["Subject19"].ToString() != "L" && dr["Subject19"].ToString() != string.Empty)
                    {
                        _Total = _Total + decimal.Parse(dr["Subject19"].ToString());
                    }
                }
                if (j == 20)
                {
                    _Subject20 = dr["Subject20"].ToString();
                    if (dr["Subject20"].ToString() != "Ab" && dr["Subject20"].ToString() != "ML" && dr["Subject20"].ToString() != "N/A" && dr["Subject20"].ToString() != "L" && dr["Subject20"].ToString() != string.Empty)
                    {
                        _Total = _Total + decimal.Parse(dr["Subject20"].ToString());
                    }
                }
                if (j == 21)
                {
                    _Subject21 = dr["Subject21"].ToString();
                    if (dr["Subject21"].ToString() != "Ab" && dr["Subject21"].ToString() != "ML" && dr["Subject21"].ToString() != "N/A" && dr["Subject21"].ToString() != "L" && dr["Subject21"].ToString() != string.Empty)
                    {
                        _Total = _Total + decimal.Parse(dr["Subject21"].ToString());
                    }
                }
                if (j == 22)
                {
                    _Subject22 = dr["Subject22"].ToString();
                    if (dr["Subject22"].ToString() != "Ab" && dr["Subject22"].ToString() != "ML" && dr["Subject22"].ToString() != "N/A" && dr["Subject22"].ToString() != "L" && dr["Subject22"].ToString() != string.Empty)
                    {
                        _Total = _Total + decimal.Parse(dr["Subject22"].ToString());
                    }
                }
                if (j == 23)
                {
                    _Subject23 = dr["Subject23"].ToString();
                    if (dr["Subject23"].ToString() != "Ab" && dr["Subject23"].ToString() != "ML" && dr["Subject23"].ToString() != "N/A" && dr["Subject23"].ToString() != "L" && dr["Subject23"].ToString() != string.Empty)
                    {
                        _Total = _Total + decimal.Parse(dr["Subject23"].ToString());
                    }
                }

                if (j == 24)
                {
                    _Subject24 = dr["Subject24"].ToString();
                    if (dr["Subject24"].ToString() != "Ab" && dr["Subject24"].ToString() != "ML" && dr["Subject24"].ToString() != "N/A" && dr["Subject24"].ToString() != "L" && dr["Subject24"].ToString() != string.Empty)
                    {
                        _Total = _Total + decimal.Parse(dr["Subject24"].ToString());
                    }
                }
            }

            _StudentName = dr["StudentName"].ToString();


                 //_Percentage = (_Total / maxmark) * 100;





        


            //if (mOrder == 1)
            //{
            //    _MinMark = int.Parse(dr["MinMark1"].ToString());
            //    _MaxMark = int.Parse(dr["MaxMark1"].ToString());
            //    _ObtainMarks = dr["ObtainMarks1"].ToString();
            //    _Grade = dr["Grade"].ToString();
            //}
            //if (mOrder == 2)
            //{
            //    _MinMark = int.Parse(dr["MinMark2"].ToString());
            //    _MaxMark = int.Parse(dr["MaxMark2"].ToString());
            //    _ObtainMarks = dr["ObtainMarks2"].ToString();
            //    _Grade = dr["ObtainGrade2"].ToString();
            //}
            //if (mOrder == 3)
            //{
            //    _MinMark = int.Parse(dr["MinMark3"].ToString());
            //    _MaxMark = int.Parse(dr["MaxMark3"].ToString());
            //    _ObtainMarks = dr["ObtainMarks3"].ToString();
            //    _Grade = dr["ObtainGrade3"].ToString();
            //}
            //if (mOrder == 4)
            //{
            //    _MinMark = int.Parse(dr["MinMark4"].ToString());
            //    _MaxMark = int.Parse(dr["MaxMark4"].ToString());
            //    _ObtainMarks = dr["ObtainMarks4"].ToString();
            //    _Grade = dr["ObtainGrade4"].ToString();
            //}
            //if (mOrder == 5)
            //{
            //    _MinMark = int.Parse(dr["MinMark5"].ToString());
            //    _MaxMark = int.Parse(dr["MaxMark5"].ToString());
            //    _ObtainMarks = dr["ObtainMarks5"].ToString();
            //    //_Grade = dr["ObtaiGrade5"].ToString();
            //}

            //if (mCompid == 7)
            //{

            //    if (mOrder == 6)
            //    {
            //        _MinMark = int.Parse(dr["MinMark6"].ToString());
            //        _MaxMark = int.Parse(dr["MaxMark6"].ToString());
            //        _ObtainMarks = dr["ObtainMarks6"].ToString();
            //        //_Grade = dr["ObtaiGrade5"].ToString();
            //    }

            //    if (mOrder == 7)
            //    {
            //        _MinMark = int.Parse(dr["MinMark7"].ToString());
            //        _MaxMark = int.Parse(dr["MaxMark7"].ToString());
            //        _ObtainMarks = dr["ObtainMarks7"].ToString();
            //        //_Grade = dr["ObtaiGrade5"].ToString();
            //    }

            //    if (mOrder == 8)
            //    {
            //        _MinMark = int.Parse(dr["MinMark8"].ToString());
            //        _MaxMark = int.Parse(dr["MaxMark8"].ToString());
            //        _ObtainMarks = dr["ObtainMarks8"].ToString();
            //        //_Grade = dr["ObtaiGrade5"].ToString();
            //    }
            //}
            //_IsAbsent = bool.Parse(dr["IsAbsent"].ToString());


        }


        #endregion

        public void CompaniWise(int mCompid,int j, DataRow dr,int maxmark) {

            double total, temp, max, max2, max3;
            max = 0;
            max2 = 0;
            max3 = 0;
            string IsAbsent="false";


            //#region SETRank&Total For Stxviear

            //if (mCompid == 4)
            //{
            //         total = 0; int Lessmaxmarks = 0; int MLMaxmarks = 0;
            //        MLMaxmarks = maxmark;

            //        for (int c = vsStudent.Cols["Section"].Index + 1; c <= vsStudent.Cols.Count - 7; c++)
            //        {
            //            if (vsStudent[i, c].ToString() != "Ab" && vsStudent[i, c].ToString() != "ML" && vsStudent[i, c].ToString() != "N/A" && vsStudent[i, c].ToString() != "L" && vsStudent[i, c].ToString() != string.Empty)
            //            {
            //                temp = 0;
            //                double.TryParse(vsStudent[i, c].ToString(), out temp);
            //                total += temp;
            //            }
            //            if (vsStudent[i, c].ToString() == "ML" || vsStudent[i, c].ToString() == "N/A")
            //            {
            //                Lessmaxmarks = int.Parse(lstMax[c - (vsStudent.Cols["Section"].Index + 1)].ToString());
            //                MLMaxmarks = MLMaxmarks - Lessmaxmarks;

            //            }

            //            if (vsStudent[i, c].ToString() == "Ab" || vsStudent[i, c].ToString() == "ML" || vsStudent[i, c].ToString() == "N/A" || vsStudent[i, c].ToString() == "L")
            //            {
            //                vsStudent[i, "IsAbsent"] = "True";
            //            }
            //            else if (double.Parse(vsStudent[i, c].ToString()) < int.Parse(lstMin[c - (vsStudent.Cols["Section"].Index + 1)].ToString()))
            //            {
            //                vsStudent[i, "IsAbsent"] = "True";
            //            }
            //        }
            //        if (total > max && vsStudent[i, "IsAbsent"].ToString() != "True")
            //            max = total;

            //        vsStudent[i, "Total"] = total;
            //        vsStudent[i, "Total"] = total;
            //        vsStudent[i, "Percentage"] = ((total / maxmark) * 100).ToString("0.00");
            //        vsStudent[i, "Select"] = false;
        

            //    //MessageBox.Show(max.ToString());
            //    for (int x = 1; x <= vsStudent.Rows.Count - 1; x++)
            //    {
            //        if (order == 1)
            //        {
            //            decimal totalmarks1 = Convert.ToDecimal(vsStudent[x, "Total"].ToString());

            //            DB.ExecuteQueryNoResult("update dbo.studentSession set TotalMarks1 ='" + vsStudent[x, "Total"].ToString() + "', IsAbsentforRank='" + vsStudent[x, "IsAbsent"].ToString() + "' where StudentID=" + vsStudent[x, "StudentID"].ToString() + " and " + Program.ZBranch);

            //        }


            //        if (order == 2)
            //        {
            //            string sqlcabsentorder2 = "select IsAbsentforRank from dbo.studentSession where  StudentID=" + vsStudent[x, "StudentID"].ToString() + " and " + Program.ZBranch;
            //            string RollNoorder2 = vsStudent[x, "RollNo"].ToString();
            //            DataRow drorder2 = DB.ExecuteSingleRow(sqlcabsentorder2); bool m2flagabsent = false; bool m2flagabsent2 = false; bool m2flagabsent3 = false;
            //            if (drorder2 != null)
            //                m2flagabsent2 = bool.Parse(drorder2["IsAbsentforRank"].ToString());

            //            if (vsStudent[x, "IsAbsent"].ToString() == "True")
            //                m2flagabsent3 = true;

            //            if (m2flagabsent2 || m2flagabsent3)
            //                m2flagabsent = true;

            //            DB.ExecuteQueryNoResult("update dbo.studentSession set TotalMarks2='" + vsStudent[x, "Total"].ToString() + "',IsAbsentforRank='" + m2flagabsent + "' where StudentID=" + vsStudent[x, "StudentID"].ToString() + " and " + Program.ZBranch);
            //        }

            //        if (order == 3)
            //        {

            //            string sqlabsentorder3 = "select IsAbsentforRank from dbo.studentSession where  StudentID=" + vsStudent[x, "StudentID"].ToString() + " and " + Program.ZBranch;
            //            string RollNoorder3 = vsStudent[x, "RollNo"].ToString();
            //            DataRow drorder3 = DB.ExecuteSingleRow(sqlabsentorder3); bool mflagabsent = false; bool mflagabsent2 = false; bool mflagabsent3 = false;
            //            if (drorder3 != null)
            //                mflagabsent2 = bool.Parse(drorder3["IsAbsentforRank"].ToString());

            //            if (vsStudent[x, "IsAbsent"].ToString() == "True")
            //                mflagabsent3 = true;

            //            if (mflagabsent2 || mflagabsent3)
            //                mflagabsent = true;


            //            DB.ExecuteQueryNoResult("update dbo.studentSession set TotalMarks3='" + vsStudent[x, "Total"].ToString() + "',IsAbsentforRank='" + mflagabsent + "' where StudentID=" + vsStudent[x, "StudentID"].ToString() + " and " + Program.ZBranch);

            //        }
            //    }




            //    vsStudent.Sort(SortFlags.Descending, vsStudent.Cols["Total"].Index);
            //    decimal lastMark = -1;
            //    int currentRank = 0; int CountRank = 0; bool flag = false;
            //    string blankReank = string.Empty; string Division = string.Empty;

            //    for (int x = 1; x <= vsStudent.Rows.Count - 1; x++)
            //    {
            //        decimal currentMark = Convert.ToDecimal(vsStudent[x, "Total"].ToString());

            //        if (currentMark == lastMark && vsStudent[x, "IsAbsent"].ToString() != "True")
            //        {
            //            CountRank++;
            //        }

            //        if (currentMark != lastMark && vsStudent[x, "IsAbsent"].ToString() != "True")
            //        {
            //            if (CountRank > 0)
            //            {
            //                currentRank += CountRank;
            //            }

            //            currentRank++;
            //            lastMark = currentMark;
            //            CountRank = 0;

            //        }


            //        if (currentRank <= 10)
            //        {
            //            vsStudent[x, "Rank"] = currentRank;

            //            if (vsStudent[x, "IsAbsent"].ToString() == "True")
            //            {
            //                vsStudent[x, "Rank"] = "";

            //            }

            //            if (order == 1)
            //                DB.ExecuteQueryNoResult("update dbo.studentSession set Rank1='" + vsStudent[x, "Rank"].ToString() + "' where StudentID=" + vsStudent[x, "StudentID"].ToString() + " and " + Program.ZBranch);

            //            if (order == 2)
            //                DB.ExecuteQueryNoResult("update dbo.studentSession set Rank2='" + vsStudent[x, "Rank"].ToString() + "' where StudentID=" + vsStudent[x, "StudentID"].ToString() + " and " + Program.ZBranch);

            //            if (order == 3)
            //                DB.ExecuteQueryNoResult("update dbo.studentSession set Rank3='" + vsStudent[x, "Rank"].ToString() + "' where StudentID=" + vsStudent[x, "StudentID"].ToString() + " and " + Program.ZBranch);
            //        }



            //        double percentage = Convert.ToDouble(vsStudent[x, "Percentage"].ToString());

            //        if (percentage >= 70 && vsStudent[x, "IsAbsent"].ToString() != "True")
            //        {
            //            Division = "I Division";
            //        }
            //        if ((percentage >= 55 && percentage < 70) && vsStudent[x, "IsAbsent"].ToString() != "True")
            //        {
            //            Division = "II Division";
            //        }

            //        if ((percentage >= 40 && percentage < 55) && vsStudent[x, "IsAbsent"].ToString() != "True")
            //        {
            //            Division = "III Division";
            //        }

            //        if (vsStudent[x, "IsAbsent"].ToString() == "True")
            //        {
            //            Division = "";
            //        }


            //        vsStudent[x, "Division"] = Division;



            //    }
            //    vsStudent.Sort(SortFlags.Ascending, vsStudent.Cols["RollNo"].Index);


            //}
            //#endregion SETRank&Total For Nirmala


            //#region SETRank&Total For StLawrence
            //int TotalSelectStudent = vsStudent.Rows.Count - 1;

            //if (Program.COMPANY_ID == 6)
            //{
            //    for (int i = 1; i <= vsStudent.Rows.Count - 1; i++)
            //    {
            //        total = 0; int Lessmaxmarks = 0; int MLMaxmarks = 0;
            //        MLMaxmarks = maxmark;

            //        for (int c = vsStudent.Cols["Section"].Index + 1; c <= vsStudent.Cols.Count - 7; c++)
            //        {
            //            if (vsStudent[i, c].ToString() != "Ab" && vsStudent[i, c].ToString() != "ML" && vsStudent[i, c].ToString() != "N/A" && vsStudent[i, c].ToString() != "L" && vsStudent[i, c].ToString() != string.Empty)
            //            {
            //                temp = 0;
            //                double.TryParse(vsStudent[i, c].ToString(), out temp);
            //                total += temp;
            //            }
            //            if (vsStudent[i, c].ToString() == "ML" || vsStudent[i, c].ToString() == "N/A")
            //            {
            //                Lessmaxmarks = int.Parse(lstMax[c - (vsStudent.Cols["Section"].Index + 1)].ToString());
            //                MLMaxmarks = MLMaxmarks - Lessmaxmarks;

            //            }

            //            if (vsStudent[i, c].ToString() == "Ab" || vsStudent[i, c].ToString() == "ML" || vsStudent[i, c].ToString() == "N/A" || vsStudent[i, c].ToString() == "L")
            //            {
            //                vsStudent[i, "IsAbsent"] = "True";
            //            }
            //            else if (double.Parse(vsStudent[i, c].ToString()) < int.Parse(lstMin[c - (vsStudent.Cols["Section"].Index + 1)].ToString()))
            //            {
            //                vsStudent[i, "IsAbsent"] = "True";
            //            }
            //        }
            //        if (total > max && vsStudent[i, "IsAbsent"].ToString() != "True")
            //            max = total;

            //        vsStudent[i, "Total"] = total;
            //        vsStudent[i, "Total"] = total;
            //        vsStudent[i, "Percentage"] = ((total / maxmark) * 100).ToString("0.00");
            //        vsStudent[i, "Select"] = false;
            //    }


            //    //MessageBox.Show(max.ToString());
            //    for (int x = 1; x <= vsStudent.Rows.Count - 1; x++)
            //    {
            //        if (order == 1)
            //        {
            //            decimal totalmarks1 = Convert.ToDecimal(vsStudent[x, "Total"].ToString());

            //            DB.ExecuteQueryNoResult("update dbo.studentSession set TotalMarks1 ='" + vsStudent[x, "Total"].ToString() + "', IsAbsentforRank='" + vsStudent[x, "IsAbsent"].ToString() + "' where StudentID=" + vsStudent[x, "StudentID"].ToString() + " and " + Program.ZBranch);

            //        }


            //        if (order == 2)
            //        {
            //            string sqlcabsentorder2 = "select IsAbsentforRank from dbo.studentSession where  StudentID=" + vsStudent[x, "StudentID"].ToString() + " and " + Program.ZBranch;
            //            string RollNoorder2 = vsStudent[x, "RollNo"].ToString();
            //            DataRow drorder2 = DB.ExecuteSingleRow(sqlcabsentorder2); bool m2flagabsent = false; bool m2flagabsent2 = false; bool m2flagabsent3 = false;
            //            if (drorder2 != null)
            //                m2flagabsent2 = bool.Parse(drorder2["IsAbsentforRank"].ToString());

            //            if (vsStudent[x, "IsAbsent"].ToString() == "True")
            //                m2flagabsent3 = true;

            //            if (m2flagabsent2 || m2flagabsent3)
            //                m2flagabsent = true;

            //            DB.ExecuteQueryNoResult("update dbo.studentSession set TotalMarks2='" + vsStudent[x, "Total"].ToString() + "',IsAbsentforRank='" + m2flagabsent + "' where StudentID=" + vsStudent[x, "StudentID"].ToString() + " and " + Program.ZBranch);
            //        }

            //        if (order == 3)
            //        {

            //            string sqlabsentorder3 = "select IsAbsentforRank from dbo.studentSession where  StudentID=" + vsStudent[x, "StudentID"].ToString() + " and " + Program.ZBranch;
            //            string RollNoorder3 = vsStudent[x, "RollNo"].ToString();
            //            DataRow drorder3 = DB.ExecuteSingleRow(sqlabsentorder3); bool mflagabsent = false; bool mflagabsent2 = false; bool mflagabsent3 = false;
            //            if (drorder3 != null)
            //                mflagabsent2 = bool.Parse(drorder3["IsAbsentforRank"].ToString());

            //            if (vsStudent[x, "IsAbsent"].ToString() == "True")
            //                mflagabsent3 = true;

            //            if (mflagabsent2 || mflagabsent3)
            //                mflagabsent = true;


            //            DB.ExecuteQueryNoResult("update dbo.studentSession set TotalMarks3='" + vsStudent[x, "Total"].ToString() + "',IsAbsentforRank='" + mflagabsent + "' where StudentID=" + vsStudent[x, "StudentID"].ToString() + " and " + Program.ZBranch);

            //        }
            //    }




            //    vsStudent.Sort(SortFlags.Descending, vsStudent.Cols["Total"].Index);
            //    decimal lastMark = -1;
            //    int currentRank = 0; int CountRank = 0; bool flag = false;
            //    string blankReank = string.Empty; string Division = string.Empty;

            //    for (int x = 1; x <= vsStudent.Rows.Count - 1; x++)
            //    {
            //        decimal currentMark = Convert.ToDecimal(vsStudent[x, "Total"].ToString());

            //        if (currentMark == lastMark && vsStudent[x, "IsAbsent"].ToString() != "True")
            //        {
            //            CountRank++;
            //        }

            //        if (currentMark != lastMark && vsStudent[x, "IsAbsent"].ToString() != "True")
            //        {
            //            if (CountRank > 0)
            //            {
            //                currentRank += CountRank;
            //            }

            //            currentRank++;
            //            lastMark = currentMark;
            //            CountRank = 0;

            //        }


            //        if (currentRank <= 10)
            //        {
            //            vsStudent[x, "Rank"] = currentRank;

            //            if (vsStudent[x, "IsAbsent"].ToString() == "True")
            //            {
            //                vsStudent[x, "Rank"] = "";

            //            }

            //            if (order == 1)
            //                DB.ExecuteQueryNoResult("update dbo.studentSession set Rank1='" + vsStudent[x, "Rank"].ToString() + "' where StudentID=" + vsStudent[x, "StudentID"].ToString() + " and " + Program.ZBranch);

            //            if (order == 2)
            //                DB.ExecuteQueryNoResult("update dbo.studentSession set Rank2='" + vsStudent[x, "Rank"].ToString() + "' where StudentID=" + vsStudent[x, "StudentID"].ToString() + " and " + Program.ZBranch);

            //            if (order == 3)
            //                DB.ExecuteQueryNoResult("update dbo.studentSession set Rank3='" + vsStudent[x, "Rank"].ToString() + "' where StudentID=" + vsStudent[x, "StudentID"].ToString() + " and " + Program.ZBranch);
            //        }



            //        double percentage = Convert.ToDouble(vsStudent[x, "Percentage"].ToString());

            //        if (percentage >= 70 && vsStudent[x, "IsAbsent"].ToString() != "True")
            //        {
            //            Division = "I Division";
            //        }
            //        if ((percentage >= 55 && percentage < 70) && vsStudent[x, "IsAbsent"].ToString() != "True")
            //        {
            //            Division = "II Division";
            //        }

            //        if ((percentage >= 40 && percentage < 55) && vsStudent[x, "IsAbsent"].ToString() != "True")
            //        {
            //            Division = "III Division";
            //        }

            //        if (vsStudent[x, "IsAbsent"].ToString() == "True")
            //        {
            //            Division = "";
            //        }


            //        vsStudent[x, "Division"] = Division;



            //    }
            //    vsStudent.Sort(SortFlags.Ascending, vsStudent.Cols["RollNo"].Index);


            //}
            //#endregion SETRank&Total For Nirmala



            #region SETRank&Total For Nirmala

            if (mCompid == 1)
            {

                total = 0;

                if (dr["Subject" + j + ""].ToString() != "Ab" && dr["Subject" + j + ""].ToString() != "ML" && dr["Subject" + j + ""].ToString() != "N/A" && dr["Subject" + j + ""].ToString() != "L" && dr["Subject" + j + ""].ToString() != string.Empty)
                {
                    temp = 0;
                    double.TryParse(dr["Subject" + j + ""].ToString(), out temp);
                    total += temp;
                }


                if (dr["Subject" + j + ""].ToString() == "Ab" || dr["Subject" + j + ""].ToString() == "N/A" || dr["Subject" + j + ""].ToString() == "ML" || dr["Subject" + j + ""].ToString() == "L")
                {
                    IsAbsent = "True";
                }
                //else if (double.Parse(vsStudent[i, c].ToString()) < int.Parse(lstMin[c - (vsStudent.Cols["Section"].Index + 1)].ToString()))
                //{
                //    IsAbsent = "True";
                //}

                if (total > max && IsAbsent != "True")
                    max = total;

                //vsStudent[i, "Total"] = total;
                //vsStudent[i, "Total"] = total;
                //_Percentage = ((total / maxmark) * 100).ToString("0.00");

                _Percentage = (_Total / maxmark) * 100;
                //vsStudent[i, "Select"] = false;



                //MessageBox.Show(max.ToString());

                DataRow drDiv = null;
                //DataTable dtdivision = DB.ExecuteQuery("SELECT  Division, PFrom, PTo FROM  dbo.DivisionMaster where " + Program.ZBranch + "");

                string Division = string.Empty;

                //for (int x = 1; x <= vsStudent.Rows.Count - 1; x++)
                //{
                //    decimal percentage = Convert.ToDecimal(vsStudent[x, "Percentage"].ToString());

                //    Division = "";
                //    for (int R = 0; R <= dtdivision.Rows.Count - 1; R++)
                //    {
                //        drDiv = dtdivision.Rows[R];

                //        if (percentage >= Convert.ToDecimal(drDiv["PFrom"].ToString()) && percentage < Convert.ToDecimal(drDiv["PTo"].ToString()))
                //        {
                //            Division = drDiv["Division"].ToString();
                //        }


                //    }


                //    //if (percentage >= 60 && vsStudent[x, "IsAbsent"].ToString() != "True")
                //    //{
                //    //    Division = "I Division";
                //    //}
                //    //if ((percentage >= 45 && percentage < 60) && vsStudent[x, "IsAbsent"].ToString() != "True")
                //    //{
                //    //    Division = "II Division";
                //    //}

                //    //if ((percentage >= 40 && percentage < 45) && vsStudent[x, "IsAbsent"].ToString() != "True")
                //    //{
                //    //    Division = "III Division";
                //    //}

                //    if (vsStudent[x, "IsAbsent"].ToString() == "True")
                //    {
                //        Division = "";
                //    }

                //    vsStudent[x, "Division"] = Division;



                //}





            }
            #endregion SETRank&Total For Nirmala




        }

    }
}