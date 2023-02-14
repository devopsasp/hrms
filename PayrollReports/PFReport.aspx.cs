using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Net;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using ePayHrms.Login;
using ePayHrms.Connection;
using ePayHrms.Candidate;
using System.IO;
using ePayHrms.Company;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using ePayHrms.BE.Recruitment;
using ePayHrms.Employee;
using ePayHrms.Leave;
using CrystalDecisions.Web;
using CrystalDecisions.Enterprise;
using CrystalDecisions.ReportAppServer;
using CrystalDecisions.Reporting;
using CrystalDecisions.ReportSource;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;


public partial class PayrollReports_Default : System.Web.UI.Page
{
    Company company = new Company();
    Employee employee = new Employee();
    Be_Recruitment r = new Be_Recruitment();
    PayRoll pay = new PayRoll();
    Candidate c = new Candidate();
    Leave l = new Leave();

    Collection<Company> CompanyList, ddlBranchsList;

    Collection<Employee> EmployeeList;
    Collection<PayRoll> PayList;
    string s_login_role;

    int i = 0, j, temp_count = 0;
    //int i_sin, i_all, i_emp, earn_count = 0, ded_count = 0, cur_count = 0;

    //string temp_string = "", temp_earn = "", emp_count = "", sel_value = "";
    string query = "";

    string fromdate, todate, month, year, s_form;
    DataSet ds_userrights;

    //bool emp_check = false;
    protected void Page_Load(object sender, EventArgs e)
    {
        
        Session["Repordid"] = "";
       
        employee.CompanyId = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
        pay.CompanyId = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
        c.CompanyID = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);

        employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
         pay.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
        c.BranchID = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);

        s_login_role = Request.Cookies["Login_temp_Role"].Value;
        lbl_error.Text = "";
        //Label2.Visible = false;
        if (!IsPostBack)
        {
            date_load();
            
            lbl_fromdate.Visible = false;
            lbl_todate.Visible = false;
            ddl_tomonthlist.Visible = false;
            ddl_toyearlist.Visible = false;

            row_EpfAcno.Visible = false;
            row_PfAcno.Visible = false;
            row_DliAcno.Visible = false;
            row_AdminAcno2.Visible = false;
            row_AdminAcno22.Visible = false;
            row_Bankname.Visible = false;
            row_Branchname.Visible = false;
            row_remitdate.Visible = false;
            row_lastmonth.Visible = false;
            row_videform5.Visible = false;
            row_videform10.Visible = false;

            txt_EpfAcno.Value = "";
            txt_PfAcno.Value = "";
            txt_DliAcno.Value = "";
            txt_AdminAcno2.Value = "";
            txt_AdminAcno22.Value = "";
            txt_Branchname.Value = "";
            txt_remitdate.Value = "";
            txt_lastmonth.Value = "0";
            txt_videform5.Value = "0";
            txt_videform10.Value = "0";
            
            CompanyList = company.fn_getCompany();

            if (CompanyList.Count > 0)
            {
                switch (s_login_role)
                {
                    case "a":
                        ddl_Branch.Visible = true;
                        ddl_Branch_load();
                        ddl_Department_load();

                        break;

                    case "h":
                        ddl_Branch.Visible = false;
                        ddl_Department_load();

                        break;
                    case "u": 
                        s_form = "55";

                        ds_userrights = company.check_Userrights((int)Session["Login_temp_EmployeeID"], s_form);

                        if (ds_userrights.Tables[0].Rows.Count > 0)
                        {
                            ddl_Branch.Visible = false;
                     //       chkEmployee_load();
                           // session_check();
                        }
                        else
                        {
                            Response.Cookies["Msg_Session"].Value=  "Permission Restricted. Please Contact Administrator.";
                            Response.Redirect("~/Company_Home.aspx");
                        }
                        break;


                    case "e":
                        ddl_Branch.Visible = false;
                        ListItem li = new ListItem();
                        li.Text = Request.Cookies["EmpCodeName"].Value;
                        li.Value = Request.Cookies["Login_temp_EmployeeID"].Value;
                        li.Selected = true;
                        chk_Empcode.Items.Add(li);
                        chk_Empcode.Enabled = false;
                        chkall.Visible = false;
                        break;

                    default:
                        Response.Cookies["Msg_Session"].Value=  "Permission Restricted. Please Contact Administrator";
                        Response.Redirect("~/Company_Home.aspx");
                        break;
                }

            }
            else
            {

                Response.Cookies["Msg_Session"].Value = "Create Company";
                Response.Redirect("~/Company_Home.aspx");
            }
        }
    }

    public void admin()
    {

        EmployeeList = employee.fn_getEmployeeList(employee);

        if (EmployeeList.Count > 0)
        {
            chk_Empcode.DataSource = EmployeeList;
            chk_Empcode.DataTextField = "LastName";
            chk_Empcode.DataValueField = "EmployeeId";
            chk_Empcode.DataBind();
        }
        else
        {
            lbl_error.Text = "No employees";
            chk_Empcode.Items.Clear();
        }


        //employee.temp_str = "select * from Temp_Employee";

        //EmpFirstList = employee.Temp_Selected_EmployeeList(employee);

        //if (EmpFirstList.Count == 0)
        //{
        //    temp_tables();

        //}  
    }
    public void hr()
    {

        try
        {
            EmployeeList = employee.fn_getEmployeeList(employee);
            if (EmployeeList.Count > 0)
            {

                chk_Empcode.DataSource = EmployeeList;
                chk_Empcode.DataValueField = "EmployeeId";
                chk_Empcode.DataTextField = "LastName";
                chk_Empcode.DataBind();

            }
            else
            {
                lbl_error.Text = "No employees";
                chk_Empcode.Items.Clear();
            }
        }
        catch (Exception ex)
        {
            lbl_error.Text = "Error";
        }
    }

    public void ddl_Department_load()
    {
        EmployeeList = employee.fn_getDepartmentList1(employee.BranchId);
        if (EmployeeList.Count > 0)
        {

            for (int ddl_i = -2; ddl_i < EmployeeList.Count; ddl_i++)
            {

                if (ddl_i == -2)
                {
                    ListItem es_list = new ListItem();

                    es_list.Text = "Select Department";
                    es_list.Value = "0";
                    ddl_department.Items.Add(es_list);
                }
                else if (ddl_i == -1)
                {
                    ListItem es_list = new ListItem();

                    es_list.Text = "All";
                    es_list.Value = "1";
                    ddl_department.Items.Add(es_list);
                }
                else
                {
                    ListItem es_list = new ListItem();
                    es_list.Value = EmployeeList[ddl_i].DepartmentId.ToString();
                    es_list.Text = EmployeeList[ddl_i].DepartmentName.ToString();
                    ddl_department.Items.Add(es_list);
                }
            }
        }

         if (ddl_department.SelectedIndex !=0)
      
        {
            if (s_login_role == "a")
            {
                admin();
            }
            if (s_login_role == "h")
            {
                hr();
            }

        }

    }




    //public void session_check()
    //{

    //    switch (Request.Cookies["Query_Session"].Value)
    //    {
    //        case "start":
    //            lbl_error.Text = "Welcome To Report Section!";
    //            Response.Cookies["Query_Session"].Value= "start";
    //            break;

    //        case "nil":
    //            lbl_error.Text = "No Result Found";
    //            Response.Cookies["Query_Session"].Value= "start";
    //            break;

    //        case "back":
    //            lbl_error.Text = "";
    //            Response.Cookies["Query_Session"].Value= "start";
    //            break;

    //        default:
    //            final_query_execute();
    //            break;

    //    }


    //}

    public void final_query_execute()
    {

        employee.temp_str = Request.Cookies["Query_Session"].Value;

        EmployeeList = employee.Temp_Selected_EmployeeList(employee);

        if (EmployeeList.Count > 0)
        {
            //19-05-2009
            //for (i = 0; i < chk_Empcode.Items.Count; i++)
            //{
            //    for (j = 0; j < EmployeeList.Count; j++)
            //    {
            //        if (Convert.ToInt32(chk_Empcode.Items[i].Value) == EmployeeList[j].EmployeeId)
            //        {
            //            chk_Empcode.Items[i].Selected = true;
            //        }

            //    }
            //}

            lbl_error.Text = EmployeeList.Count + " Employees Selected!";
            Response.Cookies["Query_Session"].Value= "start";

        }
        else
        {
            lbl_error.Text = "No Employees has been selected";
            Response.Cookies["Query_Session"].Value= "start";
        }
    }

    protected void btn_Report_Click(object sender, ImageClickEventArgs e)
    {
        
                
    }

    public void pfprocess()
    {
        decimal i_AC22, i_AC2, i_DL1, i_PF10, i_EPF10;
        string start_dt, end_dt;

        i_AC22 = Convert.ToDecimal(txt_AdminAcno22.Value) / 100;
        i_AC2 = Convert.ToDecimal(txt_AdminAcno2.Value) / 100;
        i_DL1 = Convert.ToDecimal(txt_DliAcno.Value) / 100;
        i_EPF10 = Convert.ToDecimal(txt_EpfAcno.Value) / 100;
        i_PF10 = Convert.ToDecimal(txt_PfAcno.Value) / 100;

        start_dt = "01/04/" + ddl_yearlist.SelectedValue;
        end_dt = "31/03/" + Convert.ToString((Convert.ToInt32(ddl_yearlist.SelectedValue) + 1));

        query = "delete from form12";
        employee.fn_reportbyid(query);

        //Values From Table Directly
        query = " set dateformat dmy;insert into form12 (NetPay, acno22, acno2, DLIAC, EPFAC01, PFA10, PF, EPF, FPF, SPF, SEPF, SFPF, pn_CompanyID, count) ";
        query += " select sum(d.Earned_basic) as NetPay, sum(d.Earned_basic)*" + i_AC22 + " as acno22, sum(d.Earned_basic)*" + i_AC2 + " as acno2, sum(d.Earned_basic)* " + i_DL1 + "as DLIAC, ";
        query += " sum(d.Earned_basic)*" + i_EPF10 + " as EPFAC01, sum(d.Earned_basic)*" + i_PF10 + "as PFA10, b.Emp_Con_PF as PF, b.Emp_Con_EPF as EPF, b.Emp_Con_FPF as FPF,";
        query += " (sum(a.Tot_pf)+sum(a.VPF))as SPF, sum(a.EPF) as SEPF, sum(a.FPF) as SFPF, " + employee.CompanyId + ", (select count(*) from paym_employee where pn_BranchID='"+employee.BranchId+"' and status='Y') as count ";
        //query += " from payoutput_pf a, paym_PF b where a.pn_CompanyID=b.pn_CompanyID and a.pn_CompanyID=" + employee.CompanyId + " and a.d_From_Date='" + fromdate + "' and a.d_To_Date='" + todate + "'";
        query += " from payoutput_pf a, paym_PF b, paym_Employee c, payoutput_netpay d where a.pn_CompanyID=b.pn_CompanyID and a.pn_CompanyID=" + employee.CompanyId + " and c.pn_CompanyID=b.pn_CompanyID and c.pn_BranchID="+employee.BranchId+" and a.pn_EmployeeID=c.pn_EmployeeID and a.pn_branchID = d.pn_BranchID and a.pn_EmployeeID=d.pn_EmployeeID and a.d_date=d.d_date and a.d_From_Date='" + fromdate + "' and a.d_To_Date='" + todate + "'";
        query += " group by b.Emp_Con_PF, b.Emp_Con_EPF, b.Emp_Con_FPF;set dateformat mdy";
        employee.fn_reportbyid(query);

        //Values From TextBox
        query = "set dateformat dmy;update form12 set AC22=" + Convert.ToDecimal(txt_AdminAcno22.Value) + ", AC2=" + Convert.ToDecimal(txt_AdminAcno2.Value) + ", ";
        query += "DL1=" + Convert.ToDecimal(txt_DliAcno.Value) + ", EPF10=" + Convert.ToDecimal(txt_EpfAcno.Value) + ", PF10=" + Convert.ToDecimal(txt_PfAcno.Value) + ", ";
        query += "From_dt='" + Convert.ToDateTime(fromdate) + "', To_dt='" + Convert.ToDateTime(todate) + "', Bank='" + ddl_Bankname.SelectedItem.Text + "', ";
        query += "Branch='" + txt_Branchname.Value + "', Remit_dt='" + Convert.ToDateTime(txt_remitdate.Value) + "', Noper=" + Convert.ToInt32(txt_lastmonth.Value) + ",";
        query += "Add_no=" + Convert.ToInt32(txt_videform5.Value) + ",Rem_No=" + Convert.ToInt32(txt_videform10.Value) + ", Start_dt='" + Convert.ToDateTime(start_dt) + "', End_dt='" + Convert.ToDateTime(end_dt) + "';set dateformat mdy";

        employee.fn_reportbyid(query);
    }

    public void date_load()
    {
        DateTime tnow = DateTime.Now;
        ArrayList AlYear = new ArrayList();
        int i = 0, yr_it, cur_yr;
        cur_yr = DateTime.Now.Year;
        //cur_yr = cur_yr + 5;

        for (yr_it = cur_yr - 5; yr_it <= cur_yr; yr_it++)
        {
            ddl_yearlist.Items.Add(Convert.ToString(yr_it));
            ddl_toyearlist.Items.Add(Convert.ToString(yr_it));
            i++;
        }
        i = i - 6;
        //current year is selected index
        ddl_yearlist.SelectedValue = tnow.Year.ToString();
        ddl_monthlist.SelectedValue = tnow.Month.ToString();
        ddl_toyearlist.SelectedValue = tnow.Year.ToString();
        ddl_tomonthlist.SelectedValue = tnow.Month.ToString();


        //DateTime tnow = DateTime.Now;
        //ArrayList AlYear = new ArrayList();
        //int i;
        //for (i = 2002; i <= 2016; i++)
        //    AlYear.Add(i);

        //if (!this.IsPostBack)
        //{
        //    ddl_yearlist.DataSource = AlYear;
        //    ddl_yearlist.DataBind();
        //    ddl_yearlist.SelectedValue = tnow.Year.ToString();
        //    ddl_monthlist.SelectedValue = tnow.Month.ToString();

        //    ddl_toyearlist.DataSource = AlYear;
        //    ddl_toyearlist.DataBind();
        //    ddl_tomonthlist.SelectedValue = tnow.Month.ToString();
        //    ddl_toyearlist.SelectedValue = tnow.Year.ToString();
        //}
    }

    protected void ddl_type_SelectedIndexChanged(object sender, EventArgs e)
    {
        //Visible the TextBox and Labels at each Form
        if (ddl_type.SelectedValue == "2" || ddl_type.SelectedValue == "3")
        {
            lbl_fromdate.Visible = true;
            lbl_todate.Visible = true;
            ddl_tomonthlist.Visible = true;
            ddl_toyearlist.Visible = true;
            lbl_month.Visible = false;
            lbl_year.Visible = false;
        }
        else
        {
            lbl_fromdate.Visible = false;
            lbl_todate.Visible = false;
            ddl_tomonthlist.Visible = false;
            ddl_toyearlist.Visible = false;
            lbl_month.Visible = true;
            lbl_year.Visible = true;
        }
        if (ddl_type.SelectedValue == "4" || ddl_type.SelectedValue == "5")
        {
            //div_chkempcode.Visible = false;
            chkall.Visible = false;
            //lbl_selectemp.Visible = false;
            //div_chk_Empcode.Visible = false;
            row_EpfAcno.Visible = true;
            row_PfAcno.Visible = true;
            row_DliAcno.Visible = true;
            row_AdminAcno2.Visible = true;
            row_AdminAcno22.Visible = true;
            row_Bankname.Visible = true;
            row_Branchname.Visible = true;
            row_remitdate.Visible = true;
            row_lastmonth.Visible = true;
            row_videform5.Visible = true;
            row_videform10.Visible = true;

            DateTime dat = Convert.ToDateTime(ddl_yearlist.SelectedValue + "/" + ddl_monthlist.SelectedValue + "/" + "01");
            pfform12(dat);
            PayList = pay.fn_bank();
            if (PayList.Count > 0)
            {
                ddl_Bankname.DataSource = PayList;
                ddl_Bankname.DataTextField = "bankname";
                ddl_Bankname.DataValueField = "bankcode";
                ddl_Bankname.DataBind();
            }
        }
        else
        {
            row_EpfAcno.Visible = false;
            row_PfAcno.Visible = false;
            row_DliAcno.Visible = false;
            row_AdminAcno2.Visible = false;
            row_AdminAcno22.Visible = false;
            row_Bankname.Visible = false;
            row_Branchname.Visible = false;
            row_remitdate.Visible = false;
            row_lastmonth.Visible = false;
            row_videform5.Visible = false;
            row_videform10.Visible = false;
            //div_chk_Empcode.Visible = true;
            //div_chkempcode.Visible = true;
            chkall.Visible = true;
            //lbl_selectemp.Visible = true;
        }
    }

    public void pfform12(DateTime dt)
    {
        //query = "select Emp_Con_PF,Emp_Con_EPF,Emp_Con_FPF,Admin_Charges from paym_pf where pn_CompanyID=1 and d_Date<='"+dt+"'";
        pay.d_ToDate = dt;
        PayList=pay.fn_In_PF(pay);
        if (PayList.Count > 0)
        {
            txt_EpfAcno.Value = PayList[0].Emp_Con_EPF.ToString();
            txt_PfAcno.Value = PayList[0].Emp_Con_FPF.ToString();
            txt_AdminAcno22.Value = PayList[0].charges.ToString();
        }
        else
        {
            txt_EpfAcno.Value = "";
            txt_PfAcno.Value = "";
            txt_AdminAcno22.Value = "";
        }
        
    }

    //protected void ddl_monthlist_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    DateTime dat = Convert.ToDateTime("01/" + ddl_monthlist.SelectedValue + "/" + ddl_yearlist.SelectedValue);
    //    pfform12(dat);
    //}

    //protected void ddl_yearlist_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    DateTime dat = Convert.ToDateTime("01/" + ddl_monthlist.SelectedValue + "/" + ddl_yearlist.SelectedValue);
    //    pfform12(dat);
    //}

    protected void ddl_Branch_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddl_Branch.SelectedValue != "0")
        {
            ViewState["PF_BranchID"] = Convert.ToInt32(ddl_Branch.SelectedValue);
           // session_check();
           // chkEmployee_load();
        }
        else
        {

        }
    }

    public void ddl_Branch_load()
    {
        int ddl_i;
        //branck dropdown
        ddlBranchsList = company.fn_getBranchs();

        if (ddlBranchsList.Count > 0)
        {
            for (ddl_i = -1; ddl_i < ddlBranchsList.Count; ddl_i++)
            {
                if (ddl_i == -1)
                {
                    ListItem list = new ListItem();

                    list.Text = "Select Branch";
                    list.Value = "0";
                    ddl_Branch.Items.Add(list);
                }
                else
                {
                    ListItem list = new ListItem();

                    list.Text = ddlBranchsList[ddl_i].CompanyName;
                    list.Value = ddlBranchsList[ddl_i].CompanyId.ToString();
                    ddl_Branch.Items.Add(list);
                }
            }
        }
      
    }

    //public void chkEmployee_load()
    //{
    //    if (s_login_role == "a")
    //    {
    //        pay.BranchId = (int)ViewState["PF_BranchID"];
    //        employee.BranchId = (int)ViewState["PF_BranchID"];
    //        c.BranchID = (int)ViewState["PF_BranchID"];
    //    }

    //    EmployeeList = employee.fn_getEmployeeList(employee);
    //    if (EmployeeList.Count > 0)
    //    {
    //        chk_Empcode.DataSource = EmployeeList;
    //        chk_Empcode.DataValueField = "EmployeeId";
    //        chk_Empcode.DataTextField = "LastName";
    //        chk_Empcode.DataBind();
    //    }
    //    else
    //    {
    //        chk_Empcode.Items.Clear();
    //    }
    //}

    protected void btn_Report_Click(object sender, EventArgs e)
    {
        try
        {
            int i_month, i_year;
            Collection<Employee> date_list;

            if (s_login_role == "a")
            {
                pay.BranchId = (int)ViewState["PF_BranchID"];
                employee.BranchId = (int)ViewState["PF_BranchID"];
                c.BranchID = (int)ViewState["PF_BranchID"];
            }

            month = ddl_monthlist.SelectedValue;
            year = ddl_yearlist.SelectedValue;
            i_month = Convert.ToInt32(month);
            i_year = Convert.ToInt32(year);

            if (ddl_type.SelectedValue == "2" || ddl_type.SelectedValue == "3")
            {
                fromdate = "01/" + month + "/" + year;
                todate = "01/" + ddl_tomonthlist.SelectedValue + "/" + ddl_toyearlist.SelectedValue;
            }
            else
            {
                fromdate = "01/" + month + "/" + year;
                todate = Convert.ToString(DateTime.DaysInMonth(i_year, i_month)) + "/" + month + "/" + year;
                //DateTime dt = Convert.ToDateTime("01/01/2009");
                //lbl_error.Text=dt.ToString("MMMM");
            }

            query = "delete from temp_employeeid";
            employee.fn_reportbyid(query);

            //Please Remove set dateformat dmy and set dateformat mdy text
            Session["preview_page"] = "~/PayrollReports/PFReport.aspx";
            switch (Convert.ToInt32(ddl_type.SelectedValue))
            {
                case 8:
                    {
                        string sFileName = Server.MapPath("~//Files//ECR.txt");
                        string Ecrtxt = "";
                        for (i = 0; i < chk_Empcode.Items.Count; i++)
                        {
                            string Pfno = "", EpsWage = "", EpfRemit = "", EpsRemit = "", NcpDay = "0", Refund = "0";
                            if (chk_Empcode.Items[i].Selected == true)
                            {
                                query = "set dateformat dmy;select a.PFno,a.Employee_First_Name, b.NetPay, b.PF, b.EPF, b.FPF from paym_employee a, PayOutput_PF b where a.pn_EmployeeID = b.pn_EmployeeID and a.pn_BranchID = b.pn_BranchID and a.pn_EmployeeID = " + chk_Empcode.Items[i].Value + " and b.d_Date = '" + fromdate + "' and a.pn_branchid = " + employee.BranchId + ";set dateformat mdy";
                                PayList = pay.fn_getECR(query);
                                if (PayList[0].PF_No == "")
                                    Pfno = "N/A";
                                if (PayList[0].PF_No != "")
                                    Pfno = PayList[0].PF_No;

                                //else
                                //    Pfno = PayList[0].PF_No;
                                if (PayList[0].Gross_Salary > 15000)
                                    EpsWage = "15000";
                                else
                                    EpsWage = PayList[0].Gross_Salary.ToString();
                                Ecrtxt += Pfno + "#~#" + PayList[0].FirstName + "#~#" + PayList[0].Gross_Salary.ToString() + "#~#" + PayList[0].Gross_Salary.ToString() + "#~#" + EpsWage + "#~#" + EpsWage + "#~#" + PayList[0].Emp_Con_PF.ToString() + "#~#" + PayList[0].Emp_Con_EPF.ToString() + "#~#" + PayList[0].Emp_Con_FPF.ToString() + "#~#0#~#0" + System.Environment.NewLine;
                            }
                    

                        if (File.Exists(sFileName))
                        {
                            File.Delete(sFileName);
                        }
                        using (StreamWriter sw = new StreamWriter(sFileName))
                        {
                            sw.Write(Ecrtxt);
                            sw.Close();
                        }
                    }

                        Response.Redirect("~//DownloadECR.ashx?rt=ECR.txt");

                        break;
                    }
                case 1:
                    for (i = 0; i < chk_Empcode.Items.Count; i++)
                    {
                        if (chk_Empcode.Items[i].Selected == true)
                        {
                            query = "set dateformat dmy;insert into temp_employeeid select pn_CompanyID, pn_BranchID, pn_EmployeeId, '" + fromdate + "' as d_date ";
                            query += "from paym_employee where pn_CompanyID=" + employee.CompanyId + " and pn_BranchID=" + employee.BranchId + " and pn_EmployeeId=" + chk_Empcode.Items[i].Value + ";set dateformat mdy";
                            employee.fn_reportbyid(query);
                        }
                    }

                    Session["ReportName"] = "~/crystalreports/Pf.rpt";
                    Response.Redirect("Report_view.aspx");
                    break;

                case 2:

                    query = "set dateformat dmy;SELECT DATEDIFF(month, '" + fromdate + "', '" + todate + "')+1 as differ;set dateformat mdy";
                    date_list = employee.fn_getdatediff(query);

                    if (date_list[0].Count == 12)
                    {
                        for (i = 0; i < chk_Empcode.Items.Count; i++)
                        {
                            if (chk_Empcode.Items[i].Selected == true)
                            {
                                query = "set dateformat dmy;insert into temp_Employeeid select p.pn_CompanyID, e.pn_BranchID, p.pn_EmployeeID, p.d_Date from payoutput_pf p, paym_Employee e where p.pn_CompanyID=e.pn_CompanyID and p.pn_EmployeeID=e.pn_EmployeeID and p.d_Date between '" + fromdate + "' and '" + todate + "'";
                                query += " and e.pn_CompanyID=" + employee.CompanyId + " and e.pn_BranchID=" + employee.BranchId + " and e.pn_EmployeeID=" + chk_Empcode.Items[i].Value + ";set dateformat mdy";
                                employee.fn_reportbyid(query);
                            }
                        }
                        Session["Repordid"] = "3a";
                        Session["Period"] = ddl_monthlist.SelectedItem.Text + " - " + ddl_yearlist.SelectedItem.Text + " to " + ddl_tomonthlist.SelectedItem.Text + " - " + ddl_toyearlist.SelectedItem.Text;
                        Session["ReportName"] = "~/crystalreports/form3anew.rpt";
                        Response.Redirect("Report_view.aspx");
                        break;
                    }
                    else
                    {
                        lbl_error.Text = "Select 12 Month Period";
                        break;
                    }

                case 3:

                    query = "set dateformat dmy;SELECT DATEDIFF(month, '" + fromdate + "', '" + todate + "')+1 as differ;set dateformat mdy";
                    date_list = employee.fn_getdatediff(query);

                    if (date_list[0].Count == 12)
                    {
                        for (i = 0; i < chk_Empcode.Items.Count; i++)
                        {
                            if (chk_Empcode.Items[i].Selected == true)
                            {
                                query = "set dateformat dmy;insert into temp_Employeeid select p.pn_CompanyID, e.pn_BranchID, p.pn_EmployeeID, p.d_Date from payoutput_pf p, paym_Employee e where p.pn_CompanyID=e.pn_CompanyID and p.pn_EmployeeID=e.pn_EmployeeID and p.d_Date between '" + fromdate + "' and '" + todate + "'";
                                query += " and e.pn_CompanyID=" + employee.CompanyId + " and e.pn_BranchID=" + employee.BranchId + " and e.pn_EmployeeID=" + chk_Empcode.Items[i].Value + ";set dateformat mdy";
                                employee.fn_reportbyid(query);
                            }
                        }
                        Session["Repordid"] = "6a";
                        Session["Period"] = ddl_monthlist.SelectedItem.Text + " - " + ddl_yearlist.SelectedItem.Text + " to " + ddl_tomonthlist.SelectedItem.Text + " - " + ddl_toyearlist.SelectedItem.Text;
                        Session["ReportName"] = "~/crystalreports/form6anew.rpt";
                        Response.Redirect("Report_view.aspx");
                        break;
                    }
                    else
                    {
                        lbl_error.Text = "Select 12 Month Period";
                        break;
                    }

                case 4:

                    pfprocess();
                    Session["Repordid"] = "12a";
                    Session["ReportName"] = "~/crystalreports/form12new.rpt";
                    Response.Redirect("Report_view.aspx");
                    break;

                case 5:

                    pfprocess();
                    Session["ReportName"] = "~/crystalreports/chalpf.rpt";
                    Response.Redirect("Report_view.aspx");
                    break;

                case 6:
                    for (i = 0; i < chk_Empcode.Items.Count; i++)
                    {
                        if (chk_Empcode.Items[i].Selected == true)
                        {
                            query = "set dateformat dmy;insert into temp_employeeid select e.pn_CompanyID, e.pn_BranchID, e.pn_EmployeeId, w.JoiningDate from paym_employee e, paym_employee_workdetails w";
                            query += " where e.pn_CompanyID=w.pn_CompanyID and e.pn_BranchID=w.pn_BranchID and e.pn_EmployeeID=w.pn_EmployeeID and e.pn_CompanyID=" + employee.CompanyId + "";
                            query += " and e.pn_BranchID=" + employee.BranchId + " and w.JoiningDate between '" + fromdate + "' and '" + todate + "' and e.pn_EmployeeID=" + chk_Empcode.Items[i].Value + ";set dateformat mdy";
                            employee.fn_reportbyid(query);
                        }
                    }

                    Session["ReportName"] = "~/crystalreports/Form5.rpt";
                    Response.Redirect("Report_view.aspx");
                    break;

                case 7:
                    for (i = 0; i < chk_Empcode.Items.Count; i++)
                    {
                        if (chk_Empcode.Items[i].Selected == true)
                        {
                            query = "set dateformat dmy;insert into temp_employeeid select e.pn_CompanyID, e.pn_BranchID, e.pn_EmployeeId, w.RetirementDate from paym_employee e, paym_employee_workdetails w";
                            query += " where e.pn_CompanyID=w.pn_CompanyID and e.pn_BranchID=w.pn_BranchID and e.pn_EmployeeID=w.pn_EmployeeID and e.pn_CompanyID=" + employee.CompanyId + "";
                            query += "and e.pn_BranchID=" + employee.BranchId + "and w.RetirementDate between '" + fromdate + "' and '" + todate + "' and e.pn_EmployeeID=" + chk_Empcode.Items[i].Value + ";set dateformat mdy";
                            employee.fn_reportbyid(query);
                        }
                    }
                    Session["ReportName"] = "~/crystalreports/Form10.rpt";
                    Response.Redirect("Report_view.aspx");
                    break;
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Enter all the mandatory fields');", true);
        }
    }
    protected void ddl_department_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddl_Employee_load();
        chkall.Checked = false;
    }

    public void ddl_Employee_load()
    {
        chk_Empcode.Items.Clear();

        if (ddl_department.SelectedItem.Text == "All")
        {
            EmployeeList = employee.fn_getEmployeeList(employee);
            if (EmployeeList.Count > 0)
            {
                for (int ddl_i = -1; ddl_i < EmployeeList.Count; ddl_i++)
                {
                    if (ddl_i == -1)
                    {
                       // Label2.Visible = true;
                    }
                    else
                    {
                        ListItem es_list = new ListItem();
                        es_list.Value = EmployeeList[ddl_i].EmployeeId.ToString();
                        es_list.Text = EmployeeList[ddl_i].LastName.ToString();
                        chk_Empcode.Items.Add(es_list);
                    }
                }
            }
        }
        else
        {
            employee.DepartmentId = Convert.ToInt32(ddl_department.SelectedValue);
            EmployeeList = employee.fn_getEmployeeDepartment(employee);
            if (EmployeeList.Count > 0)
            {
                for (int ddl_i = -1; ddl_i < EmployeeList.Count; ddl_i++)
                {
                    if (ddl_i == -1)
                    {
                       // Label2.Visible = true;
                    }
                    else
                    {
                        ListItem es_list = new ListItem();
                        es_list.Value = EmployeeList[ddl_i].EmployeeId.ToString();
                        es_list.Text = EmployeeList[ddl_i].LastName.ToString();
                        chk_Empcode.Items.Add(es_list);
                    }
                }
            }
        }
    }
}
