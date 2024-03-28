using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.ObjectModel;
using ePayHrms.Connection;
using ePayHrms.Company;
using System.IO;

namespace ePayHrms.Employee
{
    /// <summary>
    /// Summary description for Employee
    /// </summary>
    public class Employee
    {
        public Employee()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        
        private SqlConnection _Connection;
        ePayHrms.Connection.Connection Con = new ePayHrms.Connection.Connection();       
        private int _CompanyId;
        private int _BranchId;
        private int _DivisionId;
        private string _DivisionName;
        private int _DesignationId;
        private string _DesignationName;
        private int _DepartmentId;
        private int _HiddenID;
        private int _SectionId;
        private string _DepartmentName;
        private string _SectionName;
        private string _SectionView;
        private string _SectionEdit;
        private string _SectionDelete;
        private int _CourseId;
        private string _CourseName;
        private int _SkillId;
        private string _SkillName;
        private int _ProjectsiteId;
        private string _ProjectsiteName;
        private string _Location;
        private string _ProjectName;
        private int _CategoryId;
        private string _CategoryName;
        private int _ShiftId;
        private string _ShiftName;
        private string _Shiftcode;
        private string _ShiftInTime;
        private string _ShiftOutTime;
        private int _AllowanceId;
        private string _AllowanceName;
        private int _DeductionId;
        private string _DeductionName;
        private string _DeducationCode;
        private int _AppointmentTypeId;
        private string _AppointmentTypeName;
        private int _LevelId;
        private string _LevelName;
        private int _GradeId;
        private string _GradeName;
        private int _JobStatusId;
        private string _JobStatusName;
        private string _ShiftFrom;
        private string _ShiftTo;
        private string _ShiftCategory;
        private int _EarningsId;
        private string _EarningsName;
        private string _EarningsType;
        private string _ResidenceNo;
        private string _CellNo;
        private int _Amount;
        private DateTime _Date;
        private char _status;
        private int _Rating;
        private string _JobCodeId;
        private int _AssetId;
        private string _Incharge_name;

        private int _ReportId;
        public int ReportID
        {
            get { return _ReportId; }
            set { _ReportId = value; }
        }

        private string _AlterEmailId;
        public string Alternate_EmailId
        {
            get { return _AlterEmailId; }
            set { _AlterEmailId = value; }
        }

        private string _Alt_CellNo;
        public string Alt_CellNo
        {
            get { return _Alt_CellNo; }
            set { _Alt_CellNo = value; }
        }

        private string _Alt_OfficeNo;
        public string Alt_Officeno
        {
            get { return _Alt_OfficeNo; }
            set { _Alt_OfficeNo = value; }
        }

        private string _Alt_Residence;
        public string Alt_ResidenceNo
        {
            get { return _Alt_Residence; }
            set { _Alt_Residence = value; }
        }

        private string _emgAddress;
        public string empaddress
        {
            get { return _emgAddress; }
            set { _emgAddress = value; }
        }

        private string _Address;
        public string Address
        {
            get { return _Address; }
            set { _Address = value; }
        }

        public string Incharge_name
        {
            get { return _Incharge_name; }
            set { _Incharge_name = value; }
        }

        private string _Flag;

        public string Flag
        {
            get { return _Flag; }
            set { _Flag = value; }
        }

        public int AssetId
        {
            get { return _AssetId; }
            set { _AssetId = value; }
        }
        private string _AssetName;

        public string AssetName
        {
            get { return _AssetName; }
            set { _AssetName = value; }
        }

        private string _AssetNo;

        public string AssetNo
        {
            get { return _AssetNo; }
            set { _AssetNo = value; }
        }
        public string JobCodeId
        {
            get { return _JobCodeId; }
            set { _JobCodeId = value; }
        }
        private string _JobCodeName;

        public string JobCodeName
        {
            get { return _JobCodeName; }
            set { _JobCodeName = value; }
        }
        private int _OverHeadingCostId;

        public int OverHeadingCostId
        {
            get { return _OverHeadingCostId; }
            set { _OverHeadingCostId = value; }
        }
        private string _OverHeadingCostName;

        public string OverHeadingCostName
        {
            get { return _OverHeadingCostName; }
            set { _OverHeadingCostName = value; }
        }

        //^^^^^^^^^^^^^^^^^Employee^^^^^^^^^^^^^^^^^^

        private int _EmployeeId;
        private string _EmployeeCode;
        private string _FirstName;
        private string _FullName;
        private string _MiddleName;
        private string _LastName;
        private string _password;
        private string _c_password;
        private int _role;
        private string _img_path;
        private string _Gender;
        private int Id;
        public int Id1
        {
            get { return Id; }
            set { Id = value; }
        }
        private string Status2;

        public string Status21
        {
            get { return Status2; }
            set { Status2 = value; }
        }
        private string Compliant_Text;

        public string Compliant_Text1
        {
            get { return Compliant_Text; }
            set { Compliant_Text = value; }
        }
        private string Compliant_Subject;

        public string Compliant_Subject1
        {
            get { return Compliant_Subject; }
            set { Compliant_Subject = value; }
        }
        private int _Announcement_id1;

        public int Announcement_id1
        {
            get { return _Announcement_id1; }
            set { _Announcement_id1 = value; }
        }
       
        private string Subject;

        public string Subject1
        {
            get { return Subject; }
            set { Subject = value; }
        }
        private string Announcement;

        public string Announcement1
        {
            get { return Announcement; }
            set { Announcement = value; }
        }
        private DateTime _d_birth;
        private DateTime _d_join;
        private DateTime _d_probotion;
        private DateTime _d_extended;
        private DateTime _d_confirmation;
        private DateTime _d_retirement;
        private DateTime _d_renue;
        private DateTime _d_Offer;

        private string _EmailId;
        private string _A_EmailId;
        private string _BloodGroup;
        private string _Religion;
        private string _Nationality;
        private string _HouseNo;
        private string _StreetName;
        private string _AddressLine1;
        private string _AddressLine2;
        private string _City;
        private string _State;
        private string _P_HouseNo;
        private string _P_StreetName;
        private string _P_AddressLine1;
        private string _P_AddressLine2;
        private string _P_City;
        private string _P_State;
        private string _PGCourseName;
        private string _PGSpecialaization;
        private string _PGInstutionName;
        private string _PGPercentage;
        private string _PGCompletedYear;
        private string _PGCompletedinf;
        private int _PGSpecializationId;
        private int _PGCourseID;

        private string _ph_Office;
        private string _ph_Residence;
        private string _Fax;
        private string _specializationName;
        private int _specializationID;
        private string _mode;
        private string _emgname;
        private string _emgno;
        private string _Experience;
        private string _Proficiency;
        private string _temp_str;
        private string _Driver_name;

        public string Driver_name
        {
            get { return _Driver_name; }
            set { _Driver_name = value; }
        }
        private char _Salutation;
        private char _MaritalStatus;
        private string _FatherName;
        private string _MotherName;
        private string _SpouseName;
        private string _Children;
        private string _IDtype;
        private string _IDOthers;
        private string _IDno;
        private string _Ref1_Name;
        private string _Ref1_Phno;
        private string _Ref1_Email;
        private string _Ref1_Relation;
        private string _Ref2_Name;
        private string _Ref2_Phno;
        private string _Ref2_Email;
        private string _Ref2_Relation;
        private string _training_attended;
        private string _training_duration;
        private string _salary;
        private string _position;
        private string _BankCode;
        private string _BankName;
        private string _BranchName;
        private string _AccountType;
        private string _MICRCode;
        private string _IFSCCode;
        private string _BankAddr;
        private string _OtherInfo;
        private string _Reporting;
        private string _PanNo;
        private string _SalaryType;
        private char _TDS;
        private string _CardNo;
        

        public char Salutation
        {
            get { return _Salutation; }
            set { _Salutation = value; }
        }

        public char MaritalStatus
        {
            get { return _MaritalStatus; }
            set { _MaritalStatus = value; }
        }

        public string FatherName
        {
            get { return _FatherName; }
            set { _FatherName = value; }
        }

        public int Role
        {
            get { return _role; }
            set { _role = value; }
        }

        public string MotherName
        {
            get { return _MotherName; }
            set { _MotherName = value; }
        }

        public string SpouseName
        {
            get { return _SpouseName; }
            set { _SpouseName = value; }
        }
        private int _FeedbackID;

        public int FeedbackID
        {
            get { return _FeedbackID; }
            set { _FeedbackID = value; }
        }

        private string _FeedbackQues;

        public string FeedbackQues
        {
            get { return _FeedbackQues; }
            set { _FeedbackQues = value; }
        }

        public string Children
        {
            get { return _Children; }
            set { _Children = value; }
        }

        public string IDtype
        {
            get { return _IDtype; }
            set { _IDtype = value; }
        }

        public string IDOthers
        {
            get { return _IDOthers; }
            set { _IDOthers = value; }
        }
        public string IDno
        {
            get { return _IDno; }
            set { _IDno = value; }
        }

        public string Ref1_Name
        {
            get { return _Ref1_Name; }
            set { _Ref1_Name = value; }
        }

        public string Ref1_Phno
        {
            get { return _Ref1_Phno; }
            set { _Ref1_Phno = value; }
        }

        public string Ref1_Email
        {
            get { return _Ref1_Email; }
            set { _Ref1_Email = value; }
        }

        public string Ref1_Relation
        {
            get { return _Ref1_Relation; }
            set { _Ref1_Relation = value; }
        }

        public string Ref2_Name
        {
            get { return _Ref2_Name; }
            set { _Ref2_Name = value; }
        }

        public string Ref2_Phno
        {
            get { return _Ref2_Phno; }
            set { _Ref2_Phno = value; }
        }

        public string Ref2_Email
        {
            get { return _Ref2_Email; }
            set { _Ref2_Email = value; }
        }

        public string Ref2_Relation
        {
            get { return _Ref2_Relation; }
            set { _Ref2_Relation = value; }
        }

        public string training_attended
        {
            get { return _training_attended; }
            set { _training_attended = value; }
        }

        public string training_duration
        {
            get { return _training_duration; }
            set { _training_duration = value; }
        }

        public string salary
        {
            get { return _salary; }
            set { _salary = value; }
        }

        private string _CTC;
        public string CTC
        {
            get { return _CTC; }
            set { _CTC = value; }
        }

        public string position
        {
            get { return _position; }
            set { _position = value; }
        }

        public string Bank_Code
        {
            get { return _BankCode; }
            set { _BankCode = value; }
        }

        public string Bank_Name
        {
            get { return _BankName; }
            set { _BankName = value; }
        }

        public string Branch_Name
        {
            get { return _BranchName; }
            set { _BranchName = value; }
        }

        public string Account_Type
        {
            get { return _AccountType; }
            set { _AccountType = value; }
        }

        public string MICR_Code
        {
            get { return _MICRCode; }
            set { _MICRCode = value; }
        }

        public string IFSC_Code
        {
            get { return _IFSCCode; }
            set { _IFSCCode = value; }
        }

        public string Bank_Addr
        {
            get { return _BankAddr; }
            set { _BankAddr = value; }
        }

        public string Other_Info
        {
            get { return _OtherInfo; }
            set { _OtherInfo = value; }
        }

        public string Reporting
        {
            get { return _Reporting; }
            set { _Reporting = value; }
        }

        private string _ReportingEmail;
        public string ReportingEmail
        {
            get { return _ReportingEmail; }
            set { _ReportingEmail = value; }
        }

        public string Pan_no
        {
            get { return _PanNo; }
            set { _PanNo = value; }
        }

        public string Salary_Type
        {
            get { return _SalaryType; }
            set { _SalaryType = value; }
        }

        public char TDS
        {
            get { return _TDS; }
            set { _TDS = value; }
        }

        public string CardNo
        {
            get { return _CardNo; }
            set { _CardNo = value; }
        }

        public int Rating
        {
            get { return _Rating; }
            set { _Rating = value; }
        }

        //******************************** Training ******************************************************

        private int _InstitutionId;
        private string _InstitutionName;
        private string _websitename;
        private int _prgmtypId;
        private string _prgmtypName;
        private int _trnrID;
        private string _trnrName;
        private string _trnrExp;
        private string _trnrSkill;
        private string _trnrWorkType;
        private int _TrainingID;
        private string _DurationFrom;
        private string _DurationTo;
        private int _prgmId;
        private string _prgmname;
        private string _P_Country;
        private int _Route_ID;
        private string _Routes;
        private int _temp_emp_id;
        private string _Boarding_point;
        private string _Driver_id;

        public string Driver_id
        {
            get { return _Driver_id; }
            set { _Driver_id = value; }
        }
        public string Boarding_point
        {
            get { return _Boarding_point; }
            set { _Boarding_point = value; }
        }
        public int Temp_emp_id
        {
            get { return _temp_emp_id; }
            set { _temp_emp_id = value; }
        }
        public string Routes
        {
            get { return _Routes; }
            set { _Routes = value; }
        }
        public int Route_ID
        {
            get { return _Route_ID; }
            set { _Route_ID = value; }
        }
        private DateTime _CurrentDate;
        private string _ItemCode;
        private string _Item;
        private string _Reason;
        private int _Role;
        private int _Area_id;
        private string _Area_name;
        private string _Veh_number;
        private string _Veh_id;
        private string _Veh_type;
        private int _Veh_capacity;
        private string _Vehicle;

        public string Vehicle
        {
            get { return _Vehicle; }
            set { _Vehicle = value; }
        }
        public int Veh_capacity
        {
            get { return _Veh_capacity; }
            set { _Veh_capacity = value; }
        }
        public string Veh_type
        {
            get { return _Veh_type; }
            set { _Veh_type = value; }
        }
        public string Veh_id
        {
            get { return _Veh_id; }
            set { _Veh_id = value; }
        }
        public string Veh_number
        {
            get { return _Veh_number; }
            set { _Veh_number = value; }
        }
        public string Area_name
        {
            get { return _Area_name; }
            set { _Area_name = value; }
        }
        public int Area_id
        {
            get { return _Area_id; }
            set { _Area_id = value; }
        }
        //######################################################################################

        private string _TaskSubject;
        private string _TaskDescription;
        private char _Priority;

        public string TaskSubject
        {
            get { return _TaskSubject; }
            set { _TaskSubject = value; }

        }
        public string TaskDescription
        {
            get { return _TaskDescription; }
            set { _TaskDescription = value; }
        }

        public char Priority
        {
            get { return _Priority; }
            set { _Priority = value; }
        }

        public string websitename
        {
            get { return _websitename; }
            set { _websitename = value; }
        }

        public int prgmtypId
        {
            get { return _prgmtypId; }
            set { _prgmtypId = value; }
        }

        public int prgmid
        {
            get { return _prgmId; }
            set { _prgmId = value; }
        }

        public string prgmtypName
        {
            get { return _prgmtypName; }
            set { _prgmtypName = value; }
        }
        public string Gender
        {
            get { return _Gender; }
            set { _Gender = value; }
        }

        public string prgmname
        {
            get { return _prgmname; }
            set { _prgmname = value; }
        }

        public int TrainingID
        {
            get { return _TrainingID; }
            set { _TrainingID = value; }
        }

        public string DurationFrom
        {
            get { return _DurationFrom; }
            set { _DurationFrom = value; }
        }

        public string DurationTo
        {
            get { return _DurationTo; }
            set { _DurationTo = value; }
        }

        public string trnrExp
        {
            get { return _trnrExp; }
            set { _trnrExp = value; }
        }

        public string trnrSkill
        {
            get { return _trnrSkill; }
            set { _trnrSkill = value; }
        }

        public string trnrWorkType
        {
            get { return _trnrWorkType; }
            set { _trnrWorkType = value; }
        }

        public int trnrID
        {
            get { return _trnrID; }
            set { _trnrID = value; }
        }

        public string trnrName
        {
            get { return _trnrName; }
            set { _trnrName = value; }
        }

        public int InstitutionId
        {
            get { return _InstitutionId; }
            set { _InstitutionId = value; }
        }

        public string InstitutionName
        {
            get { return _InstitutionName; }
            set { _InstitutionName = value; }
        }


        //******************************************************************************************
         public string temp_str
        {
            get { return _temp_str; }
            set { _temp_str = value; }
        }

        public int CompanyId
        {
            get { return _CompanyId; }
            set { _CompanyId = value; }
        }

        public int BranchId
        {
            get { return _BranchId; }
            set { _BranchId = value; }
        }

        public int DivisionId
        {
            get { return _DivisionId; }
            set { _DivisionId = value; }
        }

        public string DivisionName
        {
            get { return _DivisionName; }
            set { _DivisionName = value; }
        }

        public int DesignationId
        {
            get { return _DesignationId; }
            set { _DesignationId = value; }
        }

        public string DesignationName
        {
            get { return _DesignationName; }
            set { _DesignationName = value; }
        }

        private string _DesigName;
        public string DesgName
        {
            get { return _DesigName; }
            set { _DesigName = value; }
        }

        public int DepartmentId
        {
            get { return _DepartmentId; }
            set { _DepartmentId = value; }
        }

        private string _Department_Name;
        public string Department_Name
        {
            get { return _Department_Name; }
            set { _Department_Name = value; }
        }
        public int HiddenID
        {
            get { return _HiddenID; }
            set { _HiddenID = value; }
        }

        public string DepartmentName
        {
            get { return _DepartmentName; }
            set { _DepartmentName = value; }
        }

        public int SectionId
        {
            get { return _SectionId; }
            set { _SectionId = value; }
        }

        public string SectionName
        {
            get { return _SectionName; }
            set { _SectionName = value; }
        }

        public string SectionView
        {
            get { return _SectionView; }
            set { _SectionView = value; }
        }

        public string SectionEdit
        {
            get { return _SectionEdit; }
            set { _SectionEdit = value; }
        }

        public string SectionDelete
        {
            get { return _SectionDelete; }
            set { _SectionDelete = value; }
        }

        public int CourseId
        {
            get { return _CourseId; }
            set { _CourseId = value; }
        }
        public string CourseName
        {
            get { return _CourseName; }
            set { _CourseName = value; }
        }
        public int ProjectsiteId
        {
            get { return _ProjectsiteId; }
            set { _ProjectsiteId = value; }
        }
        public string ProjectsiteName
        {
            get { return _ProjectsiteName; }
            set { _ProjectsiteName = value; }
        }

        public string Location
        {
            get { return _Location; }
            set { _Location = value; }
        }

        public string ProjectName
        {
            get { return _ProjectName; }
            set { _ProjectName = value; }
        }

        public int CategoryId
        {
            get { return _CategoryId; }
            set { _CategoryId = value; }
        }

        public string CategoryName
        {
            get { return _CategoryName; }
            set { _CategoryName = value; }
        }

        public int ShiftId
        {
            get { return _ShiftId; }
            set { _ShiftId = value; }
        }

        public string ShiftName
        {
            get { return _ShiftName; }
            set { _ShiftName = value; }
        }
        public string ShiftCode
        {
            get { return _Shiftcode; }
            set { _Shiftcode = value; }
        }

        public string ShiftIntime
        {
            get { return _ShiftInTime; }
            set { _ShiftInTime = value; }
        }

        public string ShiftOutTime
        {
            get { return _ShiftOutTime; }
            set { _ShiftOutTime = value; }
        }

        public int AllowanceId
        {
            get { return _AllowanceId; }
            set { _AllowanceId = value; }
        }

        public string AllowanceName
        {
            get { return _AllowanceName; }
            set { _AllowanceName = value; }
        }

        public int DeductionId
        {
            get { return _DeductionId; }
            set { _DeductionId = value; }
        }

        public string DeductionName
        {
            get { return _DeductionName; }
            set { _DeductionName = value; }
        }


        public string DeducationCode
        {
            get { return _DeducationCode; }
            set { _DeducationCode = value; }
        }


        public int AppointmentTypeId
        {
            get { return _AppointmentTypeId; }
            set { _AppointmentTypeId = value; }
        }

        public string AppointmentTypeName
        {
            get { return _AppointmentTypeName; }
            set { _AppointmentTypeName = value; }
        }

        public int LevelId
        {
            get { return _LevelId; }
            set { _LevelId = value; }
        }

        public string LevelName
        {
            get { return _LevelName; }
            set { _LevelName = value; }
        }

        public int GradeId
        {
            get { return _GradeId; }
            set { _GradeId = value; }
        }

        public string GradeName
        {
            get { return _GradeName; }
            set { _GradeName = value; }
        }

        public int JobStatusId
        {
            get { return _JobStatusId; }
            set { _JobStatusId = value; }
        }

        public string JobStatusName
        {
            get { return _JobStatusName; }
            set { _JobStatusName = value; }
        }

        public string ShiftFrom
        {
            get { return _ShiftFrom; }
            set { _ShiftFrom = value; }
        }

        public string ShiftTo
        {
            get { return _ShiftTo; }
            set { _ShiftTo = value; }
        }

        public string ShiftCategory
        {
            get { return _ShiftCategory; }
            set { _ShiftCategory = value; }
        }

        public int EarningsId
        {
            get { return _EarningsId; }
            set { _EarningsId = value; }
        }

        public string EarningsName
        {
            get { return _EarningsName; }
            set { _EarningsName = value; }
        }

        public string EarningsType
        {
            get { return _EarningsType; }
            set { _EarningsType = value; }
        }


        public int Amount
        {
            get { return _Amount; }
            set { _Amount = value; }
        }


        public DateTime Date
        {
            get { return _Date; }
            set { _Date = value; }
        }


        //****************************************************************************************************


        public int EmployeeId
        {
            get { return _EmployeeId; }
            set { _EmployeeId = value; }
        }

        public string EmployeeCode
        {
            get { return _EmployeeCode; }
            set { _EmployeeCode = value; }
        }

        public DateTime Dates { get; private set; }

        public string FullName
        {
            get { return _FullName; }
            set { _FullName = value; }
         }


        public string FirstName
        {
            get { return _FirstName; }
            set { _FirstName = value; }
        }

        public string MiddleName
        {
            get { return _MiddleName; }
            set { _MiddleName = value; }
        }

        public string LastName
        {
            get { return _LastName; }
            set { _LastName = value; }
        }

        public DateTime d_birth
        {
            get { return _d_birth; }
            set { _d_birth = value; }
        }

        public DateTime d_join
        {
            get { return _d_join; }
            set { _d_join = value; }
        }




        public string password
        {
            get { return _password; }
            set { _password = value; }
        }

        public string c_password
        {
            get { return _c_password; }
            set { _c_password = value; }
        }


        public DateTime d_probotion
        {
            get { return _d_probotion; }
            set { _d_probotion = value; }
        }

        public DateTime d_extended
        {
            get { return _d_extended; }
            set { _d_extended = value; }
        }

        public DateTime d_confirmation
        {
            get { return _d_confirmation; }
            set { _d_confirmation = value; }
        }

        public DateTime d_retirement
        {
            get { return _d_retirement; }
            set { _d_retirement = value; }
        }

        public DateTime d_renue
        {
            get { return _d_renue; }
            set { _d_renue = value; }
        }

        public string EmailId
        {
            get { return _EmailId; }
            set { _EmailId = value; }
        }

        public string A_EmailId
        {
            get { return _A_EmailId; }
            set { _A_EmailId = value; }
        }

        public string BloodGroup
        {
            get { return _BloodGroup; }
            set { _BloodGroup = value; }
        }

        public string Religion
        {
            get { return _Religion; }
            set { _Religion = value; }
        }

        public string Nationality
        {
            get { return _Nationality; }
            set { _Nationality = value; }
        }

        public string HouseNo
        {
            get { return _HouseNo; }
            set { _HouseNo = value; }
        }

        public string StreetName
        {
            get { return _StreetName; }
            set { _StreetName = value; }
        }

        public string AddressLine1
        {
            get { return _AddressLine1; }
            set { _AddressLine1 = value; }
        }

        public string AddressLine2
        {
            get { return _AddressLine2; }
            set { _AddressLine2 = value; }
        }

        public string City
        {
            get { return _City; }
            set { _City = value; }
        }

        public string State
        {
            get { return _State; }
            set { _State = value; }
        }



        public string p_HouseNo
        {
            get { return _P_HouseNo; }
            set { _P_HouseNo = value; }
        }

        public string p_StreetName
        {
            get { return _P_StreetName; }
            set { _P_StreetName = value; }
        }

        public string P_AddressLine1
        {
            get { return _P_AddressLine1; }
            set { _P_AddressLine1 = value; }
        }

        public string P_AddressLine2
        {
            get { return _P_AddressLine2; }
            set { _P_AddressLine2 = value; }
        }

        public string P_City
        {
            get { return _P_City; }
            set { _P_City = value; }
        }

        public string P_State
        {
            get { return _P_State; }
            set { _P_State = value; }
        }



        public string P_Country
        {
            get { return _P_Country; }
            set { _P_Country = value; }
        }


        public string PGCourseName
        {
            get { return _PGCourseName; }
            set { _PGCourseName = value; }
        }

        public string PGSpecialaization
        {
            get { return _PGSpecialaization; }
            set { _PGSpecialaization = value; }
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


        public string PGCompletedinf
        {
            get { return _PGCompletedinf; }
            set { _PGCompletedinf = value; }
        }


        public string ResidenceNo
        {
            get { return _ResidenceNo; }
            set { _ResidenceNo = value; }
        }

        public string CellNo
        {
            get { return _CellNo; }
            set { _CellNo = value; }
        }


        public int PGSpecializationId
        {
            get { return _PGSpecializationId; }
            set { _PGSpecializationId = value; }
        }

        public int PGCourseID
        {
            get { return _PGCourseID; }
            set { _PGCourseID = value; }

        }



        public string img_path
        {
            get { return _img_path; }
            set { _img_path = value; }

        }


        public DateTime d_Offer
        {
            get { return _d_Offer; }
            set { _d_Offer = value; }

        }


        public string ph_Office
        {
            get { return _ph_Office; }
            set { _ph_Office = value; }

        }

        public string ph_Residence
        {
            get { return _ph_Residence; }
            set { _ph_Residence = value; }

        }

        public string Fax
        {
            get { return _Fax; }
            set { _Fax = value; }

        }

        public string specializationName
        {
            get { return _specializationName; }
            set { _specializationName = value; }

        }

        public int specializationID
        {
            get { return _specializationID; }
            set { _specializationID = value; }

        }

        public string mode
        {
            get { return _mode; }
            set { _mode = value; }

        }

        public string emgname
        {
            get { return _emgname; }
            set { _emgname = value; }

        }

        public string emgno
        {
            get { return _emgno; }
            set { _emgno = value; }

        }
        public int SkillId
        {
            get { return _SkillId; }
            set { _SkillId = value; }
        }
        public string SkillName
        {
            get { return _SkillName; }
            set { _SkillName = value; }

        }
        public string Experience
        {
            get { return _Experience; }
            set { _Experience = value; }
        }
        public string Proficiency
        {
            get { return _Proficiency; }
            set { _Proficiency = value; }
        }
        public char status
        {
            get { return _status; }
            set { _status = value; }
        }



        public string Item
        {
            get { return _Item; }
            set { _Item = value; }

        }

        public string ItemCode
        {
            get { return _ItemCode; }
            set { _ItemCode = value; }

        }

        public string Reason
        {
            get { return _Reason; }
            set { _Reason = value; }

        }

        public DateTime CurrentDate
        {
            get { return _CurrentDate; }
            set { _CurrentDate = value; }

        }
        private int _ReaderId;

        public int ReaderId
        {
            get { return _ReaderId; }
            set { _ReaderId = value; }
        }
        private char _OT_Eligible;
        public char OT_Eligible
        {
            get { return _OT_Eligible; }
            set { _OT_Eligible = value; }
        }
        
        
        private string _PFno;
        public string PFno
        {
            get { return _PFno; }
            set { _PFno = value; }
        }
        private string _ESIno;

        public string ESIno
        {
            get { return _ESIno; }
            set { _ESIno = value; }
        }
          private string _basic;

        public string basic
        {
            get { return _basic; }
            set { _basic = value; }
        }

        private double _basic_salary;

        public double basic_salary
        {
            get { return _basic_salary; }
            set { _basic_salary = value; }
        }

        private double _CTC_salary;

        public double CTC_salary
        {
            get { return _CTC_salary; }
            set { _CTC_salary = value; }
        }

        private double _inc_value;

        public double inc_value
        {
            get { return _inc_value; }
            set { _inc_value = value; }
        }

        private double _OT_calc;

        public double OT_calc
        {
            get { return _OT_calc; }
            set { _OT_calc = value; }
        }


        private int _Netpay;

        public int Netpay
        {
            get { return _Netpay; }
            set { _Netpay = value; }
        }

        private double _ESI_EMP;

        public double ESI_EMP
        {
            get { return _ESI_EMP; }
            set { _ESI_EMP = value; }
        }

        private double _ESI_EMPR;

        public double ESI_EMPR
        {
            get { return _ESI_EMPR; }
            set { _ESI_EMPR = value; }
        }

        private int _Count;

        public int Count
        {
            get { return _Count; }
            set { _Count = value; }
        }
        private double _Paiddays;

        public double Paiddays
        {
            get { return _Paiddays; }
            set { _Paiddays = value; }
        }

        private string _d_Date;

        public string d_Date
        {
            get { return _d_Date; }
            set { _d_Date = value; }
        }
        public string _e_date;
        public string e_date
        {
            get { return _e_date; }
            set { _e_date = value; }
        }
        private DateTime _dat;

        public DateTime dat
        {
            get { return _dat; }
            set { _dat = value; }
        }
        private DateTime _Intime;

        public DateTime Intime
        {
            get { return _Intime; }
            set { _Intime = value; }
        }

        private DateTime _Outtime;

        public DateTime Outtime
        {
            get { return _Outtime; }
            set { _Outtime = value; }
        }

        private DateTime _Latein;

        public DateTime Latein
        {
            get { return _Latein; }
            set { _Latein = value; }
        }

        private DateTime _Lateout;

        public DateTime Lateout
        {
            get { return _Lateout; }
            set { _Lateout = value; }
        }

        private DateTime _Earlyout;

        public DateTime Earlyout
        {
            get { return _Earlyout; }
            set { _Earlyout = value; }
        }
        private DateTime _whours;

        
        public DateTime whours

        {
            get { return _whours; }
            set{ _whours = value; }
        }
        private string _dept;
        public string dept
        {
            get { return _dept; }
            set { _dept = value; }
        }
        private string _shiftcode;
        
        public string shiftcode

        {
            get { return _shiftcode; }
            set { _shiftcode = value; }
        }
            private DateTime _dates;

        public DateTime dates

        {
            get { return _dates; }
            set { _dates = value; }
        }
        
        private string _data;
        public string data
        {
            get { return _data; }
            set { _data = value; }
        }

        public string depid { get; private set; }
        public string depname { get; private set; }
        public object e { get; private set; }

        //##########################################################################################
        public string _leave_Code;
        public int _leave_id;
        public string _leave_name;
        public DateTime _from_date;
        public DateTime _to_date;
        public DateTime _Submit_date;
        public int _yearend;
        public string _approve;
        public string _reason;
        public string _priority;
        public string _from_status;
        public string _to_status;
        public double _day;
        public string leave_Code
        {
            get { return _leave_Code; }
            set { _leave_Code = value; }
        }
        public int leave_id
        {
            get { return _leave_id; }
            set { _leave_id = value; }
        }
        public string leave_name
        {
            get { return _leave_name; }
            set { _leave_name = value; }
        }
        public DateTime from_date
        {
            get { return _from_date; }
            set { _from_date = value; }
        }
        public DateTime to_date
        {
            get { return _to_date; }
            set { _to_date = value; }
        }
        public DateTime Submit_date
        {
            get { return _Submit_date; }
            set { _Submit_date = value; }
        }
        public string approve
        {
            get { return _approve; }
            set { _approve = value; }

        }
        public int yearend
        {
            get { return _yearend; }
            set { _yearend = value; }

        }
        public string reason
        {
            get { return _reason; }
            set { _reason = value; }
        }
        public string priority
        {
            get { return _priority; }
            set { _priority = value; }
        }
        public string from_status
        {
            get { return _from_status; ; }
            set { _from_status = value; }
        }
        
        public string to_status
        {
            get { return _to_status; }
            set { _to_status = value; }
        }
        public double day
        {
            get { return _day; }
            set { _day = value; }
        }
        //##########################################################################################
        public Collection<Employee> EmployeeDesignation(Employee e)
        {
            Collection<Employee> EmployeeDesignationList=new Collection<Employee>();
            _Connection = Con.fn_Connection();
            SqlCommand cmd = new SqlCommand("select * from paym_employee_profile1 where pn_CompanyID='" + e.CompanyId + "' and pn_BranchID='" + e.BranchId + "' and pn_EmployeeID='" +e.EmployeeId + "'", _Connection);
            _Connection.Open();
            SqlDataReader dr =cmd.ExecuteReader();
            while (dr.Read())
            {   Employee emp = new Employee();
                emp.EmployeeId = Convert.ToInt32(dr["pn_EmployeeID"]);
                emp.DesignationId = Convert.ToInt32(dr["pn_DesingnationId"]);
                EmployeeDesignationList.Add(emp);
            }
            return EmployeeDesignationList;           
        }

        public Collection<Employee> fn_getAllEmployees()
        {            
            Collection<Employee> EmployeeList = new Collection<Employee>();
            _Connection = Con.fn_Connection();
            SqlCommand _Course = new SqlCommand("select * from paym_Employee where status='Y'", _Connection);
            _Connection.Open();
            SqlDataReader dr = _Course.ExecuteReader();
            while (dr.Read())
            {
                Employee employee = new Employee();                
                employee.EmployeeId = Convert.ToInt32(dr["pn_EmployeeID"]);
                employee.EmployeeCode = Convert.IsDBNull(dr["EmployeeCode"]) ? "" : (string)dr["EmployeeCode"];
                employee.FirstName = Convert.IsDBNull(dr["Employee_First_Name"]) ? "" : (string)dr["Employee_First_Name"];
                employee.LastName = (string)dr["EmployeeCode"] + " - " + (string)dr["Employee_First_Name"];
                employee.basic_salary = (double)dr["basic_salary"];
                employee.CTC_salary = (double)dr["CTC"];
                EmployeeList.Add(employee);
            }
            return EmployeeList;
        }

        public Collection<Employee> fn_getOldEmployees()
        {
            Collection<Employee> EmployeeList = new Collection<Employee>();
            _Connection = Con.fn_Connection();
            SqlCommand _Course = new SqlCommand("select * from paym_Employee where status='N'", _Connection);
            _Connection.Open();
            SqlDataReader dr = _Course.ExecuteReader();
            while (dr.Read())
            {
                Employee employee = new Employee();
                employee.EmployeeId = (int)dr["pn_EmployeeID"];
                employee.EmployeeCode = Convert.IsDBNull(dr["EmployeeCode"]) ? "" : (string)dr["EmployeeCode"];
                employee.FirstName = Convert.IsDBNull(dr["Employee_First_Name"]) ? "" : (string)dr["Employee_First_Name"];

                employee.LastName = (string)dr["EmployeeCode"] + " - " + (string)dr["Employee_First_Name"];

                EmployeeList.Add(employee);
            }
            return EmployeeList;
        }



        //################################################################################################################


        public string Employee_First(Employee e)
        {
            try
            {
                _Connection = Con.fn_Connection();
                SqlCommand _Cmd = new SqlCommand("sp_paym_Employee", _Connection);
                _Cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter[] _ISPParamter = new SqlParameter[32];
                _ISPParamter[0] = new SqlParameter("@pn_CompanyID", SqlDbType.Int);
                _ISPParamter[0].Value = e.CompanyId;
                _ISPParamter[1] = new SqlParameter("@pn_BranchID", SqlDbType.Int);
                _ISPParamter[1].Value = e.BranchId;
                _ISPParamter[2] = new SqlParameter("@pn_EmployeeID", SqlDbType.Int);
                _ISPParamter[2].Value = e.EmployeeId;
                _ISPParamter[3] = new SqlParameter("@EmployeeCode", SqlDbType.VarChar);
                _ISPParamter[3].Value = e.EmployeeCode;
                _ISPParamter[4] = new SqlParameter("@Employee_First_Name", SqlDbType.VarChar);
                _ISPParamter[4].Value = e.FirstName;
                _ISPParamter[5] = new SqlParameter("@Employee_Middle_Name", SqlDbType.VarChar);
                _ISPParamter[5].Value = e.MiddleName;
                _ISPParamter[6] = new SqlParameter("@Employee_Last_Name", SqlDbType.VarChar);
                _ISPParamter[6].Value = e.LastName;
                _ISPParamter[7] = new SqlParameter("@DateofBirth", SqlDbType.DateTime);
                _ISPParamter[7].Value = e.d_birth;
                _ISPParamter[8] = new SqlParameter("@Password", SqlDbType.VarChar);
                _ISPParamter[8].Value = e.password;
                _ISPParamter[9] = new SqlParameter("@Gender", SqlDbType.VarChar);
                _ISPParamter[9].Value = e.Gender;
                _ISPParamter[10] = new SqlParameter("@status", SqlDbType.Char);
                _ISPParamter[10].Value = e.status;
                _ISPParamter[11] = new SqlParameter("@Employee_Full_Name", SqlDbType.VarChar);
                _ISPParamter[11].Value = e.FullName;
                _ISPParamter[12] = new SqlParameter("@Readerid", SqlDbType.Int);
                _ISPParamter[12].Value = e.ReaderId;
                _ISPParamter[13] = new SqlParameter("@OT_Eligible", SqlDbType.Char);
                _ISPParamter[13].Value = e.OT_Eligible;
                _ISPParamter[14] = new SqlParameter("@Pfno", SqlDbType.VarChar);
                _ISPParamter[14].Value = e.PFno;
                _ISPParamter[15] = new SqlParameter("@Esino", SqlDbType.VarChar);
                _ISPParamter[15].Value = e.ESIno;
                _ISPParamter[16] = new SqlParameter("@OT_calc", SqlDbType.Float);
                _ISPParamter[16].Value = e.OT_calc;
                _ISPParamter[17] = new SqlParameter("@CTC", SqlDbType.VarChar);
                _ISPParamter[17].Value = e.CTC;
                _ISPParamter[18] = new SqlParameter("@basic_salary", SqlDbType.VarChar);
                _ISPParamter[18].Value = e.basic;
                _ISPParamter[19] = new SqlParameter("@Bank_code", SqlDbType.VarChar);
                _ISPParamter[19].Value = e.Bank_Code;
                _ISPParamter[20] = new SqlParameter("@Bank_Name", SqlDbType.VarChar);
                _ISPParamter[20].Value = e.Bank_Name;
                _ISPParamter[21] = new SqlParameter("@Branch_Name", SqlDbType.VarChar);
                _ISPParamter[21].Value = e.Branch_Name;
                _ISPParamter[22] = new SqlParameter("@Account_type", SqlDbType.VarChar);
                _ISPParamter[22].Value = e.Account_Type;
                _ISPParamter[23] = new SqlParameter("@MICR_code", SqlDbType.VarChar);
                _ISPParamter[23].Value = e.MICR_Code;
                _ISPParamter[24] = new SqlParameter("@IFSC_code", SqlDbType.VarChar);
                _ISPParamter[24].Value = e.IFSC_Code;
                _ISPParamter[25] = new SqlParameter("@Address", SqlDbType.VarChar);
                _ISPParamter[25].Value = e.Bank_Addr;
                _ISPParamter[26] = new SqlParameter("@Other_info", SqlDbType.VarChar);
                _ISPParamter[26].Value = e.Other_Info;
                _ISPParamter[27] = new SqlParameter("@reporting_person", SqlDbType.VarChar);
                _ISPParamter[27].Value = e.Reporting;
                _ISPParamter[28] = new SqlParameter("@Pan_no", SqlDbType.VarChar);
                _ISPParamter[28].Value = e.Pan_no;
                _ISPParamter[29] = new SqlParameter("@salary_type", SqlDbType.VarChar);
                _ISPParamter[29].Value = e.Salary_Type;
                _ISPParamter[30] = new SqlParameter("@TDS_Applicable", SqlDbType.Char);
                _ISPParamter[30].Value = e.TDS;
                _ISPParamter[31] = new SqlParameter("@Flag", SqlDbType.VarChar);
                _ISPParamter[31].Value = 'Y';
                //_ISPParamter[32] = new SqlParameter("@role", SqlDbType.Int);
                //_ISPParamter[32].Value = e.Role;
                for (int i = 0; i < _ISPParamter.Length; i++)
                {
                    _Cmd.Parameters.Add(_ISPParamter[i]);
                }
                _Connection.Open();
                _Cmd.ExecuteNonQuery();
                _Connection.Close();
                return "0";
            }
            catch (SqlException Ex)
            {
                return "1";
            }
        }

        

        //***************************************************************************************************************//

        public string fn_Update_Asset(Employee e)
        {
            try
            {
                _Connection = Con.fn_Connection();

                _Connection.Open();

                SqlCommand cmd = new SqlCommand("update Assets set Asset_name='" + e.AssetName + "' where pn_AssetID=" + e.AssetId + "", _Connection);

                cmd.ExecuteNonQuery();

                _Connection.Close();

                return "0";
            }
            catch (Exception ex)
            {
                return "1";
            }

        }
        public Collection<Employee> fn_Asset(int Assetid)
        {
            Collection<Employee> AssetList = new Collection<Employee>();
            _Connection = Con.fn_Connection();
            string _SqlString = "select * from Assets where pn_Assetid !=0 and BranchID='" + Assetid + "'";
            SqlCommand _SSAsset = new SqlCommand(_SqlString, _Connection);
            _Connection.Open();
            SqlDataReader dr_Asset = _SSAsset.ExecuteReader();
            while (dr_Asset.Read())
            {
                Employee employee = new Employee();
                employee.AssetId = (int)dr_Asset["pn_Assetid"];
                employee.AssetName = Convert.IsDBNull(dr_Asset["Asset_name"]) ? "" : (string)dr_Asset["Asset_name"];
                employee.BranchId = (int)dr_Asset["BranchID"];
                AssetList.Add(employee);
            }
            return AssetList;
        }
        
        public string Employee_Asset(Employee e)
        {
            try
            {
                _Connection = Con.fn_Connection();


                SqlCommand _Cmd = new SqlCommand("sp_paym_Employee_Asset", _Connection);
                _Cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter[] _ISPParamter = new SqlParameter[5];

                _ISPParamter[0] = new SqlParameter("@pn_CompanyID", SqlDbType.Int);
                _ISPParamter[0].Value = e.CompanyId;
                _ISPParamter[1] = new SqlParameter("@pn_BranchID", SqlDbType.Int);
                _ISPParamter[1].Value = e.BranchId;
                _ISPParamter[2] = new SqlParameter("@pn_EmployeeID", SqlDbType.Int);
                _ISPParamter[2].Value = e.EmployeeId;
                _ISPParamter[3] = new SqlParameter("@pn_AssetID", SqlDbType.Int);
                _ISPParamter[3].Value = e.AssetId;
                _ISPParamter[4] = new SqlParameter("@pn_assetno", SqlDbType.VarChar);
                _ISPParamter[4].Value = e.AssetNo;


                for (int i = 0; i < _ISPParamter.Length; i++)
                {
                    _Cmd.Parameters.Add(_ISPParamter[i]);
                }
                _Connection.Open();
                _Cmd.ExecuteNonQuery();
                _Connection.Close();
                return "0";
            }
            catch (SqlException Ex)
            {
                return "1";
            }
        }

        public Collection<Employee> fn_getEmployee_Asset(Employee e)
        {
            string str_edu;
            Collection<Employee> EmployeeList = new Collection<Employee>();
            _Connection = Con.fn_Connection();

            str_edu = "select c.Asset_name,ed.pn_AssetID,ed.pn_assetno from paym_Employee_Assets ed,Assets c ";
            str_edu = str_edu + "where ed.pn_CompanyID=" + e.CompanyId + " and ed.pn_BranchID =" + e.BranchId + " and ed.pn_EmployeeID=" + e.EmployeeId + " and ";
            str_edu = str_edu + "c.pn_AssetID in(select ed.pn_AssetID from paym_Employee_Assets where ed.pn_CompanyID=" + e.CompanyId + " and ed.pn_BranchID =" + e.BranchId + " and ed.pn_EmployeeID=" + e.EmployeeId + ")";

            SqlCommand _Course = new SqlCommand(str_edu, _Connection);
            _Connection.Open();
            SqlDataReader dr = _Course.ExecuteReader();
            while (dr.Read())
            {
                Employee employee = new Employee();
                employee.AssetId = (int)dr["pn_AssetID"];
                employee.AssetName = Convert.IsDBNull(dr["Asset_name"]) ? "" : (string)dr["Asset_name"];
                employee.AssetNo = Convert.IsDBNull(dr["pn_assetno"]) ? "" : (string)dr["pn_assetno"];


                EmployeeList.Add(employee);
            }
            return EmployeeList;
        }

        public Collection<Employee> fn_getAssetList1(int sid)
        {
            Collection<Employee> AssetList = new Collection<Employee>();
            _Connection = Con.fn_Connection();
            SqlCommand _Asset = new SqlCommand("select * from Assets where BranchID='" + sid + "'", _Connection);
            _Connection.Open();
            SqlDataReader dr = _Asset.ExecuteReader();
            while (dr.Read())
            {
                Employee employee = new Employee();
                employee.AssetId = (int)dr["pn_AssetID"];
                employee.AssetName = Convert.IsDBNull(dr["Asset_name"]) ? "" : (string)dr["Asset_name"];
                AssetList.Add(employee);
            }
            return AssetList;
        }



        public Collection<Employee> fn_Assets(Employee e)
        {
            Collection<Employee> CourseList = new Collection<Employee>();
            _Connection = Con.fn_Connection();
            SqlCommand _Course = new SqlCommand("select * from Assets where pn_AssetID in" + e.temp_str + "", _Connection);
            _Connection.Open();
            SqlDataReader dr = _Course.ExecuteReader();
            while (dr.Read())
            {
                Employee course = new Employee();
                course.AssetId = (int)dr["pn_AssetID"];
                course.AssetName = Convert.IsDBNull(dr["Asset_name"]) ? "" : (string)dr["Asset_name"];
                course.PGInstutionName = "";
                course.PGPercentage = "";
                course.PGCompletedYear = "";
                
                CourseList.Add(course);
            }
            return CourseList;
        }

        public string Employee_salary(Employee e)
        {
            try
            {
                _Connection = Con.fn_Connection();

                string st = Convert_ToSqlDatestring(DateTime.Now.ToString("dd/MM/yyyy"));
                SqlCommand _Cmd = new SqlCommand("insert into salary_structure values('" + e.CompanyId + "','" + e.BranchId + "','" + e.EmployeeId + "','" + e.salary + "','" + st + "','Basic')", _Connection);

                _Connection.Open();
                _Cmd.ExecuteNonQuery();
                _Connection.Close();
                return "0";
            }
            catch (SqlException Ex)
            {
                return "1";
            }
        }

        public string salary_Increment(Employee e)
        {
            try
            {
                _Connection = Con.fn_Connection();

                string st = Convert_ToSqlDatestring(e.d_Date);
                SqlCommand _Cmd = new SqlCommand("insert into salary_structure values('" + e.CompanyId + "','" + e.BranchId + "','" + e.EmployeeId + "','" + e.salary + "','" + st + "','Annual Increment')", _Connection);

                _Connection.Open();
                _Cmd.ExecuteNonQuery();
                _Connection.Close();
                return "0";
            }
            catch (SqlException Ex)
            {
                return "1";
            }
        }

        public string salary_Appraisal(Employee e)
        {
            try
            {
                _Connection = Con.fn_Connection();

                string st = Convert_ToSqlDatestring(e.d_Date);
                SqlCommand _Cmd = new SqlCommand("insert into salary_structure values('" + e.CompanyId + "','" + e.BranchId + "','" + e.EmployeeId + "','" + e.salary + "','" + st + "','Appraisal')", _Connection);

                _Connection.Open();
                _Cmd.ExecuteNonQuery();
                _Connection.Close();
                return "0";
            }
            catch (SqlException Ex)
            {
                return "1";
            }
        }

        public string Promotion(Employee e)
        {
            try
            {
                _Connection = Con.fn_Connection();

                string st = Convert_ToSqlDatestring(e.d_Date);
                SqlCommand _Cmd = new SqlCommand("insert into promotion values('" + e.CompanyId + "','" + e.BranchId + "','" + e.EmployeeId + "','" + e.DepartmentId + "','" + e.DesignationId + "','" + e.GradeId + "','" + e.LevelId + "','" + e.salary + "','" + e.inc_value + "','" + st + "')", _Connection);

                _Connection.Open();
                _Cmd.ExecuteNonQuery();
                _Connection.Close();
                return "0";
            }
            catch (SqlException Ex)
            {
                return "1";
            }
        }

        public Collection<Employee> Promotion_check(Employee e)
        {
            _Connection = Con.fn_Connection();
            Collection<Employee> EmployeeList = new Collection<Employee>();
            string st = Convert_ToSqlDatestring(e.d_Date);
            SqlCommand _Cmd = new SqlCommand("select top 1 * from promotion where pn_companyID = '" + e.CompanyId + "' and pn_BranchID='" + e.BranchId + "' and pn_EmployeeID='" + e.EmployeeId + "' order by effective_date desc", _Connection);
            SqlDataReader _read = _Cmd.ExecuteReader();
            if (_read.Read())
            {
                Employee employee = new Employee();
                employee.EmployeeId = Convert.ToInt32(_read["pn_EmployeeID"]);
                employee.DesignationId = Convert.ToInt32(_read["pn_DesignationID"]);
                employee.GradeId = Convert.ToInt32(_read["pn_GradeID"]);
                employee.LevelId = Convert.ToInt32(_read["pn_LevelID"]);
                employee.inc_value = Convert.ToDouble(_read["Increment_Value"]);
                employee.d_Date = Convert.ToString(_read["Effective_Date"]);
                EmployeeList.Add(employee);
            }
            return EmployeeList;
        }


        public string Employee_General(Employee e)
        {
            try
            {
                _Connection = Con.fn_Connection();
                SqlCommand _Cmd = new SqlCommand("sp_paym_Employee_General1", _Connection);
                _Cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter[] _ISPParamter = new SqlParameter[48];
                _ISPParamter[0] = new SqlParameter("@pn_CompanyID", SqlDbType.Int);
                _ISPParamter[0].Value = e.CompanyId;
                _ISPParamter[1] = new SqlParameter("@pn_BranchID", SqlDbType.Int);
                _ISPParamter[1].Value = e.BranchId;
                _ISPParamter[2] = new SqlParameter("@pn_EmployeeID", SqlDbType.Int);
                _ISPParamter[2].Value = e.EmployeeId;
                _ISPParamter[3] = new SqlParameter("@EmailId", SqlDbType.VarChar);
                _ISPParamter[3].Value = e.EmailId;
                _ISPParamter[4] = new SqlParameter("@AlternateEmailId", SqlDbType.VarChar);
                _ISPParamter[4].Value = e.A_EmailId;
                _ISPParamter[5] = new SqlParameter("@BloodGroup", SqlDbType.VarChar);
                _ISPParamter[5].Value = e.BloodGroup;
                _ISPParamter[6] = new SqlParameter("@Religion", SqlDbType.VarChar);
                _ISPParamter[6].Value = e.Religion;
                _ISPParamter[7] = new SqlParameter("@Nationality", SqlDbType.VarChar);
                _ISPParamter[7].Value = e.Nationality;
                _ISPParamter[8] = new SqlParameter("@PresentHouseNo", SqlDbType.VarChar);
                _ISPParamter[8].Value = e.HouseNo;
                _ISPParamter[9] = new SqlParameter("@PresentStreetName", SqlDbType.VarChar);
                _ISPParamter[9].Value = e.StreetName;
                _ISPParamter[10] = new SqlParameter("@PresentAddLine1", SqlDbType.VarChar);
                _ISPParamter[10].Value = e.AddressLine1;
                _ISPParamter[11] = new SqlParameter("@PresentAddLine2", SqlDbType.VarChar);
                _ISPParamter[11].Value = e.AddressLine2;
                _ISPParamter[12] = new SqlParameter("@PresentCity", SqlDbType.VarChar);
                _ISPParamter[12].Value = e.City;
                _ISPParamter[13] = new SqlParameter("@PresentState", SqlDbType.VarChar);
                _ISPParamter[13].Value = e.State;
                _ISPParamter[14] = new SqlParameter("@PermanentHouseNo", SqlDbType.VarChar);
                _ISPParamter[14].Value = e.p_HouseNo;
                _ISPParamter[15] = new SqlParameter("@PermanentStreetName", SqlDbType.VarChar);
                _ISPParamter[15].Value = e.p_StreetName;
                _ISPParamter[16] = new SqlParameter("@PermanentAddLine1", SqlDbType.VarChar);
                _ISPParamter[16].Value = e.P_AddressLine1;
                _ISPParamter[17] = new SqlParameter("@PermanentAddLine2", SqlDbType.VarChar);
                _ISPParamter[17].Value = e.P_AddressLine2;
                _ISPParamter[18] = new SqlParameter("@PermanentCity", SqlDbType.VarChar);
                _ISPParamter[18].Value = e.P_City;
                _ISPParamter[19] = new SqlParameter("@PermanentState", SqlDbType.VarChar);
                _ISPParamter[19].Value = e.P_State;
                _ISPParamter[20] = new SqlParameter("@ph_Office", SqlDbType.VarChar);
                _ISPParamter[20].Value = e.ph_Office;
                _ISPParamter[21] = new SqlParameter("@ph_Residence", SqlDbType.VarChar);
                _ISPParamter[21].Value = e.ph_Residence;
                _ISPParamter[22] = new SqlParameter("@CellNo", SqlDbType.VarChar);
                _ISPParamter[22].Value = e.CellNo;
                _ISPParamter[23] = new SqlParameter("@Fax", SqlDbType.VarChar);
                _ISPParamter[23].Value = e.Fax;
                _ISPParamter[24] = new SqlParameter("@emgName", SqlDbType.VarChar);
                _ISPParamter[24].Value = e.emgname;
                _ISPParamter[25] = new SqlParameter("@emgPhone", SqlDbType.VarChar);
                _ISPParamter[25].Value = e.emgno;


                _ISPParamter[26] = new SqlParameter("@Salutation", SqlDbType.Char);
                _ISPParamter[26].Value = e.Salutation;
                _ISPParamter[27] = new SqlParameter("@M_Status", SqlDbType.Char);
                _ISPParamter[27].Value = e.MaritalStatus;
                _ISPParamter[28] = new SqlParameter("@FatherName", SqlDbType.VarChar);
                _ISPParamter[28].Value = e.FatherName;
                _ISPParamter[29] = new SqlParameter("@MotherName", SqlDbType.VarChar);
                _ISPParamter[29].Value = e.MotherName;
                _ISPParamter[30] = new SqlParameter("@Children", SqlDbType.VarChar);
                _ISPParamter[30].Value = e.Children;
                _ISPParamter[31] = new SqlParameter("@SpouseName", SqlDbType.VarChar);
                _ISPParamter[31].Value = e.SpouseName;
                _ISPParamter[32] = new SqlParameter("@Ref1_Name", SqlDbType.VarChar);
                _ISPParamter[32].Value = e.Ref1_Name;
                _ISPParamter[33] = new SqlParameter("@Ref1_Phno", SqlDbType.VarChar);
                _ISPParamter[33].Value = e.Ref1_Phno;
                _ISPParamter[34] = new SqlParameter("@Ref1_Email", SqlDbType.VarChar);
                _ISPParamter[34].Value = e.Ref1_Email;
                _ISPParamter[35] = new SqlParameter("@Ref1_Relation", SqlDbType.VarChar);
                _ISPParamter[35].Value = e.Ref1_Relation;
                _ISPParamter[36] = new SqlParameter("@Ref2_Name", SqlDbType.VarChar);
                _ISPParamter[36].Value = e.Ref2_Name;
                _ISPParamter[37] = new SqlParameter("@Ref2_Phno", SqlDbType.VarChar);
                _ISPParamter[37].Value = e.Ref2_Phno;
                _ISPParamter[38] = new SqlParameter("@Ref2_Email", SqlDbType.VarChar);
                _ISPParamter[38].Value = e.Ref2_Email;
                _ISPParamter[39] = new SqlParameter("@Ref2_Relation", SqlDbType.VarChar);
                _ISPParamter[39].Value = e.Ref2_Relation;
                _ISPParamter[40] = new SqlParameter("@training_attended", SqlDbType.VarChar);
                _ISPParamter[40].Value = e.training_attended;
                _ISPParamter[41] = new SqlParameter("@training_duration", SqlDbType.VarChar);
                _ISPParamter[41].Value = e.training_duration;
                _ISPParamter[42] = new SqlParameter("@position", SqlDbType.VarChar);
                _ISPParamter[42].Value = e.position;
                _ISPParamter[43] = new SqlParameter("@salary", SqlDbType.VarChar);
                _ISPParamter[43].Value = e.salary;
                _ISPParamter[44] = new SqlParameter("@EmployeeCode", SqlDbType.VarChar);
                _ISPParamter[44].Value = e.EmployeeCode;
                _ISPParamter[45] = new SqlParameter("@ID_Type", SqlDbType.VarChar);
                _ISPParamter[45].Value = e.IDtype;
                _ISPParamter[46] = new SqlParameter("@ID_Others", SqlDbType.VarChar);
                _ISPParamter[46].Value = e.IDOthers;
                _ISPParamter[47] = new SqlParameter("@ID_No", SqlDbType.VarChar);
                _ISPParamter[47].Value = e.IDno;
                //_ISPParamter[48] = new SqlParameter("@role", SqlDbType.Int);
                //_ISPParamter[48].Value = e.Role;

                for (int i = 0; i < _ISPParamter.Length; i++)
                {
                    _Cmd.Parameters.Add(_ISPParamter[i]);
                }
                _Connection.Open();
                _Cmd.ExecuteNonQuery();
                _Connection.Close();
                return "0";
            }
            catch (SqlException Ex)
            {
                return "1";
            }
        }

        public string Employee_profile1(Employee e)
        {
            try
            {
                _Connection = Con.fn_Connection();
                SqlCommand _Cmd = new SqlCommand("sp_paym_Employee_profile1", _Connection);
                _Cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter[] _ISPParamter = new SqlParameter[15];
                _ISPParamter[0] = new SqlParameter("@pn_CompanyID", SqlDbType.Int);
                _ISPParamter[0].Value = e.CompanyId;
                _ISPParamter[1] = new SqlParameter("@pn_BranchID", SqlDbType.Int);
                _ISPParamter[1].Value = e.BranchId;
                _ISPParamter[2] = new SqlParameter("@pn_EmployeeID", SqlDbType.Int);
                _ISPParamter[2].Value = e.EmployeeId;
                _ISPParamter[3] = new SqlParameter("@pn_DivisionId", SqlDbType.Int);
                _ISPParamter[3].Value = e.DivisionId;
                _ISPParamter[4] = new SqlParameter("@pn_DepartmentId", SqlDbType.Int);
                _ISPParamter[4].Value = e.DepartmentId;
                _ISPParamter[5] = new SqlParameter("@pn_DesingnationId", SqlDbType.Int);
                _ISPParamter[5].Value = e.DesignationId;
                _ISPParamter[6] = new SqlParameter("@pn_GradeId", SqlDbType.Int);
                _ISPParamter[6].Value = e.GradeId;
                _ISPParamter[7] = new SqlParameter("@pn_ShiftId", SqlDbType.Int);
                _ISPParamter[7].Value = e.ShiftId;
                _ISPParamter[8] = new SqlParameter("@pn_CategoryId", SqlDbType.Int);
                _ISPParamter[8].Value = e.CategoryId;
                _ISPParamter[9] = new SqlParameter("@pn_JobStatusId", SqlDbType.Int);
                _ISPParamter[9].Value = e.JobStatusId;
                _ISPParamter[10] = new SqlParameter("@pn_LevelID", SqlDbType.Int);
                _ISPParamter[10].Value = e.LevelId;
                _ISPParamter[11] = new SqlParameter("@pn_projectsiteID", SqlDbType.Int);
                _ISPParamter[11].Value = e.ProjectsiteId;
                _ISPParamter[12] = new SqlParameter("@d_Date", SqlDbType.DateTime);
                _ISPParamter[12].Value = e.Date;
                _ISPParamter[13] = new SqlParameter("@r_Department", SqlDbType.Int);
                _ISPParamter[13].Value = e.ReportID;
                _ISPParamter[14] = new SqlParameter("@v_Reason", SqlDbType.VarChar);
                _ISPParamter[14].Value = e.temp_str;
                for (int i = 0; i < _ISPParamter.Length; i++)
                {
                    _Cmd.Parameters.Add(_ISPParamter[i]);
                }
                _Connection.Open();
                _Cmd.ExecuteNonQuery();
                _Connection.Close();
                return "0";
            }
            catch (SqlException Ex)
            {
                return "1";
            }
        }
        

        public string Employee_profile(Employee e)
        {
            try
            {
                _Connection = Con.fn_Connection();


                SqlCommand _Cmd = new SqlCommand("sp_paym_Employee_profile1", _Connection);
                _Cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter[] _ISPParamter = new SqlParameter[14];

                _ISPParamter[0] = new SqlParameter("@pn_CompanyID", SqlDbType.Int);
                _ISPParamter[0].Value = e.CompanyId;
                _ISPParamter[1] = new SqlParameter("@pn_BranchID", SqlDbType.Int);
                _ISPParamter[1].Value = e.BranchId;
                _ISPParamter[2] = new SqlParameter("@pn_EmployeeID", SqlDbType.Int);
                _ISPParamter[2].Value = e.EmployeeId;
                _ISPParamter[3] = new SqlParameter("@pn_DivisionId", SqlDbType.Int);
                _ISPParamter[3].Value = e.DivisionId;
                _ISPParamter[4] = new SqlParameter("@pn_DepartmentId", SqlDbType.Int);
                _ISPParamter[4].Value = e.DepartmentId;
                _ISPParamter[5] = new SqlParameter("@pn_DesingnationId", SqlDbType.Int);
                _ISPParamter[5].Value = e.DesignationId;
                _ISPParamter[6] = new SqlParameter("@pn_GradeId", SqlDbType.Int);
                _ISPParamter[6].Value = e.GradeId;
                _ISPParamter[7] = new SqlParameter("@pn_ShiftId", SqlDbType.Int);
                _ISPParamter[7].Value = e.ShiftId;
                _ISPParamter[8] = new SqlParameter("@pn_CategoryId", SqlDbType.Int);
                _ISPParamter[8].Value = e.CategoryId;
                _ISPParamter[9] = new SqlParameter("@pn_JobStatusId", SqlDbType.Int);
                _ISPParamter[9].Value = e.JobStatusId;
                _ISPParamter[10] = new SqlParameter("@pn_LevelID", SqlDbType.Int);
                _ISPParamter[10].Value = e.LevelId;
                _ISPParamter[11] = new SqlParameter("@pn_projectsiteID", SqlDbType.Int);
                _ISPParamter[11].Value = e.ProjectsiteId;
                _ISPParamter[12] = new SqlParameter("@d_Date", SqlDbType.DateTime);
                _ISPParamter[12].Value = e.Date;
                _ISPParamter[13] = new SqlParameter("@v_Reason", SqlDbType.VarChar);
                _ISPParamter[13].Value = e.temp_str;


                for (int i = 0; i < _ISPParamter.Length; i++)
                {
                    _Cmd.Parameters.Add(_ISPParamter[i]);
                }
                _Connection.Open();
                _Cmd.ExecuteNonQuery();
                _Connection.Close();
                return "0";
            }
            catch (SqlException Ex)
            {
                return "1";
            }
        }

        public string Employee_WorkDetails(Employee e)
        {
            try
            {
                _Connection = Con.fn_Connection();

                SqlCommand _Cmd = new SqlCommand("sp_paym_Employee_WorkDetails", _Connection);
                _Cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter[] _ISPParamter = new SqlParameter[11];

                _ISPParamter[0] = new SqlParameter("@pn_CompanyID", SqlDbType.Int);
                _ISPParamter[0].Value = e.CompanyId;
                _ISPParamter[1] = new SqlParameter("@pn_BranchID", SqlDbType.Int);
                _ISPParamter[1].Value = e.BranchId;
                _ISPParamter[2] = new SqlParameter("@pn_EmployeeID", SqlDbType.Int);
                _ISPParamter[2].Value = e.EmployeeId;
                _ISPParamter[3] = new SqlParameter("@JoiningDate", SqlDbType.DateTime);
                _ISPParamter[3].Value = e.d_join;
                _ISPParamter[4] = new SqlParameter("@OfferDate", SqlDbType.DateTime);
                _ISPParamter[4].Value = e.d_Offer;
                _ISPParamter[5] = new SqlParameter("@ProbationUpto", SqlDbType.DateTime);
                _ISPParamter[5].Value = e.d_probotion;
                _ISPParamter[6] = new SqlParameter("@ExtendedUpto", SqlDbType.DateTime);
                _ISPParamter[6].Value = e.d_extended;
                _ISPParamter[7] = new SqlParameter("@ConfirmationDate", SqlDbType.DateTime);
                _ISPParamter[7].Value = e.d_confirmation;
                _ISPParamter[8] = new SqlParameter("@RetirementDate", SqlDbType.DateTime);
                _ISPParamter[8].Value = e.d_retirement;
                _ISPParamter[9] = new SqlParameter("@ContractRenviewDate", SqlDbType.DateTime);
                _ISPParamter[9].Value = e.d_renue;
                _ISPParamter[10] = new SqlParameter("@v_Reason", SqlDbType.VarChar);
                _ISPParamter[10].Value = e.Reason;

                for (int i = 0; i < _ISPParamter.Length; i++)
                {
                    _Cmd.Parameters.Add(_ISPParamter[i]);
                }
                _Connection.Open();
                _Cmd.ExecuteNonQuery();
                _Connection.Close();
                return "0";
            }
            catch (SqlException Ex)
            {
                return "1";
            }
        }

        public string Employee_PGEducation(Employee e)
        {
            try
            {
                _Connection = Con.fn_Connection();
                SqlCommand _Cmd = new SqlCommand("sp_paym_Employee_Education", _Connection);
                _Cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter[] _ISPParamter = new SqlParameter[10];
                _ISPParamter[0] = new SqlParameter("@pn_CompanyID", SqlDbType.Int);
                _ISPParamter[0].Value = e.CompanyId;
                _ISPParamter[1] = new SqlParameter("@pn_BranchID", SqlDbType.Int);
                _ISPParamter[1].Value = e.BranchId;
                _ISPParamter[2] = new SqlParameter("@pn_EmployeeID", SqlDbType.Int);
                _ISPParamter[2].Value = e.EmployeeId;
                _ISPParamter[3] = new SqlParameter("@pn_CourseID", SqlDbType.Int);
                _ISPParamter[3].Value = e.PGCourseID;
                _ISPParamter[4] = new SqlParameter("@pn_SpecializationID", SqlDbType.Int);
                _ISPParamter[4].Value = e.specializationID;
                _ISPParamter[5] = new SqlParameter("@InstitutionName", SqlDbType.VarChar);
                _ISPParamter[5].Value = e.PGInstutionName;
                _ISPParamter[6] = new SqlParameter("@Percentage", SqlDbType.VarChar);
                _ISPParamter[6].Value = e.PGPercentage;
                _ISPParamter[7] = new SqlParameter("@CompletedYear", SqlDbType.VarChar);
                _ISPParamter[7].Value = e.PGCompletedYear;
                _ISPParamter[8] = new SqlParameter("@Mode", SqlDbType.VarChar);
                _ISPParamter[8].Value = e.mode;
                _ISPParamter[9] = new SqlParameter("@Information", SqlDbType.VarChar);
                _ISPParamter[9].Value = e.PGCompletedinf;

                for (int i = 0; i < _ISPParamter.Length; i++)
                {
                    _Cmd.Parameters.Add(_ISPParamter[i]);
                }
                _Connection.Open();
                _Cmd.ExecuteNonQuery();
                _Connection.Close();
                return "0";
            }
            catch (SqlException Ex)
            {
                return "1";
            }
        }

        public string Employee_Skills(Employee e)
        {
            try
            {
                _Connection = Con.fn_Connection();


                SqlCommand _Cmd = new SqlCommand("sp_paym_Employee_Skills", _Connection);
                _Cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter[] _ISPParamter = new SqlParameter[6];

                _ISPParamter[0] = new SqlParameter("@pn_CompanyID", SqlDbType.Int);
                _ISPParamter[0].Value = e.CompanyId;
                _ISPParamter[1] = new SqlParameter("@pn_BranchID", SqlDbType.Int);
                _ISPParamter[1].Value = e.BranchId;
                _ISPParamter[2] = new SqlParameter("@pn_EmployeeID", SqlDbType.Int);
                _ISPParamter[2].Value = e.EmployeeId;
                _ISPParamter[3] = new SqlParameter("@pn_SkillID", SqlDbType.Int);
                _ISPParamter[3].Value = e.SkillId;
                _ISPParamter[4] = new SqlParameter("@v_Experience", SqlDbType.VarChar);
                _ISPParamter[4].Value = e.Experience;
                _ISPParamter[5] = new SqlParameter("@v_Proficiency", SqlDbType.VarChar);
                _ISPParamter[5].Value = e.Proficiency;

                for (int i = 0; i < _ISPParamter.Length; i++)
                {
                    _Cmd.Parameters.Add(_ISPParamter[i]);
                }
                _Connection.Open();
                _Cmd.ExecuteNonQuery();
                _Connection.Close();
                return "0";
            }
            catch (SqlException Ex)
            {
                return "1";
            }
        }

        public string Employee_Photo(Employee e)
        {
            try
            {
                _Connection = Con.fn_Connection();
                SqlCommand _Cmd = new SqlCommand("sp_paym_Employee_Photo", _Connection);
                _Cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter[] _ISPParamter = new SqlParameter[4];
                _ISPParamter[0] = new SqlParameter("@pn_CompanyID", SqlDbType.Int);
                _ISPParamter[0].Value = e.CompanyId;
                _ISPParamter[1] = new SqlParameter("@pn_BranchID", SqlDbType.Int);
                _ISPParamter[1].Value = e.BranchId;
                _ISPParamter[2] = new SqlParameter("@pn_EmployeeID", SqlDbType.Int);
                _ISPParamter[2].Value = e.EmployeeId;
                _ISPParamter[3] = new SqlParameter("@v_ImagePath", SqlDbType.VarChar);
                _ISPParamter[3].Value = e.img_path;
                for (int i = 0; i < _ISPParamter.Length; i++)
                {
                    _Cmd.Parameters.Add(_ISPParamter[i]);
                }
                _Connection.Open();
                _Cmd.ExecuteNonQuery();
                _Connection.Close();
                return "0";
            }
            catch (SqlException Ex)
            {
                return "1";
            }
        }

        //$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$

        public int fn_GetEmployeeId(string _EmpCode)
        {
            _Connection = Con.fn_Connection();
            string _scmd = "select pn_EmployeeID from paym_Employee where EmployeeCode = '" + _EmpCode + "' and status='Y'";
            SqlCommand _Cmd = new SqlCommand(_scmd, _Connection);
            _Connection.Open();
            SqlDataReader dr_Department = _Cmd.ExecuteReader();
            Employee employee = new Employee();
            while (dr_Department.Read())
            {
                employee.EmployeeId = (int)dr_Department["pn_EmployeeID"];
            }
            return employee.EmployeeId;
        }    

        public string fn_GetEmployeeCode(int _Empid)
        {
            _Connection = Con.fn_Connection();
            string _scmd = "select EmployeeCode from paym_Employee where pn_EmployeeID = " + _Empid + " and status='Y'";
            SqlCommand _Cmd = new SqlCommand(_scmd, _Connection);
            _Connection.Open();
            SqlDataReader dr_Department = _Cmd.ExecuteReader();
            Employee employee = new Employee();
            employee.EmployeeCode = "";

            while (dr_Department.Read())
            {
                employee.EmployeeCode = Convert.IsDBNull(dr_Department["EmployeeCode"]) ? "" : (string)dr_Department["EmployeeCode"];
            }

            return employee.EmployeeCode;
        }

        public string fn_GetEmployeeCode1(int _Empid , int _branchid)
        {
            _Connection = Con.fn_Connection();
            string _scmd = "select EmployeeCode from paym_Employee where pn_EmployeeID = " + _Empid + " and pn_BranchID= "+_branchid+" and  status='Y'";
            SqlCommand _Cmd = new SqlCommand(_scmd, _Connection);
            _Connection.Open();
            SqlDataReader dr_Department = _Cmd.ExecuteReader();
            Employee employee = new Employee();
            employee.EmployeeCode = "";

            while (dr_Department.Read())
            {
                employee.EmployeeCode = Convert.IsDBNull(dr_Department["EmployeeCode"]) ? "" : (string)dr_Department["EmployeeCode"];
            }

            return employee.EmployeeCode;
        }

        public Collection<Employee> fn_get_EmployeeID(string _EmpCode)
        {
            Collection<Employee> DeductionList = new Collection<Employee>();
            _Connection = Con.fn_Connection();
            string _SqlString = "select pn_EmployeeID from paym_Employee where EmployeeCode = '" + _EmpCode + "' and status='Y'";
            SqlCommand _SSDeduction = new SqlCommand(_SqlString, _Connection);
            _Connection.Open();
            SqlDataReader dr_Deduction = _SSDeduction.ExecuteReader();
            while (dr_Deduction.Read())
            {
                Employee employee = new Employee();
                employee.EmployeeId = (int)dr_Deduction["pn_EmployeeID"];
                DeductionList.Add(employee);
            }
            return DeductionList;
        }

        public Collection<Employee> fn_get_TempID(string _userid)
        {
            Collection<Employee> DeductionList = new Collection<Employee>();
            _Connection = Con.fn_Connection();
            string _SqlString = "select * from paym_Employee where EmployeeCode='" + _userid + "'";
            SqlCommand _SSDeduction = new SqlCommand(_SqlString, _Connection);
            _Connection.Open();
            SqlDataReader dr_Deduction = _SSDeduction.ExecuteReader();
            while (dr_Deduction.Read())
            {
                Employee employee = new Employee();
                employee.EmployeeId = (int)dr_Deduction["n_BranchID"];
                DeductionList.Add(employee);
            }
            return DeductionList;
        }

        public string fn_GetEmployeePhoto(int _EmpID)
        {
            _Connection = Con.fn_Connection();
            string _scmd = "select v_ImagePath from paym_Employee_Photo where pn_EmployeeID = '" + _EmpID + "'";
            SqlCommand _Cmd = new SqlCommand(_scmd, _Connection);
            _Connection.Open();
            SqlDataReader dr_Department = _Cmd.ExecuteReader();
            Employee employee = new Employee();
            while (dr_Department.Read())
            {
                employee.img_path = (string)dr_Department["v_ImagePath"];
            }
            return employee.img_path;
        }
        
        //################################################################################################################


        public string DepartmentUpdate(Employee e)
        {
            try
            {
                _Connection = Con.fn_Connection();


                SqlCommand _Cmd = new SqlCommand("sp_paym_Department", _Connection);
                _Cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter[] _ISPParamter = new SqlParameter[5];

                _ISPParamter[0] = new SqlParameter("@pn_CompanyID", SqlDbType.VarChar);
                _ISPParamter[0].Value = e.CompanyId;
                _ISPParamter[1] = new SqlParameter("@pn_BranchID", SqlDbType.Int);
                _ISPParamter[1].Value = e.BranchId;
                _ISPParamter[2] = new SqlParameter("@pn_DepartmentID", SqlDbType.Int);
                _ISPParamter[2].Value = e.DepartmentId;
                _ISPParamter[3] = new SqlParameter("@v_DepartmentName", SqlDbType.VarChar);
                _ISPParamter[3].Value = e.DepartmentName;
                _ISPParamter[4] = new SqlParameter("@status", SqlDbType.VarChar);
                _ISPParamter[4].Value = "Y";

                for (int i = 0; i < _ISPParamter.Length; i++)
                {
                    _Cmd.Parameters.Add(_ISPParamter[i]);
                }
                _Connection.Open();
                _Cmd.ExecuteNonQuery();
                _Connection.Close();
                return "0";
            }
            catch (SqlException Ex)
            {
                return "1";
            }
        }

        public int DepartmentValidate(string e)
        {
            try
            {
                _Connection = Con.fn_Connection();
                _Connection.Open();
                SqlCommand _Cmd = new SqlCommand("select * from paym_department where v_departmentname = '" + e + "'", _Connection);
                SqlDataReader _rd = _Cmd.ExecuteReader();

                if (_rd.Read())
                {
                    return 1;
                }
                else
                {
                    return 0;
                }

            }
            catch (SqlException Ex)
            {
                return 1;
            }
            finally
            {
                _Connection.Close();
            }
        }

         

        public int DesignationValidate(string e)
        {
            try
            {
                _Connection = Con.fn_Connection();
                _Connection.Open();
                SqlCommand _Cmd = new SqlCommand("select * from paym_designation where v_designationname = '" + e + "'", _Connection);
                SqlDataReader _rd = _Cmd.ExecuteReader();

                if (_rd.Read())
                {
                    return 1;
                }
                else
                {
                    return 0;
                }

            }
            catch (SqlException Ex)
            {
                return 1;
            }
            finally
            {
                _Connection.Close();
            }
        }

        public int DivisionValidate(string e)
        {
            try
            {
                _Connection = Con.fn_Connection();
                _Connection.Open();
                SqlCommand _Cmd = new SqlCommand("select * from paym_division where v_divisionname = '" + e + "'", _Connection);
                SqlDataReader _rd = _Cmd.ExecuteReader();

                if (_rd.Read())
                {
                    return 1;
                }
                else
                {
                    return 0;
                }

            }
            catch (SqlException Ex)
            {
                return 1;
            }
            finally
            {
                _Connection.Close();
            }
        }

        public int LevelValidate(string e)
        {
            try
            {
                _Connection = Con.fn_Connection();
                _Connection.Open();
                SqlCommand _Cmd = new SqlCommand("select * from paym_level where v_levelname = '" + e + "'", _Connection);
                SqlDataReader _rd = _Cmd.ExecuteReader();

                if (_rd.Read())
                {
                    return 1;
                }
                else
                {
                    return 0;
                }

            }
            catch (SqlException Ex)
            {
                return 1;
            }
            finally
            {
                _Connection.Close();
            }
        }

        public int GradeValidate(string e)
        {
            try
            {
                _Connection = Con.fn_Connection();
                _Connection.Open();
                SqlCommand _Cmd = new SqlCommand("select * from paym_grade where v_gradename = '" + e + "'", _Connection);
                SqlDataReader _rd = _Cmd.ExecuteReader();

                if (_rd.Read())
                {
                    return 1;
                }
                else
                {
                    return 0;
                }

            }
            catch (SqlException Ex)
            {
                return 1;
            }
            finally
            {
                _Connection.Close();
            }
        }

        public int CategoryValidate(string e)
        {
            try
            {
                _Connection = Con.fn_Connection();
                _Connection.Open();
                SqlCommand _Cmd = new SqlCommand("select * from paym_Category where v_Categoryname = '" + e + "'", _Connection);
                SqlDataReader _rd = _Cmd.ExecuteReader();

                if (_rd.Read())
                {
                    return 1;
                }
                else
                {
                    return 0;
                }

            }
            catch (SqlException Ex)
            {
                return 1;
            }
            finally
            {
                _Connection.Close();
            }
        }

        public int JobstatusValidate(string e)
        {
            try
            {
                _Connection = Con.fn_Connection();
                _Connection.Open();
                SqlCommand _Cmd = new SqlCommand("select * from paym_jobstatus where v_jobstatusname = '" + e + "'", _Connection);
                SqlDataReader _rd = _Cmd.ExecuteReader();

                if (_rd.Read())
                {
                    return 1;
                }
                else
                {
                    return 0;
                }

            }
            catch (SqlException Ex)
            {
                return 1;
            }
            finally
            {
                _Connection.Close();
            }
        }

        public int OverheadingValidate(string e)
        {
            try
            {
                _Connection = Con.fn_Connection();
                _Connection.Open();
                SqlCommand _Cmd = new SqlCommand("select * from paym_overheadingcost where overheadingname = '" + e + "'", _Connection);
                SqlDataReader _rd = _Cmd.ExecuteReader();

                if (_rd.Read())
                {
                    return 1;
                }
                else
                {
                    return 0;
                }

            }
            catch (SqlException Ex)
            {
                return 1;
            }
            finally
            {
                _Connection.Close();
            }
        }

        public int AssetValidate(string e)
        {
            try
            {
                _Connection = Con.fn_Connection();
                _Connection.Open();
                SqlCommand _Cmd = new SqlCommand("select * from assets where Asset_name = '" + e + "'", _Connection);
                SqlDataReader _rd = _Cmd.ExecuteReader();

                if (_rd.Read())
                {
                    return 1;
                }
                else
                {
                    return 0;
                }

            }
            catch (SqlException Ex)
            {
                return 1;
            }
            finally
            {
                _Connection.Close();
            }
        }

        public string courseUpdate(Employee e)
        {
            try
            {
                _Connection = Con.fn_Connection();
                SqlCommand _Cmd = new SqlCommand("sp_paym_course", _Connection);
                _Cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter[] _ISPParamter = new SqlParameter[5];
                _ISPParamter[0] = new SqlParameter("@pn_CompanyID", SqlDbType.VarChar);
                _ISPParamter[0].Value = e.CompanyId;
                _ISPParamter[1] = new SqlParameter("@pn_courseID", SqlDbType.Int);
                _ISPParamter[1].Value = e.CourseId;
                _ISPParamter[2] = new SqlParameter("@v_courseName", SqlDbType.VarChar);
                _ISPParamter[2].Value = e.CourseName;
                _ISPParamter[3] = new SqlParameter("@status", SqlDbType.VarChar);
                _ISPParamter[3].Value = e.status;
                _ISPParamter[4] = new SqlParameter("@pn_BranchID", SqlDbType.Int);
                _ISPParamter[4].Value = e.BranchId;

                for (int i = 0; i < _ISPParamter.Length; i++)
                {
                    _Cmd.Parameters.Add(_ISPParamter[i]);
                }
                _Connection.Open();
                _Cmd.ExecuteNonQuery();
                _Connection.Close();
                return "0";
            }
            catch (SqlException Ex)
            {
                return "1";
            }
        }

        public string skillUpdate(Employee e)
        {
            try
            {
                _Connection = Con.fn_Connection();


                SqlCommand _Cmd = new SqlCommand("sp_paym_skillsmaster", _Connection);
                _Cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter[] _ISPParamter = new SqlParameter[5];

                _ISPParamter[0] = new SqlParameter("@pn_CompanyID", SqlDbType.VarChar);
                _ISPParamter[0].Value = e.CompanyId;
                _ISPParamter[1] = new SqlParameter("@BranchID", SqlDbType.Int);
                _ISPParamter[1].Value = e.BranchId;
                _ISPParamter[2] = new SqlParameter("@pn_SkillID", SqlDbType.Int);
                _ISPParamter[2].Value = e.SkillId;
                _ISPParamter[3] = new SqlParameter("@v_SkillName", SqlDbType.VarChar);
                _ISPParamter[3].Value = e.SkillName;
                _ISPParamter[4] = new SqlParameter("@status", SqlDbType.VarChar);
                _ISPParamter[4].Value = "Y";

                for (int i = 0; i < _ISPParamter.Length; i++)
                {
                    _Cmd.Parameters.Add(_ISPParamter[i]);
                }
                _Connection.Open();
                _Cmd.ExecuteNonQuery();
                _Connection.Close();
                return "0";
            }
            catch (SqlException Ex)
            {
                return "1";
            }
        }

        public string projectsiteUpdate(Employee e)
        {
            try
            {
                _Connection = Con.fn_Connection();
                SqlCommand _Cmd = new SqlCommand("sp_paym_projectsite", _Connection);
                _Cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter[] _ISPParamter = new SqlParameter[7];
                _ISPParamter[0] = new SqlParameter("@pn_CompanyID", SqlDbType.VarChar);
                _ISPParamter[0].Value = e.CompanyId;
                _ISPParamter[1] = new SqlParameter("@BranchID", SqlDbType.Int);
                _ISPParamter[1].Value = e.BranchId;
                _ISPParamter[2] = new SqlParameter("@pn_projectsiteID", SqlDbType.Int);
                _ISPParamter[2].Value = e.ProjectsiteId;
                _ISPParamter[3] = new SqlParameter("@v_projectsiteName", SqlDbType.VarChar);
                _ISPParamter[3].Value = e.ProjectsiteName;
                _ISPParamter[4] = new SqlParameter("@Location", SqlDbType.VarChar);
                _ISPParamter[4].Value = e.Location;
                _ISPParamter[5] = new SqlParameter("@projectName", SqlDbType.VarChar);
                _ISPParamter[5].Value = e.ProjectName;
                _ISPParamter[6] = new SqlParameter("@status", SqlDbType.VarChar);
                _ISPParamter[6].Value = "Y";
                for (int i = 0; i < _ISPParamter.Length; i++)
                {
                    _Cmd.Parameters.Add(_ISPParamter[i]);
                }
                _Connection.Open();
                _Cmd.ExecuteNonQuery();
                _Connection.Close();
                return "0";
            }
            catch (SqlException Ex)
            {
                return "1";
            }
        }

        public string ShiftUpdate(Employee e)
        {
            try
            {
                _Connection = Con.fn_Connection();
                SqlCommand _Cmd = new SqlCommand("sp_paym_Shift", _Connection);
                _Cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter[] _ISPParamter = new SqlParameter[7];

                _ISPParamter[0] = new SqlParameter("@pn_CompanyID", SqlDbType.VarChar);
                _ISPParamter[0].Value = e.CompanyId;
                _ISPParamter[1] = new SqlParameter("@BranchID", SqlDbType.Int);
                _ISPParamter[1].Value = e.BranchId;
                _ISPParamter[2] = new SqlParameter("@pn_ShiftId", SqlDbType.Int);
                _ISPParamter[2].Value = e.ShiftId;
                _ISPParamter[3] = new SqlParameter("@v_ShiftName", SqlDbType.VarChar);
                _ISPParamter[3].Value = e.ShiftName;
                _ISPParamter[4] = new SqlParameter("@v_ShiftFrom", SqlDbType.VarChar);
                _ISPParamter[4].Value = e.ShiftFrom;
                _ISPParamter[5] = new SqlParameter("@v_ShiftTo", SqlDbType.VarChar);
                _ISPParamter[5].Value = e.ShiftTo;
                _ISPParamter[6] = new SqlParameter("@status", SqlDbType.VarChar);
                _ISPParamter[6].Value = "Y";
                for (int i = 0; i < _ISPParamter.Length; i++)
                {
                    _Cmd.Parameters.Add(_ISPParamter[i]);
                }
                _Connection.Open();
                _Cmd.ExecuteNonQuery();
                _Connection.Close();
                return "0";
            }
            catch (SqlException Ex)
            {
                return "1";
            }
        }

        public string CategoryUpdate(Employee e)
        {
            try
            {
                _Connection = Con.fn_Connection();
                SqlCommand _Cmd = new SqlCommand("sp_paym_Category", _Connection);
                _Cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter[] _ISPParamter = new SqlParameter[5];

                _ISPParamter[0] = new SqlParameter("@pn_CompanyID", SqlDbType.VarChar);
                _ISPParamter[0].Value = e.CompanyId;
                _ISPParamter[1] = new SqlParameter("@BranchID", SqlDbType.Int);
                _ISPParamter[1].Value = e.BranchId;
                _ISPParamter[2] = new SqlParameter("@pn_CategoryId", SqlDbType.Int);
                _ISPParamter[2].Value = e.CategoryId;
                _ISPParamter[3] = new SqlParameter("@v_CategoryName", SqlDbType.VarChar);
                _ISPParamter[3].Value = e.CategoryName;
                _ISPParamter[4] = new SqlParameter("@status", SqlDbType.VarChar);
                _ISPParamter[4].Value = "Y";
                for (int i = 0; i < _ISPParamter.Length; i++)
                {
                    _Cmd.Parameters.Add(_ISPParamter[i]);
                }
                _Connection.Open();
                _Cmd.ExecuteNonQuery();
                _Connection.Close();
                return "0";
            }
            catch (SqlException Ex)
            {
                return "1";
            }
        }



        public string DivisionUpdate(Employee e)
        {
            try
            {
                _Connection = Con.fn_Connection();
                SqlCommand _Cmd = new SqlCommand("sp_paym_Division", _Connection);
                _Cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter[] _ISPParamter = new SqlParameter[5];

                _ISPParamter[0] = new SqlParameter("@pn_CompanyID", SqlDbType.VarChar);
                _ISPParamter[0].Value = e.CompanyId;
                _ISPParamter[1] = new SqlParameter("@BranchID", SqlDbType.Int);
                _ISPParamter[1].Value = e.BranchId;
                _ISPParamter[2] = new SqlParameter("@pn_DivisionId", SqlDbType.Int);
                _ISPParamter[2].Value = e.DivisionId;
                _ISPParamter[3] = new SqlParameter("@v_DivisionName", SqlDbType.VarChar);
                _ISPParamter[3].Value = e.DivisionName;
                _ISPParamter[4] = new SqlParameter("@status", SqlDbType.VarChar);
                _ISPParamter[4].Value = "Y";
                for (int i = 0; i < _ISPParamter.Length; i++)
                {
                    _Cmd.Parameters.Add(_ISPParamter[i]);
                }
                _Connection.Open();
                _Cmd.ExecuteNonQuery();
                _Connection.Close();
                return "0";
            }
            catch (SqlException Ex)
            {
                return "1";
            }
        }

        public string DesignationUpdate(Employee e)
        {
            try
            {
                _Connection = Con.fn_Connection();
                SqlCommand _Cmd = new SqlCommand("sp_paym_Designation", _Connection);
                _Cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter[] _ISPParamter = new SqlParameter[5];

                _ISPParamter[0] = new SqlParameter("@pn_CompanyID", SqlDbType.VarChar);
                _ISPParamter[0].Value = e.CompanyId;
                _ISPParamter[1] = new SqlParameter("@BranchID", SqlDbType.Int);
                _ISPParamter[1].Value = e.BranchId;
                _ISPParamter[2] = new SqlParameter("@pn_DesignationId", SqlDbType.Int);
                _ISPParamter[2].Value = e.DesignationId;
                _ISPParamter[3] = new SqlParameter("@v_DesignationName", SqlDbType.VarChar);
                _ISPParamter[3].Value = e.DesignationName;
                _ISPParamter[4] = new SqlParameter("@status", SqlDbType.VarChar);
                _ISPParamter[4].Value = "Y";
                for (int i = 0; i < _ISPParamter.Length; i++)
                {
                    _Cmd.Parameters.Add(_ISPParamter[i]);
                }
                _Connection.Open();
                _Cmd.ExecuteNonQuery();
                _Connection.Close();
                return "0";
            }
            catch (SqlException Ex)
            {
                return "1";
            }
        }

        public string LevelUpdate(Employee e)
        {
            try
            {
                _Connection = Con.fn_Connection();
                SqlCommand _Cmd = new SqlCommand("sp_paym_Level", _Connection);
                _Cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter[] _ISPParamter = new SqlParameter[5];

                _ISPParamter[0] = new SqlParameter("@pn_CompanyID", SqlDbType.VarChar);
                _ISPParamter[0].Value = e.CompanyId;
                _ISPParamter[1] = new SqlParameter("BranchID", SqlDbType.Int);
                _ISPParamter[1].Value = e.BranchId;
                _ISPParamter[2] = new SqlParameter("@pn_LevelId", SqlDbType.Int);
                _ISPParamter[2].Value = e.LevelId;
                _ISPParamter[3] = new SqlParameter("@v_LevelName", SqlDbType.VarChar);
                _ISPParamter[3].Value = e.LevelName;
                _ISPParamter[4] = new SqlParameter("@status", SqlDbType.VarChar);
                _ISPParamter[4].Value = "Y";
                for (int i = 0; i < _ISPParamter.Length; i++)
                {
                    _Cmd.Parameters.Add(_ISPParamter[i]);
                }
                _Connection.Open();
                _Cmd.ExecuteNonQuery();
                _Connection.Close();
                return "0";
            }
            catch (SqlException Ex)
            {
                return "1";
            }
        }

        public string GradeUpdate(Employee e)
        {
            try
            {
                _Connection = Con.fn_Connection();
                SqlCommand _Cmd = new SqlCommand("sp_paym_Grade", _Connection);
                _Cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter[] _ISPParamter = new SqlParameter[5];

                _ISPParamter[0] = new SqlParameter("@pn_CompanyID", SqlDbType.VarChar);
                _ISPParamter[0].Value = e.CompanyId;
                _ISPParamter[1] = new SqlParameter("@BranchID", SqlDbType.Int);
                _ISPParamter[1].Value = e.BranchId;
                _ISPParamter[2] = new SqlParameter("@pn_GradeId", SqlDbType.Int);
                _ISPParamter[2].Value = e.GradeId;
                _ISPParamter[3] = new SqlParameter("@v_GradeName", SqlDbType.VarChar);
                _ISPParamter[3].Value = e.GradeName;
                _ISPParamter[4] = new SqlParameter("@status", SqlDbType.VarChar);
                _ISPParamter[4].Value = "Y";
                for (int i = 0; i < _ISPParamter.Length; i++)
                {
                    _Cmd.Parameters.Add(_ISPParamter[i]);
                }
                _Connection.Open();
                _Cmd.ExecuteNonQuery();
                _Connection.Close();
                return "0";
            }
            catch (SqlException Ex)
            {
                return "1";
            }
        }

        public string JobStatusUpdate(Employee e)
        {
            try
            {
                _Connection = Con.fn_Connection();
                SqlCommand _Cmd = new SqlCommand("sp_paym_JobStatus", _Connection);
                _Cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter[] _ISPParamter = new SqlParameter[5];

                _ISPParamter[0] = new SqlParameter("@pn_CompanyID", SqlDbType.VarChar);
                _ISPParamter[0].Value = e.CompanyId;
                _ISPParamter[1] = new SqlParameter("@BranchID", SqlDbType.Int);
                _ISPParamter[1].Value = e.BranchId;
                _ISPParamter[2] = new SqlParameter("@pn_JobStatusId", SqlDbType.Int);
                _ISPParamter[2].Value = e.JobStatusId;
                _ISPParamter[3] = new SqlParameter("@v_JobStatusName", SqlDbType.VarChar);
                _ISPParamter[3].Value = e.JobStatusName;
                _ISPParamter[4] = new SqlParameter("@status", SqlDbType.VarChar);
                _ISPParamter[4].Value = "Y";

                for (int i = 0; i < _ISPParamter.Length; i++)
                {
                    _Cmd.Parameters.Add(_ISPParamter[i]);
                }
                _Connection.Open();
                _Cmd.ExecuteNonQuery();
                _Connection.Close();
                return "0";
            }
            catch (SqlException Ex)
            {
                return "1";
            }
        }




        //$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$


        public Collection<Employee> fn_getDepartmentList()
        {
            Collection<Employee> DepartmentList = new Collection<Employee>();
            _Connection = Con.fn_Connection();
            _Connection.Open();
            string _SqlString = "select * from paym_Department where status='Y'";
            SqlCommand _SSDepartment = new SqlCommand(_SqlString, _Connection);           
            SqlDataReader dr = _SSDepartment.ExecuteReader();
            while (dr.Read())
            {
                Employee employee = new Employee();
                employee.DepartmentId = (int)dr["pn_DepartmentID"];
                employee.Department_Name = Convert.IsDBNull(dr["v_DepartmentName"]) ? "" : (string)dr["v_DepartmentName"];
                DepartmentList.Add(employee);
            }
            return DepartmentList;
        }

        public Collection<Employee> fn_getIncrementList(int e)
        {
            Collection<Employee> DepartmentList = new Collection<Employee>();
            _Connection = Con.fn_Connection();
            string _SqlString = "select * from paym_Increment where pn_BranchID='" + e + "'";
            SqlCommand _SSDepartment = new SqlCommand(_SqlString, _Connection);
            _Connection.Open();
            SqlDataReader dr_Department = _SSDepartment.ExecuteReader();
            while (dr_Department.Read())
            {
                Employee employee = new Employee();
                employee.Id1 = (int)dr_Department["Increment_ID"];
                employee.temp_str = Convert.IsDBNull(dr_Department["Inc_name"]) ? "" : (string)dr_Department["Inc_name"];
                DepartmentList.Add(employee);
            }
            return DepartmentList;
        }

        public Collection<Employee> fn_getDepartmentList1(int e)
        {
            Collection<Employee> DepartmentList = new Collection<Employee>();
            _Connection = Con.fn_Connection();
            string _SqlString = "select * from paym_Department where status='Y' and pn_BranchID='" + e + "' order by v_DepartmentName ";

            //string _SqlString = "select * from paym_Department where status='Y' and pn_BranchID='" + e + "'";
            SqlCommand _SSDepartment = new SqlCommand(_SqlString, _Connection);
            _Connection.Open();
            SqlDataReader dr_Department = _SSDepartment.ExecuteReader();
            while (dr_Department.Read())
            {
                Employee employee = new Employee();
                employee.DepartmentId = (int)dr_Department["pn_DepartmentID"];
                employee.DepartmentName = Convert.IsDBNull(dr_Department["v_DepartmentName"]) ? "" : (string)dr_Department["v_DepartmentName"];
                DepartmentList.Add(employee);
            }
            return DepartmentList;
        }
        public Collection<Employee> leave_process(int e)
        {
            string _SqlString = "SELECT * FROM leave_apply INNER JOIN paym_employee_Profile1 ON leave_apply.pn_EmployeeID=paym_employee_Profile1.pn_EmployeeID where  leave_apply.pn_BranchID='" + e + "' and FLAG IS NULL";
            Collection<Employee> leave_Processs = new Collection<Employee>();
            _Connection = Con.fn_Connection();
            _Connection.Open();
            SqlCommand _SSleaveprocess = new SqlCommand(_SqlString, _Connection);
            SqlDataReader dr = _SSleaveprocess.ExecuteReader();
            while (dr.Read())
            {
                Employee employee = new Employee();
                employee.EmployeeId = (int)dr["pn_EmployeeID"];
                employee.EmployeeCode = Convert.IsDBNull(dr["Emp_code"]) ? "" : (string)dr["Emp_code"];
                employee.FirstName = Convert.IsDBNull(dr["Emp_name"]) ? "" : (string)dr["Emp_name"];
                employee.leave_name= Convert.IsDBNull(dr["pn_Leavename"]) ? "" : (string)dr["pn_Leavename"];
                employee.leave_id= (int)dr["pn_LeaveID"];
                employee.leave_Code = Convert.IsDBNull(dr["pn_leavecode"]) ? "" : (string)dr["pn_leavecode"];
                employee.from_date=Convert.IsDBNull(dr["from_date"]) ? DateTime.Now : Convert.ToDateTime(dr["from_date"]);
               // employee.from_date= Convert.IsDBNull(dr["from_date"]) ? "" : (string)dr["from_date"];
                employee.to_date = Convert.IsDBNull(dr["to_date"]) ? DateTime.Now : Convert.ToDateTime(dr["to_date"]);
                employee.Submit_date = Convert.IsDBNull(dr["submitted_date"]) ? DateTime.Now : Convert.ToDateTime(dr["submitted_date"]);
                //employee.to_date = Convert.IsDBNull(dr["to_date"]) ? "" : (string)dr["to_date"];
                //employee.Submit_date = Convert.IsDBNull(dr["submitted_date"]) ? "" : (string)dr["submitted_date"];
                employee.approve= Convert.IsDBNull(dr["approve"]) ? "" : (string)dr["approve"];
                employee.yearend = (int)dr["yearend"];
                //employee.yearend = Convert.IsDBNull(dr["yearend"]) ? "":(string)dr["yearend"];
                //employee.from_status = Convert.IsDBNull(dr["from_status"]) ? DateTime.Now : Convert.ToDateTime(dr["from_status"]);
                //employee.to_status = Convert.IsDBNull(dr["to_date"]) ? DateTime.Now : Convert.ToDateTime(dr["to_date"]);
                employee.from_status = Convert.IsDBNull(dr["from_status"]) ? "" : (string)dr["from_status"];
                employee.to_status = Convert.IsDBNull(dr["status"]) ? "" : (string)dr["status"];
               // employee.DesignationId = Convert.IsDBNull(dr["status"]) ? "" : (int)dr["status"];
                employee.DesignationId = (int)dr["pn_DesingnationId"];
                //employee.day = (int)dr["days"];
                employee.day = (double)dr["days"];

                leave_Processs.Add(employee);
            }
            return leave_Processs;
        }
        public Collection<Employee> fn_getPatternList(int e)
        {
            Collection<Employee> PList = new Collection<Employee>();
            _Connection = Con.fn_Connection();
            string _SqlString = "select pattern_code from shift_pattern where pn_BranchID='" + e + "'";
            SqlCommand _SSDepartment = new SqlCommand(_SqlString, _Connection);
            _Connection.Open();
            SqlDataReader dr_pattern = _SSDepartment.ExecuteReader();
            while (dr_pattern.Read())
            {
                Employee employee = new Employee();
                employee.ShiftName = Convert.IsDBNull(dr_pattern["pattern_code"]) ? "" : (string)dr_pattern["pattern_code"];
                PList.Add(employee);
            }
            return PList;
        }

        public Collection<Employee> fn_getshiftList(int e)
        {
            Collection<Employee> SList = new Collection<Employee>();
            _Connection = Con.fn_Connection();
            string _SqlString = "SELECT shift_code FROM shift_details where pn_BranchID='" + e + "'";
            SqlCommand _SSDepartment = new SqlCommand(_SqlString, _Connection);
            _Connection.Open();
            SqlDataReader dr_pattern = _SSDepartment.ExecuteReader();
            while (dr_pattern.Read())
            {
                Employee employee = new Employee();
                employee.ShiftCode = Convert.IsDBNull(dr_pattern["shift_code"]) ? "" : (string)dr_pattern["shift_code"];
                SList.Add(employee);
            }
            return SList;
        }

        public Collection<Employee> fn_getcourseList1(int cid)
        {
            Collection<Employee> courseList = new Collection<Employee>();
            _Connection = Con.fn_Connection();
            string _SqlString = "select * from paym_course where status='Y' and pn_BranchID='"+cid+"'";
            SqlCommand _SScourse = new SqlCommand(_SqlString, _Connection);
            _Connection.Open();
            SqlDataReader dr_course = _SScourse.ExecuteReader();
            while (dr_course.Read())
            {
                Employee employee = new Employee();
                employee.CourseId = (int)dr_course["pn_courseID"];
                employee.CourseName = Convert.IsDBNull(dr_course["v_courseName"]) ? "" : (string)dr_course["v_courseName"];
                courseList.Add(employee);
            }
            return courseList;
        }        

        public Collection<Employee> fn_getcourseList(Employee e)
        {
            Collection<Employee> courseList = new Collection<Employee>();
            _Connection = Con.fn_Connection();
            string _SqlString = "select * from paym_course where status='Y'";
            SqlCommand _SScourse = new SqlCommand(_SqlString, _Connection);
            _Connection.Open();
            SqlDataReader dr_course = _SScourse.ExecuteReader();
            while (dr_course.Read())
            {
                Employee employee = new Employee();
                employee.CourseId = (int)dr_course["pn_courseID"];
                employee.CourseName = Convert.IsDBNull(dr_course["v_courseName"]) ? "" : (string)dr_course["v_courseName"];
                courseList.Add(employee);
            }
            return courseList;
        }
        public Collection<Employee> fn_getCourseListHRMCourse(Employee e)
        {
            Collection<Employee> courseList = new Collection<Employee>();
            _Connection = Con.fn_Connection();
            string _SqlString = "select * from hrmm_course where status='Y'";
            SqlCommand _SScourse = new SqlCommand(_SqlString, _Connection);
            _Connection.Open();
            SqlDataReader dr_course = _SScourse.ExecuteReader();
            while (dr_course.Read())
            {
                Employee employee = new Employee();
                employee.CourseId = (int)dr_course["pn_CourseID"];
                employee.CourseName = Convert.IsDBNull(dr_course["v_CourseName"]) ? "" : (string)dr_course["v_CourseName"];
                courseList.Add(employee);
            }
            return courseList;
        }

        public Collection<Employee> fn_getskillList()
        {
            Collection<Employee> skillList = new Collection<Employee>();
            _Connection = Con.fn_Connection();
            string _SqlString = "select * from paym_skill where status='Y'";
            SqlCommand _SSskill = new SqlCommand(_SqlString, _Connection);
            _Connection.Open();
            SqlDataReader dr_skill = _SSskill.ExecuteReader();
            while (dr_skill.Read())
            {
                Employee employee = new Employee();
                employee.SkillId = (int)dr_skill["pn_skillID"];
                employee.SkillName = Convert.IsDBNull(dr_skill["v_skillName"]) ? "" : (string)dr_skill["v_skillName"];
                skillList.Add(employee);
            }
            return skillList;
        }

        public Collection<Employee> fn_getprojectsiteList()
        {
            Collection<Employee> projectsiteList = new Collection<Employee>();
            _Connection = Con.fn_Connection();
            string _SqlString = "select * from paym_projectsite where status='Y'";
            SqlCommand _SSprojectsite = new SqlCommand(_SqlString, _Connection);
            _Connection.Open();
            SqlDataReader dr_projectsite = _SSprojectsite.ExecuteReader();
            while (dr_projectsite.Read())
            {
                Employee employee = new Employee();
                employee.ProjectsiteId = (int)dr_projectsite["pn_projectsiteID"];
                employee.ProjectsiteName = Convert.IsDBNull(dr_projectsite["v_projectsiteName"]) ? "" : (string)dr_projectsite["v_projectsiteName"];
                employee.Location = Convert.IsDBNull(dr_projectsite["Location"]) ? "" : (string)dr_projectsite["Location"];
                employee.ProjectName = Convert.IsDBNull(dr_projectsite["projectName"]) ? "" : (string)dr_projectsite["projectName"];
                projectsiteList.Add(employee);
            }
            return projectsiteList;
        }

        public Collection<Employee> fn_getDeductionList()
        {
            Collection<Employee> DeductionList = new Collection<Employee>();
            _Connection = Con.fn_Connection();
            string _SqlString = "select * from paym_Deduction where status='Y'";
            SqlCommand _SSDeduction = new SqlCommand(_SqlString, _Connection);
            _Connection.Open();
            SqlDataReader dr_Deduction = _SSDeduction.ExecuteReader();
            while (dr_Deduction.Read())
            {
                Employee employee = new Employee();
                employee.DeductionId = (int)dr_Deduction["pn_DeductionId"];
                employee.DeductionName = Convert.IsDBNull(dr_Deduction["v_Description"]) ? "" : (string)dr_Deduction["v_Description"];
                DeductionList.Add(employee);
            }
            return DeductionList;
        }        
 

        public Collection<Employee> fn_getDivisionList()
        {
            Collection<Employee> DivisionList = new Collection<Employee>();
            _Connection = Con.fn_Connection();
            string _SqlString = "select * from paym_Division where status='Y'";
            SqlCommand _SSDivision = new SqlCommand(_SqlString, _Connection);
            _Connection.Open();
            SqlDataReader dr_Division = _SSDivision.ExecuteReader();
            while (dr_Division.Read())
            {
                Employee employee = new Employee();
                employee.DivisionId = (int)dr_Division["pn_DivisionID"];
                employee.DivisionName = Convert.IsDBNull(dr_Division["v_DivisionName"]) ? "" : (string)dr_Division["v_DivisionName"];
                DivisionList.Add(employee);
            }
            return DivisionList;
        }

        public Collection<Employee> fn_getDivisionList1(int d)
        {
            Collection<Employee> DivisionList = new Collection<Employee>();
            _Connection = Con.fn_Connection();
            string _SqlString = "select * from paym_Division where status='Y' and BranchID='"+d+"'";
            SqlCommand _SSDivision = new SqlCommand(_SqlString, _Connection);
            _Connection.Open();
            SqlDataReader dr_Division = _SSDivision.ExecuteReader();
            while (dr_Division.Read())
            {
                Employee employee = new Employee();
                employee.DivisionId = (int)dr_Division["pn_DivisionID"];
                employee.DivisionName = Convert.IsDBNull(dr_Division["v_DivisionName"]) ? "" : (string)dr_Division["v_DivisionName"];
                DivisionList.Add(employee);
            }
            return DivisionList;
        }


        public Collection<Employee> fn_getShiftList()
        {
            Collection<Employee> ShiftList = new Collection<Employee>();
            _Connection = Con.fn_Connection();
            string _SqlString = "select * from paym_Shift where status='Y'";
            SqlCommand _SSShift = new SqlCommand(_SqlString, _Connection);
            _Connection.Open();
            SqlDataReader dr_Shift = _SSShift.ExecuteReader();
            while (dr_Shift.Read())
            {
                Employee employee = new Employee();
                employee.ShiftId = (int)dr_Shift["pn_ShiftID"];
                employee.ShiftName = Convert.IsDBNull(dr_Shift["v_ShiftName"]) ? "" : (string)dr_Shift["v_ShiftName"];
                employee.ShiftFrom = Convert.IsDBNull(dr_Shift["v_ShiftFrom"]) ? "" : (string)dr_Shift["v_ShiftFrom"];
                employee.ShiftTo = Convert.IsDBNull(dr_Shift["v_ShiftTo"]) ? "" : (string)dr_Shift["v_ShiftTo"];
                ShiftList.Add(employee);
            }
            return ShiftList;
        }

        public Collection<Employee> fn_getCategoryList()
        {
            Collection<Employee> CategoryList = new Collection<Employee>();
            _Connection = Con.fn_Connection();
            string _SqlString = "select * from paym_Category where status='Y'";
            SqlCommand _SSCategory = new SqlCommand(_SqlString, _Connection);
            _Connection.Open();
            SqlDataReader dr_Category = _SSCategory.ExecuteReader();
            while (dr_Category.Read())
            {
                Employee employee = new Employee();
                employee.CategoryId = (int)dr_Category["pn_CategoryID"];
                employee.CategoryName = Convert.IsDBNull(dr_Category["v_CategoryName"]) ? "" : (string)dr_Category["v_CategoryName"];
                CategoryList.Add(employee);
            }
            return CategoryList;
        }

        public Collection<Employee> fn_getCategoryList1(int c)
        {
            Collection<Employee> CategoryList = new Collection<Employee>();
            _Connection = Con.fn_Connection();
            string _SqlString = "select * from paym_Category where status='Y' and BranchID='" + c + "'";
            SqlCommand _SSCategory = new SqlCommand(_SqlString, _Connection);
            _Connection.Open();
            SqlDataReader dr_Category = _SSCategory.ExecuteReader();
            while (dr_Category.Read())
            {
                Employee employee = new Employee();
                employee.CategoryId = (int)dr_Category["pn_CategoryID"];
                employee.CategoryName = Convert.IsDBNull(dr_Category["v_CategoryName"]) ? "" : (string)dr_Category["v_CategoryName"];
                CategoryList.Add(employee);
            }
            return CategoryList;
        }

        public Collection<Employee> fn_getDesignation(Employee e)
        {
            Collection<Employee> DesignationList = new Collection<Employee>();
            _Connection = Con.fn_Connection();
            string _SqlString = "select * from paym_Designation where BranchID = '" + e.BranchId + "' and pn_CompanyID = '" + e.CompanyId + "' and status='Y' and Authority = 'Y'";
            SqlCommand _SSDesignation = new SqlCommand(_SqlString, _Connection);
            _Connection.Open();
            SqlDataReader dr_Designation = _SSDesignation.ExecuteReader();
            while (dr_Designation.Read())
            {
                Employee employee = new Employee();
                employee.DesignationId = (int)dr_Designation["pn_DesignationID"];
                employee.DesignationName = Convert.IsDBNull(dr_Designation["v_DesignationName"]) ? "" : (string)dr_Designation["v_DesignationName"];
                DesignationList.Add(employee);
            }
            return DesignationList;
        }
        public Collection<Employee> fn_getEmployeeDesignation(Employee e)
        {
            Collection<Employee> DesignationList = new Collection<Employee>();
            _Connection = Con.fn_Connection();
            string _SqlString = "select * from paym_Designation where   status='Y'";
            SqlCommand _SSDesignation = new SqlCommand(_SqlString, _Connection);
            _Connection.Open();
            SqlDataReader dr = _SSDesignation.ExecuteReader();
            while (dr.Read())
            {
                Employee emp = new Employee();
                emp.DesignationId = (int)dr["pn_DesignationID"];
                emp.DesgName = Convert.IsDBNull(dr["v_DesignationName"]) ? "" : (string)dr["v_DesignationName"];
                DesignationList.Add(emp);
            }
            return DesignationList;            
        }

        public Collection<Employee> fn_getDesignationList()
        {
            Collection<Employee> DesignationList = new Collection<Employee>();
            _Connection = Con.fn_Connection();            
            string _SqlString = "select * from paym_Designation where   status='Y'";
            SqlCommand _SSDesignation = new SqlCommand(_SqlString, _Connection);
            _Connection.Open();
            SqlDataReader dr_Designation = _SSDesignation.ExecuteReader();
            while (dr_Designation.Read())
            {
                Employee employee = new Employee();
                employee.DesignationId = (int)dr_Designation["pn_DesignationID"];
                employee.DesignationName = Convert.IsDBNull(dr_Designation["v_DesignationName"]) ? "" : (string)dr_Designation["v_DesignationName"];
                DesignationList.Add(employee);
            }
            return DesignationList;
        }

        public Collection<Employee> fn_getLevelList()
        {
            Collection<Employee> DesignationList = new Collection<Employee>();
            _Connection = Con.fn_Connection();
            string _SqlString = "select * from paym_Level where status='Y'";
            SqlCommand _SSDesignation = new SqlCommand(_SqlString, _Connection);
            _Connection.Open();
            SqlDataReader dr_Designation = _SSDesignation.ExecuteReader();
            while (dr_Designation.Read())
            {
                Employee employee = new Employee();
                employee.LevelId = (int)dr_Designation["pn_LevelID"];
                employee.LevelName = Convert.IsDBNull(dr_Designation["v_LevelName"]) ? "" : (string)dr_Designation["v_LevelName"];
                DesignationList.Add(employee);
            }
            return DesignationList;
        }

        public Collection<Employee> fn_getGradeList()
        {
            Collection<Employee> DesignationList = new Collection<Employee>();
            _Connection = Con.fn_Connection();
            string _SqlString = "select * from paym_Grade where status='Y'";
            SqlCommand _SSDesignation = new SqlCommand(_SqlString, _Connection);
            _Connection.Open();
            SqlDataReader dr_Designation = _SSDesignation.ExecuteReader();
            while (dr_Designation.Read())
            {
                Employee employee = new Employee();
                employee.GradeId = (int)dr_Designation["pn_GradeID"];
                employee.GradeName = Convert.IsDBNull(dr_Designation["v_GradeName"]) ? "" : (string)dr_Designation["v_GradeName"];
                DesignationList.Add(employee);
            }
            return DesignationList;
        }

        public Collection<Employee> fn_getJobStatusList()
        {
            Collection<Employee> DesignationList = new Collection<Employee>();
            _Connection = Con.fn_Connection();
            string _SqlString = "select * from paym_JobStatus where status='Y'";
            SqlCommand _SSDesignation = new SqlCommand(_SqlString, _Connection);
            _Connection.Open();
            SqlDataReader dr_Designation = _SSDesignation.ExecuteReader();
            while (dr_Designation.Read())
            {
                Employee employee = new Employee();
                employee.JobStatusId = (int)dr_Designation["pn_JobStatusID"];
                employee.JobStatusName = Convert.IsDBNull(dr_Designation["v_JobStatusName"]) ? "" : (string)dr_Designation["v_JobStatusName"];
                DesignationList.Add(employee);
            }
            return DesignationList;
        }

        public Collection<Employee> fn_getEarningsList()
        {
            Collection<Employee> DesignationList = new Collection<Employee>();
            _Connection = Con.fn_Connection();
            string _SqlString = "select * from paym_Earnings where status='Y'";
            SqlCommand _SSDesignation = new SqlCommand(_SqlString, _Connection);
            _Connection.Open();
            SqlDataReader dr_Designation = _SSDesignation.ExecuteReader();
            while (dr_Designation.Read())
            {
                Employee employee = new Employee();
                employee.EarningsId = (int)dr_Designation["pn_EarningsID"];
                employee.EarningsName = Convert.IsDBNull(dr_Designation["v_EarningsName"]) ? "" : (string)dr_Designation["v_EarningsName"];
                employee.EarningsType = Convert.IsDBNull(dr_Designation["c_EarningType"]) ? "" : (string)dr_Designation["c_EarningType"];

                DesignationList.Add(employee);
            }
            return DesignationList;
        }

        public Collection<Employee> fn_getEarningsList_Regular(Employee ee)
        {
            Collection<Employee> DesignationList = new Collection<Employee>();
            _Connection = Con.fn_Connection();
            string _SqlString = "select * from paym_Earnings where status='Y' and pn_CompanyID='" + ee.CompanyId + "' and pn_BranchID='" + ee.BranchId + "' and c_regular='Y'";
            SqlCommand _SSDesignation = new SqlCommand(_SqlString, _Connection);
            _Connection.Open();
            SqlDataReader dr_Designation = _SSDesignation.ExecuteReader();
            while (dr_Designation.Read())
            {
                Employee employee = new Employee();
                employee.EarningsId = (int)dr_Designation["pn_EarningsID"];
                employee.EarningsName = Convert.IsDBNull(dr_Designation["v_EarningsName"]) ? "" : (string)dr_Designation["v_EarningsName"];
                
                DesignationList.Add(employee);
            }
            return DesignationList;
        }


        //$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$


        public Collection<Employee> fn_EmptyDepartmentList()
        {
            Collection<Employee> DepartmentList = new Collection<Employee>();
            _Connection = Con.fn_Connection();
            string _SqlString = "select * from paym_Department where status='Y'";
            SqlCommand _SSDepartment = new SqlCommand(_SqlString, _Connection);
            _Connection.Open();
            SqlDataReader dr_Department = _SSDepartment.ExecuteReader();
            while (dr_Department.Read())
            {
                Employee employee = new Employee();
                employee.DepartmentId = (int)dr_Department["pn_DepartmentID"];
                employee.DepartmentName = "v_DepartmentName";
                DepartmentList.Add(employee);
            }
            return DepartmentList;
        }

        public Collection<Employee> fn_EmptycourseList(Employee e)
        {
            Collection<Employee> courseList = new Collection<Employee>();
            _Connection = Con.fn_Connection();
            string _SqlString = "select * from hrmm_Course where status='Y' and branchID='"+e.BranchId+"'";
            SqlCommand _SScourse = new SqlCommand(_SqlString, _Connection);
            _Connection.Open();
            SqlDataReader dr_course = _SScourse.ExecuteReader();
            while (dr_course.Read())
            {
                Employee employee = new Employee();
                employee.CourseId = (int)dr_course["pn_courseID"];
                employee.CourseName = "No course";
                courseList.Add(employee);
            }
            return courseList;
        }

        public Collection<Employee> fn_EmptyskillList(Employee e)
        {
            Collection<Employee> skillList = new Collection<Employee>();
            _Connection = Con.fn_Connection();
            string _SqlString = "select * from hrmm_SkillsMaster where status='Y' and branchID='" + e.BranchId + "'";
            SqlCommand _SSskill = new SqlCommand(_SqlString, _Connection);
            _Connection.Open();
            SqlDataReader dr_skill = _SSskill.ExecuteReader();
            while (dr_skill.Read())
            {
                Employee employee = new Employee();
                employee.SkillId = (int)dr_skill["pn_skillID"];
                employee.SkillName = "No skill";
                skillList.Add(employee);
            }
            return skillList;
        }

        public Collection<Employee> fn_EmptySpecialization(Employee e)
        {
            Collection<Employee> SpecializationList = new Collection<Employee>();
            _Connection = Con.fn_Connection();
            string _sqlstring = "select * from hrmm_Specialization where status='y' and  pn_BranchID='" + e.BranchId + "'";
            SqlCommand ssspecialization = new SqlCommand(_sqlstring, _Connection);
            _Connection.Open();
            SqlDataReader drspecialization = ssspecialization.ExecuteReader();
            while (drspecialization.Read())
            {
                Employee emp = new Employee();                
                emp.specializationID = (int)drspecialization["pn_SpecializationId"];
                emp.specializationName = "No Specialization";
                SpecializationList.Add(emp);
            }
            return SpecializationList;
        }

        public Collection<Employee> fn_EmptyprojectsiteList()
        {
            Collection<Employee> projectsiteList = new Collection<Employee>();
            _Connection = Con.fn_Connection();
            string _SqlString = "select * from paym_projectsite where status='Y'";
            SqlCommand _SSprojectsite = new SqlCommand(_SqlString, _Connection);
            _Connection.Open();
            SqlDataReader dr_projectsite = _SSprojectsite.ExecuteReader();
            while (dr_projectsite.Read())
            {
                Employee employee = new Employee();
                employee.ProjectsiteId = (int)dr_projectsite["pn_projectsiteID"];
                employee.ProjectsiteName = "No projectsite";
                projectsiteList.Add(employee);
            }
            return projectsiteList;
        }

        

        public Collection<Employee> fn_EmptyDeductionList()
        {
            Collection<Employee> DeductionList = new Collection<Employee>();
            _Connection = Con.fn_Connection();
            string _SqlString = "select * from paym_Deduction where status='Y'";
            SqlCommand _SSDeduction = new SqlCommand(_SqlString, _Connection);
            _Connection.Open();
            SqlDataReader dr_Deduction = _SSDeduction.ExecuteReader();
            while (dr_Deduction.Read())
            {
                Employee employee = new Employee();
                employee.DeductionId = (int)dr_Deduction["pn_DeductionId"];
                employee.DeductionName = "No Deduction";
                DeductionList.Add(employee);
            }
            return DeductionList;
        }
        //public Collection<Employee> fn_getJobCodeList(int e, int e1)
        //{
        //    Collection<Employee> JobCodeList = new Collection<Employee>();
        //    _Connection = Con.fn_Connection();
        //    string _SqlString = "select * from Recruit_jobrequistion where pn_BranchID='" + e + "' and pn_CompanyID='" + e1 + "' and Status='Approved'";
        //    SqlCommand _SSJobCode = new SqlCommand(_SqlString, _Connection);
        //    _Connection.Open();
        //    SqlDataReader dr_JobCode = _SSJobCode.ExecuteReader();
        //    while (dr_JobCode.Read())
        //    {
        //        Employee employee = new Employee();
        //        employee.JobCodeId = Convert.IsDBNull(dr_JobCode["Job_Code"]) ? "" : (string)dr_JobCode["Job_Code"];
        //        employee.JobCodeName = (string)dr_JobCode["Job_Code"] + "-" + (string)dr_JobCode["Job_Title"];

        //        JobCodeList.Add(employee);
        //    }
        //    return JobCodeList;
        //}


        public Collection<Employee> fn_EmptyDivisionList()
        {
            Collection<Employee> DivisionList = new Collection<Employee>();
            _Connection = Con.fn_Connection();
            string _SqlString = "select * from paym_Division where status='Y'";
            SqlCommand _SSDivision = new SqlCommand(_SqlString, _Connection);
            _Connection.Open();
            SqlDataReader dr_Division = _SSDivision.ExecuteReader();
            while (dr_Division.Read())
            {
                Employee employee = new Employee();
                employee.DivisionId = (int)dr_Division["pn_DivisionID"];
                employee.DivisionName = "No Division";
                DivisionList.Add(employee);
            }
            return DivisionList;
        }

        public Collection<Employee> fn_EmptyShiftList()
        {
            Collection<Employee> ShiftList = new Collection<Employee>();
            _Connection = Con.fn_Connection();
            string _SqlString = "select * from paym_Shift where status='Y'";
            SqlCommand _SSShift = new SqlCommand(_SqlString, _Connection);
            _Connection.Open();
            SqlDataReader dr_Shift = _SSShift.ExecuteReader();
            while (dr_Shift.Read())
            {
                Employee employee = new Employee();
                employee.ShiftId = (int)dr_Shift["pn_ShiftID"];
                employee.ShiftName = "No Shift";
                employee.ShiftFrom = "";
                employee.ShiftTo = "";
                ShiftList.Add(employee);
            }
            return ShiftList;
        }

        public Collection<Employee> fn_EmptyCategoryList()
        {
            Collection<Employee> CategoryList = new Collection<Employee>();
            _Connection = Con.fn_Connection();
            string _SqlString = "select * from paym_Category where status='Y'";
            SqlCommand _SSCategory = new SqlCommand(_SqlString, _Connection);
            _Connection.Open();
            SqlDataReader dr_Category = _SSCategory.ExecuteReader();
            while (dr_Category.Read())
            {
                Employee employee = new Employee();
                employee.CategoryId = (int)dr_Category["pn_CategoryID"];
                employee.CategoryName = "No Category";
                CategoryList.Add(employee);
            }
            return CategoryList;
        }

        public Collection<Employee> fn_EmptyDesignationList()
        {
            Collection<Employee> DesignationList = new Collection<Employee>();
            _Connection = Con.fn_Connection();
            string _SqlString = "select * from paym_Designation where status='Y'";
            SqlCommand _SSDesignation = new SqlCommand(_SqlString, _Connection);
            _Connection.Open();
            SqlDataReader dr_Designation = _SSDesignation.ExecuteReader();
            while (dr_Designation.Read())
            {
                Employee employee = new Employee();
                employee.DesignationId = (int)dr_Designation["pn_DesignationID"];
                employee.DesignationName = "No Designation";
                DesignationList.Add(employee);
            }
            return DesignationList;
        }

        public Collection<Employee> fn_EmptyLevelList()
        {
            Collection<Employee> DesignationList = new Collection<Employee>();
            _Connection = Con.fn_Connection();
            string _SqlString = "select * from paym_Level where status='Y'";
            SqlCommand _SSDesignation = new SqlCommand(_SqlString, _Connection);
            _Connection.Open();
            SqlDataReader dr_Designation = _SSDesignation.ExecuteReader();
            while (dr_Designation.Read())
            {
                Employee employee = new Employee();
                employee.LevelId = (int)dr_Designation["pn_LevelID"];
                employee.LevelName = "No Level";
                DesignationList.Add(employee);
            }
            return DesignationList;
        }

        public Collection<Employee> fn_EmptyGradeList()
        {
            Collection<Employee> DesignationList = new Collection<Employee>();
            _Connection = Con.fn_Connection();
            string _SqlString = "select * from paym_Grade where status='Y'";
            SqlCommand _SSDesignation = new SqlCommand(_SqlString, _Connection);
            _Connection.Open();
            SqlDataReader dr_Designation = _SSDesignation.ExecuteReader();
            while (dr_Designation.Read())
            {
                Employee employee = new Employee();
                employee.GradeId = (int)dr_Designation["pn_GradeID"];
                employee.GradeName = "No Grade";
                DesignationList.Add(employee);
            }
            return DesignationList;
        }

        public Collection<Employee> fn_EmptyJobStatusList()
        {
            Collection<Employee> DesignationList = new Collection<Employee>();
            _Connection = Con.fn_Connection();
            string _SqlString = "select * from paym_JobStatus where status='Y'";
            SqlCommand _SSDesignation = new SqlCommand(_SqlString, _Connection);
            _Connection.Open();
            SqlDataReader dr_Designation = _SSDesignation.ExecuteReader();
            while (dr_Designation.Read())
            {
                Employee employee = new Employee();
                employee.JobStatusId = (int)dr_Designation["pn_JobStatusID"];
                employee.JobStatusName = "No JobStatus";
                DesignationList.Add(employee);
            }
            return DesignationList;
        }


        //$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$



        public Collection<Employee> fn_Department(int bid)
        {
            Collection<Employee> DepartmentList = new Collection<Employee>();
            _Connection = Con.fn_Connection();
            string _SqlString = "select * from paym_Department where pn_DepartmentID !=1 and status='Y' and pn_BranchID = '"+bid+"'";
            SqlCommand _SSDepartment = new SqlCommand(_SqlString, _Connection);
            _Connection.Open();
            SqlDataReader dr_Department = _SSDepartment.ExecuteReader();
               while (dr_Department.Read())
                {
                    Employee employee = new Employee();
                    employee.DepartmentId = (int)dr_Department["pn_DepartmentID"];
                    employee.DepartmentName = Convert.IsDBNull(dr_Department["v_DepartmentName"]) ? "" : (string)dr_Department["v_DepartmentName"];
                    DepartmentList.Add(employee);
                }
            
            return DepartmentList;
           
        }


        public Collection<Employee> fn_Department1(Employee e)
        {
            Collection<Employee> DepartmentList = new Collection<Employee>();
            _Connection = Con.fn_Connection();
            string _SqlString = "select * from paym_Department where pn_DepartmentID !=1 and status='Y' and pn_BranchID='" + e.BranchId + "'";
            SqlCommand _SSDepartment = new SqlCommand(_SqlString, _Connection);
            _Connection.Open();
            SqlDataReader dr_Department = _SSDepartment.ExecuteReader();
            while (dr_Department.Read())
            {
                Employee employee = new Employee();
                employee.DepartmentId = (int)dr_Department["pn_DepartmentID"];
                employee.DepartmentName = Convert.IsDBNull(dr_Department["v_DepartmentName"]) ? "" : (string)dr_Department["v_DepartmentName"];
                employee.BranchId = (int)dr_Department["pn_BranchID"];
                DepartmentList.Add(employee);
            }
            return DepartmentList;
        }

        public Collection<Employee> fn_Department()
        {
            Collection<Employee> DepartmentList = new Collection<Employee>();
            _Connection = Con.fn_Connection();
            string _SqlString = "select * from paym_Department";
            SqlCommand _SSDepartment = new SqlCommand(_SqlString, _Connection);
            _Connection.Open();
            SqlDataReader dr_Department = _SSDepartment.ExecuteReader();
            
            while (dr_Department.Read())
            {
                Employee employee = new Employee();
                employee.DepartmentId = (int)dr_Department["pn_DepartmentID"];
                employee.DepartmentName = Convert.IsDBNull(dr_Department["v_DepartmentName"]) ? "" : (string)dr_Department["v_DepartmentName"];               
                DepartmentList.Add(employee);
            }
            return DepartmentList;
        }

        public Collection<Employee> fn_section()
        {
            Collection<Employee> sectionList = new Collection<Employee>();
            _Connection = Con.fn_Connection();
            string _SqlString = "select * from sections where status='Y'";
            SqlCommand _SSsection = new SqlCommand(_SqlString, _Connection);
            _Connection.Open();
            SqlDataReader dr_section = _SSsection.ExecuteReader();
            while (dr_section.Read())
            {
                Employee employee = new Employee();
                employee.SectionId = (int)dr_section["Section_id"];
                employee.SectionName = Convert.IsDBNull(dr_section["Section_Name"]) ? "" : (string)dr_section["Section_Name"];
                sectionList.Add(employee);
            }
            return sectionList;
        }

        public Collection<Employee> fn_course()
        {
            Collection<Employee> courseList = new Collection<Employee>();
            _Connection = Con.fn_Connection();
            string _SqlString = "select * from hrmm_Course where pn_courseID !=1 and status='Y'";
            SqlCommand _SScourse = new SqlCommand(_SqlString, _Connection);
            _Connection.Open();
            SqlDataReader dr_course = _SScourse.ExecuteReader();
            while (dr_course.Read())
            {
                Employee employee = new Employee();
                employee.CourseId = (int)dr_course["pn_courseID"];
                employee.CourseName = Convert.IsDBNull(dr_course["v_courseName"]) ? "" : (string)dr_course["v_courseName"];
                courseList.Add(employee);
            }
            return courseList;
        }


        public Collection<Employee> fn_course1(int courid)
        {
            
            Collection<Employee> courseList = new Collection<Employee>();
            _Connection = Con.fn_Connection();
            string _SqlString = "select * from hrmm_Course where pn_courseID !=1 and status='Y' and BranchID='"+courid+"'";
            SqlCommand _SScourse = new SqlCommand(_SqlString, _Connection);
            _Connection.Open();
            SqlDataReader dr_course = _SScourse.ExecuteReader();
            while (dr_course.Read())
            {
                Employee employee = new Employee();
                employee.CourseId = (int)dr_course["pn_courseID"];
                employee.CourseName = Convert.IsDBNull(dr_course["v_courseName"]) ? "" : (string)dr_course["v_courseName"];
                employee.BranchId = (int)dr_course["BranchID"];
                courseList.Add(employee);
            }
            return courseList;
        }

        public Collection<Employee> fn_Specialization(int specializationID)
        {
            Collection<Employee> SpecializationList = new Collection<Employee>();
            _Connection = Con.fn_Connection();
            string _sqlstring = "select * from hrmm_Specialization where status='y' and pn_BranchID='" + specializationID + "'";
            SqlCommand ssspecialization = new SqlCommand(_sqlstring, _Connection);
            _Connection.Open();
            SqlDataReader drspecialization = ssspecialization.ExecuteReader();
            while (drspecialization.Read())
            {
                Employee emp = new Employee();
                emp.specializationID = (int)drspecialization["pn_SpecializationId"];
                emp.specializationName = Convert.IsDBNull(drspecialization["v_SpecializationName"]) ? "" : (string)drspecialization["v_SpecializationName"];
                SpecializationList.Add(emp);
            }
            return SpecializationList;
        }

        public Collection<Employee> fn_skill()
        {
            Collection<Employee> skillList = new Collection<Employee>();
            _Connection = Con.fn_Connection();
            string _SqlString = "select * from hrmm_SkillsMaster where pn_skillID !=1 and status='Y'";
            SqlCommand _SSskill = new SqlCommand(_SqlString, _Connection);
            _Connection.Open();
            SqlDataReader dr_skill = _SSskill.ExecuteReader();
            while (dr_skill.Read())
            {
                Employee employee = new Employee();
                employee.SkillId = (int)dr_skill["pn_skillID"];
                employee.SkillName = Convert.IsDBNull(dr_skill["v_skillName"]) ? "" : (string)dr_skill["v_skillName"];
                skillList.Add(employee);
            }
            return skillList;
        }


        public Collection<Employee> fn_skill1(Employee e)
        {
            Collection<Employee> skillList = new Collection<Employee>();
            _Connection = Con.fn_Connection();
            string _SqlString = "select * from hrmm_SkillsMaster where pn_skillID !=1 and status='Y' and BranchID='" + e.BranchId + "'";
            SqlCommand _SSskill = new SqlCommand(_SqlString, _Connection);
            _Connection.Open();
            SqlDataReader dr_skill = _SSskill.ExecuteReader();
            while (dr_skill.Read())
            {
                Employee employee = new Employee();
                employee.SkillId = (int)dr_skill["pn_skillID"];
                employee.SkillName = Convert.IsDBNull(dr_skill["v_skillName"]) ? "" : (string)dr_skill["v_skillName"];
                employee.CourseId = (int)dr_skill["BranchID"];
                skillList.Add(employee);
            }
            return skillList;
        }

  
        public Collection<Employee> fn_projectsite()
        {
            Collection<Employee> projectsiteList = new Collection<Employee>();
            _Connection = Con.fn_Connection();
            string _SqlString = "select * from paym_projectsite where pn_projectsiteID !=1 and status='Y'";
            SqlCommand _SSprojectsite = new SqlCommand(_SqlString, _Connection);
            _Connection.Open();
            SqlDataReader dr_projectsite = _SSprojectsite.ExecuteReader();
            while (dr_projectsite.Read())
            {
                Employee employee = new Employee();
                employee.ProjectsiteId = (int)dr_projectsite["pn_projectsiteID"];
                employee.ProjectsiteName = Convert.IsDBNull(dr_projectsite["v_projectsiteName"]) ? "" : (string)dr_projectsite["v_projectsiteName"];
                employee.Location = Convert.IsDBNull(dr_projectsite["Location"]) ? "" : (string)dr_projectsite["Location"];
                employee.ProjectName = Convert.IsDBNull(dr_projectsite["projectName"]) ? "" : (string)dr_projectsite["projectName"];
                projectsiteList.Add(employee);
            }
            return projectsiteList;
        }


        public Collection<Employee> fn_projectsite1()
        {
            Collection<Employee> projectsiteList = new Collection<Employee>();
            _Connection = Con.fn_Connection();
            string _SqlString = "select * from paym_projectsite where pn_projectsiteID !=1 and status='Y' and BranchID='" + _BranchId + "'";
            SqlCommand _SSprojectsite = new SqlCommand(_SqlString, _Connection);
            _Connection.Open();
            SqlDataReader dr_projectsite = _SSprojectsite.ExecuteReader();
            while (dr_projectsite.Read())
            {
                Employee employee = new Employee();
                employee.ProjectsiteId = (int)dr_projectsite["pn_projectsiteID"];
                employee.ProjectsiteName = Convert.IsDBNull(dr_projectsite["v_projectsiteName"]) ? "" : (string)dr_projectsite["v_projectsiteName"];
                employee.Location = Convert.IsDBNull(dr_projectsite["Location"]) ? "" : (string)dr_projectsite["Location"];
                employee.ProjectName = Convert.IsDBNull(dr_projectsite["projectName"]) ? "" : (string)dr_projectsite["projectName"];
                employee.BranchId = (int)dr_projectsite["BranchID"];
                projectsiteList.Add(employee);
            }
            return projectsiteList;
        }



        public Collection<Employee> fn_Division()
        {
            Collection<Employee> DivisionList = new Collection<Employee>();
            _Connection = Con.fn_Connection();
            string _SqlString = "select * from paym_Division where pn_DivisionID !=1 and status='Y'";
            SqlCommand _SSDivision = new SqlCommand(_SqlString, _Connection);
            _Connection.Open();
            SqlDataReader dr_Division = _SSDivision.ExecuteReader();
            while (dr_Division.Read())
            {
                Employee employee = new Employee();
                employee.DivisionId = (int)dr_Division["pn_DivisionID"];
                employee.DivisionName = Convert.IsDBNull(dr_Division["v_DivisionName"]) ? "" : (string)dr_Division["v_DivisionName"];
                DivisionList.Add(employee);
            }
            return DivisionList;
        }


        public Collection<Employee> fn_Division1()
        {
            Collection<Employee> DivisionList = new Collection<Employee>();
            _Connection = Con.fn_Connection();
            string _SqlString = "select * from paym_Division where pn_DivisionID !=1 and status='Y' and BranchID='" + _BranchId + "'";
            SqlCommand _SSDivision = new SqlCommand(_SqlString, _Connection);
            _Connection.Open();
            SqlDataReader dr_Division = _SSDivision.ExecuteReader();
            while (dr_Division.Read())
            {
                Employee employee = new Employee();
                employee.DivisionId = (int)dr_Division["pn_DivisionID"];
                employee.DivisionName = Convert.IsDBNull(dr_Division["v_DivisionName"]) ? "" : (string)dr_Division["v_DivisionName"];
                employee.BranchId = (int)dr_Division["BranchID"];
                DivisionList.Add(employee);
            }
            return DivisionList;
        }



        public Collection<Employee> fn_Shift()
        {
            Collection<Employee> ShiftList = new Collection<Employee>();
            _Connection = Con.fn_Connection();
            string _SqlString = "select * from paym_Shift where pn_ShiftID !=1 and status='Y'";
            SqlCommand _SSShift = new SqlCommand(_SqlString, _Connection);
            _Connection.Open();
            SqlDataReader dr_Shift = _SSShift.ExecuteReader();
            while (dr_Shift.Read())
            {
                Employee employee = new Employee();
                employee.ShiftId = (int)dr_Shift["pn_ShiftID"];
                employee.ShiftName = Convert.IsDBNull(dr_Shift["v_ShiftName"]) ? "" : (string)dr_Shift["v_ShiftName"];
                employee.ShiftFrom = Convert.IsDBNull(dr_Shift["v_ShiftFrom"]) ? "" : (string)dr_Shift["v_ShiftFrom"];
                employee.ShiftTo = Convert.IsDBNull(dr_Shift["v_ShiftTo"]) ? "" : (string)dr_Shift["v_ShiftTo"];
                ShiftList.Add(employee);
            }
            return ShiftList;
        }


        public Collection<Employee> fn_Shift1(int sid)
        {
            Collection<Employee> ShiftList = new Collection<Employee>();
            _Connection = Con.fn_Connection();
            string _SqlString = "select * from paym_Shift where pn_ShiftID !=1 and status='Y' and BranchID='" + sid + "'";
            SqlCommand _SSShift = new SqlCommand(_SqlString, _Connection);
            _Connection.Open();
            SqlDataReader dr_Shift = _SSShift.ExecuteReader();
            while (dr_Shift.Read())
            {
                Employee employee = new Employee();
                employee.ShiftId = (int)dr_Shift["pn_ShiftID"];
                employee.ShiftName = Convert.IsDBNull(dr_Shift["v_ShiftName"]) ? "" : (string)dr_Shift["v_ShiftName"];
                employee.ShiftFrom = Convert.IsDBNull(dr_Shift["v_ShiftFrom"]) ? "" : (string)dr_Shift["v_ShiftFrom"];
                employee.ShiftTo = Convert.IsDBNull(dr_Shift["v_ShiftTo"]) ? "" : (string)dr_Shift["v_ShiftTo"];
                employee.ShiftCategory = Convert.IsDBNull(dr_Shift["v_ShiftName"]) ? "" : (string)dr_Shift["v_ShiftCategory"]; 
                employee.BranchId = (int)dr_Shift["BranchID"];
                ShiftList.Add(employee);
            }
            return ShiftList;
        }



        public Collection<Employee> fn_Category()
        {
            Collection<Employee> CategoryList = new Collection<Employee>();
            _Connection = Con.fn_Connection();
            string _SqlString = "select * from paym_Category where pn_CategoryID !=1 and status='Y'";
            SqlCommand _SSCategory = new SqlCommand(_SqlString, _Connection);
            _Connection.Open();
            SqlDataReader dr_Category = _SSCategory.ExecuteReader();
            while (dr_Category.Read())
            {
                Employee employee = new Employee();
                employee.CategoryId = (int)dr_Category["pn_CategoryID"];
                employee.CategoryName = Convert.IsDBNull(dr_Category["v_CategoryName"]) ? "" : (string)dr_Category["v_CategoryName"];
                CategoryList.Add(employee);
            }
            return CategoryList;
        }


        public Collection<Employee> fn_Category1()
        {
            Collection<Employee> CategoryList = new Collection<Employee>();
            _Connection = Con.fn_Connection();
            string _SqlString = "select * from paym_Category where pn_CategoryID !=1 and status='Y' and BranchID='" + _BranchId + "'";
            SqlCommand _SSCategory = new SqlCommand(_SqlString, _Connection);
            _Connection.Open();
            SqlDataReader dr_Category = _SSCategory.ExecuteReader();
            while (dr_Category.Read())
            {
                Employee employee = new Employee();
                employee.CategoryId = (int)dr_Category["pn_CategoryID"];
                employee.CategoryName = Convert.IsDBNull(dr_Category["v_CategoryName"]) ? "" : (string)dr_Category["v_CategoryName"];
                employee.BranchId = (int)dr_Category["BranchID"];
                CategoryList.Add(employee);
            }
            return CategoryList;
        }


        public Collection<Employee> fn_Designation()
        {
            Collection<Employee> DesignationList = new Collection<Employee>();
            _Connection = Con.fn_Connection();
            string _SqlString = "select * from paym_Designation where pn_DesignationID !=1 and status='Y'";
            SqlCommand _SSDesignation = new SqlCommand(_SqlString, _Connection);
            _Connection.Open();
            SqlDataReader dr_Designation = _SSDesignation.ExecuteReader();
            while (dr_Designation.Read())
            {
                Employee employee = new Employee();
                employee.DesignationId = (int)dr_Designation["pn_DesignationID"];
                employee.DesignationName = Convert.IsDBNull(dr_Designation["v_DesignationName"]) ? "" : (string)dr_Designation["v_DesignationName"];
                DesignationList.Add(employee);
            }
            return DesignationList;
        }


        public Collection<Employee> fn_Designation1()
        {
            Collection<Employee> DesignationList = new Collection<Employee>();
            _Connection = Con.fn_Connection();
            string _SqlString = "select * from paym_Designation where pn_DesignationID !=1 and status='Y' and BranchID='" + _BranchId + "'";
            SqlCommand _SSDesignation = new SqlCommand(_SqlString, _Connection);
            _Connection.Open();
            SqlDataReader dr_Designation = _SSDesignation.ExecuteReader();
            while (dr_Designation.Read())
            {
                Employee employee = new Employee();
                employee.DesignationId = (int)dr_Designation["pn_DesignationID"];
                employee.DesignationName = Convert.IsDBNull(dr_Designation["v_DesignationName"]) ? "" : (string)dr_Designation["v_DesignationName"];
                employee.BranchId = (int)dr_Designation["BranchID"];
                DesignationList.Add(employee);
            }
            return DesignationList;
        }


        public Collection<Employee> fn_Level()
        {
            Collection<Employee> DesignationList = new Collection<Employee>();
            _Connection = Con.fn_Connection();
            string _SqlString = "select * from paym_Level where pn_LevelID !=1 and status='Y'";
            SqlCommand _SSDesignation = new SqlCommand(_SqlString, _Connection);
            _Connection.Open();
            SqlDataReader dr_Designation = _SSDesignation.ExecuteReader();
            while (dr_Designation.Read())
            {
                Employee employee = new Employee();
                employee.LevelId = (int)dr_Designation["pn_LevelID"];
                employee.LevelName = Convert.IsDBNull(dr_Designation["v_LevelName"]) ? "" : (string)dr_Designation["v_LevelName"];
                DesignationList.Add(employee);
            }
            return DesignationList;
        }

        public string OverHeadingCostUpdate(Employee e)
        {
            try
            {
                _Connection = Con.fn_Connection();
                SqlCommand _Cmd = new SqlCommand("sp_paym_OverHeadingCost", _Connection);
                _Cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter[] _ISPParamter = new SqlParameter[5];

                _ISPParamter[0] = new SqlParameter("@pn_CompanyID", SqlDbType.VarChar);
                _ISPParamter[0].Value = e.CompanyId;
                _ISPParamter[1] = new SqlParameter("BranchID", SqlDbType.Int);
                _ISPParamter[1].Value = e.BranchId;
                _ISPParamter[2] = new SqlParameter("@overHeadingID", SqlDbType.Int);
                _ISPParamter[2].Value = e.OverHeadingCostId;
                _ISPParamter[3] = new SqlParameter("@OverHeadingName", SqlDbType.VarChar);
                _ISPParamter[3].Value = e.OverHeadingCostName;
                _ISPParamter[4] = new SqlParameter("@status", SqlDbType.VarChar);
                _ISPParamter[4].Value = "Y";

                for (int i = 0; i < _ISPParamter.Length; i++)
                {
                    _Cmd.Parameters.Add(_ISPParamter[i]);
                }
                _Connection.Open();
                _Cmd.ExecuteNonQuery();
                _Connection.Close();
                return "0";
            }
            catch (SqlException Ex)
            {
                return "1";
            }
        }


        public Collection<Employee> fn_overheadingcost()
        {
            Collection<Employee> DesignationList = new Collection<Employee>();
            _Connection = Con.fn_Connection();
            string _SqlString = "select * from paym_OverHeadingCost where overHeadingID !=1 and status='Y' and BranchID='" + _BranchId + "'";
            SqlCommand _SSDesignation = new SqlCommand(_SqlString, _Connection);
            _Connection.Open();
            SqlDataReader dr_Designation = _SSDesignation.ExecuteReader();
            while (dr_Designation.Read())
            {
                Employee employee = new Employee();
                employee.OverHeadingCostId = (int)dr_Designation["overHeadingID"];
                employee.OverHeadingCostName = Convert.IsDBNull(dr_Designation["OverHeadingName"]) ? "" : (string)dr_Designation["OverHeadingName"];
                employee.BranchId = (int)dr_Designation["BranchID"];
                DesignationList.Add(employee);
            }
            return DesignationList;
        }


        public Collection<Employee> fn_EmptyOverHeadingCostList()
        {
            Collection<Employee> DesignationList = new Collection<Employee>();
            _Connection = Con.fn_Connection();
            string _SqlString = "select * from paym_OverHeadingCost where status='Y'";
            SqlCommand _SSDesignation = new SqlCommand(_SqlString, _Connection);
            _Connection.Open();
            SqlDataReader dr_Designation = _SSDesignation.ExecuteReader();
            while (dr_Designation.Read())
            {
                Employee employee = new Employee();
                employee.LevelId = (int)dr_Designation["overHeadingID"];
                employee.LevelName = "No OverHeadingCost";
                DesignationList.Add(employee);
            }
            return DesignationList;
        }

        public void fn_Update_OverHeadingCost(Employee e)
        {


            _Connection = Con.fn_Connection();

            _Connection.Open();

            SqlCommand _RSC_can = new SqlCommand("update paym_OverHeadingCost set OverHeadingName='" + e.OverHeadingCostName + "' where overHeadingID=" + e.OverHeadingCostId + "", _Connection);

            _RSC_can.ExecuteNonQuery();

            _Connection.Close();


        }

        public Collection<Employee> fn_Level1()
        {
            Collection<Employee> DesignationList = new Collection<Employee>();
            _Connection = Con.fn_Connection();
            string _SqlString = "select * from paym_Level where pn_LevelID !=1 and status='Y' and BranchID='" + _BranchId + "'";
            SqlCommand _SSDesignation = new SqlCommand(_SqlString, _Connection);
            _Connection.Open();
            SqlDataReader dr_Designation = _SSDesignation.ExecuteReader();
            while (dr_Designation.Read())
            {
                Employee employee = new Employee();
                employee.LevelId = (int)dr_Designation["pn_LevelID"];
                employee.LevelName = Convert.IsDBNull(dr_Designation["v_LevelName"]) ? "" : (string)dr_Designation["v_LevelName"];
                employee.BranchId = (int)dr_Designation["BranchID"];
                DesignationList.Add(employee);
            }
            return DesignationList;
        }



        public Collection<Employee> fn_Grade1()
        {
            Collection<Employee> DesignationList = new Collection<Employee>();
            _Connection = Con.fn_Connection();
            string _SqlString = "select * from paym_Grade where pn_GradeID !=1 and status='Y' and BranchID='" + _BranchId + "'";
            SqlCommand _SSDesignation = new SqlCommand(_SqlString, _Connection);
            _Connection.Open();
            SqlDataReader dr_Designation = _SSDesignation.ExecuteReader();
            while (dr_Designation.Read())
            {
                Employee employee = new Employee();
                employee.GradeId = (int)dr_Designation["pn_GradeID"];
                employee.GradeName = Convert.IsDBNull(dr_Designation["v_GradeName"]) ? "" : (string)dr_Designation["v_GradeName"];
                employee.BranchId = (int)dr_Designation["BranchID"];
                DesignationList.Add(employee);
            }
            return DesignationList;
        }


        public Collection<Employee> fn_Grade()
        {
            Collection<Employee> DesignationList = new Collection<Employee>();
            _Connection = Con.fn_Connection();
            string _SqlString = "select * from paym_Grade where pn_GradeID !=1 and status='Y' ";
            SqlCommand _SSDesignation = new SqlCommand(_SqlString, _Connection);
            _Connection.Open();
            SqlDataReader dr_Designation = _SSDesignation.ExecuteReader();
            while (dr_Designation.Read())
            {
                Employee employee = new Employee();
                employee.GradeId = (int)dr_Designation["pn_GradeID"];
                employee.GradeName = Convert.IsDBNull(dr_Designation["v_GradeName"]) ? "" : (string)dr_Designation["v_GradeName"];
                DesignationList.Add(employee);
            }
            return DesignationList;
        }


        public Collection<Employee> fn_JobStatus()
        {
            Collection<Employee> DesignationList = new Collection<Employee>();
            _Connection = Con.fn_Connection();
            string _SqlString = "select * from paym_JobStatus where pn_JobStatusID !=1 and status='Y'";
            SqlCommand _SSDesignation = new SqlCommand(_SqlString, _Connection);
            _Connection.Open();
            SqlDataReader dr_Designation = _SSDesignation.ExecuteReader();
            while (dr_Designation.Read())
            {
                Employee employee = new Employee();
                employee.JobStatusId = (int)dr_Designation["pn_JobStatusID"];
                employee.JobStatusName = Convert.IsDBNull(dr_Designation["v_JobStatusName"]) ? "" : (string)dr_Designation["v_JobStatusName"];
                DesignationList.Add(employee);
            }
            return DesignationList;
        }



        public Collection<Employee> fn_JobStatus1()
        {
            Collection<Employee> DesignationList = new Collection<Employee>();
            _Connection = Con.fn_Connection();
            string _SqlString = "select * from paym_JobStatus where pn_JobStatusID !=1 and status='Y' and BranchID='" + _BranchId + "'";
            SqlCommand _SSDesignation = new SqlCommand(_SqlString, _Connection);
            _Connection.Open();
            SqlDataReader dr_Designation = _SSDesignation.ExecuteReader();
            while (dr_Designation.Read())
            {
                Employee employee = new Employee();
                employee.JobStatusId = (int)dr_Designation["pn_JobStatusID"];
                employee.JobStatusName = Convert.IsDBNull(dr_Designation["v_JobStatusName"]) ? "" : (string)dr_Designation["v_JobStatusName"];
                employee.BranchId=(int)dr_Designation["BranchID"];
                DesignationList.Add(employee);
            }
            return DesignationList;
        }


        //$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$



        public string Task_Shedule(Employee e)
        {
            try
            {
                _Connection = Con.fn_Connection();

                SqlCommand _Cmd = new SqlCommand("sp_Task_Shedule", _Connection);
                _Cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter[] _ISPParamter = new SqlParameter[8];
                _ISPParamter[0] = new SqlParameter("@pn_CompanyID", SqlDbType.Int);
                _ISPParamter[0].Value = e.CompanyId;
                _ISPParamter[1] = new SqlParameter("@pn_BranchID", SqlDbType.Int);
                _ISPParamter[1].Value = e.BranchId;
                _ISPParamter[2] = new SqlParameter("@pn_EmployeeID", SqlDbType.Int);
                _ISPParamter[2].Value = e.EmployeeId;
                _ISPParamter[3] = new SqlParameter("@TaskSubject", SqlDbType.VarChar);
                _ISPParamter[3].Value = e.TaskSubject;
                _ISPParamter[4] = new SqlParameter("@TaskDescription", SqlDbType.VarChar);
                _ISPParamter[4].Value = e.TaskDescription;
                _ISPParamter[5] = new SqlParameter("@Priority", SqlDbType.Char);
                _ISPParamter[5].Value = e.Priority;
                _ISPParamter[6] = new SqlParameter("@Status", SqlDbType.Char);
                _ISPParamter[6].Value = e.status;
                _ISPParamter[7] = new SqlParameter("@DateofCompletion", SqlDbType.DateTime);
                _ISPParamter[7].Value = e.Date;

                for (int i = 0; i < _ISPParamter.Length; i++)
                {
                    _Cmd.Parameters.Add(_ISPParamter[i]);
                }
                _Connection.Open();
                _Cmd.ExecuteNonQuery();
                _Connection.Close();
                return "0";
            }
            catch (SqlException Ex)
            {
                return "1";
            }
        }
       public Collection<Employee> fn_compliant_box(Employee e)
        {
            Collection<Employee> EmployeeList = new Collection<Employee>();
            _Connection = Con.fn_Connection();
            string _SqlString = " select a.*,c.Employee_First_Name, c.pn_employeeID from compliant_box a,Paym_employee c where a.Pn_EmployeeId=c.Pn_EmployeeID  and a.pn_branchID='" + e.BranchId + "' and a.Pn_CompanyID='" + e.CompanyId + "' and a.Pn_EmployeeId='" + e.EmployeeCode + "'";
            SqlCommand _SSDesignation = new SqlCommand(_SqlString, _Connection);
            _Connection.Open();
            SqlDataReader dr_Compliant = _SSDesignation.ExecuteReader();
            while (dr_Compliant.Read())
            {
                Employee employee = new Employee();
                employee.Id1 = (int)dr_Compliant["Id"];
                employee.EmployeeCode = (string)dr_Compliant["pn_employeeid"] + "-" + (string)dr_Compliant["Employee_First_Name"];
                employee.Compliant_Subject1 = Convert.IsDBNull(dr_Compliant["Compliant_subject"]) ? "" : (string)dr_Compliant["Compliant_subject"];
                employee.Compliant_Text1 = Convert.IsDBNull(dr_Compliant["Compliant_Text"]) ? "" : (string)dr_Compliant["Compliant_Text"];
                employee.Status21 = Convert.IsDBNull(dr_Compliant["status"]) ? "" : (string)dr_Compliant["status"];

                EmployeeList.Add(employee);
            }
            return EmployeeList;
        }
        public Collection<Employee> fn_compliant_box1(Employee e)
        {
            Collection<Employee> EmployeeList = new Collection<Employee>();
            _Connection = Con.fn_Connection();
            string _SqlString = " select a.*,c.Employee_First_Name, c.pn_employeeID from compliant_box a,Paym_employee c where a.Pn_EmployeeId=c.Pn_EmployeeID  and a.pn_branchID='" + e.BranchId + "' and a.Pn_CompanyID='" + e.CompanyId + "'";
            SqlCommand _SSDesignation = new SqlCommand(_SqlString, _Connection);
            _Connection.Open();
            SqlDataReader dr_Compliant = _SSDesignation.ExecuteReader();
            while (dr_Compliant.Read())
            {
                Employee employee = new Employee();
                employee.Id = (int)dr_Compliant["Id"];
                employee.EmployeeCode = (string)dr_Compliant["pn_employeeid"] + "-" + (string)dr_Compliant["Employee_First_Name"];
                employee.Compliant_Subject = Convert.IsDBNull(dr_Compliant["Compliant_subject"]) ? "" : (string)dr_Compliant["Compliant_subject"];
                employee.Compliant_Text = Convert.IsDBNull(dr_Compliant["Compliant_Text"]) ? "" : (string)dr_Compliant["Compliant_Text"];
                employee.Status2 = Convert.IsDBNull(dr_Compliant["status"]) ? "" : (string)dr_Compliant["status"];

                EmployeeList.Add(employee);
            }
            return EmployeeList;
        }








        //$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$

        public Collection<Employee> fn_getPlace(Employee e)
        {
            Collection<Employee> DepartmentList = new Collection<Employee>();
            _Connection = Con.fn_Connection();
            string _SqlString = "select * from paym_Branch where status='Y' and pn_CompanyID='" + e.CompanyId + "' and pn_BranchID = '" + e.BranchId + "'";
            SqlCommand _SSDepartment = new SqlCommand(_SqlString, _Connection);
            _Connection.Open();
            SqlDataReader dr_Department = _SSDepartment.ExecuteReader();
            while (dr_Department.Read())
            {
                Employee employee = new Employee();
                employee.DepartmentId = (int)dr_Department["Pn_BranchId"];
                employee.DepartmentName = (string)(dr_Department["Address_Line2"]) + "-" + (string)dr_Department["City"];
                employee.State = (string)dr_Department["State"];
                DepartmentList.Add(employee);
            }
            return DepartmentList;
        }

        public Collection<Employee> fn_getJobCodeList(int e, int e1)
        {
            Collection<Employee> JobCodeList = new Collection<Employee>();
            _Connection = Con.fn_Connection();
            string _SqlString = "select * from Recruit_jobrequistion where pn_BranchID='" + e + "' and pn_CompanyID='" + e1 + "' and Status='Approved'";
            SqlCommand _SSJobCode = new SqlCommand(_SqlString, _Connection);
            _Connection.Open();
            SqlDataReader dr_JobCode = _SSJobCode.ExecuteReader();
            while (dr_JobCode.Read())
            {
                Employee employee = new Employee();
                employee.JobCodeId = Convert.IsDBNull(dr_JobCode["Job_Code"]) ? "" : (string)dr_JobCode["Job_Code"];
                employee.JobCodeName = (string)dr_JobCode["Job_Code"] + "-" + (string)dr_JobCode["Job_Title"];

                JobCodeList.Add(employee);
            }
            return JobCodeList;
        }

        public Collection<Employee> fn_getJobCodeList1(int e, int e1)
        {
            Collection<Employee> JobCodeList = new Collection<Employee>();
            _Connection = Con.fn_Connection();
            string _SqlString = "select * from Recruit_jobrequistion where pn_BranchID='" + e + "' and pn_CompanyID='" + e1 + "'";
            SqlCommand _SSJobCode = new SqlCommand(_SqlString, _Connection);
            _Connection.Open();
            SqlDataReader dr_JobCode = _SSJobCode.ExecuteReader();
            while (dr_JobCode.Read())
            {
                Employee employee = new Employee();
                employee.JobCodeId = Convert.IsDBNull(dr_JobCode["Job_Code"]) ? "" : (string)dr_JobCode["Job_Code"];
                employee.JobCodeName = (string)dr_JobCode["Job_Code"] + "-" + (string)dr_JobCode["Job_Title"];

                JobCodeList.Add(employee);
            }
            return JobCodeList;
        }

        public string Department(Employee e)
        {
            try
            {
                _Connection = Con.fn_Connection();

                SqlCommand _Cmd = new SqlCommand("sp_Department", _Connection);
                _Cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter[] _ISPParamter = new SqlParameter[3];
                _ISPParamter[0] = new SqlParameter("@pn_CompanyID", SqlDbType.Int);
                _ISPParamter[0].Value = e.CompanyId;
                _ISPParamter[1] = new SqlParameter("@pn_BranchID", SqlDbType.Int);
                _ISPParamter[1].Value = e.BranchId;
                _ISPParamter[2] = new SqlParameter("@fn_DepartmentID", SqlDbType.Int);
                _ISPParamter[2].Value = e.DepartmentId;

                for (int i = 0; i < _ISPParamter.Length; i++)
                {
                    _Cmd.Parameters.Add(_ISPParamter[i]);
                }
                _Connection.Open();
                _Cmd.ExecuteNonQuery();
                _Connection.Close();
                return "0";
            }
            catch (SqlException Ex)
            {
                return "1";
            }
        }

        public string Section(Employee e)
        {
            try
            {
                _Connection = Con.fn_Connection();


                SqlCommand _Cmd = new SqlCommand("insert into hr_authentication values('"+e.CompanyId+"','"+e.BranchId+"','"+e.SectionId+"','"+e.SectionView+"','"+e.SectionEdit+"','"+e.SectionDelete+"')", _Connection);
                
                _Connection.Open();
                _Cmd.ExecuteNonQuery();
                _Connection.Close();
                return "0";
            }
            catch (SqlException Ex)
            {
                return "1";
            }
        }


        public string projectsite(Employee e)
        {
            try
            {
                _Connection = Con.fn_Connection();

                SqlCommand _Cmd = new SqlCommand("sp_projectsite", _Connection);
                _Cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter[] _ISPParamter = new SqlParameter[3];
                _ISPParamter[0] = new SqlParameter("@pn_CompanyID", SqlDbType.Int);
                _ISPParamter[0].Value = e.CompanyId;
                _ISPParamter[1] = new SqlParameter("@pn_BranchID", SqlDbType.Int);
                _ISPParamter[1].Value = e.BranchId;
                _ISPParamter[2] = new SqlParameter("@fn_projectsiteID", SqlDbType.Int);
                _ISPParamter[2].Value = e.ProjectsiteId;

                for (int i = 0; i < _ISPParamter.Length; i++)
                {
                    _Cmd.Parameters.Add(_ISPParamter[i]);
                }
                _Connection.Open();
                _Cmd.ExecuteNonQuery();
                _Connection.Close();
                return "0";
            }
            catch (SqlException Ex)
            {
                return "1";
            }
        }

        public string Division(Employee e)
        {
            try
            {
                _Connection = Con.fn_Connection();


                SqlCommand _Cmd = new SqlCommand("sp_Division", _Connection);
                _Cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter[] _ISPParamter = new SqlParameter[3];
                _ISPParamter[0] = new SqlParameter("@pn_CompanyID", SqlDbType.Int);
                _ISPParamter[0].Value = e.CompanyId;
                _ISPParamter[1] = new SqlParameter("@pn_BranchID", SqlDbType.Int);
                _ISPParamter[1].Value = e.BranchId;
                _ISPParamter[2] = new SqlParameter("@fn_DivisionID", SqlDbType.Int);
                _ISPParamter[2].Value = e.DivisionId;

                for (int i = 0; i < _ISPParamter.Length; i++)
                {
                    _Cmd.Parameters.Add(_ISPParamter[i]);
                }
                _Connection.Open();
                _Cmd.ExecuteNonQuery();
                _Connection.Close();
                return "0";
            }
            catch (SqlException Ex)
            {
                return "1";
            }
        }

        public string Level(Employee e)
        {
            try
            {
                _Connection = Con.fn_Connection();


                SqlCommand _Cmd = new SqlCommand("sp_Level", _Connection);
                _Cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter[] _ISPParamter = new SqlParameter[3];
                _ISPParamter[0] = new SqlParameter("@pn_CompanyID", SqlDbType.Int);
                _ISPParamter[0].Value = e.CompanyId;
                _ISPParamter[1] = new SqlParameter("@pn_BranchID", SqlDbType.Int);
                _ISPParamter[1].Value = e.BranchId;
                _ISPParamter[2] = new SqlParameter("@fn_LevelID", SqlDbType.Int);
                _ISPParamter[2].Value = e.LevelId;

                for (int i = 0; i < _ISPParamter.Length; i++)
                {
                    _Cmd.Parameters.Add(_ISPParamter[i]);
                }
                _Connection.Open();
                _Cmd.ExecuteNonQuery();
                _Connection.Close();
                return "0";
            }
            catch (SqlException Ex)
            {
                return "1";
            }
        }

        public string Designation(Employee e)
        {
            try
            {
                _Connection = Con.fn_Connection();


                SqlCommand _Cmd = new SqlCommand("sp_Designation", _Connection);
                _Cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter[] _ISPParamter = new SqlParameter[3];
                _ISPParamter[0] = new SqlParameter("@pn_CompanyID", SqlDbType.Int);
                _ISPParamter[0].Value = e.CompanyId;
                _ISPParamter[1] = new SqlParameter("@pn_BranchID", SqlDbType.Int);
                _ISPParamter[1].Value = e.BranchId;
                _ISPParamter[2] = new SqlParameter("@fn_DesignationID", SqlDbType.Int);
                _ISPParamter[2].Value = e.DesignationId;

                for (int i = 0; i < _ISPParamter.Length; i++)
                {
                    _Cmd.Parameters.Add(_ISPParamter[i]);
                }
                _Connection.Open();
                _Cmd.ExecuteNonQuery();
                _Connection.Close();
                return "0";
            }
            catch (SqlException Ex)
            {
                return "1";
            }
        }

        public string Grade(Employee e)
        {
            try
            {
                _Connection = Con.fn_Connection();


                SqlCommand _Cmd = new SqlCommand("sp_Grade", _Connection);
                _Cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter[] _ISPParamter = new SqlParameter[3];
                _ISPParamter[0] = new SqlParameter("@pn_CompanyID", SqlDbType.Int);
                _ISPParamter[0].Value = e.CompanyId;
                _ISPParamter[1] = new SqlParameter("@pn_BranchID", SqlDbType.Int);
                _ISPParamter[1].Value = e.BranchId;
                _ISPParamter[2] = new SqlParameter("@fn_GradeID", SqlDbType.Int);
                _ISPParamter[2].Value = e.GradeId;

                for (int i = 0; i < _ISPParamter.Length; i++)
                {
                    _Cmd.Parameters.Add(_ISPParamter[i]);
                }
                _Connection.Open();
                _Cmd.ExecuteNonQuery();
                _Connection.Close();
                return "0";
            }
            catch (SqlException Ex)
            {
                return "1";
            }
        }

        public string Category(Employee e)
        {
            try
            {
                _Connection = Con.fn_Connection();

                SqlCommand _Cmd = new SqlCommand("sp_Category", _Connection);
                _Cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter[] _ISPParamter = new SqlParameter[3];
                _ISPParamter[0] = new SqlParameter("@pn_CompanyID", SqlDbType.Int);
                _ISPParamter[0].Value = e.CompanyId;
                _ISPParamter[1] = new SqlParameter("@pn_BranchID", SqlDbType.Int);
                _ISPParamter[1].Value = e.BranchId;
                _ISPParamter[2] = new SqlParameter("@fn_CategoryID", SqlDbType.Int);
                _ISPParamter[2].Value = e.CategoryId;

                for (int i = 0; i < _ISPParamter.Length; i++)
                {
                    _Cmd.Parameters.Add(_ISPParamter[i]);
                }
                _Connection.Open();
                _Cmd.ExecuteNonQuery();
                _Connection.Close();
                return "0";
            }
            catch (SqlException Ex)
            {
                return "1";
            }
        }

        public string JobStatus(Employee e)
        {
            try
            {
                _Connection = Con.fn_Connection();


                SqlCommand _Cmd = new SqlCommand("sp_JobStatus", _Connection);
                _Cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter[] _ISPParamter = new SqlParameter[3];
                _ISPParamter[0] = new SqlParameter("@pn_CompanyID", SqlDbType.Int);
                _ISPParamter[0].Value = e.CompanyId;
                _ISPParamter[1] = new SqlParameter("@pn_BranchID", SqlDbType.Int);
                _ISPParamter[1].Value = e.BranchId;
                _ISPParamter[2] = new SqlParameter("@fn_JobStatusID", SqlDbType.Int);
                _ISPParamter[2].Value = e.JobStatusId;

                for (int i = 0; i < _ISPParamter.Length; i++)
                {
                    _Cmd.Parameters.Add(_ISPParamter[i]);
                }
                _Connection.Open();
                _Cmd.ExecuteNonQuery();
                _Connection.Close();
                return "0";
            }
            catch (SqlException Ex)
            {
                return "1";
            }
        }

        public string Shift(Employee e)
        {
            try
            {
                _Connection = Con.fn_Connection();
                SqlCommand _Cmd = new SqlCommand("sp_Shift", _Connection);
                _Cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter[] _ISPParamter = new SqlParameter[3];
                _ISPParamter[0] = new SqlParameter("@pn_CompanyID", SqlDbType.Int);
                _ISPParamter[0].Value = e.CompanyId;
                _ISPParamter[1] = new SqlParameter("@pn_BranchID", SqlDbType.Int);
                _ISPParamter[1].Value = e.BranchId;
                _ISPParamter[2] = new SqlParameter("@fn_ShiftID", SqlDbType.Int);
                _ISPParamter[2].Value = e.ShiftId;

                for (int i = 0; i < _ISPParamter.Length; i++)
                {
                    _Cmd.Parameters.Add(_ISPParamter[i]);
                }
                _Connection.Open();
                _Cmd.ExecuteNonQuery();
                _Connection.Close();
                return "0";
            }
            catch (SqlException Ex)
            {
                return "1";
            }
        }





        //######################################################################################################




        //####################################################################################


        public Collection<Employee> fn_getCourseList1(int clid)
        {
            Collection<Employee> CourseList = new Collection<Employee>();
            _Connection = Con.fn_Connection();
            SqlCommand _Course = new SqlCommand("select * from hrmm_Course where status='Y' and BranchID='"+clid+"'", _Connection);
            _Connection.Open();
            SqlDataReader dr = _Course.ExecuteReader();
            while (dr.Read())
            {
                Employee employee = new Employee();
                employee.PGCourseID = (int)dr["pn_CourseID"];
                employee.PGCourseName = Convert.IsDBNull(dr["v_CourseName"]) ? "" : (string)dr["v_CourseName"];
                CourseList.Add(employee);
            }
            return CourseList;
        }
        

        public Collection<Employee> fn_getSkillsList1(int sid)
        {
            Collection<Employee> SkillsList = new Collection<Employee>();
            _Connection = Con.fn_Connection();
            SqlCommand _Course = new SqlCommand("select * from hrmm_SkillsMaster where status='Y' and BranchID='" + sid + "'", _Connection);
            _Connection.Open();
            SqlDataReader dr = _Course.ExecuteReader();
            while (dr.Read())
            {
                Employee employee = new Employee();
                employee.SkillId = (int)dr["pn_SkillID"];
                employee.SkillName = Convert.IsDBNull(dr["v_SkillName"]) ? "" : (string)dr["v_SkillName"];
                SkillsList.Add(employee);
            }
            return SkillsList;
        }
        public Collection<Employee> fn_getSkillDetails(Employee e)
        {
            Collection<Employee> SkillsList = new Collection<Employee>();
            _Connection = Con.fn_Connection();
            SqlCommand _Skill = new SqlCommand("select * from hrmm_SkillsMaster where status='Y'", _Connection);
            _Connection.Open();
            SqlDataReader dr = _Skill.ExecuteReader();
            while (dr.Read())
            {
                Employee employee = new Employee();
                employee.SkillId = (int)dr["pn_SkillID"];
                employee.SkillName = Convert.IsDBNull(dr["v_SkillName"]) ? "" : (string)dr["v_SkillName"];
                SkillsList.Add(employee);
            }
            return SkillsList;
        }


        public Collection<Employee> fn_getSkillsList()
        {
            Collection<Employee> CourseList = new Collection<Employee>();
            _Connection = Con.fn_Connection();
            SqlCommand _Course = new SqlCommand("select * from hrmm_SkillsMaster where status='Y'", _Connection);
            _Connection.Open();
            SqlDataReader dr = _Course.ExecuteReader();
            while (dr.Read())
            {
                Employee employee = new Employee();
                employee.SkillId = (int)dr["pn_SkillID"];
                employee.SkillName = Convert.IsDBNull(dr["v_SkillName"]) ? "" : (string)dr["v_SkillName"];
                CourseList.Add(employee);
            }
            return CourseList;
        }

        public Collection<Employee> fn_getSpecializationList()
        {
            Collection<Employee> SpecializationList = new Collection<Employee>();
            _Connection = Con.fn_Connection();
            SqlCommand _Course = new SqlCommand("select * from hrmm_Specialization where status='Y'", _Connection);
            _Connection.Open();
            SqlDataReader dr = _Course.ExecuteReader();
            while (dr.Read())
            {
                Employee employee = new Employee();
                employee.PGSpecializationId = (int)dr["pn_SpecializationId"];
                employee.PGSpecialaization = Convert.IsDBNull(dr["v_SpecializationName"]) ? "" : (string)dr["v_SpecializationName"];
                SpecializationList.Add(employee);
            }
            return SpecializationList;
        }

        //*******************************************************************************

        public Collection<Employee> fn_getEmployeeList(Employee e)
        {
            Collection<Employee> EmployeeList = new Collection<Employee>();
            _Connection = Con.fn_Connection();//order by Employee_First_Name asc
            SqlCommand _Course = new SqlCommand("select a.* from paym_Employee a where a.pn_CompanyID=" + e.CompanyId + " and a.pn_BranchID =" + e.BranchId + " and a.status!='N' order by a.employeecode asc", _Connection);
            _Connection.Open();
            SqlDataReader dr = _Course.ExecuteReader();
            while (dr.Read())
            {
                Employee employee = new Employee();
                employee.EmployeeId =(int)dr["pn_EmployeeID"];
                employee.EmployeeCode = Convert.IsDBNull(dr["EmployeeCode"]) ? "" : (string)dr["EmployeeCode"];
                employee.FirstName = Convert.IsDBNull(dr["Employee_First_Name"]) ? "" : (string)dr["Employee_First_Name"];
                employee.LastName = (string)dr["EmployeeCode"] + "-" + (string)dr["Employee_First_Name"];
                employee.FullName = (int)dr["pn_EmployeeID"] + "-" + (string)dr["Employee_First_Name"];
                EmployeeList.Add(employee);
            }
            return EmployeeList;
        }

        public Collection<Employee> fn_getoldEmployeeList(Employee e)
        {
            Collection<Employee> EmployeeList = new Collection<Employee>();
            _Connection = Con.fn_Connection();//order by Employee_First_Name asc
            SqlCommand _Course = new SqlCommand("select * from paym_Employee where pn_CompanyID=" + e.CompanyId + " and pn_BranchID =" + e.BranchId + " and status='N' order by Employee_first_name", _Connection);
            _Connection.Open();
            SqlDataReader dr = _Course.ExecuteReader();
            while (dr.Read())
            {
                Employee employee = new Employee();
                employee.EmployeeId = (int)dr["pn_EmployeeID"];
                employee.EmployeeCode = Convert.IsDBNull(dr["EmployeeCode"]) ? "" : (string)dr["EmployeeCode"];
                employee.FirstName = Convert.IsDBNull(dr["Employee_First_Name"]) ? "" : (string)dr["Employee_First_Name"];
                employee.LastName = (string)dr["EmployeeCode"] + "-" + (string)dr["Employee_First_Name"];
                employee.FullName = (int)dr["pn_EmployeeID"] + "-" + (string)dr["Employee_First_Name"];
                EmployeeList.Add(employee);
            }
            return EmployeeList;
        }

        public Collection<Employee> fn_getEmployeeDepartment(Employee e)
        {
            Collection<Employee> EmployeeList = new Collection<Employee>();
            _Connection = Con.fn_Connection();  //order by Employee_First_Name asc
            SqlCommand _Course = new SqlCommand("select a.pn_employeeid,b.EmployeeCode,b.employee_First_Name from paym_employee_profile1 a,paym_employee b where a.pn_branchid='" + e.BranchId + "' and a.pn_departmentid='" + e.DepartmentId + "' and a.pn_employeeid=b.pn_employeeid and a.pn_companyid='" + e.CompanyId + "' and b.status != 'N' order by a.pn_gradeID asc ", _Connection);
            _Connection.Open();
            SqlDataReader dr = _Course.ExecuteReader();
            while (dr.Read())
            {
                Employee employee = new Employee();
                employee.EmployeeId = (int)dr["pn_EmployeeID"];
                employee.EmployeeCode = Convert.IsDBNull(dr["EmployeeCode"]) ? "" : (string)dr["EmployeeCode"];
                employee.FirstName = Convert.IsDBNull(dr["Employee_First_Name"]) ? "" : (string)dr["Employee_First_Name"];
                employee.LastName = (string)dr["EmployeeCode"] + " - " + (string)dr["Employee_First_Name"];
                employee.FullName = (int)dr["pn_EmployeeID"] + "-" + (string)dr["Employee_First_Name"];
                EmployeeList.Add(employee);
            }
            return EmployeeList;
        }
        public Collection<Employee> fn_getEmployeeDepartment1(Employee e)
        {
            Collection<Employee> EmployeeList = new Collection<Employee>();
            _Connection = Con.fn_Connection();  //order by Employee_First_Name asc
            SqlCommand _Course = new SqlCommand("select a.pn_employeeid,b.EmployeeCode,b.employee_First_Name from paym_employee_profile1 a,paym_employee b where a.pn_branchid='" + e.BranchId + "' and  a.pn_employeeid=b.pn_employeeid and a.pn_companyid='" + e.CompanyId + "' and b.status != 'N' order by b.EmployeeCode asc ", _Connection);
            _Connection.Open();
            SqlDataReader dr = _Course.ExecuteReader();
            while (dr.Read())
            {
                Employee employee = new Employee();
                employee.EmployeeId = (int)dr["pn_EmployeeID"];
                employee.EmployeeCode = Convert.IsDBNull(dr["EmployeeCode"]) ? "" : (string)dr["EmployeeCode"];
                employee.FirstName = Convert.IsDBNull(dr["Employee_First_Name"]) ? "" : (string)dr["Employee_First_Name"];
                employee.LastName = (string)dr["EmployeeCode"] + " - " + (string)dr["Employee_First_Name"];
                employee.FullName = (int)dr["pn_EmployeeID"] + "-" + (string)dr["Employee_First_Name"];
                EmployeeList.Add(employee);
            }
            return EmployeeList;
        }

        public Collection<Employee> fn_getEmployeeCategory(Employee e)
        {
            Collection<Employee> EmployeeList = new Collection<Employee>();
            string qry = "";
            if (e.DepartmentId == 0 && e.CategoryId != 0)
            {
                qry = "select a.pn_employeeid,b.EmployeeCode,b.employee_First_Name from paym_employee_profile1 a,paym_employee b where a.pn_branchid='" + e.BranchId + "' and a.pn_categoryid='" + e.CategoryId + "' and a.pn_employeeid=b.pn_employeeid and a.pn_companyid='" + e.CompanyId + "' and b.status != 'N' order by a.pn_gradeID asc ";
            }
            else if(e.DepartmentId !=0 && e.CategoryId == 0)
            {
                qry = "select a.pn_employeeid,b.EmployeeCode,b.employee_First_Name from paym_employee_profile1 a,paym_employee b where a.pn_branchid='" + e.BranchId + "' and a.pn_departmentid = '" + e.DepartmentId + "' and a.pn_employeeid=b.pn_employeeid and a.pn_companyid='" + e.CompanyId + "' and b.status != 'N' order by a.pn_gradeID asc ";
            }
            else if (e.DepartmentId == 0 && e.CategoryId == 0)
            {
                qry = "select a.pn_employeeid,b.EmployeeCode,b.employee_First_Name from paym_employee_profile1 a,paym_employee b where a.pn_branchid='" + e.BranchId + "' and a.pn_employeeid=b.pn_employeeid and a.pn_companyid='" + e.CompanyId + "' and b.status != 'N' order by a.pn_gradeID asc ";
            }
            else
            {
                qry = "select a.pn_employeeid,b.EmployeeCode,b.employee_First_Name from paym_employee_profile1 a,paym_employee b where a.pn_branchid='" + e.BranchId + "' and a.pn_categoryid='" + e.CategoryId + "' and a.pn_departmentid = '" + e.DepartmentId + "' and a.pn_employeeid=b.pn_employeeid and a.pn_companyid='" + e.CompanyId + "' and b.status != 'N' order by a.pn_gradeID asc ";
            }
            _Connection = Con.fn_Connection();//order by Employee_First_Name asc
            SqlCommand _Course = new SqlCommand(qry, _Connection);
            _Connection.Open();
            SqlDataReader dr = _Course.ExecuteReader();
            while (dr.Read())
            {
                Employee employee = new Employee();
                employee.EmployeeId = (int)dr["pn_EmployeeID"];
                employee.EmployeeCode = Convert.IsDBNull(dr["EmployeeCode"]) ? "" : (string)dr["EmployeeCode"];
                employee.FirstName = Convert.IsDBNull(dr["Employee_First_Name"]) ? "" : (string)dr["Employee_First_Name"];
                employee.LastName = (string)dr["EmployeeCode"] + " - " + (string)dr["Employee_First_Name"];
                employee.FullName = (int)dr["pn_EmployeeID"] + "-" + (string)dr["Employee_First_Name"];
                EmployeeList.Add(employee);
            }
            return EmployeeList;
        }
        
        public Collection<Employee> fn_getEmployeeList1(Employee e)
        {
            Collection<Employee> EmployeeList = new Collection<Employee>();
            _Connection = Con.fn_Connection(); //order by Employee_First_Name asc
            SqlCommand _Course = new SqlCommand("select * from paym_Employee where pn_CompanyID=" + e.CompanyId + " and pn_BranchID =" + e.BranchId + " and pn_employeeID = '" + e.EmployeeId + "' and status!='N' order by Employee_first_name", _Connection);
            _Connection.Open();
            SqlDataReader dr = _Course.ExecuteReader();
            while (dr.Read())
            {
                Employee employee = new Employee();
                employee.EmployeeId = (int)dr["pn_EmployeeID"];
                employee.EmployeeCode = Convert.IsDBNull(dr["EmployeeCode"]) ? "" : (string)dr["EmployeeCode"];
                employee.FirstName = Convert.IsDBNull(dr["Employee_First_Name"]) ? "" : (string)dr["Employee_First_Name"];
                employee.LastName = (string)dr["EmployeeCode"] + " - " + (string)dr["Employee_First_Name"];
                employee.basic_salary = (double)dr["basic_salary"];
                employee.Gender = (string)dr["Gender"];
                EmployeeList.Add(employee);
            }
            return EmployeeList;
        }



        public Collection<Employee> fn_getOldEmployeeList(Employee e)
        {
            Collection<Employee> EmployeeList = new Collection<Employee>();
            _Connection = Con.fn_Connection();//order by Employee_First_Name asc
            SqlCommand _Course = new SqlCommand("select * from paym_Employee where pn_CompanyID=" + e.CompanyId + " and pn_BranchID =" + e.BranchId + " and status='N' order by EmployeeCode", _Connection);
            _Connection.Open();
            SqlDataReader dr = _Course.ExecuteReader();
            while (dr.Read())
            {
                Employee employee = new Employee();
                employee.EmployeeId = (int)dr["pn_EmployeeID"];
                employee.EmployeeCode = Convert.IsDBNull(dr["EmployeeCode"]) ? "" : (string)dr["EmployeeCode"];
                employee.FirstName = Convert.IsDBNull(dr["Employee_First_Name"]) ? "" : (string)dr["Employee_First_Name"];
                employee.LastName = (string)dr["EmployeeCode"] + " - " + (string)dr["Employee_First_Name"];
                EmployeeList.Add(employee);
            }
            return EmployeeList;
        }

        public Collection<Employee> fn_getPrevEmplist(string str_query)
        {
            Collection<Employee> DeductionList = new Collection<Employee>();
            _Connection = Con.fn_Connection();
            SqlCommand _SSDeduction = new SqlCommand(str_query, _Connection);
            _Connection.Open();
            SqlDataReader dr_Deduction = _SSDeduction.ExecuteReader();
            while (dr_Deduction.Read())
            {
                Employee employee = new Employee();
                employee.EmployeeId = (int)dr_Deduction["pn_EmployeeID"];
                employee.LastName = (string)dr_Deduction["EmployeeCode"] + "-" + (string)dr_Deduction["Employee_First_Name"];
                employee.FirstName = (string)dr_Deduction["Employee_First_Name"];
                employee.d_retirement = Convert.ToDateTime(dr_Deduction["RetirementDate"]);
                DeductionList.Add(employee);
            }
            return DeductionList;
        }
        public Collection<Employee> fn_getraviList(Employee e)
        {
            Collection<Employee> EmployeeList = new Collection<Employee>();
            _Connection = Con.fn_Connection();
            SqlCommand _Course = new SqlCommand("select EmployeeCode+space(8-len(EmployeeCode))+' - '+Employee_First_Name as SelEmployee,* from paym_Employee where pn_CompanyID=" + e.CompanyId + " and pn_BranchID =" + e.BranchId + " and status='Y' and left(employeecode,1)='s' order by Employee_First_Name asc", _Connection);
            _Connection.Open();
            SqlDataReader dr = _Course.ExecuteReader();
            while (dr.Read())
            {
                Employee employee = new Employee();
                employee.EmployeeId = (int)dr["pn_EmployeeID"];
                employee.EmployeeCode = Convert.IsDBNull(dr["EmployeeCode"]) ? "" : (string)dr["EmployeeCode"];
                employee.FirstName = Convert.IsDBNull(dr["Employee_First_Name"]) ? "" : (string)dr["Employee_First_Name"];
                employee.LastName = (string)dr["SelEmployee"];
                EmployeeList.Add(employee);
            }
            return EmployeeList;
        }

        public Collection<Employee> fn_getEmployees(Employee e)
        {
            string str_query;

            Collection<Employee> EmployeeList = new Collection<Employee>();

            str_query = "select e.pn_CompanyID,e.pn_BranchID,e.pn_EmployeeID,e.EmployeeCode,e.Employee_First_Name,e.Employee_Middle_Name,e.Employee_Last_Name,";
            str_query = str_query + "e.DateofBirth,ep.pn_DivisionId,ep.pn_DepartmentId,ep.pn_DesingnationId,ep.pn_GradeId,";
            str_query = str_query + "ep.pn_ShiftId,ep.pn_CategoryId,ep.pn_JobStatusID,ew.JoiningDate,ew.OfferDate,ew.ProbationUpto,ew.ExtendedUpto,ew.ConfirmationDate,ew.RetirementDate,";
            str_query = str_query + "ew.ContractRenviewDate,eg.EmailId,eg.AlternateEmailId,eg.BloodGroup,eg.Religion,eg.Nationality,eg.PresentHouseNo,";
            str_query = str_query + "eg.PresentStreetName,eg.PresentAddLine1,eg.PresentAddLine2,eg.PresentCity,eg.PresentState,eg.PermanentHouseNo,";
            str_query = str_query + "eg.PermanentStreetName,eg.PermanentAddLine1,eg.PermanentAddLine2,eg.PermanentCity,eg.PermanentState,eg.ph_Office,eg.ph_Residence,eg.CellNo,eg.Fax,eg.emgName,eg.emgPhone,eph.v_ImagePath";

            str_query = str_query + " from paym_Employee e,paym_Employee_profile1 ep,paym_Employee_WorkDetails ew,paym_Employee_General eg,paym_Employee_Photo eph";

            str_query = str_query + " where ";

            str_query = str_query + "e.pn_CompanyID=" + e.CompanyId + " and e.pn_BranchID =" + e.BranchId + " and e.pn_EmployeeID=" + e.EmployeeId + " and e.status='Y' and ";

            str_query = str_query + "ep.pn_CompanyID=" + e.CompanyId + " and ep.pn_BranchID =" + e.BranchId + " and ep.pn_EmployeeID=" + e.EmployeeId + " and ";

            str_query = str_query + "ew.pn_CompanyID=" + e.CompanyId + " and ew.pn_BranchID =" + e.BranchId + " and ew.pn_EmployeeID=" + e.EmployeeId + " and ";

            str_query = str_query + "eg.pn_CompanyID=" + e.CompanyId + " and eg.pn_BranchID =" + e.BranchId + " and eg.pn_EmployeeID=" + e.EmployeeId + " and ";

            str_query = str_query + "eph.pn_CompanyID=" + e.CompanyId + " and eph.pn_BranchID =" + e.BranchId + " and eph.pn_EmployeeID=" + e.EmployeeId + " and ";

            _Connection = Con.fn_Connection();
            SqlCommand _Course = new SqlCommand(str_query, _Connection);
            _Connection.Open();

            SqlDataReader dr_BranchCompany = _Course.ExecuteReader();

            while (dr_BranchCompany.Read())
            {
                Employee employee = new Employee();
                
                employee.EmployeeCode = Convert.IsDBNull(dr_BranchCompany["EmployeeCode"]) ? "" : (string)dr_BranchCompany["EmployeeCode"];
                employee.FirstName = Convert.IsDBNull(dr_BranchCompany["Employee_First_Name"]) ? "" : (string)dr_BranchCompany["Employee_First_Name"];
                employee.MiddleName = Convert.IsDBNull(dr_BranchCompany["Employee_Middle_Name"]) ? "" : (string)dr_BranchCompany["Employee_Middle_Name"];
                employee.LastName = Convert.IsDBNull(dr_BranchCompany["Employee_Last_Name"]) ? "" : (string)dr_BranchCompany["Employee_Last_Name"];
                employee.d_birth = Convert.ToDateTime(dr_BranchCompany["DateofBirth"]);
                //employee.d_join = Convert.IsDBNull(dr_BranchCompany["DateofJoining"]) ? "" : (string)dr_BranchCompany["DateofJoining"];
                employee._BranchId = (int)dr_BranchCompany["BranchID"];
                employee.DivisionId = (int)dr_BranchCompany["pn_DivisionId"];
                employee.DepartmentId = (int)dr_BranchCompany["pn_DepartmentId"];
                employee.DesignationId = (int)dr_BranchCompany["pn_DesingnationId"];
                employee.GradeId = (int)dr_BranchCompany["pn_GradeId"];
                employee.ShiftId = (int)dr_BranchCompany["pn_ShiftId"];
                employee.CategoryId = (int)dr_BranchCompany["pn_CategoryId"];

                employee.JobStatusId = (int)dr_BranchCompany["pn_JobStatusID"];
                employee.d_join = Convert.ToDateTime(dr_BranchCompany["JoiningDate"]);
                employee.d_Offer = Convert.ToDateTime(dr_BranchCompany["OfferDate"]);

                employee.d_probotion = Convert.ToDateTime(dr_BranchCompany["ProbationUpto"]);
                employee.d_extended = Convert.ToDateTime(dr_BranchCompany["ExtendedUpto"]);
                employee.d_confirmation = Convert.ToDateTime(dr_BranchCompany["ConfirmationDate"]);
                employee.d_retirement = Convert.ToDateTime(dr_BranchCompany["RetirementDate"]);
                employee.d_renue = Convert.ToDateTime(dr_BranchCompany["ContractRenviewDate"]);

                employee.EmailId = Convert.IsDBNull(dr_BranchCompany["EmailId"]) ? "" : (string)dr_BranchCompany["EmailId"];
                employee.A_EmailId = Convert.IsDBNull(dr_BranchCompany["AlternateEmailId"]) ? "" : (string)dr_BranchCompany["AlternateEmailId"];
                employee.BloodGroup = Convert.IsDBNull(dr_BranchCompany["BloodGroup"]) ? "" : (string)dr_BranchCompany["BloodGroup"];
                employee.Religion = Convert.IsDBNull(dr_BranchCompany["Religion"]) ? "" : (string)dr_BranchCompany["Religion"];
                employee.Nationality = Convert.IsDBNull(dr_BranchCompany["Nationality"]) ? "" : (string)dr_BranchCompany["Nationality"];
                employee.HouseNo = Convert.IsDBNull(dr_BranchCompany["PresentHouseNo"]) ? "" : (string)dr_BranchCompany["PresentHouseNo"];
                employee.StreetName = Convert.IsDBNull(dr_BranchCompany["PresentStreetName"]) ? "" : (string)dr_BranchCompany["PresentStreetName"];
                employee.AddressLine1 = Convert.IsDBNull(dr_BranchCompany["PresentAddLine1"]) ? "" : (string)dr_BranchCompany["PresentAddLine1"];
                employee.AddressLine2 = Convert.IsDBNull(dr_BranchCompany["PresentAddLine2"]) ? "" : (string)dr_BranchCompany["PresentAddLine2"];
                employee.City = Convert.IsDBNull(dr_BranchCompany["PresentCity"]) ? "" : (string)dr_BranchCompany["PresentCity"];
                employee.State = Convert.IsDBNull(dr_BranchCompany["PresentState"]) ? "" : (string)dr_BranchCompany["PresentState"];
                employee.p_HouseNo = Convert.IsDBNull(dr_BranchCompany["PermanentHouseNo"]) ? "" : (string)dr_BranchCompany["PermanentHouseNo"];
                employee.p_StreetName = Convert.IsDBNull(dr_BranchCompany["PermanentStreetName"]) ? "" : (string)dr_BranchCompany["PermanentStreetName"];
                employee.P_AddressLine1 = Convert.IsDBNull(dr_BranchCompany["PermanentAddLine1"]) ? "" : (string)dr_BranchCompany["PermanentAddLine1"];
                employee.P_AddressLine2 = Convert.IsDBNull(dr_BranchCompany["PermanentAddLine2"]) ? "" : (string)dr_BranchCompany["PermanentAddLine2"];
                employee.P_City = Convert.IsDBNull(dr_BranchCompany["PermanentCity"]) ? "" : (string)dr_BranchCompany["PermanentCity"];
                employee.P_State = Convert.IsDBNull(dr_BranchCompany["PermanentState"]) ? "" : (string)dr_BranchCompany["PermanentState"];

                employee.ph_Office = Convert.IsDBNull(dr_BranchCompany["ph_Office"]) ? "" : (string)dr_BranchCompany["ph_Office"];
                employee.ph_Residence = Convert.IsDBNull(dr_BranchCompany["ph_Residence"]) ? "" : (string)dr_BranchCompany["ph_Residence"];
                employee.CellNo = Convert.IsDBNull(dr_BranchCompany["CellNo"]) ? "" : (string)dr_BranchCompany["CellNo"];
                employee.Fax = Convert.IsDBNull(dr_BranchCompany["Fax"]) ? "" : (string)dr_BranchCompany["Fax"];
                employee.emgname = Convert.IsDBNull(dr_BranchCompany["emgName"]) ? "" : (string)dr_BranchCompany["emgName"];
                employee.emgno = Convert.IsDBNull(dr_BranchCompany["emgPhone"]) ? "" : (string)dr_BranchCompany["emgPhone"];

                employee.img_path = Convert.IsDBNull(dr_BranchCompany["v_ImagePath"]) ? "" : (string)dr_BranchCompany["v_ImagePath"];
                EmployeeList.Add(employee);
            }
            return EmployeeList;
        }

        public Collection<Employee> fn_get_Emp_first(Employee e)
        {
            Collection<Employee> EmployeeList = new Collection<Employee>();
            _Connection = Con.fn_Connection();
            string substring = "select *,convert(varchar(15),DateofBirth,103) as dob from paym_Employee where pn_CompanyID=" + e.CompanyId + " and pn_BranchID =" + e.BranchId + " and pn_EmployeeID=" + e.EmployeeId + "";
            SqlCommand _Course = new SqlCommand(substring, _Connection);
            //SqlCommand _Course = new SqlCommand("select *,convert(varchar(15),DateofBirth,103) as dob from paym_Employee where pn_CompanyID=" + e.CompanyId + " and pn_BranchID =" + e.BranchId + " and pn_EmployeeID=" + e.EmployeeId + " and Readerid=" + e.ReaderId + "and status='Y'", _Connection);
            _Connection.Open();
            SqlDataReader dr = _Course.ExecuteReader();
            while (dr.Read())
            {
                Employee employee = new Employee();
                employee.EmployeeId = (int)dr["pn_EmployeeID"];
                employee.EmployeeCode = Convert.IsDBNull(dr["EmployeeCode"]) ? "" : (string)dr["EmployeeCode"];
                employee.password = Convert.IsDBNull(dr["Password"]) ? "" : (string)dr["Password"];
                employee.FirstName = Convert.IsDBNull(dr["Employee_First_Name"]) ? "" : (string)dr["Employee_First_Name"];
                employee.MiddleName = Convert.IsDBNull(dr["Employee_Middle_Name"]) ? "" : (string)dr["Employee_Middle_Name"];
                employee.LastName = Convert.IsDBNull(dr["Employee_Last_Name"]) ? "" : (string)dr["Employee_Last_Name"];
                employee.d_birth = Convert.ToDateTime(dr["DateofBirth"]);
                employee.temp_str = dr["dob"].ToString();
                employee.password = Convert.IsDBNull(dr["Password"]) ? "" : (string)dr["Password"];
                employee.Gender = Convert.IsDBNull(dr["Gender"]) ? "" : (string)dr["Gender"];
                employee.FullName = Convert.IsDBNull(dr["Employee_Full_Name"]) ? "" : (string)dr["Employee_Full_Name"];
                employee.ReaderId = (int)dr["Readerid"];
                employee.OT_Eligible = Convert.ToChar(dr["OT_Eligible"]);
                employee.PFno = Convert.IsDBNull(dr["Pfno"]) ? "" : (string)dr["Pfno"];
                employee.ESIno = Convert.IsDBNull(dr["Esino"]) ? "" : (string)dr["Esino"];
                employee.OT_calc = (double)dr["OT_calc"];
                employee.Reporting = Convert.IsDBNull(dr["Reporting_person"]) ? "" : (string)dr["Reporting_person"];
                employee.ReportID = Convert.IsDBNull(dr["ReportingID"]) ? 0 : Convert.ToInt32(dr["ReportingID"]);
                employee.ReportingEmail = Convert.IsDBNull(dr["Reporting_email"]) ? "" : (string)dr["Reporting_email"];
                employee.basic_salary = (double)dr["basic_salary"];
                employee.Salary_Type = Convert.IsDBNull(dr["salary_type"]) ? "" : (string)dr["salary_type"];
                employee.Pan_no = Convert.IsDBNull(dr["Pan_no"]) ? "" : (string)dr["Pan_no"];
                employee.TDS = Convert.ToChar(dr["TDS_Applicable"]);
                employee.Other_Info = Convert.IsDBNull(dr["Other_Info"]) ? "" : (string)dr["Other_Info"];
                employee.status = Convert.ToChar(dr["status"]);
                //employee.Role = Convert.ToInt32(dr["role"]);
                EmployeeList.Add(employee);
            }
            return EmployeeList;
        }
        public string Area_Update(Employee e)
        {
            try
            {
                _Connection = Con.fn_Connection();
                SqlCommand _Cmd = new SqlCommand("sp_paym_BusArea", _Connection);
                _Cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter[] _ISPParamter = new SqlParameter[5];

                _ISPParamter[0] = new SqlParameter("@pn_CompanyID", SqlDbType.VarChar);
                _ISPParamter[0].Value = e.CompanyId;
                _ISPParamter[1] = new SqlParameter("@pn_BranchID", SqlDbType.Int);
                _ISPParamter[1].Value = e.BranchId;
                _ISPParamter[2] = new SqlParameter("@Area_ID", SqlDbType.Int);
                _ISPParamter[2].Value = e.Area_id;
                _ISPParamter[3] = new SqlParameter("@Area_name", SqlDbType.VarChar);
                _ISPParamter[3].Value = e.Area_name;
                _ISPParamter[4] = new SqlParameter("@status", SqlDbType.VarChar);
                _ISPParamter[4].Value = "Y";
                for (int i = 0; i < _ISPParamter.Length; i++)
                {
                    _Cmd.Parameters.Add(_ISPParamter[i]);
                }
                _Connection.Open();
                _Cmd.ExecuteNonQuery();
                _Connection.Close();
                return "0";
            }
            catch (SqlException Ex)
            {
                return "1";
            }
        }

        public Collection<Employee> fn_boarding()
        {
            Collection<Employee> BoardingList = new Collection<Employee>();
            _Connection = Con.fn_Connection();
            string _SqlString = "select Area_id,Area_name from paym_Bus_area where status='Y'";
            SqlCommand _SSBoarding = new SqlCommand(_SqlString, _Connection);
            _Connection.Open();
            SqlDataReader dr_Boarding = _SSBoarding.ExecuteReader();
            while (dr_Boarding.Read())
            {
                Employee employee = new Employee();
                employee.Area_id = (int)dr_Boarding["Area_id"];
                employee.Area_name = Convert.IsDBNull(dr_Boarding["Area_name"]) ? "" : (string)dr_Boarding["Area_name"];
                BoardingList.Add(employee);
            }
            return BoardingList;
        }

        public string Vehicle_Details(Employee e)
        {
            try
            {
                _Connection = Con.fn_Connection();
                SqlCommand _Cmd = new SqlCommand("sp_vehicle_details", _Connection);
                _Cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter[] _ISPParamter = new SqlParameter[6];

                _ISPParamter[0] = new SqlParameter("@pn_CompanyID", SqlDbType.VarChar);
                _ISPParamter[0].Value = e.CompanyId;
                _ISPParamter[1] = new SqlParameter("@pn_BranchID", SqlDbType.Int);
                _ISPParamter[1].Value = e.BranchId;
                _ISPParamter[2] = new SqlParameter("@vehicle_number", SqlDbType.VarChar);
                _ISPParamter[2].Value = e.Veh_number;
                _ISPParamter[3] = new SqlParameter("@vehicle_id", SqlDbType.Int);
                _ISPParamter[3].Value = e.Veh_id;
                _ISPParamter[4] = new SqlParameter("@vehicle_type", SqlDbType.VarChar);
                _ISPParamter[4].Value = e.Veh_type;
                _ISPParamter[5] = new SqlParameter("@vehicle_capacity", SqlDbType.Int);
                _ISPParamter[5].Value = e.Veh_capacity;
                for (int i = 0; i < _ISPParamter.Length; i++)
                {
                    _Cmd.Parameters.Add(_ISPParamter[i]);
                }
                _Connection.Open();
                _Cmd.ExecuteNonQuery();
                _Connection.Close();
                return "0";
            }
            catch (SqlException Ex)
            {
                return "1";
            }
        }

        public Collection<Employee> fn_vehicles()
        {
            Collection<Employee> VehicleList = new Collection<Employee>();
            _Connection = Con.fn_Connection();
            string _SqlString = "select vehicle_id,vehicle_number,vehicle_type,vehicle_capacity from paym_vehicle_details";
            SqlCommand _SSBoarding = new SqlCommand(_SqlString, _Connection);
            _Connection.Open();
            SqlDataReader dr_vehicle = _SSBoarding.ExecuteReader();
            while (dr_vehicle.Read())
            {
                Employee employee = new Employee();
                employee.Veh_id = Convert.IsDBNull(dr_vehicle["vehicle_id"]) ? "" : (string)dr_vehicle["vehicle_id"];
                employee.Veh_number = Convert.IsDBNull(dr_vehicle["vehicle_number"]) ? "" : (string)dr_vehicle["vehicle_number"];
                employee.Veh_type = Convert.IsDBNull(dr_vehicle["vehicle_type"]) ? "" : (string)dr_vehicle["vehicle_type"];
                employee.Veh_capacity = (int)dr_vehicle["vehicle_capacity"];
                employee.Vehicle = employee.Veh_number + " -- " + employee.Veh_type;
                VehicleList.Add(employee);
            }
            return VehicleList;
        }

        public Collection<Employee> fn_Vehicles1(Employee e)
        {
            Collection<Employee> VehicleList1 = new Collection<Employee>();
            _Connection = Con.fn_Connection();
            _Connection.Open();
            SqlCommand _vehiclecount = new SqlCommand("select count(*) from paym_bus_Details where pn_companyID='" + e.CompanyId + "' and pn_branchID='" + e.BranchId + "'", _Connection);
            int count = Convert.ToInt32(_vehiclecount.ExecuteScalar());
            if (count == 0)
            {
                string _SqlString = "select Vehicle_id,Vehicle_number,vehicle_type from paym_vehicle_details where pn_companyID='" + e.CompanyId + "'";
                SqlCommand _SSvehicle = new SqlCommand(_SqlString, _Connection);

                SqlDataReader dr_Vehicles = _SSvehicle.ExecuteReader();
                while (dr_Vehicles.Read())
                {
                    Employee employee = new Employee();
                    employee.Veh_id = dr_Vehicles["Vehicle_id"].ToString();
                    employee.Vehicle = (string)(dr_Vehicles["Vehicle_number"]) + "-" + (string)dr_Vehicles["vehicle_type"];
                    //employee.Veh_type = (string)(dr_Vehicles["vehicle_type"]) + "-" + (string)dr_Vehicles["vehicle_type"];
                    VehicleList1.Add(employee);
                }
                return VehicleList1;
            }
            else
            {
                string _SqlString = "select a.Vehicle_id,a.Vehicle_number,a.vehicle_type from paym_vehicle_details a where a.pn_companyID='" + e.CompanyId + "' and a.vehicle_id  not in (Select vehicle_id from paym_bus_Details)";
                SqlCommand _SSvehicle = new SqlCommand(_SqlString, _Connection);
                SqlDataReader dr_Vehicles = _SSvehicle.ExecuteReader();
                while (dr_Vehicles.Read())
                {
                    Employee employee = new Employee();
                    employee.Vehicle = (string)(dr_Vehicles["Vehicle_number"]) + "-" + (string)dr_Vehicles["vehicle_type"];
                    employee.Veh_id = dr_Vehicles["Vehicle_id"].ToString(); ;
                    //  employee.Veh_type = (string)(dr_Vehicles["vehicle_type"]) + "-" + (string)dr_Vehicles["vehicle_type"];
                    VehicleList1.Add(employee);
                }
                return VehicleList1;
            }
        }

        public Collection<Employee> fn_get_destination(Employee e)
        {
            Collection<Employee> DestinationList = new Collection<Employee>();
            _Connection = Con.fn_Connection();
            string _SqlString = "select Area_id,Area_name from paym_Bus_area where pn_companyID='" + e.CompanyId + "' and pn_branchID='" + e.BranchId + "'";
            SqlCommand _SSDestination = new SqlCommand(_SqlString, _Connection);
            _Connection.Open();
            SqlDataReader dr_places = _SSDestination.ExecuteReader();
            while (dr_places.Read())
            {
                Employee employee = new Employee();
                employee.Area_id = (int)(dr_places["Area_id"]);
                employee.Area_name = (string)dr_places["Area_name"];
                //  employee.Veh_type = (string)(dr_Vehicles["vehicle_type"]) + "-" + (string)dr_Vehicles["vehicle_type"];
                DestinationList.Add(employee);
            }
            return DestinationList;
        }

        public Collection<Employee> fn_get_drivers(Employee e)
        {
            Collection<Employee> DriverList = new Collection<Employee>();
            _Connection = Con.fn_Connection();
            _Connection.Open();
            SqlCommand _vehiclecount = new SqlCommand("select count(*) from paym_bus_Details where pn_companyID='" + e.CompanyId + "' and pn_branchID='" + e.BranchId + "'", _Connection);
            int count = Convert.ToInt32(_vehiclecount.ExecuteScalar());
            if (count == 0)
            {
                string _SqlString = "select a.employee_first_name,a.pn_employeeid from paym_employee a,paym_Employee_profile b where a.pn_employeeid=b.pn_employeeid and (b.pn_desingnationid=48 or b.pn_desingnationid=49 or b.pn_desingnationid=50) and a.pn_companyID='" + e.CompanyId + "'";
                SqlCommand _SSvehicle = new SqlCommand(_SqlString, _Connection);
                SqlDataReader dr_driver = _SSvehicle.ExecuteReader();
                while (dr_driver.Read())
                {
                    Employee employee = new Employee();
                    employee.Driver_name = (string)(dr_driver["employee_first_name"]);
                    employee.EmployeeId = (int)(dr_driver["pn_employeeid"]);
                    //  employee.Veh_type = (string)(dr_Vehicles["vehicle_type"]) + "-" + (string)dr_Vehicles["vehicle_type"];
                    DriverList.Add(employee);
                }
                return DriverList;
            }
            else
            {
                string _SqlString = "select a.employee_first_name,a.pn_employeeid from paym_employee a,paym_Employee_profile b where (b.pn_desingnationid=48 or b.pn_desingnationid=49 or b.pn_desingnationid=50) and a.pn_employeeid=b.pn_employeeid and b.pn_employeeid not in (Select Driver_id from paym_bus_Details) and a.pn_companyID='" + e.CompanyId + "' ";
                SqlCommand _SSvehicle = new SqlCommand(_SqlString, _Connection);
                SqlDataReader dr_driver = _SSvehicle.ExecuteReader();
                while (dr_driver.Read())
                {
                    Employee employee = new Employee();
                    employee.Driver_name = (string)(dr_driver["employee_first_name"]);
                    employee.EmployeeId = (int)(dr_driver["pn_employeeid"]);
                    //  employee.Veh_type = (string)(dr_Vehicles["vehicle_type"]) + "-" + (string)dr_Vehicles["vehicle_type"];
                    DriverList.Add(employee);
                }
                return DriverList;
            }

        }

        public string Route_details(Employee e)
        {
            try
            {
                _Connection = Con.fn_Connection();
                SqlCommand _Cmd = new SqlCommand("sp_Bus_details", _Connection);
                _Cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter[] _ISPParamter = new SqlParameter[9];

                _ISPParamter[0] = new SqlParameter("@pn_CompanyID", SqlDbType.VarChar);
                _ISPParamter[0].Value = e.CompanyId;
                _ISPParamter[1] = new SqlParameter("@pn_BranchID", SqlDbType.Int);
                _ISPParamter[1].Value = e.BranchId;
                _ISPParamter[2] = new SqlParameter("@route_id", SqlDbType.Int);
                _ISPParamter[2].Value = e.Route_ID;
                _ISPParamter[3] = new SqlParameter("@Vehicle_id", SqlDbType.Int);
                _ISPParamter[3].Value = e.Veh_id;
                _ISPParamter[4] = new SqlParameter("@vehicle_number", SqlDbType.VarChar);
                _ISPParamter[4].Value = e.Vehicle;
                _ISPParamter[5] = new SqlParameter("@Destination", SqlDbType.VarChar);
                _ISPParamter[5].Value = e.Area_name;
                _ISPParamter[6] = new SqlParameter("@Driver_id", SqlDbType.Int);
                _ISPParamter[6].Value = e.EmployeeId;
                _ISPParamter[7] = new SqlParameter("@stop_timings", SqlDbType.VarChar);
                _ISPParamter[7].Value = e.Routes;
                _ISPParamter[8] = new SqlParameter("@current_capacity", SqlDbType.Int);
                _ISPParamter[8].Value = e.Veh_capacity;
                for (int i = 0; i < _ISPParamter.Length; i++)
                {
                    _Cmd.Parameters.Add(_ISPParamter[i]);
                }
                _Connection.Open();
                _Cmd.ExecuteNonQuery();
                _Connection.Close();
                return "0";
            }
            catch (SqlException Ex)
            {
                return "1";
            }
        }

        public Collection<Employee> fn_EmployeeTimeCard(Employee e)
        {
            Collection<Employee> EmployeeList = new Collection<Employee>();
            _Connection = Con.fn_Connection();
            //SqlCommand _Course = new SqlCommand("select * from time_card where pn_CompanyID=" + e.CompanyId + " and pn_BranchID =" + e.BranchId + " and dates = '" + e.d_Date + "' and pn_employeeid = '" + e.EmployeeId + "' order by emp_name asc", _Connection);
            SqlCommand _Course = new SqlCommand("select a.*, b.pn_gradeid from time_card a, paym_employee_profile1 b where a.pn_CompanyID=" + e.CompanyId + " and a.pn_BranchID =" + e.BranchId + " and a.dates between '" + e.d_Date + "' and'"+e.e_date+ "' and a.shift_code ='"+e.shiftcode+"' and a.pn_employeeid = '" + e.EmployeeId + "' and a.pn_Employeeid = b.pn_Employeeid order by b.pn_gradeid asc", _Connection);
            _Connection.Open();
            SqlDataReader dr = _Course.ExecuteReader();
            while (dr.Read())
            {
                Employee employee = new Employee();
                employee.FirstName = Convert.IsDBNull(dr["emp_name"]) ? "" : (string)dr["emp_name"];
                employee.EmployeeCode = Convert.IsDBNull(dr["emp_code"]) ? "" : (string)dr["emp_code"];
                employee.Intime = Convert.IsDBNull(dr["intime"]) ? DateTime.Now : Convert.ToDateTime(dr["intime"]);
                employee.Outtime = Convert.IsDBNull(dr["outtime"]) ? DateTime.Now : Convert.ToDateTime(dr["outtime"]);
                employee.Latein = Convert.IsDBNull(dr["Late_in"]) ? DateTime.Now : Convert.ToDateTime(dr["Late_in"]);
                employee.Lateout = Convert.IsDBNull(dr["Late_out"]) ? DateTime.Now : Convert.ToDateTime(dr["Late_out"]);
                employee.Earlyout = Convert.IsDBNull(dr["Early_out"]) ? DateTime.Now : Convert.ToDateTime(dr["Early_out"]);
                employee.whours = Convert.IsDBNull(dr["ot_hrs"]) ? DateTime.Now : Convert.ToDateTime(dr["ot_hrs"]);
                employee.shiftcode = Convert.IsDBNull(dr["shift_code"]) ?"": (string)dr["shift_code"];
                employee.dates = Convert.IsDBNull(dr["dates"]) ? DateTime.Now : Convert.ToDateTime(dr["dates"]);
                employee.Flag = Convert.IsDBNull(dr["status"]) ? "" : (string)dr["status"];
                employee.Status21 = Convert.IsDBNull(dr["leave_code"]) ? "" : (string)dr["leave_code"];
                employee.shiftcode = Convert.IsDBNull(dr["Shift_code"]) ? "" : (string)dr["Shift_code"];
                employee.GradeId = Convert.IsDBNull(dr["pn_gradeid"]) ? 0 : Convert.ToInt32(dr["pn_gradeid"]);
                employee.data = Convert.IsDBNull(dr["data"]) ? "" : (string)dr["data"];
                EmployeeList.Add(employee);
            }
            return EmployeeList;
        }
        public Collection<Employee> fn_shift(Employee e)
        {
            Collection<Employee> EmployeeList = new Collection<Employee>();
            _Connection = Con.fn_Connection();
            //SqlCommand _Course = new SqlCommand("select * from time_card where pn_CompanyID=" + e.CompanyId + " and pn_BranchID =" + e.BranchId + " and dates = '" + e.d_Date + "' and pn_employeeid = '" + e.EmployeeId + "' order by emp_name asc", _Connection);
            SqlCommand _Course = new SqlCommand("select a.*, b.pn_gradeid from time_card a, paym_employee_profile1 b where a.pn_CompanyID=" + e.CompanyId + " and a.pn_BranchID =" + e.BranchId + " and a.dates = '" + e.d_Date + "' and a.pn_employeeid = '" + e.EmployeeId + "' and a.pn_Employeeid = b.pn_Employeeid order by b.pn_gradeid asc", _Connection);
            _Connection.Open();
            SqlDataReader dr = _Course.ExecuteReader();
            while (dr.Read())
            {
                Employee employee = new Employee();
                employee.FirstName = Convert.IsDBNull(dr["emp_name"]) ? "" : (string)dr["emp_name"];
                employee.EmployeeCode = Convert.IsDBNull(dr["emp_code"]) ? "" : (string)dr["emp_code"];
                employee.Intime = Convert.IsDBNull(dr["intime"]) ? DateTime.Now : Convert.ToDateTime(dr["intime"]);
                employee.Outtime = Convert.IsDBNull(dr["outtime"]) ? DateTime.Now : Convert.ToDateTime(dr["outtime"]);
                employee.Latein = Convert.IsDBNull(dr["Late_in"]) ? DateTime.Now : Convert.ToDateTime(dr["Late_in"]);
                employee.Lateout = Convert.IsDBNull(dr["Late_out"]) ? DateTime.Now : Convert.ToDateTime(dr["Late_out"]);
                employee.Earlyout = Convert.IsDBNull(dr["Early_out"]) ? DateTime.Now : Convert.ToDateTime(dr["Early_out"]);
                employee.whours = Convert.IsDBNull(dr["ot_hrs"]) ? DateTime.Now : Convert.ToDateTime(dr["ot_hrs"]);
                employee.Flag = Convert.IsDBNull(dr["status"]) ? "" : (string)dr["status"];
                employee.Status21 = Convert.IsDBNull(dr["leave_code"]) ? "" : (string)dr["leave_code"];
                employee.GradeId = Convert.IsDBNull(dr["pn_gradeid"]) ? 0 : Convert.ToInt32(dr["pn_gradeid"]);
                EmployeeList.Add(employee);
            }
            return EmployeeList;
        }
        //public Collection<Employee> otcall(Employee e)
        //{
        //    Collection<Employee> EmployeeList = new Collection<Employee>();
        //    _Connection = Con.fn_Connection();
        //    //SqlCommand _Course = new SqlCommand("select * from time_card where pn_CompanyID=" + e.CompanyId + " and pn_BranchID =" + e.BranchId + " and dates = '" + e.d_Date + "' and pn_employeeid = '" + e.EmployeeId + "' order by emp_name asc", _Connection);
        //    SqlCommand _Course = new SqlCommand("select a.*, b.pn_gradeid from time_card a, paym_employee_profile1 b where a.pn_CompanyID=" + e.CompanyId + " and a.pn_BranchID =" + e.BranchId + " and a.dates = '" + e.d_Date + "' and a.pn_employeeid = '" + e.EmployeeId + "' and a.pn_Employeeid = b.pn_Employeeid order by b.pn_gradeid asc", _Connection);
        //    _Connection.Open();
        //    SqlDataReader dr = _Course.ExecuteReader();
        //    while (dr.Read())
        //    {
        //        Employee employee = new Employee();
        //        employee.FirstName = Convert.IsDBNull(dr["emp_name"]) ? "" : (string)dr["emp_name"];
        //        employee.EmployeeCode = Convert.IsDBNull(dr["emp_code"]) ? "" : (string)dr["emp_code"];
        //        employee.Intime = Convert.IsDBNull(dr["intime"]) ? DateTime.Now : Convert.ToDateTime(dr["intime"]);
        //        employee.Outtime = Convert.IsDBNull(dr["outtime"]) ? DateTime.Now : Convert.ToDateTime(dr["outtime"]);
        //        employee.Latein = Convert.IsDBNull(dr["Late_in"]) ? DateTime.Now : Convert.ToDateTime(dr["Late_in"]);
        //        employee.Lateout = Convert.IsDBNull(dr["Late_out"]) ? DateTime.Now : Convert.ToDateTime(dr["Late_out"]);
        //        employee.Earlyout = Convert.IsDBNull(dr["Early_out"]) ? DateTime.Now : Convert.ToDateTime(dr["Early_out"]);
        //        employee.Flag = Convert.IsDBNull(dr["status"]) ? "" : (string)dr["status"];
        //        employee.Status21 = Convert.IsDBNull(dr["leave_code"]) ? "" : (string)dr["leave_code"];
        //        employee.GradeId = Convert.IsDBNull(dr["pn_gradeid"]) ? 0 : Convert.ToInt32(dr["pn_gradeid"]);
        //        EmployeeList.Add(employee);
        //    }
        //    return EmployeeList;
        //}

        public Collection<Employee> fn_EmployeeMuster(Employee e)
        {
            Collection<Employee> EmployeeList = new Collection<Employee>();
            _Connection = Con.fn_Connection();
            SqlCommand _Course = new SqlCommand("Set DateFormat dmy;select a.*, b.pn_gradeid from time_card a, paym_employee_profile1 b where a.pn_CompanyID=" + e.CompanyId + " and a.pn_BranchID =" + e.BranchId + " and  a.dates >= '" + e.DurationFrom + "' and a.dates <='" + e.DurationTo + "' and a.pn_employeeid = '" + e.EmployeeId + "' and a.pn_Employeeid = b.pn_Employeeid order by dates asc;set dateformat mdy;", _Connection);
            _Connection.Open();
            SqlDataReader dr = _Course.ExecuteReader();
            while (dr.Read())
            {
                Employee employee = new Employee();
                employee.FirstName = Convert.IsDBNull(dr["emp_name"]) ? "" : (string)dr["emp_name"];
                employee.EmployeeCode = Convert.IsDBNull(dr["emp_code"]) ? "" : (string)dr["emp_code"];
                employee.Intime = Convert.IsDBNull(dr["intime"]) ? DateTime.Now : Convert.ToDateTime(dr["intime"]);
                employee.Outtime = Convert.IsDBNull(dr["outtime"]) ? DateTime.Now : Convert.ToDateTime(dr["outtime"]);
                employee.Latein = Convert.IsDBNull(dr["Late_in"]) ? DateTime.Now : Convert.ToDateTime(dr["Late_in"]);
                employee.Lateout = Convert.IsDBNull(dr["Late_out"]) ? DateTime.Now : Convert.ToDateTime(dr["Late_out"]);
                employee.Earlyout = Convert.IsDBNull(dr["Early_out"]) ? DateTime.Now : Convert.ToDateTime(dr["Early_out"]);
                employee.Flag = Convert.IsDBNull(dr["status"]) ? "" : (string)dr["status"];
                employee.Status21 = Convert.IsDBNull(dr["leave_code"]) ? "" : (string)dr["leave_code"];
                employee.GradeId = Convert.IsDBNull(dr["pn_gradeid"]) ? 0 : Convert.ToInt32(dr["pn_gradeid"]);
                employee.dates = Convert.IsDBNull(dr["dates"]) ? DateTime.Now : Convert.ToDateTime(dr["dates"]); 
                EmployeeList.Add(employee);
            }
            return EmployeeList;
        }

        public Collection<Employee> fn_EmployeeConsolidate(Employee e)
        {
            Collection<Employee> EmployeeList = new Collection<Employee>();
            _Connection = Con.fn_Connection();
            SqlCommand _Course = new SqlCommand("select a.*, b.pn_gradeid from time_card a, paym_employee_profile1 b where a.pn_CompanyID=" + e.CompanyId + " and a.pn_BranchID =" + e.BranchId + " and a.dates >= '" + e.DurationFrom + "' and a.dates <='" + e.DurationTo + "'  and a.pn_employeeid = '" + e.EmployeeId + "' and a.pn_Employeeid = b.pn_Employeeid order by dates asc", _Connection);
            _Connection.Open();
            SqlDataReader dr = _Course.ExecuteReader();
            while (dr.Read())
            {
                Employee employee = new Employee();
                employee.FirstName = Convert.IsDBNull(dr["emp_name"]) ? "" : (string)dr["emp_name"];
                employee.EmployeeCode = Convert.IsDBNull(dr["emp_code"]) ? "" : (string)dr["emp_code"];
                employee.Dates = Convert.IsDBNull(dr["dates"]) ? DateTime.Now : Convert.ToDateTime(dr["dates"]);
                employee.Intime = Convert.IsDBNull(dr["intime"]) ? DateTime.Now : Convert.ToDateTime(dr["intime"]);
                employee.Outtime = Convert.IsDBNull(dr["outtime"]) ? DateTime.Now : Convert.ToDateTime(dr["outtime"]);
                employee.Latein = Convert.IsDBNull(dr["late_in"]) ? DateTime.Now : Convert.ToDateTime(dr["late_in"]);
                employee.Lateout = Convert.IsDBNull(dr["late_out"]) ? DateTime.Now : Convert.ToDateTime(dr["late_out"]);
                employee.Flag = Convert.IsDBNull(dr["status"]) ? "" : (string)dr["status"];
                employee.shiftcode = Convert.IsDBNull(dr["shift_code"]) ? "" : (string)dr["shift_code"];
                employee.Status21 = Convert.IsDBNull(dr["leave_code"]) ? "" : (string)dr["leave_code"];
                employee.GradeId = Convert.IsDBNull(dr["pn_gradeid"]) ? 0 : Convert.ToInt32(dr["pn_gradeid"]);
                EmployeeList.Add(employee);
            }
            return EmployeeList;
        }



        public Collection<Employee> fn_route(Employee e)
        {
            Collection<Employee> RouteList = new Collection<Employee>();
            _Connection = Con.fn_Connection();
            string _SqlString = "select *,b.Employee_First_Name from paym_bus_Details a,Paym_employee b where  a.pn_branchID='" + e.BranchId + "' and a.Pn_CompanyID='" + e.CompanyId + "' and b.pn_employeeId=a.driver_ID ";
            SqlCommand _SSRoute = new SqlCommand(_SqlString, _Connection);
            _Connection.Open();
            SqlDataReader dr_Route = _SSRoute.ExecuteReader();
            while (dr_Route.Read())
            {
                Employee employee = new Employee();
                employee.Route_ID = (int)dr_Route["route_Id"];
                employee.Veh_id = dr_Route["vehicle_id"].ToString();
                employee.Veh_number = Convert.IsDBNull(dr_Route["vehicle_number"]) ? "" : (string)dr_Route["vehicle_number"];
                employee.Area_name = Convert.IsDBNull(dr_Route["Destination"]) ? "" : (string)dr_Route["Destination"];
                employee.EmployeeCode = (int)dr_Route["Driver_ID"] + "-" + (string)dr_Route["Employee_First_Name"];
                employee.Routes = Convert.IsDBNull(dr_Route["stop_timings"]) ? "" : (string)dr_Route["stop_timings"];


                RouteList.Add(employee);
            }
            return RouteList;
        }

        public Collection<Employee> fn_route1(Employee e)
        {
            Collection<Employee> RouteList = new Collection<Employee>();
            _Connection = Con.fn_Connection();

            string _SqlString = "select *,b.Employee_First_Name from paym_bus_Details a,Paym_employee b where  a.pn_branchID='" + e.BranchId + "' and a.Pn_CompanyID='" + e.CompanyId + "' and b.pn_employeeId=a.driver_ID ";

            SqlCommand _SSRoute = new SqlCommand(_SqlString, _Connection);
            _Connection.Open();
            SqlDataReader dr_Route = _SSRoute.ExecuteReader();
            while (dr_Route.Read())
            {
                Employee employee = new Employee();
                employee.Route_ID = (int)dr_Route["route_Id"];
                employee.Veh_id = dr_Route["vehicle_id"].ToString();
                employee.Veh_number = Convert.IsDBNull(dr_Route["vehicle_number"]) ? "" : (string)dr_Route["vehicle_number"];
                employee.Area_name = Convert.IsDBNull(dr_Route["Destination"]) ? "" : (string)dr_Route["Destination"];
                employee.EmployeeCode = (int)dr_Route["Driver_ID"] + "-" + (string)dr_Route["Employee_First_Name"];
                employee.Routes = Convert.IsDBNull(dr_Route["stop_timings"]) ? "" : (string)dr_Route["stop_timings"];
                employee.FirstName = Convert.IsDBNull(dr_Route["Incharge"]) ? "" : (string)dr_Route["Incharge"];
                RouteList.Add(employee);
            }
            return RouteList;
        }

        public Collection<Employee> fn_get_bus(Employee e)
        {
            Collection<Employee> BusList = new Collection<Employee>();
            _Connection = Con.fn_Connection();
            string _SqlString = " select a.*,b.employee_first_name from paym_bus_members a,paym_employee b where a.pn_branchID='" + e.BranchId + "' and a.Pn_CompanyID='" + e.CompanyId + "' and a.pn_employee_ID='" + e.EmployeeId + "' and a.pn_employee_Id=b.pn_employeeID";
            SqlCommand _SSBusInfo = new SqlCommand(_SqlString, _Connection);
            _Connection.Open();
            SqlDataReader dr_BusInfo = _SSBusInfo.ExecuteReader();
            while (dr_BusInfo.Read())
            {
                Employee employee = new Employee();
                employee.Area_name = Convert.IsDBNull(dr_BusInfo["boarding_area"]) ? "" : (string)dr_BusInfo["boarding_area"];
                employee.Veh_id = Convert.IsDBNull(dr_BusInfo["Bus_number"]) ? "" : (string)dr_BusInfo["Bus_number"];
                employee.Veh_number = Convert.IsDBNull(dr_BusInfo["vehicle_no"]) ? "" : (string)dr_BusInfo["vehicle_no"];
                employee.Boarding_point = Convert.IsDBNull(dr_BusInfo["boarding_point"]) ? "" : (string)dr_BusInfo["boarding_point"];
                employee.Driver_name = Convert.IsDBNull(dr_BusInfo["Driver_Name"]) ? "" : (string)dr_BusInfo["Driver_Name"];
                BusList.Add(employee);
            }
            _Connection.Close();
            return BusList;
        }

        public Collection<Employee> fn_getemployee(Employee e)
        {
            Collection<Employee> DestinationList = new Collection<Employee>();
            _Connection = Con.fn_Connection();
            string _SqlString = "select distinct a.pn_employee_id,b.Employee_first_name from paym_bus_members a,paym_employee b where a.pn_employee_id=b.pn_employeeid and a.pn_companyID='" + e.CompanyId + "' and a.pn_branchID='" + e.BranchId + "'";
            SqlCommand _SSDestination = new SqlCommand(_SqlString, _Connection);
            _Connection.Open();
            SqlDataReader dr_places = _SSDestination.ExecuteReader();
            while (dr_places.Read())
            {
                Employee employee = new Employee();
                employee.EmployeeId = (int)(dr_places["pn_employee_id"]);
                employee.LastName = Convert.IsDBNull(dr_places["Employee_first_name"]) ? "" : (string)dr_places["Employee_first_name"];
                //  employee.Veh_type = (string)(dr_Vehicles["vehicle_type"]) + "-" + (string)dr_Vehicles["vehicle_type"];
                DestinationList.Add(employee);
            }
            return DestinationList;
        }
        public Collection<Employee> fn_getPastEmploymentDetails(Employee e)
        {
            Collection<Employee> PastEmploymentList = new Collection<Employee>();
            _Connection = Con.fn_Connection();
            _Connection.Open();
            SqlCommand cmd = new SqlCommand("select * from paym_Employee_General where  pn_CompanyID = '" + e.CompanyId + "' and pn_branchId = '" + e.BranchId + "' and pn_EmployeeID= '" + e.EmployeeId+ "' ", _Connection);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                Employee emp = new Employee();
                emp.salary = Convert.IsDBNull(dr["salary"]) ? "" : (string)dr["salary"];
                emp.position = Convert.IsDBNull(dr["position"]) ? "" : (string)dr["position"];
                emp.training_attended = Convert.IsDBNull(dr["training_attended"]) ? "" : (string)dr["training_attended"];
                emp.training_duration = Convert.IsDBNull(dr["training_duration"]) ? "" : (string)dr["training_duration"];
                emp.Ref1_Name = Convert.IsDBNull(dr["Ref1_Name"]) ? "" : (string)dr["Ref1_Name"];
                emp.Ref1_Phno = Convert.IsDBNull(dr["Ref1_Phno"]) ? "" : (string)dr["Ref1_Phno"];
                emp.Ref1_Email = Convert.IsDBNull(dr["Ref1_Email"]) ? "" : (string)dr["Ref1_Email"];
                emp.Ref1_Relation = Convert.IsDBNull(dr["Ref1_Relation"]) ? "" : (string)dr["Ref1_Relation"];
                emp.Ref2_Name = Convert.IsDBNull(dr["Ref2_Name"]) ? "" : (string)dr["Ref2_Name"];
                emp.Ref2_Phno = Convert.IsDBNull(dr["Ref2_Phno"]) ? "" : (string)dr["Ref2_Phno"];
                emp.Ref2_Email = Convert.IsDBNull(dr["Ref2_Email"]) ? "" : (string)dr["Ref2_Email"];
                emp.Ref2_Relation = Convert.IsDBNull(dr["Ref2_Relation"]) ? "" : (string)dr["Ref2_Relation"];
                PastEmploymentList.Add(emp);
            }
            return PastEmploymentList;
        }

        public string PastEmployment_Details(Employee e)
        {
            try
            {            
            _Connection = Con.fn_Connection();
            SqlCommand cmd = new SqlCommand("sp_paym_Employee_PastEmploymentDetails", _Connection);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter[] _ISPParameter=new SqlParameter[15];           
            _ISPParameter[0] = new SqlParameter("@pn_companyID", SqlDbType.Int);
            _ISPParameter[0].Value = e.CompanyId;
            _ISPParameter[1] = new SqlParameter("@pn_branchID", SqlDbType.Int);
            _ISPParameter[1].Value = e.BranchId;
            _ISPParameter[2] = new SqlParameter("@pn_EmployeeID", SqlDbType.Int);
            _ISPParameter[2].Value = e.EmployeeId;
            _ISPParameter[3] = new SqlParameter("@salary", SqlDbType.VarChar);
            _ISPParameter[3].Value = e.salary;
            _ISPParameter[4] = new SqlParameter("@position", SqlDbType.VarChar);
            _ISPParameter[4].Value = e.position;
            _ISPParameter[5] = new SqlParameter("@training_attended", SqlDbType.VarChar);
            _ISPParameter[5].Value = e.training_attended;
            _ISPParameter[6] = new SqlParameter("@training_duration", SqlDbType.VarChar);
            _ISPParameter[6].Value = e.training_duration;
            _ISPParameter[7] = new SqlParameter("@Ref1_Name", SqlDbType.VarChar);
            _ISPParameter[7].Value = e.Ref1_Name;
            _ISPParameter[8] = new SqlParameter("@Ref1_Phno", SqlDbType.VarChar);
            _ISPParameter[8].Value = e.Ref1_Phno;
            _ISPParameter[9] = new SqlParameter("@Ref1_Email", SqlDbType.VarChar);
            _ISPParameter[9].Value = e.Ref1_Email;
            _ISPParameter[10] = new SqlParameter("@Ref1_Relation", SqlDbType.VarChar);
            _ISPParameter[10].Value = e._Ref1_Relation;
            _ISPParameter[11] = new SqlParameter("@Ref2_Name", SqlDbType.VarChar);
            _ISPParameter[11].Value = e._Ref2_Name;
            _ISPParameter[12] = new SqlParameter("@Ref2_Phno", SqlDbType.VarChar);
            _ISPParameter[12].Value = e.Ref2_Phno;
            _ISPParameter[13] = new SqlParameter("@Ref2_Email", SqlDbType.VarChar);
            _ISPParameter[13].Value = e.Ref2_Email;
            _ISPParameter[14] = new SqlParameter("@Ref2_Relation", SqlDbType.VarChar);
            _ISPParameter[14].Value = e.Ref2_Relation;
            for (int i = 0; i < _ISPParameter.Length; i++)
            {
                cmd.Parameters.Add(_ISPParameter[i]);
            }
            _Connection.Open();
            cmd.ExecuteNonQuery();
            _Connection.Close();
            return "0";
            }
            catch(Exception ex)
            {
                return "1";
            }
        }

        public string Transport_details(Employee e)
        {
            try
            {
                _Connection = Con.fn_Connection();
                SqlCommand _Cmd = new SqlCommand("sp_bus_members", _Connection);
                _Cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter[] _ISPParamter = new SqlParameter[8];
                _ISPParamter[0] = new SqlParameter("@pn_companyID", SqlDbType.Int);
                _ISPParamter[0].Value = e.CompanyId;
                _ISPParamter[1] = new SqlParameter("@pn_branchID", SqlDbType.Int);
                _ISPParamter[1].Value = e.BranchId;
                _ISPParamter[2] = new SqlParameter("@pn_employee_Id", SqlDbType.Int);
                _ISPParamter[2].Value = e.EmployeeId;
                _ISPParamter[3] = new SqlParameter("@Boarding_area", SqlDbType.VarChar);
                _ISPParamter[3].Value = e.Area_name;
                _ISPParamter[4] = new SqlParameter("@bus_number", SqlDbType.VarChar);
                _ISPParamter[4].Value = e.Veh_id;
                _ISPParamter[5] = new SqlParameter("@vehicle_no", SqlDbType.VarChar);
                _ISPParamter[5].Value = e.Veh_number;
                _ISPParamter[6] = new SqlParameter("@Boarding_point", SqlDbType.VarChar);
                _ISPParamter[6].Value = e.Boarding_point;
                _ISPParamter[7] = new SqlParameter("@Driver_Name", SqlDbType.VarChar);
                _ISPParamter[7].Value = e.Driver_id;
                for (int i = 0; i < _ISPParamter.Length; i++)
                {
                    _Cmd.Parameters.Add(_ISPParamter[i]);
                }
                _Connection.Open();
                _Cmd.ExecuteNonQuery();
                _Connection.Close();
                return "0";
            }
            catch (SqlException Ex)
            {
                 return "1";
            }
        }

        
        public Collection<Employee> fn_get_Vehicles(Employee e)
        {
            Collection<Employee> VehicleList1 = new Collection<Employee>();
            _Connection = Con.fn_Connection();
            _Connection.Open();
            SqlCommand _vehiclecount = new SqlCommand("select count(*) from paym_bus_Details where pn_companyID='" + e.CompanyId + "' and pn_branchID='" + e.BranchId + "'", _Connection);
            int count = Convert.ToInt32(_vehiclecount.ExecuteScalar());
            string _SqlString = "select Vehicle_id from paym_bus_Details where pn_companyID='" + e.CompanyId + "'";
            SqlCommand _SSvehicle = new SqlCommand(_SqlString, _Connection);

            SqlDataReader dr_Vehicles = _SSvehicle.ExecuteReader();
            while (dr_Vehicles.Read())
            {
                Employee employee = new Employee();
                employee.Veh_id = dr_Vehicles["Vehicle_id"].ToString();
                //employee.Vehicle = (string)(dr_Vehicles["Vehicle_number"]) + "-" + (string)dr_Vehicles["vehicle_type"];
                //  employee.Veh_type = (string)(dr_Vehicles["vehicle_type"]) + "-" + (string)dr_Vehicles["vehicle_type"];
                VehicleList1.Add(employee);
            }
            return VehicleList1;
        }

        public Collection<Employee> fn_get_Emp_general(Employee e)
        {
            Collection<Employee> EmployeeList = new Collection<Employee>();
            _Connection = Con.fn_Connection();
            SqlCommand _Course = new SqlCommand("select * from paym_Employee_General where pn_CompanyID=" + e.CompanyId + " and pn_BranchID =" + e.BranchId + " and pn_EmployeeID=" + e.EmployeeId + "", _Connection);
            _Connection.Open();
            SqlDataReader dr_BranchCompany = _Course.ExecuteReader();
            while (dr_BranchCompany.Read())
            {
                Employee employee = new Employee();
                employee.EmailId = Convert.IsDBNull(dr_BranchCompany["EmailId"]) ? "" : (string)dr_BranchCompany["EmailId"];
                employee.A_EmailId = Convert.IsDBNull(dr_BranchCompany["AlternateEmailId"]) ? "" : (string)dr_BranchCompany["AlternateEmailId"];
                employee.BloodGroup = Convert.IsDBNull(dr_BranchCompany["BloodGroup"]) ? "" : (string)dr_BranchCompany["BloodGroup"];
                employee.Religion = Convert.IsDBNull(dr_BranchCompany["Religion"]) ? "" : (string)dr_BranchCompany["Religion"];
                employee.Nationality = Convert.IsDBNull(dr_BranchCompany["Nationality"]) ? "" : (string)dr_BranchCompany["Nationality"];
                employee.HouseNo = Convert.IsDBNull(dr_BranchCompany["PresentHouseNo"]) ? "" : (string)dr_BranchCompany["PresentHouseNo"];
                employee.StreetName = Convert.IsDBNull(dr_BranchCompany["PresentStreetName"]) ? "" : (string)dr_BranchCompany["PresentStreetName"];
                employee.AddressLine1 = Convert.IsDBNull(dr_BranchCompany["PresentAddLine1"]) ? "" : (string)dr_BranchCompany["PresentAddLine1"];
                employee.AddressLine2 = Convert.IsDBNull(dr_BranchCompany["PresentAddLine2"]) ? "" : (string)dr_BranchCompany["PresentAddLine2"];
                employee.City = Convert.IsDBNull(dr_BranchCompany["PresentCity"]) ? "" : (string)dr_BranchCompany["PresentCity"];
                employee.State = Convert.IsDBNull(dr_BranchCompany["PresentState"]) ? "" : (string)dr_BranchCompany["PresentState"];
                employee.p_HouseNo = Convert.IsDBNull(dr_BranchCompany["PermanentHouseNo"]) ? "" : (string)dr_BranchCompany["PermanentHouseNo"];
                employee.p_StreetName = Convert.IsDBNull(dr_BranchCompany["PermanentStreetName"]) ? "" : (string)dr_BranchCompany["PermanentStreetName"];
                employee.P_AddressLine1 = Convert.IsDBNull(dr_BranchCompany["PermanentAddLine1"]) ? "" : (string)dr_BranchCompany["PermanentAddLine1"];
                employee.P_AddressLine2 = Convert.IsDBNull(dr_BranchCompany["PermanentAddLine2"]) ? "" : (string)dr_BranchCompany["PermanentAddLine2"];
                employee.P_City = Convert.IsDBNull(dr_BranchCompany["PermanentCity"]) ? "" : (string)dr_BranchCompany["PermanentCity"];
                employee.P_State = Convert.IsDBNull(dr_BranchCompany["PermanentState"]) ? "" : (string)dr_BranchCompany["PermanentState"];

                employee.ph_Office = Convert.IsDBNull(dr_BranchCompany["ph_Office"]) ? "" : (string)dr_BranchCompany["ph_Office"];
                employee._Alt_OfficeNo = Convert.IsDBNull(dr_BranchCompany["Alt_Office"]) ? "" : (string)dr_BranchCompany["Alt_Office"];
                employee.ph_Residence = Convert.IsDBNull(dr_BranchCompany["ph_Residence"]) ? "" : (string)dr_BranchCompany["ph_Residence"];
                employee._Alt_Residence = Convert.IsDBNull(dr_BranchCompany["Alt_Residence"]) ? "" : (string)dr_BranchCompany["Alt_Residence"];
                employee.CellNo = Convert.IsDBNull(dr_BranchCompany["CellNo"]) ? "" : (string)dr_BranchCompany["CellNo"];
                employee.Alt_CellNo = Convert.IsDBNull(dr_BranchCompany["Alt_CellNo"]) ? "" : (string)dr_BranchCompany["Alt_CellNo"];
                employee.Fax = Convert.IsDBNull(dr_BranchCompany["Fax"]) ? "" : (string)dr_BranchCompany["Fax"];
                employee.emgname = Convert.IsDBNull(dr_BranchCompany["emgName"]) ? "" : (string)dr_BranchCompany["emgName"];
                employee.emgno = Convert.IsDBNull(dr_BranchCompany["emgPhone"]) ? "" : (string)dr_BranchCompany["emgPhone"];
                employee.empaddress = Convert.IsDBNull(dr_BranchCompany["emgAddress"]) ? "" : (string)dr_BranchCompany["emgAddress"];


                employee.FatherName = Convert.IsDBNull(dr_BranchCompany["FatherName"]) ? "" : (string)dr_BranchCompany["FatherName"];
                employee.MotherName = Convert.IsDBNull(dr_BranchCompany["MotherName"]) ? "" : (string)dr_BranchCompany["MotherName"];
                employee.Children = Convert.IsDBNull(dr_BranchCompany["Children"]) ? "" : (string)dr_BranchCompany["Children"];
                employee.SpouseName = Convert.IsDBNull(dr_BranchCompany["SpouseName"]) ? "" : (string)dr_BranchCompany["SpouseName"];
                employee.IDtype = Convert.IsDBNull(dr_BranchCompany["ID_type"]) ? "" : (string)dr_BranchCompany["ID_type"];
                employee.IDno = Convert.IsDBNull(dr_BranchCompany["ID_no"]) ? "" : (string)dr_BranchCompany["ID_no"];                
                employee.Ref1_Name = Convert.IsDBNull(dr_BranchCompany["Ref1_Name"]) ? "" : (string)dr_BranchCompany["Ref1_Name"];
                employee.Ref1_Phno = Convert.IsDBNull(dr_BranchCompany["Ref1_Phno"]) ? "" : (string)dr_BranchCompany["Ref1_Phno"];
                employee.Ref1_Email = Convert.IsDBNull(dr_BranchCompany["Ref1_Email"]) ? "" : (string)dr_BranchCompany["Ref1_Email"];
                employee.Ref1_Relation = Convert.IsDBNull(dr_BranchCompany["Ref1_Relation"]) ? "" : (string)dr_BranchCompany["Ref1_Relation"];
                employee.Ref2_Name = Convert.IsDBNull(dr_BranchCompany["Ref2_Name"]) ? "" : (string)dr_BranchCompany["Ref2_Name"];
                employee.Ref2_Phno = Convert.IsDBNull(dr_BranchCompany["Ref2_Phno"]) ? "" : (string)dr_BranchCompany["Ref2_Phno"];
                employee.Ref2_Email = Convert.IsDBNull(dr_BranchCompany["Ref2_Email"]) ? "" : (string)dr_BranchCompany["Ref2_Email"];
                employee.Ref2_Relation = Convert.IsDBNull(dr_BranchCompany["Ref2_Relation"]) ? "" : (string)dr_BranchCompany["Ref2_Relation"];

                employee.BloodGroup = Convert.IsDBNull(dr_BranchCompany["BloodGroup"]) ? "" : (string)dr_BranchCompany["BloodGroup"];
                employee.Religion = Convert.IsDBNull(dr_BranchCompany["Religion"]) ? "" : (string)dr_BranchCompany["Religion"];
                employee.Nationality = Convert.IsDBNull(dr_BranchCompany["Nationality"]) ? "" : (string)dr_BranchCompany["Nationality"];
                employee.Salutation = Convert.ToChar(dr_BranchCompany["Salutation"]);
                employee.MaritalStatus = Convert.ToChar(dr_BranchCompany["M_Status"]);
                employee.IDtype = Convert.IsDBNull(dr_BranchCompany["ID_Type"]) ? "" : (string)dr_BranchCompany["ID_Type"];
                employee.IDno = Convert.IsDBNull(dr_BranchCompany["ID_No"]) ? "" : (string)dr_BranchCompany["ID_No"];
                EmployeeList.Add(employee);

            }
            return EmployeeList;
        }

        public string Emp_First(Employee e)
        {
            try
            {
                _Connection = Con.fn_Connection();
                SqlCommand _Cmd = new SqlCommand("sp_paym_Emp_First", _Connection);
                _Cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter[] _ISPParamter = new SqlParameter[23];
                _ISPParamter[0] = new SqlParameter("@pn_CompanyID", SqlDbType.Int);
                _ISPParamter[0].Value = e.CompanyId;
                _ISPParamter[1] = new SqlParameter("@pn_BranchID", SqlDbType.Int);
                _ISPParamter[1].Value = e.BranchId;
                _ISPParamter[2] = new SqlParameter("@pn_EmployeeID", SqlDbType.Int);
                _ISPParamter[2].Value = e.EmployeeId;
                _ISPParamter[3] = new SqlParameter("@EmployeeCode", SqlDbType.VarChar);
                _ISPParamter[3].Value = e.EmployeeCode;
                _ISPParamter[4] = new SqlParameter("@Employee_Full_Name", SqlDbType.VarChar);
                _ISPParamter[4].Value = e.FullName;
                _ISPParamter[5] = new SqlParameter("@Employee_First_Name", SqlDbType.VarChar);
                _ISPParamter[5].Value = e.FirstName;
                _ISPParamter[6] = new SqlParameter("@Employee_Middle_Name", SqlDbType.VarChar);
                _ISPParamter[6].Value = e.MiddleName;
                _ISPParamter[7] = new SqlParameter("@Employee_Last_Name", SqlDbType.VarChar);
                _ISPParamter[7].Value = e.LastName;
                _ISPParamter[8] = new SqlParameter("@DateofBirth", SqlDbType.DateTime);
                _ISPParamter[8].Value = e.d_birth;
                _ISPParamter[9] = new SqlParameter("@Gender", SqlDbType.VarChar);
                _ISPParamter[9].Value = e.Gender;
                _ISPParamter[10] = new SqlParameter("@Readerid", SqlDbType.Int);
                _ISPParamter[10].Value = e.ReaderId;
                _ISPParamter[11] = new SqlParameter("@OT_Eligible", SqlDbType.Char);
                _ISPParamter[11].Value = e.OT_Eligible;
                _ISPParamter[12] = new SqlParameter("@OT_calc", SqlDbType.Float);
                _ISPParamter[12].Value = e.OT_calc;
                _ISPParamter[13] = new SqlParameter("@Pfno", SqlDbType.VarChar);
                _ISPParamter[13].Value = e.PFno;
                _ISPParamter[14] = new SqlParameter("@Esino", SqlDbType.VarChar);
                _ISPParamter[14].Value = e.ESIno;
                _ISPParamter[15] = new SqlParameter("@Pan_no", SqlDbType.VarChar);
                _ISPParamter[15].Value = e.Pan_no;
                _ISPParamter[16] = new SqlParameter("@reporting_person", SqlDbType.VarChar);
                _ISPParamter[16].Value = e.Reporting;
                _ISPParamter[17] = new SqlParameter("@reporting_email", SqlDbType.VarChar);
                _ISPParamter[17].Value = e.ReportingEmail;
                _ISPParamter[18] = new SqlParameter("@reportingID", SqlDbType.Int);
                _ISPParamter[18].Value = e.ReportID;
                _ISPParamter[19] = new SqlParameter("@basic_salary", SqlDbType.VarChar);
                _ISPParamter[19].Value = e.basic_salary;
                _ISPParamter[20] = new SqlParameter("@salary_type", SqlDbType.VarChar);
                _ISPParamter[20].Value = e.Salary_Type;
                _ISPParamter[21] = new SqlParameter("@TDS_Applicable", SqlDbType.Char);
                _ISPParamter[21].Value = e.TDS;
                _ISPParamter[22] = new SqlParameter("@status", SqlDbType.Char);
                _ISPParamter[22].Value = e.status;
                //_ISPParamter[23] = new SqlParameter("@role", SqlDbType.Int);
                //_ISPParamter[23].Value = e.Role;
                for (int i = 0; i < _ISPParamter.Length; i++)
                {
                    _Cmd.Parameters.Add(_ISPParamter[i]);
                }
                _Connection.Open();
                _Cmd.ExecuteNonQuery();
                _Connection.Close();
                return "0";
            }
            catch (SqlException Ex)
            {
                return "1";
            }
        }

        public string Emp_General(Employee e)
        {
            try
            {
                _Connection = Con.fn_Connection();
                SqlCommand _Cmd = new SqlCommand("[sp_paym_Emp_General]", _Connection);
                _Cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter[] _ISPParamter = new SqlParameter[12];
                _ISPParamter[0] = new SqlParameter("@pn_CompanyID", SqlDbType.Int);
                _ISPParamter[0].Value = e.CompanyId;
                _ISPParamter[1] = new SqlParameter("@pn_BranchID", SqlDbType.Int);
                _ISPParamter[1].Value = e.BranchId;
                _ISPParamter[2] = new SqlParameter("@pn_EmployeeID", SqlDbType.Int);
                _ISPParamter[2].Value = e.EmployeeId;
                _ISPParamter[3] = new SqlParameter("@BloodGroup", SqlDbType.VarChar);
                _ISPParamter[3].Value = e.BloodGroup;
                _ISPParamter[4] = new SqlParameter("@Religion", SqlDbType.VarChar);
                _ISPParamter[4].Value = e.Religion;
                _ISPParamter[5] = new SqlParameter("@Nationality", SqlDbType.VarChar);
                _ISPParamter[5].Value = e.Nationality;
                _ISPParamter[6] = new SqlParameter("@Salutation", SqlDbType.Char);
                _ISPParamter[6].Value = e.Salutation;
                _ISPParamter[7] = new SqlParameter("@M_Status", SqlDbType.Char);
                _ISPParamter[7].Value = e.MaritalStatus;
                _ISPParamter[8] = new SqlParameter("@ID_Type", SqlDbType.VarChar);
                _ISPParamter[8].Value = e.IDtype;
                _ISPParamter[9] = new SqlParameter("@ID_Others", SqlDbType.VarChar);
                _ISPParamter[9].Value = e.IDOthers;
                _ISPParamter[10] = new SqlParameter("@ID_No", SqlDbType.VarChar);
                _ISPParamter[10].Value = e.IDno;
                _ISPParamter[11] = new SqlParameter("@EmailID", SqlDbType.VarChar);
                _ISPParamter[11].Value = e.EmailId;
                for (int i = 0; i < _ISPParamter.Length; i++)
                {
                    _Cmd.Parameters.Add(_ISPParamter[i]);
                }
                _Connection.Open();
                _Cmd.ExecuteNonQuery();
                _Connection.Close();
                return "0";
            }
            catch (SqlException Ex)
            {
                return "1";
            }
        }

        public Collection<Employee> fn_get_Emp_Bank(Employee e)
        {
            Collection<Employee> BankList = new Collection<Employee>();

            _Connection = Con.fn_Connection();
            SqlCommand cmd = new SqlCommand("select * from paym_employee where pn_CompanyID = '" + e.CompanyId + "' and pn_branchId = '" + e.BranchId + "' and pn_EmployeeID = '" + e.EmployeeId + "' ", _Connection);
            _Connection.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                Employee employee = new Employee();
                employee.Bank_Code = Convert.IsDBNull(dr["Bank_code"]) ? "" : (string)dr["Bank_code"];
                employee.Bank_Name = Convert.IsDBNull(dr["Bank_Name"]) ? "" : (string)dr["Bank_Name"];
                employee.Branch_Name = Convert.IsDBNull(dr["Branch_Name"]) ? "" : (string)dr["Branch_Name"];
                employee.Account_Type = Convert.IsDBNull(dr["Account_Type"]) ? "" : (string)dr["Account_Type"];
                employee.MICR_Code = Convert.IsDBNull(dr["MICR_Code"]) ? "" : (string)dr["MICR_Code"];
                employee.IFSC_Code = Convert.IsDBNull(dr["IFSC_Code"]) ? "" : (string)dr["IFSC_Code"];
                employee.Address = Convert.IsDBNull(dr["Address"]) ? "" : (string)dr["Address"];
                employee.Other_Info = Convert.IsDBNull(dr["Other_Info"]) ? "" : (string)dr["Other_Info"];
                BankList.Add(employee);
            }
            return BankList;
        }

        public string Employee_Contact(Employee e)
        {
            try
            {
                _Connection = Con.fn_Connection();
                SqlCommand _Cmd = new SqlCommand("sp_paym_Employee_Contact", _Connection);
                _Cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter[] _ISPParamter = new SqlParameter[27];
                _ISPParamter[0] = new SqlParameter("@pn_CompanyID", SqlDbType.Int);
                _ISPParamter[0].Value = e.CompanyId;
                _ISPParamter[1] = new SqlParameter("@pn_BranchID", SqlDbType.Int);
                _ISPParamter[1].Value = e.BranchId;
                _ISPParamter[2] = new SqlParameter("@pn_EmployeeID", SqlDbType.Int);
                _ISPParamter[2].Value = e.EmployeeId;

                _ISPParamter[3] = new SqlParameter("@PresentHouseNo", SqlDbType.VarChar);
                _ISPParamter[3].Value = e.HouseNo;
                _ISPParamter[4] = new SqlParameter("@PresentAddLine1", SqlDbType.VarChar);
                _ISPParamter[4].Value = e.AddressLine1;
                _ISPParamter[5] = new SqlParameter("@PresentAddLine2", SqlDbType.VarChar);
                _ISPParamter[5].Value = e.AddressLine2;
                _ISPParamter[6] = new SqlParameter("@PresentCity", SqlDbType.VarChar);
                _ISPParamter[6].Value = e.City;
                _ISPParamter[7] = new SqlParameter("@PresentState", SqlDbType.VarChar);
                _ISPParamter[7].Value = e.State;
                _ISPParamter[8] = new SqlParameter("@PresentStreetName", SqlDbType.VarChar);
                _ISPParamter[8].Value = e.StreetName;

                _ISPParamter[9] = new SqlParameter("@PermanentHouseNo", SqlDbType.VarChar);
                _ISPParamter[9].Value = e.p_HouseNo;
                _ISPParamter[10] = new SqlParameter("@PermanentAddLine1", SqlDbType.VarChar);
                _ISPParamter[10].Value = e.P_AddressLine1;
                _ISPParamter[11] = new SqlParameter("@PermanentAddLine2", SqlDbType.VarChar);
                _ISPParamter[11].Value = e.P_AddressLine2;
                _ISPParamter[12] = new SqlParameter("@PermanentState", SqlDbType.VarChar);
                _ISPParamter[12].Value = e.P_State;
                _ISPParamter[13] = new SqlParameter("@PermanentCity", SqlDbType.VarChar);
                _ISPParamter[13].Value = e.P_City;
                _ISPParamter[14] = new SqlParameter("@PermanentStreetName", SqlDbType.VarChar);
                _ISPParamter[14].Value = e.p_StreetName;

                _ISPParamter[15] = new SqlParameter("@ph_Office", SqlDbType.VarChar);
                _ISPParamter[15].Value = e.ph_Office;
                _ISPParamter[16] = new SqlParameter("@Alt_Office", SqlDbType.VarChar);
                _ISPParamter[16].Value = e.Alt_Officeno;
                _ISPParamter[17] = new SqlParameter("@ph_Residence", SqlDbType.VarChar);
                _ISPParamter[17].Value = e.ph_Residence;
                _ISPParamter[18] = new SqlParameter("@Alt_Residence", SqlDbType.VarChar);
                _ISPParamter[18].Value = e.Alt_ResidenceNo;
                _ISPParamter[19] = new SqlParameter("@CellNo", SqlDbType.VarChar);
                _ISPParamter[19].Value = e.CellNo;
                _ISPParamter[20] = new SqlParameter("@Alt_CellNo", SqlDbType.VarChar);
                _ISPParamter[20].Value = e.Alt_CellNo;
                _ISPParamter[21] = new SqlParameter("@EmailId", SqlDbType.VarChar);
                _ISPParamter[21].Value = e.EmailId;
                _ISPParamter[22] = new SqlParameter("@AlternateEmailId", SqlDbType.VarChar);
                _ISPParamter[22].Value = e.A_EmailId;
                _ISPParamter[23] = new SqlParameter("@Fax", SqlDbType.VarChar);
                _ISPParamter[23].Value = e.Fax;

                _ISPParamter[24] = new SqlParameter("@emgName", SqlDbType.VarChar);
                _ISPParamter[24].Value = e.emgname;
                _ISPParamter[25] = new SqlParameter("@emgPhone", SqlDbType.VarChar);
                _ISPParamter[25].Value = e.emgno;
                _ISPParamter[26] = new SqlParameter("@emgAddress", SqlDbType.VarChar);
                _ISPParamter[26].Value = e.empaddress;

                for (int i = 0; i < _ISPParamter.Length; i++)
                {
                    _Cmd.Parameters.Add(_ISPParamter[i]);
                }
                _Connection.Open();
                _Cmd.ExecuteNonQuery();
                _Connection.Close();
                return "0";
            }
            catch (SqlException Ex)
            {
                return "1";
            }
        }
        //Employee Master Update Procedure End


        public string Employee_Family(Employee e)
        {
            try
            {
                _Connection = Con.fn_Connection();
                SqlCommand _Cmd = new SqlCommand("sp_paym_Employee_Family", _Connection);
                _Cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter[] _ISPParamter = new SqlParameter[7];
                _ISPParamter[0] = new SqlParameter("@pn_CompanyID", SqlDbType.Int);
                _ISPParamter[0].Value = e.CompanyId;
                _ISPParamter[1] = new SqlParameter("@pn_BranchID", SqlDbType.Int);
                _ISPParamter[1].Value = e.BranchId;
                _ISPParamter[2] = new SqlParameter("@pn_EmployeeID", SqlDbType.Int);
                _ISPParamter[2].Value = e.EmployeeId;
                _ISPParamter[3] = new SqlParameter("@FatherName", SqlDbType.VarChar);
                _ISPParamter[3].Value = e.FatherName;
                _ISPParamter[4] = new SqlParameter("@MotherName", SqlDbType.VarChar);
                _ISPParamter[4].Value = e.MotherName;
                _ISPParamter[5] = new SqlParameter("@Children", SqlDbType.VarChar);
                _ISPParamter[5].Value = e.Children;
                _ISPParamter[6] = new SqlParameter("@SpouseName", SqlDbType.VarChar);
                _ISPParamter[6].Value = e.SpouseName;

                for (int i = 0; i < _ISPParamter.Length; i++)
                {
                    _Cmd.Parameters.Add(_ISPParamter[i]);
                }
                _Connection.Open();
                _Cmd.ExecuteNonQuery();
                _Connection.Close();
                return "0";
            }
            catch (SqlException Ex)
            {
                return "1";
            }
        }

        public string Employee_Bank(Employee e)
        {
            try
            {
                _Connection = Con.fn_Connection();
                SqlCommand _Cmd = new SqlCommand("sp_paym_Employee_Bank", _Connection);
                _Cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter[] _ISPParamter = new SqlParameter[11];
                _ISPParamter[0] = new SqlParameter("@pn_CompanyID", SqlDbType.Int);
                _ISPParamter[0].Value = e.CompanyId;
                _ISPParamter[1] = new SqlParameter("@pn_BranchID", SqlDbType.Int);
                _ISPParamter[1].Value = e.BranchId;
                _ISPParamter[2] = new SqlParameter("@pn_EmployeeID", SqlDbType.Int);
                _ISPParamter[2].Value = e.EmployeeId;
                _ISPParamter[3] = new SqlParameter("@Bank_code", SqlDbType.VarChar);
                _ISPParamter[3].Value = e.Bank_Code;
                _ISPParamter[4] = new SqlParameter("@Bank_Name", SqlDbType.VarChar);
                _ISPParamter[4].Value = e.Bank_Name;
                _ISPParamter[5] = new SqlParameter("@Branch_Name", SqlDbType.VarChar);
                _ISPParamter[5].Value = e.Branch_Name;
                _ISPParamter[6] = new SqlParameter("@Account_type", SqlDbType.VarChar);
                _ISPParamter[6].Value = e.Account_Type;
                _ISPParamter[7] = new SqlParameter("@MICR_code", SqlDbType.VarChar);
                _ISPParamter[7].Value = e.MICR_Code;
                _ISPParamter[8] = new SqlParameter("@IFSC_code", SqlDbType.VarChar);
                _ISPParamter[8].Value = e.IFSC_Code;
                _ISPParamter[9] = new SqlParameter("@Address", SqlDbType.VarChar);
                _ISPParamter[9].Value = e.Address;
                _ISPParamter[10] = new SqlParameter("@Other_info", SqlDbType.VarChar);
                _ISPParamter[10].Value = e.Other_Info;

                for (int i = 0; i < _ISPParamter.Length; i++)
                {
                    _Cmd.Parameters.Add(_ISPParamter[i]);
                }
                _Connection.Open();
                _Cmd.ExecuteNonQuery();
                _Connection.Close();
                return "0";

            }
            catch (SqlException Ex)
            {
                return "1";
            }
        }

        public Collection<Employee> fn_getEmployee_EducationList(Employee e)
        {
            string str_edu;
            Collection<Employee> EmployeeList = new Collection<Employee>();
            _Connection = Con.fn_Connection();

            str_edu = "select c.v_CourseName,ed.pn_CourseID,ed.pn_SpecializationID,ed.InstitutionName,ed.Percentage,ed.CompletedYear,ed.Mode,ed.Information from paym_Employee_Education ed,hrmm_Course c ";
            str_edu = str_edu + "where ed.pn_CompanyID=" + e.CompanyId + " and ed.pn_BranchID =" + e.BranchId + " and ed.pn_EmployeeID=" + e.EmployeeId + " and ";
            str_edu = str_edu + "c.pn_CourseID in(select ed.pn_CourseID from paym_Employee_Education where ed.pn_CompanyID=" + e.CompanyId + " and ed.pn_BranchID =" + e.BranchId + " and ed.pn_EmployeeID=" + e.EmployeeId + ")";
            SqlCommand _Course = new SqlCommand(str_edu, _Connection);
            _Connection.Open();
            SqlDataReader dr = _Course.ExecuteReader();
            while (dr.Read())
            {
                Employee employee = new Employee();
                employee.PGCourseID = (int)dr["pn_CourseID"];
                employee.specializationID = (int)dr["pn_SpecializationID"];
                employee.PGCourseName = Convert.IsDBNull(dr["v_CourseName"]) ? "" : (string)dr["v_CourseName"];
                employee.PGInstutionName = Convert.IsDBNull(dr["InstitutionName"]) ? "" : (string)dr["InstitutionName"];
                employee.PGPercentage = Convert.IsDBNull(dr["Percentage"]) ? "" : (string)dr["Percentage"];
                employee.PGCompletedYear = Convert.IsDBNull(dr["CompletedYear"]) ? "" : (string)dr["CompletedYear"];
                employee.mode = Convert.IsDBNull(dr["Mode"]) ? "" : (string)dr["Mode"];
                employee.PGCompletedinf = Convert.IsDBNull(dr["Information"]) ? "" : (string)dr["Information"];
                EmployeeList.Add(employee);
            }
            return EmployeeList;
        }

        public Collection<Employee> Priview_Employee_EducationList(Employee e)
        {
            string str_edu;
            Collection<Employee> EmployeeList = new Collection<Employee>();
            _Connection = Con.fn_Connection();

            str_edu = "select c.v_CourseName,ed.pn_CourseID,ed.pn_SpecializationID,ed.pn_EmployeeID,ed.InstitutionName,ed.Percentage,ed.CompletedYear,ed.Mode,ed.Information,s.v_SpecializationName from paym_Employee_Education ed,hrmm_Course c,hrmm_Specialization s ";
            str_edu = str_edu + "where ed.pn_CompanyID=" + e.CompanyId + " and ed.pn_BranchID =" + e.BranchId + " and ed.pn_EmployeeID=" + e.EmployeeId + " and ";
            str_edu = str_edu + "c.pn_CourseID=ed.pn_CourseID and s.pn_SpecializationID=ed.pn_SpecializationID";

            SqlCommand _Course = new SqlCommand(str_edu, _Connection);
            _Connection.Open();
            SqlDataReader dr = _Course.ExecuteReader();
            while (dr.Read())
            {
                Employee employee = new Employee();
                employee.PGCourseID = (int)dr["pn_CourseID"];
                employee.EmployeeId = (int)dr["pn_EmployeeID"];
                employee.specializationID = (int)dr["pn_SpecializationID"];
                employee.PGCourseName = Convert.IsDBNull(dr["v_CourseName"]) ? "" : (string)dr["v_CourseName"];
                employee.PGInstutionName = Convert.IsDBNull(dr["InstitutionName"]) ? "" : (string)dr["InstitutionName"];
                employee.PGPercentage = Convert.IsDBNull(dr["Percentage"]) ? "" : (string)dr["Percentage"];
                employee.PGCompletedYear = Convert.IsDBNull(dr["CompletedYear"]) ? "" : (string)dr["CompletedYear"];
                employee.mode = Convert.IsDBNull(dr["Mode"]) ? "" : (string)dr["Mode"];
                employee.PGCompletedinf = Convert.IsDBNull(dr["Information"]) ? "" : (string)dr["Information"];
                employee.specializationName = Convert.IsDBNull(dr["v_SpecializationName"]) ? "" : (string)dr["v_SpecializationName"];
                EmployeeList.Add(employee);
            }
            return EmployeeList;
        }

        public Collection<Employee> fn_getEmployee_skills(Employee e)
        {
            string str_edu;
            Collection<Employee> EmployeeList = new Collection<Employee>();
            _Connection = Con.fn_Connection();

            str_edu = "select c.v_SkillName,ed.pn_SkillID,ed.v_Experience,ed.v_Proficiency from paym_Employee_Skills ed,hrmm_SkillsMaster c ";
            str_edu = str_edu + "where ed.pn_CompanyID=" + e.CompanyId + " and ed.pn_BranchID =" + e.BranchId + " and ed.pn_EmployeeID=" + e.EmployeeId + " and ";
            str_edu = str_edu + "c.pn_SkillID in(select ed.pn_SkillID from paym_Employee_Skills where ed.pn_CompanyID=" + e.CompanyId + " and ed.pn_BranchID =" + e.BranchId + " and ed.pn_EmployeeID=" + e.EmployeeId + ")";

            SqlCommand _Course = new SqlCommand(str_edu, _Connection);
            _Connection.Open();
            SqlDataReader dr = _Course.ExecuteReader();
            while (dr.Read())
            {
                Employee employee = new Employee();
                employee.SkillId = (int)dr["pn_SkillID"];
                employee.SkillName = Convert.IsDBNull(dr["v_SkillName"]) ? "" : (string)dr["v_SkillName"];
                employee.Experience = Convert.IsDBNull(dr["v_Experience"]) ? "" : (string)dr["v_Experience"];
                employee.Proficiency = Convert.IsDBNull(dr["v_Proficiency"]) ? "" : (string)dr["v_Proficiency"];
                EmployeeList.Add(employee);
            }
            return EmployeeList;
        }

        public Collection<Employee> fn_Skills(Employee e)
        {
            Collection<Employee> CourseList = new Collection<Employee>();
            _Connection = Con.fn_Connection();
            SqlCommand _Course = new SqlCommand("select * from hrmm_SkillsMaster where pn_SkillID in" + e.temp_str + " and status='Y'", _Connection);
            _Connection.Open();
            SqlDataReader dr = _Course.ExecuteReader();
            while (dr.Read())
            {
                Employee course = new Employee();
                course.SkillId = (int)dr["pn_SkillID"];
                course.SkillName = Convert.IsDBNull(dr["v_SkillName"]) ? "" : (string)dr["v_SkillName"];
                course.PGInstutionName = "";
                course.PGPercentage = "";
                course.PGCompletedYear = "";

                CourseList.Add(course);
            }
            return CourseList;
        }

        public Collection<Employee> fn_get_Emp_Profile1(Employee e)
        {
            Collection<Employee> EmployeeList = new Collection<Employee>();
            _Connection = Con.fn_Connection();
            SqlCommand _Course = new SqlCommand("set dateformat dmy;select top 1 * from paym_Employee_profile1 where pn_CompanyID=" + e.CompanyId + " and pn_BranchID =" + e.BranchId + " and pn_EmployeeID=" + e.EmployeeId + "  order by d_Date desc;set dateformat mdy;", _Connection);
            _Connection.Open();
            SqlDataReader dr = _Course.ExecuteReader();
            while (dr.Read())
            {
                Employee employee = new Employee();
                employee.DivisionId = (int)dr["pn_DivisionId"];
                employee.DepartmentId = (int)dr["pn_DepartmentId"];
                employee.DesignationId = (int)dr["pn_DesingnationId"];
                employee.GradeId = (int)dr["pn_GradeId"];
                employee.ShiftId = (int)dr["pn_ShiftId"];
                employee.CategoryId = (int)dr["pn_CategoryId"];
                employee.JobStatusId = (int)dr["pn_JobStatusID"];
                employee.LevelId = (int)dr["pn_LevelID"];
                employee.ProjectsiteId = (int)dr["pn_projectsiteID"];
                employee.ReportID = (int)dr["r_Department"];
                employee.temp_str = Convert.IsDBNull(dr["v_Reason"]) ? "" : (string)dr["v_Reason"];
                EmployeeList.Add(employee);
            }
            return EmployeeList;
        }

        public Collection<Employee> fn_get_Emp_Profile(Employee e)
        {
            Collection<Employee> EmployeeList = new Collection<Employee>();
            _Connection = Con.fn_Connection();

            SqlCommand _Course = new SqlCommand("select top 1 * from paym_Employee_profile1 where pn_CompanyID=" + e.CompanyId + " and pn_BranchID =" + e.BranchId + " and pn_EmployeeID=" + e.EmployeeId + "  order by d_Date desc", _Connection);
            _Connection.Open();
            SqlDataReader dr = _Course.ExecuteReader();
            while (dr.Read())
            {
                Employee employee = new Employee();
                employee.Date = (DateTime)dr["d_Date"];
                employee.DivisionId = (int)dr["pn_DivisionId"];
                employee.DepartmentId = (int)dr["pn_DepartmentId"];
                employee.DesignationId = (int)dr["pn_DesingnationId"];
                employee.GradeId = (int)dr["pn_GradeId"];
                employee.ShiftId = (int)dr["pn_ShiftId"];
                employee.CategoryId = (int)dr["pn_CategoryId"];
                employee.JobStatusId = (int)dr["pn_JobStatusID"];
                employee.LevelId = (int)dr["pn_LevelID"];
                employee.ProjectsiteId = (int)dr["pn_projectsiteID"];
                employee.ReportID = (int)dr["r_Department"];
                employee.temp_str = Convert.ToString(dr["v_Reason"]);

                EmployeeList.Add(employee);
            }
            return EmployeeList;

        }

        public Collection<Employee> fn_get_Emp_WorkDetails(Employee e)
        {
            Collection<Employee> EmployeeList = new Collection<Employee>();
            _Connection = Con.fn_Connection();
            SqlCommand _Course = new SqlCommand("select * from paym_Employee_WorkDetails where pn_CompanyID=" + e.CompanyId + " and pn_BranchID =" + e.BranchId + " and pn_EmployeeID=" + e.EmployeeId + "", _Connection);
            _Connection.Open();
            SqlDataReader dr = _Course.ExecuteReader();
            while (dr.Read())
            {
                Employee employee = new Employee();
                employee.d_join = Convert.ToDateTime(dr["JoiningDate"]);
                employee.d_Offer = Convert.ToDateTime(dr["OfferDate"]);
                employee.d_probotion = Convert.ToDateTime(dr["ProbationUpto"]);
                employee.d_extended = Convert.ToDateTime(dr["ExtendedUpto"]);
                employee.d_confirmation = Convert.ToDateTime(dr["ConfirmationDate"]);
                employee.d_retirement = Convert.ToDateTime(dr["RetirementDate"]);
                employee.d_renue = Convert.ToDateTime(dr["ContractRenviewDate"]);
                employee.Reason = Convert.IsDBNull(dr["v_Reason"]) ? "" : (string)dr["v_Reason"];

                EmployeeList.Add(employee);
            }
            return EmployeeList;
        }

        public Collection<Employee> fn_get_Emp_photo(Employee e)
        {
            Collection<Employee> EmployeeList = new Collection<Employee>();
            _Connection = Con.fn_Connection();
            SqlCommand _Course = new SqlCommand("select * from paym_Employee_Photo where pn_CompanyID=" + e.CompanyId + " and pn_BranchID =" + e.BranchId + " and pn_EmployeeID=" + e.EmployeeId + "", _Connection);
            _Connection.Open();
            SqlDataReader dr = _Course.ExecuteReader();
            while (dr.Read())
            {
                Employee employee = new Employee();
                employee.img_path = Convert.IsDBNull(dr["v_ImagePath"]) ? "" : (string)dr["v_ImagePath"];
                EmployeeList.Add(employee);
            }
            return EmployeeList;
        }

        //*******************************************************************************

        //public Collection<Employee> fn_getEmployeeName(int empid)
        //{
        //    Collection<Employee> EmployeeList = new Collection<Employee>();
        //    _Connection = Con.fn_Connection();
        //    SqlCommand _Course = new SqlCommand("select * from paym_Employee where pn_EmployeeID="+empid+"", _Connection);
        //    _Connection.Open();
        //    SqlDataReader dr = _Course.ExecuteReader();
        //    while (dr.Read())
        //    {
        //        Employee employee = new Employee();

        //        employee.FirstName = Convert.IsDBNull(dr["Employee_First_Name"]) ? "" : (string)dr["Employee_First_Name"];

        //        EmployeeList.Add(employee);
        //    }
        //    return EmployeeList;
        //}


        //############################################################################################################


        public Collection<Company.Company> fn_getIds(string _pCompanyCode)
        {
            Collection<Company.Company> CompanyList = new Collection<ePayHrms.Company.Company>();
            _Connection = Con.fn_Connection();
            string _scmd = "select pn_CompanyID,pn_BranchID from paym_Branch where CompanyCode ='" + _pCompanyCode + "' and status='Y'";
            SqlCommand _Cmd = new SqlCommand(_scmd, _Connection);
            _Connection.Open();
            SqlDataReader dr_Company = _Cmd.ExecuteReader();
            while (dr_Company.Read())
            {
                Company.Company company = new ePayHrms.Company.Company();
                company.CompanyId = (int)dr_Company["pn_CompanyID"];
                company.BranchCompanyId = (int)dr_Company["pn_BranchID"];
                CompanyList.Add(company);
            }
            return CompanyList;
        }

        public string fn_GetDeductionName(int _DeductionId)
        {
            _Connection = Con.fn_Connection();
            string _scmd = "select v_Description from paym_Deduction where pn_DeductionID = " + _DeductionId + " and status='Y'";
            SqlCommand _Cmd = new SqlCommand(_scmd, _Connection);
            _Connection.Open();
            SqlDataReader dr_Deduction = _Cmd.ExecuteReader();
            Employee employee = new Employee();
            while (dr_Deduction.Read())
            {
                employee.DeductionName = Convert.IsDBNull(dr_Deduction["v_Description"]) ? "" : (string)dr_Deduction["v_Description"];
            }
            return employee.DeductionName;
        }

        public string fn_GetAppointmentTypeName(int _AppointmentTypeId)
        {
            _Connection = Con.fn_Connection();
            string _scmd = "select v_AppointmentDisc from paym_AppointmentType where pn_AppointmentTypeId = " + _AppointmentTypeId + " and status='Y'";
            SqlCommand _Cmd = new SqlCommand(_scmd, _Connection);
            _Connection.Open();
            SqlDataReader dr_AppointmentType = _Cmd.ExecuteReader();
            Employee employee = new Employee();
            while (dr_AppointmentType.Read())
            {
                employee.AppointmentTypeName = Convert.IsDBNull(dr_AppointmentType["v_AppointmentDisc"]) ? "" : (string)dr_AppointmentType["v_AppointmentDisc"];
            }
            return employee.AppointmentTypeName;
        }

        public string fn_GetAllowanceName(int _AllowanceId)
        {
            _Connection = Con.fn_Connection();
            string _scmd = "select AllowanceName from paym_Allowance where pn_AllowanceID = " + _AllowanceId + " and status='Y'";
            SqlCommand _Cmd = new SqlCommand(_scmd, _Connection);
            _Connection.Open();
            SqlDataReader dr_Allowance = _Cmd.ExecuteReader();
            Employee employee = new Employee();
            while (dr_Allowance.Read())
            {
                employee.AllowanceName = Convert.IsDBNull(dr_Allowance["AllowanceName"]) ? "" : (string)dr_Allowance["AllowanceName"];
            }
            return employee.AllowanceName;
        }

        public string fn_GetDivisionName(int _DivisionId)
        {
            _Connection = Con.fn_Connection();
            string _scmd = "select v_DivisionName from paym_Division where pn_DivisionID = " + _DivisionId + " and status='Y'";
            SqlCommand _Cmd = new SqlCommand(_scmd, _Connection);
            _Connection.Open();
            SqlDataReader dr_Division = _Cmd.ExecuteReader();
            Employee employee = new Employee();
            while (dr_Division.Read())
            {
                employee.DivisionName = Convert.IsDBNull(dr_Division["v_DivisionName"]) ? "" : (string)dr_Division["v_DivisionName"];
            }
            return employee.DivisionName;
        }


        public string fn_GetLevelName(int _LevelId)
        {
            _Connection = Con.fn_Connection();
            string _scmd = "select v_LevelName from paym_Level where pn_LevelID = " + _LevelId + " and status='Y'";
            SqlCommand _Cmd = new SqlCommand(_scmd, _Connection);
            _Connection.Open();
            SqlDataReader dr_Department = _Cmd.ExecuteReader();
            Employee employee = new Employee();
            while (dr_Department.Read())
            {
                employee.DepartmentName = Convert.IsDBNull(dr_Department["v_LevelName"]) ? "" : (string)dr_Department["v_LevelName"];
            }
            return employee.DepartmentName;
        }

        public string fn_GetDepartmentName(int _DepartmentId)
        {
            _Connection = Con.fn_Connection();
            string _scmd = "select v_DepartmentName from paym_Department where pn_DepartmentID = " + _DepartmentId + " and status='Y'";
            SqlCommand _Cmd = new SqlCommand(_scmd, _Connection);
            _Connection.Open();
            SqlDataReader dr_Department = _Cmd.ExecuteReader();
            Employee employee = new Employee();
            while (dr_Department.Read())
            {
                employee.DepartmentName = Convert.IsDBNull(dr_Department["v_DepartmentName"]) ? "" : (string)dr_Department["v_DepartmentName"];
            }
            return employee.DepartmentName;
        }

        public string fn_GetDesignationName(int _DesignationId)
        {
            _Connection = Con.fn_Connection();
            string _scmd = "select v_DesignationName from paym_Designation where pn_DesignationID = " + _DesignationId + " and status='Y'";
            SqlCommand _Cmd = new SqlCommand(_scmd, _Connection);
            _Connection.Open();
            SqlDataReader dr_Designation = _Cmd.ExecuteReader();
            Employee employee = new Employee();
            while (dr_Designation.Read())
            {
                employee.DesignationName = Convert.IsDBNull(dr_Designation["v_DesignationName"]) ? "" : (string)dr_Designation["v_DesignationName"];
            }
            return employee.DesignationName;
        }

        public string fn_GetCategoryName(int _CategoryId)
        {
            _Connection = Con.fn_Connection();
            string _scmd = "select v_CategoryName from paym_Category where pn_CategoryID = " + _CategoryId + " and status='Y'";
            SqlCommand _Cmd = new SqlCommand(_scmd, _Connection);
            _Connection.Open();
            SqlDataReader dr_Category = _Cmd.ExecuteReader();
            Employee employee = new Employee();
            while (dr_Category.Read())
            {
                employee.CategoryName = Convert.IsDBNull(dr_Category["v_CategoryName"]) ? "" : (string)dr_Category["v_CategoryName"];
            }
            return employee.CategoryName;
        }

        public Collection<Employee> fn_GetShiftName(int _ShiftId)
        {
            Collection<Employee> ShiftList = new Collection<Employee>();
            _Connection = Con.fn_Connection();
            string _scmd = "select * from paym_Shift where pn_ShiftID = " + _ShiftId + " and status='Y'";
            SqlCommand _Cmd = new SqlCommand(_scmd, _Connection);
            _Connection.Open();
            SqlDataReader dr_Shift = _Cmd.ExecuteReader();
            while (dr_Shift.Read())
            {
                Employee employee = new Employee();
                employee.ShiftName = Convert.IsDBNull(dr_Shift["v_ShiftName"]) ? "" : (string)dr_Shift["v_ShiftName"];
                employee.ShiftIntime = Convert.IsDBNull(dr_Shift["v_ShiftFrom"]) ? "" : (string)dr_Shift["v_ShiftFrom"];
                employee.ShiftOutTime = Convert.IsDBNull(dr_Shift["v_ShiftTo"]) ? "" : (string)dr_Shift["v_ShiftTo"];
                ShiftList.Add(employee);
            }
            return ShiftList;
        }

        public string fn_GetGradeName(int _GradeId)
        {
            _Connection = Con.fn_Connection();
            string _scmd = "select v_GradeName from paym_Grade where pn_GradeID= " + _GradeId + " and status='Y'";
            SqlCommand _Cmd = new SqlCommand(_scmd, _Connection);
            _Connection.Open();
            SqlDataReader dr_Category = _Cmd.ExecuteReader();
            Employee employee = new Employee();
            while (dr_Category.Read())
            {
                employee.CategoryName = Convert.IsDBNull(dr_Category["v_GradeName"]) ? "" : (string)dr_Category["v_GradeName"];
            }
            return employee.CategoryName;
        }

        public string fn_GetJobStatusName(int _JobStatusId)
        {
            _Connection = Con.fn_Connection();
            string _scmd = "select v_JobStatusName from paym_JobStatus where pn_JobStatusID= " + _JobStatusId + " and status='Y'";
            SqlCommand _Cmd = new SqlCommand(_scmd, _Connection);
            _Connection.Open();
            SqlDataReader dr_Category = _Cmd.ExecuteReader();
            Employee employee = new Employee();
            while (dr_Category.Read())
            {
                employee.CategoryName = Convert.IsDBNull(dr_Category["v_JobStatusName"]) ? "" : (string)dr_Category["v_JobStatusName"];
            }
            return employee.CategoryName;
        }

        public string fn_ProjectsiteName(int _siteId)
        {
            _Connection = Con.fn_Connection();
            string _scmd = "select v_projectsiteName from paym_projectsite where pn_projectsiteID= " + _siteId + " and status='Y'";
            SqlCommand _Cmd = new SqlCommand(_scmd, _Connection);
            _Connection.Open();
            SqlDataReader dr_Category = _Cmd.ExecuteReader();
            Employee employee = new Employee();
            while (dr_Category.Read())
            {
                employee.CategoryName = Convert.IsDBNull(dr_Category["v_projectsiteName"]) ? "" : (string)dr_Category["v_projectsiteName"];
            }
            return employee.CategoryName;
        }

        public string fn_Get_SkillName(int _skillId)
        {
            _Connection = Con.fn_Connection();
            string _scmd = "select v_SkillName from hrmm_SkillsMaster where pn_SkillID= " + _skillId + " and status='Y'";
            SqlCommand _Cmd = new SqlCommand(_scmd, _Connection);
            _Connection.Open();
            SqlDataReader dr_Category = _Cmd.ExecuteReader();
            Employee employee = new Employee();
            while (dr_Category.Read())
            {
                employee.CategoryName = Convert.IsDBNull(dr_Category["v_SkillName"]) ? "" : (string)dr_Category["v_SkillName"];
            }
            return employee.CategoryName;
        }

        public string fn_Get_QualificationName(int _QualId)
        {
            _Connection = Con.fn_Connection();
            string _scmd = "select v_CourseName from hrmm_Course where pn_CourseID= " + _QualId + " and status='Y'";
            SqlCommand _Cmd = new SqlCommand(_scmd, _Connection);
            _Connection.Open();
            SqlDataReader dr_Category = _Cmd.ExecuteReader();
            Employee employee = new Employee();
            while (dr_Category.Read())
            {
                employee.CategoryName = Convert.IsDBNull(dr_Category["v_CourseName"]) ? "" : (string)dr_Category["v_CourseName"];
            }
            return employee.CategoryName;
        }

        public void fn_Update_Department(Employee e)
        {

            _Connection = Con.fn_Connection();

            _Connection.Open();

            SqlCommand _RSC_can = new SqlCommand("update paym_Department set v_DepartmentName='" + e.DepartmentName + "' where pn_DepartmentID=" + e.DepartmentId + "", _Connection);

            _RSC_can.ExecuteNonQuery();

            _Connection.Close();

        }

        public string fn_Update_course(Employee e)
        {
            try
            {
                _Connection = Con.fn_Connection();

                _Connection.Open();

                SqlCommand _RSC_can = new SqlCommand("update hrmm_Course set v_courseName='" + e.CourseName + "' where pn_courseID=" + e.CourseId + "", _Connection);

                _RSC_can.ExecuteNonQuery();

                _Connection.Close();

                return "0";
            }
            catch (Exception ex)
            {
                return "1";
            }

        }

        public void fn_Update_skill(Employee e)
        {
            
            _Connection = Con.fn_Connection();

            _Connection.Open();

            SqlCommand _RSC_can = new SqlCommand("update hrmm_SkillsMaster set v_skillName='" + e.SkillName + "' where pn_skillID=" + e.SkillId + "", _Connection);

            _RSC_can.ExecuteNonQuery();
            _Connection.Close();
        }

        public void fn_Update_Specialization(Employee e)
        {
            _Connection = Con.fn_Connection();
            _Connection.Open();
            SqlCommand cmd = new SqlCommand("update hrmm_Specialization set v_SpecializationName='" + e.specializationName + "' where pn_SpecializationId=" +e.specializationID + "",_Connection);
            cmd.ExecuteNonQuery();
            _Connection.Close();
        }

        public void fn_Update_projectsite(Employee e)
        {


            _Connection = Con.fn_Connection();

            _Connection.Open();

            SqlCommand _RSC_can = new SqlCommand("update paym_projectsite set v_projectsiteName='" + e.ProjectsiteName + "' where pn_projectsiteID=" + e.ProjectsiteId + "", _Connection);

            _RSC_can.ExecuteNonQuery();

            _Connection.Close();


        }

        public void fn_Update_Division(Employee e)
        {


            _Connection = Con.fn_Connection();

            _Connection.Open();

            SqlCommand _RSC_can = new SqlCommand("update paym_Division  set v_DivisionName='" + e.DivisionName + "' where pn_DivisionID=" + e.DivisionId + "", _Connection);

            _RSC_can.ExecuteNonQuery();

            _Connection.Close();


        }

        public void fn_Update_Level(Employee e)
        {


            _Connection = Con.fn_Connection();

            _Connection.Open();

            SqlCommand _RSC_can = new SqlCommand("update paym_Level set v_LevelName='" + e.LevelName + "' where pn_LevelID=" + e.LevelId + "", _Connection);

            _RSC_can.ExecuteNonQuery();

            _Connection.Close();


        }

        public void fn_Update_Designation(Employee e)
        {


            _Connection = Con.fn_Connection();

            _Connection.Open();

            SqlCommand _RSC_can = new SqlCommand("update paym_Designation set v_DesignationName='" + e.DesignationName + "' where pn_DesignationID=" + e.DesignationId + "", _Connection);

            _RSC_can.ExecuteNonQuery();

            _Connection.Close();


        }

        public void fn_Update_Grade(Employee e)
        {


            _Connection = Con.fn_Connection();

            _Connection.Open();

            SqlCommand _RSC_can = new SqlCommand("update paym_Grade set v_GradeName='" + e.GradeName + "' where pn_GradeID=" + e.GradeId + "", _Connection);

            _RSC_can.ExecuteNonQuery();

            _Connection.Close();


        }

        public void fn_Update_Category(Employee e)
        {


            _Connection = Con.fn_Connection();

            _Connection.Open();

            SqlCommand _RSC_can = new SqlCommand("update paym_Category set v_CategoryName='" + e.CategoryName + "' where pn_CategoryID=" + e.CategoryId + "", _Connection);

            _RSC_can.ExecuteNonQuery();

            _Connection.Close();


        }

        public void fn_Update_JobStatus(Employee e)
        {


            _Connection = Con.fn_Connection();

            _Connection.Open();

            SqlCommand _RSC_can = new SqlCommand("update paym_JobStatus set v_JobStatusName='" + e.JobStatusName + "' where pn_JobStatusID=" + e.JobStatusId + "", _Connection);

            _RSC_can.ExecuteNonQuery();

            _Connection.Close();


        }

        public void fn_Update_Shift(Employee e)
        {


            _Connection = Con.fn_Connection();

            _Connection.Open();

            SqlCommand _RSC_can = new SqlCommand("update paym_Shift set v_ShiftName ='" + e.ShiftName + "',v_ShiftFrom ='" + e.ShiftFrom + "',v_ShiftTo ='" + e.ShiftTo + "' where pn_ShiftID=" + e.ShiftId + "", _Connection);

            _RSC_can.ExecuteNonQuery();

            _Connection.Close();


        }

        public void fn_Delete_Emp_Education(Employee e)
        {


            _Connection = Con.fn_Connection();

            _Connection.Open();

            SqlCommand _RSC_can = new SqlCommand("Delete from paym_Employee_Education where pn_EmployeeID=" + e.EmployeeId + " and pn_CourseID=" + e.CourseId, _Connection);

            _RSC_can.ExecuteNonQuery();

            _Connection.Close();


        }

        public void fn_Delete_Emp_skill(Employee e)
        {


            _Connection = Con.fn_Connection();

            _Connection.Open();

            SqlCommand _RSC_can = new SqlCommand("Delete from paym_Employee_Skills where pn_EmployeeID=" + e.EmployeeId + " and pn_SkillID=" + e.SkillId, _Connection);

            _RSC_can.ExecuteNonQuery();

            _Connection.Close();


        }

        public void fn_temp_table(string tq)
        {


            _Connection = Con.fn_Connection();

            _Connection.Open();

            SqlCommand _RSC_can = new SqlCommand(tq, _Connection);

            _RSC_can.ExecuteNonQuery();

            _Connection.Close();


        }

        public string date_join(string dt, string mnt, string yr)
        {
            string str_date;

            str_date = dt + "/" + mnt + "/" + yr;

            return str_date;

        }

        public int ddl_check(DropDownList ddl)
        {
            int i_ddl;

            if (ddl.Enabled == true)
            {
                i_ddl = Convert.ToInt32(ddl.SelectedItem.Value);
            }
            else
            {
                i_ddl = 1;
            }
            return i_ddl;

        }

        public int Check_Count(string EmployeeID, string date)
        {
            int i_ddl;
            var myDate = Convert.ToDateTime(date);
            var startOfMonth = new DateTime(myDate.Year, myDate.Month, 1);
            var endOfMonth = startOfMonth.AddMonths(1).AddDays(-1);
            _Connection = Con.fn_Connection();
            _Connection.Open();
            SqlCommand _RSC_can = new SqlCommand("set dateformat dmy;select count(*) from paym_permission where EmployeeID = '" + EmployeeID + "' and date between '" + startOfMonth + "' and '" + endOfMonth + "';set dateformat mdy;", _Connection);
            i_ddl = (int)_RSC_can.ExecuteScalar();
            _Connection.Close();
            return i_ddl;
        }

        public string select_Query(CheckBoxList chk_select)
        {
            int i, k = 0;
            string str;

            string s_query = "";

            for (i = 0; i < chk_select.Items.Count; i++)
            {

                if (chk_select.Items[i].Selected)
                {
                    str = chk_select.Items[i].Value;

                    if (str != "*")
                    {

                        if (k == 0)
                        {
                            s_query = s_query + str;
                            k = k + 1;
                        }
                        else
                        {
                            s_query = s_query + "," + str;
                        }
                    }
                    else
                    {
                        s_query = s_query + str;
                        break;

                    }
                }
            }
            return s_query;
        }

        public string Where_Masters_Query(CheckBoxList chk_Master, string str_M_where)
        {
            int i, k = 0;
            string str, w_query = "";


            for (i = 0; i < chk_Master.Items.Count; i++)
            {

                if (chk_Master.Items[i].Selected)
                {
                    str = chk_Master.Items[i].Value;

                    if (k == 0)
                    {
                        w_query = w_query + str_M_where + " in (" + str;
                        k = k + 1;
                    }
                    else
                    {
                        w_query = w_query + "," + str;
                    }
                }
            }

            return w_query;
        }


        //$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$ Training $$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$

        public string Employee_Training(Employee e)
        {
            try
            {
                _Connection = Con.fn_Connection();


                SqlCommand _Cmd = new SqlCommand("sp_paym_Training_New", _Connection);
                _Cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter[] _ISPParamter = new SqlParameter[12];

                _ISPParamter[0] = new SqlParameter("@pn_CompanyID", SqlDbType.Int);
                _ISPParamter[0].Value = e.CompanyId;
                _ISPParamter[1] = new SqlParameter("@pn_BranchID", SqlDbType.Int);
                _ISPParamter[1].Value = e.BranchId;
                _ISPParamter[2] = new SqlParameter("@pn_EmployeeID", SqlDbType.Int);
                _ISPParamter[2].Value = e.EmployeeId;
                _ISPParamter[3] = new SqlParameter("@TrainingID", SqlDbType.Int);
                _ISPParamter[3].Value = e.TrainingID;
                _ISPParamter[4] = new SqlParameter("@v_DurationFrom", SqlDbType.DateTime);
                _ISPParamter[4].Value = e.DurationFrom;
                _ISPParamter[5] = new SqlParameter("@v_DurationTo", SqlDbType.DateTime);
                _ISPParamter[5].Value = e.DurationTo;
                _ISPParamter[6] = new SqlParameter("@instid", SqlDbType.Int);
                _ISPParamter[6].Value = e.InstitutionId;
                _ISPParamter[7] = new SqlParameter("@TrainingCost", SqlDbType.VarChar);
                _ISPParamter[7].Value = e.prgmtypName;
                _ISPParamter[8] = new SqlParameter("@programname", SqlDbType.VarChar);
                _ISPParamter[8].Value = e.prgmname;
                _ISPParamter[9] = new SqlParameter("@trainerid", SqlDbType.Int);
                _ISPParamter[9].Value = e.trnrID;
                _ISPParamter[10] = new SqlParameter("@v_summary", SqlDbType.VarChar);
                _ISPParamter[10].Value = e.temp_str;

                _ISPParamter[11] = new SqlParameter("@TrainingHrs", SqlDbType.VarChar);
                _ISPParamter[11].Value = e.IDno;

                for (int i = 0; i < _ISPParamter.Length; i++)
                {
                    _Cmd.Parameters.Add(_ISPParamter[i]);
                }
                _Connection.Open();
                _Cmd.ExecuteNonQuery();
                _Connection.Close();
                return "0";
            }
            catch (SqlException Ex)
            {
                return "1";
            }
        }

        public string Employee_Training1(Employee e)
        {
            try
            {
                _Connection = Con.fn_Connection();


                SqlCommand _Cmd = new SqlCommand("sp_Update_paym_Training_New", _Connection);
                _Cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter[] _ISPParamter = new SqlParameter[11];

                _ISPParamter[0] = new SqlParameter("@pn_CompanyID", SqlDbType.Int);
                _ISPParamter[0].Value = e.CompanyId;
                _ISPParamter[1] = new SqlParameter("@pn_BranchID", SqlDbType.Int);
                _ISPParamter[1].Value = e.BranchId;
                //_ISPParamter[2] = new SqlParameter("@pn_EmployeeID", SqlDbType.Int);
                //_ISPParamter[2].Value = e.EmployeeId;
                _ISPParamter[2] = new SqlParameter("@TrainingID", SqlDbType.Int);
                _ISPParamter[2].Value = e.TrainingID;
                _ISPParamter[3] = new SqlParameter("@v_DurationFrom", SqlDbType.DateTime);
                _ISPParamter[3].Value = e.DurationFrom;
                _ISPParamter[4] = new SqlParameter("@v_DurationTo", SqlDbType.DateTime);
                _ISPParamter[4].Value = e.DurationTo;
                _ISPParamter[5] = new SqlParameter("@instid", SqlDbType.Int);
                _ISPParamter[5].Value = e.InstitutionId;
                _ISPParamter[6] = new SqlParameter("@TrainingCost", SqlDbType.VarChar);
                _ISPParamter[6].Value = e.prgmtypName;
                _ISPParamter[7] = new SqlParameter("@programname", SqlDbType.VarChar);
                _ISPParamter[7].Value = e.prgmname;
                _ISPParamter[8] = new SqlParameter("@trainerid", SqlDbType.Int);
                _ISPParamter[8].Value = e.trnrID;
                _ISPParamter[9] = new SqlParameter("@v_summary", SqlDbType.VarChar);
                _ISPParamter[9].Value = e.temp_str;
                _ISPParamter[10] = new SqlParameter("@TrainingHrs", SqlDbType.VarChar);
                _ISPParamter[10].Value = e.IDno;

                for (int i = 0; i < _ISPParamter.Length; i++)
                {
                    _Cmd.Parameters.Add(_ISPParamter[i]);
                }
                _Connection.Open();
                _Cmd.ExecuteNonQuery();
                _Connection.Close();
                return "0";
            }
            catch (SqlException Ex)
            {
                return "1";
            }
        }

        public Collection<Employee> fn_gettrainer(Employee e)
        {
            Collection<Employee> DepartmentList = new Collection<Employee>();
            _Connection = Con.fn_Connection();
            string _SqlString = "Select * from trainer_profile1 where id='" + e.InstitutionId + "' and pn_branchid='" + e.BranchId + "'";
            SqlCommand _SSDepartment = new SqlCommand(_SqlString, _Connection);
            _Connection.Open();
            SqlDataReader dr_Department = _SSDepartment.ExecuteReader();
            while (dr_Department.Read())
            {
                Employee employee = new Employee();
                employee.DepartmentId = (int)dr_Department["trainer_id"];
                employee.DepartmentName = Convert.IsDBNull(dr_Department["fname"]) ? "" : (string)dr_Department["fname"];
                DepartmentList.Add(employee);
            }
            return DepartmentList;
        }

        public string InstitutionUpdate(Employee e)
        {
            try
            {
                _Connection = Con.fn_Connection();


                SqlCommand _Cmd = new SqlCommand("sp_paym_Institution", _Connection);
                _Cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter[] _ISPParamter = new SqlParameter[12];

                _ISPParamter[0] = new SqlParameter("@pn_CompanyID", SqlDbType.VarChar);
                _ISPParamter[0].Value = e.CompanyId;
                _ISPParamter[1] = new SqlParameter("@pn_trninstID", SqlDbType.Int);
                _ISPParamter[1].Value = e.InstitutionId;
                _ISPParamter[2] = new SqlParameter("@v_trninstName", SqlDbType.VarChar);
                _ISPParamter[2].Value = e.InstitutionName;
                _ISPParamter[3] = new SqlParameter("@v_trninstadd1", SqlDbType.VarChar);
                _ISPParamter[3].Value = e.P_AddressLine1;
                _ISPParamter[4] = new SqlParameter("@v_trninstadd2", SqlDbType.VarChar);
                _ISPParamter[4].Value = e.P_AddressLine2;
                _ISPParamter[5] = new SqlParameter("@v_trninstCity", SqlDbType.VarChar);
                _ISPParamter[5].Value = e.P_City;
                _ISPParamter[6] = new SqlParameter("@v_trninstState", SqlDbType.VarChar);
                _ISPParamter[6].Value = e.P_State;
                _ISPParamter[7] = new SqlParameter("@v_trninstCountry", SqlDbType.VarChar);
                _ISPParamter[7].Value = e.P_Country;
                _ISPParamter[8] = new SqlParameter("@v_trninstphone", SqlDbType.VarChar);
                _ISPParamter[8].Value = e.ph_Residence;
                _ISPParamter[9] = new SqlParameter("@v_trninstmail", SqlDbType.VarChar);
                _ISPParamter[9].Value = e.EmailId;
                _ISPParamter[10] = new SqlParameter("@v_trninstweb", SqlDbType.VarChar);
                _ISPParamter[10].Value = e.websitename;
                _ISPParamter[11] = new SqlParameter("@status", SqlDbType.VarChar);
                _ISPParamter[11].Value = e.status;
                for (int i = 0; i < _ISPParamter.Length; i++)
                {
                    _Cmd.Parameters.Add(_ISPParamter[i]);
                }
                _Connection.Open();
                _Cmd.ExecuteNonQuery();
                _Connection.Close();
                return "0";
            }
            catch (SqlException Ex)
            {
                return "1";
            }
        }

        public string TrainerUpdate(Employee e)
        {
            try
            {
                _Connection = Con.fn_Connection();


                SqlCommand _Cmd = new SqlCommand("sp_paym_trainer", _Connection);
                _Cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter[] _ISPParamter = new SqlParameter[7];

                _ISPParamter[0] = new SqlParameter("@pn_CompanyID", SqlDbType.VarChar);
                _ISPParamter[0].Value = e.CompanyId;
                _ISPParamter[1] = new SqlParameter("@pn_pgmtrnrNameID", SqlDbType.Int);
                _ISPParamter[1].Value = e.trnrID;
                _ISPParamter[2] = new SqlParameter("@v_pgmtrnrName", SqlDbType.VarChar);
                _ISPParamter[2].Value = e.trnrName;
                _ISPParamter[3] = new SqlParameter("@v_pgmtrnrSkill", SqlDbType.VarChar);
                _ISPParamter[3].Value = e.trnrSkill;
                _ISPParamter[4] = new SqlParameter("@v_pgmtrnrExpr", SqlDbType.VarChar);
                _ISPParamter[4].Value = e.trnrExp;
                _ISPParamter[5] = new SqlParameter("@v_pgmtrnrWorktype", SqlDbType.VarChar);
                _ISPParamter[5].Value = e.trnrWorkType;
                _ISPParamter[6] = new SqlParameter("@status", SqlDbType.VarChar);
                _ISPParamter[6].Value = e.status;
                for (int i = 0; i < _ISPParamter.Length; i++)
                {
                    _Cmd.Parameters.Add(_ISPParamter[i]);
                }
                _Connection.Open();
                _Cmd.ExecuteNonQuery();
                _Connection.Close();
                return "0";
            }
            catch (SqlException Ex)
            {
                return "1";
            }
        }

        public string programUpdate(Employee e)
        {
            try
            {
                _Connection = Con.fn_Connection();


                SqlCommand _Cmd = new SqlCommand("sp_paym_programname", _Connection);
                _Cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter[] _ISPParamter = new SqlParameter[4];

                _ISPParamter[0] = new SqlParameter("@pn_CompanyID", SqlDbType.Int);
                _ISPParamter[0].Value = e.CompanyId;
                _ISPParamter[1] = new SqlParameter("@pn_pgrmNameID", SqlDbType.Int);
                _ISPParamter[1].Value = e.prgmid;
                _ISPParamter[2] = new SqlParameter("@v_pgrmName", SqlDbType.VarChar);
                _ISPParamter[2].Value = e.prgmname;
                _ISPParamter[3] = new SqlParameter("@status", SqlDbType.VarChar);
                _ISPParamter[3].Value = e.status;

                for (int i = 0; i < _ISPParamter.Length; i++)
                {
                    _Cmd.Parameters.Add(_ISPParamter[i]);
                }
                _Connection.Open();
                _Cmd.ExecuteNonQuery();
                _Connection.Close();
                return "0";
            }
            catch (SqlException Ex)
            {
                return "1";
            }
        }

        public string programtypeUpdate(Employee e)
        {
            try
            {
                _Connection = Con.fn_Connection();


                SqlCommand _Cmd = new SqlCommand("sp_paym_programtype", _Connection);
                _Cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter[] _ISPParamter = new SqlParameter[4];

                _ISPParamter[0] = new SqlParameter("@pn_CompanyID", SqlDbType.Int);
                _ISPParamter[0].Value = e.CompanyId;
                _ISPParamter[1] = new SqlParameter("@pn_pgrmtypeID", SqlDbType.Int);
                _ISPParamter[1].Value = e.prgmtypId;
                _ISPParamter[2] = new SqlParameter("@v_pgrmtypeName", SqlDbType.VarChar);
                _ISPParamter[2].Value = e.prgmtypName;
                _ISPParamter[3] = new SqlParameter("@status", SqlDbType.VarChar);
                _ISPParamter[3].Value = e.status;

                for (int i = 0; i < _ISPParamter.Length; i++)
                {
                    _Cmd.Parameters.Add(_ISPParamter[i]);
                }
                _Connection.Open();
                _Cmd.ExecuteNonQuery();
                _Connection.Close();
                return "0";
            }
            catch (SqlException Ex)
            {
                return "1";
            }
        }

        public string Compliant_Box(Employee e)
        {

            try
            {
                _Connection = Con.fn_Connection();


                SqlCommand _Cmd = new SqlCommand("sp_compliant_Box", _Connection);
                _Cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter[] _ISPParamter = new SqlParameter[7];

                _ISPParamter[0] = new SqlParameter("@pn_CompanyID", SqlDbType.Int);
                _ISPParamter[0].Value = e.CompanyId;
                _ISPParamter[1] = new SqlParameter("@pn_BranchID", SqlDbType.Int);
                _ISPParamter[1].Value = e.BranchId;
                _ISPParamter[2] = new SqlParameter("@pn_EmployeeID", SqlDbType.VarChar);
                _ISPParamter[2].Value = e.EmployeeCode;
                _ISPParamter[3] = new SqlParameter("@Compliant_Subject", SqlDbType.VarChar);
                _ISPParamter[3].Value = e.Compliant_Subject;
                _ISPParamter[4] = new SqlParameter("@Compliant_Text", SqlDbType.VarChar);
                _ISPParamter[4].Value = e.Compliant_Text;

                _ISPParamter[5] = new SqlParameter("@status", SqlDbType.VarChar);
                _ISPParamter[5].Value = e.Status2;
                _ISPParamter[6] = new SqlParameter("@Id", SqlDbType.Int);
                _ISPParamter[6].Value = e.Id;

                for (int i = 0; i < _ISPParamter.Length; i++)
                {
                    _Cmd.Parameters.Add(_ISPParamter[i]);
                }
                _Connection.Open();
                _Cmd.ExecuteNonQuery();
                _Connection.Close();
                return "0";
            }
            catch (SqlException Ex)
            {
                return "1";
            }
        }

        public string _Announcements(Employee a)
        {
            try
            {
                _Connection = Con.fn_Connection();
                SqlCommand _Cmd = new SqlCommand("sp_announcements", _Connection);
                _Cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter[] _ISPAnnouncement = new SqlParameter[6];
                _ISPAnnouncement[0] = new SqlParameter("@pn_companyID", SqlDbType.Int);
                _ISPAnnouncement[0].Value = a.CompanyId;
                _ISPAnnouncement[1] = new SqlParameter("@pn_BranchID", SqlDbType.Int);
                _ISPAnnouncement[1].Value = a.BranchId;
                _ISPAnnouncement[2] = new SqlParameter("@announcementid", SqlDbType.Int);
                _ISPAnnouncement[2].Value = a._Announcement_id1;
                _ISPAnnouncement[3] = new SqlParameter("@Date", SqlDbType.DateTime);
                _ISPAnnouncement[3].Value = a.Date;
                _ISPAnnouncement[4] = new SqlParameter("@Subject", SqlDbType.VarChar);
                _ISPAnnouncement[4].Value = a.Subject;
                _ISPAnnouncement[5] = new SqlParameter("@Information", SqlDbType.VarChar);
                _ISPAnnouncement[5].Value = a.Announcement;
                for (int i = 0; i < _ISPAnnouncement.Length; i++)
                {
                    _Cmd.Parameters.Add(_ISPAnnouncement[i]);
                }
                _Connection.Open();
                _Cmd.ExecuteNonQuery();
                _Connection.Close();
                return "0";

            }
            catch (Exception ex)
            {
                return "1";
            }
        }

        public string programnameUpdate(Employee e)
        {
            try
            {
                _Connection = Con.fn_Connection();


                SqlCommand _Cmd = new SqlCommand("sp_paym_programname", _Connection);
                _Cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter[] _ISPParamter = new SqlParameter[4];

                _ISPParamter[0] = new SqlParameter("@pn_CompanyID", SqlDbType.Int);
                _ISPParamter[0].Value = e.CompanyId;
                _ISPParamter[1] = new SqlParameter("@pn_pgrmNameID", SqlDbType.Int);
                _ISPParamter[1].Value = e.prgmid;
                _ISPParamter[2] = new SqlParameter("@v_pgrmName", SqlDbType.VarChar);
                _ISPParamter[2].Value = e.prgmname;
                _ISPParamter[3] = new SqlParameter("@status", SqlDbType.VarChar);
                _ISPParamter[3].Value = e.status;

                for (int i = 0; i < _ISPParamter.Length; i++)
                {
                    _Cmd.Parameters.Add(_ISPParamter[i]);
                }
                _Connection.Open();
                _Cmd.ExecuteNonQuery();
                _Connection.Close();
                return "0";
            }
            catch (SqlException Ex)
            {
                return "1";
            }
        }

        public Collection<Employee> fn_getInstList(Employee e)
        {
            Collection<Employee> employeeList = new Collection<Employee>();
            _Connection = Con.fn_Connection();
            string _SqlString = "select * from institution_profile where  pn_companyid='"+e.CompanyId+"' and pn_branchid='"+e.BranchId+"' and status='Y'";
            SqlCommand _SSemployee = new SqlCommand(_SqlString, _Connection);
            _Connection.Open();
            SqlDataReader dr_employee = _SSemployee.ExecuteReader();
            while (dr_employee.Read())
            {
                Employee employee = new Employee();

                employee.InstitutionId = (int)dr_employee["id"];
                employee.InstitutionName = Convert.IsDBNull(dr_employee["ins_name"]) ? "" : (string)dr_employee["ins_name"];
                employeeList.Add(employee);
            }
            return employeeList;
        }

        public Collection<Employee> fn_getInstList1()
        {
            Collection<Employee> employeeList = new Collection<Employee>();
            _Connection = Con.fn_Connection();
            string _SqlString = "select * from paym_trninst where status='Y'";
            SqlCommand _SSemployee = new SqlCommand(_SqlString, _Connection);
            _Connection.Open();
            SqlDataReader dr_employee = _SSemployee.ExecuteReader();
            while (dr_employee.Read())
            {
                Employee employee = new Employee();

                employee.InstitutionId = (int)dr_employee["pn_trninstID"];
                employee.InstitutionName = Convert.IsDBNull(dr_employee["v_trninstName"]) ? "" : (string)dr_employee["v_trninstName"];
                employeeList.Add(employee);
                employee.BranchId = (int)dr_employee["BranchID"];
            }
            return employeeList;
        }

        public Collection<Employee> fn_getInstName(int Inid)
        {
            Collection<Employee> InstituteList = new Collection<Employee>();
            _Connection = Con.fn_Connection();
            string _SqlString = "select * from paym_trninst where pn_trninstID = " + Inid + " and status='Y'";
            SqlCommand _SSEmployee = new SqlCommand(_SqlString, _Connection);
            _Connection.Open();
            SqlDataReader dr_Employee = _SSEmployee.ExecuteReader();
            while (dr_Employee.Read())
            {
                Employee employee = new Employee();
                employee.CompanyId = (int)dr_Employee["pn_CompanyID"];
                employee.InstitutionId = (int)dr_Employee["pn_trninstID"];
        

                employee.InstitutionName = Convert.IsDBNull(dr_Employee["v_trninstName"]) ? "" : (string)dr_Employee["v_trninstName"];
                employee.P_AddressLine1 = Convert.IsDBNull(dr_Employee["v_trninstadd1"]) ? "" : (string)dr_Employee["v_trninstadd1"];
                employee.P_AddressLine2 = Convert.IsDBNull(dr_Employee["v_trninstadd2"]) ? "" : (string)dr_Employee["v_trninstadd2"];
                employee.P_City = Convert.IsDBNull(dr_Employee["v_trninstcity"]) ? "" : (string)dr_Employee["v_trninstcity"];
                employee.P_State = Convert.IsDBNull(dr_Employee["v_trninstState"]) ? "" : (string)dr_Employee["v_trninstState"];
                employee.P_Country = Convert.IsDBNull(dr_Employee["v_trninstCountry"]) ? "" : (string)dr_Employee["v_trninstCountry"];
                employee.ph_Residence = Convert.IsDBNull(dr_Employee["V_trninstphone"]) ? "" : (string)dr_Employee["V_trninstphone"];
                employee.EmailId = Convert.IsDBNull(dr_Employee["v_trninstmail"]) ? "" : (string)dr_Employee["v_trninstmail"];
                employee.websitename = Convert.IsDBNull(dr_Employee["v_trninstweb"]) ? "" : (string)dr_Employee["v_trninstweb"];
                InstituteList.Add(employee);
            }
            return InstituteList;
        }

        public Collection<Employee> fn_gettrainerNameList()
        {
            Collection<Employee> TrainerNameList = new Collection<Employee>();
            _Connection = Con.fn_Connection();
            string _SqlString = "select * from paym_instpgmtrnr where status='Y'";
            SqlCommand _SSEmployee = new SqlCommand(_SqlString, _Connection);
            _Connection.Open();
            SqlDataReader dr_Employee = _SSEmployee.ExecuteReader();
            while (dr_Employee.Read())
            {
                Employee employee = new Employee();
                employee.trnrID = (int)dr_Employee["pn_pgmtrnrNameID"];
                employee.trnrName = Convert.IsDBNull(dr_Employee["v_pgmtrnrName"]) ? "" : (string)dr_Employee["v_pgmtrnrName"];
                TrainerNameList.Add(employee);
            }
            return TrainerNameList;
        }

        public Collection<Employee> fn_gettrainerNameList1(Employee e)
        {
            Collection<Employee> TrainerNameList = new Collection<Employee>();
            _Connection = Con.fn_Connection();
            string _SqlString = "select * from trainer_profile1 where pn_BranchID='" + e.BranchId + "' and pn_companyid = '"+e.CompanyId+"'";
            SqlCommand _SSEmployee = new SqlCommand(_SqlString, _Connection);
            _Connection.Open();
            SqlDataReader dr_Employee = _SSEmployee.ExecuteReader();
            while (dr_Employee.Read())
            {
                Employee employee = new Employee();
                employee.trnrID = (int)dr_Employee["trainer_id"];
                employee.trnrName = Convert.IsDBNull(dr_Employee["fname"]) ? "" : (string)dr_Employee["fname"];
                employee.BranchId = (int)dr_Employee["pn_branchid"];
                TrainerNameList.Add(employee);
            }
            return TrainerNameList;
        }

        public Collection<Employee> fn_gettrainerName(int Inid)
        {
            Collection<Employee> TrainerList = new Collection<Employee>();
            _Connection = Con.fn_Connection();
            string _SqlString = "select * from paym_instpgmtrnr where pn_pgmtrnrNameID = " + Inid + " and status='Y'";
            SqlCommand _SSEmployee = new SqlCommand(_SqlString, _Connection);
            _Connection.Open();
            SqlDataReader dr_Employee = _SSEmployee.ExecuteReader();
            while (dr_Employee.Read())
            {
                Employee employee = new Employee();
                employee.CompanyId = (int)dr_Employee["pn_CompanyID"];
                employee.trnrID = (int)dr_Employee["pn_pgmtrnrNameID"];
                employee.trnrName = Convert.IsDBNull(dr_Employee["v_pgmtrnrName"]) ? "" : (string)dr_Employee["v_pgmtrnrName"];
                employee.trnrSkill = Convert.IsDBNull(dr_Employee["v_pgmtrnrSkill"]) ? "" : (string)dr_Employee["v_pgmtrnrSkill"];
                employee.trnrExp = Convert.IsDBNull(dr_Employee["v_pgmtrnrExpr"]) ? "" : (string)dr_Employee["v_pgmtrnrExpr"];
                employee.trnrWorkType = Convert.IsDBNull(dr_Employee["v_pgmtrnrWorktype"]) ? "" : (string)dr_Employee["v_pgmtrnrWorktype"];
                TrainerList.Add(employee);
            }
            return TrainerList;
        }

        public Collection<Employee> fn_programtypes(Employee e)
        {
            Collection<Employee> programtypeList = new Collection<Employee>();
            _Connection = Con.fn_Connection();
            string _SqlString = "select distinct ptype from trainer_profile1 where pn_companyid = '"+e.CompanyId+"' and pn_branchid = '"+e.BranchId+"'";
            SqlCommand _SSprogramtype = new SqlCommand(_SqlString, _Connection);
            _Connection.Open();
            SqlDataReader dr_programtype = _SSprogramtype.ExecuteReader();
            while (dr_programtype.Read())
            {
                Employee employee = new Employee();
               // employee.prgmtypId = (int)dr_programtype["pn_pgrmtypeID"];
                employee.prgmtypName = Convert.IsDBNull(dr_programtype["ptype"]) ? "" : (string)dr_programtype["ptype"];
                programtypeList.Add(employee);
            }
            return programtypeList;
        }

        public Collection<Employee> fn_programtype()
        {
            Collection<Employee> programtypeList = new Collection<Employee>();
            _Connection = Con.fn_Connection();
            string _SqlString = "select * from paym_instpgmtype where status='Y'";
            SqlCommand _SSprogramtype = new SqlCommand(_SqlString, _Connection);
            _Connection.Open();
            SqlDataReader dr_programtype = _SSprogramtype.ExecuteReader();
            while (dr_programtype.Read())
            {
                Employee employee = new Employee();
                employee.prgmtypId = (int)dr_programtype["pn_pgrmtypeID"];
                employee.prgmtypName = Convert.IsDBNull(dr_programtype["v_pgrmtypeName"]) ? "" : (string)dr_programtype["v_pgrmtypeName"];
                programtypeList.Add(employee);
            }
            return programtypeList;
        }

        public Collection<Employee> fn_programtype1(int proid)
        {
            Collection<Employee> programtypeList = new Collection<Employee>();
            _Connection = Con.fn_Connection();
            string _SqlString = "select * from paym_instpgmtype where status='Y' and BranchID='" + proid + "'";
            SqlCommand _SSprogramtype = new SqlCommand(_SqlString, _Connection);
            _Connection.Open();
            SqlDataReader dr_programtype = _SSprogramtype.ExecuteReader();
            while (dr_programtype.Read())
            {
                Employee employee = new Employee();
                employee.prgmtypId = (int)dr_programtype["pn_pgrmtypeID"];
                employee.prgmtypName = Convert.IsDBNull(dr_programtype["v_pgrmtypeName"]) ? "" : (string)dr_programtype["v_pgrmtypeName"];
                employee.BranchId = (int)dr_programtype["BranchID"];
                programtypeList.Add(employee);
            }
            return programtypeList;
        }

        public Collection<Employee> fn_programname(Employee e)
        {
            Collection<Employee> programnameList = new Collection<Employee>();
            _Connection = Con.fn_Connection();
            string _SqlString = "select distinct specification from trainer_profile1 where pn_companyid='"+e.CompanyId+"' and pn_branchid='"+e.BranchId+"'";
            SqlCommand _SSprogramname = new SqlCommand(_SqlString, _Connection);
            _Connection.Open();
            SqlDataReader dr_programname = _SSprogramname.ExecuteReader();
            while (dr_programname.Read())
            {
                Employee employee = new Employee();
                //employee.prgmid = (int)dr_programname["pn_pgrmNameID"];
                employee.prgmname = Convert.IsDBNull(dr_programname["specification"]) ? "" : (string)dr_programname["specification"];
                programnameList.Add(employee);
            }
            return programnameList;
        }

        public Collection<Employee> fn_programname1(int type)
        {
            Collection<Employee> programnameList = new Collection<Employee>();
            _Connection = Con.fn_Connection();
            string _SqlString = "select * from paym_instpgmname where status='Y' and BranchID='" + type + "'";
            SqlCommand _SSprogramname = new SqlCommand(_SqlString, _Connection);
            _Connection.Open();
            SqlDataReader dr_programname = _SSprogramname.ExecuteReader();
            while (dr_programname.Read())
            {
                Employee employee = new Employee();
                employee.prgmid = (int)dr_programname["pn_pgrmNameID"];
                employee.prgmname = Convert.IsDBNull(dr_programname["v_pgrmName"]) ? "" : (string)dr_programname["v_pgrmName"];
                employee.BranchId = (int)dr_programname["BranchID"];
                programnameList.Add(employee);
            }
            return programnameList;
        }

        //public Collection<Employee> fn_Training_grid(Employee e)
        //{
        //    Collection<Employee> InstitutionList = new Collection<Employee>();
        //    _Connection = Con.fn_Connection();
        //    string _Sqlstring = "select t.TrainingID,t.v_DurationFrom,t.v_DurationTo,t.Rating,ti.ins_name,tpt.ptype,tpt.fname,tpt.specification";

        //    _Sqlstring += " from paym_training_New t,institution_profile ti,trainer_profile1 tpt";
        //    _Sqlstring += " where t.pn_EmployeeID=" + e.EmployeeId + " and t.pn_CompanyID=" + e.CompanyId + "";
        //    _Sqlstring += " and ti.ID=t.InstID and tpt.specification=t.ProgramName";

        //    SqlCommand _SSInstitution = new SqlCommand(_Sqlstring, _Connection);
        //    _Connection.Open();
        //    SqlDataReader dr_Institution = _SSInstitution.ExecuteReader();
        //    while (dr_Institution.Read())
        //    {
        //        Employee employee = new Employee();
        //        employee.TrainingID = (int)dr_Institution["TrainingID"];
        //        employee.DurationFrom = Convert.IsDBNull(dr_Institution["v_DurationFrom"]) ? "" : (string)dr_Institution["v_DurationFrom"];
        //        employee.DurationTo = Convert.IsDBNull(dr_Institution["v_DurationTo"]) ? "" : (string)dr_Institution["v_DurationTo"];
        //        employee.Rating = Convert.IsDBNull(dr_Institution["Rating"]) ? 0 : (int)dr_Institution["Rating"];
        //        employee.InstitutionName = Convert.IsDBNull(dr_Institution["ins_name"]) ? "" : (string)dr_Institution["ins_name"];
        //        employee.prgmtypName = Convert.IsDBNull(dr_Institution["ptype"]) ? "" : (string)dr_Institution["ptype"];
        //        employee.trnrName = Convert.IsDBNull(dr_Institution["fName"]) ? "" : (string)dr_Institution["fName"];
        //        employee.prgmname = Convert.IsDBNull(dr_Institution["specification"]) ? "" : (string)dr_Institution["specification"];

        //        InstitutionList.Add(employee);
        //    }
        //    return InstitutionList;
        //}

        public Collection<Employee> fn_Training(Employee e)
        {
            Collection<Employee> InstitutionList = new Collection<Employee>();
            _Connection = Con.fn_Connection();
            string _Sqlstring = "select t.pn_TrainingID,t.v_summary,t.v_DurationFrom,t.v_DurationTo,ti.v_trninstName,tpt.v_pgrmtypeName,tt.v_pgmtrnrName,tpn.v_pgrmName";

            _Sqlstring += " from paym_training t,paym_trninst ti,paym_instpgmtype tpt,paym_instpgmtrnr tt,paym_instpgmname tpn";
            _Sqlstring += " where t.pn_TrainingID=" + e.TrainingID + " and t.pn_EmployeeID=" + e.EmployeeId + " and t.pn_CompanyID=" + e.CompanyId + "";
            _Sqlstring += " and ti.pn_trninstID in (select fn_trninstID from paym_training where pn_EmployeeID=" + e.EmployeeId + " and pn_TrainingID=" + e.TrainingID + ")";
            _Sqlstring += " and tpt.pn_pgrmtypeID in (select fn_pgrmtypeID from paym_training where pn_EmployeeID=" + e.EmployeeId + " and pn_TrainingID=" + e.TrainingID + ")";
            _Sqlstring += " and tt.pn_pgmtrnrNameID in (select fn_pgmtrnrNameID from paym_training where pn_EmployeeID=" + e.EmployeeId + " and pn_TrainingID=" + e.TrainingID + ")";
            _Sqlstring += " and tpn.pn_pgrmNameID in (select fn_pgrmNameID from paym_training where pn_EmployeeID=" + e.EmployeeId + " and pn_TrainingID=" + e.TrainingID + ")";

            SqlCommand _SSInstitution = new SqlCommand(_Sqlstring, _Connection);
            _Connection.Open();
            SqlDataReader dr_Institution = _SSInstitution.ExecuteReader();
            while (dr_Institution.Read())
            {
                Employee employee = new Employee();
                employee.TrainingID = (int)dr_Institution["pn_TrainingID"];
                employee.DurationFrom = Convert.IsDBNull(dr_Institution["v_DurationFrom"]) ? "" : (string)dr_Institution["v_DurationFrom"];
                employee.DurationTo = Convert.IsDBNull(dr_Institution["v_DurationTo"]) ? "" : (string)dr_Institution["v_DurationTo"];
                employee.InstitutionName = Convert.IsDBNull(dr_Institution["v_trninstName"]) ? "" : (string)dr_Institution["v_trninstName"];
                employee.prgmtypName = Convert.IsDBNull(dr_Institution["v_pgrmtypeName"]) ? "" : (string)dr_Institution["v_pgrmtypeName"];
                employee.trnrName = Convert.IsDBNull(dr_Institution["v_pgmtrnrName"]) ? "" : (string)dr_Institution["v_pgmtrnrName"];
                employee.prgmname = Convert.IsDBNull(dr_Institution["v_pgrmName"]) ? "" : (string)dr_Institution["v_pgrmName"];
                employee.temp_str = Convert.IsDBNull(dr_Institution["v_summary"]) ? "" : (string)dr_Institution["v_summary"];

                InstitutionList.Add(employee);
            }
            return InstitutionList;
        }

        public string Employee_Training_Req(Employee e)
        {
            try
            {
                _Connection = Con.fn_Connection();


                SqlCommand _Cmd = new SqlCommand("sp_paym_Training_Request", _Connection);
                _Cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter[] _ISPParamter = new SqlParameter[8];

                _ISPParamter[0] = new SqlParameter("@pn_CompanyID", SqlDbType.Int);
                _ISPParamter[0].Value = e.CompanyId;
                _ISPParamter[1] = new SqlParameter("@pn_BranchID", SqlDbType.Int);
                _ISPParamter[1].Value = e.BranchId;
                _ISPParamter[2] = new SqlParameter("@ProgramName", SqlDbType.VarChar);
                _ISPParamter[2].Value = e.prgmname;
                _ISPParamter[3] = new SqlParameter("@ProgramType", SqlDbType.VarChar);
                _ISPParamter[3].Value = e.prgmtypName;
                _ISPParamter[4] = new SqlParameter("@summary", SqlDbType.VarChar);
                _ISPParamter[4].Value = e.temp_str;
                _ISPParamter[5] = new SqlParameter("@id", SqlDbType.Int);
                _ISPParamter[5].Value = e.TrainingID;
                _ISPParamter[6] = new SqlParameter("@Status", SqlDbType.VarChar);
                _ISPParamter[6].Value = e.IDno;
                _ISPParamter[7] = new SqlParameter("@Reason", SqlDbType.VarChar);
                _ISPParamter[7].Value = e.Reason;

                for (int i = 0; i < _ISPParamter.Length; i++)
                {
                    _Cmd.Parameters.Add(_ISPParamter[i]);
                }
                _Connection.Open();
                _Cmd.ExecuteNonQuery();
                _Connection.Close();
                return "0";
            }
            catch (SqlException Ex)
            {
                return "1";
            }
        }

        public Collection<Employee> fn_getTraininglist(string str_query)
        {
            Collection<Employee> DeductionList = new Collection<Employee>();
            _Connection = Con.fn_Connection();
            SqlCommand _SSDeduction = new SqlCommand(str_query, _Connection);
            _Connection.Open();
            SqlDataReader dr_Deduction = _SSDeduction.ExecuteReader();
            while (dr_Deduction.Read())
            {
                Employee employee = new Employee();
                employee.TrainingID = (int)dr_Deduction["ID"];
                employee.prgmname = Convert.IsDBNull(dr_Deduction["ProgramName"]) ? "" : (string)dr_Deduction["ProgramName"];
                employee.prgmtypName = Convert.IsDBNull(dr_Deduction["ProgramType"]) ? "" : (string)dr_Deduction["ProgramType"];
                employee.temp_str = Convert.IsDBNull(dr_Deduction["Summary"]) ? "" : (string)dr_Deduction["Summary"];
                employee.IDno = Convert.IsDBNull(dr_Deduction["Status"]) ? "" : (string)dr_Deduction["Status"];
                employee.Reason = Convert.IsDBNull(dr_Deduction["Reason"]) ? "" : (string)dr_Deduction["Reason"];
                DeductionList.Add(employee);
            }
            return DeductionList;
        }
        public Collection<Employee> fn_getReportingList(Employee e)
        {
            Collection<Employee> EmployeeList = new Collection<Employee>();
            _Connection = Con.fn_Connection();//order by Employee_First_Name asc
            SqlCommand _Course = new SqlCommand("select * from paym_Designation where pn_CompanyID=" + e.CompanyId + " and BranchID =" + e.BranchId + " and status = 'Y' ", _Connection);
            _Connection.Open();
            SqlDataReader dr = _Course.ExecuteReader();
            while (dr.Read())
            {
                Employee employee = new Employee();
                employee.DesignationId = (int)dr["pn_DesignationID"];
                employee.DesignationName = Convert.IsDBNull(dr["v_DesignationName"]) ? "" : (string)dr["v_DesignationName"];
                
                EmployeeList.Add(employee);
            }
            return EmployeeList;
        }
        //public Collection<Employee> fn_getReportingList(Employee e)
        //{
        //    Collection<Employee> EmployeeList = new Collection<Employee>();
        //    _Connection = Con.fn_Connection();//order by Employee_First_Name asc
        //    SqlCommand _Course = new SqlCommand("select a.* from paym_Employee a where a.pn_CompanyID=" + e.CompanyId + " and a.pn_BranchID =" + e.BranchId + " and a.status!='N' and Flag != 'N' order by a.employeecode asc", _Connection);
        //    _Connection.Open();
        //    SqlDataReader dr = _Course.ExecuteReader();
        //    while (dr.Read())
        //    {
        //        Employee employee = new Employee();
        //        employee.EmployeeId = (int)dr["pn_EmployeeID"];
        //        employee.EmployeeCode = Convert.IsDBNull(dr["EmployeeCode"]) ? "" : (string)dr["EmployeeCode"];
        //        employee.FirstName = Convert.IsDBNull(dr["Employee_First_Name"]) ? "" : (string)dr["Employee_First_Name"];
        //        employee.LastName = (string)dr["EmployeeCode"] + "-" + (string)dr["Employee_First_Name"];
        //        employee.FullName = (int)dr["pn_EmployeeID"] + "-" + (string)dr["Employee_First_Name"];
        //        EmployeeList.Add(employee);
        //    }
        //    return EmployeeList;
        //}

        public Collection<Employee> fn_getEmployeeReporting(Employee e)
        {
            Collection<Employee> EmployeeList = new Collection<Employee>();
            _Connection = Con.fn_Connection();//order by Employee_First_Name asc
            SqlCommand _Course = new SqlCommand("select employee_first_name,employeecode,pn_employeeID from paym_employee where pn_CompanyID = '" + e.CompanyId + "' and pn_BranchID = '" + e.BranchId + "' and reportingid = '" + e.EmployeeId + "' and status!='N' order by employeecode asc", _Connection);
            _Connection.Open();
            SqlDataReader dr = _Course.ExecuteReader();
            while (dr.Read())
            {
                Employee employee = new Employee();
                employee.EmployeeId = (int)dr["pn_EmployeeID"];
                employee.EmployeeCode = Convert.IsDBNull(dr["EmployeeCode"]) ? "" : (string)dr["EmployeeCode"];
                employee.LastName = (string)dr["EmployeeCode"] + "-" + (string)dr["Employee_First_Name"];
                EmployeeList.Add(employee);
            }
            return EmployeeList;
        }

        public string fn_Update_pgmtype(Employee e)
        {
            try
            {
                _Connection = Con.fn_Connection();

                _Connection.Open();

                SqlCommand _RSC_can = new SqlCommand("update paym_instpgmtype set v_pgrmtypeName='" + e.prgmtypName + "' where pn_pgrmtypeID=" + e.prgmtypId + "", _Connection);

                _RSC_can.ExecuteNonQuery();

                _Connection.Close();

                return "0";
            }

            catch (Exception ex)
            {
                return "1";
            }

        }

        public string fn_Update_pgmname(Employee e)
        {

            try
            {
                _Connection = Con.fn_Connection();

                _Connection.Open();

                SqlCommand _RSC_can = new SqlCommand("update paym_instpgmname set v_pgrmName='" + e.prgmname + "' where pn_pgrmNameID=" + e.prgmid + "", _Connection);

                _RSC_can.ExecuteNonQuery();

                _Connection.Close();
                return "0";
            }
            catch(Exception ex)
            {
                return "1";
            }

        }

        public void fn_Update_Institution(Employee e)
        {

            _Connection = Con.fn_Connection();

            _Connection.Open();

            SqlCommand _RSC_can = new SqlCommand("update paym_trninst set v_trninstName='" + e.InstitutionName + "',v_trninstadd1='" + e.P_AddressLine1 + "',v_trninstadd2='" + e.P_AddressLine2 + "',v_trninstcity='" + e.P_City + "',v_trninststate='" + e.P_State + "',v_trninstcountry='" + e.P_Country + "',v_trninstphone='" + e.ph_Residence + "',v_trninstmail='" + e.EmailId + "',v_trninstweb='" + e.websitename + "', where pn_trninstID=" + e.InstitutionId + "", _Connection);

            _RSC_can.ExecuteNonQuery();

            _Connection.Close();

        }

        //$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$ Temp Table $$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$


        //Employee Profile


        public Collection<Employee> Temp_Emp_Profile_update(Employee emp)
        {
            Collection<Employee> EmployeeList = new Collection<Employee>();
            _Connection = Con.fn_Connection();

            //string _Sqlstring = "select div.v_DivisionName,dep.v_DepartmentName,des.v_DesignationName,gra.v_GradeName,";

            //_Sqlstring = _Sqlstring + "shi.v_ShiftName,cat.v_CategoryName,job.v_JobStatusName,lev.v_LevelName,pro.v_projectsiteName,e.pn_EmployeeID,e.d_Date,e.v_Reason from ";

            //_Sqlstring = _Sqlstring + "paym_Division div,paym_Department dep,paym_Designation des,paym_Grade gra,paym_Shift shi,paym_Category cat,";

            //_Sqlstring = _Sqlstring + "paym_JobStatus job,paym_Level lev,paym_projectsite pro,paym_Employee_profile1 e where ";

            //_Sqlstring = _Sqlstring + "div.pn_DivisionId=e.pn_DivisionId and dep.pn_DepartmentId=e.pn_DepartmentId and des.pn_DesignationID=e.pn_DesingnationId and ";

            //_Sqlstring = _Sqlstring + "gra.pn_GradeId=e.pn_GradeId and shi.pn_ShiftId=e.pn_ShiftId and cat.pn_CategoryId=e.pn_CategoryId and ";

            //_Sqlstring = _Sqlstring + "job.pn_JobStatusID=e.pn_JobStatusID and lev.pn_LevelID=e.pn_LevelID and pro.pn_projectsiteID = e.pn_projectsiteID and e.d_date <= '" + emp.d_Date +"' order by e.d_Date asc";


            string _Sqlstring = "select dep.v_DepartmentName,des.v_DesignationName,";

            _Sqlstring = _Sqlstring + "e.pn_EmployeeID,e.d_Date,e.v_Reason from ";

            _Sqlstring = _Sqlstring + "paym_Department dep,paym_Designation des,";

            _Sqlstring = _Sqlstring + "paym_Employee_profile1 e where ";

            _Sqlstring = _Sqlstring + "dep.pn_DepartmentId=e.pn_DepartmentId and des.pn_DesignationID=e.pn_DesingnationId order by e.d_Date asc ";


            SqlCommand _Course = new SqlCommand(_Sqlstring, _Connection);
            _Connection.Open();
            SqlDataReader dr = _Course.ExecuteReader();
            while (dr.Read())
            {
                Employee employee = new Employee();

                employee.EmployeeId = (int)dr["pn_EmployeeID"];

                //employee.DivisionName = Convert.IsDBNull(dr["v_DivisionName"]) ? "" : (string)dr["v_DivisionName"];
                employee.DepartmentName = Convert.IsDBNull(dr["v_DepartmentName"]) ? "" : (string)dr["v_DepartmentName"];
                employee.DesignationName = Convert.IsDBNull(dr["v_DesignationName"]) ? "" : (string)dr["v_DesignationName"];
                //employee.GradeName = Convert.IsDBNull(dr["v_GradeName"]) ? "" : (string)dr["v_GradeName"];
                //employee.ShiftName = Convert.IsDBNull(dr["v_ShiftName"]) ? "" : (string)dr["v_ShiftName"];
                //employee.CategoryName = Convert.IsDBNull(dr["v_CategoryName"]) ? "" : (string)dr["v_CategoryName"];
                //employee.JobStatusName = Convert.IsDBNull(dr["v_JobStatusName"]) ? "" : (string)dr["v_JobStatusName"];
                //employee.LevelName = Convert.IsDBNull(dr["v_LevelName"]) ? "" : (string)dr["v_LevelName"];
                //employee.ProjectsiteName = Convert.IsDBNull(dr["v_projectsiteName"]) ? "" : (string)dr["v_projectsiteName"];

                employee.Date = Convert.ToDateTime(dr["d_Date"]);
                employee.temp_str = Convert.IsDBNull(dr["v_Reason"]) ? "" : (string)dr["v_Reason"];

                EmployeeList.Add(employee);
            }
            return EmployeeList;

        }

        public Collection<Employee> Temp_Emp_first(Employee e)
        {
            Collection<Employee> EmployeeList = new Collection<Employee>();
            _Connection = Con.fn_Connection();

            string _Sqlstring = "select c.CompanyName,b.BranchName,e.pn_EmployeeID,e.EmployeeCode,e.Password,e.Gender,e.Employee_First_Name,";

            _Sqlstring += "e.Employee_Middle_Name,e.Employee_Last_Name,e.basic_salary,e.CTC, e.status, e.DateofBirth, e.Esino, e.Pfno, e.bank_name, e.account_type,e.IFSC_Code from paym_Company c,paym_Branch b,paym_Employee e";

            _Sqlstring += " where c.pn_CompanyID=e.pn_CompanyID and b.pn_BranchID=e.pn_BranchID and e.pn_BranchID = '"+e.BranchId+"'";

            SqlCommand _Course = new SqlCommand(_Sqlstring, _Connection);
            _Connection.Open();
            SqlDataReader dr = _Course.ExecuteReader();
            while (dr.Read())
            {
                Employee employee = new Employee();
                employee.CourseName = Convert.IsDBNull(dr["CompanyName"]) ? "" : (string)dr["CompanyName"];
                employee.SkillName = Convert.IsDBNull(dr["BranchName"]) ? "" : (string)dr["BranchName"];
                employee.EmployeeId = (int)dr["pn_EmployeeID"];
                employee.EmployeeCode = Convert.IsDBNull(dr["EmployeeCode"]) ? "" : (string)dr["EmployeeCode"];
                employee.password = Convert.IsDBNull(dr["Password"]) ? "" : (string)dr["Password"];
                employee.FirstName = Convert.IsDBNull(dr["Employee_First_Name"]) ? "" : (string)dr["Employee_First_Name"];
                employee.MiddleName = Convert.IsDBNull(dr["Employee_Middle_Name"]) ? "" : (string)dr["Employee_Middle_Name"];
                employee.LastName = Convert.IsDBNull(dr["Employee_Last_Name"]) ? "" : (string)dr["Employee_Last_Name"];
                employee.basic_salary = Convert.IsDBNull(dr["basic_salary"]) ? 0 : (double)(dr["basic_salary"]);
                employee.CTC_salary = Convert.IsDBNull(dr["CTC"]) ? 0 : (double)(dr["CTC"]);
                employee.status = Convert.ToChar(dr["Status"]);
                employee.d_birth = Convert.ToDateTime(dr["DateofBirth"]);
                //employee.Date = Convert.ToDateTime(dr["dob"]);
                employee.Gender = Convert.IsDBNull(dr["Gender"]) ? "" : (string)dr["Gender"];
                employee.PFno = Convert.IsDBNull(dr["Pfno"]) ? "" : (string)dr["Pfno"];
                employee.ESIno = Convert.IsDBNull(dr["Esino"]) ? "" : (string)dr["Esino"];
                employee.Bank_Name = Convert.IsDBNull(dr["bank_name"]) ? "" : (string)dr["bank_name"];
                employee.IFSC_Code = Convert.IsDBNull(dr["IFSC_Code"]) ? "" : (string)dr["IFSC_Code"];
                employee.Account_Type = Convert.IsDBNull(dr["account_type"]) ? "" : (string)dr["account_type"];
                EmployeeList.Add(employee);
            }
            return EmployeeList;
        }

        //Employee General

        public Collection<Employee> Temp_Emp_general(Employee e)
        {

            Collection<Employee> EmployeeList = new Collection<Employee>();

            _Connection = Con.fn_Connection();

            SqlCommand _Course = new SqlCommand("select * from paym_Employee_General where pn_BranchID = '"+e.BranchId+"'", _Connection);

            _Connection.Open();

            SqlDataReader dr_BranchCompany = _Course.ExecuteReader();

            while (dr_BranchCompany.Read())
            {
                Employee employee = new Employee();

                employee.EmployeeId = (int)dr_BranchCompany["pn_EmployeeID"];
                employee.EmailId = Convert.IsDBNull(dr_BranchCompany["EmailId"]) ? "" : (string)dr_BranchCompany["EmailId"];
                employee.A_EmailId = Convert.IsDBNull(dr_BranchCompany["AlternateEmailId"]) ? "" : (string)dr_BranchCompany["AlternateEmailId"];
                employee.BloodGroup = Convert.IsDBNull(dr_BranchCompany["BloodGroup"]) ? "" : (string)dr_BranchCompany["BloodGroup"];
                employee.Religion = Convert.IsDBNull(dr_BranchCompany["Religion"]) ? "" : (string)dr_BranchCompany["Religion"];
                employee.Nationality = Convert.IsDBNull(dr_BranchCompany["Nationality"]) ? "" : (string)dr_BranchCompany["Nationality"];
                employee.HouseNo = Convert.IsDBNull(dr_BranchCompany["PresentHouseNo"]) ? "" : (string)dr_BranchCompany["PresentHouseNo"];
                employee.StreetName = Convert.IsDBNull(dr_BranchCompany["PresentStreetName"]) ? "" : (string)dr_BranchCompany["PresentStreetName"];
                employee.AddressLine1 = Convert.IsDBNull(dr_BranchCompany["PresentAddLine1"]) ? "" : (string)dr_BranchCompany["PresentAddLine1"];
                employee.AddressLine2 = Convert.IsDBNull(dr_BranchCompany["PresentAddLine2"]) ? "" : (string)dr_BranchCompany["PresentAddLine2"];
                employee.City = Convert.IsDBNull(dr_BranchCompany["PresentCity"]) ? "" : (string)dr_BranchCompany["PresentCity"];
                employee.State = Convert.IsDBNull(dr_BranchCompany["PresentState"]) ? "" : (string)dr_BranchCompany["PresentState"];
                employee.p_HouseNo = Convert.IsDBNull(dr_BranchCompany["PermanentHouseNo"]) ? "" : (string)dr_BranchCompany["PermanentHouseNo"];
                employee.p_StreetName = Convert.IsDBNull(dr_BranchCompany["PermanentStreetName"]) ? "" : (string)dr_BranchCompany["PermanentStreetName"];
                employee.P_AddressLine1 = Convert.IsDBNull(dr_BranchCompany["PermanentAddLine1"]) ? "" : (string)dr_BranchCompany["PermanentAddLine1"];
                employee.P_AddressLine2 = Convert.IsDBNull(dr_BranchCompany["PermanentAddLine2"]) ? "" : (string)dr_BranchCompany["PermanentAddLine2"];
                employee.P_City = Convert.IsDBNull(dr_BranchCompany["PermanentCity"]) ? "" : (string)dr_BranchCompany["PermanentCity"];
                employee.P_State = Convert.IsDBNull(dr_BranchCompany["PermanentState"]) ? "" : (string)dr_BranchCompany["PermanentState"];

                employee.ph_Office = Convert.IsDBNull(dr_BranchCompany["ph_Office"]) ? "" : (string)dr_BranchCompany["ph_Office"];
                employee.ph_Residence = Convert.IsDBNull(dr_BranchCompany["ph_Residence"]) ? "" : (string)dr_BranchCompany["ph_Residence"];
                employee.CellNo = Convert.IsDBNull(dr_BranchCompany["CellNo"]) ? "" : (string)dr_BranchCompany["CellNo"];
                employee.Fax = Convert.IsDBNull(dr_BranchCompany["Fax"]) ? "" : (string)dr_BranchCompany["Fax"];
                employee.emgname = Convert.IsDBNull(dr_BranchCompany["emgName"]) ? "" : (string)dr_BranchCompany["emgName"];
                employee.emgno = Convert.IsDBNull(dr_BranchCompany["emgPhone"]) ? "" : (string)dr_BranchCompany["emgPhone"];

                employee.Salutation = Convert.ToChar(dr_BranchCompany["Salutation"]);
                employee.MaritalStatus = Convert.ToChar(dr_BranchCompany["M_Status"]);
                employee.FatherName = Convert.IsDBNull(dr_BranchCompany["FatherName"]) ? "" : (string)dr_BranchCompany["FatherName"];
                employee.MotherName = Convert.IsDBNull(dr_BranchCompany["MotherName"]) ? "" : (string)dr_BranchCompany["MotherName"];
                employee.Children = Convert.IsDBNull(dr_BranchCompany["Children"]) ? "" : (string)dr_BranchCompany["Children"];
                employee.SpouseName = Convert.IsDBNull(dr_BranchCompany["SpouseName"]) ? "" : (string)dr_BranchCompany["SpouseName"];
                employee.Ref1_Name = Convert.IsDBNull(dr_BranchCompany["Ref1_Name"]) ? "" : (string)dr_BranchCompany["Ref1_Name"];
                employee.Ref1_Phno = Convert.IsDBNull(dr_BranchCompany["Ref1_Phno"]) ? "" : (string)dr_BranchCompany["Ref1_Phno"];
                employee.Ref1_Email = Convert.IsDBNull(dr_BranchCompany["Ref1_Email"]) ? "" : (string)dr_BranchCompany["Ref1_Email"];
                employee.Ref1_Relation = Convert.IsDBNull(dr_BranchCompany["Ref1_Relation"]) ? "" : (string)dr_BranchCompany["Ref1_Relation"];
                employee.Ref2_Name = Convert.IsDBNull(dr_BranchCompany["Ref2_Name"]) ? "" : (string)dr_BranchCompany["Ref2_Name"];
                employee.Ref2_Phno = Convert.IsDBNull(dr_BranchCompany["Ref2_Phno"]) ? "" : (string)dr_BranchCompany["Ref2_Phno"];
                employee.Ref2_Email = Convert.IsDBNull(dr_BranchCompany["Ref2_Email"]) ? "" : (string)dr_BranchCompany["Ref2_Email"];
                employee.Ref2_Relation = Convert.IsDBNull(dr_BranchCompany["Ref2_Relation"]) ? "" : (string)dr_BranchCompany["Ref2_Relation"];


                EmployeeList.Add(employee);

            }
            return EmployeeList;
        }

        //Employee Work Details 1(div,....)

        public Collection<Employee> Temp_Emp_Profile(Employee e)
        {
            Collection<Employee> EmployeeList = new Collection<Employee>();
            _Connection = Con.fn_Connection();

            //string _Sqlstring = "select div.v_DivisionName,dep.v_DepartmentName,des.v_DesignationName,gra.v_GradeName,";

            //_Sqlstring = _Sqlstring + "shi.v_ShiftName,cat.v_CategoryName,job.v_JobStatusName,lev.v_LevelName,pro.v_projectsiteName,e.pn_EmployeeID,e.d_Date,e.v_Reason from ";

            //_Sqlstring = _Sqlstring + "paym_Division div,paym_Department dep,paym_Designation des,paym_Grade gra,paym_Shift shi,paym_Category cat,";

            //_Sqlstring = _Sqlstring + "paym_JobStatus job,paym_Level lev,paym_projectsite pro,paym_Employee_profile1 e where ";

            //_Sqlstring = _Sqlstring + "div.pn_DivisionId=e.pn_DivisionId and dep.pn_DepartmentId=e.pn_DepartmentId and des.pn_DesignationID=e.pn_DesingnationId and ";

            //_Sqlstring = _Sqlstring + "gra.pn_GradeId=e.pn_GradeId and shi.pn_ShiftId=e.pn_ShiftId and cat.pn_CategoryId=e.pn_CategoryId and ";

            //_Sqlstring = _Sqlstring + "job.pn_JobStatusID=e.pn_JobStatusID and lev.pn_LevelID=e.pn_LevelID and pro.pn_projectsiteID = e.pn_projectsiteID";


            string _Sqlstring = "select dep.v_DepartmentName,des.v_DesignationName,gra.v_GradeName,cat.v_CategoryName,e.pn_EmployeeID,e.d_Date,e.v_Reason from paym_Department dep,paym_Designation des, paym_Grade gra,paym_Category cat, paym_Employee_profile1 e where dep.pn_DepartmentId = e.pn_DepartmentId and des.pn_DesignationID = e.pn_DesingnationId and gra.pn_GradeId = e.pn_GradeId and cat.pn_CategoryId = e.pn_CategoryId";

            SqlCommand _Course = new SqlCommand(_Sqlstring, _Connection);
            _Connection.Open();
            SqlDataReader dr = _Course.ExecuteReader();
            while (dr.Read())
            {
                Employee employee = new Employee();

                employee.EmployeeId = Convert.ToInt32(dr["pn_EmployeeID"]);

                //employee.DivisionName = Convert.IsDBNull(dr["v_DivisionName"]) ? "" : (string)dr["v_DivisionName"];
                employee.DepartmentName = Convert.IsDBNull(dr["v_DepartmentName"]) ? "" : (string)dr["v_DepartmentName"];
                employee.DesignationName = Convert.IsDBNull(dr["v_DesignationName"]) ? "" : (string)dr["v_DesignationName"];
                employee.GradeName = Convert.IsDBNull(dr["v_GradeName"]) ? "" : (string)dr["v_GradeName"];
                //employee.ShiftName = Convert.IsDBNull(dr["v_ShiftName"]) ? "" : (string)dr["v_ShiftName"];
                //employee.CategoryName = Convert.IsDBNull(dr["v_CategoryName"]) ? "" : (string)dr["v_CategoryName"];
                //employee.JobStatusName = Convert.IsDBNull(dr["v_JobStatusName"]) ? "" : (string)dr["v_JobStatusName"];
                //employee.LevelName = Convert.IsDBNull(dr["v_LevelName"]) ? "" : (string)dr["v_LevelName"];
                //employee.ProjectsiteName = Convert.IsDBNull(dr["v_projectsiteName"]) ? "" : (string)dr["v_projectsiteName"];

                employee.Date = Convert.ToDateTime(dr["d_Date"]);
                employee.temp_str = Convert.IsDBNull(dr["v_Reason"]) ? "" : (string)dr["v_Reason"];

                EmployeeList.Add(employee);
            }
            return EmployeeList;

        }   

        //Employee work details 2

        public Collection<Employee> Temp_Emp_WorkDetails(Employee e)
        {
            Collection<Employee> EmployeeList = new Collection<Employee>();
            _Connection = Con.fn_Connection();
            SqlCommand _Course = new SqlCommand("select * from paym_Employee_WorkDetails where pn_BranchID='"+e.BranchId+"'", _Connection);
            _Connection.Open();
            SqlDataReader dr = _Course.ExecuteReader();
            while (dr.Read())
            {
                Employee employee = new Employee();

                employee.EmployeeId = (int)dr["pn_EmployeeID"];

                employee.d_join = Convert.ToDateTime(dr["JoiningDate"]);
                employee.d_Offer = Convert.ToDateTime(dr["OfferDate"]);
                employee.d_probotion = Convert.ToDateTime(dr["ProbationUpto"]);
                employee.d_extended = Convert.ToDateTime(dr["ExtendedUpto"]);
                employee.d_confirmation = Convert.ToDateTime(dr["ConfirmationDate"]);
                employee.d_retirement = Convert.ToDateTime(dr["RetirementDate"]);
                employee.d_renue = Convert.ToDateTime(dr["ContractRenviewDate"]);

                EmployeeList.Add(employee);
            }
            return EmployeeList;
        }

        public void Temp_Employee(string t_q)
        {

            _Connection = Con.fn_Connection();

            _Connection.Open();

            SqlCommand _RSC_can = new SqlCommand(t_q, _Connection);

            _RSC_can.ExecuteNonQuery();

            _Connection.Close();

        }

        public Collection<Employee> Temp_Selected_EmployeeList(Employee e)
        {
            Collection<Employee> EmployeeList = new Collection<Employee>();
            _Connection = Con.fn_Connection();
            SqlCommand _Course = new SqlCommand(e.temp_str, _Connection);
            _Connection.Open();
            SqlDataReader dr = _Course.ExecuteReader();
            while (dr.Read())
            {
                Employee employee = new Employee();
                employee.EmployeeId = (int)dr["pn_EmployeeID"];
                //employee.EmployeeCode = Convert.IsDBNull(dr["EmployeeCode"]) ? "" : (string)dr["EmployeeCode"];
                //employee.FirstName = Convert.IsDBNull(dr["Employee_First_Name"]) ? "" : (string)dr["Employee_First_Name"];

                EmployeeList.Add(employee);
            }
            return EmployeeList;
        }

        public Collection<Employee> Temp_checkList(string str_report)
        {
            Collection<Employee> EmployeeList = new Collection<Employee>();
            _Connection = Con.fn_Connection();
            SqlCommand _Course = new SqlCommand(str_report, _Connection);
            _Connection.Open();
            SqlDataReader dr = _Course.ExecuteReader();
            int inc_i = 0;

            while (dr.Read())
            {
                Employee employee = new Employee();
                employee.EmployeeCode = Convert.IsDBNull(dr["COLUMN_NAME"]) ? "" : (string)dr["COLUMN_NAME"];
                inc_i = inc_i + 1;
                employee.EmployeeId = inc_i;
                EmployeeList.Add(employee);
            }
            return EmployeeList;
        }


        public DataSet Temp_Employee_retrive(string t_q)
        {

            DataSet ds = new DataSet();

            _Connection = Con.fn_Connection();

            _Connection.Open();

            SqlCommand _RSC_can = new SqlCommand(t_q, _Connection);

            SqlDataAdapter sda = new SqlDataAdapter(_RSC_can);

            sda.Fill(ds);

            _Connection.Close();

            return ds;

        }

        public Collection<Employee> Temp_Emp_Earnings1(Employee e)
        {
            Collection<Employee> EmployeeList = new Collection<Employee>();
            _Connection = Con.fn_Connection();
            //new query for order the earnings
            //select * from paym_earnings where status='y' order by c_regular desc, pn_Earningsid asc 
            SqlCommand _Course = new SqlCommand("select * from paym_Earnings where pn_BranchID='" + e.BranchId + "' and status='Y'", _Connection);
            _Connection.Open();
            SqlDataReader dr = _Course.ExecuteReader();
            while (dr.Read())
            {
                Employee employee = new Employee();

                employee.EarningsName = Convert.IsDBNull(dr["v_earningscode"]) ? "" : (string)dr["v_earningscode"];
                EmployeeList.Add(employee);
            }
            return EmployeeList;
        }

        public Collection<Employee> Temp_Emp_Deductions1(Employee e)
        {
            Collection<Employee> EmployeeList = new Collection<Employee>();
            _Connection = Con.fn_Connection();
            //new query for order the earnings
            //select * from paym_deduction where status='y' order by c_regular desc, pn_Deductionid asc 
            SqlCommand _Course = new SqlCommand("select * from paym_Deduction where pn_BranchID='" + e.BranchId + "' and status='Y'", _Connection);
            _Connection.Open();
            SqlDataReader dr = _Course.ExecuteReader();
            while (dr.Read())
            {
                Employee employee = new Employee();
                employee.DeducationCode = Convert.IsDBNull(dr["v_DeductionCode"]) ? "" : (string)dr["v_DeductionCode"];
                EmployeeList.Add(employee);
            }
            return EmployeeList;
        }

        public Collection<Employee> fn_query_transwer(string str_query) 
        {
            Collection<Employee> DeductionList = new Collection<Employee>();
            _Connection = Con.fn_Connection();
            SqlCommand _SSDeduction = new SqlCommand(str_query, _Connection);
            _Connection.Open();
            SqlDataReader dr_Deduction = _SSDeduction.ExecuteReader();
            while (dr_Deduction.Read())
            {
                Employee employee = new Employee();
                employee.EmployeeId = (int)dr_Deduction["pn_EmployeeID"];
                employee.LastName = (string)dr_Deduction["EmployeeCode"] + "," + (string)dr_Deduction["Employee_First_Name"];
                DeductionList.Add(employee);
            }
            return DeductionList;
        }


        public Collection<Employee> fn_getEmplist(string str_query)
        {
            Collection<Employee> DeductionList = new Collection<Employee>();
            _Connection = Con.fn_Connection();
            SqlCommand _SSDeduction = new SqlCommand(str_query, _Connection);
            _Connection.Open();
            SqlDataReader dr_Deduction = _SSDeduction.ExecuteReader();
            while (dr_Deduction.Read())
            {
                Employee employee = new Employee();
                employee.EmployeeId = (int)dr_Deduction["pn_EmployeeID"];
                employee.LastName = (string)dr_Deduction["EmployeeCode"] + "-" + (string)dr_Deduction["Employee_First_Name"];
                employee.FirstName = (string)dr_Deduction["Employee_First_Name"];
                DeductionList.Add(employee);
            }
            return DeductionList;
        }

        public Collection<Employee> fn_getprojectlist(string str_query)
        {
            Collection<Employee> DeductionList = new Collection<Employee>();
            _Connection = Con.fn_Connection();
            SqlCommand _SSDeduction = new SqlCommand(str_query, _Connection);
            _Connection.Open();
            SqlDataReader dr_Deduction = _SSDeduction.ExecuteReader();
            while (dr_Deduction.Read())
            {
                Employee employee = new Employee();
                employee.ProjectsiteId= (int)dr_Deduction["pn_ProjectsiteID"];
                employee.ProjectsiteName = (string)dr_Deduction["projectName"];
                DeductionList.Add(employee);
            }
            return DeductionList;
        }

        public Collection<Employee> fn_programname1(Employee e)
        {
            Collection<Employee> programnameList = new Collection<Employee>();
            _Connection = Con.fn_Connection();
            string _SqlString = "select distinct(ProgramName) from paym_training_new where pn_companyid='" + e.CompanyId + "' and pn_branchid='" + e.BranchId + "'";
            SqlCommand _SSprogramname = new SqlCommand(_SqlString, _Connection);
            _Connection.Open();
            SqlDataReader dr_programname = _SSprogramname.ExecuteReader();
            while (dr_programname.Read())
            {
                Employee employee = new Employee();
                //employee.prgmid = (int)dr_programname["pn_pgrmNameID"];
                employee.prgmname = Convert.IsDBNull(dr_programname["ProgramName"]) ? "" : (string)dr_programname["ProgramName"];
                programnameList.Add(employee);
            }
            return programnameList;
        }

        public Collection<Employee> fn_Training_grid(Employee e)
        {
            Collection<Employee> InstitutionList = new Collection<Employee>();
            _Connection = Con.fn_Connection();
            string _Sqlstring = "select t.TrainingID,t.v_DurationFrom,t.v_DurationTo,t.Rating,ti.ins_name,tpt.ptype,tpt.fname,tpt.specification";

            _Sqlstring += " from paym_training_New t,institution_profile ti,trainer_profile1 tpt";
            _Sqlstring += " where t.pn_EmployeeID=" + e.EmployeeId + " and t.pn_CompanyID=" + e.CompanyId + "";
            _Sqlstring += " and ti.ID=t.InstID and tpt.specification=t.ProgramName";

            SqlCommand _SSInstitution = new SqlCommand(_Sqlstring, _Connection);
            _Connection.Open();
            SqlDataReader dr_Institution = _SSInstitution.ExecuteReader();
            while (dr_Institution.Read())
            {
                Employee employee = new Employee();
                employee.TrainingID = (int)dr_Institution["TrainingID"];
                employee.DurationFrom = Convert.IsDBNull(dr_Institution["v_DurationFrom"]) ? "" : (string)dr_Institution["v_DurationFrom"];
                employee.DurationTo = Convert.IsDBNull(dr_Institution["v_DurationTo"]) ? "" : (string)dr_Institution["v_DurationTo"];
                employee.Rating = Convert.IsDBNull(dr_Institution["Rating"]) ? 0 : (int)dr_Institution["Rating"];
                employee.InstitutionName = Convert.IsDBNull(dr_Institution["ins_name"]) ? "" : (string)dr_Institution["ins_name"];
                employee.prgmtypName = Convert.IsDBNull(dr_Institution["ptype"]) ? "" : (string)dr_Institution["ptype"];
                employee.trnrName = Convert.IsDBNull(dr_Institution["fName"]) ? "" : (string)dr_Institution["fName"];
                employee.prgmname = Convert.IsDBNull(dr_Institution["specification"]) ? "" : (string)dr_Institution["specification"];

                InstitutionList.Add(employee);
            }
            return InstitutionList;
        }

        public Collection<Employee> fn_emp(Employee e)
        {
            Collection<Employee> programnameList = new Collection<Employee>();
            _Connection = Con.fn_Connection();
            string _SqlString = "select a.pn_employeeid,(a.employeecode+'-'+a.employee_first_name)  as firstname from paym_employee a,Paym_Training_New b where a.pn_employeeid=b.pn_employeeid and b.ProgramName='" + e.prgmname + "' and b.pn_companyid='" + e.CompanyId + "' and b.pn_branchid='" + e.BranchId + "'";
            SqlCommand _SSprogramname = new SqlCommand(_SqlString, _Connection);
            _Connection.Open();
            SqlDataReader dr_programname = _SSprogramname.ExecuteReader();
            while (dr_programname.Read())
            {
                Employee employee = new Employee();
                employee.prgmid = (int)dr_programname["pn_employeeid"];
                employee.prgmname = Convert.IsDBNull(dr_programname["firstname"]) ? "" : (string)dr_programname["firstname"];
                programnameList.Add(employee);
            }
            return programnameList;
        }

        public Collection<Employee> fn_Training_grid1(Employee e)
        {
            Collection<Employee> InstitutionList = new Collection<Employee>();
            _Connection = Con.fn_Connection();
            string _Sqlstring = "select (a.employeecode+'-'+a.Employee_First_Name) as FirstName,b.*,c.Ins_Name,d.fname from paym_employee a,paym_training_new b,institution_profile c,trainer_profile1 d where b.instID=c.id and a.pn_employeeid=b.pn_employeeid and b.TrainerID=d.trainer_id and b.ProgramName='" + e.prgmname + "'";

            //_Sqlstring += " from paym_training_New t,institution_profile ti,trainer_profile1 tpt";
            //_Sqlstring += " where t.pn_EmployeeID=" + e.EmployeeId + " and t.pn_CompanyID=" + e.CompanyId + "";
            //_Sqlstring += " and ti.ID=t.fn_trninstID and tpt.specification=t.fn_pgrmName";

            SqlCommand _SSInstitution = new SqlCommand(_Sqlstring, _Connection);
            _Connection.Open();
            SqlDataReader dr_Institution = _SSInstitution.ExecuteReader();
            while (dr_Institution.Read())
            {
                Employee employee = new Employee();
                employee.TrainingID = (int)dr_Institution["pn_TrainingID"];
                employee.InstitutionName = Convert.IsDBNull(dr_Institution["Ins_Name"]) ? "" : (string)dr_Institution["Ins_Name"];
                employee.trnrName = Convert.IsDBNull(dr_Institution["fname"]) ? "" : (string)dr_Institution["fname"];
                employee.FirstName = Convert.IsDBNull(dr_Institution["FirstName"]) ? "" : (string)dr_Institution["FirstName"];
                employee.Rating = Convert.IsDBNull(dr_Institution["Rating"]) ? 0 : (int)dr_Institution["Rating"];


                InstitutionList.Add(employee);
            }
            return InstitutionList;
        }

        public Collection<Employee> fn_getInstitution(int e)
        {
            Collection<Employee> DepartmentList = new Collection<Employee>();
            _Connection = Con.fn_Connection();
            string _SqlString = "select * from Institution_Profile where pn_BranchID='" + e + "'";
            SqlCommand _SSDepartment = new SqlCommand(_SqlString, _Connection);
            _Connection.Open();
            SqlDataReader dr_Department = _SSDepartment.ExecuteReader();
            while (dr_Department.Read())
            {
                Employee employee = new Employee();
                employee.DepartmentId = (int)dr_Department["ID"];
                employee.DepartmentName = Convert.IsDBNull(dr_Department["ins_Name"]) ? "" : (string)dr_Department["ins_Name"];
                DepartmentList.Add(employee);
            }
            return DepartmentList;
        }

        public Collection<Employee> fn_programname2(Employee e)
        {
            Collection<Employee> programnameList = new Collection<Employee>();
            _Connection = Con.fn_Connection();
            string _SqlString = "select distinct(fn_pgrmName) from paym_training_new where pn_employeeid='" + e.EmployeeId + "' and  pn_companyid='" + e.CompanyId + "' and pn_branchid='" + e.BranchId + "'";
            SqlCommand _SSprogramname = new SqlCommand(_SqlString, _Connection);
            _Connection.Open();
            SqlDataReader dr_programname = _SSprogramname.ExecuteReader();
            while (dr_programname.Read())
            {
                Employee employee = new Employee();
                //employee.prgmid = (int)dr_programname["pn_pgrmNameID"];
                employee.prgmname = Convert.IsDBNull(dr_programname["fn_pgrmName"]) ? "" : (string)dr_programname["fn_pgrmName"];
                programnameList.Add(employee);
            }
            return programnameList;
        }

        public Collection<Employee> fn_feedback(int feedbackid)
        {
            Collection<Employee> FeedbackList = new Collection<Employee>();
            _Connection = Con.fn_Connection();
            string _SqlString = "select * from training_feedback where pn_feedbackid !=0 and BranchID='" + feedbackid + "'";
            SqlCommand _SSfeedback = new SqlCommand(_SqlString, _Connection);
            _Connection.Open();
            SqlDataReader dr_feedback = _SSfeedback.ExecuteReader();
            while (dr_feedback.Read())
            {
                Employee employee = new Employee();
                employee.FeedbackID = (int)dr_feedback["pn_feedbackid"];
                employee.FeedbackQues = Convert.IsDBNull(dr_feedback["v_feedback_ques"]) ? "" : (string)dr_feedback["v_feedback_ques"];
                employee.BranchId = (int)dr_feedback["BranchID"];
                FeedbackList.Add(employee);
            }
            return FeedbackList;
        }

        public Collection<Employee> fn_EmptyFeedback(Employee e)
        {
            Collection<Employee> FeedbackList = new Collection<Employee>();
            _Connection = Con.fn_Connection();
            string _SqlString = "select * from training_feedback  branchID='" + e.BranchId + "'";
            SqlCommand _SSfeedback = new SqlCommand(_SqlString, _Connection);
            _Connection.Open();
            SqlDataReader dr_feedback = _SSfeedback.ExecuteReader();
            while (dr_feedback.Read())
            {
                Employee employee = new Employee();
                employee.FeedbackID = (int)dr_feedback["pn_feedbackID"];
                employee.FeedbackQues = "No Feedback Questions";
                FeedbackList.Add(employee);
            }
            return FeedbackList;
        }

        public string fn_Update_feedback(Employee e)
        {
            try
            {
                _Connection = Con.fn_Connection();
                _Connection.Open();
                SqlCommand cmd = new SqlCommand("update training_feedback set v_feedback_ques='" + e.FeedbackQues + "' where pn_feedbackID=" + e.FeedbackID + "", _Connection);
                cmd.ExecuteNonQuery();
                _Connection.Close();
                return "0";
            }
            catch (Exception ex)
            {
                return "1";
            }

        }

        public string Convert_ToSqlDatestring(string cur_date)
        {
            try
            {
                string _d, _m, _y;
                string sql_date;

                if (cur_date != "")
                {
                    string[] da = cur_date.Split('/');
                    _d = da[0];
                    _m = da[1];
                    _y = da[2];

                    sql_date = _y + "/" + _m + "/" + _d;
                }
                else
                {
                    sql_date = "1900/01/01";
                }

                return sql_date;
            }
            catch
            {
                return "0";
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
                sql_date = DateTime.Now;
            }
            return sql_date;
        }

        public string Convert_ToIISDate(string cur_date)
        {

            string _d, _m, _y, sql_date = "";

            char[] splitter ={ '/' };
            string[] str_ary = new string[4];

            if (cur_date.Length == 10)
            {
                _m = cur_date.Substring(0, 2);
                _d = cur_date.Substring(3, 2);
                _y = cur_date.Substring(6, 4);

                sql_date = _d + "/" + _m + "/" + _y;
            }
            else
            {
                str_ary = cur_date.Split(splitter);

                _m = check_single(str_ary[0]);
                _d = check_single(str_ary[1]);
                _y = str_ary[2];

                sql_date = _d + "/" + _m + "/" + _y;

            }

            return sql_date;

        }

        public string check_single(string str_single)
        {
            if (str_single.Length == 1)
            {
                str_single = "0" + str_single;
            }
            return str_single;
        }

        public void Deletion(Employee e)
        {
            _Connection = Con.fn_Connection();

            SqlCommand _Cmd = new SqlCommand("sp_paym_delete", _Connection);
            _Cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter[] _ISPParamter = new SqlParameter[9];

            _ISPParamter[0] = new SqlParameter("@Emp_Code", SqlDbType.VarChar);
            _ISPParamter[0].Value = e.EmployeeCode;
            _ISPParamter[1] = new SqlParameter("@Emp_Role", SqlDbType.Char);
            _ISPParamter[1].Value = e.Role;
            _ISPParamter[2] = new SqlParameter("@delete_item", SqlDbType.VarChar);
            _ISPParamter[2].Value = e.Item;
            _ISPParamter[3] = new SqlParameter("@Item_Code", SqlDbType.VarChar);
            _ISPParamter[3].Value = e.ItemCode;
            _ISPParamter[4] = new SqlParameter("@Reason", SqlDbType.VarChar);
            _ISPParamter[4].Value = e.Reason;
            _ISPParamter[5] = new SqlParameter("@Summary", SqlDbType.VarChar);
            _ISPParamter[5].Value = e.temp_str;
            _ISPParamter[6] = new SqlParameter("@EffectiveDate", SqlDbType.DateTime);
            _ISPParamter[6].Value = e.Date;
            _ISPParamter[7] = new SqlParameter("@CurrentDate", SqlDbType.DateTime);
            _ISPParamter[7].Value = e.CurrentDate;
            _ISPParamter[8] = new SqlParameter("@status", SqlDbType.Char); 
            _ISPParamter[8].Value = e.status;

            for (int i = 0; i < _ISPParamter.Length; i++)
            {
                _Cmd.Parameters.Add(_ISPParamter[i]);
            }
            _Connection.Open();
            _Cmd.ExecuteNonQuery();
            _Connection.Close();
        }

        public void fn_reportbyid(string qry)
        {
            _Connection = Con.fn_Connection();
            SqlCommand cmd = new SqlCommand(qry, _Connection);
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
        public string fn_DepartmentName( )
        {
            string depid = "", depname = "";
            string query= "select pn_DepartmentId from paym_employee_profile1 where pn_employeeid = '" + EmployeeId + "' ";
            _Connection = Con.fn_Connection();
            SqlCommand cmd = new SqlCommand(query, _Connection);
            try
            {
                _Connection.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if(dr.Read())
                {
                    depid = dr[0].ToString();
                }
                _Connection.Close();
            }
            catch (Exception ex)
            {

            }
            
            query = "Select v_DepartmentName from paym_Department where pn_DepartmentID='" + depid + "'";
            _Connection = Con.fn_Connection();
            SqlCommand _cmd = new SqlCommand(query, _Connection);
            try
            {
                _Connection.Open();
                SqlDataReader dr = _cmd.ExecuteReader();
                if (dr.Read())
                {
                    depname = dr[0].ToString();
                }
                _Connection.Close();
            }
            catch (Exception ex)
            {

            }
            return depname;

        }
        public string fn_Date()
        {
            string enddate= e_date;
            return enddate;

        }
        public int  fn_RowCount(string qry)
        {
            int s=0;
            _Connection = Con.fn_Connection();
            SqlCommand cmd = new SqlCommand(qry, _Connection);
            try
            {
                _Connection.Open();
                s = (int)cmd.ExecuteScalar();
                _Connection.Close();
            }
            catch (Exception ex)
            {

            }
            return s;

        }
        


        public string  fn_procappraisal(string qry)
        {
            _Connection = Con.fn_Connection();
            SqlCommand cmd = new SqlCommand(qry, _Connection);
            //cmd.CommandType = CommandType.Text;
            try
            {
                _Connection.Open();
                cmd.ExecuteNonQuery();
                _Connection.Close();
                return "0";
            }
            catch (Exception ex)
            {
                return "1";
            }
        }

        public Collection<Employee> fn_getesidetails(string qry)
        {
            Collection<Employee> emp_esi = new Collection<Employee>();
            _Connection = Con.fn_Connection();
            SqlCommand cmd = new SqlCommand(qry, _Connection);
            _Connection.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                Employee employee = new Employee();
                employee.CompanyId = (int)dr["pn_CompanyID"];
                employee.BranchId = (int)dr["pn_BranchID"];
                employee.EmployeeId = (int)dr["pn_EmployeeID"];
                employee.EmployeeCode = Convert.IsDBNull(dr["EmployeeCode"]) ? "" : (string)dr["EmployeeCode"];
                employee.ESIno = Convert.IsDBNull(dr["Esino"]) ? "" : (string)dr["Esino"];
                employee.Netpay = Convert.IsDBNull(dr["NetPay"]) ? 0 : (int)dr["NetPay"];
                employee.ESI_EMP = Convert.IsDBNull(dr["ESI_EMP"]) ? 0 : (double)dr["ESI_EMP"];
                employee.ESI_EMPR = Convert.IsDBNull(dr["ESI_EPR"]) ? 0 : (double)dr["ESI_EPR"];
                employee.Paiddays = Convert.IsDBNull(dr["Paid_Days"]) ? 0 : (double)dr["Paid_Days"];
                employee.d_Date = Convert.IsDBNull(dr["d_Date"]) ? "" : Convert.ToString(dr["d_Date"]);
                emp_esi.Add(employee);
                
            }
            return emp_esi;
        }

        public Collection<Employee> fn_getdatediff(string qry)
        {
            Collection<Employee> datediff = new Collection<Employee>();
            _Connection = Con.fn_Connection();
            SqlCommand cmd = new SqlCommand(qry, _Connection);
            _Connection.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                Employee employee = new Employee();
                employee.Count = (int)dr["differ"];
                datediff.Add(employee);
            }
            return datediff;
        }


        public Collection<Employee> fn_getdt(string qry)
        {
            Collection<Employee> dated = new Collection<Employee>();
            _Connection = Con.fn_Connection();
            SqlCommand cmd = new SqlCommand(qry, _Connection);
            _Connection.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                Employee employee = new Employee();
                employee.dat = (DateTime)dr["date"];
                dated.Add(employee);
            }
            return dated;
        }

        public void date_insert(Employee e)
        {

            _Connection = Con.fn_Connection();

            SqlCommand _Cmd = new SqlCommand("sp_temp_date", _Connection);
            _Cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter[] _ISPParamter = new SqlParameter[1];

            _ISPParamter[0] = new SqlParameter("@date", SqlDbType.DateTime);
            _ISPParamter[0].Value = e.Date;

            for (int i = 0; i < _ISPParamter.Length; i++)
            {
                _Cmd.Parameters.Add(_ISPParamter[i]);
            }
            _Connection.Open();
            _Cmd.ExecuteNonQuery();
            _Connection.Close();

        }

        public Collection<Employee> fn_getempcount(string qry)
        {
            Collection<Employee> empcount = new Collection<Employee>();
            _Connection = Con.fn_Connection();
            SqlCommand cmd = new SqlCommand(qry, _Connection);
            _Connection.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                Employee employee = new Employee();
                employee.EmployeeCode = Convert.IsDBNull(dr["EmployeeCode"]) ? "" : dr["EmployeeCode"].ToString();
                empcount.Add(employee);
            }
            _Connection.Close();
            return empcount;
        }

        public Collection<Employee> get_Tasksheduledetails(Employee e)
        {
            Collection<Employee> EmpTask = new Collection<Employee>();
            _Connection = Con.fn_Connection();
            //string qry = "Select * from Task_Shedule where pn_CompanyID="+ e.CompanyId +" and pn_BranchID="+ e.BranchId +"";
            string qry = "select a.*, b.EmployeeCode, b.Employee_First_Name from Task_Shedule a, paym_Employee b where a.pn_CompanyID=" + e.CompanyId + " and a.pn_BranchID=" + e.BranchId + " and a.pn_CompanyID=b.pn_CompanyID and a.pn_BranchID=b.pn_BranchID and a.pn_EmployeeID=b.pn_EmployeeID";
            SqlCommand cmd = new SqlCommand(qry, _Connection);
            _Connection.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                Employee emp = new Employee();
                emp.EmployeeId = (int)dr["pn_EmployeeID"];
                emp.EmployeeCode = Convert.IsDBNull(dr["EmployeeCode"]) ? "" : dr["EmployeeCode"].ToString();
                emp.FirstName = Convert.IsDBNull(dr["Employee_First_Name"]) ? "" : dr["Employee_First_Name"].ToString();
                emp.TaskSubject = Convert.IsDBNull(dr["TaskSubject"]) ? "" : dr["TaskSubject"].ToString();
                emp.TaskDescription = Convert.IsDBNull(dr["TaskDescription"]) ? "" : dr["TaskDescription"].ToString();
                emp.Priority = Convert.IsDBNull(dr["Priority"]) ? Convert.ToChar("") : Convert.ToChar(dr["Priority"]);
                emp.status = Convert.IsDBNull(dr["Status"]) ? Convert.ToChar("") : Convert.ToChar(dr["Status"]);
                emp.d_Date = Convert_ToIISDate(Convert.IsDBNull(dr["DateofCompletion"]) ? "" : dr["DateofCompletion"].ToString());
                emp.LastName = emp.EmployeeCode + "-" + emp.FirstName;
                EmpTask.Add(emp);
            }
            _Connection.Close();
            return EmpTask;
        }        
    }
}
