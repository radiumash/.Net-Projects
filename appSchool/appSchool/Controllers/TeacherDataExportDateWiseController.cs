using appSchool.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DevExpress.Web.Mvc;
using appSchool.ViewModels;


namespace appSchool.Controllers
{
    //[NoCache]
    public class TeacherDataExportDateWiseController : Controller
    {
        private UnitOfWork unitOfWork = new UnitOfWork();

        public ActionResult Index()
        {

            if (Session["UserID"] == null || (int)SubMenuModules.appTeacherDataExportDateWise == 0)
            {

                return Redirect("~/");
            }

            //UserPermission objuser = new UserPermission();
            //objuser = unitOfWork.userPermissionService.CheckUserPermissionModulewise(int.Parse(Session["UserID"].ToString()), 45, byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()));
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


                Session["TeacherDataExportDateWise"] = null;

                return View("");
        }

        public ActionResult ListTeacherDataPartial()
        {
            if (Session["UserID"] == null) { return Redirect("~/"); }
            return PartialView("ListTeacherDataPartial");
        }

        public ActionResult ExportTo(string OutputFormat)
        {
            if (Session["UserID"] == null) { return Redirect("~/"); }
            var model = Session["TeacherDataExportDateWise"];

            switch (OutputFormat.ToUpper())
            {
                case "CSV":
                    return GridViewExtension.ExportToCsv(GridViewTeacherDataExportDateWise.ExportGridViewSettings, model);
                case "PDF":
                    return GridViewExtension.ExportToPdf(GridViewTeacherDataExportDateWise.ExportGridViewSettings, model);
                case "RTF":
                    return GridViewExtension.ExportToRtf(GridViewTeacherDataExportDateWise.ExportGridViewSettings, model);
                case "XLS":
                    return GridViewExtension.ExportToXls(GridViewTeacherDataExportDateWise.ExportGridViewSettings, model);
                case "XLSX":
                    return GridViewExtension.ExportToXlsx(GridViewTeacherDataExportDateWise.ExportGridViewSettings, model);
                default:
                    return RedirectToAction("Index");
            }
        }

        public ActionResult PartialClassSetupView()
        {
            if (Session["UserID"] == null) { return Redirect("~/"); }
            return PartialView("ListClassSetupPartial", unitOfWork.classSetupService.GetClassSetupList(byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));
        }

   


        public ActionResult GetPersonalInfoListClassWise()
        {
            if (Session["UserID"] == null) { return Redirect("~/"); }
            Session["TeacherDataExportDateWise"] = unitOfWork.vTeacherDataExportDateWiseService.GetTeacherListForDataExport(int.Parse(Session["SessionID"].ToString()), byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()));
            return PartialView("ListTeacherDataPartial", Session["TeacherDataExportDateWise"]);

        }

        public ActionResult GetEmployeeAtendanceList(DateTime newFromDate, DateTime newToDate)
        {
            if (Session["UserID"] == null)
            {
                return Redirect("~/");
            }
            ViewData["newFromDate"] = newFromDate;
            ViewData["newToDate"] = newToDate;


            List<vEmployeeattendancelist> list = unitOfWork.employeeAttendanceDailyservices.GetEmployeAbsentPresentListReport(newFromDate, newToDate, byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()), int.Parse(Session["SessionID"].ToString()));
            Session["TeacherDataExportDateWise"] = list;
            return PartialView("ListTeacherDataPartial", list);

        }

    }

}


public static class GridViewTeacherDataExportDateWise
{
  
    private static GridViewSettings exportGridViewSettings;
    //public static bool _PersonalInfo = false;
    //public static bool _ParentsInfo = false;
    //public static bool _GardianInfo = false;

    public static GridViewSettings ExportGridViewSettings
    {
        get
        {
            if (exportGridViewSettings == null)
                exportGridViewSettings = CreateExportGridViewSettingsForTeacherData();
            else
                exportGridViewSettings = CreateExportGridViewSettingsForTeacherData();
            return exportGridViewSettings;
        }
    }

    private static GridViewSettings CreateExportGridViewSettingsForTeacherData()
    {
        GridViewSettings settings = new GridViewSettings();
        
        settings.Name = "GridTeacherDataExportDateWise";
        settings.CallbackRouteValues = new { Controller = "TeacherDataExportDateWise", Action = "ListTeacherDataPartial" };

        settings.KeyFieldName = "TeacherID";
        settings.Settings.ShowFilterRow = true;
        settings.Width = System.Web.UI.WebControls.Unit.Percentage(100);
      

        settings.CommandColumn.Visible = true;
        settings.CommandColumn.ShowSelectCheckbox = true;

        settings.CommandColumn.Width = System.Web.UI.WebControls.Unit.Pixel(30);
        settings.CommandColumn.Caption =string.Empty;

        //settings.SettingsExport.ExportedRowType = DevExpress.Web.ASPxGridView.Export.GridViewExportedRowType.Selected;

        settings.Columns.Add(column =>
        {
            column.FieldName = "TeacherID";
            column.Visible = false;
        });
        settings.Columns.Add(column =>
        {
            column.FieldName = "AttendanceLogId";
            column.Visible = false;
        });

        settings.Columns.Add(column =>
        {
            column.FieldName = "EmployeeCode";
            column.Caption = "Employee Code";
            column.Width = System.Web.UI.WebControls.Unit.Pixel(150);
            column.Visible = true;
        });
        settings.Columns.Add(column =>
        {
            column.FieldName = "EmployeeName";
            column.Caption = "Employee Name";
            column.Width = System.Web.UI.WebControls.Unit.Pixel(350);
            column.Visible = true;
        });

        settings.Columns.Add(column =>
        {
            column.FieldName = "InTime";
            column.Caption = "In Time";
            column.Width = System.Web.UI.WebControls.Unit.Pixel(150);
            column.Visible = true;
        });

        settings.Columns.Add(column =>
        {
            column.FieldName = "OutTime";
            column.Caption = "Out Time";
            column.Width = System.Web.UI.WebControls.Unit.Pixel(150);
            column.Visible = true;
        });

        settings.Columns.Add(column =>
        {
            column.FieldName = "Status";
            column.Width = System.Web.UI.WebControls.Unit.Pixel(150);
            column.Visible = true;
        });

        settings.Columns.Add(column =>
        {
            column.FieldName = "TimeStatus";
            column.Caption = "Late By";
            column.Width = System.Web.UI.WebControls.Unit.Pixel(150);
            column.Visible = true;
        });


        settings.CommandColumn.ShowClearFilterButton = true;

        settings.SettingsPager.Position = System.Web.UI.WebControls.PagerPosition.Bottom;
        settings.SettingsPager.FirstPageButton.Visible = true;
        settings.SettingsPager.LastPageButton.Visible = true;
        settings.SettingsPager.PageSizeItemSettings.Visible = true;
        settings.SettingsPager.PageSize = 200;

        //settings.Settings.VerticalScrollBarMode = ScrollBarMode.Visible;
        //settings.Settings.HorizontalScrollBarMode = ScrollBarMode.Visible;
        settings.TotalSummary.Add(DevExpress.Data.SummaryItemType.Count, "FullName");
        settings.Settings.ShowFooter = true;
        settings.GroupSummary.Add(DevExpress.Data.SummaryItemType.Count, "FullName");
        settings.Settings.ShowGroupPanel = true;
      
        return settings;
    }
}