var rolePermissionController = {

    splitterResized: function (s, e) {
        switch (e.pane.name) {

            case 'UserPermissionBody':
                //  GridStudentSession.SetHeight(e.pane.GetClientHeight());
                break;
            case 'UserPermissionFooter':
                GridUserPermission.SetHeight(e.pane.GetClientHeight() - 20);
                break;

            //case 'ClassesFooter':
            //    tabClassRecord.SetWidth(e.pane.GetClientWidth());
            //    break;
        }
    },

    SelectedUser: function (s, e) {
        //alert('hi')
        // var classID = ClassSetupID.GetSelectedValues();
        var RoleId = s.GetValue();
        $("#RoleidS_I").val(RoleId);
        //alert(RoleId);
        if (RoleId.toString() == "") {
            alert("Please Select User");
            return;
        }

    },

    SelectedAppModule: function (s, e) {
        // var classID = ClassSetupID.GetSelectedValues();
        var ModuleID = s.GetValue();
        $("#ModuleIdS_I").val(ModuleID);
        //alert(ModuleID);
        if (ModuleID.toString() == "") {
            alert("Please Select Module");
            return;
        }

        $.ajax({
            url: "/RolePermission/GetFeatureListByFModuleIDView",
            type: "POST",
            data: { mModuleID: ModuleID },
            success: function (data) {
                $("#FromFeatureIdTD").html(data);

            },
            error: function () {
            }
        });

        //rolePermissionController.FillToFeatureData(ModuleID);

    },

    FillToFeatureData: function (value) {

        $.ajax({
            url: "/RolePermission/GetToClassList",
            type: "POST",
            data: { mFClassID: value },
            success: function (data) {
                $("#ToClassIDtd").html(data);

            },
            error: function () {
            }
        });




    },

    SelectedAppFeature: function (s, e) {
        // var classID = ClassSetupID.GetSelectedValues();
        var FeatureID = s.GetValue();
        $("#FeatureIdS_I").val(FeatureID);
        //alert(ModuleID);
        if (FeatureID.toString() == "") {
            alert("Please Select Module");
            return;
        }

        var ModuleID = ModuleIdS.GetValue();
        //alert(ModuleID);
        if (ModuleID.toString() == "") {
            alert("Please Select Module");
            return;
        }
        var RoleID = RoleidS.GetValue();
        //alert(RoleID);
        if (RoleID.toString() == "") {
            alert("Please Select User");
            return;
        }

        $.ajax({
            url: "/RolePermission/GetAllRolePermission",
            type: "POST",
            data: { PFeatureID: FeatureID.toString(), PModuleID: ModuleID.toString(), PRoleID: RoleID.toString() },
            beforeSend: function () {
                LoadingPanelUserPermission.Show();
            },
            complete: function () {
                LoadingPanelUserPermission.Hide();
            },

            success: function (data) {
                $("#divuserrole").html(data);
                //var winHeight = document.getElementById('divuserrole').offsetHeight;

                //GridUserPermission.SetHeight(winHeight - 10);
            },
            error: function () {
            }
        }).done(function (data) {
            LoadingPanelUserPermission.Hide();

        });
    },


    SelectAllStudent: function (s, e) {

        if (s.GetChecked()) {
            alert(s.GetChecked());
            GridUserPermission.row[12].cells[5].checked = true;










        }
        else {

        }

    },



    CreateUserPermission: function (s, e) {

        // var classID = ClassSetupID.GetSelectedValues();
        var UserID = txtUserName.GetValue();
        //var ModuleID = txtModuleName.GetValue();
        if (UserID.toString() == "") {
            alert("Please Select Role");
            return;
        }
        $.ajax({
            //url: "/UserPermission/CreateUserPermission",
            url: "/UserPermission/GetAllRolePermission",
            type: "POST",
            data: { mUserID: UserID.toString() },
            beforeSend: function () {
                LoadingPanelUserPermission.Show();
            },
            complete: function () {
                LoadingPanelUserPermission.Hide();
            },

            success: function (data) {
                $("#divuserrole").html(data);
                //var winHeight = document.getElementById('divuserrole').offsetHeight;

                //GridUserPermission.SetHeight(winHeight - 10);
            },
            error: function () {
            }
        }).done(function (data) {
            LoadingPanelUserPermission.Hide();

        });

    },


    SelectionChanged: function (s, e) {
        s.GetSelectedFieldValues("Id", rolePermissionController.GetSelectedFieldValuesCallback);
        s.GetSelectedFieldValues("ModuleId", rolePermissionController.GetSelectedFieldValuesModuleIdCallback);
        s.GetSelectedFieldValues("FeatureId", rolePermissionController.GetSelectedFieldValuesFeatureIdCallback);
        s.GetSelectedFieldValues("CanAdd", rolePermissionController.GetSelectedFieldValuesCanAddCallback);
        s.GetSelectedFieldValues("CanEdit", rolePermissionController.GetSelectedFieldValuesCanEditCallback);
        s.GetSelectedFieldValues("CanDelete", rolePermissionController.GetSelectedFieldValuesCanDeleteCallback);
        s.GetSelectedFieldValues("CanView", rolePermissionController.GetSelectedFieldValuesCanCanViewCallback);

    },
    GetSelectedFieldValuesCallback: function (values) {
        IDS.SetText(values);
    },


    GetSelectedFieldValuesModuleIdCallback: function (values) {
        ModuleIdS.SetText(values);
    },

    GetSelectedFieldValuesFeatureIdCallback: function (values) {
        FeatureIdS.SetText(values);
    },

    GetSelectedFieldValuesCanAddCallback: function (values) {
        CanAddS.SetText(values);
    },

    GetSelectedFieldValuesCanEditCallback: function (values) {
        CanEditS.SetText(values);
    },

    GetSelectedFieldValuesCanDeleteCallback: function (values) {
        CanDeleteS.SetText(values);
    },


    GetSelectedFieldValuesCanCanViewCallback: function (values) {
        CanViewS.SetText(values);
    },

    SetAllUserPermission: function (s, e) {
        // var classID = ClassSetupID.GetSelectedValues();
        var option = chkBoxLstAll.GetSelectedValues();

        var UserID = txtUserName.GetValue();
        if (UserID.toString() == "") {
            alert("Please Select User");
            return;
        }
        $.ajax({
            url: "/UserPermission/SetAllUserPermission",
            type: "POST",
            data: { mOption: option.toString(), mUserID: UserID.toString() },
            beforeSend: function () {
                LoadingPanelUserPermission.Show();
            },
            complete: function () {
                LoadingPanelUserPermission.Hide();
            },
            success: function (data) {
                //CallbackPanelUP.PerformCallback();

                $("#UserPermissionSplitter_1_CC").html(data);
                var winHeight = document.getElementById('UserPermissionSplitter_1_CC').offsetHeight;

                GridUserPermission.SetHeight(winHeight - 10);
            },
            error: function () {
            }
        });

    },



    UpdateRolePermisttion: function (s, e) {

//        var option = chkBoxLstAll.GetSelectedValues();

        var mRoleID = RoleidS.GetValue();
        //alert(mUserID)
        if (mRoleID == null) {
            mRoleID = 0;
            alert("Please Select Role.");
            return;
        }
        var mModuleId = ModuleIdS.GetValue();
        if (mModuleId == "") {
            alert("Please Select Module.");
            return;
        }
        var mFeatureId = FromFeatureID.GetValue();
        if (mFeatureId == null) {
            mFeatureId = 0;
            alert("Please Select Feature.");
            return;
        }
       
        $("#lblmessage").html('');

        $.ajax({
            url: "/RolePermission/UpdateRolePermission",
            type: "POST",
            data: { pRoleIDs: mRoleID.toString(), PModuleId: mModuleId.toString(), PFeatureId: mFeatureId.toString() },
            beforeSend: (function (data) {
                LoadingPanelUserPermission.Show();
            }),
            success: function (data) {
                
                alert(data.ResponseMessage)

                var htmlres = "<h3>" + data.ResponseMessage + "</h3>"

                 //$('#divsmsresponse').empty();
                //$('#lblmessage').append(htmlres);

                $("#lblmessage").html(htmlres);
                
            },
            error: function () {
            }
        }).done(function (data) {
            LoadingPanelUserPermission.Hide();

        });


    },











    RowSelectionChange: function OnGridFocusedRowChanged(s, e) {

        s.GetRowValues(s.GetFocusedRowIndex(), 'PermitId', rolePermissionController.RefreshTabsView);
    },

    RefreshTabsView: function (values) {
        var nID;
        if (values != null)
        {
            nID = values;
        }
        $.ajax({
            url: "/UserPermission/UserPermissionGridRowChange",
            type: "POST",
            data: { ID: nID },
            success: function (data) {
                $("#UserPermissionSplitter_1_CC").html(data);
            },
            error: function () {
            }
        });
    }



}