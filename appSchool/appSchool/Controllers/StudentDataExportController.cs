using appSchool.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DevExpress.Web;
using DevExpress.Web.Mvc;
using appSchool.ViewModels;


namespace appSchool.Controllers
{
    [NoCache]
    public class StudentDataExportController : Controller
    {
        private UnitOfWork unitOfWork = new UnitOfWork();

        public ActionResult Index()
        {
            if (Session["UserID"] == null || (int)SubMenuModules.appStudentDataExport == 0)
            {

                return Redirect("~/");
            }
            //UserPermission objuser = new UserPermission();
            //objuser = unitOfWork.userPermissionService.CheckUserPermissionModulewise(int.Parse(Session["UserID"].ToString()), 44, byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()));
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

            //GridViewStudentDataExport._PersonalInfo = false;
            //GridViewStudentDataExport._ParentsInfo = false;
            //GridViewStudentDataExport._GardianInfo = false;
            //if (Session["TypedListModel"] == null)
            //    Session["TypedListModel"] = null;
            //else
            //    Session["TypedListModel"] = null;
            //   //Session["TypedListModel"] = new UnitOfWork().studentSessionService.GetStudentForSessionID(int.Parse(Session["SessionID"].ToString()));

            return View();
        }

        public ActionResult ListStudentDataPartial()
        {
            if (Session["UserID"] == null) { return Redirect("~/"); }
            return PartialView(Session["TypedListModel"]);
        }


        [HttpPost]
        public JsonResult SMSButtonClick(modelSendSms obj)
        {
            //if (Session["UserID"] == null) { return Redirect("~/"); }
            bool Result = false;
            string Errormsg = string.Empty;
            SettingMasterStaticClass._SaveSMSLog = true;


            #region ONETIME SMS

            if (ModelState.IsValid)
            {
               


                string mMobileNo = obj.smsMobileNo;

                if (obj.smsCopy == true)
                {
                    string TeacherMNo = unitOfWork.teacherRegistrationService.GetByID(obj.OrderedBy).MobileNo;
                    mMobileNo = mMobileNo + "," + TeacherMNo;
                }
                if (obj.smsAdminCopy == true) // smsAdminCopy Code Changes --- 28/may/2022
                {
                    SchoolMaster objschool = unitOfWork.schoolMasterService.GetSchoolSetup();
                    if (objschool.AdminSMSCopyNo != null)
                    {
                        if (objschool.AdminSMSCopyNo.Length > 9)
                            mMobileNo = mMobileNo + "," + objschool.AdminSMSCopyNo;
                    }

                } // smsAdminCopy Code Changes --- 28/may/2022

                if (obj.SMSLanguage == 2)
                {
                    string NewMessage = obj.PrefixEnglish + " " + obj.smsTextEnglish;
                    if (obj.smsMobileNo != string.Empty)
                    {
                        //Errormsg = SendMesssageText((NewMessage.Trim()), mMobileNo);

                        Errormsg = unitOfWork.sendMessegeService.SendMesssageText((NewMessage.Trim()), mMobileNo, byte.Parse(Session["CompID"].ToString()));

                    }
                }
                else
                {
                    string NewMessage = obj.PrefixHindi + " " + obj.smsTextHindi;
                    if (obj.smsMobileNo != string.Empty)
                    {
                        //Errormsg = SendMesssageTextHindi(ConvertUnicodeStringToHexString(NewMessage.Trim()), mMobileNo);

                        //Errormsg = unitOfWork.sendMessegeService.SendMesssageTextHindi(ConvertUnicodeStringToHexString(NewMessage.Trim()), mMobileNo, byte.Parse(Session["CompID"].ToString()));
                        Errormsg = unitOfWork.sendMessegeService.SendMesssageTextHindi(NewMessage.Trim(), mMobileNo, byte.Parse(Session["CompID"].ToString()));
                    }
                }

          
            }
            else
            {
                Errormsg += "ModelState is not Valid.";
            }
            #endregion

            modelSendSms objsms = new modelSendSms();
            objsms.includeName = true;


            return new JsonResult()
            {
                //JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                //Data = new
                //{
                //    Resultmsg = Errormsg,
                //    ResultData = RenderRazorViewToString("SendGeneralMessegeEditTextView", objsms, ControllerContext, ViewData, TempData)
                //}
            };

        }

        public RedirectResult ExportTo(string mClassesID,string mStudentID)
        {
            if (Session["UserID"] == null) { return Redirect("~/"); }
            var model = Session["TypedListModel"];

            return Redirect("~/ReportForms/ClassSetupReports.aspx");
            //switch (OutputFormat.ToUpper())
            //{

            //    case "CSV":
            //        return GridViewExtension.ExportToCsv(GridViewStudentDataExport.ExportGridViewSettings, model);
            //    case "PDF":
            //        return GridViewExtension.ExportToPdf(GridViewStudentDataExport.ExportGridViewSettings, model);
            //    case "RTF":
            //        return GridViewExtension.ExportToRtf(GridViewStudentDataExport.ExportGridViewSettings, model);
            //    case "XLS":
            //        return GridViewExtension.ExportToXls(GridViewStudentDataExport.ExportGridViewSettings, model);
            //    case "XLSX":
            //        return GridViewExtension.ExportToXlsx(GridViewStudentDataExport.ExportGridViewSettings, model);
            //    default:
            //        return RedirectToAction("Index");
            //}


            //return PartialView("ListStudentDataPartial", unitOfWork.vStudentDataExportService.GetStudentListByClassSetupIDs(mClassesID, int.Parse(Session["SessionID"].ToString()), byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));
        }

        public ActionResult PartialClassSetupView()
        {
            if (Session["UserID"] == null) { return Redirect("~/"); }
            return PartialView("ListClassSetupPartial", unitOfWork.classSetupService.GetClassSetupList(byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));
        }



        public ActionResult ListPartialClassSetup()
        {
            return PartialView("ListClassGridLookupPartial", unitOfWork.classSetupService.GetClassSetupList(byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));
        }

        public ActionResult GetStudentListClassWise(string mClassesID)
        {
            if (Session["UserID"] == null) { return Redirect("~/"); }
            ViewData["ClassSetupID"] = mClassesID;


            Session["TypedListModel"] = unitOfWork.vStudentDataExportService.GetStudentListByClassSetupIDs(mClassesID, int.Parse(Session["SessionID"].ToString()), byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()));
            return PartialView("ListStudentDataPartial", Session["TypedListModel"]);

        }


        public ActionResult GetStudentListFor(string mClassesID)
        {
            if (Session["UserID"] == null) { return Redirect("~/"); }
            ViewData["ClassSetupID"] = mClassesID;
            return PartialView("ListStudentDataPartial", unitOfWork.sendMessegeService.GetStudentSessionByClassSetupIDs(mClassesID, int.Parse(Session["SessionID"].ToString()), byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));

        }

        public ActionResult GetPersonalInfoListClassWise(string mClassesID)
        {
            if (Session["UserID"] == null) { return Redirect("~/"); }
            ViewData["ClassSetupID"] = mClassesID;
            

            //GridViewStudentDataExport._PersonalInfo = true;
            //GridViewStudentDataExport._ParentsInfo = false;
            //GridViewStudentDataExport._GardianInfo = false;

            //Session["TypedListModel"] = unitOfWork.vStudentDataExportService.GetStudentListByClassSetupIDs(mClassesID, int.Parse(Session["SessionID"].ToString()), byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()));
            return PartialView("ListStudentDataPartial", unitOfWork.vStudentDataExportService.GetStudentListByClassSetupIDs(mClassesID, int.Parse(Session["SessionID"].ToString()), byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));

        }

    


        public ActionResult GetParentsInfoListClassWise(string mClassesID)
        {
            if (Session["UserID"] == null) { return Redirect("~/"); }
            ViewData["ClassSetupID"] = mClassesID;

            //GridViewStudentDataExport._PersonalInfo = false;
            //GridViewStudentDataExport._ParentsInfo = true;
            //GridViewStudentDataExport._GardianInfo = false;

            //Session["TypedListModel"] = unitOfWork.vStudentDataExportService.GetStudentListByClassSetupIDs(mClassesID, int.Parse(Session["SessionID"].ToString()), byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()));
            return PartialView("ListStudentPersonalDataPartial", unitOfWork.vStudentDataExportService.GetStudentListByClassSetupIDs(mClassesID, int.Parse(Session["SessionID"].ToString()), byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));

        }

        public ActionResult GetGardianInfoListClassWise(string mClassesID)
        {
            if (Session["UserID"] == null) { return Redirect("~/"); }
            ViewData["ClassSetupID"] = mClassesID;
            //GridViewStudentDataExport._PersonalInfo = false;
            //GridViewStudentDataExport._ParentsInfo = false;
            //GridViewStudentDataExport._GardianInfo = true;

            //Session["TypedListModel"] = unitOfWork.vStudentDataExportService.GetStudentListByClassSetupIDs(mClassesID, int.Parse(Session["SessionID"].ToString()), byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()));
            return PartialView("ListGardianInfoDataPartial", unitOfWork.vStudentDataExportService.GetStudentListByClassSetupIDs(mClassesID, int.Parse(Session["SessionID"].ToString()), byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));

        }


        public List<vStudentSession> GetStudentListByClassSetupIDs()
        {
            List<vStudentSession> list = new List<vStudentSession>();
            //List<vStudentSession> obj = this.context.vStudentSessions.SqlQuery("SELECT * FROM dbo.vStudentSession where ClassSetupID in (" + mClassSetupID + ") and TCGiven=0  and SessionID=" + mSessionID + " AND CompID=" + mCompID + " AND BranchID=" + mBranchID + " ").ToList();
            return list;
        }
    }

}


public static class GridViewStudentDataExport
{
  
    private static GridViewSettings exportGridViewSettings;
    public static bool _PersonalInfo = false;
    public static bool _ParentsInfo = false;
    public static bool _GardianInfo = false;

    public static GridViewSettings ExportGridViewSettings
    {
        get
        {
            if (exportGridViewSettings == null)
                exportGridViewSettings = CreateExportGridViewSettingsForStudentData();
            else
                exportGridViewSettings = CreateExportGridViewSettingsForStudentData();
            return exportGridViewSettings;
        }
    }

    private static GridViewSettings CreateExportGridViewSettingsForStudentData()
    {
        GridViewSettings settings = new GridViewSettings();
        
        settings.Name = "GridStudentData";
        settings.CallbackRouteValues = new { Controller = "StudentDataExport", Action = "ListStudentDataPartial" };

        settings.KeyFieldName = "StudentID";
        settings.Settings.ShowFilterRow = true;
        settings.Width = System.Web.UI.WebControls.Unit.Percentage(100);
      

        settings.CommandColumn.Visible = true;
        settings.CommandColumn.ShowSelectCheckbox = true;

        settings.CommandColumn.Width = System.Web.UI.WebControls.Unit.Pixel(30);
        settings.CommandColumn.Caption =string.Empty;

        settings.SettingsExport.ExportedRowType = DevExpress.Web.GridViewExportedRowType.Selected;
        settings.Columns.Add(column =>
        {
            column.FieldName = "StudentID";
            column.Visible = true;
        });
        
        settings.Columns.Add(column =>
        {
            column.FieldName = "EnrollmentNo";
            column.Visible = true;
        });
         settings.Columns.Add(column =>
        {
            column.FieldName = "EnrollmentDate";
            column.Caption = "Enrollment Date";
            column.ColumnType = MVCxGridViewColumnType.DateEdit;
            column.Visible = GridViewStudentDataExport._PersonalInfo;
            var dateProperties = column.PropertiesEdit as DateEditProperties;
            dateProperties.UseMaskBehavior = true;
            dateProperties.EditFormat = EditFormat.Custom;
            dateProperties.EditFormatString = "MMMM dd, yyyy";
            dateProperties.DisplayFormatString = "MMMM dd, yyyy";
            dateProperties.MaxDate = DateTime.Now;
            column.Width = System.Web.UI.WebControls.Unit.Pixel(150);
            column.Visible = true;
        });
        
        settings.Columns.Add(column =>
        {
            column.FieldName = "RollNo";
            column.Visible = true;
        });
        
        settings.Columns.Add(column =>
        {
            column.FieldName = "FullName";
            column.Caption = "Student Name";
            column.Width = System.Web.UI.WebControls.Unit.Pixel(150);
        });



        settings.Columns.Add(column =>
        {
            column.FieldName = "ClassDescription";
            column.Caption = "Class";
            column.Width = System.Web.UI.WebControls.Unit.Pixel(80);
        });
        
       

        settings.Columns.Add(column =>
        {
            column.FieldName = "FatherName";
            column.Caption = "Father Name";
            column.Width = System.Web.UI.WebControls.Unit.Pixel(180);
        });

        settings.Columns.Add(column =>
        {
            column.FieldName = "MotherName";
            column.Caption = "Mother Name";
           // column.Visible = GridViewStudentDataExport._ParentsInfo;
            column.Width = System.Web.UI.WebControls.Unit.Pixel(180);
        });


        settings.Columns.Add(column =>
        {
            column.FieldName = "SMSMobileNo";
            column.Caption = "Mobile No";
            column.Visible = GridViewStudentDataExport._PersonalInfo;
        });
       
        settings.Columns.Add(column =>
        {
            column.FieldName = "ICardNo";
            column.Caption = "ICard No";
            column.Visible = GridViewStudentDataExport._PersonalInfo;
        });

        settings.Columns.Add(column =>
        {
            column.FieldName = "Gender";
            column.Caption = "Gender";
            column.Visible = GridViewStudentDataExport._PersonalInfo;
        });

        settings.Columns.Add(column =>
        {
            column.FieldName = "DateOfBirth";
            column.Caption = "Student B'Day";
            column.ColumnType = MVCxGridViewColumnType.DateEdit;
            column.Visible = GridViewStudentDataExport._PersonalInfo;
            var dateProperties = column.PropertiesEdit as DateEditProperties;
            dateProperties.UseMaskBehavior = true;
            dateProperties.EditFormat = EditFormat.Custom;
            dateProperties.EditFormatString = "MMMM dd, yyyy";
            dateProperties.DisplayFormatString = "MMMM dd, yyyy";
            dateProperties.MaxDate = DateTime.Now;
            column.Width = System.Web.UI.WebControls.Unit.Pixel(150);
            //column.EditFormSettings.ColumnSpan = 1;
           

        });




        settings.Columns.Add(column =>
        {
            column.FieldName = "City";
            column.Visible = GridViewStudentDataExport._PersonalInfo;

        });
        settings.Columns.Add(column =>
        {
            column.FieldName = "Nationality";
            column.Visible = GridViewStudentDataExport._PersonalInfo;
        });
        settings.Columns.Add(column =>
        {
            column.FieldName = "MotherTounge";
            column.Visible = GridViewStudentDataExport._PersonalInfo;
        });
        settings.Columns.Add(column =>
        {
            column.FieldName = "LocalAddress";
            column.Visible = GridViewStudentDataExport._PersonalInfo;
        });
        settings.Columns.Add(column =>
        {
            column.FieldName = "ParmanentAddress";
            column.Visible = GridViewStudentDataExport._PersonalInfo;
        });

        settings.Columns.Add(column =>
        {
            column.FieldName = "Caste";
            column.Visible = GridViewStudentDataExport._PersonalInfo;
        });
        settings.Columns.Add(column =>
        {
            column.FieldName = "BloodGroup";
            column.Visible = GridViewStudentDataExport._PersonalInfo;
        });
        settings.Columns.Add(column =>
        {
            column.FieldName = "Religion";
            column.Visible = GridViewStudentDataExport._PersonalInfo;
        });
      
       
   
       
        settings.Columns.Add(column =>
        {
            column.FieldName = "MQualification";
            column.Caption = "Mother Qualification";
            column.Visible = GridViewStudentDataExport._ParentsInfo;
        });
        settings.Columns.Add(column =>
        {
            column.FieldName = "MOccupation";
            column.Visible = GridViewStudentDataExport._ParentsInfo;
        });
        settings.Columns.Add(column =>
        {
            column.FieldName = "MotherDOB";
            column.Visible = StudentDataExportSC._ParentsInfo;
        });

        settings.Columns.Add(column =>
        {
            column.FieldName = "MIncome";
            column.Visible = GridViewStudentDataExport._ParentsInfo;
        });
        settings.Columns.Add(column =>
        {
            column.FieldName = "MOfficeAddress";
            column.Visible = GridViewStudentDataExport._ParentsInfo;
        });
        settings.Columns.Add(column =>
        {
            column.FieldName = "MotherEmailID";
            column.Visible = GridViewStudentDataExport._ParentsInfo;
        });

        settings.Columns.Add(column =>
        {
            column.FieldName = "MOfficePhoneNo";
            column.Visible = GridViewStudentDataExport._ParentsInfo;
        });

               
        settings.Columns.Add(column =>
        {
            column.FieldName = "FQualification";
            column.Caption = "Father Qualification";
            column.Visible = GridViewStudentDataExport._ParentsInfo;
        });
        
        settings.Columns.Add(column =>
        {
            column.FieldName = "FatherMobileNo";
            column.Caption = "Father MobileNo";
            column.Visible = GridViewStudentDataExport._ParentsInfo;
        });
        settings.Columns.Add(column =>
        {
            column.FieldName = "FOccupation";
            column.Caption = "Father Occupation";
            column.Visible = GridViewStudentDataExport._ParentsInfo;
        });
       
        settings.Columns.Add(column =>
        {
            column.FieldName = "FatherDOB";
            column.Caption = "Fathers B'Day";

            column.Visible = GridViewStudentDataExport._ParentsInfo;
            column.ColumnType = MVCxGridViewColumnType.DateEdit;
            
            var dateProperties = column.PropertiesEdit as DateEditProperties;
            dateProperties.UseMaskBehavior = true;
            dateProperties.EditFormat = EditFormat.Custom;
            dateProperties.EditFormatString = "MMMM dd, yyyy";
            dateProperties.DisplayFormatString = "MMMM dd, yyyy";
            dateProperties.MaxDate = DateTime.Now;
            column.Width = System.Web.UI.WebControls.Unit.Pixel(150);
            //column.EditFormSettings.ColumnSpan = 1;
        });
       
        settings.Columns.Add(column =>
        {
            column.FieldName = "FIncome";
            column.Visible = GridViewStudentDataExport._ParentsInfo;
        });
      
        settings.Columns.Add(column =>
        {
            column.FieldName = "FOfficeAddress";
            column.Visible = GridViewStudentDataExport._ParentsInfo;
        });
       
        settings.Columns.Add(column =>
        {
            column.FieldName = "FatherEmailID";
            column.Visible = GridViewStudentDataExport._ParentsInfo;
        });
      
        settings.Columns.Add(column =>
        {
            column.FieldName = "FOfficePhoneNo";
            column.Visible = GridViewStudentDataExport._ParentsInfo;
        });
     
        settings.Columns.Add(column =>
        {
            column.FieldName = "HomePhoneNo";
            column.Visible = GridViewStudentDataExport._ParentsInfo;
        });
        
        settings.Columns.Add(column =>
        {
            column.FieldName = "AnniversaryDate";
            column.Caption = "Parents Anniversary";

            column.Visible = GridViewStudentDataExport._ParentsInfo;
        });


        settings.Columns.Add(column =>
        {
            column.FieldName = "GuardianName";
            column.Visible = GridViewStudentDataExport._GardianInfo;
        });
        settings.Columns.Add(column =>
        {
            column.FieldName = "GOccupation";
            column.Visible = GridViewStudentDataExport._GardianInfo;
        });
        settings.Columns.Add(column =>
        {
            column.FieldName = "Relationship";
            column.Visible = GridViewStudentDataExport._GardianInfo;
        });
        settings.Columns.Add(column =>
        {
            column.FieldName = "GAddress";
            column.Visible = GridViewStudentDataExport._GardianInfo;
        });
        settings.Columns.Add(column =>
        {
            column.FieldName = "GEmailID";
            column.Visible = GridViewStudentDataExport._GardianInfo;
        });
        settings.Columns.Add(column =>
        {
            column.FieldName = "GMobileNo";
            column.Visible = GridViewStudentDataExport._GardianInfo;
        });
        settings.Columns.Add(column =>
        {
            column.FieldName = "GPhoneNo";
            column.Visible = GridViewStudentDataExport._GardianInfo;
        });
        settings.CommandColumn.ShowClearFilterButton = true;

        settings.SettingsPager.Position = System.Web.UI.WebControls.PagerPosition.Bottom;
        settings.SettingsPager.FirstPageButton.Visible = true;
        settings.SettingsPager.LastPageButton.Visible = true;
        settings.SettingsPager.PageSizeItemSettings.Visible = true;
        settings.SettingsPager.PageSize = 200;


        settings.Settings.VerticalScrollBarMode = ScrollBarMode.Visible;
        settings.Settings.HorizontalScrollBarMode = ScrollBarMode.Visible;
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