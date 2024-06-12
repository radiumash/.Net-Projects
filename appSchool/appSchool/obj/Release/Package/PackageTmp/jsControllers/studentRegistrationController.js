var ShowFilterRow = false;


var studentRegistrationController = {

    splitterResized: function (s, e) {
        switch (e.pane.name) {
            case 'RegistrationBody':
                GridStudents.SetHeight(e.pane.GetClientHeight() - 1);
                break;
            case 'RegistrationFooter':
               
                //$("#pcClientSideAPI_CC").SetHeight(e.pane.GetClientHeight() - 100);
               // GridDocuments.SetHeight(e.pane.GetClientHeight() - 70);
             
                //tabControl = tabStudentRecord;
                //if (tabControl != 'undefined') {
                //    tabStudentRecord.SetWidth(e.pane.GetClientWidth());
                //};
                break;
        }
    },
    AddNewClicked: function (s, e) {
        GridStudents.AddNewRow();
    },
    EditClicked: function (s, e) {
        rowIdx = GridStudents.GetFocusedRowIndex();
        GridStudents.StartEditRow(rowIdx);
    },

    DeleteClicked: function (s, e) {
        alert("call delete");
        rowIdx = GridStudents.GetFocusedRowIndex();
        GridStudents.DeleteRow(rowIdx);
    },


    FilterClicked: function (s, e) {
        ShowFilterRow = !ShowFilterRow;
        $.ajax({
            url: "/StudentRegistration/RefreshRegistrationGrid",
            type: "POST",
            data: { mShowFilter: ShowFilterRow },
            beforeSend: (function (data) {
                loadingPanelStudentRegistration.Show();
            }),
            success: function (data) {
                $("#divstudregistration").html(data);
            },
            error: function () {
            }
        }).done(function (data) {
            loadingPanelStudentRegistration.Hide();
        });
       
    },
   

    RefreshClicked: function (s, e) {
        ShowFilterRow = !ShowFilterRow;
        $.ajax({
            url: "/StudentRegistration/RefreshRegistrationGrid",
            type: "POST",
            data: { mShowFilter: ShowFilterRow },

            beforeSend: (function (data) {
                loadingPanelStudentRegistration.Show();
            }),
            success: function (data) {
                $("#divstudregistration").html(data);
                //var winHeight = document.getElementById('divstudregistration').offsetHeight;
                //  GridStudents.SetHeight(winHeight - 10);
            },
            error: function () {
            }
        }).done(function (data) {
            loadingPanelStudentRegistration.Hide();
        });
    },


    RowSelectionChange: function OnGridFocusedRowChanged(s, e) {
        s.GetRowValues(s.GetFocusedRowIndex(), 'StudentID', studentRegistrationController.GetSelectedFieldValuesCallback);

       
    },

    GetSelectedFieldValuesCallback: function (values) {
        StudentIDs.SetText(values);

        var regID;
        if (values != null) {
            regID = values;
        }
        $.ajax({
            url: "/StudentRegistration/GetStudentFullName",
            type: "POST",
            data: { StudentID: regID },
            success: function (data) {
                /*StudentFullName.SetText(data);*/

                document.getElementById("StudentFullName").innerHTML = data;
                //$("#RegistrationSplitter_3_CC").html("");
                //btnPersonal.SetEnabled(true);
                //btnParnt.SetEnabled(true);
                //btnDocLoad.SetEnabled(true);
                //btnPhotoload.SetEnabled(true);
                //btnPreviousDetail.SetEnabled(true);
                //btnPreviousSessiondetail.SetEnabled(true);
            },
            error: function () {
            }
        });




    },


    //ClickPersonalDetail: function (s, e) {
      ClickPersonalDetail: function (Ispopupcall) {

          var mStudentIDs = 0;

          if (Ispopupcall == 1)
              mStudentIDs = txtstudentIDpopup.GetText();
          else
              mStudentIDs = StudentIDs.GetText();

        if (mStudentIDs.toString() == "") {
            alert("Please Select Students");
            return;
        }

        $.ajax({
            url: "/StudentRegistration/EditPersonaldetailbyClick",
            type: "POST",
            data: { mStudentID: mStudentIDs },
            datatype: "json",
            beforeSend: (function (data) {
                loadingPanelStudentRegistration.Show();
            }),
            success: function (data) {

                //console.log('Ispopupcall' + Ispopupcall)

                $("#divpopupstudentupdate").html(data.ResultData);

                //if (Ispopupcall == 1)
                //    $("#divpopupstudentupdate").html(data.ResultData);
                //else
                //    $("#divstudregistration").html(data.ResultData);
                //var winHeight = document.getElementById('divstudregistration').offsetHeight;



            },
            error: function () {
            }
        }).done(function (data) {
            loadingPanelStudentRegistration.Hide();

            //btnPersonal.SetEnabled(false);
            //btnParnt.SetEnabled(true);
            //btnDocLoad.SetEnabled(true);
            //btnPhotoload.SetEnabled(true);
            //btnPreviousDetail.SetEnabled(true);
            //btnPreviousSessiondetail.SetEnabled(true);
        });
    },

    ClickParntDetail: function (Ispopupcall) {

        var mStudentIDs = 0;

        if (Ispopupcall == 1)
            mStudentIDs = txtstudentIDpopup.GetText();
        else
            mStudentIDs = StudentIDs.GetText();

        if (mStudentIDs.toString() == "") {
            alert("Please Select Students");
            return;
        }


        $.ajax({
            url: "/StudentRegistration/EditParentdetailbyClick",
            type: "POST",
            data: { mStudentID: mStudentIDs },
            beforeSend: (function (data) {
                loadingPanelStudentRegistration.Show();
            }),

            success: function (data) {

                $("#divpopupstudentupdate").html(data.ResultData);
                //if (Ispopupcall == 1)
                //    $("#divpopupstudentupdate").html(data.ResultData);
                //else
                //    $("#divstudregistration").html(data.ResultData);
                //var winHeight = document.getElementById('divstudregistration').offsetHeight;


            },
            error: function () {
            }
        }).done(function (data) {
            loadingPanelStudentRegistration.Hide();
            //btnPersonal.SetEnabled(true);
            //btnParnt.SetEnabled(false);
            //btnDocLoad.SetEnabled(true);
            //btnPhotoload.SetEnabled(true);
            //btnPreviousDetail.SetEnabled(true);
            //btnPreviousSessiondetail.SetEnabled(true);
        });
    },


    ClickDocsLoadDetail: function (Ispopupcall) {

        var mStudentIDs = 0;

        if (Ispopupcall == 1)
            mStudentIDs = txtstudentIDpopup.GetText();
        else
            mStudentIDs = StudentIDs.GetText();

        if (mStudentIDs.toString() == "") {
            alert("Please Select Students");
            return;
        }


        $.ajax({
            url: "/StudentRegistration/EditUpLoadDocsbyClick",
            type: "POST",
            data: { mStudentID: mStudentIDs },
            beforeSend: (function (data) {
                loadingPanelStudentRegistration.Show();
            }),

            success: function (data) {

                $("#divpopupstudentupdate").html(data);

                //if (Ispopupcall == 1)
                //    $("#divpopupstudentupdate").html(data);
                //else
                //    $("#divstudregistration").html(data);
                //var winHeight = document.getElementById('divstudregistration').offsetHeight;


            },
            error: function () {
            }
        }).done(function (data) {
            loadingPanelStudentRegistration.Hide();
            //btnPersonal.SetEnabled(true);
            //btnParnt.SetEnabled(true);
            //btnDocLoad.SetEnabled(false);
            //btnPhotoload.SetEnabled(true);
            //btnPreviousDetail.SetEnabled(true);
            //btnPreviousSessiondetail.SetEnabled(true);
        });
    },

    ClickPhotoLoadDetail: function (Ispopupcall) {

        var mStudentIDs = 0;

        if (Ispopupcall == 1)
            mStudentIDs = txtstudentIDpopup.GetText();
        else
            mStudentIDs = StudentIDs.GetText();
        if (mStudentIDs.toString() == "") {
            alert("Please Select Students");
            return;
        }


        $.ajax({
            url: "/StudentRegistration/EditUpLoadPhotobyClick",
            type: "POST",
            data: { mStudentID: mStudentIDs },
            beforeSend: (function (data) {
                loadingPanelStudentRegistration.Show();
            }),
            success: function (data) {

                $("#divpopupstudentupdate").html(data);

                //if (Ispopupcall == 1)
                //    $("#divpopupstudentupdate").html(data);
                //else
                //    $("#divstudregistration").html(data);
                //var winHeight = document.getElementById('divstudregistration').offsetHeight;


            },
            error: function () {
            }
        }).done(function (data) {
            loadingPanelStudentRegistration.Hide();
            //btnPersonal.SetEnabled(true);
            //btnParnt.SetEnabled(true);
            //btnDocLoad.SetEnabled(true);
            //btnPhotoload.SetEnabled(false);
            //btnPreviousDetail.SetEnabled(true);
            //btnPreviousSessiondetail.SetEnabled(true);
        });
    },

    ClickStudentPreviousDetail: function (Ispopupcall) {
        var mStudentIDs = 0;

        if (Ispopupcall == 1)
            mStudentIDs = txtstudentIDpopup.GetText();
        else
            mStudentIDs = StudentIDs.GetText();

        if (mStudentIDs.toString() == "") {
            alert("Please Select Student");
            return;
        }

        $.ajax({
            url: "/StudentRegistration/EditPreviousDetail",
            type: "Post",
            datatype: "json",
            data: { mStudentID: mStudentIDs },
            beforeSend: (function (data) {
                loadingPanelStudentRegistration.Show();
            }),
            success: function (data) {

                $("#divpopupstudentupdate").html(data.ResultData);

                //if (Ispopupcall == 1)
                //    $("#divpopupstudentupdate").html(data.ResultData);
                //else
                //    $("#divstudregistration").html(data.ResultData);
               // var winHeight = document.getElementById('divstudregistration').offsetHeight;

            },
            error: function () {
            }
        }).done(function (data) {
            loadingPanelStudentRegistration.Hide();
            //btnPersonal.SetEnabled(true);
            //btnParnt.SetEnabled(true);
            //btnDocLoad.SetEnabled(true);
            //btnPhotoload.SetEnabled(true);
            //btnPreviousDetail.SetEnabled(false);
            //btnPreviousSessiondetail.SetEnabled(true);
        });

    },

    ClickStudentBackLockSessionDetail: function (Ispopupcall) {

        var mStudentIDs = 0;

        if (Ispopupcall == 1)
            mStudentIDs = txtstudentIDpopup.GetText();
        else
            mStudentIDs = StudentIDs.GetText();

        if (mStudentIDs.toString() == "") {
            alert("Please Select Students");
            return;
        }

        $.ajax({
            url: "/StudentRegistration/EditStudentBackLockSessionDetailbyClick",
            type: "POST",
            data: { mStudentID: mStudentIDs },
            datatype: "json",
            beforeSend: (function (data) {
                loadingPanelStudentRegistration.Show();
            }),
            success: function (data) {

                $("#divpopupstudentupdate").html(data);

                //if (Ispopupcall == 1)
                //    $("#divpopupstudentupdate").html(data);
                //else
                //    $("#divstudregistration").html(data);
                //var winHeight = document.getElementById('divstudregistration').offsetHeight;
                //GridAllSession.SetHeight(winHeight - 10);


            },
            error: function () {
            }
        }).done(function (data) {
            loadingPanelStudentRegistration.Hide();

            //btnPersonal.SetEnabled(true);
            //btnParnt.SetEnabled(true);
            //btnDocLoad.SetEnabled(true);
            //btnPhotoload.SetEnabled(true);
            //btnPreviousDetail.SetEnabled(true);
            //btnPreviousSessiondetail.SetEnabled(false);
            //btnPreviousSessiondetail.SetEnabled(false);
        });
    },

    RefreshTabsView: function (values) {
        var regID;
        if (values != null) {
            regID = values;
        }
    $.ajax({
            url: "/StudentRegistration/StudentGridRowChange",
            type: "POST",
            data: {RegID:regID},
            success: function (data) {
                $("#RegistrationSplitter_2_CC").html(data);
            },
            error: function () {
            }
        });
        //$("#BodySplitter_1_CC").html("<h1>"+values+"</h1>");
        //alert(values);
    },


    RowSelectionChangeByClick: function(s,e)
    {
        var values = s.name;
        var regID;
        if (values != null) {
            regID = values;
        }
        $.ajax({
            url: "/StudentRegistration/StudentGridRowChange",
            type: "POST",
            data: { RegID: regID },
            success: function (data) {
                $("#RegistrationSplitter_2_CC").html(data);
            },
            error: function () {
            }
        });


    },

    RefreshDocumentList: function (s,e) {
        var regID;
        regID=StudentID.GetText();
      

        $.ajax({
            url: "/StudentRegistration/StudentGridRowChange",
            type: "POST",
            data: {RegID:regID},
            success: function (data) {
                $("#RegistrationSplitter_2_CC").html(data);
            },
            error: function () {
            }
        });
        //$("#BodySplitter_1_CC").html("<h1>"+values+"</h1>");
        //alert(values);
    },


    StudentDetailItemClicked: function (s, e)
    {
        var selItem = e.item.name;

        //var studentID = StudentIDs.GetText();;
        //txtstudentIDpopup.SetText(studentID)

        
        
        if (selItem == "updatepersonal") {
            studentRegistrationController.ClickPersonalDetail(0);
        }
        else if (selItem == "updateparant") {
            studentRegistrationController.ClickParntDetail(0);
        }
        else if (selItem == "uploaddoc") {
            studentRegistrationController.ClickDocsLoadDetail(0);
        }
        else if (selItem == "uploadphotp") {
            studentRegistrationController.ClickPhotoLoadDetail(0);
        }
        else if (selItem == "previoussessiondetail") {
            studentRegistrationController.ClickStudentBackLockSessionDetail(0);
        }
        else if (selItem == "previousDetail") {
            studentRegistrationController.ClickStudentPreviousDetail(0);
        }
        else if (selItem == "refresh") {
            studentRegistrationController.FilterClicked();
        }

       
        //PopupStudentUpdate.Show();
       
        
    },



    StudentDetailPopupItemClicked: function (s, e) {
        var selItem = e.item.name;

        console.log(selItem)

        if (selItem == "updatepersonalpop") {
            studentRegistrationController.ClickPersonalDetail(1);
        }
        else if (selItem == "updateparantpop") {
            studentRegistrationController.ClickParntDetail(1);
        }
        else if (selItem == "uploaddocpop") {
            studentRegistrationController.ClickDocsLoadDetail(1);
        }
        else if (selItem == "uploadphotppop") {
            studentRegistrationController.ClickPhotoLoadDetail(1);
        }
        else if (selItem == "previoussessiondetailpop") {
            studentRegistrationController.ClickStudentBackLockSessionDetail(1);
        }
        else if (selItem == "previousDetailpop") {
            studentRegistrationController.ClickStudentPreviousDetail(1);
        }
        //else if (selItem == "refreshpop") {
        //    studentRegistrationController.FilterClicked(1);
        //}
        
    },

    OpenStudentPopupForUpdateDetails: function (value) {

        var studentID = value;
        txtstudentIDpopup.SetText(studentID)
        PopupStudentUpdate.Show();

        studentRegistrationController.ClickPersonalDetail(1);
    },

    CloseStudentPopupForUpdateDetails: function (value) {

        PopupStudentUpdate.Hide();
    },


   

}


