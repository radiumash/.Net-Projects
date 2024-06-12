var ShowFilterRow = false;


var attendanceDataExportController = {

    splitterResized: function (s, e) {
      
        switch (e.pane.name) {
                case 'AttendanceDataExportHeader':

                break;
                case 'AttendanceDataList':
                      GridAttendanceDataExport.SetHeight(e.pane.GetClientHeight() - 1);
                break;
                case 'AttendanceDataLeftBottom':
                      GridMonthList.SetHeight(e.pane.GetClientHeight() - 1);
                break;
                case 'AttendanceDataLeftTop':
                      GridClassSetup.SetHeight(e.pane.GetClientHeight() - 1);
                      break;
            case 'SentSMSReportFooter':
                GridAttendanceDataExport.SetHeight(e.pane.GetClientHeight() - 1);
                break;
                      
            }
        },



    SelectionChangedForClasses: function (s, e) {

        s.GetSelectedFieldValues("ClassSetupID", attendanceDataExportController.GetSelectedFieldValuesCallbackForClasses);

    },
    GetSelectedFieldValuesCallbackForClasses: function (values) {

        txtclassSetup.SetText(values);
        // $("txtclassSetup").val(values);

    },

    SelectionChangedForMonth: function (s, e) {
    
        s.GetSelectedFieldValues("MonthID", attendanceDataExportController.GetSelectedFieldValuesCallbackForMonth);

    },
    GetSelectedFieldValuesCallbackForMonth: function (values) {
      
        txtMonth.SetText(values);
        // $("txtclassSetup").val(values);

    },

    CloseGridLookup: function () {
        gridLookup.ConfirmCurrentSelection();
        gridLookup.HideDropDown();

        attendanceDataExportController.ClickLoadStudentGrid();
    },


    ClickLoadStudentGrid: function () {
        var classID = txtclassSetup.GetText();


        if (classID.toString() == "") {
            alert("Please Select Classes");
            return;
        }
        //$.ajax({
        //    url: "/SendGeneralMessege/GetStudentListForSMS",
        //    type: "POST",
        //    data: { mClassesID: classID.toString() },
        //    beforeSend: (function (data) {
        //        loadingPanelSmscomman.Show();
        //    }),
        //    success: function (data) {
        //        $("#divsmsstudent").html(data);
        //    },
        //    error: function () {
        //    }
        //}).done(function (data) {
        //    loadingPanelSmscomman.Hide();

        //});



    },


    ClosemGridLookup: function () {
        gridmonthLookup.ConfirmCurrentSelection();
        gridmonthLookup.HideDropDown();

        attendanceDataExportController.ClickLoadmStudentGrid();
    },

    ClickLoadmStudentGrid: function () {
        var MonthID = txtMonth.GetText();


        if (MonthID.toString() == "") {
            alert("Please Select Month");
            return;
        }
        //$.ajax({
        //    url: "/SendGeneralMessege/GetStudentListForSMS",
        //    type: "POST",
        //    data: { mClassesID: classID.toString() },
        //    beforeSend: (function (data) {
        //        loadingPanelSmscomman.Show();
        //    }),
        //    success: function (data) {
        //        $("#divsmsstudent").html(data);
        //    },
        //    error: function () {
        //    }
        //}).done(function (data) {
        //    loadingPanelSmscomman.Hide();

        //});



    },
 

    ClickPersonalInfo: function (s, e) {
        var classID = txtclassSetup.GetText();
        var mfromDate = FromDate.GetDate().toDateString();
        var mToDate = ToDate.GetDate().toDateString();

       

        if (classID.toString() == "") {
            alert("Please Select Classes");
            return;
        }
        $.ajax({
            url: "/AttendanceDataExport/GetAttendanceInfoListDateWise",
            type: "POST",
            data: { mClassesID: classID.toString(), newFromDate: mfromDate, newToDate: mToDate },
            beforeSend: (function (data) {
                LoadingPanelAttDataExport.Show();
            }),
            success: function (data) {
                $("#divstuadd").html(data);
                //var winHeight = document.getElementById('ExportButtonSplitter_1_CC').offsetHeight;
                //GridAttendanceDataExport.SetHeight(winHeight - 10);
                cbSelectAll.SetChecked(false);
            },
            error: function () {
            }
        }).done(function (data) {
            LoadingPanelAttDataExport.Hide();

        });


    },

    ClickMonthlyInfo: function (s, e) {
        var classID = txtclassSetup.GetText();
        var MonthID = txtMonth.GetText();
      
       

        if (classID.toString() == "") {
            alert("Please Select Classes");
            return;
        }
        if (MonthID.toString() == "") {
            alert("Please Select Months");
            return;
        }


        $.ajax({
            url: "/AttendanceDataExport/GetAttendanceInfoListMonthwise",
            type: "POST",
            data: { mClassesID: classID.toString(), mMonthsID: MonthID.toString() },
            beforeSend: (function (data) {
                LoadingPanelAttDataExport.Show();
            }),
            success: function (data) {
                $("#ExportButtonSplitter_1_CC").html(data);
                var winHeight = document.getElementById('ExportButtonSplitter_1_CC').offsetHeight;
                GridAttendanceDataExport.SetHeight(winHeight - 10);
                cbSelectAll.SetChecked(false);
            },
            error: function () {
            }
        }).done(function (data) {
            LoadingPanelAttDataExport.Hide();

        });
    },

   
    SelectAllStudent: function (s, e) {

        if (s.GetChecked()) {
            GridAttendanceDataExport.SelectRows();
        }
        else {
            GridAttendanceDataExport.UnselectRows();
        }

    },

    SelectionChanged: function (s, e) {

        //s.GetSelectedFieldValues("SMSMobileNo", studentDataExportController.GetSelectedFieldValuesCallback);

        s.GetSelectedFieldValues("StudentAttendanceID", attendanceDataExportController.GetSelectedFieldValuesCallbackForStudentID);

    },


    GetSelectedFieldValuesCallbackForStudentID: function (values) {
        //alert(values);
        txtClassAttendanceID.SetText(values);

    },


}