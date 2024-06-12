var subjectAllotmentController = {

    splitterResized: function (s, e) {
        switch (e.pane.name) {

            case 'SubjectAllotmentMiddel':
                //GridSubjectlevelOne.SetHeight(e.pane.GetClientHeight() - 10);
                break;
            case 'SubjectAllotmentBottom':
                //GridSubjectsAllotment.SetHeight(e.pane.GetClientHeight() - 10);
                break;

        }
    },

    SelectionChanged: function (s, e) {
        s.GetSelectedFieldValues("IdL1", subjectAllotmentController.GetSelectedFieldValuesCallbackForLevelOne);
  },

    SelectionChangedForSubjectLevelTwo: function (s, e) {
        s.GetSelectedFieldValues("IdL2", subjectAllotmentController.GetSelectedFieldValuesCallbackForLevelTwo);
    },

    SelectionChangedForSubjectLevelThree: function (s, e) {
        s.GetSelectedFieldValues("IdL3", subjectAllotmentController.GetSelectedFieldValuesCallbackForLevelThree);
    },


    GetSelectedFieldValuesCallbackForLevelOne: function (values) {
        //alert(values);
        SubjectLevelD.SetText(values);
    },

    GetSelectedFieldValuesCallbackForLevelTwo: function (values) {
        //alert(values);
       SubjectLevelD.SetText(values);
    },

    GetSelectedFieldValuesCallbackForLevelThree: function (values) {
        //alert("call");
        SubjectLevelD.SetText(values);
    },
  

   SelectedClass: function (s, e) {

        var mClassID = s.GetValue();
        if (mClassID == null) {
            alert("Please Select Class");
            return;
        }

        $.ajax({
            url: "/SubjectAllotment/GetSubjectlevelbyClassID",
            type: "Post",
            datatype: "json",
            data: { MClassID: mClassID.toString() },
            success: function (data) {
               
                txtSubjectlevel.SetText(data.ResultSubLevel);
                txtSubjectlevelName.SetText(data.ResultSubLevelName);
            },
            error: function () {

            }

        });

    },

    SelectionChangedForClasses: function (s, e) {

        s.GetSelectedFieldValues("ClassID", subjectAllotmentController.GetSelectedFieldValuesCallbackForClasses);

    },
    GetSelectedFieldValuesCallbackForClasses: function (values) {
        txtclassids.SetText(values);
       
    },

    SelectionChangedForSubjectOne: function (s, e) {

        if (chkIsincludesubjecttwo.GetChecked()) {

            txtsubjectoneids.SetText('');
            s.GetSelectedFieldValues("IdL1", subjectAllotmentController.GetSelectedFieldValuesCallbackForSubjectOne);
        }
        else {
            s.GetSelectedFieldValues("IdL1", subjectAllotmentController.GetSelectedFieldValuesCallbackForSubjectOne);
        }
        

    },

    GetSelectedFieldValuesCallbackForSubjectOne: function (values)
    {
         txtsubjectoneids.SetText(values);
    },

    SelectionChangedForSubjectTwo: function (s, e) {

        s.GetSelectedFieldValues("IdL2", subjectAllotmentController.GetSelectedFieldValuesCallbackForSubjectTwo);

    },
    GetSelectedFieldValuesCallbackForSubjectTwo: function (values) {

        txtsubjecttwoids.SetText(values);
        // $("txtclassSetup").val(values);


    },
   
   ShowSubjectForClass: function (s, e) {

       var mClassID = ClassID.GetValue();
       if (mClassID == null) {
           alert("Please Select Class");
           return;
       }
       var mSubjectLevel = txtSubjectlevel.GetText();
      

       $.ajax({

           url: "/SubjectAllotment/ShowSubjectForClass",
           type: "Post",
           datatype: "json",
           data: { PClassID: mClassID, PSubjectlevel: mSubjectLevel },

           beforeSend: (function (data) { loadingPanelSubjectAllotment.Show(); }),

           success: function (data) {
               if (data.DisplaySubjectLevel == 1) {

                   $("#SubjectAllotmentSplitter_1_CC").html(data.ListData);
                   var winHeight = document.getElementById('SubjectAllotmentSplitter_1_CC').offsetHeight;
                   GridSubjectlevelOne.SetHeight(winHeight - 1);

                   $("#SubjectAllotmentSplitter_3_CC").html(data.ListDataSuballot);
                   var winHeight = document.getElementById('SubjectAllotmentSplitter_3_CC').offsetHeight;
                   GridSubjectAllotment.SetHeight(winHeight - 1);


               }
             else {
                   if (data.DisplaySubjectLevel == 2) {
                       $("#SubjectAllotmentSplitter_1_CC").html(data.ListData);
                       var winHeight = document.getElementById('SubjectAllotmentSplitter_1_CC').offsetHeight;
                       GridSubjectlevelTwo.SetHeight(winHeight - 1);

                       $("#SubjectAllotmentSplitter_3_CC").html(data.ListDataSuballot);
                       var winHeight = document.getElementById('SubjectAllotmentSplitter_3_CC').offsetHeight;
                       GridSubjectAllotment.SetHeight(winHeight - 1);
                   }

                   else
                   {
                       $("#SubjectAllotmentSplitter_1_CC").html(data.ListData);
                       var winHeight = document.getElementById('SubjectAllotmentSplitter_1_CC').offsetHeight;
                       GridSubjectlevelThree.SetHeight(winHeight - 1);

                       $("#SubjectAllotmentSplitter_3_CC").html(data.ListDataSuballot);
                       var winHeight = document.getElementById('SubjectAllotmentSplitter_3_CC').offsetHeight;
                       GridSubjectAllotment.SetHeight(winHeight - 1);
                   }
                }
               SubjectLevelD.SetText("");
           },
           error: function () {

           }

       }).done(function (data) { loadingPanelSubjectAllotment.Hide(); });


   },


    AddSubjectToSubjectAllotment: function (s, e) {

        var mClassIDs = txtclassids.GetText();

        if (mClassIDs == null || mClassIDs == "") {
            alert("Please Select Class");
            return;
        }

        var mSubjectOneIDs = txtsubjectoneids.GetText();
        if (mSubjectOneIDs == null || mSubjectOneIDs == "") {
            alert("Please Select Subject One");
            return;
        }
        var issubjectlevetwoapply = 0;

        var mSubjectTwoIDs = "";

        if (chkIsincludesubjecttwo.GetChecked()) {

            issubjectlevetwoapply = 1;
            mSubjectTwoIDs = txtsubjecttwoids.GetText();
            if (mSubjectTwoIDs == null || mSubjectTwoIDs == "") {
                alert("Please Select Subject Two");
                return;
            }
        }
        else {
            issubjectlevetwoapply = 0;
            mSubjectTwoIDs = "-1";
        }
        

        var mSubjectLevel = txtSubjectlevel.GetText();
        

        $.ajax({

            url: "/SubjectAllotment/AddSubjectsToSubjectAllotment",
            type: "Post",
            datatype: "json",
            data: { ClassIDs: mClassIDs, SubjectOneIDs: mSubjectOneIDs, SubjectTwoIDs: mSubjectTwoIDs, Issubjectlevetwo: issubjectlevetwoapply },

            beforeSend: (function (data) { loadingPanelSubjectAllotment.Show(); }),

            success: function (data) {

                //console.log(data.ListDataSuballot)

                $("#divsubjectallotment").html(data.ListDataSuballot);
                    
            },
            error: function () {

            }

        }).done(function (data) { loadingPanelSubjectAllotment.Hide(); });


    },
  
   AllotSubjectForClass: function (s, e) {

       var mClassID = ClassID.GetValue();
       if (mClassID == null) {
           alert("Please Select Class");
           return;
       }

       var mSubjectLevel = txtSubjectlevel.GetText();
       if (mSubjectLevel== null) {
           alert("Please Select Subject Level");
           return;
       }

       var mSubjectLevelDs = SubjectLevelD.GetValue();
       if (mSubjectLevelDs == null) {
           alert("Please Select Subject");
           return;
       }
      
       $.ajax({

           url: "/SubjectAllotment/AllotmentSubjectForClass",
           type: "Post",
           datatype: "json",
           data: { ClassID: mClassID, pSubjectlevel: mSubjectLevel,  PSubjectlevelID: mSubjectLevelDs.toString() },

           beforeSend: (function (data) { loadingPanelSubjectAllotment.Show(); }),

           success: function (data) {
               alert(data.Displaymsg);

               $("#SubjectAllotmentSplitter_3_CC").html(data.ListData);
               var winHeight = document.getElementById('SubjectAllotmentSplitter_3_CC').offsetHeight;
               GridSubjectAllotment.SetHeight(winHeight - 1);

               SubjectLevelD.SetText("");

           },
           error: function () {

           }

       }).done(function (data) { loadingPanelSubjectAllotment.Hide(); });


   },


    DeleteSubjectAllotment: function (values) {


        var subjectallotids = values;

        var subjectallotidssplit = subjectallotids.split('|');

        var Classid = subjectallotidssplit[0];
        var subjectid1 = subjectallotidssplit[1];
        var subjectid2 = subjectallotidssplit[2];
        var subjectid3 = subjectallotidssplit[3];

        $.ajax({

            url: "/SubjectAllotment/DeleteSubjectAlloted",
            type: "Post",
            datatype: "json",
            data: { ClassIDs: Classid, SubjectOneIDs: subjectid1, SubjectTwoIDs: subjectid2, SubjectThreeIDs: subjectid3 },

            beforeSend: (function (data) { loadingPanelSubjectAllotment.Show(); }),

            success: function (data) {
                alert(data.DataResponseMsg);

                $("#divsubjectallotment").html(data.ListDataSuballot);
            },
            error: function () {

            }

        }).done(function (data) { loadingPanelSubjectAllotment.Hide(); });


    },

}