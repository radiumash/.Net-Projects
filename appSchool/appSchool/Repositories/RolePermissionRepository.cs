using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using appSchool.ViewModels;
using DevExpress.Web.Mvc;

namespace appSchool.Repositories
{
    public class RolePermissionRepository : GenericRepository<RolePermission> 
    {
        public RolePermissionRepository() : base(new dbSchoolAppEntities()) { }
        public RolePermissionRepository(dbSchoolAppEntities dbContext): base(dbContext) {}

        public RolePermission GetRolePermissionByRoleID(int mRoleID, int mfeatureID)
        {
            RolePermission obj = new RolePermission();
            obj = this.context.RolePermissions.Where(x => x.RoleId == mRoleID && x.FeatureId == mfeatureID).FirstOrDefault();

            return obj;
        }

        public RolePermission GetRolePermissionByRoleANDModelANDFeatureID(int mRoleID, int mModuleID, int mfeatureID)
        {
            RolePermission obj = new RolePermission();
            obj = this.context.RolePermissions.Where(x => x.RoleId == mRoleID && x.FeatureId == mfeatureID && x.ModuleId == mModuleID).FirstOrDefault();

            return obj;
        }

        public List<RolePermission> GetRolePermissionByRoleIDAndModuleID(int mRoleID)
        {
            List<RolePermission> obj = new List<RolePermission>();
            obj = this.context.RolePermissions.Where(x => x.RoleId == mRoleID).ToList();

            return obj;
        }


        public List<RolePermission> GetRolePermissionByRoleIDAndModuleIDDefult()
        {
            List<RolePermission> obj = new List<RolePermission>();
            //obj = this.context.RolePermissions.Where(x => x.RoleId == mRoleID).FirstOrDefault();

            return obj;
        }

        public List<RolePermission> GetRolePermissionList(byte mCompID, byte mBranchID)
        {
            List<RolePermission> obj = new List<RolePermission>();
            obj = this.context.RolePermissions.Where(x => x.CompID == mCompID && x.BranchID == mBranchID).ToList();


            return obj;
        }



    }








}
