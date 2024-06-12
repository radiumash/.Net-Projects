using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using appSchool.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace appSchool.Repositories
{
    public class UserPermissionRepository : GenericRepository<RolePermission>
    {
        public UserPermissionRepository() : base(new dbSchoolAppEntities()) { }
        public UserPermissionRepository(dbSchoolAppEntities dbContext) : base(dbContext) { }



        public int CheckDuplicatePermission(int mUserID, byte mCompID, byte mBranchID)
        {

            int objUserID = 0;
            UserPermission objuser = new UserPermission();
            objuser = this.context.UserPermissions.Where(i => i.UserId == mUserID && i.CompID==mCompID && i.BranchID==mBranchID ).FirstOrDefault();
            if (objuser != null)
            {
                objUserID = int.Parse(objuser.UserId.ToString());
            }

            return objUserID;
        }


        public List<RolePermission> GetAllFormNameForPermission()
        {
            List<RolePermission> objvUserPermission = new List<RolePermission>();
            objvUserPermission = this.context.RolePermissions.Where(x => x.Id > 0).ToList();
            return objvUserPermission;
        }


        public UserPermission CheckUserPermissionModulewise(int mUserID ,int mMenuID, byte mCompID, byte mBranchID )
        {
            UserPermission obj = new UserPermission();
            
            obj = this.context.UserPermissions.Where(a =>a.UserId == mUserID && a.MenuId == mMenuID && a.CompID==mCompID && a.BranchID==mBranchID).SingleOrDefault();
            return obj;
        }


        public List<RolePermission> GetUserPermissionListUserwise(int UserID, byte mCompID, byte mBranchID)
        {
            byte muserID = byte.Parse(UserID.ToString());

            List<RolePermission> objvUserPermission = new List<RolePermission>();
            objvUserPermission = this.context.RolePermissions.Where(x => x.RoleId==muserID && x.CompID==mCompID && x.BranchID==mBranchID ).ToList();
            return objvUserPermission;
        }
        public void UpdateAllPermissionToUser(RolePermission UPermit, bool Add, bool Mod, bool Del,bool view)
        {

            RolePermission editUPermit = this.GetByID(UPermit.Id);
            if (editUPermit != null)
            {

                editUPermit.CanAdd = Add;
                editUPermit.CanEdit = Mod;
                editUPermit.CanDelete = Del;
                editUPermit.CanView = view;

                this.Update(editUPermit);
            }
        }


        public void UpdateUserPermission(RolePermission UPermit)
        {
            RolePermission editUPermit = this.GetByID(UPermit.Id);
            if (editUPermit != null)
            {
              
                editUPermit.CanAdd = UPermit.CanAdd;
                editUPermit.CanEdit = UPermit.CanEdit;
                editUPermit.CanDelete = UPermit.CanDelete;
                editUPermit.CanView = UPermit.CanView;

                this.Update(editUPermit);
            }




        }




       
    }
  
}