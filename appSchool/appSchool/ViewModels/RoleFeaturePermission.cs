using appSchool.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace appSchool.ViewModels
{
    public class RoleFeaturePermission
    {

        

        public static  bool GetUserAddPermission(object currentuserRoleID, appFeatureCode featureCode, apppermittionMode permitMode)
        {
            bool ispermit = false;

            try 
            {
                int userRoleID = 0;

                if (currentuserRoleID != null)
                    userRoleID = int.Parse(currentuserRoleID.ToString());

                RolePermission objpermit = new UnitOfWork().rolePermissionservices.GetRolePermissionByRoleID(userRoleID, (int)featureCode);

                if (objpermit != null)
                {
                    switch (permitMode)
                    {
                        case apppermittionMode.appView:
                            ispermit = objpermit.CanView;
                            break;
                        case apppermittionMode.appInsert:
                            ispermit = objpermit.CanAdd;
                            break;
                        case apppermittionMode.appUpdate:
                            ispermit = objpermit.CanEdit;
                            break;
                        case apppermittionMode.appDelete:
                            ispermit = objpermit.CanDelete;
                            break;
                        
                    }
                }
                
            }
            catch(Exception ex)
            {

            }
            return ispermit;
        }



    }



}