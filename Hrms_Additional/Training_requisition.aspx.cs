using System;
using System.Configuration;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using ePayHrms.Candidate;
using ePayHrms.Company;
using System.Collections.ObjectModel;
using ePayHrms.BE.Recruitment;
using ePayHrms.Employee;
using ePayHrms.Leave;

public partial class Hrms_Additional_Training_requisition : System.Web.UI.Page
{
    ePayHrms.Connection.Connection Con = new ePayHrms.Connection.Connection();
    SqlConnection myConnection = new SqlConnection(ConfigurationManager.AppSettings["Connectionstring"]);
    SqlCommand cmd1 = new SqlCommand();
    Company company = new Company();
    Employee employee = new Employee();
    Be_Recruitment r = new Be_Recruitment();
    PayRoll pay = new PayRoll();
    Candidate c = new Candidate();
    Leave l = new Leave();

    Collection<Company> CompanyList, ddlBranchsList;
    Collection<Employee> DepartmentList;
    Collection<Employee> EmployeeList;
    Collection<PayRoll> PayList;
    Collection<Employee> InstitutionName;
    Collection<Employee> prgmnameList;
    Collection<Employee> prgmtypList;
    Collection<Employee> TrainerName;
    string s_login_role;
    int presentday = 1;
    int i = 0, j, temp_count = 0;
    int ddl_i = 0;
    string query = "";
    DateTime from_date, to_date;
    int empid, count;
    int daycount;
    string empname, date1, date2, _Month, monthyear;
    string[] sd, ed;
    string s_form = "";
    int from_month, to_month, _Year;
    double act_basic, earned_basic, _Amount, Tot_amt;
    DateTime fromdate, todate;
    DataSet ds_userrights;
    int chk_i = 0;
    string _Value;
    protected void Page_Load(object sender, EventArgs e)
    {
        
        Session["Repordid"] = "";
        Session["fdate"] = "";
        Session["tdate"] = "";

        employee.CompanyId = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
        pay.CompanyId = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
        c.CompanyID = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);

        employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
         pay.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
        c.BranchID = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);

        s_login_role = Request.Cookies["Login_temp_Role"].Value;
        //lbl_error.Text = "";

        if (!IsPostBack)
        {
            // date_load();

            //InstitutionName = employee.fn_getInstList(employee);
            //ddl_InstName.DataSource = InstitutionName;
            //ddl_InstName.DataValueField = "InstitutionId";
            //ddl_InstName.DataTextField = "InstitutionName";
            //ddl_InstName.DataBind();

            ////prgmnameList = employee.fn_programname(employee);
            ////ddl_PrgmType.DataSource = prgmnameList;
            ////ddl_PrgmType.DataValueField = "prgmid";
            ////ddl_PrgmType.DataTextField = "prgmname";
            ////ddl_PrgmType.DataBind();

            ////prgmtypList = employee.fn_programtypes(employee);
            ////ddl_PrgmType.DataSource = prgmtypList;
            ////ddl_PrgmType.DataValueField = "prgmtypId";
            ////ddl_PrgmType.DataTextField = "prgmtypName";
            ////ddl_PrgmType.DataBind();

            //TrainerName = employee.fn_gettrainerNameList1(employee);
            //ddl_TrainerName.DataSource = TrainerName;
            //ddl_TrainerName.DataValueField = "trnrID";
            //ddl_TrainerName.DataTextField = "trnrName";
            //ddl_TrainerName.DataBind();
            CompanyList = company.fn_getCompany();
            ListItem li = new ListItem();
            if (CompanyList.Count > 0)
            {
                switch (s_login_role)
                {
                    case "a":
                        //admin();
                        //session_check();

                        ddl_Branch.Visible = true;
                        ddl_department_load1();
                        auto_increment();
                        ddl_Institution_load();
                        grid();
                        //ddl_Branch_load();
                        break;

                    case "h":
                        ddl_Institution_load();
                        ddl_department_load1();
                        ddl_Branch.Visible = false;
                        auto_increment();
                        grid();
                        //session_check();
                        break;

                    case "u": s_form = "79";
                        ds_userrights = company.check_Userrights((int)Session["Login_temp_EmployeeID"], s_form);

                        if (ds_userrights.Tables[0].Rows.Count > 0)
                        {
                            ddl_Branch.Visible = false;
                            li = new ListItem();
                            li.Text = Request.Cookies["EmpCodeName"].Value;
                            li.Value = Request.Cookies["Login_temp_EmployeeID"].Value;
                            li.Selected = true;
                            chk_Empcode.Items.Add(li);
                            chk_Empcode.Enabled = false;
                            lbl_selectemp.Visible = false;
                            chkall.Visible = false;
                            ddl_department_load1();
                            ddl_Branch.Visible = false;
                            auto_increment();
                            ddl_Institution_load();
                        }
                        else
                        {
                            Response.Cookies["Msg_Session"].Value=  "Permission Restricted. Please Contact Administrator.";
                            Response.Redirect("~/Company_Home.aspx");
                        }
                        break;

                    case "e":
                        ddl_Branch.Visible = false;
                        li = new ListItem();
                        li.Text = Request.Cookies["EmpCodeName"].Value;
                        li.Value = Request.Cookies["Login_temp_EmployeeID"].Value;
                        li.Selected = true;
                        chk_Empcode.Items.Add(li);
                        chk_Empcode.Enabled = false;
                        lbl_selectemp.Visible = false;
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
    public void ddl_Institution_load()
    {

        ddl_InstName.Items.Clear();
        EmployeeList = employee.fn_getInstitution(employee.BranchId);
        if (EmployeeList.Count > 0)
        {

            for (int ddl_i = -1; ddl_i < EmployeeList.Count; ddl_i++)
            {
                if (ddl_i == -1)
                {
                    ListItem list = new ListItem();

                    list.Text = "Select Institution";
                    list.Value = "sd";
                    ddl_InstName.Items.Add(list);
                }
                else
                {
                    ListItem list = new ListItem();

                    list.Text = EmployeeList[ddl_i].DepartmentName;
                    list.Value = EmployeeList[ddl_i].DepartmentId.ToString();
                    ddl_InstName.Items.Add(list);
                }
            }
        }
        else
        {
            lbl_Error.Text = "No Institution Available";
        }

    }
    public void ddl_Trainer_load()
    {
        ddl_TrainerName.Items.Clear();
        employee.CompanyId = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
        employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
        employee.InstitutionId = Convert.ToInt32(ddl_InstName.SelectedValue);
        EmployeeList = employee.fn_gettrainer(employee);
        if (EmployeeList.Count > 0)
        {

            for (int ddl_i = -1; ddl_i < EmployeeList.Count; ddl_i++)
            {
                if (ddl_i == -1)
                {
                    ListItem list = new ListItem();

                    list.Text = "Select Trainer";
                    list.Value = "sd";
                    ddl_TrainerName.Items.Add(list);
                }
                else
                {
                    ListItem list = new ListItem();

                    list.Text = EmployeeList[ddl_i].DepartmentName;
                    list.Value = EmployeeList[ddl_i].DepartmentId.ToString();
                    ddl_TrainerName.Items.Add(list);
                }
            }
        }
        else
        {
            lbl_Error.Text = "No Trainer Available";
        }

    }
    public void ddl_department_load1()
    {
        ddl_department.Items.Clear();
        employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
        if (s_login_role == "a")
        {
            DepartmentList = employee.fn_getDepartmentList1(Convert.ToInt32(ddl_Branch.SelectedItem.Value));
        }
        else if (s_login_role == "h")
        {
            DepartmentList = employee.fn_getDepartmentList1(employee.BranchId);
        }

        if (DepartmentList.Count > 0)
        {
            for (ddl_i = -1; ddl_i < DepartmentList.Count; ddl_i++)
            {
                if (ddl_i == -1)
                {
                    ListItem e_list = new ListItem();
                    e_list.Text = "Select";
                    e_list.Value = "0";
                    ddl_department.Items.Add(e_list);
                }
                else
                {
                    ListItem e_list = new ListItem();
                    e_list.Value = DepartmentList[ddl_i].DepartmentId.ToString();
                    e_list.Text = DepartmentList[ddl_i].DepartmentName.ToString();
                    ddl_department.Items.Add(e_list);
                }
            }
        }
        else
        {

            ClientScriptManager manager = Page.ClientScript;
            manager.RegisterStartupScript(this.GetType(), "Call", "show_Error();", true);
        }
    }

    public void chkEmployee_load()
    {
        employee.DepartmentId = Convert.ToInt32(ddl_department.SelectedItem.Value);
        ViewState["pn_DepartmentId"] = Convert.ToInt32(ddl_department.SelectedItem.Value);
        employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);

        string str1 = ddl_department.SelectedItem.Text;
        employee.DivisionName = str1;
        string qry = "Select a.pn_EmployeeID,a.EmployeeCode,a.Employee_First_Name from paym_employee a, paym_employee_profile1 b where a.pn_CompanyID=b.pn_CompanyID and a.pn_BranchID=b.pn_BranchID and a.pn_EmployeeID=b.pn_EmployeeID and b.pn_DepartmentId=" + ddl_department.SelectedValue + " and b.pn_CompanyID=" + employee.CompanyId + " and b.pn_BranchID=" + employee.BranchId + " and status='Y' order by EmployeeCode ";

        EmployeeList = employee.fn_getEmplist(qry);
        if (EmployeeList.Count > 0)
        {
            chk_Empcode.DataSource = EmployeeList;
            chk_Empcode.DataValueField = "EmployeeId";
            chk_Empcode.DataTextField = "LastName";
            chk_Empcode.DataBind();
        }
        else
        {
            chk_Empcode.Items.Clear();
            div_chkempcode.Visible = false;
            lbl.Visible = false;
            chk_Empcode.Visible = false;
            lbl_selectemp.Visible = false;
            chkall.Visible = false;
            lbl_Error.Text = "No Employee Found";
        }
    }

    protected void ddl_department_SelectedIndexChanged(object sender, EventArgs e)
    {
        div_chkempcode.Visible = true;
        lbl.Visible = true;
        chk_Empcode.Visible = true;
        lbl_selectemp.Visible = true;
        chkall.Visible = true;
        chkEmployee_load();
    }

    public void save()
    {
        //employee.TrainingID = 0;
        employee.TrainingID = Convert.ToInt32(txtid.Text);
        employee.prgmname = (txtid.Text + "-" + txtpgmname.Text);
        employee.DurationFrom = txtDurationFrom.Text;
        employee.DurationTo = txtDurationTo.Text;
        employee.temp_str = txtsummary.Text;
        employee.InstitutionId = Convert.ToInt32(ddl_InstName.SelectedItem.Value);
        employee.prgmtypName = txtcost.Text;
        employee.trnrID = Convert.ToInt32(ddl_TrainerName.SelectedItem.Value);
        employee.IDno = txtHrs.Text;
        _Value = employee.Employee_Training(employee);

    }
    public void auto_increment()
    {

        myConnection.Open();
        cmd1 = new SqlCommand("select max(trainingid) from paym_training_new", myConnection);
        string count = Convert.ToString(cmd1.ExecuteScalar());
        if (count.Length == 0)
        {
            txtid.Text = "001";
        }
        else
        {
            int pcount = Convert.ToInt32(count);
            int pcounta = pcount + 1;
            if (pcounta > 0 && pcounta < 10)
            {
                //string pcountAdd =
                txtid.Text = "00" + pcounta;
            }
            else if (pcounta > 9 && pcounta < 100)
            {
                txtid.Text = "0" + pcounta;
            }
            else
            {
                txtid.Text = "" + pcounta;
            }
        }
        myConnection.Close();
    }

    public void clear()
    {
        txtpgmname.Text = "";
        ddl_InstName.SelectedItem.Text = "Select Institution";
        ddl_TrainerName.Items.Clear();
        // ddl_TrainerName.SelectedItem.Text = "Select Trainer";
        txtDurationFrom.Text = "";
        txtDurationTo.Text = "";
        txtcost.Text = "0.00";
        ddl_department.SelectedItem.Text = "Select Department";
        div_chkempcode.Visible = false;
        lbl.Visible = false;
        chk_Empcode.Visible = false;
        lbl_selectemp.Visible = false;
        chkall.Visible = false;
    }
    protected void ImageButton1_Click(object sender, EventArgs e)
    {
        try
        {
            if (s_login_role == "a")
            {
                //employee.EmployeeId = 0;
                employee.BranchId = (int)ViewState["Training_BranchID"];
            }

            if (s_login_role == "h")
            {
                //employee.EmployeeId = 0;
                employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
            }

            string hSeqID = "0";

            int _Value = 0;
            for (chk_i = 0; chk_i < chk_Empcode.Items.Count; chk_i++)
            {
                if (chk_Empcode.Items[chk_i].Selected == true)
                {
                    employee.EmployeeId = Convert.ToInt32(chk_Empcode.Items[chk_i].Value);
                    employee.TrainingID = Convert.ToInt32(hSeqID);
                    save();
                }
            }
            if (_Value != 1)
            {
                lbl_Error.Text = "<font color=Blue>Added Successfully</font>";
                auto_increment();

               
                clear();
                grid();
                //grid_load();
                //Response.Redirect("PreviewEmployee.aspx");
            }
            else
            {
                lbl_Error.Text = "<font color=Red>Error Occured</font>";
            }
            //}
        }
        catch (Exception ex)
        {
            lbl_Error.Text = "Error";
        }
    }
    protected void ddl_InstName_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddl_Trainer_load();
    }


    public void grid()
    {
        try
        {
            employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);

            myConnection.Open();
            cmd1 = new SqlCommand("select distinct(b.ProgramName),b.Trainingid,b.v_durationfrom,b.v_durationto,b.TrainingHrs,b.TrainingCost,c.fname,d.ins_name,(a.Employeecode+'-'+a.Employee_First_Name) as EmployeeName,b.v_summary from paym_employee a,paym_training_new b,trainer_profile1 c,institution_profile d where a.pn_employeeid=b.pn_employeeid and b.TrainerId=c.trainer_id and b.InstId=d.id and b.pn_branchid='" + employee.BranchId + "'", myConnection);
            SqlDataAdapter da = new SqlDataAdapter(cmd1);
            DataSet ds = new DataSet();
            da.Fill(ds);
            GridView1.DataSource = ds;
            GridView1.DataBind();
            //int i = GridView1.Rows.Count;
            
           
            myConnection.Close();
        }
        catch (Exception ex) { }
    }


    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView1.EditIndex = e.NewEditIndex;
        grid();
        DropDownList drp = (DropDownList)GridView1.Rows[e.NewEditIndex].FindControl("ddl_dinst");
        drp.Items.Clear();
        EmployeeList = employee.fn_getInstitution(employee.BranchId);
        if (EmployeeList.Count > 0)
        {

            for (int ddl_i = -1; ddl_i < EmployeeList.Count; ddl_i++)
            {
                if (ddl_i == -1)
                {
                    ListItem list = new ListItem();

                    list.Text = "Select Institution";
                    list.Value = "sd";
                    drp.Items.Add(list);
                }
                else
                {
                    ListItem list = new ListItem();

                    list.Text = EmployeeList[ddl_i].DepartmentName;
                    list.Value = EmployeeList[ddl_i].DepartmentId.ToString();
                    drp.Items.Add(list);
                }
            }
        }
        else
        {
            lbl_Error.Text = "No Institution Available";
        }
        
    }

    protected void ddl_dinst_selectedindex_changed(object sender, EventArgs e)
    {
        DropDownList drp = (DropDownList)sender;
        GridViewRow row = (GridViewRow)drp.NamingContainer;

        DropDownList drp1 = (DropDownList)drp.NamingContainer.FindControl("ddl_traner");
        employee.CompanyId = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
        employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
        employee.InstitutionId = Convert.ToInt32(drp.SelectedValue);

        EmployeeList = employee.fn_gettrainer(employee);
        drp1.Items.Clear();
        if (EmployeeList.Count > 0)
        {

            for (int ddl_i = -1; ddl_i < EmployeeList.Count; ddl_i++)
            {
                if (ddl_i == -1)
                {
                    ListItem list = new ListItem();

                    list.Text = "Select Trainer";
                    list.Value = "sd";
                    drp1.Items.Add(list);
                }
                else
                {
                    ListItem list = new ListItem();

                    list.Text = EmployeeList[ddl_i].DepartmentName;
                    list.Value = EmployeeList[ddl_i].DepartmentId.ToString();
                    drp1.Items.Add(list);
                }
            }
        }
        else
        {
            lbl_Error.Text = "No Trainer Available";
        }

    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //for (int rowIndex = 7 - 2;
        //                              rowIndex >= 0; rowIndex--)
        //{
        //    GridViewRow gvRow = e.Row[rowIndex];// GridView1.Rows[rowIndex];
        //    GridViewRow gvPreviousRow = GridView1.Rows[rowIndex + 1];
        //    for (int cellCount = 0; cellCount < gvRow.Cells.Count;
        //                                                  cellCount++)
        //    {
        //        if (gvRow.Cells[cellCount].Text ==
        //                               gvPreviousRow.Cells[cellCount].Text)
        //        {
        //            if (gvPreviousRow.Cells[cellCount].RowSpan < 2)
        //            {
        //                gvRow.Cells[cellCount].RowSpan = 2;
        //            }
        //            else
        //            {
        //                gvRow.Cells[cellCount].RowSpan =
        //                    gvPreviousRow.Cells[cellCount].RowSpan + 1;
        //            }
        //            gvPreviousRow.Cells[cellCount].Visible = false;
        //        }
        //    }
        //}
    }
    protected void GridView1_PreRender(object sender, EventArgs e)
    {
        int i = GridView1.Rows.Count;
        for (int rowIndex = i - 2; rowIndex >= 0; rowIndex--)
        {
            GridViewRow row = GridView1.Rows[rowIndex];
            GridViewRow previousRow = GridView1.Rows[rowIndex + 1];

            if (((Label)row.Cells[0].FindControl("lblid")).Text == ((Label)previousRow.Cells[0].FindControl("lblid")).Text)
            {
                row.Cells[0].RowSpan = previousRow.Cells[0].RowSpan < 2 ? 2 : previousRow.Cells[0].RowSpan + 1;
                row.Cells[1].RowSpan = previousRow.Cells[1].RowSpan < 2 ? 2 : previousRow.Cells[1].RowSpan + 1;
                row.Cells[2].RowSpan = previousRow.Cells[2].RowSpan < 2 ? 2 : previousRow.Cells[2].RowSpan + 1;
                row.Cells[3].RowSpan = previousRow.Cells[3].RowSpan < 2 ? 2 : previousRow.Cells[3].RowSpan + 1;
                row.Cells[4].RowSpan = previousRow.Cells[4].RowSpan < 2 ? 2 : previousRow.Cells[4].RowSpan + 1;
                row.Cells[5].RowSpan = previousRow.Cells[3].RowSpan < 2 ? 2 : previousRow.Cells[3].RowSpan + 1;
                row.Cells[6].RowSpan = previousRow.Cells[4].RowSpan < 2 ? 2 : previousRow.Cells[4].RowSpan + 1;
                row.Cells[7].RowSpan = previousRow.Cells[4].RowSpan < 2 ? 2 : previousRow.Cells[4].RowSpan + 1;

                previousRow.Cells[0].Visible = false;
                previousRow.Cells[1].Visible = false;
                previousRow.Cells[2].Visible = false;
                previousRow.Cells[3].Visible = false;
                previousRow.Cells[4].Visible = false;
                previousRow.Cells[5].Visible = false;
                previousRow.Cells[6].Visible = false;
                previousRow.Cells[7].Visible = false;
            }
        }
    }

    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex = -1;
        grid();
    }
    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            GridViewRow Gvrow = GridView1.Rows[e.RowIndex];
            if (Gvrow != null)
            {
                employee.CompanyId = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
                employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
                employee.TrainingID = Convert.ToInt32(((Label)Gvrow.FindControl("lblid")).Text);
                employee.prgmname = ((Label)Gvrow.FindControl("lblPgmName")).Text;
                employee.DurationFrom = ((TextBox)Gvrow.FindControl("txtfromdate")).Text;
                employee.DurationTo = ((TextBox)Gvrow.FindControl("txtTodate")).Text;
                employee.InstitutionId = Convert.ToInt32(((DropDownList)Gvrow.FindControl("ddl_dinst")).SelectedValue);
                employee.trnrID = Convert.ToInt32(((DropDownList)Gvrow.FindControl("ddl_traner")).SelectedValue);
                employee.prgmtypName = ((TextBox)Gvrow.FindControl("txt_tCost")).Text;
                employee.temp_str = ((TextBox)Gvrow.FindControl("txtsum")).Text;
                employee.IDno = ((TextBox)Gvrow.FindControl("txthrs")).Text;
                _Value = employee.Employee_Training1(employee);

                if (_Value != "1")
                {
                    lbl_Error.Text = "<font color=Blue>Updated Successfully</font>";
                    GridView1.EditIndex = -1;
                    grid();
                }
                else
                {
                    lbl_Error.Text = "<font color=Red>Error Occured</font>";
                }
            }
          
        }
        catch (Exception ex) { }
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            string ID = ((Label)GridView1.Rows[e.RowIndex].Cells[0].FindControl("lblPgmName")).Text;

            DeleteRecord(ID);

            grid();
        }
        catch (Exception ex) { }
       
    }
    private void DeleteRecord(string ID)
    {
        lbl_Error.Text = "";
        string sqlStatement = "DELETE FROM paym_training_new WHERE  ProgramName= @ProgramName";
        try
        {
            myConnection.Open();
            SqlCommand cmd = new SqlCommand(sqlStatement, myConnection);
            cmd.Parameters.AddWithValue("@ProgramName", ID);
            cmd.CommandType = CommandType.Text;
            cmd.ExecuteNonQuery();
            myConnection.Close();
        }
        catch (Exception ex)
        {
            lbl_Error.Text = "errrrrorrrrrr";

        }
    }

}
