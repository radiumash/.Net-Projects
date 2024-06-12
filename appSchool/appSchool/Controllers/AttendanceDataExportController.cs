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
    [NoCache]
    public class AttendanceDataExportController : Controller
    {
        private UnitOfWork unitOfWork = new UnitOfWork();

        public ActionResult Index()
        {
            if (Session["UserID"] == null || (int)SubMenuModules.appAttendanceDataExport == 0)
            {

                return Redirect("~/");
            }
            //UserPermission objuser = new UserPermission();
            //objuser = unitOfWork.userPermissionService.CheckUserPermissionModulewise(int.Parse(Session["UserID"].ToString()), 48, byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()));
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


                Session["AttendanceList"] = null;
            return View("");
        }

        public ActionResult ListAttendanceDataPartial()
        {
            return PartialView("ListAttendanceDataPartial");
        }

        public ActionResult ListPartialClassSetup()
        {
            return PartialView("ListClassGridLookupPartial", unitOfWork.classSetupService.GetClassSetupList(byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));
        }


        public ActionResult ExportTo(string OutputFormat)
        {
            var model = Session["AttendanceList"];

            switch (OutputFormat.ToUpper())
            {

                case "CSV":
                    return GridViewExtension.ExportToCsv(GridViewAttendanceDataExport.ExportGridViewSettings, model);
                case "PDF":
                    return GridViewExtension.ExportToPdf(GridViewAttendanceDataExport.ExportGridViewSettings, model);
                case "RTF":
                    return GridViewExtension.ExportToRtf(GridViewAttendanceDataExport.ExportGridViewSettings, model);
                case "XLS":
                    return GridViewExtension.ExportToXls(GridViewAttendanceDataExport.ExportGridViewSettings, model);
                case "XLSX":
                    return GridViewExtension.ExportToXlsx(GridViewAttendanceDataExport.ExportGridViewSettings, model);
                default:
                    return RedirectToAction("Index");
            }
        }

        public ActionResult PartialClassSetupView()
        {
            return PartialView("ListClassSetupPartial", unitOfWork.classSetupService.GetClassSetupList(byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));
        }
        public ActionResult PartialMonthListView()
        {
            return PartialView("ListMonthGridLookupPartial", cCommon.GetMonthNameList());
        }

        public ActionResult GetAttendanceListClassWise(string mClassesID)
        {

            ViewData["ClassSetupID"] = mClassesID;


            Session["AttendanceList"] = unitOfWork.vStudentDataExportService.GetStudentListByClassSetupIDs(mClassesID, int.Parse(Session["SessionID"].ToString()), byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()));
            return PartialView("ListAttendanceDataPartial", Session["AttendanceList"]);

        }


        public ActionResult GetAttendanceInfoListDatewise(string mClassesID, DateTime newFromDate, DateTime newToDate)
        {

            ViewData["ClassSetupID"] = mClassesID;

            Session["AttendanceList"] = unitOfWork.vAttendanceDataExportService.GetAttendanceListByClassSetupIDandDatewise(mClassesID, newFromDate, newToDate, int.Parse(Session["SessionID"].ToString()), byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()));
            return PartialView("ListAttendanceDataPartial", Session["AttendanceList"]);

        }



        public ActionResult GetAttendanceInfoListMonthwise(string mClassesID, string mMonthsID)
        {

            ViewData["ClassSetupID"] = mClassesID;
            ViewData["MonthID"] = mMonthsID;

            //GridViewStudentDataExport._PersonalInfo = true;
            //GridViewStudentDataExport._ParentsInfo = false;
            //GridViewStudentDataExport._GardianInfo = false;

            Session["AttendanceList"] = unitOfWork.vAttendanceDataExportService.GetAttendanceListByClassSetupIDandMonthWise(mClassesID, mMonthsID, int.Parse(Session["SessionID"].ToString()), byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()));
            return PartialView("ListAttendanceDataPartial", Session["AttendanceList"]);

        }







    }

}


public static class GridViewAttendanceDataExport
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
                exportGridViewSettings = CreateExportGridViewSettingsForAttendanceData();
            else
                exportGridViewSettings = CreateExportGridViewSettingsForAttendanceData();
            return exportGridViewSettings;
        }
    }

    private static GridViewSettings CreateExportGridViewSettingsForAttendanceData()
    {
        GridViewSettings settings = new GridViewSettings();
        
        settings.Name = "GridAttendanceDataExport";
        settings.CallbackRouteValues = new { Controller = "AttendanceDataExport", Action = "ListAttendanceDataPartial" };

        settings.KeyFieldName = "StudentAttendanceID";
        settings.Settings.ShowFilterRow = true;
        settings.Width = System.Web.UI.WebControls.Unit.Percentage(100);

        settings.CommandColumn.Visible = true;
        settings.CommandColumn.ShowSelectCheckbox = true;

        settings.CommandColumn.Width = System.Web.UI.WebControls.Unit.Pixel(30);
        settings.CommandColumn.Caption =string.Empty;

        //settings.SettingsExport.ExportedRowType = DevExpress.Web.ASPxGridView.Export.GridViewExportedRowType.Selected;

        settings.Columns.Add(column =>
        {
            column.FieldName = "StudentAttendanceID";
            column.Visible = false;
        });

        settings.Columns.Add(column =>
        {
            column.FieldName = "AttendanceDate";
            column.Caption = "Attendance Date";
            column.ColumnType = MVCxGridViewColumnType.DateEdit;
           
            //var dateProperties = column.PropertiesEdit as DateEditProperties;
            //dateProperties.UseMaskBehavior = true;
            //dateProperties.EditFormat = EditFormat.Custom;
            //dateProperties.EditFormatString = "MMMM dd, yyyy";
            //dateProperties.DisplayFormatString = "MMMM dd, yyyy";
            //dateProperties.MaxDate = DateTime.Now;
            //column.Width = System.Web.UI.WebControls.Unit.Pixel(150);
            //column.Visible = true;
            //column.EditFormSettings.ColumnSpan = 1;


        });
        
       
        settings.Columns.Add(column =>
        {
            column.FieldName = "Month";
            column.Caption = "Month";
            //column.EditFormSettings.ColumnSpan = 1;
            column.Width = System.Web.UI.WebControls.Unit.Pixel(80);
            column.ColumnType = MVCxGridViewColumnType.ComboBox;
            //var comboBoxProperties = column.PropertiesEdit as ComboBoxProperties;
            //comboBoxProperties.DataSource = cCommon.GetMonthNameList();
            //comboBoxProperties.TextField = "MonthName";
            //comboBoxProperties.ValueField = "MonthID";
           // comboBoxProperties.ValueType = typeof(int);
        });


        settings.Columns.Add(column =>
        {
            column.FieldName = "Day";
            column.Visible = false;
        });

        settings.Columns.Add(column =>
        {
            column.FieldName = "RollNo";
            column.Caption = "Roll No.";
            column.Width = System.Web.UI.WebControls.Unit.Pixel(80);
        });
        settings.Columns.Add(column =>
        {
            column.FieldName = "FullName";
            column.Caption = "Student Name";
            column.Width = System.Web.UI.WebControls.Unit.Pixel(180);
            column.Visible = true;
        });

        settings.Columns.Add(column =>
        {
            column.FieldName = "FatherName";
            column.Width = System.Web.UI.WebControls.Unit.Pixel(180);
            column.Visible = true;
        });

        settings.Columns.Add(column =>
        {
            column.FieldName = "ClassSetupName";
            column.Caption = "Class";
           // column.Width = System.Web.UI.WebControls.Unit.Pixel(80);
            column.Visible = true;
        });
        settings.Columns.Add(column =>
        {
            column.FieldName = "Description";
            column.Caption = "Description";
            column.Visible = true;
        });

       
        settings.Columns.Add(column =>
        {
            column.FieldName = "Attendance";
            column.Caption = "Attendance";
            //column.EditFormSettings.ColumnSpan = 1;
            column.Width = System.Web.UI.WebControls.Unit.Pixel(60);
            column.ColumnType = MVCxGridViewColumnType.ComboBox;
            //var comboBoxProperties = column.PropertiesEdit as ComboBoxProperties;
            //comboBoxProperties.DataSource = cCommon.GetAttendanceTypeList();
            //comboBoxProperties.TextField = "Value";
            //comboBoxProperties.ValueField = "Key";
            //column.Width = System.Web.UI.WebControls.Unit.Pixel(100);
            // comboBoxProperties.ValueType = typeof(int);
        });




        settings.Columns.Add(column =>
        {
            column.FieldName = "SMSMobileNo";
            column.Caption = "Mobile No.";
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
        //settings.TotalSummary.Add(DevExpress.Data.SummaryItemType.None, "ClassDescription");
        settings.Settings.ShowFooter = true;
        //settings.SettingsExport.RenderBrick = (sender, e) =>
        //{
        //    if (e.RowType == GridViewRowType.Data && e.VisibleIndex % 2 == 0)
        //        e.BrickStyle.BackColor = System.Drawing.Color.FromArgb(0xEE, 0xEE, 0xEE);
        //};

        settings.GroupSummary.Add(DevExpress.Data.SummaryItemType.Count, "FullName");
        //settings.GroupSummary.Add(DevExpress.Data.SummaryItemType.None, "ClassDescription");
        settings.Settings.ShowGroupPanel = true;
      
      
        return settings;
    }
}