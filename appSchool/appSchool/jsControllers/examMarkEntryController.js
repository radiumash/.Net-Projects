var examMarkEntryController = {

    splitterResized: function (s, e) {
        switch (e.pane.name) {
            case 'ExamMarkEntryBody':
           
                break;
            case 'ExamMarkEntryFooter':
                  GridStudent.SetHeight(e.pane.GetClientHeight()-10);
                break;

        }
    },

    SelectedClass: function (s, e) {
        var classID = s.GetValue();
        if (classID.toString() == "") {
            alert("Please Select Classes");
            return;
        }
        $.ajax({
            url: "/ExamMarkEntry/GetExamListView",
            type: "POST",
            datatype: 'json',
            beforeSend: (function (data) {
                loadingPanelExamMarkEntry.Show();
            }),
            data: { mClassID: classID.toString() },
            success: function (data) {
                if (data.Status == true)
                {
                    alert(data.DisplayMsg);
                }

                var examList = JSON.parse(data.ExamNameList);
                ExamID.ClearItems();
                for (var i = 0; i < examList.length; i++)
                {
                    ExamID.AddItem(examList[i].ExamName, examList[i].ExamID);
                }


                var ClassSetList = JSON.parse(data.ClassSetupList);
                ClassSetupID.ClearItems();
                for (var i = 0; i < ClassSetList.length; i++) {
                    ClassSetupID.AddItem(ClassSetList[i].ClassDescription, ClassSetList[i].ClassSetupID);
                }
              
                var SubjectL1List = JSON.parse(data.SubjectList);
                SubjectID.ClearItems();
                for (var i = 0; i < SubjectL1List.length; i++)
                {
                    SubjectID.AddItem(SubjectL1List[i].SubjectNameL1, SubjectL1List[i].IdL1);
                }


            },
            error: function () {
            }
        }).done(function (data) {
            loadingPanelExamMarkEntry.Hide();

        });

      

    },


    SelectedSubjectLevelOne: function (s, e) {

        var pClassID = ClassID.GetValue();
        if (pClassID == null) {
            alert("Please Select Class");
            return;
        }

        var pSubjectID = s.GetValue();
        if (pSubjectID == null) {
            alert("Please Select Classes");
            return;
        }

        

        var pExamID = ExamID.GetValue();
        if (pExamID == null) {
            alert("Please Select Exam");
            return;
        }

       

        $.ajax({
            url: "/ExamMarkEntry/GetSubjectLevelTwoListView",
            type: "POST",
            datatype: 'json',
            beforeSend: (function (data) {
                loadingPanelExamMarkEntry.Show();
            }),
            data: { mClassID: pClassID, mEamID: pExamID, mSubjectID1: pSubjectID},
            success: function (data) {
                if (data.Status == true) {
                    alert(data.DisplayMsg);
                }

                var SubjectL2List = JSON.parse(data.SubjectList);
                SubjectID2.ClearItems();
                for (var i = 0; i < SubjectL2List.length; i++) {
                    SubjectID2.AddItem(SubjectL2List[i].SubjectNameL2, SubjectL2List[i].IdL2);
                }


            },
            error: function () {
            }
        }).done(function (data) {
            loadingPanelExamMarkEntry.Hide();

        });



    },

    btnClickGetStudentList: function (s, e) {
               

        var pClassID = ClassID.GetValue();
        if (pClassID == null) {
            pClassID = 0;
        }
        var pClassSetupID = ClassSetupID.GetValue();
        if (pClassSetupID == null) {
            pClassSetupID = 0;
        }
        var pExamID = ExamID.GetValue();
        if (pExamID == null) {
            pExamID = 0;
        }

        var pSubjectID1=SubjectID.GetValue();
        if (pSubjectID1==null)
        {
            pSubjectID1=-1;
        }

        var pSubjectID2 = SubjectID2.GetValue();
        if (pSubjectID2 == null) {
            pSubjectID2 =-1;
        }

        var pSubjectID3 = SubjectID3.GetValue();
        if (pSubjectID3 == null) {
            pSubjectID3 = -1;
        }

        

        $.ajax({
            url: "/ExamMarkEntry/GetStudentListForMarkEntry",
            type: "POST",
            data: { mCLassID: pClassID, mClassSetupID: pClassSetupID, mExamID: pExamID, mSubjectID1: pSubjectID1, mSubjectID2: pSubjectID2, mSubjectID3: pSubjectID3},
            datatype:'json',
            beforeSend: (function (data) {
                loadingPanelExamMarkEntry.Show();
            }),
            success: function (data) {
                $("#divexammarksentry").html(data.DataList);
               
            },
            error: function () {
            }
        }).done(function (data) {
            loadingPanelExamMarkEntry.Hide();

        });


    },
    
    FillClassSetupData: function (value) {
   
        $.ajax({
            url: "/StudentSession/GetClassSetupList",
            type: "POST",
            data: { mClassID: value },
            success: function (data) {
                $("#ClassSetupIDtd").html(data);
                if (rbtnStudSession.GetValue() == 2) {
                    ClassSetupID.SetVisible(true);
                    lblClassSetupID.SetVisible(true);
                }

                //var winHeight = document.getElementById('StudentSessionSplitter_1_CC').offsetHeight;
                //GridStudentSession.SetHeight(winHeight - 10);




            },
            error: function () {
            }
        });




    },





    SelectionChanged: function (s, e) {
        s.GetSelectedFieldValues("StudentSessionID", examMarkEntryController.GetSelectedFieldValuesCallback);
    },
    GetSelectedFieldValuesCallback: function (values) {
        StudentIDs.SetText(values);
    },

    SelectAllStudent: function (s, e) {

        if (s.GetChecked()) {
            GridStudentSession.SelectRows();
        }
        else {
            GridStudentSession.UnselectRows();
        }

    },

    SelectedIndexChangedForRbtnUpdate: function (s, e) {
        var v = s.GetValue();

        if (s.GetValue()==1) {

            HouseID.SetVisible(true);
            lblHouseID.SetVisible(true);

            ClassSetupID.SetVisible(false);
            lblClassSetupID.SetVisible(false);

            chkSMSInHindi.SetVisible(false);
        }
        else if(s.GetValue()==2)
        {
            ClassSetupID.SetVisible(true);
            lblClassSetupID.SetVisible(true);

            HouseID.SetVisible(false);
            lblHouseID.SetVisible(false);

            chkSMSInHindi.SetVisible(false);
        }
        else if (s.GetValue() == 3) {

            chkSMSInHindi.SetVisible(true);

            ClassSetupID.SetVisible(false);
            lblClassSetupID.SetVisible(false);

            HouseID.SetVisible(false);
            lblHouseID.SetVisible(false);
        }
    },

    SelectionChangedForStudent: function (s, e) {

        /*alert('dfdf');*/
        loadingPanelstudent.Show();
        s.GetRowValues(s.GetFocusedRowIndex(), 'StudentID', studentListfeesStructureController.GetSelectedFieldValuesCallbackForStudentID);

    },

    GetSelectedFieldValuesCallbackForStudentID: function (values) {



        txtStudentID.SetText(values);
        loadingPanelstudent.Hide();

       

    },

  


}