var homepagepermissionController = {

    SelectedRole: function (s, e) {

        var mRoleID = s.GetValue();

        if (mRoleID == null) {
            alert("Please Select Role");
            return;
        }

        $.ajax({
            url: "/HomePagePermission/GetModuleListByRoleID",
            type: "POST",
            data: { RoleID: mRoleID },
            success: function (data) {

                var ModuleIDList = JSON.parse(data.ModuleList);
                ModuleID.ClearItems();
                for (var i = 0; i < ModuleIDList.length; i++) {
                    ModuleID.AddItem(ModuleIDList[i].Name, ModuleIDList[i].Id);
                }

            },
            error: function () {
            }
        });
    },

    SelectedAppModule: function (s, e) {
      
        var mModuleID = ModuleID.GetValue();
      
        
        if (mModuleID  == null) {
            alert("Please Select Module");
            return;
        }

        $.ajax({
            url: "/HomePagePermission/GetFeatureListByModuleID",
            type: "POST",
            data: { ModuleID: mModuleID },
            beforeSend: (function (data) {
                LoadingPanelHomePagePermission.Show();
            }),
            success: function (data) {


                

                var FeatureIDList = JSON.parse(data.FeatureList);
                FeatureID.ClearItems();
                for (var i = 0; i < FeatureIDList.length; i++) {
                    FeatureID.AddItem(FeatureIDList[i].FMenuText, FeatureIDList[i].FeatureId);
                }

                txtOrderno.SetText(data.MaxFeatureOrderReturn)
            },
            error: function () {
            }
        }).done(function (data) {
            LoadingPanelHomePagePermission.Hide();

        });
    },

    SelectIsmodulerequired: function (s, e) {

        
        var mIsmodulerequired = chkIsmodulerequired.GetChecked();

        

        if (mIsmodulerequired) {

            $.ajax({
                url: "/HomePagePermission/GetFeatureListForISOnlyModulerequired",
                type: "POST",
                data: {},
                beforeSend: (function (data) {
                    LoadingPanelHomePagePermission.Show();
                }),
                success: function (data) {

                    var FeatureIDList = JSON.parse(data.FeatureList);
                    FeatureID.ClearItems();
                    for (var i = 0; i < FeatureIDList.length; i++) {
                        FeatureID.AddItem(FeatureIDList[i].FMenuText, FeatureIDList[i].FeatureId);
                    }


                },
                error: function () {
                }
            }).done(function (data) {
                LoadingPanelHomePagePermission.Hide();

            });

        }
        else {
            homepagepermissionController.SelectedAppModule();
        }
       
    },

  
    AddHomePageModuleFeature (s, e) {

        var mRoleID = 1;

        //var mRoleID = RoleID.GetValue();

        //if (mRoleID == null || mRoleID == "") {
        //    alert("Please Select Role");
        //    return;
        //}

        var mfeatureorder = txtOrderno.GetText();

        if (mfeatureorder == "" ) {
            alert("Please enter order");
            return;
        }

        if (mfeatureorder == "0") {
            alert("Please enter valid feature order");
            return;
        }

        var mModuleID = ModuleID.GetValue();

        if (mModuleID == null || mModuleID == "") {
            alert("Please Select Module");
            return;
        }

        var mFeatureID = FeatureID.GetValue();

        if (mFeatureID == null || mFeatureID == "") {
            alert("Please Select Feature");
            return;
        }
        var mIsmodulerequired = chkIsmodulerequired.GetChecked();

        var mIsvisible = chkCanview.GetChecked();
        

        $.ajax({

            url: "/HomePagePermission/AddNewHomePagePermissionByClientSide",
            type: "Post",
            datatype: "json",
            data: { RoleID: mRoleID, ModuleID: mModuleID, FeatureID: mFeatureID, FeatureOrder: mfeatureorder, Ismodulerequired: mIsmodulerequired, Isvisible: mIsvisible},

            beforeSend: (function (data) { LoadingPanelHomePagePermission.Show(); }),

            success: function (data) {

                alert(data.DataResponseMsg)

                $("#divhomepahepermit").html(data.ListDataHomePagePermission);
                    
            },
            error: function () {

            }

        }).done(function (data) { LoadingPanelHomePagePermission.Hide(); });


    },
  
   

}