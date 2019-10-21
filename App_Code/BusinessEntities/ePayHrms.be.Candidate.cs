using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using ePayHrms.Connection;


namespace ePayHrms.Candidate
{
    /// <summary>
    /// Summary description for ePayHrms
    /// </summary>
    public class Candidate
    {
        public Candidate()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        private SqlConnection _Connection;
        ePayHrms.Connection.Connection Con = new ePayHrms.Connection.Connection();

        private int _CompanyID;

        private int _BranchID;
        private int _EmployeeID;
               
        
        private int _CourseID;
        private string _CourseName;
        


        private int _InstitutionID;
        private string _InstitutionName;

        private int _FunctionalAreaID;
        private string _FunctionalAreaName;
        


        private int _CandidateID;
        private string _ProfileUsername;
        private string _Password;
        private string _Username;
        private string _CandidateName;
        private string _CandidateCode;
        private string _FatherHusbandName;
        //private string _EmployeeCode;
                                                private char _RecruitSource;
        private string _DOB;
        private string _AddressLine1;
        private string _AddressLine2;
        private string _City;
        private string _Country;
        private string _State;
                                                private char _Gender;
                                                private char _MaritialStatus;
        private string _PhoneResidence;
        private string _PhoneOffice;
        private string _PhoneMobile;
        private string _Email;
        private string _KeySkills;
        private string _ResumeHeadline;
        private string _Summary;
        private string _Domain;
        private string _TotalExperience;
        private string _RelevantExperience;
        private int _JobStatusID;
        private string _JobStatusName;


        private string _CompletedMonth;
        private int _CompletedYear;
        private string _Specialisation;
        private string _Percentage;


        private int _WorkHistorySeqID;
        private string _WorkHistoryFromDateMonth;
        private int _WorkHistoryFromDateYear;
        private string _WorkHistoryToDateMonth;
        private int _WorkHistoryToDateYear;
        private string _CompanyName;
        private string _CompanyLocation;
        private string _DesignationCode;
        private string _Salary;
        private string _Role;
        private string _Responsibility;


        private int _SkillID;
        private string _SkillName;
        private int _LastUsedYear;
        private string _LastUsedMonth;
        private string _ProficiencyLevel;
        private string _Experience;


        private int _ProjectSeqID;
        private string _ProjectTitle;
        private string _ClientName;
        private string _ProjectFromDateMonth;
        private int _ProjectFromDateYear;
        private string _ProjectToDateMonth;
        private int _ProjectToDateYear;
        private string _ProjectLocation;
                                                private char _ImplementationType;
        private string _Projectdescription;
        private string _ProjectRole;
        private string _ProjectDesignation;
        private int _TeamSize;
        private string _SkillsUsed;


        private int _ReferenceID;
        private string _ReferencePerson;
        private string _ReferenceCompany;
        private string _ReferenceDesignation;
        private string _Relationship;
        private string _ReferencePhone;
        private string _ReferenceEmail;


        private int _LanguageSeqID;
        private string _LanguageName;
                                                private char _Read;
                                                private char _Write;
                                                private char _Speak;

        private int _QuestionID;
        private string _Question;

        private int _total_exp;

        //-----------------------------------------------------------------------------

        public int total_exp
        {
            get { return _total_exp; }
            set { _total_exp = value; }
        }


        public int CompanyID
        {
            get { return _CompanyID; }
            set { _CompanyID = value; }
        }

        public int BranchID
        {
            get { return _BranchID; }
            set { _BranchID = value; }
        }



        public int EmployeeID
        {
            get { return _EmployeeID; }
            set { _EmployeeID = value; }
        }


        public int CourseID
        {
            get { return _CourseID; }
            set { _CourseID = value; }
        }

        public string CourseName
        {
            get { return _CourseName; }
            set { _CourseName = value; }
        }

        public int InstitutionID
        {
            get { return _InstitutionID; }
            set { _InstitutionID = value; }
        }

        public string InstitutionName
        {
            get { return _InstitutionName; }
            set { _InstitutionName = value; }
        }

        public int FunctionalAreaID
        {
            get { return _FunctionalAreaID; }
            set { _FunctionalAreaID = value; }
        }

        public string FunctionalAreaName
        {
            get { return _FunctionalAreaName; }
            set { _FunctionalAreaName = value; }
        }

        public int CandidateID
        {
            get { return _CandidateID; }
            set { _CandidateID = value; }
        }

        public string ProfileUsername
        {
            get { return _ProfileUsername; }
            set { _ProfileUsername = value; }
        }

        public string Username
        {
            get { return _Username; }
            set { _Username = value; }
        }

        public string Password
        {
            get { return _Password; }
            set { _Password = value; }
        }
                
        public string CandidateName
        {
            get { return _CandidateName; }
            set { _CandidateName = value; }
        }

        public string CandidateCode
        {
            get { return _CandidateCode; }
            set { _CandidateCode = value; }
        }

        public string FatherHusbandName
        {
            get { return _FatherHusbandName; }
            set { _FatherHusbandName = value; }
        }

        public string DOB
        {
            get { return _DOB; }
            set { _DOB = value; }
        }

       // public string EmployeeCode
       // {
       //    get { return _EmployeeCode; }
       //   set { _EmployeeCode = value; }
       // }

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

        public string Country
        {
            get { return _Country; }
            set { _Country = value; }
        }

        public string State
        {
            get { return _State; }
            set { _State = value; }
        }
        public char RecruitSource
        {
            get { return _RecruitSource; }
            set { _RecruitSource = value; }
        }

        public char Gender
        {
            get { return _Gender; }
            set { _Gender = value; }
        }

        public char MaritialStatus
        {
            get { return _MaritialStatus; }
            set { _MaritialStatus = value; }
        }                                                    
                   
        public string PhoneResidence
        {
            get { return _PhoneResidence; }
            set { _PhoneResidence = value; }
        }

        public string PhoneOffice
        {
            get { return _PhoneOffice; }
            set { _PhoneOffice = value; }
        }

        public string PhoneMobile
        {
            get { return _PhoneMobile; }
            set { _PhoneMobile = value; }
        }

        public string Email
        {
            get { return _Email; }
            set { _Email = value; }
        }

        public string KeySkills
        {
            get { return _KeySkills; }
            set { _KeySkills = value; }
        }

        public string ResumeHeadline
        {
            get { return _ResumeHeadline; }
            set { _ResumeHeadline = value; }
        }

        public string Summary
        {
            get { return _Summary; }
            set { _Summary = value; }
        }

        public string Domain
        {
            get { return _Domain; }
            set { _Domain = value; }
        }

        public string TotalExperience
        {
            get { return _TotalExperience; }
            set { _TotalExperience = value; }
        }

        public string RelevantExperience
        {
            get { return _RelevantExperience; }
            set { _RelevantExperience = value; }
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

        public string CompletedMonth
        {
            get { return _CompletedMonth; }
            set { _CompletedMonth = value; }
        }

        public int CompletedYear
        {
            get { return _CompletedYear; }
            set { _CompletedYear = value; }
        }

        public string Specialisation
        {
            get { return _Specialisation; }
            set { _Specialisation = value; }
        }

        public string Percentage
        {
            get { return _Percentage; }
            set { _Percentage = value; }
        }

        public int WorkHistorySeqID
        {
            get { return _WorkHistorySeqID; }
            set { _WorkHistorySeqID = value; }
        }

        public string WorkHistoryFromDateMonth
        {
            get { return _WorkHistoryFromDateMonth; }
            set { _WorkHistoryFromDateMonth = value; }
        }

        public int WorkHistoryFromDateYear
        {
            get { return _WorkHistoryFromDateYear; }
            set { _WorkHistoryFromDateYear = value; }
        }

        public string WorkHistoryToDateMonth
        {
            get { return _WorkHistoryToDateMonth; }
            set { _WorkHistoryToDateMonth = value; }
        }

        public int WorkHistoryToDateYear
        {
            get { return _WorkHistoryToDateYear; }
            set { _WorkHistoryToDateYear = value; }
        }

        public string CompanyName
        {
            get { return _CompanyName; }
            set { _CompanyName = value; }
        }

        public string CompanyLocation
        {
            get { return _CompanyLocation; }
            set { _CompanyLocation = value; }
        }

        public string DesignationCode
        {
            get { return _DesignationCode; }
            set { _DesignationCode = value; }
        }

        public string Salary
        {
            get { return _Salary; }
            set { _Salary = value; }
        }

        public string Role
        {
            get { return _Role; }
            set { _Role = value; }
        }

        public string Responsibility
        {
            get { return _Responsibility; }
            set { _Responsibility = value; }

        }

        public int SkillID
        {
            get { return _SkillID; }
            set { _SkillID = value; }
        }
        

        public string SkillName
        {
            get { return _SkillName; }
            set { _SkillName = value; }
        }

        public int LastUsedYear
        {
            get { return _LastUsedYear; }
            set { _LastUsedYear = value; }
        }

        public string LastUsedMonth
        {
            get { return _LastUsedMonth; }
            set { _LastUsedMonth = value; }
        }
        
        public string Experience
        {
            get { return _Experience; }
            set { _Experience = value; }
        }

                                                public string ProficiencyLevel
                                                {
                                                    get { return _ProficiencyLevel; }
                                                    set { _ProficiencyLevel = value; }
                                                }


        public int ProjectSeqID
        {
            get { return _ProjectSeqID; }
            set { _ProjectSeqID = value; }
        }

        public string ProjectTitle
        {
            get { return _ProjectTitle; }
            set { _ProjectTitle = value; }
        }

        public string ClientName
        {
            get { return _ClientName; }
            set { _ClientName = value; }
        }

        public string ProjectFromDateMonth
        {
            get { return _ProjectFromDateMonth; }
            set { _ProjectFromDateMonth = value; }
        }

        public int ProjectFromDateYear
        {
            get { return _ProjectFromDateYear; }
            set { _ProjectFromDateYear = value; }
        }

        public string ProjectToDateMonth
        {
            get { return _ProjectToDateMonth; }
            set { _ProjectToDateMonth = value; }
        }

        public int ProjectToDateYear
        {
            get { return _ProjectToDateYear; }
            set { _ProjectToDateYear = value; }
        }

        public string ProjectLocation
        {
            get { return _ProjectLocation; }
            set { _ProjectLocation = value; }
        }

                                                public char ImplementationType
                                                {
                                                    get { return _ImplementationType; }
                                                    set { _ImplementationType = value; }
                                                }

        public string Projectdescription
        {
            get { return _Projectdescription; }
            set { _Projectdescription = value; }
        }

        public string ProjectRole
        {
            get { return _ProjectRole; }
            set { _ProjectRole = value; }
        }

        public string ProjectDesignation
        {
            get { return _ProjectDesignation; }
            set { _ProjectDesignation = value; }
        }

        public int TeamSize
        {
            get { return _TeamSize; }
            set { _TeamSize = value; }
        }

        public string SkillsUsed
        {
            get { return _SkillsUsed; }
            set { _SkillsUsed = value; }
        }

        public int ReferenceID
        {
            get { return _ReferenceID; }
            set { _ReferenceID = value; }
        }

        public string ReferencePerson
        {
            get { return _ReferencePerson; }
            set { _ReferencePerson = value; }
        }

        public string ReferenceCompany
        {
            get { return _ReferenceCompany; }
            set { _ReferenceCompany = value; }
        }

        public string ReferenceDesignation
        {
            get { return _ReferenceDesignation; }
            set { _ReferenceDesignation = value; }
        }

        public string Relationship
        {
            get { return _Relationship; }
            set { _Relationship = value; }
        }

        public string ReferencePhone
        {
            get { return _ReferencePhone; }
            set { _ReferencePhone = value; }
        }

        public string ReferenceEmail
        {
            get { return _ReferenceEmail; }
            set { _ReferenceEmail = value; }
        }


        public int LanguageSeqID
        {
            get { return _LanguageSeqID; }
            set { _LanguageSeqID = value; }
        }

        public string LanguageName
        {
            get { return _LanguageName; }
            set { _LanguageName = value; }
        }

                                                public char Read
                                                {
                                                    get { return _Read; }
                                                    set { _Read = value; }
                                                }

                                                public char Write
                                                {
                                                    get { return _Write; }
                                                    set { _Write = value; }
                                                }

                                                public char Speak
                                                {
                                                    get { return _Speak; }
                                                    set { _Speak = value; }
                                                }


        public int QuestionID
        {
            get { return _QuestionID; }
            set { _QuestionID = value; }
        }

        public string Question
        {
            get { return _Question; }
            set { _Question = value; }
        }




 //------------------------------------------------------------------------------------------------------------

        public DataSet fn_Login_reg(string l_uid, string l_pwd)
        {


            _Connection = Con.fn_Connection();

            string _reg = "select * from hrmm_Registration where v_Username ='" + l_uid + "' and Password ='" + l_pwd + "'";


            _Connection.Open();

            SqlDataAdapter _Ad_reg = new SqlDataAdapter(_reg, _Connection);

            DataSet _Ds_reg = new DataSet();

            _Ad_reg.Fill(_Ds_reg, "hrmm_Registration");

            _Connection.Close();

            return _Ds_reg;



        }

        public DataSet fn_Login_cand(string c_uid)
        {

            _Connection = Con.fn_Connection();

            string _cand = "select * from hrmm_CandidateProfile where v_Username ='" + c_uid + "'";


            _Connection.Open();

            SqlDataAdapter _Ad_cand = new SqlDataAdapter(_cand, _Connection);

            DataSet _Ds_cand = new DataSet();

            _Ad_cand.Fill(_Ds_cand, "hrmm_CandidateProfile");

            _Connection.Close();

            return _Ds_cand;

        }

        public void fn_resume(Candidate c)
        {

            _Connection = Con.fn_Connection();

            string _str = "update hrmm_CandidateProfile set v_summary='"+c.Summary+"' where pn_CandidateID="+c.CandidateID+"";

            SqlCommand _cmd = new SqlCommand(_str, _Connection);

            _Connection.Open();

            _cmd.ExecuteNonQuery();

            _Connection.Close();


        }



//------------------------------------------------------------------------------------------------------------
        public string LanguagesKnownUpdate(Candidate c)
        {
            try
            {
                _Connection = Con.fn_Connection();
                SqlCommand _ISPCandidate = new SqlCommand("sp_LanguagesKnown", _Connection);
                _ISPCandidate.CommandType = CommandType.StoredProcedure;
                SqlParameter[] _ISPCandidateParam = new SqlParameter[7];
                _ISPCandidateParam[0]=new SqlParameter("@pn_CompanyID",SqlDbType.Int);
                _ISPCandidateParam[0].Value=c.CompanyID;
                //_ISPCandidateParam[1] = new SqlParameter("@pn_BranchID", SqlDbType.Int);
                //_ISPCandidateParam[1].Value = c.BranchID;
                _ISPCandidateParam[1]=new SqlParameter("@pn_CandidateID",SqlDbType.Int);
                _ISPCandidateParam[1].Value=c.CandidateID;
                _ISPCandidateParam[2]=new SqlParameter("@pn_SeqID",SqlDbType.Int);
                _ISPCandidateParam[2].Value=c.LanguageSeqID;
                _ISPCandidateParam[3]=new SqlParameter("@v_LanguageName",SqlDbType.VarChar);
                _ISPCandidateParam[3].Value=c.LanguageName;
                _ISPCandidateParam[4] = new SqlParameter("@c_Read", SqlDbType.Char);
                _ISPCandidateParam[4].Value = c.Read;
                _ISPCandidateParam[5] = new SqlParameter("@c_Write", SqlDbType.Char);
                _ISPCandidateParam[5].Value = c.Write;
                _ISPCandidateParam[6] = new SqlParameter("@c_Speak", SqlDbType.Char);
                _ISPCandidateParam[6].Value = c.Speak;
                for (int i = 0; i < _ISPCandidateParam.Length; i++)
                 {
                    _ISPCandidate.Parameters.Add(_ISPCandidateParam[i]);
                 }
                _Connection.Open();
                _ISPCandidate.ExecuteNonQuery();
                _Connection.Close();
                return "Records Saved Successfully";
            }
            catch (SqlException e1)
            {
                return e1.ToString();
            }
        
        }
        
        
//------------------------------------------------------------------------------------------------------------        
        public string ReferenceUpdate(Candidate c)
        {
            try
            {
                _Connection = Con.fn_Connection();
                SqlCommand _ISPCandidate = new SqlCommand("sp_References", _Connection);
                _ISPCandidate.CommandType = CommandType.StoredProcedure;
                SqlParameter[] _ISPCandidateParam = new SqlParameter[9];
                _ISPCandidateParam[0]=new SqlParameter("@pn_CompanyID",SqlDbType.Int);
                _ISPCandidateParam[0].Value=c.CompanyID;
                _ISPCandidateParam[1]=new SqlParameter("@pn_CandidateID",SqlDbType.Int);
                _ISPCandidateParam[1].Value=c.CandidateID;
                _ISPCandidateParam[2]=new SqlParameter("@pn_ReferenceID",SqlDbType.Int);
                _ISPCandidateParam[2].Value=c.ReferenceID;
                _ISPCandidateParam[3]=new SqlParameter("@v_ReferencePerson",SqlDbType.VarChar);
                _ISPCandidateParam[3].Value=c.ReferencePerson;
                _ISPCandidateParam[4]=new SqlParameter("@v_Company",SqlDbType.VarChar);
                _ISPCandidateParam[4].Value=c.ReferenceCompany;
                _ISPCandidateParam[5]=new SqlParameter("@v_Designation",SqlDbType.VarChar);
                _ISPCandidateParam[5].Value=c.ReferenceDesignation;
                _ISPCandidateParam[6]=new SqlParameter("@v_Relationship",SqlDbType.VarChar);
                _ISPCandidateParam[6].Value=c.Relationship;
                _ISPCandidateParam[7]=new SqlParameter("@v_Phone",SqlDbType.VarChar);
                _ISPCandidateParam[7].Value=c.ReferencePhone;
                _ISPCandidateParam[8]=new SqlParameter("@v_Email",SqlDbType.VarChar);
                _ISPCandidateParam[8].Value=c.ReferenceEmail;
                for (int i = 0; i < _ISPCandidateParam.Length; i++)
                 {
                    _ISPCandidate.Parameters.Add(_ISPCandidateParam[i]);
                 }
                _Connection.Open();
                _ISPCandidate.ExecuteNonQuery();
                _Connection.Close();
                return "Records Saved Successfully";
            }
            catch (SqlException e1)
            {
                return e1.ToString();
            }
        
        }
                
        
//------------------------------------------------------------------------------------------------------------

        public string ProjectUpdate(Candidate c)
        {
            try
            {
                _Connection = Con.fn_Connection();
                SqlCommand _ISPCandidate = new SqlCommand("sp_projects", _Connection);
                _ISPCandidate.CommandType = CommandType.StoredProcedure;
                SqlParameter[] _ISPCandidateParam = new SqlParameter[17];
                _ISPCandidateParam[0] = new SqlParameter("@pn_CompanyID", SqlDbType.Int);
                _ISPCandidateParam[0].Value = c.CompanyID;
                //_ISPCandidateParam[1] = new SqlParameter("@pn_BranchID", SqlDbType.Int);
                //_ISPCandidateParam[1].Value = c.BranchID;
                _ISPCandidateParam[1] = new SqlParameter("@pn_CandidateID", SqlDbType.Int);
                _ISPCandidateParam[1].Value = c.CandidateID;
                _ISPCandidateParam[2]=new SqlParameter("@pn_SeqID",SqlDbType.Int);
                _ISPCandidateParam[2].Value=c.ProjectSeqID;
                _ISPCandidateParam[3]=new SqlParameter("@v_ProjectTitle",SqlDbType.VarChar);
                _ISPCandidateParam[3].Value=c.ProjectTitle;
                _ISPCandidateParam[4]=new SqlParameter("@v_ClientName",SqlDbType.VarChar);
                _ISPCandidateParam[4].Value=c.ClientName;
                _ISPCandidateParam[5]=new SqlParameter("@n_FromDateMonth",SqlDbType.VarChar);
                _ISPCandidateParam[5].Value=c.ProjectFromDateMonth;
                _ISPCandidateParam[6]=new SqlParameter("@n_FromDateYear",SqlDbType.Int);
                _ISPCandidateParam[6].Value=c.ProjectFromDateYear;
                _ISPCandidateParam[7]=new SqlParameter("@n_ToDateMonth",SqlDbType.VarChar);
                _ISPCandidateParam[7].Value=c.ProjectToDateMonth;
                _ISPCandidateParam[8]=new SqlParameter("@n_ToDateYear",SqlDbType.Int);
                _ISPCandidateParam[8].Value=c.ProjectToDateYear;
                _ISPCandidateParam[9]=new SqlParameter("@v_Location",SqlDbType.VarChar);
                _ISPCandidateParam[9].Value=c.ProjectLocation;
                    _ISPCandidateParam[10]=new SqlParameter("@c_ImplementationType",SqlDbType.Char);
                    _ISPCandidateParam[10].Value=c.ImplementationType;
                _ISPCandidateParam[11]=new SqlParameter("@fn_JobStatusID",SqlDbType.Int);
                _ISPCandidateParam[11].Value=c.JobStatusID;
                _ISPCandidateParam[12]=new SqlParameter("@v_Projectdescription",SqlDbType.VarChar);
                _ISPCandidateParam[12].Value=c.Projectdescription;
                _ISPCandidateParam[13]=new SqlParameter("@v_Role",SqlDbType.VarChar);
                _ISPCandidateParam[13].Value=c.ProjectRole;
                _ISPCandidateParam[14]=new SqlParameter("@v_Designation",SqlDbType.VarChar);
                _ISPCandidateParam[14].Value=c.ProjectDesignation;
                _ISPCandidateParam[15]=new SqlParameter("@n_TeamSize",SqlDbType.Int);
                _ISPCandidateParam[15].Value=c.TeamSize;
                _ISPCandidateParam[16]=new SqlParameter("@v_SkillsUsed",SqlDbType.VarChar);
                _ISPCandidateParam[16].Value=c.SkillsUsed;
               
                for (int i = 0; i < _ISPCandidateParam.Length; i++)
                {
                    _ISPCandidate.Parameters.Add(_ISPCandidateParam[i]);
                }
                _Connection.Open();
                _ISPCandidate.ExecuteNonQuery();
                _Connection.Close();
                return "Records Saved Successfully";
            }
            catch (SqlException e1)
            {
                return e1.ToString();
            }
        }

//------------------------------------------------------------------------------------------------------------
        public string SkillsUpdate(Candidate c)
        {
            try
            {
                _Connection = Con.fn_Connection();
                SqlCommand _ISPCandidate = new SqlCommand("sp_CandidateSkills", _Connection);
                _ISPCandidate.CommandType = CommandType.StoredProcedure;
                SqlParameter[] _ISPCandidateParam = new SqlParameter[7];
                _ISPCandidateParam[0]=new SqlParameter("@pn_CompanyID",SqlDbType.Int);
                _ISPCandidateParam[0].Value=c.CompanyID;
                _ISPCandidateParam[1]=new SqlParameter("@pn_CandidateID",SqlDbType.Int);
                _ISPCandidateParam[1].Value=c.CandidateID;
                _ISPCandidateParam[2]=new SqlParameter("@n_LastUsedYear",SqlDbType.Int);
                _ISPCandidateParam[2].Value=c.LastUsedYear;
                _ISPCandidateParam[3]=new SqlParameter("@v_LastUsedMonth",SqlDbType.VarChar);
                _ISPCandidateParam[3].Value=c.LastUsedMonth;
                _ISPCandidateParam[4] = new SqlParameter("@c_ProficiencyLevel", SqlDbType.VarChar);
                _ISPCandidateParam[4].Value =c.ProficiencyLevel ;
                _ISPCandidateParam[5] = new SqlParameter("@n_Experience", SqlDbType.VarChar);
                _ISPCandidateParam[5].Value = c.Experience;
                _ISPCandidateParam[6] = new SqlParameter("@pn_SkillID", SqlDbType.Int);
                _ISPCandidateParam[6].Value = c.SkillID;
                
                for (int i = 0; i < _ISPCandidateParam.Length; i++)
                {
                    _ISPCandidate.Parameters.Add(_ISPCandidateParam[i]);
                }
                _Connection.Open();
                _ISPCandidate.ExecuteNonQuery();
                _Connection.Close();
                return "Records Saved Successfully";

            }
            catch (SqlException e1)
            {
                return e1.ToString();
            }
        }


//------------------------------------------------------------------------------------------------------------
        public string WorkHistoryUpdate(Candidate c)
        {
            try
            {
                _Connection = Con.fn_Connection();
                SqlCommand _ISPCandidate = new SqlCommand("sp_WorkHistory", _Connection);
                _ISPCandidate.CommandType = CommandType.StoredProcedure;
                SqlParameter[] _ISPCandidateParam = new SqlParameter[13];
                _ISPCandidateParam[0] = new SqlParameter("@pn_CompanyID", SqlDbType.Int);
                _ISPCandidateParam[0].Value = c.CompanyID;
                _ISPCandidateParam[1] = new SqlParameter("@pn_CandidateID", SqlDbType.Int);
                _ISPCandidateParam[1].Value = c.CandidateID;
                _ISPCandidateParam[2]=new SqlParameter("@pn_SeqID",SqlDbType.Int);
                _ISPCandidateParam[2].Value=c.WorkHistorySeqID;
                _ISPCandidateParam[3]=new SqlParameter("@n_FromDateMonth",SqlDbType.VarChar);
                _ISPCandidateParam[3].Value=c.WorkHistoryFromDateMonth;
                _ISPCandidateParam[4]=new SqlParameter("@n_FromDateYear",SqlDbType.Int);
                _ISPCandidateParam[4].Value=c.WorkHistoryFromDateYear;
                _ISPCandidateParam[5]=new SqlParameter("@n_ToDateMonth",SqlDbType.VarChar);
                _ISPCandidateParam[5].Value=c.WorkHistoryToDateMonth;
                _ISPCandidateParam[6]=new SqlParameter("@n_ToDateYear",SqlDbType.Int);
                _ISPCandidateParam[6].Value=c.WorkHistoryToDateYear;
                _ISPCandidateParam[7]=new SqlParameter("@v_CompanyName",SqlDbType.VarChar);
                _ISPCandidateParam[7].Value=c.CompanyName;
                _ISPCandidateParam[8]=new SqlParameter("@v_CompanyLocation",SqlDbType.VarChar);
                _ISPCandidateParam[8].Value=c.CompanyLocation;
                _ISPCandidateParam[9]=new SqlParameter("@v_DesignationCode",SqlDbType.VarChar);
                _ISPCandidateParam[9].Value=c.DesignationCode;
                _ISPCandidateParam[10]=new SqlParameter("@n_Salary",SqlDbType.VarChar);
                _ISPCandidateParam[10].Value=c.Salary;
                _ISPCandidateParam[11]=new SqlParameter("@v_Role",SqlDbType.VarChar);
                _ISPCandidateParam[11].Value=c.Role;
                _ISPCandidateParam[12]=new SqlParameter("@v_Responsibility",SqlDbType.VarChar);
                _ISPCandidateParam[12].Value=c.Responsibility;
                for (int i = 0; i < _ISPCandidateParam.Length; i++)
                {
                    _ISPCandidate.Parameters.Add(_ISPCandidateParam[i]);
                }
                _Connection.Open();
                _ISPCandidate.ExecuteNonQuery();
                _Connection.Close();
                return "Records Saved Successfully";

            }
            catch (SqlException e1)
            {
                return e1.ToString();
            }
            
        }
        
        
//------------------------------------------------------------------------------------------------------------


        public string EducationUpdate(Candidate c)
        {
            try
            {
                _Connection = Con.fn_Connection();
                SqlCommand _ISPCandidate = new SqlCommand("sp_Education", _Connection);
                _ISPCandidate.CommandType = CommandType.StoredProcedure;
                SqlParameter[] _ISPCandidateParam = new SqlParameter[8];
                _ISPCandidateParam[0] = new SqlParameter("@pn_CompanyID", SqlDbType.Int);
                _ISPCandidateParam[0].Value = c.CompanyID;
                _ISPCandidateParam[1] = new SqlParameter("@pn_CandidateID", SqlDbType.Int);
                _ISPCandidateParam[1].Value = c.CandidateID;
                _ISPCandidateParam[2] = new SqlParameter("@pn_CourseID", SqlDbType.Int);
                _ISPCandidateParam[2].Value = c.CourseID;
                _ISPCandidateParam[3] = new SqlParameter("@fn_InstitutionID", SqlDbType.Int);
                _ISPCandidateParam[3].Value = c.InstitutionID;
                _ISPCandidateParam[4] = new SqlParameter("@n_CompletedMonth", SqlDbType.VarChar);
                _ISPCandidateParam[4].Value = c.CompletedMonth;
                _ISPCandidateParam[5] = new SqlParameter("@n_CompletedYear", SqlDbType.Int);
                _ISPCandidateParam[5].Value = c.CompletedYear;
                _ISPCandidateParam[6] = new SqlParameter("@v_Specialisation", SqlDbType.VarChar);
                _ISPCandidateParam[6].Value = c.Specialisation;
                _ISPCandidateParam[7] = new SqlParameter("@n_Percentage", SqlDbType.VarChar);
                _ISPCandidateParam[7].Value = c.Percentage;
                
                for (int i = 0; i < _ISPCandidateParam.Length; i++)
                {
                    _ISPCandidate.Parameters.Add(_ISPCandidateParam[i]);
                }
                _Connection.Open();
                _ISPCandidate.ExecuteNonQuery();
                _Connection.Close();
                return "Records Saved Successfully";

            }
            catch (SqlException e1)
            {
                return e1.ToString();
            }
        
        }

//------------------------------------------------------------------------------------------------------------

        public string ProfileUpdate(Candidate c)
        {
            try
            {
                _Connection = Con.fn_Connection();
                SqlCommand _ISPCandidate = new SqlCommand("sp_CandidateProfile", _Connection);
                _ISPCandidate.CommandType = CommandType.StoredProcedure;
                SqlParameter[] _ISPCandidateParam = new SqlParameter[26];
                _ISPCandidateParam[0] = new SqlParameter("@pn_CompanyID", SqlDbType.Int);
                _ISPCandidateParam[0].Value = c.CompanyID;
                _ISPCandidateParam[1] = new SqlParameter("@pn_CandidateID", SqlDbType.Int);
                _ISPCandidateParam[1].Value = c.CandidateID;
                _ISPCandidateParam[2] = new SqlParameter("@v_Username", SqlDbType.VarChar);
                _ISPCandidateParam[2].Value = c.ProfileUsername;
                _ISPCandidateParam[3] = new SqlParameter("@v_CandidateName", SqlDbType.VarChar);
                _ISPCandidateParam[3].Value = c.CandidateName;
                _ISPCandidateParam[4] = new SqlParameter("@c_RecruitSource", SqlDbType.Char);
                _ISPCandidateParam[4].Value = c.RecruitSource;
                _ISPCandidateParam[5] = new SqlParameter("@v_FatherHusbandName", SqlDbType.VarChar);
                _ISPCandidateParam[5].Value = c.FatherHusbandName;
                _ISPCandidateParam[6] = new SqlParameter("@v_DOB", SqlDbType.VarChar);
                _ISPCandidateParam[6].Value = c.DOB;
                _ISPCandidateParam[7] = new SqlParameter("@v_AddressLine1", SqlDbType.VarChar);
                _ISPCandidateParam[7].Value = c.AddressLine1;
                _ISPCandidateParam[8] = new SqlParameter("@v_AddressLine2", SqlDbType.VarChar);
                _ISPCandidateParam[8].Value = c.AddressLine2;
                _ISPCandidateParam[9] = new SqlParameter("@v_City", SqlDbType.VarChar);
                _ISPCandidateParam[9].Value = c.City;
                _ISPCandidateParam[10] = new SqlParameter("@v_Country", SqlDbType.VarChar);
                _ISPCandidateParam[10].Value = c.Country;
                _ISPCandidateParam[11] = new SqlParameter("@v_State", SqlDbType.VarChar);
                _ISPCandidateParam[11].Value = c.State;
                _ISPCandidateParam[12] = new SqlParameter("@c_Gender", SqlDbType.Char);
                _ISPCandidateParam[12].Value = c.Gender;
                _ISPCandidateParam[13] = new SqlParameter("@c_MaritialStatus", SqlDbType.Char);
                _ISPCandidateParam[13].Value = c.MaritialStatus;
                _ISPCandidateParam[14] = new SqlParameter("@v_PhoneResidence", SqlDbType.VarChar);
                _ISPCandidateParam[14].Value = c.PhoneResidence;
                _ISPCandidateParam[15] = new SqlParameter("@v_PhoneOffice",SqlDbType.VarChar);
                _ISPCandidateParam[15].Value=c.PhoneOffice;
                _ISPCandidateParam[16] = new SqlParameter("@v_PhoneMobile", SqlDbType.VarChar);
                _ISPCandidateParam[16].Value = c.PhoneMobile;
                _ISPCandidateParam[17] = new SqlParameter("@v_Email",SqlDbType.VarChar);
                _ISPCandidateParam[17].Value = c.Email;
                _ISPCandidateParam[18] = new SqlParameter("@v_KeySkills",SqlDbType.VarChar);
                _ISPCandidateParam[18].Value = c.KeySkills;
                _ISPCandidateParam[19] = new SqlParameter("@v_ResumeHeadline", SqlDbType.VarChar);
                _ISPCandidateParam[19].Value = c.ResumeHeadline;
                _ISPCandidateParam[20] = new SqlParameter("@v_Summary", SqlDbType.VarChar);
                _ISPCandidateParam[20].Value = c.Summary;
                _ISPCandidateParam[21] = new SqlParameter("@fn_FunctionalAreaID", SqlDbType.Int);
                _ISPCandidateParam[21].Value = c.FunctionalAreaID;
                _ISPCandidateParam[22] = new SqlParameter("@v_Domain", SqlDbType.VarChar);
                _ISPCandidateParam[22].Value = c.Domain;
                _ISPCandidateParam[23] = new SqlParameter("@n_TotalExperience", SqlDbType.VarChar);
                _ISPCandidateParam[23].Value = c.TotalExperience;
                _ISPCandidateParam[24] = new SqlParameter("@n_RelevantExperience", SqlDbType.VarChar);
                _ISPCandidateParam[24].Value = c.RelevantExperience;
                _ISPCandidateParam[25] = new SqlParameter("@fn_JobStatusID", SqlDbType.Int);
                _ISPCandidateParam[25].Value = c.JobStatusID;
                //_ISPCandidateParam[26] = new SqlParameter("@pn_CandidateID_out",SqlDbType.Int);
                //_ISPCandidateParam[26].Direction = ParameterDirection.Output;
                //_ISPCandidateParam[27] = new SqlParameter("@v_FunctionalAreaName", SqlDbType.VarChar);
                //_ISPCandidateParam[27].Value = c.FunctionalAreaName;
                //_ISPCandidateParam[28] = new SqlParameter("@fn_FunctionalAreaID_Out", SqlDbType.Int);
                //_ISPCandidateParam[28].Direction = ParameterDirection.Output;

                for (int i = 0; i < _ISPCandidateParam.Length; i++)
                {
                    _ISPCandidate.Parameters.Add(_ISPCandidateParam[i]);
                }
                _Connection.Open();
                _ISPCandidate.ExecuteNonQuery();
                _Connection.Close();
                return "Records Saved Successfully";
                                                
            }
            catch (SqlException e1)
            {
                 return e1.ToString();
            }
        }

            
//------------------------------------------------------------------------------------------------------------
        
        public Collection<Candidate> fn_getFunctionalArea()
        {
            Collection<Candidate> FunctionalAreaList = new Collection<Candidate>();
            _Connection = Con.fn_Connection();
            SqlCommand _SFunctionalArea = new SqlCommand("select * from hrmm_FunctionalArea", _Connection);
            _Connection.Open();
            SqlDataReader dr_FunctionalArea = _SFunctionalArea.ExecuteReader();

            while (dr_FunctionalArea.Read())
            {
                Candidate candidate = new  Candidate();
                candidate.FunctionalAreaID  = (int)dr_FunctionalArea["pn_FunctionalAreaID"];
                candidate.FunctionalAreaName = Convert.IsDBNull(dr_FunctionalArea["v_FunctionalAreaName"]) ? "" : (string)dr_FunctionalArea["v_FunctionalAreaName"];
                FunctionalAreaList.Add(candidate);
            }

            return FunctionalAreaList;
        }
       
//-------------------------------------------------------------------------------
        public Collection<Candidate> fn_getJobStatus()
        {
            Collection<Candidate> JobStatusList = new Collection<Candidate>();
            _Connection = Con.fn_Connection();
            SqlCommand _SJobStatus = new SqlCommand("select * from paym_JobStatus", _Connection);
            _Connection.Open();
            SqlDataReader dr_JobStatus = _SJobStatus.ExecuteReader();

            while (dr_JobStatus.Read())
            {
                Candidate candidate = new Candidate();
                candidate.JobStatusID = (int)dr_JobStatus["pn_JobStatusID"];
                candidate.JobStatusName = Convert.IsDBNull(dr_JobStatus["v_JobStatusName"]) ? "" : (string)dr_JobStatus["v_JobStatusName"];
                JobStatusList.Add(candidate);
            }
            return JobStatusList;
        }


//-------------------------------------------------------------------------------
        public string fn_JobStatusName(int js_id)
        {
            Collection<Candidate> JobStatusList = new Collection<Candidate>();
            _Connection = Con.fn_Connection();
            SqlCommand _SJobStatus = new SqlCommand("select v_JobStatusName from paym_JobStatus where pn_JobStatusID=" + js_id + "", _Connection);
            _Connection.Open();
            SqlDataReader dr_JobStatus = _SJobStatus.ExecuteReader();

            Candidate candidate = new Candidate();  
            while (dr_JobStatus.Read())
            {
                             
                candidate.JobStatusName = Convert.IsDBNull(dr_JobStatus["v_JobStatusName"]) ? "" : (string)dr_JobStatus["v_JobStatusName"];
                
            }
            return candidate.JobStatusName;
        }

 //-------------------------------------------------------------------------------


        public Collection<Candidate> fn_getSkillsName()
        { 
            Collection<Candidate> SkillsNameList=new Collection<Candidate>();
            _Connection = Con.fn_Connection();
            SqlCommand _SSkillsName = new SqlCommand(" ", _Connection);
            _Connection.Open();
            SqlDataReader dr_SkillsName = _SSkillsName.ExecuteReader();

            while (dr_SkillsName.Read())
            {
                Candidate candidate = new Candidate();
                candidate.SkillID = (int)dr_SkillsName["pn_SkillID"];
                candidate.SkillName=Convert.IsDBNull(dr_SkillsName["v_SkillName"]) ? "" : (string)dr_SkillsName["v_SkillName"];
                SkillsNameList.Add(candidate);
            }
            return SkillsNameList;
        }


//-------------------------------------------------------------------------------
        
        
        public Collection<Candidate> fn_getCourseName()
        {
            Collection<Candidate> CourseNameList = new Collection<Candidate>();
            _Connection = Con.fn_Connection();
            SqlCommand _SCourse = new SqlCommand("select * from hrmm_Course", _Connection);
            _Connection.Open();
            SqlDataReader dr_Course =_SCourse.ExecuteReader();
            
            while(dr_Course.Read())
            {
                Candidate candidate=new Candidate();
                candidate.CourseID=(int)dr_Course["pn_CourseID"];
                candidate.CourseName=Convert.IsDBNull(dr_Course["v_CourseName"]) ? "" :(string)dr_Course["v_CourseName"];
                CourseNameList.Add(candidate);
            }
            return CourseNameList;
        }

       
//--------------------------------------------------------------------------------

        
        public Collection<Candidate> fn_getInstitution()
        {
            Collection<Candidate> InstitutionList = new Collection<Candidate>();
            _Connection = Con.fn_Connection();
            SqlCommand _SInstitution = new SqlCommand("select * from hrmm_Institution", _Connection);
            _Connection.Open();
            SqlDataReader dr_Institution = _SInstitution.ExecuteReader();

            while (dr_Institution.Read())
            {
                Candidate candidate = new Candidate();
                candidate.InstitutionID = (int)dr_Institution["pn_InstitutionID"];
                candidate.InstitutionName = Convert.IsDBNull(dr_Institution["v_InstitutionName"]) ? "" : (string)dr_Institution["v_InstitutionName"];
                InstitutionList.Add(candidate);
            }
            return InstitutionList;
        }

        //------------------------------------------------------------------------------------
        
        public Collection<Candidate> fn_getLanguages()
        {
            Collection<Candidate> LanguagesList = new Collection<Candidate>();
            _Connection = Con.fn_Connection();
            SqlCommand _SLanguages = new SqlCommand("select * from hrmm_CandidateLanguagesKnown", _Connection);
            _Connection.Open();
            SqlDataReader dr_Languages = _SLanguages.ExecuteReader();

            while (dr_Languages.Read())
            {
                Candidate candidate = new Candidate();
                candidate.LanguageSeqID = (int)dr_Languages["pn_SeqID"];
                candidate.LanguageName = Convert.IsDBNull(dr_Languages["v_LanguageName"]) ? "" : (string)dr_Languages["v_LanguageName"];
                LanguagesList.Add(candidate);
            }
            return LanguagesList;
        }

        
        //Loading data to grid
        //------------------------------------------------------------------------------------
       
        public Collection<Candidate> fn_getEducation(int c_id)
        {
            Collection<Candidate> EducationList = new Collection<Candidate>();
            _Connection = Con.fn_Connection();
            string _SqlString = "select e.pn_CourseID, c.v_CourseName , e.v_Specialisation, i.v_InstitutionName ,";
            _SqlString += " e.n_Percentage,e.n_CompletedMonth,e.n_CompletedYear from hrmm_CandidateEducation e, hrmm_Course c, hrmm_Institution i";
           _SqlString += " where c.pn_CourseID =e.pn_CourseID and i.pn_InstitutionID =e.fn_InstitutionID and e.pn_CandidateID =" + c_id + "";
            SqlCommand _SSCandidate = new SqlCommand(_SqlString, _Connection);
            _Connection.Open();
            SqlDataReader dr_Candidate = _SSCandidate.ExecuteReader();
            while (dr_Candidate.Read())
            {
                Candidate candidate = new Candidate();
                candidate.CourseID = (int)dr_Candidate["pn_CourseID"];
                candidate.CourseName = Convert.IsDBNull(dr_Candidate["v_CourseName"]) ? "" : (string)dr_Candidate["v_CourseName"];
                candidate.Specialisation = Convert.IsDBNull(dr_Candidate["v_Specialisation"]) ? "" : (string)dr_Candidate["v_Specialisation"];
                candidate.InstitutionName = Convert.IsDBNull(dr_Candidate["v_InstitutionName"]) ? "" : (string)dr_Candidate["v_InstitutionName"];
                candidate.Percentage = Convert.IsDBNull(dr_Candidate["n_Percentage"]) ? "" : (string)dr_Candidate["n_Percentage"];
                candidate.CompletedMonth = Convert.IsDBNull(dr_Candidate["n_CompletedMonth"]) ? "" : (string)dr_Candidate["n_CompletedMonth"];
                candidate.CompletedYear = Convert.IsDBNull(dr_Candidate["n_CompletedYear"]) ? 0 : (int)dr_Candidate["n_CompletedYear"];
                EducationList.Add(candidate);
            }
            return EducationList;           
        }
        //-----------------------------------------------------------------------------------------------

        public Collection<Candidate> fn_getLanguageKnown(int c_id)
        {
            Collection<Candidate> LanguageKnownList = new Collection<Candidate>();
            _Connection = Con.fn_Connection();
            string _SqlString = "select pn_SeqID, v_LanguageName, c_Read, c_Write, c_Speak from hrmm_CandidateLanguagesKnown where pn_CandidateID=" + c_id + "";
            SqlCommand _SSCandidate=new SqlCommand(_SqlString, _Connection);
            _Connection.Open();
            SqlDataReader dr_Candidate = _SSCandidate.ExecuteReader();
            while(dr_Candidate.Read())
            {
                Candidate candidate=new Candidate();
                candidate.LanguageSeqID = (int)dr_Candidate["pn_SeqID"];
                candidate.LanguageName=Convert.IsDBNull(dr_Candidate["v_LanguageName"]) ? "" : (string)dr_Candidate["v_LanguageName"];
                candidate.Read = Convert.ToChar(dr_Candidate["c_Read"]);
                candidate.Write = Convert.ToChar(dr_Candidate["c_Write"]);
                candidate.Speak = Convert.ToChar(dr_Candidate["c_Speak"]);
                LanguageKnownList.Add(candidate);

            }
            return LanguageKnownList;
        }

        //-----------------------------------------------------------------------------------------------

        public Collection<Candidate> fn_getProject(int c_id)
        {
            Collection<Candidate> projectList=new Collection<Candidate>();
            _Connection=Con.fn_Connection();
            string _Sqlstring="select p.pn_SeqID, p.v_ProjectTitle, p.v_ClientName, p.n_FromDateMonth, p.n_FromDateYear,";
            _Sqlstring += "p.n_ToDateMonth, p.n_ToDateYear, p.v_Location, p.c_ImplementationType, p.fn_JobStatusID, p.v_Projectdescription,";
            _Sqlstring +=" p.v_Role, p.v_Designation, p.n_TeamSize, p.v_SkillsUsed from hrmm_CandidateProjects p, paym_JobStatus j";
            _Sqlstring += " where j.pn_JobStatusID=p.fn_JobStatusID and p.pn_CandidateID =" + c_id + "";
            SqlCommand _SSCandidate=new SqlCommand(_Sqlstring, _Connection);
            _Connection.Open();
            SqlDataReader dr_Candidate = _SSCandidate.ExecuteReader();
            while(dr_Candidate.Read())
            {
                Candidate candidate = new Candidate();
                candidate.ProjectSeqID=(int)dr_Candidate["pn_SeqID"];
                candidate.ProjectTitle=Convert.IsDBNull(dr_Candidate["v_ProjectTitle"]) ? "" : (string)dr_Candidate["v_ProjectTitle"];
                candidate.ClientName=Convert.IsDBNull(dr_Candidate["v_ClientName"]) ? "" : (string)dr_Candidate["v_ClientName"];
                candidate.ProjectFromDateMonth = Convert.IsDBNull(dr_Candidate["n_FromDateMonth"]) ? "" : (string)dr_Candidate["n_FromDateMonth"];
                candidate.ProjectFromDateYear=(int)dr_Candidate["n_FromDateYear"];
                candidate.ProjectToDateMonth = Convert.IsDBNull(dr_Candidate["n_ToDateMonth"]) ? "" : (string)dr_Candidate["n_ToDateMonth"];
                candidate.ProjectToDateYear=(int)dr_Candidate["n_ToDateYear"];
                candidate.ProjectLocation=Convert.IsDBNull(dr_Candidate["v_Location"]) ? "" : (string)dr_Candidate["v_Location"];
                candidate.ImplementationType=Convert.ToChar(dr_Candidate["c_ImplementationType"]);
                candidate.JobStatusID=(int)dr_Candidate["fn_JobStatusID"];
                candidate.Projectdescription = Convert.IsDBNull(dr_Candidate["v_Projectdescription"]) ? "" : (string)dr_Candidate["v_Projectdescription"];
                candidate.ProjectRole=Convert.IsDBNull(dr_Candidate["v_Role"]) ? "" : (string)dr_Candidate["v_Role"];
                candidate.ProjectDesignation=Convert.IsDBNull(dr_Candidate["v_Designation"]) ? "" : (string)dr_Candidate["v_Designation"];
                candidate.TeamSize=(int)dr_Candidate["n_TeamSize"];
                candidate.SkillsUsed=Convert.IsDBNull(dr_Candidate["v_SkillsUsed"]) ? "" : (string)dr_Candidate["v_SkillsUsed"];
                projectList.Add(candidate);
            }
                return projectList;
        }

        //-----------------------------------------------------------------------------------------------

        public Collection<Candidate> fn_getReference(int c_id)
        {
            Collection<Candidate> ReferenceList = new Collection<Candidate>();
            _Connection = Con.fn_Connection();
            string _Sqlstring = "select pn_ReferenceID,v_Designation, v_ReferencePerson, v_Relationship, v_Company, ";
            _Sqlstring += "v_Phone, v_Email from hrmm_CandidateReferences where pn_CandidateID=" + c_id + "";
            SqlCommand _SSCandidate = new SqlCommand(_Sqlstring, _Connection);
            _Connection.Open();
            SqlDataReader dr_candidate = _SSCandidate.ExecuteReader();
            while (dr_candidate.Read())
            {
                Candidate candidate = new Candidate();
                candidate.ReferenceID = (int)dr_candidate["pn_ReferenceID"];
                candidate.ReferencePerson = Convert.IsDBNull(dr_candidate["v_ReferencePerson"]) ? "" : (string)dr_candidate["v_ReferencePerson"];
                candidate.ReferenceCompany = Convert.IsDBNull(dr_candidate["v_Company"]) ? "" : (string)dr_candidate["v_Company"];
                candidate.ReferenceDesignation = Convert.IsDBNull(dr_candidate["v_Designation"]) ? "" : (string)dr_candidate["v_Designation"];
                candidate.Relationship = Convert.IsDBNull(dr_candidate["v_Relationship"]) ? "" : (string)dr_candidate["v_Relationship"];
                candidate.ReferencePhone = Convert.IsDBNull(dr_candidate["v_Phone"]) ? "" : (string)dr_candidate["v_Phone"];
                candidate.ReferenceEmail = Convert.IsDBNull(dr_candidate["v_Email"]) ? "" : (string)dr_candidate["v_Email"];
                ReferenceList.Add(candidate);
            }
            return ReferenceList;
        }

        //-----------------------------------------------------------------------------------------------

        public Collection<Candidate> fn_getSkills(int c_id)
        {
            Collection<Candidate> skillsList = new Collection<Candidate>();
            _Connection = Con.fn_Connection();
            string _Sqlstring = "select sm.v_SkillName, cs.pn_SkillID, cs.n_LastUsedYear, cs.v_LastUsedMonth,";
            _Sqlstring += " cs.c_ProficiencyLevel, cs.n_Experience from hrmm_CandidateSkills cs, hrmm_SkillsMaster sm";
            _Sqlstring += " where sm.pn_SkillID =cs.pn_SkillID and cs.pn_CandidateID =" + c_id + "";
            SqlCommand _SSCandidate = new SqlCommand(_Sqlstring, _Connection);
            _Connection.Open();
            SqlDataReader dr_candidate = _SSCandidate.ExecuteReader();
            while (dr_candidate.Read())
            {
                Candidate candidate = new Candidate();
                candidate.SkillName = Convert.IsDBNull(dr_candidate["v_SkillName"]) ? "" : (string)dr_candidate["v_SkillName"];
                candidate.SkillID = (int)dr_candidate["pn_SkillID"];
                candidate.LastUsedYear = (int)dr_candidate["n_LastUsedYear"];
                candidate.LastUsedMonth = Convert.IsDBNull(dr_candidate["v_LastUsedMonth"]) ? "" : (string)dr_candidate["v_LastUsedMonth"];
                candidate.ProficiencyLevel = Convert.IsDBNull(dr_candidate["c_ProficiencyLevel"]) ? "" : (string)dr_candidate["c_ProficiencyLevel"];
              
                candidate.Experience = Convert.IsDBNull(dr_candidate["n_Experience"]) ? "" : (string)dr_candidate["n_Experience"];
                skillsList.Add(candidate);
            }
            return skillsList;
        }

        //-----------------------------------------------------------------------------------------------

        public Collection<Candidate> fn_getWorkHistory(int c_id)
        {
            Collection<Candidate> WorkHistoryList = new Collection<Candidate>();
            _Connection = Con.fn_Connection();
            string _Sqlstring = "select pn_SeqID, n_FromDateMonth, n_FromDateYear, n_ToDateMonth,";
            _Sqlstring += "n_ToDateYear, v_CompanyName, v_CompanyLocation, v_DesignationCode,";
            _Sqlstring += "n_Salary, v_Role, v_Responsibility from hrmm_candidateworkhistory where pn_CandidateID =" + c_id + "";
            SqlCommand _SSCandidate = new SqlCommand(_Sqlstring, _Connection);
            _Connection.Open();
            SqlDataReader dr_candidate = _SSCandidate.ExecuteReader();
            while (dr_candidate.Read())
            {
                Candidate candidate = new Candidate();
                candidate.WorkHistorySeqID = (int)dr_candidate["pn_SeqID"];
                candidate.WorkHistoryFromDateMonth = Convert.IsDBNull(dr_candidate["n_FromDateMonth"]) ? "" : (string)dr_candidate["n_FromDateMonth"];
                candidate.WorkHistoryFromDateYear = (int)dr_candidate["n_FromDateYear"];
                candidate.WorkHistoryToDateMonth = Convert.IsDBNull(dr_candidate["n_ToDateMonth"]) ? "" : (string)dr_candidate["n_ToDateMonth"];
                candidate.WorkHistoryToDateYear = (int)dr_candidate["n_ToDateYear"];
                candidate.CompanyName = Convert.IsDBNull(dr_candidate["v_CompanyName"]) ? "" : (string)dr_candidate["v_CompanyName"];
                candidate.CompanyLocation = Convert.IsDBNull(dr_candidate["v_CompanyLocation"]) ? "" : (string)dr_candidate["v_CompanyLocation"];
                candidate.DesignationCode = Convert.IsDBNull(dr_candidate["v_DesignationCode"]) ? "" : (string)dr_candidate["v_DesignationCode"];
                candidate.Salary = Convert.IsDBNull(dr_candidate["n_Salary"]) ? "" : (string)dr_candidate["n_Salary"];
                candidate.Role = Convert.IsDBNull(dr_candidate["v_Role"]) ? "" : (string)dr_candidate["v_Role"];
                candidate.Responsibility=Convert.IsDBNull(dr_candidate["v_Responsibility"]) ? "" : (string)dr_candidate["v_Responsibility"];
                WorkHistoryList.Add(candidate);
            }

            return WorkHistoryList;
        }

        //-----------------------------------------------------------------------------------------------

        public void DeleteEducation(int CourseID)
        {
            _Connection = Con.fn_Connection();
            string _DelCommand = "Delete from hrmm_CandidateEducation where pn_CourseID  =" + CourseID;
            SqlCommand cmd = new SqlCommand(_DelCommand, _Connection);
            _Connection.Open();
            cmd.ExecuteNonQuery();
            _Connection.Close();
        }

        //------------------------------------------------------------------------------------------------

        public void DeleteLanguageKnown(int LanguageSeqID)
        {
            _Connection=Con.fn_Connection();
            string _DelCommand = "delete from hrmm_CandidateLanguagesKnown where pn_SeqID =" + LanguageSeqID;
            SqlCommand cmd = new SqlCommand(_DelCommand, _Connection);
            _Connection.Open();
            cmd.ExecuteNonQuery();
            _Connection.Close();
        }

        //------------------------------------------------------------------------------------------------

        public void DeleteProjects(int ProjectSeqID)
        {
            _Connection = Con.fn_Connection();
            string _Delcommand = "delete from hrmm_CandidateProjects where pn_SeqID =" + ProjectSeqID;
            SqlCommand cmd = new SqlCommand(_Delcommand, _Connection);
            _Connection.Open();
            cmd.ExecuteNonQuery();
            _Connection.Close();
        
        }

        //------------------------------------------------------------------------------------------------

        public void DeleteReferences(int ReferenceID)
        {
            _Connection = Con.fn_Connection();
            string _Delcommand = "delete from hrmm_CandidateReferences where pn_ReferenceID =" + ReferenceID;
            SqlCommand cmd = new SqlCommand(_Delcommand ,_Connection);
            _Connection.Open();
            cmd.ExecuteNonQuery();
            _Connection.Close();
        }

        //------------------------------------------------------------------------------------------------

        public void DeleteSkills(int SkillID)
        {
            _Connection = Con.fn_Connection();
            string _Delcommand = "Delete from hrmm_candidateskills where pn_SkillID =" + SkillID;
            SqlCommand cmd = new SqlCommand(_Delcommand, _Connection);
            _Connection.Open();
            cmd.ExecuteNonQuery();
            _Connection.Close();
        }

        //------------------------------------------------------------------------------------------------

        public void DeleteHistory(int WorkHistorySeqID)
        {
            _Connection = Con.fn_Connection();
            string _Delcommand = "Delete from hrmm_candidateworkhistory where pn_SeqID =" + WorkHistorySeqID;
            SqlCommand cmd = new SqlCommand(_Delcommand, _Connection);
            _Connection.Open();
            cmd.ExecuteNonQuery();
            _Connection.Close();
        
        }
        
        
        //Repopulate data to page from grid
        //------------------------------------------------------------------------------------------------
        public Collection<Candidate> fn_ProfileEdit(string Username)
        {
            Collection<Candidate> ProfileEditing = new Collection<Candidate>();
            _Connection = Con.fn_Connection();
            string _SqlString = "select p.pn_CandidateID,p.v_Username,p.v_CandidateName,p.c_RecruitSource,p.v_FatherHusbandName,p.v_DOB,";
            _SqlString += "p.v_AddressLine1,p.v_AddressLine2,p.v_City,p.v_Country,p.v_State,p.c_Gender,p.c_MaritialStatus,p.v_PhoneResidence,";
            _SqlString += "p.v_PhoneOffice,p.v_PhoneMobile,p.v_Email,p.v_KeySkills,p.v_ResumeHeadline,p.v_Summary,";
            _SqlString += "f.v_FunctionalAreaName,p.v_Domain,p.n_TotalExperience,p.n_RelevantExperience,";
            _SqlString += "j.v_JobStatusName from hrmm_CandidateProfile p,hrmm_FunctionalArea f,";
            _SqlString += "paym_JobStatus j where j.pn_JobStatusID=p.fn_JobStatusID and f.pn_FunctionalAreaID=p.fn_FunctionalAreaID and p.v_Username ='" + Username + "'";
            SqlCommand cmd = new SqlCommand(_SqlString, _Connection);
            _Connection.Open();
            SqlDataReader dr_Candidate = cmd.ExecuteReader();
            while (dr_Candidate.Read())
            {
                Candidate candidate = new Candidate();
                candidate.CandidateID = (int)dr_Candidate["pn_CandidateID"];
                candidate.ProfileUsername = Convert.IsDBNull(dr_Candidate["v_Username"]) ? "" : (string)dr_Candidate["v_Username"];
                candidate.CandidateName = Convert.IsDBNull(dr_Candidate["v_CandidateName"]) ? "" : (string)dr_Candidate["v_CandidateName"];
                candidate.RecruitSource = Convert.ToChar(dr_Candidate["c_RecruitSource"]);
                candidate.FatherHusbandName = Convert.IsDBNull(dr_Candidate["v_FatherHusbandName"]) ? "" : (string)dr_Candidate["v_FatherHusbandName"];
                candidate.DOB = Convert.IsDBNull(dr_Candidate["v_DOB"]) ? "" : (string)dr_Candidate["v_DOB"];
                candidate.AddressLine1 = Convert.IsDBNull(dr_Candidate["v_AddressLine1"]) ? "" : (string)dr_Candidate["v_AddressLine1"];
                candidate.AddressLine2 = Convert.IsDBNull(dr_Candidate["v_AddressLine2"]) ? "" : (string)dr_Candidate["v_AddressLine2"];
                candidate.City = Convert.IsDBNull(dr_Candidate["v_City"]) ? "" : (string)dr_Candidate["v_City"];
                candidate.Country = Convert.IsDBNull(dr_Candidate["v_Country"]) ? "" : (string)dr_Candidate["v_Country"];
                candidate.State = Convert.IsDBNull(dr_Candidate["v_State"]) ? "" : (string)dr_Candidate["v_State"];
                candidate.Gender = Convert.ToChar(dr_Candidate["c_Gender"]);
                candidate.MaritialStatus = Convert.ToChar(dr_Candidate["c_MaritialStatus"]);
                candidate.PhoneResidence = Convert.IsDBNull(dr_Candidate["v_PhoneResidence"]) ? "" : (string)dr_Candidate["v_PhoneResidence"];
                candidate.PhoneOffice = Convert.IsDBNull(dr_Candidate["v_PhoneOffice"]) ? "" : (string)dr_Candidate["v_PhoneOffice"];
                candidate.PhoneMobile = Convert.IsDBNull(dr_Candidate["v_PhoneMobile"]) ? "" : (string)dr_Candidate["v_PhoneMobile"];
                candidate.Email = Convert.IsDBNull(dr_Candidate["v_Email"]) ? "" : (string)dr_Candidate["v_Email"];
                candidate.KeySkills = Convert.IsDBNull(dr_Candidate["v_KeySkills"]) ? "" : (string)dr_Candidate["v_KeySkills"];
                candidate.ResumeHeadline = Convert.IsDBNull(dr_Candidate["v_ResumeHeadline"]) ? "" : (string)dr_Candidate["v_ResumeHeadline"];
                candidate.Summary = Convert.IsDBNull(dr_Candidate["v_Summary"]) ? "" : (string)dr_Candidate["v_Summary"];
                candidate.FunctionalAreaName = Convert.IsDBNull(dr_Candidate["v_FunctionalAreaName"]) ? "" : (string)dr_Candidate["v_FunctionalAreaName"];
                candidate.Domain = Convert.IsDBNull(dr_Candidate["v_Domain"]) ? "" : (string)dr_Candidate["v_Domain"];


                //candidate.TotalExperience = Convert.IsDBNull(dr_Candidate["n_TotalExperience"]) ? "" : (string)dr_Candidate["n_TotalExperience"];
                candidate.total_exp = (int)dr_Candidate["n_TotalExperience"];                
              
                
                candidate.RelevantExperience = Convert.IsDBNull(dr_Candidate["n_RelevantExperience"]) ? "" : (string)dr_Candidate["n_RelevantExperience"];
                candidate.JobStatusName = Convert.IsDBNull(dr_Candidate["v_JobStatusName"]) ? "" : (string)dr_Candidate["v_JobStatusName"];
                //candidate.Username = Convert.IsDBNull(dr_Candidate["v_Username"]) ? "" : (string)dr_Candidate["v_Username"];
                //candidate.Password = Convert.IsDBNull(dr_Candidate["Password"]) ? "" : (string)dr_Candidate["Password"];
                ProfileEditing.Add(candidate);
            }
            return ProfileEditing;
        }

        //---------------------------------------------------------------------------------------------------------------------------------------------------

        public Collection<Candidate> fn_EducationGridEdit(int ECourseId,int c_id) 
        {
            Collection<Candidate> EducationGridEditing = new Collection<Candidate>();
            _Connection = Con.fn_Connection();
            string _SqlString = "select e.pn_CourseID, c.v_CourseName , e.v_Specialisation, i.v_InstitutionName ,";
            _SqlString += " e.n_Percentage,e.n_CompletedMonth,e.n_CompletedYear from hrmm_CandidateEducation e, hrmm_Course c, hrmm_Institution i";
            _SqlString += " where c.pn_CourseID =e.pn_CourseID and i.pn_InstitutionID =e.fn_InstitutionID and e.pn_CandidateID ="+c_id+" and e.pn_CourseID = " + ECourseId;
            SqlCommand cmd = new SqlCommand(_SqlString, _Connection);
            _Connection.Open();
            SqlDataReader dr_Candidate = cmd.ExecuteReader();
            while (dr_Candidate.Read())
            {
                Candidate candidate = new Candidate();
                candidate.CourseID = (int)dr_Candidate["pn_CourseID"];
                candidate.CourseName = Convert.IsDBNull(dr_Candidate["v_CourseName"]) ? "" : (string)dr_Candidate["v_CourseName"];
                candidate.Specialisation = Convert.IsDBNull(dr_Candidate["v_Specialisation"]) ? "" : (string)dr_Candidate["v_Specialisation"];
                candidate.InstitutionName = Convert.IsDBNull(dr_Candidate["v_InstitutionName"]) ? "" : (string)dr_Candidate["v_InstitutionName"];
                candidate.Percentage = Convert.IsDBNull(dr_Candidate["n_Percentage"]) ? "" : (string)dr_Candidate["n_Percentage"];
                candidate.CompletedMonth = Convert.IsDBNull(dr_Candidate["n_CompletedMonth"]) ? "" : (string)dr_Candidate["n_CompletedMonth"];
                candidate.CompletedYear = Convert.IsDBNull(dr_Candidate["n_CompletedYear"]) ? 0 : (int)dr_Candidate["n_CompletedYear"];
                EducationGridEditing.Add(candidate);
            }
            return EducationGridEditing;

        }

        //------------------------------------------------------------------------------------------------

        public Collection<Candidate> fn_LanguageGridEdit(int ESeqID,int c_id)
        {
            Collection<Candidate> LanguageGridEditing = new Collection<Candidate>();
            _Connection = Con.fn_Connection();
            string _SqlString = "select pn_SeqID, v_LanguageName, c_Read, c_Write, c_Speak from hrmm_CandidateLanguagesKnown where pn_CandidateID = "+c_id+" and pn_SeqID = " + ESeqID;
            SqlCommand _SSCandidate = new SqlCommand(_SqlString, _Connection);
            _Connection.Open();
            SqlDataReader dr_Candidate = _SSCandidate.ExecuteReader();
            while (dr_Candidate.Read())
            {
                Candidate candidate = new Candidate();
                candidate.LanguageSeqID = (int)dr_Candidate["pn_SeqID"];
                candidate.LanguageName = Convert.IsDBNull(dr_Candidate["v_LanguageName"]) ? "" : (string)dr_Candidate["v_LanguageName"];
                candidate.Read = Convert.ToChar(dr_Candidate["c_Read"]);
                candidate.Write = Convert.ToChar(dr_Candidate["c_Write"]);
                candidate.Speak = Convert.ToChar(dr_Candidate["c_Speak"]);
                LanguageGridEditing.Add(candidate);

            }
            return LanguageGridEditing;
        }

        //------------------------------------------------------------------------------------------------

        public Collection<Candidate> fn_ProjectGridEdit(int ESeqID,int c_id)
        {
            Collection<Candidate> projectGridEditing = new Collection<Candidate>();
            _Connection = Con.fn_Connection();
            string _Sqlstring = "select pn_SeqID, v_ProjectTitle, v_ClientName, n_FromDateMonth, n_FromDateYear,";
            _Sqlstring += "n_ToDateMonth, n_ToDateYear, v_Location, c_ImplementationType, fn_JobStatusID, v_Projectdescription,";
            _Sqlstring += "v_Role, v_Designation, n_TeamSize, v_SkillsUsed from hrmm_CandidateProjects where pn_CandidateID = "+c_id+" and pn_SeqID = " + ESeqID;
            SqlCommand _SSCandidate = new SqlCommand(_Sqlstring, _Connection);
            _Connection.Open();
            SqlDataReader dr_Candidate = _SSCandidate.ExecuteReader();
            while (dr_Candidate.Read())
            {
                Candidate candidate = new Candidate();
                candidate.ProjectSeqID = (int)dr_Candidate["pn_SeqID"];
                candidate.ProjectTitle = Convert.IsDBNull(dr_Candidate["v_ProjectTitle"]) ? "" : (string)dr_Candidate["v_ProjectTitle"];
                candidate.ClientName = Convert.IsDBNull(dr_Candidate["v_ClientName"]) ? "" : (string)dr_Candidate["v_ClientName"];
                candidate.ProjectFromDateMonth = Convert.IsDBNull(dr_Candidate["n_FromDateMonth"]) ? "" : (string)dr_Candidate["n_FromDateMonth"];
                candidate.ProjectFromDateYear = (int)dr_Candidate["n_FromDateYear"];
                candidate.ProjectToDateMonth = Convert.IsDBNull(dr_Candidate["n_ToDateMonth"]) ? "" : (string)dr_Candidate["n_ToDateMonth"];
                candidate.ProjectToDateYear = (int)dr_Candidate["n_ToDateYear"];
                candidate.ProjectLocation = Convert.IsDBNull(dr_Candidate["v_Location"]) ? "" : (string)dr_Candidate["v_Location"];
                candidate.ImplementationType = Convert.ToChar(dr_Candidate["c_ImplementationType"]);
                candidate.JobStatusID = (int)dr_Candidate["fn_JobStatusID"];
                candidate.Projectdescription = Convert.IsDBNull(dr_Candidate["v_Projectdescription"]) ? "" : (string)dr_Candidate["v_Projectdescription"];
                candidate.ProjectRole = Convert.IsDBNull(dr_Candidate["v_Role"]) ? "" : (string)dr_Candidate["v_Role"];
                candidate.ProjectDesignation = Convert.IsDBNull(dr_Candidate["v_Designation"]) ? "" : (string)dr_Candidate["v_Designation"];
                candidate.TeamSize = (int)dr_Candidate["n_TeamSize"];
                candidate.SkillsUsed = Convert.IsDBNull(dr_Candidate["v_SkillsUsed"]) ? "" : (string)dr_Candidate["v_SkillsUsed"];
                projectGridEditing.Add(candidate);
            }
            return projectGridEditing;
        }

        //------------------------------------------------------------------------------------------------

        public Collection<Candidate> fn_ReferenceGridEdit(int EReferenceID,int c_id)
        {
            Collection<Candidate> ReferenceGridEditing = new Collection<Candidate>();
            _Connection = Con.fn_Connection();
            string _Sqlstring = "select pn_ReferenceID, v_ReferencePerson, v_Company, v_Designation, v_Relationship,";
            _Sqlstring += "v_Phone, v_Email from hrmm_CandidateReferences where pn_CandidateID = "+c_id+" and pn_ReferenceID = " + EReferenceID;
            SqlCommand _SSCandidate = new SqlCommand(_Sqlstring, _Connection);
            _Connection.Open();
            SqlDataReader dr_candidate = _SSCandidate.ExecuteReader();
            while (dr_candidate.Read())
            {
                Candidate candidate = new Candidate();
                candidate.ReferenceID = (int)dr_candidate["pn_ReferenceID"];
                candidate.ReferencePerson = Convert.IsDBNull(dr_candidate["v_ReferencePerson"]) ? "" : (string)dr_candidate["v_ReferencePerson"];
                candidate.ReferenceCompany = Convert.IsDBNull(dr_candidate["v_Company"]) ? "" : (string)dr_candidate["v_Company"];
                candidate.ReferenceDesignation = Convert.IsDBNull(dr_candidate["v_Designation"]) ? "" : (string)dr_candidate["v_Designation"];
                candidate.Relationship = Convert.IsDBNull(dr_candidate["v_Relationship"]) ? "" : (string)dr_candidate["v_Relationship"];
                candidate.ReferencePhone = Convert.IsDBNull(dr_candidate["v_Phone"]) ? "" : (string)dr_candidate["v_Phone"];
                candidate.ReferenceEmail = Convert.IsDBNull(dr_candidate["v_Email"]) ? "" : (string)dr_candidate["v_Email"];
                ReferenceGridEditing.Add(candidate);

            }
            return ReferenceGridEditing;
        }

        //------------------------------------------------------------------------------------------------

        public Collection<Candidate> fn_SkillsGridEdit(int ESkillID,int c_id)
        {
            Collection<Candidate> skillsGridEditing = new Collection<Candidate>();
            _Connection = Con.fn_Connection();
            string _Sqlstring = "select sm.v_SkillName, cs.pn_SkillID, cs.n_LastUsedYear, cs.v_LastUsedMonth,";
            _Sqlstring += " cs.c_ProficiencyLevel, cs.n_Experience from hrmm_CandidateSkills cs, hrmm_SkillsMaster sm";
            _Sqlstring += " where sm.pn_SkillID =cs.pn_SkillID and cs.pn_CandidateID ="+c_id+" and cs.pn_SkillID = " + ESkillID;
            SqlCommand _SSCandidate = new SqlCommand(_Sqlstring, _Connection);
            _Connection.Open();
            SqlDataReader dr_candidate = _SSCandidate.ExecuteReader();
            while (dr_candidate.Read())
            {
                Candidate candidate = new Candidate();
                candidate.SkillName = Convert.IsDBNull(dr_candidate["v_SkillName"]) ? "" : (string)dr_candidate["v_SkillName"];
                candidate.SkillID = (int)dr_candidate["pn_SkillID"];
                candidate.LastUsedYear = (int)dr_candidate["n_LastUsedYear"];
                candidate.LastUsedMonth = Convert.IsDBNull(dr_candidate["v_LastUsedMonth"]) ? "" : (string)dr_candidate["v_LastUsedMonth"];
                candidate.ProficiencyLevel = Convert.IsDBNull(dr_candidate["c_ProficiencyLevel"]) ? "" : (string)dr_candidate["c_ProficiencyLevel"];
                candidate.Experience = Convert.IsDBNull(dr_candidate["n_Experience"]) ? "" : (string)dr_candidate["n_Experience"];
                skillsGridEditing.Add(candidate);
            }
            return skillsGridEditing;
        }

        //------------------------------------------------------------------------------------------------

        public Collection<Candidate> fn_WorkHistoryGridEdit(int EWorkHistorySeqID,int c_id)
        {
            Collection<Candidate> WorkHistoryGridEditing = new Collection<Candidate>();
            _Connection = Con.fn_Connection();
            string _Sqlstring = "select pn_SeqID, n_FromDateMonth, n_FromDateYear, n_ToDateMonth,";
            _Sqlstring += "n_ToDateYear, v_CompanyName, v_CompanyLocation, v_DesignationCode,";
            _Sqlstring += "n_Salary, v_Role, v_Responsibility from hrmm_candidateworkhistory where pn_CandidateID =" + c_id + " and pn_SeqID = " + EWorkHistorySeqID;
            SqlCommand _SSCandidate = new SqlCommand(_Sqlstring, _Connection);
            _Connection.Open();
            SqlDataReader dr_candidate = _SSCandidate.ExecuteReader();
            while (dr_candidate.Read())
            {
                Candidate candidate = new Candidate();
                candidate.WorkHistorySeqID = (int)dr_candidate["pn_SeqID"];
                candidate.WorkHistoryFromDateMonth = Convert.IsDBNull(dr_candidate["n_FromDateMonth"]) ? "" : (string)dr_candidate["n_FromDateMonth"];
                candidate.WorkHistoryFromDateYear = (int)dr_candidate["n_FromDateYear"];
                candidate.WorkHistoryToDateMonth = Convert.IsDBNull(dr_candidate["n_ToDateMonth"]) ? "" : (string)dr_candidate["n_ToDateMonth"];
                candidate.WorkHistoryToDateYear = (int)dr_candidate["n_ToDateYear"];
                candidate.CompanyName = Convert.IsDBNull(dr_candidate["v_CompanyName"]) ? "" : (string)dr_candidate["v_CompanyName"];
                candidate.CompanyLocation = Convert.IsDBNull(dr_candidate["v_CompanyLocation"]) ? "" : (string)dr_candidate["v_CompanyLocation"];
                candidate.DesignationCode = Convert.IsDBNull(dr_candidate["v_DesignationCode"]) ? "" : (string)dr_candidate["v_DesignationCode"];
                candidate.Salary = Convert.IsDBNull(dr_candidate["n_Salary"]) ? "" : (string)dr_candidate["n_Salary"];
                candidate.Role = Convert.IsDBNull(dr_candidate["v_Role"]) ? "" : (string)dr_candidate["v_Role"];
                candidate.Responsibility = Convert.IsDBNull(dr_candidate["v_Responsibility"]) ? "" : (string)dr_candidate["v_Responsibility"];
                WorkHistoryGridEditing.Add(candidate);
            }

            return WorkHistoryGridEditing;
        }

        //------------------------------------------------------------------------------------------------

        public Collection<Candidate> fn_getusername(string uid)
        {
            Collection<Candidate> InstitutionList = new Collection<Candidate>();
            _Connection = Con.fn_Connection();
            SqlCommand _SUsername = new SqlCommand("select v_Username from hrmm_Registration where v_Username='" + uid + "'", _Connection);
            _Connection.Open();
            SqlDataReader dr_Username = _SUsername.ExecuteReader();

            while (dr_Username.Read())
            {
                Candidate candidate = new Candidate();
                //candidate.InstitutionID = (int)dr_Institution["pn_InstitutionID"];
                //candidate.InstitutionName = Convert.IsDBNull(dr_Institution["v_InstitutionName"]) ? "" : (string)dr_Institution["v_InstitutionName"];
                InstitutionList.Add(candidate);
            }
            return InstitutionList;
        }

        
        //----------------------------------------------------------------------------------
        
        public Collection<Candidate> fn_getmail(string mail)
        {
            Collection<Candidate> InstitutionList = new Collection<Candidate>();
            _Connection = Con.fn_Connection();
            SqlCommand _SMail = new SqlCommand("select v_Username from hrmm_Registration where v_Email='" + mail + "'", _Connection);
            _Connection.Open();
            SqlDataReader dr_Mail = _SMail.ExecuteReader();
            while (dr_Mail.Read())
            {
                Candidate candidate = new Candidate();
                //candidate.InstitutionID = (int)dr_Institution["pn_InstitutionID"];
                //candidate.InstitutionName = Convert.IsDBNull(dr_Institution["v_InstitutionName"]) ? "" : (string)dr_Institution["v_InstitutionName"];
                InstitutionList.Add(candidate);
            }
            return InstitutionList;
        }
        
        //---------------------------------------------------------------------------------------

        public Collection<Candidate> fn_username(string Usernameid)
        {
            Collection<Candidate> InstitutionList = new Collection<Candidate>();
            _Connection = Con.fn_Connection();
            SqlCommand _SUsername = new SqlCommand("select pn_CandidateID from hrmm_CandidateProfile where v_Username='" + Usernameid + "'", _Connection);
            _Connection.Open();
            SqlDataReader dr_Username = _SUsername.ExecuteReader();

            while (dr_Username.Read())
            {
                Candidate candidate = new Candidate();
                candidate.CandidateID = (int)dr_Username["pn_CandidateID"];
                //candidate.InstitutionName = Convert.IsDBNull(dr_Institution["v_InstitutionName"]) ? "" : (string)dr_Institution["v_InstitutionName"];
                InstitutionList.Add(candidate);
            }
            return InstitutionList;
        }
       //--------------------------------------------------------------------------------------------


     public string get_Text(char c_txt)
        {
            string str="";

            switch(c_txt)
            {
             case 'M': str = "Male";
                          break;
             case 'F': str = "Female";
                          break;
             case 'K': str = "Married";
                          break;
             case 'u': str = "Unmarried";
                          break;
             case 'e': str = "Employee";
                          break;
             case 'W': str = "Web";
                          break;
             case 'C': str = "Consultancy";
                          break;
             case 'O': str = "Onsite";
                          break;
             case 'f': str = "Offsite";
                          break;
             case 'Y': str = "Yes";
                          break;
             case 'N': str = "No";
                          break;

           }

            return str;
        }


        //Employee workhistory details


        public string Emp_WorkHistorySave(Candidate c)
        {
            try
            {
                _Connection = Con.fn_Connection();
                SqlCommand _ISPCandidate = new SqlCommand("sp_Employee_WorkHistorysave", _Connection);
                _ISPCandidate.CommandType = CommandType.StoredProcedure;
                SqlParameter[] _ISPCandidateParam = new SqlParameter[14];
                _ISPCandidateParam[0] = new SqlParameter("@pn_CompanyID", SqlDbType.Int);
                _ISPCandidateParam[0].Value = c.CompanyID;
                _ISPCandidateParam[1] = new SqlParameter("@pn_BranchID", SqlDbType.Int);
                _ISPCandidateParam[1].Value = c.BranchID;
                _ISPCandidateParam[2] = new SqlParameter("@pn_EmployeeID", SqlDbType.Int);
                _ISPCandidateParam[2].Value = c.EmployeeID;
                _ISPCandidateParam[3] = new SqlParameter("@pn_SeqID", SqlDbType.Int);
                _ISPCandidateParam[3].Value = c.WorkHistorySeqID;
                _ISPCandidateParam[4] = new SqlParameter("@n_FromDateMonth", SqlDbType.VarChar);
                _ISPCandidateParam[4].Value = c.WorkHistoryFromDateMonth;
                _ISPCandidateParam[5] = new SqlParameter("@n_FromDateYear", SqlDbType.Int);
                _ISPCandidateParam[5].Value = c.WorkHistoryFromDateYear;
                _ISPCandidateParam[6] = new SqlParameter("@n_ToDateMonth", SqlDbType.VarChar);
                _ISPCandidateParam[6].Value = c.WorkHistoryToDateMonth;
                _ISPCandidateParam[7] = new SqlParameter("@n_ToDateYear", SqlDbType.Int);
                _ISPCandidateParam[7].Value = c.WorkHistoryToDateYear;
                _ISPCandidateParam[8] = new SqlParameter("@v_CompanyName", SqlDbType.VarChar);
                _ISPCandidateParam[8].Value = c.CompanyName;
                _ISPCandidateParam[9] = new SqlParameter("@v_CompanyLocation", SqlDbType.VarChar);
                _ISPCandidateParam[9].Value = c.CompanyLocation;
                _ISPCandidateParam[10] = new SqlParameter("@v_DesignationCode", SqlDbType.VarChar);
                _ISPCandidateParam[10].Value = c.DesignationCode;
                _ISPCandidateParam[11] = new SqlParameter("@n_Salary", SqlDbType.VarChar);
                _ISPCandidateParam[11].Value = c.Salary;
                _ISPCandidateParam[12] = new SqlParameter("@v_Role", SqlDbType.VarChar);
                _ISPCandidateParam[12].Value = c.Role;
                _ISPCandidateParam[13] = new SqlParameter("@v_Responsibility", SqlDbType.VarChar);
                _ISPCandidateParam[13].Value = c.Responsibility;
                for (int i = 0; i < _ISPCandidateParam.Length; i++)
                {
                    _ISPCandidate.Parameters.Add(_ISPCandidateParam[i]);
                }
                _Connection.Open();
                _ISPCandidate.ExecuteNonQuery();
                _Connection.Close();
                return "Records Saved Successfully";

            }
            catch (SqlException e1)
            {
                return e1.ToString();
            }

        }

        public string Emp_WorkHistoryUpdate(Candidate c)
        {
            try
            {
                _Connection = Con.fn_Connection();
                SqlCommand _ISPCandidate = new SqlCommand("sp_Employee_WorkHistory", _Connection);
                _ISPCandidate.CommandType = CommandType.StoredProcedure;
                SqlParameter[] _ISPCandidateParam = new SqlParameter[14];
                _ISPCandidateParam[0] = new SqlParameter("@pn_CompanyID", SqlDbType.Int);
                _ISPCandidateParam[0].Value = c.CompanyID;
                _ISPCandidateParam[1] = new SqlParameter("@pn_BranchID", SqlDbType.Int);
                _ISPCandidateParam[1].Value = c.BranchID;
                _ISPCandidateParam[2] = new SqlParameter("@pn_EmployeeID", SqlDbType.Int);
                _ISPCandidateParam[2].Value = c.EmployeeID;
                _ISPCandidateParam[3] = new SqlParameter("@pn_SeqID", SqlDbType.Int);
                _ISPCandidateParam[3].Value = c.WorkHistorySeqID;
                _ISPCandidateParam[4] = new SqlParameter("@n_FromDateMonth", SqlDbType.VarChar);
                _ISPCandidateParam[4].Value = c.WorkHistoryFromDateMonth;
                _ISPCandidateParam[5] = new SqlParameter("@n_FromDateYear", SqlDbType.Int);
                _ISPCandidateParam[5].Value = c.WorkHistoryFromDateYear;
                _ISPCandidateParam[6] = new SqlParameter("@n_ToDateMonth", SqlDbType.VarChar);
                _ISPCandidateParam[6].Value = c.WorkHistoryToDateMonth;
                _ISPCandidateParam[7] = new SqlParameter("@n_ToDateYear", SqlDbType.Int);
                _ISPCandidateParam[7].Value = c.WorkHistoryToDateYear;
                _ISPCandidateParam[8] = new SqlParameter("@v_CompanyName", SqlDbType.VarChar);
                _ISPCandidateParam[8].Value = c.CompanyName;
                _ISPCandidateParam[9] = new SqlParameter("@v_CompanyLocation", SqlDbType.VarChar);
                _ISPCandidateParam[9].Value = c.CompanyLocation;
                _ISPCandidateParam[10] = new SqlParameter("@v_DesignationCode", SqlDbType.VarChar);
                _ISPCandidateParam[10].Value = c.DesignationCode;
                _ISPCandidateParam[11] = new SqlParameter("@n_Salary", SqlDbType.VarChar);
                _ISPCandidateParam[11].Value = c.Salary;
                _ISPCandidateParam[12] = new SqlParameter("@v_Role", SqlDbType.VarChar);
                _ISPCandidateParam[12].Value = c.Role;
                _ISPCandidateParam[13] = new SqlParameter("@v_Responsibility", SqlDbType.VarChar);
                _ISPCandidateParam[13].Value = c.Responsibility;
                for (int i = 0; i < _ISPCandidateParam.Length; i++)
                {
                    _ISPCandidate.Parameters.Add(_ISPCandidateParam[i]);
                }
                _Connection.Open();
                _ISPCandidate.ExecuteNonQuery();
                _Connection.Close();
                return "Records Saved Successfully";

            }
            catch (SqlException e1)
            {
                return e1.ToString();
            }

        }

        public Collection<Candidate> fn_get_Employee_WorkHistory(int c_id)
        {
            Collection<Candidate> WorkHistoryList = new Collection<Candidate>();
            _Connection = Con.fn_Connection();
            string _Sqlstring = "select pn_SeqID, n_FromDateMonth, n_FromDateYear, n_ToDateMonth,";
            _Sqlstring += "n_ToDateYear, v_CompanyName, v_CompanyLocation, v_DesignationCode,";
            _Sqlstring += "n_Salary, v_Role, v_Responsibility from paym_Employee_WorkHistory where pn_EmployeeID =" + c_id + "";
            SqlCommand _SSCandidate = new SqlCommand(_Sqlstring, _Connection);
            _Connection.Open();
            SqlDataReader dr_candidate = _SSCandidate.ExecuteReader();
            while (dr_candidate.Read())
            {
                Candidate candidate = new Candidate();
                candidate.WorkHistorySeqID = (int)dr_candidate["pn_SeqID"];
                candidate.WorkHistoryFromDateMonth = Convert.IsDBNull(dr_candidate["n_FromDateMonth"]) ? "" : (string)dr_candidate["n_FromDateMonth"];
                candidate.WorkHistoryFromDateYear = (int)dr_candidate["n_FromDateYear"];
                candidate.WorkHistoryToDateMonth = Convert.IsDBNull(dr_candidate["n_ToDateMonth"]) ? "" : (string)dr_candidate["n_ToDateMonth"];
                candidate.WorkHistoryToDateYear = (int)dr_candidate["n_ToDateYear"];
                candidate.CompanyName = Convert.IsDBNull(dr_candidate["v_CompanyName"]) ? "" : (string)dr_candidate["v_CompanyName"];
                candidate.CompanyLocation = Convert.IsDBNull(dr_candidate["v_CompanyLocation"]) ? "" : (string)dr_candidate["v_CompanyLocation"];
                candidate.DesignationCode = Convert.IsDBNull(dr_candidate["v_DesignationCode"]) ? "" : (string)dr_candidate["v_DesignationCode"];
                candidate.Salary = Convert.IsDBNull(dr_candidate["n_Salary"]) ? "" : (string)dr_candidate["n_Salary"];
                candidate.Role = Convert.IsDBNull(dr_candidate["v_Role"]) ? "" : (string)dr_candidate["v_Role"];
                candidate.Responsibility = Convert.IsDBNull(dr_candidate["v_Responsibility"]) ? "" : (string)dr_candidate["v_Responsibility"];
                WorkHistoryList.Add(candidate);
            }

            return WorkHistoryList;
        }

        public Collection<Candidate> fn_Employee_WorkHistoryGridEdit(int EWorkHistorySeqID, int c_id)
        {
            Collection<Candidate> WorkHistoryGridEditing = new Collection<Candidate>();
            _Connection = Con.fn_Connection();
            string _Sqlstring = "select pn_SeqID, n_FromDateMonth, n_FromDateYear, n_ToDateMonth,";
            _Sqlstring += "n_ToDateYear, v_CompanyName, v_CompanyLocation, v_DesignationCode,";
            _Sqlstring += "n_Salary, v_Role, v_Responsibility from paym_Employee_WorkHistory where pn_EmployeeID =" + c_id + " and pn_SeqID = " + EWorkHistorySeqID;
            SqlCommand _SSCandidate = new SqlCommand(_Sqlstring, _Connection);
            _Connection.Open();
            SqlDataReader dr_candidate = _SSCandidate.ExecuteReader();
            while (dr_candidate.Read())
            {
                Candidate candidate = new Candidate();
                candidate.WorkHistorySeqID = (int)dr_candidate["pn_SeqID"];
                candidate.WorkHistoryFromDateMonth = Convert.IsDBNull(dr_candidate["n_FromDateMonth"]) ? "" : (string)dr_candidate["n_FromDateMonth"];
                candidate.WorkHistoryFromDateYear = (int)dr_candidate["n_FromDateYear"];
                candidate.WorkHistoryToDateMonth = Convert.IsDBNull(dr_candidate["n_ToDateMonth"]) ? "" : (string)dr_candidate["n_ToDateMonth"];
                candidate.WorkHistoryToDateYear = (int)dr_candidate["n_ToDateYear"];
                candidate.CompanyName = Convert.IsDBNull(dr_candidate["v_CompanyName"]) ? "" : (string)dr_candidate["v_CompanyName"];
                candidate.CompanyLocation = Convert.IsDBNull(dr_candidate["v_CompanyLocation"]) ? "" : (string)dr_candidate["v_CompanyLocation"];
                candidate.DesignationCode = Convert.IsDBNull(dr_candidate["v_DesignationCode"]) ? "" : (string)dr_candidate["v_DesignationCode"];
                candidate.Salary = Convert.IsDBNull(dr_candidate["n_Salary"]) ? "" : (string)dr_candidate["n_Salary"];
                candidate.Role = Convert.IsDBNull(dr_candidate["v_Role"]) ? "" : (string)dr_candidate["v_Role"];
                candidate.Responsibility = Convert.IsDBNull(dr_candidate["v_Responsibility"]) ? "" : (string)dr_candidate["v_Responsibility"];
                WorkHistoryGridEditing.Add(candidate);
            }

            return WorkHistoryGridEditing;
        }



    }
}
