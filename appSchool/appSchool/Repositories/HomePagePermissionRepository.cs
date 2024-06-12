using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using appSchool.ViewModels;
using DevExpress.Web.Mvc;

namespace appSchool.Repositories
{


    public class HomePagePermissionRepository : GenericRepository<HomePageRolePermission>
    {
        public HomePagePermissionRepository() : base(new dbSchoolAppEntities()) { }
        public HomePagePermissionRepository(dbSchoolAppEntities dbContext) : base(dbContext) { }


        public List<HomePageRolePermission> GetHomePageRolePermissionList(byte mCompID, byte mBranchID)
        {
            List<HomePageRolePermission> obj = new List<HomePageRolePermission>();
            obj = this.context.HomePageRolePermissions.Where(x => x.CompID == mCompID && x.BranchID == mBranchID).ToList();


            return obj;
        }

        public List<HomePageRolePermission> GetHomePageRolePermissionListBYFeatureOrder(byte mCompID, byte mBranchID)
        {
            List<HomePageRolePermission> obj = new List<HomePageRolePermission>();
            obj = this.context.HomePageRolePermissions.Where(x => x.CompID == mCompID && x.BranchID == mBranchID).OrderBy(x=> x.FeatureOrder).ToList();

            return obj;
        }

        public HomePageRolePermission GetHomePageRolePermission(int moduleID, byte mCompID, byte mBranchID)
        {
            HomePageRolePermission obj = new HomePageRolePermission();
            obj = this.context.HomePageRolePermissions.Where(x => x.ModuleID == moduleID && x.CompID == mCompID && x.BranchID == mBranchID).SingleOrDefault();
            return obj;
        }


        public void AddNewAHomePageRolePermission(HomePageRolePermission obj)
        {
            this.Insert(new HomePageRolePermission() { ModuleID = obj.ModuleID, FeatureID = obj.FeatureID, FeatureOrder = obj.FeatureOrder, CanView = obj.CanView, CompID = obj.CompID, BranchID = obj.BranchID });
            return;
        }

        public void AddNewHomePageRolePermission(HomePageRolePermission obj)
        {
            this.Insert(obj);
            return;
        }


        public void UpdateHomePageRolePermission(HomePageRolePermission obj)
        {
            HomePageRolePermission home = this.GetByID(obj.HomePageID);
           
            home.FeatureOrder = obj.FeatureOrder;
            home.CanView = obj.CanView;
            home.ModDate = obj.ModDate;
            this.Update(home);
            return;
        }
        public void DeleteHomePageRolePermission(HomePageRolePermission obj)
        {
            HomePageRolePermission home = this.GetByID(obj.HomePageID);
            this.Delete(home);
            return;
        }


        public int CheckDelete(int mID)
        {
            int ID = 0;
            ID = this.context.HomePageRolePermissions.Where(x => x.HomePageID == mID).Count();
            return ID;
        }

        public int MaxFeatureOrderNo(byte mCompID, byte mBranchID)
        {
            int MaxOrder = 0;
            try
            {
                MaxOrder = this.context.HomePageRolePermissions.Where(x => x.FeatureOrder > 0 && x.CompID == mCompID && x.BranchID == mBranchID).Max(y => y.FeatureOrder);
            }
            catch (Exception ex)
            { }
            return MaxOrder;
        }


        public bool CheckISExist(int mModuleID, int mFeatureID, bool Ismodulerequired, byte mCompID, byte mBranchID)
        {
            int ID = 0;
            ID = this.context.HomePageRolePermissions.Where(x => x.FeatureID == mFeatureID && x.IsOnlyModuleRequire == Ismodulerequired && x.ModuleID == mModuleID && x.CompID == mCompID && x.BranchID == mBranchID).Count();
           
            if(ID > 0)
            return true;
            else
            return false;

        }



        public List<vHomePageUserPermission> GetAllModuleAndFeatureListByRoleID(int mRoleID, byte mCompID, byte mBranchID)
        {
            List<vHomePageUserPermission> obj = new List<vHomePageUserPermission>();
            obj = this.context.vHomePageUserPermissions.Where(x => x.CompID == mCompID && x.BranchID == mBranchID && x.RoleID == mRoleID && x.CanView == true).ToList();

            //string sql = "SELECT * FROM dbo.vHomePageUserPermission   ";

            //obj = this.context.vHomePageUserPermissions.SqlQuery(sql).ToList();
            
            return obj;
        }

        public List<vHomePageUserPermission> GetAllModuleAndFeatureListByRoleIDOrderbyIndex(int moduleID, int mRoleID, byte mCompID, byte mBranchID)
        {
            List<vHomePageUserPermission> obj = new List<vHomePageUserPermission>();
             obj = this.context.vHomePageUserPermissions.Where(x => x.ModuleID == moduleID && x.CompID == mCompID && x.BranchID == mBranchID && x.RoleID == mRoleID && x.CanView == true).OrderBy(y => y.FeatureOrder).ToList();

            //string sql = "SELECT * FROM dbo.vHomePageUserPermission where CompID=" + mCompID + " AND BranchID=" + mBranchID + " AND CanView = 1  AND RoleId=" + mRoleID + "  Order by FeatureOrder ";
            //obj = this.context.vHomePageUserPermissions.SqlQuery(sql).ToList();

            return obj;
        }

        public List<vHomePageUserPermission> GetAllModuleAndFeatureListByFiltertext(int mRoleID, byte mCompID, byte mBranchID, string FilterText)
        {
            List<vHomePageUserPermission> obj = new List<vHomePageUserPermission>();

            string sql = "SELECT * FROM dbo.vHomePageUserPermission where CompID=" + mCompID + " AND BranchID=" + mBranchID + " AND CanView = 1  AND RoleId=" + mRoleID + " and FMenuText Like '%" + FilterText + "%' ";

            obj = this.context.vHomePageUserPermissions.SqlQuery(sql).ToList();

            return obj;
        }

        public List<vHomePageUserPermission> GetAllModuleAndFeatureList(byte mCompID, byte mBranchID)
        {
            List<vHomePageUserPermission> obj = new List<vHomePageUserPermission>();
            obj = this.context.vHomePageUserPermissions.Where(x => x.CompID == mCompID && x.BranchID == mBranchID).ToList();

            return obj;
        }


    }



}
