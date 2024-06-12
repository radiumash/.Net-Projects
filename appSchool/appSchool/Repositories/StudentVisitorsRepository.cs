using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using appSchool.ViewModels;

namespace appSchool.Repositories
{
    public class StudentVisitorsRepository : GenericRepository<StudentVisitor> 
    {
        public StudentVisitorsRepository() : base(new dbSchoolAppEntities()) { }
        public StudentVisitorsRepository(dbSchoolAppEntities dbContext) : base(dbContext) { }



        public List<StudentVisitor> GetStudentVisitorList(byte mCompID, byte mBranchID)
        {
            List<StudentVisitor> obj = new List<StudentVisitor>();
            obj = this.context.StudentVisitors.Where(i => i.VisitorID > 0 && i.CompID == mCompID && i.BranchID== mBranchID).OrderByDescending(i => i.VisitDate).ToList();

            return obj;
        }

        public void InsertStudentVisitorMaster(StudentVisitor obj)
        {
            this.Insert(obj);
        }
        //public void AddNewStudentComplaint(StudentComplaint obj)
        //{

        //    obj.StudentID = SetStudentComplaint(obj);
        //    Insert(obj);
        //    return;
        //}
        public void UpdateStudentVisitor(StudentVisitor obj, byte UserID)
        {
            StudentVisitor objNew = this.GetByID(obj.VisitorID);
            if (objNew != null)
            {
                objNew.VisitorName = obj.VisitorName;
                objNew.RelationShip = obj.RelationShip;
                objNew.StudentID = obj.StudentID;
                objNew.VisitorMobileNo = obj.VisitorMobileNo;
                objNew.VisitDate = obj.VisitDate;
                objNew.VisitTime = obj.VisitTime;
                objNew.VisitorAddress = obj.VisitorAddress;
                objNew.Remark = obj.Remark;
                objNew.SessionID = obj.SessionID;
                objNew.UIDMod = UserID;
                objNew.ModDate = DateTime.Now;


                Update(objNew);
            }
            return;
        }
        //private string SetStudentComplaint(StudentComplaint obj)
        //{
        //    string strDescription = string.Empty;
        //    strDescription += this.context.Classes.Where(x => x.ClassID == obj.ClassID).FirstOrDefault().ClassName+" ";
        //    strDescription += this.context.Sections.Where(x => x.SectionID == obj.SectionID).FirstOrDefault().SectionName + "  ";
        //    //if(obj.ClassCategoryID!=null)
        //    //    strDescription += this.context.ClassCategories.Where(x => x.ClassCategoryID == obj.ClassCategoryID).FirstOrDefault().ClassCategoryName;
        //    return strDescription;
        //}

        public IEnumerable<listItem> GetStudentComplaintItemsforClass(int typeID)
        {
            List<listItem> lst = new List<listItem>();
            lst.AddRange(
                from xx in this.context.ClassSetups
                where xx.ClassID == typeID
                orderby xx.DisplayOrder 
                select new listItem() { Value = xx.ClassSetupID, Description = xx.ClassDescription }
                );
            return lst;
        }
        public IEnumerable<listItem> GetClassSetupforSelectWithNone()
        {
            List<listItem> lst = new List<listItem>();
            lst.Add(new listItem() { Value = -1, Description = "(None)" });
            lst.AddRange(
                from xx in this.context.ClassSetups
                select new listItem() { Value = xx.ClassSetupID, Description = xx.ClassDescription }
                );
            return lst;
        }
        public IEnumerable<listItem> GetClassSetupforSelect()
        {
            IEnumerable<listItem> lst = null;
            lst =(
                from xx in this.context.ClassSetups
                select new listItem() { Value = xx.ClassSetupID, Description = xx.ClassDescription }
                );
            return lst;
        }
        public List<vClass> GetAllClassForSMS()
        {
            List<vClass> obj = new List<vClass>();
            obj = this.context.vClasses.Where(i => i.ClassID >0).ToList();

            return obj;
        }

        public List<ClassSetup> GetAllClassNameByClassID(int mClassID)
        {
            List<ClassSetup> obj = new List<ClassSetup>();
            obj = this.context.ClassSetups.Where(i => i.ClassID == mClassID).OrderBy(i => i.DisplayOrder).ToList();

            return obj;
        }

        public string GetClassNameByClassID(int classID)
        {
           
            string mclassName = this.context.vClasses.Where(x => x.ClassID == classID).SingleOrDefault().ClassName;

            return mclassName ;
            
        }
        
      

        public int CheckDelete(int mID)
        {
            int ID = 0;
            ID = this.context.StudentSessions.Where(x => x.ClassSetupID == mID).Count();
            return ID;
        }

        
    }








}
