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
    public partial class TeacherDetails : System.Web.UI.Page
    {
        ReportDocument rptDoc;
        protected void Page_Load(object sender, EventArgs e)
        {
            string TeacherID = Request.QueryString["TeacherID"];
            int IsAllSelect =int.Parse(Request.QueryString["IsAllSelect"]);

            try
            {
                rptDoc = new ReportDocument();
                string sql = string.Empty;
                string mPath = string.Empty;
                sql = " SELECT vTeacherDataExport.*FROM dbo.vTeacherDataExport " +
                     " Where  SessionID=" + int.Parse(Session["SessionID"].ToString()) + "  and CompID=" + byte.Parse(Session["CompID"].ToString()) + " AND BranchID=" + byte.Parse(Session["BranchID"].ToString());
                if(IsAllSelect== 0)
                sql = sql + "and TeacherID in (" + TeacherID + ")";

                #region 

                mPath = Server.MapPath("~/Reports/CryTeacherDetails.rpt");

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