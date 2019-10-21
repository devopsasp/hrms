using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Data.SqlClient;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using ePayHrms.Connection;
using ePayHrms.Candidate;
using ePayHrms.Company;

namespace ePayHrms.BE.Recruitment
{
    /// <summary>
    /// Summary description for ePayHrms.BE.Recruitment
    /// </summary>   

    public class Be_Recruitment : Candidate.Candidate
    {
        public Be_Recruitment()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        //Table hrmt_Requisition

        //**********************************Variables*********************************************************
        private SqlConnection _connection;
        ePayHrms.Connection.Connection con = new ePayHrms.Connection.Connection();

        int[] a = new int[10];
        int r_qualificationcgy, r_skillset, r_qualification, c_candidateskills, c_CandidateEducation, s_RequisitionNo;
        private string _Skills_str;
        int r_expmin, r_expmax, r_agemin, r_agemax;

        //popup variables

        string pop_emsg, temp1;
        int pop_cg, pop_ct, pop_ci = 0, pop_eg, pop_et, pop_ei = 0, r_i, r_j;
        #region
        private string _common;

        private int _CompanyID;
        private string _CompanyName;
        private int _BranchID;
        private int _Entry_BranchID;
        private string _BranchName;

        private int _RequisitionNo;
        private string _RequisitionCode;

        private DateTime _RequisitionDate;

        public DateTime RequisitionDate
        {
            get { return _RequisitionDate; }
            set { _RequisitionDate = value; }
        }
        private DateTime _RequisitionDate_r;

        private string _RequiredDate;
        private DateTime _RequiredDate_r;

        private string _SortListDate;
        private string _InterviewDate;
        private string _InterviewTime;

        private string _CurrentDate;
        private string _CurrentTime;

        private string _JobType;
        private string _Comment;
        private string _CurrentRound;
        private int _Rounds;

        public int Rounds
        {
            get { return _Rounds; }
            set { _Rounds = value; }
        }       

        public string CurrentRound
        {
            get { return _CurrentRound; }
            set { _CurrentRound = value; }
        }
        public string Comment
        {
            get { return _Comment; }
            set { _Comment = value; }
        }   

        
        public string JobType
        {
            get { return _JobType; }
            set { _JobType = value; }
        }
       
        private int _Entry_EmployeeID;

        private string _Place;

        public string Place
        {
            get { return _Place; }
            set { _Place = value; }
        }       


//************Applicant Info*************

        private string pn_ApplicantID;
        public string ApplicatID
        {
            get { return pn_ApplicantID; }
            set {pn_ApplicantID=value; }
        }
        private string Salutation;
        public string _Salutation
        {
            get { return Salutation; }
            set { Salutation = value; }
        }
        private string Applicant_Full_Name;
        public string ApplicantfullName
        {
            get { return Applicant_Full_Name; }
            set { Applicant_Full_Name = value; }
        }
        private string Applicant_First_Name;
        public string ApplicantFirstName
        {
            get { return Applicant_First_Name;}
            set { Applicant_First_Name = value; }
        }
        private string Applicant_Midle_Name;
        public string ApplicantMiddleName
        {
            get { return Applicant_Midle_Name; }
            set { Applicant_Midle_Name = value; }
        }
        private string Applicant_Last_Name;
        public string ApplicantLastName
        {
            get { return Applicant_Last_Name; }
            set { Applicant_Last_Name = value; }
        }
        private string Gender;
        public string _Gender
        {
            get { return Gender;}
            set { Gender = value; }
        }
        private string age;
        public string Age
        {
            get { return age; }
            set { age = value; }
        }             
        private string Marital_Status;
        public string Mstatus
        {
            get { return Marital_Status; }
            set { Marital_Status = value; }
        }
        private string Religion;
        public string _Religion
        {
            get { return Religion; }
            set { Religion = value; }
        }
        private DateTime Date;
        public DateTime _Date
        {
            get { return Date; }
            set { Date = value; }
        }
        private DateTime Dob;
        public DateTime _Dob
        {
            get { return Dob; }
            set { Dob = value; }
        }
        private string EmailID;
        public string _EmailID
        {
            get { return EmailID; }
            set { EmailID = value; }
        }
        private string CellNo;
        public string _CellNo
        {
            get { return CellNo; }
            set { CellNo = value; }
        }
        private string Nationality;
        public string _Nationality
        {
            get { return Nationality; }
            set { Nationality = value; }
        }
        private string Designation;
        public string _Designation
        {
            get { return Designation; }
            set { Designation = value; }
        }
        private string Tenth_Institute_Name;
        public string _Tenth_Institute_Name
        {
            get { return Tenth_Institute_Name; }
            set { Tenth_Institute_Name = value; }
        }
        private string Tenth_Board_Name;
        public string _Tenth_Board_Name
        {
            get { return Tenth_Board_Name; }
            set { Tenth_Board_Name = value; }
        }
        private int Tenth_Yearofpass;
        public int _Tenth_Yearofpass
        {
            get { return Tenth_Yearofpass; }
            set { Tenth_Yearofpass = value; }
        }
        private string Tenth_Percentage;
        public string _Tenth_Percentage
        {
            get { return Tenth_Percentage; }
            set { Tenth_Percentage = value; }
        }

        private string Twelth_Institute_Name;
        public string _Twelth_Institute_Name
        {
            get { return Twelth_Institute_Name; }
            set { Twelth_Institute_Name = value; }
        }

        private string Twelth_Board_Name;
        public string _Twelth_Board_Name
        {
            get { return Twelth_Board_Name; }
            set { Twelth_Board_Name = value; }
        }

        private int Twelth_Yearofpass;
        public int _Twelth_Yearofpass
        {
            get { return Twelth_Yearofpass; }
            set { Twelth_Yearofpass = value; }
        }
        private string Twelth_Percentage;
        public string _Twelth_Percentage
        {
            get { return Twelth_Percentage; }
            set { Twelth_Percentage = value; }
        }
        private string Ug_College_Name;
        public string _Ug_College_Name
        {
            get { return Ug_College_Name; }
            set { Ug_College_Name = value; }
        }

        private string Ug_University_Name;
        public string _Ug_University_Name
        {
            get { return Ug_University_Name; }
            set { Ug_University_Name = value; }
        }

        private string _Ug_Course_Name;
        public string Ug_Course_Name
         {
             get { return _Ug_Course_Name; }
             set { _Ug_Course_Name = value; }
         }
        private int Ug_Yearofpass;
        public int _Ug_Yearofpass
        {
            get { return Ug_Yearofpass; }
            set { Ug_Yearofpass = value; }
        }
        private string Ug_Percentage;
        public string _Ug_Percentage
        {
            get { return Ug_Percentage; }
            set { Ug_Percentage = value; }
        }
        private string Pg_College_Name;
        public string _Pg_College_Name
        {
            get { return Pg_College_Name; }
            set { Pg_College_Name = value; }
        }
        private string Pg_University_Name;
        public string _Pg_University_Name
        {
            get { return Pg_University_Name; }
            set { Pg_University_Name = value; }
        }
        private string _Pg_Course_Name;
        public string Pg_Course_Name
        {
            get { return _Pg_Course_Name; }
            set { _Pg_Course_Name = value; }
        }
        private int Pg_Yearofpass;
        public int _Pg_Yearofpass
        {
            get { return Pg_Yearofpass; }
            set { Pg_Yearofpass = value; }
        }
        private string Pg_Percentage;
        public string _Pg_Percentage
        {
            get { return Pg_Percentage; }
            set { Pg_Percentage = value; }
        }
        private string _JobCode;
        public string JobCode
        {
            get { return _JobCode; }
            set { _JobCode = value; }
        }
        private string _Category1;
        public string Category1
        {
            get { return _Category1; }
            set { _Category1 = value; }
        }
        private string _Category2;
        public string Category2
        {
            get { return _Category2; }
            set { _Category2 = value; }
        }

        private string _EmployeeReference;
        public string EmployeeReference
        {
            get { return _EmployeeReference; }
            set { _EmployeeReference = value; }
        }
        private string _OtherReference;
        public string OtherReference
        {
            get { return _OtherReference; }
            set { _OtherReference = value; }
        }
       
        private string Resume;
        public string _Resume
        {
            get { return Resume; }
            set { Resume = value; }
        }

        private string _ImgName;
        public string ImgName
        {
            get { return _ImgName; }
            set { _ImgName = value; }
        }

        private string _ImgPath;
        public string ImgPath
         {
             get { return _ImgPath; }
             set { _ImgPath = value; }
         }

         private int _ReferenceID;
         public int ReferenceID
         {
             get { return _ReferenceID; }
             set { _ReferenceID = value; }
         }
         private string _ReferenceName;
         public string ReferenceName
         {
             get { return _ReferenceName; }
             set { _ReferenceName = value; }
         }
         private int _ID;
         public int ID
         {
             get { return _ID; }
             set { _ID = value; }
         }
         private int _Name;
         public int Name
         {
             get { return _Name;}
             set { _Name = value; }
         }

         private string _CourseName;
         public string CourseName
         {
             get { return _CourseName;}
             set { _CourseName = value; }
         }

         //*************JobRequitionLogin
         private string _JobDesDesignation;
         public string JobDescriptDesignation
         {

             get { return _JobDesDesignation; }
             set { _JobDesDesignation = value; }
         }
        /*Job Requistion*/
         private string _pn_Jobcode;
         public string pn_Jobcode
         {
             get { return _pn_Jobcode; }
             set { _pn_Jobcode = value; }
         }
        private string _ContractPeriod;
        public string ContractPeriod
        {
            get { return _ContractPeriod; }
            set { _ContractPeriod = value; }
        }
        private string _JobTitle;
        public string JobTitle
        {
            get { return _JobTitle;}
            set{_JobTitle=value;}
        }  
        private int _NoofRequired;
        public int NoofRequired
        {
            get { return _NoofRequired; }
            set { _NoofRequired = value; }
        }
        private DateTime _RequisitioDate;
        public DateTime requsiondate
        {
            get { return _RequisitioDate; }
            set { _RequisitioDate = value; }
        }
        private DateTime _RequireDate;
        public DateTime RequireDated
        {
            get { return _RequireDate; }
            set { _RequireDate = value; }
        }
        private string _Qualification;
        public string Qualification
        {
            get { return _Qualification; }
            set { _Qualification = value; }
        }
        private string _DutyAndResponsibility;
        public string DutyAndResponsibility
        {
            get { return _DutyAndResponsibility; }
            set { _DutyAndResponsibility = value; }
        }
        private string _AgeLimit;
        public string AgeLimit
        {
            get { return _AgeLimit; }
            set { _AgeLimit = value; }
        }
        private string _Department;
        public string Department
        {
            get { return _Department; }
            set { _Department = value; }
        }
        private string _ApprovedBy;
        public string ApprovedBy
        {
            get { return _ApprovedBy; }
            set { _ApprovedBy = value; }
        }
        private string _SubmitTo;
        public string SubmitTo
        {
            get { return _SubmitTo; }
            set { _SubmitTo = value; }
        }
        private string _Statuss;
        public string Statuss
        {
            get { return _Statuss; }
            set { _Statuss = value; }
        }    

        private string _EmployeeName;
        private int _OfficeID;
        private int _DivisionID;
        private string _DivisionName;
        private int _DepartmentID;
        private string _DepartmentName;
        private int _DesignationID;
        private string _DesignationName;
        private int _GradeID;
        private string _GradeName;
        private int _JobStatusID;
        private string _JobStatusName;
        private int _ShiftTypeID;
        private string _ShiftTypeName;
        private string _Reason;
        private int _CategoryCode;
        private string _CategoryName;
        private int _ProjectID;
        private string _ProjectName;
        private int _ExperienceMin;
        private int _ExperienceMax;
        private string _Experience;
        private int _AgeGroupMin;
        private string _Age;
        private string _Resumename;
        private int _ResumeId;
        private string _InterviewType;
        private string _QuerBy;
        public string QuerBy
        {
            get { return _QuerBy; }
            set { _QuerBy = value; }
       }

        public string queryby
        {
            get { return _Query_by; }
            set { _Query_by=value;}
        }

        private int _query_id;
        public int Query_id
        {
            get { return _query_id; }
            set { _query_id = value; }
        }
        private string _Queries;

        public string Queries
            {
            get { return _Queries; }
            set { _Queries = value; }
            }
        public string InterviewType
        {
            get { return _InterviewType; }
            set { _InterviewType = value; }
        }

        public int ResumeId
        {
            get { return _ResumeId; }
            set { _ResumeId = value; }
        }

        public string Resumename
        {
            get { return _Resumename; }
            set { _Resumename = value; }
        }
        private string _Filetype1;

        public string Filetype1
        {
            get { return _Filetype1; }
            set { _Filetype1 = value; }
        }
        private Byte[] _Filedata1;

        public Byte[] Filedata1
        {
            get { return _Filedata1; }
            set { _Filedata1 = value; }
        }       
        
        private int _AgeGroupMax;
        private int _NoofVacancies;

        private int _RequestStatusCode;

        private int _SkillID;
        private int _QualificationCode;
        private string _QualificationName;
        private string _Skills;
        private string _EmailId;
        private string _Round1;

        public string Round1
        {
            get { return _Round1; }
            set { _Round1 = value; }
        }

        private string _Round2;

        public string Round2
        {
            get { return _Round2; }
            set { _Round2 = value; }
        }
        private string _Round3;

        public string Round3
        {
            get { return _Round3; }
            set { _Round3 = value; }
        }
        private string _Round4;

        public string Round4
        {
            get { return _Round4; }
            set { _Round4 = value; }
        }
        private string _Round5;

        public string Round5
        {
            get { return _Round5; }
            set { _Round5 = value; }
        }
        private string _FinalResult;

        public string FinalResult
        {
            get { return _FinalResult; }
            set { _FinalResult = value; }
        }
        private string _Round1Date;

        public string Round1Date
        {
            get { return _Round1Date; }
            set { _Round1Date = value; }
        }
        private string _Round2Date;

        public string Round2Date
        {
            get { return _Round2Date; }
            set { _Round2Date = value; }
        }
        private string _Round3Date;

        public string Round3Date
        {
            get { return _Round3Date; }
            set { _Round3Date = value; }
        }
        private string _Round4Date;

        public string Round4Date
        {
            get { return _Round4Date; }
            set { _Round4Date = value; }
        }
        private string _Round5Date;
        private string _Period;

        public string Period
        {
            get { return _Period; }
            set { _Period = value; }
        }

        public string Round5Date
        {
            get { return _Round5Date; }
            set { _Round5Date = value; }
        }

        private string _ApplicantName;

        public string ApplicantName
        {
            get { return _ApplicantName; }
            set { _ApplicantName = value; }
        }
       
        public string EmailId
        {
            get { return _EmailId; }
            set { _EmailId = value; }
        }

        public string Skills
        {
            get { return _Skills; }
            set { _Skills = value; }
        }

        public string QualificationName
        {
            get { return _QualificationName; }
            set { _QualificationName = value; }
        }

        private string _Description;

        public string Description
        {
            get { return _Description; }
            set { _Description = value; }
        }
        private string _contract_from_date;

        public string Contract_from_date
        {
            get { return _contract_from_date; }
            set { _contract_from_date = value; }
        }
        private string _contract_to_date;

        public string Contract_to_date
        {
            get { return _contract_to_date; }
            set { _contract_to_date = value; }
        }
        private int _SeqNo;

        private string _Criteria;
        private string _Rating;
       
        private string _Comments;
        private int _Salary;

        public int Salary1
        {
            get { return _Salary; }
            set { _Salary = value; }
        }
        
        private string _FinalStatus;
        private string _HRComments;
        private char _c;
        private int _temp_int;
        private string _temp_string;

        private string _PGCourseName;
        private int _PGCourseID;
        private string _PGInstutionName;
        private string _PGPercentage;
        private string _PGCompletedYear;
        private string _period1;
        private string _Query_by;
        private string _Batch;

        public string Batch
        {
            get { return _Batch; }
            set { _Batch = value; }
        }

        public string Query_by
        {
            get { return _Query_by; }
            set { _Query_by = value; }
        }
       

        public string Period1
        {
            get { return _period1; }
            set { _period1 = value; }
        }

        private string _duration;

        public string Duration
        {
            get { return _duration; }
            set { _duration = value; }
        }
        private SqlConnection _Connection;
        ePayHrms.Connection.Connection Con = new ePayHrms.Connection.Connection();

       

        //**********************************Properties*********************************************************


       
        public string common
        {
            get { return _common; }
            set { _common = value; }
        }


        public int CompanyID
        {
            get { return _CompanyID; }
            set { _CompanyID = value; }
        }



        public string CompanyName
        {
            get { return _CompanyName; }
            set { _CompanyName = value; }
        }


        public int BranchID
        {
            get { return _BranchID; }
            set { _BranchID = value; }
        }

        public int Entry_BranchID
        {
            get { return _Entry_BranchID; }
            set { _Entry_BranchID = value; }
        }


        public string BranchName
        {
            get { return _BranchName; }
            set { _BranchName = value; }
        }




        public int RequisitionNo
        {
            get { return _RequisitionNo; }
            set { _RequisitionNo = value; }
        }

        public string RequisitionCode
        {
            get { return _RequisitionCode; }
            set { _RequisitionCode = value; }
        }  
       
        
        public DateTime RequisitionDate_r
        {
            get { return _RequisitionDate_r; }
            set { _RequisitionDate_r = value; }
        }

        public string RequiredDate
        {
            get { return _RequiredDate; }
            set { _RequiredDate = value; }
        }


        public DateTime RequiredDate_r
        {
            get { return _RequiredDate_r; }
            set { _RequiredDate_r = value; }
        }

        public string SortListDate
        {
            get { return _SortListDate; }
            set { _SortListDate = value; }
        }
        public string InterviewDate
        {
            get { return _InterviewDate; }
            set { _InterviewDate = value; }
        }

        public string InterviewTime
        {
            get { return _InterviewTime; }
            set { _InterviewTime = value; }
        }

        public string CurrentTime
        {
            get { return _CurrentTime; }
            set { _CurrentTime = value; }
        }

        public string CurrentDate
        {
            get { return _CurrentDate; }
            set { _CurrentDate = value; }
        }
        private int _EmployeeID;
        public int EmployeeID
        {
            get { return _EmployeeID; }
            set { _EmployeeID = value; }
        }

        public int Entry_EmployeeID
        {
            get { return _Entry_EmployeeID; }
            set { _Entry_EmployeeID = value; }
        }


        public string EmployeeName
        {
            get { return _EmployeeName; }
            set { _EmployeeName = value; }
        }
        public int OfficeID
        {
            get { return _OfficeID; }
            set { _OfficeID = value; }
        }


        public int DivisionID
        {
            get { return _DivisionID; }
            set { _DivisionID = value; }
        }


        public string DivisionName
        {
            get { return _DivisionName; }
            set { _DivisionName = value; }
        }



        public int DepartmentID
        {
            get { return _DepartmentID; }
            set { _DepartmentID = value; }
        }


        public string DepartmentName
        {
            get { return _DepartmentName; }
            set { _DepartmentName = value; }
        }


        public int DesignationID
        {
            get { return _DesignationID; }
            set { _DesignationID = value; }
        }


        public string DesignationName
        {
            get { return _DesignationName; }
            set { _DesignationName = value; }
        }

        public int GradeID
        {
            get { return _GradeID; }
            set { _GradeID = value; }
        }


        public string GradeName
        {
            get { return _GradeName; }
            set { _GradeName = value; }
        }

        public int JobStatusID
        {
            get { return _JobStatusID; }
            set { _JobStatusID = value; }
        }

        public string JobStatusName
        {
            get { return _JobStatusName; }
            set { _JobStatusName = value; }
        }


        public int ShiftTypeID
        {
            get { return _ShiftTypeID; }
            set { _ShiftTypeID = value; }
        }


        public string ShiftTypeName
        {
            get { return _ShiftTypeName; }
            set { _ShiftTypeName = value; }
        }



        public string Reason
        {
            get { return _Reason; }
            set { _Reason = value; }
        }



        public int CategoryCode
        {
            get { return _CategoryCode; }
            set { _CategoryCode = value; }
        }


        public string CategoryName
        {
            get { return _CategoryName; }
            set { _CategoryName = value; }
        }

        

        public int ProjectID
        {
            get { return _ProjectID; }
            set { _ProjectID = value; }
        }


        public string ProjectName
        {
            get { return _ProjectName; }
            set { _ProjectName = value; }
        }


        public int ExperienceMin
        {
            get { return _ExperienceMin; }
            set { _ExperienceMin = value; }
        }

        public string Experience
        {
            get { return _Experience; }
            set { _Experience = value; }
        }

        public int ExperienceMax
        {
            get { return _ExperienceMax; }
            set { _ExperienceMax = value; }
        }


        public int AgeGroupMin
        {
            get { return _AgeGroupMin; }
            set { _AgeGroupMin = value; }
        }
       

        public int AgeGroupMax
        {
            get { return _AgeGroupMax; }
            set { _AgeGroupMax = value; }
        }


        public int NoofVacancies
        {
            get { return _NoofVacancies; }
            set { _NoofVacancies = value; }
        }


        public int RequestStatusCode
        {
            get { return _RequestStatusCode; }
            set { _RequestStatusCode = value; }
        }
        public int SkillID
        {
            get { return _SkillID; }
            set { _SkillID = value; }
        }


        public int QualificationCode
        {
            get { return _QualificationCode; }
            set { _QualificationCode = value; }
        }


        public string r_Skills_str
        {
            get { return _Skills_str; }
            set { _Skills_str = value; }
        }

        public int SeqNo
        {
            get { return _SeqNo; }
            set { _SeqNo = value; }
        }


        public string Criteria
        {
            get { return _Criteria; }
            set { _Criteria = value; }
        }

        public string Rating
        {
            get { return _Rating; }
            set { _Rating = value; }
        }

        private string _Status;
        public string Status
        {
            get { return _Status; }
            set { _Status = value; }
        }


        public string Comments
        {
            get { return _Comments; }
            set { _Comments = value; }
        }



        public string FinalStatus
        {
            get { return _FinalStatus; }
            set { _FinalStatus = value; }
        }


        public string HRComments
        {
            get { return _HRComments; }
            set { _HRComments = value; }
        }


        public char c
        {
            get { return _c; }
            set { _c = value; }
        }


        public int temp_int
        {
            get { return _temp_int; }
            set { _temp_int = value; }
        }



        public string temp_string
        {
            get { return _temp_string; }
            set { _temp_string = value; }
        }


        public string PGCourseName
        {
            get { return _PGCourseName; }
            set { _PGCourseName = value; }
        }

        public int PGCourseID
        {
            get { return _PGCourseID; }
            set { _PGCourseID = value; }

        }


        public string PGInstutionName
        {
            get { return _PGInstutionName; }
            set { _PGInstutionName = value; }
        }

        public string PGPercentage
        {
            get { return _PGPercentage; }
            set { _PGPercentage = value; }
        }

        public string PGCompletedYear
        {
            get { return _PGCompletedYear; }
            set { _PGCompletedYear = value; }
        }

        #endregion

        //**********************************Functions & Procedures*********************************************************

        public Collection<Be_Recruitment> fn_get_ApplicantCodeee()
        {
            Collection<Be_Recruitment> ApplicantCode = new Collection<Be_Recruitment>();
            _connection = con.fn_Connection();
            SqlCommand cmd = new SqlCommand("select * from Recruit_JobDescriptions", _connection);
            _connection.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                Be_Recruitment recruitment = new Be_Recruitment();
                recruitment.pn_Jobcode = Convert.IsDBNull(dr["pn_Jobcode"]) ? "" : (string)dr["pn_Jobcode"];
                ApplicantCode.Add(recruitment);
            }
            return ApplicantCode;
        }

        public Collection<Be_Recruitment> fn_get_ApplicantCodeById(string _Jobcode)
          {
              Collection<Be_Recruitment> ApplicantCode = new Collection<Be_Recruitment>();
              _connection = con.fn_Connection();
              SqlCommand cmd = new SqlCommand("select * from Recruit_JobDescriptions where pn_Jobcode='" + _Jobcode + "'", _connection);
              _connection.Open();
              SqlDataReader dr = cmd.ExecuteReader();
              while (dr.Read())
              {
                  Be_Recruitment recruitment = new Be_Recruitment();
                  recruitment.pn_Jobcode = Convert.IsDBNull(dr["pn_Jobcode"]) ? "" : (string)dr["pn_Jobcode"];
                  ApplicantCode.Add(recruitment);
              }
              return ApplicantCode;
          }

         public Collection<Be_Recruitment> fn_get_ApplicantID(Be_Recruitment r)
           {           
                Collection<Be_Recruitment> applicantIDList = new Collection<Be_Recruitment>();
                _connection = con.fn_Connection();
                SqlCommand cmd = new SqlCommand("select * from Recruit_ApplicantInformation where pn_CompanyID=" + r.CompanyID + " and pn_BranchID =" + r.BranchID + "", _connection);
                _connection.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Be_Recruitment recruitment = new Be_Recruitment();
                    recruitment.ApplicatID = Convert.IsDBNull(dr["pn_ApplicantID"]) ? "" : (string)dr["pn_ApplicantID"];
                    recruitment.ApplicantFirstName = Convert.IsDBNull(dr["Applicant_First_Name"]) ? "" : (string)dr["Applicant_First_Name"];
                    recruitment.ApplicantfullName = (string)dr["pn_ApplicantID"] + "-" + (string)dr["Applicant_First_Name"];
                    applicantIDList.Add(recruitment);
                }
                return applicantIDList;
         }

         public Collection<Be_Recruitment> fn_getApplntID(Be_Recruitment r)
         {
             Collection<Be_Recruitment> applicantIDList = new Collection<Be_Recruitment>();
             _connection = con.fn_Connection();
             SqlCommand cmd = new SqlCommand("select * from Recruit_ApplicantInformation where pn_CompanyID='" + r.CompanyID + "' and pn_BranchID ='" + r.BranchID + "' and pn_ApplicantID='" +r.ApplicatID + "'", _connection);
             _connection.Open();
             SqlDataReader dr = cmd.ExecuteReader();
             while (dr.Read())
             {
                 Be_Recruitment recruitment = new Be_Recruitment();
                 recruitment.ApplicatID = Convert.IsDBNull(dr["pn_ApplicantID"]) ? "" : (string)dr["pn_ApplicantID"];                 
                 applicantIDList.Add(recruitment);
             }
             return applicantIDList;
         }         
         public Collection<Be_Recruitment> fn_GetApplicantCode(string _ApplicantID)
         {   
             Collection<Be_Recruitment> ApplicantCode = new Collection<Be_Recruitment>();
             _Connection = Con.fn_Connection();
             string _scmd = "select * from Recruit_ApplicantInformation where pn_ApplicantID='" + _ApplicantID + "'";
             SqlCommand _Cmd = new SqlCommand(_scmd, _Connection);
             _Connection.Open();
             SqlDataReader dr_Department = _Cmd.ExecuteReader();            
             while (dr_Department.Read())
             {
                 Be_Recruitment recruitment = new Be_Recruitment();
                 recruitment.ApplicatID = Convert.IsDBNull(dr_Department["pn_ApplicantID"]) ? "" : (string)dr_Department["pn_ApplicantID"];
                 ApplicantCode.Add(recruitment);
             }
             return ApplicantCode;
         }

         public Collection<Be_Recruitment> fn_getApplicantList(Be_Recruitment r)
         {
             Collection<Be_Recruitment> applicantList = new Collection<Be_Recruitment>();
             _connection = con.fn_Connection();
             SqlCommand cmd = new SqlCommand("select * from Recruit_ApplicantInformation where pn_CompanyID='" + r.CompanyID + "' and pn_BranchID ='" + r.BranchID + "' and pn_ApplicantID='" +r.ApplicatID +"'", _connection);
             _connection.Open();
             SqlDataReader dr = cmd.ExecuteReader();
             while (dr.Read())
             {
                 Be_Recruitment recruitment = new Be_Recruitment();
                 recruitment.ApplicatID = Convert.IsDBNull(dr["pn_ApplicantID"]) ? "" : (string)dr["pn_ApplicantID"];
                 recruitment.Applicant_Full_Name = Convert.IsDBNull(dr["Applicant_Full_Name"]) ? "" : (string)dr["Applicant_Full_Name"];
                 recruitment.ApplicantFirstName = Convert.IsDBNull(dr["Applicant_First_Name"]) ? "" : (string)dr["Applicant_First_Name"];
                 recruitment.ApplicantMiddleName = Convert.IsDBNull(dr["Applicant_Midle_Name"]) ? "" : (string)dr["Applicant_Midle_Name"];
                 recruitment.ApplicantLastName = Convert.IsDBNull(dr["Applicant_Last_Name"]) ? "" : (string)dr["Applicant_Last_Name"];
                 recruitment._Gender = Convert.IsDBNull(dr["Gender"]) ? "" : (string)dr["Gender"];
                 recruitment.Mstatus =(string)dr["MStatus"];
                 recruitment._Religion = Convert.IsDBNull(dr["Religion"]) ? "" : (string)dr["Religion"];
                 recruitment._Date = Convert.ToDateTime(dr["Date"]);
                 recruitment._Dob = Convert.ToDateTime(dr["Dob"]);
                 recruitment.Age = Convert.IsDBNull(dr["Age"]) ? "" : (string)dr["Age"];
                 recruitment._EmailID = Convert.IsDBNull(dr["EmailID"]) ? "" : (string)dr["EmailID"];
                 recruitment._CellNo = Convert.IsDBNull(dr["CellNo"]) ? "" : (string)dr["CellNo"];
                 recruitment._Nationality = Convert.IsDBNull(dr["Nationality"]) ? "" : (string)dr["Nationality"];
                 recruitment.Skills = Convert.IsDBNull(dr["Skills"]) ? "" : (string)dr["Skills"];
                 recruitment._Designation = Convert.IsDBNull(dr["Designation"]) ? "" : (string)dr["Designation"];
                 recruitment.Experience = Convert.IsDBNull(dr["Experience"]) ? "" : (string)dr["Experience"];
                 recruitment._Tenth_Institute_Name = Convert.IsDBNull(dr["Tenth_Institute_Name"]) ? "" : (string)dr["Tenth_Institute_Name"];
                 recruitment._Tenth_Board_Name = Convert.IsDBNull(dr["Tenth_Board_Name"]) ? "" : (string)dr["Tenth_Board_Name"];
                 recruitment._Tenth_Yearofpass =(int)dr["Tenth_Yearofpass"];
                 recruitment._Tenth_Percentage = Convert.IsDBNull(dr["Tenth_Percentage"]) ? "" : (string)dr["Tenth_Percentage"];
                 recruitment._Twelth_Institute_Name = Convert.IsDBNull(dr["Twelth_Institute_Name"]) ? "" : (string)dr["Twelth_Institute_Name"];
                 recruitment._Twelth_Board_Name = Convert.IsDBNull(dr["Twelth_Board_Name"]) ? "" : (string)dr["Twelth_Board_Name"];
                 recruitment._Tenth_Yearofpass = (int)dr["Twelth_Yearofpass"];
                 recruitment._Tenth_Percentage = Convert.IsDBNull(dr["Twelth_Percentage"]) ? "" : (string)dr["Twelth_Percentage"];
                 recruitment.Ug_College_Name = Convert.IsDBNull(dr["Ug_College_Name"]) ? "" : (string)dr["Ug_College_Name"];
                 recruitment.Ug_University_Name = Convert.IsDBNull(dr["Ug_University_Name"]) ? "" : (string)dr["Ug_University_Name"];
                 recruitment.Ug_Yearofpass =(int)dr["Ug_Yearofpass"];
                 recruitment.Ug_Percentage = Convert.IsDBNull(dr["Ug_Percentage"]) ? "" : (string)dr["Ug_Percentage"];
                 recruitment.Pg_College_Name = Convert.IsDBNull(dr["Pg_College_Name"]) ? "" : (string)dr["Pg_College_Name"];
                 recruitment.Pg_University_Name = Convert.IsDBNull(dr["Pg_University_Name"]) ? "" : (string)dr["Pg_University_Name"];
                 recruitment.Pg_Yearofpass = (int)dr["Pg_Yearofpass"];
                 recruitment.Pg_Percentage = Convert.IsDBNull(dr["Pg_Percentage"]) ? "" : (string)dr["Pg_Percentage"];
                 recruitment.Category1 = Convert.IsDBNull(dr["Category1"]) ? "" : (string)dr["Category1"];
                 recruitment.Category2 = Convert.IsDBNull(dr["Category2"]) ? "" : (string)dr["Category2"];                 
                 //recruitment._Resume = Convert.IsDBNull(dr["Resume"]) ? "" : (string)dr["Resume"];  
                 recruitment.ImgPath = Convert.IsDBNull(dr["ImgPath"]) ? "" : (string)dr["ImgPath"];
                 applicantList.Add(recruitment);
             }
                 return applicantList;
         }

         //public Collection<Be_Recruitment> fn_getApplicantcode(Be_Recruitment e)
         //{

         //}
         public Collection<Be_Recruitment> fn_get_Applicant_photo(Be_Recruitment r)
         {
             Collection<Be_Recruitment> EmployeeList = new Collection<Be_Recruitment>();
             _Connection = Con.fn_Connection();
             SqlCommand _Course = new SqlCommand("select * from Recruit_ApplicantInformation where pn_CompanyID='" + r.CompanyID + "' and pn_BranchID ='" + r.BranchID + "' and pn_ApplicantID='" + r.ApplicatID + "'", _Connection);
             _Connection.Open();
             SqlDataReader dr = _Course.ExecuteReader();
             while (dr.Read())
             {
                 Be_Recruitment recruitment = new Be_Recruitment();
                 recruitment.ImgPath = Convert.IsDBNull(dr["ImgName"]) ? "" : (string)dr["ImgName"];
                 EmployeeList.Add(recruitment);
             }
             return EmployeeList;
         }
         public Collection<Be_Recruitment> fn_getCategoryList1(Be_Recruitment r)
         {            
                 Collection<Be_Recruitment> CategoryList1 = new Collection<Be_Recruitment>();
                 _connection = con.fn_Connection();
                 SqlCommand cmd = new SqlCommand("select * from Recruit_Applicant_Category1 where pn_CompanyID=" + r.CompanyID + " and pn_BranchID =" + r.BranchID + "", _connection);
                 _connection.Open();
                 SqlDataReader dr = cmd.ExecuteReader();
                 while (dr.Read())
                 {
                     Be_Recruitment recruitment = new Be_Recruitment();
                     recruitment.ReferenceID = (int)dr["Reference_ID"];
                     recruitment.ReferenceName = Convert.IsDBNull(dr["Reference_Name"]) ? "" : (string)dr["Reference_Name"];
                     CategoryList1.Add(recruitment);
                 }
                 return CategoryList1;                

         }
         public Collection<Be_Recruitment> fn_getCategoryList2(Be_Recruitment r)
         {
             Collection<Be_Recruitment> CategoryList1 = new Collection<Be_Recruitment>();
             _connection = con.fn_Connection();
             SqlCommand cmd = new SqlCommand("select * from Recruit_Applicant_Category2 where pn_CompanyID=" + r.CompanyID + " and pn_BranchID =" + r.BranchID + "", _connection);
             _connection.Open();
             SqlDataReader dr = cmd.ExecuteReader();
             while (dr.Read())
             {
                 Be_Recruitment recruitment = new Be_Recruitment();
                 recruitment.ID = (int)dr["ID"];
                 recruitment.ReferenceName = Convert.IsDBNull(dr["Name"]) ? "" : (string)dr["Name"];
                 recruitment.ReferenceID = (int)dr["Reference_ID"];
                 CategoryList1.Add(recruitment);
             }
             return CategoryList1;  
         }

         public string Applicant_Info(Be_Recruitment r)
          {
             try
             {
                 _Connection = Con.fn_Connection();                 
                 SqlCommand cmdApplicant = new SqlCommand("sp_Applicant_Details", _Connection);
                 cmdApplicant.CommandType = CommandType.StoredProcedure;
                 SqlParameter[] sqlpara = new SqlParameter[45];
                 sqlpara[0] = new SqlParameter("@pn_CompanyID", SqlDbType.Int);
                 sqlpara[0].Value = r.CompanyID;
                 sqlpara[1] = new SqlParameter("@pn_BranchID", SqlDbType.Int);
                 sqlpara[1].Value = r.BranchID;
                 sqlpara[2] = new SqlParameter("@pn_ApplicantID",SqlDbType.VarChar);
                 sqlpara[2].Value = r.ApplicatID;
                 sqlpara[3] = new SqlParameter("@Salutation", SqlDbType.VarChar);
                 sqlpara[3].Value = r._Salutation;                
                 sqlpara[4] = new SqlParameter("@Applicant_Full_Name", SqlDbType.VarChar);
                 sqlpara[4].Value = r.Applicant_Full_Name;
                 sqlpara[5] = new SqlParameter("@Applicant_First_Name", SqlDbType.VarChar);
                 sqlpara[5].Value = r.Applicant_First_Name;
                 sqlpara[6] = new SqlParameter("@Applicant_Midle_Name", SqlDbType.VarChar);
                 sqlpara[6].Value = r.Applicant_Midle_Name;
                 sqlpara[7] = new SqlParameter("@Applicant_Last_Name", SqlDbType.VarChar);
                 sqlpara[7].Value = r.Applicant_Last_Name;
                 sqlpara[8] = new SqlParameter("@Gender", SqlDbType.VarChar);
                 sqlpara[8].Value = r._Gender;
                 sqlpara[9] = new SqlParameter("@MStatus", SqlDbType.VarChar);
                 sqlpara[9].Value = r.Mstatus;
                 sqlpara[10] = new SqlParameter("@Religion", SqlDbType.VarChar);
                 sqlpara[10].Value = r._Religion;
                 sqlpara[11] = new SqlParameter("@Date", SqlDbType.DateTime);
                 sqlpara[11].Value = r.Date;
                 sqlpara[12] = new SqlParameter("@Dob", SqlDbType.DateTime);
                 sqlpara[12].Value = r._Dob;
                 sqlpara[13] = new SqlParameter("@Age", SqlDbType.VarChar);
                 sqlpara[13].Value = r.Age;
                 sqlpara[14] = new SqlParameter("@EmailID", SqlDbType.VarChar);
                 sqlpara[14].Value = r._EmailID;
                 sqlpara[15] = new SqlParameter("@CellNo", SqlDbType.VarChar);
                 sqlpara[15].Value = r._CellNo;
                 sqlpara[16] = new SqlParameter("@Nationality", SqlDbType.VarChar);
                 sqlpara[16].Value = r._Nationality;
                 sqlpara[17] = new SqlParameter("@Skills", SqlDbType.VarChar);
                 sqlpara[17].Value = r.Skills;
                 sqlpara[18] = new SqlParameter("@Designation", SqlDbType.VarChar);
                 sqlpara[18].Value = r._Designation;
                 sqlpara[19] = new SqlParameter("@Experience", SqlDbType.VarChar);
                 sqlpara[19].Value = r.Experience;
                 sqlpara[20] = new SqlParameter("@Tenth_Institute_Name", SqlDbType.VarChar);
                 sqlpara[20].Value = r.Tenth_Institute_Name;
                 sqlpara[21] = new SqlParameter("@Tenth_Board_Name", SqlDbType.VarChar);
                 sqlpara[21].Value = r.Tenth_Board_Name;
                 sqlpara[22] = new SqlParameter("@Tenth_Yearofpass", SqlDbType.Int);
                 sqlpara[22].Value = r._Tenth_Yearofpass;
                 sqlpara[23] = new SqlParameter("@Tenth_Percentage", SqlDbType.VarChar);
                 sqlpara[23].Value = r._Tenth_Percentage;
                 sqlpara[24] = new SqlParameter("@Twelth_Institute_Name", SqlDbType.VarChar);
                 sqlpara[24].Value = r.Twelth_Institute_Name;
                 sqlpara[25] = new SqlParameter("@Twelth_Board_Name", SqlDbType.VarChar);
                 sqlpara[25].Value = r._Twelth_Board_Name;
                 sqlpara[26] = new SqlParameter("@Twelth_Yearofpass", SqlDbType.Int);
                 sqlpara[26].Value = r._Tenth_Yearofpass;
                 sqlpara[27] = new SqlParameter("@Twelth_Percentage", SqlDbType.VarChar);
                 sqlpara[27].Value = r._Twelth_Percentage;
                 sqlpara[28] = new SqlParameter("@Ug_College_Name", SqlDbType.VarChar);
                 sqlpara[28].Value = r.Ug_College_Name;
                 sqlpara[29] = new SqlParameter("@Ug_University_Name", SqlDbType.VarChar);
                 sqlpara[29].Value = r.Ug_University_Name;
                 sqlpara[30] = new SqlParameter("@Ug_Yearofpass", SqlDbType.Int);
                 sqlpara[30].Value = r.Ug_Yearofpass;
                 sqlpara[31] = new SqlParameter("@Ug_Percentage", SqlDbType.VarChar);
                 sqlpara[31].Value = r.Ug_Percentage;
                 sqlpara[32] = new SqlParameter("Ug_Course_Name",SqlDbType.VarChar);
                 sqlpara[32].Value = r.Ug_Course_Name;
                 sqlpara[33] = new SqlParameter("@Pg_College_Name", SqlDbType.VarChar);
                 sqlpara[33].Value = r.Pg_College_Name;
                 sqlpara[34] = new SqlParameter("@Pg_University_Name", SqlDbType.VarChar);
                 sqlpara[34].Value = r.Pg_University_Name;
                 sqlpara[35] = new SqlParameter("@Pg_Yearofpass", SqlDbType.Int);
                 sqlpara[35].Value = r.Pg_Yearofpass;
                 sqlpara[36] = new SqlParameter("@Pg_Percentage", SqlDbType.VarChar);
                 sqlpara[36].Value = r.Pg_Percentage;
                 sqlpara[37] = new SqlParameter("@Pg_Course_Name",SqlDbType.VarChar);
                 sqlpara[37].Value = r.PGCourseName;
                 sqlpara[38] = new SqlParameter("@JobCode", SqlDbType.VarChar);
                 sqlpara[38].Value = r.JobCode;
                 sqlpara[39] = new SqlParameter("@EmployeeReference",SqlDbType.VarChar);
                 sqlpara[39].Value = r.EmployeeReference;
                 sqlpara[40] = new SqlParameter("@Category1",SqlDbType.VarChar);
                 sqlpara[40].Value = r.Category1;
                 sqlpara[41] = new SqlParameter("@Category2",SqlDbType.VarChar);
                 sqlpara[41].Value = r.Category2;
                 //sqlpara[39] = new SqlParameter("@Resume", SqlDbType.VarChar);
                 //sqlpara[39].Value = r._Resume;
                 sqlpara[42] = new SqlParameter("@ImgName", SqlDbType.VarChar);
                 sqlpara[42].Value = r.ImgName;
                 sqlpara[43] = new SqlParameter("@ImgPath",SqlDbType.VarChar);
                 sqlpara[43].Value = r.ImgPath;
                 sqlpara[44] = new SqlParameter("@CourseName", SqlDbType.VarChar);
                 sqlpara[44].Value = r.CourseName;
                 for (int i = 0; i < sqlpara.Length; i++)
                 {
                     cmdApplicant.Parameters.Add(sqlpara[i]);
                 }
                 _Connection.Open();
                 cmdApplicant.ExecuteNonQuery();
                 _Connection.Close();
                 return "0";
             }
             catch (SqlException ex)
             {
                 return "1";
             }             
         }

         public DateTime Convert_ToSqlDate(string cur_date)
         {
             string _d, _m, _y;
             DateTime sql_date;


             if (cur_date != "")
             {
                 string[] da = cur_date.Split('/');
                 _d = da[0];
                 _m = da[1];
                 _y = da[2];
                 //sql_date = Convert.ToDateTime("1900/01/01");
                 sql_date = Convert.ToDateTime(_y + "/" + _m + "/" + _d);
             }
             else
             {
                 sql_date = Convert.ToDateTime("2000/01/01");

             }

             return sql_date;

         }
        
         public void JobReqDesignation(string r)
         {
             _Connection = Con.fn_Connection();

             SqlCommand cmd = new SqlCommand(r, _Connection);
             //cmd.CommandType = CommandType.Text;
             try
             {
                 _Connection.Open();
                 cmd.ExecuteNonQuery();
                 _Connection.Close();
             }
             catch (Exception ex)
             {

             }
         }

         public void fn_getShortlisted(string shortlist)
         {
             _Connection = Con.fn_Connection();
             _Connection.Open();
             SqlCommand cmd = new SqlCommand(shortlist, _Connection);
             SqlDataReader dr = cmd.ExecuteReader();
             
         }

         public Collection<Be_Recruitment> JobDescriptionDesignation(Be_Recruitment r)
         {
             Collection<Be_Recruitment> DesignationList = new Collection<Be_Recruitment>();
             _connection = con.fn_Connection();
             string sql_sqry = "select * from Recruit_JobRequsitionDesignation where pn_CompanyID='" + r.CompanyID + "' and pn_BranchID='" + r.BranchID + "'";
             SqlCommand cmd = new SqlCommand(sql_sqry, _connection);
             _connection.Open();
             SqlDataReader dr = cmd.ExecuteReader();
             while (dr.Read())
             {
                 Be_Recruitment JobDesignation = new Be_Recruitment();
                 JobDesignation.JobDescriptDesignation = (string)dr["Designation"];
                 DesignationList.Add(JobDesignation);
             }
             return DesignationList;
         }  


        public string  JobDescription(Be_Recruitment r)
         {
            try
             {
                 _Connection = Con.fn_Connection();
                 _Connection.Open();
                 SqlCommand cmd = new SqlCommand("sp_Recruit_JobDescription", _Connection);
                 cmd.CommandType = CommandType.StoredProcedure;
                 SqlParameter[] JobDescript=new SqlParameter[18];
                 JobDescript[0] = new SqlParameter("@pn_CompanyID",SqlDbType.Int);
                 JobDescript[0].Value = r.CompanyID;
                 JobDescript[1] = new SqlParameter("@pn_BranchID", SqlDbType.Int);
                 JobDescript[1].Value = r.BranchID;
                 JobDescript[2] = new SqlParameter("@pn_Jobcode", SqlDbType.VarChar);
                 JobDescript[2].Value = r.pn_Jobcode;
                 JobDescript[3] = new SqlParameter("@JobType", SqlDbType.VarChar);
                 JobDescript[3].Value = r.JobType;
                 JobDescript[4] = new SqlParameter("@ContractPeriod", SqlDbType.VarChar);
                 JobDescript[4].Value = r.ContractPeriod;
                 JobDescript[5] = new SqlParameter("@JobTitle", SqlDbType.VarChar);
                 JobDescript[5].Value = r.JobTitle;
                 JobDescript[6] = new SqlParameter("@NoofRequired",SqlDbType.Int);
                 JobDescript[6].Value = r.NoofRequired;
                 JobDescript[7] = new SqlParameter("@RequisitionDate", SqlDbType.DateTime);
                 JobDescript[7].Value = r.requsiondate;
                 JobDescript[8] = new SqlParameter("@RequiredDate",SqlDbType.DateTime);
                 JobDescript[8].Value = r.RequireDated;
                 JobDescript[9] = new SqlParameter("@Qualification",SqlDbType.VarChar);
                 JobDescript[9].Value = r.Qualification;
                 JobDescript[10] = new SqlParameter("@Skills",SqlDbType.VarChar);
                 JobDescript[10].Value = r.Skills;
                 JobDescript[11] = new SqlParameter("@DutyAndResponsibility",SqlDbType.VarChar);
                 JobDescript[11].Value = r.DutyAndResponsibility;
                 JobDescript[12] = new SqlParameter("@AgeLimit",SqlDbType.VarChar);
                 JobDescript[12].Value = r.AgeLimit;
                 JobDescript[13] = new SqlParameter("@Experience",SqlDbType.VarChar);
                 JobDescript[13].Value = r.Experience;
                 JobDescript[14] = new SqlParameter("@Department",SqlDbType.VarChar);
                 JobDescript[14].Value = r.Department;
                 JobDescript[15] = new SqlParameter("@Status",SqlDbType.VarChar);
                 JobDescript[15].Value = r._Statuss;
                 JobDescript[16] = new SqlParameter("@ApprovedBy", SqlDbType.VarChar);
                 JobDescript[16].Value = r.ApprovedBy;
                 JobDescript[17] = new SqlParameter("@SubmitTo", SqlDbType.VarChar);
                 JobDescript[17].Value = r.SubmitTo;
                 for (int i = 0; i < JobDescript.Length; i++)
                 {
                     cmd.Parameters.Add(JobDescript[i]);
                 }                 
                 cmd.ExecuteNonQuery();
                 _Connection.Close();

                 return "0";
             }  
            catch(Exception EX)
             {
                 return "1";
            }  
         }



        public string Recruitment(Be_Recruitment r)
        {
            try
            {
                _Connection = Con.fn_Connection();
                SqlCommand _ISPRecruitment = new SqlCommand("sp_Recruit_Jobrequisition", _Connection);
                _ISPRecruitment.CommandType = CommandType.StoredProcedure;
                SqlParameter[] _ISPRecruitmentParam = new SqlParameter[18];
                _ISPRecruitmentParam[0] = new SqlParameter("@pn_CompanyID", SqlDbType.Int);
                _ISPRecruitmentParam[0].Value = r.CompanyID;
                _ISPRecruitmentParam[1] = new SqlParameter("@pn_BranchID", SqlDbType.Int);
                _ISPRecruitmentParam[1].Value = r.BranchID;
                _ISPRecruitmentParam[2] = new SqlParameter("@pn_DepartmentID", SqlDbType.VarChar);
                _ISPRecruitmentParam[2].Value = r.DepartmentName;
                _ISPRecruitmentParam[3] = new SqlParameter("@Job_code", SqlDbType.VarChar);
                _ISPRecruitmentParam[3].Value = r.RequisitionCode;
                _ISPRecruitmentParam[4] = new SqlParameter("@Job_Title", SqlDbType.VarChar);
                _ISPRecruitmentParam[4].Value = r.JobStatusName;
                _ISPRecruitmentParam[5] = new SqlParameter("@pn_EmployeeID", SqlDbType.VarChar);
                _ISPRecruitmentParam[5].Value = r.EmployeeID;
                _ISPRecruitmentParam[6] = new SqlParameter("@No_of_Vacancies", SqlDbType.Int);
                _ISPRecruitmentParam[6].Value = r.NoofVacancies;
                _ISPRecruitmentParam[7] = new SqlParameter("@Age", SqlDbType.VarChar);
                _ISPRecruitmentParam[7].Value = r.Age;
                _ISPRecruitmentParam[8] = new SqlParameter("@Experience", SqlDbType.VarChar);
                _ISPRecruitmentParam[8].Value = r.Experience;
                _ISPRecruitmentParam[9] = new SqlParameter("@Job_type", SqlDbType.VarChar);
                _ISPRecruitmentParam[9].Value = r.JobType;
                _ISPRecruitmentParam[10] = new SqlParameter("@contract_period", SqlDbType.VarChar);
                _ISPRecruitmentParam[10].Value = r.Duration;               
                _ISPRecruitmentParam[11] = new SqlParameter("@Requisition_Date", SqlDbType.DateTime);
                _ISPRecruitmentParam[11].Value = r.RequisitionDate;
                _ISPRecruitmentParam[12] = new SqlParameter("@Required_Date", SqlDbType.VarChar);
                _ISPRecruitmentParam[12].Value = r.RequiredDate;
                _ISPRecruitmentParam[13] = new SqlParameter("@Qualification", SqlDbType.VarChar);
                _ISPRecruitmentParam[13].Value = r.QualificationName;
                _ISPRecruitmentParam[14] = new SqlParameter("@Skills", SqlDbType.VarChar);
                _ISPRecruitmentParam[14].Value = r.Skills;
                _ISPRecruitmentParam[15] = new SqlParameter("@Status", SqlDbType.VarChar);
                _ISPRecruitmentParam[15].Value = r.Status;            
                
                _ISPRecruitmentParam[16] = new SqlParameter("@comments", SqlDbType.VarChar);
                _ISPRecruitmentParam[16].Value = r.Comment;
                _ISPRecruitmentParam[17] = new SqlParameter("@Description", SqlDbType.VarChar);
                _ISPRecruitmentParam[17].Value = r.Description;

                for (int i = 0; i < _ISPRecruitmentParam.Length; i++)
                {
                    _ISPRecruitment.Parameters.Add(_ISPRecruitmentParam[i]);
                }
                _Connection.Open();
                _ISPRecruitment.ExecuteNonQuery();
                _Connection.Close();
                return "0";
            }
            catch (SqlException Ex)
            {
                return "1";
            }
        }

        public string Resume_Shortlist(Be_Recruitment s)
        {
            try
            {             
                _Connection = con.fn_Connection();
                SqlCommand _ISPSHORTLIST = new SqlCommand("sp_Recruit_Resume", _Connection);
                _ISPSHORTLIST.CommandType = CommandType.StoredProcedure;
                SqlParameter[] _ISPShortlistparam = new SqlParameter[13];
                _ISPShortlistparam[0] = new SqlParameter("@pn_CompanyID", SqlDbType.Int);
                _ISPShortlistparam[0].Value = s.CompanyID;
                _ISPShortlistparam[1] = new SqlParameter("@pn_BranchID", SqlDbType.Int);
                _ISPShortlistparam[1].Value = s.BranchID;
                _ISPShortlistparam[2] = new SqlParameter("@Resume_ID", SqlDbType.Int);
                _ISPShortlistparam[2].Value = s.ResumeId;            
                _ISPShortlistparam[3] = new SqlParameter("@Job_code", SqlDbType.VarChar);
                _ISPShortlistparam[3].Value = s.RequisitionCode;
                _ISPShortlistparam[4] = new SqlParameter("@Candidate_Name", SqlDbType.VarChar);
                _ISPShortlistparam[4].Value = s.ApplicantName;
                _ISPShortlistparam[5] = new SqlParameter("@Qualification", SqlDbType.VarChar);
                _ISPShortlistparam[5].Value = s.QualificationName;
                _ISPShortlistparam[6] = new SqlParameter("@Skills", SqlDbType.VarChar);
                _ISPShortlistparam[6].Value = s.Skills;
                _ISPShortlistparam[7] = new SqlParameter("@PhoneNo", SqlDbType.VarChar);
                _ISPShortlistparam[7].Value = s.PhoneMobile;
                _ISPShortlistparam[8] = new SqlParameter("@Resume_name", SqlDbType.VarChar);
                _ISPShortlistparam[8].Value = s.Resumename;
                _ISPShortlistparam[9] = new SqlParameter("@EmailId", SqlDbType.VarChar);
                _ISPShortlistparam[9].Value = s.EmailId;
                _ISPShortlistparam[10] = new SqlParameter("@Filetype", SqlDbType.VarChar);
                _ISPShortlistparam[10].Value = s.Filetype1;                
                _ISPShortlistparam[11] = new SqlParameter("@Status", SqlDbType.VarChar);
                _ISPShortlistparam[11].Value = s.Status;
                _ISPShortlistparam[12] = new SqlParameter("@Final_result", SqlDbType.VarChar);
                _ISPShortlistparam[12].Value = s.FinalStatus;               

                for (int i = 0; i < _ISPShortlistparam.Length; i++)
                {

                    _ISPSHORTLIST.Parameters.Add(_ISPShortlistparam[i]);
                }
                _Connection.Open();
                _ISPSHORTLIST.ExecuteNonQuery();
                _Connection.Close();
                return "0";

            }
            catch (Exception ex)
            {
                return "1";
            }
        }
       
        public string Resume_Shortlist1(Be_Recruitment s)
        {
            try
            {
                _Connection = con.fn_Connection();
                SqlCommand _ISPSHORTLIST = new SqlCommand("sp_Recruit_Resume1", _Connection);
                _ISPSHORTLIST.CommandType = CommandType.StoredProcedure;
                SqlParameter[] _ISPShortlistparam = new SqlParameter[5];
                _ISPShortlistparam[0] = new SqlParameter("@pn_CompanyID", SqlDbType.Int);
                _ISPShortlistparam[0].Value = s.CompanyID;
                _ISPShortlistparam[1] = new SqlParameter("@pn_BranchID", SqlDbType.Int);
                _ISPShortlistparam[1].Value = s.BranchID;
                _ISPShortlistparam[2] = new SqlParameter("@Resume_ID", SqlDbType.Int);
                _ISPShortlistparam[2].Value = s.ResumeId;
                _ISPShortlistparam[3] = new SqlParameter("@Job_code", SqlDbType.VarChar);
                _ISPShortlistparam[3].Value = s.RequisitionCode;
                _ISPShortlistparam[4] = new SqlParameter("@Candidate_Name", SqlDbType.VarChar);
                _ISPShortlistparam[4].Value = s.ApplicantName;
                for (int i = 0; i < _ISPShortlistparam.Length; i++)
                {
                    _ISPSHORTLIST.Parameters.Add(_ISPShortlistparam[i]);
                }
                _Connection.Open();
                _ISPSHORTLIST.ExecuteNonQuery();
                _Connection.Close();
                return "0";
            }
            catch (Exception ex)
            {
                return "1";
            }
        }

        public Collection<Be_Recruitment> Re_Shortlist(Be_Recruitment s)
        {
            Collection<Be_Recruitment> ResumeList = new Collection<Be_Recruitment>();
            _connection = con.fn_Connection();
            string sql_sqry = "select * from recruit_resume where pn_CompanyID='" + s.CompanyID + "' and pn_BranchID='" + s.BranchID + "' and job_code='"+s.RequisitionCode+"' order by resume_id ";
            SqlCommand cmd = new SqlCommand(sql_sqry, _connection);
            _connection.Open();
            SqlDataReader drd = cmd.ExecuteReader();
            while (drd.Read())
            {
                Be_Recruitment Shortlist = new Be_Recruitment();
                Shortlist.ResumeId = (int)drd["Resume_ID"];
                Shortlist.RequisitionCode = Convert.IsDBNull(drd["Job_Code"]) ? "" : (string)drd["Job_code"];
                Shortlist.ApplicantName = Convert.IsDBNull(drd["Candidate_Name"]) ? "" : (string)drd["Candidate_Name"];
                Shortlist.QualificationName = Convert.IsDBNull(drd["Qualification"]) ? "" : (string)drd["Qualification"];
                Shortlist.Skills = Convert.IsDBNull(drd["Skills"]) ? "" : (string)drd["Skills"];
                Shortlist.PhoneMobile = Convert.IsDBNull(drd["PhoneNo"]) ? "" : (string)drd["PhoneNo"];
                Shortlist.Resumename = Convert.IsDBNull(drd["Resume_name"]) ? "" : (string)drd["Resume_name"];
                Shortlist.EmailId = Convert.IsDBNull(drd["EmailID"]) ? "" : (string)drd["EmailID"];
                Shortlist.Status = Convert.IsDBNull(drd["Status"]) ? "" : (string)drd["Status"];
                //Shortlist.Filedata1 = Convert.IsDBNull(drd["Filedata"]) ? "" : (Byte[])drd["Filedata"];
                //Schedule.Place = Convert.IsDBNull(rdr["place"]) ? "" : (string)rdr["place"];
                //Schedule.DepartmentName = Convert.IsDBNull(rdr["v_DepartmentName"]) ? "" : (string)rdr["v_DepartmentName"];
                ResumeList.Add(Shortlist);
            }
            return ResumeList;
        }

        public Collection<Be_Recruitment> Re_JobRequest(Be_Recruitment r)
        {
            Collection<Be_Recruitment> RecruitmentList = new Collection<Be_Recruitment>();
            _Connection = con.fn_Connection();
            string _SqlString = "select a.*,c.Employee_First_Name, c.pn_employeeID from Recruit_jobrequistion a,Paym_employee c where a.Pn_EmployeeId=c.Pn_EmployeeID  and a.pn_branchID='" + r.BranchID + "' and a.Pn_CompanyID='" + r.CompanyID + "' and a.Pn_EmployeeId='"+ r.EmployeeID +"' order by a.Job_code";
            SqlCommand cmd = new SqlCommand(_SqlString, _Connection);
            _Connection.Open();
            SqlDataReader dr_recruit = cmd.ExecuteReader();
            while (dr_recruit.Read())
            {
                Be_Recruitment Recruitment = new Be_Recruitment();
                Recruitment.RequisitionCode = (string)dr_recruit["Job_Code"] + "-" + (string)dr_recruit["Job_Title"]; //Convert.IsDBNull(dr_recruit["Job_code"]) ? "" : (string)dr_recruit["Job_code_Title"];
               
                Recruitment.DepartmentName = Convert.IsDBNull(dr_recruit["pn_departmentID"]) ? "" : (string)dr_recruit["pn_departmentID"];
               
                Recruitment.JobType = Convert.IsDBNull(dr_recruit["Job_Type"]) ? "" : (string)dr_recruit["Job_Type"];
                Recruitment.Duration = Convert.IsDBNull(dr_recruit["contract_period"]) ? "" : (string)dr_recruit["contract_period"];
                Recruitment.NoofVacancies = (int)dr_recruit["No_Of_Vacancies"];
                Recruitment.RequisitionDate = Convert.ToDateTime(dr_recruit["Requisition_Date"]);// Convert.IsDBNull(dr_recruit["Requisition_Date"]) ? "" : (DateTime)dr_recruit["Requisition_Date"]; //(DateTime)dr_recruit["Requisition_Date"];
                Recruitment.RequiredDate = Convert.IsDBNull(dr_recruit["Required_Date"]) ? "" : (string)dr_recruit["Required_Date"];
                Recruitment.EmployeeName = (int)dr_recruit["pn_employeeID"] + "-" + (string)dr_recruit["Employee_First_Name"]; //Convert.IsDBNull(dr_recruit["Employee_First_Name"]) ? "" : (string)dr_recruit["Employee_First_Name"];
                Recruitment.QualificationName = Convert.IsDBNull(dr_recruit["Qualification"]) ? "" : (string)dr_recruit["Qualification"];
                Recruitment.Skills = Convert.IsDBNull(dr_recruit["Skills"]) ? "" : (string)dr_recruit["Skills"];
               
                Recruitment.Age = Convert.IsDBNull(dr_recruit["Age"]) ? "" : (string)dr_recruit["Age"];
                Recruitment.Experience = Convert.IsDBNull(dr_recruit["Experience"]) ? "" : (string)dr_recruit["Experience"];
                Recruitment.Status = Convert.IsDBNull(dr_recruit["Status"]) ? "" : (string)dr_recruit["Status"];
                Recruitment.Description = Convert.IsDBNull(dr_recruit["Description"]) ? "" : (string)dr_recruit["Description"];
                Recruitment.Comment = Convert.IsDBNull(dr_recruit["comments"]) ? "" : (string)dr_recruit["comments"];
                RecruitmentList.Add(Recruitment);
            }
            return RecruitmentList;
        }
        public Collection<Be_Recruitment> Re_Jobapprove(Be_Recruitment r)
        {
            Collection<Be_Recruitment> RecruitmentList = new Collection<Be_Recruitment>();
            _Connection = con.fn_Connection();
            string _SqlString = "select a.*,c.Employee_First_Name, c.pn_employeeID from Recruit_jobrequistion a,Paym_employee c where a.Pn_EmployeeId=c.Pn_EmployeeID  and a.pn_branchID='" + r.BranchID + "' and a.Pn_CompanyID='" + r.CompanyID + "' order by a.Job_code";
            SqlCommand cmd = new SqlCommand(_SqlString, _Connection);
            _Connection.Open();
            SqlDataReader dr_recruit = cmd.ExecuteReader();
            while (dr_recruit.Read())
            {
                Be_Recruitment Recruitment = new Be_Recruitment();
                Recruitment.RequisitionCode = (string)dr_recruit["Job_Code"] + "-" + (string)dr_recruit["Job_Title"]; //Convert.IsDBNull(dr_recruit["Job_code"]) ? "" : (string)dr_recruit["Job_code_Title"];

                Recruitment.DepartmentName = Convert.IsDBNull(dr_recruit["pn_departmentID"]) ? "" : (string)dr_recruit["pn_departmentID"];

                Recruitment.JobType = Convert.IsDBNull(dr_recruit["Job_Type"]) ? "" : (string)dr_recruit["Job_Type"];
                Recruitment.Duration = Convert.IsDBNull(dr_recruit["contract_period"]) ? "" : (string)dr_recruit["contract_period"];
                Recruitment.NoofVacancies = (int)dr_recruit["No_Of_Vacancies"];
                Recruitment.RequisitionDate = Convert.ToDateTime(dr_recruit["Requisition_Date"]);// Convert.IsDBNull(dr_recruit["Requisition_Date"]) ? "" : (DateTime)dr_recruit["Requisition_Date"]; //(DateTime)dr_recruit["Requisition_Date"];
                Recruitment.RequiredDate = Convert.IsDBNull(dr_recruit["Required_Date"]) ? "" : (string)dr_recruit["Required_Date"];
                Recruitment.EmployeeName = (int)dr_recruit["pn_employeeID"] + "-" + (string)dr_recruit["Employee_First_Name"]; //Convert.IsDBNull(dr_recruit["Employee_First_Name"]) ? "" : (string)dr_recruit["Employee_First_Name"];
                Recruitment.QualificationName = Convert.IsDBNull(dr_recruit["Qualification"]) ? "" : (string)dr_recruit["Qualification"];
                Recruitment.Skills = Convert.IsDBNull(dr_recruit["Skills"]) ? "" : (string)dr_recruit["Skills"];

                Recruitment.Age = Convert.IsDBNull(dr_recruit["Age"]) ? "" : (string)dr_recruit["Age"];
                Recruitment.Experience = Convert.IsDBNull(dr_recruit["Experience"]) ? "" : (string)dr_recruit["Experience"];
                Recruitment.Status = Convert.IsDBNull(dr_recruit["Status"]) ? "" : (string)dr_recruit["Status"];
                Recruitment.Description = Convert.IsDBNull(dr_recruit["Description"]) ? "" : (string)dr_recruit["Description"];
                Recruitment.Comment = Convert.IsDBNull(dr_recruit["comments"]) ? "" : (string)dr_recruit["comments"];
                RecruitmentList.Add(Recruitment);
            }
            return RecruitmentList;
        }
        public Collection<Be_Recruitment> Re_Jobapprove1(Be_Recruitment r)
        {
            Collection<Be_Recruitment> RecruitmentList = new Collection<Be_Recruitment>();
            _Connection = con.fn_Connection();
            string _SqlString = "select a.*,c.Employee_First_Name, c.pn_employeeID from Recruit_jobrequistion a,Paym_employee c where a.Pn_EmployeeId=c.Pn_EmployeeID  and a.pn_branchID='" + r.BranchID + "' and a.Pn_CompanyID='" + r.CompanyID + "' and c.pn_employeeID='"+r.EmployeeID+"' order by a.Job_code";
            SqlCommand cmd = new SqlCommand(_SqlString, _Connection);
            _Connection.Open();
            SqlDataReader dr_recruit = cmd.ExecuteReader();
            while (dr_recruit.Read())
            {
                Be_Recruitment Recruitment = new Be_Recruitment();
                Recruitment.RequisitionCode = (string)dr_recruit["Job_Code"] + "-" + (string)dr_recruit["Job_Title"]; //Convert.IsDBNull(dr_recruit["Job_code"]) ? "" : (string)dr_recruit["Job_code_Title"];

                Recruitment.DepartmentName = Convert.IsDBNull(dr_recruit["pn_departmentID"]) ? "" : (string)dr_recruit["pn_departmentID"];

                Recruitment.JobType = Convert.IsDBNull(dr_recruit["Job_Type"]) ? "" : (string)dr_recruit["Job_Type"];
                Recruitment.Duration = Convert.IsDBNull(dr_recruit["contract_period"]) ? "" : (string)dr_recruit["contract_period"];
                Recruitment.NoofVacancies = (int)dr_recruit["No_Of_Vacancies"];
                Recruitment.RequisitionDate = Convert.ToDateTime(dr_recruit["Requisition_Date"]);// Convert.IsDBNull(dr_recruit["Requisition_Date"]) ? "" : (DateTime)dr_recruit["Requisition_Date"]; //(DateTime)dr_recruit["Requisition_Date"];
                Recruitment.RequiredDate = Convert.IsDBNull(dr_recruit["Required_Date"]) ? "" : (string)dr_recruit["Required_Date"];
                Recruitment.EmployeeName = (int)dr_recruit["pn_employeeID"] + "-" + (string)dr_recruit["Employee_First_Name"]; //Convert.IsDBNull(dr_recruit["Employee_First_Name"]) ? "" : (string)dr_recruit["Employee_First_Name"];
                Recruitment.QualificationName = Convert.IsDBNull(dr_recruit["Qualification"]) ? "" : (string)dr_recruit["Qualification"];
                Recruitment.Skills = Convert.IsDBNull(dr_recruit["Skills"]) ? "" : (string)dr_recruit["Skills"];

                Recruitment.Age = Convert.IsDBNull(dr_recruit["Age"]) ? "" : (string)dr_recruit["Age"];
                Recruitment.Experience = Convert.IsDBNull(dr_recruit["Experience"]) ? "" : (string)dr_recruit["Experience"];
                Recruitment.Status = Convert.IsDBNull(dr_recruit["Status"]) ? "" : (string)dr_recruit["Status"];
                Recruitment.Description = Convert.IsDBNull(dr_recruit["Description"]) ? "" : (string)dr_recruit["Description"];
                Recruitment.Comment = Convert.IsDBNull(dr_recruit["comments"]) ? "" : (string)dr_recruit["comments"];
                RecruitmentList.Add(Recruitment);
            }
            return RecruitmentList;
        }

        public Collection<Be_Recruitment> Re_Jobapprove2(Be_Recruitment r)
        {
            Collection<Be_Recruitment> RecruitmentList = new Collection<Be_Recruitment>();
            _Connection = con.fn_Connection();
            string _SqlString = "set dateformat dmy;select a.*,c.Employee_First_Name, c.pn_employeeID from Recruit_jobrequistion a,Paym_employee c where a.Pn_EmployeeId=c.Pn_EmployeeID  and a.pn_branchID='" + r.BranchID + "' and a.Pn_CompanyID='" + r.CompanyID + "' and a.Requisition_Date='" + r.CurrentDate + "' order by a.Job_code;set dateformat mdy";
            SqlCommand cmd = new SqlCommand(_SqlString, _Connection);
            _Connection.Open();
            SqlDataReader dr_recruit = cmd.ExecuteReader();
            while (dr_recruit.Read())
            {
                Be_Recruitment Recruitment = new Be_Recruitment();
                Recruitment.RequisitionCode = (string)dr_recruit["Job_Code"] + "-" + (string)dr_recruit["Job_Title"]; //Convert.IsDBNull(dr_recruit["Job_code"]) ? "" : (string)dr_recruit["Job_code_Title"];

                Recruitment.DepartmentName = Convert.IsDBNull(dr_recruit["pn_departmentID"]) ? "" : (string)dr_recruit["pn_departmentID"];

                Recruitment.JobType = Convert.IsDBNull(dr_recruit["Job_Type"]) ? "" : (string)dr_recruit["Job_Type"];
                Recruitment.Duration = Convert.IsDBNull(dr_recruit["contract_period"]) ? "" : (string)dr_recruit["contract_period"];
                Recruitment.NoofVacancies = (int)dr_recruit["No_Of_Vacancies"];
                Recruitment.RequisitionDate = Convert.ToDateTime(dr_recruit["Requisition_Date"]);// Convert.IsDBNull(dr_recruit["Requisition_Date"]) ? "" : (DateTime)dr_recruit["Requisition_Date"]; //(DateTime)dr_recruit["Requisition_Date"];
                Recruitment.RequiredDate = Convert.IsDBNull(dr_recruit["Required_Date"]) ? "" : (string)dr_recruit["Required_Date"];
                Recruitment.EmployeeName = (int)dr_recruit["pn_employeeID"] + "-" + (string)dr_recruit["Employee_First_Name"]; //Convert.IsDBNull(dr_recruit["Employee_First_Name"]) ? "" : (string)dr_recruit["Employee_First_Name"];
                Recruitment.QualificationName = Convert.IsDBNull(dr_recruit["Qualification"]) ? "" : (string)dr_recruit["Qualification"];
                Recruitment.Skills = Convert.IsDBNull(dr_recruit["Skills"]) ? "" : (string)dr_recruit["Skills"];

                Recruitment.Age = Convert.IsDBNull(dr_recruit["Age"]) ? "" : (string)dr_recruit["Age"];
                Recruitment.Experience = Convert.IsDBNull(dr_recruit["Experience"]) ? "" : (string)dr_recruit["Experience"];
                Recruitment.Status = Convert.IsDBNull(dr_recruit["Status"]) ? "" : (string)dr_recruit["Status"];
                Recruitment.Description = Convert.IsDBNull(dr_recruit["Description"]) ? "" : (string)dr_recruit["Description"];
                Recruitment.Comment = Convert.IsDBNull(dr_recruit["comments"]) ? "" : (string)dr_recruit["comments"];
                RecruitmentList.Add(Recruitment);
            }
            return RecruitmentList;
        }

        public Collection<Be_Recruitment> Re_JobRequest1(Be_Recruitment r)
        {
            Collection<Be_Recruitment> RecruitmentList = new Collection<Be_Recruitment>();
            _Connection = con.fn_Connection();
            string _SqlString = "select * from recruit_job_queries where pn_branchID='" + r.BranchID + "' and pn_employeeId='"+r.EmployeeID+"' and Pn_CompanyID='" + r.CompanyID + "' order by Job_code";
            SqlCommand cmd = new SqlCommand(_SqlString, _Connection);
            _Connection.Open();
            SqlDataReader dr_recruit = cmd.ExecuteReader();
            while (dr_recruit.Read())
            {
                Be_Recruitment Recruitment = new Be_Recruitment();
                Recruitment.RequisitionCode = (string)dr_recruit["Job_Code"] + "-" + (string)dr_recruit["Job_Title"]; //Convert.IsDBNull(dr_recruit["Job_code"]) ? "" : (string)dr_recruit["Job_code_Title"];
                              
                Recruitment.Queries= Convert.IsDBNull(dr_recruit["Queries"]) ? "" : (string)dr_recruit["Queries"];
               
                RecruitmentList.Add(Recruitment);
            }
            return RecruitmentList;
        }
       
        public string Interview_Schedule(Be_Recruitment I)
        {
            try
            {
                _Connection = con.fn_Connection();
                SqlCommand _ISPSCHEDULE = new SqlCommand("sp_schedule", _Connection);
                _ISPSCHEDULE.CommandType = CommandType.StoredProcedure;
                SqlParameter[] _ISPScheduleparam = new SqlParameter[7];
                _ISPScheduleparam[0] = new SqlParameter("@pn_CompanyID", SqlDbType.Int);
                _ISPScheduleparam[0].Value = I.CompanyID;
                _ISPScheduleparam[1] = new SqlParameter("@pn_BranchID", SqlDbType.Int);
                _ISPScheduleparam[1].Value = I.BranchID;
                //_ISPScheduleparam[2] = new SqlParameter("@pn_EmployeeID", SqlDbType.VarChar);
                //_ISPScheduleparam[2].Value = I.EmployeeName;
                _ISPScheduleparam[2] = new SqlParameter("@Job_code", SqlDbType.VarChar);
                _ISPScheduleparam[2].Value = I.RequisitionCode;
                //_ISPScheduleparam[4] = new SqlParameter("@pn_DepartmentID", SqlDbType.VarChar);
                //_ISPScheduleparam[4].Value = I.DepartmentName;
                _ISPScheduleparam[3] = new SqlParameter("@Candidate_id", SqlDbType.Int);
                _ISPScheduleparam[3].Value = I.ResumeId;
                //_ISPScheduleparam[6] = new SqlParameter("@Current_Round", SqlDbType.VarChar);
                //_ISPScheduleparam[6].Value = I.CurrentRound;
                //_ISPScheduleparam[7] = new SqlParameter("@interview_date", SqlDbType.DateTime);
                //_ISPScheduleparam[7].Value = I.InterviewDate;
                //_ISPScheduleparam[8] = new SqlParameter("@interview_time", SqlDbType.VarChar);
                //_ISPScheduleparam[8].Value = I.InterviewTime;
                _ISPScheduleparam[4] = new SqlParameter("@candidate", SqlDbType.VarChar);
                _ISPScheduleparam[4].Value = I.Resumename;
                _ISPScheduleparam[5] = new SqlParameter("@No_of_rounds", SqlDbType.Int);
                _ISPScheduleparam[5].Value = I.Rounds;
                _ISPScheduleparam[6] = new SqlParameter("@Batch", SqlDbType.VarChar);
                _ISPScheduleparam[6].Value = I.Batch;
                //_ISPScheduleparam[10] = new SqlParameter("@interview_type", SqlDbType.VarChar);
                //_ISPScheduleparam[10].Value = I.InterviewType;
                //_ISPScheduleparam[11] = new SqlParameter("@place", SqlDbType.VarChar);
                //_ISPScheduleparam[11].Value = I.Place;

                for (int i = 0; i < _ISPScheduleparam.Length; i++)
                {

                    _ISPSCHEDULE.Parameters.Add(_ISPScheduleparam[i]);
                }
                _Connection.Open();
                _ISPSCHEDULE.ExecuteNonQuery();
                _Connection.Close();
                return "0";


            }
            catch (Exception e)
            {
                return "1";
            }
        }
        public string Interview_Details(Be_Recruitment I)
        {
            try
            {
                _Connection = con.fn_Connection();
                SqlCommand _ISPINTERVIEWDETAILS = new SqlCommand("sp_Recruit_Interview_Details", _Connection);
                _ISPINTERVIEWDETAILS.CommandType = CommandType.StoredProcedure;
                SqlParameter[] _ISPSInterDetailsparam = new SqlParameter[5];
                _ISPSInterDetailsparam[0] = new SqlParameter("@pn_CompanyID", SqlDbType.Int);
                _ISPSInterDetailsparam[0].Value = I.CompanyID;
                _ISPSInterDetailsparam[1] = new SqlParameter("@pn_BranchID", SqlDbType.Int);
                _ISPSInterDetailsparam[1].Value = I.BranchID;
                _ISPSInterDetailsparam[2] = new SqlParameter("@Job_code", SqlDbType.VarChar);
                _ISPSInterDetailsparam[2].Value = I.RequisitionCode;
                _ISPSInterDetailsparam[3] = new SqlParameter("@Candidate_id", SqlDbType.Int);
                _ISPSInterDetailsparam[3].Value = I.ResumeId;
                _ISPSInterDetailsparam[4] = new SqlParameter("@candidate", SqlDbType.VarChar);
                _ISPSInterDetailsparam[4].Value = I.ApplicantName;
                _ISPSInterDetailsparam[5] = new SqlParameter("@Rounds", SqlDbType.VarChar);
                _ISPSInterDetailsparam[5].Value = I.Rounds;
                _ISPSInterDetailsparam[6] = new SqlParameter("@interview_date", SqlDbType.DateTime);
                _ISPSInterDetailsparam[6].Value = I.InterviewDate;
                _ISPSInterDetailsparam[7] = new SqlParameter("@interview_time", SqlDbType.VarChar);
                _ISPSInterDetailsparam[7].Value = I.InterviewTime;
                _ISPSInterDetailsparam[8] = new SqlParameter("@pn_DepartmentID", SqlDbType.VarChar);
                _ISPSInterDetailsparam[8].Value = I.DepartmentName;
                _ISPSInterDetailsparam[9] = new SqlParameter("@interview_type", SqlDbType.VarChar);
                _ISPSInterDetailsparam[9].Value = I.InterviewType;
                _ISPSInterDetailsparam[10] = new SqlParameter("@pn_EmployeeID", SqlDbType.VarChar);
                _ISPSInterDetailsparam[10].Value = I.EmployeeName;
                _ISPSInterDetailsparam[11] = new SqlParameter("@place", SqlDbType.VarChar);
                _ISPSInterDetailsparam[11].Value = I.Place;

                for (int i = 0; i < _ISPSInterDetailsparam.Length; i++)
                {

                    _ISPINTERVIEWDETAILS.Parameters.Add(_ISPSInterDetailsparam[i]);
                }
                _Connection.Open();
                _ISPINTERVIEWDETAILS.ExecuteNonQuery();
                _Connection.Close();
                return "0";


            }
            catch (Exception e)
            {
                return "1";
            }
        }
        public Collection<Be_Recruitment> Re_Intr_CandidateStatus(Be_Recruitment I)
        {
            Collection<Be_Recruitment> CandidateStatusList = new Collection<Be_Recruitment>();
            _Connection = con.fn_Connection();
            string _Sqlcmd = "select * from recruit_candidate_status where Pn_BranchID='" + I.BranchID + "' and Pn_CompanyID='" + I.CompanyID + "' order by candidate";
            SqlCommand cmd = new SqlCommand(_Sqlcmd, _Connection);
            _Connection.Open();
            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                Be_Recruitment CandidateStatus = new Be_Recruitment();
                CandidateStatus.RequisitionCode = Convert.IsDBNull(rdr["Job_Code"]) ? "" : (string)rdr["Job_code"];
                CandidateStatus.CandidateID = (int)rdr["Candidate_id"];
                CandidateStatus.ApplicantName = Convert.IsDBNull(rdr["Candidate"]) ? "" : (string)rdr["Candidate"];
                CandidateStatus.Batch = Convert.IsDBNull(rdr["Batch"]) ? "" : (string)rdr["Batch"];
                CandidateStatus.Round1 = Convert.IsDBNull(rdr["round1"]) ? "" : (string)rdr["round1"];
                CandidateStatus.Round2 = Convert.IsDBNull(rdr["round2"]) ? "" : (string)rdr["round2"];
                CandidateStatus.Round3 = Convert.IsDBNull(rdr["round3"]) ? "" : (string)rdr["round3"];
                CandidateStatus.Round4 = Convert.IsDBNull(rdr["round4"]) ? "" : (string)rdr["round4"];
                CandidateStatus.Round5 = Convert.IsDBNull(rdr["round5"]) ? "" : (string)rdr["round5"];
                CandidateStatus.FinalResult = Convert.IsDBNull(rdr["Final_result"]) ? "" : (string)rdr["Final_result"];
                CandidateStatus.Comment = Convert.IsDBNull(rdr["Reason"]) ? "" : (string)rdr["Reason"];

                CandidateStatusList.Add(CandidateStatus);
            }
            return CandidateStatusList;
        }

        public Collection<Be_Recruitment> Re_Intr_CandidateStatus1(Be_Recruitment I)
        {
            Collection<Be_Recruitment> CandidateStatusList = new Collection<Be_Recruitment>();
            _Connection = con.fn_Connection();
            string _Sqlcmd = "select * from recruit_candidate_status where Pn_BranchID='" + I.BranchID + "' and Pn_CompanyID='" + I.CompanyID + "' and Job_Code='" + I.RequisitionCode + "' and batch='"+ I.Batch +"' order by candidate";
            SqlCommand cmd = new SqlCommand(_Sqlcmd, _Connection);
            _Connection.Open();
            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                Be_Recruitment CandidateStatus = new Be_Recruitment();
                CandidateStatus.RequisitionCode = Convert.IsDBNull(rdr["Job_Code"]) ? "" : (string)rdr["Job_code"];
                CandidateStatus.CandidateID = (int)rdr["Candidate_id"];
                CandidateStatus.ApplicantName = Convert.IsDBNull(rdr["Candidate"]) ? "" : (string)rdr["Candidate"];
                CandidateStatus.Batch = Convert.IsDBNull(rdr["Batch"]) ? "" : (string)rdr["Batch"];
                CandidateStatus.Round1 = Convert.IsDBNull(rdr["round1"]) ? "" : (string)rdr["round1"];
                CandidateStatus.Round2 = Convert.IsDBNull(rdr["round2"]) ? "" : (string)rdr["round2"];
                CandidateStatus.Round3 = Convert.IsDBNull(rdr["round3"]) ? "" : (string)rdr["round3"];
                CandidateStatus.Round4 = Convert.IsDBNull(rdr["round4"]) ? "" : (string)rdr["round4"];
                CandidateStatus.Round5 = Convert.IsDBNull(rdr["round5"]) ? "" : (string)rdr["round5"];
                CandidateStatus.FinalResult = Convert.IsDBNull(rdr["Final_result"]) ? "" : (string)rdr["Final_result"];
                CandidateStatus.Comment = Convert.IsDBNull(rdr["Reason"]) ? "" : (string)rdr["Reason"];
                CandidateStatusList.Add(CandidateStatus);
            }
            return CandidateStatusList;
        }


        public string CandidateStatus(Be_Recruitment I)
        {
            try
            {
                _Connection = con.fn_Connection();
                SqlCommand _ISSTATUS = new SqlCommand("sp_Recruit_candidate_status", _Connection);
                _ISSTATUS.CommandType = CommandType.StoredProcedure;
                SqlParameter[] _ISPSTATUSparam = new SqlParameter[13];
                _ISPSTATUSparam[0] = new SqlParameter("@pn_CompanyID", SqlDbType.Int);
                _ISPSTATUSparam[0].Value = I.CompanyID;
                _ISPSTATUSparam[1] = new SqlParameter("@pn_BranchID", SqlDbType.Int);
                _ISPSTATUSparam[1].Value = I.BranchID;
                _ISPSTATUSparam[2] = new SqlParameter("@Job_code", SqlDbType.VarChar);
                _ISPSTATUSparam[2].Value = I.RequisitionCode;
                _ISPSTATUSparam[3] = new SqlParameter("@Candidate_id", SqlDbType.Int);
                _ISPSTATUSparam[3].Value = I.CandidateID;
                _ISPSTATUSparam[4] = new SqlParameter("@Candidate", SqlDbType.VarChar);
                _ISPSTATUSparam[4].Value = I.ApplicantName;
                _ISPSTATUSparam[5] = new SqlParameter("@Batch", SqlDbType.VarChar);
                _ISPSTATUSparam[5].Value = I.Batch;
                _ISPSTATUSparam[6] = new SqlParameter("@Round1", SqlDbType.VarChar);
                _ISPSTATUSparam[6].Value = I.Round1;
                _ISPSTATUSparam[7] = new SqlParameter("@Round2", SqlDbType.VarChar);
                _ISPSTATUSparam[7].Value = I.Round2;
                _ISPSTATUSparam[8] = new SqlParameter("@Round3", SqlDbType.VarChar);
                _ISPSTATUSparam[8].Value = I.Round3;
                _ISPSTATUSparam[9] = new SqlParameter("@Round4", SqlDbType.VarChar);
                _ISPSTATUSparam[9].Value = I.Round4;
                _ISPSTATUSparam[10] = new SqlParameter("@Round5", SqlDbType.VarChar);
                _ISPSTATUSparam[10].Value = I.Round5;
                _ISPSTATUSparam[11] = new SqlParameter("@Final_result", SqlDbType.VarChar);
                _ISPSTATUSparam[11].Value = I.FinalResult;
                _ISPSTATUSparam[12] = new SqlParameter("@Reason", SqlDbType.VarChar);
                _ISPSTATUSparam[12].Value = I.Comment;


                for (int i = 0; i < _ISPSTATUSparam.Length; i++)
                {

                    _ISSTATUS.Parameters.Add(_ISPSTATUSparam[i]);
                }
                _Connection.Open();
                _ISSTATUS.ExecuteNonQuery();
                _Connection.Close();
                return "0";


            }
            catch (Exception e)
            {
                return "1";
            }
        }
        
        public string Job_queries(Be_Recruitment r)
        {
            try
            {
                _Connection = con.fn_Connection();
                SqlCommand _ISPSHORTLIST = new SqlCommand("sp_Recruit_Job_queries", _Connection);
                _ISPSHORTLIST.CommandType = CommandType.StoredProcedure;
                SqlParameter[] _ISPShortlistparam = new SqlParameter[8];
                _ISPShortlistparam[0] = new SqlParameter("@pn_CompanyID", SqlDbType.Int);
                _ISPShortlistparam[0].Value = r.CompanyID;
                _ISPShortlistparam[1] = new SqlParameter("@pn_BranchID", SqlDbType.Int);
                _ISPShortlistparam[1].Value = r.BranchID;
                _ISPShortlistparam[2] = new SqlParameter("@Query_Id", SqlDbType.Int);
                _ISPShortlistparam[2].Value = r.Query_id;
                _ISPShortlistparam[3] = new SqlParameter("@Job_code", SqlDbType.VarChar);
                _ISPShortlistparam[3].Value = r.RequisitionCode;
                _ISPShortlistparam[4] = new SqlParameter("@Job_Title", SqlDbType.VarChar);
                _ISPShortlistparam[4].Value = r.JobStatusName;
                _ISPShortlistparam[5] = new SqlParameter("@pn_EmployeeID", SqlDbType.Int);
                _ISPShortlistparam[5].Value = r.EmployeeID;
                _ISPShortlistparam[6] = new SqlParameter("@Query_By", SqlDbType.VarChar);
                _ISPShortlistparam[6].Value = r.QuerBy;
                _ISPShortlistparam[7] = new SqlParameter("@Queries", SqlDbType.VarChar);
                _ISPShortlistparam[7].Value = r.Queries;

                for (int i = 0; i < _ISPShortlistparam.Length; i++)
                {

                    _ISPSHORTLIST.Parameters.Add(_ISPShortlistparam[i]);
                }
                _Connection.Open();
                _ISPSHORTLIST.ExecuteNonQuery();
                _Connection.Close();
                return "0";

            }
            catch (Exception ex)
            {
                return "1";
            }
        }

        public Collection<Be_Recruitment> Re_Intr_Schedule(Be_Recruitment I)
        {
            Collection<Be_Recruitment> ScheduleList = new Collection<Be_Recruitment>();
            _Connection = con.fn_Connection();
            string _Sqlcmd = "select a.*,resume_id from recruit_resume d, Recruit_schedule a where a.candidate_id=d.resume_id and a.Pn_BranchID='" + I.BranchID + "' and a.Pn_CompanyID='" + I.CompanyID + "' order by a.Job_Code";
            SqlCommand cmd = new SqlCommand(_Sqlcmd, _Connection);
            _Connection.Open();
            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                Be_Recruitment Schedule = new Be_Recruitment();
                Schedule.RequisitionCode = Convert.IsDBNull(rdr["Job_Code"]) ? "" : (string)rdr["Job_code"];
                Schedule.ResumeId = (int)rdr["Candidate_id"];
                Schedule.ApplicantName = Convert.IsDBNull(rdr["Candidate"]) ? "" : (string)rdr["Candidate"];               
                ScheduleList.Add(Schedule);
            }
            return ScheduleList;
        }
        public Collection<Be_Recruitment> sortbytype(Be_Recruitment r)
        {
            Collection<Be_Recruitment> RecruitmentList = new Collection<Be_Recruitment>();
            _Connection = con.fn_Connection();
            string _SqlString = "set dateformat dmy;select a.*,c.Employee_First_Name, c.pn_employeeID from Recruit_jobrequistion a,Paym_employee c where a.Pn_EmployeeId=c.Pn_EmployeeID  and a.pn_branchID='" + r.BranchID + "' and a.Pn_CompanyID='" + r.CompanyID + "' and Job_Type='" + r.DepartmentName + "' order by a.Job_code;set dateformat mdy";
            SqlCommand cmd = new SqlCommand(_SqlString, _Connection);
            _Connection.Open();
            SqlDataReader dr_recruit = cmd.ExecuteReader();
            while (dr_recruit.Read())
            {
                Be_Recruitment Recruitment = new Be_Recruitment();
                Recruitment.RequisitionCode = (string)dr_recruit["Job_Code"] + "-" + (string)dr_recruit["Job_Title"]; //Convert.IsDBNull(dr_recruit["Job_code"]) ? "" : (string)dr_recruit["Job_code_Title"];

                Recruitment.DepartmentName = Convert.IsDBNull(dr_recruit["pn_departmentID"]) ? "" : (string)dr_recruit["pn_departmentID"];

                Recruitment.JobType = Convert.IsDBNull(dr_recruit["Job_Type"]) ? "" : (string)dr_recruit["Job_Type"];
                Recruitment.Duration = Convert.IsDBNull(dr_recruit["contract_period"]) ? "" : (string)dr_recruit["contract_period"];
                Recruitment.NoofVacancies = (int)dr_recruit["No_Of_Vacancies"];
                Recruitment.RequisitionDate = Convert.ToDateTime(dr_recruit["Requisition_Date"]);// Convert.IsDBNull(dr_recruit["Requisition_Date"]) ? "" : (DateTime)dr_recruit["Requisition_Date"]; //(DateTime)dr_recruit["Requisition_Date"];
                Recruitment.RequiredDate = Convert.IsDBNull(dr_recruit["Required_Date"]) ? "" : (string)dr_recruit["Required_Date"];
                Recruitment.EmployeeName = (int)dr_recruit["pn_employeeID"] + "-" + (string)dr_recruit["Employee_First_Name"]; //Convert.IsDBNull(dr_recruit["Employee_First_Name"]) ? "" : (string)dr_recruit["Employee_First_Name"];
                Recruitment.QualificationName = Convert.IsDBNull(dr_recruit["Qualification"]) ? "" : (string)dr_recruit["Qualification"];
                Recruitment.Skills = Convert.IsDBNull(dr_recruit["Skills"]) ? "" : (string)dr_recruit["Skills"];

                Recruitment.Age = Convert.IsDBNull(dr_recruit["Age"]) ? "" : (string)dr_recruit["Age"];
                Recruitment.Experience = Convert.IsDBNull(dr_recruit["Experience"]) ? "" : (string)dr_recruit["Experience"];
                Recruitment.Status = Convert.IsDBNull(dr_recruit["Status"]) ? "" : (string)dr_recruit["Status"];
                Recruitment.Description = Convert.IsDBNull(dr_recruit["Description"]) ? "" : (string)dr_recruit["Description"];
                Recruitment.Comment = Convert.IsDBNull(dr_recruit["comments"]) ? "" : (string)dr_recruit["comments"];
                RecruitmentList.Add(Recruitment);
            }
            return RecruitmentList;
        }

        public Collection<Be_Recruitment> sortbydept(Be_Recruitment r)
        {
            Collection<Be_Recruitment> RecruitmentList = new Collection<Be_Recruitment>();
            _Connection = con.fn_Connection();
            string _SqlString = "set dateformat dmy;select a.*,c.Employee_First_Name, c.pn_employeeID from Recruit_jobrequistion a,Paym_employee c where a.Pn_EmployeeId=c.Pn_EmployeeID  and a.pn_branchID='" + r.BranchID + "' and a.Pn_CompanyID='" + r.CompanyID + "' and pn_departmentid='" + r.DepartmentName + "' order by a.Job_code;set dateformat mdy";
            SqlCommand cmd = new SqlCommand(_SqlString, _Connection);
            _Connection.Open();
            SqlDataReader dr_recruit = cmd.ExecuteReader();
            while (dr_recruit.Read())
            {
                Be_Recruitment Recruitment = new Be_Recruitment();
                Recruitment.RequisitionCode = (string)dr_recruit["Job_Code"] + "-" + (string)dr_recruit["Job_Title"]; //Convert.IsDBNull(dr_recruit["Job_code"]) ? "" : (string)dr_recruit["Job_code_Title"];

                Recruitment.DepartmentName = Convert.IsDBNull(dr_recruit["pn_departmentID"]) ? "" : (string)dr_recruit["pn_departmentID"];

                Recruitment.JobType = Convert.IsDBNull(dr_recruit["Job_Type"]) ? "" : (string)dr_recruit["Job_Type"];
                Recruitment.Duration = Convert.IsDBNull(dr_recruit["contract_period"]) ? "" : (string)dr_recruit["contract_period"];
                Recruitment.NoofVacancies = (int)dr_recruit["No_Of_Vacancies"];
                Recruitment.RequisitionDate = Convert.ToDateTime(dr_recruit["Requisition_Date"]);// Convert.IsDBNull(dr_recruit["Requisition_Date"]) ? "" : (DateTime)dr_recruit["Requisition_Date"]; //(DateTime)dr_recruit["Requisition_Date"];
                Recruitment.RequiredDate = Convert.IsDBNull(dr_recruit["Required_Date"]) ? "" : (string)dr_recruit["Required_Date"];
                Recruitment.EmployeeName = (int)dr_recruit["pn_employeeID"] + "-" + (string)dr_recruit["Employee_First_Name"]; //Convert.IsDBNull(dr_recruit["Employee_First_Name"]) ? "" : (string)dr_recruit["Employee_First_Name"];
                Recruitment.QualificationName = Convert.IsDBNull(dr_recruit["Qualification"]) ? "" : (string)dr_recruit["Qualification"];
                Recruitment.Skills = Convert.IsDBNull(dr_recruit["Skills"]) ? "" : (string)dr_recruit["Skills"];

                Recruitment.Age = Convert.IsDBNull(dr_recruit["Age"]) ? "" : (string)dr_recruit["Age"];
                Recruitment.Experience = Convert.IsDBNull(dr_recruit["Experience"]) ? "" : (string)dr_recruit["Experience"];
                Recruitment.Status = Convert.IsDBNull(dr_recruit["Status"]) ? "" : (string)dr_recruit["Status"];
                Recruitment.Description = Convert.IsDBNull(dr_recruit["Description"]) ? "" : (string)dr_recruit["Description"];
                Recruitment.Comment = Convert.IsDBNull(dr_recruit["comments"]) ? "" : (string)dr_recruit["comments"];
                RecruitmentList.Add(Recruitment);
            }
            return RecruitmentList;
        }

        public Collection<Be_Recruitment> sortbyemployee(Be_Recruitment r)
        {
            Collection<Be_Recruitment> RecruitmentList = new Collection<Be_Recruitment>();
            _Connection = con.fn_Connection();
            string _SqlString = "set dateformat dmy;select a.*,c.Employee_First_Name, c.pn_employeeID from Recruit_jobrequistion a,Paym_employee c where a.Pn_EmployeeId=c.Pn_EmployeeID  and a.pn_branchID='" + r.BranchID + "' and a.Pn_CompanyID='" + r.CompanyID + "' and c.pn_employeeID='" + r.EmployeeID + "' order by a.Job_code;set dateformat mdy";
            SqlCommand cmd = new SqlCommand(_SqlString, _Connection);
            _Connection.Open();
            SqlDataReader dr_recruit = cmd.ExecuteReader();
            while (dr_recruit.Read())
            {
                Be_Recruitment Recruitment = new Be_Recruitment();
                Recruitment.RequisitionCode = (string)dr_recruit["Job_Code"] + "-" + (string)dr_recruit["Job_Title"]; //Convert.IsDBNull(dr_recruit["Job_code"]) ? "" : (string)dr_recruit["Job_code_Title"];

                Recruitment.DepartmentName = Convert.IsDBNull(dr_recruit["pn_departmentID"]) ? "" : (string)dr_recruit["pn_departmentID"];
                Recruitment.JobType = Convert.IsDBNull(dr_recruit["Job_Type"]) ? "" : (string)dr_recruit["Job_Type"];
                Recruitment.Duration = Convert.IsDBNull(dr_recruit["contract_period"]) ? "" : (string)dr_recruit["contract_period"];
                Recruitment.NoofVacancies = (int)dr_recruit["No_Of_Vacancies"];
                Recruitment.RequisitionDate = Convert.ToDateTime(dr_recruit["Requisition_Date"]);// Convert.IsDBNull(dr_recruit["Requisition_Date"]) ? "" : (DateTime)dr_recruit["Requisition_Date"]; //(DateTime)dr_recruit["Requisition_Date"];
                Recruitment.RequiredDate = Convert.IsDBNull(dr_recruit["Required_Date"]) ? "" : (string)dr_recruit["Required_Date"];
                Recruitment.EmployeeName = (int)dr_recruit["pn_employeeID"] + "-" + (string)dr_recruit["Employee_First_Name"]; //Convert.IsDBNull(dr_recruit["Employee_First_Name"]) ? "" : (string)dr_recruit["Employee_First_Name"];
                Recruitment.QualificationName = Convert.IsDBNull(dr_recruit["Qualification"]) ? "" : (string)dr_recruit["Qualification"];
                Recruitment.Skills = Convert.IsDBNull(dr_recruit["Skills"]) ? "" : (string)dr_recruit["Skills"];

                Recruitment.Age = Convert.IsDBNull(dr_recruit["Age"]) ? "" : (string)dr_recruit["Age"];
                Recruitment.Experience = Convert.IsDBNull(dr_recruit["Experience"]) ? "" : (string)dr_recruit["Experience"];
                Recruitment.Status = Convert.IsDBNull(dr_recruit["Status"]) ? "" : (string)dr_recruit["Status"];
                Recruitment.Description = Convert.IsDBNull(dr_recruit["Description"]) ? "" : (string)dr_recruit["Description"];
                Recruitment.Comment = Convert.IsDBNull(dr_recruit["comments"]) ? "" : (string)dr_recruit["comments"];
                RecruitmentList.Add(Recruitment);
            }
            return RecruitmentList;
        }
        public Collection<Be_Recruitment> sortbyreq(Be_Recruitment r)
        {
            Collection<Be_Recruitment> RecruitmentList = new Collection<Be_Recruitment>();
            _Connection = con.fn_Connection();
            string _SqlString = "set dateformat dmy;select a.*,c.Employee_First_Name, c.pn_employeeID from Recruit_jobrequistion a,Paym_employee c where a.Pn_EmployeeId=c.Pn_EmployeeID  and a.pn_branchID='" + r.BranchID + "' and a.Pn_CompanyID='" + r.CompanyID + "' and Requisition_Date='" + r.RequisitionDate + "' order by a.Job_code;set dateformat mdy";
            SqlCommand cmd = new SqlCommand(_SqlString, _Connection);
            _Connection.Open();
            SqlDataReader dr_recruit = cmd.ExecuteReader();
            while (dr_recruit.Read())
            {
                Be_Recruitment Recruitment = new Be_Recruitment();
                Recruitment.RequisitionCode = (string)dr_recruit["Job_Code"] + "-" + (string)dr_recruit["Job_Title"]; //Convert.IsDBNull(dr_recruit["Job_code"]) ? "" : (string)dr_recruit["Job_code_Title"];

                Recruitment.DepartmentName = Convert.IsDBNull(dr_recruit["pn_departmentID"]) ? "" : (string)dr_recruit["pn_departmentID"];

                Recruitment.JobType = Convert.IsDBNull(dr_recruit["Job_Type"]) ? "" : (string)dr_recruit["Job_Type"];
                Recruitment.Duration = Convert.IsDBNull(dr_recruit["contract_period"]) ? "" : (string)dr_recruit["contract_period"];
                Recruitment.NoofVacancies = (int)dr_recruit["No_Of_Vacancies"];
                Recruitment.RequisitionDate = Convert.ToDateTime(dr_recruit["Requisition_Date"]);// Convert.IsDBNull(dr_recruit["Requisition_Date"]) ? "" : (DateTime)dr_recruit["Requisition_Date"]; //(DateTime)dr_recruit["Requisition_Date"];
                Recruitment.RequiredDate = Convert.IsDBNull(dr_recruit["Required_Date"]) ? "" : (string)dr_recruit["Required_Date"];
                Recruitment.EmployeeName = (int)dr_recruit["pn_employeeID"] + "-" + (string)dr_recruit["Employee_First_Name"]; //Convert.IsDBNull(dr_recruit["Employee_First_Name"]) ? "" : (string)dr_recruit["Employee_First_Name"];
                Recruitment.QualificationName = Convert.IsDBNull(dr_recruit["Qualification"]) ? "" : (string)dr_recruit["Qualification"];
                Recruitment.Skills = Convert.IsDBNull(dr_recruit["Skills"]) ? "" : (string)dr_recruit["Skills"];

                Recruitment.Age = Convert.IsDBNull(dr_recruit["Age"]) ? "" : (string)dr_recruit["Age"];
                Recruitment.Experience = Convert.IsDBNull(dr_recruit["Experience"]) ? "" : (string)dr_recruit["Experience"];
                Recruitment.Status = Convert.IsDBNull(dr_recruit["Status"]) ? "" : (string)dr_recruit["Status"];
                Recruitment.Description = Convert.IsDBNull(dr_recruit["Description"]) ? "" : (string)dr_recruit["Description"];
                Recruitment.Comment = Convert.IsDBNull(dr_recruit["comments"]) ? "" : (string)dr_recruit["comments"];
                RecruitmentList.Add(Recruitment);
            }
            return RecruitmentList;
        }
        public Collection<Be_Recruitment> sortbyrdate(Be_Recruitment r)
        {
            Collection<Be_Recruitment> RecruitmentList = new Collection<Be_Recruitment>();
            _Connection = con.fn_Connection();
            string _SqlString = "set dateformat dmy;select a.*,c.Employee_First_Name, c.pn_employeeID from Recruit_jobrequistion a,Paym_employee c where a.Pn_EmployeeId=c.Pn_EmployeeID  and a.pn_branchID='" + r.BranchID + "' and a.Pn_CompanyID='" + r.CompanyID + "' and Required_Date='" + r.RequisitionCode + "' order by a.Job_code;set dateformat mdy";
            SqlCommand cmd = new SqlCommand(_SqlString, _Connection);
            _Connection.Open();
            SqlDataReader dr_recruit = cmd.ExecuteReader();
            while (dr_recruit.Read())
            {
                Be_Recruitment Recruitment = new Be_Recruitment();
                Recruitment.RequisitionCode = (string)dr_recruit["Job_Code"] + "-" + (string)dr_recruit["Job_Title"]; //Convert.IsDBNull(dr_recruit["Job_code"]) ? "" : (string)dr_recruit["Job_code_Title"];

                Recruitment.DepartmentName = Convert.IsDBNull(dr_recruit["pn_departmentID"]) ? "" : (string)dr_recruit["pn_departmentID"];

                Recruitment.JobType = Convert.IsDBNull(dr_recruit["Job_Type"]) ? "" : (string)dr_recruit["Job_Type"];
                Recruitment.Duration = Convert.IsDBNull(dr_recruit["contract_period"]) ? "" : (string)dr_recruit["contract_period"];
                Recruitment.NoofVacancies = (int)dr_recruit["No_Of_Vacancies"];
                Recruitment.RequisitionDate = Convert.ToDateTime(dr_recruit["Requisition_Date"]);// Convert.IsDBNull(dr_recruit["Requisition_Date"]) ? "" : (DateTime)dr_recruit["Requisition_Date"]; //(DateTime)dr_recruit["Requisition_Date"];
                Recruitment.RequiredDate = Convert.IsDBNull(dr_recruit["Required_Date"]) ? "" : (string)dr_recruit["Required_Date"];
                Recruitment.EmployeeName = (int)dr_recruit["pn_employeeID"] + "-" + (string)dr_recruit["Employee_First_Name"]; //Convert.IsDBNull(dr_recruit["Employee_First_Name"]) ? "" : (string)dr_recruit["Employee_First_Name"];
                Recruitment.QualificationName = Convert.IsDBNull(dr_recruit["Qualification"]) ? "" : (string)dr_recruit["Qualification"];
                Recruitment.Skills = Convert.IsDBNull(dr_recruit["Skills"]) ? "" : (string)dr_recruit["Skills"];

                Recruitment.Age = Convert.IsDBNull(dr_recruit["Age"]) ? "" : (string)dr_recruit["Age"];
                Recruitment.Experience = Convert.IsDBNull(dr_recruit["Experience"]) ? "" : (string)dr_recruit["Experience"];
                Recruitment.Status = Convert.IsDBNull(dr_recruit["Status"]) ? "" : (string)dr_recruit["Status"];
                Recruitment.Description = Convert.IsDBNull(dr_recruit["Description"]) ? "" : (string)dr_recruit["Description"];
                Recruitment.Comment = Convert.IsDBNull(dr_recruit["comments"]) ? "" : (string)dr_recruit["comments"];
                RecruitmentList.Add(Recruitment);
            }
            return RecruitmentList;
        }
        public Collection<Be_Recruitment> fn_getEmployeeList(int e)
        {
            Collection<Be_Recruitment> DepartmentList = new Collection<Be_Recruitment>();
            _Connection = Con.fn_Connection();
            string _SqlString = "Select distinct(b.pn_EmployeeID),a.EmployeeCode,a.Employee_First_Name from Recruit_jobrequistion b, paym_employee a where a.pn_CompanyID=b.pn_CompanyID and a.pn_BranchID=b.pn_BranchID and a.pn_EmployeeID=b.pn_EmployeeID and b.pn_BranchID=" + e + "";
            SqlCommand _SSDepartment = new SqlCommand(_SqlString, _Connection);
            _Connection.Open();
            SqlDataReader dr_Department = _SSDepartment.ExecuteReader();
            while (dr_Department.Read())
            {
                Be_Recruitment employee = new Be_Recruitment();
                employee.RequisitionCode = (string)dr_Department["pn_EmployeeID"];
                employee.EmployeeName = (string)dr_Department["pn_EmployeeID"] + "-" + (string)dr_Department["Employee_First_Name"];
                //employee.FirstName = (string)dr_Department["Employee_First_Name"];
                DepartmentList.Add(employee);
            }
            return DepartmentList;
        }
        public Collection<Be_Recruitment> fn_getDepartmentList(int e)
        {
            Collection<Be_Recruitment> DepartmentList = new Collection<Be_Recruitment>();
            _Connection = Con.fn_Connection();
            string _SqlString = "Select distinct(pn_DepartmentID) from Recruit_jobrequistion where pn_BranchID=" + e + "";
            SqlCommand _SSDepartment = new SqlCommand(_SqlString, _Connection);
            _Connection.Open();
            SqlDataReader dr_Department = _SSDepartment.ExecuteReader();
            while (dr_Department.Read())
            {
                Be_Recruitment employee = new Be_Recruitment();
                employee.DepartmentName = (string)dr_Department["pn_DepartmentID"];
                //employee.FirstName = (string)dr_Department["Employee_First_Name"];
                DepartmentList.Add(employee);
            }
            return DepartmentList;
        }
        public Collection<Be_Recruitment> fn_getJobType(int e)
        {
            Collection<Be_Recruitment> DepartmentList = new Collection<Be_Recruitment>();
            _Connection = Con.fn_Connection();
            string _SqlString = "select distinct(Job_type) from Recruit_jobrequistion where pn_BranchID='" + e + "'";
            SqlCommand _SSDepartment = new SqlCommand(_SqlString, _Connection);
            _Connection.Open();
            SqlDataReader dr_Department = _SSDepartment.ExecuteReader();
            while (dr_Department.Read())
            {
                Be_Recruitment employee = new Be_Recruitment();
                //employee.DepartmentId = (int)dr_Department["pn_DepartmentID"];
                employee.JobType = Convert.IsDBNull(dr_Department["Job_type"]) ? "" : (string)dr_Department["Job_type"];
                DepartmentList.Add(employee);
            }
            return DepartmentList;
        }
        public Collection<Be_Recruitment> fn_getReqDate(int e)
        {
            Collection<Be_Recruitment> DepartmentList = new Collection<Be_Recruitment>();
            _Connection = Con.fn_Connection();
            string _SqlString = "select distinct(Requisition_Date) from Recruit_jobrequistion where pn_BranchID='" + e + "'";
            SqlCommand _SSDepartment = new SqlCommand(_SqlString, _Connection);
            _Connection.Open();
            SqlDataReader dr_Department = _SSDepartment.ExecuteReader();
            while (dr_Department.Read())
            {
                Be_Recruitment employee = new Be_Recruitment();
                //employee.DepartmentId = (int)dr_Department["pn_DepartmentID"];
                employee.RequisitionDate = Convert.ToDateTime(dr_Department["Requisition_Date"]);
                DepartmentList.Add(employee);
            }
            return DepartmentList;
        }

        public Collection<Be_Recruitment> fn_getDate(int e)
        {
            Collection<Be_Recruitment> DepartmentList = new Collection<Be_Recruitment>();
            _Connection = Con.fn_Connection();
            string _SqlString = "select distinct(Required_Date) from Recruit_jobrequistion where pn_BranchID='" + e + "'";
            SqlCommand _SSDepartment = new SqlCommand(_SqlString, _Connection);
            _Connection.Open();
            SqlDataReader dr_Department = _SSDepartment.ExecuteReader();
            while (dr_Department.Read())
            {
                Be_Recruitment employee = new Be_Recruitment();
                //employee.DepartmentId = (int)dr_Department["pn_DepartmentID"];
                employee.RequisitionDate = Convert.ToDateTime(dr_Department["Required_Date"]);
                DepartmentList.Add(employee);
            }
            return DepartmentList;
        }

        public Collection<Be_Recruitment> fn_getreqDate(int e)
        {
            Collection<Be_Recruitment> DepartmentList = new Collection<Be_Recruitment>();
            _Connection = Con.fn_Connection();
            string _SqlString = "select distinct(Required_Date) from Recruit_jobrequistion where pn_BranchID='" + e + "'";
            SqlCommand _SSDepartment = new SqlCommand(_SqlString, _Connection);
            _Connection.Open();
            SqlDataReader dr_Department = _SSDepartment.ExecuteReader();
            while (dr_Department.Read())
            {
                Be_Recruitment employee = new Be_Recruitment();
                //employee.DepartmentId = (int)dr_Department["pn_DepartmentID"];
                employee.RequiredDate = Convert.IsDBNull(dr_Department["Required_Date"]) ? "" : (string)dr_Department["Required_Date"];
                DepartmentList.Add(employee);
            }
            return DepartmentList;
        }
        public Collection<Be_Recruitment> fn_getcandidatelist(Be_Recruitment r)
        {
            Collection<Be_Recruitment> CandidateList = new Collection<Be_Recruitment>();
            _Connection = Con.fn_Connection();//order by Employee_First_Name asc
            SqlCommand _Course = new SqlCommand("select * from recruit_resume where pn_CompanyID=" + r.CompanyID + " and pn_BranchID =" + r.BranchID + " and job_code="+r.RequisitionCode+" and status='Not Scheduled' order by Job_Code,resume_id", _Connection);
            _Connection.Open();
            SqlDataReader dr = _Course.ExecuteReader();
            while (dr.Read())
            {
                Be_Recruitment recruitment = new Be_Recruitment();
                recruitment.ResumeId = (int)dr["Resume_Id"];
                recruitment.RequisitionCode = Convert.IsDBNull(dr["job_code"]) ? "" : (string)dr["job_code"];
               recruitment.CandidateName = (int)dr["Resume_Id"] + " - " + (string)dr["candidate_name"];
               recruitment.ApplicantName= Convert.IsDBNull(dr["Candidate_name"]) ? "" : (string)dr["Candidate_name"];
                CandidateList.Add(recruitment);
            }
            return CandidateList;
        }
        public Collection<Be_Recruitment> fn_getcandidatelist1(Be_Recruitment r)
        {
            _Connection = Con.fn_Connection();
            _Connection.Open();
            Collection<Be_Recruitment> CandidateList = new Collection<Be_Recruitment>();
            //order by Employee_First_Name asc
            SqlCommand cmd = new SqlCommand("select count(*) from recruit_candidate_status", _Connection);
           
                SqlCommand _Course = new SqlCommand("select * from recruit_resume where pn_CompanyID=" + r.CompanyID + " and pn_BranchID =" + r.BranchID + " and job_code=" + r.RequisitionCode + " and final_result='On Process'  order by Job_Code", _Connection);

                SqlDataReader dr2 = _Course.ExecuteReader();
                while (dr2.Read())
                {
                    Be_Recruitment recruitment = new Be_Recruitment();
                    recruitment.ResumeId = (int)dr2["Resume_Id"];
                    recruitment.RequisitionCode = Convert.IsDBNull(dr2["job_code"]) ? "" : (string)dr2["job_code"];
                    recruitment.CandidateName = (int)dr2["Resume_Id"] + " - " + (string)dr2["candidate_name"];
                    recruitment.ApplicantName = Convert.IsDBNull(dr2["Candidate_name"]) ? "" : (string)dr2["Candidate_name"];
                    CandidateList.Add(recruitment);
                }
                return CandidateList;
                dr2.Close();

                _Connection.Close();
    

            }

        public void Recruitment_Qualification(Be_Recruitment r,ListBox q)
        {

            _Connection = Con.fn_Connection();                     

 //Qualification Procedure

            for (int i = 0; i < q.Items.Count; i++)
            {
                if (q.Items[i].Selected)
                {


                    SqlCommand _ISPRecruitment_Qualification = new SqlCommand("sp_hrmt_Requisition_Qualification", _Connection);
                    _ISPRecruitment_Qualification.CommandType = CommandType.StoredProcedure;
                    SqlParameter[] _ISPRecruitment_QualificationParam = new SqlParameter[4];



                    _ISPRecruitment_QualificationParam[0] = new SqlParameter("@pn_CompanyID", SqlDbType.Int);
                    _ISPRecruitment_QualificationParam[0].Value = r.CompanyID;
                    _ISPRecruitment_QualificationParam[1] = new SqlParameter("@pn_BranchID", SqlDbType.Int);
                    _ISPRecruitment_QualificationParam[1].Value = r.BranchID;
                    _ISPRecruitment_QualificationParam[2] = new SqlParameter("@pn_RequisitionNo", SqlDbType.Int);
                    _ISPRecruitment_QualificationParam[2].Value = r.RequisitionNo;
                    _ISPRecruitment_QualificationParam[3] = new SqlParameter("@pn_QualificationCode", SqlDbType.Int);

                    _ISPRecruitment_QualificationParam[3].Value = Convert.ToInt32(q.Items[i].Value);

                    //_ISPRecruitment_QualificationParam[2].Value = Convert.ToInt32(q.SelectedItem.Value);

                    for (int j = 0; j < _ISPRecruitment_QualificationParam.Length; j++)
                    {

                        _ISPRecruitment_Qualification.Parameters.Add(_ISPRecruitment_QualificationParam[j]);

                    }

                    _Connection.Open();

                    _ISPRecruitment_Qualification.ExecuteNonQuery();

                    _Connection.Close();

                  
                }

            }

        }
        public void Recruitment_Skills(Be_Recruitment r, ListBox ss)
        {

            _Connection = Con.fn_Connection();

         
            for (int i = 0; i < ss.Items.Count; i++)
            {
                if (ss.Items[i].Selected)
                {

                    SqlCommand _ISPRecruitment_SkillSet = new SqlCommand("sp_hrmt_Requisition_SkillSet", _Connection);
                    _ISPRecruitment_SkillSet.CommandType = CommandType.StoredProcedure;
                    SqlParameter[] _ISPRecruitment_SkillSetParam = new SqlParameter[4];



                    _ISPRecruitment_SkillSetParam[0] = new SqlParameter("@pn_CompanyID", SqlDbType.Int);
                    _ISPRecruitment_SkillSetParam[0].Value = r.CompanyID;
                    _ISPRecruitment_SkillSetParam[1] = new SqlParameter("@pn_BranchID", SqlDbType.Int);
                    _ISPRecruitment_SkillSetParam[1].Value = r.BranchID;
                    _ISPRecruitment_SkillSetParam[2] = new SqlParameter("@pn_RequisitionNo", SqlDbType.Int);
                    _ISPRecruitment_SkillSetParam[2].Value = r.RequisitionNo;
                    _ISPRecruitment_SkillSetParam[3] = new SqlParameter("@pn_SkillSetCode", SqlDbType.Int);

                    _ISPRecruitment_SkillSetParam[3].Value = Convert.ToInt32(ss.Items[i].Value);

                    for (int j = 0; j < _ISPRecruitment_SkillSetParam.Length; j++)
                    {

                        _ISPRecruitment_SkillSet.Parameters.Add(_ISPRecruitment_SkillSetParam[j]);

                    }

                    _Connection.Open();

                    _ISPRecruitment_SkillSet.ExecuteNonQuery();

                    _Connection.Close();



                }

            }
            

        }       

        public Collection<Be_Recruitment> fn_getRequisition_No(Be_Recruitment rsc)
        {
            Collection<Be_Recruitment> AfterRequisitionNoSelectList = new Collection<Be_Recruitment>();
            _Connection = Con.fn_Connection();
            SqlCommand _AfterRequisitionNoSelect = new SqlCommand("select pn_RequisitionNo from hrmt_Requisition where pn_CompanyID=" + rsc.CompanyID + "and v_RequisitionCode='" + rsc.RequisitionCode + "'", _Connection);
            _Connection.Open();
            SqlDataReader dr = _AfterRequisitionNoSelect.ExecuteReader();
           
            while (dr.Read())
            {

                Be_Recruitment afterRequisitionNoSelect = new Be_Recruitment();

                afterRequisitionNoSelect.RequisitionNo = (int)dr["pn_RequisitionNo"];               

                AfterRequisitionNoSelectList.Add(afterRequisitionNoSelect);
            }
            
            return AfterRequisitionNoSelectList;


        }
        public Collection<Be_Recruitment> fn_getRequisition_Qual(int rq)
        {
            Collection<Be_Recruitment> AfterRequisitionNoSelectList = new Collection<Be_Recruitment>();
            _Connection = Con.fn_Connection();
            SqlCommand _AfterRequisitionNoSelect = new SqlCommand("select * from hrmt_RequisitionQualification where pn_RequisitionNo=" + rq + "", _Connection);
            _Connection.Open();
            SqlDataReader dr = _AfterRequisitionNoSelect.ExecuteReader();

            while (dr.Read())
            {

                Be_Recruitment afterRequisitionNoSelect = new Be_Recruitment();

                afterRequisitionNoSelect.RequisitionNo = (int)dr["pn_RequisitionNo"];

                AfterRequisitionNoSelectList.Add(afterRequisitionNoSelect);
            }

            return AfterRequisitionNoSelectList;


        }
        public Collection<Be_Recruitment> fn_getRequisition_skills(int rs)
        {
            Collection<Be_Recruitment> AfterRequisitionNoSelectList = new Collection<Be_Recruitment>();
            _Connection = Con.fn_Connection();
            SqlCommand _AfterRequisitionNoSelect = new SqlCommand("select * from hrmt_RequiredSkills where pn_RequisitionNo=" + rs + "", _Connection);
            _Connection.Open();
            SqlDataReader dr = _AfterRequisitionNoSelect.ExecuteReader();

            while (dr.Read())
            {

                Be_Recruitment afterRequisitionNoSelect = new Be_Recruitment();

                afterRequisitionNoSelect.RequisitionNo = (int)dr["pn_RequisitionNo"];

                AfterRequisitionNoSelectList.Add(afterRequisitionNoSelect);
            }

            return AfterRequisitionNoSelectList;


        }
        //Resume Sort Listing      
        public DataSet fn_get_Comparisition(Be_Recruitment com)
        {


            string f_qualification, f_skills, final;

            _Connection = Con.fn_Connection();

            //SqlCommand _Candidate = new SqlCommand("select * from hrmm_CandidateProfile where c_RecruitSource='" + paramsource + "' and n_TotalExperience between '" + r_expmin + "' and '" + r_expmax + "' and (select datediff(yy,d_DOB,getdate()) between '"+r_agemin+"' and '"+r_agemax+"' and '"+c_candidateskills+"'='"+r_skillset+"' and '"+c_CandidateEducation+"'='"+r_qualification+"' ", _Connection);
            //SqlCommand _Candidate = new SqlCommand("select * from hrmm_CandidateProfile where c_RecruitSource='" + paramsource + "' and n_TotalExperience between '" + r_expmin + "' and '" + r_expmax + "' and datediff(yy,d_DOB,getdate()) between '" + r_agemin + "' and '" + r_agemax + "'", _Connection);


            f_qualification = fn_get_Comparisition_Qualification(com);

            f_skills = fn_get_Comparisition_SkillS(com);


            final = "(" + f_skills + " and pn_CandidateID in(" + f_qualification + "))";





            string _Comparisition = "select pn_CandidateID,v_CandidateName from hrmm_CandidateProfile where pn_CompanyID=" + com.CompanyID + " and c_RecruitSource='" + com.c + "' and n_TotalExperience between '" + r_expmin + "' and '" + r_expmax + "' and datediff(yy,v_DOB,getdate()) between '" + r_agemin + "' and '" + r_agemax + "' and pn_CandidateID in " + final;



            _Connection.Open();


            SqlDataAdapter _Ad_Comparisition = new SqlDataAdapter(_Comparisition, _Connection);

            DataSet _Ds_Comparisition = new DataSet();

            _Ad_Comparisition.Fill(_Ds_Comparisition, "hrmm_CandidateProfile");


            //g.DataSource = _Ds_Comparisition;

            //g.DataBind();


            _Connection.Close();



            return _Ds_Comparisition;



        }
        public string fn_get_Comparisition_Qualification(Be_Recruitment com_qal)
        {

            int c_i, c_Q_rows;

            int[] A_Comparisition_Qualification = new int[10];

            string c_qualification;


            _Connection = Con.fn_Connection();

            //SqlCommand _s_Qualification = new SqlCommand("select n_QualificationCode from hrmt_RequisitionQualification where fn_RequisitionNo='" + c_rno + "'");

            string _s_Qualification = "select pn_QualificationCode from hrmt_RequisitionQualification where pn_CompanyID=" + com_qal.CompanyID + " and pn_RequisitionNo='" + com_qal.RequisitionNo + "'";

            _Connection.Open();

            SqlDataAdapter _Ad_s_Qualification = new SqlDataAdapter(_s_Qualification, _Connection);

            DataSet _Ds_s_Qualification = new DataSet();


            _Ad_s_Qualification.Fill(_Ds_s_Qualification, "hrmt_RequisitionQualification");

            _Connection.Close();

            c_Q_rows = _Ds_s_Qualification.Tables[0].Rows.Count;




            string _Compare_Qualification = "select pn_CandidateID from hrmm_CandidateEducation where pn_CompanyID=" + com_qal.CompanyID + " and pn_CourseID in(";

            for (c_i = 0; c_i < c_Q_rows; c_i++)
            {

                c_qualification = Convert.ToString(_Ds_s_Qualification.Tables[0].Rows[c_i][0]);

                if (c_i != c_Q_rows - 1)
                {

                    _Compare_Qualification = _Compare_Qualification + c_qualification + ",";

                    continue;
                }
                else
                {

                    _Compare_Qualification = _Compare_Qualification + c_qualification;

                    continue;

                }


            }



            _Compare_Qualification = _Compare_Qualification + ")";





            return _Compare_Qualification;


        }
        public string fn_get_Comparisition_SkillS(Be_Recruitment com_sk)
        {

            int c1_i, c1_Q_rows;

            //int[] A_Comparisition_SkillS = new int[10];

            string c1_Skills;


            _Connection = Con.fn_Connection();

            //SqlCommand _s_Qualification = new SqlCommand("select n_QualificationCode from hrmt_RequisitionQualification where fn_RequisitionNo='" + c_rno + "'");

            string _s_Skills = "select pn_SkillSetCode from hrmt_RequiredSkills where pn_CompanyID=" + com_sk.CompanyID + " and pn_RequisitionNo='" + com_sk.RequisitionNo + "'";

            _Connection.Open();

            SqlDataAdapter _Ad_s_Skills = new SqlDataAdapter(_s_Skills, _Connection);

            DataSet _Ds_s_Skills = new DataSet();


            _Ad_s_Skills.Fill(_Ds_s_Skills, "hrmt_RequiredSkills");


            _Connection.Close();

            c1_Q_rows = _Ds_s_Skills.Tables[0].Rows.Count;






            string _Compare_Skills = "select pn_CandidateID from hrmm_CandidateSkills where pn_CompanyID=" + com_sk.CompanyID + " and pn_SkillID in(";

            for (c1_i = 0; c1_i < c1_Q_rows; c1_i++)
            {

                c1_Skills = Convert.ToString(_Ds_s_Skills.Tables[0].Rows[c1_i][0]);



                if (c1_i != c1_Q_rows - 1)
                {

                    _Compare_Skills = _Compare_Skills + c1_Skills + ",";

                    continue;
                }
                else
                {

                    _Compare_Skills = _Compare_Skills + c1_Skills;

                    continue;

                }




            }


            _Compare_Skills = _Compare_Skills + ")";






            return _Compare_Skills;


        }
        public void ResumeSortListing(Be_Recruitment r)
        {

            _Connection = Con.fn_Connection();
            SqlCommand _ISPResumeSortListing = new SqlCommand("sp_hrmt_ResumeSortListing", _Connection);

            _ISPResumeSortListing.CommandType = CommandType.StoredProcedure;

            SqlParameter[] _ISPResumeSortListingParam = new SqlParameter[8];
            _ISPResumeSortListingParam[0] = new SqlParameter("@pn_CompanyID", SqlDbType.Int);
            _ISPResumeSortListingParam[0].Value = r.CompanyID;
            _ISPResumeSortListingParam[1] = new SqlParameter("@pn_BranchID", SqlDbType.Int);
            _ISPResumeSortListingParam[1].Value = r.BranchID;
            _ISPResumeSortListingParam[2] = new SqlParameter("@pn_RequisitionNo", SqlDbType.Int);
            _ISPResumeSortListingParam[2].Value = r.RequisitionNo;
            _ISPResumeSortListingParam[3] = new SqlParameter("@pn_CandidateID", SqlDbType.VarChar);
            _ISPResumeSortListingParam[3].Value = r.CandidateID;
            _ISPResumeSortListingParam[4] = new SqlParameter("@n_EmployeeID", SqlDbType.VarChar);
            _ISPResumeSortListingParam[4].Value = r.EmployeeID;
            _ISPResumeSortListingParam[5] = new SqlParameter("@d_ShortlistDate", SqlDbType.DateTime);
            _ISPResumeSortListingParam[5].Value = r.InterviewDate;
            _ISPResumeSortListingParam[6] = new SqlParameter("@d_ShortlistTime", SqlDbType.DateTime);
            _ISPResumeSortListingParam[6].Value = r.InterviewTime;
            _ISPResumeSortListingParam[7] = new SqlParameter("@c_Selected", SqlDbType.Char);
            _ISPResumeSortListingParam[7].Value = r.c;



            for (int i = 0; i < _ISPResumeSortListingParam.Length; i++)
            {
                _ISPResumeSortListing.Parameters.Add(_ISPResumeSortListingParam[i]);


            }

            _Connection.Open();

            _ISPResumeSortListing.ExecuteNonQuery();

            _Connection.Close();



        }
        public void fn_get_RS_cancel(Be_Recruitment RS_can)
        {


            _Connection = Con.fn_Connection();

            _Connection.Open();

            SqlCommand _RSC_can = new SqlCommand("update hrmt_ResumeShortlisting set c_Selected='r' where pn_CompanyID=" + RS_can.CompanyID + " and pn_BranchID=" + RS_can.BranchID + " and pn_RequisitionNo=" + RS_can.RequisitionNo + " and pn_CandidateID=" + RS_can.CandidateID + "", _Connection);

            _RSC_can.ExecuteNonQuery();

            _Connection.Close();



        }
        public DataSet fn_get_RS_Retrive(Be_Recruitment ret_rno)
        {


            _Connection = Con.fn_Connection();


            string _RS_Retrive = "select pn_CandidateID from hrmt_ResumeShortlisting where pn_CompanyID=" + ret_rno.CompanyID + " and pn_BranchID=" + ret_rno.BranchID + " and pn_RequisitionNo=" + ret_rno.RequisitionNo + " and c_Selected='S'";


            _Connection.Open();


            SqlDataAdapter _Ad_RS_Retrive = new SqlDataAdapter(_RS_Retrive, _Connection);

            DataSet _Ds_RS_Retrive = new DataSet();

            _Ad_RS_Retrive.Fill(_Ds_RS_Retrive, "hrmt_ResumeShortlisting");


            _Connection.Close();


            return _Ds_RS_Retrive;



        }
        //Interview Sheduling
        public void InterviewSheduling(Be_Recruitment Is)
        {

            _Connection = Con.fn_Connection();
            SqlCommand _ISPInterviewSheduling = new SqlCommand("sp_hrmt_InterviewScheduling", _Connection);

            _ISPInterviewSheduling.CommandType = CommandType.StoredProcedure;

            SqlParameter[] _ISPInterviewShedulingParam = new SqlParameter[11];
            _ISPInterviewShedulingParam[0] = new SqlParameter("@pn_CompanyID", SqlDbType.Int);
            _ISPInterviewShedulingParam[0].Value = Is.CompanyID;
            _ISPInterviewShedulingParam[1] = new SqlParameter("@pn_BranchID", SqlDbType.Int);
            _ISPInterviewShedulingParam[1].Value = Is.BranchID;
            _ISPInterviewShedulingParam[2] = new SqlParameter("@pn_RequisitionNo", SqlDbType.Int);
            _ISPInterviewShedulingParam[2].Value = Is.RequisitionNo;
            _ISPInterviewShedulingParam[3] = new SqlParameter("@pn_SeqNo", SqlDbType.Int);
            _ISPInterviewShedulingParam[3].Value = Is.SeqNo;
            _ISPInterviewShedulingParam[4] = new SqlParameter("@pn_CandidateID", SqlDbType.Int);
            _ISPInterviewShedulingParam[4].Value = Is.CandidateID;
            _ISPInterviewShedulingParam[5] = new SqlParameter("@pn_EmployeeID", SqlDbType.Int);
            _ISPInterviewShedulingParam[5].Value = Is.EmployeeID;
            _ISPInterviewShedulingParam[6] = new SqlParameter("@n_EmployeeID", SqlDbType.Int);
            _ISPInterviewShedulingParam[6].Value = Is.Entry_EmployeeID;
            _ISPInterviewShedulingParam[7] = new SqlParameter("@d_InterviewDate", SqlDbType.DateTime);
            _ISPInterviewShedulingParam[7].Value = Convert.ToDateTime(Is.InterviewDate);
            _ISPInterviewShedulingParam[8] = new SqlParameter("@d_InterviewTime", SqlDbType.VarChar);
            _ISPInterviewShedulingParam[8].Value = Is.InterviewTime;
            _ISPInterviewShedulingParam[9] = new SqlParameter("@d_SchedulingDate", SqlDbType.DateTime);
            _ISPInterviewShedulingParam[9].Value =Convert.ToDateTime(Is.CurrentDate);
            _ISPInterviewShedulingParam[10] = new SqlParameter("@d_SchedulingTime", SqlDbType.VarChar);
            _ISPInterviewShedulingParam[10].Value = Is.CurrentTime;

            for (int i = 0; i < _ISPInterviewShedulingParam.Length; i++)
            {
                _ISPInterviewSheduling.Parameters.Add(_ISPInterviewShedulingParam[i]);


            }

            _Connection.Open();

            _ISPInterviewSheduling.ExecuteNonQuery();

            _Connection.Close();



        }
        public DataSet fn_get_SortListCandidate(Be_Recruitment sl)
        {



            _Connection = Con.fn_Connection();


            string _SortList = "select cp.pn_CandidateID,cp.v_CandidateName,cp.c_RecruitSource,cp.v_DOB,cp.c_Gender,cp.v_KeySkills,rs.d_ShortlistDate from hrmm_CandidateProfile cp,hrmt_ResumeShortlisting rs where rs.pn_CompanyID=" + sl.CompanyID + " and rs.pn_BranchID=" + sl.BranchID + " and rs.pn_RequisitionNo=" + sl.RequisitionNo + " and rs.c_Selected='s' and rs.pn_CandidateID=cp.pn_CandidateID";


            _Connection.Open();


            SqlDataAdapter _Ad_SortList = new SqlDataAdapter(_SortList, _Connection);

            DataSet _Ds_SortList = new DataSet();

            _Ad_SortList.Fill(_Ds_SortList, "hrmm_CandidateProfile");


            _Connection.Close();


            return _Ds_SortList;



        }
        public DataSet fn_get_SortListCandidate1(Be_Recruitment sl1)
        {


            _Connection = Con.fn_Connection();


            string _SortList1 = "select cp.pn_CandidateID,cp.v_CandidateName,cp.c_RecruitSource,cp.v_DOB,cp.c_Gender,cp.v_KeySkills from hrmm_CandidateProfile cp,hrmt_InterviewAssesment ia where ia.pn_CompanyID=" + sl1.CompanyID + " and ia.pn_BranchID=" + sl1.BranchID + " and ia.pn_RequisitionNo=" + sl1.RequisitionNo + " and ia.pn_SeqNo=" + sl1.SeqNo + " and ia.c_CandidateStatus ='s' and ia.pn_CandidateID=cp.pn_CandidateID";


            _Connection.Open();


            SqlDataAdapter _Ad_SortList1 = new SqlDataAdapter(_SortList1, _Connection);



            DataSet _Ds_SortList1 = new DataSet();

            _Ad_SortList1.Fill(_Ds_SortList1, "hrmt_InterviewAssesment");


            _Connection.Close();


            return _Ds_SortList1;



        }
        public DataSet fn_get_SortListCandidate2(Be_Recruitment sl2)
        {


            _Connection = Con.fn_Connection();


            string _SortList2 = "select pn_candidateID,pn_EmployeeID from hrmt_interviewscheduling where pn_CompanyID=" + sl2.CompanyID + " and pn_BranchID=" + sl2.BranchID + " and pn_RequisitionNo=" + sl2.RequisitionNo + " and pn_SeqNo=" + sl2.SeqNo + " ";


            _Connection.Open();


            SqlDataAdapter _Ad_SortList2 = new SqlDataAdapter(_SortList2, _Connection);

            DataSet _Ds_SortList2 = new DataSet();

            _Ad_SortList2.Fill(_Ds_SortList2, "hrmt_interviewscheduling");


            _Connection.Close();


            return _Ds_SortList2;



        }
        public DataSet fn_get_SelectInterviewer(Be_Recruitment si)
        {


            _Connection = Con.fn_Connection();


            string _SelectInterviewer = "select * from paym_Employee_profile where "+si.temp_string+"";


            _Connection.Open();


            SqlDataAdapter _Ad_SelectInterviewer = new SqlDataAdapter(_SelectInterviewer, _Connection);

            DataSet _Ds_SelectInterviewer = new DataSet();

            _Ad_SelectInterviewer.Fill(_Ds_SelectInterviewer, "paym_Employee_profile");


            _Connection.Close();


            return _Ds_SelectInterviewer;



        }
        public void fn_get_IS_Delete(Be_Recruitment IS_Del)
        {


            _Connection = Con.fn_Connection();

            _Connection.Open();

            SqlCommand _ISC_Del = new SqlCommand("delete from hrmt_InterviewScheduling where pn_CompanyID=" + IS_Del.CompanyID + " and pn_BranchID=" + IS_Del.BranchID + " and pn_RequisitionNo=" + IS_Del.RequisitionNo + " and pn_SeqNo=" + IS_Del.SeqNo + " and pn_CandidateID=" + IS_Del.CandidateID + "", _Connection);

            _ISC_Del.ExecuteNonQuery();

            _Connection.Close();



        }
        public void fn_get_IS_Delete1(Be_Recruitment IS_Del1)
        {


            _Connection = Con.fn_Connection();

            _Connection.Open();

            SqlCommand _ISC_Del1 = new SqlCommand("delete from hrmt_InterviewScheduling where pn_CompanyID=" + IS_Del1.CompanyID + " and pn_BranchID=" + IS_Del1.BranchID + " and pn_RequisitionNo=" + IS_Del1.RequisitionNo + " and pn_SeqNo=" + IS_Del1.SeqNo + " and pn_EmployeeID =" + IS_Del1.EmployeeID + "", _Connection);

            _ISC_Del1.ExecuteNonQuery();

            _Connection.Close();



        }
        public void fn_get_IS_Cancel(Be_Recruitment IS_can)
        {


            _Connection = Con.fn_Connection();

            _Connection.Open();

            SqlCommand _ISC_can = new SqlCommand("delete from hrmt_InterviewScheduling where pn_CompanyID=" + IS_can.CompanyID + " and pn_BranchID=" + IS_can.BranchID + " and pn_RequisitionNo=" + IS_can.RequisitionNo + " and pn_SeqNo=" + IS_can.SeqNo + " and pn_CandidateID=" + IS_can.CandidateID + "", _Connection);

            _ISC_can.ExecuteNonQuery();

            _Connection.Close();



        }
        public void fn_get_IS_Update(Be_Recruitment IS_Upd)
        {


            _Connection = Con.fn_Connection();

            _Connection.Open();

            SqlCommand _ISC_can = new SqlCommand("update hrmt_InterviewScheduling set pn_EmployeeID=" + IS_Upd.temp_int + ",d_InterviewDate='" + IS_Upd.InterviewDate + "',d_InterviewTime='" + IS_Upd.InterviewTime + "'  where pn_CompanyID=" + IS_Upd.CompanyID + " and pn_BranchID=" + IS_Upd.BranchID + " and pn_RequisitionNo=" + IS_Upd.RequisitionNo + " and pn_SeqNo=" + IS_Upd.SeqNo + " and pn_EmployeeID =" + IS_Upd.EmployeeID + " ", _Connection);

            _ISC_can.ExecuteNonQuery();

            _Connection.Close();



        }
        public DataSet fn_get_IS_emp(Be_Recruitment IS_emp)
        {


            _Connection = Con.fn_Connection();

            _Connection.Open();

            string _IS_emp = "select pn_EmployeeID  from hrmt_InterviewScheduling where pn_CompanyID=" + IS_emp.CompanyID + " and pn_BranchID=" + IS_emp.BranchID + " and pn_RequisitionNo=" + IS_emp.RequisitionNo + " and pn_SeqNo=" + IS_emp.SeqNo + " and pn_CandidateID =" + IS_emp.CandidateName + "";


            SqlDataAdapter _Ad_emp = new SqlDataAdapter(_IS_emp, _Connection);

            DataSet _Ds_emp = new DataSet();

            _Ad_emp.Fill(_Ds_emp, "hrmt_InterviewScheduling");


            _Connection.Close();

            return _Ds_emp;



        }
        public DataSet fn_get_IS_can(Be_Recruitment IS_can)
        {


            _Connection = Con.fn_Connection();

            _Connection.Open();

            string _IS_can = "select pn_CandidateID,pn_EmployeeID from hrmt_InterviewScheduling where pn_CompanyID=" + IS_can.CompanyID + " and pn_BranchID=" + IS_can.BranchID + " and pn_RequisitionNo=" + IS_can.RequisitionNo + " and pn_SeqNo=" + IS_can.SeqNo + " ";


            SqlDataAdapter _Ad_can = new SqlDataAdapter(_IS_can, _Connection);

            DataSet _Ds_can = new DataSet();

            _Ad_can.Fill(_Ds_can, "hrmt_InterviewScheduling");


            _Connection.Close();

            return _Ds_can;

        }
        public DataSet fn_get_IS_can1(Be_Recruitment IS_can1)
        {


            _Connection = Con.fn_Connection();

            _Connection.Open();

            string _IS_can1 = "select pn_CandidateID,pn_EmployeeID from hrmt_InterviewAssesment where pn_CompanyID=" + IS_can1.CompanyID + " and pn_BranchID=" + IS_can1.BranchID + " and pn_RequisitionNo=" + IS_can1.RequisitionNo + " and pn_SeqNo=" + IS_can1.SeqNo + " and c_CandidateStatus ='s' ";


            SqlDataAdapter _Ad_can1 = new SqlDataAdapter(_IS_can1, _Connection);

            DataSet _Ds_can1 = new DataSet();

            _Ad_can1.Fill(_Ds_can1, "hrmt_InterviewAssesment");


            _Connection.Close();

            return _Ds_can1;

        }
        public DataSet fn_get_SelectInterviewer2(Be_Recruitment si)
        {


            _Connection = Con.fn_Connection();


            string _SelectInterviewer2 = "select pn_EmployeeID from paym_Employee where pn_CompanyID=" + si.CompanyID + " and EmployeeCode='" + si.temp_string + "'";


            _Connection.Open();


            SqlDataAdapter _Ad_SelectInterviewer2 = new SqlDataAdapter(_SelectInterviewer2, _Connection);

            DataSet _Ds_SelectInterviewer2 = new DataSet();

            _Ad_SelectInterviewer2.Fill(_Ds_SelectInterviewer2, "paym_Employee");


            _Connection.Close();


            return _Ds_SelectInterviewer2;



        }      
        //Interview Assesment
        public void InterviewAssesment(Be_Recruitment IAC)
        {

            _Connection = Con.fn_Connection();
            SqlCommand _ISPInterviewAssesment = new SqlCommand("sp_hrmt_InterviewAssesment", _Connection);

            _ISPInterviewAssesment.CommandType = CommandType.StoredProcedure;

            SqlParameter[] _ISPInterviewAssesmentParam = new SqlParameter[13];
            _ISPInterviewAssesmentParam[0] = new SqlParameter("@pn_CompanyID", SqlDbType.Int);
            _ISPInterviewAssesmentParam[0].Value = IAC.CompanyID;
            _ISPInterviewAssesmentParam[1] = new SqlParameter("@pn_BranchID", SqlDbType.VarChar);
            _ISPInterviewAssesmentParam[1].Value = IAC.BranchID;
            _ISPInterviewAssesmentParam[2] = new SqlParameter("@pn_RequisitionNo", SqlDbType.Int);
            _ISPInterviewAssesmentParam[2].Value = IAC.RequisitionNo;
            _ISPInterviewAssesmentParam[3] = new SqlParameter("@pn_CandidateID", SqlDbType.Int);
            _ISPInterviewAssesmentParam[3].Value = IAC.CandidateID;
            _ISPInterviewAssesmentParam[4] = new SqlParameter("@pn_SeqNo", SqlDbType.Int);
            _ISPInterviewAssesmentParam[4].Value = IAC.SeqNo;
            _ISPInterviewAssesmentParam[5] = new SqlParameter("@d_InterviewDate", SqlDbType.DateTime);
            _ISPInterviewAssesmentParam[5].Value = Convert.ToDateTime(IAC.InterviewDate);
            _ISPInterviewAssesmentParam[6] = new SqlParameter("@d_InterviewTime", SqlDbType.VarChar);
            _ISPInterviewAssesmentParam[6].Value = IAC.InterviewTime;
            _ISPInterviewAssesmentParam[7] = new SqlParameter("@pn_EmployeeID", SqlDbType.Int);
            _ISPInterviewAssesmentParam[7].Value = IAC.EmployeeID;
            _ISPInterviewAssesmentParam[8] = new SqlParameter("@v_Comments", SqlDbType.VarChar);
            _ISPInterviewAssesmentParam[8].Value = IAC.Comments;
            _ISPInterviewAssesmentParam[9] = new SqlParameter("@c_CandidateStatus", SqlDbType.Char);
            _ISPInterviewAssesmentParam[9].Value = IAC.Status;
            _ISPInterviewAssesmentParam[10] = new SqlParameter("@v_Criteria", SqlDbType.VarChar);
            _ISPInterviewAssesmentParam[10].Value = IAC.Criteria;
            _ISPInterviewAssesmentParam[11] = new SqlParameter("@v_Rating", SqlDbType.VarChar);
            _ISPInterviewAssesmentParam[11].Value = IAC.Rating;
            _ISPInterviewAssesmentParam[12] = new SqlParameter("@n_marks", SqlDbType.Int);
            _ISPInterviewAssesmentParam[12].Value = IAC.temp_int;


            for (int i = 0; i < _ISPInterviewAssesmentParam.Length; i++)
            {
                _ISPInterviewAssesment.Parameters.Add(_ISPInterviewAssesmentParam[i]);


            }

            _Connection.Open();

            _ISPInterviewAssesment.ExecuteNonQuery();

            _Connection.Close();



        }
        public DataSet fn_get_InterviewAssesment1(Be_Recruitment IA1)
        {


            _Connection = Con.fn_Connection();


            string _InterviewAssesment1 = "select cp.pn_CandidateID,cp.v_CandidateName,cp.c_RecruitSource,cp.v_DOB,cp.c_Gender,cp.v_KeySkills,ish.d_InterviewDate from hrmm_CandidateProfile cp,hrmt_InterviewScheduling ish where ish.pn_CompanyID=" + IA1.CompanyID + " and ish.pn_BranchID=" + IA1.Entry_BranchID + " and ish.pn_RequisitionNo=" + IA1.RequisitionNo + " and ish.pn_EmployeeID=" + IA1.EmployeeID + " and ish.pn_SeqNo=" + IA1.SeqNo + " and ish.pn_CandidateID=cp.pn_CandidateID";


            _Connection.Open();


            SqlDataAdapter _Ad_InterviewAssesment1 = new SqlDataAdapter(_InterviewAssesment1, _Connection);

            DataSet _Ds_InterviewAssesment1 = new DataSet();

            _Ad_InterviewAssesment1.Fill(_Ds_InterviewAssesment1, "hrmt_InterviewScheduling");


            _Connection.Close();


            return _Ds_InterviewAssesment1;



        }
        public DataSet fn_get_InterviewAssesment2(Be_Recruitment IA2)
        {
            int IA2_IS_SeqNo = IA2.SeqNo + 1;



            _Connection = Con.fn_Connection();


            string _InterviewAssesment2 = "select cp.pn_CandidateID,cp.v_CandidateName,cp.c_RecruitSource,cp.v_DOB,cp.c_Gender,cp.v_KeySkills,ish.d_InterviewDate from hrmm_CandidateProfile cp,hrmt_InterviewScheduling ish where ish.pn_CompanyID=" + IA2.CompanyID + " and ish.pn_BranchID=" + IA2.Entry_BranchID + " and ish.pn_RequisitionNo=" + IA2.RequisitionNo + " and ish.pn_EmployeeID=" + IA2.EmployeeID + " and ish.pn_SeqNo=" + IA2_IS_SeqNo + " and ish.pn_CandidateID in(select pn_CandidateID from hrmt_InterviewAssesment where pn_RequisitionNo=" + IA2.RequisitionNo + " and pn_SeqNo=" + IA2.SeqNo + " and c_CandidateStatus ='s') and ish.pn_CandidateID=cp.pn_CandidateID";


            _Connection.Open();


            SqlDataAdapter _Ad_InterviewAssesment2 = new SqlDataAdapter(_InterviewAssesment2, _Connection);

            DataSet _Ds_InterviewAssesment2 = new DataSet();

            _Ad_InterviewAssesment2.Fill(_Ds_InterviewAssesment2, "hrmt_InterviewAssesment");


            _Connection.Close();


            return _Ds_InterviewAssesment2;



        }
        public Collection<Be_Recruitment> fn_getAssesment(Be_Recruitment a_cID)
        {
            Collection<Be_Recruitment> AssesmentList = new Collection<Be_Recruitment>();
            _Connection = Con.fn_Connection();
            SqlCommand _Assesment = new SqlCommand("select * from hrmt_InterviewAssesment where pn_CompanyID=" + a_cID.CompanyID + " and pn_BranchID=" + a_cID.BranchID + " and pn_CandidateID=" + a_cID.CandidateID + "", _Connection);
            _Connection.Open();
            SqlDataReader dr = _Assesment.ExecuteReader();
            while (dr.Read())
            {
                Be_Recruitment assesment = new Be_Recruitment();
                //assesment.CompanyID = (int)dr["pn_CompanyID"];
                assesment.Comments = (string)dr["v_Comments"];
                assesment.Criteria = (string)dr["v_Criteria"];
                assesment.Rating = (string)dr["v_Rating"];
                assesment.Status = Convert.ToString(dr["c_CandidateStatus"]);

                AssesmentList.Add(assesment);
            }
            return AssesmentList;
        }
        public Collection<Be_Recruitment> fn_get_Edit_Result(Be_Recruitment R_cID)
        {
            Collection<Be_Recruitment> Edit_ResultList = new Collection<Be_Recruitment>();
            _Connection = Con.fn_Connection();
            SqlCommand _Edit_Result = new SqlCommand("select * from hrmt_InterviewResult where pn_CompanyID=" + R_cID.CompanyID + " and pn_BranchID=" + R_cID.BranchID + " and pn_CandidateID=" + R_cID.CandidateID + "", _Connection);
            _Connection.Open();
            SqlDataReader dr = _Edit_Result.ExecuteReader();
            while (dr.Read())
            {
                Be_Recruitment edit_Result = new Be_Recruitment();

                //edit_Result.Status = Convert.ToChar(dr["v_CurrentStatus"]);

                edit_Result.Comments = (string)dr["v_AdditionalComments"];
                edit_Result.HRComments = (string)dr["v_HRComments"];

                Edit_ResultList.Add(edit_Result);
            }
            return Edit_ResultList;
        }
        public Collection<Be_Recruitment> fn_getIRCheck(Be_Recruitment RC_cID)
        {
            Collection<Be_Recruitment> IRCheckList = new Collection<Be_Recruitment>();
            _Connection = Con.fn_Connection();
            SqlCommand _IRCheck = new SqlCommand("select * from hrmt_ReferenceCheck where pn_CompanyID=" + RC_cID.CompanyID + " and pn_BranchID=" + RC_cID.BranchID + " and pn_CandidateID=" + RC_cID.CandidateID + "", _Connection);
            _Connection.Open();
            SqlDataReader dr = _IRCheck.ExecuteReader();
            while (dr.Read())
            {
                Be_Recruitment iRCheck = new Be_Recruitment();


                iRCheck.Comments = (string)dr["v_Comment"];


                IRCheckList.Add(iRCheck);
            }
            return IRCheckList;
        }
        public void fn_get_IA_cancel(Be_Recruitment IA_can)
        {


            _Connection = Con.fn_Connection();

            _Connection.Open();

            SqlCommand _ISC_can = new SqlCommand("delete from hrmt_InterviewAssesment where pn_CompanyID=" + IA_can.CompanyID + " and pn_BranchID=" + IA_can.BranchID + "  and pn_RequisitionNo=" + IA_can.RequisitionNo + " and pn_SeqNo=" + IA_can.SeqNo + " and pn_EmployeeID =" + IA_can.EmployeeID + " and pn_CandidateID=" + IA_can.CandidateID + "", _Connection);

            _ISC_can.ExecuteNonQuery();

            _Connection.Close();



        }
        public DataSet fn_get_IA_Retrive(Be_Recruitment IA_ret)
        {


            _Connection = Con.fn_Connection();


            string _IA_Retrive = "select pn_CandidateID,v_Comments,c_CandidateStatus,v_Criteria,v_Rating from hrmt_InterviewAssesment where pn_CompanyID=" + IA_ret.CompanyID + " and pn_BranchID=" + IA_ret.BranchID + " and pn_RequisitionNo=" + IA_ret.RequisitionNo + " and pn_EmployeeID =" + IA_ret.EmployeeID + "";


            _Connection.Open();


            SqlDataAdapter _Ad_IA_Retrive = new SqlDataAdapter(_IA_Retrive, _Connection);

            DataSet _Ds_IA_Retrive = new DataSet();

            _Ad_IA_Retrive.Fill(_Ds_IA_Retrive, "hrmt_InterviewAssesment");


            _Connection.Close();


            return _Ds_IA_Retrive;



        }
        //Interview Result
        public DataSet fn_get_Result(Be_Recruitment IR)
        {


            _Connection = Con.fn_Connection();


            string _Result = "select * from hrmm_CandidateProfile where pn_CompanyID=" + IR.CompanyID + " and pn_CandidateID in (select pn_CandidateID  from hrmt_InterviewAssesment where pn_CompanyID=" + IR.CompanyID + " and pn_BranchID=" + IR.BranchID + " and pn_RequisitionNo=" + IR.RequisitionNo + " and pn_SeqNo=" + IR.SeqNo + " and c_CandidateStatus ='s') ";


            _Connection.Open();


            SqlDataAdapter _Ad_Result = new SqlDataAdapter(_Result, _Connection);

            DataSet _Ds_Result = new DataSet();

            _Ad_Result.Fill(_Ds_Result, "hrmm_CandidateProfile");


            _Connection.Close();


            return _Ds_Result;



        }
        public DataSet fn_get_Result2(Be_Recruitment IR2)
        {


            _Connection = Con.fn_Connection();


            //string _Result2 = "select c.pn_CandidateID,c.v_CandidateName,a.pn_SeqNo,a.d_InterviewDate,a.pn_EmployeeID,a.v_Comments,a.c_CandidateStatus,a.v_Criteria,a.v_Rating from hrmm_CandidateProfile c,hrmt_InterviewAssesment a where c.pn_CandidateID =" + IR2.CandidateID + " and a.pn_CandidateID =" + IR2.CandidateID + "";

            string _Result2 = "select ia.pn_SeqNo,ia.pn_EmployeeID,ia.v_Comments,ia.c_CandidateStatus,ia.v_Criteria,v_Rating,ep.EmployeeCode from hrmt_InterviewAssesment ia,paym_Employee ep where ia.pn_CompanyID=" + IR2.CompanyID + " and ia.pn_BranchID=" + IR2.BranchID + " and ia.pn_RequisitionNo=" + IR2.RequisitionNo + " and ia.pn_CandidateID =" + IR2.CandidateID + " and ia.pn_EmployeeID=ep.pn_EmployeeID";


            _Connection.Open();


            SqlDataAdapter _Ad_Result2 = new SqlDataAdapter(_Result2, _Connection);

            DataSet _Ds_Result2 = new DataSet();

            _Ad_Result2.Fill(_Ds_Result2);


            _Connection.Close();


            return _Ds_Result2;



        }
        public void InterviewResult(Be_Recruitment I_R)
        {

            _Connection = Con.fn_Connection();
            SqlCommand _ISPInterviewResult = new SqlCommand("sp_hrmt_InterviewResult", _Connection);

            _ISPInterviewResult.CommandType = CommandType.StoredProcedure;

            SqlParameter[] _ISPInterviewResultParam = new SqlParameter[10];
            _ISPInterviewResultParam[0] = new SqlParameter("@pn_CompanyID", SqlDbType.Int);
            _ISPInterviewResultParam[0].Value = I_R.CompanyID;
            _ISPInterviewResultParam[1] = new SqlParameter("@pn_BranchID", SqlDbType.VarChar);
            _ISPInterviewResultParam[1].Value = I_R.BranchID;
            _ISPInterviewResultParam[2] = new SqlParameter("@pn_RequisitionNo", SqlDbType.Int);
            _ISPInterviewResultParam[2].Value = I_R.RequisitionNo;
            _ISPInterviewResultParam[3] = new SqlParameter("@pn_CandidateID", SqlDbType.Int);
            _ISPInterviewResultParam[3].Value = I_R.CandidateID;
            _ISPInterviewResultParam[4] = new SqlParameter("@n_EmployeeID", SqlDbType.Int);
            _ISPInterviewResultParam[4].Value = I_R.EmployeeID;
            _ISPInterviewResultParam[5] = new SqlParameter("@d_ResultDate ", SqlDbType.DateTime);
            _ISPInterviewResultParam[5].Value = Convert.ToDateTime(I_R.InterviewDate);
            _ISPInterviewResultParam[6] = new SqlParameter("@d_ResultTime ", SqlDbType.VarChar);
            _ISPInterviewResultParam[6].Value = I_R.InterviewTime;
            _ISPInterviewResultParam[7] = new SqlParameter("@v_CurrentStatus ", SqlDbType.VarChar);
            _ISPInterviewResultParam[7].Value = I_R.FinalStatus;
            _ISPInterviewResultParam[8] = new SqlParameter("@v_AdditionalComments ", SqlDbType.VarChar);
            _ISPInterviewResultParam[8].Value = I_R.Comments;
            _ISPInterviewResultParam[9] = new SqlParameter("@v_HRComments ", SqlDbType.VarChar);
            _ISPInterviewResultParam[9].Value = I_R.HRComments;


            for (int i = 0; i < _ISPInterviewResultParam.Length; i++)
            {
                _ISPInterviewResult.Parameters.Add(_ISPInterviewResultParam[i]);


            }

            _Connection.Open();

            _ISPInterviewResult.ExecuteNonQuery();

            _Connection.Close();



        }
        public DataSet fn_get_IR_Retrive(Be_Recruitment IR_ret)
        {


            _Connection = Con.fn_Connection();


            string _IR_Retrive = "select pn_CandidateID,v_CurrentStatus,v_AdditionalComments,v_HRComments from hrmt_InterviewResult where pn_CompanyID=" + IR_ret.CompanyID + " and pn_BranchID=" + IR_ret.BranchID + " and pn_RequisitionNo=" + IR_ret.RequisitionNo + "";


            _Connection.Open();


            SqlDataAdapter _Ad_IR_Retrive = new SqlDataAdapter(_IR_Retrive, _Connection);

            DataSet _Ds_IR_Retrive = new DataSet();

            _Ad_IR_Retrive.Fill(_Ds_IR_Retrive, "hrmt_InterviewResult");


            _Connection.Close();


            return _Ds_IR_Retrive;



        }
        public void fn_get_IR_cancel(Be_Recruitment IR_can)
        {


            _Connection = Con.fn_Connection();

            _Connection.Open();

            SqlCommand _ISC_can = new SqlCommand("delete from hrmt_InterviewResult where pn_CompanyID=" + IR_can.CompanyID + " and pn_BranchID=" + IR_can.BranchID + " and pn_RequisitionNo=" + IR_can.RequisitionNo + " and pn_CandidateID=" + IR_can.CandidateID + "", _Connection);
            _ISC_can.ExecuteNonQuery();

            _Connection.Close();



        }
        //Interview Hold And Rejected
        public void HoldAndReject(Be_Recruitment har)
        {


            _Connection = Con.fn_Connection();
            SqlCommand _ISPHoldAndReject = new SqlCommand("sp_hrmt_InterviewResult", _Connection);

            _ISPHoldAndReject.CommandType = CommandType.StoredProcedure;

            SqlParameter[] _ISPHoldAndRejectParam = new SqlParameter[10];
            _ISPHoldAndRejectParam[0] = new SqlParameter("@pn_CompanyID", SqlDbType.Int);
            _ISPHoldAndRejectParam[0].Value = har.CompanyID;
            _ISPHoldAndRejectParam[1] = new SqlParameter("@pn_BranchID", SqlDbType.VarChar);
            _ISPHoldAndRejectParam[1].Value = har.BranchID;
            _ISPHoldAndRejectParam[2] = new SqlParameter("@pn_RequisitionNo", SqlDbType.Int);
            _ISPHoldAndRejectParam[2].Value = har.RequisitionNo;
            _ISPHoldAndRejectParam[3] = new SqlParameter("@pn_CandidateID", SqlDbType.Int);
            _ISPHoldAndRejectParam[3].Value = har.CandidateID;
            _ISPHoldAndRejectParam[4] = new SqlParameter("@n_EmployeeID", SqlDbType.Int);
            _ISPHoldAndRejectParam[4].Value = har.EmployeeID;
            _ISPHoldAndRejectParam[5] = new SqlParameter("@d_ResultDate ", SqlDbType.DateTime);
            _ISPHoldAndRejectParam[5].Value = Convert.ToDateTime(har.InterviewDate);
            _ISPHoldAndRejectParam[6] = new SqlParameter("@d_ResultTime ", SqlDbType.VarChar);
            _ISPHoldAndRejectParam[6].Value = har.InterviewTime;
            _ISPHoldAndRejectParam[7] = new SqlParameter("@v_CurrentStatus ", SqlDbType.VarChar);
            _ISPHoldAndRejectParam[7].Value = har.FinalStatus;
            _ISPHoldAndRejectParam[8] = new SqlParameter("@v_AdditionalComments ", SqlDbType.VarChar);
            _ISPHoldAndRejectParam[8].Value = har.Comments;
            _ISPHoldAndRejectParam[9] = new SqlParameter("@v_HRComments ", SqlDbType.VarChar);
            _ISPHoldAndRejectParam[9].Value = har.HRComments;



            for (int i = 0; i < _ISPHoldAndRejectParam.Length; i++)
            {
                _ISPHoldAndReject.Parameters.Add(_ISPHoldAndRejectParam[i]);


            }

            _Connection.Open();

            _ISPHoldAndReject.ExecuteNonQuery();

            _Connection.Close();

        }
        public DataSet fn_get_RejectHold(Be_Recruitment IRH)
        {

            string _RejectHold;

            _Connection = Con.fn_Connection();

            if (IRH.common == "a")
            {

                _RejectHold = "select * from hrmm_CandidateProfile where pn_CompanyID=" + IRH.CompanyID + " and pn_CandidateID in (select pn_CandidateID  from  hrmt_InterviewAssesment where pn_CompanyID=" + IRH.CompanyID + " and pn_BranchID=" + IRH.BranchID + " and pn_RequisitionNo=" + IRH.RequisitionNo + " and c_CandidateStatus in('h','r'))";


            }
            else
            {

                _RejectHold = "select * from hrmm_CandidateProfile where pn_CompanyID=" + IRH.CompanyID + "  and pn_CandidateID in (select pn_CandidateID  from hrmt_InterviewAssesment where pn_CompanyID=" + IRH.CompanyID + " and pn_BranchID=" + IRH.BranchID + " and pn_RequisitionNo=" + IRH.RequisitionNo + " and c_CandidateStatus ='" + IRH.common + "')";


            }

            _Connection.Open();

            SqlDataAdapter _Ad_RejectHold = new SqlDataAdapter(_RejectHold, _Connection);

            DataSet _Ds_RejectHold = new DataSet();

            _Ad_RejectHold.Fill(_Ds_RejectHold, "hrmt_InterviewAssesment");

            _Connection.Close();

            return _Ds_RejectHold;
        }
        public DataSet fn_get_RejectHold2(Be_Recruitment IRH2)
        {
            _Connection = Con.fn_Connection();

            //string _Result2 = "select c.pn_CandidateID,c.v_CandidateName,a.pn_SeqNo,a.d_InterviewDate,a.pn_EmployeeID,a.v_Comments,a.c_CandidateStatus,a.v_Criteria,a.v_Rating from hrmm_CandidateProfile c,hrmt_InterviewAssesment a where c.pn_CandidateID =" + IR2.CandidateID + " and a.pn_CandidateID =" + IR2.CandidateID + "";

            string _RejectHold2 = "select ia.pn_SeqNo,ia.pn_EmployeeID,ia.v_Comments,ia.c_CandidateStatus,ia.v_Criteria,v_Rating,ep.EmployeeCode from hrmt_InterviewAssesment ia,paym_Employee ep where ia.pn_CompanyID=" + IRH2.CompanyID + " and ia.pn_BranchID=" + IRH2.BranchID + " and ia.pn_RequisitionNo=" + IRH2.RequisitionNo + " and ia.pn_CandidateID =" + IRH2.CandidateID + " and ia.pn_EmployeeID=ep.pn_EmployeeID";

            _Connection.Open();
            SqlDataAdapter _Ad_RejectHold2 = new SqlDataAdapter(_RejectHold2, _Connection);

            DataSet _Ds_RejectHold2 = new DataSet();
            _Ad_RejectHold2.Fill(_Ds_RejectHold2);

            _Connection.Close();
            return _Ds_RejectHold2;
        }
        //Interview Reference Check
        public DataSet fn_get_Reference(Be_Recruitment IRC)
        {
            _Connection = Con.fn_Connection();
            string _Reference = "select * from hrmm_CandidateProfile where pn_CompanyID=" + IRC.CompanyID + " and pn_CandidateID in (select pn_CandidateID from hrmt_InterviewResult where pn_CompanyID=" + IRC.CompanyID + " and pn_BranchID=" + IRC.BranchID + " and pn_RequisitionNo=" + IRC.RequisitionNo + ")";
            _Connection.Open();

            SqlDataAdapter _Ad_Reference = new SqlDataAdapter(_Reference, _Connection);

            DataSet _Ds_Reference = new DataSet();

            _Ad_Reference.Fill(_Ds_Reference, "hrmt_InterviewResult");

            _Connection.Close();

            return _Ds_Reference;

        }
        public DataSet fn_get_Reference1(Be_Recruitment IRC1)
        {

            _Connection = Con.fn_Connection();

            string _Reference1 = "select * from paym_Employee where pn_CompanyID=" + IRC1.CompanyID + " and pn_BranchID=" + IRC1.BranchID + " and pn_EmployeeID in (select fn_EmployeeID from hrmm_CandidateProfile where pn_BranchID=" + IRC1.BranchID + " and pn_CandidateID =" + IRC1.CandidateID + ")";

            _Connection.Open();

            SqlDataAdapter _Ad_Reference1 = new SqlDataAdapter(_Reference1, _Connection);

            DataSet _Ds_Reference1 = new DataSet();

            _Ad_Reference1.Fill(_Ds_Reference1, "paym_Employee");

            _Connection.Close();
            return _Ds_Reference1;
        }
        public void InterviewReference(Be_Recruitment I_RC)
        {

            _Connection = Con.fn_Connection();
            SqlCommand _ISPInterviewReference = new SqlCommand("sp_hrmt_ReferenceCheck", _Connection);

            _ISPInterviewReference.CommandType = CommandType.StoredProcedure;

            SqlParameter[] _ISPInterviewReferenceParam = new SqlParameter[10];
            _ISPInterviewReferenceParam[0] = new SqlParameter("@pn_CompanyID", SqlDbType.Int);
            _ISPInterviewReferenceParam[0].Value = I_RC.CompanyID;
            _ISPInterviewReferenceParam[1] = new SqlParameter("@pn_BranchID", SqlDbType.Int);
            _ISPInterviewReferenceParam[1].Value = I_RC.BranchID;
            _ISPInterviewReferenceParam[2] = new SqlParameter("@pn_RequisitionNo", SqlDbType.Int);
            _ISPInterviewReferenceParam[2].Value = I_RC.RequisitionNo;
            _ISPInterviewReferenceParam[3] = new SqlParameter("@pn_CandidateID", SqlDbType.Int);
            _ISPInterviewReferenceParam[3].Value = I_RC.CandidateID;
            _ISPInterviewReferenceParam[4] = new SqlParameter("@n_EmployeeID", SqlDbType.Int);
            _ISPInterviewReferenceParam[4].Value = I_RC.EmployeeID;
            _ISPInterviewReferenceParam[5] = new SqlParameter("@pn_ReferenceID", SqlDbType.Int);
            _ISPInterviewReferenceParam[5].Value = I_RC.QuestionID;
            _ISPInterviewReferenceParam[6] = new SqlParameter("@pn_QuestionID", SqlDbType.Int);
            _ISPInterviewReferenceParam[6].Value = I_RC.QuestionID;
            _ISPInterviewReferenceParam[7] = new SqlParameter("@d_ReferenceCheckDate", SqlDbType.DateTime);
            _ISPInterviewReferenceParam[7].Value =Convert.ToDateTime(I_RC.InterviewDate);
            _ISPInterviewReferenceParam[8] = new SqlParameter("@d_ReferenceCheckTime", SqlDbType.VarChar);
            _ISPInterviewReferenceParam[8].Value = I_RC.InterviewTime;
            _ISPInterviewReferenceParam[9] = new SqlParameter("@v_Comment", SqlDbType.VarChar);
            _ISPInterviewReferenceParam[9].Value = I_RC.Comments;


            for (int i = 0; i < _ISPInterviewReferenceParam.Length; i++)
            {
                _ISPInterviewReference.Parameters.Add(_ISPInterviewReferenceParam[i]);


            }

            _Connection.Open();

            _ISPInterviewReference.ExecuteNonQuery();

            _Connection.Close();



        }
        public void fn_get_RC_cancel(Be_Recruitment RC_can)
        {


            _Connection = Con.fn_Connection();

            _Connection.Open();

            SqlCommand _ISC_can = new SqlCommand("delete from hrmt_ReferenceCheck where pn_CompanyID=" + RC_can.CompanyID + " and pn_BranchID=" + RC_can.BranchID + " and pn_RequisitionNo=" + RC_can.RequisitionNo + " and pn_CandidateID=" + RC_can.CandidateID + "", _Connection);
            _ISC_can.ExecuteNonQuery();

            _Connection.Close();



        }
        public DataSet fn_get_RC_Retrive(Be_Recruitment RC_ret)
        {


            _Connection = Con.fn_Connection();


            string _RC_Retrive = "select pn_CandidateID,v_Comment from hrmt_ReferenceCheck where pn_CompanyID=" + RC_ret.CompanyID + " and pn_BranchID=" + RC_ret.BranchID + " and pn_RequisitionNo=" + RC_ret.RequisitionNo + "";


            _Connection.Open();


            SqlDataAdapter _Ad_RC_Retrive = new SqlDataAdapter(_RC_Retrive, _Connection);

            DataSet _Ds_RC_Retrive = new DataSet();

            _Ad_RC_Retrive.Fill(_Ds_RC_Retrive, "hrmt_ReferenceCheck");


            _Connection.Close();


            return _Ds_RC_Retrive;



        }

        //**********************************Collections*********************************************************

        public Collection<Be_Recruitment> fn_getCompany()
        {
            Collection<Be_Recruitment> CompanyList = new Collection<Be_Recruitment>();
            _Connection = Con.fn_Connection();
            SqlCommand _Company = new SqlCommand("select * from paym_Company", _Connection);
            _Connection.Open();
            SqlDataReader dr = _Company.ExecuteReader();
            while (dr.Read())
            {
                Be_Recruitment company = new Be_Recruitment();
                company.CompanyID = (int)dr["pn_CompanyID"];
                company.CompanyName = Convert.IsDBNull(dr["v_CompanyName"]) ? "" : (string)dr["v_CompanyName"];
                CompanyList.Add(company);
            }
            return CompanyList;
        }
        public Collection<Be_Recruitment> fn_getEmployee(Be_Recruitment c_em)
        {
            Collection<Be_Recruitment> DepartmentHeadList = new Collection<Be_Recruitment>();
            _Connection = Con.fn_Connection();
            SqlCommand _DepartmentHead = new SqlCommand("select * from paym_Employee where pn_CompanyID=" + c_em.CompanyID + " and pn_BranchID=" + c_em.BranchID + " and status='Y'", _Connection);
            _Connection.Open();
            SqlDataReader dr = _DepartmentHead.ExecuteReader();

            while (dr.Read())
            {
                Be_Recruitment employee = new Be_Recruitment();
                employee.EmployeeID = (int)dr["pn_EmployeeID"];
                employee.EmployeeName = Convert.IsDBNull(dr["v_FirstName"]) ? "" : (string)dr["v_FirstName"];
                DepartmentHeadList.Add(employee);
            }
            return DepartmentHeadList;
        }
        public Collection<Be_Recruitment> fn_getBranch(Be_Recruitment c_br)
        {
            Collection<Be_Recruitment> BranchList = new Collection<Be_Recruitment>();
            _Connection = Con.fn_Connection();
            SqlCommand _Branch = new SqlCommand("select * from paym_branch where pn_CompanyID=" + c_br.CompanyID + " and status='Y'", _Connection);
            _Connection.Open();
            SqlDataReader dr = _Branch.ExecuteReader();
            while (dr.Read())
            {
                Be_Recruitment branch = new Be_Recruitment();
                branch.BranchID = (int)dr["pn_BranchID"];
                branch.BranchName = Convert.IsDBNull(dr["BranchName"]) ? "" : (string)dr["BranchName"];
                BranchList.Add(branch);
            }
            return BranchList;
        }
        public Collection<Be_Recruitment> fn_getDivision()
        {
            Collection<Be_Recruitment> DivisionList = new Collection<Be_Recruitment>();
            _Connection = Con.fn_Connection();
            SqlCommand _Division = new SqlCommand("select * from paym_Division where status='Y'", _Connection);
            _Connection.Open();
            SqlDataReader dr = _Division.ExecuteReader();
            while (dr.Read())
            {
                Be_Recruitment division = new Be_Recruitment();
                division.DivisionID = (int)dr["pn_DivisionID"];
                division.DivisionName = Convert.IsDBNull(dr["v_DivisionName"]) ? "" : (string)dr["v_DivisionName"];
                DivisionList.Add(division);
            }
            return DivisionList;
        }
        public Collection<Be_Recruitment> fn_getDepartment()
        {
            Collection<Be_Recruitment> DepartmentList = new Collection<Be_Recruitment>();
            _Connection = Con.fn_Connection();
            SqlCommand _Department = new SqlCommand("select * from paym_Department where status='Y'", _Connection);
            _Connection.Open();
            SqlDataReader dr = _Department.ExecuteReader();
            while (dr.Read())
            {
                Be_Recruitment department = new Be_Recruitment();
                department.DepartmentID = (int)dr["pn_DepartmentID"];
                department.DepartmentName = Convert.IsDBNull(dr["v_DepartmentName"]) ? "" : (string)dr["v_DepartmentName"];
                DepartmentList.Add(department);
            }
            return DepartmentList;
        }
        public Collection<Be_Recruitment> fn_getDesignation()
        {
            Collection<Be_Recruitment> DesignationList = new Collection<Be_Recruitment>();
            _Connection = Con.fn_Connection();
            SqlCommand _Designation = new SqlCommand("select * from paym_Designation where status='Y'", _Connection);
            _Connection.Open();
            SqlDataReader dr = _Designation.ExecuteReader();
            while (dr.Read())
            {
                Be_Recruitment designation = new Be_Recruitment();
                designation.DesignationID = (int)dr["pn_DesignationID"];
                designation.DesignationName = Convert.IsDBNull(dr["v_DesignationName"]) ? "" : (string)dr["v_DesignationName"];
                DesignationList.Add(designation);
            }
            return DesignationList;
        }
        public Collection<Be_Recruitment> fn_getGrade()
        {
            Collection<Be_Recruitment> GradeList = new Collection<Be_Recruitment>();
            _Connection = Con.fn_Connection();
            SqlCommand _Grade = new SqlCommand("select * from paym_Grade where status='Y'", _Connection);
            _Connection.Open();
            SqlDataReader dr = _Grade.ExecuteReader();
            while (dr.Read())
            {
                Be_Recruitment grade = new Be_Recruitment();
                grade.GradeID = (int)dr["pn_GradeID"];
                grade.GradeName = Convert.IsDBNull(dr["v_GradeName"]) ? "" : (string)dr["v_GradeName"];
                GradeList.Add(grade);
            }
            return GradeList;
        }
        public Collection<Be_Recruitment> fn_getJobStatus()
        {
            Collection<Be_Recruitment> JobStatusList = new Collection<Be_Recruitment>();
            _Connection = Con.fn_Connection();
            SqlCommand _JobStatus = new SqlCommand("select * from paym_JobStatus where status='Y'", _Connection);
            _Connection.Open();
            SqlDataReader dr = _JobStatus.ExecuteReader();
            while (dr.Read())
            {
                Be_Recruitment jobStatus = new Be_Recruitment();
                jobStatus.JobStatusID = (int)dr["pn_JobStatusID"];
                jobStatus.JobStatusName = Convert.IsDBNull(dr["v_JobStatusName"]) ? "" : (string)dr["v_JobStatusName"];
                JobStatusList.Add(jobStatus);
            }
            return JobStatusList;
        }
        public Collection<Be_Recruitment> fn_getShift()
        {
            Collection<Be_Recruitment> ShiftList = new Collection<Be_Recruitment>();
            _Connection = Con.fn_Connection();
            SqlCommand _Shift = new SqlCommand("select * from paym_Shift where status='Y'", _Connection);
            _Connection.Open();
            SqlDataReader dr = _Shift.ExecuteReader();
            while (dr.Read())
            {
                Be_Recruitment shift = new Be_Recruitment();
                shift.ShiftTypeID = (int)dr["pn_ShiftID"];
                shift.ShiftTypeName = Convert.IsDBNull(dr["v_ShiftName"]) ? "" : (string)dr["v_ShiftName"];
                ShiftList.Add(shift);
            }
            return ShiftList;
        }
        public Collection<Be_Recruitment> fn_getEmployeeCategory()
        {
            Collection<Be_Recruitment> EmployeeCategoryList = new Collection<Be_Recruitment>();
            _Connection = Con.fn_Connection();
            SqlCommand _EmployeeCategory = new SqlCommand("select * from paym_Category where status='Y'", _Connection);
            _Connection.Open();
            SqlDataReader dr = _EmployeeCategory.ExecuteReader();
            while (dr.Read())
            {
                Be_Recruitment employeeCategory = new Be_Recruitment();


                employeeCategory.CategoryCode = (int)dr["pn_CategoryID"];
                employeeCategory.CategoryName = Convert.IsDBNull(dr["v_CategoryName"]) ? "" : (string)dr["v_CategoryName"];



                EmployeeCategoryList.Add(employeeCategory);
            }
            return EmployeeCategoryList;
        }
        public Collection<Be_Recruitment> fn_getLevel()
        {
            Collection<Be_Recruitment> EmployeeCategoryList = new Collection<Be_Recruitment>();
            _Connection = Con.fn_Connection();
            SqlCommand _EmployeeCategory = new SqlCommand("select * from paym_Level where status='Y'", _Connection);
            _Connection.Open();
            SqlDataReader dr = _EmployeeCategory.ExecuteReader();
            while (dr.Read())
            {
                Be_Recruitment employeeCategory = new Be_Recruitment();


                employeeCategory.temp_int = (int)dr["pn_LevelID"];
                employeeCategory.temp_string = Convert.IsDBNull(dr["v_LevelName"]) ? "" : (string)dr["v_LevelName"];



                EmployeeCategoryList.Add(employeeCategory);
            }
            return EmployeeCategoryList;
        }
        public Collection<Be_Recruitment> fn_Division1(Be_Recruitment mr)
        {
            Collection<Be_Recruitment> DivisionList = new Collection<Be_Recruitment>();
            _Connection = Con.fn_Connection();
            //string substring = "select * from paym_Division where pn_DivisionID in(select fn_DivisionID from Division where pn_CompanyID=" + mr.CompanyID + " and pn_BranchID=" + mr.BranchID + ") and status='Y'";
            SqlCommand _Division = new SqlCommand("select * from paym_Division where pn_CompanyID=" + mr.CompanyID + " and BranchID=" + mr.BranchID + " and status='Y' ", _Connection);
            _Connection.Open();
            SqlDataReader dr = _Division.ExecuteReader();
            while (dr.Read())
            {
                Be_Recruitment division = new Be_Recruitment();
                division.DivisionID = (int)dr["pn_DivisionID"];
                division.DivisionName = Convert.IsDBNull(dr["v_DivisionName"]) ? "" : (string)dr["v_DivisionName"];
                DivisionList.Add(division);
            }
            return DivisionList;
        }
        public Collection<Be_Recruitment> fn_Division(Be_Recruitment mr)
        {
            Collection<Be_Recruitment> DivisionList = new Collection<Be_Recruitment>();
            _Connection = Con.fn_Connection();
            string substring = "select * from paym_Division where pn_DivisionID in(select fn_DivisionID from Division where pn_CompanyID=" + mr.CompanyID + " and pn_BranchID=" + mr.BranchID + ") and status='Y'";
            SqlCommand _Division = new SqlCommand(substring, _Connection);
            _Connection.Open();
            SqlDataReader dr = _Division.ExecuteReader();
            while (dr.Read())
            {
                Be_Recruitment division = new Be_Recruitment();
                division.DivisionID = (int)dr["pn_DivisionID"];
                division.DivisionName = Convert.IsDBNull(dr["v_DivisionName"]) ? "" : (string)dr["v_DivisionName"];
                DivisionList.Add(division);
            }
            return DivisionList;
        }
        public Collection<Be_Recruitment> fn_Department(Be_Recruitment mr)
        {
            Collection<Be_Recruitment> DepartmentList = new Collection<Be_Recruitment>();
            _Connection = Con.fn_Connection();
            SqlCommand _Department = new SqlCommand("select * from paym_Department where pn_DepartmentID in(select fn_DepartmentID from Department where pn_CompanyID=" + mr.CompanyID + " and pn_BranchID=" + mr.BranchID + ") and status='Y' ", _Connection);
            _Connection.Open();
            SqlDataReader dr = _Department.ExecuteReader();
            while (dr.Read())
            {
                Be_Recruitment department = new Be_Recruitment();
                department.DepartmentID = (int)dr["pn_DepartmentID"];
                department.DepartmentName = Convert.IsDBNull(dr["v_DepartmentName"]) ? "" : (string)dr["v_DepartmentName"];
                DepartmentList.Add(department);
            }
            return DepartmentList;
        }
        public Collection<Be_Recruitment> fn_Department1(Be_Recruitment mr)
        {
            Collection<Be_Recruitment> DepartmentList = new Collection<Be_Recruitment>();
            _Connection = Con.fn_Connection();
            SqlCommand _Department = new SqlCommand("select * from paym_Department where pn_CompanyID=" + mr.CompanyID + " and pn_BranchID=" + mr.BranchID + " and status='Y' order by v_DepartmentName asc ", _Connection);
            _Connection.Open();
            SqlDataReader dr = _Department.ExecuteReader();
            while (dr.Read())
            {
                Be_Recruitment department = new Be_Recruitment();
                department.DepartmentID = (int)dr["pn_DepartmentID"];
                department.DepartmentName = Convert.IsDBNull(dr["v_DepartmentName"]) ? "" : (string)dr["v_DepartmentName"];
                DepartmentList.Add(department);
            }
            return DepartmentList;
        }
        public Collection<Be_Recruitment> fn_Designation(Be_Recruitment mr)
        {
            Collection<Be_Recruitment> DesignationList = new Collection<Be_Recruitment>();
            _Connection = Con.fn_Connection();
            SqlCommand _Designation = new SqlCommand("select * from paym_Designation where pn_DesignationID in(select fn_DesignationID from Designation where pn_CompanyID=" + mr.CompanyID + " and pn_BranchID=" + mr.BranchID + ") and status='Y' ", _Connection);
            _Connection.Open();
            SqlDataReader dr = _Designation.ExecuteReader();
            while (dr.Read())
            {
                Be_Recruitment designation = new Be_Recruitment();
                designation.DesignationID = (int)dr["pn_DesignationID"];
                designation.DesignationName = Convert.IsDBNull(dr["v_DesignationName"]) ? "" : (string)dr["v_DesignationName"];
                DesignationList.Add(designation);
            }
            return DesignationList;
        }
        public Collection<Be_Recruitment> fn_Designation1(Be_Recruitment mr)
        {
            Collection<Be_Recruitment> DesignationList = new Collection<Be_Recruitment>();
            _Connection = Con.fn_Connection();
            SqlCommand _Designation = new SqlCommand("select * from paym_Designation where pn_CompanyID=" + mr.CompanyID + " and BranchID=" + mr.BranchID + " and status='Y' order by v_DesignationName ", _Connection);
            _Connection.Open();
            SqlDataReader dr = _Designation.ExecuteReader();
            while (dr.Read())
            {
                Be_Recruitment designation = new Be_Recruitment();
                designation.DesignationID = (int)dr["pn_DesignationID"];
                designation.DesignationName = Convert.IsDBNull(dr["v_DesignationName"]) ? "" : (string)dr["v_DesignationName"];
                DesignationList.Add(designation);
            }
            return DesignationList;
        }
        public Collection<Be_Recruitment> fn_Grade(Be_Recruitment mr)
        {
            Collection<Be_Recruitment> GradeList = new Collection<Be_Recruitment>();
            _Connection = Con.fn_Connection();
            SqlCommand _Grade = new SqlCommand("select * from paym_Grade where pn_GradeID in(select fn_GradeID from Grade where pn_CompanyID=" + mr.CompanyID + " and pn_BranchID=" + mr.BranchID + ") and status='Y' ", _Connection);
            _Connection.Open();
            SqlDataReader dr = _Grade.ExecuteReader();
            while (dr.Read())
            {
                Be_Recruitment grade = new Be_Recruitment();
                grade.GradeID = (int)dr["pn_GradeID"];
                grade.GradeName = Convert.IsDBNull(dr["v_GradeName"]) ? "" : (string)dr["v_GradeName"];
                GradeList.Add(grade);
            }
            return GradeList;
        }
        public Collection<Be_Recruitment> fn_Grade1(Be_Recruitment mr)
        {
            Collection<Be_Recruitment> GradeList = new Collection<Be_Recruitment>();
            _Connection = Con.fn_Connection();
            SqlCommand _Grade = new SqlCommand("select * from paym_Grade where pn_CompanyID=" + mr.CompanyID + " and BranchID=" + mr.BranchID + " and status='Y' ", _Connection);
            _Connection.Open();
            SqlDataReader dr = _Grade.ExecuteReader();
            while (dr.Read())
            {
                Be_Recruitment grade = new Be_Recruitment();
                grade.GradeID = (int)dr["pn_GradeID"];
                grade.GradeName = Convert.IsDBNull(dr["v_GradeName"]) ? "" : (string)dr["v_GradeName"];
                GradeList.Add(grade);
            }
            return GradeList;
        }
        public Collection<Be_Recruitment> fn_JobStatus(Be_Recruitment mr)
        {
            Collection<Be_Recruitment> JobStatusList = new Collection<Be_Recruitment>();
            _Connection = Con.fn_Connection();
            SqlCommand _JobStatus = new SqlCommand("select * from paym_JobStatus where pn_JobStatusID in(select fn_JobStatusID from JobStatus where pn_CompanyID=" + mr.CompanyID + " and pn_BranchID=" + mr.BranchID + ") and status='Y' ", _Connection);
            _Connection.Open();
            SqlDataReader dr = _JobStatus.ExecuteReader();
            while (dr.Read())
            {
                Be_Recruitment jobStatus = new Be_Recruitment();
                jobStatus.JobStatusID = (int)dr["pn_JobStatusID"];
                jobStatus.JobStatusName = Convert.IsDBNull(dr["v_JobStatusName"]) ? "" : (string)dr["v_JobStatusName"];
                JobStatusList.Add(jobStatus);
            }
            return JobStatusList;
        }
        public Collection<Be_Recruitment> fn_JobStatus1(Be_Recruitment mr)
        {
            Collection<Be_Recruitment> JobStatusList = new Collection<Be_Recruitment>();
            _Connection = Con.fn_Connection();
            SqlCommand _JobStatus = new SqlCommand("select * from paym_JobStatus where pn_CompanyID=" + mr.CompanyID + " and BranchID=" + mr.BranchID + " and status='Y' ", _Connection);
            _Connection.Open();
            SqlDataReader dr = _JobStatus.ExecuteReader();
            while (dr.Read())
            {
                Be_Recruitment jobStatus = new Be_Recruitment();
                jobStatus.JobStatusID = (int)dr["pn_JobStatusID"];
                jobStatus.JobStatusName = Convert.IsDBNull(dr["v_JobStatusName"]) ? "" : (string)dr["v_JobStatusName"];
                JobStatusList.Add(jobStatus);
            }
            return JobStatusList;
        }
        public Collection<Be_Recruitment> fn_Shift(Be_Recruitment mr)
        {
            Collection<Be_Recruitment> ShiftList = new Collection<Be_Recruitment>();
            _Connection = Con.fn_Connection();
            SqlCommand _Shift = new SqlCommand("select * from paym_Shift where pn_ShiftID in(select fn_ShiftID from Shift where pn_CompanyID=" + mr.CompanyID + " and pn_BranchID=" + mr.BranchID + ") and status='Y' ", _Connection);
            _Connection.Open();
            SqlDataReader dr = _Shift.ExecuteReader();
            while (dr.Read())
            {
                Be_Recruitment shift = new Be_Recruitment();
                shift.ShiftTypeID = (int)dr["pn_ShiftID"];
                shift.ShiftTypeName = Convert.IsDBNull(dr["v_ShiftName"]) ? "" : (string)dr["v_ShiftName"];
                ShiftList.Add(shift);
            }
            return ShiftList;
        }
        public Collection<Be_Recruitment> fn_Shift1(Be_Recruitment mr)
        {
            Collection<Be_Recruitment> ShiftList = new Collection<Be_Recruitment>();
            _Connection = Con.fn_Connection();
            SqlCommand _Shift = new SqlCommand("select * from paym_Shift where pn_CompanyID=" + mr.CompanyID + " and BranchID=" + mr.BranchID + " and status='Y' ", _Connection);
            _Connection.Open();
            SqlDataReader dr = _Shift.ExecuteReader();
            while (dr.Read())
            {
                Be_Recruitment shift = new Be_Recruitment();
                shift.ShiftTypeID = (int)dr["pn_ShiftID"];
                shift.ShiftTypeName = Convert.IsDBNull(dr["v_ShiftName"]) ? "" : (string)dr["v_ShiftName"];
                ShiftList.Add(shift);
            }
            return ShiftList;
        }
        public Collection<Be_Recruitment> fn_EmployeeCategory(Be_Recruitment mr)
        {
            Collection<Be_Recruitment> EmployeeCategoryList = new Collection<Be_Recruitment>();
            _Connection = Con.fn_Connection();
            SqlCommand _EmployeeCategory = new SqlCommand("select * from paym_Category where pn_CategoryID in(select fn_CategoryID from Category where pn_CompanyID=" + mr.CompanyID + " and pn_BranchID=" + mr.BranchID + ") and status='Y' ", _Connection);
            _Connection.Open();
            SqlDataReader dr = _EmployeeCategory.ExecuteReader();
            while (dr.Read())
            {
                Be_Recruitment employeeCategory = new Be_Recruitment();


                employeeCategory.CategoryCode = (int)dr["pn_CategoryID"];
                employeeCategory.CategoryName = Convert.IsDBNull(dr["v_CategoryName"]) ? "" : (string)dr["v_CategoryName"];



                EmployeeCategoryList.Add(employeeCategory);
            }
            return EmployeeCategoryList;
        }
        public Collection<Be_Recruitment> fn_EmployeeCategory1(Be_Recruitment mr)
        {
            Collection<Be_Recruitment> EmployeeCategoryList = new Collection<Be_Recruitment>();
            _Connection = Con.fn_Connection();
            SqlCommand _EmployeeCategory = new SqlCommand("select * from paym_Category where pn_CompanyID=" + mr.CompanyID + " and BranchID=" + mr.BranchID + " and status='y' ", _Connection);
            _Connection.Open();
            SqlDataReader dr = _EmployeeCategory.ExecuteReader();
            while (dr.Read())
            {
                Be_Recruitment employeeCategory = new Be_Recruitment();


                employeeCategory.CategoryCode = (int)dr["pn_CategoryID"];
                employeeCategory.CategoryName = Convert.IsDBNull(dr["v_CategoryName"]) ? "" : (string)dr["v_CategoryName"];

                EmployeeCategoryList.Add(employeeCategory);
            }
            return EmployeeCategoryList;
        }
        public Collection<Be_Recruitment> fn_Level(Be_Recruitment mr)
        {
            Collection<Be_Recruitment> EmployeeCategoryList = new Collection<Be_Recruitment>();
            _Connection = Con.fn_Connection();
            SqlCommand _EmployeeCategory = new SqlCommand("select * from paym_Level where pn_LevelID in(select fn_LevelID from Level where pn_CompanyID=" + mr.CompanyID + " and pn_BranchID=" + mr.BranchID + ") and status='Y' ", _Connection);
            _Connection.Open();
            SqlDataReader dr = _EmployeeCategory.ExecuteReader();
            while (dr.Read())
            {
                Be_Recruitment employeeCategory = new Be_Recruitment();


                employeeCategory.temp_int = (int)dr["pn_LevelID"];
                employeeCategory.temp_string = Convert.IsDBNull(dr["v_LevelName"]) ? "" : (string)dr["v_LevelName"];



                EmployeeCategoryList.Add(employeeCategory);
            }
            return EmployeeCategoryList;
        }
        public Collection<Be_Recruitment> fn_Level1(Be_Recruitment mr)
        {
            Collection<Be_Recruitment> EmployeeCategoryList = new Collection<Be_Recruitment>();
            _Connection = Con.fn_Connection();
            SqlCommand _EmployeeCategory = new SqlCommand("select * from paym_Level where pn_CompanyID=" + mr.CompanyID + " and BranchID=" + mr.BranchID + " and status='Y' ", _Connection);
            _Connection.Open();
            SqlDataReader dr = _EmployeeCategory.ExecuteReader();
            while (dr.Read())
            {
                Be_Recruitment employeeCategory = new Be_Recruitment();


                employeeCategory.temp_int = (int)dr["pn_LevelID"];
                employeeCategory.temp_string = Convert.IsDBNull(dr["v_LevelName"]) ? "" : (string)dr["v_LevelName"];



                EmployeeCategoryList.Add(employeeCategory);
            }
            return EmployeeCategoryList;
        }
        public Collection<Be_Recruitment> fn_Project(Be_Recruitment pr)
        {
            Collection<Be_Recruitment> EmployeeCategoryList = new Collection<Be_Recruitment>();
            _Connection = Con.fn_Connection();
            SqlCommand _EmployeeCategory = new SqlCommand("select * from paym_projectsite where pn_projectsiteID in(select fn_projectsiteID from projectsite where pn_CompanyID=" + pr.CompanyID + " and pn_BranchID=" + pr.BranchID + ") and status='Y' ", _Connection);
            _Connection.Open();
            SqlDataReader dr = _EmployeeCategory.ExecuteReader();
            while (dr.Read())
            {
                Be_Recruitment employeeCategory = new Be_Recruitment();


                employeeCategory.ProjectID = (int)dr["pn_projectsiteID"];
                employeeCategory.ProjectName = Convert.IsDBNull(dr["v_projectsiteName"]) ? "" : (string)dr["v_projectsiteName"];



                EmployeeCategoryList.Add(employeeCategory);
            }
            return EmployeeCategoryList;
        }
        public Collection<Be_Recruitment> fn_Project1(Be_Recruitment pr)
        {
            Collection<Be_Recruitment> EmployeeCategoryList = new Collection<Be_Recruitment>();
            _Connection = Con.fn_Connection();
            SqlCommand _EmployeeCategory = new SqlCommand("select * from paym_projectsite where pn_CompanyID=" + pr.CompanyID + " and BranchID=" + pr.BranchID + " and status='Y' ", _Connection);
            _Connection.Open();
            SqlDataReader dr = _EmployeeCategory.ExecuteReader();
            while (dr.Read())
            {
                Be_Recruitment employeeCategory = new Be_Recruitment();


                employeeCategory.ProjectID = (int)dr["pn_projectsiteID"];
                employeeCategory.ProjectName = Convert.IsDBNull(dr["v_projectsiteName"]) ? "" : (string)dr["v_projectsiteName"];



                EmployeeCategoryList.Add(employeeCategory);
            }
            return EmployeeCategoryList;
        }
        public Collection<Be_Recruitment> fn_getCourse( Be_Recruitment re)
        {
            Collection<Be_Recruitment> CourseList = new Collection<Be_Recruitment>();
            _Connection = Con.fn_Connection();
            SqlCommand _Course = new SqlCommand("select * from hrmm_Course where pn_CompanyID='" + re.CompanyID + "' and BranchID='" +re.BranchID + "' and status='Y' ", _Connection);
            _Connection.Open();
            SqlDataReader dr = _Course.ExecuteReader();
            while (dr.Read())
            {
                Be_Recruitment course = new Be_Recruitment();
                course.CourseID = (int)dr["pn_CourseID"];
                course.CourseName = Convert.IsDBNull(dr["v_CourseName"]) ? "" : (string)dr["v_CourseName"];
                CourseList.Add(course);
            }
            return CourseList;
        }
        public Collection<Be_Recruitment>fn_Course(Be_Recruitment cl)
        {
            Collection<Be_Recruitment> CourseList = new Collection<Be_Recruitment>();
            _Connection = Con.fn_Connection();
            SqlCommand _Course = new SqlCommand("select * from hrmm_Course where pn_CourseID in" + cl.temp_string + " and status='Y'", _Connection);
            _Connection.Open();
            SqlDataReader dr = _Course.ExecuteReader();
            while (dr.Read())
            {
                Be_Recruitment course = new Be_Recruitment();
                course.PGCourseID = (int)dr["pn_CourseID"];
                course.PGCourseName = Convert.IsDBNull(dr["v_CourseName"]) ? "" : (string)dr["v_CourseName"];
                course.PGInstutionName = "";
                course.PGPercentage = "";
                course.PGCompletedYear = "";
                CourseList.Add(course);
            }
            return CourseList;
        }
        public Collection<Be_Recruitment> fn_Skills(Be_Recruitment cl)
        {
            Collection<Be_Recruitment> CourseList = new Collection<Be_Recruitment>();
            _Connection = Con.fn_Connection();
            SqlCommand _Course = new SqlCommand("select * from hrmm_SkillsMaster where pn_SkillID in" + cl.temp_string+ " and status='Y'", _Connection);
            _Connection.Open();
            SqlDataReader dr = _Course.ExecuteReader();
            while (dr.Read())
            {
                Be_Recruitment course = new Be_Recruitment();
                course.PGCourseID = (int)dr["pn_SkillID"];
                course.PGCourseName = Convert.IsDBNull(dr["v_SkillName"]) ? "" : (string)dr["v_SkillName"];
                course.PGInstutionName = "";
                course.PGPercentage = "";
                course.PGCompletedYear = "";
                CourseList.Add(course);
            }
            return CourseList;
        }
        public Collection<Be_Recruitment> fn_getSkillsMaster()
        {
            Collection<Be_Recruitment> SkillsMasterList = new Collection<Be_Recruitment>();
            _Connection = Con.fn_Connection();
            SqlCommand _SkillsMaster = new SqlCommand("select * from hrmm_SkillsMaster where status='Y'", _Connection);
            _Connection.Open();
            SqlDataReader dr = _SkillsMaster.ExecuteReader();
            while (dr.Read())
            {
                Be_Recruitment skillsMaster = new Be_Recruitment();
                skillsMaster.SkillID = (int)dr["pn_SkillID"];
                skillsMaster.SkillName = Convert.IsDBNull(dr["v_SkillName"]) ? "" : (string)dr["v_SkillName"];

                c_candidateskills = skillsMaster.SkillID;

                SkillsMasterList.Add(skillsMaster);
            }
            return SkillsMasterList;
        }
        public Collection<Be_Recruitment> fn_getRequisitionNo_Search(Be_Recruitment c_rnos)
        {
            Collection<Be_Recruitment> RequisitionNo_SearchList = new Collection<Be_Recruitment>();
            _Connection = Con.fn_Connection();
            SqlCommand _requisitionNo_search = new SqlCommand("select * from hrmt_Requisition where pn_CompanyID=" + c_rnos.CompanyID + " and pn_RequisitionNo='" + c_rnos.RequisitionNo + "'", _Connection);
            _Connection.Open();
            SqlDataReader dr = _requisitionNo_search.ExecuteReader();
            while (dr.Read())
            {
                Be_Recruitment requisitionNo_search = new Be_Recruitment();
                requisitionNo_search.RequisitionNo = (int)dr["pn_RequisitionNo"];

                requisitionNo_search.ExperienceMin = (int)dr["n_ExperienceMin"];
                requisitionNo_search.ExperienceMax = (int)dr["n_ExperienceMax"];
                requisitionNo_search.AgeGroupMin = (int)dr["n_AgeGroupMin"];
                requisitionNo_search.AgeGroupMax = (int)dr["n_AgeGroupMax"];


                // n_ExperienceMin numeric(4),


                r_expmin = Convert.ToInt32(requisitionNo_search.ExperienceMin);
                r_expmax = Convert.ToInt32(requisitionNo_search.ExperienceMax);
                r_agemin = Convert.ToInt32(requisitionNo_search.AgeGroupMin);
                r_agemax = Convert.ToInt32(requisitionNo_search.AgeGroupMax);



                RequisitionNo_SearchList.Add(requisitionNo_search);

            }
            return RequisitionNo_SearchList;
        }
        //public Collection<Be_Recruitment> fn_getRequisitionNo(Be_Recruitment c_rno)
        //{
        //    Collection<Be_Recruitment> RequisitionNoList = new Collection<Be_Recruitment>();
        //    _Connection = Con.fn_Connection();
        //    SqlCommand _RequisitionNo = new SqlCommand("select * from hrmt_Requisition where pn_CompanyID=" + c_rno.CompanyID + " and pn_BranchID=" + c_rno.BranchID + "", _Connection);
        //    _Connection.Open();
        //    SqlDataReader dr = _RequisitionNo.ExecuteReader();
        //    while (dr.Read())
        //    {
        //        Be_Recruitment requisitionNo = new Be_Recruitment();

        //        requisitionNo.RequisitionNo = (int)dr["pn_RequisitionNo"];
        //        requisitionNo.RequisitionCode = (int)dr["v_RequisitionCode"];


        //        RequisitionNoList.Add(requisitionNo);
        //    }
        //    return RequisitionNoList;
        //}
        //public Collection<Be_Recruitment> fn_getRequisitionNo_Ad(Be_Recruitment ad_rno)
        //{
        //    Collection<Be_Recruitment> RequisitionNoList = new Collection<Be_Recruitment>();
        //    _Connection = Con.fn_Connection();
        //    SqlCommand _RequisitionNo = new SqlCommand("select * from hrmt_Requisition where pn_CompanyID=" + ad_rno.CompanyID + "", _Connection);
        //    _Connection.Open();
        //    SqlDataReader dr = _RequisitionNo.ExecuteReader();
        //    while (dr.Read())
        //    {
        //        Be_Recruitment requisitionNo = new Be_Recruitment();

        //        requisitionNo.RequisitionNo = (int)dr["pn_RequisitionNo"];
        //        requisitionNo.RequisitionCode = (int)dr["v_RequisitionCode"];


        //        RequisitionNoList.Add(requisitionNo);
        //    }
        //    return RequisitionNoList;
        //}
        //public Collection<Be_Recruitment> fn_getSeqNo(Be_Recruitment c_seqno)
        //{
        //    Collection<Be_Recruitment> SeqNoList = new Collection<Be_Recruitment>();
        //    _Connection = Con.fn_Connection();
        //    SqlCommand _seqNo = new SqlCommand("select n_SeqNo from hrmt_Requisition where pn_CompanyID=" + c_seqno.CompanyID + " and pn_RequisitionNo=" + c_seqno.RequisitionNo + "", _Connection);
        //    _Connection.Open();
        //    SqlDataReader dr = _seqNo.ExecuteReader();
        //    while (dr.Read())
        //    {
        //        Be_Recruitment seqNos = new Be_Recruitment();

        //        seqNos.SeqNo = (int)dr["n_SeqNo"];



        //        SeqNoList.Add(seqNos);
        //    }
        //    return SeqNoList;
        //}
        public DataSet fn_getRequiredSkills(Be_Recruitment c_rsk)
        {
            Collection<Be_Recruitment> RequiredSkillsList = new Collection<Be_Recruitment>();
            _Connection = Con.fn_Connection();
            string _RequiredSkills = "select r.pn_SkillSetCode,m.v_SkillName from hrmt_RequiredSkills r,hrmm_skillsmaster m where m.pn_skillid = r.pn_SkillSetCode and r.pn_RequisitionNo=" + c_rsk.RequisitionNo + " and r.pn_CompanyID=" + c_rsk.CompanyID + "";

            SqlDataAdapter _AdRequiedSkills = new SqlDataAdapter(_RequiredSkills, _Connection);
            DataSet _DsRequriedSkils = new DataSet();
            _AdRequiedSkills.Fill(_DsRequriedSkils, "hrmt_RequiredSkills");

            return _DsRequriedSkils;
        }
        public DataSet fn_getRequisitionQualification(Be_Recruitment c_rql)
        {
            Collection<Be_Recruitment> RequisitionQualificationList = new Collection<Be_Recruitment>();
            _Connection = Con.fn_Connection();
            string _RequisitionQualification = "select pn_QualificationCode from hrmt_RequisitionQualification where pn_RequisitionNo=" + c_rql.RequisitionNo + " and pn_CompanyID=" + c_rql.CompanyID + "";

            SqlDataAdapter _AdRequisitionQualification = new SqlDataAdapter(_RequisitionQualification, _Connection);
            DataSet _DsRequisitionQualification = new DataSet();
            //_AdRequiedSkills(_DsRequriedSkil);

            _AdRequisitionQualification.Fill(_DsRequisitionQualification, "hrmt_RequisitionQualification");

            //r_qualification = _DsRequisitionQualification.Tables[0].Rows[0][0];

            return _DsRequisitionQualification;
        }
        public Collection<Be_Recruitment> fn_getCandidateEducation(Be_Recruitment c_ced)
        {
            Collection<Be_Recruitment> CandidateEducationList = new Collection<Be_Recruitment>();
            _Connection = Con.fn_Connection();
            SqlCommand _CandidateEducation = new SqlCommand("select * from hrmm_CandidateEducation  where pn_CompanyID=" + c_ced.CompanyID + "", _Connection);
            _Connection.Open();
            SqlDataReader dr = _CandidateEducation.ExecuteReader();
            while (dr.Read())
            {
                Be_Recruitment candidateEducation = new Be_Recruitment();
                candidateEducation.CourseID = (int)dr["pn_CourseID"];
                //candidateSkills.SkillName = Convert.IsDBNull(dr["v_CourseTypeName"]) ? "" : (string)dr["v_CourseTypeName"];

                c_CandidateEducation = candidateEducation.CourseID;

                CandidateEducationList.Add(candidateEducation);
            }
            return CandidateEducationList;
        }       
        //Resume Sort Listing
        public Collection<Be_Recruitment> fn_get_ResumeSortList(Be_Recruitment c_RSL)
        {

            Collection<Be_Recruitment> ResumeSortListList = new Collection<Be_Recruitment>();
            _Connection = Con.fn_Connection();
            SqlCommand _ResumeSortList = new SqlCommand("select c_Selected from hrmt_ResumeShortlisting where pn_CompanyID=" + c_RSL.CompanyID + " and pn_BranchID=" + c_RSL.BranchID + " and pn_CandidateID=" + c_RSL.CandidateID + "", _Connection);
            _Connection.Open();
            SqlDataReader dr = _ResumeSortList.ExecuteReader();
            while (dr.Read())
            {
                Be_Recruitment resumeSortList = new Be_Recruitment();
                resumeSortList.Status = Convert.ToString(dr["c_Selected"]);
                //resumeSortList.DepartmentName = dr["c_Selected"];


                ResumeSortListList.Add(resumeSortList);
            }
            return ResumeSortListList;
        }   
        //Interview Sheduling
        public Collection<Be_Recruitment> fn_get_Search_InterviewSheduling(Be_Recruitment S_IS)
        {

            Collection<Be_Recruitment> Search_InterviewShedulingList = new Collection<Be_Recruitment>();
            _Connection = Con.fn_Connection();
            SqlCommand _Search_InterviewShedulingList = new SqlCommand("select * from hrmt_InterviewScheduling where pn_CompanyID=" + S_IS.CompanyID + " and pn_BranchID=" + S_IS.BranchID + " and pn_RequisitionNo=" + S_IS.RequisitionNo + " and pn_SeqNo=" + S_IS.SeqNo + " and pn_CandidateID=" + S_IS.CandidateID + "", _Connection);
            _Connection.Open();
            SqlDataReader dr = _Search_InterviewShedulingList.ExecuteReader();
            while (dr.Read())
            {
                Be_Recruitment search_InterviewSheduling = new Be_Recruitment();
                search_InterviewSheduling.CandidateID = (int)dr["pn_CandidateID"];
                search_InterviewSheduling.EmployeeID = (int)dr["pn_EmployeeID"];

                Search_InterviewShedulingList.Add(search_InterviewSheduling);
            }
            return Search_InterviewShedulingList;
        }
        public int fn_GetBranchId_rno(int _reqno)
        {
            _Connection = Con.fn_Connection();
            string _scmd = "select pn_BranchID from hrmt_Requisition where pn_RequisitionNo =" + _reqno + "";
            SqlCommand _Cmd = new SqlCommand(_scmd, _Connection);
            _Connection.Open();
            SqlDataReader dr_Department = _Cmd.ExecuteReader();
            Be_Recruitment rno = new Be_Recruitment();
            while (dr_Department.Read())
            {                
                rno.BranchID = (int)dr_Department["pn_BranchID"];
            }
            return rno.BranchID;
        }
        //popup
        public void popup_is(GridView pop_cgrid, GridView pop_egrid, Be_Recruitment pop)
        {







            DataSet pop_ds = fn_get_IS_can(pop);





            //**************************************candidate popup*****************************************************                


            if (pop_ds.Tables[0].Rows.Count != 0 && pop_cgrid.Rows.Count != 0)
            {



                for (pop_cg = 0; pop_cg < pop_cgrid.Rows.Count; pop_cg++)
                {

                    GridViewRow pop_cand_row = pop_cgrid.Rows[pop_cg];



                    for (pop_ct = 0; pop_ct < pop_ds.Tables[0].Rows.Count; pop_ct++)
                    {




                        if (pop_ds.Tables[0].Rows[pop_ct][0].ToString() == pop_cgrid.DataKeys[pop_cand_row.RowIndex].Value.ToString())
                        {



                            ((CheckBox)pop_cand_row.FindControl("Candidate_select")).BackColor = System.Drawing.Color.Blue;


                            //candidate_row.BackColor = System.Drawing.Color.Blue;                           

                            //((CheckBox)pop_cand_row.FindControl("Candidate_select")).Checked = false;
                            //((CheckBox)pop_cand_row.FindControl("Candidate_select")).Enabled = false;






                            if (pop_ci == 0)
                            {
                                pop_emsg = pop_ds.Tables[0].Rows[pop_ct][1].ToString();

                                pop_ci++;

                            }

                            else
                            {

                                pop_emsg = pop_emsg + "," + pop_ds.Tables[0].Rows[pop_ct][1].ToString();


                                pop_ci++;

                            }


                        }

                    }


                    //assign

                    ((HtmlInputImage)pop_cand_row.FindControl("cand_img")).Value = pop_emsg;

                    pop_emsg = "";

                    pop_ci = 0;


                }
            }




            //**************************************employee popup*****************************************************                





            if (pop_ds.Tables[0].Rows.Count != 0 && pop_egrid.Rows.Count != 0)
            {



                for (pop_eg = 0; pop_eg < pop_egrid.Rows.Count; pop_eg++)
                {

                    GridViewRow pop_emp_row = pop_egrid.Rows[pop_eg];






                    for (pop_et = 0; pop_et < pop_ds.Tables[0].Rows.Count; pop_et++)
                    {




                        if (pop_ds.Tables[0].Rows[pop_et][1].ToString() == pop_egrid.DataKeys[pop_emp_row.RowIndex].Value.ToString())
                        {

                            ((CheckBox)pop_emp_row.FindControl("Employee_select")).BackColor = System.Drawing.Color.Blue;


                            //pop_emp_row.BackColor = System.Drawing.Color.Blue;                           

                            //((CheckBox)pop_emp_row.FindControl("Employee_select")).Checked = false;
                            //((CheckBox)pop_emp_row.FindControl("Employee_select")).Enabled = false;



                            pop_ei++;



                        }

                    }


                    //assign

                    ((HtmlInputImage)pop_emp_row.FindControl("emp_img")).Value = pop_ei.ToString();

                    pop_ei = 0;


                }


            }










            //**************************************end popup***************************************************** 







        }
        public void popup_is1(GridView pop_cgrid, GridView pop_egrid, Be_Recruitment pop)
        {

            DataSet pop_ds = fn_get_IS_can1(pop);

            //**************************************candidate popup*****************************************************                


            if (pop_ds.Tables[0].Rows.Count != 0 && pop_cgrid.Rows.Count != 0)
            {

                for (pop_cg = 0; pop_cg < pop_cgrid.Rows.Count; pop_cg++)
                {

                    GridViewRow pop_cand_row = pop_cgrid.Rows[pop_cg];

                    for (pop_ct = 0; pop_ct < pop_ds.Tables[0].Rows.Count; pop_ct++)
                    {

                        if (pop_ds.Tables[0].Rows[pop_ct][0].ToString() == pop_cgrid.DataKeys[pop_cand_row.RowIndex].Value.ToString())
                        {

                            ((CheckBox)pop_cand_row.FindControl("Candidate_select")).BackColor = System.Drawing.Color.Blue;

                            if (pop_ci == 0)
                            {
                                pop_emsg = pop_ds.Tables[0].Rows[pop_ct][1].ToString();
                            }

                            else
                            {
                                pop_emsg = pop_emsg + "," + pop_ds.Tables[0].Rows[pop_ct][1].ToString();

                                pop_ci++;
                            }

                        }

                    }

                    //assign

                    ((HtmlInputImage)pop_cand_row.FindControl("cand_img")).Value = pop_emsg;

                    pop_emsg = "";

                }
            }




            //**************************************employee popup*****************************************************                





            if (pop_ds.Tables[0].Rows.Count != 0 && pop_egrid.Rows.Count != 0)
            {



                for (pop_eg = 0; pop_eg < pop_egrid.Rows.Count; pop_eg++)
                {

                    GridViewRow pop_emp_row = pop_egrid.Rows[pop_eg];






                    for (pop_et = 0; pop_et < pop_ds.Tables[0].Rows.Count; pop_et++)
                    {




                        if (pop_ds.Tables[0].Rows[pop_et][1].ToString() == pop_egrid.DataKeys[pop_emp_row.RowIndex].Value.ToString())
                        {

                            ((CheckBox)pop_emp_row.FindControl("Employee_select")).BackColor = System.Drawing.Color.Blue;


                            //pop_emp_row.BackColor = System.Drawing.Color.Blue;                           

                            //((CheckBox)pop_emp_row.FindControl("Employee_select")).Checked = false;
                            //((CheckBox)pop_emp_row.FindControl("Employee_select")).Enabled = false;



                            pop_ei++;



                        }

                    }


                    //assign

                    ((HtmlInputImage)pop_emp_row.FindControl("emp_img")).Value = pop_ei.ToString();

                    pop_ei = 0;


                }


            }










            //**************************************end popup***************************************************** 







        }
        public void popup_ia(GridView pop_iagrid, Be_Recruitment pop_ia)
        {




            DataSet r_ds = fn_get_IA_Retrive(pop_ia);



            if (r_ds.Tables[0].Rows.Count != 0)
            {



                for (r_i = 0; r_i < pop_iagrid.Rows.Count; r_i++)
                {


                    GridViewRow row_Rsl = pop_iagrid.Rows[r_i];



                    for (r_j = 0; r_j < r_ds.Tables[0].Rows.Count; r_j++)
                    {


                        if (pop_iagrid.DataKeys[row_Rsl.RowIndex].Value.ToString() == r_ds.Tables[0].Rows[r_j][0].ToString())
                        {

                            //pn_CandidateID,v_Comments,c_CandidateStatus,v_Criteria,v_Rating


                            ((CheckBox)row_Rsl.FindControl("IA_Candidate_select")).BackColor = System.Drawing.Color.Blue;


                            Comments = r_ds.Tables[0].Rows[r_j][1].ToString();
                            Status = Convert.ToString(r_ds.Tables[0].Rows[r_j][2]);
                            Criteria = r_ds.Tables[0].Rows[r_j][3].ToString();
                            Rating = r_ds.Tables[0].Rows[r_j][4].ToString();
                            temp_int = Convert.ToInt32(r_ds.Tables[0].Rows[r_j][5].ToString());

                            //temp1 = "Status :" + r.Status + "\n"+" Rating :" + r.Rating + "\n "+"Criteria :" + r.Criteria + "\n "+"Comments :" + r.Comments + "";

                            temp1 = "Status :" + Status + "\n";
                            temp1 = temp1 + " Rating :" + Rating + "\n " + "Criteria :" + Criteria + "\n " + "Comments :" + Comments + "\n " + " Marks :" + temp_int + "Percentage";

                            ((HtmlInputImage)row_Rsl.FindControl("img_details")).Value = temp1;





                        }


                    }



                }





            }







        }
        public void popup_ir(GridView pop_irgrid, Be_Recruitment pop_ir)
        {



            DataSet r_ds = fn_get_IR_Retrive(pop_ir);



            if (r_ds.Tables[0].Rows.Count != 0)
            {



                for (r_i = 0; r_i < pop_irgrid.Rows.Count; r_i++)
                {


                    GridViewRow row_Rsl = pop_irgrid.Rows[r_i];



                    for (r_j = 0; r_j < r_ds.Tables[0].Rows.Count; r_j++)
                    {


                        if (pop_irgrid.DataKeys[row_Rsl.RowIndex].Value.ToString() == r_ds.Tables[0].Rows[r_j][0].ToString())
                        {


                            ((CheckBox)row_Rsl.FindControl("Result_select")).BackColor = System.Drawing.Color.Blue;


                            FinalStatus = r_ds.Tables[0].Rows[r_j][1].ToString();
                            Comments = r_ds.Tables[0].Rows[r_j][2].ToString();
                            HRComments = r_ds.Tables[0].Rows[r_j][3].ToString();



                            temp1 = "Status :" + FinalStatus + "\n Additional Comments :" + Comments + "\n HR Comments" + HRComments + "\n";


                            ((HtmlInputImage)row_Rsl.FindControl("img_details")).Value = temp1;







                        }


                    }



                }



            }




        }
        public void popup_hr(GridView pop_hrgrid, Be_Recruitment pop_hr)
        {



            DataSet r_ds = fn_get_IR_Retrive(pop_hr);



            if (r_ds.Tables[0].Rows.Count != 0)
            {



                for (r_i = 0; r_i < pop_hrgrid.Rows.Count; r_i++)
                {


                    GridViewRow row_Rsl = pop_hrgrid.Rows[r_i];



                    for (r_j = 0; r_j < r_ds.Tables[0].Rows.Count; r_j++)
                    {


                        if (pop_hrgrid.DataKeys[row_Rsl.RowIndex].Value.ToString() == r_ds.Tables[0].Rows[r_j][0].ToString())
                        {



                            ((CheckBox)row_Rsl.FindControl("RH_select")).BackColor = System.Drawing.Color.Blue;


                            FinalStatus = r_ds.Tables[0].Rows[r_j][1].ToString();
                            Comments = r_ds.Tables[0].Rows[r_j][2].ToString();
                            HRComments = r_ds.Tables[0].Rows[r_j][3].ToString();



                            temp1 = "Status :" + FinalStatus + "\n Additional Comments :" + Comments + "\n HR Comments" + HRComments + "\n";


                            ((HtmlInputImage)row_Rsl.FindControl("img_details")).Value = temp1;



                        }


                    }



                }




            }





        }
        public void popup_rc(GridView pop_rcgrid, Be_Recruitment pop_rc)
        {



            DataSet r_ds = fn_get_RC_Retrive(pop_rc);



            if (r_ds.Tables[0].Rows.Count != 0)
            {



                for (r_i = 0; r_i < pop_rcgrid.Rows.Count; r_i++)
                {


                    GridViewRow row_Rsl = pop_rcgrid.Rows[r_i];



                    for (r_j = 0; r_j < r_ds.Tables[0].Rows.Count; r_j++)
                    {


                        if (pop_rcgrid.DataKeys[row_Rsl.RowIndex].Value.ToString() == r_ds.Tables[0].Rows[r_j][0].ToString())
                        {



                            ((CheckBox)row_Rsl.FindControl("reference_check")).BackColor = System.Drawing.Color.Blue;


                            Comments = r_ds.Tables[0].Rows[r_j][1].ToString();



                            temp1 = "\n Comments :" + Comments + "\n";


                            ((HtmlInputImage)row_Rsl.FindControl("img_details")).Value = temp1;







                        }


                    }



                }









            }



        }
        public void popup_rs(GridView pop_rsgrid, Be_Recruitment pop_rc)
        {

            DataSet r_ds = fn_get_RS_Retrive(pop_rc);


            for (r_i = 0; r_i < pop_rsgrid.Rows.Count; r_i++)
            {


                GridViewRow row_Rsl = pop_rsgrid.Rows[r_i];



                for (r_j = 0; r_j < r_ds.Tables[0].Rows.Count; r_j++)
                {


                    if (pop_rsgrid.DataKeys[row_Rsl.RowIndex].Value.ToString() == r_ds.Tables[0].Rows[r_j][0].ToString())
                    {



                        ((CheckBox)row_Rsl.FindControl("check")).BackColor = System.Drawing.Color.Blue;


                    }


                }



            }






        }
        //to get candidate name
        public DataSet fn_get_CandidateName(Be_Recruitment can_Name)
        {


            _Connection = Con.fn_Connection();


            string _CandidateName = "select v_CandidateName from hrmm_CandidateProfile where pn_CompanyID=" + can_Name.CompanyID + " and pn_BranchID=" + can_Name.BranchID + " and  pn_CandidateID='" + can_Name.temp_int + "'";


            _Connection.Open();


            SqlDataAdapter _Ad_CandidateName = new SqlDataAdapter(_CandidateName, _Connection);

            DataSet _Ds_CandidateName = new DataSet();

            _Ad_CandidateName.Fill(_Ds_CandidateName, "hrmm_CandidateProfile");


            _Connection.Close();


            return _Ds_CandidateName;



        }
        //to get employee code
        public DataSet fn_get_EmployeeCode(Be_Recruitment emp_code)
        {


            _Connection = Con.fn_Connection();


            string _EmployeeCode = "select v_EmployeeCode from paym_Employee where pn_CompanyID=" + emp_code.CompanyID + " and pn_BranchID=" + emp_code.BranchID + " and  pn_EmployeeID='" + emp_code.temp_int + "'";


            _Connection.Open();


            SqlDataAdapter _Ad_EmployeeCode = new SqlDataAdapter(_EmployeeCode, _Connection);

            DataSet _Ds_EmployeeCode = new DataSet();

            _Ad_EmployeeCode.Fill(_Ds_EmployeeCode, "paym_Employee");


            _Connection.Close();


            return _Ds_EmployeeCode;



        }
        //training fist row visible
        public void row_visible(GridView grdrow)
        {


            string rowID = String.Empty;

            string txtID = String.Empty;

            string ddlID = String.Empty;

            int rid;


            for (int grdi = 0; grdi < grdrow.Rows.Count; grdi++)
            {



                if (grdrow.Rows[grdi].RowType == DataControlRowType.DataRow)
                {


                    txtID = ((TextBox)grdrow.Rows[grdi].FindControl("htxt")).ClientID;
                    ddlID = ((DropDownList)grdrow.Rows[grdi].FindControl("ddl")).ClientID;


                    rowID = "cell" + grdrow.Rows[0].RowIndex;



                    //rid = e.Row.RowIndex;

                    grdrow.Rows[grdi].Cells[1].Attributes.Add("id", "cell" + grdrow.Rows[0].RowIndex);
                    grdrow.Rows[grdi].Cells[2].Attributes.Add("id", "cell" + grdrow.Rows[0].RowIndex);


                    // e.Row.Cells[2].Attributes.Add("id", "cell" + ((TextBox)e.Row.FindControl("htxt")).ClientID);
                    //e.Row.Cells[1].Attributes.Add("onclick", "visible1(" + "'" + ddlID + "','" + txtID + "'" + ")");

                    grdrow.Rows[grdi].Cells[1].Attributes.Add("onclick", "visible1('" + txtID + "')");
                    grdrow.Rows[grdi].Cells[2].Attributes.Add("onclick", "visible2('" + ddlID + "')");






                }


            }


        }

    }
}



