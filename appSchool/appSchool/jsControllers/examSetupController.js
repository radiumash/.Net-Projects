var examSetupController = {

    splitterResized: function (s, e) {
        switch (e.pane.name) {
            
            case 'ExamSetupBody':
           
                break;

            case 'ExamSetupFooter':
                GridExamSetup.SetHeight(e.pane.GetClientHeight() - 10);
                break;

        }
    },

    SendbuttonClick: function (s, e) {
      
        var mClassID = ClassID.GetValue();
        if (mClassID == null) {
            alert("Please Select Class.");
            return;
        }

        var mExamID = ExamID.GetValue();
        if (mExamID == null) {
            alert("Please Select Exam.");
            return;
        }

        var mOrderID = txtExamOrder.GetValue();
        if (mOrderID == null || mOrderID == "") {
            mOrderID = 0;
            //alert("Please Select Order.");
            //return;
        }

        var mFromDate = txtfromdate.GetValue();
        //if (mFromDate == null) {
        //    alert("Please Select FromDate.");
        //    return;
        //}

        var mToDate = txttodate.GetValue();
        //if (mToDate == null) {
        //    alert("Please Select ToDate.");
        //    return;
        //}

      
        $.ajax({
            url: "/ExamSetup/CreateOrUpdateExamSetUpint",
            type: "POST",
            //data: { pClassID: mClassID.toString(), pExamID: mExamID.toString(), pStartDate: mFromDate.toDateString(), pEndDate: mToDate.toDateString(), pOrderID: mOrderID.toString() },
            data: { pClassID: mClassID, pExamID: mExamID, pStartDate: mFromDate, pEndDate: mToDate, pOrderID: mOrderID },
            beforeSend: (function (data) {
                LoadingPanelExamSetup.Show();
            }),
            success: function (data) {
                $("#divexamsetup").html(data);
               
            },
            error: function () {

            }
        }).done(function (data) {
            LoadingPanelExamSetup.Hide();

        });
    },

    FillMaxMinMarks: function (s, e) {

        var mClassID = ClassID.GetValue();
        if (mClassID == null) {
            alert("Please Select Class.");
            return;
        }

        var mExamID = ExamID.GetValue();
        if (mExamID == null) {
            alert("Please Select Exam.");
            return;
        }

        //var mOrderID = ExamOrder.GetValue();
        //if (mOrderID == null) {
        //    alert("Please Select Order.");
        //    return;
        //}

        //var mFromDate = StartDate.GetValue();
        //if (mFromDate == null) {
        //    alert("Please Select FromDate.");
        //    return;
        //}

        //var mToDate = EndDate.GetValue();
        //if (mToDate == null) {
        //    alert("Please Select ToDate.");
        //    return;
        //}

        var maxmarks = txtMaxmarks.GetText();
        if (maxmarks == null || maxmarks == "") {
            alert("Please Enter Max Marks.");
            return;
        }



        var minmarks = txtMinmarks.GetText();
        if (minmarks == null || minmarks == "")  {
            alert("Please Select Min Marks");
            return;
        }



        $.ajax({
            url: "/ExamSetup/UpdateMinMaxExamSetupDetail",
            type: "POST",
            //data: { pClassID: mClassID.toString(), pExamID: mExamID.toString(), pStartDate: mFromDate.toDateString(), pEndDate: mToDate.toDateString(), pOrderID: mOrderID.toString() },
            data: { pClassID: mClassID, pExamID: mExamID, pMaxmarks: maxmarks, pMinmarks: minmarks  },
            beforeSend: (function (data) {
                LoadingPanelExamSetup.Show();
            }),
            success: function (data) {
                $("#divexamsetup").html(data);

            },
            error: function () {

            }
        }).done(function (data) {
            LoadingPanelExamSetup.Hide();

        });
    },


    UpdateMaxMinMarks: function(s,e)
    {
        var mClassID = ClassID.GetValue();
        if (mClassID == null) {
            alert("Please Select Class.");
            return;
        }

        var mExamID = ExamID.GetValue();
        if (mExamID == null) {
            alert("Please Select Exam.");
            return;
        }


        //var mMaxmarks = txtMaxmarks.GetText();

        //if (mMaxmarks == "") {
        //    alert("Please Enter Max Marks");
        //    return;
        //}

        var mMaxmarks = txtMaxmarks.GetText();

        if (mMaxmarks == "") {
            alert("Please Enter Max Marks");
            return;
        }

        var mMinmarks = EndDate.GetValue();
        if (mMinmarks == "") {
            alert("Please Enter Min Marks");
            return;
        }


        $.ajax({ 
            url:"/ExamSetup/UpdateMinMaxExamSetupDetail",
            type:"POST",
            data: { pClassID: mClassID.toString(), pExamID: mExamID.toString(), pMaxmarks: mMaxmarks, pMinmarks: mMinmarks  },
            beforeSend: (function (data) {
              
                LoadingPanelExamSetup.Show();
            }),
            success: function(data)
            {
              
                $("#ExamSetupSplitter_1_CC").html(data);
                var winHeight = document.getElementById('ExamSetupSplitter_1_CC').offsetHeight;
                GridExamSetup.SetHeight(winHeight - 10);
            },
            error: function()
            {

            }
        } ).done( function(data)
        {
            LoadingPanelExamSetup.Hide();
        }) ;
    },




     SelectedExam: function (s, e) {
        var mExamID = s.GetValue();
        var mClassID = ClassID.GetValue();
      

        $.ajax({
            url: "/ExamSetup/CheckDuplicateMasterAction",
            type: "POST",
            data: { pExamID: mExamID.toString(), pClassID: mClassID.toString() },
            beforeSend: (function (data) {

                LoadingPanelExamSetup.Show();
            }),
            success: function(data)
            {
                if (data.recordFound == 1) {

                    txtfromdate.SetText(data.examfromDate);
                    txttodate.SetText(data.examtoDate);
                    txtExamOrder.SetText(data.examOrder);

                    $("#divexamsetup").html(data.ListDataExamSetup);
                    
                }
                //$("#DateOrderPartial").html(data);
            },
            error: function () {

            }
            }).done(function (data) {
            LoadingPanelExamSetup.Hide();
        });

    },

    SelectedClass: function (s, e) {
        var mClassID = s.GetValue();
        alert(mClassID);

        $.ajax({
            url: "/FacultyAllotment/GetClassSetupList",
            type: "POST",
            data: { pClassID: mClassID.toString() },
            success: function(data)
            {
                $("#ClassSetupID").html(data);
            },
            error: function () {
            }
        });

    }










}