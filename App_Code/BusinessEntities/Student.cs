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


namespace ePayHrms.Student
{
    public class Student
    {
        public Student()
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
        private int _ClassId;
        private string _ClassName;
        private int _DepartmentId;
        private string _DepartmentName;
        private int _CourseId;
        private string _CourseName;
        private int _SkillId;
        private string _SkillName;
        private string _Location;
        private int _CategoryId;
        private string _CategoryName;
        private int _Amount;
        private DateTime _Date;
        private char _status;
        private string _status21;
        private string _Incharge_name;
        private string _RegisterNo;
        private string _RollNo;
        private string _Section;
        private string _Cyear;
        private string _Phone;
        private string _IPAddr;
        private string _MacNo;
        private string _DurationFrom;
        private string _DurationTo;

        public string Incharge_name
        {
            get { return _Incharge_name; }
            set { _Incharge_name = value; }
        }

        public string RegisterNo
        {
            get { return _RegisterNo; }
            set { _RegisterNo = value; }
        }

        public string RollNo
        {
            get { return _RollNo; }
            set { _RollNo = value; }
        }

        public string Section
        {
            get { return _Section; }
            set { _Section = value; }
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

        public string Cyear
        {
            get { return _Cyear; }
            set { _Cyear = value; }
        }

        public string Phone
        {
            get { return _Phone; }
            set { _Phone = value; }
        }

        public string IPAddr
        {
            get { return _IPAddr; }
            set { _IPAddr = value; }
        }

        public string MacNo
        {
            get { return _MacNo; }
            set { _MacNo = value; }
        }

        //^^^^^^^^^^^^^^^^^Student^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^

        private int _StudentId;
        private string _StudentCode;
        private string _FirstName;
        private string _FullName;
        private string _MiddleName;
        private string _LastName;
        private string _img_path;
        private string _Gender;


        
        private DateTime _d_birth;
        private DateTime _d_join;


        private string _EmailId;
        private string _BloodGroup;
        private string _Religion;
        private string _Nationality;
        private string _HouseNo;
        private string _StreetName;
        private string _AddressLine1;
        private string _AddressLine2;
        private string _City;
        private string _State;
       
        private string _PGCourseName;
        private string _PGSpecialaization;
        private string _PGInstutionName;
        private string _PGPercentage;
        private string _PGCompletedYear;
        private string _PGCompletedinf;

        private string _ph_Office;
        private string _ph_Residence;
       
        private char _Salutation;
        private char _MaritalStatus;
        private string _FatherName;
        private string _MotherName;
        private string _SpouseName;
        private string _Children;
        
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

        private string _Flag;

        public string Flag
        {
            get { return _Flag; }
            set { _Flag = value; }
        }

        public string Children
        {
            get { return _Children; }
            set { _Children = value; }
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

        public string Pan_no
        {
            get { return _PanNo; }
            set { _PanNo = value; }
        }




        //******************************** Training ******************************************************


        
        private string _Routes;
        
        private string _Boarding_point;
        private int _Driver_id;

        public int Driver_id
        {
            get { return _Driver_id; }
            set { _Driver_id = value; }
        }
        public string Boarding_point
        {
            get { return _Boarding_point; }
            set { _Boarding_point = value; }
        }

        public string Routes
        {
            get { return _Routes; }
            set { _Routes = value; }
        }
 

       
        //######################################################################################

        public string Gender
        {
            get { return _Gender; }
            set { _Gender = value; }
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

    
        public int DepartmentId
        {
            get { return _DepartmentId; }
            set { _DepartmentId = value; }
        }

    
        public string DepartmentName
        {
            get { return _DepartmentName; }
            set { _DepartmentName = value; }
        }


        public int ClassId
        {
            get { return _ClassId; }
            set { _ClassId = value; }
        }


        public string ClassName
        {
            get { return _ClassName; }
            set { _ClassName = value; }
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
     
        public string Location
        {
            get { return _Location; }
            set { _Location = value; }
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

        private int _AcademicYear;

        public int AcademicYear
        {
            get { return _AcademicYear; }
            set { _AcademicYear = value; }
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

        private DateTime _Dob;

        public DateTime DOB
        {
            get { return _Dob; }
            set { _Dob = value; }
        }

        private TimeSpan _Intimestr;
        
        public TimeSpan Intimestr
        {
            get { return _Intimestr; }
            set { _Intimestr = value; }
        }
        private TimeSpan ot_hrs;

        public TimeSpan _ot_hrs
        {
            get { return _ot_hrs; }
            set { _ot_hrs = value; }
        }

        private TimeSpan _Outtimestr;

        public TimeSpan Outtimestr
        {
            get { return _Outtimestr; }
            set { _Outtimestr = value; }
        }

        private TimeSpan _Lateinstr;

        public TimeSpan Lateinstr
        {
            get { return _Lateinstr; }
            set { _Lateinstr = value; }
        }

        private TimeSpan _Lateoutstr;

        public TimeSpan Lateoutstr
        {
            get { return _Lateoutstr; }
            set { _Lateoutstr = value; }
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

        private DateTime _AdmissionDate;

        public DateTime AdmissionDate
        {
            get { return _AdmissionDate; }
            set { _AdmissionDate = value; }
        }

        public int StudentId
        {
            get { return _StudentId; }
            set { _StudentId = value; }
        }

        public string StudentCode
        {
            get { return _StudentCode; }
            set { _StudentCode = value; }
        }

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

        public string EmailId
        {
            get { return _EmailId; }
            set { _EmailId = value; }
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

        public string img_path
        {
            get { return _img_path; }
            set { _img_path = value; }

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

        public char status
        {
            get { return _status; }
            set { _status = value; }
        }

        public string status21
        {
            get { return _status21; }
            set { _status21 = value; }
        }

        private int _ReaderId;

        public int ReaderId
        {
            get { return _ReaderId; }
            set { _ReaderId = value; }
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

        private string _Residence;

        public string Residence
        {
            get { return _Residence; }
            set { _Residence = value; }
        }

        private string _AdmissionType;

        public string AdmissionType
        {
            get { return _AdmissionType; }
            set { _AdmissionType = value; }
        }

        private string _Community;

        public string Community
        {
            get { return _Community; }
            set { _Community = value; }
        }

        private string _Language;

        public string Language
        {
            get { return _Language; }
            set { _Language = value; }
        }

        private string _District;

        public string District
        {
            get { return _District; }
            set { _District = value; }
        }

        private string _Country;

        public string Country
        {
            get { return _Country; }
            set { _Country = value; }
        }

        private DateTime _dat;

        public DateTime dat
        {
            get { return _dat; }
            set { _dat = value; }
        }

        public string e_Date { get; set; }
        public string e_date { get; set; }

        public Collection<Student> fn_getStudentList(Student e)
        {
            Collection<Student> StudentList = new Collection<Student>();
            _Connection = Con.fn_Connection();
            SqlCommand _Course = new SqlCommand("select * from paym_Student where pn_CompanyID=" + e.CompanyId + " and pn_BranchID =" + e.BranchId + " and AcademicYear='" + e.AcademicYear + "' and status='Y' order by RegisterNo", _Connection);
            _Connection.Open();
            SqlDataReader dr = _Course.ExecuteReader();
            while (dr.Read())
            {
                Student Student = new Student();
                Student.StudentId = (int)dr["pn_StudentID"];
                Student.RegisterNo = (string)dr["RegisterNo"];
                Student.FirstName = (string)dr["StudentName"];
                Student.FullName = (string)dr["RegisterNo"] + " - " + (string)dr["StudentName"];
                StudentList.Add(Student);
            }
            return StudentList;
        }

        public Collection<Student> fn_getStudentList_Currentyear(Student e)
        {
            Collection<Student> StuList = new Collection<Student>();
            _Connection = Con.fn_Connection();
            SqlCommand cmd = new SqlCommand("select * from paym_Student where pn_CompanyID=" + e.CompanyId + " and pn_BranchID =" + e.BranchId + "and ClassName='" + e.ClassName + "' and Department='" + e.DepartmentName + "'and Section='" + e.Section + "' and CurrentYear='" + e.Cyear + "' and Status = 'Y'", _Connection);
            _Connection.Open();
            SqlDataReader read = cmd.ExecuteReader();
            while (read.Read())
            {
                Student List = new Student();
                List.RegisterNo = read["RegisterNo"].ToString();
                List.FirstName = read["RegisterNo"].ToString() + " - " + read["StudentName"].ToString();
                List.LastName = read["StudentName"].ToString();
                StuList.Add(List);
            }
            return StuList;
        }

        public Collection<Student> fn_getStudentList_Department(Student e)
        {
            Collection<Student> StuList = new Collection<Student>();
            _Connection = Con.fn_Connection();
            SqlCommand cmd = new SqlCommand("select * from paym_Student where pn_CompanyID=" + e.CompanyId + " and pn_BranchID =" + e.BranchId + " and Department='" + e.DepartmentName + "' and CurrentYear='" + e.Cyear + "' and Status = 'Y'", _Connection);
            _Connection.Open();
            SqlDataReader read = cmd.ExecuteReader();
            while (read.Read())
            {
                Student List = new Student();
                List.RegisterNo = read["RegisterNo"].ToString();
                List.FirstName = read["RegisterNo"].ToString() + " - " + read["StudentName"].ToString();
                List.LastName = read["StudentName"].ToString();
                StuList.Add(List);
            }
            return StuList;
        }

        public Collection<Student> fn_getStudentTimeCard(Student e)
        {
            Collection<Student> StudentList = new Collection<Student>();
            _Connection = Con.fn_Connection();
            SqlCommand _Course = new SqlCommand("select * from paym_Student where pn_CompanyID=" + e.CompanyId + " and pn_BranchID =" + e.BranchId + " and Department = '" + e.DepartmentName + "' and Section = '" + e.Section + "' and CurrentYear= '" + e.Cyear + "' and status='Y' order by RegisterNo", _Connection);
            _Connection.Open();
            SqlDataReader dr = _Course.ExecuteReader();
            while (dr.Read())
            {
                Student Student = new Student();
                Student.StudentId = (int)dr["pn_StudentID"];
                Student.RegisterNo = (string)dr["RegisterNo"];
                Student.FirstName = (string)dr["StudentName"];
                Student.FullName = (string)dr["RegisterNo"] + " - " + (string)dr["StudentName"];
                StudentList.Add(Student);
            }
            return StudentList;
        }
        public Collection<Student> fn_calc(Student e)
        {
            Collection<Student> StudentList = new Collection<Student>();
            _Connection = Con.fn_Connection();
            SqlCommand _Course = new SqlCommand("select * from time_card_student where pn_CompanyID=" + e.CompanyId + " and pn_BranchID =" + e.BranchId + " and dates = '" + e.d_Date + "' and registerno = '" + e.RegisterNo + "' order by RegisterNo asc", _Connection);
            _Connection.Open();
            SqlDataReader dr = _Course.ExecuteReader();
            while (dr.Read())
            {
                Student Student = new Student();
                Student.FirstName = Convert.IsDBNull(dr["StudentName"]) ? "" : (string)dr["StudentName"];
                Student.ot_hrs = TimeSpan.Parse(dr["ot_hrs"].ToString());
                //Student.Intimestr = TimeSpan.Parse(dr["intime"].ToString());
                //Student.Outtimestr = TimeSpan.Parse(dr["outtime"].ToString());  //Convert.IsDBNull(dr["outtime"]) ? "" : (string)dr["outtime"];
                //Student.Lateinstr = TimeSpan.Parse(dr["Late_in"].ToString());  //Convert.IsDBNull(dr["Late_in"]) ? "" : (string)dr["Late_in"];
                //Student.Lateoutstr = TimeSpan.Parse(dr["Late_out"].ToString());  //Convert.IsDBNull(dr["Late_out"]) ? "" : (string)dr["Late_out"];
                Student.Flag = Convert.IsDBNull(dr["status"]) ? "" : (string)dr["status"];
                StudentList.Add(Student);
            }
            return StudentList;
        }
        public Collection<Student> fn_StudentTimeCard(Student e)
        {
            Collection<Student> StudentList = new Collection<Student>();
            _Connection = Con.fn_Connection();
            SqlCommand _Course = new SqlCommand("select * from time_card_student where pn_CompanyID=" + e.CompanyId + " and pn_BranchID =" + e.BranchId + " and dates = '" + e.d_Date + "' and registerno = '" + e.RegisterNo + "' order by RegisterNo asc", _Connection);
            _Connection.Open();
            SqlDataReader dr = _Course.ExecuteReader();
            while (dr.Read())
            {
                Student Student = new Student();
                Student.FirstName = Convert.IsDBNull(dr["StudentName"]) ? "" : (string)dr["StudentName"];
                Student.Intimestr = TimeSpan.Parse(dr["intime"].ToString());
                Student.Outtimestr = TimeSpan.Parse(dr["outtime"].ToString());  //Convert.IsDBNull(dr["outtime"]) ? "" : (string)dr["outtime"];
                Student.Lateinstr = TimeSpan.Parse(dr["Late_in"].ToString());  //Convert.IsDBNull(dr["Late_in"]) ? "" : (string)dr["Late_in"];
                Student.Lateoutstr = TimeSpan.Parse(dr["Late_out"].ToString());  //Convert.IsDBNull(dr["Late_out"]) ? "" : (string)dr["Late_out"];
                Student.Flag = Convert.IsDBNull(dr["status"]) ? "" : (string)dr["status"];
                StudentList.Add(Student);
            }
            return StudentList;
        }

        public Collection<Student> fn_StudentConsolidate(Student e)
        {
            Collection<Student> EmployeeList = new Collection<Student>();
            _Connection = Con.fn_Connection();
            SqlCommand _Course = new SqlCommand("select * from time_card_student where pn_CompanyID=" + e.CompanyId + " and pn_BranchID =" + e.BranchId + " and dates >= '" + e.DurationFrom + "' and dates <='" + e.DurationTo + "'  and RegisterNo = '" + e.RegisterNo + "' order by RegisterNo asc", _Connection);
            _Connection.Open();
            SqlDataReader dr = _Course.ExecuteReader();
            while (dr.Read())
            {
                Student employee = new Student();
                employee.FirstName = Convert.IsDBNull(dr["StudentName"]) ? "" : (string)dr["StudentName"];
                employee.RegisterNo = Convert.IsDBNull(dr["RegisterNo"]) ? "" : (string)dr["RegisterNo"];
                employee.Date = Convert.IsDBNull(dr["dates"]) ? DateTime.Now : Convert.ToDateTime(dr["dates"]);
                employee.Intimestr = TimeSpan.Parse(dr["intime"].ToString());
                employee.Outtimestr = TimeSpan.Parse(dr["outtime"].ToString());  //Convert.IsDBNull(dr["outtime"]) ? "" : (string)dr["outtime"];
                employee.Lateinstr = TimeSpan.Parse(dr["Late_in"].ToString());  //Convert.IsDBNull(dr["Late_in"]) ? "" : (string)dr["Late_in"];
                employee.Lateoutstr = TimeSpan.Parse(dr["Late_out"].ToString());  //Convert.IsDBNull(dr["Late_out"]) ? "" : (string)dr["Late_out"];
                //employee.Intime = Convert.IsDBNull(dr["intime"]) ? DateTime.Now : Convert.ToDateTime(dr["intime"]);
                //employee.Outtime = Convert.IsDBNull(dr["outtime"]) ? DateTime.Now : Convert.ToDateTime(dr["outtime"]);
                //employee.Latein = Convert.IsDBNull(dr["late_in"]) ? DateTime.Now : Convert.ToDateTime(dr["late_in"]);
                //employee.Lateout = Convert.IsDBNull(dr["late_out"]) ? DateTime.Now : Convert.ToDateTime(dr["late_out"]);
                employee.Flag = Convert.IsDBNull(dr["status"]) ? "" : (string)dr["status"];
                employee.status21 = Convert.IsDBNull(dr["leave_code"]) ? "" : (string)dr["leave_code"];
                EmployeeList.Add(employee);
            }
            return EmployeeList;
        }

        public Collection<Student> fn_course(int id)
        {
            Collection<Student> courseList = new Collection<Student>();
            _Connection = Con.fn_Connection();
            string _SqlString = "select * from Student_Course where pn_BranchID = '" + id + "' and pn_CourseID != 1 order by pn_CourseID Asc";
            SqlCommand _SScourse = new SqlCommand(_SqlString, _Connection);
            _Connection.Open();
            SqlDataReader dr_course = _SScourse.ExecuteReader();
            while (dr_course.Read())
            {
                Student employee = new Student();
                employee.CourseId = (int)dr_course["pn_courseID"];
                employee.CourseName = Convert.IsDBNull(dr_course["courseName"]) ? "" : (string)dr_course["courseName"];
                courseList.Add(employee);
            }
            return courseList;
        }

        public Collection<Student> fn_department(int id)
        {
            Collection<Student> DeptList = new Collection<Student>();
            _Connection = Con.fn_Connection();
            string _SqlString = "select * from Student_Department where pn_BranchID = '" + id + "' and pn_DepartmentID != 1 order by pn_DepartmentID Asc";
            SqlCommand _SScourse = new SqlCommand(_SqlString, _Connection);
            _Connection.Open();
            SqlDataReader dr_course = _SScourse.ExecuteReader();
            while (dr_course.Read())
            {
                Student employee = new Student();
                employee.DepartmentId = (int)dr_course["pn_DepartmentID"];
                employee.DepartmentName = Convert.IsDBNull(dr_course["DepartmentName"]) ? "" : (string)dr_course["DepartmentName"];
                DeptList.Add(employee);
            }
            return DeptList;
        }

        public Collection<Student> fn_Class(int id)
        {
            Collection<Student> ClassList = new Collection<Student>();
            _Connection = Con.fn_Connection();
            string _SqlString = "select * from Student_Class where pn_BranchID = '" + id + "'";
            SqlCommand _SScourse = new SqlCommand(_SqlString, _Connection);
            _Connection.Open();
            SqlDataReader dr_course = _SScourse.ExecuteReader();
            while (dr_course.Read())
            {
                Student employee = new Student();
                employee.ClassId = (int)dr_course["pn_ClassID"];
                employee.CourseName = Convert.IsDBNull(dr_course["CourseName"]) ? "" : (string)dr_course["CourseName"];
                employee.DepartmentName = Convert.IsDBNull(dr_course["DepartmentName"]) ? "" : (string)dr_course["DepartmentName"];
                employee.DivisionName = Convert.IsDBNull(dr_course["Section"]) ? "" : (string)dr_course["Section"];
                employee.PGCompletedYear = Convert.IsDBNull(dr_course["Year"]) ? "" : (string)dr_course["Year"];
                employee.ClassName = Convert.IsDBNull(dr_course["ClassName"]) ? "" : (string)dr_course["ClassName"];
                ClassList.Add(employee);
            }
            return ClassList;
        }

        public Collection<Student> fn_GetMachine(int id)
        {
            Collection<Student> MacList = new Collection<Student>();
            _Connection = Con.fn_Connection();
            string _SqlString = "select * from Machine where pn_BranchID = '" + id + "'";
            SqlCommand _SScourse = new SqlCommand(_SqlString, _Connection);
            _Connection.Open();
            SqlDataReader dr_course = _SScourse.ExecuteReader();
            while (dr_course.Read())
            {
                Student employee = new Student();
                employee.IPAddr = Convert.IsDBNull(dr_course["IPAddr"]) ? "" : (string)dr_course["IPAddr"];
                employee.MacNo = Convert.IsDBNull(dr_course["MNo"]) ? "" : (string)dr_course["MNo"];
                MacList.Add(employee);
            }
            return MacList;
        }

        public Collection<Student> fn_getStudent(Student e)
        {
            Collection<Student> StudentList = new Collection<Student>();
            _Connection = Con.fn_Connection();
            SqlCommand _Course = new SqlCommand("select * from paym_Student where pn_CompanyID=" + e.CompanyId + " and pn_BranchID =" + e.BranchId + " and RegisterNo = '" + e.RegisterNo + "' order by RegisterNo", _Connection);
            _Connection.Open();
            SqlDataReader dr = _Course.ExecuteReader();
            if (dr.Read())
            {
                Student Student = new Student();
                Student.StudentId = (int)dr["pn_StudentID"];
                Student.FirstName = (string)dr["StudentName"];
                Student.RegisterNo = (string)dr["RegisterNo"];
                Student.RollNo = Convert.IsDBNull(dr["RollNo"]) ? "" : (string)dr["RollNo"];
                Student.DOB = Convert.IsDBNull(dr["DateofBirth"]) ? Convert.ToDateTime("1900/01/01") : Convert.ToDateTime(dr["DateofBirth"]);
                Student.Gender = Convert.IsDBNull(dr["Gender"]) ? "" : (string)dr["Gender"];
                Student.AdmissionDate = Convert.IsDBNull(dr["AdmissionDate"]) ? Convert.ToDateTime("1900/01/01") : Convert.ToDateTime(dr["AdmissionDate"]);
                Student.AcademicYear = Convert.IsDBNull(dr["AcademicYear"]) ? 0 : Convert.ToInt32(dr["AcademicYear"]);
                Student.CourseName = Convert.IsDBNull(dr["ClassName"]) ? "" : (string)dr["ClassName"];
                Student.DepartmentName = Convert.IsDBNull(dr["Department"]) ? "" : (string)dr["Department"];
                Student.Section = Convert.IsDBNull(dr["Section"]) ? "" : (string)dr["Section"];
                Student.Cyear = Convert.IsDBNull(dr["CurrentYear"]) ? "" : (string)dr["CurrentYear"];
                Student.Phone = Convert.IsDBNull(dr["Phone"]) ? "" : (string)dr["Phone"];
                Student.Residence = Convert.IsDBNull(dr["Place"]) ? "" : (string)dr["Place"];
                Student.BloodGroup = Convert.IsDBNull(dr["BloodGroup"]) ? "" : (string)dr["BloodGroup"];
                Student.EmailId = Convert.IsDBNull(dr["EmailID"]) ? "" : (string)dr["EmailID"];
                Student.Incharge_name = Convert.IsDBNull(dr["FacultyAdvisor"]) ? "" : (string)dr["FacultyAdvisor"];
                Student.AdmissionType = Convert.IsDBNull(dr["AdmissionType"]) ? "" : (string)dr["AdmissionType"];
                Student.Amount = Convert.IsDBNull(dr["InstitutionFee"]) ? 0 : Convert.ToInt32(dr["InstitutionFee"]);
                Student.Community = Convert.IsDBNull(dr["Community"]) ? "" : (string)dr["Community"];
                Student.Religion = Convert.IsDBNull(dr["Religion"]) ? "" : (string)dr["Religion"];
                Student.Nationality = Convert.IsDBNull(dr["Nationality"]) ? "" : (string)dr["Nationality"];
                Student.Boarding_point = Convert.IsDBNull(dr["BusDetail"]) ? "" : (string)dr["BusDetail"];
                Student.ReaderId = Convert.IsDBNull(dr["ReaderID"]) ? 0 : Convert.ToInt32(dr["ReaderID"]);
                Student.Bank_Code = Convert.IsDBNull(dr["BankAC"]) ? "" : (string)dr["BankAC"];
                Student.FatherName = Convert.IsDBNull(dr["FatherName"]) ? "" : (string)dr["FatherName"];
                Student.MotherName = Convert.IsDBNull(dr["MotherName"]) ? "" : (string)dr["MotherName"];
                Student.Language = Convert.IsDBNull(dr["MotherTongue"]) ? "" : (string)dr["MotherTongue"];
                Student.AddressLine1 = Convert.IsDBNull(dr["Address1"]) ? "" : (string)dr["Address1"];
                Student.AddressLine2 = Convert.IsDBNull(dr["Address2"]) ? "" : (string)dr["Address2"];
                Student.City = Convert.IsDBNull(dr["City"]) ? "" : (string)dr["City"];
                Student.State = Convert.IsDBNull(dr["State"]) ? "" : (string)dr["State"];
                Student.District = Convert.IsDBNull(dr["District"]) ? "" : (string)dr["District"];
                Student.Country = Convert.IsDBNull(dr["Country"]) ? "" : (string)dr["Country"];
                Student.status21 = Convert.IsDBNull(dr["Status"]) ? "" : (string)dr["Status"];
                Student.ph_Residence = Convert.IsDBNull(dr["ParentsContact"]) ? "" : (string)dr["ParentsContact"];
                
                StudentList.Add(Student);
            }
            return StudentList;
        }



        public string Student_Details(Student e)
        {
            try
            {
                _Connection = Con.fn_Connection();
                SqlCommand _Cmd = new SqlCommand("sp_paym_Student", _Connection);
                _Cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter[] _ISPParamter = new SqlParameter[37];
                _ISPParamter[0] = new SqlParameter("@pn_CompanyID", SqlDbType.Int);
                _ISPParamter[0].Value = e.CompanyId;
                _ISPParamter[1] = new SqlParameter("@pn_BranchID", SqlDbType.Int);
                _ISPParamter[1].Value = e.BranchId;
                _ISPParamter[2] = new SqlParameter("@RollNo", SqlDbType.VarChar);
                _ISPParamter[2].Value = e.RollNo;
                _ISPParamter[3] = new SqlParameter("@RegisterNo", SqlDbType.VarChar);
                _ISPParamter[3].Value = e.RegisterNo;
                _ISPParamter[4] = new SqlParameter("@StudentName", SqlDbType.VarChar);
                _ISPParamter[4].Value = e.FirstName;
                _ISPParamter[5] = new SqlParameter("@DateofBirth", SqlDbType.DateTime);
                _ISPParamter[5].Value = e.DOB;
                _ISPParamter[6] = new SqlParameter("@Gender", SqlDbType.VarChar);
                _ISPParamter[6].Value = e.Gender;
                _ISPParamter[7] = new SqlParameter("@AdmissionDate", SqlDbType.DateTime);
                _ISPParamter[7].Value = e.AdmissionDate;
                _ISPParamter[8] = new SqlParameter("@ReaderID", SqlDbType.Int);
                _ISPParamter[8].Value = e.ReaderId;
                _ISPParamter[9] = new SqlParameter("@AcademicYear", SqlDbType.Int);
                _ISPParamter[9].Value = e.AcademicYear;
                _ISPParamter[10] = new SqlParameter("@ClassName", SqlDbType.VarChar);
                _ISPParamter[10].Value = e.CourseName;
                _ISPParamter[11] = new SqlParameter("@Department", SqlDbType.VarChar);
                _ISPParamter[11].Value = e.DepartmentName;
                _ISPParamter[12] = new SqlParameter("@Section", SqlDbType.VarChar);
                _ISPParamter[12].Value = e.Section;
                _ISPParamter[13] = new SqlParameter("@CurrentYear", SqlDbType.VarChar);
                _ISPParamter[13].Value = e.Cyear;
                _ISPParamter[14] = new SqlParameter("@Phone", SqlDbType.VarChar);
                _ISPParamter[14].Value = e.Phone;
                _ISPParamter[15] = new SqlParameter("@Place", SqlDbType.VarChar);
                _ISPParamter[15].Value = e.Residence;
                _ISPParamter[16] = new SqlParameter("@BloodGroup", SqlDbType.VarChar);
                _ISPParamter[16].Value = e.BloodGroup;
                _ISPParamter[17] = new SqlParameter("@EmailID", SqlDbType.VarChar);
                _ISPParamter[17].Value = e.EmailId;
                _ISPParamter[18] = new SqlParameter("@FacultyAdvisor", SqlDbType.VarChar);
                _ISPParamter[18].Value = e.Incharge_name;
                _ISPParamter[19] = new SqlParameter("@Mothertongue", SqlDbType.VarChar);
                _ISPParamter[19].Value = e.Language;
                _ISPParamter[20] = new SqlParameter("@AdmissionType", SqlDbType.VarChar);
                _ISPParamter[20].Value = e.AdmissionType;
                _ISPParamter[21] = new SqlParameter("@InstitutionFee", SqlDbType.Int);
                _ISPParamter[21].Value = e.Amount;
                _ISPParamter[22] = new SqlParameter("@Community", SqlDbType.VarChar);
                _ISPParamter[22].Value = e.Community;
                _ISPParamter[23] = new SqlParameter("@Religion", SqlDbType.VarChar);
                _ISPParamter[23].Value = e.Religion;
                _ISPParamter[24] = new SqlParameter("@Nationality", SqlDbType.VarChar);
                _ISPParamter[24].Value = e.Nationality;
                _ISPParamter[25] = new SqlParameter("@BusDetail", SqlDbType.VarChar);
                _ISPParamter[25].Value = e.Boarding_point;
                _ISPParamter[26] = new SqlParameter("@BankAC", SqlDbType.VarChar);
                _ISPParamter[26].Value = e.Bank_Code;
                _ISPParamter[27] = new SqlParameter("@FatherName", SqlDbType.VarChar);
                _ISPParamter[27].Value = e.FatherName;
                _ISPParamter[28] = new SqlParameter("@MotherName", SqlDbType.VarChar);
                _ISPParamter[28].Value = e.MotherName;
                _ISPParamter[29] = new SqlParameter("@Address1", SqlDbType.VarChar);
                _ISPParamter[29].Value = e.AddressLine1;
                _ISPParamter[30] = new SqlParameter("@Address2", SqlDbType.VarChar);
                _ISPParamter[30].Value = e.AddressLine2;
                _ISPParamter[31] = new SqlParameter("@City", SqlDbType.VarChar);
                _ISPParamter[31].Value = e.City;
                _ISPParamter[32] = new SqlParameter("@State", SqlDbType.VarChar);
                _ISPParamter[32].Value = e.State;
                _ISPParamter[33] = new SqlParameter("@District", SqlDbType.VarChar);
                _ISPParamter[33].Value = e.District;
                _ISPParamter[34] = new SqlParameter("@Country", SqlDbType.VarChar);
                _ISPParamter[34].Value = e.Country;
                _ISPParamter[35] = new SqlParameter("@ParentsContact", SqlDbType.VarChar);
                _ISPParamter[35].Value = e.ph_Residence;
                _ISPParamter[36] = new SqlParameter("@Status", SqlDbType.Char);
                _ISPParamter[36].Value = e.status;

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


    }
}