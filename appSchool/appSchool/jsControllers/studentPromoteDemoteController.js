var studentPromoteDemoteController = {

    SelectedClass: function (s, e) {
        var classID = s.GetValue();
        //alert(classID);
        if (classID.toString() == "") {
            alert("Please Select Classes");
            return;
        }

        $.ajax({
            url: "/StudentPromoteDemote/GetSectionListView",
            type: "POST",
            data: { mClassID: classID.toString() },
            beforeSend: (function (data) {
                loadingPanelMenu.Show();
            }),
            success: function (data) {
                //$("#divstudsession").html(data);

                var FromSectionList = JSON.parse(data.SectionList);
                FromSectionID.ClearItems();
                for (var i = 0; i < FromSectionList.length; i++) {
                    FromSectionID.AddItem(FromSectionList[i].ClassDescription , FromSectionList[i].ClassSetupID);
                }

            },
            error: function () {
            }
        }).done(function (data) {
            loadingPanelMenu.Hide();

        });

    },

    SelectedToClass: function (s, e) {
        var classID = s.GetValue();
        //alert(classID);
        if (classID.toString() == "") {
            alert("Please Select Classes");
            return;
        }

        $.ajax({
            url: "/StudentPromoteDemote/GetSectionListView",
            type: "POST",
            data: { mClassID: classID.toString() },
            beforeSend: (function (data) {
                loadingPanelMenu.Show();
            }),
            success: function (data) {
                //$("#divstudsession").html(data);

                var ToSectionList = JSON.parse(data.SectionList);
                ToSectionID.ClearItems();
                for (var i = 0; i < ToSectionList.length; i++) {
                    ToSectionID.AddItem(ToSectionList[i].ClassDescription, ToSectionList[i].ClassSetupID);
                }

            },
            error: function () {
            }
        }).done(function (data) {
            loadingPanelMenu.Hide();

        });

    },

    GetStudentDetails: function (s, e) {
        var classID = ClassID.GetValue();
        //alert(classID);
        if (classID.toString() == "") {
            alert("Please Select Classes");
            return;
        }

        var classesSetupID = FromSectionID.GetValue();
        //alert(classID);
        if (classesSetupID.toString() == "") {
            alert("Please Select Section");
            return;
        }

       
        ToSectionID.SetEnabled(false);


        $.ajax({
            url: "/StudentPromoteDemote/GetAllStudentListView",
            type: "POST",
            data: { mClassesID: classID.toString(), mClassesSetupID: classesSetupID.toString() },
            beforeSend: (function (data) {
                loadingPanelMenu.Show();
            }),
            success: function (data) {
                $("#divstudsession").html(data);

                btnpromtedemote.SetEnabled(true)
            },
            error: function () {
            }
        }).done(function (data) {
            loadingPanelMenu.Hide();

        });

    },

    SelectionChanged: function (s, e) {
        s.GetSelectedFieldValues("StudentSessionID", studentPromoteDemoteController.GetSelectedFieldValuesCallback);
    },

    GetSelectedFieldValuesCallback: function (values) {
        StudentIDs.SetText(values);
    },

    RowSelectionChange: function OnGridFocusedRowChanged(s, e) {
        s.GetRowValues(s.GetFocusedRowIndex(), 'StudentID', studentPromoteDemoteController.GetSelectedFieldValuesCallback);
        s.GetRowValues(s.GetFocusedRowIndex(), 'FullName', studentPromoteDemoteController.GetSelectedStudentNameValuesCallback);
    },

    GetSelectedFieldValuesCallback: function (values) {
        StudentIDs.SetText(values);

    },

    GetSelectedStudentNameValuesCallback: function (values) {
       
        
        var mstudentName = '';
        if (values != null) {
            txtStudentName.SetText(values);
            mstudentName = values;
        }
            



        var defaultmsg = "Single Click on the List to Select Student '" + mstudentName +"' ";
        $('#divresmsg').empty();
        var html = "<h5>" + defaultmsg + "</h5>"
        $('#divresmsg').append(html);

    },

    SelectAllStudent: function (s, e) {

        if (s.GetChecked()) {
            GridStudentSession.SelectRows();
        }
        else {
            GridStudentSession.UnselectRows();
        }

    },

    OpenMarksDeletePopUp: function () {

        var mstudentID = StudentIDs.GetText();

        var url = "/ExamMarksDelete/Index?ispoup=1&mstudid=" + mstudentID + "";
       
        Popupmarksentrydelete.SetContentUrl(url);

        //var purl = Popupmarksentrydelete.GetContentUrl();

        Popupmarksentrydelete.Show();
    },

    SavePromteDemote: function (s, e) {
        var classID = ClassID.GetValue();
        if (classID == null) {
            alert("Please Select Classes");
            return;
        }

        var classesSetupID = FromSectionID.GetValue();
       
        if (classesSetupID == null) {
            alert("Please Select Section");
            return;
        }

        var toclassID = ToClassID.GetValue();
      
        if (toclassID == null) {
            alert("Please Select To Classes");
            return;
        }

        var toclassesSetupID = ToSectionID.GetValue();

        if (toclassesSetupID == null) {
            alert("Please Select To Section");
            return;
        }

        var studentID = StudentIDs.GetText();

        if (studentID.toString() == "") {
            alert("Please Select Student");
            return;
        }


        var className = ClassID.GetText();
        var toclassName = ToClassID.GetText();
        var studentName = txtStudentName.GetText();


       

        $.ajax({
            url: "/StudentPromoteDemote/SaveStudentPromoteDetaote",
            type: "POST",
            //data: { mClassesID: classID, mClassesSetupID: classesSetupID, mToClassID: toclassID, mClassesSetupID: toclassesSetupID, mStudentID: mStudentID.toString() },
            data: { mClassesID: classID, mClassesSetupID: classesSetupID, mToClassID: toclassID, mToClassesSetupID: toclassesSetupID, mStudentID: studentID.toString(), mClassName: className, mtoClassName: toclassName, mStudentName: studentName },
            beforeSend: (function (data) {
                loadingPanelMenu.Show();
            }),
            success: function (data) {


                loadingPanelMenu.Hide();

                var resMessage = data.DisplayMsg;
                var responseCode = data.ResponseCode;
                alert(resMessage)

                if (responseCode == 3 || responseCode == 4) {
                    let text = "Please delete first exam marks entry, Prease Ok to continue";
                    if (confirm(text) == true) {
                        //studentPromoteDemoteController.RedirectToExamMarksDelete();
                        studentPromoteDemoteController.OpenMarksDeletePopUp();
                    } else {
                        alert("You canceled!");
                    }
                }
                else
                {
                    $('#divresmsg').empty();
                    var html = "<h5>" + resMessage + "</h5>"
                    $('#divresmsg').append(html);

                    console.log(data.ButtonAttendanceEnabled)

                    if (data.ButtonAttendanceEnabled == 1) {
                        btntransferattendance.SetEnabled(true)

                        btnpfeesstructure.SetEnabled(false)
                        btnrecreatefeesstructure.SetEnabled(false)
                        btnpromtedemote.SetEnabled(false)
                    }
                    else
                    {
                        
                        btntransferattendance.SetEnabled(false)
                        btnpfeesstructure.SetEnabled(true)
                        btnrecreatefeesstructure.SetEnabled(false)
                        btnpromtedemote.SetEnabled(false)

                        ClassID.SetEnabled(false)
                        FromSectionID.SetEnabled(false)
                        ToClassID.SetEnabled(false)
                        ToSectionID.SetEnabled(false)
                        
                    }

                }

                
            },
            error: function () {
            }
        }).done(function (data) {
            loadingPanelMenu.Hide();

        });

    },

    RedirectToExamMarksDelete: function () {

        $.ajax({

            url: "/ExamMarksDelete/Index",
            beforeSend: (function (data) {
                //$("#maincontentpage").html(null);
                loadingPanelMenu.Show();
            }),
            success: function (data) {
                $("#maincontentpage").html(data);
            }
        }).done(function (data) {

            loadingPanelMenu.Hide()

        });
    },

    TransferAttendance: function (s, e) {
        var classID = ClassID.GetValue();
        if (classID.toString() == "") {
            alert("Please Select Classes");
            return;
        }

        var classesSetupID = FromSectionID.GetValue();

        if (classesSetupID.toString() == "") {
            alert("Please Select Section");
            return;
        }

        var toclassID = ToClassID.GetValue();
        if (toclassID.toString() == "") {
            alert("Please Select To Classes");
            return;
        }

        var toclassesSetupID = ToSectionID.GetValue();

        if (toclassesSetupID.toString() == "") {
            alert("Please Select To Section");
            return;
        }

        var studentID = StudentIDs.GetText();

        if (studentID.toString() == "") {
            alert("Please Select Student");
            return;
        }

        $.ajax({
            url: "/StudentPromoteDemote/TransferStudentAttendance",
            type: "POST",
            //data: { mClassesID: classID, mClassesSetupID: classesSetupID, mToClassID: toclassID, mClassesSetupID: toclassesSetupID, mStudentID: mStudentID.toString() },
            data: { mClassesID: classID, mClassesSetupID: classesSetupID, mToClassID: toclassID, mToClassesSetupID: toclassesSetupID, mStudentID: studentID.toString() },
            beforeSend: (function (data) {
                loadingPanelMenu.Show();
            }),
            success: function (data) {


                loadingPanelMenu.Hide();

                var resMessage = data.DisplayMsg;
                var responseCode = data.ResponseCode;
                alert(resMessage)


                $('#divresmsg').empty();
                var html = "<h5>" + resMessage + "</h5>"
                $('#divresmsg').append(html);

                btntransferattendance.SetEnabled(false)
                btnpfeesstructure.SetEnabled(true)
                btnrecreatefeesstructure.SetEnabled(false)
                btnpromtedemote.SetEnabled(false)

            },
            error: function () {
            }
        }).done(function (data) {
            loadingPanelMenu.Hide();

        });

    },

    DeleteFeesStructure: function (s, e) {
        var classID = ClassID.GetValue();
        if (classID.toString() == "") {
            alert("Please Select Classes");
            return;
        }

        var classesSetupID = FromSectionID.GetValue();

        if (classesSetupID.toString() == "") {
            alert("Please Select Section");
            return;
        }

        var toclassID = ToClassID.GetValue();
        if (toclassID.toString() == "") {
            alert("Please Select To Classes");
            return;
        }

        var toclassesSetupID = ToSectionID.GetValue();

        if (toclassesSetupID.toString() == "") {
            alert("Please Select To Section");
            return;
        }

        var studentID = StudentIDs.GetText();

        if (studentID.toString() == "") {
            alert("Please Select Student");
            return;
        }


        var className = ClassID.GetText();
        var toclassName = ToClassID.GetText();
        var studentName = txtStudentName.GetText();

        $.ajax({
            url: "/StudentPromoteDemote/DeleteFeesStructure",
            type: "POST",
            //data: { mClassesID: classID, mClassesSetupID: classesSetupID, mToClassID: toclassID, mClassesSetupID: toclassesSetupID, mStudentID: mStudentID.toString() },
            data: { mClassesID: classID, mClassesSetupID: classesSetupID, mToClassID: toclassID, mToClassesSetupID: toclassesSetupID, mStudentID: studentID.toString(), mClassName: className, mtoClassName: toclassName, mStudentName: studentName },
            beforeSend: (function (data) {
                loadingPanelMenu.Show();
            }),
            success: function (data) {


                loadingPanelMenu.Hide();

                var resMessage = data.DisplayMsg;
                var responseCode = data.ResponseCode;
                alert(resMessage)


                $('#divresmsg').empty();
                var html = "<h5>" + resMessage + "</h5>"
                $('#divresmsg').append(html);

                btntransferattendance.SetEnabled(false)
                btnpfeesstructure.SetEnabled(false)
                btnrecreatefeesstructure.SetEnabled(true)
                btnpromtedemote.SetEnabled(false)

            },
            error: function () {
            }
        }).done(function (data) {
            loadingPanelMenu.Hide();

        });

    },

    Recreatefeesstructure: function (s, e) {
        var classID = ClassID.GetValue();
        if (classID.toString() == "") {
            alert("Please Select Classes");
            return;
        }

        var classesSetupID = FromSectionID.GetValue();

        if (classesSetupID.toString() == "") {
            alert("Please Select Section");
            return;
        }

        var toclassID = ToClassID.GetValue();
        if (toclassID.toString() == "") {
            alert("Please Select To Classes");
            return;
        }

        var toclassesSetupID = ToSectionID.GetValue();

        if (toclassesSetupID.toString() == "") {
            alert("Please Select To Section");
            return;
        }

        var studentID = StudentIDs.GetText();

        if (studentID.toString() == "") {
            alert("Please Select Student");
            return;
        }

        $.ajax({
            url: "/StudentPromoteDemote/RecreateFeesStructure",
            type: "POST",
            //data: { mClassesID: classID, mClassesSetupID: classesSetupID, mToClassID: toclassID, mClassesSetupID: toclassesSetupID, mStudentID: mStudentID.toString() },
            data: { mClassesID: classID, mClassesSetupID: classesSetupID, mToClassID: toclassID, mToClassesSetupID: toclassesSetupID, mStudentID: studentID.toString() },
            beforeSend: (function (data) {
                loadingPanelMenu.Show();
            }),
            success: function (data) {


                loadingPanelMenu.Hide();

                var resMessage = data.DisplayMsg;
                var responseCode = data.ResponseCode;
                alert(resMessage)

                $('#divresmsg').empty();
                var html = "<h5>" + resMessage + "</h5>"
                $('#divresmsg').append(html);

                btntransferattendance.SetEnabled(false)
                btnpfeesstructure.SetEnabled(false)
                btnrecreatefeesstructure.SetEnabled(false)
                btnpromtedemote.SetEnabled(false)

            },
            error: function () {
            }
        }).done(function (data) {
            loadingPanelMenu.Hide();

        });

    },




}