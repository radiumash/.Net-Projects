var feesTransactionController = {

    splitterResized: function (s, e) {
        
        switch (e.pane.name) {
            //case 'FeesStructureFooter':
            //  //  GridFeeTerm.SetHeight(e.pane.GetClientHeight() - 40);
            //    break;
            //case 'ListFeesStructureFooter':
            // //   GridFeesStructure.SetHeight(e.pane.GetClientHeight() - 40);
            //    break;

        }
    },

    RowSelectionChange: function OnGridFocusedRowChanged(s, e) {
    
        s.GetRowValues(s.GetFocusedRowIndex(),'StudentID', feesTransactionController.RefreshTabsView);
       
    },

    RefreshTabsView: function(values) {
        var regID;
       // $("#txtStudentID").val(values);
        $("#StudentID").val(values);
        $("#txtStudentID").val(values);
        $("#txtstudentname").val(values);
     
        if (values != null) {
            regID = values;
          
        }
    $.ajax({
        url: "/FeesTransaction/GetAllTermForClasswise",
            type: "POST",
            data: {RegID:regID},
            success: function (data) {
                //alert(data);
                //$("#GridTermListForFeeID").html(data);
            },
            error: function () {
            }
        });
        //$("#BodySplitter_1_CC").html("<h1>"+values+"</h1>");
        //alert(values);
    },
  

    SelectedFeeTerm: function (s, e) {
        // var classID = ClassSetupID.GetSelectedValues();
      
        var TermID = s.GetValue();
        var StudentID = txtStudentID.GetValue();
        if (StudentID.toString() == "") {
            alert("Please Select Student for Fee");
        }
        if (TermID.toString() == "") {
            alert("Please Select FeeTerm");
            return;
        }
        $.ajax({
            url: "/FeesTransaction/GetStudentDataforFeeReceipt",
            type: "POST",
            data: { mTermID: TermID, mStudentID:StudentID },
            success: function (data) {
                alert(data);
                $("#FeesTransactionBodySplitter_1_CC").html(data);
            },
            error: function () {
            }
        });

    },


    ClickCheckFeePaidnew: function (value) {

        //alert(value)

        var TermID = value;
        var paidflag = false;
        var StudentID = txtStudentID.GetValue();
        if (StudentID.toString() == "") {
            alert("Please Select Student for Fee");
        }
        if (TermID.toString() == "") {
            alert("Please Select FeeTerm");
            return;
        }



        //$.ajax({
        //    url: "/FeesTransaction/CheckDuplicateReciept",
        //    type: "POST",
        //    data: { mTermID: TermID, mStudentID: StudentID },
        //    beforeSend: (function (data) {
        //        loadingPanelFeestructure.Show();
        //    }),
        //    success: function (data) {

        //        if (data == "Paid") {

        //            paidflag = true;
        //            alert("This Term Fee Already paid");
        //        }
        //        else {
        //            paidflag = false;
        //        }

        //    },
        //    error: function () {
        //    }
        //}).done(function (data) {
        //    loadingPanelFeestructure.Hide();

        //});

        //if (paidflag == false) {

        //    $.ajax({
        //        url: "/FeesTransaction/GetStudentDataforFeeReceipt",
        //        type: "POST",
        //        data: { mTermID: TermID, mStudentID: StudentID },
        //        success: function (data) {
        //            $("#GridHeadListForFeeID").html(data);

        //        },
        //        error: function () {
        //        }
        //    }).done(function (data) {
        //        loadingPanelFeestructure.Hide();

        //    });
        //}


    },




    ClickCheckFeePaid: function (s, e) {

       
        var TermID = s.name;
        var paidflag = false;
        var StudentID = txtStudentID.GetValue();
        if (StudentID.toString() == "") {
            alert("Please Select Student for Fee");
        }
        if (TermID.toString() == "") {
            alert("Please Select FeeTerm");
            return;
        }
        $.ajax({
            url: "/FeesTransaction/CheckDuplicateReciept",
            type: "POST",
            data: { mTermID: TermID, mStudentID: StudentID },
            success: function (data) {
              
                if (data == "Paid") {

                    paidflag = true;
                    alert("This Term Fee Already paid");
                }
                else {
                    paidflag = false;
                }
               
            },
            error: function () {
            }
        });

     
        if (paidflag == false) {
        
            $.ajax({
                url: "/FeesTransaction/GetStudentDataforFeeReceipt",
                type: "POST",
                data: { mTermID: TermID, mStudentID: StudentID },
                success: function (data) {
                        $("#FeesTransactionBodySplitter_1_CC").html(data);
                   
                },
                error: function () {
                }
            });
        }

    },
     
   
    Gettotal: function (s, e) {

        var total = parseInt(TermTotal.GetValue()) + parseInt(FineAmount.GetValue()) + parseInt(OtherAmount.GetValue());
        $("#SubTotal_I").val(total);
        $("#FinalAmount_I").val(total);

        var mode = DiscountType.GetValue();
        if (mode == "P") {
            if (isNaN(SubTotal.GetValue())) {
                $("#SubTotal_I").val(0);
            }
            if (isNaN(DiscountPercent.GetValue())) {
                $("#DiscountPercent_I").val(0);
            }

            var disAmount = (parseInt(SubTotal.GetValue()) * parseInt(DiscountPercent.GetValue())) / 100;
            $("#DiscountAmount_I").val(disAmount);
            var finalamt = (parseInt(SubTotal.GetValue()) - parseInt(DiscountAmount.GetValue()));
            $("#FinalAmount_I").val(finalamt);
        }
        else {

            if (isNaN(SubTotal.GetValue())) {
                $("#SubTotal_I").val(0);
            }
            if (isNaN(DiscountPercent.GetValue())) {
                $("#DiscountPercent_I").val(0);
            }

            var disAmount = parseInt(DiscountPercent.GetValue());
            $("#DiscountAmount_I").val(disAmount);
            var finalamt = (parseInt(SubTotal.GetValue()) - parseInt(DiscountAmount.GetValue()));
            $("#FinalAmount_I").val(finalamt);

        }

    },
    
    SelectionChangebankPaymode: function (s, e) {
        var mode = s.GetValue();
        if (mode == "Cash") {
          //  ChequeNo.GetInputElement().setAttribute('style', 'background:#CCCCCC;');
            ChequeNo.GetInputElement().readOnly = true;
           // ChequeDate.GetInputElement().readOnly = true;
            BankName.GetInputElement().readOnly = true;
            BranchName.GetInputElement().readOnly = true;
          
            $("ChequeNo_I").val("");
          //  $("ChequeDate_I").val(" ");
            $("BankName_I").val("");
            $("BranchName_I").val("");
         
        }
        else {
          //  ChequeNo.GetInputElement().setAttribute('style', 'background:#CCCCCC;');
            ChequeNo.GetInputElement().readOnly = false;
           // ChequeDate.GetInputElement().readOnly = false;
            BankName.GetInputElement().readOnly = false;
            BranchName.GetInputElement().readOnly = false;

            $("ChequeNo_I").val("");
          //  $("ChequeDate_I").val(" ");
            $("BankName_I").val("");
            $("BranchName_I").val("");
        }
    },

    SelectionChangePaymode: function (s, e) {
        var mode = s.GetValue();
        if (mode == "Bank") {
         
        }
    },


    SelectionChangeDiscountType: function (s, e) {
        var mode = s.GetValue();
        if (mode == "P") {
            if (isNaN(SubTotal.GetValue())) {
                $("#SubTotal_I").val(0);
            }
            if (isNaN(DiscountPercent.GetValue())) {
                $("#DiscountPercent_I").val(0);
            }

            var disAmount = (parseInt(SubTotal.GetValue()) * parseInt(DiscountPercent.GetValue())) / 100;
            $("#DiscountAmount_I").val(disAmount);
            var finalamt = (parseInt(SubTotal.GetValue()) - parseInt(DiscountAmount.GetValue()));
            $("#FinalAmount_I").val(finalamt);

        }
        else {

            if (isNaN(SubTotal.GetValue())) {
                $("#SubTotal_I").val(0);
            }
            if (isNaN(DiscountPercent.GetValue())) {
                $("#DiscountPercent_I").val(0);
            }

            var disAmount = parseInt(DiscountPercent.GetValue());
            $("#DiscountAmount_I").val(disAmount);

            var finalamt = (parseInt(SubTotal.GetValue()) - parseInt(DiscountAmount.GetValue()));
            $("#FinalAmount_I").val(finalamt);


        }
      
    },

    GetDiscount: function (s, e) {
        var mode = DiscountType.GetValue();

        if (mode == "P") {
            if (isNaN(SubTotal.GetValue())) {
                $("#SubTotal_I").val(0);
            }
            if (isNaN(DiscountPercent.GetValue())) {
                $("#DiscountPercent_I").val(0);
            }

            var disAmount = (parseInt(SubTotal.GetValue()) * parseInt(DiscountPercent.GetValue())) / 100;
            $("#DiscountAmount_I").val(disAmount);
            var finalamt = (parseInt(SubTotal.GetValue()) - parseInt(DiscountAmount.GetValue()));

            $("#FinalAmount_I").val(finalamt);
        }
        else {


            if (isNaN(SubTotal.GetValue())) {
                $("#SubTotal_I").val(0);
            }
            if (isNaN(DiscountPercent.GetValue())) {
                $("#DiscountPercent_I").val(0);
            }

            var disAmount = parseInt(DiscountPercent.GetValue());
            $("#DiscountAmount_I").val(disAmount);
            var finalamt = (parseInt(SubTotal.GetValue()) - parseInt(DiscountAmount.GetValue()));

            $("#FinalAmount_I").val(finalamt);


        }

    },


    SelectionChangedForStudent: function (s, e) {

        /*alert('dfdf');*/
        loadingPanelFeestructure.Show();
        s.GetRowValues(s.GetFocusedRowIndex(), 'StudentID', feesTransactionController.GetSelectedFieldValuesCallbackForStudentID);

        
        //s.GetSelectedFieldValues("StudentID", feesTransactionController.GetSelectedFieldValuesCallbackForStudentID);

    },


    GetSelectedFieldValuesCallbackForStudentID: function (values) {

        StudentID.SetText(values);

        txtStudentID.SetText(values);
        txtstudentname.SetText(values);

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


                EnrollmentNo.SetText(data.studentEnrollno);
                FatherName.SetText(data.studentFathername);
                MobileNo.SetText(data.studentMobileno);
                Class.SetText(data.studentClass);
                ClassID.SetText(data.studentclassid);
                txttermid.SetText(data.feestermid);
                TermId.SetText(data.feestermid);
                TermName.SetText(data.feestermname);


               

                feesTransactionController.GetFeeTermList(studentID);

                feesTransactionController.GetFeeHeadList(studentID, data.feestermid);

                feesTransactionController.GetFeeCalculateamountdata(studentID, data.feestermid);

            },
            error: function () {
            }
        }).done(function (data) {
            loadingPanelFeestructure.Hide();
        });
       
       
    },


    GetFeeTermList: function (mstudentID) {

        $.ajax({
            url: "/FeesTransaction/GetAllTermForClasswise",
            type: "POST",
            data: { studentID: mstudentID },
            beforeSend: (function (data) {

            }),
            success: function (data) {
               // console.log(data);
                //var tempList = JSON.parse(data);
                //console.log(tempList);
                $("#GridTermListForFeeID").html(data);
            },
            error: function () {
            }
        }).done(function (data) {

        });

    },

    GetFeeHeadList: function (mstudentID, mtermID) {

        $.ajax({
            url: "/FeesTransaction/GetAllHeadForTermAndStudentWise",
            type: "POST",
            data: { studentID: mstudentID, termID: mtermID },
            beforeSend: (function (data) {

            }),
            success: function (data) {
               
                $("#GridHeadListForFeeID").html(data);
            },
            error: function () {
            }
        }).done(function (data) {

        });

    },

    GetFeeCalculateamountdata: function (mstudentID, mtermID) {

        $.ajax({
            url: "/FeesTransaction/GetStudentFeesFooterData",
            type: "POST",
            data: { studentID: mstudentID, termID: mtermID },
            beforeSend: (function (data) {

            }),
            success: function (data) {


                TermTotal.SetText(data.feeTermTotal);
                FineAmount.SetText(data.feeFineAmount);
                OtherAmount.SetText(data.feeOtherAmount);
                SubTotal.SetText(data.feeSubTotal);
                FinalAmount.SetText(data.feeFinalAmount);
                DiscountAmount.SetText(data.feeDiscountAmount);
                DiscountPercent.SetText(data.feeDiscountPercent);
            },
            error: function () {
            }
        }).done(function (data) {

        });

    },


    Onclickreceiptsearchshowpopup: function (s, e) {
     
        var searchmode = s.name;
        txtreceiptsearchmode.SetText(searchmode);


        if (searchmode == "btnDeleteFeeReceipt") {

            btnsearchbyPopup.SetText('Delete');
        }
        else {

            btnsearchbyPopup.SetText('Search');
        }

        popupControlsearchreceipt.Show();
    },

    CloseFeesStructureChangePopupLoading: function (s, e) {

        
        studentID = StudentID.GetText();
        termID = TermId.GetText();
        



        loadingPanelFeestructure.Show();
        


        feesTransactionController.GetFeeHeadList(studentID, termID);

        feesTransactionController.GetFeeCalculateamountdata(studentID, termID);

        setTimeout(feesTransactionController.Closepopupfeeschange, 2000);
        
        //feesTransactionController.Closepopupfeeschange();
    },

    Closepopupfeeschange: function (s, e) {
        loadingPanelFeestructure.Hide();
        PopupControlChangeFeesStructure.Hide();
        
    },



    Onclickreceiptsearch: function () {


        var searchmode = txtreceiptsearchmode.GetText();

        console.log(searchmode)
      
        if (searchmode == "btnViewFeeReceipt") {

            var txtsearch = txtreceiptnosearch.GetValue();
            if (txtsearch == "" || txtsearch == null) {
                alert("please enter bilty no");
                return 0;
            }
            $.ajax({
                url: "/FeesTransaction/GetStudentFeesData",
                type: "POST",
                data: { receiptno: txtsearch },
                datatype: "json",
                beforeSend: (function (data) {
                    loadingPanelFeestructure.Show();
                }),
                success: function (data) {

                    if (data.Resultmsg == "404") {
                        alert("receipt not found");
                    }

                    $("#contentPersonal").html(data.ResultData);
                    popupControlsearchreceipt.Hide();

                    var mstudentID = StudentID.GetText();

                    gridLookup.SetValue(mstudentID);

                    var mTermId = TermId.GetText();

                    feesTransactionController.GetFeeTermList(mstudentID);

                    feesTransactionController.GetFeeHeadList(mstudentID, mTermId);

                    // GridBiltyItem(txtsearch);
                    //InsertMod.SetText("Update")
                    // btnSave.SetText("Update");
                },
                error: function (data) {
                    alert("Error")
                }

            }).done(function (data) {
                loadingPanelFeestructure.Hide();
            });
        }

        if (searchmode == "btnDeleteFeeReceipt") {

            var confrm = confirm("Do you want delete this receipt ?");

            if (confrm == false)
                return;

            var txtsearch = txtreceiptnosearch.GetValue();
            if (txtsearch == "" || txtsearch == null) {
                alert("please enter receipt no");
                return 0;
            }
            $.ajax({
                url: "/FeesTransaction/DeleteFeesReceipt",
                type: "POST",
                data: { receiptno: txtsearch },
                datatype: "json",
                beforeSend: (function (data) {
                    loadingPanelFeestructure.Show();
                }),
                success: function (data) {

                    alert(data.errorMsg)
                    //$("#contentPersonal").html(data.ResultData);
                    popupControlsearchreceipt.Hide();
                    // GridBiltyItem(txtsearch);
                    //InsertMod.SetText("Update")
                    // btnSave.SetText("Update");
                },
                error: function (data) {
                    alert("Error")
                }

            }).done(function (data) {
                loadingPanelFeestructure.Hide();
            });
        }

       


       
    },

    OnclickChangeFeesStructure: function () {


        var mstudentID = StudentID.GetText();
        var mclassID = ClassID.GetText();
        var mtermID = TermId.GetText();

        var url = "/StudentListFeesStructure/Index?ispoup=1&mstudid=" + mstudentID + "&mclassid=" + mclassID + "&mtermid=" + mtermID +""
        //feesTransactionController.Setcoocies("popupstudentid", mstudentID, 1);
        //feesTransactionController.Setcoocies("popuptermid", mtermID, 1);
        //feesTransactionController.Setcoocies("popupclassid", mclassID, 1);
       

        PopupControlChangeFeesStructure.SetContentUrl(url);

        var purl = PopupControlChangeFeesStructure.GetContentUrl();
        

        PopupControlChangeFeesStructure.Show();


        
    },

    Setcoocies: function (cname, cvalue, exdays) {

        const d = new Date();
        d.setTime(d.getTime() + (exdays * 24 * 60 * 60 * 1000));
        let expires = "expires=" + d.toUTCString();
        document.cookie = cname + "=" + cvalue + ";" + expires + ";path=/";

    }





}