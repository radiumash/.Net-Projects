using appSchool.DataSet;
using appSchool.ViewModels;
using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace appSchool.ReportForms
{
    public partial class HouseAllotmentExport : System.Web.UI.Page
    {
        ReportDocument rptDoc;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                string IsStudentID = Request.QueryString["StudentID"];
                string IsClassID = Request.QueryString["ClassID"];

                try
                {
                    rptDoc = new ReportDocument();
                    string sql = string.Empty;
                    string mPath = string.Empty;
                    sql = " SELECT vStudentDataExport.*FROM dbo.vStudentDataExport " +
                         " Where  ClassSetupID in (" + IsClassID + ") and TCGiven=0 AND SessionID=" + int.Parse(Session["SessionID"].ToString()) + "  and CompID=" + byte.Parse(Session["CompID"].ToString()) + " AND BranchID=" + byte.Parse(Session["BranchID"].ToString());
                    sql = sql + "and StudentID in (" + IsStudentID + ")";

                    #region 

                    mPath = Server.MapPath("~/Reports/CryHouseAllotment.rpt");
                
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
            catch (Exception ex)
            {

            }
            finally
            {
                //crystalReport.Close();
                // crystalReport.Dispose();
            }
        }

        public void MsgBox(String ex, Page pg, Object obj)
        {
            string s = "<SCRIPT language='javascript'>alert('" + ex.Replace("\r\n", "\\n").Replace("'", "") + "'); </SCRIPT>";
            Type cstype = obj.GetType();
            ClientScriptManager cs = pg.ClientScript;
            cs.RegisterClientScriptBlock(cstype, s, s.ToString());
        }
        //private DSStudentRegistration GetData(string query, string Constring)
        //{

        //    string conString = ConfigurationManager.ConnectionStrings[Constring].ConnectionString;
        //    SqlCommand cmd = new SqlCommand(query);
        //    using (SqlConnection con = new SqlConnection(conString))
        //    {
        //        using (SqlDataAdapter sda = new SqlDataAdapter())
        //        {
        //            cmd.Connection = con;

        //            sda.SelectCommand = cmd;

        //            using (DSStudentRegistration dsitems = new DSStudentRegistration())
        //            {
        //                //dsitems.Clear();
        //                sda.Fill(dsitems, "vStudentDataExport");
        //                return dsitems;
        //            }
        //        }
        //    }
        //}
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