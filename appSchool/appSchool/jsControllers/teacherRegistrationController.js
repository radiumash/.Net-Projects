var ShowFilterRow = false;


var teacherRegistrationController = {

    splitterResized: function (s, e) {
        switch (e.pane.name) {
            case 'TeacherRegistrationBody':
                var tt = $('#TeacherRegistrationSplitter_0_CC').width();
                GridTeachers.SetHeight(e.pane.GetClientHeight());
                GridTeachers.SetWidth(tt-10);

                break;
            case 'TeacherRegistrationFooter':

                //GridTeacherExprtiseData.SetHeight(e.pane.GetClientHeight() - 70);
                //GridTeacherQualificationData.SetHeight(e.pane.GetClientHeight() - 70);
               // GridSubjectExprtiseData.SetHeight(e.pane.GetClientHeight() - 70);
                //GridTeacherAchivmentData.SetHeight(e.pane.GetClientHeight() - 70);
                //tabControl = tabStudentRecord;
                //if (tabControl != 'undefined') {
                //    tabStudentRecord.SetWidth(e.pane.GetClientWidth());
                //};
                break;
        }
    },
    AddNewClicked: function (s, e) {
        GridTeachers.AddNewRow();
    },
    EditClicked: function (s, e) {
        rowIdx = GridTeachers.GetFocusedRowIndex();
        GridTeachers.StartEditRow(rowIdx);
    },
    FilterClicked: function (s, e) {
        ShowFilterRow = !ShowFilterRow;
        $.ajax({
            url: "/teacherRegistration/RefreshTeacherRegistrationGrid",
            type: "POST",
            data: { mShowFilter: ShowFilterRow },
            success: function (data) {
                $("#divtearegistration").html(data.ResultData);
                //$("#TeacherRegistrationSplitter_1_CC").html(data);
            },
            error: function () {
            }
        }).done(function (data) {
            loadingPanelStudentRegistration.Hide();
        });
    },
    RowSelectionChange: function OnGridFocusedRowChanged(s, e) {
      
        s.GetRowValues(s.GetFocusedRowIndex(), 'TeacherID', teacherRegistrationController.GetSelectedFieldValuesCallback);
        
    },

   
    ////RefreshTabsView: function (values) {
    ////    var regID;
    ////    if (values != null) {
    ////        regID = values;
    ////    }
     
    ////$.ajax({
    ////        url: "/TeacherRegistration/teacherGridRowChange",
    ////        type: "POST",
    ////        data: {RegID:regID},
    ////        success: function (data) {
    ////            $("#TeacherRegistrationSplitter_2_CC").html(data);
    ////        },
    ////        error: function () {
    ////        }
    ////    });
    ////    //$("#BodySplitter_1_CC").html("<h1>"+values+"</h1>");
    ////    //alert(values);
    ////},

    ////RefreshSignatureImg: function (s, e) {
    ////    var regID;
    ////    regID = TeacherID.GetText();


    ////    $.ajax({
    ////        url: "/TeacherRegistration/teacherGridRowChange",
    ////        type: "POST",
    ////        data: { RegID: regID },
    ////        success: function (data) {
    ////            $("#TeacherRegistrationSplitter_2_CC").html(data);
    ////        },
    ////        error: function () {
    ////        }
    ////    });
    ////    //$("#BodySplitter_1_CC").html("<h1>"+values+"</h1>");
    ////    //alert(values);
    ////}

    GetSelectedFieldValuesCallback: function (values) {
        TeacherIDs.SetText(values);
        var TeacherID;
        if (values != null) {
            TeacherID = values;
        }

        $.ajax({
            url: "/TeacherRegistration/GetTeacherFullName",
            type: "POST",
            data: { mTeacherID: TeacherID },
            success: function (data) {

                document.getElementById("TeacherFullName").innerHTML = data;


                //TeacherFullName.SetText(data.TeacherFullName);
                //btnPersonal.SetEnabled(true);
                //btnPhotoload.SetEnabled(true);
                //btnSignature.SetEnabled(true);
                //btnExperties.SetEnabled(true);
                //btnQualifications.SetEnabled(true);
                //btnSubjectExpertise.SetEnabled(true);
                //btnTeacherAchievements.SetEnabled(true);
            },
            error: function () {
            }
        });

    },


    RefreshClicked: function (s, e) {
        ShowFilterRow = !ShowFilterRow;
        $.ajax({
            url: "/TeacherRegistration/RefreshRegistrationGrid",
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


    ClickPersonalDetail: function (Ispopupcall) {
        var mTeacherIDs = 0;

        if (Ispopupcall == 1)
            mTeacherIDs = txtteacherIDpopup.GetText();
        else
            mTeacherIDs = TeacherIDs.GetText();

        if (mTeacherIDs.toString() == "") {
            alert("Please Select Teacher");
            return;
        }


        $.ajax({
            url: "/TeacherRegistration/EditPersonaldetailbyClick",
            type: "POST",
            data: { mTeacherID: mTeacherIDs },
            datatype:"json",
            beforeSend: (function (data) {
                loadingPanelTeacherRegistration.Show();
            }),
            success: function (data) {
                //$("#TeacherRegistrationSplitter_3_CC").html(data.ResultData);
                //var winHeight = document.getElementById('TeacherRegistrationSplitter_3_CC').offsetHeight;
                $("#divpopupteacherupdate").html(data.ResultData);
            },
            error: function () {
            }
        }).done(function (data) {
            loadingPanelTeacherRegistration.Hide();
            //btnPersonal.SetEnabled(false);
            //btnPhotoload.SetEnabled(true);
            //btnSignature.SetEnabled(true);
            //btnExperties.SetEnabled(true);
            //btnTeacherDocUpload.SetEnabled(true);
            //btnQualifications.SetEnabled(true);
            //btnSubjectExpertise.SetEnabled(true);
            //btnTeacherAchievements.SetEnabled(true);
        });
    },

    ClickPhotoLoadDetail: function (Ispopupcall) {

        var mTeacherIDs = 0;

        if (Ispopupcall == 1)
            mTeacherIDs = txtteacherIDpopup.GetText();
        else
            mTeacherIDs = TeacherIDs.GetText();

        if (mTeacherIDs.toString() == "") {
            alert("Please Select Teacher");
            return;
        }



        $.ajax({
            url: "/TeacherRegistration/EditUpLoadPhotobyClick",
            type: "POST",
            data: { mTeacherID: mTeacherIDs },
            datatype:"json",
            beforeSend: (function (data) {
                loadingPanelTeacherRegistration.Show();
            }),
            success: function (data) {
                //$("#TeacherRegistrationSplitter_3_CC").html(data.ResultData);
                $("#divpopupteacherupdate").html(data.ResultData);
            },
            error: function () {
            }
        }).done(function (data) {
            loadingPanelTeacherRegistration.Hide();
            //btnPersonal.SetEnabled(true);
            //btnPhotoload.SetEnabled(false);
            //btnSignature.SetEnabled(true);
            //btnExperties.SetEnabled(true);
            //btnTeacherDocUpload.SetEnabled(true);
            //btnQualifications.SetEnabled(true);
            //btnSubjectExpertise.SetEnabled(true);
            //btnTeacherAchievements.SetEnabled(true);
        });
    },




    ClickTeacherDocumentUploadDetail: function (Ispopupcall) {
        var mTeacherIDs = 0;

        if (Ispopupcall == 1)
            mTeacherIDs = txtteacherIDpopup.GetText();
        else
            mTeacherIDs = TeacherIDs.GetText();

        if (mTeacherIDs.toString() == "") {
            alert("Please Select Teacher");
            return;
        }


        $.ajax({
            url: "/TeacherRegistration/EditUpLoadDocsbyClick",
            type: "POST",
            data: { mTeacherID: mTeacherIDs },
            datatype: "json",
            beforeSend: (function (data) {
                loadingPanelTeacherRegistration.Show();
            }),
            success: function (data) {
                $("#divpopupteacherupdate").html(data);
                //$("#TeacherRegistrationSplitter_3_CC").html(data.ResultData);
            },
            error: function () {
            }
        }).done(function (data) {
            loadingPanelTeacherRegistration.Hide();
            //btnPersonal.SetEnabled(true);
            //btnPhotoload.SetEnabled(true);
            //btnTeacherDocUpload.SetEnabled(false);
            //btnSignature.SetEnabled(true);
            //btnExperties.SetEnabled(true);
            //btnQualifications.SetEnabled(true);
            //btnSubjectExpertise.SetEnabled(true);
            //btnTeacherAchievements.SetEnabled(true);
        });
    },

    ClickSignatureDetail: function (Ispopupcall) {

        var mTeacherIDs = 0;

        if (Ispopupcall == 1)
            mTeacherIDs = txtteacherIDpopup.GetText();
        else
            mTeacherIDs = TeacherIDs.GetText();

        if (mTeacherIDs.toString() == "") {
            alert("Please Select Teacher");
            return;
        }



        $.ajax({
            url: "/TeacherRegistration/EditSignatureDetailbyClickButton",
            type: "POST",
            data: { mTeacherID: mTeacherIDs },
            datatype:"json",
            beforeSend: (function (data) {
                loadingPanelTeacherRegistration.Show();
            }),
            success: function (data) {
                $("#divpopupteacherupdate").html(data.ResultData);
                //$("#TeacherRegistrationSplitter_3_CC").html(data.ResultData);
                //var winHeight = document.getElementById('TeacherRegistrationSplitter_3_CC').offsetHeight;


            },
            error: function () {
            }
        }).done(function (data) {
            loadingPanelTeacherRegistration.Hide();
            //btnPersonal.SetEnabled(true);
            //btnPhotoload.SetEnabled(true);
            //btnSignature.SetEnabled(false);
            //btnExperties.SetEnabled(true);
            //btnQualifications.SetEnabled(true);
            //btnSubjectExpertise.SetEnabled(true);
            //btnTeacherAchievements.SetEnabled(true);
            //btnTeacherDocUpload.SetEnabled(true);
        });
    },


    RefreshDocumentList: function (s, e) {
        var regID;
        regID = TeacherID.GetText();


        $.ajax({
            url: "/TeacherRegistration/StudentGridRowChange",
            type: "POST",
            data: { RegID: regID },
            success: function (data) {
                $("#RegistrationSplitter_2_CC").html(data);
            },
            error: function () {
            }
        });
        //$("#BodySplitter_1_CC").html("<h1>"+values+"</h1>");
        //alert(values);
    },


    ClickExpertiesDetail: function (Ispopupcall) {

        var mTeacherIDs = 0;

        if (Ispopupcall == 1)
            mTeacherIDs = txtteacherIDpopup.GetText();
        else
            mTeacherIDs = TeacherIDs.GetText();

        if (mTeacherIDs.toString() == "") {
            alert("Please Select Teacher");
            return;
        }


        $.ajax({
            url: "/TeacherRegistration/EditExpertiesDetailbyClickButton",
            type: "POST",
            data: { mTeacherID: mTeacherIDs },
            datatype:"json",
            beforeSend: (function (data) {
                loadingPanelTeacherRegistration.Show();
            }),
            success: function (data) {
                $("#divpopupteacherupdate").html(data.ResultData);
                //$("#TeacherRegistrationSplitter_3_CC").html(data.ResultData);
                //var winHeight = document.getElementById('TeacherRegistrationSplitter_3_CC').offsetHeight;


            },
            error: function () {
            }
        }).done(function (data) {
            loadingPanelTeacherRegistration.Hide();
            //btnPersonal.SetEnabled(true);
            //btnPhotoload.SetEnabled(true);
            //btnTeacherDocUpload.SetEnabled(true);
            //btnSignature.SetEnabled(true);
            //btnExperties.SetEnabled(false);
            //btnQualifications.SetEnabled(true);
            //btnSubjectExpertise.SetEnabled(true);
            //btnTeacherAchievements.SetEnabled(true);

        });
    },

    ClickQualificationsDetail: function (Ispopupcall) {

        var mTeacherIDs = 0;

        if (Ispopupcall == 1)
            mTeacherIDs = txtteacherIDpopup.GetText();
        else
            mTeacherIDs = TeacherIDs.GetText();

        if (mTeacherIDs.toString() == "") {
            alert("Please Select Teacher");
            return;
        }


        $.ajax({
            url: "/TeacherRegistration/EditQualificationsDetailbyClickButton",
            type: "POST",
            data: { mTeacherID: mTeacherIDs },
            datatype:"json",
            beforeSend: (function (data) {
                loadingPanelTeacherRegistration.Show();
            }),
            success: function (data) {
                $("#divpopupteacherupdate").html(data.ResultData);
                //$("#TeacherRegistrationSplitter_3_CC").html(data.ResultData);
                //var winHeight = document.getElementById('TeacherRegistrationSplitter_3_CC').offsetHeight;
            },
            error: function () {
            }
        }).done(function (data) {
            loadingPanelTeacherRegistration.Hide();
            //btnPersonal.SetEnabled(true);
            //btnPhotoload.SetEnabled(true);
            //btnSignature.SetEnabled(true);
            //btnTeacherDocUpload.SetEnabled(true);
            //btnExperties.SetEnabled(true);
            //btnQualifications.SetEnabled(false);
            //btnSubjectExpertise.SetEnabled(true);
            //btnTeacherAchievements.SetEnabled(true);
        });
    },


    ClickSubjectExpertisDetail: function (Ispopupcall) {

        var mTeacherIDs = 0;

        if (Ispopupcall == 1)
            mTeacherIDs = txtteacherIDpopup.GetText();
        else
            mTeacherIDs = TeacherIDs.GetText();

        if (mTeacherIDs.toString() == "") {
            alert("Please Select Teacher");
            return;
        }


        $.ajax({
            url: "/TeacherRegistration/EditSubjectExpertisDetailbyClickButton",
            type: "POST",
            data: { mTeacherID: mTeacherIDs },
            datatype:"json",
            beforeSend: (function (data) {
                loadingPanelTeacherRegistration.Show();
            }),
            success: function (data) {

                //$("#TeacherRegistrationSplitter_3_CC").html(data.ResultData);
                //var winHeight = document.getElementById('TeacherRegistrationSplitter_3_CC').offsetHeight;
                $("#divpopupteacherupdate").html(data.ResultData);

            },
            error: function () {
            }
        }).done(function (data) {
            loadingPanelTeacherRegistration.Hide();
            //btnPersonal.SetEnabled(true);
            //btnPhotoload.SetEnabled(true);
            //btnSignature.SetEnabled(true);
            //btnExperties.SetEnabled(true);
            //btnTeacherDocUpload.SetEnabled(true);
            //btnQualifications.SetEnabled(true);
            //btnSubjectExpertise.SetEnabled(false);
            //btnTeacherAchievements.SetEnabled(true);
        });
    },


    ClickTeacherAchievementsDetail: function (Ispopupcall) {

        var mTeacherIDs = 0;

        if (Ispopupcall == 1)
            mTeacherIDs = txtteacherIDpopup.GetText();
        else
            mTeacherIDs = TeacherIDs.GetText();

        if (mTeacherIDs.toString() == "") {
            alert("Please Select Teacher");
            return;
        }


        $.ajax({
            url: "/TeacherRegistration/EditTeacherAchievementsDetailbyClickButton",
            type: "POST",
            data: { mTeacherID: mTeacherIDs },
            datatype:"json",
            beforeSend: (function (data) {
                loadingPanelTeacherRegistration.Show();
            }),
            success: function (data) {
                $("#divpopupteacherupdate").html(data.ResultData);
                //$("#TeacherRegistrationSplitter_3_CC").html(data.ResultData);
                //var winHeight = document.getElementById('TeacherRegistrationSplitter_3_CC').offsetHeight;


            },
            error: function () {
            }
        }).done(function (data) {
            loadingPanelTeacherRegistration.Hide();
            //btnPersonal.SetEnabled(true);
            //btnPhotoload.SetEnabled(true);
            //btnSignature.SetEnabled(true);
            //btnTeacherDocUpload.SetEnabled(true);
            //btnExperties.SetEnabled(true);
            //btnQualifications.SetEnabled(true);
            //btnSubjectExpertise.SetEnabled(true);
            //btnTeacherAchievements.SetEnabled(false);
        });
    },





    RefreshSignatureImg: function (s, e) {
        var regID;
        regID = TeacherID.GetText();


        $.ajax({
            url: "/TeacherRegistration/teacherGridRowChange",
            type: "POST",
            data: { RegID: regID },
            success: function (data) {
                $("#TeacherRegistrationSplitter_2_CC").html(data);
            },
            error: function () {
            }
        });
        //$("#BodySplitter_1_CC").html("<h1>"+values+"</h1>");
        //alert(values);
    },
    TeacherDetailPopupItemClicked: function (s, e) {
        var selItem = e.item.name;

        console.log(selItem)

        if (selItem == "updatepersonalpop") {
            teacherRegistrationController.ClickPersonalDetail(1);
        }
        else if (selItem == "uploadphotppop") {
            teacherRegistrationController.ClickPhotoLoadDetail(1);
        }
        else if (selItem == "signature") {
            teacherRegistrationController.ClickSignatureDetail(1);
        }
        else if (selItem == "uploaddocpop") {
            teacherRegistrationController.ClickTeacherDocumentUploadDetail(1);
        }
        else if (selItem == "experties") {
            teacherRegistrationController.ClickExpertiesDetail(1);
        }
        else if (selItem == "qualifications") {
            teacherRegistrationController.ClickQualificationsDetail(1);
        }
        else if (selItem == "subjectExpertise") {
            teacherRegistrationController.ClickSubjectExpertisDetail(1);
        }
        else if (selItem == "achievements") {
            teacherRegistrationController.ClickTeacherAchievementsDetail(1);
        }
        //else if (selItem == "previoussessiondetailpop") {
        //    teacherRegistrationController.ClickStudentBackLockSessionDetail(1);
        //}
        //else if (selItem == "previousDetailpop") {
        //    teacherRegistrationController.ClickStudentPreviousDetail(1);
        //}
        //else if (selItem == "refreshpop") {
        //    studentRegistrationController.FilterClicked(1);
        //}

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
            url: "/TeacherRegistration/EditParentdetailbyClick",
            type: "POST",
            data: { mStudentID: mStudentIDs },
            beforeSend: (function (data) {
                loadingPanelStudentRegistration.Show();
            }),

            success: function (data) {

                $("#divpopupteacherupdate").html(data.ResultData);
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


    OpenTeacherPopupForUpdateDetails: function (value) {

        var teacherID = value;
        txtteacherIDpopup.SetText(teacherID)
        PopupStudentUpdate.Show();

        teacherRegistrationController.ClickPersonalDetail(1);
    },

    CloseTeacherPopupForUpdateDetails: function (value) {

        PopupStudentUpdate.Hide();
    }

}
