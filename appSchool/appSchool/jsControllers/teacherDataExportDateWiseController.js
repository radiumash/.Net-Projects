var ShowFilterRow = false;


var teacherDataExportDateWiseController = {

    splitterResized: function (s, e) {
      
        switch (e.pane.name) {
                
            case 'TeacherDataListDateWise':
                GridTeacherDataExportDateWise.SetHeight(e.pane.GetClientHeight() - 1);
                break;
            }
        },



    SelectionChangedForClasses: function (s, e) {

        s.GetSelectedFieldValues("ClassSetupID", teacherDataExportDateWiseController.GetSelectedFieldValuesCallbackForClasses);

    },
    GetSelectedFieldValuesCallbackForClasses: function (values){

    },

  
    ClickTeacherInfo: function (s, e) {

        $.ajax({
            url: "/TeacherDataExportDateWise/GetPersonalInfoListClassWise",
            type: "POST",
            data: { },
            success: function (data) {
                $("#TeacherDataExportSplitter_1_CC").html(data);
                var winHeight = document.getElementById('TeacherDataExportSplitter_1_CC').offsetHeight;
                GridTeacherDataExportDateWise.SetHeight(winHeight - 10);
                cbSelectAll.SetChecked(false);
            },
            error: function () {
            }
        });



    },

    ClickAllAttendance: function (s, e) {

        var a1 = FromDate.GetDate().toDateString();
        var a2 = ToDate.GetDate().toDateString();
        $.ajax({
            url: "/TeacherDataExportDateWise/GetEmployeeAtendanceList",
            type: "POST",
            data: { newFromDate: a1.toString(), newToDate: a2.toString() },
            beforeSend: (function (data) {
                loadingPanelTeacher.Show();
            }),
            success: function (data) {
                $("#divteacher").html(data);

            },
            error: function () {
            }
        }).done(function (data) {
            loadingPanelTeacher.Hide();

        });
    },

    SelectAllTeacher: function (s, e) {
       
        if (s.GetChecked()) {
            
            GridTeacherDataExportDateWise.SelectRows();
            txtisall.SetText(1);
        }
        else {
            GridTeacherDataExportDateWise.UnselectRows();
            txtisall.SetText(0);
        }

    },

    SelectionChanged: function (s, e) {

        //s.GetSelectedFieldValues("SMSMobileNo", studentDataExportController.GetSelectedFieldValuesCallback);

        s.GetSelectedFieldValues("TeacherID", teacherDataExportController.GetSelectedFieldValuesCallbackForStudentID);

    },


    GetSelectedFieldValuesCallbackForStudentID: function (values) {
        //alert(values);
        txtTeacherID.SetText(values);

    },

    ClickReportViewInfo: function (s, e) {

        var TeacherID = txtTeacherID.GetText();

        if (TeacherID.toString() == "") {
            alert("Please Select Teacher");
            return;
        }
        var IsAllSelect = txtisall.GetText();

        if (IsAllSelect == "1") {
            TeacherID = 0;
            var iframe = '<html><head><style>body, html {width: 100%; height: 100%; margin: 0; padding: 0}</style></head><body><iframe src="http://localhost:51734/ReportForms/EmployeeAttendanceReport.aspx?TeacherID=' + TeacherID + '&IsAllSelect=' + IsAllSelect + '" style="height:calc(100% - 4px);width:calc(100% - 4px)"></iframe></html></body>';

            var win = window.open("", "", "width=1000,height=480,top=150,left=250,toolbar=no,menubar=no,resizable=yes");

            win.document.write(iframe);
        }
        else {
            var iframe = '<html><head><style>body, html {width: 100%; height: 100%; margin: 0; padding: 0}</style></head><body><iframe src="http://localhost:51734/ReportForms/EmployeeAttendanceReport.aspx?TeacherID=' + TeacherID + '&IsAllSelect=' + IsAllSelect + '" style="height:calc(100% - 4px);width:calc(100% - 4px)"></iframe></html></body>';

            var win = window.open("", "", "width=1000,height=480,top=150,left=250,toolbar=no,menubar=no,resizable=yes");

            win.document.write(iframe);
        }

    },


    OnclickChangeReports: function () {

        var TeacherID = txtTeacherID.GetText();

        if (TeacherID.toString() == "") {
            alert("Please Select Teacher");
            return;
        }
        var IsAllSelect = txtisall.GetText();


        if (IsAllSelect == "1") {
            TeacherID = 0;
            var url = "/ReportForms/EmployeeAttendanceReport.aspx?TeacherID=" + TeacherID + "&IsAllSelect=" + IsAllSelect + ""

        }
        else {
            IsAllSelect = 0;
            var url = "/ReportForms/EmployeeAttendanceReport.aspx?TeacherID=" + TeacherID + "&IsAllSelect=" + IsAllSelect + ""

        }

        //var url = "/ReportForms/HouseAllotmentExport.aspx?StudentID=" + StudentID + "&ClassID=" + classID + ""
        //feesTransactionController.Setcoocies("popupstudentid", mstudentID, 1);
        //feesTransactionController.Setcoocies("popuptermid", mtermID, 1);
        //feesTransactionController.Setcoocies("popupclassid", mclassID, 1);


        PopupControlChangeReportView.SetContentUrl(url);

        var purl = PopupControlChangeReportView.GetContentUrl();


        PopupControlChangeReportView.Show();



    },

    CloseReportViewPopupLoading: function (s, e) {


        studentID = txtTeacherID.GetText();




        loadingPanelTeacher.Show();



        //teacherDataExportController.GetFeeHeadList(studentID, termID);

        //teacherDataExportController.GetFeeCalculateamountdata(studentID, termID);

        setTimeout(teacherDataExportController.Closepopupfeeschange, 2000);

        //teacherDataExportController.Closepopupfeeschange();
    },

    Closepopupfeeschange: function (s, e) {
        loadingPanelTeacher.Hide();
        PopupControlChangeReportView.Hide();

    },
}