using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CrystalDecisions.CrystalReports.Engine;
using System.Configuration;
using System.Data.SqlClient;
using appSchool.ViewModels;


namespace appSchool.ReportForms
{
    public partial class EmployeeAttendanceReport : System.Web.UI.Page
    {
        ReportDocument rptDoc;
        protected void Page_Load(object sender, EventArgs e)
        {
            string TeacherID = Request.QueryString["TeacherID"];
            int IsAllSelect = int.Parse(Request.QueryString["IsAllSelect"]);

            try
            {
                rptDoc = new ReportDocument();
                string sql = string.Empty;
                string mPath = string.Empty;
                sql = " SELECT vEmployeeattendancelist.*FROM dbo.vEmployeeattendancelist " +
                     " Where  SessionID=" + int.Parse(Session["SessionID"].ToString()) + "  and CompID=" + byte.Parse(Session["CompID"].ToString()) + " AND BranchID=" + byte.Parse(Session["BranchID"].ToString());
                if (IsAllSelect == 0)
                    sql = sql + "and TeacherID in (" + TeacherID + ")";

                #region 

                mPath = Server.MapPath("~/Reports/CryEmployeeAttendance.rpt");

                try
                {
                    rptDoc.Load(mPath);
                }
                catch (Exception ex)
                {
                    Response.Write(ex.Message.ToString());
                    return;
                }
                rptDoc.Database.Tables[0].SetDataSource(DB.ExecuteQuery(sql));

                CrystalReportViewer.ReportSource = rptDoc;


                #endregion
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message.ToString());
            }
        }
    }
}