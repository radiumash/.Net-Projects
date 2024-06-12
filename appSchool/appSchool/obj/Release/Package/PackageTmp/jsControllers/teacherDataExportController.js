var ShowFilterRow = false;


var teacherDataExportController = {

    splitterResized: function (s, e) {
      
        switch (e.pane.name) {
                
            case 'TeacherDataList':
                GridTeacherDataExport.SetHeight(e.pane.GetClientHeight() - 1);
                break;
         
            }
        },



    SelectionChangedForClasses: function (s, e) {

        s.GetSelectedFieldValues("ClassSetupID", teacherDataExportController.GetSelectedFieldValuesCallbackForClasses);

    },
    GetSelectedFieldValuesCallbackForClasses: function (values) {

     
        // $("txtclassSetup").val(values);

    },

  
    ClickTeacherInfo: function (s, e) {

        $.ajax({
            url: "/TeacherDataExport/GetPersonalInfoListClassWise",
            type: "POST",
            data: { },
            success: function (data) {
                $("#divteacher").html(data);
            },
            error: function () {
            }
        });



    },

  

    SelectAllTeacher: function (s, e) {
        if (s.GetChecked()) {
            GridView.SelectRows();
            txtisall.SetText(1);
        }
        else {
            GridView.UnselectRows();
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
            var iframe = '<html><head><style>body, html {width: 100%; height: 100%; margin: 0; padding: 0}</style></head><body><iframe src="http://localhost:51734/ReportForms/TeacherDetails.aspx?TeacherID=' + TeacherID + '&IsAllSelect=' + IsAllSelect + '" style="height:calc(100% - 4px);width:calc(100% - 4px)"></iframe></html></body>';

            var win = window.open("", "", "width=1000,height=480,top=150,left=250,toolbar=no,menubar=no,resizable=yes");

            win.document.write(iframe);
        }
        else {
            var iframe = '<html><head><style>body, html {width: 100%; height: 100%; margin: 0; padding: 0}</style></head><body><iframe src="http://localhost:51734/ReportForms/TeacherDetails.aspx?TeacherID=' + TeacherID + '&IsAllSelect=' + IsAllSelect + '" style="height:calc(100% - 4px);width:calc(100% - 4px)"></iframe></html></body>';

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
            var url = "/ReportForms/TeacherDetails.aspx?TeacherID=" + TeacherID + "&IsAllSelect=" + IsAllSelect + ""

        }
        else {
            IsAllSelect = 0;
            var url = "/ReportForms/TeacherDetails.aspx?TeacherID=" + TeacherID + "&IsAllSelect=" + IsAllSelect + ""
     
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


    //splitterResized: function (s, e) {
    //    switch (e.pane.name) {
    //        case 'RegistrationBody':
    //            GridStudents.SetHeight(e.pane.GetClientHeight() - 1);
    //            break;
    //        case 'RegistrationFooter':
    //            //tabControl = tabStudentRecord;
    //            //if (tabControl != 'undefined') {
    //            //    tabStudentRecord.SetWidth(e.pane.GetClientWidth());
    //            //};
    //            break;
    //    }
    //},
    //AddNewClicked: function (s, e) {
    //    GridStudents.AddNewRow();
    //},
    //EditClicked: function (s, e) {
    //    rowIdx = GridStudents.GetFocusedRowIndex();
    //    GridStudents.StartEditRow(rowIdx);
    //},

    //DeleteClicked: function (s, e) {
    //    GridStudents.DeleteRow();
    //},


    //FilterClicked: function (s, e) {
    //    ShowFilterRow = !ShowFilterRow;
    //    $.ajax({
    //        url: "/StudentRegistration/RefreshRegistrationGrid",
    //        type: "POST",
    //        data: { mShowFilter: ShowFilterRow },
    //        success: function (data) {
    //            $("#RegistrationSplitter_1_CC").html(data);
    //        },
    //        error: function () {
    //        }
    //    });
       
    //},
   
    
    //RowSelectionChange: function OnGridFocusedRowChanged(s, e) {
    //    s.GetRowValues(s.GetFocusedRowIndex(), 'StudentID', studentRegistrationController.RefreshTabsView);

       
    //},

    //RefreshTabsView: function (values) {
    //    var regID;
    //    if (values != null) {
    //        regID = values;
    //    }
    //$.ajax({
    //        url: "/StudentRegistration/StudentGridRowChange",
    //        type: "POST",
    //        data: {RegID:regID},
    //        success: function (data) {
    //            $("#RegistrationSplitter_2_CC").html(data);
    //        },
    //        error: function () {
    //        }
    //    });
    //    //$("#BodySplitter_1_CC").html("<h1>"+values+"</h1>");
    //    //alert(values);
    //}
}