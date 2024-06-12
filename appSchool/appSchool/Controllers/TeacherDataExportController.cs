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
    public class TeacherDataExportController : Controller
    {
        private UnitOfWork unitOfWork = new UnitOfWork();

        public ActionResult Index()
        {

            if (Session["UserID"] == null || (int)SubMenuModules.appTeacherDataExport == 0)
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
          
           
                Session["TeacherDataExport"] = null;

            return View("");
        }

        public ActionResult ListTeacherDataPartial()
        {
            if (Session["UserID"] == null) { return Redirect("~/"); }
            return PartialView("ListTeacherDataPartial", unitOfWork.vTeacherDataExportService.GetTeacherListForDataExport(int.Parse(Session["SessionID"].ToString()), byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));
        }

        public ActionResult ExportTo(string OutputFormat)
        {
            if (Session["UserID"] == null) { return Redirect("~/"); }
            var model = Session["TeacherDataExport"];

            switch (OutputFormat.ToUpper())
            {
                case "CSV":
                    return GridViewExtension.ExportToCsv(GridViewTeacherDataExport.ExportGridViewSettings, model);
                case "PDF":
                    return GridViewExtension.ExportToPdf(GridViewTeacherDataExport.ExportGridViewSettings, model);
                case "RTF":
                    return GridViewExtension.ExportToRtf(GridViewTeacherDataExport.ExportGridViewSettings, model);
                case "XLS":
                    return GridViewExtension.ExportToXls(GridViewTeacherDataExport.ExportGridViewSettings, model);
                case "XLSX":
                    return GridViewExtension.ExportToXlsx(GridViewTeacherDataExport.ExportGridViewSettings, model);
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
            //Session["TeacherDataExport"] = unitOfWork.vTeacherDataExportService.GetTeacherListForDataExport(int.Parse(Session["SessionID"].ToString()), byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()));
            return PartialView("ListTeacherDataPartial", unitOfWork.vTeacherDataExportService.GetTeacherListForDataExport(int.Parse(Session["SessionID"].ToString()), byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));

        }

    }

}


public static class GridViewTeacherDataExport
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
        
        settings.Name = "GridTeacherDataExport";
        settings.CallbackRouteValues = new { Controller = "TeacherDataExport", Action = "ListTeacherDataPartial" };

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
            column.FieldName = "EmployeeCode";
            column.Visible = false;
        });
        settings.Columns.Add(column =>
        {
            column.FieldName = "TeacherID";
            column.Visible = false;
        });
        settings.Columns.Add(column =>
        {
            column.FieldName = "FullName";
            column.Caption = "Teacher Name";
            column.Width = System.Web.UI.WebControls.Unit.Pixel(150);
        });


        settings.Columns.Add(column =>
        {
            column.FieldName = "DOB";
            column.Caption = "Teacher B'Day";
            column.ColumnType = MVCxGridViewColumnType.DateEdit;
            column.Visible = GridViewStudentDataExport._PersonalInfo;
            //var dateProperties = column.PropertiesEdit as DateEditProperties;
            //dateProperties.UseMaskBehavior = true;
            //dateProperties.EditFormat = EditFormat.Custom;
            //dateProperties.EditFormatString = "MMMM dd, yyyy";
            //dateProperties.DisplayFormatString = "MMMM dd, yyyy";
            //dateProperties.MaxDate = DateTime.Now;
            //column.Width = System.Web.UI.WebControls.Unit.Pixel(150);
            //column.EditFormSettings.ColumnSpan = 1;


        });

        settings.Columns.Add(column =>
        {
            column.FieldName = "PhoneNo";
            column.Caption = "Phone No.";
            column.Width = System.Web.UI.WebControls.Unit.Pixel(180);
        });

        settings.Columns.Add(column =>
        {
            column.FieldName = "MobileNo";
            column.Caption = "Mobile No";
          
        });

        settings.Columns.Add(column =>
        {
            column.FieldName = "Gender";
            column.Caption = "Gender";
           
        });

        settings.Columns.Add(column =>
        {
            column.FieldName = "Religion";
            column.Caption = "Religion";
           
        });

        settings.Columns.Add(column =>
        {
            column.FieldName = "MaritalStatus";
            column.Caption = "MaritalStatus";
           
        });


        settings.Columns.Add(column =>
        {
            column.FieldName = "EmailID";
            column.Caption = "EmailID";
           
        });
      

        settings.Columns.Add(column =>
        {
            column.FieldName = "LocalAddress";
            column.Caption = "Local Address";
          
        });


        settings.Columns.Add(column =>
        {
            column.FieldName = "ParmanentAddress";
            column.Caption = "Local Address";
          
        });
        settings.Columns.Add(column =>
        {
            column.FieldName = "FatherName";
            column.Caption = "Father Name";
          
        });

        settings.Columns.Add(column =>
        {
            column.FieldName = "MotherName";
            column.Caption = "Mother Name";
           
        });


        settings.Columns.Add(column =>
        {
            column.FieldName = "MotherName";
            column.Caption = "Mother Name";
          
        });


       

        settings.Columns.Add(column =>
        {
            column.FieldName = "AnniversaryDate";
            column.Caption = "Anniversary";
            column.ColumnType = MVCxGridViewColumnType.DateEdit;
            column.Visible = GridViewStudentDataExport._PersonalInfo;
            //var dateProperties = column.PropertiesEdit as DateEditProperties;
            //dateProperties.UseMaskBehavior = true;
            //dateProperties.EditFormat = EditFormat.Custom;
            //dateProperties.EditFormatString = "MMMM dd, yyyy";
            //dateProperties.DisplayFormatString = "MMMM dd, yyyy";
            //dateProperties.MaxDate = DateTime.Now;
            //column.Width = System.Web.UI.WebControls.Unit.Pixel(150);
            //column.EditFormSettings.ColumnSpan = 1;
           

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