var ShowFilterRow = false;


var houseAllotmentExportController = {

    splitterResized: function (s, e) {
      
        switch (e.pane.name) {
                case 'HouseAllotmentExportHeader':

                break;
            case 'HouseAllotmentExportBody':
                   // GridStudents.SetHeight(e.pane.GetClientHeight() - 1);
                    break;
            case 'HouseAllotmentExportFooter':
                    //tabControl = tabStudentRecord;
                    //if (tabControl != 'undefined') {
                    //    tabStudentRecord.SetWidth(e.pane.GetClientWidth());
                    //};
                    break;
            case 'StudentDataList':
                GridStudentData.SetHeight(e.pane.GetClientHeight() - 1);
                break;
            case 'StudentDataClass':
                GridClassSetup.SetHeight(e.pane.GetClientHeight() - 1);
                break;
            }
        },

    CloseGridLookup: function () {
        gridLookup.ConfirmCurrentSelection();
        gridLookup.HideDropDown();

        houseAllotmentExportController.ClickLoadGrid();
    },

    SelectionChangedForClasses: function (s, e) {

        s.GetSelectedFieldValues("ClassSetupID", houseAllotmentExportController.GetSelectedFieldValuesCallbackForClasses);

    },


    SelectionChanged: function (s, e) {

        //s.GetSelectedFieldValues("SMSMobileNo", studentDataExportController.GetSelectedFieldValuesCallback);

        s.GetSelectedFieldValues("StudentID", houseAllotmentExportController.GetSelectedFieldValuesCallbackForStudentID);

    },

    GetSelectedFieldValuesCallbackForClasses: function (values) {

        txtclassSetup.SetText(values);
        // $("txtclassSetup").val(values);

    },


    GetSelectedFieldValuesCallbackForStudentID: function (values) {
        //alert(values);
        txtStudentID.SetText(values);

    },
    ClickLoadGrid: function (s, e) {
        var classID = txtclassSetup.GetText();


        if (classID.toString() == "") {
            alert("Please Select Classes");
            return;
        }
        $.ajax({
            url: "/HouseAllotmentExport/GetStudentListClassWise",
            type: "POST",
            data: { mClassesID: classID.toString() },
            beforeSend: (function (data) {
                LoadingPanelStudentDataExport.Show();
            }),
            success: function (data) {
                $("#divstudent").html(data);
                //var winHeight = document.getElementById('HouseBottomSplitter_1_CC').offsetHeight;
            
                //GridStudentData.SetHeight(winHeight);

            },
            error: function () {
            }
        }).done(function (data) {
            LoadingPanelStudentDataExport.Hide();

        });



    },

    ClickPersonalInfo: function (s, e) {
        var classID = txtclassSetup.GetText();


        if (classID.toString() == "") {
            alert("Please Select Classes");
            return;
        }
        $.ajax({
            url: "/HouseAllotmentExport/GetPersonalInfoListClassWise",
            type: "POST",
            data: { mClassesID: classID.toString() },
            beforeSend: (function (data) {
                LoadingPanelStudentDataExport.Show();
            }),
            success: function (data) {
                $("#HouseBottomSplitter_1_CC").html(data);
                var winHeight = document.getElementById('HouseBottomSplitter_1_CC').offsetHeight;

                GridStudentData.SetHeight(winHeight - 10);
                cbSelectAll.SetChecked(false);
            },
            error: function () {
            }
        }).done(function (data) {
            LoadingPanelStudentDataExport.Hide();

        });



    },


    ClickReportViewInfo: function (s, e) {

        var classID = txtclassSetup.GetText();

        if (classID.toString() == "") {
            alert("Please Select Classes");
            return;
        }
        var StudentID = txtStudentID.GetText();

        if (StudentID.toString() == "") {
            alert("Please Select Student");
            return;
        }

        

        var iframe = '<html><head><style>body, html {width: 100%; height: 100%; margin: 0; padding: 0}</style></head><body><iframe src="http://localhost:51734/ReportForms/HouseAllotmentExport.aspx?StudentID=' + StudentID + '&ClassID=' + classID + '"style="height:calc(100% - 4px);width:calc(100% - 4px)"></iframe></html></body>';

        var win = window.open("", "", "width=1000,height=480,top=150,left=250,toolbar=no,menubar=no,resizable=yes");

        win.document.write(iframe);






    },


    OnclickChangeReports: function () {


        var classID = txtclassSetup.GetText();

        if (classID.toString() == "") {
            alert("Please Select Classes");
            return;
        }
        var StudentID = txtStudentID.GetText();

        if (StudentID.toString() == "") {
            alert("Please Select Student");
            return;
        }
        
        var url = "/ReportForms/HouseAllotmentExport.aspx?StudentID=" + StudentID + "&ClassID=" + classID + ""
        //feesTransactionController.Setcoocies("popupstudentid", mstudentID, 1);
        //feesTransactionController.Setcoocies("popuptermid", mtermID, 1);
        //feesTransactionController.Setcoocies("popupclassid", mclassID, 1);


        PopupControlChangeReportView.SetContentUrl(url);

        var purl = PopupControlChangeReportView.GetContentUrl();


        PopupControlChangeReportView.Show();



    },

    CloseReportViewPopupLoading: function (s, e) {


        studentID = txtStudentID.GetText();




        LoadingPanelStudentDataExport.Show();



        //houseAllotmentExportController.GetFeeHeadList(studentID, termID);

        //houseAllotmentExportController.GetFeeCalculateamountdata(studentID, termID);

        setTimeout(houseAllotmentExportController.Closepopupfeeschange, 2000);

        //houseAllotmentExportController.Closepopupfeeschange();
    },

    Closepopupfeeschange: function (s, e) {
        LoadingPanelStudentDataExport.Hide();
        PopupControlChangeReportView.Hide();

    },

    //ClickParentsInfo: function (s, e) {
    //    var classID = txtclassSetup.GetText();
       
    //    if (classID.toString() == "") {
    //        alert("Please Select Classes");
    //        return;
    //    }
    //    $.ajax({
    //        url: "/StudentDataExport/GetParentsInfoListClassWise",
    //        type: "POST",
    //        data: { mClassesID: classID.toString() },
    //        beforeSend: (function (data) {
    //            LoadingPanelStudentDataExport.Show();
    //        }),
    //        success: function (data) {
    //            $("#StudentDataBottomSplitter_1_CC").html(data);
    //            var winHeight = document.getElementById('StudentDataBottomSplitter_1_CC').offsetHeight;

    //            GridStudentData.SetHeight(winHeight-10);
    //        },
    //        error: function () {
    //        }
    //    }).done(function (data) {
    //        LoadingPanelStudentDataExport.Hide();

    //    });



    //},

    //ClickGardianInfo: function (s, e) {
    //    var classID = txtclassSetup.GetText();


    //    if (classID.toString() == "") {
    //        alert("Please Select Classes");
    //        return;
    //    }
    //    $.ajax({
    //        url: "/StudentDataExport/GetGardianInfoListClassWise",
    //        type: "POST",
    //        data: { mClassesID: classID.toString() },
    //        beforeSend: (function (data) {
    //            LoadingPanelStudentDataExport.Show();
    //        }),
    //        success: function (data) {
    //            $("#StudentDataBottomSplitter_1_CC").html(data);
    //            var winHeight = document.getElementById('StudentDataBottomSplitter_1_CC').offsetHeight;

    //            GridStudentData.SetHeight(winHeight-10);
    //        },
    //        error: function () {
    //        }
    //    }).done(function (data) {
    //        LoadingPanelStudentDataExport.Hide();

    //    });



    //},

    SelectAllStudent: function (s, e) {

        if (s.GetChecked()) {
            GridStudentData.SelectRows();
         
        }
        else {
            GridStudentData.UnselectRows();
        }

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