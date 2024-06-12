using System;
using System.Collections.Generic;
using System.Text;

namespace appSchool.App_Code.BL
{


    public  class ExamMarkSheetSql
    {

       private string StudentIds ; 
       private string ClassId;
       private string SectionId; 
       private string ExamId;
       private string SubExamId;
       private int COMPANY_ID;
       private int SESSION_ID;
       private int BRANCH_ID;
       private int Order;
       private int Subjectlevel;
       private string TermOneID = "0";
       private string TermTwoID = "0";
       public string SqlDetail1 {get; set;}
       public string SqlDetail2 { get; set; }
       public string SqlDetail3 { get; set; }
       public string SqlDetail4 { get; set; }

     

        public ExamMarkSheetSql()
        {

        }

        public ExamMarkSheetSql(string mStudentIds, string mClassId, string mSectionId, string mExamId, int mOrder, int mCOMPANY_ID,int mSESSION_ID,int mBRANCH_ID)
        {
             StudentIds = mStudentIds ;
             ClassId = mClassId;
             ExamId = mExamId;
             SectionId = mSectionId;
             Order = mOrder;
             COMPANY_ID = mCOMPANY_ID;
             SESSION_ID = mSESSION_ID;
             BRANCH_ID = mBRANCH_ID;

        }

        public ExamMarkSheetSql(string mStudentIds, string mClassId, string mExamId, string mSubExamId, int mOrder, int mSubjectlevel, string mTermOneID, string mTermTwoID,int mCOMPANY_ID, int mSESSION_ID, int mBRANCH_ID)
        {
            StudentIds = mStudentIds;
            ClassId = mClassId;
            ExamId = mExamId;
            SubExamId = mSubExamId;
            Order = mOrder;
            Subjectlevel = mSubjectlevel;
            TermOneID = mTermOneID;
            TermTwoID = mTermTwoID;
            COMPANY_ID = mCOMPANY_ID;
            SESSION_ID = mSESSION_ID;
            BRANCH_ID = mBRANCH_ID;
        }

        public string GetExamMarksheetSql()
        {
            string sql = "";
            
            
            if (COMPANY_ID == 1)
              ReturnNirmalaExamMarksheetSql();

            if (COMPANY_ID == 2)
                 ReturnStMarkExamMarksheetSql();

            if (COMPANY_ID == 4)
                 ReturnStXavierExamMarksheetSql();

            if (COMPANY_ID == 5)
                ReturnStJosephExamMarksheetSql();

            if (COMPANY_ID == 7)
                 ReturnShantiDhamExamMarksheetSql();

            
            return sql;
        }

        public void ReturnNirmalaExamMarksheetSql()
        {
            //string sql = "";

            #region for nirmala
            if (COMPANY_ID == 1)
            {
                if (Order == 1)
                {
                    SqlDetail1 = "SELECT TOP (100) PERCENT dbo.SubjectLevelOne.SubjectNameL1 AS Subject,dbo.SubjectLevelTwo.SubjectNameL2 AS SubjectD," +
                        "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks1)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=3)) as MaxEnglishOral, " +
                        "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks1)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=4)) as MaxEnglishWritten, " +
                        "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks1)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=7)) as MaxHindiOral, " +
                        "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks1)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=8)) as MaxHindiWritten, " +
                        "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks1)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=11)) as MaxMathsOral, " +
                        "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks1)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=12)) as MaxMathsWritten, " +
                        "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks1)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=18)) as MaxScience, " +
                        "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks1)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=19)) as MaxPoem, " +
                         "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks1)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=20)) as MaxSocialStudies, " +
                         " (select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks1)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=1)) as MaxEnglishI, " +
                        " (select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks1)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=2)) as MaxEnglishII, " +
                        " (select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks1)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=5)) as MaxHindiI, " +
                        "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks1)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=6)) as MaxHindiII," +
                         "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks1)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=9)) as MaxMathsI, " +
                         "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks1)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=10)) as MaxMathsII, " +
                        "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks1)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=59)) as MaxComputerPractical, " +
                        "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks1)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=85)) as MaxComputer, " +
                         "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks1)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=81)) as MaxEnglish, " +
                         "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks1)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=82)) as MaxHindi, " +
                         "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks1)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=83)) as MaxMaths, " +
                         "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks1)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=45)) as MathsHomeScience, " +
                        "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks1)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=28)) as MaxGeneralKnowledge, " +
                         "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks1)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=23)) as MaxSanskrit, " +
                           "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks1)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=25)) as MaxGeography, " +
                         "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks1)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=24)) as MaxHistory, " +
                        "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks1)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=95 )) as MaxGenHindiI, " +
                          "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks1)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL1=53)) as MaxArtI, " +
                         "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks1)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL1=48)) as MaxSociologyI, " +

                          "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks1)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=94)) as MaxBiologyI, " +
                          "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks1)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=58)) as MaxBiologyPractical, " +
                        "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks1)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=93)) as MaxChemistryI, " +
                        "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks1)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=57)) as MaxChemistryPractical, " +
                        "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks1)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=92)) as MaxPhysicsI, " +
                        "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks1)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=56)) as MaxPhysicsPractical, " +
                        "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks1)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=106)) as MAxGKOral, " +
                        "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks1)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=96)) as MaxArt, " +
                        
                        "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks1)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=97)) as MaxEnglishPractical, " +
                        "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks1)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=98)) as MaxHindiPractical, " +
                        "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks1)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=101)) as MaxMathsHomeSciencePractical, " +
                        "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks1)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=99)) as MaxSocialStudiesPractical, " +
                         "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks1)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=100)) as MaxSciencePractical, " +

                        "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMax1)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL1=1)) as MaxCalEnglishI, " +
                       " (select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMax1)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL1=2)) as MaxCalHindiI, " +
                        "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMax1)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL1=3)) as MaxCalMathsI, " +
                        "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMax1)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL1=11)) as MaxCalComputer, " +
                       "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks1)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL1=47)) as MaxCivicsI, " +




                        " dbo.ExamResult.ObtainMarks1 AS Obtain1,dbo.ExamResult.ObtainMarks2 AS Obtain2,dbo.ExamResult.ObtainMarks3 AS Obtain3,dbo.ExamResult.ObtainMarks4 AS Obtain4, dbo.ExamResult.StudentID, " +
                        " dbo.ExamSetupDetail.OrderNo,ExamResult.MaxMark1 as Max1,ExamResult.MaxMark2 as Max2,ExamResult.MaxMark3 as Max3,ExamResult.MaxMark4 as Max4,ExamResult.MinMark1 as Min1,ExamResult.MinMark2 as Min2,ExamResult.MinMark3 as Min3,ExamResult.MinMark4 as Min4 " +
                        " FROM dbo.ExamResult INNER JOIN dbo.SubjectLevelOne ON dbo.ExamResult.SubjectIDL1 = dbo.SubjectLevelOne.IdL1 INNER JOIN " +
                        " dbo.ExamSetupDetail ON dbo.SubjectLevelOne.IdL1 = dbo.ExamSetupDetail.SubjectIDL1 AND dbo.SubjectLevelOne.BranchID = dbo.ExamSetupDetail.BranchID AND " +
                        " dbo.SubjectLevelOne.CompID = dbo.ExamSetupDetail.CompID " +
                        " INNER JOIN dbo.SubjectLevelTwo ON dbo.ExamResult.SubjectIDL2 = dbo.SubjectLevelTwo.IdL2 AND dbo.ExamSetupDetail.SubjectIDL2 = dbo.SubjectLevelTwo.IdL2 " +
                        " INNER JOIN dbo.ExamSetupMaster ON dbo.ExamSetupDetail.ExamSetupID = dbo.ExamSetupMaster.ExamSetupID AND " +
                        " dbo.ExamSetupDetail.BranchID = dbo.ExamSetupMaster.BranchID AND dbo.ExamSetupDetail.CompID = dbo.ExamSetupMaster.CompID AND dbo.ExamResult.ClassID = dbo.ExamSetupMaster.ClassID " +
                        " WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.ExamResult.StudentID IN (" + StudentIds + ")) " +
                        " AND (dbo.ExamResult.SessionID =" + SESSION_ID + ") AND (dbo.ExamSetupMaster.SessionID=" + SESSION_ID + ") AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND " +
                        " (dbo.ExamResult.BranchID =" + BRANCH_ID + ") AND (dbo.SubjectLevelOne.SubjectCategoryID = 1) AND (dbo.ExamSetupMaster.ExamID =" + ExamId + ")" +
                        " ORDER BY dbo.ExamResult.StudentID, dbo.ExamSetupDetail.OrderNo ";


                    SqlDetail2 = "SELECT TOP (100) PERCENT dbo.SubjectLevelOne.SubjectNameL1 AS Subject,dbo.SubjectLevelTwo.SubjectNameL2 AS SubjectD,dbo.ExamResult.ObtainMarks1 AS Obtain1,dbo.ExamResult.ObtainMarks2 AS Obtain2,dbo.ExamResult.ObtainMarks3 AS Obtain3,dbo.ExamResult.ObtainMarks4 AS Obtain4, dbo.ExamResult.StudentID, " +
                                " dbo.ExamSetupDetail.OrderNo,ExamResult.MaxMark1 as Max1,ExamResult.MaxMark2 as Max2,ExamResult.MaxMark3 as Max3,ExamResult.MaxMark4 as Max4,ExamResult.MinMark1 as Min1,ExamResult.MinMark2 as Min2,ExamResult.MinMark3 as Min3,ExamResult.MinMark4 as Min4 " +
                                " FROM dbo.ExamResult INNER JOIN dbo.SubjectLevelOne ON dbo.ExamResult.SubjectIDL1 = dbo.SubjectLevelOne.IdL1 INNER JOIN " +
                                " dbo.ExamSetupDetail ON dbo.SubjectLevelOne.IdL1 = dbo.ExamSetupDetail.SubjectIDL1 AND dbo.SubjectLevelOne.BranchID = dbo.ExamSetupDetail.BranchID AND " +
                                " dbo.SubjectLevelOne.CompID = dbo.ExamSetupDetail.CompID " +
                                " INNER JOIN dbo.SubjectLevelTwo ON dbo.ExamResult.SubjectIDL2 = dbo.SubjectLevelTwo.IdL2 AND dbo.ExamSetupDetail.SubjectIDL2 = dbo.SubjectLevelTwo.IdL2 " +
                                " INNER JOIN dbo.ExamSetupMaster ON dbo.ExamSetupDetail.ExamSetupID = dbo.ExamSetupMaster.ExamSetupID AND " +
                                " dbo.ExamSetupDetail.BranchID = dbo.ExamSetupMaster.BranchID AND dbo.ExamSetupDetail.CompID = dbo.ExamSetupMaster.CompID AND dbo.ExamResult.ClassID = dbo.ExamSetupMaster.ClassID " +
                                " WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.ExamResult.StudentID IN (" + StudentIds + ")) " +
                                " AND (dbo.ExamResult.SessionID =" + SESSION_ID + ")  AND (dbo.ExamSetupMaster.SessionID =" + SESSION_ID + ") AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND " +
                                " (dbo.ExamResult.BranchID =" + BRANCH_ID + ") AND (dbo.SubjectLevelOne.SubjectCategoryID = 2) AND (dbo.ExamSetupMaster.ExamID =" + ExamId + ")" +
                                " ORDER BY dbo.ExamResult.StudentID, dbo.ExamSetupDetail.OrderNo ";


                    SqlDetail3 = "SELECT TOP (100) PERCENT dbo.SubjectLevelOne.SubjectNameL1 AS Subject,dbo.SubjectLevelTwo.SubjectNameL2 AS SubjectD,dbo.ExamResult.ObtainMarks1 AS Obtain1,dbo.ExamResult.ObtainMarks2 AS Obtain2,dbo.ExamResult.ObtainMarks3 AS Obtain3,dbo.ExamResult.ObtainMarks4 AS Obtain4, dbo.ExamResult.StudentID, " +
                                " dbo.ExamSetupDetail.OrderNo,ExamResult.MaxMark1 as Max1,ExamResult.MaxMark2 as Max2,ExamResult.MaxMark3 as Max3,ExamResult.MaxMark4 as Max4,ExamResult.MinMark1 as Min1,ExamResult.MinMark2 as Min2,ExamResult.MinMark3 as Min3,ExamResult.MinMark4 as Min4 " +
                                " FROM dbo.ExamResult INNER JOIN dbo.SubjectLevelOne ON dbo.ExamResult.SubjectIDL1 = dbo.SubjectLevelOne.IdL1 INNER JOIN " +
                                " dbo.ExamSetupDetail ON dbo.SubjectLevelOne.IdL1 = dbo.ExamSetupDetail.SubjectIDL1 AND dbo.SubjectLevelOne.BranchID = dbo.ExamSetupDetail.BranchID AND " +
                                " dbo.SubjectLevelOne.CompID = dbo.ExamSetupDetail.CompID " +
                                " INNER JOIN dbo.SubjectLevelTwo ON dbo.ExamResult.SubjectIDL2 = dbo.SubjectLevelTwo.IdL2 AND dbo.ExamSetupDetail.SubjectIDL2 = dbo.SubjectLevelTwo.IdL2 " +
                                " INNER JOIN dbo.ExamSetupMaster ON dbo.ExamSetupDetail.ExamSetupID = dbo.ExamSetupMaster.ExamSetupID AND " +
                                " dbo.ExamSetupDetail.BranchID = dbo.ExamSetupMaster.BranchID AND dbo.ExamSetupDetail.CompID = dbo.ExamSetupMaster.CompID AND dbo.ExamResult.ClassID = dbo.ExamSetupMaster.ClassID " +
                               " WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.ExamResult.StudentID IN (" + StudentIds + ")) " +
                               " AND (dbo.ExamResult.SessionID =" + SESSION_ID + ") AND (dbo.ExamSetupMaster.SessionID =" + SESSION_ID + ") AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND " +
                               " (dbo.ExamResult.BranchID =" + BRANCH_ID + ") AND (dbo.SubjectLevelOne.SubjectCategoryID = 3) AND (dbo.ExamSetupMaster.ExamID =" + ExamId + ")" +
                               " ORDER BY dbo.ExamResult.StudentID, dbo.ExamSetupDetail.OrderNo ";

                }
                if (Order == 2)
                {
                    SqlDetail1 = "SELECT TOP (100) PERCENT dbo.SubjectLevelOne.SubjectNameL1 AS Subject,dbo.SubjectLevelTwo.SubjectNameL2 AS SubjectD, " +
                          "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks2)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=3)) as MaxEnglishOral, " +
                        "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks2)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=4)) as MaxEnglishWritten, " +
                        "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks2)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=7)) as MaxHindiOral, " +
                        "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks2)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=8)) as MaxHindiWritten, " +
                        "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks2)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=11)) as MaxMathsOral, " +
                        "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks2)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=12)) as MaxMathsWritten, " +
                        "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks2)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=18)) as MaxScience, " +
                        "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks2)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=19)) as MaxPoem, " +
                         "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks2)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=20)) as MaxSocialStudies, " +
                         " (select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks2)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=1)) as MaxEnglishI, " +
                        " (select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks2)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=2)) as MaxEnglishII, " +
                        " (select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks2)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=5)) as MaxHindiI, " +
                        "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks2)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=6)) as MaxHindiII," +
                         "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks2)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=9)) as MaxMathsI, " +
                         "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks2)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=10)) as MaxMathsII, " +
                        "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks2)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=59)) as MaxComputerPractical, " +
                        "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks2)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=85)) as MaxComputer, " +
                         "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks2)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=81)) as MaxEnglish, " +
                         "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks2)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=82)) as MaxHindi, " +
                         "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks2)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=83)) as MaxMaths, " +
                         "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks2)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=45)) as MathsHomeScience, " +
                        "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks2)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=28)) as MaxGeneralKnowledge, " +
                         "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks2)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=23)) as MaxSanskrit, " +
                           "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks2)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=25)) as MaxGeography, " +
                         "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks2)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=24)) as MaxHistory, " +
                        "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks2)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=95 )) as MaxGenHindiI, " +
                          "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks2)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL1=53)) as MaxArtI, " +
                         "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks2)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL1=48)) as MaxSociologyI, " +

                          "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks2)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=94)) as MaxBiologyI, " +
                          "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks2)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=58)) as MaxBiologyPractical, " +
                        "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks2)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=93)) as MaxChemistryI, " +
                        "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks2)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=57)) as MaxChemistryPractical, " +
                        "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks2)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=92)) as MaxPhysicsI, " +
                        "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks2)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=56)) as MaxPhysicsPractical, " +
                        "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks2)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=106)) as MAxGKOral, " +
                        "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks2)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=96)) as MaxArt, " +

                        "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks2)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=97)) as MaxEnglishPractical, " +
                        "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks2)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=98)) as MaxHindiPractical, " +
                        "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks2)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=101)) as MaxMathsHomeSciencePractical, " +
                        "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks2)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=99)) as MaxSocialStudiesPractical, " +
                         "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks2)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=100)) as MaxSciencePractical, " +

                        "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMax1)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL1=1)) as MaxCalEnglishI, " +
                       " (select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMax1)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL1=2)) as MaxCalHindiI, " +
                        "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMax1)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL1=3)) as MaxCalMathsI, " +
                        "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMax1)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL1=11)) as MaxCalComputer, " +
                       "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks2)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL1=47)) as MaxCivicsI, " +

                    " dbo.ExamResult.ObtainMarks1 AS Obtain1,dbo.ExamResult.ObtainMarks2 AS Obtain2,dbo.ExamResult.ObtainMarks3 AS Obtain3,dbo.ExamResult.ObtainMarks4 AS Obtain4, dbo.ExamResult.StudentID, " +
                    " dbo.ExamSetupDetail.OrderNo,ExamResult.MaxMark1 as Max1,ExamResult.MaxMark2 as Max2,ExamResult.MaxMark3 as Max3,ExamResult.MaxMark4 as Max4,ExamResult.MinMark1 as Min1,ExamResult.MinMark2 as Min2,ExamResult.MinMark3 as Min3,ExamResult.MinMark4 as Min4 " +
                    " FROM dbo.ExamResult INNER JOIN dbo.SubjectLevelOne ON dbo.ExamResult.SubjectIDL1 = dbo.SubjectLevelOne.IdL1 INNER JOIN " +
                    " dbo.ExamSetupDetail ON dbo.SubjectLevelOne.IdL1 = dbo.ExamSetupDetail.SubjectIDL1 AND dbo.SubjectLevelOne.BranchID = dbo.ExamSetupDetail.BranchID AND " +
                    " dbo.SubjectLevelOne.CompID = dbo.ExamSetupDetail.CompID " +
                    " INNER JOIN dbo.SubjectLevelTwo ON dbo.ExamResult.SubjectIDL2 = dbo.SubjectLevelTwo.IdL2 AND dbo.ExamSetupDetail.SubjectIDL2 = dbo.SubjectLevelTwo.IdL2 " +
                    " INNER JOIN dbo.ExamSetupMaster ON dbo.ExamSetupDetail.ExamSetupID = dbo.ExamSetupMaster.ExamSetupID AND " +
                    " dbo.ExamSetupDetail.BranchID = dbo.ExamSetupMaster.BranchID AND dbo.ExamSetupDetail.CompID = dbo.ExamSetupMaster.CompID AND dbo.ExamResult.ClassID = dbo.ExamSetupMaster.ClassID " +
                    " WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.ExamResult.StudentID IN (" + StudentIds + ")) " +
                    " AND (dbo.ExamResult.SessionID =" + SESSION_ID + ") AND (dbo.ExamSetupMaster.SessionID=" + SESSION_ID + ") AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND " +
                    " (dbo.ExamResult.BranchID =" + BRANCH_ID + ") AND (dbo.SubjectLevelOne.SubjectCategoryID = 1) AND (dbo.ExamSetupMaster.ExamID =" + ExamId + ")" +
                    " ORDER BY dbo.ExamResult.StudentID, dbo.ExamSetupDetail.OrderNo ";


                    SqlDetail2 = "SELECT TOP (100) PERCENT dbo.SubjectLevelOne.SubjectNameL1 AS Subject,dbo.SubjectLevelTwo.SubjectNameL2 AS SubjectD,dbo.ExamResult.ObtainMarks1 AS Obtain1,dbo.ExamResult.ObtainMarks2 AS Obtain2,dbo.ExamResult.ObtainMarks3 AS Obtain3,dbo.ExamResult.ObtainMarks4 AS Obtain4, dbo.ExamResult.StudentID, " +
                                " dbo.ExamSetupDetail.OrderNo,ExamResult.MaxMark1 as Max1,ExamResult.MaxMark2 as Max2,ExamResult.MaxMark3 as Max3,ExamResult.MaxMark4 as Max4,ExamResult.MinMark1 as Min1,ExamResult.MinMark2 as Min2,ExamResult.MinMark3 as Min3,ExamResult.MinMark4 as Min4 " +
                                " FROM dbo.ExamResult INNER JOIN dbo.SubjectLevelOne ON dbo.ExamResult.SubjectIDL1 = dbo.SubjectLevelOne.IdL1 INNER JOIN " +
                                " dbo.ExamSetupDetail ON dbo.SubjectLevelOne.IdL1 = dbo.ExamSetupDetail.SubjectIDL1 AND dbo.SubjectLevelOne.BranchID = dbo.ExamSetupDetail.BranchID AND " +
                                " dbo.SubjectLevelOne.CompID = dbo.ExamSetupDetail.CompID " +
                                " INNER JOIN dbo.SubjectLevelTwo ON dbo.ExamResult.SubjectIDL2 = dbo.SubjectLevelTwo.IdL2 AND dbo.ExamSetupDetail.SubjectIDL2 = dbo.SubjectLevelTwo.IdL2 " +
                                " INNER JOIN dbo.ExamSetupMaster ON dbo.ExamSetupDetail.ExamSetupID = dbo.ExamSetupMaster.ExamSetupID AND " +
                                " dbo.ExamSetupDetail.BranchID = dbo.ExamSetupMaster.BranchID AND dbo.ExamSetupDetail.CompID = dbo.ExamSetupMaster.CompID AND dbo.ExamResult.ClassID = dbo.ExamSetupMaster.ClassID " +
                                " WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.ExamResult.StudentID IN (" + StudentIds + ")) " +
                                " AND (dbo.ExamResult.SessionID =" + SESSION_ID + ")  AND (dbo.ExamSetupMaster.SessionID =" + SESSION_ID + ") AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND " +
                                " (dbo.ExamResult.BranchID =" + BRANCH_ID + ") AND (dbo.SubjectLevelOne.SubjectCategoryID = 2) AND (dbo.ExamSetupMaster.ExamID =" + ExamId + ")" +
                                " ORDER BY dbo.ExamResult.StudentID, dbo.ExamSetupDetail.OrderNo ";


                    SqlDetail3 = "SELECT TOP (100) PERCENT dbo.SubjectLevelOne.SubjectNameL1 AS Subject,dbo.SubjectLevelTwo.SubjectNameL2 AS SubjectD,dbo.ExamResult.ObtainMarks1 AS Obtain1,dbo.ExamResult.ObtainMarks2 AS Obtain2,dbo.ExamResult.ObtainMarks3 AS Obtain3,dbo.ExamResult.ObtainMarks4 AS Obtain4, dbo.ExamResult.StudentID, " +
                                " dbo.ExamSetupDetail.OrderNo,ExamResult.MaxMark1 as Max1,ExamResult.MaxMark2 as Max2,ExamResult.MaxMark3 as Max3,ExamResult.MaxMark4 as Max4,ExamResult.MinMark1 as Min1,ExamResult.MinMark2 as Min2,ExamResult.MinMark3 as Min3,ExamResult.MinMark4 as Min4 " +
                                " FROM dbo.ExamResult INNER JOIN dbo.SubjectLevelOne ON dbo.ExamResult.SubjectIDL1 = dbo.SubjectLevelOne.IdL1 INNER JOIN " +
                                " dbo.ExamSetupDetail ON dbo.SubjectLevelOne.IdL1 = dbo.ExamSetupDetail.SubjectIDL1 AND dbo.SubjectLevelOne.BranchID = dbo.ExamSetupDetail.BranchID AND " +
                                " dbo.SubjectLevelOne.CompID = dbo.ExamSetupDetail.CompID " +
                                " INNER JOIN dbo.SubjectLevelTwo ON dbo.ExamResult.SubjectIDL2 = dbo.SubjectLevelTwo.IdL2 AND dbo.ExamSetupDetail.SubjectIDL2 = dbo.SubjectLevelTwo.IdL2 " +
                                " INNER JOIN dbo.ExamSetupMaster ON dbo.ExamSetupDetail.ExamSetupID = dbo.ExamSetupMaster.ExamSetupID AND " +
                                " dbo.ExamSetupDetail.BranchID = dbo.ExamSetupMaster.BranchID AND dbo.ExamSetupDetail.CompID = dbo.ExamSetupMaster.CompID AND dbo.ExamResult.ClassID = dbo.ExamSetupMaster.ClassID " +
                               " WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.ExamResult.StudentID IN (" + StudentIds + ")) " +
                               " AND (dbo.ExamResult.SessionID =" + SESSION_ID + ") AND (dbo.ExamSetupMaster.SessionID =" + SESSION_ID + ") AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND " +
                               " (dbo.ExamResult.BranchID =" + BRANCH_ID + ") AND (dbo.SubjectLevelOne.SubjectCategoryID = 3) AND (dbo.ExamSetupMaster.ExamID =" + ExamId + ")" +
                               " ORDER BY dbo.ExamResult.StudentID, dbo.ExamSetupDetail.OrderNo ";
                }
                if (Order == 3)
                {
                    SqlDetail1 = "SELECT TOP (100) PERCENT dbo.SubjectLevelOne.SubjectNameL1 AS Subject, dbo.SubjectLevelTwo.SubjectNameL2 AS SubjectD,  " +
                       "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks3)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=3)) as MaxEnglishOral, " +
                        "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks3)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=4)) as MaxEnglishWritten, " +
                        "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks3)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=7)) as MaxHindiOral, " +
                        "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks3)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=8)) as MaxHindiWritten, " +
                        "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks3)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=11)) as MaxMathsOral, " +
                        "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks3)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=12)) as MaxMathsWritten, " +
                        "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks3)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=18)) as MaxScience, " +
                        "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks3)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=19)) as MaxPoem, " +
                         "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks3)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=20)) as MaxSocialStudies, " +
                         " (select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks3)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=1)) as MaxEnglishI, " +
                        " (select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks3)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=2)) as MaxEnglishII, " +
                        " (select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks3)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=5)) as MaxHindiI, " +
                        "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks3)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=6)) as MaxHindiII," +
                         "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks3)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=9)) as MaxMathsI, " +
                         "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks3)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=10)) as MaxMathsII, " +
                        "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks3)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=59)) as MaxComputerPractical, " +
                        "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks3)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=85)) as MaxComputer, " +
                         "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks3)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=81)) as MaxEnglish, " +
                         "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks3)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=82)) as MaxHindi, " +
                         "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks3)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=83)) as MaxMaths, " +
                         "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks3)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=45)) as MathsHomeScience, " +
                        "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks3)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=28)) as MaxGeneralKnowledge, " +
                         "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks3)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=23)) as MaxSanskrit, " +
                           "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks3)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=25)) as MaxGeography, " +
                         "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks3)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=24)) as MaxHistory, " +
                        "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks3)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=95 )) as MaxGenHindiI, " +
                          "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks3)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL1=53)) as MaxArtI, " +
                         "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks3)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL1=48)) as MaxSociologyI, " +

                          "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks3)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=94)) as MaxBiologyI, " +
                          "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks3)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=58)) as MaxBiologyPractical, " +
                        "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks3)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=93)) as MaxChemistryI, " +
                        "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks3)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=57)) as MaxChemistryPractical, " +
                        "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks3)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=92)) as MaxPhysicsI, " +
                        "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks3)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=56)) as MaxPhysicsPractical, " +
                        "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks3)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=106)) as MAxGKOral, " +
                        "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks3)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=96)) as MaxArt, " +

                        "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks3)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=97)) as MaxEnglishPractical, " +
                        "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks3)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=98)) as MaxHindiPractical, " +
                        "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks3)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=101)) as MaxMathsHomeSciencePractical, " +
                        "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks3)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=99)) as MaxSocialStudiesPractical, " +
                         "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks3)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=100)) as MaxSciencePractical, " +

                        "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMax1)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL1=1)) as MaxCalEnglishI, " +
                       " (select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMax1)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL1=2)) as MaxCalHindiI, " +
                        "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMax1)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL1=3)) as MaxCalMathsI, " +
                        "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMax1)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL1=11)) as MaxCalComputer, " +
                       "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks3)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL1=47)) as MaxCivicsI, " +

                      "  dbo.ExamResult.ObtainMarks1 AS Obtain1, dbo.ExamResult.ObtainMarks2 AS Obtain2,dbo.ExamResult.ObtainMarks3 AS Obtain3,dbo.ExamResult.ObtainMarks4 AS Obtain4, dbo.ExamResult.StudentID, " +
                        " dbo.ExamSetupDetail.OrderNo,ExamResult.MaxMark1 as Max1,ExamResult.MaxMark2 as Max2,ExamResult.MaxMark3 as Max3,ExamResult.MaxMark4 as Max4,ExamResult.MinMark1 as Min1,ExamResult.MinMark2 as Min2,ExamResult.MinMark3 as Min3,ExamResult.MinMark4 as Min4 " +
                        " FROM dbo.ExamResult INNER JOIN dbo.SubjectLevelOne ON dbo.ExamResult.SubjectIDL1 = dbo.SubjectLevelOne.IdL1 INNER JOIN " +
                        " dbo.ExamSetupDetail ON dbo.SubjectLevelOne.IdL1 = dbo.ExamSetupDetail.SubjectIDL1 AND dbo.SubjectLevelOne.BranchID = dbo.ExamSetupDetail.BranchID AND " +
                        " dbo.SubjectLevelOne.CompID = dbo.ExamSetupDetail.CompID " +
                        " INNER JOIN dbo.SubjectLevelTwo ON dbo.ExamResult.SubjectIDL2 = dbo.SubjectLevelTwo.IdL2 AND dbo.ExamSetupDetail.SubjectIDL2 = dbo.SubjectLevelTwo.IdL2 " +
                        "INNER JOIN dbo.ExamSetupMaster ON dbo.ExamSetupDetail.ExamSetupID = dbo.ExamSetupMaster.ExamSetupID AND " +
                        " dbo.ExamSetupDetail.BranchID = dbo.ExamSetupMaster.BranchID AND dbo.ExamSetupDetail.CompID = dbo.ExamSetupMaster.CompID AND dbo.ExamResult.ClassID = dbo.ExamSetupMaster.ClassID AND dbo.ExamResult.SessionID = dbo.ExamSetupMaster.SessionID " +
                        " WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.ExamResult.StudentID IN (" + StudentIds + ")) " +
                        " AND (dbo.ExamResult.SessionID =" + SESSION_ID + ")  AND (dbo.ExamSetupMaster.SessionID =" + SESSION_ID + ") AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND " +
                        " (dbo.ExamResult.BranchID =" + BRANCH_ID + ") AND (dbo.SubjectLevelOne.SubjectCategoryID = 1) AND (dbo.ExamSetupMaster.ExamID =" + ExamId + ")" +
                        " ORDER BY dbo.ExamResult.StudentID, dbo.ExamSetupDetail.OrderNo ";


                    SqlDetail2 = "SELECT TOP (100) PERCENT dbo.SubjectLevelOne.SubjectNameL1 AS Subject,dbo.SubjectLevelTwo.SubjectNameL2 AS SubjectD,dbo.ExamResult.ObtainMarks1 AS Obtain1,dbo.ExamResult.ObtainMarks2 AS Obtain2,dbo.ExamResult.ObtainMarks3 AS Obtain3,dbo.ExamResult.ObtainMarks4 AS Obtain4, dbo.ExamResult.StudentID, " +
                                " dbo.ExamSetupDetail.OrderNo,ExamResult.MaxMark1 as Max1,ExamResult.MaxMark2 as Max2,ExamResult.MaxMark3 as Max3,ExamResult.MaxMark4 as Max4,ExamResult.MinMark1 as Min1,ExamResult.MinMark2 as Min2,ExamResult.MinMark3 as Min3,ExamResult.MinMark4 as Min4 " +
                                " FROM dbo.ExamResult INNER JOIN dbo.SubjectLevelOne ON dbo.ExamResult.SubjectIDL1 = dbo.SubjectLevelOne.IdL1 INNER JOIN " +
                                " dbo.ExamSetupDetail ON dbo.SubjectLevelOne.IdL1 = dbo.ExamSetupDetail.SubjectIDL1 AND dbo.SubjectLevelOne.BranchID = dbo.ExamSetupDetail.BranchID AND " +
                                " dbo.SubjectLevelOne.CompID = dbo.ExamSetupDetail.CompID " +
                                " INNER JOIN dbo.SubjectLevelTwo ON dbo.ExamResult.SubjectIDL2 = dbo.SubjectLevelTwo.IdL2 AND dbo.ExamSetupDetail.SubjectIDL2 = dbo.SubjectLevelTwo.IdL2 " +
                                " INNER JOIN dbo.ExamSetupMaster ON dbo.ExamSetupDetail.ExamSetupID = dbo.ExamSetupMaster.ExamSetupID AND " +
                                " dbo.ExamSetupDetail.BranchID = dbo.ExamSetupMaster.BranchID AND dbo.ExamSetupDetail.CompID = dbo.ExamSetupMaster.CompID AND dbo.ExamResult.ClassID = dbo.ExamSetupMaster.ClassID " +
                                " WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.ExamResult.StudentID IN (" + StudentIds + ")) " +
                                " AND (dbo.ExamResult.SessionID =" + SESSION_ID + ")  AND (dbo.ExamSetupMaster.SessionID =" + SESSION_ID + ") AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND " +
                                " (dbo.ExamResult.BranchID =" + BRANCH_ID + ") AND (dbo.SubjectLevelOne.SubjectCategoryID = 2) AND (dbo.ExamSetupMaster.ExamID =" + ExamId + ")" +
                                " ORDER BY dbo.ExamResult.StudentID, dbo.ExamSetupDetail.OrderNo ";


                    SqlDetail3 = "SELECT TOP (100) PERCENT dbo.SubjectLevelOne.SubjectNameL1 AS Subject,dbo.SubjectLevelTwo.SubjectNameL2 AS SubjectD,dbo.ExamResult.ObtainMarks1 AS Obtain1,dbo.ExamResult.ObtainMarks2 AS Obtain2,dbo.ExamResult.ObtainMarks3 AS Obtain3,dbo.ExamResult.ObtainMarks4 AS Obtain4, dbo.ExamResult.StudentID, " +
                                " dbo.ExamSetupDetail.OrderNo,ExamResult.MaxMark1 as Max1,ExamResult.MaxMark2 as Max2,ExamResult.MaxMark3 as Max3,ExamResult.MaxMark4 as Max4,ExamResult.MinMark1 as Min1,ExamResult.MinMark2 as Min2,ExamResult.MinMark3 as Min3,ExamResult.MinMark4 as Min4 " +
                                " FROM dbo.ExamResult INNER JOIN dbo.SubjectLevelOne ON dbo.ExamResult.SubjectIDL1 = dbo.SubjectLevelOne.IdL1 INNER JOIN " +
                                " dbo.ExamSetupDetail ON dbo.SubjectLevelOne.IdL1 = dbo.ExamSetupDetail.SubjectIDL1 AND dbo.SubjectLevelOne.BranchID = dbo.ExamSetupDetail.BranchID AND " +
                                " dbo.SubjectLevelOne.CompID = dbo.ExamSetupDetail.CompID " +
                                " INNER JOIN dbo.SubjectLevelTwo ON dbo.ExamResult.SubjectIDL2 = dbo.SubjectLevelTwo.IdL2 AND dbo.ExamSetupDetail.SubjectIDL2 = dbo.SubjectLevelTwo.IdL2 " +
                                " INNER JOIN dbo.ExamSetupMaster ON dbo.ExamSetupDetail.ExamSetupID = dbo.ExamSetupMaster.ExamSetupID AND " +
                                " dbo.ExamSetupDetail.BranchID = dbo.ExamSetupMaster.BranchID AND dbo.ExamSetupDetail.CompID = dbo.ExamSetupMaster.CompID AND dbo.ExamResult.ClassID = dbo.ExamSetupMaster.ClassID " +
                               " WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.ExamResult.StudentID IN (" + StudentIds + ")) " +
                               " AND (dbo.ExamResult.SessionID =" + SESSION_ID + ") AND (dbo.ExamSetupMaster.SessionID =" + SESSION_ID + ") AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND " +
                               " (dbo.ExamResult.BranchID =" + BRANCH_ID + ") AND (dbo.SubjectLevelOne.SubjectCategoryID = 3) AND (dbo.ExamSetupMaster.ExamID =" + ExamId + ")" +
                               " ORDER BY dbo.ExamResult.StudentID, dbo.ExamSetupDetail.OrderNo ";
                }
                if (Order == 4)
                {
                    SqlDetail1 = "SELECT TOP (100) PERCENT dbo.SubjectLevelOne.SubjectNameL1 AS Subject, dbo.ExamResult.ObtainMarks1 AS Obtain1,dbo.ExamResult.ObtainMarks2 AS Obtain2,dbo.ExamResult.ObtainMarks3 AS Obtain3,dbo.ExamResult.ObtainMarks4 AS Obtain4, dbo.ExamResult.StudentID, " +
                                " dbo.ExamSetupDetail.OrderNo,ExamResult.MaxMark1 as Max1,ExamResult.MaxMark2 as Max2,ExamResult.MaxMark3 as Max3,ExamResult.MaxMark4 as Max4,ExamResult.MinMark1 as Min1,ExamResult.MinMark2 as Min2,ExamResult.MinMark3 as Min3,ExamResult.MinMark4 as Min4 " +
                                " FROM dbo.ExamResult INNER JOIN dbo.SubjectLevelOne ON dbo.ExamResult.SubjectIDL1 = dbo.SubjectLevelOne.IdL1 INNER JOIN " +
                                " dbo.ExamSetupDetail ON dbo.SubjectLevelOne.IdL1 = dbo.ExamSetupDetail.SubjectIDL1 AND dbo.SubjectLevelOne.BranchID = dbo.ExamSetupDetail.BranchID AND " +
                                " dbo.SubjectLevelOne.CompID = dbo.ExamSetupDetail.CompID INNER JOIN dbo.ExamSetupMaster ON dbo.ExamSetupDetail.ExamSetupID = dbo.ExamSetupMaster.ExamSetupID AND " +
                                " dbo.ExamSetupDetail.BranchID = dbo.ExamSetupMaster.BranchID AND dbo.ExamSetupDetail.CompID = dbo.ExamSetupMaster.CompID AND dbo.ExamResult.ClassID = dbo.ExamSetupMaster.ClassID AND dbo.ExamResult.SessionID = dbo.ExamSetupMaster.SessionID " +
                                " WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.ExamResult.StudentID IN (" + StudentIds + ")) " +
                                " AND (dbo.ExamResult.SessionID =" + SESSION_ID + ") AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND " +
                                " (dbo.ExamResult.BranchID =" + BRANCH_ID + ") AND (dbo.SubjectLevelOne.SubjectCategoryID = 1) AND (dbo.ExamSetupMaster.ExamID =" + ExamId + ")" +
                                " ORDER BY dbo.ExamResult.StudentID, dbo.ExamSetupDetail.OrderNo ";


                    SqlDetail2 = "SELECT TOP (100) PERCENT dbo.SubjectLevelOne.SubjectNameL1 AS Subject, dbo.ExamResult.ObtainMarks1 AS Obtain1,dbo.ExamResult.ObtainMarks2 AS Obtain2,dbo.ExamResult.ObtainMarks3 AS Obtain3,dbo.ExamResult.ObtainMarks4 AS Obtain4, dbo.ExamResult.StudentID, " +
                                " dbo.ExamSetupDetail.OrderNo FROM dbo.ExamResult INNER JOIN dbo.SubjectLevelOne ON dbo.ExamResult.SubjectIDL1 = dbo.SubjectLevelOne.IdL1 INNER JOIN " +
                                " dbo.ExamSetupDetail ON dbo.SubjectLevelOne.IdL1 = dbo.ExamSetupDetail.SubjectIDL1 AND dbo.SubjectLevelOne.BranchID = dbo.ExamSetupDetail.BranchID AND " +
                                " dbo.SubjectLevelOne.CompID = dbo.ExamSetupDetail.CompID INNER JOIN dbo.ExamSetupMaster ON dbo.ExamSetupDetail.ExamSetupID = dbo.ExamSetupMaster.ExamSetupID AND " +
                                " dbo.ExamSetupDetail.BranchID = dbo.ExamSetupMaster.BranchID AND dbo.ExamSetupDetail.CompID = dbo.ExamSetupMaster.CompID AND dbo.ExamResult.ClassID = dbo.ExamSetupMaster.ClassID AND dbo.ExamResult.SessionID = dbo.ExamSetupMaster.SessionID " +
                                " WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.ExamResult.StudentID IN (" + StudentIds + ")) " +
                                " AND (dbo.ExamResult.SessionID =" + SESSION_ID + ") AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND " +
                                " (dbo.ExamResult.BranchID =" + BRANCH_ID + ") AND (dbo.SubjectLevelOne.SubjectCategoryID = 2) AND (dbo.ExamSetupMaster.ExamID =" + ExamId + ")" +
                                " ORDER BY dbo.ExamResult.StudentID, dbo.ExamSetupDetail.OrderNo ";


                    SqlDetail3 = "SELECT TOP (100) PERCENT dbo.SubjectLevelOne.SubjectNameL1 AS Subject, dbo.ExamResult.ObtainMarks1 AS Obtain1,dbo.ExamResult.ObtainMarks2 AS Obtain2,dbo.ExamResult.ObtainMarks3 AS Obtain3,dbo.ExamResult.ObtainMarks4 AS Obtain4, dbo.ExamResult.StudentID, " +
                               " dbo.ExamSetupDetail.OrderNo FROM dbo.ExamResult INNER JOIN dbo.SubjectLevelOne ON dbo.ExamResult.SubjectIDL1 = dbo.SubjectLevelOne.IdL1 INNER JOIN " +
                               " dbo.ExamSetupDetail ON dbo.SubjectLevelOne.IdL1 = dbo.ExamSetupDetail.SubjectIDL1 AND dbo.SubjectLevelOne.BranchID = dbo.ExamSetupDetail.BranchID AND " +
                               " dbo.SubjectLevelOne.CompID = dbo.ExamSetupDetail.CompID INNER JOIN dbo.ExamSetupMaster ON dbo.ExamSetupDetail.ExamSetupID = dbo.ExamSetupMaster.ExamSetupID AND " +
                               " dbo.ExamSetupDetail.BranchID = dbo.ExamSetupMaster.BranchID AND dbo.ExamSetupDetail.CompID = dbo.ExamSetupMaster.CompID AND dbo.ExamResult.ClassID = dbo.ExamSetupMaster.ClassID AND dbo.ExamResult.SessionID = dbo.ExamSetupMaster.SessionID " +
                               " WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.ExamResult.StudentID IN (" + StudentIds + ")) " +
                               " AND (dbo.ExamResult.SessionID =" + SESSION_ID + ") AND (dbo.ExamSetupMaster.SessionID =" + SESSION_ID + ") AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND " +
                               " (dbo.ExamResult.BranchID =" + BRANCH_ID + ") AND (dbo.SubjectLevelOne.SubjectCategoryID = 3) AND (dbo.ExamSetupMaster.ExamID =" + ExamId + ")" +
                               " ORDER BY dbo.ExamResult.StudentID, dbo.ExamSetupDetail.OrderNo ";
                }
            }
            #endregion



            
        }

        public void ReturnStMarkExamMarksheetSql()
        {
            #region for stmark  new logic for marksheet Get marks for grpah
            if (COMPANY_ID == 2)
            {
                if (Order == 1)
                {
                    SqlDetail1 = "SELECT TOP (100) PERCENT dbo.SubjectLevelOne.SubjectNameL1 AS Subject," +
                    "(select  max(TRY_CONVERT(decimal(18,2), dbo.ExamResult.ObtainMarks1)) FROM  dbo.StudentSession With (NoLock) INNER JOIN  dbo.ExamResult With (NoLock) ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL1=1 OR dbo.ExamResult.SubjectIDL1 = 1080 OR dbo.ExamResult.SubjectIDL1=1038 )) as MaxEnglish, " +
                        "(select  max(TRY_CONVERT(decimal(18,2), dbo.ExamResult.ObtainMarks1)) FROM  dbo.StudentSession With (NoLock) INNER JOIN  dbo.ExamResult With (NoLock) ON  dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL1=2 OR dbo.ExamResult.SubjectIDL1 = 1081 OR dbo.ExamResult.SubjectIDL1=1039 )) as MaxEnglishLiterature, " +
                        "(select  max(TRY_CONVERT(decimal(18,2), dbo.ExamResult.ObtainMarks1)) FROM  dbo.StudentSession With (NoLock) INNER JOIN  dbo.ExamResult With (NoLock) ON  dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL1=3 OR dbo.ExamResult.SubjectIDL1 = 1082 OR dbo.ExamResult.SubjectIDL1=1040 )) as MaxHindi, " +
                        "(select  max(TRY_CONVERT(decimal(18,2), dbo.ExamResult.ObtainMarks1)) FROM  dbo.StudentSession With (NoLock) INNER JOIN  dbo.ExamResult With (NoLock) ON  dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL1=4  OR dbo.ExamResult.SubjectIDL1 = 1083 OR dbo.ExamResult.SubjectIDL1=1041 )) as MaxMaths, " +
                        "(select  max(TRY_CONVERT(decimal(18,2), dbo.ExamResult.ObtainMarks1)) FROM  dbo.StudentSession With (NoLock) INNER JOIN  dbo.ExamResult With (NoLock) ON  dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL1=5 OR dbo.ExamResult.SubjectIDL1 = 1084 OR dbo.ExamResult.SubjectIDL1=1042)) as MaxMathsWritten, " +
                        "(select  max(TRY_CONVERT(decimal(18,2), dbo.ExamResult.ObtainMarks1)) FROM  dbo.StudentSession With (NoLock) INNER JOIN  dbo.ExamResult With (NoLock) ON  dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL1=6 OR dbo.ExamResult.SubjectIDL1 = 1085 OR dbo.ExamResult.SubjectIDL1=1043)) as MaxMathsOral, " +
                       "(select  max(TRY_CONVERT(decimal(18,2), dbo.ExamResult.ObtainMarks1)) FROM  dbo.StudentSession With (NoLock) INNER JOIN  dbo.ExamResult With (NoLock) ON  dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                       " AND (dbo.ExamResult.SubjectIDL1=7 OR dbo.ExamResult.SubjectIDL1 = 1086 OR dbo.ExamResult.SubjectIDL1=1044)) as MAxDictWriting, " +
                         "(select  max(TRY_CONVERT(decimal(18,2), dbo.ExamResult.ObtainMarks1)) FROM  dbo.StudentSession With (NoLock) INNER JOIN  dbo.ExamResult With (NoLock) ON  dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                       " AND (dbo.ExamResult.SubjectIDL1=8  OR dbo.ExamResult.SubjectIDL1 = 1087 OR dbo.ExamResult.SubjectIDL1=1045)) as MaxGK, " +
                         "(select  max(TRY_CONVERT(decimal(18,2), dbo.ExamResult.ObtainMarks1)) FROM  dbo.StudentSession With (NoLock) INNER JOIN  dbo.ExamResult With (NoLock) ON  dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                       " AND (dbo.ExamResult.SubjectIDL1=9 OR dbo.ExamResult.SubjectIDL1 = 1088 OR dbo.ExamResult.SubjectIDL1=1046)) as MaxEnglishReading, " +
                        "(select  max(TRY_CONVERT(decimal(18,2), dbo.ExamResult.ObtainMarks1)) FROM  dbo.StudentSession With (NoLock) INNER JOIN  dbo.ExamResult With (NoLock) ON  dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                       " AND (dbo.ExamResult.SubjectIDL1=10 OR dbo.ExamResult.SubjectIDL1 = 1089 OR dbo.ExamResult.SubjectIDL1=1047)) as MaxEnglishRecitation , " +
                         "(select  max(TRY_CONVERT(decimal(18,2), dbo.ExamResult.ObtainMarks1)) FROM  dbo.StudentSession With (NoLock) INNER JOIN  dbo.ExamResult With (NoLock) ON  dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                       " AND (dbo.ExamResult.SubjectIDL1= 11 OR dbo.ExamResult.SubjectIDL1 = 1090 OR dbo.ExamResult.SubjectIDL1=1048)) as MaxReadRecitation, " +
                       "(select  max(TRY_CONVERT(decimal(18,2), dbo.ExamResult.ObtainMarks1)) FROM  dbo.StudentSession With (NoLock) INNER JOIN  dbo.ExamResult With (NoLock) ON  dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                       " AND (dbo.ExamResult.SubjectIDL1=12 OR dbo.ExamResult.SubjectIDL1 = 1091 OR dbo.ExamResult.SubjectIDL1=1049)) as MaxStory , " +
                         "(select  max(TRY_CONVERT(decimal(18,2), dbo.ExamResult.ObtainMarks1)) FROM  dbo.StudentSession With (NoLock) INNER JOIN  dbo.ExamResult With (NoLock) ON  dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                       " AND (dbo.ExamResult.SubjectIDL1= 13  OR dbo.ExamResult.SubjectIDL1 = 1092 OR dbo.ExamResult.SubjectIDL1=1050)) as MaxEnglishDictationSpelling, " +
                        "(select  max(TRY_CONVERT(decimal(18,2), dbo.ExamResult.ObtainMarks1)) FROM  dbo.StudentSession With (NoLock) INNER JOIN  dbo.ExamResult With (NoLock) ON  dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                       " AND (dbo.ExamResult.SubjectIDL1=14 OR dbo.ExamResult.SubjectIDL1 = 1093 OR dbo.ExamResult.SubjectIDL1=1051)) as MaxHandWriting , " +
                         "(select  max(TRY_CONVERT(decimal(18,2), dbo.ExamResult.ObtainMarks1)) FROM  dbo.StudentSession With (NoLock) INNER JOIN  dbo.ExamResult With (NoLock) ON  dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                       " AND (dbo.ExamResult.SubjectIDL1= 15 OR dbo.ExamResult.SubjectIDL1 = 1094 OR dbo.ExamResult.SubjectIDL1=1052)) as MaxHistoryCivics, " +
                        "(select  max(TRY_CONVERT(decimal(18,2), dbo.ExamResult.ObtainMarks1)) FROM  dbo.StudentSession With (NoLock) INNER JOIN  dbo.ExamResult With (NoLock) ON  dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                       " AND (dbo.ExamResult.SubjectIDL1=16 OR dbo.ExamResult.SubjectIDL1 = 1095 OR dbo.ExamResult.SubjectIDL1=1053)) as MaxGeography , " +
                         "(select  max(TRY_CONVERT(decimal(18,2), dbo.ExamResult.ObtainMarks1)) FROM  dbo.StudentSession With (NoLock) INNER JOIN  dbo.ExamResult With (NoLock) ON  dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                       " AND (dbo.ExamResult.SubjectIDL1= 17 OR dbo.ExamResult.SubjectIDL1 = 1096 OR dbo.ExamResult.SubjectIDL1=1054)) as MaxSocialStudies, " +
                        "(select  max(TRY_CONVERT(decimal(18,2), dbo.ExamResult.ObtainMarks1)) FROM  dbo.StudentSession With (NoLock) INNER JOIN  dbo.ExamResult With (NoLock) ON  dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                       " AND (dbo.ExamResult.SubjectIDL1=18  OR dbo.ExamResult.SubjectIDL1 = 1097 OR dbo.ExamResult.SubjectIDL1=1055) ) as MaxMoralScience , " +
                         "(select  max(TRY_CONVERT(decimal(18,2), dbo.ExamResult.ObtainMarks1)) FROM  dbo.StudentSession With (NoLock) INNER JOIN  dbo.ExamResult With (NoLock) ON  dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                       " AND (dbo.ExamResult.SubjectIDL1= 19 OR dbo.ExamResult.SubjectIDL1 = 1098 OR dbo.ExamResult.SubjectIDL1=1056)) as MaxPhysicsI, " +
                        "(select  max(TRY_CONVERT(decimal(18,2), dbo.ExamResult.ObtainMarks1)) FROM  dbo.StudentSession With (NoLock) INNER JOIN  dbo.ExamResult With (NoLock) ON  dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                       " AND (dbo.ExamResult.SubjectIDL1= 20 OR dbo.ExamResult.SubjectIDL1 = 1099 OR dbo.ExamResult.SubjectIDL1=1057)) as MaxChemistryI , " +
                       "(select  max(TRY_CONVERT(decimal(18,2), dbo.ExamResult.ObtainMarks1)) FROM  dbo.StudentSession With (NoLock) INNER JOIN  dbo.ExamResult With (NoLock) ON  dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                       " AND (dbo.ExamResult.SubjectIDL1= 21 OR dbo.ExamResult.SubjectIDL1 = 1100 OR dbo.ExamResult.SubjectIDL1=1058)) as MaxBiologyI , " +
                        "(select  max(TRY_CONVERT(decimal(18,2), dbo.ExamResult.ObtainMarks1)) FROM  dbo.StudentSession With (NoLock) INNER JOIN  dbo.ExamResult With (NoLock) ON  dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                       " AND (dbo.ExamResult.SubjectIDL1= 22 OR dbo.ExamResult.SubjectIDL1 = 1111 OR dbo.ExamResult.SubjectIDL1=1058)) as MaxPhysicalEduc, " +
                        "(select  max(TRY_CONVERT(decimal(18,2), dbo.ExamResult.ObtainMarks1)) FROM  dbo.StudentSession With (NoLock) INNER JOIN  dbo.ExamResult With (NoLock) ON  dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                       " AND (dbo.ExamResult.SubjectIDL1= 1028 OR dbo.ExamResult.SubjectIDL1 = 1112 OR dbo.ExamResult.SubjectIDL1=1070)) as MaxComputer, " +
                       "(select  max(TRY_CONVERT(decimal(18,2), dbo.ExamResult.ObtainMarks1)) FROM  dbo.StudentSession With (NoLock) INNER JOIN  dbo.ExamResult With (NoLock) ON  dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                       " AND (dbo.ExamResult.SubjectIDL1= 1029 OR dbo.ExamResult.SubjectIDL1 = 1113 OR dbo.ExamResult.SubjectIDL1=1069)) as MaxComputerPhyEdu, " +
                       "(select  max(TRY_CONVERT(decimal(18,2), dbo.ExamResult.ObtainMarks1)) FROM  dbo.StudentSession With (NoLock) INNER JOIN  dbo.ExamResult With (NoLock) ON  dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                       " AND (dbo.ExamResult.SubjectIDL1= 1030 OR dbo.ExamResult.SubjectIDL1 = 1114 OR dbo.ExamResult.SubjectIDL1=1072)) as MaxAccounts, " +
                         "(select  max(TRY_CONVERT(decimal(18,2), dbo.ExamResult.ObtainMarks1)) FROM  dbo.StudentSession With (NoLock) INNER JOIN  dbo.ExamResult With (NoLock) ON  dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                       " AND (dbo.ExamResult.SubjectIDL1= 1031 OR dbo.ExamResult.SubjectIDL1 = 1115 OR dbo.ExamResult.SubjectIDL1=1073)) as MaxCommerce, " +
                         "(select  max(TRY_CONVERT(decimal(18,2), dbo.ExamResult.ObtainMarks1)) FROM  dbo.StudentSession With (NoLock) INNER JOIN  dbo.ExamResult With (NoLock) ON  dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                       " AND (dbo.ExamResult.SubjectIDL1= 1032 OR dbo.ExamResult.SubjectIDL1 = 1116 OR dbo.ExamResult.SubjectIDL1=1074)) as MaxMathsBusiness , " +
                       "(select  max(TRY_CONVERT(decimal(18,2), dbo.ExamResult.ObtainMarks1)) FROM  dbo.StudentSession With (NoLock) INNER JOIN  dbo.ExamResult With (NoLock) ON  dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                       " AND (dbo.ExamResult.SubjectIDL1= 1035 OR dbo.ExamResult.SubjectIDL1 = 1119 OR dbo.ExamResult.SubjectIDL1=1077)) as MaxSanskrit, " +
                       "(select  max(TRY_CONVERT(decimal(18,2), dbo.ExamResult.ObtainMarks1)) FROM  dbo.StudentSession With (NoLock) INNER JOIN  dbo.ExamResult With (NoLock) ON  dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                       " AND (dbo.ExamResult.SubjectIDL1= 1037 OR dbo.ExamResult.SubjectIDL1 = 1121 OR dbo.ExamResult.SubjectIDL1=1079)) as MAxComputerArt, " +
                        "(select  max(TRY_CONVERT(decimal(18,2), dbo.ExamResult.ObtainMarks1)) FROM  dbo.StudentSession With (NoLock) INNER JOIN  dbo.ExamResult With (NoLock) ON  dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                       " AND (dbo.ExamResult.SubjectIDL1= 1132)) as MAxComputerArt, " +
                         "(select  max(TRY_CONVERT(decimal(18,2), dbo.ExamResult.ObtainMarks1)) FROM  dbo.StudentSession With (NoLock) INNER JOIN  dbo.ExamResult With (NoLock) ON  dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                       " AND (dbo.ExamResult.SubjectIDL1= 1132 or dbo.ExamResult.SubjectIDL1 = 1135 or dbo.ExamResult.SubjectIDL1 = 1136)) as MAxEVS, " +
                        // "(select  max(TRY_CONVERT(decimal(18,2), dbo.ExamResult.ObtainMarks1)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult With (NoLock) ON  dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        //" AND (dbo.ExamResult.SubjectIDL1= 1131 ) as MAxCompArtPhyEdu, " +


                     " dbo.ExamResult.ObtainMarks1 AS Obtain1,dbo.ExamResult.ObtainMarks2 AS Obtain2,dbo.ExamResult.ObtainMarks3 AS Obtain3,dbo.ExamResult.ObtainMarks4 AS Obtain4, dbo.ExamResult.ObtainMax1,dbo.ExamResult.ObtainMax2,dbo.ExamResult.ObtainMax3,dbo.ExamResult.ObtainMax4, dbo.ExamResult.StudentID, " +
                                " dbo.ExamSetupDetail.OrderNo,dbo.ExamResult.Grade AS Grade1, dbo.ExamResult.ObtainGrade2 AS Grade2,dbo.ExamResult.ObtainGrade3 AS Grade3,dbo.ExamResult.ObtainGrade4 AS Grade4, ExamResult.MaxMark1 as Max1,ExamResult.MaxMark2 as Max2,ExamResult.MaxMark3 as Max3,ExamResult.MaxMark4 as Max4,ExamResult.MinMark1 as Min1,ExamResult.MinMark2 as Min2,ExamResult.MinMark3 as Min3,ExamResult.MinMark4 as Min4 " +
                                " FROM dbo.ExamResult With (NoLock) INNER JOIN dbo.SubjectLevelOne With (NoLock) ON dbo.ExamResult.SubjectIDL1 = dbo.SubjectLevelOne.IdL1 INNER JOIN " +
                                " dbo.ExamSetupDetail With (NoLock) ON dbo.SubjectLevelOne.IdL1 = dbo.ExamSetupDetail.SubjectIDL1 AND dbo.SubjectLevelOne.BranchID = dbo.ExamSetupDetail.BranchID AND " +
                                " dbo.SubjectLevelOne.CompID = dbo.ExamSetupDetail.CompID INNER JOIN dbo.ExamSetupMaster With (NoLock) ON dbo.ExamSetupDetail.ExamSetupID = dbo.ExamSetupMaster.ExamSetupID AND " +
                                " dbo.ExamSetupDetail.BranchID = dbo.ExamSetupMaster.BranchID AND dbo.ExamSetupDetail.CompID = dbo.ExamSetupMaster.CompID AND dbo.ExamResult.ClassID = dbo.ExamSetupMaster.ClassID AND dbo.ExamResult.SessionID = dbo.ExamSetupMaster.SessionID " +
                                " WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.ExamResult.StudentID IN (" + StudentIds + ")) " +
                                " AND (dbo.ExamResult.SessionID =" + SESSION_ID + ") AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND " +
                                " (dbo.ExamResult.BranchID =" + BRANCH_ID + ") AND (dbo.SubjectLevelOne.SubjectCategoryID = 1) AND (dbo.ExamSetupMaster.ExamID =" + ExamId + ")" +
                                " ORDER BY dbo.ExamResult.StudentID, dbo.ExamSetupDetail.OrderNo ";


                    SqlDetail2 = "SELECT TOP (100) PERCENT dbo.SubjectLevelOne.SubjectNameL1 AS Subject, dbo.ExamResult.ObtainMarks1 AS Obtain1,dbo.ExamResult.ObtainMarks2 AS Obtain2,dbo.ExamResult.ObtainMarks3 AS Obtain3,dbo.ExamResult.ObtainMarks4 AS Obtain4, dbo.ExamResult.StudentID, " +
                                " dbo.ExamSetupDetail.OrderNo FROM dbo.ExamResult INNER JOIN dbo.SubjectLevelOne ON dbo.ExamResult.SubjectIDL1 = dbo.SubjectLevelOne.IdL1 INNER JOIN " +
                                " dbo.ExamSetupDetail ON dbo.SubjectLevelOne.IdL1 = dbo.ExamSetupDetail.SubjectIDL1 AND dbo.SubjectLevelOne.BranchID = dbo.ExamSetupDetail.BranchID AND " +
                                " dbo.SubjectLevelOne.CompID = dbo.ExamSetupDetail.CompID INNER JOIN dbo.ExamSetupMaster ON dbo.ExamSetupDetail.ExamSetupID = dbo.ExamSetupMaster.ExamSetupID AND " +
                                " dbo.ExamSetupDetail.BranchID = dbo.ExamSetupMaster.BranchID AND dbo.ExamSetupDetail.CompID = dbo.ExamSetupMaster.CompID AND dbo.ExamResult.ClassID = dbo.ExamSetupMaster.ClassID AND dbo.ExamResult.SessionID = dbo.ExamSetupMaster.SessionID " +
                                " WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.ExamResult.StudentID IN (" + StudentIds + ")) " +
                                " AND (dbo.ExamResult.SessionID =" + SESSION_ID + ") AND (dbo.ExamSetupMaster.SessionID =" + SESSION_ID + ") AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND " +
                                " (dbo.ExamResult.BranchID =" + BRANCH_ID + ") AND (dbo.SubjectLevelOne.SubjectCategoryID = 2) AND (dbo.ExamSetupMaster.ExamID =" + ExamId + ")" +
                                " ORDER BY dbo.ExamResult.StudentID, dbo.ExamSetupDetail.OrderNo ";


                    SqlDetail3 = "SELECT TOP (100) PERCENT dbo.SubjectLevelOne.SubjectNameL1 AS Subject, dbo.ExamResult.ObtainMarks1 AS Obtain1,dbo.ExamResult.ObtainMarks2 AS Obtain2,dbo.ExamResult.ObtainMarks3 AS Obtain3,dbo.ExamResult.ObtainMarks4 AS Obtain4, dbo.ExamResult.StudentID, " +
                               " dbo.ExamSetupDetail.OrderNo FROM dbo.ExamResult INNER JOIN dbo.SubjectLevelOne ON dbo.ExamResult.SubjectIDL1 = dbo.SubjectLevelOne.IdL1 INNER JOIN " +
                               " dbo.ExamSetupDetail ON dbo.SubjectLevelOne.IdL1 = dbo.ExamSetupDetail.SubjectIDL1 AND dbo.SubjectLevelOne.BranchID = dbo.ExamSetupDetail.BranchID AND " +
                               " dbo.SubjectLevelOne.CompID = dbo.ExamSetupDetail.CompID INNER JOIN dbo.ExamSetupMaster ON dbo.ExamSetupDetail.ExamSetupID = dbo.ExamSetupMaster.ExamSetupID AND " +
                               " dbo.ExamSetupDetail.BranchID = dbo.ExamSetupMaster.BranchID AND dbo.ExamSetupDetail.CompID = dbo.ExamSetupMaster.CompID AND dbo.ExamResult.ClassID = dbo.ExamSetupMaster.ClassID AND dbo.ExamResult.SessionID = dbo.ExamSetupMaster.SessionID " +
                               " WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.ExamResult.StudentID IN (" + StudentIds + ")) " +
                               " AND (dbo.ExamResult.SessionID =" + SESSION_ID + ") AND (dbo.ExamSetupMaster.SessionID =" + SESSION_ID + ") AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND " +
                               " (dbo.ExamResult.BranchID =" + BRANCH_ID + ") AND (dbo.SubjectLevelOne.SubjectCategoryID = 3) AND (dbo.ExamSetupMaster.ExamID =" + ExamId + ")" +
                               " ORDER BY dbo.ExamResult.StudentID, dbo.ExamSetupDetail.OrderNo ";

                }
                if (Order == 2)
                {
                    SqlDetail1 = "SELECT TOP (100) PERCENT dbo.SubjectLevelOne.SubjectNameL1 AS Subject," +
                   "(select  max(TRY_CONVERT(decimal(18,2), dbo.ExamResult.ObtainMarks2)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL1=1 OR dbo.ExamResult.SubjectIDL1 = 1080 OR dbo.ExamResult.SubjectIDL1=1038 )) as MaxEnglish, " +
                        "(select  max(TRY_CONVERT(decimal(18,2), dbo.ExamResult.ObtainMarks2)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL1=2 OR dbo.ExamResult.SubjectIDL1 = 1081 OR dbo.ExamResult.SubjectIDL1=1039 )) as MaxEnglishLiterature, " +
                        "(select  max(TRY_CONVERT(decimal(18,2), dbo.ExamResult.ObtainMarks2)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL1=3 OR dbo.ExamResult.SubjectIDL1 = 1082 OR dbo.ExamResult.SubjectIDL1=1040 )) as MaxHindi, " +
                        "(select  max(TRY_CONVERT(decimal(18,2), dbo.ExamResult.ObtainMarks2)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL1=4  OR dbo.ExamResult.SubjectIDL1 = 1083 OR dbo.ExamResult.SubjectIDL1=1041 )) as MaxMaths, " +
                        "(select  max(TRY_CONVERT(decimal(18,2), dbo.ExamResult.ObtainMarks2)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL1=5 OR dbo.ExamResult.SubjectIDL1 = 1084 OR dbo.ExamResult.SubjectIDL1=1042)) as MaxMathsWritten, " +
                        "(select  max(TRY_CONVERT(decimal(18,2), dbo.ExamResult.ObtainMarks2)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL1=6 OR dbo.ExamResult.SubjectIDL1 = 1085 OR dbo.ExamResult.SubjectIDL1=1043)) as MaxMathsOral, " +
                       "(select  max(TRY_CONVERT(decimal(18,2), dbo.ExamResult.ObtainMarks2)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                       " AND (dbo.ExamResult.SubjectIDL1=7 OR dbo.ExamResult.SubjectIDL1 = 1086 OR dbo.ExamResult.SubjectIDL1=1044)) as MAxDictWriting, " +
                         "(select  max(TRY_CONVERT(decimal(18,2), dbo.ExamResult.ObtainMarks2)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                       " AND (dbo.ExamResult.SubjectIDL1=8  OR dbo.ExamResult.SubjectIDL1 = 1087 OR dbo.ExamResult.SubjectIDL1=1045)) as MaxGK, " +
                         "(select  max(TRY_CONVERT(decimal(18,2), dbo.ExamResult.ObtainMarks2)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                       " AND (dbo.ExamResult.SubjectIDL1=9 OR dbo.ExamResult.SubjectIDL1 = 1088 OR dbo.ExamResult.SubjectIDL1=1046)) as MaxEnglishReading, " +
                        "(select  max(TRY_CONVERT(decimal(18,2), dbo.ExamResult.ObtainMarks2)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                       " AND (dbo.ExamResult.SubjectIDL1=10 OR dbo.ExamResult.SubjectIDL1 = 1089 OR dbo.ExamResult.SubjectIDL1=1047)) as MaxEnglishRecitation , " +
                         "(select  max(TRY_CONVERT(decimal(18,2), dbo.ExamResult.ObtainMarks2)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                       " AND (dbo.ExamResult.SubjectIDL1= 11 OR dbo.ExamResult.SubjectIDL1 = 1090 OR dbo.ExamResult.SubjectIDL1=1048)) as MaxReadRecitation, " +
                       "(select  max(TRY_CONVERT(decimal(18,2), dbo.ExamResult.ObtainMarks2)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                       " AND (dbo.ExamResult.SubjectIDL1=12 OR dbo.ExamResult.SubjectIDL1 = 1091 OR dbo.ExamResult.SubjectIDL1=1049)) as MaxStory , " +
                         "(select  max(TRY_CONVERT(decimal(18,2), dbo.ExamResult.ObtainMarks2)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                       " AND (dbo.ExamResult.SubjectIDL1= 13  OR dbo.ExamResult.SubjectIDL1 = 1092 OR dbo.ExamResult.SubjectIDL1=1050)) as MaxEnglishDictationSpelling, " +
                        "(select  max(TRY_CONVERT(decimal(18,2), dbo.ExamResult.ObtainMarks2)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                       " AND (dbo.ExamResult.SubjectIDL1=14 OR dbo.ExamResult.SubjectIDL1 = 1093 OR dbo.ExamResult.SubjectIDL1=1051)) as MaxHandWriting , " +
                         "(select  max(TRY_CONVERT(decimal(18,2), dbo.ExamResult.ObtainMarks2)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                       " AND (dbo.ExamResult.SubjectIDL1= 15 OR dbo.ExamResult.SubjectIDL1 = 1094 OR dbo.ExamResult.SubjectIDL1=1052)) as MaxHistoryCivics, " +
                        "(select  max(TRY_CONVERT(decimal(18,2), dbo.ExamResult.ObtainMarks2)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                       " AND (dbo.ExamResult.SubjectIDL1=16 OR dbo.ExamResult.SubjectIDL1 = 1095 OR dbo.ExamResult.SubjectIDL1=1053)) as MaxGeography , " +
                         "(select  max(TRY_CONVERT(decimal(18,2), dbo.ExamResult.ObtainMarks2)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                       " AND (dbo.ExamResult.SubjectIDL1= 17 OR dbo.ExamResult.SubjectIDL1 = 1096 OR dbo.ExamResult.SubjectIDL1=1054)) as MaxSocialStudies, " +
                        "(select  max(TRY_CONVERT(decimal(18,2), dbo.ExamResult.ObtainMarks2)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                       " AND (dbo.ExamResult.SubjectIDL1=18  OR dbo.ExamResult.SubjectIDL1 = 1097 OR dbo.ExamResult.SubjectIDL1=1055) ) as MaxMoralScience , " +
                         "(select  max(TRY_CONVERT(decimal(18,2), dbo.ExamResult.ObtainMarks2)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                       " AND (dbo.ExamResult.SubjectIDL1= 19 OR dbo.ExamResult.SubjectIDL1 = 1098 OR dbo.ExamResult.SubjectIDL1=1056)) as MaxPhysicsI, " +
                        "(select  max(TRY_CONVERT(decimal(18,2), dbo.ExamResult.ObtainMarks2)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                       " AND (dbo.ExamResult.SubjectIDL1= 20 OR dbo.ExamResult.SubjectIDL1 = 1099 OR dbo.ExamResult.SubjectIDL1=1057)) as MaxChemistryI , " +
                       "(select  max(TRY_CONVERT(decimal(18,2), dbo.ExamResult.ObtainMarks2)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                       " AND (dbo.ExamResult.SubjectIDL1= 21 OR dbo.ExamResult.SubjectIDL1 = 1100 OR dbo.ExamResult.SubjectIDL1=1058)) as MaxBiologyI , " +
                        "(select  max(TRY_CONVERT(decimal(18,2), dbo.ExamResult.ObtainMarks2)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                       " AND (dbo.ExamResult.SubjectIDL1= 22 OR dbo.ExamResult.SubjectIDL1 = 1111 OR dbo.ExamResult.SubjectIDL1=1058)) as MaxPhysicalEduc, " +
                        "(select  max(TRY_CONVERT(decimal(18,2), dbo.ExamResult.ObtainMarks2)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                       " AND (dbo.ExamResult.SubjectIDL1= 1028 OR dbo.ExamResult.SubjectIDL1 = 1112 OR dbo.ExamResult.SubjectIDL1=1070)) as MaxComputer, " +
                       "(select  max(TRY_CONVERT(decimal(18,2), dbo.ExamResult.ObtainMarks2)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                       " AND (dbo.ExamResult.SubjectIDL1= 1029 OR dbo.ExamResult.SubjectIDL1 = 1113 OR dbo.ExamResult.SubjectIDL1=1069)) as MaxComputerPhyEdu, " +
                       "(select  max(TRY_CONVERT(decimal(18,2), dbo.ExamResult.ObtainMarks2)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                       " AND (dbo.ExamResult.SubjectIDL1= 1030 OR dbo.ExamResult.SubjectIDL1 = 1114 OR dbo.ExamResult.SubjectIDL1=1072)) as MaxAccounts, " +
                         "(select  max(TRY_CONVERT(decimal(18,2), dbo.ExamResult.ObtainMarks2)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                       " AND (dbo.ExamResult.SubjectIDL1= 1031 OR dbo.ExamResult.SubjectIDL1 = 1115 OR dbo.ExamResult.SubjectIDL1=1073)) as MaxCommerce, " +
                         "(select  max(TRY_CONVERT(decimal(18,2), dbo.ExamResult.ObtainMarks2)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                       " AND (dbo.ExamResult.SubjectIDL1= 1032 OR dbo.ExamResult.SubjectIDL1 = 1116 OR dbo.ExamResult.SubjectIDL1=1074)) as MaxMathsBusiness , " +
                       "(select  max(TRY_CONVERT(decimal(18,2), dbo.ExamResult.ObtainMarks2)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                       " AND (dbo.ExamResult.SubjectIDL1= 1035 OR dbo.ExamResult.SubjectIDL1 = 1119 OR dbo.ExamResult.SubjectIDL1=1077)) as MaxSanskrit, " +
                       "(select  max(TRY_CONVERT(decimal(18,2), dbo.ExamResult.ObtainMarks2)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                       " AND (dbo.ExamResult.SubjectIDL1= 1037 OR dbo.ExamResult.SubjectIDL1 = 1121 OR dbo.ExamResult.SubjectIDL1=1079)) as MAxComputerArt, " +
                       "(select  max(TRY_CONVERT(decimal(18,2), dbo.ExamResult.ObtainMarks2)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                       " AND (dbo.ExamResult.SubjectIDL1= 1132 or dbo.ExamResult.SubjectIDL1 = 1135 or dbo.ExamResult.SubjectIDL1 = 1136 )) as MAxEVS, " +



                     " dbo.ExamResult.ObtainMarks1 AS Obtain1,dbo.ExamResult.ObtainMarks2 AS Obtain2,dbo.ExamResult.ObtainMarks3 AS Obtain3,dbo.ExamResult.ObtainMarks4 AS Obtain4, dbo.ExamResult.StudentID, " +
                                " dbo.ExamSetupDetail.OrderNo,dbo.ExamResult.Grade AS Grade1, dbo.ExamResult.ObtainGrade2 AS Grade2,dbo.ExamResult.ObtainGrade3 AS Grade3,dbo.ExamResult.ObtainGrade4 AS Grade4, ExamResult.MaxMark1 as Max1,ExamResult.MaxMark2 as Max2,ExamResult.MaxMark3 as Max3,ExamResult.MaxMark4 as Max4,ExamResult.MinMark1 as Min1,ExamResult.MinMark2 as Min2,ExamResult.MinMark3 as Min3,ExamResult.MinMark4 as Min4 " +
                                " FROM dbo.ExamResult INNER JOIN dbo.SubjectLevelOne ON dbo.ExamResult.SubjectIDL1 = dbo.SubjectLevelOne.IdL1 INNER JOIN " +
                                " dbo.ExamSetupDetail ON dbo.SubjectLevelOne.IdL1 = dbo.ExamSetupDetail.SubjectIDL1 AND dbo.SubjectLevelOne.BranchID = dbo.ExamSetupDetail.BranchID AND " +
                                " dbo.SubjectLevelOne.CompID = dbo.ExamSetupDetail.CompID INNER JOIN dbo.ExamSetupMaster ON dbo.ExamSetupDetail.ExamSetupID = dbo.ExamSetupMaster.ExamSetupID AND " +
                                " dbo.ExamSetupDetail.BranchID = dbo.ExamSetupMaster.BranchID AND dbo.ExamSetupDetail.CompID = dbo.ExamSetupMaster.CompID AND dbo.ExamResult.ClassID = dbo.ExamSetupMaster.ClassID AND dbo.ExamResult.SessionID = dbo.ExamSetupMaster.SessionID " +
                                " WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.ExamResult.StudentID IN (" + StudentIds + ")) " +
                                " AND (dbo.ExamResult.SessionID =" + SESSION_ID + ") AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND " +
                                " (dbo.ExamResult.BranchID =" + BRANCH_ID + ") AND (dbo.SubjectLevelOne.SubjectCategoryID = 1) AND (dbo.ExamSetupMaster.ExamID =" + ExamId + ")" +
                                " ORDER BY dbo.ExamResult.StudentID, dbo.ExamSetupDetail.OrderNo ";


                    SqlDetail2 = "SELECT TOP (100) PERCENT dbo.SubjectLevelOne.SubjectNameL1 AS Subject,dbo.ExamResult.ObtainMarks1 AS Obtain1,dbo.ExamResult.ObtainMarks2 AS Obtain2,dbo.ExamResult.ObtainMarks3 AS Obtain3,dbo.ExamResult.ObtainMarks4 AS Obtain4, dbo.ExamResult.StudentID, " +
                                " dbo.ExamSetupDetail.OrderNo FROM dbo.ExamResult INNER JOIN dbo.SubjectLevelOne ON dbo.ExamResult.SubjectIDL1 = dbo.SubjectLevelOne.IdL1 INNER JOIN " +
                                " dbo.ExamSetupDetail ON dbo.SubjectLevelOne.IdL1 = dbo.ExamSetupDetail.SubjectIDL1 AND dbo.SubjectLevelOne.BranchID = dbo.ExamSetupDetail.BranchID AND " +
                                " dbo.SubjectLevelOne.CompID = dbo.ExamSetupDetail.CompID INNER JOIN dbo.ExamSetupMaster ON dbo.ExamSetupDetail.ExamSetupID = dbo.ExamSetupMaster.ExamSetupID AND " +
                                " dbo.ExamSetupDetail.BranchID = dbo.ExamSetupMaster.BranchID AND dbo.ExamSetupDetail.CompID = dbo.ExamSetupMaster.CompID AND dbo.ExamResult.ClassID = dbo.ExamSetupMaster.ClassID AND dbo.ExamResult.SessionID = dbo.ExamSetupMaster.SessionID " +
                                "  WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.ExamResult.StudentID IN (" + StudentIds + ")) " +
                                " AND (dbo.ExamResult.SessionID =" + SESSION_ID + ") AND (dbo.ExamSetupMaster.SessionID =" + SESSION_ID + ") AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND " +
                                " (dbo.ExamResult.BranchID =" + BRANCH_ID + ") AND (dbo.SubjectLevelOne.SubjectCategoryID = 2) AND (dbo.ExamSetupMaster.ExamID =" + ExamId + ")" +
                                " ORDER BY dbo.ExamResult.StudentID, dbo.ExamSetupDetail.OrderNo ";


                    SqlDetail3 = "SELECT TOP (100) PERCENT dbo.SubjectLevelOne.SubjectNameL1 AS Subject, dbo.ExamResult.ObtainMarks1 AS Obtain1,dbo.ExamResult.ObtainMarks2 AS Obtain2,dbo.ExamResult.ObtainMarks3 AS Obtain3,dbo.ExamResult.ObtainMarks4 AS Obtain4, dbo.ExamResult.StudentID, " +
                               " dbo.ExamSetupDetail.OrderNo FROM dbo.ExamResult INNER JOIN dbo.SubjectLevelOne ON dbo.ExamResult.SubjectIDL1 = dbo.SubjectLevelOne.IdL1 INNER JOIN " +
                               " dbo.ExamSetupDetail ON dbo.SubjectLevelOne.IdL1 = dbo.ExamSetupDetail.SubjectIDL1 AND dbo.SubjectLevelOne.BranchID = dbo.ExamSetupDetail.BranchID AND " +
                               " dbo.SubjectLevelOne.CompID = dbo.ExamSetupDetail.CompID INNER JOIN dbo.ExamSetupMaster ON dbo.ExamSetupDetail.ExamSetupID = dbo.ExamSetupMaster.ExamSetupID AND " +
                               " dbo.ExamSetupDetail.BranchID = dbo.ExamSetupMaster.BranchID AND dbo.ExamSetupDetail.CompID = dbo.ExamSetupMaster.CompID AND dbo.ExamResult.ClassID = dbo.ExamSetupMaster.ClassID AND dbo.ExamResult.SessionID = dbo.ExamSetupMaster.SessionID " +
                               " WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.ExamResult.StudentID IN (" + StudentIds + ")) " +
                               " AND (dbo.ExamResult.SessionID =" + SESSION_ID + ") AND (dbo.ExamSetupMaster.SessionID =" + SESSION_ID + ") AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND " +
                               " (dbo.ExamResult.BranchID =" + BRANCH_ID + ") AND (dbo.SubjectLevelOne.SubjectCategoryID = 3) AND (dbo.ExamSetupMaster.ExamID =" + ExamId + ")" +
                               " ORDER BY dbo.ExamResult.StudentID, dbo.ExamSetupDetail.OrderNo ";
                }
                if (Order == 3)
                {
                    SqlDetail1 = "SELECT TOP (100) PERCENT dbo.SubjectLevelOne.SubjectNameL1 AS Subject," +
                   "(select  max(TRY_CONVERT(decimal(18,2), dbo.ExamResult.ObtainMarks3)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL1=1 OR dbo.ExamResult.SubjectIDL1 = 1080 OR dbo.ExamResult.SubjectIDL1=1038 )) as MaxEnglish, " +
                        "(select  max(TRY_CONVERT(decimal(18,2), dbo.ExamResult.ObtainMarks3)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL1=2 OR dbo.ExamResult.SubjectIDL1 = 1081 OR dbo.ExamResult.SubjectIDL1=1039 )) as MaxEnglishLiterature, " +
                        "(select  max(TRY_CONVERT(decimal(18,2), dbo.ExamResult.ObtainMarks3)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL1=3 OR dbo.ExamResult.SubjectIDL1 = 1082 OR dbo.ExamResult.SubjectIDL1=1040 )) as MaxHindi, " +
                        "(select  max(TRY_CONVERT(decimal(18,2), dbo.ExamResult.ObtainMarks3)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL1=4  OR dbo.ExamResult.SubjectIDL1 = 1083 OR dbo.ExamResult.SubjectIDL1=1041 )) as MaxMaths, " +
                        "(select  max(TRY_CONVERT(decimal(18,2), dbo.ExamResult.ObtainMarks3)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL1=5 OR dbo.ExamResult.SubjectIDL1 = 1084 OR dbo.ExamResult.SubjectIDL1=1042)) as MaxMathsWritten, " +
                        "(select  max(TRY_CONVERT(decimal(18,2), dbo.ExamResult.ObtainMarks3)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL1=6 OR dbo.ExamResult.SubjectIDL1 = 1085 OR dbo.ExamResult.SubjectIDL1=1043)) as MaxMathsOral, " +
                       "(select  max(TRY_CONVERT(decimal(18,2), dbo.ExamResult.ObtainMarks3)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                       " AND (dbo.ExamResult.SubjectIDL1=7 OR dbo.ExamResult.SubjectIDL1 = 1086 OR dbo.ExamResult.SubjectIDL1=1044)) as MAxDictWriting, " +
                         "(select  max(TRY_CONVERT(decimal(18,2), dbo.ExamResult.ObtainMarks3)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                       " AND (dbo.ExamResult.SubjectIDL1=8  OR dbo.ExamResult.SubjectIDL1 = 1087 OR dbo.ExamResult.SubjectIDL1=1045)) as MaxGK, " +
                         "(select  max(TRY_CONVERT(decimal(18,2), dbo.ExamResult.ObtainMarks3)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                       " AND (dbo.ExamResult.SubjectIDL1=9 OR dbo.ExamResult.SubjectIDL1 = 1088 OR dbo.ExamResult.SubjectIDL1=1046)) as MaxEnglishReading, " +
                        "(select  max(TRY_CONVERT(decimal(18,2), dbo.ExamResult.ObtainMarks3)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                       " AND (dbo.ExamResult.SubjectIDL1=10 OR dbo.ExamResult.SubjectIDL1 = 1089 OR dbo.ExamResult.SubjectIDL1=1047)) as MaxEnglishRecitation , " +
                         "(select  max(TRY_CONVERT(decimal(18,2), dbo.ExamResult.ObtainMarks3)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                       " AND (dbo.ExamResult.SubjectIDL1= 11 OR dbo.ExamResult.SubjectIDL1 = 1090 OR dbo.ExamResult.SubjectIDL1=1048)) as MaxReadRecitation, " +
                       "(select  max(TRY_CONVERT(decimal(18,2), dbo.ExamResult.ObtainMarks3)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                       " AND (dbo.ExamResult.SubjectIDL1=12 OR dbo.ExamResult.SubjectIDL1 = 1091 OR dbo.ExamResult.SubjectIDL1=1049)) as MaxStory , " +
                         "(select  max(TRY_CONVERT(decimal(18,2), dbo.ExamResult.ObtainMarks3)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                       " AND (dbo.ExamResult.SubjectIDL1= 13  OR dbo.ExamResult.SubjectIDL1 = 1092 OR dbo.ExamResult.SubjectIDL1=1050)) as MaxEnglishDictationSpelling, " +
                        "(select  max(TRY_CONVERT(decimal(18,2), dbo.ExamResult.ObtainMarks3)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                       " AND (dbo.ExamResult.SubjectIDL1=14 OR dbo.ExamResult.SubjectIDL1 = 1093 OR dbo.ExamResult.SubjectIDL1=1051)) as MaxHandWriting , " +
                         "(select  max(TRY_CONVERT(decimal(18,2), dbo.ExamResult.ObtainMarks3)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                       " AND (dbo.ExamResult.SubjectIDL1= 15 OR dbo.ExamResult.SubjectIDL1 = 1094 OR dbo.ExamResult.SubjectIDL1=1052)) as MaxHistoryCivics, " +
                        "(select  max(TRY_CONVERT(decimal(18,2), dbo.ExamResult.ObtainMarks3)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                       " AND (dbo.ExamResult.SubjectIDL1=16 OR dbo.ExamResult.SubjectIDL1 = 1095 OR dbo.ExamResult.SubjectIDL1=1053)) as MaxGeography , " +
                         "(select  max(TRY_CONVERT(decimal(18,2), dbo.ExamResult.ObtainMarks3)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                       " AND (dbo.ExamResult.SubjectIDL1= 17 OR dbo.ExamResult.SubjectIDL1 = 1096 OR dbo.ExamResult.SubjectIDL1=1054)) as MaxSocialStudies, " +
                        "(select  max(TRY_CONVERT(decimal(18,2), dbo.ExamResult.ObtainMarks3)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                       " AND (dbo.ExamResult.SubjectIDL1=18  OR dbo.ExamResult.SubjectIDL1 = 1097 OR dbo.ExamResult.SubjectIDL1=1055) ) as MaxMoralScience , " +
                         "(select  max(TRY_CONVERT(decimal(18,2), dbo.ExamResult.ObtainMarks3)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                       " AND (dbo.ExamResult.SubjectIDL1= 19 OR dbo.ExamResult.SubjectIDL1 = 1098 OR dbo.ExamResult.SubjectIDL1=1056)) as MaxPhysicsI, " +
                        "(select  max(TRY_CONVERT(decimal(18,2), dbo.ExamResult.ObtainMarks3)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                       " AND (dbo.ExamResult.SubjectIDL1= 20 OR dbo.ExamResult.SubjectIDL1 = 1099 OR dbo.ExamResult.SubjectIDL1=1057)) as MaxChemistryI , " +
                       "(select  max(TRY_CONVERT(decimal(18,2), dbo.ExamResult.ObtainMarks3)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                       " AND (dbo.ExamResult.SubjectIDL1= 21 OR dbo.ExamResult.SubjectIDL1 = 1100 OR dbo.ExamResult.SubjectIDL1=1058)) as MaxBiologyI , " +
                        "(select  max(TRY_CONVERT(decimal(18,2), dbo.ExamResult.ObtainMarks3)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                       " AND (dbo.ExamResult.SubjectIDL1= 22 OR dbo.ExamResult.SubjectIDL1 = 1111 OR dbo.ExamResult.SubjectIDL1=1058)) as MaxPhysicalEduc, " +
                        "(select  max(TRY_CONVERT(decimal(18,2), dbo.ExamResult.ObtainMarks3)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                       " AND (dbo.ExamResult.SubjectIDL1= 1028 OR dbo.ExamResult.SubjectIDL1 = 1112 OR dbo.ExamResult.SubjectIDL1=1070)) as MaxComputer, " +
                       "(select  max(TRY_CONVERT(decimal(18,2), dbo.ExamResult.ObtainMarks3)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                       " AND (dbo.ExamResult.SubjectIDL1= 1029 OR dbo.ExamResult.SubjectIDL1 = 1113 OR dbo.ExamResult.SubjectIDL1=1069)) as MaxComputerPhyEdu, " +
                       "(select  max(TRY_CONVERT(decimal(18,2), dbo.ExamResult.ObtainMarks3)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                       " AND (dbo.ExamResult.SubjectIDL1= 1030 OR dbo.ExamResult.SubjectIDL1 = 1114 OR dbo.ExamResult.SubjectIDL1=1072)) as MaxAccounts, " +
                         "(select  max(TRY_CONVERT(decimal(18,2), dbo.ExamResult.ObtainMarks3)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                       " AND (dbo.ExamResult.SubjectIDL1= 1031 OR dbo.ExamResult.SubjectIDL1 = 1115 OR dbo.ExamResult.SubjectIDL1=1073)) as MaxCommerce, " +
                         "(select  max(TRY_CONVERT(decimal(18,2), dbo.ExamResult.ObtainMarks3)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                       " AND (dbo.ExamResult.SubjectIDL1= 1032 OR dbo.ExamResult.SubjectIDL1 = 1116 OR dbo.ExamResult.SubjectIDL1=1074)) as MaxMathsBusiness , " +
                       "(select  max(TRY_CONVERT(decimal(18,2), dbo.ExamResult.ObtainMarks3)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                       " AND (dbo.ExamResult.SubjectIDL1= 1035 OR dbo.ExamResult.SubjectIDL1 = 1119 OR dbo.ExamResult.SubjectIDL1=1077)) as MaxSanskrit, " +
                       "(select  max(TRY_CONVERT(decimal(18,2), dbo.ExamResult.ObtainMarks3)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                       " AND (dbo.ExamResult.SubjectIDL1= 1037 OR dbo.ExamResult.SubjectIDL1 = 1121 OR dbo.ExamResult.SubjectIDL1=1079)) as MAxComputerArt, " +
                       "(select  max(TRY_CONVERT(decimal(18,2), dbo.ExamResult.ObtainMarks3)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                       " AND (dbo.ExamResult.SubjectIDL1= 1132 or dbo.ExamResult.SubjectIDL1 = 1135 or dbo.ExamResult.SubjectIDL1 = 1136 )) as MAxEVS, " +
                       " dbo.ExamResult.ObtainMarks1 AS Obtain1,dbo.ExamResult.ObtainMarks2 AS Obtain2,dbo.ExamResult.ObtainMarks3 AS Obtain3,dbo.ExamResult.ObtainMarks4 AS Obtain4, dbo.ExamResult.StudentID, " +
                                " dbo.ExamSetupDetail.OrderNo,dbo.ExamResult.Grade AS Grade1, dbo.ExamResult.ObtainGrade2 AS Grade2,dbo.ExamResult.ObtainGrade3 AS Grade3,dbo.ExamResult.ObtainGrade4 AS Grade4, ExamResult.MaxMark1 as Max1,ExamResult.MaxMark2 as Max2,ExamResult.MaxMark3 as Max3,ExamResult.MaxMark4 as Max4,ExamResult.MinMark1 as Min1,ExamResult.MinMark2 as Min2,ExamResult.MinMark3 as Min3,ExamResult.MinMark4 as Min4 " +
                                " FROM dbo.ExamResult INNER JOIN dbo.SubjectLevelOne ON dbo.ExamResult.SubjectIDL1 = dbo.SubjectLevelOne.IdL1 INNER JOIN " +
                                " dbo.ExamSetupDetail ON dbo.SubjectLevelOne.IdL1 = dbo.ExamSetupDetail.SubjectIDL1 AND dbo.SubjectLevelOne.BranchID = dbo.ExamSetupDetail.BranchID AND " +
                                " dbo.SubjectLevelOne.CompID = dbo.ExamSetupDetail.CompID INNER JOIN dbo.ExamSetupMaster ON dbo.ExamSetupDetail.ExamSetupID = dbo.ExamSetupMaster.ExamSetupID AND " +
                                " dbo.ExamSetupDetail.BranchID = dbo.ExamSetupMaster.BranchID AND dbo.ExamSetupDetail.CompID = dbo.ExamSetupMaster.CompID AND dbo.ExamResult.ClassID = dbo.ExamSetupMaster.ClassID AND dbo.ExamResult.SessionID = dbo.ExamSetupMaster.SessionID " +
                                " WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.ExamResult.StudentID IN (" + StudentIds + ")) " +
                                " AND (dbo.ExamResult.SessionID =" + SESSION_ID + ") AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND " +
                                " (dbo.ExamResult.BranchID =" + BRANCH_ID + ") AND (dbo.SubjectLevelOne.SubjectCategoryID = 1) AND (dbo.ExamSetupMaster.ExamID =" + ExamId + ")" +
                                " ORDER BY dbo.ExamResult.StudentID, dbo.ExamSetupDetail.OrderNo ";



                    SqlDetail2 = "SELECT TOP (100) PERCENT dbo.SubjectLevelOne.SubjectNameL1 AS Subject, dbo.ExamResult.ObtainMarks1 AS Obtain1,dbo.ExamResult.ObtainMarks2 AS Obtain2,dbo.ExamResult.ObtainMarks3 AS Obtain3,dbo.ExamResult.ObtainMarks4 AS Obtain4, dbo.ExamResult.StudentID, " +
                                " dbo.ExamSetupDetail.OrderNo FROM dbo.ExamResult INNER JOIN dbo.SubjectLevelOne ON dbo.ExamResult.SubjectIDL1 = dbo.SubjectLevelOne.IdL1 INNER JOIN " +
                                " dbo.ExamSetupDetail ON dbo.SubjectLevelOne.IdL1 = dbo.ExamSetupDetail.SubjectIDL1 AND dbo.SubjectLevelOne.BranchID = dbo.ExamSetupDetail.BranchID AND " +
                                " dbo.SubjectLevelOne.CompID = dbo.ExamSetupDetail.CompID INNER JOIN dbo.ExamSetupMaster ON dbo.ExamSetupDetail.ExamSetupID = dbo.ExamSetupMaster.ExamSetupID AND " +
                                " dbo.ExamSetupDetail.BranchID = dbo.ExamSetupMaster.BranchID AND dbo.ExamSetupDetail.CompID = dbo.ExamSetupMaster.CompID AND dbo.ExamResult.ClassID = dbo.ExamSetupMaster.ClassID AND dbo.ExamResult.SessionID = dbo.ExamSetupMaster.SessionID " +
                                " WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.ExamResult.StudentID IN (" + StudentIds + ")) " +
                                " AND (dbo.ExamResult.SessionID =" + SESSION_ID + ") AND (dbo.ExamSetupMaster.SessionID =" + SESSION_ID + ") AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND " +
                                " (dbo.ExamResult.BranchID =" + BRANCH_ID + ") AND (dbo.SubjectLevelOne.SubjectCategoryID = 2) AND (dbo.ExamSetupMaster.ExamID =" + ExamId + ")" +
                                " ORDER BY dbo.ExamResult.StudentID, dbo.ExamSetupDetail.OrderNo ";


                    SqlDetail3 = "SELECT TOP (100) PERCENT dbo.SubjectLevelOne.SubjectNameL1 AS Subject, dbo.ExamResult.ObtainMarks1 AS Obtain1,dbo.ExamResult.ObtainMarks2 AS Obtain2,dbo.ExamResult.ObtainMarks3 AS Obtain3,dbo.ExamResult.ObtainMarks4 AS Obtain4, dbo.ExamResult.StudentID, " +
                               " dbo.ExamSetupDetail.OrderNo FROM dbo.ExamResult INNER JOIN dbo.SubjectLevelOne ON dbo.ExamResult.SubjectIDL1 = dbo.SubjectLevelOne.IdL1 INNER JOIN " +
                               " dbo.ExamSetupDetail ON dbo.SubjectLevelOne.IdL1 = dbo.ExamSetupDetail.SubjectIDL1 AND dbo.SubjectLevelOne.BranchID = dbo.ExamSetupDetail.BranchID AND " +
                               " dbo.SubjectLevelOne.CompID = dbo.ExamSetupDetail.CompID INNER JOIN dbo.ExamSetupMaster ON dbo.ExamSetupDetail.ExamSetupID = dbo.ExamSetupMaster.ExamSetupID AND " +
                               " dbo.ExamSetupDetail.BranchID = dbo.ExamSetupMaster.BranchID AND dbo.ExamSetupDetail.CompID = dbo.ExamSetupMaster.CompID AND dbo.ExamResult.ClassID = dbo.ExamSetupMaster.ClassID AND dbo.ExamResult.SessionID = dbo.ExamSetupMaster.SessionID " +
                               " WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.ExamResult.StudentID IN (" + StudentIds + ")) " +
                               " AND (dbo.ExamResult.SessionID =" + SESSION_ID + ") AND (dbo.ExamSetupMaster.SessionID =" + SESSION_ID + ") AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND " +
                               " (dbo.ExamResult.BranchID =" + BRANCH_ID + ") AND (dbo.SubjectLevelOne.SubjectCategoryID = 3) AND (dbo.ExamSetupMaster.ExamID =" + ExamId + ")" +
                               " ORDER BY dbo.ExamResult.StudentID, dbo.ExamSetupDetail.OrderNo ";
                }
                if (Order == 4)
                {

                    SqlDetail1 = "SELECT TOP (100) PERCENT dbo.SubjectLevelOne.SubjectNameL1 AS Subject," +
                   "(select  max(TRY_CONVERT(decimal(18,2), dbo.ExamResult.ObtainMarks4)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL1=1 OR dbo.ExamResult.SubjectIDL1 = 1080 OR dbo.ExamResult.SubjectIDL1=1038 )) as MaxEnglish, " +
                        "(select  max(TRY_CONVERT(decimal(18,2), dbo.ExamResult.ObtainMarks4)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL1=2 OR dbo.ExamResult.SubjectIDL1 = 1081 OR dbo.ExamResult.SubjectIDL1=1039 )) as MaxEnglishLiterature, " +
                        "(select  max(TRY_CONVERT(decimal(18,2), dbo.ExamResult.ObtainMarks4)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL1=3 OR dbo.ExamResult.SubjectIDL1 = 1082 OR dbo.ExamResult.SubjectIDL1=1040 )) as MaxHindi, " +
                        "(select  max(TRY_CONVERT(decimal(18,2), dbo.ExamResult.ObtainMarks4)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL1=4  OR dbo.ExamResult.SubjectIDL1 = 1083 OR dbo.ExamResult.SubjectIDL1=1041 )) as MaxMaths, " +
                        "(select  max(TRY_CONVERT(decimal(18,2), dbo.ExamResult.ObtainMarks4)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL1=5 OR dbo.ExamResult.SubjectIDL1 = 1084 OR dbo.ExamResult.SubjectIDL1=1042)) as MaxMathsWritten, " +
                        "(select  max(TRY_CONVERT(decimal(18,2), dbo.ExamResult.ObtainMarks4)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL1=6 OR dbo.ExamResult.SubjectIDL1 = 1085 OR dbo.ExamResult.SubjectIDL1=1043)) as MaxMathsOral, " +
                       "(select  max(TRY_CONVERT(decimal(18,2), dbo.ExamResult.ObtainMarks4)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                       " AND (dbo.ExamResult.SubjectIDL1=7 OR dbo.ExamResult.SubjectIDL1 = 1086 OR dbo.ExamResult.SubjectIDL1=1044)) as MAxDictWriting, " +
                         "(select  max(TRY_CONVERT(decimal(18,2), dbo.ExamResult.ObtainMarks4)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                       " AND (dbo.ExamResult.SubjectIDL1=8  OR dbo.ExamResult.SubjectIDL1 = 1087 OR dbo.ExamResult.SubjectIDL1=1045)) as MaxGK, " +
                         "(select  max(TRY_CONVERT(decimal(18,2), dbo.ExamResult.ObtainMarks4)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                       " AND (dbo.ExamResult.SubjectIDL1=9 OR dbo.ExamResult.SubjectIDL1 = 1088 OR dbo.ExamResult.SubjectIDL1=1046)) as MaxEnglishReading, " +
                        "(select  max(TRY_CONVERT(decimal(18,2), dbo.ExamResult.ObtainMarks4)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                       " AND (dbo.ExamResult.SubjectIDL1=10 OR dbo.ExamResult.SubjectIDL1 = 1089 OR dbo.ExamResult.SubjectIDL1=1047)) as MaxEnglishRecitation , " +
                         "(select  max(TRY_CONVERT(decimal(18,2), dbo.ExamResult.ObtainMarks4)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                       " AND (dbo.ExamResult.SubjectIDL1= 11 OR dbo.ExamResult.SubjectIDL1 = 1090 OR dbo.ExamResult.SubjectIDL1=1048)) as MaxReadRecitation, " +
                       "(select  max(TRY_CONVERT(decimal(18,2), dbo.ExamResult.ObtainMarks4)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                       " AND (dbo.ExamResult.SubjectIDL1=12 OR dbo.ExamResult.SubjectIDL1 = 1091 OR dbo.ExamResult.SubjectIDL1=1049)) as MaxStory , " +
                         "(select  max(TRY_CONVERT(decimal(18,2), dbo.ExamResult.ObtainMarks4)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                       " AND (dbo.ExamResult.SubjectIDL1= 13  OR dbo.ExamResult.SubjectIDL1 = 1092 OR dbo.ExamResult.SubjectIDL1=1050)) as MaxEnglishDictationSpelling, " +
                        "(select  max(TRY_CONVERT(decimal(18,2), dbo.ExamResult.ObtainMarks4)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                       " AND (dbo.ExamResult.SubjectIDL1=14 OR dbo.ExamResult.SubjectIDL1 = 1093 OR dbo.ExamResult.SubjectIDL1=1051)) as MaxHandWriting , " +
                         "(select  max(TRY_CONVERT(decimal(18,2), dbo.ExamResult.ObtainMarks4)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                       " AND (dbo.ExamResult.SubjectIDL1= 15 OR dbo.ExamResult.SubjectIDL1 = 1094 OR dbo.ExamResult.SubjectIDL1=1052)) as MaxHistoryCivics, " +
                        "(select  max(TRY_CONVERT(decimal(18,2), dbo.ExamResult.ObtainMarks4)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                       " AND (dbo.ExamResult.SubjectIDL1=16 OR dbo.ExamResult.SubjectIDL1 = 1095 OR dbo.ExamResult.SubjectIDL1=1053)) as MaxGeography , " +
                         "(select  max(TRY_CONVERT(decimal(18,2), dbo.ExamResult.ObtainMarks4)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                       " AND (dbo.ExamResult.SubjectIDL1= 17 OR dbo.ExamResult.SubjectIDL1 = 1096 OR dbo.ExamResult.SubjectIDL1=1054)) as MaxSocialStudies, " +
                        "(select  max(TRY_CONVERT(decimal(18,2), dbo.ExamResult.ObtainMarks4)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                       " AND (dbo.ExamResult.SubjectIDL1=18  OR dbo.ExamResult.SubjectIDL1 = 1097 OR dbo.ExamResult.SubjectIDL1=1055) ) as MaxMoralScience , " +
                         "(select  max(TRY_CONVERT(decimal(18,2), dbo.ExamResult.ObtainMarks4)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                       " AND (dbo.ExamResult.SubjectIDL1= 19 OR dbo.ExamResult.SubjectIDL1 = 1098 OR dbo.ExamResult.SubjectIDL1=1056)) as MaxPhysicsI, " +
                        "(select  max(TRY_CONVERT(decimal(18,2), dbo.ExamResult.ObtainMarks4)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                       " AND (dbo.ExamResult.SubjectIDL1= 20 OR dbo.ExamResult.SubjectIDL1 = 1099 OR dbo.ExamResult.SubjectIDL1=1057)) as MaxChemistryI , " +
                       "(select  max(TRY_CONVERT(decimal(18,2), dbo.ExamResult.ObtainMarks4)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                       " AND (dbo.ExamResult.SubjectIDL1= 21 OR dbo.ExamResult.SubjectIDL1 = 1100 OR dbo.ExamResult.SubjectIDL1=1058)) as MaxBiologyI , " +
                        "(select  max(TRY_CONVERT(decimal(18,2), dbo.ExamResult.ObtainMarks4)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                       " AND (dbo.ExamResult.SubjectIDL1= 22 OR dbo.ExamResult.SubjectIDL1 = 1111 OR dbo.ExamResult.SubjectIDL1=1058)) as MaxPhysicalEduc, " +
                        "(select  max(TRY_CONVERT(decimal(18,2), dbo.ExamResult.ObtainMarks4)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                       " AND (dbo.ExamResult.SubjectIDL1= 1028 OR dbo.ExamResult.SubjectIDL1 = 1112 OR dbo.ExamResult.SubjectIDL1=1070)) as MaxComputer, " +
                       "(select  max(TRY_CONVERT(decimal(18,2), dbo.ExamResult.ObtainMarks4)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                       " AND (dbo.ExamResult.SubjectIDL1= 1029 OR dbo.ExamResult.SubjectIDL1 = 1113 OR dbo.ExamResult.SubjectIDL1=1069)) as MaxComputerPhyEdu, " +
                       "(select  max(TRY_CONVERT(decimal(18,2), dbo.ExamResult.ObtainMarks4)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                       " AND (dbo.ExamResult.SubjectIDL1= 1030 OR dbo.ExamResult.SubjectIDL1 = 1114 OR dbo.ExamResult.SubjectIDL1=1072)) as MaxAccounts, " +
                         "(select  max(TRY_CONVERT(decimal(18,2), dbo.ExamResult.ObtainMarks4)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                       " AND (dbo.ExamResult.SubjectIDL1= 1031 OR dbo.ExamResult.SubjectIDL1 = 1115 OR dbo.ExamResult.SubjectIDL1=1073)) as MaxCommerce, " +
                         "(select  max(TRY_CONVERT(decimal(18,2), dbo.ExamResult.ObtainMarks4)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                       " AND (dbo.ExamResult.SubjectIDL1= 1032 OR dbo.ExamResult.SubjectIDL1 = 1116 OR dbo.ExamResult.SubjectIDL1=1074)) as MaxMathsBusiness , " +
                       "(select  max(TRY_CONVERT(decimal(18,2), dbo.ExamResult.ObtainMarks4)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                       " AND (dbo.ExamResult.SubjectIDL1= 1035 OR dbo.ExamResult.SubjectIDL1 = 1119 OR dbo.ExamResult.SubjectIDL1=1077)) as MaxSanskrit, " +
                       "(select  max(TRY_CONVERT(decimal(18,2), dbo.ExamResult.ObtainMarks4)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                       " AND (dbo.ExamResult.SubjectIDL1= 1037 OR dbo.ExamResult.SubjectIDL1 = 1121 OR dbo.ExamResult.SubjectIDL1=1079)) as MAxComputerArt, " +
                       "(select  max(TRY_CONVERT(decimal(18,2), dbo.ExamResult.ObtainMarks4)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                       " AND (dbo.ExamResult.SubjectIDL1= 1132 or dbo.ExamResult.SubjectIDL1 = 1135 or dbo.ExamResult.SubjectIDL1 = 1136 )) as MAxEVS, " +

                     " dbo.ExamResult.ObtainMarks1 AS Obtain1,dbo.ExamResult.ObtainMarks2 AS Obtain2,dbo.ExamResult.ObtainMarks3 AS Obtain3,dbo.ExamResult.ObtainMarks4 AS Obtain4, dbo.ExamResult.StudentID, " +
                                " dbo.ExamSetupDetail.OrderNo,dbo.ExamResult.Grade AS Grade1, dbo.ExamResult.ObtainGrade2 AS Grade2,dbo.ExamResult.ObtainGrade3 AS Grade3,dbo.ExamResult.ObtainGrade4 AS Grade4, ExamResult.MaxMark1 as Max1,ExamResult.MaxMark2 as Max2,ExamResult.MaxMark3 as Max3,ExamResult.MaxMark4 as Max4,ExamResult.MinMark1 as Min1,ExamResult.MinMark2 as Min2,ExamResult.MinMark3 as Min3,ExamResult.MinMark4 as Min4 " +
                                " FROM dbo.ExamResult INNER JOIN dbo.SubjectLevelOne ON dbo.ExamResult.SubjectIDL1 = dbo.SubjectLevelOne.IdL1 INNER JOIN " +
                                " dbo.ExamSetupDetail ON dbo.SubjectLevelOne.IdL1 = dbo.ExamSetupDetail.SubjectIDL1 AND dbo.SubjectLevelOne.BranchID = dbo.ExamSetupDetail.BranchID AND " +
                                " dbo.SubjectLevelOne.CompID = dbo.ExamSetupDetail.CompID INNER JOIN dbo.ExamSetupMaster ON dbo.ExamSetupDetail.ExamSetupID = dbo.ExamSetupMaster.ExamSetupID AND " +
                                " dbo.ExamSetupDetail.BranchID = dbo.ExamSetupMaster.BranchID AND dbo.ExamSetupDetail.CompID = dbo.ExamSetupMaster.CompID AND dbo.ExamResult.ClassID = dbo.ExamSetupMaster.ClassID AND dbo.ExamResult.SessionID = dbo.ExamSetupMaster.SessionID " +
                                " WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.ExamResult.StudentID IN (" + StudentIds + ")) " +
                                " AND (dbo.ExamResult.SessionID =" + SESSION_ID + ") AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND " +
                                " (dbo.ExamResult.BranchID =" + BRANCH_ID + ") AND (dbo.SubjectLevelOne.SubjectCategoryID = 1) AND (dbo.ExamSetupMaster.ExamID =" + ExamId + ")" +
                                " ORDER BY dbo.ExamResult.StudentID, dbo.ExamSetupDetail.OrderNo ";




                    SqlDetail2 = "SELECT TOP (100) PERCENT dbo.SubjectLevelOne.SubjectNameL1 AS Subject, dbo.ExamResult.ObtainMarks1 AS Obtain1,dbo.ExamResult.ObtainMarks2 AS Obtain2,dbo.ExamResult.ObtainMarks3 AS Obtain3,dbo.ExamResult.ObtainMarks4 AS Obtain4, dbo.ExamResult.StudentID, " +
                                " dbo.ExamSetupDetail.OrderNo FROM dbo.ExamResult INNER JOIN dbo.SubjectLevelOne ON dbo.ExamResult.SubjectIDL1 = dbo.SubjectLevelOne.IdL1 INNER JOIN " +
                                " dbo.ExamSetupDetail ON dbo.SubjectLevelOne.IdL1 = dbo.ExamSetupDetail.SubjectIDL1 AND dbo.SubjectLevelOne.BranchID = dbo.ExamSetupDetail.BranchID AND " +
                                " dbo.SubjectLevelOne.CompID = dbo.ExamSetupDetail.CompID INNER JOIN dbo.ExamSetupMaster ON dbo.ExamSetupDetail.ExamSetupID = dbo.ExamSetupMaster.ExamSetupID AND " +
                                " dbo.ExamSetupDetail.BranchID = dbo.ExamSetupMaster.BranchID AND dbo.ExamSetupDetail.CompID = dbo.ExamSetupMaster.CompID AND dbo.ExamResult.ClassID = dbo.ExamSetupMaster.ClassID AND dbo.ExamResult.SessionID = dbo.ExamSetupMaster.SessionID " +
                                " WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.ExamResult.StudentID IN (" + StudentIds + ")) " +
                                " AND (dbo.ExamResult.SessionID =" + SESSION_ID + ") AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND " +
                                " (dbo.ExamResult.BranchID =" + BRANCH_ID + ") AND (dbo.SubjectLevelOne.SubjectCategoryID = 2) AND (dbo.ExamSetupMaster.ExamID =" + ExamId + ")" +
                                " ORDER BY dbo.ExamResult.StudentID, dbo.ExamSetupDetail.OrderNo ";


                    SqlDetail3 = "SELECT TOP (100) PERCENT dbo.SubjectLevelOne.SubjectNameL1 AS Subject, dbo.ExamResult.ObtainMarks1 AS Obtain1,dbo.ExamResult.ObtainMarks2 AS Obtain2,dbo.ExamResult.ObtainMarks3 AS Obtain3,dbo.ExamResult.ObtainMarks4 AS Obtain4, dbo.ExamResult.StudentID, " +
                               " dbo.ExamSetupDetail.OrderNo FROM dbo.ExamResult INNER JOIN dbo.SubjectLevelOne ON dbo.ExamResult.SubjectIDL1 = dbo.SubjectLevelOne.IdL1 INNER JOIN " +
                               " dbo.ExamSetupDetail ON dbo.SubjectLevelOne.IdL1 = dbo.ExamSetupDetail.SubjectIDL1 AND dbo.SubjectLevelOne.BranchID = dbo.ExamSetupDetail.BranchID AND " +
                               " dbo.SubjectLevelOne.CompID = dbo.ExamSetupDetail.CompID INNER JOIN dbo.ExamSetupMaster ON dbo.ExamSetupDetail.ExamSetupID = dbo.ExamSetupMaster.ExamSetupID AND " +
                               " dbo.ExamSetupDetail.BranchID = dbo.ExamSetupMaster.BranchID AND dbo.ExamSetupDetail.CompID = dbo.ExamSetupMaster.CompID AND dbo.ExamResult.ClassID = dbo.ExamSetupMaster.ClassID AND dbo.ExamResult.SessionID = dbo.ExamSetupMaster.SessionID " +
                               " WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.ExamResult.StudentID IN (" + StudentIds + ")) " +
                               " AND (dbo.ExamResult.SessionID =" + SESSION_ID + ") AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND " +
                               " (dbo.ExamResult.BranchID =" + BRANCH_ID + ") AND (dbo.SubjectLevelOne.SubjectCategoryID = 3) AND (dbo.ExamSetupMaster.ExamID =" + ExamId + ")" +
                               " ORDER BY dbo.ExamResult.StudentID, dbo.ExamSetupDetail.OrderNo ";
                }

            }
            #endregion

            #region for stmark old logic before 15-07-2018
            //if (COMPANY_ID == 2)
            //{
            //    if (Order == 1)
            //    {
            //        sqlDetail = "SELECT TOP (100) PERCENT dbo.SubjectLevelOne.SubjectNameL1 AS Subject,dbo.ExamResult.ObtainMarks1 AS Obtain1,dbo.ExamResult.ObtainMarks2 AS Obtain2,dbo.ExamResult.ObtainMarks3 AS Obtain3,dbo.ExamResult.ObtainMarks4 AS Obtain4, dbo.ExamResult.StudentID, " +
            //                    " dbo.ExamSetupDetail.OrderNo, ExamResult.MaxMark1 as Max1,ExamResult.MaxMark2 as Max2,ExamResult.MaxMark3 as Max3,ExamResult.MaxMark4 as Max4,ExamResult.MinMark1 as Min1,ExamResult.MinMark2 as Min2,ExamResult.MinMark3 as Min3,ExamResult.MinMark4 as Min4 " +
            //                    " FROM dbo.ExamResult INNER JOIN dbo.SubjectLevelOne ON dbo.ExamResult.SubjectIDL1 = dbo.SubjectLevelOne.IdL1 INNER JOIN " +
            //                    " dbo.ExamSetupDetail ON dbo.SubjectLevelOne.IdL1 = dbo.ExamSetupDetail.SubjectIDL1 AND dbo.SubjectLevelOne.BranchID = dbo.ExamSetupDetail.BranchID AND " +
            //                    " dbo.SubjectLevelOne.CompID = dbo.ExamSetupDetail.CompID INNER JOIN dbo.ExamSetupMaster ON dbo.ExamSetupDetail.ExamSetupID = dbo.ExamSetupMaster.ExamSetupID AND " +
            //                    " dbo.ExamSetupDetail.BranchID = dbo.ExamSetupMaster.BranchID AND dbo.ExamSetupDetail.CompID = dbo.ExamSetupMaster.CompID AND dbo.ExamResult.ClassID = dbo.ExamSetupMaster.ClassID AND dbo.ExamResult.SessionID = dbo.ExamSetupMaster.SessionID " +
            //                    " WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.ExamResult.StudentID IN (" + StudentIds + ")) " +
            //                    " AND (dbo.ExamResult.SessionID =" + SESSION_ID + ") AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND " +
            //                    " (dbo.ExamResult.BranchID =" + BRANCH_ID + ") AND (dbo.SubjectLevelOne.SubjectCategoryID = 1) AND (dbo.ExamSetupMaster.ExamID =" + ExamId + ")" +
            //                    " ORDER BY dbo.ExamResult.StudentID, dbo.ExamSetupDetail.OrderNo ";


            //        sqlDetail2 = "SELECT TOP (100) PERCENT dbo.SubjectLevelOne.SubjectNameL1 AS Subject, dbo.ExamResult.ObtainMarks1 AS Obtain1,dbo.ExamResult.ObtainMarks2 AS Obtain2,dbo.ExamResult.ObtainMarks3 AS Obtain3,dbo.ExamResult.ObtainMarks4 AS Obtain4, dbo.ExamResult.StudentID, " +
            //                    " dbo.ExamSetupDetail.OrderNo FROM dbo.ExamResult INNER JOIN dbo.SubjectLevelOne ON dbo.ExamResult.SubjectIDL1 = dbo.SubjectLevelOne.IdL1 INNER JOIN " +
            //                    " dbo.ExamSetupDetail ON dbo.SubjectLevelOne.IdL1 = dbo.ExamSetupDetail.SubjectIDL1 AND dbo.SubjectLevelOne.BranchID = dbo.ExamSetupDetail.BranchID AND " +
            //                    " dbo.SubjectLevelOne.CompID = dbo.ExamSetupDetail.CompID INNER JOIN dbo.ExamSetupMaster ON dbo.ExamSetupDetail.ExamSetupID = dbo.ExamSetupMaster.ExamSetupID AND " +
            //                    " dbo.ExamSetupDetail.BranchID = dbo.ExamSetupMaster.BranchID AND dbo.ExamSetupDetail.CompID = dbo.ExamSetupMaster.CompID AND dbo.ExamResult.ClassID = dbo.ExamSetupMaster.ClassID AND dbo.ExamResult.SessionID = dbo.ExamSetupMaster.SessionID " +
            //                    " WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.ExamResult.StudentID IN (" + StudentIds + ")) " +
            //                    " AND (dbo.ExamResult.SessionID =" + SESSION_ID + ") AND (dbo.ExamSetupMaster.SessionID =" + SESSION_ID + ") AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND " +
            //                    " (dbo.ExamResult.BranchID =" + BRANCH_ID + ") AND (dbo.SubjectLevelOne.SubjectCategoryID = 2) AND (dbo.ExamSetupMaster.ExamID =" + ExamId + ")" +
            //                    " ORDER BY dbo.ExamResult.StudentID, dbo.ExamSetupDetail.OrderNo ";


            //        sqlDetail3 = "SELECT TOP (100) PERCENT dbo.SubjectLevelOne.SubjectNameL1 AS Subject, dbo.ExamResult.ObtainMarks1 AS Obtain1,dbo.ExamResult.ObtainMarks2 AS Obtain2,dbo.ExamResult.ObtainMarks3 AS Obtain3,dbo.ExamResult.ObtainMarks4 AS Obtain4, dbo.ExamResult.StudentID, " +
            //                   " dbo.ExamSetupDetail.OrderNo FROM dbo.ExamResult INNER JOIN dbo.SubjectLevelOne ON dbo.ExamResult.SubjectIDL1 = dbo.SubjectLevelOne.IdL1 INNER JOIN " +
            //                   " dbo.ExamSetupDetail ON dbo.SubjectLevelOne.IdL1 = dbo.ExamSetupDetail.SubjectIDL1 AND dbo.SubjectLevelOne.BranchID = dbo.ExamSetupDetail.BranchID AND " +
            //                   " dbo.SubjectLevelOne.CompID = dbo.ExamSetupDetail.CompID INNER JOIN dbo.ExamSetupMaster ON dbo.ExamSetupDetail.ExamSetupID = dbo.ExamSetupMaster.ExamSetupID AND " +
            //                   " dbo.ExamSetupDetail.BranchID = dbo.ExamSetupMaster.BranchID AND dbo.ExamSetupDetail.CompID = dbo.ExamSetupMaster.CompID AND dbo.ExamResult.ClassID = dbo.ExamSetupMaster.ClassID AND dbo.ExamResult.SessionID = dbo.ExamSetupMaster.SessionID " +
            //                   " WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.ExamResult.StudentID IN (" + StudentIds + ")) " +
            //                   " AND (dbo.ExamResult.SessionID =" + SESSION_ID + ") AND (dbo.ExamSetupMaster.SessionID =" + SESSION_ID + ") AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND " +
            //                   " (dbo.ExamResult.BranchID =" + BRANCH_ID + ") AND (dbo.SubjectLevelOne.SubjectCategoryID = 3) AND (dbo.ExamSetupMaster.ExamID =" + ExamId + ")" +
            //                   " ORDER BY dbo.ExamResult.StudentID, dbo.ExamSetupDetail.OrderNo ";

            //    }
            //    if (Order == 2)
            //    {
            //        sqlDetail = "SELECT TOP (100) PERCENT dbo.SubjectLevelOne.SubjectNameL1 AS Subject, dbo.ExamResult.ObtainMarks1 AS Obtain1,dbo.ExamResult.ObtainMarks2 AS Obtain2,dbo.ExamResult.ObtainMarks3 AS Obtain3,dbo.ExamResult.ObtainMarks4 AS Obtain4, dbo.ExamResult.StudentID, " +
            //                     " dbo.ExamSetupDetail.OrderNo,ExamResult.MaxMark1 as Max1,ExamResult.MaxMark2 as Max2,ExamResult.MaxMark3 as Max3,ExamResult.MaxMark4 as Max4,ExamResult.MinMark1 as Min1,ExamResult.MinMark2 as Min2,ExamResult.MinMark3 as Min3,ExamResult.MinMark4 as Min4" +
            //                     " FROM dbo.ExamResult INNER JOIN dbo.SubjectLevelOne ON dbo.ExamResult.SubjectIDL1 = dbo.SubjectLevelOne.IdL1 INNER JOIN " +
            //                     " dbo.ExamSetupDetail ON dbo.SubjectLevelOne.IdL1 = dbo.ExamSetupDetail.SubjectIDL1 AND dbo.SubjectLevelOne.BranchID = dbo.ExamSetupDetail.BranchID AND " +
            //                     " dbo.SubjectLevelOne.CompID = dbo.ExamSetupDetail.CompID INNER JOIN dbo.ExamSetupMaster ON dbo.ExamSetupDetail.ExamSetupID = dbo.ExamSetupMaster.ExamSetupID AND " +
            //                     " dbo.ExamSetupDetail.BranchID = dbo.ExamSetupMaster.BranchID AND dbo.ExamSetupDetail.CompID = dbo.ExamSetupMaster.CompID AND dbo.ExamResult.ClassID = dbo.ExamSetupMaster.ClassID AND dbo.ExamResult.SessionID = dbo.ExamSetupMaster.SessionID " +
            //                     "  WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.ExamResult.StudentID IN (" + StudentIds + ")) " +
            //                     " AND (dbo.ExamResult.SessionID =" + SESSION_ID + ") AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND " +
            //                     " (dbo.ExamResult.BranchID =" + BRANCH_ID + ") AND (dbo.SubjectLevelOne.SubjectCategoryID = 1) AND (dbo.ExamSetupMaster.ExamID =" + ExamId + ")" +
            //                     " ORDER BY dbo.ExamResult.StudentID, dbo.ExamSetupDetail.OrderNo ";


            //        sqlDetail2 = "SELECT TOP (100) PERCENT dbo.SubjectLevelOne.SubjectNameL1 AS Subject,dbo.ExamResult.ObtainMarks1 AS Obtain1,dbo.ExamResult.ObtainMarks2 AS Obtain2,dbo.ExamResult.ObtainMarks3 AS Obtain3,dbo.ExamResult.ObtainMarks4 AS Obtain4, dbo.ExamResult.StudentID, " +
            //                    " dbo.ExamSetupDetail.OrderNo FROM dbo.ExamResult INNER JOIN dbo.SubjectLevelOne ON dbo.ExamResult.SubjectIDL1 = dbo.SubjectLevelOne.IdL1 INNER JOIN " +
            //                    " dbo.ExamSetupDetail ON dbo.SubjectLevelOne.IdL1 = dbo.ExamSetupDetail.SubjectIDL1 AND dbo.SubjectLevelOne.BranchID = dbo.ExamSetupDetail.BranchID AND " +
            //                    " dbo.SubjectLevelOne.CompID = dbo.ExamSetupDetail.CompID INNER JOIN dbo.ExamSetupMaster ON dbo.ExamSetupDetail.ExamSetupID = dbo.ExamSetupMaster.ExamSetupID AND " +
            //                    " dbo.ExamSetupDetail.BranchID = dbo.ExamSetupMaster.BranchID AND dbo.ExamSetupDetail.CompID = dbo.ExamSetupMaster.CompID AND dbo.ExamResult.ClassID = dbo.ExamSetupMaster.ClassID AND dbo.ExamResult.SessionID = dbo.ExamSetupMaster.SessionID " +
            //                    "  WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.ExamResult.StudentID IN (" + StudentIds + ")) " +
            //                    " AND (dbo.ExamResult.SessionID =" + SESSION_ID + ") AND (dbo.ExamSetupMaster.SessionID =" + SESSION_ID + ") AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND " +
            //                    " (dbo.ExamResult.BranchID =" + BRANCH_ID + ") AND (dbo.SubjectLevelOne.SubjectCategoryID = 2) AND (dbo.ExamSetupMaster.ExamID =" + ExamId + ")" +
            //                    " ORDER BY dbo.ExamResult.StudentID, dbo.ExamSetupDetail.OrderNo ";


            //        sqlDetail3 = "SELECT TOP (100) PERCENT dbo.SubjectLevelOne.SubjectNameL1 AS Subject, dbo.ExamResult.ObtainMarks1 AS Obtain1,dbo.ExamResult.ObtainMarks2 AS Obtain2,dbo.ExamResult.ObtainMarks3 AS Obtain3,dbo.ExamResult.ObtainMarks4 AS Obtain4, dbo.ExamResult.StudentID, " +
            //                   " dbo.ExamSetupDetail.OrderNo FROM dbo.ExamResult INNER JOIN dbo.SubjectLevelOne ON dbo.ExamResult.SubjectIDL1 = dbo.SubjectLevelOne.IdL1 INNER JOIN " +
            //                   " dbo.ExamSetupDetail ON dbo.SubjectLevelOne.IdL1 = dbo.ExamSetupDetail.SubjectIDL1 AND dbo.SubjectLevelOne.BranchID = dbo.ExamSetupDetail.BranchID AND " +
            //                   " dbo.SubjectLevelOne.CompID = dbo.ExamSetupDetail.CompID INNER JOIN dbo.ExamSetupMaster ON dbo.ExamSetupDetail.ExamSetupID = dbo.ExamSetupMaster.ExamSetupID AND " +
            //                   " dbo.ExamSetupDetail.BranchID = dbo.ExamSetupMaster.BranchID AND dbo.ExamSetupDetail.CompID = dbo.ExamSetupMaster.CompID AND dbo.ExamResult.ClassID = dbo.ExamSetupMaster.ClassID AND dbo.ExamResult.SessionID = dbo.ExamSetupMaster.SessionID " +
            //                   " WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.ExamResult.StudentID IN (" + StudentIds + ")) " +
            //                   " AND (dbo.ExamResult.SessionID =" + SESSION_ID + ") AND (dbo.ExamSetupMaster.SessionID =" + SESSION_ID + ") AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND " +
            //                   " (dbo.ExamResult.BranchID =" + BRANCH_ID + ") AND (dbo.SubjectLevelOne.SubjectCategoryID = 3) AND (dbo.ExamSetupMaster.ExamID =" + ExamId + ")" +
            //                   " ORDER BY dbo.ExamResult.StudentID, dbo.ExamSetupDetail.OrderNo ";
            //    }
            //    if (Order == 3)
            //    {
            //        sqlDetail = "SELECT TOP (100) PERCENT dbo.SubjectLevelOne.SubjectNameL1 AS Subject, " +
            //                     "  dbo.ExamResult.ObtainMarks1 AS Obtain1, dbo.ExamResult.ObtainMarks2 AS Obtain2,dbo.ExamResult.ObtainMarks3 AS Obtain3,dbo.ExamResult.ObtainMarks4 AS Obtain4, dbo.ExamResult.StudentID, " +
            //                     " dbo.ExamSetupDetail.OrderNo,ExamResult.MaxMark1 as Max1,ExamResult.MaxMark2 as Max2,ExamResult.MaxMark3 as Max3,ExamResult.MaxMark4 as Max4,ExamResult.MinMark1 as Min1,ExamResult.MinMark2 as Min2,ExamResult.MinMark3 as Min3,ExamResult.MinMark4 as Min4 " +
            //                     " FROM dbo.ExamResult INNER JOIN dbo.SubjectLevelOne ON dbo.ExamResult.SubjectIDL1 = dbo.SubjectLevelOne.IdL1 INNER JOIN " +
            //                     " dbo.ExamSetupDetail ON dbo.SubjectLevelOne.IdL1 = dbo.ExamSetupDetail.SubjectIDL1 AND dbo.SubjectLevelOne.BranchID = dbo.ExamSetupDetail.BranchID AND " +
            //                     " dbo.SubjectLevelOne.CompID = dbo.ExamSetupDetail.CompID INNER JOIN dbo.ExamSetupMaster ON dbo.ExamSetupDetail.ExamSetupID = dbo.ExamSetupMaster.ExamSetupID AND " +
            //                     " dbo.ExamSetupDetail.BranchID = dbo.ExamSetupMaster.BranchID AND dbo.ExamSetupDetail.CompID = dbo.ExamSetupMaster.CompID AND dbo.ExamResult.ClassID = dbo.ExamSetupMaster.ClassID AND dbo.ExamResult.SessionID = dbo.ExamSetupMaster.SessionID " +
            //                     " WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.ExamResult.StudentID IN (" + StudentIds + ")) " +
            //                     " AND (dbo.ExamResult.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND " +
            //                     " (dbo.ExamResult.BranchID =" + BRANCH_ID + ") AND (dbo.SubjectLevelOne.SubjectCategoryID = 1) AND (dbo.ExamSetupMaster.ExamID =" + ExamId + ")" +
            //                     " ORDER BY dbo.ExamResult.StudentID, dbo.ExamSetupDetail.OrderNo ";


            //        sqlDetail2 = "SELECT TOP (100) PERCENT dbo.SubjectLevelOne.SubjectNameL1 AS Subject, dbo.ExamResult.ObtainMarks1 AS Obtain1,dbo.ExamResult.ObtainMarks2 AS Obtain2,dbo.ExamResult.ObtainMarks3 AS Obtain3,dbo.ExamResult.ObtainMarks4 AS Obtain4, dbo.ExamResult.StudentID, " +
            //                    " dbo.ExamSetupDetail.OrderNo FROM dbo.ExamResult INNER JOIN dbo.SubjectLevelOne ON dbo.ExamResult.SubjectIDL1 = dbo.SubjectLevelOne.IdL1 INNER JOIN " +
            //                    " dbo.ExamSetupDetail ON dbo.SubjectLevelOne.IdL1 = dbo.ExamSetupDetail.SubjectIDL1 AND dbo.SubjectLevelOne.BranchID = dbo.ExamSetupDetail.BranchID AND " +
            //                    " dbo.SubjectLevelOne.CompID = dbo.ExamSetupDetail.CompID INNER JOIN dbo.ExamSetupMaster ON dbo.ExamSetupDetail.ExamSetupID = dbo.ExamSetupMaster.ExamSetupID AND " +
            //                    " dbo.ExamSetupDetail.BranchID = dbo.ExamSetupMaster.BranchID AND dbo.ExamSetupDetail.CompID = dbo.ExamSetupMaster.CompID AND dbo.ExamResult.ClassID = dbo.ExamSetupMaster.ClassID AND dbo.ExamResult.SessionID = dbo.ExamSetupMaster.SessionID " +
            //                    " WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.ExamResult.StudentID IN (" + StudentIds + ")) " +
            //                    " AND (dbo.ExamResult.SessionID =" + SESSION_ID + ") AND (dbo.ExamSetupMaster.SessionID =" + SESSION_ID + ") AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND " +
            //                    " (dbo.ExamResult.BranchID =" + BRANCH_ID + ") AND (dbo.SubjectLevelOne.SubjectCategoryID = 2) AND (dbo.ExamSetupMaster.ExamID =" + ExamId + ")" +
            //                    " ORDER BY dbo.ExamResult.StudentID, dbo.ExamSetupDetail.OrderNo ";


            //        sqlDetail3 = "SELECT TOP (100) PERCENT dbo.SubjectLevelOne.SubjectNameL1 AS Subject, dbo.ExamResult.ObtainMarks1 AS Obtain1,dbo.ExamResult.ObtainMarks2 AS Obtain2,dbo.ExamResult.ObtainMarks3 AS Obtain3,dbo.ExamResult.ObtainMarks4 AS Obtain4, dbo.ExamResult.StudentID, " +
            //                   " dbo.ExamSetupDetail.OrderNo FROM dbo.ExamResult INNER JOIN dbo.SubjectLevelOne ON dbo.ExamResult.SubjectIDL1 = dbo.SubjectLevelOne.IdL1 INNER JOIN " +
            //                   " dbo.ExamSetupDetail ON dbo.SubjectLevelOne.IdL1 = dbo.ExamSetupDetail.SubjectIDL1 AND dbo.SubjectLevelOne.BranchID = dbo.ExamSetupDetail.BranchID AND " +
            //                   " dbo.SubjectLevelOne.CompID = dbo.ExamSetupDetail.CompID INNER JOIN dbo.ExamSetupMaster ON dbo.ExamSetupDetail.ExamSetupID = dbo.ExamSetupMaster.ExamSetupID AND " +
            //                   " dbo.ExamSetupDetail.BranchID = dbo.ExamSetupMaster.BranchID AND dbo.ExamSetupDetail.CompID = dbo.ExamSetupMaster.CompID AND dbo.ExamResult.ClassID = dbo.ExamSetupMaster.ClassID AND dbo.ExamResult.SessionID = dbo.ExamSetupMaster.SessionID " +
            //                   " WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.ExamResult.StudentID IN (" + StudentIds + ")) " +
            //                   " AND (dbo.ExamResult.SessionID =" + SESSION_ID + ") AND (dbo.ExamSetupMaster.SessionID =" + SESSION_ID + ") AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND " +
            //                   " (dbo.ExamResult.BranchID =" + BRANCH_ID + ") AND (dbo.SubjectLevelOne.SubjectCategoryID = 3) AND (dbo.ExamSetupMaster.ExamID =" + ExamId + ")" +
            //                   " ORDER BY dbo.ExamResult.StudentID, dbo.ExamSetupDetail.OrderNo ";
            //    }
            //    if (Order == 4)
            //    {
            //        sqlDetail = "SELECT TOP (100) PERCENT dbo.SubjectLevelOne.SubjectNameL1 AS Subject, dbo.ExamResult.ObtainMarks1 AS Obtain1,dbo.ExamResult.ObtainMarks2 AS Obtain2,dbo.ExamResult.ObtainMarks3 AS Obtain3,dbo.ExamResult.ObtainMarks4 AS Obtain4, dbo.ExamResult.StudentID, " +
            //                    " dbo.ExamSetupDetail.OrderNo,ExamResult.MaxMark1 as Max1,ExamResult.MaxMark2 as Max2,ExamResult.MaxMark3 as Max3,ExamResult.MaxMark4 as Max4,ExamResult.MinMark1 as Min1,ExamResult.MinMark2 as Min2,ExamResult.MinMark3 as Min3,ExamResult.MinMark4 as Min4 " +
            //                    " FROM dbo.ExamResult INNER JOIN dbo.SubjectLevelOne ON dbo.ExamResult.SubjectIDL1 = dbo.SubjectLevelOne.IdL1 INNER JOIN " +
            //                    " dbo.ExamSetupDetail ON dbo.SubjectLevelOne.IdL1 = dbo.ExamSetupDetail.SubjectIDL1 AND dbo.SubjectLevelOne.BranchID = dbo.ExamSetupDetail.BranchID AND " +
            //                    " dbo.SubjectLevelOne.CompID = dbo.ExamSetupDetail.CompID INNER JOIN dbo.ExamSetupMaster ON dbo.ExamSetupDetail.ExamSetupID = dbo.ExamSetupMaster.ExamSetupID AND " +
            //                    " dbo.ExamSetupDetail.BranchID = dbo.ExamSetupMaster.BranchID AND dbo.ExamSetupDetail.CompID = dbo.ExamSetupMaster.CompID AND dbo.ExamResult.ClassID = dbo.ExamSetupMaster.ClassID AND dbo.ExamResult.SessionID = dbo.ExamSetupMaster.SessionID " +
            //                    " WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.ExamResult.StudentID IN (" + StudentIds + ")) " +
            //                    " AND (dbo.ExamResult.SessionID =" + SESSION_ID + ") AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND " +
            //                    " (dbo.ExamResult.BranchID =" + BRANCH_ID + ") AND (dbo.SubjectLevelOne.SubjectCategoryID = 1) AND (dbo.ExamSetupMaster.ExamID =" + ExamId + ")" +
            //                    " ORDER BY dbo.ExamResult.StudentID, dbo.ExamSetupDetail.OrderNo ";


            //        sqlDetail2 = "SELECT TOP (100) PERCENT dbo.SubjectLevelOne.SubjectNameL1 AS Subject, dbo.ExamResult.ObtainMarks1 AS Obtain1,dbo.ExamResult.ObtainMarks2 AS Obtain2,dbo.ExamResult.ObtainMarks3 AS Obtain3,dbo.ExamResult.ObtainMarks4 AS Obtain4, dbo.ExamResult.StudentID, " +
            //                    " dbo.ExamSetupDetail.OrderNo FROM dbo.ExamResult INNER JOIN dbo.SubjectLevelOne ON dbo.ExamResult.SubjectIDL1 = dbo.SubjectLevelOne.IdL1 INNER JOIN " +
            //                    " dbo.ExamSetupDetail ON dbo.SubjectLevelOne.IdL1 = dbo.ExamSetupDetail.SubjectIDL1 AND dbo.SubjectLevelOne.BranchID = dbo.ExamSetupDetail.BranchID AND " +
            //                    " dbo.SubjectLevelOne.CompID = dbo.ExamSetupDetail.CompID INNER JOIN dbo.ExamSetupMaster ON dbo.ExamSetupDetail.ExamSetupID = dbo.ExamSetupMaster.ExamSetupID AND " +
            //                    " dbo.ExamSetupDetail.BranchID = dbo.ExamSetupMaster.BranchID AND dbo.ExamSetupDetail.CompID = dbo.ExamSetupMaster.CompID AND dbo.ExamResult.ClassID = dbo.ExamSetupMaster.ClassID AND dbo.ExamResult.SessionID = dbo.ExamSetupMaster.SessionID " +
            //                    " WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.ExamResult.StudentID IN (" + StudentIds + ")) " +
            //                    " AND (dbo.ExamResult.SessionID =" + SESSION_ID + ") AND (dbo.ExamSetupMaster.SessionID =" + SESSION_ID + ") AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND " +
            //                    " (dbo.ExamResult.BranchID =" + BRANCH_ID + ") AND (dbo.SubjectLevelOne.SubjectCategoryID = 2) AND (dbo.ExamSetupMaster.ExamID =" + ExamId + ")" +
            //                    " ORDER BY dbo.ExamResult.StudentID, dbo.ExamSetupDetail.OrderNo ";


            //        sqlDetail3 = "SELECT TOP (100) PERCENT dbo.SubjectLevelOne.SubjectNameL1 AS Subject, dbo.ExamResult.ObtainMarks1 AS Obtain1,dbo.ExamResult.ObtainMarks2 AS Obtain2,dbo.ExamResult.ObtainMarks3 AS Obtain3,dbo.ExamResult.ObtainMarks4 AS Obtain4, dbo.ExamResult.StudentID, " +
            //                   " dbo.ExamSetupDetail.OrderNo FROM dbo.ExamResult INNER JOIN dbo.SubjectLevelOne ON dbo.ExamResult.SubjectIDL1 = dbo.SubjectLevelOne.IdL1 INNER JOIN " +
            //                   " dbo.ExamSetupDetail ON dbo.SubjectLevelOne.IdL1 = dbo.ExamSetupDetail.SubjectIDL1 AND dbo.SubjectLevelOne.BranchID = dbo.ExamSetupDetail.BranchID AND " +
            //                   " dbo.SubjectLevelOne.CompID = dbo.ExamSetupDetail.CompID INNER JOIN dbo.ExamSetupMaster ON dbo.ExamSetupDetail.ExamSetupID = dbo.ExamSetupMaster.ExamSetupID AND " +
            //                   " dbo.ExamSetupDetail.BranchID = dbo.ExamSetupMaster.BranchID AND dbo.ExamSetupDetail.CompID = dbo.ExamSetupMaster.CompID AND dbo.ExamResult.ClassID = dbo.ExamSetupMaster.ClassID AND dbo.ExamResult.SessionID = dbo.ExamSetupMaster.SessionID " +
            //                   " WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.ExamResult.StudentID IN (" + StudentIds + ")) " +
            //                   " AND (dbo.ExamResult.SessionID =" + SESSION_ID + ") AND (dbo.ExamSetupMaster.SessionID =" + SESSION_ID + ") AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND " +
            //                   " (dbo.ExamResult.BranchID =" + BRANCH_ID + ") AND (dbo.SubjectLevelOne.SubjectCategoryID = 3) AND (dbo.ExamSetupMaster.ExamID =" + ExamId + ")" +
            //                   " ORDER BY dbo.ExamResult.StudentID, dbo.ExamSetupDetail.OrderNo ";
            //    }

            //}
            #endregion 
        }

        public void ReturnStXavierExamMarksheetSql()
        {
            #region for St xviear
            if (COMPANY_ID == 4)
            {
                if (Order == 1)
                {
                    SqlDetail1 = "SELECT TOP (100) PERCENT dbo.SubjectLevelOne.SubjectNameL1 AS Subject,dbo.SubjectLevelTwo.SubjectNameL2 AS SubjectD," +
                            "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks1)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=29)) as MaxEnglishWritten, " +
                            "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks1)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=30)) as MaxEnglishReading, " +
                           "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks1)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=31)) as MaxEnglishRecitation, " +
                           "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks1)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=32)) as MaxEnglishDictation, " +
                        "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks1)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=33)) as MaxHindiReading, " +
                         "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks1)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=34)) as MaxHindiRecitation, " +
                         "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks1)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=35)) as MaxHindiDictation, " +
                         "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks1)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=36)) as MaxHindiWritten, " +
                         "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks1)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=37)) as MaxMathsNumberWork, " +
                        "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks1)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=38)) as MaxEnglishI, " +
                        "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks1)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=45)) as MaxEngLangReadRecitaion, " +
                         "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks1)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=51)) as MaxEnglishLitDictation, " +
                         "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks1)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=52)) as MaxHindiLangReadRecitation, " +
                         "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks1)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=53)) as MaxHindiLitDictation, " +
                         "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks1)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=54)) as MaxMathsI, " +
                        "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks1)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=55)) as MaxScience, " +
                        "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks1)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=56)) as MaxSocialStudies, " +
                         "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks1)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=57)) as MaxComputer, " +
                        "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks1)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=63)) as MaxEnglishLanguage, " +
                          "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks1)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=64)) as MaxEnglishLiterature, " +
                         "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks1)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=65)) as MaxHindiLanguage, " +
                         "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks1)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=67)) as MaxPhysicsI, " +
                          "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks1)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=68)) as MaxChemistryI, " +
                          "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks1)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=69)) as MaxBiologyI, " +
                          "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks1)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=70)) as MaxHistoryCivics, " +
                        "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks1)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=71)) as MaxGeography, " +
                        "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks1)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=72)) as MaxHindiLiterature, " +
                        "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks1)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=73)) as MAxComputerArt, " +
                         "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks1)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=74)) as MaxHindi, " +
                         "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks1)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=76)) as MaxComputerPractical, " +
                        "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks1)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=81)) as MaxMathsWritten, " +
                         "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks1)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=82)) as MaxMathsOral, " +
                         "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks1)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=83)) as MaxEnglish, " +
                        "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks1)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=84)) as MaxMoralScience, " +
                         "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks1)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=85)) as MaxGeneralKnowledge, " +
                        "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks1)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=86)) as MaxEVS, " +
                        " dbo.ExamResult.ObtainMarks1 AS Obtain1,dbo.ExamResult.ObtainMarks2 AS Obtain2,dbo.ExamResult.ObtainMarks3 AS Obtain3,dbo.ExamResult.ObtainMarks4 AS Obtain4, dbo.ExamResult.ObtainMax1,dbo.ExamResult.ObtainMax2,dbo.ExamResult.ObtainMax3,dbo.ExamResult.ObtainMax4, dbo.ExamResult.StudentID, " +
                        " dbo.ExamSetupDetail.OrderNo,ExamResult.MaxMark1 as Max1,ExamResult.MaxMark2 as Max2,ExamResult.MaxMark3 as Max3,ExamResult.MaxMark4 as Max4,ExamResult.MinMark1 as Min1,ExamResult.MinMark2 as Min2,ExamResult.MinMark3 as Min3,ExamResult.MinMark4 as Min4 " +
                        " FROM dbo.ExamResult INNER JOIN dbo.SubjectLevelOne ON dbo.ExamResult.SubjectIDL1 = dbo.SubjectLevelOne.IdL1 INNER JOIN " +
                        " dbo.ExamSetupDetail ON dbo.SubjectLevelOne.IdL1 = dbo.ExamSetupDetail.SubjectIDL1 AND dbo.SubjectLevelOne.BranchID = dbo.ExamSetupDetail.BranchID AND " +
                        " dbo.SubjectLevelOne.CompID = dbo.ExamSetupDetail.CompID " +
                        " INNER JOIN dbo.SubjectLevelTwo ON dbo.ExamResult.SubjectIDL2 = dbo.SubjectLevelTwo.IdL2 AND dbo.ExamSetupDetail.SubjectIDL2 = dbo.SubjectLevelTwo.IdL2 " +
                        " INNER JOIN dbo.ExamSetupMaster ON dbo.ExamSetupDetail.ExamSetupID = dbo.ExamSetupMaster.ExamSetupID AND " +
                        " dbo.ExamSetupDetail.BranchID = dbo.ExamSetupMaster.BranchID AND dbo.ExamSetupDetail.CompID = dbo.ExamSetupMaster.CompID AND dbo.ExamResult.ClassID = dbo.ExamSetupMaster.ClassID AND ExamResult.SessionID = dbo.ExamSetupMaster.SessionID" +
                        " WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.ExamResult.StudentID IN (" + StudentIds + ")) " +
                        " AND (dbo.ExamResult.SessionID =" + SESSION_ID + ") AND (dbo.ExamSetupMaster.SessionID=" + SESSION_ID + ") AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND " +
                        " (dbo.ExamResult.BranchID =" + BRANCH_ID + ") AND (dbo.SubjectLevelOne.SubjectCategoryID = 1) AND (dbo.ExamSetupMaster.ExamID =" + ExamId + ")" +
                        " Order BY dbo.ExamResult.StudentID, dbo.ExamSetupDetail.OrderNo ";


                    SqlDetail2 = "SELECT TOP (100) PERCENT dbo.SubjectLevelOne.SubjectNameL1 AS Subject,dbo.SubjectLevelTwo.SubjectNameL2 AS SubjectD, " +
                              " dbo.ExamResult.ObtainMarks1 AS Obtain1,dbo.ExamResult.ObtainMarks2 AS Obtain2,dbo.ExamResult.ObtainMarks3 AS Obtain3,dbo.ExamResult.ObtainMarks4 AS Obtain4, dbo.ExamResult.StudentID, " +
                                " dbo.ExamSetupDetail.OrderNo,ExamResult.MaxMark1 as Max1,ExamResult.MaxMark2 as Max2,ExamResult.MaxMark3 as Max3,ExamResult.MaxMark4 as Max4,ExamResult.MinMark1 as Min1,ExamResult.MinMark2 as Min2,ExamResult.MinMark3 as Min3,ExamResult.MinMark4 as Min4 " +
                                " FROM dbo.ExamResult INNER JOIN dbo.SubjectLevelOne ON dbo.ExamResult.SubjectIDL1 = dbo.SubjectLevelOne.IdL1 INNER JOIN " +
                                " dbo.ExamSetupDetail ON dbo.SubjectLevelOne.IdL1 = dbo.ExamSetupDetail.SubjectIDL1 AND dbo.SubjectLevelOne.BranchID = dbo.ExamSetupDetail.BranchID AND " +
                                " dbo.SubjectLevelOne.CompID = dbo.ExamSetupDetail.CompID " +
                                " INNER JOIN dbo.SubjectLevelTwo ON dbo.ExamResult.SubjectIDL2 = dbo.SubjectLevelTwo.IdL2 AND dbo.ExamSetupDetail.SubjectIDL2 = dbo.SubjectLevelTwo.IdL2 " +
                                " INNER JOIN dbo.ExamSetupMaster ON dbo.ExamSetupDetail.ExamSetupID = dbo.ExamSetupMaster.ExamSetupID AND " +
                                " dbo.ExamSetupDetail.BranchID = dbo.ExamSetupMaster.BranchID AND dbo.ExamSetupDetail.CompID = dbo.ExamSetupMaster.CompID AND dbo.ExamResult.ClassID = dbo.ExamSetupMaster.ClassID " +
                                " WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.ExamResult.StudentID IN (" + StudentIds + ")) " +
                                " AND (dbo.ExamResult.SessionID =" + SESSION_ID + ") AND (dbo.ExamSetupMaster.SessionID =" + SESSION_ID + ") AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND " +
                                " (dbo.ExamResult.BranchID =" + BRANCH_ID + ") AND (dbo.SubjectLevelOne.SubjectCategoryID = 2) AND (dbo.ExamSetupMaster.ExamID =" + ExamId + ")" +
                                " Order BY dbo.ExamResult.StudentID, dbo.ExamSetupDetail.OrderNo ";


                    SqlDetail3 = "SELECT TOP (100) PERCENT dbo.SubjectLevelOne.SubjectNameL1 AS Subject,dbo.SubjectLevelTwo.SubjectNameL2 AS SubjectD,dbo.ExamResult.ObtainMarks1 AS Obtain1,dbo.ExamResult.ObtainMarks2 AS Obtain2,dbo.ExamResult.ObtainMarks3 AS Obtain3,dbo.ExamResult.ObtainMarks4 AS Obtain4, dbo.ExamResult.StudentID, " +
                                " dbo.ExamSetupDetail.OrderNo,ExamResult.MaxMark1 as Max1,ExamResult.MaxMark2 as Max2,ExamResult.MaxMark3 as Max3,ExamResult.MaxMark4 as Max4,ExamResult.MinMark1 as Min1,ExamResult.MinMark2 as Min2,ExamResult.MinMark3 as Min3,ExamResult.MinMark4 as Min4 " +
                                " FROM dbo.ExamResult INNER JOIN dbo.SubjectLevelOne ON dbo.ExamResult.SubjectIDL1 = dbo.SubjectLevelOne.IdL1 INNER JOIN " +
                                " dbo.ExamSetupDetail ON dbo.SubjectLevelOne.IdL1 = dbo.ExamSetupDetail.SubjectIDL1 AND dbo.SubjectLevelOne.BranchID = dbo.ExamSetupDetail.BranchID AND " +
                                " dbo.SubjectLevelOne.CompID = dbo.ExamSetupDetail.CompID " +
                                " INNER JOIN dbo.SubjectLevelTwo ON dbo.ExamResult.SubjectIDL2 = dbo.SubjectLevelTwo.IdL2 AND dbo.ExamSetupDetail.SubjectIDL2 = dbo.SubjectLevelTwo.IdL2 " +
                                " INNER JOIN dbo.ExamSetupMaster ON dbo.ExamSetupDetail.ExamSetupID = dbo.ExamSetupMaster.ExamSetupID AND " +
                                " dbo.ExamSetupDetail.BranchID = dbo.ExamSetupMaster.BranchID AND dbo.ExamSetupDetail.CompID = dbo.ExamSetupMaster.CompID AND dbo.ExamResult.ClassID = dbo.ExamSetupMaster.ClassID " +
                               " WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.ExamResult.StudentID IN (" + StudentIds + ")) " +
                               " AND (dbo.ExamResult.SessionID =" + SESSION_ID + ") AND (dbo.ExamSetupMaster.SessionID =" + SESSION_ID + ") AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND " +
                               " (dbo.ExamResult.BranchID =" + BRANCH_ID + ") AND (dbo.SubjectLevelOne.SubjectCategoryID = 3) AND (dbo.ExamSetupMaster.ExamID =" + ExamId + ")" +
                               " Order BY dbo.ExamResult.StudentID, dbo.ExamSetupDetail.OrderNo ";

                }
                if (Order == 2)
                {
                    SqlDetail1 = "SELECT TOP (100) PERCENT dbo.SubjectLevelOne.SubjectNameL1 AS Subject,dbo.SubjectLevelTwo.SubjectNameL2 AS SubjectD,  " +
                        "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks2)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=29)) as MaxEnglishWritten, " +
                            "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks2)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=30)) as MaxEnglishReading, " +
                           "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks2)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=31)) as MaxEnglishRecitation, " +
                           "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks2)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=32)) as MaxEnglishDictation, " +
                        "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks2)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=33)) as MaxHindiReading, " +
                         "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks2)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=34)) as MaxHindiRecitation, " +
                         "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks2)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=35)) as MaxHindiDictation, " +
                         "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks2)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=36)) as MaxHindiWritten, " +
                         "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks2)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=37)) as MaxMathsNumberWork, " +
                        "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks2)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=38)) as MaxEnglishI, " +
                         "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks2)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=45)) as MaxEngLangReadRecitaion, " +
                         "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks2)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=51)) as MaxEnglishLitDictation, " +
                         "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks2)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=52)) as MaxHindiLangReadRecitation, " +
                         "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks2)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=53)) as MaxHindiLitDictation, " +
                         "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks2)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=54)) as MaxMathsI, " +
                        "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks2)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=55)) as MaxScience, " +
                        "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks2)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=56)) as MaxSocialStudies, " +
                         "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks2)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=57)) as MaxComputer, " +
                        "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks2)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=63)) as MaxEnglishLanguage, " +
                          "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks2)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=64)) as MaxEnglishLiterature, " +
                         "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks2)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=65)) as MaxHindiLanguage, " +
                         "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks2)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=67)) as MaxPhysicsI, " +
                          "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks2)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=68)) as MaxChemistryI, " +
                          "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks2)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=69)) as MaxBiologyI, " +
                          "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks2)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=70)) as MaxHistoryCivics, " +
                        "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks2)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=71)) as MaxGeography, " +
                        "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks2)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=72)) as MaxHindiLiterature, " +
                        "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks2)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=73)) as MAxComputerArt, " +
                         "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks2)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=74)) as MaxHindi, " +
                         "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks2)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=76)) as MaxComputerPractical, " +
                        "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks2)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=81)) as MaxMathsWritten, " +
                         "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks2)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=82)) as MaxMathsOral, " +
                         "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks2)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=83)) as MaxEnglish, " +
                        "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks2)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=84)) as MaxMoralScience, " +
                         "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks2)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=85)) as MaxGeneralKnowledge, " +
                        "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks2)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=86)) as MaxEVS, " +
                       " dbo.ExamResult.ObtainMarks1 AS Obtain1,dbo.ExamResult.ObtainMarks2 AS Obtain2,dbo.ExamResult.ObtainMarks3 AS Obtain3,dbo.ExamResult.ObtainMarks4 AS Obtain4, dbo.ExamResult.StudentID, " +
                        " dbo.ExamSetupDetail.OrderNo,ExamResult.MaxMark1 as Max1,ExamResult.MaxMark2 as Max2,ExamResult.MaxMark3 as Max3,ExamResult.MaxMark4 as Max4,ExamResult.MinMark1 as Min1,ExamResult.MinMark2 as Min2,ExamResult.MinMark3 as Min3,ExamResult.MinMark4 as Min4 " +
                        " FROM dbo.ExamResult INNER JOIN dbo.SubjectLevelOne ON dbo.ExamResult.SubjectIDL1 = dbo.SubjectLevelOne.IdL1 INNER JOIN " +
                        " dbo.ExamSetupDetail ON dbo.SubjectLevelOne.IdL1 = dbo.ExamSetupDetail.SubjectIDL1 AND dbo.SubjectLevelOne.BranchID = dbo.ExamSetupDetail.BranchID AND " +
                        " dbo.SubjectLevelOne.CompID = dbo.ExamSetupDetail.CompID " +
                        " INNER JOIN dbo.SubjectLevelTwo ON dbo.ExamResult.SubjectIDL2 = dbo.SubjectLevelTwo.IdL2 AND dbo.ExamSetupDetail.SubjectIDL2 = dbo.SubjectLevelTwo.IdL2 " +
                        " INNER JOIN dbo.ExamSetupMaster ON dbo.ExamSetupDetail.ExamSetupID = dbo.ExamSetupMaster.ExamSetupID AND " +
                        " dbo.ExamSetupDetail.BranchID = dbo.ExamSetupMaster.BranchID AND dbo.ExamSetupDetail.CompID = dbo.ExamSetupMaster.CompID AND dbo.ExamResult.ClassID = dbo.ExamSetupMaster.ClassID AND ExamResult.SessionID = dbo.ExamSetupMaster.SessionID " +
                        " WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.ExamResult.StudentID IN (" + StudentIds + ")) " +
                        " AND (dbo.ExamResult.SessionID =" + SESSION_ID + ") AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND " +
                        " (dbo.ExamResult.BranchID =" + BRANCH_ID + ") AND (dbo.SubjectLevelOne.SubjectCategoryID = 1) AND (dbo.ExamSetupMaster.ExamID =" + ExamId + ")" +
                        " Order BY dbo.ExamResult.StudentID, dbo.ExamSetupDetail.OrderNo ";


                    SqlDetail2 = "SELECT TOP (100) PERCENT dbo.SubjectLevelOne.SubjectNameL1 AS Subject,dbo.SubjectLevelTwo.SubjectNameL2 AS SubjectD,dbo.ExamResult.ObtainMarks1 AS Obtain1,dbo.ExamResult.ObtainMarks2 AS Obtain2,dbo.ExamResult.ObtainMarks3 AS Obtain3,dbo.ExamResult.ObtainMarks4 AS Obtain4, dbo.ExamResult.StudentID, " +
                                " dbo.ExamSetupDetail.OrderNo,ExamResult.MaxMark1 as Max1,ExamResult.MaxMark2 as Max2,ExamResult.MaxMark3 as Max3,ExamResult.MaxMark4 as Max4,ExamResult.MinMark1 as Min1,ExamResult.MinMark2 as Min2,ExamResult.MinMark3 as Min3,ExamResult.MinMark4 as Min4 " +
                                " FROM dbo.ExamResult INNER JOIN dbo.SubjectLevelOne ON dbo.ExamResult.SubjectIDL1 = dbo.SubjectLevelOne.IdL1 INNER JOIN " +
                                " dbo.ExamSetupDetail ON dbo.SubjectLevelOne.IdL1 = dbo.ExamSetupDetail.SubjectIDL1 AND dbo.SubjectLevelOne.BranchID = dbo.ExamSetupDetail.BranchID AND " +
                                " dbo.SubjectLevelOne.CompID = dbo.ExamSetupDetail.CompID " +
                                " INNER JOIN dbo.SubjectLevelTwo ON dbo.ExamResult.SubjectIDL2 = dbo.SubjectLevelTwo.IdL2 AND dbo.ExamSetupDetail.SubjectIDL2 = dbo.SubjectLevelTwo.IdL2 " +
                                " INNER JOIN dbo.ExamSetupMaster ON dbo.ExamSetupDetail.ExamSetupID = dbo.ExamSetupMaster.ExamSetupID AND " +
                                " dbo.ExamSetupDetail.BranchID = dbo.ExamSetupMaster.BranchID AND dbo.ExamSetupDetail.CompID = dbo.ExamSetupMaster.CompID AND dbo.ExamResult.ClassID = dbo.ExamSetupMaster.ClassID " +
                                " WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.ExamResult.StudentID IN (" + StudentIds + ")) " +
                                " AND (dbo.ExamResult.SessionID =" + SESSION_ID + ") AND (dbo.ExamSetupMaster.SessionID =" + SESSION_ID + ") AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND " +
                                " (dbo.ExamResult.BranchID =" + BRANCH_ID + ") AND (dbo.SubjectLevelOne.SubjectCategoryID = 2) AND (dbo.ExamSetupMaster.ExamID =" + ExamId + ")" +
                                " Order BY dbo.ExamResult.StudentID, dbo.ExamSetupDetail.OrderNo ";


                    SqlDetail3 = "SELECT TOP (100) PERCENT dbo.SubjectLevelOne.SubjectNameL1 AS Subject,dbo.SubjectLevelTwo.SubjectNameL2 AS SubjectD,dbo.ExamResult.ObtainMarks1 AS Obtain1,dbo.ExamResult.ObtainMarks2 AS Obtain2,dbo.ExamResult.ObtainMarks3 AS Obtain3,dbo.ExamResult.ObtainMarks4 AS Obtain4, dbo.ExamResult.StudentID, " +
                                " dbo.ExamSetupDetail.OrderNo,ExamResult.MaxMark1 as Max1,ExamResult.MaxMark2 as Max2,ExamResult.MaxMark3 as Max3,ExamResult.MaxMark4 as Max4,ExamResult.MinMark1 as Min1,ExamResult.MinMark2 as Min2,ExamResult.MinMark3 as Min3,ExamResult.MinMark4 as Min4 " +
                                " FROM dbo.ExamResult INNER JOIN dbo.SubjectLevelOne ON dbo.ExamResult.SubjectIDL1 = dbo.SubjectLevelOne.IdL1 INNER JOIN " +
                                " dbo.ExamSetupDetail ON dbo.SubjectLevelOne.IdL1 = dbo.ExamSetupDetail.SubjectIDL1 AND dbo.SubjectLevelOne.BranchID = dbo.ExamSetupDetail.BranchID AND " +
                                " dbo.SubjectLevelOne.CompID = dbo.ExamSetupDetail.CompID " +
                                " INNER JOIN dbo.SubjectLevelTwo ON dbo.ExamResult.SubjectIDL2 = dbo.SubjectLevelTwo.IdL2 AND dbo.ExamSetupDetail.SubjectIDL2 = dbo.SubjectLevelTwo.IdL2 " +
                                " INNER JOIN dbo.ExamSetupMaster ON dbo.ExamSetupDetail.ExamSetupID = dbo.ExamSetupMaster.ExamSetupID AND " +
                                " dbo.ExamSetupDetail.BranchID = dbo.ExamSetupMaster.BranchID AND dbo.ExamSetupDetail.CompID = dbo.ExamSetupMaster.CompID AND dbo.ExamResult.ClassID = dbo.ExamSetupMaster.ClassID " +
                               " WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.ExamResult.StudentID IN (" + StudentIds + ")) " +
                               " AND (dbo.ExamResult.SessionID =" + SESSION_ID + ") AND (dbo.ExamSetupMaster.SessionID =" + SESSION_ID + ") AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND " +
                               " (dbo.ExamResult.BranchID =" + BRANCH_ID + ") AND (dbo.SubjectLevelOne.SubjectCategoryID = 3) AND (dbo.ExamSetupMaster.ExamID =" + ExamId + ")" +
                               " Order BY dbo.ExamResult.StudentID, dbo.ExamSetupDetail.OrderNo ";
                }
                if (Order == 3)
                {
                     SqlDetail1= "SELECT TOP (100) PERCENT dbo.SubjectLevelOne.SubjectNameL1 AS Subject,dbo.SubjectLevelTwo.SubjectNameL2 AS SubjectD, " +
                         "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks3)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=29)) as MaxEnglishWritten, " +
                            "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks3)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=30)) as MaxEnglishReading, " +
                           "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks3)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=31)) as MaxEnglishRecitation, " +
                           "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks3)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=32)) as MaxEnglishDictation, " +
                        "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks3)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=33)) as MaxHindiReading, " +
                         "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks3)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=34)) as MaxHindiRecitation, " +
                         "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks3)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=35)) as MaxHindiDictation, " +
                         "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks3)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=36)) as MaxHindiWritten, " +
                         "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks3)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=37)) as MaxMathsNumberWork, " +
                        "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks3)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=38)) as MaxEnglishI, " +
                         "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks3)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=45)) as MaxEngLangReadRecitaion, " +
                         "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks3)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=51)) as MaxEnglishLitDictation, " +
                         "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks3)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=52)) as MaxHindiLangReadRecitation, " +
                         "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks3)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=53)) as MaxHindiLitDictation, " +
                         "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks3)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=54)) as MaxMathsI, " +
                        "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks3)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=55)) as MaxScience, " +
                        "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks3)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=56)) as MaxSocialStudies, " +
                         "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks3)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=57)) as MaxComputer, " +
                        "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks3)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=63)) as MaxEnglishLanguage, " +
                          "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks3)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=64)) as MaxEnglishLiterature, " +
                         "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks3)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=65)) as MaxHindiLanguage, " +
                         "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks3)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=67)) as MaxPhysicsI, " +
                          "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks3)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=68)) as MaxChemistryI, " +
                          "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks3)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=69)) as MaxBiologyI, " +
                          "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks3)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=70)) as MaxHistoryCivics, " +
                        "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks3)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=71)) as MaxGeography, " +
                        "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks3)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=72)) as MaxHindiLiterature, " +
                        "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks3)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=73)) as MAxComputerArt, " +
                         "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks3)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=74)) as MaxHindi, " +
                         "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks3)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=76)) as MaxComputerPractical, " +
                        "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks3)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=81)) as MaxMathsWritten, " +
                         "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks3)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=82)) as MaxMathsOral, " +
                         "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks3)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=83)) as MaxEnglish, " +

                         "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks3)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=84)) as MaxMoralScience, " +
                         "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks3)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=85)) as MaxGeneralKnowledge, " +
                        "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks3)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL2=86)) as MaxEVS, " +

                        "  dbo.ExamResult.ObtainMarks1 AS Obtain1, dbo.ExamResult.ObtainMarks2 AS Obtain2,dbo.ExamResult.ObtainMarks3 AS Obtain3,dbo.ExamResult.ObtainMarks4 AS Obtain4, dbo.ExamResult.StudentID, " +
                        " dbo.ExamSetupDetail.OrderNo,ExamResult.MaxMark1 as Max1,ExamResult.MaxMark2 as Max2,ExamResult.MaxMark3 as Max3,ExamResult.MaxMark4 as Max4,ExamResult.MinMark1 as Min1,ExamResult.MinMark2 as Min2,ExamResult.MinMark3 as Min3,ExamResult.MinMark4 as Min4 " +
                        " FROM dbo.ExamResult INNER JOIN dbo.SubjectLevelOne ON dbo.ExamResult.SubjectIDL1 = dbo.SubjectLevelOne.IdL1 INNER JOIN " +
                        " dbo.ExamSetupDetail ON dbo.SubjectLevelOne.IdL1 = dbo.ExamSetupDetail.SubjectIDL1 AND dbo.SubjectLevelOne.BranchID = dbo.ExamSetupDetail.BranchID AND " +
                        " dbo.SubjectLevelOne.CompID = dbo.ExamSetupDetail.CompID " +
                        " INNER JOIN dbo.SubjectLevelTwo ON dbo.ExamResult.SubjectIDL2 = dbo.SubjectLevelTwo.IdL2 AND dbo.ExamSetupDetail.SubjectIDL2 = dbo.SubjectLevelTwo.IdL2 " +
                        " INNER JOIN dbo.ExamSetupMaster ON dbo.ExamSetupDetail.ExamSetupID = dbo.ExamSetupMaster.ExamSetupID AND " +
                        " dbo.ExamSetupDetail.BranchID = dbo.ExamSetupMaster.BranchID AND dbo.ExamSetupDetail.CompID = dbo.ExamSetupMaster.CompID AND dbo.ExamResult.ClassID = dbo.ExamSetupMaster.ClassID AND dbo.ExamResult.SessionID = dbo.ExamSetupMaster.SessionID " +
                        " WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.ExamResult.StudentID IN (" + StudentIds + ")) " +
                        " AND (dbo.ExamResult.SessionID =" + SESSION_ID + ") AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND " +
                        " (dbo.ExamResult.BranchID =" + BRANCH_ID + ") AND (dbo.SubjectLevelOne.SubjectCategoryID = 1) AND (dbo.ExamSetupMaster.ExamID =" + ExamId + ")" +
                        " Order BY dbo.ExamResult.StudentID, dbo.ExamSetupDetail.OrderNo ";


                    SqlDetail2 = "SELECT TOP (100) PERCENT dbo.SubjectLevelOne.SubjectNameL1 AS Subject,dbo.SubjectLevelTwo.SubjectNameL2 AS SubjectD,dbo.ExamResult.ObtainMarks1 AS Obtain1,dbo.ExamResult.ObtainMarks2 AS Obtain2,dbo.ExamResult.ObtainMarks3 AS Obtain3,dbo.ExamResult.ObtainMarks4 AS Obtain4, dbo.ExamResult.StudentID, " +
                               " dbo.ExamSetupDetail.OrderNo,ExamResult.MaxMark1 as Max1,ExamResult.MaxMark2 as Max2,ExamResult.MaxMark3 as Max3,ExamResult.MaxMark4 as Max4,ExamResult.MinMark1 as Min1,ExamResult.MinMark2 as Min2,ExamResult.MinMark3 as Min3,ExamResult.MinMark4 as Min4 " +
                               " FROM dbo.ExamResult INNER JOIN dbo.SubjectLevelOne ON dbo.ExamResult.SubjectIDL1 = dbo.SubjectLevelOne.IdL1 INNER JOIN " +
                               " dbo.ExamSetupDetail ON dbo.SubjectLevelOne.IdL1 = dbo.ExamSetupDetail.SubjectIDL1 AND dbo.SubjectLevelOne.BranchID = dbo.ExamSetupDetail.BranchID AND " +
                               " dbo.SubjectLevelOne.CompID = dbo.ExamSetupDetail.CompID " +
                               " INNER JOIN dbo.SubjectLevelTwo ON dbo.ExamResult.SubjectIDL2 = dbo.SubjectLevelTwo.IdL2 AND dbo.ExamSetupDetail.SubjectIDL2 = dbo.SubjectLevelTwo.IdL2 " +
                               " INNER JOIN dbo.ExamSetupMaster ON dbo.ExamSetupDetail.ExamSetupID = dbo.ExamSetupMaster.ExamSetupID AND " +
                               " dbo.ExamSetupDetail.BranchID = dbo.ExamSetupMaster.BranchID AND dbo.ExamSetupDetail.CompID = dbo.ExamSetupMaster.CompID AND dbo.ExamResult.ClassID = dbo.ExamSetupMaster.ClassID " +
                               " WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.ExamResult.StudentID IN (" + StudentIds + ")) " +
                               " AND (dbo.ExamResult.SessionID =" + SESSION_ID + ") AND (dbo.ExamSetupMaster.SessionID =" + SESSION_ID + ") AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND " +
                               " (dbo.ExamResult.BranchID =" + BRANCH_ID + ") AND (dbo.SubjectLevelOne.SubjectCategoryID = 2) AND (dbo.ExamSetupMaster.ExamID =" + ExamId + ")" +
                               " Order BY dbo.ExamResult.StudentID, dbo.ExamSetupDetail.OrderNo ";


                    SqlDetail3 = "SELECT TOP (100) PERCENT dbo.SubjectLevelOne.SubjectNameL1 AS Subject,dbo.SubjectLevelTwo.SubjectNameL2 AS SubjectD,dbo.ExamResult.ObtainMarks1 AS Obtain1,dbo.ExamResult.ObtainMarks2 AS Obtain2,dbo.ExamResult.ObtainMarks3 AS Obtain3,dbo.ExamResult.ObtainMarks4 AS Obtain4, dbo.ExamResult.StudentID, " +
                                " dbo.ExamSetupDetail.OrderNo,ExamResult.MaxMark1 as Max1,ExamResult.MaxMark2 as Max2,ExamResult.MaxMark3 as Max3,ExamResult.MaxMark4 as Max4,ExamResult.MinMark1 as Min1,ExamResult.MinMark2 as Min2,ExamResult.MinMark3 as Min3,ExamResult.MinMark4 as Min4 " +
                                " FROM dbo.ExamResult INNER JOIN dbo.SubjectLevelOne ON dbo.ExamResult.SubjectIDL1 = dbo.SubjectLevelOne.IdL1 INNER JOIN " +
                                " dbo.ExamSetupDetail ON dbo.SubjectLevelOne.IdL1 = dbo.ExamSetupDetail.SubjectIDL1 AND dbo.SubjectLevelOne.BranchID = dbo.ExamSetupDetail.BranchID AND " +
                                " dbo.SubjectLevelOne.CompID = dbo.ExamSetupDetail.CompID " +
                                " INNER JOIN dbo.SubjectLevelTwo ON dbo.ExamResult.SubjectIDL2 = dbo.SubjectLevelTwo.IdL2 AND dbo.ExamSetupDetail.SubjectIDL2 = dbo.SubjectLevelTwo.IdL2 " +
                                " INNER JOIN dbo.ExamSetupMaster ON dbo.ExamSetupDetail.ExamSetupID = dbo.ExamSetupMaster.ExamSetupID AND " +
                                " dbo.ExamSetupDetail.BranchID = dbo.ExamSetupMaster.BranchID AND dbo.ExamSetupDetail.CompID = dbo.ExamSetupMaster.CompID AND dbo.ExamResult.ClassID = dbo.ExamSetupMaster.ClassID " +
                               " WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.ExamResult.StudentID IN (" + StudentIds + ")) " +
                               " AND (dbo.ExamResult.SessionID =" + SESSION_ID + ") AND (dbo.ExamSetupMaster.SessionID =" + SESSION_ID + ") AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND " +
                               " (dbo.ExamResult.BranchID =" + BRANCH_ID + ") AND (dbo.SubjectLevelOne.SubjectCategoryID = 3) AND (dbo.ExamSetupMaster.ExamID =" + ExamId + ")" +
                               " Order BY dbo.ExamResult.StudentID, dbo.ExamSetupDetail.OrderNo ";
                }
                if (Order == 4)
                {
                    SqlDetail1 = "SELECT TOP (100) PERCENT dbo.SubjectLevelOne.SubjectNameL1 AS Subject, dbo.ExamResult.ObtainMarks1 AS Obtain1,dbo.ExamResult.ObtainMarks2 AS Obtain2,dbo.ExamResult.ObtainMarks3 AS Obtain3,dbo.ExamResult.ObtainMarks4 AS Obtain4, dbo.ExamResult.StudentID, " +
                                " dbo.ExamSetupDetail.OrderNo,ExamResult.MaxMark1 as Max1,ExamResult.MaxMark2 as Max2,ExamResult.MaxMark3 as Max3,ExamResult.MaxMark4 as Max4,ExamResult.MinMark1 as Min1,ExamResult.MinMark2 as Min2,ExamResult.MinMark3 as Min3,ExamResult.MinMark4 as Min4 " +
                                " FROM dbo.ExamResult INNER JOIN dbo.SubjectLevelOne ON dbo.ExamResult.SubjectIDL1 = dbo.SubjectLevelOne.IdL1 INNER JOIN " +
                                " dbo.ExamSetupDetail ON dbo.SubjectLevelOne.IdL1 = dbo.ExamSetupDetail.SubjectIDL1 AND dbo.SubjectLevelOne.BranchID = dbo.ExamSetupDetail.BranchID AND " +
                                " dbo.SubjectLevelOne.CompID = dbo.ExamSetupDetail.CompID INNER JOIN dbo.ExamSetupMaster ON dbo.ExamSetupDetail.ExamSetupID = dbo.ExamSetupMaster.ExamSetupID AND " +
                                " dbo.ExamSetupDetail.BranchID = dbo.ExamSetupMaster.BranchID AND dbo.ExamSetupDetail.CompID = dbo.ExamSetupMaster.CompID AND dbo.ExamResult.ClassID = dbo.ExamSetupMaster.ClassID AND dbo.ExamResult.SessionID = dbo.ExamSetupMaster.SessionID " +
                                " WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.ExamResult.StudentID IN (" + StudentIds + ")) " +
                                " AND (dbo.ExamResult.SessionID =" + SESSION_ID + ") AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND " +
                                " (dbo.ExamResult.BranchID =" + BRANCH_ID + ") AND (dbo.SubjectLevelOne.SubjectCategoryID = 1) AND (dbo.ExamSetupMaster.ExamID =" + ExamId + ")" +
                                " Order BY dbo.ExamResult.StudentID, dbo.ExamSetupDetail.OrderNo ";


                    SqlDetail2 = "SELECT TOP (100) PERCENT dbo.SubjectLevelOne.SubjectNameL1 AS Subject, dbo.ExamResult.ObtainMarks1 AS Obtain1,dbo.ExamResult.ObtainMarks2 AS Obtain2,dbo.ExamResult.ObtainMarks3 AS Obtain3,dbo.ExamResult.ObtainMarks4 AS Obtain4, dbo.ExamResult.StudentID, " +
                                " dbo.ExamSetupDetail.OrderNo FROM dbo.ExamResult INNER JOIN dbo.SubjectLevelOne ON dbo.ExamResult.SubjectIDL1 = dbo.SubjectLevelOne.IdL1 INNER JOIN " +
                                " dbo.ExamSetupDetail ON dbo.SubjectLevelOne.IdL1 = dbo.ExamSetupDetail.SubjectIDL1 AND dbo.SubjectLevelOne.BranchID = dbo.ExamSetupDetail.BranchID AND " +
                                " dbo.SubjectLevelOne.CompID = dbo.ExamSetupDetail.CompID INNER JOIN dbo.ExamSetupMaster ON dbo.ExamSetupDetail.ExamSetupID = dbo.ExamSetupMaster.ExamSetupID AND " +
                                " dbo.ExamSetupDetail.BranchID = dbo.ExamSetupMaster.BranchID AND dbo.ExamSetupDetail.CompID = dbo.ExamSetupMaster.CompID AND dbo.ExamResult.ClassID = dbo.ExamSetupMaster.ClassID AND dbo.ExamResult.SessionID = dbo.ExamSetupMaster.SessionID " +
                                " WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.ExamResult.StudentID IN (" + StudentIds + ")) " +
                                " AND (dbo.ExamResult.SessionID =" + SESSION_ID + ") AND (dbo.ExamSetupMaster.SessionID =" + SESSION_ID + ") AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND " +
                                " (dbo.ExamResult.BranchID =" + BRANCH_ID + ") AND (dbo.SubjectLevelOne.SubjectCategoryID = 2) AND (dbo.ExamSetupMaster.ExamID =" + ExamId + ")" +
                                " Order BY dbo.ExamResult.StudentID, dbo.ExamSetupDetail.OrderNo ";


                    SqlDetail3 = "SELECT TOP (100) PERCENT dbo.SubjectLevelOne.SubjectNameL1 AS Subject, dbo.ExamResult.ObtainMarks1 AS Obtain1,dbo.ExamResult.ObtainMarks2 AS Obtain2,dbo.ExamResult.ObtainMarks3 AS Obtain3,dbo.ExamResult.ObtainMarks4 AS Obtain4, dbo.ExamResult.StudentID, " +
                               " dbo.ExamSetupDetail.OrderNo FROM dbo.ExamResult INNER JOIN dbo.SubjectLevelOne ON dbo.ExamResult.SubjectIDL1 = dbo.SubjectLevelOne.IdL1 INNER JOIN " +
                               " dbo.ExamSetupDetail ON dbo.SubjectLevelOne.IdL1 = dbo.ExamSetupDetail.SubjectIDL1 AND dbo.SubjectLevelOne.BranchID = dbo.ExamSetupDetail.BranchID AND " +
                               " dbo.SubjectLevelOne.CompID = dbo.ExamSetupDetail.CompID INNER JOIN dbo.ExamSetupMaster ON dbo.ExamSetupDetail.ExamSetupID = dbo.ExamSetupMaster.ExamSetupID AND " +
                               " dbo.ExamSetupDetail.BranchID = dbo.ExamSetupMaster.BranchID AND dbo.ExamSetupDetail.CompID = dbo.ExamSetupMaster.CompID AND dbo.ExamResult.ClassID = dbo.ExamSetupMaster.ClassID AND dbo.ExamResult.SessionID = dbo.ExamSetupMaster.SessionID " +
                               " WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.ExamResult.StudentID IN (" + StudentIds + ")) " +
                               " AND (dbo.ExamResult.SessionID =" + SESSION_ID + ") AND (dbo.ExamSetupMaster.SessionID =" + SESSION_ID + ") AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND " +
                               " (dbo.ExamResult.BranchID =" + BRANCH_ID + ") AND (dbo.SubjectLevelOne.SubjectCategoryID = 3) AND (dbo.ExamSetupMaster.ExamID =" + ExamId + ")" +
                               " Order BY dbo.ExamResult.StudentID, dbo.ExamSetupDetail.OrderNo ";
                }
            }

            #endregion
        }

        public void ReturnStJosephExamMarksheetSql()
        {
            #region  St Joseph
            if (COMPANY_ID == 5)
            {
                if (Order == 1)
                {
                    SqlDetail1 = "SELECT TOP (100) PERCENT dbo.SubjectLevelOne.SubjectNameL1 AS Subject," +

                    "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks1)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL1=1)) as MaxEnglishWritten, " +
                        "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks1)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL1=2)) as MaxEnglishReading, " +
                        "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks1)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL1=3)) as MaxEnglishRecitation, " +
                        "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks1)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL1=4)) as MaxHindiWritten, " +
                        "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks1)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL1=5)) as MaxHindiReading, " +
                        "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks1)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL1=6)) as MaxHindiRhymsTelling, " +
                       "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks1)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                       " AND (dbo.ExamResult.SubjectIDL1=6)) as MaxHindiRecitation, " +
                         "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks1)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                       " AND (dbo.ExamResult.SubjectIDL1=7)) as MaxMathsWritten, " +
                         "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks1)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                       " AND (dbo.ExamResult.SubjectIDL1=8)) as MaxMathsOral, " +
                        "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks1)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                       " AND (dbo.ExamResult.SubjectIDL1=17)) as MaxEnglishDictation , " +
                         "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks1)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                       " AND (dbo.ExamResult.SubjectIDL1= 18)) as MaxHindiDictation, " +
                       "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks1)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                       " AND (dbo.ExamResult.SubjectIDL1=22)) as MaxEnglishLanguage , " +
                         "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks1)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                       " AND (dbo.ExamResult.SubjectIDL1= 23)) as MaxEnglishLiterature, " +
                        "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks1)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                       " AND (dbo.ExamResult.SubjectIDL1=24)) as MaxHindiLanguage , " +
                         "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks1)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                       " AND (dbo.ExamResult.SubjectIDL1= 25)) as MaxHindiLiterature, " +
                        "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks1)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                       " AND (dbo.ExamResult.SubjectIDL1=26)) as MaxMaths , " +
                         "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks1)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                       " AND (dbo.ExamResult.SubjectIDL1= 27)) as MaxScience, " +
                        "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks1)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                       " AND (dbo.ExamResult.SubjectIDL1=28)) as MaxSocialStudies , " +
                         "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks1)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                       " AND (dbo.ExamResult.SubjectIDL1= 29)) as MaxComputer, " +
                        "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks1)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                       " AND (dbo.ExamResult.SubjectIDL1= 30)) as MaxMoralKnowledge , " +
                       "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks1)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                       " AND (dbo.ExamResult.SubjectIDL1= 31)) as MaxGK , " +
                        "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks1)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                       " AND (dbo.ExamResult.SubjectIDL1= 34)) as MaxPhysicsI, " +
                        "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks1)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                       " AND (dbo.ExamResult.SubjectIDL1= 35)) as MaxChemistryI, " +
                       "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks1)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                       " AND (dbo.ExamResult.SubjectIDL1= 36)) as MaxBiologyI, " +
                       "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks1)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                       " AND (dbo.ExamResult.SubjectIDL1= 37)) as MaxHistoryCivics, " +
                         "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks1)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                       " AND (dbo.ExamResult.SubjectIDL1= 38)) as MaxGeography, " +
                         "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks1)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                       " AND (dbo.ExamResult.SubjectIDL1= 39)) as MaxMoralScience , " +
                       "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks1)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                       " AND (dbo.ExamResult.SubjectIDL1= 40)) as MaxGeneralKnowledge, " +
                       "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks1)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                       " AND (dbo.ExamResult.SubjectIDL1= 42)) as MaxHindi, " +
                          "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks1)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                       " AND (dbo.ExamResult.SubjectIDL1= 43)) as MaxComputerPractical, " +
                       "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks1)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                       " AND (dbo.ExamResult.SubjectIDL1= 44)) as MaxEnglishOral, " +
                         "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks1)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                       " AND (dbo.ExamResult.SubjectIDL1= 45)) as MaxHindioral, " +
                        "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks1)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                       " AND (dbo.ExamResult.SubjectIDL1= 48)) as MAxComputerArt, " +
                       "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks1)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                       " AND (dbo.ExamResult.SubjectIDL1=49)) as MaxCivicsI, " +
                       "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks1)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                       " AND (dbo.ExamResult.SubjectIDL1=50)) as MaxArtI, " +
                        "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks1)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                       " AND (dbo.ExamResult.SubjectIDL1=51)) as MaxArtII, " +
                        "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks1)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                       " AND (dbo.ExamResult.SubjectIDL1=52)) as MaxPhysicsII, " +
                      "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks1)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                       " AND (dbo.ExamResult.SubjectIDL1=54)) as MaxEVS, " +
                       "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks1)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                       " AND (dbo.ExamResult.SubjectIDL1=53)) as MaxMusic, " +
                        "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks1)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                       " AND (dbo.ExamResult.SubjectIDL1=43)) as MaxComputerPhyEdu, " +


                     " dbo.ExamResult.ObtainMarks1 AS Obtain1,dbo.ExamResult.ObtainMarks2 AS Obtain2,dbo.ExamResult.ObtainMarks3 AS Obtain3,dbo.ExamResult.ObtainMarks4 AS Obtain4, dbo.ExamResult.StudentID, " +
                                " dbo.ExamSetupDetail.OrderNo,dbo.ExamResult.Grade AS Grade1, dbo.ExamResult.ObtainGrade2 AS Grade2,dbo.ExamResult.ObtainGrade3 AS Grade3,dbo.ExamResult.ObtainGrade4 AS Grade4, ExamResult.MaxMark1 as Max1,ExamResult.MaxMark2 as Max2,ExamResult.MaxMark3 as Max3,ExamResult.MaxMark4 as Max4,ExamResult.MinMark1 as Min1,ExamResult.MinMark2 as Min2,ExamResult.MinMark3 as Min3,ExamResult.MinMark4 as Min4 " +
                                " FROM dbo.ExamResult INNER JOIN dbo.SubjectLevelOne ON dbo.ExamResult.SubjectIDL1 = dbo.SubjectLevelOne.IdL1 INNER JOIN " +
                                " dbo.ExamSetupDetail ON dbo.SubjectLevelOne.IdL1 = dbo.ExamSetupDetail.SubjectIDL1 AND dbo.SubjectLevelOne.BranchID = dbo.ExamSetupDetail.BranchID AND " +
                                " dbo.SubjectLevelOne.CompID = dbo.ExamSetupDetail.CompID INNER JOIN dbo.ExamSetupMaster ON dbo.ExamSetupDetail.ExamSetupID = dbo.ExamSetupMaster.ExamSetupID AND " +
                                " dbo.ExamSetupDetail.BranchID = dbo.ExamSetupMaster.BranchID AND dbo.ExamSetupDetail.CompID = dbo.ExamSetupMaster.CompID AND dbo.ExamResult.ClassID = dbo.ExamSetupMaster.ClassID AND dbo.ExamResult.SessionID = dbo.ExamSetupMaster.SessionID " +
                                " WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.ExamResult.StudentID IN (" + StudentIds + ")) " +
                                " AND (dbo.ExamResult.SessionID =" + SESSION_ID + ") AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND " +
                                " (dbo.ExamResult.BranchID =" + BRANCH_ID + ") AND (dbo.SubjectLevelOne.SubjectCategoryID = 1) AND (dbo.ExamSetupMaster.ExamID =" + ExamId + ")" +
                                " ORDER BY dbo.ExamResult.StudentID, dbo.ExamSetupDetail.OrderNo ";


                    SqlDetail2 = "SELECT TOP (100) PERCENT dbo.SubjectLevelOne.SubjectNameL1 AS Subject, dbo.ExamResult.ObtainMarks1 AS Obtain1,dbo.ExamResult.ObtainMarks2 AS Obtain2,dbo.ExamResult.ObtainMarks3 AS Obtain3,dbo.ExamResult.ObtainMarks4 AS Obtain4, dbo.ExamResult.StudentID, " +
                                " dbo.ExamSetupDetail.OrderNo FROM dbo.ExamResult INNER JOIN dbo.SubjectLevelOne ON dbo.ExamResult.SubjectIDL1 = dbo.SubjectLevelOne.IdL1 INNER JOIN " +
                                " dbo.ExamSetupDetail ON dbo.SubjectLevelOne.IdL1 = dbo.ExamSetupDetail.SubjectIDL1 AND dbo.SubjectLevelOne.BranchID = dbo.ExamSetupDetail.BranchID AND " +
                                " dbo.SubjectLevelOne.CompID = dbo.ExamSetupDetail.CompID INNER JOIN dbo.ExamSetupMaster ON dbo.ExamSetupDetail.ExamSetupID = dbo.ExamSetupMaster.ExamSetupID AND " +
                                " dbo.ExamSetupDetail.BranchID = dbo.ExamSetupMaster.BranchID AND dbo.ExamSetupDetail.CompID = dbo.ExamSetupMaster.CompID AND dbo.ExamResult.ClassID = dbo.ExamSetupMaster.ClassID AND dbo.ExamResult.SessionID = dbo.ExamSetupMaster.SessionID " +
                                " WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.ExamResult.StudentID IN (" + StudentIds + ")) " +
                                " AND (dbo.ExamResult.SessionID =" + SESSION_ID + ") AND (dbo.ExamSetupMaster.SessionID =" + SESSION_ID + ") AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND " +
                                " (dbo.ExamResult.BranchID =" + BRANCH_ID + ") AND (dbo.SubjectLevelOne.SubjectCategoryID = 2) AND (dbo.ExamSetupMaster.ExamID =" + ExamId + ")" +
                                " ORDER BY dbo.ExamResult.StudentID, dbo.ExamSetupDetail.OrderNo ";


                    SqlDetail3 = "SELECT TOP (100) PERCENT dbo.SubjectLevelOne.SubjectNameL1 AS Subject, dbo.ExamResult.ObtainMarks1 AS Obtain1,dbo.ExamResult.ObtainMarks2 AS Obtain2,dbo.ExamResult.ObtainMarks3 AS Obtain3,dbo.ExamResult.ObtainMarks4 AS Obtain4, dbo.ExamResult.StudentID, " +
                               " dbo.ExamSetupDetail.OrderNo FROM dbo.ExamResult INNER JOIN dbo.SubjectLevelOne ON dbo.ExamResult.SubjectIDL1 = dbo.SubjectLevelOne.IdL1 INNER JOIN " +
                               " dbo.ExamSetupDetail ON dbo.SubjectLevelOne.IdL1 = dbo.ExamSetupDetail.SubjectIDL1 AND dbo.SubjectLevelOne.BranchID = dbo.ExamSetupDetail.BranchID AND " +
                               " dbo.SubjectLevelOne.CompID = dbo.ExamSetupDetail.CompID INNER JOIN dbo.ExamSetupMaster ON dbo.ExamSetupDetail.ExamSetupID = dbo.ExamSetupMaster.ExamSetupID AND " +
                               " dbo.ExamSetupDetail.BranchID = dbo.ExamSetupMaster.BranchID AND dbo.ExamSetupDetail.CompID = dbo.ExamSetupMaster.CompID AND dbo.ExamResult.ClassID = dbo.ExamSetupMaster.ClassID AND dbo.ExamResult.SessionID = dbo.ExamSetupMaster.SessionID " +
                               " WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.ExamResult.StudentID IN (" + StudentIds + ")) " +
                               " AND (dbo.ExamResult.SessionID =" + SESSION_ID + ") AND (dbo.ExamSetupMaster.SessionID =" + SESSION_ID + ") AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND " +
                               " (dbo.ExamResult.BranchID =" + BRANCH_ID + ") AND (dbo.SubjectLevelOne.SubjectCategoryID = 3) AND (dbo.ExamSetupMaster.ExamID =" + ExamId + ")" +
                               " ORDER BY dbo.ExamResult.StudentID, dbo.ExamSetupDetail.OrderNo ";

                }
                if (Order == 2)
                {
                    SqlDetail1 = "SELECT TOP (100) PERCENT dbo.SubjectLevelOne.SubjectNameL1 AS Subject, " +
                         "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks2)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL1=1)) as MaxEnglishWritten, " +
                        "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks2)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL1=2)) as MaxEnglishReading, " +
                        "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks2)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL1=3)) as MaxEnglishRecitation, " +
                        "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks2)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL1=4)) as MaxHindiWritten, " +
                        "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks2)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL1=5)) as MaxHindiReading, " +
                        "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks2)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL1=6)) as MaxHindiRhymsTelling, " +
                       "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks2)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                       " AND (dbo.ExamResult.SubjectIDL1=6)) as MaxHindiRecitation, " +
                         "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks2)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                       " AND (dbo.ExamResult.SubjectIDL1=7)) as MaxMathsWritten, " +
                         "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks2)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                       " AND (dbo.ExamResult.SubjectIDL1=8)) as MaxMathsOral, " +
                        "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks2)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                       " AND (dbo.ExamResult.SubjectIDL1=17)) as MaxEnglishDictation , " +
                         "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks2)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                       " AND (dbo.ExamResult.SubjectIDL1= 18)) as MaxHindiDictation, " +
                        "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks2)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                       " AND (dbo.ExamResult.SubjectIDL1=22)) as MaxEnglishLanguage , " +
                         "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks2)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                       " AND (dbo.ExamResult.SubjectIDL1= 23)) as MaxEnglishLiterature, " +
                        "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks2)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                       " AND (dbo.ExamResult.SubjectIDL1=24)) as MaxHindiLanguage , " +
                         "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks2)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                       " AND (dbo.ExamResult.SubjectIDL1= 25)) as MaxHindiLiterature, " +
                        "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks2)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                       " AND (dbo.ExamResult.SubjectIDL1=26)) as MaxMaths , " +
                         "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks2)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                       " AND (dbo.ExamResult.SubjectIDL1= 27)) as MaxScience, " +
                        "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks2)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                       " AND (dbo.ExamResult.SubjectIDL1=28)) as MaxSocialStudies , " +
                         "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks2)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                       " AND (dbo.ExamResult.SubjectIDL1= 29)) as MaxComputer, " +
                        "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks2)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                       " AND (dbo.ExamResult.SubjectIDL1= 30)) as MaxMoralKnowledge , " +
                       "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks2)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                       " AND (dbo.ExamResult.SubjectIDL1= 31)) as MaxGK , " +
                        "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks2)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                       " AND (dbo.ExamResult.SubjectIDL1= 34)) as MaxPhysicsI, " +
                        "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks2)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                       " AND (dbo.ExamResult.SubjectIDL1= 35)) as MaxChemistryI, " +
                       "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks2)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                       " AND (dbo.ExamResult.SubjectIDL1= 36)) as MaxBiologyI, " +
                       "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks2)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                       " AND (dbo.ExamResult.SubjectIDL1= 37)) as MaxHistoryCivics, " +
                         "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks2)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                       " AND (dbo.ExamResult.SubjectIDL1= 38)) as MaxGeography, " +
                         "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks2)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                       " AND (dbo.ExamResult.SubjectIDL1= 39)) as MaxMoralScience , " +
                       "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks2)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                       " AND (dbo.ExamResult.SubjectIDL1= 40)) as MaxGeneralKnowledge, " +
                       "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks2)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                       " AND (dbo.ExamResult.SubjectIDL1= 42)) as MaxHindi, " +
                          "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks2)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                       " AND (dbo.ExamResult.SubjectIDL1= 43)) as MaxComputerPractical, " +
                       "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks2)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                       " AND (dbo.ExamResult.SubjectIDL1= 44)) as MaxEnglishOral, " +
                         "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks2)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                       " AND (dbo.ExamResult.SubjectIDL1= 45)) as MaxHindioral, " +
                         "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks2)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                       " AND (dbo.ExamResult.SubjectIDL1= 48)) as MAxComputerArt, " +
                        "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks2)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                       " AND (dbo.ExamResult.SubjectIDL1=49)) as MaxCivicsI, " +
                       "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks2)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                       " AND (dbo.ExamResult.SubjectIDL1=50)) as MaxArtI, " +
                        "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks2)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                       " AND (dbo.ExamResult.SubjectIDL1=51)) as MaxArtII, " +
                        "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks2)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                       " AND (dbo.ExamResult.SubjectIDL1=52)) as MaxPhysicsII, " +
                       "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks2)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                       " AND (dbo.ExamResult.SubjectIDL1=54)) as MaxEVS, " +
                       "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks2)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                       " AND (dbo.ExamResult.SubjectIDL1=53)) as MaxMusic, " +
                        "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks2)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                       " AND (dbo.ExamResult.SubjectIDL1=43)) as MaxComputerPhyEdu, " +


                            " dbo.ExamResult.ObtainMarks1 AS Obtain1,dbo.ExamResult.ObtainMarks2 AS Obtain2,dbo.ExamResult.ObtainMarks3 AS Obtain3,dbo.ExamResult.ObtainMarks4 AS Obtain4, dbo.ExamResult.StudentID, " +
                            " dbo.ExamSetupDetail.OrderNo,dbo.ExamResult.Grade AS Grade1, dbo.ExamResult.ObtainGrade2 AS Grade2,dbo.ExamResult.ObtainGrade3 AS Grade3,dbo.ExamResult.ObtainGrade4 AS Grade4,ExamResult.MaxMark1 as Max1,ExamResult.MaxMark2 as Max2,ExamResult.MaxMark3 as Max3,ExamResult.MaxMark4 as Max4,ExamResult.MinMark1 as Min1,ExamResult.MinMark2 as Min2,ExamResult.MinMark3 as Min3,ExamResult.MinMark4 as Min4" +
                            " FROM dbo.ExamResult INNER JOIN dbo.SubjectLevelOne ON dbo.ExamResult.SubjectIDL1 = dbo.SubjectLevelOne.IdL1 INNER JOIN " +
                            " dbo.ExamSetupDetail ON dbo.SubjectLevelOne.IdL1 = dbo.ExamSetupDetail.SubjectIDL1 AND dbo.SubjectLevelOne.BranchID = dbo.ExamSetupDetail.BranchID AND " +
                            " dbo.SubjectLevelOne.CompID = dbo.ExamSetupDetail.CompID INNER JOIN dbo.ExamSetupMaster ON dbo.ExamSetupDetail.ExamSetupID = dbo.ExamSetupMaster.ExamSetupID AND " +
                            " dbo.ExamSetupDetail.BranchID = dbo.ExamSetupMaster.BranchID AND dbo.ExamSetupDetail.CompID = dbo.ExamSetupMaster.CompID AND dbo.ExamResult.ClassID = dbo.ExamSetupMaster.ClassID AND dbo.ExamResult.SessionID = dbo.ExamSetupMaster.SessionID " +
                            "  WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.ExamResult.StudentID IN (" + StudentIds + ")) " +
                            " AND (dbo.ExamResult.SessionID =" + SESSION_ID + ") AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND " +
                            " (dbo.ExamResult.BranchID =" + BRANCH_ID + ") AND (dbo.SubjectLevelOne.SubjectCategoryID = 1) AND (dbo.ExamSetupMaster.ExamID =" + ExamId + ")" +
                            " ORDER BY dbo.ExamResult.StudentID, dbo.ExamSetupDetail.OrderNo ";


                    SqlDetail2 = "SELECT TOP (100) PERCENT dbo.SubjectLevelOne.SubjectNameL1 AS Subject,dbo.ExamResult.ObtainMarks1 AS Obtain1,dbo.ExamResult.ObtainMarks2 AS Obtain2,dbo.ExamResult.ObtainMarks3 AS Obtain3,dbo.ExamResult.ObtainMarks4 AS Obtain4, dbo.ExamResult.StudentID, " +
                                " dbo.ExamSetupDetail.OrderNo FROM dbo.ExamResult INNER JOIN dbo.SubjectLevelOne ON dbo.ExamResult.SubjectIDL1 = dbo.SubjectLevelOne.IdL1 INNER JOIN " +
                                " dbo.ExamSetupDetail ON dbo.SubjectLevelOne.IdL1 = dbo.ExamSetupDetail.SubjectIDL1 AND dbo.SubjectLevelOne.BranchID = dbo.ExamSetupDetail.BranchID AND " +
                                " dbo.SubjectLevelOne.CompID = dbo.ExamSetupDetail.CompID INNER JOIN dbo.ExamSetupMaster ON dbo.ExamSetupDetail.ExamSetupID = dbo.ExamSetupMaster.ExamSetupID AND " +
                                " dbo.ExamSetupDetail.BranchID = dbo.ExamSetupMaster.BranchID AND dbo.ExamSetupDetail.CompID = dbo.ExamSetupMaster.CompID AND dbo.ExamResult.ClassID = dbo.ExamSetupMaster.ClassID AND dbo.ExamResult.SessionID = dbo.ExamSetupMaster.SessionID " +
                                "  WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.ExamResult.StudentID IN (" + StudentIds + ")) " +
                                " AND (dbo.ExamResult.SessionID =" + SESSION_ID + ") AND (dbo.ExamSetupMaster.SessionID =" + SESSION_ID + ") AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND " +
                                " (dbo.ExamResult.BranchID =" + BRANCH_ID + ") AND (dbo.SubjectLevelOne.SubjectCategoryID = 2) AND (dbo.ExamSetupMaster.ExamID =" + ExamId + ")" +
                                " ORDER BY dbo.ExamResult.StudentID, dbo.ExamSetupDetail.OrderNo ";


                    SqlDetail3 = "SELECT TOP (100) PERCENT dbo.SubjectLevelOne.SubjectNameL1 AS Subject, dbo.ExamResult.ObtainMarks1 AS Obtain1,dbo.ExamResult.ObtainMarks2 AS Obtain2,dbo.ExamResult.ObtainMarks3 AS Obtain3,dbo.ExamResult.ObtainMarks4 AS Obtain4, dbo.ExamResult.StudentID, " +
                               " dbo.ExamSetupDetail.OrderNo FROM dbo.ExamResult INNER JOIN dbo.SubjectLevelOne ON dbo.ExamResult.SubjectIDL1 = dbo.SubjectLevelOne.IdL1 INNER JOIN " +
                               " dbo.ExamSetupDetail ON dbo.SubjectLevelOne.IdL1 = dbo.ExamSetupDetail.SubjectIDL1 AND dbo.SubjectLevelOne.BranchID = dbo.ExamSetupDetail.BranchID AND " +
                               " dbo.SubjectLevelOne.CompID = dbo.ExamSetupDetail.CompID INNER JOIN dbo.ExamSetupMaster ON dbo.ExamSetupDetail.ExamSetupID = dbo.ExamSetupMaster.ExamSetupID AND " +
                               " dbo.ExamSetupDetail.BranchID = dbo.ExamSetupMaster.BranchID AND dbo.ExamSetupDetail.CompID = dbo.ExamSetupMaster.CompID AND dbo.ExamResult.ClassID = dbo.ExamSetupMaster.ClassID AND dbo.ExamResult.SessionID = dbo.ExamSetupMaster.SessionID " +
                               " WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.ExamResult.StudentID IN (" + StudentIds + ")) " +
                               " AND (dbo.ExamResult.SessionID =" + SESSION_ID + ") AND (dbo.ExamSetupMaster.SessionID =" + SESSION_ID + ") AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND " +
                               " (dbo.ExamResult.BranchID =" + BRANCH_ID + ") AND (dbo.SubjectLevelOne.SubjectCategoryID = 3) AND (dbo.ExamSetupMaster.ExamID =" + ExamId + ")" +
                               " ORDER BY dbo.ExamResult.StudentID, dbo.ExamSetupDetail.OrderNo ";
                }
                if (Order == 3)
                {
                    SqlDetail1 = "SELECT TOP (100) PERCENT dbo.SubjectLevelOne.SubjectNameL1 AS Subject, " +

                       "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks3)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL1=1)) as MaxEnglishWritten, " +
                        "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks3)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL1=2)) as MaxEnglishReading, " +
                        "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks3)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL1=3)) as MaxEnglishRecitation, " +
                        "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks3)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL1=4)) as MaxHindiWritten, " +
                        "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks3)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL1=5)) as MaxHindiReading, " +
                        "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks3)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                        " AND (dbo.ExamResult.SubjectIDL1=6)) as MaxHindiRhymsTelling, " +
                       "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks3)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                       " AND (dbo.ExamResult.SubjectIDL1=6)) as MaxHindiRecitation, " +
                         "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks3)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                       " AND (dbo.ExamResult.SubjectIDL1=7)) as MaxMathsWritten, " +
                         "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks3)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                       " AND (dbo.ExamResult.SubjectIDL1=8)) as MaxMathsOral, " +
                        "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks3)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                       " AND (dbo.ExamResult.SubjectIDL1=17)) as MaxEnglishDictation , " +
                         "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks3)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                       " AND (dbo.ExamResult.SubjectIDL1= 18)) as MaxHindiDictation, " +
                        "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks3)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                       " AND (dbo.ExamResult.SubjectIDL1=22)) as MaxEnglishLanguage , " +
                         "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks3)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                       " AND (dbo.ExamResult.SubjectIDL1= 23)) as MaxEnglishLiterature, " +
                        "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks3)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                       " AND (dbo.ExamResult.SubjectIDL1=24)) as MaxHindiLanguage , " +
                         "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks3)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                       " AND (dbo.ExamResult.SubjectIDL1= 25)) as MaxHindiLiterature, " +
                        "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks3)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                       " AND (dbo.ExamResult.SubjectIDL1=26)) as MaxMaths , " +
                         "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks3)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                       " AND (dbo.ExamResult.SubjectIDL1= 27)) as MaxScience, " +
                        "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks3)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                       " AND (dbo.ExamResult.SubjectIDL1=28)) as MaxSocialStudies , " +
                         "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks3)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                       " AND (dbo.ExamResult.SubjectIDL1= 29)) as MaxComputer, " +
                       "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks3)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                       " AND (dbo.ExamResult.SubjectIDL1= 30)) as MaxMoralKnowledge , " +
                       "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks3)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                       " AND (dbo.ExamResult.SubjectIDL1= 31)) as MaxGK , " +
                        "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks3)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                       " AND (dbo.ExamResult.SubjectIDL1= 34)) as MaxPhysicsI, " +
                        "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks3)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                       " AND (dbo.ExamResult.SubjectIDL1= 35)) as MaxChemistryI, " +
                       "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks3)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                       " AND (dbo.ExamResult.SubjectIDL1= 36)) as MaxBiologyI, " +
                       "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks3)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                       " AND (dbo.ExamResult.SubjectIDL1= 37)) as MaxHistoryCivics, " +
                         "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks3)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                       " AND (dbo.ExamResult.SubjectIDL1= 38)) as MaxGeography, " +
                         "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks3)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                       " AND (dbo.ExamResult.SubjectIDL1= 39)) as MaxMoralScience , " +
                       "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks3)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                       " AND (dbo.ExamResult.SubjectIDL1= 40)) as MaxGeneralKnowledge, " +
                       "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks3)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                       " AND (dbo.ExamResult.SubjectIDL1= 42)) as MaxHindi, " +
                          "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks3)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                       " AND (dbo.ExamResult.SubjectIDL1= 43)) as MaxComputerPractical, " +
                       "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks3)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                       " AND (dbo.ExamResult.SubjectIDL1= 44)) as MaxEnglishOral, " +
                         "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks3)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                       " AND (dbo.ExamResult.SubjectIDL1= 45)) as MaxHindioral, " +
                        "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks3)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                       " AND (dbo.ExamResult.SubjectIDL1= 48)) as MAxComputerArt, " +
                       "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks3)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                       " AND (dbo.ExamResult.SubjectIDL1=49)) as MaxCivicsI, " +
                       "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks3)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                       " AND (dbo.ExamResult.SubjectIDL1=50)) as MaxArtI, " +
                        "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks3)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                       " AND (dbo.ExamResult.SubjectIDL1=51)) as MaxArtII, " +
                        "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks3)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                       " AND (dbo.ExamResult.SubjectIDL1=52)) as MaxPhysicsII, " +
                         "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks3)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                       " AND (dbo.ExamResult.SubjectIDL1=54)) as MaxEVS, " +
                       "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks3)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                       " AND (dbo.ExamResult.SubjectIDL1=53)) as MaxMusic, " +
                        "(select  max(TRY_CONVERT(int, dbo.ExamResult.ObtainMarks3)) FROM  dbo.StudentSession INNER JOIN  dbo.ExamResult ON dbo.StudentSession.StudentID = dbo.ExamResult.StudentID WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.StudentSession.SessionID =" + SESSION_ID + ")  AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND  (dbo.ExamResult.BranchID =" + BRANCH_ID + ")  AND (dbo.StudentSession.ClassSetupID = " + SectionId + ") " +
                       " AND (dbo.ExamResult.SubjectIDL1=43)) as MaxComputerPhyEdu, " +
                  " dbo.ExamResult.ObtainMarks1 AS Obtain1, dbo.ExamResult.ObtainMarks2 AS Obtain2,dbo.ExamResult.ObtainMarks3 AS Obtain3,dbo.ExamResult.ObtainMarks4 AS Obtain4, dbo.ExamResult.StudentID, " +
                    " dbo.ExamSetupDetail.OrderNo,dbo.ExamResult.Grade AS Grade1, dbo.ExamResult.ObtainGrade2 AS Grade2,dbo.ExamResult.ObtainGrade3 AS Grade3,dbo.ExamResult.ObtainGrade4 AS Grade4,ExamResult.MaxMark1 as Max1,ExamResult.MaxMark2 as Max2,ExamResult.MaxMark3 as Max3,ExamResult.MaxMark4 as Max4,ExamResult.MinMark1 as Min1,ExamResult.MinMark2 as Min2,ExamResult.MinMark3 as Min3,ExamResult.MinMark4 as Min4 " +
                    " FROM dbo.ExamResult INNER JOIN dbo.SubjectLevelOne ON dbo.ExamResult.SubjectIDL1 = dbo.SubjectLevelOne.IdL1 INNER JOIN " +
                    " dbo.ExamSetupDetail ON dbo.SubjectLevelOne.IdL1 = dbo.ExamSetupDetail.SubjectIDL1 AND dbo.SubjectLevelOne.BranchID = dbo.ExamSetupDetail.BranchID AND " +
                    " dbo.SubjectLevelOne.CompID = dbo.ExamSetupDetail.CompID INNER JOIN dbo.ExamSetupMaster ON dbo.ExamSetupDetail.ExamSetupID = dbo.ExamSetupMaster.ExamSetupID AND " +
                    " dbo.ExamSetupDetail.BranchID = dbo.ExamSetupMaster.BranchID AND dbo.ExamSetupDetail.CompID = dbo.ExamSetupMaster.CompID AND dbo.ExamResult.ClassID = dbo.ExamSetupMaster.ClassID AND dbo.ExamResult.SessionID = dbo.ExamSetupMaster.SessionID " +
                    " WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.ExamResult.StudentID IN (" + StudentIds + ")) " +
                    " AND (dbo.ExamResult.SessionID =" + SESSION_ID + ") AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND " +
                    " (dbo.ExamResult.BranchID =" + BRANCH_ID + ") AND (dbo.SubjectLevelOne.SubjectCategoryID = 1) AND (dbo.ExamSetupMaster.ExamID =" + ExamId + ")" +
                    " ORDER BY dbo.ExamResult.StudentID, dbo.ExamSetupDetail.OrderNo ";


                    SqlDetail2 = "SELECT TOP (100) PERCENT dbo.SubjectLevelOne.SubjectNameL1 AS Subject, dbo.ExamResult.ObtainMarks1 AS Obtain1,dbo.ExamResult.ObtainMarks2 AS Obtain2,dbo.ExamResult.ObtainMarks3 AS Obtain3,dbo.ExamResult.ObtainMarks4 AS Obtain4, dbo.ExamResult.StudentID, " +
                                " dbo.ExamSetupDetail.OrderNo FROM dbo.ExamResult INNER JOIN dbo.SubjectLevelOne ON dbo.ExamResult.SubjectIDL1 = dbo.SubjectLevelOne.IdL1 INNER JOIN " +
                                " dbo.ExamSetupDetail ON dbo.SubjectLevelOne.IdL1 = dbo.ExamSetupDetail.SubjectIDL1 AND dbo.SubjectLevelOne.BranchID = dbo.ExamSetupDetail.BranchID AND " +
                                " dbo.SubjectLevelOne.CompID = dbo.ExamSetupDetail.CompID INNER JOIN dbo.ExamSetupMaster ON dbo.ExamSetupDetail.ExamSetupID = dbo.ExamSetupMaster.ExamSetupID AND " +
                                " dbo.ExamSetupDetail.BranchID = dbo.ExamSetupMaster.BranchID AND dbo.ExamSetupDetail.CompID = dbo.ExamSetupMaster.CompID AND dbo.ExamResult.ClassID = dbo.ExamSetupMaster.ClassID AND dbo.ExamResult.SessionID = dbo.ExamSetupMaster.SessionID " +
                                " WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.ExamResult.StudentID IN (" + StudentIds + ")) " +
                                " AND (dbo.ExamResult.SessionID =" + SESSION_ID + ") AND (dbo.ExamSetupMaster.SessionID =" + SESSION_ID + ") AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND " +
                                " (dbo.ExamResult.BranchID =" + BRANCH_ID + ") AND (dbo.SubjectLevelOne.SubjectCategoryID = 2) AND (dbo.ExamSetupMaster.ExamID =" + ExamId + ")" +
                                " ORDER BY dbo.ExamResult.StudentID, dbo.ExamSetupDetail.OrderNo ";


                    SqlDetail3 = "SELECT TOP (100) PERCENT dbo.SubjectLevelOne.SubjectNameL1 AS Subject, dbo.ExamResult.ObtainMarks1 AS Obtain1,dbo.ExamResult.ObtainMarks2 AS Obtain2,dbo.ExamResult.ObtainMarks3 AS Obtain3,dbo.ExamResult.ObtainMarks4 AS Obtain4, dbo.ExamResult.StudentID, " +
                               " dbo.ExamSetupDetail.OrderNo FROM dbo.ExamResult INNER JOIN dbo.SubjectLevelOne ON dbo.ExamResult.SubjectIDL1 = dbo.SubjectLevelOne.IdL1 INNER JOIN " +
                               " dbo.ExamSetupDetail ON dbo.SubjectLevelOne.IdL1 = dbo.ExamSetupDetail.SubjectIDL1 AND dbo.SubjectLevelOne.BranchID = dbo.ExamSetupDetail.BranchID AND " +
                               " dbo.SubjectLevelOne.CompID = dbo.ExamSetupDetail.CompID INNER JOIN dbo.ExamSetupMaster ON dbo.ExamSetupDetail.ExamSetupID = dbo.ExamSetupMaster.ExamSetupID AND " +
                               " dbo.ExamSetupDetail.BranchID = dbo.ExamSetupMaster.BranchID AND dbo.ExamSetupDetail.CompID = dbo.ExamSetupMaster.CompID AND dbo.ExamResult.ClassID = dbo.ExamSetupMaster.ClassID AND dbo.ExamResult.SessionID = dbo.ExamSetupMaster.SessionID " +
                               " WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.ExamResult.StudentID IN (" + StudentIds + ")) " +
                               " AND (dbo.ExamResult.SessionID =" + SESSION_ID + ") AND (dbo.ExamSetupMaster.SessionID =" + SESSION_ID + ") AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND " +
                               " (dbo.ExamResult.BranchID =" + BRANCH_ID + ") AND (dbo.SubjectLevelOne.SubjectCategoryID = 3) AND (dbo.ExamSetupMaster.ExamID =" + ExamId + ")" +
                               " ORDER BY dbo.ExamResult.StudentID, dbo.ExamSetupDetail.OrderNo ";
                }
                if (Order == 4)
                {
                    SqlDetail1 = "SELECT TOP (100) PERCENT dbo.SubjectLevelOne.SubjectNameL1 AS Subject, dbo.ExamResult.ObtainMarks1 AS Obtain1,dbo.ExamResult.ObtainMarks2 AS Obtain2,dbo.ExamResult.ObtainMarks3 AS Obtain3,dbo.ExamResult.ObtainMarks4 AS Obtain4, dbo.ExamResult.StudentID, " +
                                " dbo.ExamSetupDetail.OrderNo,dbo.ExamResult.Grade AS Grade1, dbo.ExamResult.ObtainGrade2 AS Grade2,dbo.ExamResult.ObtainGrade3 AS Grade3,dbo.ExamResult.ObtainGrade4 AS Grade4,ExamResult.MaxMark1 as Max1,ExamResult.MaxMark2 as Max2,ExamResult.MaxMark3 as Max3,ExamResult.MaxMark4 as Max4,ExamResult.MinMark1 as Min1,ExamResult.MinMark2 as Min2,ExamResult.MinMark3 as Min3,ExamResult.MinMark4 as Min4 " +
                                " FROM dbo.ExamResult INNER JOIN dbo.SubjectLevelOne ON dbo.ExamResult.SubjectIDL1 = dbo.SubjectLevelOne.IdL1 INNER JOIN " +
                                " dbo.ExamSetupDetail ON dbo.SubjectLevelOne.IdL1 = dbo.ExamSetupDetail.SubjectIDL1 AND dbo.SubjectLevelOne.BranchID = dbo.ExamSetupDetail.BranchID AND " +
                                " dbo.SubjectLevelOne.CompID = dbo.ExamSetupDetail.CompID INNER JOIN dbo.ExamSetupMaster ON dbo.ExamSetupDetail.ExamSetupID = dbo.ExamSetupMaster.ExamSetupID AND " +
                                " dbo.ExamSetupDetail.BranchID = dbo.ExamSetupMaster.BranchID AND dbo.ExamSetupDetail.CompID = dbo.ExamSetupMaster.CompID AND dbo.ExamResult.ClassID = dbo.ExamSetupMaster.ClassID AND dbo.ExamResult.SessionID = dbo.ExamSetupMaster.SessionID " +
                                " WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.ExamResult.StudentID IN (" + StudentIds + ")) " +
                                " AND (dbo.ExamResult.SessionID =" + SESSION_ID + ") AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND " +
                                " (dbo.ExamResult.BranchID =" + BRANCH_ID + ") AND (dbo.SubjectLevelOne.SubjectCategoryID = 1) AND (dbo.ExamSetupMaster.ExamID =" + ExamId + ")" +
                                " ORDER BY dbo.ExamResult.StudentID, dbo.ExamSetupDetail.OrderNo ";


                    SqlDetail2 = "SELECT TOP (100) PERCENT dbo.SubjectLevelOne.SubjectNameL1 AS Subject, dbo.ExamResult.ObtainMarks1 AS Obtain1,dbo.ExamResult.ObtainMarks2 AS Obtain2,dbo.ExamResult.ObtainMarks3 AS Obtain3,dbo.ExamResult.ObtainMarks4 AS Obtain4, dbo.ExamResult.StudentID, " +
                                " dbo.ExamSetupDetail.OrderNo FROM dbo.ExamResult INNER JOIN dbo.SubjectLevelOne ON dbo.ExamResult.SubjectIDL1 = dbo.SubjectLevelOne.IdL1 INNER JOIN " +
                                " dbo.ExamSetupDetail ON dbo.SubjectLevelOne.IdL1 = dbo.ExamSetupDetail.SubjectIDL1 AND dbo.SubjectLevelOne.BranchID = dbo.ExamSetupDetail.BranchID AND " +
                                " dbo.SubjectLevelOne.CompID = dbo.ExamSetupDetail.CompID INNER JOIN dbo.ExamSetupMaster ON dbo.ExamSetupDetail.ExamSetupID = dbo.ExamSetupMaster.ExamSetupID AND " +
                                " dbo.ExamSetupDetail.BranchID = dbo.ExamSetupMaster.BranchID AND dbo.ExamSetupDetail.CompID = dbo.ExamSetupMaster.CompID AND dbo.ExamResult.ClassID = dbo.ExamSetupMaster.ClassID AND dbo.ExamResult.SessionID = dbo.ExamSetupMaster.SessionID " +
                                " WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.ExamResult.StudentID IN (" + StudentIds + ")) " +
                                " AND (dbo.ExamResult.SessionID =" + SESSION_ID + ") AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND " +
                                " (dbo.ExamResult.BranchID =" + BRANCH_ID + ") AND (dbo.SubjectLevelOne.SubjectCategoryID = 2) AND (dbo.ExamSetupMaster.ExamID =" + ExamId + ")" +
                                " ORDER BY dbo.ExamResult.StudentID, dbo.ExamSetupDetail.OrderNo ";


                    SqlDetail3 = "SELECT TOP (100) PERCENT dbo.SubjectLevelOne.SubjectNameL1 AS Subject, dbo.ExamResult.ObtainMarks1 AS Obtain1,dbo.ExamResult.ObtainMarks2 AS Obtain2,dbo.ExamResult.ObtainMarks3 AS Obtain3,dbo.ExamResult.ObtainMarks4 AS Obtain4, dbo.ExamResult.StudentID, " +
                               " dbo.ExamSetupDetail.OrderNo FROM dbo.ExamResult INNER JOIN dbo.SubjectLevelOne ON dbo.ExamResult.SubjectIDL1 = dbo.SubjectLevelOne.IdL1 INNER JOIN " +
                               " dbo.ExamSetupDetail ON dbo.SubjectLevelOne.IdL1 = dbo.ExamSetupDetail.SubjectIDL1 AND dbo.SubjectLevelOne.BranchID = dbo.ExamSetupDetail.BranchID AND " +
                               " dbo.SubjectLevelOne.CompID = dbo.ExamSetupDetail.CompID INNER JOIN dbo.ExamSetupMaster ON dbo.ExamSetupDetail.ExamSetupID = dbo.ExamSetupMaster.ExamSetupID AND " +
                               " dbo.ExamSetupDetail.BranchID = dbo.ExamSetupMaster.BranchID AND dbo.ExamSetupDetail.CompID = dbo.ExamSetupMaster.CompID AND dbo.ExamResult.ClassID = dbo.ExamSetupMaster.ClassID AND dbo.ExamResult.SessionID = dbo.ExamSetupMaster.SessionID " +
                               " WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.ExamResult.StudentID IN (" + StudentIds + ")) " +
                               " AND (dbo.ExamResult.SessionID =" + SESSION_ID + ") AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND " +
                               " (dbo.ExamResult.BranchID =" + BRANCH_ID + ") AND (dbo.SubjectLevelOne.SubjectCategoryID = 3) AND (dbo.ExamSetupMaster.ExamID =" + ExamId + ")" +
                               " ORDER BY dbo.ExamResult.StudentID, dbo.ExamSetupDetail.OrderNo ";
                }

            }
            #endregion
        }

        public void ReturnShantiDhamExamMarksheetSql()
        {

            #region  for Shanti Dham
            if (COMPANY_ID == 7)
            {

                #region Term One
                if (ExamId == TermOneID)
                {

                    if (Order == 1)
                    {

                        if (Subjectlevel == 1)
                        {
                            SqlDetail1 = "SELECT TOP (100) PERCENT dbo.SubjectLevelOne.SubjectNameL1 AS Subject," +
                                        " dbo.ExamResult.ObtainMarks1 AS Obtain1,dbo.ExamResult.ObtainMarks2 AS Obtain2,dbo.ExamResult.ObtainMarks3 AS Obtain3,dbo.ExamResult.ObtainMarks4 AS Obtain4, dbo.ExamResult.StudentID, " +
                                        " dbo.ExamSetupDetail.OrderNo,dbo.ExamResult.Grade AS Grade1, dbo.ExamResult.ObtainGrade2 AS Grade2,dbo.ExamResult.ObtainGrade3 AS Grade3,dbo.ExamResult.ObtainGrade4 AS Grade4, ExamResult.MaxMark1 as Max1,ExamResult.MaxMark2 as Max2,ExamResult.MaxMark3 as Max3,ExamResult.MaxMark4 as Max4,ExamResult.MinMark1 as Min1,ExamResult.MinMark2 as Min2,ExamResult.MinMark3 as Min3,ExamResult.MinMark4 as Min4 " +
                                        " FROM dbo.ExamResult INNER JOIN dbo.SubjectLevelOne ON dbo.ExamResult.SubjectIDL1 = dbo.SubjectLevelOne.IdL1 INNER JOIN " +
                                        " dbo.ExamSetupDetail ON dbo.SubjectLevelOne.IdL1 = dbo.ExamSetupDetail.SubjectIDL1 AND dbo.SubjectLevelOne.BranchID = dbo.ExamSetupDetail.BranchID AND " +
                                        " dbo.SubjectLevelOne.CompID = dbo.ExamSetupDetail.CompID INNER JOIN dbo.ExamSetupMaster ON dbo.ExamSetupDetail.ExamSetupID = dbo.ExamSetupMaster.ExamSetupID AND " +
                                        " dbo.ExamSetupDetail.BranchID = dbo.ExamSetupMaster.BranchID AND dbo.ExamSetupDetail.CompID = dbo.ExamSetupMaster.CompID AND dbo.ExamResult.ClassID = dbo.ExamSetupMaster.ClassID AND dbo.ExamResult.SessionID = dbo.ExamSetupMaster.SessionID " +
                                        " WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.ExamResult.StudentID IN (" + StudentIds + ")) " +
                                        " AND (dbo.ExamResult.SessionID =" + SESSION_ID + ") AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND " +
                                        " (dbo.ExamResult.BranchID =" + BRANCH_ID + ") AND (dbo.SubjectLevelOne.SubjectCategoryID = 1) AND (dbo.ExamSetupMaster.ExamID =" + ExamId + ")" +
                                        " and dbo.ExamSetupMaster.SubExamID= " + SubExamId + " " +
                                        " Order BY dbo.ExamResult.StudentID, dbo.ExamSetupDetail.OrderNo ";

                            SqlDetail2 = "SELECT TOP (100) PERCENT dbo.SubjectLevelOne.SubjectNameL1 AS Subject, dbo.ExamResult.ObtainMarks1 AS Obtain1,dbo.ExamResult.ObtainMarks2 AS Obtain2,dbo.ExamResult.ObtainMarks3 AS Obtain3,dbo.ExamResult.ObtainMarks4 AS Obtain4, dbo.ExamResult.StudentID, " +
                                  " dbo.ExamSetupDetail.OrderNo FROM dbo.ExamResult INNER JOIN dbo.SubjectLevelOne ON dbo.ExamResult.SubjectIDL1 = dbo.SubjectLevelOne.IdL1 INNER JOIN " +
                                  " dbo.ExamSetupDetail ON dbo.SubjectLevelOne.IdL1 = dbo.ExamSetupDetail.SubjectIDL1 AND dbo.SubjectLevelOne.BranchID = dbo.ExamSetupDetail.BranchID AND " +
                                  " dbo.SubjectLevelOne.CompID = dbo.ExamSetupDetail.CompID INNER JOIN dbo.ExamSetupMaster ON dbo.ExamSetupDetail.ExamSetupID = dbo.ExamSetupMaster.ExamSetupID AND " +
                                  " dbo.ExamSetupDetail.BranchID = dbo.ExamSetupMaster.BranchID AND dbo.ExamSetupDetail.CompID = dbo.ExamSetupMaster.CompID AND dbo.ExamResult.ClassID = dbo.ExamSetupMaster.ClassID AND dbo.ExamResult.SessionID = dbo.ExamSetupMaster.SessionID " +
                                  " WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.ExamResult.StudentID IN (" + StudentIds + ")) " +
                                  " AND (dbo.ExamResult.SessionID =" + SESSION_ID + ") AND (dbo.ExamSetupMaster.SessionID =" + SESSION_ID + ") AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND " +
                                  " (dbo.ExamResult.BranchID =" + BRANCH_ID + ") AND (dbo.SubjectLevelOne.SubjectCategoryID = 2) AND (dbo.ExamSetupMaster.ExamID =" + ExamId + ")  and dbo.ExamSetupMaster.SubExamID= " + SubExamId + " " +
                                  " Order BY dbo.ExamResult.StudentID, dbo.ExamSetupDetail.OrderNo ";


                            SqlDetail3 = "SELECT TOP (100) PERCENT dbo.SubjectLevelOne.SubjectNameL1 AS Subject, dbo.ExamResult.ObtainMarks1 AS Obtain1,dbo.ExamResult.ObtainMarks2 AS Obtain2,dbo.ExamResult.ObtainMarks3 AS Obtain3,dbo.ExamResult.ObtainMarks4 AS Obtain4, dbo.ExamResult.StudentID, " +
                                       " dbo.ExamSetupDetail.OrderNo FROM dbo.ExamResult INNER JOIN dbo.SubjectLevelOne ON dbo.ExamResult.SubjectIDL1 = dbo.SubjectLevelOne.IdL1 INNER JOIN " +
                                       " dbo.ExamSetupDetail ON dbo.SubjectLevelOne.IdL1 = dbo.ExamSetupDetail.SubjectIDL1 AND dbo.SubjectLevelOne.BranchID = dbo.ExamSetupDetail.BranchID AND " +
                                       " dbo.SubjectLevelOne.CompID = dbo.ExamSetupDetail.CompID INNER JOIN dbo.ExamSetupMaster ON dbo.ExamSetupDetail.ExamSetupID = dbo.ExamSetupMaster.ExamSetupID AND " +
                                       " dbo.ExamSetupDetail.BranchID = dbo.ExamSetupMaster.BranchID AND dbo.ExamSetupDetail.CompID = dbo.ExamSetupMaster.CompID AND dbo.ExamResult.ClassID = dbo.ExamSetupMaster.ClassID AND dbo.ExamResult.SessionID = dbo.ExamSetupMaster.SessionID " +
                                       " WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.ExamResult.StudentID IN (" + StudentIds + ")) " +
                                       " AND (dbo.ExamResult.SessionID =" + SESSION_ID + ") AND (dbo.ExamSetupMaster.SessionID =" + SESSION_ID + ") AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND " +
                                       " (dbo.ExamResult.BranchID =" + BRANCH_ID + ") AND (dbo.SubjectLevelOne.SubjectCategoryID = 3) AND (dbo.ExamSetupMaster.ExamID =" + ExamId + ") and dbo.ExamSetupMaster.SubExamID= " + SubExamId + " " +
                                       " Order BY dbo.ExamResult.StudentID, dbo.ExamSetupDetail.OrderNo ";
                        }

                        if (Subjectlevel == 2)
                        {

                            SqlDetail1 = "SELECT TOP (100) PERCENT dbo.SubjectLevelOne.SubjectNameL1 AS Subject,dbo.SubjectLevelTwo.SubjectNameL2 AS SubjectD," +
                                       " dbo.ExamResult.ObtainMarks1 AS Obtain1,dbo.ExamResult.ObtainMarks2 AS Obtain2,dbo.ExamResult.ObtainMarks3 AS Obtain3,dbo.ExamResult.ObtainMarks4 AS Obtain4, dbo.ExamResult.StudentID, " +
                                       " dbo.ExamSetupDetail.OrderNo,dbo.ExamResult.Grade AS Grade1, dbo.ExamResult.ObtainGrade2 AS Grade2,dbo.ExamResult.ObtainGrade3 AS Grade3,dbo.ExamResult.ObtainGrade4 AS Grade4, ExamResult.MaxMark1 as Max1,ExamResult.MaxMark2 as Max2,ExamResult.MaxMark3 as Max3,ExamResult.MaxMark4 as Max4,ExamResult.MinMark1 as Min1,ExamResult.MinMark2 as Min2,ExamResult.MinMark3 as Min3,ExamResult.MinMark4 as Min4 " +
                                       " FROM dbo.ExamResult INNER JOIN dbo.SubjectLevelOne ON dbo.ExamResult.SubjectIDL1 = dbo.SubjectLevelOne.IdL1 INNER JOIN " +
                                       " dbo.ExamSetupDetail ON dbo.SubjectLevelOne.IdL1 = dbo.ExamSetupDetail.SubjectIDL1 AND dbo.SubjectLevelOne.BranchID = dbo.ExamSetupDetail.BranchID AND " +
                                       " dbo.SubjectLevelOne.CompID = dbo.ExamSetupDetail.CompID " +
                                       " INNER JOIN dbo.SubjectLevelTwo ON dbo.ExamResult.SubjectIDL2 = dbo.SubjectLevelTwo.IdL2 AND dbo.ExamSetupDetail.SubjectIDL2 = dbo.SubjectLevelTwo.IdL2 " +
                                       " INNER JOIN dbo.ExamSetupMaster ON dbo.ExamSetupDetail.ExamSetupID = dbo.ExamSetupMaster.ExamSetupID AND " +
                                       " dbo.ExamSetupDetail.BranchID = dbo.ExamSetupMaster.BranchID AND dbo.ExamSetupDetail.CompID = dbo.ExamSetupMaster.CompID AND dbo.ExamResult.ClassID = dbo.ExamSetupMaster.ClassID AND dbo.ExamResult.SessionID = dbo.ExamSetupMaster.SessionID " +
                                       " WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.ExamResult.StudentID IN (" + StudentIds + ")) " +
                                       " AND (dbo.ExamResult.SessionID =" + SESSION_ID + ") AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND " +
                                       " (dbo.ExamResult.BranchID =" + BRANCH_ID + ") AND (dbo.SubjectLevelOne.SubjectCategoryID = 1) AND (dbo.ExamSetupMaster.ExamID =" + ExamId + ")" +
                                       " and dbo.ExamSetupMaster.SubExamID= " + SubExamId + " " +
                                       " Order BY dbo.ExamResult.StudentID, dbo.ExamSetupDetail.OrderNo ";


                            SqlDetail2 = "SELECT TOP (100) PERCENT dbo.SubjectLevelOne.SubjectNameL1 AS Subject,dbo.SubjectLevelTwo.SubjectNameL2 AS SubjectD,dbo.ExamResult.ObtainMarks1 AS Obtain1,dbo.ExamResult.ObtainMarks2 AS Obtain2,dbo.ExamResult.ObtainMarks3 AS Obtain3,dbo.ExamResult.ObtainMarks4 AS Obtain4, dbo.ExamResult.StudentID, " +
                                    " dbo.ExamSetupDetail.OrderNo,ExamResult.MaxMark1 as Max1,ExamResult.MaxMark2 as Max2,ExamResult.MaxMark3 as Max3,ExamResult.MaxMark4 as Max4,ExamResult.MinMark1 as Min1,ExamResult.MinMark2 as Min2,ExamResult.MinMark3 as Min3,ExamResult.MinMark4 as Min4 " +
                                    " FROM dbo.ExamResult INNER JOIN dbo.SubjectLevelOne ON dbo.ExamResult.SubjectIDL1 = dbo.SubjectLevelOne.IdL1 INNER JOIN " +
                                    " dbo.ExamSetupDetail ON dbo.SubjectLevelOne.IdL1 = dbo.ExamSetupDetail.SubjectIDL1 AND dbo.SubjectLevelOne.BranchID = dbo.ExamSetupDetail.BranchID AND " +
                                    " dbo.SubjectLevelOne.CompID = dbo.ExamSetupDetail.CompID " +
                                    " INNER JOIN dbo.SubjectLevelTwo ON dbo.ExamResult.SubjectIDL2 = dbo.SubjectLevelTwo.IdL2 AND dbo.ExamSetupDetail.SubjectIDL2 = dbo.SubjectLevelTwo.IdL2 " +
                                    " INNER JOIN dbo.ExamSetupMaster ON dbo.ExamSetupDetail.ExamSetupID = dbo.ExamSetupMaster.ExamSetupID AND " +
                                    " dbo.ExamSetupDetail.BranchID = dbo.ExamSetupMaster.BranchID AND dbo.ExamSetupDetail.CompID = dbo.ExamSetupMaster.CompID AND dbo.ExamResult.ClassID = dbo.ExamSetupMaster.ClassID " +
                                    " WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.ExamResult.StudentID IN (" + StudentIds + ")) " +
                                    " AND (dbo.ExamResult.SessionID =" + SESSION_ID + ")  AND (dbo.ExamSetupMaster.SessionID =" + SESSION_ID + ") AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND " +
                                    " (dbo.ExamResult.BranchID =" + BRANCH_ID + ") AND (dbo.SubjectLevelOne.SubjectCategoryID = 2) AND (dbo.ExamSetupMaster.ExamID =" + ExamId + ")" +
                                    " and dbo.ExamSetupMaster.SubExamID= " + SubExamId + " " +
                                    " Order BY dbo.ExamResult.StudentID, dbo.ExamSetupDetail.OrderNo ";


                            SqlDetail3 = "SELECT TOP (100) PERCENT dbo.SubjectLevelOne.SubjectNameL1 AS Subject,dbo.SubjectLevelTwo.SubjectNameL2 AS SubjectD,dbo.ExamResult.ObtainMarks1 AS Obtain1,dbo.ExamResult.ObtainMarks2 AS Obtain2,dbo.ExamResult.ObtainMarks3 AS Obtain3,dbo.ExamResult.ObtainMarks4 AS Obtain4, dbo.ExamResult.StudentID, " +
                                        " dbo.ExamSetupDetail.OrderNo,ExamResult.MaxMark1 as Max1,ExamResult.MaxMark2 as Max2,ExamResult.MaxMark3 as Max3,ExamResult.MaxMark4 as Max4,ExamResult.MinMark1 as Min1,ExamResult.MinMark2 as Min2,ExamResult.MinMark3 as Min3,ExamResult.MinMark4 as Min4 " +
                                        " FROM dbo.ExamResult INNER JOIN dbo.SubjectLevelOne ON dbo.ExamResult.SubjectIDL1 = dbo.SubjectLevelOne.IdL1 INNER JOIN " +
                                        " dbo.ExamSetupDetail ON dbo.SubjectLevelOne.IdL1 = dbo.ExamSetupDetail.SubjectIDL1 AND dbo.SubjectLevelOne.BranchID = dbo.ExamSetupDetail.BranchID AND " +
                                        " dbo.SubjectLevelOne.CompID = dbo.ExamSetupDetail.CompID " +
                                        " INNER JOIN dbo.SubjectLevelTwo ON dbo.ExamResult.SubjectIDL2 = dbo.SubjectLevelTwo.IdL2 AND dbo.ExamSetupDetail.SubjectIDL2 = dbo.SubjectLevelTwo.IdL2 " +
                                        " INNER JOIN dbo.ExamSetupMaster ON dbo.ExamSetupDetail.ExamSetupID = dbo.ExamSetupMaster.ExamSetupID AND " +
                                        " dbo.ExamSetupDetail.BranchID = dbo.ExamSetupMaster.BranchID AND dbo.ExamSetupDetail.CompID = dbo.ExamSetupMaster.CompID AND dbo.ExamResult.ClassID = dbo.ExamSetupMaster.ClassID " +
                                       " WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.ExamResult.StudentID IN (" + StudentIds + ")) " +
                                       " AND (dbo.ExamResult.SessionID =" + SESSION_ID + ") AND (dbo.ExamSetupMaster.SessionID =" + SESSION_ID + ") AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND " +
                                       " (dbo.ExamResult.BranchID =" + BRANCH_ID + ") AND (dbo.SubjectLevelOne.SubjectCategoryID = 3) AND (dbo.ExamSetupMaster.ExamID =" + ExamId + ")" +
                                      " and dbo.ExamSetupMaster.SubExamID= " + SubExamId + " " +
                                       " Order BY dbo.ExamResult.StudentID, dbo.ExamSetupDetail.OrderNo ";
                        }


                    }

                    if (Order == 2)
                    {
                        if (Subjectlevel == 1)
                        {

                            SqlDetail1 = "SELECT TOP (100) PERCENT dbo.SubjectLevelOne.SubjectNameL1 AS Subject, " +
                                    " dbo.ExamResult.ObtainMarks1 AS Obtain1,dbo.ExamResult.ObtainMarks2 AS Obtain2,dbo.ExamResult.ObtainMarks3 AS Obtain3,dbo.ExamResult.ObtainMarks4 AS Obtain4, dbo.ExamResult.StudentID, " +
                                    " dbo.ExamSetupDetail.OrderNo,dbo.ExamResult.Grade AS Grade1, dbo.ExamResult.ObtainGrade2 AS Grade2,dbo.ExamResult.ObtainGrade3 AS Grade3,dbo.ExamResult.ObtainGrade4 AS Grade4,ExamResult.MaxMark1 as Max1,ExamResult.MaxMark2 as Max2,ExamResult.MaxMark3 as Max3,ExamResult.MaxMark4 as Max4,ExamResult.MinMark1 as Min1,ExamResult.MinMark2 as Min2,ExamResult.MinMark3 as Min3,ExamResult.MinMark4 as Min4" +
                                    " FROM dbo.ExamResult INNER JOIN dbo.SubjectLevelOne ON dbo.ExamResult.SubjectIDL1 = dbo.SubjectLevelOne.IdL1 INNER JOIN " +
                                    " dbo.ExamSetupDetail ON dbo.SubjectLevelOne.IdL1 = dbo.ExamSetupDetail.SubjectIDL1 AND dbo.SubjectLevelOne.BranchID = dbo.ExamSetupDetail.BranchID AND " +
                                    " dbo.SubjectLevelOne.CompID = dbo.ExamSetupDetail.CompID INNER JOIN dbo.ExamSetupMaster ON dbo.ExamSetupDetail.ExamSetupID = dbo.ExamSetupMaster.ExamSetupID AND " +
                                    " dbo.ExamSetupDetail.BranchID = dbo.ExamSetupMaster.BranchID AND dbo.ExamSetupDetail.CompID = dbo.ExamSetupMaster.CompID AND dbo.ExamResult.ClassID = dbo.ExamSetupMaster.ClassID AND dbo.ExamResult.SessionID = dbo.ExamSetupMaster.SessionID " +
                                    "  WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.ExamResult.StudentID IN (" + StudentIds + ")) " +
                                    " AND (dbo.ExamResult.SessionID =" + SESSION_ID + ") AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND " +
                                    " (dbo.ExamResult.BranchID =" + BRANCH_ID + ") AND (dbo.SubjectLevelOne.SubjectCategoryID = 1) AND (dbo.ExamSetupMaster.ExamID =" + ExamId + ")" +
                                     " and dbo.ExamSetupMaster.SubExamID= " + SubExamId + " " +
                                    " Order BY dbo.ExamResult.StudentID, dbo.ExamSetupDetail.OrderNo ";


                            SqlDetail2 = "SELECT TOP (100) PERCENT dbo.SubjectLevelOne.SubjectNameL1 AS Subject,dbo.ExamResult.ObtainMarks1 AS Obtain1,dbo.ExamResult.ObtainMarks2 AS Obtain2,dbo.ExamResult.ObtainMarks3 AS Obtain3,dbo.ExamResult.ObtainMarks4 AS Obtain4, dbo.ExamResult.StudentID, " +
                                        " dbo.ExamSetupDetail.OrderNo FROM dbo.ExamResult INNER JOIN dbo.SubjectLevelOne ON dbo.ExamResult.SubjectIDL1 = dbo.SubjectLevelOne.IdL1 INNER JOIN " +
                                        " dbo.ExamSetupDetail ON dbo.SubjectLevelOne.IdL1 = dbo.ExamSetupDetail.SubjectIDL1 AND dbo.SubjectLevelOne.BranchID = dbo.ExamSetupDetail.BranchID AND " +
                                        " dbo.SubjectLevelOne.CompID = dbo.ExamSetupDetail.CompID INNER JOIN dbo.ExamSetupMaster ON dbo.ExamSetupDetail.ExamSetupID = dbo.ExamSetupMaster.ExamSetupID AND " +
                                        " dbo.ExamSetupDetail.BranchID = dbo.ExamSetupMaster.BranchID AND dbo.ExamSetupDetail.CompID = dbo.ExamSetupMaster.CompID AND dbo.ExamResult.ClassID = dbo.ExamSetupMaster.ClassID AND dbo.ExamResult.SessionID = dbo.ExamSetupMaster.SessionID " +
                                        "  WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.ExamResult.StudentID IN (" + StudentIds + ")) " +
                                        " AND (dbo.ExamResult.SessionID =" + SESSION_ID + ") AND (dbo.ExamSetupMaster.SessionID =" + SESSION_ID + ") AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND " +
                                        " (dbo.ExamResult.BranchID =" + BRANCH_ID + ") AND (dbo.SubjectLevelOne.SubjectCategoryID = 2) AND (dbo.ExamSetupMaster.ExamID =" + ExamId + ")" +
                                         " and dbo.ExamSetupMaster.SubExamID= " + SubExamId + " " +
                                        " Order BY dbo.ExamResult.StudentID, dbo.ExamSetupDetail.OrderNo ";


                            SqlDetail3 = "SELECT TOP (100) PERCENT dbo.SubjectLevelOne.SubjectNameL1 AS Subject, dbo.ExamResult.ObtainMarks1 AS Obtain1,dbo.ExamResult.ObtainMarks2 AS Obtain2,dbo.ExamResult.ObtainMarks3 AS Obtain3,dbo.ExamResult.ObtainMarks4 AS Obtain4, dbo.ExamResult.StudentID, " +
                                       " dbo.ExamSetupDetail.OrderNo FROM dbo.ExamResult INNER JOIN dbo.SubjectLevelOne ON dbo.ExamResult.SubjectIDL1 = dbo.SubjectLevelOne.IdL1 INNER JOIN " +
                                       " dbo.ExamSetupDetail ON dbo.SubjectLevelOne.IdL1 = dbo.ExamSetupDetail.SubjectIDL1 AND dbo.SubjectLevelOne.BranchID = dbo.ExamSetupDetail.BranchID AND " +
                                       " dbo.SubjectLevelOne.CompID = dbo.ExamSetupDetail.CompID INNER JOIN dbo.ExamSetupMaster ON dbo.ExamSetupDetail.ExamSetupID = dbo.ExamSetupMaster.ExamSetupID AND " +
                                       " dbo.ExamSetupDetail.BranchID = dbo.ExamSetupMaster.BranchID AND dbo.ExamSetupDetail.CompID = dbo.ExamSetupMaster.CompID AND dbo.ExamResult.ClassID = dbo.ExamSetupMaster.ClassID AND dbo.ExamResult.SessionID = dbo.ExamSetupMaster.SessionID " +
                                       " WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.ExamResult.StudentID IN (" + StudentIds + ")) " +
                                       " AND (dbo.ExamResult.SessionID =" + SESSION_ID + ") AND (dbo.ExamSetupMaster.SessionID =" + SESSION_ID + ") AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND " +
                                       " (dbo.ExamResult.BranchID =" + BRANCH_ID + ") AND (dbo.SubjectLevelOne.SubjectCategoryID = 3) AND (dbo.ExamSetupMaster.ExamID =" + ExamId + ")" +
                                         " and dbo.ExamSetupMaster.SubExamID= " + SubExamId + " " +
                                       " Order BY dbo.ExamResult.StudentID, dbo.ExamSetupDetail.OrderNo ";
                        }
                        if (Subjectlevel == 2)
                        {

                            SqlDetail1 = "SELECT TOP (100) PERCENT dbo.SubjectLevelOne.SubjectNameL1 AS Subject,dbo.SubjectLevelTwo.SubjectNameL2 AS SubjectD," +
                                       " dbo.ExamResult.ObtainMarks1 AS Obtain1,dbo.ExamResult.ObtainMarks2 AS Obtain2,dbo.ExamResult.ObtainMarks3 AS Obtain3,dbo.ExamResult.ObtainMarks4 AS Obtain4, dbo.ExamResult.StudentID, " +
                                       " dbo.ExamSetupDetail.OrderNo,dbo.ExamResult.Grade AS Grade1, dbo.ExamResult.ObtainGrade2 AS Grade2,dbo.ExamResult.ObtainGrade3 AS Grade3,dbo.ExamResult.ObtainGrade4 AS Grade4, ExamResult.MaxMark1 as Max1,ExamResult.MaxMark2 as Max2,ExamResult.MaxMark3 as Max3,ExamResult.MaxMark4 as Max4,ExamResult.MinMark1 as Min1,ExamResult.MinMark2 as Min2,ExamResult.MinMark3 as Min3,ExamResult.MinMark4 as Min4 " +
                                       " FROM dbo.ExamResult INNER JOIN dbo.SubjectLevelOne ON dbo.ExamResult.SubjectIDL1 = dbo.SubjectLevelOne.IdL1 INNER JOIN " +
                                       " dbo.ExamSetupDetail ON dbo.SubjectLevelOne.IdL1 = dbo.ExamSetupDetail.SubjectIDL1 AND dbo.SubjectLevelOne.BranchID = dbo.ExamSetupDetail.BranchID AND " +
                                       " dbo.SubjectLevelOne.CompID = dbo.ExamSetupDetail.CompID " +
                                       " INNER JOIN dbo.SubjectLevelTwo ON dbo.ExamResult.SubjectIDL2 = dbo.SubjectLevelTwo.IdL2 AND dbo.ExamSetupDetail.SubjectIDL2 = dbo.SubjectLevelTwo.IdL2 " +
                                       " INNER JOIN dbo.ExamSetupMaster ON dbo.ExamSetupDetail.ExamSetupID = dbo.ExamSetupMaster.ExamSetupID AND " +
                                       " dbo.ExamSetupDetail.BranchID = dbo.ExamSetupMaster.BranchID AND dbo.ExamSetupDetail.CompID = dbo.ExamSetupMaster.CompID AND dbo.ExamResult.ClassID = dbo.ExamSetupMaster.ClassID AND dbo.ExamResult.SessionID = dbo.ExamSetupMaster.SessionID " +
                                       " WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.ExamResult.StudentID IN (" + StudentIds + ")) " +
                                       " AND (dbo.ExamResult.SessionID =" + SESSION_ID + ") AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND " +
                                       " (dbo.ExamResult.BranchID =" + BRANCH_ID + ") AND (dbo.SubjectLevelOne.SubjectCategoryID = 1) AND (dbo.ExamSetupMaster.ExamID =" + ExamId + ")" +
                                       " and dbo.ExamSetupMaster.SubExamID= " + SubExamId + " " +
                                       " Order BY dbo.ExamResult.StudentID, dbo.ExamSetupDetail.OrderNo ";


                            SqlDetail2 = "SELECT TOP (100) PERCENT dbo.SubjectLevelOne.SubjectNameL1 AS Subject,dbo.SubjectLevelTwo.SubjectNameL2 AS SubjectD,dbo.ExamResult.ObtainMarks1 AS Obtain1,dbo.ExamResult.ObtainMarks2 AS Obtain2,dbo.ExamResult.ObtainMarks3 AS Obtain3,dbo.ExamResult.ObtainMarks4 AS Obtain4, dbo.ExamResult.StudentID, " +
                                    " dbo.ExamSetupDetail.OrderNo,ExamResult.MaxMark1 as Max1,ExamResult.MaxMark2 as Max2,ExamResult.MaxMark3 as Max3,ExamResult.MaxMark4 as Max4,ExamResult.MinMark1 as Min1,ExamResult.MinMark2 as Min2,ExamResult.MinMark3 as Min3,ExamResult.MinMark4 as Min4 " +
                                    " FROM dbo.ExamResult INNER JOIN dbo.SubjectLevelOne ON dbo.ExamResult.SubjectIDL1 = dbo.SubjectLevelOne.IdL1 INNER JOIN " +
                                    " dbo.ExamSetupDetail ON dbo.SubjectLevelOne.IdL1 = dbo.ExamSetupDetail.SubjectIDL1 AND dbo.SubjectLevelOne.BranchID = dbo.ExamSetupDetail.BranchID AND " +
                                    " dbo.SubjectLevelOne.CompID = dbo.ExamSetupDetail.CompID " +
                                    " INNER JOIN dbo.SubjectLevelTwo ON dbo.ExamResult.SubjectIDL2 = dbo.SubjectLevelTwo.IdL2 AND dbo.ExamSetupDetail.SubjectIDL2 = dbo.SubjectLevelTwo.IdL2 " +
                                    " INNER JOIN dbo.ExamSetupMaster ON dbo.ExamSetupDetail.ExamSetupID = dbo.ExamSetupMaster.ExamSetupID AND " +
                                    " dbo.ExamSetupDetail.BranchID = dbo.ExamSetupMaster.BranchID AND dbo.ExamSetupDetail.CompID = dbo.ExamSetupMaster.CompID AND dbo.ExamResult.ClassID = dbo.ExamSetupMaster.ClassID " +
                                    " WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.ExamResult.StudentID IN (" + StudentIds + ")) " +
                                    " AND (dbo.ExamResult.SessionID =" + SESSION_ID + ")  AND (dbo.ExamSetupMaster.SessionID =" + SESSION_ID + ") AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND " +
                                    " (dbo.ExamResult.BranchID =" + BRANCH_ID + ") AND (dbo.SubjectLevelOne.SubjectCategoryID = 2) AND (dbo.ExamSetupMaster.ExamID =" + ExamId + ")" +
                                    " and dbo.ExamSetupMaster.SubExamID= " + SubExamId + " " +
                                    " Order BY dbo.ExamResult.StudentID, dbo.ExamSetupDetail.OrderNo ";


                            SqlDetail3 = "SELECT TOP (100) PERCENT dbo.SubjectLevelOne.SubjectNameL1 AS Subject,dbo.SubjectLevelTwo.SubjectNameL2 AS SubjectD,dbo.ExamResult.ObtainMarks1 AS Obtain1,dbo.ExamResult.ObtainMarks2 AS Obtain2,dbo.ExamResult.ObtainMarks3 AS Obtain3,dbo.ExamResult.ObtainMarks4 AS Obtain4, dbo.ExamResult.StudentID, " +
                                        " dbo.ExamSetupDetail.OrderNo,ExamResult.MaxMark1 as Max1,ExamResult.MaxMark2 as Max2,ExamResult.MaxMark3 as Max3,ExamResult.MaxMark4 as Max4,ExamResult.MinMark1 as Min1,ExamResult.MinMark2 as Min2,ExamResult.MinMark3 as Min3,ExamResult.MinMark4 as Min4 " +
                                        " FROM dbo.ExamResult INNER JOIN dbo.SubjectLevelOne ON dbo.ExamResult.SubjectIDL1 = dbo.SubjectLevelOne.IdL1 INNER JOIN " +
                                        " dbo.ExamSetupDetail ON dbo.SubjectLevelOne.IdL1 = dbo.ExamSetupDetail.SubjectIDL1 AND dbo.SubjectLevelOne.BranchID = dbo.ExamSetupDetail.BranchID AND " +
                                        " dbo.SubjectLevelOne.CompID = dbo.ExamSetupDetail.CompID " +
                                        " INNER JOIN dbo.SubjectLevelTwo ON dbo.ExamResult.SubjectIDL2 = dbo.SubjectLevelTwo.IdL2 AND dbo.ExamSetupDetail.SubjectIDL2 = dbo.SubjectLevelTwo.IdL2 " +
                                        " INNER JOIN dbo.ExamSetupMaster ON dbo.ExamSetupDetail.ExamSetupID = dbo.ExamSetupMaster.ExamSetupID AND " +
                                        " dbo.ExamSetupDetail.BranchID = dbo.ExamSetupMaster.BranchID AND dbo.ExamSetupDetail.CompID = dbo.ExamSetupMaster.CompID AND dbo.ExamResult.ClassID = dbo.ExamSetupMaster.ClassID " +
                                       " WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.ExamResult.StudentID IN (" + StudentIds + ")) " +
                                       " AND (dbo.ExamResult.SessionID =" + SESSION_ID + ") AND (dbo.ExamSetupMaster.SessionID =" + SESSION_ID + ") AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND " +
                                       " (dbo.ExamResult.BranchID =" + BRANCH_ID + ") AND (dbo.SubjectLevelOne.SubjectCategoryID = 3) AND (dbo.ExamSetupMaster.ExamID =" + ExamId + ")" +
                                      " and dbo.ExamSetupMaster.SubExamID= " + SubExamId + " " +
                                       " Order BY dbo.ExamResult.StudentID, dbo.ExamSetupDetail.OrderNo ";
                        }

                    }
                    if (Order == 3)
                    {
                        if (Subjectlevel == 1)
                        {

                            SqlDetail1 = "SELECT TOP (100) PERCENT dbo.SubjectLevelOne.SubjectNameL1 AS Subject, " +
                            " dbo.ExamResult.ObtainMarks1 AS Obtain1, dbo.ExamResult.ObtainMarks2 AS Obtain2,dbo.ExamResult.ObtainMarks3 AS Obtain3,dbo.ExamResult.ObtainMarks4 AS Obtain4, dbo.ExamResult.StudentID, " +
                            " dbo.ExamSetupDetail.OrderNo,dbo.ExamResult.Grade AS Grade1, dbo.ExamResult.ObtainGrade2 AS Grade2,dbo.ExamResult.ObtainGrade3 AS Grade3,dbo.ExamResult.ObtainGrade4 AS Grade4,ExamResult.MaxMark1 as Max1,ExamResult.MaxMark2 as Max2,ExamResult.MaxMark3 as Max3,ExamResult.MaxMark4 as Max4,ExamResult.MinMark1 as Min1,ExamResult.MinMark2 as Min2,ExamResult.MinMark3 as Min3,ExamResult.MinMark4 as Min4 " +
                            " FROM dbo.ExamResult INNER JOIN dbo.SubjectLevelOne ON dbo.ExamResult.SubjectIDL1 = dbo.SubjectLevelOne.IdL1 INNER JOIN " +
                            " dbo.ExamSetupDetail ON dbo.SubjectLevelOne.IdL1 = dbo.ExamSetupDetail.SubjectIDL1 AND dbo.SubjectLevelOne.BranchID = dbo.ExamSetupDetail.BranchID AND " +
                            " dbo.SubjectLevelOne.CompID = dbo.ExamSetupDetail.CompID INNER JOIN dbo.ExamSetupMaster ON dbo.ExamSetupDetail.ExamSetupID = dbo.ExamSetupMaster.ExamSetupID AND " +
                            " dbo.ExamSetupDetail.BranchID = dbo.ExamSetupMaster.BranchID AND dbo.ExamSetupDetail.CompID = dbo.ExamSetupMaster.CompID AND dbo.ExamResult.ClassID = dbo.ExamSetupMaster.ClassID AND dbo.ExamResult.SessionID = dbo.ExamSetupMaster.SessionID " +
                            " WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.ExamResult.StudentID IN (" + StudentIds + ")) " +
                            " AND (dbo.ExamResult.SessionID =" + SESSION_ID + ") AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND " +
                            " (dbo.ExamResult.BranchID =" + BRANCH_ID + ") AND (dbo.SubjectLevelOne.SubjectCategoryID = 1) AND (dbo.ExamSetupMaster.ExamID =" + ExamId + ")" +
                            " and dbo.ExamSetupMaster.SubExamID= " + SubExamId + " " +
                            " Order BY dbo.ExamResult.StudentID, dbo.ExamSetupDetail.OrderNo ";


                            SqlDetail2 = "SELECT TOP (100) PERCENT dbo.SubjectLevelOne.SubjectNameL1 AS Subject, dbo.ExamResult.ObtainMarks1 AS Obtain1,dbo.ExamResult.ObtainMarks2 AS Obtain2,dbo.ExamResult.ObtainMarks3 AS Obtain3,dbo.ExamResult.ObtainMarks4 AS Obtain4, dbo.ExamResult.StudentID, " +
                                        " dbo.ExamSetupDetail.OrderNo FROM dbo.ExamResult INNER JOIN dbo.SubjectLevelOne ON dbo.ExamResult.SubjectIDL1 = dbo.SubjectLevelOne.IdL1 INNER JOIN " +
                                        " dbo.ExamSetupDetail ON dbo.SubjectLevelOne.IdL1 = dbo.ExamSetupDetail.SubjectIDL1 AND dbo.SubjectLevelOne.BranchID = dbo.ExamSetupDetail.BranchID AND " +
                                        " dbo.SubjectLevelOne.CompID = dbo.ExamSetupDetail.CompID INNER JOIN dbo.ExamSetupMaster ON dbo.ExamSetupDetail.ExamSetupID = dbo.ExamSetupMaster.ExamSetupID AND " +
                                        " dbo.ExamSetupDetail.BranchID = dbo.ExamSetupMaster.BranchID AND dbo.ExamSetupDetail.CompID = dbo.ExamSetupMaster.CompID AND dbo.ExamResult.ClassID = dbo.ExamSetupMaster.ClassID AND dbo.ExamResult.SessionID = dbo.ExamSetupMaster.SessionID " +
                                        " WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.ExamResult.StudentID IN (" + StudentIds + ")) " +
                                        " AND (dbo.ExamResult.SessionID =" + SESSION_ID + ") AND (dbo.ExamSetupMaster.SessionID =" + SESSION_ID + ") AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND " +
                                        " (dbo.ExamResult.BranchID =" + BRANCH_ID + ") AND (dbo.SubjectLevelOne.SubjectCategoryID = 2) AND (dbo.ExamSetupMaster.ExamID =" + ExamId + ")" +
                                        " and dbo.ExamSetupMaster.SubExamID= " + SubExamId + " " +
                                        " Order BY dbo.ExamResult.StudentID, dbo.ExamSetupDetail.OrderNo ";


                            SqlDetail3 = "SELECT TOP (100) PERCENT dbo.SubjectLevelOne.SubjectNameL1 AS Subject, dbo.ExamResult.ObtainMarks1 AS Obtain1,dbo.ExamResult.ObtainMarks2 AS Obtain2,dbo.ExamResult.ObtainMarks3 AS Obtain3,dbo.ExamResult.ObtainMarks4 AS Obtain4, dbo.ExamResult.StudentID, " +
                                       " dbo.ExamSetupDetail.OrderNo FROM dbo.ExamResult INNER JOIN dbo.SubjectLevelOne ON dbo.ExamResult.SubjectIDL1 = dbo.SubjectLevelOne.IdL1 INNER JOIN " +
                                       " dbo.ExamSetupDetail ON dbo.SubjectLevelOne.IdL1 = dbo.ExamSetupDetail.SubjectIDL1 AND dbo.SubjectLevelOne.BranchID = dbo.ExamSetupDetail.BranchID AND " +
                                       " dbo.SubjectLevelOne.CompID = dbo.ExamSetupDetail.CompID INNER JOIN dbo.ExamSetupMaster ON dbo.ExamSetupDetail.ExamSetupID = dbo.ExamSetupMaster.ExamSetupID AND " +
                                       " dbo.ExamSetupDetail.BranchID = dbo.ExamSetupMaster.BranchID AND dbo.ExamSetupDetail.CompID = dbo.ExamSetupMaster.CompID AND dbo.ExamResult.ClassID = dbo.ExamSetupMaster.ClassID AND dbo.ExamResult.SessionID = dbo.ExamSetupMaster.SessionID " +
                                       " WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.ExamResult.StudentID IN (" + StudentIds + ")) " +
                                       " AND (dbo.ExamResult.SessionID =" + SESSION_ID + ") AND (dbo.ExamSetupMaster.SessionID =" + SESSION_ID + ") AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND " +
                                       " (dbo.ExamResult.BranchID =" + BRANCH_ID + ") AND (dbo.SubjectLevelOne.SubjectCategoryID = 3) AND (dbo.ExamSetupMaster.ExamID =" + ExamId + ")" +
                                       " and dbo.ExamSetupMaster.SubExamID= " + SubExamId + " " +
                                       " Order BY dbo.ExamResult.StudentID, dbo.ExamSetupDetail.OrderNo ";
                        }
                    }
                    if (Order == 4)
                    {
                        if (Subjectlevel == 1)
                        {
                            SqlDetail1 = "SELECT TOP (100) PERCENT dbo.SubjectLevelOne.SubjectNameL1 AS Subject, dbo.ExamResult.ObtainMarks1 AS Obtain1,dbo.ExamResult.ObtainMarks2 AS Obtain2,dbo.ExamResult.ObtainMarks3 AS Obtain3,dbo.ExamResult.ObtainMarks4 AS Obtain4, dbo.ExamResult.StudentID, " +
                                        " dbo.ExamSetupDetail.OrderNo,dbo.ExamResult.Grade AS Grade1, dbo.ExamResult.ObtainGrade2 AS Grade2,dbo.ExamResult.ObtainGrade3 AS Grade3,dbo.ExamResult.ObtainGrade4 AS Grade4,ExamResult.MaxMark1 as Max1,ExamResult.MaxMark2 as Max2,ExamResult.MaxMark3 as Max3,ExamResult.MaxMark4 as Max4,ExamResult.MinMark1 as Min1,ExamResult.MinMark2 as Min2,ExamResult.MinMark3 as Min3,ExamResult.MinMark4 as Min4 " +
                                        " FROM dbo.ExamResult INNER JOIN dbo.SubjectLevelOne ON dbo.ExamResult.SubjectIDL1 = dbo.SubjectLevelOne.IdL1 INNER JOIN " +
                                        " dbo.ExamSetupDetail ON dbo.SubjectLevelOne.IdL1 = dbo.ExamSetupDetail.SubjectIDL1 AND dbo.SubjectLevelOne.BranchID = dbo.ExamSetupDetail.BranchID AND " +
                                        " dbo.SubjectLevelOne.CompID = dbo.ExamSetupDetail.CompID INNER JOIN dbo.ExamSetupMaster ON dbo.ExamSetupDetail.ExamSetupID = dbo.ExamSetupMaster.ExamSetupID AND " +
                                        " dbo.ExamSetupDetail.BranchID = dbo.ExamSetupMaster.BranchID AND dbo.ExamSetupDetail.CompID = dbo.ExamSetupMaster.CompID AND dbo.ExamResult.ClassID = dbo.ExamSetupMaster.ClassID AND dbo.ExamResult.SessionID = dbo.ExamSetupMaster.SessionID " +
                                        " WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.ExamResult.StudentID IN (" + StudentIds + ")) " +
                                        " AND (dbo.ExamResult.SessionID =" + SESSION_ID + ") AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND " +
                                        " (dbo.ExamResult.BranchID =" + BRANCH_ID + ") AND (dbo.SubjectLevelOne.SubjectCategoryID = 1) AND (dbo.ExamSetupMaster.ExamID =" + ExamId + ")" +
                                        " and dbo.ExamSetupMaster.SubExamID= " + SubExamId + " " +
                                        " Order BY dbo.ExamResult.StudentID, dbo.ExamSetupDetail.OrderNo ";


                            SqlDetail2 = "SELECT TOP (100) PERCENT dbo.SubjectLevelOne.SubjectNameL1 AS Subject, dbo.ExamResult.ObtainMarks1 AS Obtain1,dbo.ExamResult.ObtainMarks2 AS Obtain2,dbo.ExamResult.ObtainMarks3 AS Obtain3,dbo.ExamResult.ObtainMarks4 AS Obtain4, dbo.ExamResult.StudentID, " +
                                        " dbo.ExamSetupDetail.OrderNo FROM dbo.ExamResult INNER JOIN dbo.SubjectLevelOne ON dbo.ExamResult.SubjectIDL1 = dbo.SubjectLevelOne.IdL1 INNER JOIN " +
                                        " dbo.ExamSetupDetail ON dbo.SubjectLevelOne.IdL1 = dbo.ExamSetupDetail.SubjectIDL1 AND dbo.SubjectLevelOne.BranchID = dbo.ExamSetupDetail.BranchID AND " +
                                        " dbo.SubjectLevelOne.CompID = dbo.ExamSetupDetail.CompID INNER JOIN dbo.ExamSetupMaster ON dbo.ExamSetupDetail.ExamSetupID = dbo.ExamSetupMaster.ExamSetupID AND " +
                                        " dbo.ExamSetupDetail.BranchID = dbo.ExamSetupMaster.BranchID AND dbo.ExamSetupDetail.CompID = dbo.ExamSetupMaster.CompID AND dbo.ExamResult.ClassID = dbo.ExamSetupMaster.ClassID AND dbo.ExamResult.SessionID = dbo.ExamSetupMaster.SessionID " +
                                        " WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.ExamResult.StudentID IN (" + StudentIds + ")) " +
                                        " AND (dbo.ExamResult.SessionID =" + SESSION_ID + ") AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND " +
                                        " (dbo.ExamResult.BranchID =" + BRANCH_ID + ") AND (dbo.SubjectLevelOne.SubjectCategoryID = 2) AND (dbo.ExamSetupMaster.ExamID =" + ExamId + ")" +
                                        " and dbo.ExamSetupMaster.SubExamID= " + SubExamId + " " +
                                        " Order BY dbo.ExamResult.StudentID, dbo.ExamSetupDetail.OrderNo ";


                            SqlDetail3 = "SELECT TOP (100) PERCENT dbo.SubjectLevelOne.SubjectNameL1 AS Subject, dbo.ExamResult.ObtainMarks1 AS Obtain1,dbo.ExamResult.ObtainMarks2 AS Obtain2,dbo.ExamResult.ObtainMarks3 AS Obtain3,dbo.ExamResult.ObtainMarks4 AS Obtain4, dbo.ExamResult.StudentID, " +
                                       " dbo.ExamSetupDetail.OrderNo FROM dbo.ExamResult INNER JOIN dbo.SubjectLevelOne ON dbo.ExamResult.SubjectIDL1 = dbo.SubjectLevelOne.IdL1 INNER JOIN " +
                                       " dbo.ExamSetupDetail ON dbo.SubjectLevelOne.IdL1 = dbo.ExamSetupDetail.SubjectIDL1 AND dbo.SubjectLevelOne.BranchID = dbo.ExamSetupDetail.BranchID AND " +
                                       " dbo.SubjectLevelOne.CompID = dbo.ExamSetupDetail.CompID INNER JOIN dbo.ExamSetupMaster ON dbo.ExamSetupDetail.ExamSetupID = dbo.ExamSetupMaster.ExamSetupID AND " +
                                       " dbo.ExamSetupDetail.BranchID = dbo.ExamSetupMaster.BranchID AND dbo.ExamSetupDetail.CompID = dbo.ExamSetupMaster.CompID AND dbo.ExamResult.ClassID = dbo.ExamSetupMaster.ClassID AND dbo.ExamResult.SessionID = dbo.ExamSetupMaster.SessionID " +
                                       " WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.ExamResult.StudentID IN (" + StudentIds + ")) " +
                                       " AND (dbo.ExamResult.SessionID =" + SESSION_ID + ") AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND " +
                                       " (dbo.ExamResult.BranchID =" + BRANCH_ID + ") AND (dbo.SubjectLevelOne.SubjectCategoryID = 3) AND (dbo.ExamSetupMaster.ExamID =" + ExamId + ")" +
                                       " and dbo.ExamSetupMaster.SubExamID= " + SubExamId + " " +
                                       " Order BY dbo.ExamResult.StudentID, dbo.ExamSetupDetail.OrderNo ";
                        }


                    }



                }



                #endregion

                #region Term Two
                if (ExamId == TermTwoID)
                {

                    //change by praveen


                    if (Order == 2)
                    {
                        if (Subjectlevel == 1)
                        {

                            SqlDetail1 = "SELECT TOP (100) PERCENT dbo.SubjectLevelOne.SubjectNameL1 AS Subject, " +
                                    " dbo.ExamResult.ObtainMarks1 AS Obtain1,dbo.ExamResult.ObtainMarks2 AS Obtain2,dbo.ExamResult.ObtainMarks3 AS Obtain3,dbo.ExamResult.ObtainMarks4 AS Obtain4, dbo.ExamResult.StudentID, " +
                                    " dbo.ExamSetupDetail.OrderNo,dbo.ExamResult.Grade AS Grade1, dbo.ExamResult.ObtainGrade2 AS Grade2,dbo.ExamResult.ObtainGrade3 AS Grade3,dbo.ExamResult.ObtainGrade4 AS Grade4,ExamResult.MaxMark1 as Max1,ExamResult.MaxMark2 as Max2,ExamResult.MaxMark3 as Max3,ExamResult.MaxMark4 as Max4,ExamResult.MinMark1 as Min1,ExamResult.MinMark2 as Min2,ExamResult.MinMark3 as Min3,ExamResult.MinMark4 as Min4" +
                                    " FROM dbo.ExamResult INNER JOIN dbo.SubjectLevelOne ON dbo.ExamResult.SubjectIDL1 = dbo.SubjectLevelOne.IdL1 INNER JOIN " +
                                    " dbo.ExamSetupDetail ON dbo.SubjectLevelOne.IdL1 = dbo.ExamSetupDetail.SubjectIDL1 AND dbo.SubjectLevelOne.BranchID = dbo.ExamSetupDetail.BranchID AND " +
                                    " dbo.SubjectLevelOne.CompID = dbo.ExamSetupDetail.CompID INNER JOIN dbo.ExamSetupMaster ON dbo.ExamSetupDetail.ExamSetupID = dbo.ExamSetupMaster.ExamSetupID AND " +
                                    " dbo.ExamSetupDetail.BranchID = dbo.ExamSetupMaster.BranchID AND dbo.ExamSetupDetail.CompID = dbo.ExamSetupMaster.CompID AND dbo.ExamResult.ClassID = dbo.ExamSetupMaster.ClassID AND dbo.ExamResult.SessionID = dbo.ExamSetupMaster.SessionID " +
                                    "  WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.ExamResult.StudentID IN (" + StudentIds + ")) " +
                                    " AND (dbo.ExamResult.SessionID =" + SESSION_ID + ") AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND " +
                                    " (dbo.ExamResult.BranchID =" + BRANCH_ID + ") AND (dbo.SubjectLevelOne.SubjectCategoryID = 1) AND (dbo.ExamSetupMaster.ExamID =" + ExamId + ")" +
                                     " and dbo.ExamSetupMaster.SubExamID= " + SubExamId + " " +
                                    " Order BY dbo.ExamResult.StudentID, dbo.ExamSetupDetail.OrderNo ";


                            SqlDetail2 = "SELECT TOP (100) PERCENT dbo.SubjectLevelOne.SubjectNameL1 AS Subject,dbo.ExamResult.ObtainMarks1 AS Obtain1,dbo.ExamResult.ObtainMarks2 AS Obtain2,dbo.ExamResult.ObtainMarks3 AS Obtain3,dbo.ExamResult.ObtainMarks4 AS Obtain4, dbo.ExamResult.StudentID, " +
                                        " dbo.ExamSetupDetail.OrderNo FROM dbo.ExamResult INNER JOIN dbo.SubjectLevelOne ON dbo.ExamResult.SubjectIDL1 = dbo.SubjectLevelOne.IdL1 INNER JOIN " +
                                        " dbo.ExamSetupDetail ON dbo.SubjectLevelOne.IdL1 = dbo.ExamSetupDetail.SubjectIDL1 AND dbo.SubjectLevelOne.BranchID = dbo.ExamSetupDetail.BranchID AND " +
                                        " dbo.SubjectLevelOne.CompID = dbo.ExamSetupDetail.CompID INNER JOIN dbo.ExamSetupMaster ON dbo.ExamSetupDetail.ExamSetupID = dbo.ExamSetupMaster.ExamSetupID AND " +
                                        " dbo.ExamSetupDetail.BranchID = dbo.ExamSetupMaster.BranchID AND dbo.ExamSetupDetail.CompID = dbo.ExamSetupMaster.CompID AND dbo.ExamResult.ClassID = dbo.ExamSetupMaster.ClassID AND dbo.ExamResult.SessionID = dbo.ExamSetupMaster.SessionID " +
                                        "  WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.ExamResult.StudentID IN (" + StudentIds + ")) " +
                                        " AND (dbo.ExamResult.SessionID =" + SESSION_ID + ") AND (dbo.ExamSetupMaster.SessionID =" + SESSION_ID + ") AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND " +
                                        " (dbo.ExamResult.BranchID =" + BRANCH_ID + ") AND (dbo.SubjectLevelOne.SubjectCategoryID = 2) AND (dbo.ExamSetupMaster.ExamID =" + ExamId + ")" +
                                         " and dbo.ExamSetupMaster.SubExamID= " + SubExamId + " " +
                                        " Order BY dbo.ExamResult.StudentID, dbo.ExamSetupDetail.OrderNo ";


                            SqlDetail3 = "SELECT TOP (100) PERCENT dbo.SubjectLevelOne.SubjectNameL1 AS Subject, dbo.ExamResult.ObtainMarks1 AS Obtain1,dbo.ExamResult.ObtainMarks2 AS Obtain2,dbo.ExamResult.ObtainMarks3 AS Obtain3,dbo.ExamResult.ObtainMarks4 AS Obtain4, dbo.ExamResult.StudentID, " +
                                       " dbo.ExamSetupDetail.OrderNo FROM dbo.ExamResult INNER JOIN dbo.SubjectLevelOne ON dbo.ExamResult.SubjectIDL1 = dbo.SubjectLevelOne.IdL1 INNER JOIN " +
                                       " dbo.ExamSetupDetail ON dbo.SubjectLevelOne.IdL1 = dbo.ExamSetupDetail.SubjectIDL1 AND dbo.SubjectLevelOne.BranchID = dbo.ExamSetupDetail.BranchID AND " +
                                       " dbo.SubjectLevelOne.CompID = dbo.ExamSetupDetail.CompID INNER JOIN dbo.ExamSetupMaster ON dbo.ExamSetupDetail.ExamSetupID = dbo.ExamSetupMaster.ExamSetupID AND " +
                                       " dbo.ExamSetupDetail.BranchID = dbo.ExamSetupMaster.BranchID AND dbo.ExamSetupDetail.CompID = dbo.ExamSetupMaster.CompID AND dbo.ExamResult.ClassID = dbo.ExamSetupMaster.ClassID AND dbo.ExamResult.SessionID = dbo.ExamSetupMaster.SessionID " +
                                       " WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.ExamResult.StudentID IN (" + StudentIds + ")) " +
                                       " AND (dbo.ExamResult.SessionID =" + SESSION_ID + ") AND (dbo.ExamSetupMaster.SessionID =" + SESSION_ID + ") AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND " +
                                       " (dbo.ExamResult.BranchID =" + BRANCH_ID + ") AND (dbo.SubjectLevelOne.SubjectCategoryID = 3) AND (dbo.ExamSetupMaster.ExamID =" + ExamId + ")" +
                                         " and dbo.ExamSetupMaster.SubExamID= " + SubExamId + " " +
                                       " Order BY dbo.ExamResult.StudentID, dbo.ExamSetupDetail.OrderNo ";
                        }
                        if (Subjectlevel == 2)
                        {

                            SqlDetail1 = "SELECT TOP (100) PERCENT dbo.SubjectLevelOne.SubjectNameL1 AS Subject,dbo.SubjectLevelTwo.SubjectNameL2 AS SubjectD," +
                                       " dbo.ExamResult.ObtainMarks1 AS Obtain1,dbo.ExamResult.ObtainMarks2 AS Obtain2,dbo.ExamResult.ObtainMarks3 AS Obtain3,dbo.ExamResult.ObtainMarks4 AS Obtain4, dbo.ExamResult.StudentID, " +
                                       " dbo.ExamSetupDetail.OrderNo,dbo.ExamResult.Grade AS Grade1, dbo.ExamResult.ObtainGrade2 AS Grade2,dbo.ExamResult.ObtainGrade3 AS Grade3,dbo.ExamResult.ObtainGrade4 AS Grade4, ExamResult.MaxMark1 as Max1,ExamResult.MaxMark2 as Max2,ExamResult.MaxMark3 as Max3,ExamResult.MaxMark4 as Max4,ExamResult.MinMark1 as Min1,ExamResult.MinMark2 as Min2,ExamResult.MinMark3 as Min3,ExamResult.MinMark4 as Min4 " +
                                       " FROM dbo.ExamResult INNER JOIN dbo.SubjectLevelOne ON dbo.ExamResult.SubjectIDL1 = dbo.SubjectLevelOne.IdL1 INNER JOIN " +
                                       " dbo.ExamSetupDetail ON dbo.SubjectLevelOne.IdL1 = dbo.ExamSetupDetail.SubjectIDL1 AND dbo.SubjectLevelOne.BranchID = dbo.ExamSetupDetail.BranchID AND " +
                                       " dbo.SubjectLevelOne.CompID = dbo.ExamSetupDetail.CompID " +
                                       " INNER JOIN dbo.SubjectLevelTwo ON dbo.ExamResult.SubjectIDL2 = dbo.SubjectLevelTwo.IdL2 AND dbo.ExamSetupDetail.SubjectIDL2 = dbo.SubjectLevelTwo.IdL2 " +
                                       " INNER JOIN dbo.ExamSetupMaster ON dbo.ExamSetupDetail.ExamSetupID = dbo.ExamSetupMaster.ExamSetupID AND " +
                                       " dbo.ExamSetupDetail.BranchID = dbo.ExamSetupMaster.BranchID AND dbo.ExamSetupDetail.CompID = dbo.ExamSetupMaster.CompID AND dbo.ExamResult.ClassID = dbo.ExamSetupMaster.ClassID AND dbo.ExamResult.SessionID = dbo.ExamSetupMaster.SessionID " +
                                       " WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.ExamResult.StudentID IN (" + StudentIds + ")) " +
                                       " AND (dbo.ExamResult.SessionID =" + SESSION_ID + ") AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND " +
                                       " (dbo.ExamResult.BranchID =" + BRANCH_ID + ") AND (dbo.SubjectLevelOne.SubjectCategoryID = 1) AND (dbo.ExamSetupMaster.ExamID =" + ExamId + ")" +
                                       " and dbo.ExamSetupMaster.SubExamID= " + SubExamId + " " +
                                       " Order BY dbo.ExamResult.StudentID, dbo.ExamSetupDetail.OrderNo ";


                            SqlDetail2 = "SELECT TOP (100) PERCENT dbo.SubjectLevelOne.SubjectNameL1 AS Subject,dbo.SubjectLevelTwo.SubjectNameL2 AS SubjectD,dbo.ExamResult.ObtainMarks1 AS Obtain1,dbo.ExamResult.ObtainMarks2 AS Obtain2,dbo.ExamResult.ObtainMarks3 AS Obtain3,dbo.ExamResult.ObtainMarks4 AS Obtain4, dbo.ExamResult.StudentID, " +
                                    " dbo.ExamSetupDetail.OrderNo,ExamResult.MaxMark1 as Max1,ExamResult.MaxMark2 as Max2,ExamResult.MaxMark3 as Max3,ExamResult.MaxMark4 as Max4,ExamResult.MinMark1 as Min1,ExamResult.MinMark2 as Min2,ExamResult.MinMark3 as Min3,ExamResult.MinMark4 as Min4 " +
                                    " FROM dbo.ExamResult INNER JOIN dbo.SubjectLevelOne ON dbo.ExamResult.SubjectIDL1 = dbo.SubjectLevelOne.IdL1 INNER JOIN " +
                                    " dbo.ExamSetupDetail ON dbo.SubjectLevelOne.IdL1 = dbo.ExamSetupDetail.SubjectIDL1 AND dbo.SubjectLevelOne.BranchID = dbo.ExamSetupDetail.BranchID AND " +
                                    " dbo.SubjectLevelOne.CompID = dbo.ExamSetupDetail.CompID " +
                                    " INNER JOIN dbo.SubjectLevelTwo ON dbo.ExamResult.SubjectIDL2 = dbo.SubjectLevelTwo.IdL2 AND dbo.ExamSetupDetail.SubjectIDL2 = dbo.SubjectLevelTwo.IdL2 " +
                                    " INNER JOIN dbo.ExamSetupMaster ON dbo.ExamSetupDetail.ExamSetupID = dbo.ExamSetupMaster.ExamSetupID AND " +
                                    " dbo.ExamSetupDetail.BranchID = dbo.ExamSetupMaster.BranchID AND dbo.ExamSetupDetail.CompID = dbo.ExamSetupMaster.CompID AND dbo.ExamResult.ClassID = dbo.ExamSetupMaster.ClassID " +
                                    " WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.ExamResult.StudentID IN (" + StudentIds + ")) " +
                                    " AND (dbo.ExamResult.SessionID =" + SESSION_ID + ")  AND (dbo.ExamSetupMaster.SessionID =" + SESSION_ID + ") AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND " +
                                    " (dbo.ExamResult.BranchID =" + BRANCH_ID + ") AND (dbo.SubjectLevelOne.SubjectCategoryID = 2) AND (dbo.ExamSetupMaster.ExamID =" + ExamId + ")" +
                                    " and dbo.ExamSetupMaster.SubExamID= " + SubExamId + " " +
                                    " Order BY dbo.ExamResult.StudentID, dbo.ExamSetupDetail.OrderNo ";


                            SqlDetail3 = "SELECT TOP (100) PERCENT dbo.SubjectLevelOne.SubjectNameL1 AS Subject,dbo.SubjectLevelTwo.SubjectNameL2 AS SubjectD,dbo.ExamResult.ObtainMarks1 AS Obtain1,dbo.ExamResult.ObtainMarks2 AS Obtain2,dbo.ExamResult.ObtainMarks3 AS Obtain3,dbo.ExamResult.ObtainMarks4 AS Obtain4, dbo.ExamResult.StudentID, " +
                                        " dbo.ExamSetupDetail.OrderNo,ExamResult.MaxMark1 as Max1,ExamResult.MaxMark2 as Max2,ExamResult.MaxMark3 as Max3,ExamResult.MaxMark4 as Max4,ExamResult.MinMark1 as Min1,ExamResult.MinMark2 as Min2,ExamResult.MinMark3 as Min3,ExamResult.MinMark4 as Min4 " +
                                        " FROM dbo.ExamResult INNER JOIN dbo.SubjectLevelOne ON dbo.ExamResult.SubjectIDL1 = dbo.SubjectLevelOne.IdL1 INNER JOIN " +
                                        " dbo.ExamSetupDetail ON dbo.SubjectLevelOne.IdL1 = dbo.ExamSetupDetail.SubjectIDL1 AND dbo.SubjectLevelOne.BranchID = dbo.ExamSetupDetail.BranchID AND " +
                                        " dbo.SubjectLevelOne.CompID = dbo.ExamSetupDetail.CompID " +
                                        " INNER JOIN dbo.SubjectLevelTwo ON dbo.ExamResult.SubjectIDL2 = dbo.SubjectLevelTwo.IdL2 AND dbo.ExamSetupDetail.SubjectIDL2 = dbo.SubjectLevelTwo.IdL2 " +
                                        " INNER JOIN dbo.ExamSetupMaster ON dbo.ExamSetupDetail.ExamSetupID = dbo.ExamSetupMaster.ExamSetupID AND " +
                                        " dbo.ExamSetupDetail.BranchID = dbo.ExamSetupMaster.BranchID AND dbo.ExamSetupDetail.CompID = dbo.ExamSetupMaster.CompID AND dbo.ExamResult.ClassID = dbo.ExamSetupMaster.ClassID " +
                                       " WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.ExamResult.StudentID IN (" + StudentIds + ")) " +
                                       " AND (dbo.ExamResult.SessionID =" + SESSION_ID + ") AND (dbo.ExamSetupMaster.SessionID =" + SESSION_ID + ") AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND " +
                                       " (dbo.ExamResult.BranchID =" + BRANCH_ID + ") AND (dbo.SubjectLevelOne.SubjectCategoryID = 3) AND (dbo.ExamSetupMaster.ExamID =" + ExamId + ")" +
                                      " and dbo.ExamSetupMaster.SubExamID= " + SubExamId + " " +
                                       " Order BY dbo.ExamResult.StudentID, dbo.ExamSetupDetail.OrderNo ";
                        }

                    }
                    if (Order == 3)
                    {
                        if (Subjectlevel == 1)
                        {

                            SqlDetail1 = "SELECT TOP (100) PERCENT dbo.SubjectLevelOne.SubjectNameL1 AS Subject, " +
                            " dbo.ExamResult.ObtainMarks1 AS Obtain1, dbo.ExamResult.ObtainMarks2 AS Obtain2,dbo.ExamResult.ObtainMarks3 AS Obtain3,dbo.ExamResult.ObtainMarks4 AS Obtain4, dbo.ExamResult.StudentID, " +
                            " dbo.ExamSetupDetail.OrderNo,dbo.ExamResult.Grade AS Grade1, dbo.ExamResult.ObtainGrade2 AS Grade2,dbo.ExamResult.ObtainGrade3 AS Grade3,dbo.ExamResult.ObtainGrade4 AS Grade4,ExamResult.MaxMark1 as Max1,ExamResult.MaxMark2 as Max2,ExamResult.MaxMark3 as Max3,ExamResult.MaxMark4 as Max4,ExamResult.MinMark1 as Min1,ExamResult.MinMark2 as Min2,ExamResult.MinMark3 as Min3,ExamResult.MinMark4 as Min4 " +
                            " FROM dbo.ExamResult INNER JOIN dbo.SubjectLevelOne ON dbo.ExamResult.SubjectIDL1 = dbo.SubjectLevelOne.IdL1 INNER JOIN " +
                            " dbo.ExamSetupDetail ON dbo.SubjectLevelOne.IdL1 = dbo.ExamSetupDetail.SubjectIDL1 AND dbo.SubjectLevelOne.BranchID = dbo.ExamSetupDetail.BranchID AND " +
                            " dbo.SubjectLevelOne.CompID = dbo.ExamSetupDetail.CompID INNER JOIN dbo.ExamSetupMaster ON dbo.ExamSetupDetail.ExamSetupID = dbo.ExamSetupMaster.ExamSetupID AND " +
                            " dbo.ExamSetupDetail.BranchID = dbo.ExamSetupMaster.BranchID AND dbo.ExamSetupDetail.CompID = dbo.ExamSetupMaster.CompID AND dbo.ExamResult.ClassID = dbo.ExamSetupMaster.ClassID AND dbo.ExamResult.SessionID = dbo.ExamSetupMaster.SessionID " +
                            " WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.ExamResult.StudentID IN (" + StudentIds + ")) " +
                            " AND (dbo.ExamResult.SessionID =" + SESSION_ID + ") AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND " +
                            " (dbo.ExamResult.BranchID =" + BRANCH_ID + ") AND (dbo.SubjectLevelOne.SubjectCategoryID = 1) AND (dbo.ExamSetupMaster.ExamID =" + ExamId + ")" +
                            " and dbo.ExamSetupMaster.SubExamID= " + SubExamId + " " +
                            " Order BY dbo.ExamResult.StudentID, dbo.ExamSetupDetail.OrderNo ";


                            SqlDetail2 = "SELECT TOP (100) PERCENT dbo.SubjectLevelOne.SubjectNameL1 AS Subject, dbo.ExamResult.ObtainMarks1 AS Obtain1,dbo.ExamResult.ObtainMarks2 AS Obtain2,dbo.ExamResult.ObtainMarks3 AS Obtain3,dbo.ExamResult.ObtainMarks4 AS Obtain4, dbo.ExamResult.StudentID, " +
                                        " dbo.ExamSetupDetail.OrderNo FROM dbo.ExamResult INNER JOIN dbo.SubjectLevelOne ON dbo.ExamResult.SubjectIDL1 = dbo.SubjectLevelOne.IdL1 INNER JOIN " +
                                        " dbo.ExamSetupDetail ON dbo.SubjectLevelOne.IdL1 = dbo.ExamSetupDetail.SubjectIDL1 AND dbo.SubjectLevelOne.BranchID = dbo.ExamSetupDetail.BranchID AND " +
                                        " dbo.SubjectLevelOne.CompID = dbo.ExamSetupDetail.CompID INNER JOIN dbo.ExamSetupMaster ON dbo.ExamSetupDetail.ExamSetupID = dbo.ExamSetupMaster.ExamSetupID AND " +
                                        " dbo.ExamSetupDetail.BranchID = dbo.ExamSetupMaster.BranchID AND dbo.ExamSetupDetail.CompID = dbo.ExamSetupMaster.CompID AND dbo.ExamResult.ClassID = dbo.ExamSetupMaster.ClassID AND dbo.ExamResult.SessionID = dbo.ExamSetupMaster.SessionID " +
                                        " WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.ExamResult.StudentID IN (" + StudentIds + ")) " +
                                        " AND (dbo.ExamResult.SessionID =" + SESSION_ID + ") AND (dbo.ExamSetupMaster.SessionID =" + SESSION_ID + ") AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND " +
                                        " (dbo.ExamResult.BranchID =" + BRANCH_ID + ") AND (dbo.SubjectLevelOne.SubjectCategoryID = 2) AND (dbo.ExamSetupMaster.ExamID =" + ExamId + ")" +
                                        " and dbo.ExamSetupMaster.SubExamID= " + SubExamId + " " +
                                        " Order BY dbo.ExamResult.StudentID, dbo.ExamSetupDetail.OrderNo ";


                            SqlDetail3 = "SELECT TOP (100) PERCENT dbo.SubjectLevelOne.SubjectNameL1 AS Subject, dbo.ExamResult.ObtainMarks1 AS Obtain1,dbo.ExamResult.ObtainMarks2 AS Obtain2,dbo.ExamResult.ObtainMarks3 AS Obtain3,dbo.ExamResult.ObtainMarks4 AS Obtain4, dbo.ExamResult.StudentID, " +
                                       " dbo.ExamSetupDetail.OrderNo FROM dbo.ExamResult INNER JOIN dbo.SubjectLevelOne ON dbo.ExamResult.SubjectIDL1 = dbo.SubjectLevelOne.IdL1 INNER JOIN " +
                                       " dbo.ExamSetupDetail ON dbo.SubjectLevelOne.IdL1 = dbo.ExamSetupDetail.SubjectIDL1 AND dbo.SubjectLevelOne.BranchID = dbo.ExamSetupDetail.BranchID AND " +
                                       " dbo.SubjectLevelOne.CompID = dbo.ExamSetupDetail.CompID INNER JOIN dbo.ExamSetupMaster ON dbo.ExamSetupDetail.ExamSetupID = dbo.ExamSetupMaster.ExamSetupID AND " +
                                       " dbo.ExamSetupDetail.BranchID = dbo.ExamSetupMaster.BranchID AND dbo.ExamSetupDetail.CompID = dbo.ExamSetupMaster.CompID AND dbo.ExamResult.ClassID = dbo.ExamSetupMaster.ClassID AND dbo.ExamResult.SessionID = dbo.ExamSetupMaster.SessionID " +
                                       " WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.ExamResult.StudentID IN (" + StudentIds + ")) " +
                                       " AND (dbo.ExamResult.SessionID =" + SESSION_ID + ") AND (dbo.ExamSetupMaster.SessionID =" + SESSION_ID + ") AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND " +
                                       " (dbo.ExamResult.BranchID =" + BRANCH_ID + ") AND (dbo.SubjectLevelOne.SubjectCategoryID = 3) AND (dbo.ExamSetupMaster.ExamID =" + ExamId + ")" +
                                       " and dbo.ExamSetupMaster.SubExamID= " + SubExamId + " " +
                                       " Order BY dbo.ExamResult.StudentID, dbo.ExamSetupDetail.OrderNo ";
                        }
                    }
                    if (Order == 4)
                    {
                        if (Subjectlevel == 1)
                        {
                            SqlDetail1 = "SELECT TOP (100) PERCENT dbo.SubjectLevelOne.SubjectNameL1 AS Subject, dbo.ExamResult.ObtainMarks1 AS Obtain1,dbo.ExamResult.ObtainMarks2 AS Obtain2,dbo.ExamResult.ObtainMarks3 AS Obtain3,dbo.ExamResult.ObtainMarks4 AS Obtain4, dbo.ExamResult.StudentID, " +
                                        " dbo.ExamSetupDetail.OrderNo,dbo.ExamResult.Grade AS Grade1, dbo.ExamResult.ObtainGrade2 AS Grade2,dbo.ExamResult.ObtainGrade3 AS Grade3,dbo.ExamResult.ObtainGrade4 AS Grade4,ExamResult.MaxMark1 as Max1,ExamResult.MaxMark2 as Max2,ExamResult.MaxMark3 as Max3,ExamResult.MaxMark4 as Max4,ExamResult.MinMark1 as Min1,ExamResult.MinMark2 as Min2,ExamResult.MinMark3 as Min3,ExamResult.MinMark4 as Min4 " +
                                        " FROM dbo.ExamResult INNER JOIN dbo.SubjectLevelOne ON dbo.ExamResult.SubjectIDL1 = dbo.SubjectLevelOne.IdL1 INNER JOIN " +
                                        " dbo.ExamSetupDetail ON dbo.SubjectLevelOne.IdL1 = dbo.ExamSetupDetail.SubjectIDL1 AND dbo.SubjectLevelOne.BranchID = dbo.ExamSetupDetail.BranchID AND " +
                                        " dbo.SubjectLevelOne.CompID = dbo.ExamSetupDetail.CompID INNER JOIN dbo.ExamSetupMaster ON dbo.ExamSetupDetail.ExamSetupID = dbo.ExamSetupMaster.ExamSetupID AND " +
                                        " dbo.ExamSetupDetail.BranchID = dbo.ExamSetupMaster.BranchID AND dbo.ExamSetupDetail.CompID = dbo.ExamSetupMaster.CompID AND dbo.ExamResult.ClassID = dbo.ExamSetupMaster.ClassID AND dbo.ExamResult.SessionID = dbo.ExamSetupMaster.SessionID " +
                                        " WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.ExamResult.StudentID IN (" + StudentIds + ")) " +
                                        " AND (dbo.ExamResult.SessionID =" + SESSION_ID + ") AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND " +
                                        " (dbo.ExamResult.BranchID =" + BRANCH_ID + ") AND (dbo.SubjectLevelOne.SubjectCategoryID = 1) AND (dbo.ExamSetupMaster.ExamID =" + ExamId + ")" +
                                        " and dbo.ExamSetupMaster.SubExamID= " + SubExamId + " " +
                                        " Order BY dbo.ExamResult.StudentID, dbo.ExamSetupDetail.OrderNo ";


                            SqlDetail2 = "SELECT TOP (100) PERCENT dbo.SubjectLevelOne.SubjectNameL1 AS Subject, dbo.ExamResult.ObtainMarks1 AS Obtain1,dbo.ExamResult.ObtainMarks2 AS Obtain2,dbo.ExamResult.ObtainMarks3 AS Obtain3,dbo.ExamResult.ObtainMarks4 AS Obtain4, dbo.ExamResult.StudentID, " +
                                        " dbo.ExamSetupDetail.OrderNo FROM dbo.ExamResult INNER JOIN dbo.SubjectLevelOne ON dbo.ExamResult.SubjectIDL1 = dbo.SubjectLevelOne.IdL1 INNER JOIN " +
                                        " dbo.ExamSetupDetail ON dbo.SubjectLevelOne.IdL1 = dbo.ExamSetupDetail.SubjectIDL1 AND dbo.SubjectLevelOne.BranchID = dbo.ExamSetupDetail.BranchID AND " +
                                        " dbo.SubjectLevelOne.CompID = dbo.ExamSetupDetail.CompID INNER JOIN dbo.ExamSetupMaster ON dbo.ExamSetupDetail.ExamSetupID = dbo.ExamSetupMaster.ExamSetupID AND " +
                                        " dbo.ExamSetupDetail.BranchID = dbo.ExamSetupMaster.BranchID AND dbo.ExamSetupDetail.CompID = dbo.ExamSetupMaster.CompID AND dbo.ExamResult.ClassID = dbo.ExamSetupMaster.ClassID AND dbo.ExamResult.SessionID = dbo.ExamSetupMaster.SessionID " +
                                        " WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.ExamResult.StudentID IN (" + StudentIds + ")) " +
                                        " AND (dbo.ExamResult.SessionID =" + SESSION_ID + ") AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND " +
                                        " (dbo.ExamResult.BranchID =" + BRANCH_ID + ") AND (dbo.SubjectLevelOne.SubjectCategoryID = 2) AND (dbo.ExamSetupMaster.ExamID =" + ExamId + ")" +
                                        " and dbo.ExamSetupMaster.SubExamID= " + SubExamId + " " +
                                        " Order BY dbo.ExamResult.StudentID, dbo.ExamSetupDetail.OrderNo ";


                            SqlDetail3 = "SELECT TOP (100) PERCENT dbo.SubjectLevelOne.SubjectNameL1 AS Subject, dbo.ExamResult.ObtainMarks1 AS Obtain1,dbo.ExamResult.ObtainMarks2 AS Obtain2,dbo.ExamResult.ObtainMarks3 AS Obtain3,dbo.ExamResult.ObtainMarks4 AS Obtain4, dbo.ExamResult.StudentID, " +
                                       " dbo.ExamSetupDetail.OrderNo FROM dbo.ExamResult INNER JOIN dbo.SubjectLevelOne ON dbo.ExamResult.SubjectIDL1 = dbo.SubjectLevelOne.IdL1 INNER JOIN " +
                                       " dbo.ExamSetupDetail ON dbo.SubjectLevelOne.IdL1 = dbo.ExamSetupDetail.SubjectIDL1 AND dbo.SubjectLevelOne.BranchID = dbo.ExamSetupDetail.BranchID AND " +
                                       " dbo.SubjectLevelOne.CompID = dbo.ExamSetupDetail.CompID INNER JOIN dbo.ExamSetupMaster ON dbo.ExamSetupDetail.ExamSetupID = dbo.ExamSetupMaster.ExamSetupID AND " +
                                       " dbo.ExamSetupDetail.BranchID = dbo.ExamSetupMaster.BranchID AND dbo.ExamSetupDetail.CompID = dbo.ExamSetupMaster.CompID AND dbo.ExamResult.ClassID = dbo.ExamSetupMaster.ClassID AND dbo.ExamResult.SessionID = dbo.ExamSetupMaster.SessionID " +
                                       " WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.ExamResult.StudentID IN (" + StudentIds + ")) " +
                                       " AND (dbo.ExamResult.SessionID =" + SESSION_ID + ") AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND " +
                                       " (dbo.ExamResult.BranchID =" + BRANCH_ID + ") AND (dbo.SubjectLevelOne.SubjectCategoryID = 3) AND (dbo.ExamSetupMaster.ExamID =" + ExamId + ")" +
                                       " and dbo.ExamSetupMaster.SubExamID= " + SubExamId + " " +
                                       " Order BY dbo.ExamResult.StudentID, dbo.ExamSetupDetail.OrderNo ";
                        }


                    }

                    //end change by praveen



                    if (Order == 5)
                    {

                        if (Subjectlevel == 1)
                        {


                            SqlDetail1 = "SELECT  TOP (100) PERCENT dbo.SubjectLevelOne.SubjectNameL1 AS Subject, dbo.ExamResult.ObtainMarks1 AS Obtain1, dbo.ExamResult.ObtainMarks2 AS Obtain2, dbo.ExamResult.ObtainMarks3 AS Obtain3, " +
                                        " dbo.ExamResult.ObtainMarks4 AS Obtain4,dbo.ExamResult.ObtainMarks5 AS Obtain5, dbo.ExamResult.ObtainMarks6 AS Obtain6, dbo.ExamResult.ObtainMarks7 AS Obtain7, " +
                                        " dbo.ExamResult.ObtainMarks8 AS Obtain8, dbo.ExamResult.StudentID, dbo.ExamSetupDetail.OrderNo, dbo.ExamResult.Grade AS Grade1, dbo.ExamResult.ObtainGrade2 AS Grade2, " +
                                        " dbo.ExamResult.ObtainGrade3 AS Grade3, dbo.ExamResult.ObtainGrade4 AS Grade4,dbo.ExamResult.ObtainGrade5 AS Grade5, dbo.ExamResult.ObtainGrade6 AS Grade6, " +
                                        " dbo.ExamResult.ObtainGrade7 AS Grade7, dbo.ExamResult.ObtainGrade8 AS Grade8, dbo.ExamResult.MaxMark1 AS Max1, dbo.ExamResult.MaxMark2 AS Max2, dbo.ExamResult.MaxMark3 AS Max3, " +
                                        " dbo.ExamResult.MaxMark4 AS Max4,dbo.ExamResult.MaxMark5 AS Max5, dbo.ExamResult.MaxMark6 AS Max6, dbo.ExamResult.MaxMark7 AS Max7, " +
                                        " dbo.ExamResult.MaxMark8 AS Max8, dbo.ExamResult.MinMark1 AS Min1, dbo.ExamResult.MinMark2 AS Min2, dbo.ExamResult.MinMark3 AS Min3, dbo.ExamResult.MinMark4 AS Min4, " +
                                        " dbo.ExamResult.MinMark5 AS Min5, dbo.ExamResult.MinMark6 AS Min6, dbo.ExamResult.MinMark7 AS Min7, dbo.ExamResult.MinMark8 AS Min8 " +
                                        " FROM dbo.ExamResult INNER JOIN dbo.SubjectLevelOne ON dbo.ExamResult.SubjectIDL1 = dbo.SubjectLevelOne.IdL1 INNER JOIN " +
                                        " dbo.ExamSetupDetail ON dbo.SubjectLevelOne.IdL1 = dbo.ExamSetupDetail.SubjectIDL1 AND dbo.SubjectLevelOne.BranchID = dbo.ExamSetupDetail.BranchID AND " +
                                        " dbo.SubjectLevelOne.CompID = dbo.ExamSetupDetail.CompID INNER JOIN dbo.ExamSetupMaster ON dbo.ExamSetupDetail.ExamSetupID = dbo.ExamSetupMaster.ExamSetupID AND " +
                                        " dbo.ExamSetupDetail.BranchID = dbo.ExamSetupMaster.BranchID AND dbo.ExamSetupDetail.CompID = dbo.ExamSetupMaster.CompID AND dbo.ExamResult.ClassID = dbo.ExamSetupMaster.ClassID AND dbo.ExamResult.SessionID = dbo.ExamSetupMaster.SessionID " +
                                        " WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.ExamResult.StudentID IN (" + StudentIds + ")) " +
                                        " AND (dbo.ExamResult.SessionID =" + SESSION_ID + ") AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND " +
                                        " (dbo.ExamResult.BranchID =" + BRANCH_ID + ") AND (dbo.SubjectLevelOne.SubjectCategoryID = 1) AND (dbo.ExamSetupMaster.ExamID =" + ExamId + ")" +
                                        " and dbo.ExamSetupMaster.SubExamID= " + SubExamId + " " +
                                        " Order BY dbo.ExamResult.StudentID, dbo.ExamSetupDetail.OrderNo ";


                            //sqlDetail = "SELECT TOP (100) PERCENT dbo.SubjectLevelOne.SubjectNameL1 AS Subject," +
                            //           " dbo.ExamResult.ObtainMarks5 AS Obtain1,dbo.ExamResult.ObtainMarks6 AS Obtain2,dbo.ExamResult.ObtainMarks7 AS Obtain3,dbo.ExamResult.ObtainMarks8 AS Obtain4, dbo.ExamResult.StudentID, " +
                            //           " dbo.ExamSetupDetail.OrderNo,dbo.ExamResult.ObtainGrade5 AS Grade1, dbo.ExamResult.ObtainGrade6 AS Grade2,dbo.ExamResult.ObtainGrade7 AS Grade3,dbo.ExamResult.ObtainGrade8 AS Grade4, ExamResult.MaxMark5 as Max1,ExamResult.MaxMark6 as Max2,ExamResult.MaxMark7 as Max3,ExamResult.MaxMark8 as Max4,ExamResult.MinMark5 as Min1,ExamResult.MinMark6 as Min2,ExamResult.MinMark7 as Min3,ExamResult.MinMark8 as Min4 " +
                            //           " FROM dbo.ExamResult INNER JOIN dbo.SubjectLevelOne ON dbo.ExamResult.SubjectIDL1 = dbo.SubjectLevelOne.IdL1 INNER JOIN " +
                            //           " dbo.ExamSetupDetail ON dbo.SubjectLevelOne.IdL1 = dbo.ExamSetupDetail.SubjectIDL1 AND dbo.SubjectLevelOne.BranchID = dbo.ExamSetupDetail.BranchID AND " +
                            //           " dbo.SubjectLevelOne.CompID = dbo.ExamSetupDetail.CompID INNER JOIN dbo.ExamSetupMaster ON dbo.ExamSetupDetail.ExamSetupID = dbo.ExamSetupMaster.ExamSetupID AND " +
                            //           " dbo.ExamSetupDetail.BranchID = dbo.ExamSetupMaster.BranchID AND dbo.ExamSetupDetail.CompID = dbo.ExamSetupMaster.CompID AND dbo.ExamResult.ClassID = dbo.ExamSetupMaster.ClassID AND dbo.ExamResult.SessionID = dbo.ExamSetupMaster.SessionID " +
                            //           " WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.ExamResult.StudentID IN (" + StudentIds + ")) " +
                            //           " AND (dbo.ExamResult.SessionID =" + SESSION_ID + ") AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND " +
                            //           " (dbo.ExamResult.BranchID =" + BRANCH_ID + ") AND (dbo.SubjectLevelOne.SubjectCategoryID = 1) AND (dbo.ExamSetupMaster.ExamID =" + ExamId + ")" +
                            //           " and dbo.ExamSetupMaster.SubExamID= " + SubExamId + " " +
                            //           " Order BY dbo.ExamResult.StudentID, dbo.ExamSetupDetail.OrderNo ";


                            SqlDetail2 = "SELECT TOP (100) PERCENT dbo.SubjectLevelOne.SubjectNameL1 AS Subject," +
                                   "dbo.ExamResult.ObtainMarks1 AS Obtain1, dbo.ExamResult.ObtainMarks2 AS Obtain2, dbo.ExamResult.ObtainMarks3 AS Obtain3," +
                                    " dbo.ExamResult.ObtainMarks4 AS Obtain4,dbo.ExamResult.ObtainMarks5 AS Obtain5, dbo.ExamResult.ObtainMarks6 AS Obtain6, dbo.ExamResult.ObtainMarks7 AS Obtain7, " +
                                   "  dbo.ExamResult.ObtainMarks8 AS Obtain8, dbo.ExamResult.StudentID, " +
                                  " dbo.ExamSetupDetail.OrderNo FROM dbo.ExamResult INNER JOIN dbo.SubjectLevelOne ON dbo.ExamResult.SubjectIDL1 = dbo.SubjectLevelOne.IdL1 INNER JOIN " +
                                  " dbo.ExamSetupDetail ON dbo.SubjectLevelOne.IdL1 = dbo.ExamSetupDetail.SubjectIDL1 AND dbo.SubjectLevelOne.BranchID = dbo.ExamSetupDetail.BranchID AND " +
                                  " dbo.SubjectLevelOne.CompID = dbo.ExamSetupDetail.CompID INNER JOIN dbo.ExamSetupMaster ON dbo.ExamSetupDetail.ExamSetupID = dbo.ExamSetupMaster.ExamSetupID AND " +
                                  " dbo.ExamSetupDetail.BranchID = dbo.ExamSetupMaster.BranchID AND dbo.ExamSetupDetail.CompID = dbo.ExamSetupMaster.CompID AND dbo.ExamResult.ClassID = dbo.ExamSetupMaster.ClassID AND dbo.ExamResult.SessionID = dbo.ExamSetupMaster.SessionID " +
                                  " WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.ExamResult.StudentID IN (" + StudentIds + ")) " +
                                  " AND (dbo.ExamResult.SessionID =" + SESSION_ID + ") AND (dbo.ExamSetupMaster.SessionID =" + SESSION_ID + ") AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND " +
                                  " (dbo.ExamResult.BranchID =" + BRANCH_ID + ") AND (dbo.SubjectLevelOne.SubjectCategoryID = 2) AND (dbo.ExamSetupMaster.ExamID =" + ExamId + ")  and dbo.ExamSetupMaster.SubExamID= " + SubExamId + " " +
                                  " Order BY dbo.ExamResult.StudentID, dbo.ExamSetupDetail.OrderNo ";


                            SqlDetail3 = "SELECT TOP (100) PERCENT dbo.SubjectLevelOne.SubjectNameL1 AS Subject," +
                                       "dbo.ExamResult.ObtainMarks1 AS Obtain1, dbo.ExamResult.ObtainMarks2 AS Obtain2, dbo.ExamResult.ObtainMarks3 AS Obtain3," +
                                       " dbo.ExamResult.ObtainMarks4 AS Obtain4,dbo.ExamResult.ObtainMarks5 AS Obtain5, dbo.ExamResult.ObtainMarks6 AS Obtain6, dbo.ExamResult.ObtainMarks7 AS Obtain7, " +
                                       "  dbo.ExamResult.ObtainMarks8 AS Obtain8, dbo.ExamResult.StudentID, " +
                                       " dbo.ExamSetupDetail.OrderNo FROM dbo.ExamResult INNER JOIN dbo.SubjectLevelOne ON dbo.ExamResult.SubjectIDL1 = dbo.SubjectLevelOne.IdL1 INNER JOIN " +
                                       " dbo.ExamSetupDetail ON dbo.SubjectLevelOne.IdL1 = dbo.ExamSetupDetail.SubjectIDL1 AND dbo.SubjectLevelOne.BranchID = dbo.ExamSetupDetail.BranchID AND " +
                                       " dbo.SubjectLevelOne.CompID = dbo.ExamSetupDetail.CompID INNER JOIN dbo.ExamSetupMaster ON dbo.ExamSetupDetail.ExamSetupID = dbo.ExamSetupMaster.ExamSetupID AND " +
                                       " dbo.ExamSetupDetail.BranchID = dbo.ExamSetupMaster.BranchID AND dbo.ExamSetupDetail.CompID = dbo.ExamSetupMaster.CompID AND dbo.ExamResult.ClassID = dbo.ExamSetupMaster.ClassID AND dbo.ExamResult.SessionID = dbo.ExamSetupMaster.SessionID " +
                                       " WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.ExamResult.StudentID IN (" + StudentIds + ")) " +
                                       " AND (dbo.ExamResult.SessionID =" + SESSION_ID + ") AND (dbo.ExamSetupMaster.SessionID =" + SESSION_ID + ") AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND " +
                                       " (dbo.ExamResult.BranchID =" + BRANCH_ID + ") AND (dbo.SubjectLevelOne.SubjectCategoryID = 3) AND (dbo.ExamSetupMaster.ExamID =" + ExamId + ") and dbo.ExamSetupMaster.SubExamID= " + SubExamId + " " +
                                       " Order BY dbo.ExamResult.StudentID, dbo.ExamSetupDetail.OrderNo ";
                        }

                        if (Subjectlevel == 2)
                        {

                            SqlDetail1 = "SELECT TOP (100) PERCENT dbo.SubjectLevelOne.SubjectNameL1 AS Subject,dbo.SubjectLevelTwo.SubjectNameL2 AS SubjectD," +
                                       " dbo.ExamResult.ObtainMarks5 AS Obtain1,dbo.ExamResult.ObtainMarks6 AS Obtain2,dbo.ExamResult.ObtainMarks7 AS Obtain3,dbo.ExamResult.ObtainMarks8 AS Obtain4, dbo.ExamResult.StudentID, " +
                                       " dbo.ExamSetupDetail.OrderNo,dbo.ExamResult.ObtainGrade5 AS Grade1, dbo.ExamResult.ObtainGrade6 AS Grade2,dbo.ExamResult.ObtainGrade7 AS Grade3,dbo.ExamResult.ObtainGrade8 AS Grade4, ExamResult.MaxMark5 as Max1,ExamResult.MaxMark6 as Max2,ExamResult.MaxMark7 as Max3,ExamResult.MaxMark8 as Max4,ExamResult.MinMark5 as Min1,ExamResult.MinMark6 as Min2,ExamResult.MinMark7 as Min3,ExamResult.MinMark8 as Min4 " +
                                       " FROM dbo.ExamResult INNER JOIN dbo.SubjectLevelOne ON dbo.ExamResult.SubjectIDL1 = dbo.SubjectLevelOne.IdL1 INNER JOIN " +
                                       " dbo.ExamSetupDetail ON dbo.SubjectLevelOne.IdL1 = dbo.ExamSetupDetail.SubjectIDL1 AND dbo.SubjectLevelOne.BranchID = dbo.ExamSetupDetail.BranchID AND " +
                                       " dbo.SubjectLevelOne.CompID = dbo.ExamSetupDetail.CompID " +
                                       " INNER JOIN dbo.SubjectLevelTwo ON dbo.ExamResult.SubjectIDL2 = dbo.SubjectLevelTwo.IdL2 AND dbo.ExamSetupDetail.SubjectIDL2 = dbo.SubjectLevelTwo.IdL2 " +
                                       " INNER JOIN dbo.ExamSetupMaster ON dbo.ExamSetupDetail.ExamSetupID = dbo.ExamSetupMaster.ExamSetupID AND " +
                                       " dbo.ExamSetupDetail.BranchID = dbo.ExamSetupMaster.BranchID AND dbo.ExamSetupDetail.CompID = dbo.ExamSetupMaster.CompID AND dbo.ExamResult.ClassID = dbo.ExamSetupMaster.ClassID AND dbo.ExamResult.SessionID = dbo.ExamSetupMaster.SessionID " +
                                       " WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.ExamResult.StudentID IN (" + StudentIds + ")) " +
                                       " AND (dbo.ExamResult.SessionID =" + SESSION_ID + ") AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND " +
                                       " (dbo.ExamResult.BranchID =" + BRANCH_ID + ") AND (dbo.SubjectLevelOne.SubjectCategoryID = 1) AND (dbo.ExamSetupMaster.ExamID =" + ExamId + ")" +
                                       " and dbo.ExamSetupMaster.SubExamID= " + SubExamId + " " +
                                       " Order BY dbo.ExamResult.StudentID, dbo.ExamSetupDetail.OrderNo ";


                            SqlDetail2 = "SELECT TOP (100) PERCENT dbo.SubjectLevelOne.SubjectNameL1 AS Subject,dbo.SubjectLevelTwo.SubjectNameL2 AS SubjectD,dbo.ExamResult.ObtainMarks5 AS Obtain1,dbo.ExamResult.ObtainMarks6 AS Obtain2,dbo.ExamResult.ObtainMarks7 AS Obtain3,dbo.ExamResult.ObtainMarks8 AS Obtain4, dbo.ExamResult.StudentID, " +
                                    " dbo.ExamSetupDetail.OrderNo,ExamResult.MaxMark5 as Max1,ExamResult.MaxMark6 as Max2,ExamResult.MaxMark7 as Max3,ExamResult.MaxMark8 as Max4,ExamResult.MinMark5 as Min1,ExamResult.MinMark6 as Min2,ExamResult.MinMark7 as Min3,ExamResult.MinMark8 as Min4 " +
                                    " FROM dbo.ExamResult INNER JOIN dbo.SubjectLevelOne ON dbo.ExamResult.SubjectIDL1 = dbo.SubjectLevelOne.IdL1 INNER JOIN " +
                                    " dbo.ExamSetupDetail ON dbo.SubjectLevelOne.IdL1 = dbo.ExamSetupDetail.SubjectIDL1 AND dbo.SubjectLevelOne.BranchID = dbo.ExamSetupDetail.BranchID AND " +
                                    " dbo.SubjectLevelOne.CompID = dbo.ExamSetupDetail.CompID " +
                                    " INNER JOIN dbo.SubjectLevelTwo ON dbo.ExamResult.SubjectIDL2 = dbo.SubjectLevelTwo.IdL2 AND dbo.ExamSetupDetail.SubjectIDL2 = dbo.SubjectLevelTwo.IdL2 " +
                                    " INNER JOIN dbo.ExamSetupMaster ON dbo.ExamSetupDetail.ExamSetupID = dbo.ExamSetupMaster.ExamSetupID AND " +
                                    " dbo.ExamSetupDetail.BranchID = dbo.ExamSetupMaster.BranchID AND dbo.ExamSetupDetail.CompID = dbo.ExamSetupMaster.CompID AND dbo.ExamResult.ClassID = dbo.ExamSetupMaster.ClassID " +
                                    " WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.ExamResult.StudentID IN (" + StudentIds + ")) " +
                                    " AND (dbo.ExamResult.SessionID =" + SESSION_ID + ")  AND (dbo.ExamSetupMaster.SessionID =" + SESSION_ID + ") AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND " +
                                    " (dbo.ExamResult.BranchID =" + BRANCH_ID + ") AND (dbo.SubjectLevelOne.SubjectCategoryID = 2) AND (dbo.ExamSetupMaster.ExamID =" + ExamId + ")" +
                                    " and dbo.ExamSetupMaster.SubExamID= " + SubExamId + " " +
                                    " Order BY dbo.ExamResult.StudentID, dbo.ExamSetupDetail.OrderNo ";


                            SqlDetail3 = "SELECT TOP (100) PERCENT dbo.SubjectLevelOne.SubjectNameL1 AS Subject,dbo.SubjectLevelTwo.SubjectNameL2 AS SubjectD,dbo.ExamResult.ObtainMarks5 AS Obtain1,dbo.ExamResult.ObtainMarks6 AS Obtain2,dbo.ExamResult.ObtainMarks7 AS Obtain3,dbo.ExamResult.ObtainMarks8 AS Obtain4, dbo.ExamResult.StudentID, " +
                                        " dbo.ExamSetupDetail.OrderNo,ExamResult.MaxMark5 as Max1,ExamResult.MaxMark6 as Max2,ExamResult.MaxMark7 as Max3,ExamResult.MaxMark8 as Max4,ExamResult.MinMark5 as Min1,ExamResult.MinMark6 as Min2,ExamResult.MinMark7 as Min3,ExamResult.MinMark8 as Min4 " +
                                        " FROM dbo.ExamResult INNER JOIN dbo.SubjectLevelOne ON dbo.ExamResult.SubjectIDL1 = dbo.SubjectLevelOne.IdL1 INNER JOIN " +
                                        " dbo.ExamSetupDetail ON dbo.SubjectLevelOne.IdL1 = dbo.ExamSetupDetail.SubjectIDL1 AND dbo.SubjectLevelOne.BranchID = dbo.ExamSetupDetail.BranchID AND " +
                                        " dbo.SubjectLevelOne.CompID = dbo.ExamSetupDetail.CompID " +
                                        " INNER JOIN dbo.SubjectLevelTwo ON dbo.ExamResult.SubjectIDL2 = dbo.SubjectLevelTwo.IdL2 AND dbo.ExamSetupDetail.SubjectIDL2 = dbo.SubjectLevelTwo.IdL2 " +
                                        " INNER JOIN dbo.ExamSetupMaster ON dbo.ExamSetupDetail.ExamSetupID = dbo.ExamSetupMaster.ExamSetupID AND " +
                                        " dbo.ExamSetupDetail.BranchID = dbo.ExamSetupMaster.BranchID AND dbo.ExamSetupDetail.CompID = dbo.ExamSetupMaster.CompID AND dbo.ExamResult.ClassID = dbo.ExamSetupMaster.ClassID " +
                                       " WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.ExamResult.StudentID IN (" + StudentIds + ")) " +
                                       " AND (dbo.ExamResult.SessionID =" + SESSION_ID + ") AND (dbo.ExamSetupMaster.SessionID =" + SESSION_ID + ") AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND " +
                                       " (dbo.ExamResult.BranchID =" + BRANCH_ID + ") AND (dbo.SubjectLevelOne.SubjectCategoryID = 3) AND (dbo.ExamSetupMaster.ExamID =" + ExamId + ")" +
                                      " and dbo.ExamSetupMaster.SubExamID= " + SubExamId + " " +
                                       " Order BY dbo.ExamResult.StudentID, dbo.ExamSetupDetail.OrderNo ";
                        }


                    }

                    if (Order == 6)
                    {
                        if (Subjectlevel == 1)
                        {

                            SqlDetail1 = "SELECT TOP (100) PERCENT dbo.SubjectLevelOne.SubjectNameL1 AS Subject, " +
                                        " dbo.ExamResult.ObtainMarks1 AS Obtain1, dbo.ExamResult.ObtainMarks2 AS Obtain2, dbo.ExamResult.ObtainMarks3 AS Obtain3," +
                                        " dbo.ExamResult.ObtainMarks4 AS Obtain4,dbo.ExamResult.ObtainMarks5 AS obtain5, dbo.ExamResult.ObtainMarks6 AS Obtain6, dbo.ExamResult.ObtainMarks7 AS Obtain7," +
                                        " dbo.ExamResult.ObtainMarks8 AS Obtain8,dbo.ExamResult.StudentID, dbo.ExamSetupDetail.OrderNo, " +
                                        " dbo.ExamResult.Grade AS Grade1, dbo.ExamResult.ObtainGrade2 AS Grade2,dbo.ExamResult.ObtainGrade3 AS Grade3, dbo.ExamResult.ObtainGrade4 AS Grade4, dbo.ExamResult.ObtainGrade5 AS Grade5, dbo.ExamResult.ObtainGrade6 AS Grade6, " +
                                        " dbo.ExamResult.ObtainGrade7 AS Grade7, dbo.ExamResult.ObtainGrade8 AS Grade8, dbo.ExamResult.MaxMark1 AS Max1, dbo.ExamResult.MaxMark2 AS Max2, dbo.ExamResult.MaxMark3 AS Max3, " +
                                        " dbo.ExamResult.MaxMark4 AS Max4,dbo.ExamResult.MaxMark5 AS Max5, dbo.ExamResult.MaxMark6 AS Max6, dbo.ExamResult.MaxMark7 AS Max3, " +
                                        " dbo.ExamResult.MaxMark8 AS Max8, dbo.ExamResult.MinMark1 AS Min1, dbo.ExamResult.MinMark2 AS Min2, dbo.ExamResult.MinMark3 AS Min3, dbo.ExamResult.MinMark4 AS Min4,dbo.ExamResult.MinMark5 AS Min5, dbo.ExamResult.MinMark6 AS Min6, dbo.ExamResult.MinMark7 AS Min7, dbo.ExamResult.MinMark8 AS Min8" +
                                    " FROM dbo.ExamResult INNER JOIN dbo.SubjectLevelOne ON dbo.ExamResult.SubjectIDL1 = dbo.SubjectLevelOne.IdL1 INNER JOIN " +
                                    " dbo.ExamSetupDetail ON dbo.SubjectLevelOne.IdL1 = dbo.ExamSetupDetail.SubjectIDL1 AND dbo.SubjectLevelOne.BranchID = dbo.ExamSetupDetail.BranchID AND " +
                                    " dbo.SubjectLevelOne.CompID = dbo.ExamSetupDetail.CompID INNER JOIN dbo.ExamSetupMaster ON dbo.ExamSetupDetail.ExamSetupID = dbo.ExamSetupMaster.ExamSetupID AND " +
                                    " dbo.ExamSetupDetail.BranchID = dbo.ExamSetupMaster.BranchID AND dbo.ExamSetupDetail.CompID = dbo.ExamSetupMaster.CompID AND dbo.ExamResult.ClassID = dbo.ExamSetupMaster.ClassID AND dbo.ExamResult.SessionID = dbo.ExamSetupMaster.SessionID " +
                                    "  WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.ExamResult.StudentID IN (" + StudentIds + ")) " +
                                    " AND (dbo.ExamResult.SessionID =" + SESSION_ID + ") AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND " +
                                    " (dbo.ExamResult.BranchID =" + BRANCH_ID + ") AND (dbo.SubjectLevelOne.SubjectCategoryID = 1) AND (dbo.ExamSetupMaster.ExamID =" + ExamId + ")" +
                                     " and dbo.ExamSetupMaster.SubExamID= " + SubExamId + " " +
                                    " Order BY dbo.ExamResult.StudentID, dbo.ExamSetupDetail.OrderNo ";


                            SqlDetail2 = "SELECT TOP (100) PERCENT dbo.SubjectLevelOne.SubjectNameL1 AS Subject,dbo.ExamResult.ObtainMarks1 AS Obtain1, dbo.ExamResult.ObtainMarks2 AS Obtain2, dbo.ExamResult.ObtainMarks3 AS Obtain3, " +
                                         " dbo.ExamResult.ObtainMarks4 AS Obtain4,dbo.ExamResult.ObtainMarks5 AS Obtain5, dbo.ExamResult.ObtainMarks6 AS Obtain6, dbo.ExamResult.ObtainMarks7 AS Obtain7, " +
                                         " dbo.ExamResult.ObtainMarks8 AS Obtain8, dbo.ExamResult.StudentID, " +
                                        " dbo.ExamSetupDetail.OrderNo FROM dbo.ExamResult INNER JOIN dbo.SubjectLevelOne ON dbo.ExamResult.SubjectIDL1 = dbo.SubjectLevelOne.IdL1 INNER JOIN " +
                                        " dbo.ExamSetupDetail ON dbo.SubjectLevelOne.IdL1 = dbo.ExamSetupDetail.SubjectIDL1 AND dbo.SubjectLevelOne.BranchID = dbo.ExamSetupDetail.BranchID AND " +
                                        " dbo.SubjectLevelOne.CompID = dbo.ExamSetupDetail.CompID INNER JOIN dbo.ExamSetupMaster ON dbo.ExamSetupDetail.ExamSetupID = dbo.ExamSetupMaster.ExamSetupID AND " +
                                        " dbo.ExamSetupDetail.BranchID = dbo.ExamSetupMaster.BranchID AND dbo.ExamSetupDetail.CompID = dbo.ExamSetupMaster.CompID AND dbo.ExamResult.ClassID = dbo.ExamSetupMaster.ClassID AND dbo.ExamResult.SessionID = dbo.ExamSetupMaster.SessionID " +
                                        "  WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.ExamResult.StudentID IN (" + StudentIds + ")) " +
                                        " AND (dbo.ExamResult.SessionID =" + SESSION_ID + ") AND (dbo.ExamSetupMaster.SessionID =" + SESSION_ID + ") AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND " +
                                        " (dbo.ExamResult.BranchID =" + BRANCH_ID + ") AND (dbo.SubjectLevelOne.SubjectCategoryID = 2) AND (dbo.ExamSetupMaster.ExamID =" + ExamId + ")" +
                                         " and dbo.ExamSetupMaster.SubExamID= " + SubExamId + " " +
                                        " Order BY dbo.ExamResult.StudentID, dbo.ExamSetupDetail.OrderNo ";


                            SqlDetail3 = "SELECT TOP (100) PERCENT dbo.SubjectLevelOne.SubjectNameL1 AS Subject,  dbo.ExamResult.ObtainMarks1 AS Obtain1, dbo.ExamResult.ObtainMarks2 AS Obtain2, dbo.ExamResult.ObtainMarks3 AS Obtain3, " +
                                         " dbo.ExamResult.ObtainMarks4 AS Obtain4,dbo.ExamResult.ObtainMarks5 AS Obtain5, dbo.ExamResult.ObtainMarks6 AS Obtain6, dbo.ExamResult.ObtainMarks7 AS Obtain7," +
                                          " dbo.ExamResult.ObtainMarks8 AS Obtain8, dbo.ExamResult.StudentID, " +
                                       " dbo.ExamSetupDetail.OrderNo FROM dbo.ExamResult INNER JOIN dbo.SubjectLevelOne ON dbo.ExamResult.SubjectIDL1 = dbo.SubjectLevelOne.IdL1 INNER JOIN " +
                                       " dbo.ExamSetupDetail ON dbo.SubjectLevelOne.IdL1 = dbo.ExamSetupDetail.SubjectIDL1 AND dbo.SubjectLevelOne.BranchID = dbo.ExamSetupDetail.BranchID AND " +
                                       " dbo.SubjectLevelOne.CompID = dbo.ExamSetupDetail.CompID INNER JOIN dbo.ExamSetupMaster ON dbo.ExamSetupDetail.ExamSetupID = dbo.ExamSetupMaster.ExamSetupID AND " +
                                       " dbo.ExamSetupDetail.BranchID = dbo.ExamSetupMaster.BranchID AND dbo.ExamSetupDetail.CompID = dbo.ExamSetupMaster.CompID AND dbo.ExamResult.ClassID = dbo.ExamSetupMaster.ClassID AND dbo.ExamResult.SessionID = dbo.ExamSetupMaster.SessionID " +
                                       " WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.ExamResult.StudentID IN (" + StudentIds + ")) " +
                                       " AND (dbo.ExamResult.SessionID =" + SESSION_ID + ") AND (dbo.ExamSetupMaster.SessionID =" + SESSION_ID + ") AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND " +
                                       " (dbo.ExamResult.BranchID =" + BRANCH_ID + ") AND (dbo.SubjectLevelOne.SubjectCategoryID = 3) AND (dbo.ExamSetupMaster.ExamID =" + ExamId + ")" +
                                         " and dbo.ExamSetupMaster.SubExamID= " + SubExamId + " " +
                                       " Order BY dbo.ExamResult.StudentID, dbo.ExamSetupDetail.OrderNo ";
                        }
                        if (Subjectlevel == 2)
                        {

                            SqlDetail1 = "SELECT TOP (100) PERCENT dbo.SubjectLevelOne.SubjectNameL1 AS Subject,dbo.SubjectLevelTwo.SubjectNameL2 AS SubjectD," +
                                       " dbo.ExamResult.ObtainMarks5 AS Obtain1,dbo.ExamResult.ObtainMarks6 AS Obtain2,dbo.ExamResult.ObtainMarks7 AS Obtain3,dbo.ExamResult.ObtainMarks8 AS Obtain4, dbo.ExamResult.StudentID, " +
                                       " dbo.ExamSetupDetail.OrderNo,dbo.ExamResult.ObtainGrade5 AS Grade1, dbo.ExamResult.ObtainGrade6 AS Grade2,dbo.ExamResult.ObtainGrade7 AS Grade3,dbo.ExamResult.ObtainGrade8 AS Grade4, ExamResult.MaxMark5 as Max1,ExamResult.MaxMark6 as Max2,ExamResult.MaxMark7 as Max3,ExamResult.MaxMark8 as Max4,ExamResult.MinMark5 as Min1,ExamResult.MinMark6 as Min2,ExamResult.MinMark7 as Min3,ExamResult.MinMark8 as Min4 " +
                                       " FROM dbo.ExamResult INNER JOIN dbo.SubjectLevelOne ON dbo.ExamResult.SubjectIDL1 = dbo.SubjectLevelOne.IdL1 INNER JOIN " +
                                       " dbo.ExamSetupDetail ON dbo.SubjectLevelOne.IdL1 = dbo.ExamSetupDetail.SubjectIDL1 AND dbo.SubjectLevelOne.BranchID = dbo.ExamSetupDetail.BranchID AND " +
                                       " dbo.SubjectLevelOne.CompID = dbo.ExamSetupDetail.CompID " +
                                       " INNER JOIN dbo.SubjectLevelTwo ON dbo.ExamResult.SubjectIDL2 = dbo.SubjectLevelTwo.IdL2 AND dbo.ExamSetupDetail.SubjectIDL2 = dbo.SubjectLevelTwo.IdL2 " +
                                       " INNER JOIN dbo.ExamSetupMaster ON dbo.ExamSetupDetail.ExamSetupID = dbo.ExamSetupMaster.ExamSetupID AND " +
                                       " dbo.ExamSetupDetail.BranchID = dbo.ExamSetupMaster.BranchID AND dbo.ExamSetupDetail.CompID = dbo.ExamSetupMaster.CompID AND dbo.ExamResult.ClassID = dbo.ExamSetupMaster.ClassID AND dbo.ExamResult.SessionID = dbo.ExamSetupMaster.SessionID " +
                                       " WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.ExamResult.StudentID IN (" + StudentIds + ")) " +
                                       " AND (dbo.ExamResult.SessionID =" + SESSION_ID + ") AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND " +
                                       " (dbo.ExamResult.BranchID =" + BRANCH_ID + ") AND (dbo.SubjectLevelOne.SubjectCategoryID = 1) AND (dbo.ExamSetupMaster.ExamID =" + ExamId + ")" +
                                       " and dbo.ExamSetupMaster.SubExamID= " + SubExamId + " " +
                                       " Order BY dbo.ExamResult.StudentID, dbo.ExamSetupDetail.OrderNo ";


                            SqlDetail2 = "SELECT TOP (100) PERCENT dbo.SubjectLevelOne.SubjectNameL1 AS Subject,dbo.SubjectLevelTwo.SubjectNameL2 AS SubjectD,dbo.ExamResult.ObtainMarks5 AS Obtain1,dbo.ExamResult.ObtainMarks6 AS Obtain2,dbo.ExamResult.ObtainMarks7 AS Obtain3,dbo.ExamResult.ObtainMarks8 AS Obtain4, dbo.ExamResult.StudentID, " +
                                    " dbo.ExamSetupDetail.OrderNo,ExamResult.MaxMark5 as Max1,ExamResult.MaxMark6 as Max2,ExamResult.MaxMark7 as Max3,ExamResult.MaxMark8 as Max4,ExamResult.MinMark5 as Min1,ExamResult.MinMark6 as Min2,ExamResult.MinMark7 as Min3,ExamResult.MinMark8 as Min4 " +
                                    " FROM dbo.ExamResult INNER JOIN dbo.SubjectLevelOne ON dbo.ExamResult.SubjectIDL1 = dbo.SubjectLevelOne.IdL1 INNER JOIN " +
                                    " dbo.ExamSetupDetail ON dbo.SubjectLevelOne.IdL1 = dbo.ExamSetupDetail.SubjectIDL1 AND dbo.SubjectLevelOne.BranchID = dbo.ExamSetupDetail.BranchID AND " +
                                    " dbo.SubjectLevelOne.CompID = dbo.ExamSetupDetail.CompID " +
                                    " INNER JOIN dbo.SubjectLevelTwo ON dbo.ExamResult.SubjectIDL2 = dbo.SubjectLevelTwo.IdL2 AND dbo.ExamSetupDetail.SubjectIDL2 = dbo.SubjectLevelTwo.IdL2 " +
                                    " INNER JOIN dbo.ExamSetupMaster ON dbo.ExamSetupDetail.ExamSetupID = dbo.ExamSetupMaster.ExamSetupID AND " +
                                    " dbo.ExamSetupDetail.BranchID = dbo.ExamSetupMaster.BranchID AND dbo.ExamSetupDetail.CompID = dbo.ExamSetupMaster.CompID AND dbo.ExamResult.ClassID = dbo.ExamSetupMaster.ClassID " +
                                    " WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.ExamResult.StudentID IN (" + StudentIds + ")) " +
                                    " AND (dbo.ExamResult.SessionID =" + SESSION_ID + ")  AND (dbo.ExamSetupMaster.SessionID =" + SESSION_ID + ") AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND " +
                                    " (dbo.ExamResult.BranchID =" + BRANCH_ID + ") AND (dbo.SubjectLevelOne.SubjectCategoryID = 2) AND (dbo.ExamSetupMaster.ExamID =" + ExamId + ")" +
                                    " and dbo.ExamSetupMaster.SubExamID= " + SubExamId + " " +
                                    " Order BY dbo.ExamResult.StudentID, dbo.ExamSetupDetail.OrderNo ";


                            SqlDetail3 = "SELECT TOP (100) PERCENT dbo.SubjectLevelOne.SubjectNameL1 AS Subject,dbo.SubjectLevelTwo.SubjectNameL2 AS SubjectD,dbo.ExamResult.ObtainMarks5 AS Obtain1,dbo.ExamResult.ObtainMarks6 AS Obtain2,dbo.ExamResult.ObtainMarks7 AS Obtain3,dbo.ExamResult.ObtainMarks8 AS Obtain4, dbo.ExamResult.StudentID, " +
                                        " dbo.ExamSetupDetail.OrderNo,ExamResult.MaxMark5 as Max1,ExamResult.MaxMark6 as Max2,ExamResult.MaxMark7 as Max3,ExamResult.MaxMark8 as Max4,ExamResult.MinMark5 as Min1,ExamResult.MinMark6 as Min2,ExamResult.MinMark7 as Min3,ExamResult.MinMark8 as Min4 " +
                                        " FROM dbo.ExamResult INNER JOIN dbo.SubjectLevelOne ON dbo.ExamResult.SubjectIDL1 = dbo.SubjectLevelOne.IdL1 INNER JOIN " +
                                        " dbo.ExamSetupDetail ON dbo.SubjectLevelOne.IdL1 = dbo.ExamSetupDetail.SubjectIDL1 AND dbo.SubjectLevelOne.BranchID = dbo.ExamSetupDetail.BranchID AND " +
                                        " dbo.SubjectLevelOne.CompID = dbo.ExamSetupDetail.CompID " +
                                        " INNER JOIN dbo.SubjectLevelTwo ON dbo.ExamResult.SubjectIDL2 = dbo.SubjectLevelTwo.IdL2 AND dbo.ExamSetupDetail.SubjectIDL2 = dbo.SubjectLevelTwo.IdL2 " +
                                        " INNER JOIN dbo.ExamSetupMaster ON dbo.ExamSetupDetail.ExamSetupID = dbo.ExamSetupMaster.ExamSetupID AND " +
                                        " dbo.ExamSetupDetail.BranchID = dbo.ExamSetupMaster.BranchID AND dbo.ExamSetupDetail.CompID = dbo.ExamSetupMaster.CompID AND dbo.ExamResult.ClassID = dbo.ExamSetupMaster.ClassID " +
                                       " WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.ExamResult.StudentID IN (" + StudentIds + ")) " +
                                       " AND (dbo.ExamResult.SessionID =" + SESSION_ID + ") AND (dbo.ExamSetupMaster.SessionID =" + SESSION_ID + ") AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND " +
                                       " (dbo.ExamResult.BranchID =" + BRANCH_ID + ") AND (dbo.SubjectLevelOne.SubjectCategoryID = 3) AND (dbo.ExamSetupMaster.ExamID =" + ExamId + ")" +
                                      " and dbo.ExamSetupMaster.SubExamID= " + SubExamId + " " +
                                       " Order BY dbo.ExamResult.StudentID, dbo.ExamSetupDetail.OrderNo ";
                        }

                    }
                    if (Order == 7)
                    {
                        if (Subjectlevel == 1)
                        {

                            SqlDetail1 = "SELECT TOP (100) PERCENT dbo.SubjectLevelOne.SubjectNameL1 AS Subject, " +
                            " dbo.ExamResult.ObtainMarks1 AS Obtain1, dbo.ExamResult.ObtainMarks2 AS Obtain2, dbo.ExamResult.ObtainMarks3 AS Obtain3, " +
                            " dbo.ExamResult.ObtainMarks4 AS Obtain4,dbo.ExamResult.ObtainMarks5 AS Obtain5, dbo.ExamResult.ObtainMarks6 AS Obtain6, dbo.ExamResult.ObtainMarks7 AS Obtain7, " +
                            " dbo.ExamResult.ObtainMarks8 AS Obtain8, dbo.ExamResult.StudentID, " +
                            " dbo.ExamSetupDetail.OrderNo, dbo.ExamResult.Grade AS Grade1, dbo.ExamResult.ObtainGrade2 AS Grade2, " +
                            " dbo.ExamResult.ObtainGrade3 AS Grade3, dbo.ExamResult.ObtainGrade4 AS Grade4, dbo.ExamResult.ObtainGrade5 AS Grade5, dbo.ExamResult.ObtainGrade6 AS Grade6, " +
                            " dbo.ExamResult.ObtainGrade7 AS Grade7, dbo.ExamResult.ObtainGrade8 AS Grade8, dbo.ExamResult.MaxMark1 AS Max1, dbo.ExamResult.MaxMark2 AS Max2, dbo.ExamResult.MaxMark3 AS Max3, " +
                            " dbo.ExamResult.MaxMark4 AS Max4,dbo.ExamResult.MaxMark5 AS Max5, dbo.ExamResult.MaxMark6 AS Max6, dbo.ExamResult.MaxMark7 AS Max7, " +
                            " dbo.ExamResult.MaxMark8 AS Max8, dbo.ExamResult.MinMark1 AS Min1, dbo.ExamResult.MinMark2 AS Min2, dbo.ExamResult.MinMark3 AS Min3, dbo.ExamResult.MinMark4 AS Min4,dbo.ExamResult.MinMark5 AS Min5, dbo.ExamResult.MinMark6 AS Min6, dbo.ExamResult.MinMark7 AS Min7, dbo.ExamResult.MinMark8 AS Min8 " +
                            " FROM dbo.ExamResult INNER JOIN dbo.SubjectLevelOne ON dbo.ExamResult.SubjectIDL1 = dbo.SubjectLevelOne.IdL1 INNER JOIN " +
                            " dbo.ExamSetupDetail ON dbo.SubjectLevelOne.IdL1 = dbo.ExamSetupDetail.SubjectIDL1 AND dbo.SubjectLevelOne.BranchID = dbo.ExamSetupDetail.BranchID AND " +
                            " dbo.SubjectLevelOne.CompID = dbo.ExamSetupDetail.CompID INNER JOIN dbo.ExamSetupMaster ON dbo.ExamSetupDetail.ExamSetupID = dbo.ExamSetupMaster.ExamSetupID AND " +
                            " dbo.ExamSetupDetail.BranchID = dbo.ExamSetupMaster.BranchID AND dbo.ExamSetupDetail.CompID = dbo.ExamSetupMaster.CompID AND dbo.ExamResult.ClassID = dbo.ExamSetupMaster.ClassID AND dbo.ExamResult.SessionID = dbo.ExamSetupMaster.SessionID " +
                            " WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.ExamResult.StudentID IN (" + StudentIds + ")) " +
                            " AND (dbo.ExamResult.SessionID =" + SESSION_ID + ") AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND " +
                            " (dbo.ExamResult.BranchID =" + BRANCH_ID + ") AND (dbo.SubjectLevelOne.SubjectCategoryID = 1) AND (dbo.ExamSetupMaster.ExamID =" + ExamId + ")" +
                            " and dbo.ExamSetupMaster.SubExamID= " + SubExamId + " " +
                            " Order BY dbo.ExamResult.StudentID, dbo.ExamSetupDetail.OrderNo ";


                            SqlDetail2 = "SELECT TOP (100) PERCENT dbo.SubjectLevelOne.SubjectNameL1 AS Subject," +
                                        " dbo.ExamResult.ObtainMarks1 AS Obtain1, dbo.ExamResult.ObtainMarks2 AS Obtain2, dbo.ExamResult.ObtainMarks3 AS Obtain3, " +
                                        " dbo.ExamResult.ObtainMarks4 AS Obtain4,dbo.ExamResult.ObtainMarks5 AS Obtain5, dbo.ExamResult.ObtainMarks6 AS Obtain6, dbo.ExamResult.ObtainMarks7 AS Obtain7, " +
                                        " dbo.ExamResult.ObtainMarks8 AS Obtain8, dbo.ExamResult.StudentID, " +
                                        " dbo.ExamSetupDetail.OrderNo FROM dbo.ExamResult INNER JOIN dbo.SubjectLevelOne ON dbo.ExamResult.SubjectIDL1 = dbo.SubjectLevelOne.IdL1 INNER JOIN " +
                                        " dbo.ExamSetupDetail ON dbo.SubjectLevelOne.IdL1 = dbo.ExamSetupDetail.SubjectIDL1 AND dbo.SubjectLevelOne.BranchID = dbo.ExamSetupDetail.BranchID AND " +
                                        " dbo.SubjectLevelOne.CompID = dbo.ExamSetupDetail.CompID INNER JOIN dbo.ExamSetupMaster ON dbo.ExamSetupDetail.ExamSetupID = dbo.ExamSetupMaster.ExamSetupID AND " +
                                        " dbo.ExamSetupDetail.BranchID = dbo.ExamSetupMaster.BranchID AND dbo.ExamSetupDetail.CompID = dbo.ExamSetupMaster.CompID AND dbo.ExamResult.ClassID = dbo.ExamSetupMaster.ClassID AND dbo.ExamResult.SessionID = dbo.ExamSetupMaster.SessionID " +
                                        " WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.ExamResult.StudentID IN (" + StudentIds + ")) " +
                                        " AND (dbo.ExamResult.SessionID =" + SESSION_ID + ") AND (dbo.ExamSetupMaster.SessionID =" + SESSION_ID + ") AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND " +
                                        " (dbo.ExamResult.BranchID =" + BRANCH_ID + ") AND (dbo.SubjectLevelOne.SubjectCategoryID = 2) AND (dbo.ExamSetupMaster.ExamID =" + ExamId + ")" +
                                        " and dbo.ExamSetupMaster.SubExamID= " + SubExamId + " " +
                                        " Order BY dbo.ExamResult.StudentID, dbo.ExamSetupDetail.OrderNo ";


                            SqlDetail3 = "SELECT TOP (100) PERCENT dbo.SubjectLevelOne.SubjectNameL1 AS Subject," +
                                        " dbo.ExamResult.ObtainMarks1 AS Obtain1, dbo.ExamResult.ObtainMarks2 AS Obtain2, dbo.ExamResult.ObtainMarks3 AS Obtain3, " +
                                        " dbo.ExamResult.ObtainMarks4 AS Obtain4,dbo.ExamResult.ObtainMarks5 AS Obtain5, dbo.ExamResult.ObtainMarks6 AS Obtain6, dbo.ExamResult.ObtainMarks7 AS Obtain7, " +
                                       " dbo.ExamResult.ObtainMarks8 AS Obtain8, dbo.ExamResult.StudentID, " +
                                       " dbo.ExamSetupDetail.OrderNo FROM dbo.ExamResult INNER JOIN dbo.SubjectLevelOne ON dbo.ExamResult.SubjectIDL1 = dbo.SubjectLevelOne.IdL1 INNER JOIN " +
                                       " dbo.ExamSetupDetail ON dbo.SubjectLevelOne.IdL1 = dbo.ExamSetupDetail.SubjectIDL1 AND dbo.SubjectLevelOne.BranchID = dbo.ExamSetupDetail.BranchID AND " +
                                       " dbo.SubjectLevelOne.CompID = dbo.ExamSetupDetail.CompID INNER JOIN dbo.ExamSetupMaster ON dbo.ExamSetupDetail.ExamSetupID = dbo.ExamSetupMaster.ExamSetupID AND " +
                                       " dbo.ExamSetupDetail.BranchID = dbo.ExamSetupMaster.BranchID AND dbo.ExamSetupDetail.CompID = dbo.ExamSetupMaster.CompID AND dbo.ExamResult.ClassID = dbo.ExamSetupMaster.ClassID AND dbo.ExamResult.SessionID = dbo.ExamSetupMaster.SessionID " +
                                       " WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.ExamResult.StudentID IN (" + StudentIds + ")) " +
                                       " AND (dbo.ExamResult.SessionID =" + SESSION_ID + ") AND (dbo.ExamSetupMaster.SessionID =" + SESSION_ID + ") AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND " +
                                       " (dbo.ExamResult.BranchID =" + BRANCH_ID + ") AND (dbo.SubjectLevelOne.SubjectCategoryID = 3) AND (dbo.ExamSetupMaster.ExamID =" + ExamId + ")" +
                                       " and dbo.ExamSetupMaster.SubExamID= " + SubExamId + " " +
                                       " Order BY dbo.ExamResult.StudentID, dbo.ExamSetupDetail.OrderNo ";
                        }
                    }
                    if (Order == 8)
                    {
                        if (Subjectlevel == 1)
                        {
                            SqlDetail1 = "SELECT TOP (100) PERCENT dbo.SubjectLevelOne.SubjectNameL1 AS Subject, " +
                                        " dbo.ExamResult.ObtainMarks1 AS Obtain1, dbo.ExamResult.ObtainMarks2 AS Obtain2, dbo.ExamResult.ObtainMarks3 AS Obtain3, " +
                                        " dbo.ExamResult.ObtainMarks4 AS Obtain4,dbo.ExamResult.ObtainMarks5 AS Obtain5, dbo.ExamResult.ObtainMarks6 AS Obtain6, dbo.ExamResult.ObtainMarks7 AS Obtain7, " +
                                        "  dbo.ExamResult.ObtainMarks8 AS Obtain8, dbo.ExamResult.StudentID, " +
                                        " dbo.ExamSetupDetail.OrderNo,dbo.ExamResult.Grade AS Grade1, dbo.ExamResult.ObtainGrade2 AS Grade2, " +
                                        " dbo.ExamResult.ObtainGrade3 AS Grade3, dbo.ExamResult.ObtainGrade4 AS Grade4, dbo.ExamResult.ObtainGrade5 AS Grade5, dbo.ExamResult.ObtainGrade6 AS Grade6, " +
                                        " dbo.ExamResult.ObtainGrade7 AS Grade7, dbo.ExamResult.ObtainGrade8 AS Grade8, dbo.ExamResult.MaxMark1 AS Max1, dbo.ExamResult.MaxMark2 AS Max2, dbo.ExamResult.MaxMark3 AS Max3, " +
                                        " dbo.ExamResult.MaxMark4 AS Max4,dbo.ExamResult.MaxMark5 AS Max5, dbo.ExamResult.MaxMark6 AS Max6, dbo.ExamResult.MaxMark7 AS Max7, " +
                                        " dbo.ExamResult.MaxMark8 AS Max8, dbo.ExamResult.MinMark1 AS Min1, dbo.ExamResult.MinMark2 AS Min2, dbo.ExamResult.MinMark3 AS Min3, dbo.ExamResult.MinMark4 AS Min4,dbo.ExamResult.MinMark5 AS Min5, dbo.ExamResult.MinMark6 AS Min6, dbo.ExamResult.MinMark7 AS Min7, dbo.ExamResult.MinMark8 AS Min8 " +
                                        " FROM dbo.ExamResult INNER JOIN dbo.SubjectLevelOne ON dbo.ExamResult.SubjectIDL1 = dbo.SubjectLevelOne.IdL1 INNER JOIN " +
                                        " dbo.ExamSetupDetail ON dbo.SubjectLevelOne.IdL1 = dbo.ExamSetupDetail.SubjectIDL1 AND dbo.SubjectLevelOne.BranchID = dbo.ExamSetupDetail.BranchID AND " +
                                        " dbo.SubjectLevelOne.CompID = dbo.ExamSetupDetail.CompID INNER JOIN dbo.ExamSetupMaster ON dbo.ExamSetupDetail.ExamSetupID = dbo.ExamSetupMaster.ExamSetupID AND " +
                                        " dbo.ExamSetupDetail.BranchID = dbo.ExamSetupMaster.BranchID AND dbo.ExamSetupDetail.CompID = dbo.ExamSetupMaster.CompID AND dbo.ExamResult.ClassID = dbo.ExamSetupMaster.ClassID AND dbo.ExamResult.SessionID = dbo.ExamSetupMaster.SessionID " +
                                        " WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.ExamResult.StudentID IN (" + StudentIds + ")) " +
                                        " AND (dbo.ExamResult.SessionID =" + SESSION_ID + ") AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND " +
                                        " (dbo.ExamResult.BranchID =" + BRANCH_ID + ") AND (dbo.SubjectLevelOne.SubjectCategoryID = 1) AND (dbo.ExamSetupMaster.ExamID =" + ExamId + ")" +
                                        " and dbo.ExamSetupMaster.SubExamID= " + SubExamId + " " +
                                        " Order BY dbo.ExamResult.StudentID, dbo.ExamSetupDetail.OrderNo ";


                            SqlDetail2 = "SELECT TOP (100) PERCENT dbo.SubjectLevelOne.SubjectNameL1 AS Subject," +
                                       " dbo.ExamResult.ObtainMarks1 AS Obtain1, dbo.ExamResult.ObtainMarks2 AS Obtain2, dbo.ExamResult.ObtainMarks3 AS Obtain3, " +
                                        " dbo.ExamResult.ObtainMarks4 AS Obtain4,dbo.ExamResult.ObtainMarks5 AS Obtain5, dbo.ExamResult.ObtainMarks6 AS Obtain6, dbo.ExamResult.ObtainMarks7 AS Obtain7, " +
                                        "  dbo.ExamResult.ObtainMarks8 AS Obtain8, dbo.ExamResult.StudentID, " +
                                        " dbo.ExamSetupDetail.OrderNo FROM dbo.ExamResult INNER JOIN dbo.SubjectLevelOne ON dbo.ExamResult.SubjectIDL1 = dbo.SubjectLevelOne.IdL1 INNER JOIN " +
                                        " dbo.ExamSetupDetail ON dbo.SubjectLevelOne.IdL1 = dbo.ExamSetupDetail.SubjectIDL1 AND dbo.SubjectLevelOne.BranchID = dbo.ExamSetupDetail.BranchID AND " +
                                        " dbo.SubjectLevelOne.CompID = dbo.ExamSetupDetail.CompID INNER JOIN dbo.ExamSetupMaster ON dbo.ExamSetupDetail.ExamSetupID = dbo.ExamSetupMaster.ExamSetupID AND " +
                                        " dbo.ExamSetupDetail.BranchID = dbo.ExamSetupMaster.BranchID AND dbo.ExamSetupDetail.CompID = dbo.ExamSetupMaster.CompID AND dbo.ExamResult.ClassID = dbo.ExamSetupMaster.ClassID AND dbo.ExamResult.SessionID = dbo.ExamSetupMaster.SessionID " +
                                        " WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.ExamResult.StudentID IN (" + StudentIds + ")) " +
                                        " AND (dbo.ExamResult.SessionID =" + SESSION_ID + ") AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND " +
                                        " (dbo.ExamResult.BranchID =" + BRANCH_ID + ") AND (dbo.SubjectLevelOne.SubjectCategoryID = 2) AND (dbo.ExamSetupMaster.ExamID =" + ExamId + ")" +
                                        " and dbo.ExamSetupMaster.SubExamID= " + SubExamId + " " +
                                        " Order BY dbo.ExamResult.StudentID, dbo.ExamSetupDetail.OrderNo ";


                            SqlDetail3 = "SELECT TOP (100) PERCENT dbo.SubjectLevelOne.SubjectNameL1 AS Subject, " +
                                        " dbo.ExamResult.ObtainMarks1 AS Obtain1, dbo.ExamResult.ObtainMarks2 AS Obtain2, dbo.ExamResult.ObtainMarks3 AS Obtain3, " +
                                        " dbo.ExamResult.ObtainMarks4 AS Obtain4,dbo.ExamResult.ObtainMarks5 AS Obtain5, dbo.ExamResult.ObtainMarks6 AS Obtain6, dbo.ExamResult.ObtainMarks7 AS Obtain7, " +
                                        " dbo.ExamResult.ObtainMarks8 AS Obtain8, dbo.ExamResult.StudentID, " +
                                       " dbo.ExamSetupDetail.OrderNo FROM dbo.ExamResult INNER JOIN dbo.SubjectLevelOne ON dbo.ExamResult.SubjectIDL1 = dbo.SubjectLevelOne.IdL1 INNER JOIN " +
                                       " dbo.ExamSetupDetail ON dbo.SubjectLevelOne.IdL1 = dbo.ExamSetupDetail.SubjectIDL1 AND dbo.SubjectLevelOne.BranchID = dbo.ExamSetupDetail.BranchID AND " +
                                       " dbo.SubjectLevelOne.CompID = dbo.ExamSetupDetail.CompID INNER JOIN dbo.ExamSetupMaster ON dbo.ExamSetupDetail.ExamSetupID = dbo.ExamSetupMaster.ExamSetupID AND " +
                                       " dbo.ExamSetupDetail.BranchID = dbo.ExamSetupMaster.BranchID AND dbo.ExamSetupDetail.CompID = dbo.ExamSetupMaster.CompID AND dbo.ExamResult.ClassID = dbo.ExamSetupMaster.ClassID AND dbo.ExamResult.SessionID = dbo.ExamSetupMaster.SessionID " +
                                       " WHERE(dbo.ExamResult.ClassID=" + ClassId + ") AND (dbo.ExamResult.StudentID IN (" + StudentIds + ")) " +
                                       " AND (dbo.ExamResult.SessionID =" + SESSION_ID + ") AND (dbo.ExamResult.CompID =" + COMPANY_ID + ") AND " +
                                       " (dbo.ExamResult.BranchID =" + BRANCH_ID + ") AND (dbo.SubjectLevelOne.SubjectCategoryID = 3) AND (dbo.ExamSetupMaster.ExamID =" + ExamId + ")" +
                                       " and dbo.ExamSetupMaster.SubExamID= " + SubExamId + " " +
                                       " Order BY dbo.ExamResult.StudentID, dbo.ExamSetupDetail.OrderNo ";
                        }


                    }



                }
                #endregion


            }
            #endregion
        }


    }
}





















