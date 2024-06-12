var userPermissionController = {

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
        $("#txtUserName_I").val(RoleId);
        //alert(UserID);
        if (RoleId.toString() == "") {
            alert("Please Select User");
            return;
        }


        $.ajax({
            //url: "/UserPermission/CreateUserPermission",
            url: "/UserPermission/GetAllRolePermission",
            type: "POST",
            data: { PRoleId: RoleId.toString() },
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

    SelectedAppModule: function (s, e) {
        // var classID = ClassSetupID.GetSelectedValues();
        var ModuleID = s.GetValue();
        $("#txtModuleName_I").val(ModuleID);
        //alert(ModuleID);
        if (ModuleID.toString() == "") {
            alert("Please Select Module");
            return;
        }

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
        s.GetSelectedFieldValues("Id", userPermissionController.GetSelectedFieldValuesCallback);
        s.GetSelectedFieldValues("ModuleId", userPermissionController.GetSelectedFieldValuesModuleIdCallback);
        s.GetSelectedFieldValues("FeatureId", userPermissionController.GetSelectedFieldValuesFeatureIdCallback);
        s.GetSelectedFieldValues("CanAdd", userPermissionController.GetSelectedFieldValuesCanAddCallback);
        s.GetSelectedFieldValues("CanEdit", userPermissionController.GetSelectedFieldValuesCanEditCallback);
        s.GetSelectedFieldValues("CanDelete", userPermissionController.GetSelectedFieldValuesCanDeleteCallback);
        s.GetSelectedFieldValues("CanView", userPermissionController.GetSelectedFieldValuesCanCanViewCallback);

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

        var option = chkBoxLstAll.GetSelectedValues();

        var mRoleID = txtUserName.GetValue();
        //alert(mUserID)
        if (mRoleID == null) {
            mRoleID = 0;
            alert("Please Select Role.");
            return;
        }
        //var mModuleId = ModuleIdS.GetValue();
        //if (mModuleId == "") {
        //    alert("Please Select Module.");
        //    return;
        //}
        //var mFeatureId = FeatureIdS.GetValue();
        //if (mFeatureId == null) {
        //    mFeatureId = 0;
        //    alert("Please Select Feature.");
        //    return;
        //}
       
      

        $.ajax({
            url: "/UserPermission/UpdateRolePermission",
            type: "POST",
            data: { pRoleIDs: mRoleID.toString(), poption: option.toString(), },
            beforeSend: (function (data) {
                LoadingPanelUserPermission.Show();
            }),
            success: function (data) {

                console.log(data)
                $("#divuserrole").html(data);
                //var winHeight = document.getElementById('SessionTransferSplitter_1_CC').offsetHeight;
                //GridSessionTransfer.SetHeight(winHeight - 10);
                //StudentIDs.SetText("");
                // cbSelectAll.SetChecked(false);
            },
            error: function () {
            }
        }).done(function (data) {
            LoadingPanelUserPermission.Hide();

        });


    },











    RowSelectionChange: function OnGridFocusedRowChanged(s, e) {

        s.GetRowValues(s.GetFocusedRowIndex(), 'PermitId', userPermissionController.RefreshTabsView);
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