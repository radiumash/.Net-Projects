var studentListfeesStructureController = {

    

     
    RowSelectionChangeforGridListFeesStructure: function OnGridFocusedRowChanged(s, e) {
        s.GetRowValues(s.GetFocusedRowIndex(), 'StudentID', studentListfeesStructureController.RefreshTabsViewForGridStudentList);
    },
    RefreshTabsViewForGridStudentList: function (values) {
        var mStudentID;

        if (values != null) {
            mStudentID = values;
        }
        $.ajax({
            url: "/StudentListFeesStructure/GetFeesTermForListEdit",
                type: "POST",
                data: { newStudentID: mStudentID },
                success: function (data) {
                    $("#SplitterStudentListFeesStructure_1_CC").html(data);
                },
                error: function () {
                }
            });
        //$("#BodySplitter_1_CC").html("<h1>"+values+"</h1>");
        //alert(values);
    },

    
    
    RowSelectionChangeforHeadList: function OnGridFocusedRowChanged(s, e) {
        s.GetRowValues(s.GetFocusedRowIndex(), 'StudmasterID', studentListfeesStructureController.RefreshTabsViewForHeadList);
    },



    OnTermSelection: function OnGridFocusedRowChanged(s, e) {

        s.GetRowValues(s.GetFocusedRowIndex(), 'StudmasterID', studentListfeesStructureController.RefreshTabsViewForHeadList);
    },

    SelectTerm: function (s, e) {
        var mtermID = s.GetValue();
        if (mtermID.toString() == "") {
            alert("Please Select Term");
            return;
        }

        var mstudentID = txtStudentID.GetValue();;

        if (mstudentID.toString() == "") {
            alert("Please Select Student");
            return;
        }

        var mcalssID = txtClassID.GetValue();;

        studentListfeesStructureController.SetStudentFeesHeadList(mstudentID, mtermID, mcalssID)

    },


    SetStudentFeesHeadList: function (mstudentID, mtermID, mcalssID) {

        $.ajax({
            url: "/StudentListFeesStructure/GetFeesHeadForListEdit",
            type: "POST",
            data: { studentID: mstudentID, termID: mtermID, calssID: mcalssID },
            beforeSend: (function (data) {
                loadingPanelFeestructure.Show();
            }),
            success: function (data) {
                $("#divstudentfeesdetail").html(data);
            },
            error: function () {
            }
        }).done(function (data) {
            loadingPanelFeestructure.Hide();

        });
    },


    RefreshTabsViewForHeadList: function (termidvalue) {

       var mtermID;
  
        if (termidvalue != null) {
        mtermID = termidvalue;
          
        }

        var mstudentID = txtStudentID.GetValue();;


    $.ajax({
        url: "/StudentListFeesStructure/GetFeesHeadForListEdit",
        type: "POST",
        data: { studentID: mstudentID, termID: mtermID },
        success: function (data) {

            $("#divstudentfeesdetail").html(data);
        },
        error: function () {
        }
    });
  
    },

    SelectionChangedForStudent: function (s, e) {

        /*alert('dfdf');*/
        loadingPanelFeestructure.Show();
        s.GetRowValues(s.GetFocusedRowIndex(), 'StudentID', studentListfeesStructureController.GetSelectedFieldValuesCallbackForStudentID);


       

    },

    GetSelectedFieldValuesCallbackForStudentID: function (values) {

        

        txtStudentID.SetText(values);
        

        var studentID;

        if (values != null) {
            studentID = values;
        }


        $.ajax({
            url: "/FeesTransaction/GetStudentData",
            type: "POST",
            data: { mStudentID: studentID },
            beforeSend: (function (data) {

            }),
            success: function (data) {

                txtFatherName.SetText(data.studentFathername);
                txtMobileNo.SetText(data.studentMobileno);
                txtClass.SetText(data.studentClass);
                txtClassID.SetText(data.studentclassid);
                

            },
            error: function () {
            }
        }).done(function (data) {
            loadingPanelFeestructure.Hide();
        });


    },



}