using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using appSchool.ViewModels;
using CrystalDecisions.CrystalReports.Engine;

namespace appSchool.ReportForms
{
    public partial class SubjectAllotmentReport : System.Web.UI.Page
    {
        ReportDocument rptDoc;
        protected void Page_Load(object sender, EventArgs e)
        {
            try {
                rptDoc = new ReportDocument();

                string sql = string.Empty;
                string mPath = string.Empty;
                

                sql = "SELECT TOP (100) PERCENT Class.ClassName, SubjectLevelOne.SubjectNameL1 AS MainSubject, SubjectLevelTwo.SubjectNameL2 AS SubSubject, " +
                  " SubjectLevelThree.SubjectNameL3 AS Subject, Class.DisplayOrder, SubjectCategory.SubjectCategoryName AS SubjectCategory " +
                  " FROM SubjectCategory RIGHT OUTER JOIN  SubjectLevelOne ON SubjectCategory.SubjectCategoryID = SubjectLevelOne.SubjectCategoryID RIGHT OUTER JOIN " +
                  " SubjectAllotment LEFT OUTER JOIN Class ON SubjectAllotment.ClassID = Class.ClassID LEFT OUTER JOIN SubjectLevelTwo ON SubjectAllotment.IDL2 = SubjectLevelTwo.IdL2 LEFT OUTER JOIN " +
                  " SubjectLevelThree ON SubjectAllotment.IDL3 = SubjectLevelThree.IdL3 ON SubjectLevelOne.IdL1 = SubjectAllotment.IDL1  " +
                  " where SubjectAllotment.BranchID="+ int.Parse(Session["BranchID"].ToString()) + " and SubjectAllotment.CompID="+ int.Parse(Session["CompID"].ToString()) +"";

                sql += " ORDER BY SubjectCategory.SubjectCategoryID,Class.DisplayOrder ";

                #region 

                mPath = Server.MapPath("~/Reports/SubjectAllotment.rpt");

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
                rptDoc.PrintOptions.PrinterName ="";
                rptDoc.PrintOptions.DissociatePageSizeAndPrinterPaperSize = true;
                rptDoc.ReportOptions.EnableSaveDataWithReport = false;
                rptDoc.ParameterFields["SchoolName"].CurrentValues.AddValue("");
                rptDoc.ParameterFields["SessionName"].CurrentValues.AddValue("");
                rptDoc.ParameterFields["Address"].CurrentValues.AddValue("");

                CrystalReportViewer.ReportSource = rptDoc;


                #endregion

            }
            catch (Exception ex) {
                Response.Write(ex.Message.ToString());
            }
        }
    }
}