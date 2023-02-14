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
using System.Collections;

namespace ePayHrms.Leave
{
    /// <summary>
    /// Summary description for Leave
    /// </summary>
    public class Leave
    {
        public Leave()
        {
            //
            // TODO: Add constructor logic here
            //
        }


        private SqlConnection _Connection;
        ePayHrms.Connection.Connection Con = new ePayHrms.Connection.Connection();

        
        private int _CompanyID;
        public int _leaveID;
        private int _AppraisalID;
        private string _AppraisalName;
        private int _BonusID;
        private string _BonusName;
        private string _leaveName;
        private string _leaveCode;
        private string _AnnualLeave;
        private int _MaxDays;
        private string _EL;
        private string _Type;


        private int _Count;
        private double _Count1;
        private double _Count2;
        private double _Cur_Leave;
        private double _temp_double;
        private int _BranchID;
        private int _EmployeeID;
        private int _month;


        //private string _Leaveby;//mei
        private string _LeaveBY;//mei

        private string _str_month;
        private string _From_status;
        private string _To_status;
        private int _month2;
        private int _year;
        private int _To_year;
        private int _IncrementID;
        private int _start_point;
        private int _last_point;
        private int _increment;
        private int _totalpoint;
        private int _Availed_Days;
        private int _Allowed_Days;
        private char _status;
        private DateTime _d_appraisal;
        private DateTime _from_date;
        private DateTime _to_date;
        private string _str_from_date;
        private string _str_to_date;
        private int _AppraisalmasterId;
        private string _AppraisalmasterName;
        private string _AppraisalmasterCode;

        //public string Leaveby
        //{
        //    get { return _Leaveby; }
        //    set { _Leaveby = value; }
        //}

        public string LeaveBY
        {
            get { return _LeaveBY; }
            set { _LeaveBY = value; }
        }

        public int CompanyID
        {
            get { return _CompanyID; }
            set { _CompanyID = value; }
        }

        public char status
        {
            get { return _status; }
            set { _status = value; }
        }

        public int leaveID
        {
            get { return _leaveID; }
            set { _leaveID = value; }
        }


        public double temp_double
        {
            get { return _temp_double; }
            set { _temp_double = value; }
        }

        public int AppraisalID
        {
            get { return _AppraisalID; }
            set { _AppraisalID = value; }
        }

        public int BonusID
        {
            get { return _BonusID; }
            set { _BonusID = value; }
        }

        public string leaveName
        {
            get { return _leaveName; }
            set { _leaveName = value; }
        }

        public string AnnualLeave
        {
            get { return _AnnualLeave; }
            set { _AnnualLeave = value; }
        }

        public int MaxDays
        {
            get { return _MaxDays; }
            set { _MaxDays = value; }
        }

        public string EL
        {
            get { return _EL; }
            set { _EL = value; }
        }

        public string Type
        {
            get { return _Type; }
            set { _Type = value; }
        }

        public string AppraisalName
        {
            get { return _AppraisalName; }
            set { _AppraisalName = value; }
        }

        public string BonusName
        {
            get { return _BonusName; }
            set { _BonusName = value; }
        }

        public string leaveCode
        {
            get { return _leaveCode; }
            set { _leaveCode = value; }
        }



        public DateTime fromdate
        {
            get { return _from_date; }
            set { _from_date = value; }
        }


        public DateTime todate
        {
            get { return _to_date; }
            set { _to_date = value; }
        }


        public string str_fromdate
        {
            get { return _str_from_date; }
            set { _str_from_date = value; }
        }


        public string str_todate
        {
            get { return _str_to_date; }
            set { _str_to_date = value; }
        }


        public int Count
        {
            get { return _Count; }
            set { _Count = value; }
        }

        public double Count1
        {
            get { return _Count1; }
            set { _Count1 = value; }
        }

        public double Count2
        {
            get { return _Count2; }
            set { _Count2 = value; }
        }


        public double Cur_Leave
        {
            get { return _Cur_Leave; }
            set { _Cur_Leave = value; }
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


        public int month
        {
            get { return _month; }
            set { _month = value; }
        }

        public string str_month
        {
            get { return _str_month; }
            set { _str_month = value; }
        }


        public string From_status
        {
            get { return _From_status; }
            set { _From_status = value; }
        }


        public string To_status
        {
            get { return _To_status; }
            set { _To_status = value; }
        }

        public int month2
        {
            get { return _month2; }
            set { _month2 = value; }
        }

        public int year
        {
            get { return _year; }
            set { _year = value; }
        }

        public int To_year
        {
            get { return _To_year; }
            set { _To_year = value; }
        }


        public DateTime d_appraisal
        {
            get { return _d_appraisal; }
            set { _d_appraisal = value; }
        }


        public int IncrementID
        {
            get { return _IncrementID; }
            set { _IncrementID = value; }
        }


        public int startpoint
        {
            get { return _start_point; }
            set { _start_point = value; }
        }


        public int Availed_Days
        {
            get { return _Availed_Days; }
            set { _Availed_Days = value; }
        }

        public int Allowed_Days
        {
            get { return _Allowed_Days; }
            set { _Allowed_Days = value; }
        }


        public int totalpoint
        {
            get { return _totalpoint; }
            set { _totalpoint = value; }
        }



        public int lastpoint
        {
            get { return _last_point; }
            set { _last_point = value; }
        }



        public int increment
        {
            get { return _increment; }
            set { _increment = value; }
        }


        public int AppraisalmasterID
        {
            get { return _AppraisalmasterId; }
            set { _AppraisalmasterId = value; }
        }


        public string AppraisalmasterName
        {
            get { return _AppraisalmasterName; }
            set { _AppraisalmasterName = value; }
        }


        public string AppraisalmasterCode
        {
            get { return _AppraisalmasterCode; }
            set { _AppraisalmasterCode = value; }
        }


        private int _Departmentid;

        public int Departmentid
        {
            get { return _Departmentid; }
            set { _Departmentid = value; }
        }

        private int _Divisionid;

        public int Divisionid
        {
            get { return _Divisionid; }
            set { _Divisionid = value; }
        }

        private int _Categoryid;

        public int Categoryid
        {
            get { return _Categoryid; }
            set { _Categoryid = value; }
        }

        private string _Flag;

        public string Flag
        {
            get { return _Flag; }
            set { _Flag = value; }
        }

        private string _Feedtype;

        public string Feedtype
        {
            get { return _Feedtype; }
            set { _Feedtype = value; }
        }

        private string _Point;

        public string Point
        {
            get { return _Point; }
            set { _Point = value; }
        }

        private string _Departmentname;

        public string Departmentname
        {
            get { return _Departmentname; }
            set { _Departmentname = value; }
        }

        private string _Fond;

        public string Fond
        {
            get { return _Fond; }
            set { _Fond = value; }
        }

        private double _From_Value;

        public double From_Value
        {
            get { return _From_Value; }
            set { _From_Value = value; }
        }

        private double _To_Value;

        public double To_Value
        {
            get { return _To_Value; }
            set { _To_Value = value; }
        }

        private int _Bonus_id;

        public int Bonus_id
        {
            get { return _Bonus_id; }
            set { _Bonus_id = value; }
        }

        private int _Bonus_points;

        public int Bonus_points
        {
            get { return _Bonus_points; }
            set { _Bonus_points = value; }
        }
       
        //************************************************************************************
        
        public string Leave_First(Leave l)
        {
            try
            {
                _Connection = Con.fn_Connection();


                SqlCommand _Cmd = new SqlCommand("sp_paym_leave", _Connection);
                _Cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter[] _ISPParamter = new SqlParameter[11];

                _ISPParamter[0] = new SqlParameter("@pn_CompanyID", SqlDbType.Int);
                _ISPParamter[0].Value = l.CompanyID;
                _ISPParamter[1] = new SqlParameter("@pn_leaveID", SqlDbType.Int);
                _ISPParamter[1].Value = l.leaveID;
                _ISPParamter[2] = new SqlParameter("@v_leaveName", SqlDbType.VarChar);
                _ISPParamter[2].Value = l.leaveName;
                _ISPParamter[3] = new SqlParameter("@pn_leaveCode", SqlDbType.VarChar);
                _ISPParamter[3].Value = l.leaveCode;
                _ISPParamter[4] = new SqlParameter("@pn_Count", SqlDbType.Int);
                _ISPParamter[4].Value = l.Count;

                _ISPParamter[5] = new SqlParameter("@annual_leave", SqlDbType.VarChar);
                _ISPParamter[5].Value = l.AnnualLeave;
                _ISPParamter[6] = new SqlParameter("@max_days", SqlDbType.Int);
                _ISPParamter[6].Value = l.MaxDays;
                _ISPParamter[7] = new SqlParameter("@EL ", SqlDbType.VarChar);
                _ISPParamter[7].Value = l.Point;
                _ISPParamter[8] = new SqlParameter("@type ", SqlDbType.VarChar);
                _ISPParamter[8].Value = l.Flag;

                _ISPParamter[9] = new SqlParameter("@status", SqlDbType.VarChar);
                _ISPParamter[9].Value = l.status;
                _ISPParamter[10] = new SqlParameter("@pn_BranchID", SqlDbType.Int);
                _ISPParamter[10].Value = l.BranchID;

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

        public string Leave_Allocation(Leave l)
        {
            try
            {
                _Connection = Con.fn_Connection();


                SqlCommand _Cmd = new SqlCommand("sp_leaveAllocation", _Connection);
                _Cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter[] _ISPParamter = new SqlParameter[7];

                _ISPParamter[0] = new SqlParameter("@pn_CompanyID", SqlDbType.Int);
                _ISPParamter[0].Value = l.CompanyID;
                _ISPParamter[1] = new SqlParameter("@pn_BranchID", SqlDbType.Int);
                _ISPParamter[1].Value = l.BranchID;
                _ISPParamter[2] = new SqlParameter("@pn_EmployeeID", SqlDbType.Int);
                _ISPParamter[2].Value = l.EmployeeID;
                _ISPParamter[3] = new SqlParameter("@pn_leaveID", SqlDbType.Int);
                _ISPParamter[3].Value = l.leaveID;
                _ISPParamter[4] = new SqlParameter("@n_Count", SqlDbType.Float);
                _ISPParamter[4].Value = l.Count1;
                _ISPParamter[5] = new SqlParameter("@leaveby", SqlDbType.VarChar);
                _ISPParamter[5].Value = l.Flag;
                _ISPParamter[6] = new SqlParameter("@yearend", SqlDbType.Int);
                _ISPParamter[6].Value = l.year;

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

        public string Leave_Month(Leave l)
        {
            try
            {
                _Connection = Con.fn_Connection();


                SqlCommand _Cmd = new SqlCommand("sp_Employee_leave", _Connection);
                _Cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter[] _ISPParamter = new SqlParameter[9];


                _ISPParamter[0] = new SqlParameter("@pn_CompanyID", SqlDbType.Int);
                _ISPParamter[0].Value = l.CompanyID;
                _ISPParamter[1] = new SqlParameter("@pn_BranchID", SqlDbType.Int);
                _ISPParamter[1].Value = l.BranchID;
                _ISPParamter[2] = new SqlParameter("@pn_EmployeeID", SqlDbType.Int);
                _ISPParamter[2].Value = l.EmployeeID;
                _ISPParamter[3] = new SqlParameter("@pn_leaveID", SqlDbType.Int);
                _ISPParamter[3].Value = l.leaveID;
                _ISPParamter[4] = new SqlParameter("@From_Date", SqlDbType.DateTime);
                _ISPParamter[4].Value = l.fromdate;
                _ISPParamter[5] = new SqlParameter("@To_Date", SqlDbType.DateTime);
                _ISPParamter[5].Value = l.todate;
                _ISPParamter[6] = new SqlParameter("@From_Status", SqlDbType.VarChar);
                _ISPParamter[6].Value = l.From_status;
                _ISPParamter[7] = new SqlParameter("@To_Status", SqlDbType.VarChar);
                _ISPParamter[7].Value = l.To_status;
                _ISPParamter[8] = new SqlParameter("@Leave_Count", SqlDbType.Float);
                _ISPParamter[8].Value = l.Cur_Leave;

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


        public string Bonus(Leave e)
        {
            try
            {
                _Connection = Con.fn_Connection();


                SqlCommand _Cmd = new SqlCommand("sp_paym_Bonus", _Connection);
                _Cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter[] _ISPParamter = new SqlParameter[6];
                _ISPParamter[0] = new SqlParameter("@pn_CompanyID", SqlDbType.Int);
                _ISPParamter[0].Value = e.BranchID;
                _ISPParamter[1] = new SqlParameter("@pn_BranchID", SqlDbType.Int);
                _ISPParamter[1].Value = e.BranchID;
                _ISPParamter[2] = new SqlParameter("@pn_BonusID", SqlDbType.Int);
                _ISPParamter[2].Value = e.Bonus_id;
                _ISPParamter[3] = new SqlParameter("@v_BonusName", SqlDbType.VarChar);
                _ISPParamter[3].Value = e.BonusName;
                _ISPParamter[4] = new SqlParameter("@points", SqlDbType.Int);
                _ISPParamter[4].Value = e.totalpoint;
                _ISPParamter[5] = new SqlParameter("@status", SqlDbType.VarChar);
                _ISPParamter[5].Value = e.status;
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


        public string Appraisal(Leave e)
        {
            try
            {
                _Connection = Con.fn_Connection();


                SqlCommand _Cmd = new SqlCommand("sp_paym_Appraisal", _Connection);
                _Cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter[] _ISPParamter = new SqlParameter[5];
                _ISPParamter[0] = new SqlParameter("@pn_CompanyID", SqlDbType.Int);
                _ISPParamter[0].Value = e.CompanyID;
                _ISPParamter[1] = new SqlParameter("@pn_AppraisalID", SqlDbType.Int);
                _ISPParamter[1].Value = e.AppraisalID;
                _ISPParamter[2] = new SqlParameter("@V_AppraisalName", SqlDbType.VarChar);
                _ISPParamter[2].Value = e.AppraisalName;
                _ISPParamter[3] = new SqlParameter("@points", SqlDbType.Int);
                _ISPParamter[3].Value = e.totalpoint;
                _ISPParamter[4] = new SqlParameter("@status", SqlDbType.VarChar);
                _ISPParamter[4].Value = e.status;
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


        public string Emp_Bonus(Leave l)
        {
            try
            {
                _Connection = Con.fn_Connection();


                SqlCommand _Cmd = new SqlCommand("sp_paym_Emp_Bonus", _Connection);
                _Cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter[] _ISPParamter = new SqlParameter[6];


                _ISPParamter[0] = new SqlParameter("@pn_CompanyID", SqlDbType.Int);
                _ISPParamter[0].Value = l.CompanyID;
                _ISPParamter[1] = new SqlParameter("@pn_BranchID", SqlDbType.Int);
                _ISPParamter[1].Value = l.BranchID;
                _ISPParamter[2] = new SqlParameter("@pn_EmployeeID", SqlDbType.Int);
                _ISPParamter[2].Value = l.EmployeeID;
                _ISPParamter[3] = new SqlParameter("@pn_BonusID", SqlDbType.Int);
                _ISPParamter[3].Value = l.AppraisalID;
                _ISPParamter[4] = new SqlParameter("@n_points", SqlDbType.Int);
                _ISPParamter[4].Value = l.Count;
                _ISPParamter[5] = new SqlParameter("@d_Date ", SqlDbType.DateTime);
                _ISPParamter[5].Value = l.d_appraisal;

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


        public string Emp_Appraisal(Leave l)
        {
            try
            {
                _Connection = Con.fn_Connection();


                SqlCommand _Cmd = new SqlCommand("sp_paym_Emp_Appraisal", _Connection);
                _Cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter[] _ISPParamter = new SqlParameter[6];


                _ISPParamter[0] = new SqlParameter("@pn_CompanyID", SqlDbType.Int);
                _ISPParamter[0].Value = l.CompanyID;
                _ISPParamter[1] = new SqlParameter("@pn_BranchID", SqlDbType.Int);
                _ISPParamter[1].Value = l.BranchID;
                _ISPParamter[2] = new SqlParameter("@pn_EmployeeID", SqlDbType.Int);
                _ISPParamter[2].Value = l.EmployeeID;
                _ISPParamter[3] = new SqlParameter("@pn_AppraisalID", SqlDbType.Int);
                _ISPParamter[3].Value = l.AppraisalID;
                _ISPParamter[4] = new SqlParameter("@n_points", SqlDbType.Int);
                _ISPParamter[4].Value = l.Count;
                _ISPParamter[5] = new SqlParameter("@d_Date ", SqlDbType.DateTime);
                _ISPParamter[5].Value = l.d_appraisal;

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


        public Collection<Leave> fn_paym_leave1(int bid)
        {
            Collection<Leave> LeaveList = new Collection<Leave>();
            _Connection = Con.fn_Connection();
            string _SqlString = "select * from paym_leave where status='Y' and pn_BranchID='" + bid + "'";
            SqlCommand _SLeave = new SqlCommand(_SqlString, _Connection);
            _Connection.Open();
            SqlDataReader dr_Leave = _SLeave.ExecuteReader();
            while (dr_Leave.Read())
            {
                Leave l = new Leave();
                l.CompanyID = (int)dr_Leave["pn_CompanyID"];
                l.leaveID = (int)dr_Leave["pn_leaveID"];
                l.leaveName = Convert.IsDBNull(dr_Leave["v_leaveName"]) ? "" : (string)dr_Leave["v_leaveName"];
                l.leaveCode = Convert.IsDBNull(dr_Leave["pn_leaveCode"]) ? "" : (string)dr_Leave["pn_leaveCode"];
                l.LeaveBY = dr_Leave["EL"].ToString();
                l.Count = (int)dr_Leave["pn_Count"];
                
                LeaveList.Add(l);
            }

            _Connection.Close();
            return LeaveList;
        }


        public Collection<Leave> fn_paym_leavelist(Leave e)
        {
            Collection<Leave> LeaveList = new Collection<Leave>();
            _Connection = Con.fn_Connection();
            string _SqlString = "select * from paym_leave where status='Y' and pn_BranchID='" + e.BranchID + "'";
            SqlCommand _SLeave = new SqlCommand(_SqlString, _Connection);
            _Connection.Open();
            SqlDataReader dr_Leave = _SLeave.ExecuteReader();
            while (dr_Leave.Read())
            {
                Leave l = new Leave();
                l.CompanyID = (int)dr_Leave["pn_CompanyID"];
                l.leaveID = (int)dr_Leave["pn_leaveID"];
                l.leaveName = Convert.IsDBNull(dr_Leave["v_leaveName"]) ? "" : (string)dr_Leave["v_leaveName"];
                l.leaveCode = Convert.IsDBNull(dr_Leave["pn_leaveCode"]) ? "" : (string)dr_Leave["pn_leaveCode"];
                l.Count = (int)dr_Leave["pn_Count"];
                l.Point = dr_Leave["EL"].ToString();
                LeaveList.Add(l);
            }

            _Connection.Close();
            return LeaveList;
        }

        public Collection<Leave> fn_paym_leave(Leave e)
        {
            Collection<Leave> LeaveList = new Collection<Leave>();
            _Connection = Con.fn_Connection();
            string _SqlString = "select * from paym_leave where status='Y' and pn_BranchID='" + e.BranchID + "' and v_Leavename='" + e.leaveName + "'";
            SqlCommand _SLeave = new SqlCommand(_SqlString, _Connection);
            _Connection.Open();
            SqlDataReader dr_Leave = _SLeave.ExecuteReader();
            while (dr_Leave.Read())
            {
                Leave l = new Leave();
                l.CompanyID = (int)dr_Leave["pn_CompanyID"];
                l.leaveID = (int)dr_Leave["pn_leaveID"];
                l.leaveName = Convert.IsDBNull(dr_Leave["v_leaveName"]) ? "" : (string)dr_Leave["v_leaveName"];
                l.leaveCode = Convert.IsDBNull(dr_Leave["pn_leaveCode"]) ? "" : (string)dr_Leave["pn_leaveCode"];
                l.Count = (int)dr_Leave["pn_Count"];

                LeaveList.Add(l);
            }

            _Connection.Close();
            return LeaveList;
        }

        public string fn_paym_leaveCode(Leave e)
        {
            Collection<Leave> LeaveList = new Collection<Leave>();
            _Connection = Con.fn_Connection();
            string EL = "";
            string _SqlString = "select * from paym_leave where status='Y' and pn_BranchID='" + e.BranchID + "' and pn_LeaveID='" + e.leaveID + "'";
            SqlCommand _SLeave = new SqlCommand(_SqlString, _Connection);
            _Connection.Open();
            SqlDataReader dr_Leave = _SLeave.ExecuteReader();
            if (dr_Leave.Read())
            {
                EL = dr_Leave["EL"].ToString();
            }

            _Connection.Close();
            return EL;
        }

        public string fn_paym_leave_code(int l_id)
        {
            Collection<Leave> LeaveList = new Collection<Leave>();
            _Connection = Con.fn_Connection();
            string _SqlString = "select pn_leaveCode from paym_leave where pn_leaveID=" + l_id + " and status='Y'";
            SqlCommand _SLeave = new SqlCommand(_SqlString, _Connection);
            _Connection.Open();
            SqlDataReader dr_Leave = _SLeave.ExecuteReader();
            Leave l = new Leave();
            while (dr_Leave.Read())
            {
                l.leaveCode = Convert.IsDBNull(dr_Leave["pn_leaveCode"]) ? "" : (string)dr_Leave["pn_leaveCode"];

                //LeaveList.Add(l);
            }

            _Connection.Close();

            return l.leaveCode;
        }

        public Collection<Leave> fn_paym_leaveAllocation()
        {
            Collection<Leave> AllocationList = new Collection<Leave>();
            _Connection = Con.fn_Connection();
            string _SqlString = "select * from paym_leaveAllocation";
            SqlCommand _SAllocation = new SqlCommand(_SqlString, _Connection);
            _Connection.Open();
            SqlDataReader dr_Allocation = _SAllocation.ExecuteReader();
            while (dr_Allocation.Read())
            {
                Leave l = new Leave();
                l.CompanyID = (int)dr_Allocation["pn_CompanyID"];
                l.BranchID = (int)dr_Allocation["pn_BranchID"];
                l.leaveID = (int)dr_Allocation["pn_leaveID"];
                l.EmployeeID = (int)dr_Allocation["pn_EmployeeID"];
                l.Count = (int)dr_Allocation["pn_Count "];

                AllocationList.Add(l);
            }
            return AllocationList;
        }

        public Collection<Leave> Employee_leaveAllocation(Leave le)
        {
            Collection<Leave> AllocationList = new Collection<Leave>();
            _Connection = Con.fn_Connection();
            string _SqlString = "select * from paym_leaveAllocation where pn_EmployeeID=" + le.EmployeeID + "";
            SqlCommand _SAllocation = new SqlCommand(_SqlString, _Connection);
            _Connection.Open();
            SqlDataReader dr_Allocation = _SAllocation.ExecuteReader();
            while (dr_Allocation.Read())
            {
                Leave l = new Leave();
                //l.CompanyID = (int)dr_Allocation["pn_CompanyID"];
                //l.BranchID = (int)dr_Allocation["pn_BranchID "];
                l.leaveID = (int)dr_Allocation["pn_leaveID"];
                //l.EmployeeID = (int)dr_Allocation["pn_EmployeeID"];
                l.Count = (int)dr_Allocation["n_Count"];

                AllocationList.Add(l);
            }
            return AllocationList;
        }



        public Collection<Leave> Check_leaveAllocation(Leave le)
        {
            Collection<Leave> AllocationList = new Collection<Leave>();
            _Connection = Con.fn_Connection();
            string _SqlString = "select * from paym_leaveAllocation1 where pn_EmployeeID=" + le.EmployeeID + " and pn_leaveID=" + le.leaveID + " and yearend = '" + le.To_year + "' and n_Count>0";
            SqlCommand _SAllocation = new SqlCommand(_SqlString, _Connection);
            _Connection.Open();
            SqlDataReader dr_Allocation = _SAllocation.ExecuteReader();
            while (dr_Allocation.Read())
            {
                Leave l = new Leave();
                //l.CompanyID = (int)dr_Allocation["pn_CompanyID"];
                //l.BranchID = (int)dr_Allocation["pn_BranchID "];
                l.leaveID = (int)dr_Allocation["pn_leaveID"];
                //l.EmployeeID = (int)dr_Allocation["pn_EmployeeID"];
                l.Count1 = Convert.ToDouble(dr_Allocation["cy_count"]);

                AllocationList.Add(l);
            }
            return AllocationList;
        }

        public Collection<Leave> Get_leaveAllocation(Leave le)
        {
            Collection<Leave> AllocationList = new Collection<Leave>();
            _Connection = Con.fn_Connection();
            string _SqlString = "select * from paym_leaveAllocation1 where pn_EmployeeID=" + le.EmployeeID + " and pn_leaveID=" + le.leaveID + " and yearend = '" + le.To_year + "' and n_Count>0";
            SqlCommand _SAllocation = new SqlCommand(_SqlString, _Connection);
            _Connection.Open();
            SqlDataReader dr_Allocation = _SAllocation.ExecuteReader();
            while (dr_Allocation.Read())
            {
                Leave l = new Leave();
                l.Count1 = Convert.ToDouble(dr_Allocation["n_Count"]);
                l.Count2 = Convert.ToDouble(dr_Allocation["cy_count"]);
                AllocationList.Add(l);
            }
            return AllocationList;
        }

        public Collection<Leave> fn_emp_leaveAllocation(Leave le)
        {
            Collection<Leave> AllocationList = new Collection<Leave>();
            _Connection = Con.fn_Connection();
            string _SqlString = "select distinct la.pn_leaveID,l.pn_leaveCode,la.cy_count, l.EL from paym_leave l,paym_leaveAllocation1 la where la.pn_BranchID = " + le.BranchID + " and l.pn_leaveID=la.pn_leaveID and la.pn_Employeeid='" + le.EmployeeID + "' and l.status='Y'";
            //string _SqlString = "select distinct la.pn_leaveID,l.pn_leaveCode,la.n_Count from paym_leave l,paym_leaveAllocation1 la where la.leaveby='" + le.LeaveBY + "' and la.pn_BranchID = " + le.BranchID + " and l.pn_leaveID=la.pn_leaveID and l.status='Y'";
            SqlCommand _SAllocation = new SqlCommand(_SqlString, _Connection);
            _Connection.Open();
            SqlDataReader dr_Allocation = _SAllocation.ExecuteReader();
            while (dr_Allocation.Read())
            {
                Leave l = new Leave();
                l.leaveID = (int)dr_Allocation["pn_leaveID"];
                l.leaveCode = Convert.IsDBNull(dr_Allocation["pn_leaveCode"]) ? "" : (string)dr_Allocation["pn_leaveCode"];
                l.Count1 = Convert.ToDouble(dr_Allocation["cy_count"]);
                l.LeaveBY = dr_Allocation["EL"].ToString();
                AllocationList.Add(l);
            }
            _Connection.Close();
            return AllocationList;
        }

        //duplicate of above
        public Collection<Leave> fn_emp_leaveAllocation1(Leave le)
        {
            Collection <Leave> AllocationList = new Collection<Leave>();
            _Connection = Con.fn_Connection();
            string _SqlString = "select distinct l.pn_leaveID,l.pn_leaveCode,la.leaveby,la.n_count from paym_leave l,paym_leaveAllocation1 la where la.pn_BranchID = " + le.BranchID + " and la.leaveby='" + le.LeaveBY + "' and l.pn_leaveID=la.pn_leaveID and l.status='Y' order by pn_leaveID";
            SqlCommand _SAllocation = new SqlCommand(_SqlString, _Connection);
            _Connection.Open();
            SqlDataReader dr_Allocation = _SAllocation.ExecuteReader();
            while (dr_Allocation.Read())
            {
                Leave l = new Leave();
                l.leaveID = (int)dr_Allocation["pn_leaveID"];
                l.leaveCode = Convert.IsDBNull(dr_Allocation["pn_leaveCode"]) ? "" : (string)dr_Allocation["pn_leaveCode"];
                l.Count1 = Convert.ToDouble(dr_Allocation["n_count"]);
                AllocationList.Add(l);
            }
            _Connection.Close();
            return AllocationList;
        }


        public Collection<Leave> fn_emp_leaveAllocation_department(Leave le)
        {
            Collection<Leave> AllocationList = new Collection<Leave>();
            _Connection = Con.fn_Connection();
            string _SqlString = "select l.pn_leaveCode,la.pn_leaveID,la.n_Count from paym_leave l,paym_leaveAllocation la where la.pn_EmployeeID=" + le.EmployeeID + " and l.pn_leaveID=la.pn_leaveID and l.status='Y'";
            SqlCommand _SAllocation = new SqlCommand(_SqlString, _Connection);
            _Connection.Open();
            SqlDataReader dr_Allocation = _SAllocation.ExecuteReader();
            while (dr_Allocation.Read())
            {
                Leave l = new Leave();
                //l.CompanyID = (int)dr_Allocation["pn_CompanyID"];
                //l.BranchID = (int)dr_Allocation["pn_BranchID "];
                l.leaveID = (int)dr_Allocation["pn_leaveID"];
                //l.EmployeeID = (int)dr_Allocation["pn_EmployeeID"];
                l.leaveCode = Convert.IsDBNull(dr_Allocation["pn_leaveCode"]) ? "" : (string)dr_Allocation["pn_leaveCode"];
                l.Count = (int)dr_Allocation["n_Count"];

                AllocationList.Add(l);
            }
            _Connection.Close();

            return AllocationList;
        }

        public Collection<Leave> fn_emp_leaveAllocation_division(Leave le)
        {
            Collection<Leave> AllocationList = new Collection<Leave>();
            _Connection = Con.fn_Connection();
            string _SqlString = "select l.pn_leaveCode,la.pn_leaveID,la.n_Count from paym_leave l,paym_leaveAllocation la where la.pn_EmployeeID=" + le.EmployeeID + " and l.pn_leaveID=la.pn_leaveID and l.status='Y'";
            SqlCommand _SAllocation = new SqlCommand(_SqlString, _Connection);
            _Connection.Open();
            SqlDataReader dr_Allocation = _SAllocation.ExecuteReader();
            while (dr_Allocation.Read())
            {
                Leave l = new Leave();
                //l.CompanyID = (int)dr_Allocation["pn_CompanyID"];
                //l.BranchID = (int)dr_Allocation["pn_BranchID "];
                l.leaveID = (int)dr_Allocation["pn_leaveID"];
                //l.EmployeeID = (int)dr_Allocation["pn_EmployeeID"];
                l.leaveCode = Convert.IsDBNull(dr_Allocation["pn_leaveCode"]) ? "" : (string)dr_Allocation["pn_leaveCode"];
                l.Count = (int)dr_Allocation["n_Count"];

                AllocationList.Add(l);
            }
            _Connection.Close();
            return AllocationList;
        }

        public Collection<Leave> fn_emp_leaveAllocation_category(Leave le)
        {
            Collection<Leave> AllocationList = new Collection<Leave>();
            _Connection = Con.fn_Connection();
            string _SqlString = "select l.pn_leaveCode,la.pn_leaveID,la.n_Count from paym_leave l,paym_leaveAllocation la where la.pn_EmployeeID=" + le.EmployeeID + " and l.pn_leaveID=la.pn_leaveID and l.status='Y'";
            SqlCommand _SAllocation = new SqlCommand(_SqlString, _Connection);
            _Connection.Open();
            SqlDataReader dr_Allocation = _SAllocation.ExecuteReader();
            while (dr_Allocation.Read())
            {
                Leave l = new Leave();
                //l.CompanyID = (int)dr_Allocation["pn_CompanyID"];
                //l.BranchID = (int)dr_Allocation["pn_BranchID "];
                l.leaveID = (int)dr_Allocation["pn_leaveID"];
                //l.EmployeeID = (int)dr_Allocation["pn_EmployeeID"];
                l.leaveCode = Convert.IsDBNull(dr_Allocation["pn_leaveCode"]) ? "" : (string)dr_Allocation["pn_leaveCode"];
                l.Count = (int)dr_Allocation["n_Count"];

                AllocationList.Add(l);
            }
            _Connection.Close();

            return AllocationList;
        }

        public Collection<Leave> LeaveAllocation_check(Leave le)
        {
            Collection<Leave> AllocationList = new Collection<Leave>();
            _Connection = Con.fn_Connection();

            string _SqlString = "select n_Count,(select sum(Leave_count) as total from paym_Employee_leave";
            _SqlString = _SqlString + " where pn_EmployeeID=" + le.EmployeeID + " and (year(from_date)=" + le.year + " or year(to_date)=" + le.year + ")";
            _SqlString = _SqlString + " and pn_leaveID=" + le.leaveID + ") as Availed_Leave";
            _SqlString = _SqlString + " from paym_leaveAllocation where pn_EmployeeID=" + le.EmployeeID + " and pn_leaveID=" + le.leaveID + " and n_Count>0";

            SqlCommand _SAllocation = new SqlCommand(_SqlString, _Connection);
            _Connection.Open();
            SqlDataReader dr_Allocation = _SAllocation.ExecuteReader();
            while (dr_Allocation.Read())
            {
                Leave l = new Leave();

                l.Allowed_Days = (int)dr_Allocation["n_Count"];
                //l.Availed_Days = (int)dr_Allocation["Availed_Leave"];
                //l.Allowed_Days=(int)dr_Allocation["Current_Leave"];

                if (Convert.ToString(dr_Allocation["Availed_Leave"]) != "")
                {
                    l.Cur_Leave = (double)dr_Allocation["Availed_Leave"];
                }
                else
                {
                    l.Cur_Leave = 0;
                }

                AllocationList.Add(l);
            }

            _Connection.Close();

            return AllocationList;
        }

        public Collection<Leave> fn_paym_Employee_leave()
        {
            Collection<Leave> MonthList = new Collection<Leave>();
            _Connection = Con.fn_Connection();
            string _SqlString = "select * from paym_Employee_leave";
            SqlCommand _SMonth = new SqlCommand(_SqlString, _Connection);
            _Connection.Open();
            SqlDataReader dr_Month = _SMonth.ExecuteReader();
            
            while (dr_Month.Read())
            {
                Leave l = new Leave();
                l.CompanyID = (int)dr_Month["pn_CompanyID"];
                l.BranchID = (int)dr_Month["pn_BranchID"];
                l.leaveID = (int)dr_Month["pn_leaveID"];
                l.EmployeeID = (int)dr_Month["pn_EmployeeID"];
                l.Count = (int)dr_Month["pn_Count"];
                l.month = (int)dr_Month["n_month"];
                l.year = (int)dr_Month["n_year"];
                MonthList.Add(l);
            }
            return MonthList;
        }

        public Collection<Leave> fn_leave_PerMonth(Leave le)
        {
            Collection<Leave> MonthList = new Collection<Leave>();
            _Connection = Con.fn_Connection();
            string _SqlString = "select * from paym_Employee_leave where pn_EmployeeID=" + le.EmployeeID + "  and (year(from_date)=" + le.year + " or year(to_date)=" + le.year + ")  and (month(from_date)=" + le.month + " or month(to_date)=" + le.month + ") and pn_leaveID=" + le.leaveID + "";
            SqlCommand _SMonth = new SqlCommand(_SqlString, _Connection);
            _Connection.Open();
            SqlDataReader dr_Month = _SMonth.ExecuteReader();

            while (dr_Month.Read())
            {
                Leave l = new Leave();
                l.CompanyID = (int)dr_Month["pn_CompanyID"];
                l.BranchID = (int)dr_Month["pn_BranchID"];
                l.leaveID = (int)dr_Month["pn_leaveID"];
                l.EmployeeID = (int)dr_Month["pn_EmployeeID"];
                l.Cur_Leave = (double)dr_Month["Leave_Count"];

                l.str_fromdate = Convert_ToIISDate(Convert.ToDateTime(dr_Month["From_Date"]).ToShortDateString());
                l.str_todate = Convert_ToIISDate(Convert.ToDateTime(dr_Month["To_Date"]).ToShortDateString());

                MonthList.Add(l);
            }

            // _Connection.Close();

            return MonthList;
        }


        // Modified.. Check backup for the original..
        public Collection<Leave> fn_leave_PerYear(Leave le)
        {

            Collection<Leave> LeaveList = new Collection<Leave>();
            _Connection = Con.fn_Connection();
            string _SqlString = "select * from leave_apply  where pn_EmployeeID=" + le.EmployeeID + " and pn_leaveID=" + le.leaveID + " and DATEPART(YEAR, from_date) = '" + le.year + "' ";
            SqlCommand _SLeave = new SqlCommand(_SqlString, _Connection);
            _Connection.Open();
            SqlDataReader dr_Leave = _SLeave.ExecuteReader();

            while (dr_Leave.Read())
            {
                Leave l = new Leave();

                l.CompanyID = (int)dr_Leave["pn_CompanyID"];
                //dep
                l.BranchID = (int)dr_Leave["pn_BranchID"];
                l.leaveID = (int)dr_Leave["pn_leaveID"];
                l.EmployeeID = (int)dr_Leave["pn_EmployeeID"];
                l.Cur_Leave = (Double)dr_Leave["days"];

                l.str_fromdate = (Convert.ToDateTime(dr_Leave["From_Date"]).ToShortDateString());
                l.str_todate = (Convert.ToDateTime(dr_Leave["To_Date"]).ToShortDateString());

                LeaveList.Add(l);
            }

            _Connection.Close();
            return LeaveList;
        }


        public DataSet ds_In_Out(string Query)
        {

            _Connection = Con.fn_Connection();

            _Connection.Open();

            SqlDataAdapter _Ad_sample = new SqlDataAdapter(Query, _Connection);

            DataSet _Ds_sample = new DataSet();

            _Ad_sample.Fill(_Ds_sample);

            _Connection.Close();

            return _Ds_sample;

        }

        public Collection<Leave> fn_leave_Year(Leave le)
        {
            Collection<Leave> MonthList = new Collection<Leave>();
            _Connection = Con.fn_Connection();
            string _SqlString = "select * from paym_Employee_leave where pn_EmployeeID=" + le.EmployeeID + " and n_year=" + le.year + " and n_month=" + le.month + "";
            SqlCommand _SMonth = new SqlCommand(_SqlString, _Connection);
            _Connection.Open();
            SqlDataReader dr_Month = _SMonth.ExecuteReader();

            while (dr_Month.Read())
            {
                Leave l = new Leave();

                l.leaveID = (int)dr_Month["pn_leaveID"];
                l.Count = (int)dr_Month["n_Count"];
                l.month = (int)dr_Month["n_month"];
                l.month2 = (int)dr_Month["n_month"];

                MonthList.Add(l);
            }
            return MonthList;
        }


        public Collection<Leave> fn_Bonus1(int bid)
        {
            Collection<Leave> AppraisalList = new Collection<Leave>();
            _Connection = Con.fn_Connection();
            SqlCommand _Cmd = new SqlCommand("select * from paym_Bonus where status='Y' and pn_BranchID='"+bid+"'", _Connection);
            _Connection.Open();
            SqlDataReader dr_Bonus = _Cmd.ExecuteReader();
            while (dr_Bonus.Read())
            {
                Leave emp = new Leave();
                emp.CompanyID = (int)dr_Bonus["pn_CompanyID"];
                emp.BonusID = (int)dr_Bonus["pn_BonusID"];
                emp.BonusName = Convert.IsDBNull(dr_Bonus["v_BonusName"]) ? "" : (string)dr_Bonus["v_BonusName"];
                emp.totalpoint = (int)dr_Bonus["points"];
                emp.Count = 0;
                AppraisalList.Add(emp);
            }
            return AppraisalList;
        }


        public Collection<Leave> fn_Bonus()
        {
            Collection<Leave> BonusList = new Collection<Leave>();
            _Connection = Con.fn_Connection();
            SqlCommand _Cmd = new SqlCommand("select * from paym_Bonus where status='Y'", _Connection);
            _Connection.Open();
            SqlDataReader dr_Bonus = _Cmd.ExecuteReader();
            while (dr_Bonus.Read())
            {
                Leave emp = new Leave();
                emp.CompanyID = (int)dr_Bonus["pn_CompanyID"];
                emp.AppraisalID = (int)dr_Bonus["pn_BonusID"];
                emp.AppraisalName = Convert.IsDBNull(dr_Bonus["v_BonusName"]) ? "" : (string)dr_Bonus["v_BonusName"];
                emp.totalpoint = (int)dr_Bonus["points"];
                emp.Count = 0;
                BonusList.Add(emp);
            }
            return BonusList;
        }


        public Collection<Leave> fn_Appraisal()
        {
            Collection<Leave> AppraisalList = new Collection<Leave>();
            _Connection = Con.fn_Connection();
            SqlCommand _Cmd = new SqlCommand("select * from paym_Appraisal where status='Y'", _Connection);
            _Connection.Open();
            SqlDataReader dr_Apprais = _Cmd.ExecuteReader();
            while (dr_Apprais.Read())
            {
                Leave emp = new Leave();
                emp.CompanyID = (int)dr_Apprais["pn_CompanyID"];
                emp.AppraisalID = (int)dr_Apprais["pn_AppraisalID"];
                emp.AppraisalName = Convert.IsDBNull(dr_Apprais["v_AppraisalName"]) ? "" : (string)dr_Apprais["v_AppraisalName"];
                emp.totalpoint = (int)dr_Apprais["points"];
                emp.Count = 0;
                AppraisalList.Add(emp);
            }
            return AppraisalList;
        }


        public Collection<Leave> fn_Appraisal1(int id)
        {
            Collection<Leave> AppraisalList = new Collection<Leave>();
            _Connection = Con.fn_Connection();
            SqlCommand _Cmd = new SqlCommand("select * from paym_Appraisal where status='Y' and pn_branchID = '"+ id +"'" , _Connection);
            _Connection.Open();
            SqlDataReader dr_Apprais = _Cmd.ExecuteReader();
            while (dr_Apprais.Read())
            {
                Leave emp = new Leave();
                emp.CompanyID = (int)dr_Apprais["pn_CompanyID"];
                emp.AppraisalID = (int)dr_Apprais["pn_AppraisalID"];
                emp.AppraisalName = Convert.IsDBNull(dr_Apprais["v_AppraisalName"]) ? "" : (string)dr_Apprais["v_AppraisalName"];
                emp.totalpoint = (int)dr_Apprais["points"];
                emp.Count = 0;
                AppraisalList.Add(emp);
            }
            return AppraisalList;
        }


        public Collection<Leave> fn_Appraisaltype(string subquery)
        {
            Collection<Leave> AppraisalList = new Collection<Leave>();
            _Connection = Con.fn_Connection();
            //string qry = "select * from Appraisal_type where status='Y'";
            string qry = "select a.*, b.v_DepartmentName from Appraisal_type a, paym_Department b where a.status='Y' and a.pn_DepartmentID=b.pn_DepartmentID " + subquery;
            SqlCommand _Cmd = new SqlCommand(qry, _Connection);
            _Connection.Open();
            SqlDataReader dr_Apprais = _Cmd.ExecuteReader();
            while (dr_Apprais.Read())
            {
                Leave emp = new Leave();
                emp.CompanyID = (int)dr_Apprais["pn_CompanyID"];
                emp.AppraisalID = (int)dr_Apprais["pn_AppraisalID"];
                emp.AppraisalName = Convert.IsDBNull(dr_Apprais["v_AppraisalQues"]) ? "" : (string)dr_Apprais["v_AppraisalQues"];
                emp.MaxDays = (int)dr_Apprais["Appraisal_type"];
                emp.Departmentid = (int)dr_Apprais["pn_DepartmentID"];
                emp.Departmentname = Convert.IsDBNull(dr_Apprais["v_DepartmentName"]) ? "" : (string)dr_Apprais["v_DepartmentName"];
                emp.Feedtype = Convert.IsDBNull(dr_Apprais["Feed_type"]) ? "" : (string)dr_Apprais["Feed_type"];
                emp.Flag = Convert.IsDBNull(dr_Apprais["Flag"]) ? "" : (string)dr_Apprais["Flag"];
                AppraisalList.Add(emp);
            }
            return AppraisalList;
        }


        public Collection<Leave> fn_paym_emp_Appraisal_count(string qry)
        {
            Collection<Leave> AppraisalList = new Collection<Leave>();
            _Connection = Con.fn_Connection();
            SqlCommand _Cmd = new SqlCommand(qry, _Connection);
            _Connection.Open();
            SqlDataReader dr_Apprais = _Cmd.ExecuteReader();
            while (dr_Apprais.Read())
            {
                Leave emp = new Leave();
                //emp.CompanyID = (int)dr_Apprais["pn_CompanyID"];
                emp.AppraisalID = (int)dr_Apprais["pn_AppraisalID"];
                emp.AppraisalName = Convert.IsDBNull(dr_Apprais["v_AppraisalQues"]) ? "" : (string)dr_Apprais["v_AppraisalQues"];
                emp.Point = Convert.IsDBNull(dr_Apprais["ratings"]) ? "0" : (string)dr_Apprais["ratings"];
                //emp.Departmentid = (int)dr_Apprais["pn_DepartmentID"];
                //emp.Feedtype = Convert.IsDBNull(dr_Apprais["Feed_type"]) ? "" : (string)dr_Apprais["Feed_type"];
                //emp.Flag = Convert.IsDBNull(dr_Apprais["Flag"]) ? "" : (string)dr_Apprais["Flag"];
                AppraisalList.Add(emp);
            }
            return AppraisalList;
        }


        public Collection<Leave> fn_paym_Bonus(Leave l)
        {
            Collection<Leave> LeaveList = new Collection<Leave>();
            _Connection = Con.fn_Connection();
            string _SqlString = "select ea.pn_BonusID,ea.n_points,ea.d_Date,ma.v_BonusName from paym_Emp_Bonus ea,paym_Bonus ma where ea.pn_CompanyID=" + l.CompanyID + " and ea.pn_BranchID=" + l.BranchID + " and ea.pn_EmployeeID=" + l.EmployeeID + " and ma.pn_BonusID=ea.pn_BonusID and ma.status='Y'";
            SqlCommand _SLeave = new SqlCommand(_SqlString, _Connection);
            _Connection.Open();
            SqlDataReader dr_Leave = _SLeave.ExecuteReader();
            while (dr_Leave.Read())
            {
                Leave le = new Leave();
                le.AppraisalID = (int)dr_Leave["pn_BonusID"];
                le.Count = (int)dr_Leave["n_points"];
                le.totalpoint = 10;
                le.d_appraisal = (DateTime)dr_Leave["d_Date"];
                le.AppraisalName = Convert.IsDBNull(dr_Leave["v_BonusName"]) ? "" : (string)dr_Leave["v_BonusName"];

                LeaveList.Add(le);
            }
            return LeaveList;
        }

        public Collection<Leave> fn_paym_Appraisal(Leave l)
        {
            Collection<Leave> LeaveList = new Collection<Leave>();
            _Connection = Con.fn_Connection();
            string _SqlString = "select ea.pn_AppraisalID,ea.n_points,ea.d_Date,ma.points,ma.v_AppraisalName from paym_Emp_Appraisal ea,paym_Appraisal ma where ea.pn_CompanyID=1 and ea.pn_BranchID=7 and ea.pn_EmployeeID=517 and ma.pn_AppraisalID=ea.pn_AppraisalID and ma.status='Y' and ea.pn_branchid=ma.pn_branchid";
            SqlCommand _SLeave = new SqlCommand(_SqlString, _Connection);
            _Connection.Open();
            SqlDataReader dr_Leave = _SLeave.ExecuteReader();
            while (dr_Leave.Read())
            {
                Leave le = new Leave();
                le.AppraisalID = (int)dr_Leave["pn_AppraisalID"];
                le.Count = (int)dr_Leave["n_points"];
                le.totalpoint = (int)dr_Leave["points"];
                le.d_appraisal = (DateTime)dr_Leave["d_Date"];
                le.AppraisalName = Convert.IsDBNull(dr_Leave["v_AppraisalName"]) ? "" : (string)dr_Leave["v_AppraisalName"];

                LeaveList.Add(le);
            } 
            return LeaveList;
        }


        public int fn_Bonus_Amount(int point, string code, string gname)
        {
            string a = "";
            Collection<Leave> AppraisalList = new Collection<Leave>();
            _Connection = Con.fn_Connection();
            SqlCommand _Cmd = new SqlCommand("select increment_type, increment from Bonus_Increment where Grade='" + gname + "' and start_point<=" + point + " and last_point>=" + point, _Connection);
            _Connection.Open();
            SqlDataReader dr_Apprais1 = _Cmd.ExecuteReader();

            Leave emp = new Leave();
            while (dr_Apprais1.Read())
            {
                emp.Count = (int)dr_Apprais1["increment"];
                a = Convert.ToString(dr_Apprais1["Increment_Type"]);
                //AppraisalList.Add(emp);
            }
            //return AppraisalList;
            if (a == "Amount")
            {
                return emp.Count;
            }
            else
            {
                int basic = 0;
                _Connection = Con.fn_Connection();
                _Connection.Open();
                SqlCommand cmd = new SqlCommand("select basic_salary from paym_employee where EmployeeCode='" + code + "'", _Connection);
                SqlDataReader rea1;
                rea1 = cmd.ExecuteReader();
                if (rea1.Read())
                {
                    basic = Convert.ToInt32(rea1["basic_salary"]);
                }
                emp.Count = Convert.ToInt32(basic * (emp.Count * 0.01));
                return emp.Count;
            }
        }


        public int fn_App_Amount(double point, string code , string gname)
        {
            string a = "";
            Collection<Leave> AppraisalList = new Collection<Leave>();
            _Connection = Con.fn_Connection();
            SqlCommand _Cmd = new SqlCommand("select increment_type, increment,percentage from App_Increment where Grade='" + gname + "' and start_point<=" + point + " and last_point>=" + point, _Connection);
            _Connection.Open();
            SqlDataReader dr_Apprais = _Cmd.ExecuteReader();

            Leave emp = new Leave();
            if (dr_Apprais.Read())
            {
                
                a = Convert.ToString(dr_Apprais["Increment_Type"]);
                //AppraisalList.Add(emp);
            }
            //return AppraisalList;
            if (a == "Amount")
            {
                emp.Count = Convert.ToInt32( dr_Apprais["increment"]);
                return emp.Count;
            }
            else if (a == "Percentage")
            {
                emp.Count =Convert.ToInt32( dr_Apprais["Percentage"]);
                int basic = 0;
                _Connection = Con.fn_Connection();
                _Connection.Open();
                SqlCommand cmd = new SqlCommand("select basic_salary from paym_employee where EmployeeCode='" + code + "'", _Connection);
                SqlDataReader rea;
                rea = cmd.ExecuteReader();
                if (rea.Read())
                {
                    basic = Convert.ToInt32(rea["basic_salary"]);
                }
                emp.Count = Convert.ToInt32(basic * (emp.Count * 0.01));
                return emp.Count;
            }
            else if (a == "Whichever is higher")
            {
                int amt = Convert.ToInt32(dr_Apprais["increment"]);
                int per = Convert.ToInt32(dr_Apprais["Percentage"]);
                int basic = 0;
                _Connection = Con.fn_Connection();
                _Connection.Open();
                SqlCommand cmd = new SqlCommand("select basic_salary from paym_employee where EmployeeCode='" + code + "'", _Connection);
                SqlDataReader rea;
                rea = cmd.ExecuteReader();
                if (rea.Read())
                {
                    basic = Convert.ToInt32(rea["basic_salary"]);
                }
                per = Convert.ToInt32(basic * (per * 0.01));
                if (amt > per)
                {
                    return amt;
                }
                else
                {
                    return per;
                }
            }
            else if (a == "Whichever is lower")
            {
                int amt = Convert.ToInt32(dr_Apprais["increment"]);
                int per = Convert.ToInt32(dr_Apprais["Percentage"]);
                int basic = 0;
                _Connection = Con.fn_Connection();
                _Connection.Open();
                SqlCommand cmd = new SqlCommand("select basic_salary from paym_employee where EmployeeCode='" + code + "'", _Connection);
                SqlDataReader rea;
                rea = cmd.ExecuteReader();
                if (rea.Read())
                {
                    basic = Convert.ToInt32(rea["basic_salary"]);
                }
                per = Convert.ToInt32(basic * (per * 0.01));
                if (amt > per)
                {
                    return per;
                }
                else
                {
                    return amt;
                }
            }
            else if (a == "Average")
            {
                int amt = Convert.ToInt32(dr_Apprais["increment"]);
                int per = Convert.ToInt32(dr_Apprais["Percentage"]);
                int basic = 0;
                _Connection = Con.fn_Connection();
                _Connection.Open();
                SqlCommand cmd = new SqlCommand("select basic_salary from paym_employee where EmployeeCode='" + code + "'", _Connection);
                SqlDataReader rea;
                rea = cmd.ExecuteReader();
                if (rea.Read())
                {
                    basic = Convert.ToInt32(rea["basic_salary"]);
                }
                per = Convert.ToInt32(basic * (per * 0.01));
                int avg = (per + amt) / 2;
                return avg;
            }
            else
            {
                return emp.Count;
            }
        }

        public Collection<Leave> Get_year(Leave l)
        {
            string[] datesplt;
            Collection<Leave> LeaveList = new Collection<Leave>();
            Leave le = new Leave();
            _Connection = Con.fn_Connection();
            _Connection.Open();
            SqlCommand cmd = new SqlCommand("Select * from paym_branch where pn_companyid = '" + l.CompanyID + "' and pn_branchId = '" + l.BranchID + "'", _Connection);
            SqlDataReader rea = cmd.ExecuteReader();
            if (rea.Read())
            {
                le.str_fromdate = Convert.ToDateTime(rea["start_date"]).ToString("dd/MM/yyyy");
                le.str_todate = Convert.ToDateTime(rea["end_date"]).ToString("dd/MM/yyyy");
                datesplt = le.str_fromdate.Split('/');
                le.year = Convert.ToInt32(datesplt[2]);
                LeaveList.Add(le);
            }
            _Connection.Close();
            return LeaveList;
        }

        public string fn_getGender(Leave l)
        {
            string gender = "";
            _Connection = Con.fn_Connection();
            SqlCommand _Cmdf = new SqlCommand("select gender from paym_employee where pn_employeeid='" + l.EmployeeID + "' and pn_branchID = '" + l.BranchID + "'", _Connection);
            _Connection.Open();
            SqlDataReader dr_Appraisf = _Cmdf.ExecuteReader();

            if (dr_Appraisf.Read())
            {
                gender = Convert.ToString(dr_Appraisf["gender"]);
            }
            return gender;

        }

        public int fn_CheckHoliday(string fromdate, string todate)
        {
            int cc = 0;
            _Connection = Con.fn_Connection();
            SqlCommand _Cmdf = new SqlCommand("select branchcode from paym_Branch where '" + fromdate + "' between start_date and end_date and  '" + todate + "' between start_date and end_date;", _Connection);
            _Connection.Open();
            SqlDataReader dr_Appraisf = _Cmdf.ExecuteReader();

            if (dr_Appraisf.Read())
            {
                cc = 1;
            }
            return cc;
        }

        public string fn_App_formula(double point, string code, string gname)
        {
            string form = "";
            Collection<Leave> AppraisalList = new Collection<Leave>();
            _Connection = Con.fn_Connection();
            SqlCommand _Cmdf = new SqlCommand("select formula_name from App_Increment where Grade='" + gname + "' and start_point<=" + point + " and last_point>=" + point, _Connection);
            _Connection.Open();
            SqlDataReader dr_Appraisf = _Cmdf.ExecuteReader();

            Leave emp = new Leave();
            while (dr_Appraisf.Read())
            {
                form = Convert.ToString(dr_Appraisf["formula_name"]);
                //AppraisalList.Add(emp);
            }
            //return AppraisalList;
            return form;
        }

        public string fn_Bonus_formula(int point, string code, string gname)
        {
            string form = "";
            Collection<Leave> AppraisalList = new Collection<Leave>();
            _Connection = Con.fn_Connection();
            SqlCommand _Cmdf = new SqlCommand("select formula_name from Bonus_increment where Grade='" + gname + "' and start_point<=" + point + " and last_point>=" + point, _Connection);
            _Connection.Open();
            SqlDataReader dr_Appraisf = _Cmdf.ExecuteReader();
            Leave emp = new Leave();
            while (dr_Appraisf.Read())
            {
                form = Convert.ToString(dr_Appraisf["formula_name"]);
                //AppraisalList.Add(emp);
            }
            //return AppraisalList;
            return form;
        }

        public int fn_App_Amount1(int point)
        {
            Collection<Leave> AppraisalList = new Collection<Leave>();
            _Connection = Con.fn_Connection();
            SqlCommand _Cmd = new SqlCommand("select increment from App_Increment where start_point<=" + point + " and last_point>=" + point, _Connection);
            _Connection.Open();
            SqlDataReader dr_Apprais = _Cmd.ExecuteReader();

            Leave emp = new Leave();
            while (dr_Apprais.Read())
            {
                emp.Count = (int)dr_Apprais["increment"];
                //AppraisalList.Add(emp);
            }
            //return AppraisalList;
            return emp.Count;

        }


        public string Increment(Leave l)
        {
            try
            {
                _Connection = Con.fn_Connection();


                SqlCommand _Cmd = new SqlCommand("sp_paym_Increment", _Connection);
                _Cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter[] _ISPParamter = new SqlParameter[5];


                _ISPParamter[0] = new SqlParameter("@pn_CompanyID", SqlDbType.Int);
                _ISPParamter[0].Value = l.CompanyID;
                _ISPParamter[1] = new SqlParameter("@pn_IncrementID", SqlDbType.Int);
                _ISPParamter[1].Value = l.IncrementID;
                _ISPParamter[2] = new SqlParameter("@start_point", SqlDbType.Int);
                _ISPParamter[2].Value = l.startpoint;
                _ISPParamter[3] = new SqlParameter("@last_point ", SqlDbType.Int);
                _ISPParamter[3].Value = l.lastpoint;
                _ISPParamter[4] = new SqlParameter("@increment", SqlDbType.Int);
                _ISPParamter[4].Value = l.increment;


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


        public Collection<Leave> fn_Increment()
        {
            Collection<Leave> IncrementList = new Collection<Leave>();
            _Connection = Con.fn_Connection();
            string _SqlString = "select * from App_Increment";
            SqlCommand _SSDepartment = new SqlCommand(_SqlString, _Connection);
            _Connection.Open();
            SqlDataReader dr_Department = _SSDepartment.ExecuteReader();
            while (dr_Department.Read())
            {
                Leave l = new Leave();
                l.IncrementID = (int)dr_Department["pn_IncrementID"];
                l.startpoint = (int)dr_Department["start_point"];
                l.lastpoint = (int)dr_Department["last_point"];
                l.increment = (int)dr_Department["increment"];
                IncrementList.Add(l);
            }
            return IncrementList;
        }


        public string fn_Update_increment(Leave l)
        {

            try
            {
                _Connection = Con.fn_Connection();

                _Connection.Open();

                SqlCommand _RSC_can = new SqlCommand("update App_Increment set start_point='" + l.startpoint + "' , last_point='" + l.lastpoint + "' , increment='" + l.increment + "' where pn_IncrementID=" + l.IncrementID + "", _Connection);

                _RSC_can.ExecuteNonQuery();

                _Connection.Close();

                return "0";
            }
            catch (Exception ex)
            {
                return "1";
            }

        }


        public string Convert_ToIISDate(string cur_date)
        {

            string _d, _m, _y, sql_date = "";

            char[] splitter = { '/' };
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

        //public string Convert_ToIISDate1(string cur_date)
        //{

        //    string _d, _m, _y, sql_date = "";

        //    char[] splitter ={ '/' };
        //    string[] str_ary = new string[4];

        //    if (cur_date.Length == 10)
        //    {
        //        _d = cur_date.Substring(0, 2);
        //        _m = cur_date.Substring(3, 2);
        //        _y = cur_date.Substring(6, 4);

        //        sql_date = _d + "/" + _m + "/" + _y;
        //    }
        //    else
        //    {
        //        str_ary = cur_date.Split(splitter);


        //        _d = check_single(str_ary[0]);
        //        _m = check_single(str_ary[1]);
        //        _y = str_ary[2];

        //        sql_date = _d + "/" + _m + "/" + _y;

        //    }

        //    return sql_date;

        //}

        public string check_single(string str_single)
        {


            if (str_single.Length == 1)
            {
                str_single = "0" + str_single;

            }

            return str_single;



        }


        public string Appraisalmaster(Leave e)
        {
            try
            {
                _Connection = Con.fn_Connection();

                SqlCommand _Cmd = new SqlCommand("sp_AppraisalMaster", _Connection);
                _Cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter[] _ISPParamter = new SqlParameter[5];
                _ISPParamter[0] = new SqlParameter("@pn_CompanyID", SqlDbType.Int);
                _ISPParamter[0].Value = e.CompanyID;
                _ISPParamter[1] = new SqlParameter("@pn_Appraisalmasterid", SqlDbType.Int);
                _ISPParamter[1].Value = e.AppraisalmasterID;
                _ISPParamter[2] = new SqlParameter("@v_Appraisalmastername", SqlDbType.VarChar);
                _ISPParamter[2].Value = e.AppraisalmasterName;
                _ISPParamter[3] = new SqlParameter("@v_Appraisalmastercode", SqlDbType.VarChar);
                _ISPParamter[3].Value = e.AppraisalmasterCode;
                _ISPParamter[4] = new SqlParameter("@status", SqlDbType.Char);
                _ISPParamter[4].Value = e.status;
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



        public Collection<Leave> fn_Appraisalmaster()
        {
            Collection<Leave> AppraisalList = new Collection<Leave>();
            _Connection = Con.fn_Connection();
            SqlCommand _Cmd = new SqlCommand("select * from paym_AppraisalMaster where status='y'", _Connection);
            _Connection.Open();
            SqlDataReader dr_Apprais = _Cmd.ExecuteReader();
            while (dr_Apprais.Read())
            {
                Leave emp = new Leave();
                emp.CompanyID = (int)dr_Apprais["pn_CompanyID"];
                emp.AppraisalmasterID = (int)dr_Apprais["pn_Appraisalmasterid"];
                emp.AppraisalmasterName = Convert.IsDBNull(dr_Apprais["v_Appraisalmastername"]) ? "" : (string)dr_Apprais["v_Appraisalmastername"];
                emp.AppraisalmasterCode = Convert.IsDBNull(dr_Apprais["v_Appraisalmastercode"]) ? "" : (string)dr_Apprais["v_Appraisalmastercode"];

                AppraisalList.Add(emp);
            }
            return AppraisalList;
        }

        public Collection<Leave> fn_Appbonus(string qry)
        {
            Collection<Leave> AppraisalList = new Collection<Leave>();
            _Connection = Con.fn_Connection();
            SqlCommand _Cmd = new SqlCommand(qry, _Connection);
            _Connection.Open();
            SqlDataReader dr_Apprais = _Cmd.ExecuteReader();
            while (dr_Apprais.Read())
            {
                Leave lv = new Leave();
                lv.CompanyID = (int)dr_Apprais["pn_CompanyID"];
                lv.Bonus_id = (int)dr_Apprais["Band_ID"];
                lv.Fond = Convert.IsDBNull(dr_Apprais["Band_Name"]) ? "" : (string)dr_Apprais["Band_Name"];
                lv.From_Value = Convert.ToDouble(dr_Apprais["From_value"]); //Convert.IsDBNull(dr_Apprais["From_value"]) ? 0.0 : (float)dr_Apprais["From_value"];
                lv.To_Value = Convert.ToDouble(dr_Apprais["To_value"]); //Convert.IsDBNull(dr_Apprais["To_value"]) ? 0.0 : (float)dr_Apprais["To_value"];
                lv.Bonus_points = (int)dr_Apprais["Bonus_Points"];
                AppraisalList.Add(lv);
            }
            return AppraisalList;
        }

        public Collection<Leave> Check_leaveAllocation1(Leave le)
        {
            Collection<Leave> AllocationList = new Collection<Leave>();
            _Connection = Con.fn_Connection();

            //string _SqlString = "select "+le.Leave_First+" from LeaveAllocation_master where pn_EmployeeID=" + le.EmployeeID + " and yearend = '" + le.To_year + "'";
            SqlCommand _SAllocation = new SqlCommand("select " + le.LeaveBY + " from LeaveAllocation_Master where pn_companyID='" + le.CompanyID + "' and pn_BranchID='" + le.BranchID + "' and pn_EmployeeID='" + le.EmployeeID + "' and yearend='" + le.To_year + "'", _Connection);
            _Connection.Open();
            SqlDataReader dr_Allocation = _SAllocation.ExecuteReader();
            if (dr_Allocation.Read())
            {
                Leave l = new Leave();
                l.Count = (int)dr_Allocation[0];

                AllocationList.Add(l);
            }
            return AllocationList;
        }






        public Collection<Leave> fn_leave_PerYear1(Leave le)
        {

            Collection<Leave> LeaveList = new Collection<Leave>();
            _Connection = Con.fn_Connection();
            string _SqlString = "select * from leave_apply where pn_EmployeeID=" + le.EmployeeID + " and pn_leavename='" + le.LeaveBY + "' and yearend = '" + le.To_year + "' and record = 'T'";
            SqlCommand _SLeave = new SqlCommand(_SqlString, _Connection);
            _Connection.Open();
            SqlDataReader dr_Leave = _SLeave.ExecuteReader();

            while (dr_Leave.Read())
            {
                Leave l = new Leave();

                l.CompanyID = (int)dr_Leave["pn_CompanyID"];
                //dep
                l.BranchID = (int)dr_Leave["pn_BranchID"];
                l.leaveID = (int)dr_Leave["pn_leaveID"];
                l.EmployeeID = (int)dr_Leave["pn_EmployeeID"];
                l.Cur_Leave = (Double)dr_Leave["days"];

                l.str_fromdate = (Convert.ToDateTime(dr_Leave["From_Date"]).ToShortDateString());
                l.str_todate = (Convert.ToDateTime(dr_Leave["To_Date"]).ToShortDateString());

                LeaveList.Add(l);
            }

            _Connection.Close();
            return LeaveList;
        }
    }
}