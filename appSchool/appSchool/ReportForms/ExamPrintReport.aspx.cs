using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using appSchool.DataSet;
using appSchool.ViewModels;
using CrystalDecisions.CrystalReports.Engine;
using System.Configuration;
using System.Data.SqlClient;
using appSchool.App_Code.BL;

namespace appSchool.ReportForms
{
    public partial class ExamPrintReport : System.Web.UI.Page
    {
        ReportDocument rptDoc;
        protected void Page_Load(object sender, EventArgs e)
        {
            string studentIds = Request.QueryString["StudentId"];
            int ClassID = int.Parse(Request.QueryString["ClassID"]);
            int ExamID = int.Parse(Request.QueryString["ExamID"]);
            int ClassSetupID = int.Parse(Request.QueryString["ClassSetupID"]);
            int SESSION_ID = int.Parse(Session["SessionID"].ToString());
            int COMPANY_ID = int.Parse(Session["CompID"].ToString());
            int BRANCH_ID = int.Parse(Session["BranchID"].ToString());
            string classValue = Request.QueryString["classValue"].ToString();

            try
            {
                rptDoc = new ReportDocument();
                string sql = string.Empty;
                string mPath = string.Empty;
                string sqlMaster = string.Empty;
                string sqlMasterAttndnceTerm2 = string.Empty;
                string sqlMasterAttndnceTerm3 = string.Empty;
                string sqlMasterAttndnceTerm4 = string.Empty;

                string sqlDetail = string.Empty;
                string sqlDetail2 = string.Empty;
                string sqlDetail3 = string.Empty;
                string sqlmaxobtain = string.Empty;

                int order = DB.ExecuteScalarQuery(" SELECT ExamOrder FROM dbo.ExamSetupMaster where CompId=" + COMPANY_ID + " and ExamID=" + ExamID + " and ClassID=" + ClassID);



                if (order == 1)
                    mPath = "~/Reports/Term1";
                if (order == 2)
                    mPath = "~/Reports/Term2";
                if (order == 3)
                    mPath = "~/Reports/Term3";
                if (order == 4)
                    mPath = "~/Reports/Term4";

                if (COMPANY_ID == 1) // nirmala 
                {
                    switch (classValue)
                    {
                        case "L.K.G":
                        case "U.K.G":
                            mPath += @"/MarkSheetUKG.rpt";
                            break;
                        case "K.G":
                            mPath += @"/MarkSheetKG.rpt";
                            break;
                        case "1":
                            mPath += @"/MarkSheet1.rpt";
                            break;
                        case "2":
                            mPath += @"/MarkSheet2.rpt";
                            break;
                        case "3":
                            mPath += @"/MarkSheet3.rpt";
                            break;
                        case "9":
                        case "10":
                            mPath += @"/MarkSheet9-10.rpt";
                            break;
                        case "4":
                            mPath += @"/MarkSheet4.rpt";
                            break;
                        case "5":
                            mPath += @"/MarkSheet5.rpt";
                            break;
                        case "6":
                        case "7":
                        case "8":
                            mPath += @"/MarkSheet6-8.rpt";
                            break;
                        case "11 Art":
                        case "12 Art":
                            mPath += @"/MarkSheet11art-12art.rpt";
                            break;
                        case "11 Computer":
                        case "12 Computer":
                            mPath += @"/MarkSheet11computer-12computer.rpt";
                            break;
                        case "11 (Bio,English)":
                        case "12 Bio,English":
                            mPath += @"/MarkSheet11bioenglish-12bioenglish.rpt";
                            break;
                        case "11 (Bio,Comp)":
                        case "12 Bio,Computer":
                            mPath += @"/MarkSheet11biocomputer-12biocomputer.rpt";
                            break;
                        case "11 (Math,English)":
                        case "12 Maths,English":
                            mPath += @"/MarkSheet11mathsenglish-12mathenglish.rpt";
                            break;
                        case "11 (Math,Comp)":
                        case "12 Maths,Computer":
                            mPath += @"/MarkSheet11MathsComputer-12MathsComputer.rpt";
                            break;





                    }

                }
                if (COMPANY_ID == 2) //st mark main new condition
                {
                    switch (classValue)
                    {
                        case "Nursery":
                        case "L.K.G":
                            mPath += @"/MarkSheetLKG.rpt";
                            break;
                        case "U.K.G":
                            mPath += @"/MarkSheetUKG.rpt";
                            break;
                        case "I":
                        case "II":
                        case "III":
                        case "IV":
                        case "V":
                            mPath += @"/MarkSheet1-5.rpt";
                            break;
                        case "VI":
                        case "VII":
                        case "VIII":
                            mPath += @"/MarkSheet6-8.rpt";
                            break;
                        case "IX":
                        case "X":
                            mPath += @"/MarkSheet9-10.rpt";
                            break;
                        case "XI - Maths":
                        case "XI - Bio":
                        case "XI - Com.":
                            mPath += @"/MarkSheet11.rpt";
                            break;
                        case "XII - Maths":
                        case "XII - Bio":
                        case "XII - Com.":
                            mPath += @"/MarkSheet12.rpt";
                            break;
                    }
                }


                //#region StMark Babina before 15-07-2018 (its only for 2018-2019 session  )
                //if (Program.COMPANY_ID == 2) // old condition && Program.COMP_NAME == "St. Mark's School"
                //{

                //    switch (cboClass.Text.ToString())
                //    {
                //        case "Nursery":
                //        case "L.K.G":
                //        case "U.K.G":
                //            mPath += @"/MarkSheetLKG.rpt";
                //            break;
                //        case "I":
                //        case "II":
                //        case "III":
                //        case "IV":
                //        case "V":
                //            mPath += @"/MarkSheet1-5.rpt";
                //            break;
                //        case "VI":
                //        case "VII":
                //        case "VIII":
                //            mPath += @"/MarkSheet6-8.rpt";
                //            break;
                //        case "IX":
                //        case "X":
                //            mPath += @"/MarkSheet9-10.rpt";
                //            break;
                //        case "XI - Maths":
                //        case "XI - Bio":
                //        case "XI - Com.":
                //            mPath += @"/MarkSheet11.rpt";
                //            break;
                //        case "XII - Maths":
                //        case "XII - Bio":
                //        case "XII - Com.":
                //            mPath += @"/MarkSheet12.rpt";
                //            break;
                //    }
                //}
                //#endregion

                if (COMPANY_ID == 4) //st xvier
                {
                    switch (classValue)
                    {
                        case "L.K.G":
                        case "Nursery":
                            mPath += @"/MarkSheetNUR-LKG.rpt";
                            break;
                        case "U.K.G":
                            mPath += @"/MarkSheetUKG.rpt";
                            break;
                        case "I":
                        case "II":
                            mPath += @"/MarkSheet1-2.rpt";
                            break;
                        case "III":
                        case "IV":
                            mPath += @"/MarkSheet3-4.rpt";
                            break;
                        case "V":
                            mPath += @"/MarkSheet5.rpt";
                            break;
                        case "VI":
                        case "VII":
                        case "VIII":
                            mPath += @"/MarkSheet6-8.rpt";
                            break;
                        case "IX":
                        case "X":
                            mPath += @"/MarkSheet9-10.rpt";
                            break;
                        case "XI Maths":
                        case "XI Bio":
                            mPath += @"/MarkSheet11MATH-BIO.rpt";
                            break;
                        case "XII Maths":
                        case "XII Bio":
                            mPath += @"/MarkSheet12MATH-BIO.rpt";
                            break;

                    }

                }
                if (COMPANY_ID == 5) // st joseph
                {
                    switch (classValue)
                    {
                        case "Nursery":
                            mPath += @"/MarkSheetNursery.rpt";
                            break;
                        case "L.K.G":
                        case "U.K.G":
                            mPath += @"/MarkSheetLkg-UkG.rpt";
                            break;
                        case "I":
                        case "II":
                            mPath += @"/MarkSheet1-2.rpt";
                            break;
                        case "III":
                        case "IV":
                        case "V":
                            mPath += @"/MarkSheet3-5.rpt";
                            break;

                        case "VI":
                        case "VII":
                        case "VIII":
                            mPath += @"/MarkSheet6-8.rpt";
                            break;
                        case "IX":
                        case "X":
                            mPath += @"/MarkSheet9-10.rpt";
                            break;
                        case "XI Maths":
                        case "XI Bio":
                        case "XI Commerce":
                            mPath += @"/MarkSheet11Maths-Bio-Commerce.rpt";
                            break;

                        case "XII Maths":
                        case "XII Bio":
                        case "XII Commerce":
                            mPath += @"/MarkSheet12Maths-Bio-Commerce.rpt";
                            break;

                        case "XI Math,Hindi":
                        case "XI Bio,Hindi":
                        case "XI Commerce,Hindi":
                            mPath += @"/MarkSheet11Maths-Bio-CommerceHindi.rpt";
                            break;

                        case "XII Math,Hindi":
                        case "XII Bio,Hindi":
                        case "XII Commerce,Hindi":
                            mPath += @"/MarkSheet12Maths-Bio-CommerceHindi.rpt";
                            break;

                    }

                }

                if (COMPANY_ID == 6) // // st lawrence
                {
                    switch (classValue)
                    {
                        case "L.K.G.":
                        case "Nursery":
                            mPath += @"/MarkSheetNUR-LKG.rpt";
                            break;
                        case "U.K.G":
                            mPath += @"/MarkSheetUKG.rpt";
                            break;
                        case "I":
                        case "II":
                        case "III":
                        case "IV":
                            mPath += @"/MarkSheet1-4.rpt";
                            break;
                        case "V":
                            mPath += @"/MarkSheet5.rpt";
                            break;
                        case "VI":
                        case "VII":
                        case "VIII":
                            mPath += @"/MarkSheet6-8.rpt";
                            break;
                        case "IX":
                        case "X":
                            mPath += @"/MarkSheet9-10.rpt";
                            break;
                        case "XI Maths":
                        case "XI Bio":
                            mPath += @"/MarkSheet11MATH-BIO.rpt";
                            break;
                        case "XII Maths":
                        case "XII Bio":
                            mPath += @"/MarkSheet12MATH-BIO.rpt";
                            break;

                    }

                }
                if (COMPANY_ID == 7) // // Shanti Dham
                {
                    switch (classValue)
                    {
                        case "L.K.G.":
                        case "Nursery":
                            mPath += @"/MarkSheetNUR-LKG.rpt";
                            break;
                        case "U.K.G":
                            mPath += @"/MarkSheetUKG.rpt";
                            break;
                        case "I":
                        case "II":
                        case "III":
                        case "IV":
                            mPath += @"/MarkSheet1-4.rpt";
                            break;
                        case "V":
                            mPath += @"/MarkSheet5.rpt";
                            break;
                        case "VI":
                        case "VII":
                        case "VIII":
                            mPath += @"/MarkSheet6-8.rpt";
                            break;
                        case "IX":
                        case "X":
                            mPath += @"/MarkSheet9-10.rpt";
                            break;
                        case "XI Maths":
                        case "XI Bio":
                            mPath += @"/MarkSheet11MATH-BIO.rpt";
                            break;
                        case "11 Maths":
                        case "XII Bio":
                            mPath += @"/MarkSheet12MATH-BIO.rpt";
                            break;

                    }

                }


                mPath = Server.MapPath(mPath);

                try
                {
                    rptDoc.Load(mPath);
                }
                catch (Exception ex)
                {
                    Response.Write(ex.Message.ToString());
                    return;
                }



                //mPath = Server.MapPath("~/Reports/MarkSheetLKG.rpt");

                //if (chkExamAll.Checked == true)
                //    mPath = System.IO.Path.GetDirectoryName(Application.ExecutablePath).ToString() + @"\" + Program.DB_NAME + @"Reports";
                //else
                //{
                //    if (order == 1)
                //        mPath = System.IO.Path.GetDirectoryName(Application.ExecutablePath).ToString() + @"\" + Program.DB_NAME + @"Reports\Term1";
                //    if (order == 2)
                //        mPath = System.IO.Path.GetDirectoryName(Application.ExecutablePath).ToString() + @"\" + Program.DB_NAME + @"Reports\Term2";
                //    if (order == 3)
                //        mPath = System.IO.Path.GetDirectoryName(Application.ExecutablePath).ToString() + @"\" + Program.DB_NAME + @"Reports\Term3";
                //    if (order == 4)
                //        mPath = System.IO.Path.GetDirectoryName(Application.ExecutablePath).ToString() + @"\" + Program.DB_NAME + @"Reports\Term4";
                //}





                sqlMaster = "SELECT  dbo.Class.ClassName, dbo.Section.SectionName, dbo.StudentSession.RollNo,dbo.StudentSession.Rank, dbo.StudentSession.Rank1, dbo.StudentSession.Rank2, dbo.StudentSession.Rank3, ";

                if (COMPANY_ID == 2)
                    sqlMaster += " dbo.StudentSession.Rank4, ";

                if (COMPANY_ID == 4 || COMPANY_ID == 2 || COMPANY_ID == 5 || COMPANY_ID == 7) // new for stmark 28/06/2018
                    sqlMaster += "  dbo.StudentSession.OverallRank, ";

                sqlMaster += " dbo.StudentRegistration.EnrollmentNo,dbo.StudentRegistration.StudentID," +
                       "  dbo.StudentRegistration.FirstName + ' ' + ISNULL(dbo.StudentRegistration.LastName, '') AS StudentName,dbo.StudentRegistration.FatherName, dbo.StudentRegistration.DateOfBirth, " +
                       " dbo.ExamRemarkEntry.TeacherRemark, dbo.ExamRemarkEntry.PrincipalRemark, dbo.ExamRemarkEntry.Height, dbo.ExamRemarkEntry.Weight, " +
                       " dbo.ExamRemarkEntry.AttendanceTotal, dbo.ExamRemarkEntry.AttendancePresent , dbo.Session.SessionName FROM dbo.StudentRegistration INNER JOIN dbo.StudentSession ON " +
                       " dbo.StudentRegistration.StudentID = dbo.StudentSession.StudentID AND dbo.StudentRegistration.CompID = dbo.StudentSession.CompID AND " +
                       " dbo.StudentRegistration.BranchID = dbo.StudentSession.BranchID INNER JOIN dbo.ClassSetup ON dbo.StudentSession.ClassSetupID = dbo.ClassSetup.ClassSetupID " +
                       " AND dbo.StudentSession.ClassID = dbo.ClassSetup.ClassID AND dbo.StudentSession.BranchID = dbo.ClassSetup.BranchID AND dbo.StudentSession.CompID = dbo.ClassSetup.CompID " +
                       " INNER JOIN dbo.Class ON dbo.ClassSetup.ClassID = dbo.Class.ClassID AND dbo.ClassSetup.BranchID = dbo.Class.BranchID AND " +
                       " dbo.ClassSetup.CompID = dbo.Class.CompID INNER JOIN dbo.Section ON dbo.ClassSetup.SectionID = dbo.Section.SectionID " +
                       " AND dbo.ClassSetup.CompID = dbo.Section.CompID AND dbo.ClassSetup.BranchID = dbo.Section.BranchID INNER JOIN " +
                       " dbo.Session ON dbo.StudentSession.SessionID = dbo.Session.SessionId  LEFT OUTER JOIN " +
                       " dbo.ExamRemarkEntry ON dbo.StudentSession.StudentID = dbo.ExamRemarkEntry.StudentID AND dbo.StudentSession.SessionID = dbo.ExamRemarkEntry.SessionID " +
                       " AND dbo.StudentSession.ClassID = dbo.ExamRemarkEntry.ClassID AND dbo.StudentSession.CompID = dbo.ExamRemarkEntry.CompID AND dbo.StudentSession.BranchID = dbo.ExamRemarkEntry.BranchID "
                       + " and dbo.ExamRemarkEntry.ExamID=" + ExamID +
                       " where dbo.StudentRegistration.StudentId in (" + studentIds + ") and dbo.StudentSession.ClassSetupId=" + ClassSetupID + " and dbo.StudentSession.SessionId=" + SESSION_ID +
                       " AND dbo.StudentSession.CompID=" + COMPANY_ID + " AND dbo.StudentSession.BranchID=" + BRANCH_ID +
                       " order by dbo.StudentSession.RollNo ";





                if (COMPANY_ID == 1 || COMPANY_ID == 2 || COMPANY_ID == 4 || COMPANY_ID == 5 || COMPANY_ID == 6 || COMPANY_ID == 7)
                {
                    if (order == 2)  //----For StudentAttendance,Remark,Rank Term One&Two-----
                    {


                        ExamID = ExamID - 1;

                        sqlMasterAttndnceTerm2 = "SELECT  dbo.Class.ClassName, dbo.Section.SectionName, dbo.StudentSession.RollNo,dbo.StudentSession.Rank, dbo.StudentRegistration.EnrollmentNo,dbo.StudentRegistration.StudentID," +
                                " dbo.StudentRegistration.FirstName + ' ' + ISNULL(dbo.StudentRegistration.LastName, '') AS StudentName,dbo.StudentRegistration.FatherName, dbo.StudentRegistration.DateOfBirth, " +
                                " dbo.ExamRemarkEntry.TeacherRemark AS TeacherRemark1, dbo.ExamRemarkEntry.PrincipalRemark, dbo.ExamRemarkEntry.Height, dbo.ExamRemarkEntry.Weight, " +
                                " dbo.ExamRemarkEntry.AttendanceTotal AS AttendanceTotal1, dbo.ExamRemarkEntry.AttendancePresent AS AttendancePresent1 ,  dbo.Session.SessionName FROM dbo.StudentRegistration INNER JOIN dbo.StudentSession ON " +
                                " dbo.StudentRegistration.StudentID = dbo.StudentSession.StudentID AND dbo.StudentRegistration.CompID = dbo.StudentSession.CompID AND " +
                                " dbo.StudentRegistration.BranchID = dbo.StudentSession.BranchID INNER JOIN dbo.ClassSetup ON dbo.StudentSession.ClassSetupID = dbo.ClassSetup.ClassSetupID " +
                                " AND dbo.StudentSession.ClassID = dbo.ClassSetup.ClassID AND dbo.StudentSession.BranchID = dbo.ClassSetup.BranchID AND dbo.StudentSession.CompID = dbo.ClassSetup.CompID " +
                                " INNER JOIN dbo.Class ON dbo.ClassSetup.ClassID = dbo.Class.ClassID AND dbo.ClassSetup.BranchID = dbo.Class.BranchID AND " +
                                " dbo.ClassSetup.CompID = dbo.Class.CompID INNER JOIN dbo.Section ON dbo.ClassSetup.SectionID = dbo.Section.SectionID " +
                                " AND dbo.ClassSetup.CompID = dbo.Section.CompID AND dbo.ClassSetup.BranchID = dbo.Section.BranchID  INNER JOIN " +
                                 " dbo.Session ON dbo.StudentSession.SessionID = dbo.Session.SessionId   LEFT OUTER JOIN " +
                                " dbo.ExamRemarkEntry ON dbo.StudentSession.StudentID = dbo.ExamRemarkEntry.StudentID AND dbo.StudentSession.SessionID = dbo.ExamRemarkEntry.SessionID " +
                                " AND dbo.StudentSession.ClassID = dbo.ExamRemarkEntry.ClassID AND dbo.StudentSession.CompID = dbo.ExamRemarkEntry.CompID AND dbo.StudentSession.BranchID = dbo.ExamRemarkEntry.BranchID "
                                + " and dbo.ExamRemarkEntry.ExamID=" + ExamID + " " +
                                " where dbo.StudentRegistration.StudentId in (" + studentIds + ") and dbo.StudentSession.ClassSetupId=" + ClassSetupID + " and dbo.StudentSession.SessionId=" + SESSION_ID +
                                " AND dbo.StudentSession.CompID=" + COMPANY_ID + " AND dbo.StudentSession.BranchID=" + BRANCH_ID +
                                " order by dbo.StudentSession.RollNo ";

                    }

                    if (order == 3)  //----For StudentAttendance,Remark,Rank  Term One&Two&Three-----
                    {
                        int ExamID1 = ExamID;
                        ExamID1 = ExamID1 - 2;
                        int ExamID2 = ExamID;
                        ExamID2 = ExamID2 - 1;
                        sqlMasterAttndnceTerm2 = "SELECT  dbo.Class.ClassName, dbo.Section.SectionName, dbo.StudentSession.RollNo,dbo.StudentSession.Rank , dbo.StudentRegistration.EnrollmentNo,dbo.StudentRegistration.StudentID," +
                              " dbo.StudentRegistration.FirstName + ' ' + ISNULL(dbo.StudentRegistration.LastName, '') AS StudentName,dbo.StudentRegistration.FatherName, dbo.StudentRegistration.DateOfBirth, " +
                              " dbo.ExamRemarkEntry.TeacherRemark AS TeacherRemark1, dbo.ExamRemarkEntry.PrincipalRemark, dbo.ExamRemarkEntry.Height, dbo.ExamRemarkEntry.Weight, " +
                              " dbo.ExamRemarkEntry.AttendanceTotal AS AttendanceTotal1, dbo.ExamRemarkEntry.AttendancePresent AS AttendancePresent1  FROM dbo.StudentRegistration INNER JOIN dbo.StudentSession ON " +
                              " dbo.StudentRegistration.StudentID = dbo.StudentSession.StudentID AND dbo.StudentRegistration.CompID = dbo.StudentSession.CompID AND " +
                              " dbo.StudentRegistration.BranchID = dbo.StudentSession.BranchID INNER JOIN dbo.ClassSetup ON dbo.StudentSession.ClassSetupID = dbo.ClassSetup.ClassSetupID " +
                              " AND dbo.StudentSession.ClassID = dbo.ClassSetup.ClassID AND dbo.StudentSession.BranchID = dbo.ClassSetup.BranchID AND dbo.StudentSession.CompID = dbo.ClassSetup.CompID " +
                              " INNER JOIN dbo.Class ON dbo.ClassSetup.ClassID = dbo.Class.ClassID AND dbo.ClassSetup.BranchID = dbo.Class.BranchID AND " +
                              " dbo.ClassSetup.CompID = dbo.Class.CompID INNER JOIN dbo.Section ON dbo.ClassSetup.SectionID = dbo.Section.SectionID " +
                              " AND dbo.ClassSetup.CompID = dbo.Section.CompID AND dbo.ClassSetup.BranchID = dbo.Section.BranchID LEFT OUTER JOIN " +
                              " dbo.ExamRemarkEntry ON dbo.StudentSession.StudentID = dbo.ExamRemarkEntry.StudentID AND dbo.StudentSession.SessionID = dbo.ExamRemarkEntry.SessionID " +
                              " AND dbo.StudentSession.ClassID = dbo.ExamRemarkEntry.ClassID AND dbo.StudentSession.CompID = dbo.ExamRemarkEntry.CompID AND dbo.StudentSession.BranchID = dbo.ExamRemarkEntry.BranchID "
                              + " and dbo.ExamRemarkEntry.ExamID=" + ExamID1 + " " +
                              " where dbo.StudentRegistration.StudentId in (" + studentIds + ") and dbo.StudentSession.ClassSetupId=" + ClassSetupID + " and dbo.StudentSession.SessionId=" + SESSION_ID +
                              " AND dbo.StudentSession.CompID=" + COMPANY_ID + " AND dbo.StudentSession.BranchID=" + BRANCH_ID +
                              " order by dbo.StudentSession.RollNo ";




                        sqlMasterAttndnceTerm3 = "SELECT  dbo.Class.ClassName, dbo.Section.SectionName, dbo.StudentSession.RollNo,dbo.StudentSession.Rank, dbo.StudentRegistration.EnrollmentNo,dbo.StudentRegistration.StudentID," +
                                " dbo.StudentRegistration.FirstName + ' ' + ISNULL(dbo.StudentRegistration.LastName, '') AS StudentName, dbo.StudentRegistration.DateOfBirth, " +
                                " dbo.ExamRemarkEntry.TeacherRemark AS TeacherRemark2, dbo.ExamRemarkEntry.PrincipalRemark, dbo.ExamRemarkEntry.Height, dbo.ExamRemarkEntry.Weight, " +
                                " dbo.ExamRemarkEntry.AttendanceTotal AS AttendanceTotalTerm2, dbo.ExamRemarkEntry.AttendancePresent AS AttendancePresentTerm2 FROM dbo.StudentRegistration INNER JOIN dbo.StudentSession ON " +
                                " dbo.StudentRegistration.StudentID = dbo.StudentSession.StudentID AND dbo.StudentRegistration.CompID = dbo.StudentSession.CompID AND " +
                                " dbo.StudentRegistration.BranchID = dbo.StudentSession.BranchID INNER JOIN dbo.ClassSetup ON dbo.StudentSession.ClassSetupID = dbo.ClassSetup.ClassSetupID " +
                                " AND dbo.StudentSession.ClassID = dbo.ClassSetup.ClassID AND dbo.StudentSession.BranchID = dbo.ClassSetup.BranchID AND dbo.StudentSession.CompID = dbo.ClassSetup.CompID " +
                                " INNER JOIN dbo.Class ON dbo.ClassSetup.ClassID = dbo.Class.ClassID AND dbo.ClassSetup.BranchID = dbo.Class.BranchID AND " +
                                " dbo.ClassSetup.CompID = dbo.Class.CompID INNER JOIN dbo.Section ON dbo.ClassSetup.SectionID = dbo.Section.SectionID " +
                                " AND dbo.ClassSetup.CompID = dbo.Section.CompID AND dbo.ClassSetup.BranchID = dbo.Section.BranchID LEFT OUTER JOIN " +
                                " dbo.ExamRemarkEntry ON dbo.StudentSession.StudentID = dbo.ExamRemarkEntry.StudentID AND dbo.StudentSession.SessionID = dbo.ExamRemarkEntry.SessionID " +
                                " AND dbo.StudentSession.ClassID = dbo.ExamRemarkEntry.ClassID AND dbo.StudentSession.CompID = dbo.ExamRemarkEntry.CompID AND dbo.StudentSession.BranchID = dbo.ExamRemarkEntry.BranchID "
                                + " and dbo.ExamRemarkEntry.ExamID=" + ExamID2 + " " +
                                " where dbo.StudentRegistration.StudentId in (" + studentIds + ") and dbo.StudentSession.ClassSetupId=" + ClassSetupID + " and dbo.StudentSession.SessionId=" + SESSION_ID +
                                " AND dbo.StudentSession.CompID=" + COMPANY_ID + " AND dbo.StudentSession.BranchID=" + BRANCH_ID +
                                " order by dbo.StudentSession.RollNo ";

                    }
                    if (order == 4)  //----For StudentAttendance,Remark,Rank  Term One&Two&Three-----
                    {
                        int ExamID1 = ExamID;
                        ExamID1 = ExamID1 - 2;
                        int ExamID2 = ExamID;
                        ExamID2 = ExamID2 - 1;
                        int ExamID3 = ExamID;
                        ExamID3 = ExamID3 - 3;

                        sqlMasterAttndnceTerm2 = "SELECT  dbo.Class.ClassName, dbo.Section.SectionName, dbo.StudentSession.RollNo,dbo.StudentSession.Rank , dbo.StudentRegistration.EnrollmentNo,dbo.StudentRegistration.StudentID," +
                              " dbo.StudentRegistration.FirstName + ' ' + ISNULL(dbo.StudentRegistration.LastName, '') AS StudentName,dbo.StudentRegistration.FatherName, dbo.StudentRegistration.DateOfBirth, " +
                              " dbo.ExamRemarkEntry.TeacherRemark AS TeacherRemark1, dbo.ExamRemarkEntry.PrincipalRemark, dbo.ExamRemarkEntry.Height, dbo.ExamRemarkEntry.Weight, " +
                              " dbo.ExamRemarkEntry.AttendanceTotal AS AttendanceTotal1, dbo.ExamRemarkEntry.AttendancePresent AS AttendancePresent1  FROM dbo.StudentRegistration INNER JOIN dbo.StudentSession ON " +
                              " dbo.StudentRegistration.StudentID = dbo.StudentSession.StudentID AND dbo.StudentRegistration.CompID = dbo.StudentSession.CompID AND " +
                              " dbo.StudentRegistration.BranchID = dbo.StudentSession.BranchID INNER JOIN dbo.ClassSetup ON dbo.StudentSession.ClassSetupID = dbo.ClassSetup.ClassSetupID " +
                              " AND dbo.StudentSession.ClassID = dbo.ClassSetup.ClassID AND dbo.StudentSession.BranchID = dbo.ClassSetup.BranchID AND dbo.StudentSession.CompID = dbo.ClassSetup.CompID " +
                              " INNER JOIN dbo.Class ON dbo.ClassSetup.ClassID = dbo.Class.ClassID AND dbo.ClassSetup.BranchID = dbo.Class.BranchID AND " +
                              " dbo.ClassSetup.CompID = dbo.Class.CompID INNER JOIN dbo.Section ON dbo.ClassSetup.SectionID = dbo.Section.SectionID " +
                              " AND dbo.ClassSetup.CompID = dbo.Section.CompID AND dbo.ClassSetup.BranchID = dbo.Section.BranchID LEFT OUTER JOIN " +
                              " dbo.ExamRemarkEntry ON dbo.StudentSession.StudentID = dbo.ExamRemarkEntry.StudentID AND dbo.StudentSession.SessionID = dbo.ExamRemarkEntry.SessionID " +
                              " AND dbo.StudentSession.ClassID = dbo.ExamRemarkEntry.ClassID AND dbo.StudentSession.CompID = dbo.ExamRemarkEntry.CompID AND dbo.StudentSession.BranchID = dbo.ExamRemarkEntry.BranchID "
                              + " and dbo.ExamRemarkEntry.ExamID=" + ExamID1 + " " +
                              " where dbo.StudentRegistration.StudentId in (" + studentIds + ") and dbo.StudentSession.ClassSetupId=" + ClassSetupID + " and dbo.StudentSession.SessionId=" + SESSION_ID +
                              " AND dbo.StudentSession.CompID=" + COMPANY_ID + " AND dbo.StudentSession.BranchID=" + BRANCH_ID +
                              " order by dbo.StudentSession.RollNo ";




                        sqlMasterAttndnceTerm3 = "SELECT  dbo.Class.ClassName, dbo.Section.SectionName, dbo.StudentSession.RollNo,dbo.StudentSession.Rank, dbo.StudentRegistration.EnrollmentNo,dbo.StudentRegistration.StudentID," +
                                " dbo.StudentRegistration.FirstName + ' ' + ISNULL(dbo.StudentRegistration.LastName, '') AS StudentName, dbo.StudentRegistration.DateOfBirth, " +
                                " dbo.ExamRemarkEntry.TeacherRemark AS TeacherRemark2, dbo.ExamRemarkEntry.PrincipalRemark, dbo.ExamRemarkEntry.Height, dbo.ExamRemarkEntry.Weight, " +
                                " dbo.ExamRemarkEntry.AttendanceTotal AS AttendanceTotalTerm2, dbo.ExamRemarkEntry.AttendancePresent AS AttendancePresentTerm2 FROM dbo.StudentRegistration INNER JOIN dbo.StudentSession ON " +
                                " dbo.StudentRegistration.StudentID = dbo.StudentSession.StudentID AND dbo.StudentRegistration.CompID = dbo.StudentSession.CompID AND " +
                                " dbo.StudentRegistration.BranchID = dbo.StudentSession.BranchID INNER JOIN dbo.ClassSetup ON dbo.StudentSession.ClassSetupID = dbo.ClassSetup.ClassSetupID " +
                                " AND dbo.StudentSession.ClassID = dbo.ClassSetup.ClassID AND dbo.StudentSession.BranchID = dbo.ClassSetup.BranchID AND dbo.StudentSession.CompID = dbo.ClassSetup.CompID " +
                                " INNER JOIN dbo.Class ON dbo.ClassSetup.ClassID = dbo.Class.ClassID AND dbo.ClassSetup.BranchID = dbo.Class.BranchID AND " +
                                " dbo.ClassSetup.CompID = dbo.Class.CompID INNER JOIN dbo.Section ON dbo.ClassSetup.SectionID = dbo.Section.SectionID " +
                                " AND dbo.ClassSetup.CompID = dbo.Section.CompID AND dbo.ClassSetup.BranchID = dbo.Section.BranchID LEFT OUTER JOIN " +
                                " dbo.ExamRemarkEntry ON dbo.StudentSession.StudentID = dbo.ExamRemarkEntry.StudentID AND dbo.StudentSession.SessionID = dbo.ExamRemarkEntry.SessionID " +
                                " AND dbo.StudentSession.ClassID = dbo.ExamRemarkEntry.ClassID AND dbo.StudentSession.CompID = dbo.ExamRemarkEntry.CompID AND dbo.StudentSession.BranchID = dbo.ExamRemarkEntry.BranchID "
                                + " and dbo.ExamRemarkEntry.ExamID=" + ExamID2 + " " +
                                " where dbo.StudentRegistration.StudentId in (" + studentIds + ") and dbo.StudentSession.ClassSetupId=" + ClassSetupID + " and dbo.StudentSession.SessionId=" + SESSION_ID +
                                " AND dbo.StudentSession.CompID=" + COMPANY_ID + " AND dbo.StudentSession.BranchID=" + BRANCH_ID +
                                " order by dbo.StudentSession.RollNo ";

                        sqlMasterAttndnceTerm4 = "SELECT  dbo.Class.ClassName, dbo.Section.SectionName, dbo.StudentSession.RollNo,dbo.StudentSession.Rank, dbo.StudentRegistration.EnrollmentNo,dbo.StudentRegistration.StudentID," +
                               " dbo.StudentRegistration.FirstName + ' ' + ISNULL(dbo.StudentRegistration.LastName, '') AS StudentName, dbo.StudentRegistration.DateOfBirth, " +
                               " dbo.ExamRemarkEntry.TeacherRemark AS TeacherRemark2, dbo.ExamRemarkEntry.PrincipalRemark, dbo.ExamRemarkEntry.Height, dbo.ExamRemarkEntry.Weight, " +
                               " dbo.ExamRemarkEntry.AttendanceTotal AS AttendanceTotalTerm3, dbo.ExamRemarkEntry.AttendancePresent AS AttendancePresentTerm3 FROM dbo.StudentRegistration INNER JOIN dbo.StudentSession ON " +
                               " dbo.StudentRegistration.StudentID = dbo.StudentSession.StudentID AND dbo.StudentRegistration.CompID = dbo.StudentSession.CompID AND " +
                               " dbo.StudentRegistration.BranchID = dbo.StudentSession.BranchID INNER JOIN dbo.ClassSetup ON dbo.StudentSession.ClassSetupID = dbo.ClassSetup.ClassSetupID " +
                               " AND dbo.StudentSession.ClassID = dbo.ClassSetup.ClassID AND dbo.StudentSession.BranchID = dbo.ClassSetup.BranchID AND dbo.StudentSession.CompID = dbo.ClassSetup.CompID " +
                               " INNER JOIN dbo.Class ON dbo.ClassSetup.ClassID = dbo.Class.ClassID AND dbo.ClassSetup.BranchID = dbo.Class.BranchID AND " +
                               " dbo.ClassSetup.CompID = dbo.Class.CompID INNER JOIN dbo.Section ON dbo.ClassSetup.SectionID = dbo.Section.SectionID " +
                               " AND dbo.ClassSetup.CompID = dbo.Section.CompID AND dbo.ClassSetup.BranchID = dbo.Section.BranchID LEFT OUTER JOIN " +
                               " dbo.ExamRemarkEntry ON dbo.StudentSession.StudentID = dbo.ExamRemarkEntry.StudentID AND dbo.StudentSession.SessionID = dbo.ExamRemarkEntry.SessionID " +
                               " AND dbo.StudentSession.ClassID = dbo.ExamRemarkEntry.ClassID AND dbo.StudentSession.CompID = dbo.ExamRemarkEntry.CompID AND dbo.StudentSession.BranchID = dbo.ExamRemarkEntry.BranchID "
                               + " and dbo.ExamRemarkEntry.ExamID=" + ExamID3 + " " +
                               " where dbo.StudentRegistration.StudentId in (" + studentIds + ") and dbo.StudentSession.ClassSetupId=" + ClassSetupID + " and dbo.StudentSession.SessionId=" + SESSION_ID +
                               " AND dbo.StudentSession.CompID=" + COMPANY_ID + " AND dbo.StudentSession.BranchID=" + BRANCH_ID +
                               " order by dbo.StudentSession.RollNo ";

                    }

                }


                if (COMPANY_ID == 1 || COMPANY_ID == 2 || COMPANY_ID == 4 || COMPANY_ID == 5)
                {
                    
                    ExamMarkSheetSql objmarkshetsql = new ExamMarkSheetSql(studentIds.ToString(), ClassID.ToString(), ClassSetupID.ToString(), ExamID.ToString(), order, COMPANY_ID,SESSION_ID,BRANCH_ID);
                    objmarkshetsql.GetExamMarksheetSql();
                    sqlDetail = objmarkshetsql.SqlDetail1;
                    sqlDetail2 = objmarkshetsql.SqlDetail2;
                    sqlDetail3 = objmarkshetsql.SqlDetail3;
                }


                rptDoc.Database.Tables[0].SetDataSource(DB.ExecuteQuery(sqlMaster));
                rptDoc.Database.Tables[1].SetDataSource(DB.ExecuteQuery(sqlDetail));
                rptDoc.Database.Tables[2].SetDataSource(DB.ExecuteQuery(sqlDetail2));
                rptDoc.Database.Tables[3].SetDataSource(DB.ExecuteQuery(sqlDetail3));



                if (order == 2)
                {
                    rptDoc.Database.Tables[4].SetDataSource(DB.ExecuteQuery(sqlMasterAttndnceTerm2));
                }

                if (order == 3)
                {
                    rptDoc.Database.Tables[4].SetDataSource(DB.ExecuteQuery(sqlMasterAttndnceTerm2));
                    rptDoc.Database.Tables[5].SetDataSource(DB.ExecuteQuery(sqlMasterAttndnceTerm3));
                }


                if (COMPANY_ID == 2)
                {
                    //if (order == 4)
                    if (order == 4)
                    {
                        rptDoc.Database.Tables[4].SetDataSource(DB.ExecuteQuery(sqlMasterAttndnceTerm2));
                        rptDoc.Database.Tables[5].SetDataSource(DB.ExecuteQuery(sqlMasterAttndnceTerm3));
                        rptDoc.Database.Tables[6].SetDataSource(DB.ExecuteQuery(sqlMasterAttndnceTerm4));
                    }

                }

                CrystalReportViewer.ReportSource = rptDoc;


                


            }
            catch (Exception ex)
            {
                Response.Write(ex.Message.ToString());
            }

        }

        public void MsgBox(String ex, Page pg, Object obj)
        {
            string s = "<SCRIPT language='javascript'>alert('" + ex.Replace("\r\n", "\\n").Replace("'", "") + "'); </SCRIPT>";
            Type cstype = obj.GetType();
            ClientScriptManager cs = pg.ClientScript;
            cs.RegisterClientScriptBlock(cstype, s, s.ToString());
        }
        protected void Page_Unload(object sender, EventArgs e)
        {
            if (rptDoc != null)
            {
                rptDoc.Close();
                rptDoc.Dispose();
            }
        }
    }
}