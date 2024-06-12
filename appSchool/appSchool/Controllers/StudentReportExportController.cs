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
    public class StudentReportExportController : Controller
    {
        private UnitOfWork unitOfWork = new UnitOfWork();

        public ActionResult Index()
        {
            if (Session["UserID"] == null)
            {
                return Redirect("~/");
            }

            UserPermission objuser = new UserPermission();
            //objuser = unitOfWork.userPermissionService.CheckUserPermissionModulewise(int.Parse(Session["UserID"].ToString()), 11, byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()));
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
            return View("");
        }

        //public ActionResult Index()
        //{
        //    if (Session["UserID"] == null) { return Redirect("~/"); }
        //    return RedirectToAction("Export");
        //}

        public ActionResult Export()
        {
            if (Session["UserID"] == null) { return Redirect("~/"); }
            return PartialView("Export", unitOfWork.VstudentDetailService.GetVStudentDetailList(byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));
        }
        public ActionResult ExportPartial()
        {
            if (Session["UserID"] == null) { return Redirect("~/"); }
            return PartialView("ExportPartial", unitOfWork.VstudentDetailService.GetVStudentDetailList(byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));
        }
        public ActionResult ExportTo()
        {
            if (Session["UserID"] == null) { return Redirect("~/"); }
            foreach (string typeName in GridViewExportDemoHelper.ExportTypes.Keys)
            {
                if (Request.Params[typeName] != null)
                    return GridViewExportDemoHelper.ExportTypes[typeName].Method(GridViewExportDemoHelper.ExportGridViewSettings, unitOfWork.VstudentDetailService.GetVStudentDetailList(byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));
            }
            return RedirectToAction("Export");
        }


        public ActionResult StudentPhotoView()
        {
            ViewData["ClassIDForStudentPhoto"] = 0;
            return PartialView("StudentPhotoView");
        }

        public ActionResult ListStudentPhotoPartial(int pClassID)
        {
            ViewData["ClassIDForStudentPhoto"] = pClassID;
            List<vStudentDataExport> lst = unitOfWork.vStudentDataExportService.GetStudentListForPhotoReport(pClassID, byte.Parse(Session["SessionID"].ToString()), byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()));

            return PartialView("ListStudentPhotoView",lst);
        }

        public ActionResult GetAllStudentPhotoView(string mClassesID)
        {
            ViewData["ClassIDForStudentPhoto"] = mClassesID;
            bool mStatus = false;
            string merrormsg = string.Empty;
            List<vStudentDataExport> lst = unitOfWork.vStudentDataExportService.GetStudentListForPhotoReport(int.Parse(mClassesID), byte.Parse(Session["SessionID"].ToString()), byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()));
            if (lst != null)
            {
                mStatus = true; 
            }
            else
            {
                merrormsg = "Data Not Found.";
            }

            return Json(new { status = mStatus, errormsg = merrormsg, Listdata = cCommon.RenderRazorViewToString("ListStudentPhotoView", lst, ControllerContext, ViewData, TempData) }, JsonRequestBehavior.AllowGet);
        }
       
    
    
    }

    public partial class GridViewExportDemoHelper
    {
        static GridViewSettings exportGridViewSettings;
        public static GridViewSettings ExportGridViewSettings
        {
            get
            {
                if (exportGridViewSettings == null)
                {
                    exportGridViewSettings = CreateExportGridViewSettings();
                    exportGridViewSettings.Name = "GridStudents";
                    exportGridViewSettings.CallbackRouteValues = new { Controller = "StudentReportExport", Action = "ExportPartial" };
                }
                return exportGridViewSettings;
            }
        }






        static GridViewSettings CreateExportGridViewSettings()
        {
            GridViewSettings settings = new GridViewSettings();

            settings.Name = "GridStudents";
            settings.CallbackRouteValues = new { Controller = "StudentReportExport", Action = "ExportPartial" };
            settings.Width = System.Web.UI.WebControls.Unit.Percentage(100);
            settings.Height = System.Web.UI.WebControls.Unit.Percentage(100);


           

            settings.Columns.Add(column =>
            {
                column.FieldName = "EnrollmentNo";
                column.Caption = "Enrollment No";
                column.EditFormSettings.ColumnSpan = 2;
                column.Width = System.Web.UI.WebControls.Unit.Pixel(110);
               

            });

            settings.Columns.Add("Name");

            settings.Columns.Add(column =>
            {
                column.FieldName = "ClassDescription";
                column.Caption = "Class";

            });


            settings.Columns.Add(column =>
            {
                column.FieldName = "AppImage";
                column.Caption = "image";
              

            });



            settings.Columns.Add(column =>
                {
                    column.FieldName = "DateOfBirth";
                    column.Caption = "Birth Date";
                    column.PropertiesEdit.DisplayFormatString = "dd MMM yyyy";
                });

            settings.Columns.Add("City");


            settings.Columns.Add(column =>
            {
                column.FieldName = "SMSMobileNo";
                column.Caption = "SMS Mobile No";

            });


            settings.Columns.Add(column =>
            {
                column.FieldName = "FatherName";
                column.Caption = "Father";

            });

            settings.Columns.Add(column =>
            {
                column.FieldName = "AppImage";
                column.Caption = "AppImage";
                column.ColumnType = MVCxGridViewColumnType.BinaryImage;


            });

            















            settings.SettingsPager.Position = System.Web.UI.WebControls.PagerPosition.Bottom;
            settings.SettingsPager.FirstPageButton.Visible = true;
            settings.SettingsPager.LastPageButton.Visible = true;
            settings.SettingsPager.PageSizeItemSettings.Visible = true;
            settings.SettingsPager.PageSize = 50;
            settings.Settings.ShowFilterRow = true;
            settings.Settings.ShowFilterRowMenu = true;
            settings.CommandColumn.ShowClearFilterButton = true;
            //settings.Settings.VerticalScrollBarMode = ScrollBarMode.Visible;

            settings.CustomUnboundColumnData = (sender, e) =>
            {
                //if (e.Column.FieldName == "Total")
                //{
                //    decimal price = (decimal)e.GetListSourceFieldValue("UnitPrice");
                //    int quantity = Convert.ToInt32(e.GetListSourceFieldValue("Quantity"));
                //    e.Value = price * quantity;
                //}
            };



            settings.TotalSummary.Add(DevExpress.Data.SummaryItemType.Count, "Name");
            settings.TotalSummary.Add(DevExpress.Data.SummaryItemType.None, "ClassDescription");
            settings.Settings.ShowFooter = true;
            settings.SettingsExport.RenderBrick = (sender, e) =>
            {
                //if (e.RowType == GridViewRowType.Data && e.VisibleIndex % 2 == 0)
                //    e.BrickStyle.BackColor = System.Drawing.Color.FromArgb(0xEE, 0xEE, 0xEE);
            };

            settings.GroupSummary.Add(DevExpress.Data.SummaryItemType.Count, "Name");
            settings.GroupSummary.Add(DevExpress.Data.SummaryItemType.None, "ClassDescription");
            settings.Settings.ShowGroupPanel = true;
            return settings;
        }

        

    }


    public enum GridViewExportType { None, Pdf, Xls, Xlsx, Rtf, Csv }

    public delegate ActionResult ExportMethod(GridViewSettings settings, object dataObject);

    public class ExportType
    {
        public string Title { get; set; }
        public ExportMethod Method { get; set; }
    }

    public partial class GridViewExportDemoHelper
    {
        static Dictionary<string, ExportType> exportTypes;
        public static Dictionary<string, ExportType> ExportTypes
        {
            get
            {
                if (exportTypes == null)
                    exportTypes = CreateExportTypes();
                return exportTypes;
            }
        }
        static Dictionary<string, ExportType> CreateExportTypes()
        {
            Dictionary<string, ExportType> types = new Dictionary<string, ExportType>();
            types.Add("PDF", new ExportType { Title = "Export to PDF", Method = GridViewExtension.ExportToPdf });
            //types.Add("XLS", new ExportType { Title = "Export to XLS", Method = GridViewExtension.ExportToXls });
            types.Add("XLSX", new ExportType { Title = "Export to XLSX", Method = GridViewExtension.ExportToXlsx });
            types.Add("Word", new ExportType { Title = "Export to Word", Method = GridViewExtension.ExportToRtf });
            //types.Add("CSV", new ExportType { Title = "Export to CSV", Method = GridViewExtension.ExportToCsv });
            return types;
        }
    }

} 
