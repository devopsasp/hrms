using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using ePayHrms.Login;
using ePayHrms.BE.Recruitment;
using ePayHrms.Connection;
using ePayHrms.Candidate;
using System.IO;
using System.IO.Ports;
using ePayHrms.Company;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using ePayHrms.Leave;
using ePayHrms.Employee;
using System.Text;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Collections;

public partial class Hrms_TimeAndAttenence_Connection : System.Web.UI.Page
{

    
    private int iMachineNumber = 1;
 bool bIsConnected ;
    //***************
    Company company = new Company();
    Employee employee = new Employee();
    Machine Machin = new Machine();
    Leave l = new Leave();
    public string str2;
    Collection<Company> CompanyList;
    string str = "";

    string MacError = "";
    string s_login_role;

    string s_form = "";
    DataSet ds_userrights;

    ePayHrms.Connection.Connection Con = new ePayHrms.Connection.Connection();
    SqlConnection con_new = new SqlConnection(ConfigurationManager.ConnectionStrings["connectionstring"].ConnectionString);
    DataSet gridds = new DataSet();
    //*****************

    public void populate_lv()
    {
        var conStr = ConfigurationManager.ConnectionStrings["connectionstring"];
        string constr = conStr.ConnectionString;
        SqlConnection con = new SqlConnection(constr);
        con.Open();
        //SqlCommand cmd = new SqlCommand("create table current_details(machine_num int,enroll_num int,VerifyMode int,InOutMode int,year int,month int,day int,hour int,min int,sec int,pn_branchid int,pn_companyid int)", con);
        //cmd.ExecuteNonQuery();
        SqlCommand cmd = new SqlCommand("select * from current_details where pn_branchid='" + employee.BranchId + "' and pn_companyid='" + employee.CompanyId + "' order by month desc , day desc", con);
        SqlDataAdapter ada = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        ada.Fill(ds);
        lv_fingerDetails.DataSource = ds;
        lv_fingerDetails.DataBind();
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        
        employee.CompanyId = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
        employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
        l.CompanyID = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
        s_login_role = Request.Cookies["Login_temp_Role"].Value;

        if (!IsPostBack)
        {
            CompanyList = company.fn_getCompany();

            if (CompanyList.Count > 0)
            {
                employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
                employee.CompanyId = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
                l.BranchID = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);

                switch (s_login_role)
                {
                    case "a":
                        GridFill();
                        
                        access();

                        break;

                    case "h":
                        GridFill();
                        access();
                        break;

                    case "e":
                        break;

                    case "u": //s_form = "5";
                        s_form = "36";
                        ds_userrights = company.check_Userrights((int)Session["Login_temp_EmployeeID"], s_form);
                        if (ds_userrights.Tables[0].Rows.Count > 0)
                        {
                            GridFill();
                        }
                        else
                        {
                            Response.Cookies["Msg_Session"].Value=  "Permission Restricted. Please Contact Administrator.";
                            Response.Redirect("~/Company_Home.aspx");
                        }
                    break;


                        break;

                    default: Response.Cookies["Msg_Session"].Value=  "Permission Restricted. Please Contact Administrator";
                        Response.Redirect("Company_Home.aspx");
                        break;
                }
            }
            else
            {
                Response.Cookies["Msg_Session"].Value = "Create Company";
                Response.Redirect("Company_Home.aspx");
            }
        }
    }

    public void access()
    {
        con_new.Open();
        SqlCommand cmd = new SqlCommand();
        cmd = new SqlCommand("Select * from hr_authentication where pn_CompanyID = '" + employee.CompanyId + "' and pn_BranchID='" + employee.BranchId + "' and sectionid=7 and section_view='No'", con_new);
        SqlDataReader rdrview = cmd.ExecuteReader();
        if (rdrview.Read())
        {
            //Response.Write("<script language='javascript'>alert('Permission Restricted. Please Contact Administrator.');window.location='~/Company_Home.aspx';</script>");
        }
        rdrview.Close();
        cmd = new SqlCommand("Select * from hr_authentication where pn_CompanyID = '" + employee.CompanyId + "' and pn_BranchID='" + employee.BranchId + "' and sectionid=7 and section_edit='No'", con_new);
        SqlDataReader rdredit = cmd.ExecuteReader();
        if (rdredit.Read())
        {
            //btn_formulaedit.Visible = false;
        }
        rdredit.Close();
        cmd = new SqlCommand("Select * from hr_authentication where pn_CompanyID = '" + employee.CompanyId + "' and pn_BranchID='" + employee.BranchId + "' and sectionid=7 and  section_delete='No'", con_new);
        SqlDataReader rdrdel = cmd.ExecuteReader();
        if (rdrdel.Read())
        {
            // ((ImageButton)grid_Course.Rows[0].FindControl("img_update")).Visible = false;
        }
        rdrdel.Close();
        con_new.Close();

    }


    protected void cmd_collect_Click(object sender, EventArgs e)
    {
        try
        {
            //lblerror.Text = "Working";
            zkemkeeper.CZKEMClass CtrlBioComm = new zkemkeeper.CZKEMClass();
            zkemkeeper.CZKEMClass axCZKEM1 = new zkemkeeper.CZKEMClass();
            if (lblhidden.Text == "Connection established!!!")
            {
                bIsConnected = CtrlBioComm.Connect_Net(str2, 4370);

                bool ret = CtrlBioComm.ReadAllGLogData(1);
                if (ret)
                {
                    int dwMachineNum = 1;

                    string dwEnrollNum;

                    int dwVerifyMode = 0;
                    int dwInOutMode = 0;
                    int dwyear = 0;
                    string year = "";
                    string month = "";
                    string day = "";
                    int dwmonth = 0;
                    int dwday = 0;
                    int dwhour = 0;
                    int dwmin = 0;
                    int dwsec = 0;
                    int dwworkcode = 0;
                    int i = 0;
                    SqlCommand cmd2 = new SqlCommand();

                    while (CtrlBioComm.SSR_GetGeneralLogData(dwMachineNum, out dwEnrollNum, out dwVerifyMode, out dwInOutMode, out dwyear, out dwmonth, out dwday, out dwhour, out dwmin, out dwsec, ref dwworkcode))
                    {

                        string sc = "", en = "", ec = "";
                        //Cursor.Current = Cursors.WaitCursor;
                        DateTime dateValue = new DateTime(dwyear, dwmonth, dwday);
                        //string my = dwmonth.ToString() + "/" + dwyear.ToString();
                        //if (dwmonth.ToString().Length == 1)
                        //{
                        //    my = "0" + dwmonth.ToString() + "/" + dwyear.ToString();
                        //}
                        string preday = dateValue.DayOfWeek.ToString();
                        cmd2 = new SqlCommand("select EmployeeCode , employee_first_name , card_no from  paym_employee where readerid='" + dwEnrollNum + "' ", con_new);
                        SqlDataReader reader1 = cmd2.ExecuteReader();
                        if (reader1.Read())
                        {
                            sc = "";
                            en = Convert.ToString(reader1[1]);
                            ec = Convert.ToString(reader1[0]);
                        }
                        else
                        {
                            cmd2 = new SqlCommand("select RegisterNo, StudentName , ReaderID from paym_student where ReaderID='" + dwEnrollNum + "'", con_new);
                            reader1 = cmd2.ExecuteReader();
                            if (reader1.Read())
                            {
                                sc = "";
                                en = Convert.ToString(reader1[1]);
                                ec = Convert.ToString(reader1[0]);
                            }
                        }

                        reader1.Close();

                        StringBuilder _data = new StringBuilder();
                        _data.AppendFormat("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9}", dwMachineNum, dwEnrollNum, dwVerifyMode, dwInOutMode, dwyear, dwmonth, dwday, dwhour, dwmin, dwsec, dwworkcode);
                        year = dwyear.ToString();
                        month = dwmonth.ToString();
                        day = dwday.ToString();
                        SqlCommand cmd1 = new SqlCommand("insert into punch_details (pn_companyid,pn_branchid,machine_num,card_no,emp_code, emp_name,VerifyMode,InOutMode, shift_code,dates,days,times) values('" + employee.CompanyId + "','" + employee.BranchId + "','" + dwMachineNum + "','" + dwEnrollNum + "', '" + ec + "' , '" + en + "' , '" + dwVerifyMode + "','" + dwInOutMode + "', '" + sc + "' ,'" + (year + '/' + month + '/' + day) + "', '" + preday + "' ,'" + dwhour + ':' + dwmin + ':' + dwsec + "')", con_new);
                        cmd1.ExecuteNonQuery();
                        //cmd1 = new SqlCommand("insert into punch_details (pn_branchid,pn_companyid,Machine_num,Enroll_Num,verifymode,inoutmode,year,month,day,hour,min,sec) values('"+employee.BranchId+"','"+employee.CompanyId+"','" + dwMachineNum + "','" + dwEnrollNum + "','" + dwVerifyMode + "','" + dwInOutMode + "','" + dwyear + "','" + dwmonth + "','" + dwday + "','" + dwhour + "','" + dwmin + "','" + dwsec + "')", con);
                        //cmd1.ExecuteNonQuery();
                        cmd2 = new SqlCommand("insert into current_details (pn_branchid,pn_companyid,Machine_num,Enroll_Num,Name,Days,verifymode,inoutmode,year,month,day,hour,min,sec) values('" + employee.BranchId + "','" + employee.CompanyId + "','" + str + "','" + dwEnrollNum + "','" + en + "' ,'" + preday + "' ,'" + dwVerifyMode + "','" + dwInOutMode + "','" + dwyear + "','" + dwmonth + "','" + dwday + "','" + dwhour + "','" + dwmin + "','" + dwsec + "')", con_new);
                        cmd2.ExecuteNonQuery();
                        i++;

                        //DateTime dt = Convert.ToDateTime(dwyear + '/' + dwmonth + '/' + dwday);
                        //MessageBox.Show(Convert.ToString(year + '/' + month + '/' + day));
                        //MessageBox.Show(Convert.ToString(dwyear + '/' + dwmonth + '/' + dwday));

                    }




                    string cmd_text = "IF EXISTS (SELECT * FROM sysobjects WHERE Name = 'temp_punch_details')" + "DROP TABLE temp_punch_details";
                    cmd2 = new SqlCommand(cmd_text, con_new);
                    cmd2.ExecuteNonQuery();

                    SqlCommand cmd = new SqlCommand("create table temp_punch_details(pn_companyid int,pn_branchid int,machine_num bigint, card_no varchar(15) , emp_code varchar(15), emp_name varchar(50),VerifyMode int,InOutMode int, shift_code varchar(5),dates datetime,days varchar(15),times time , ot_hrs time , status varchar(2))", con_new);
                    cmd.ExecuteNonQuery();
                    cmd = new SqlCommand("INSERT INTO temp_punch_details SELECT DISTINCT * FROM punch_details", con_new);
                    cmd.ExecuteNonQuery();
                    cmd = new SqlCommand("drop table punch_details", con_new);
                    cmd.ExecuteNonQuery();
                    cmd = new SqlCommand("EXEC sp_rename 'temp_punch_details', 'punch_details'", con_new);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        catch (Win32Exception we)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('"+we.ErrorCode.ToString()+"');", true);
        }

    }

    protected void lv_fingerDetails_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    public void GridFill()
    {
        string sql = "select * from Machine where pn_CompanyID ='" + employee.CompanyId + "' and pn_BranchID = '" + employee.BranchId + "'";
        gridds = Machin.Grid_Output(sql);
        if (gridds.Tables[0].Rows.Count > 0)
        {
            GridMachine.DataSource = gridds;
            GridMachine.DataBind();
        }
        else
        {
            gridds.Tables[0].Rows.Add(gridds.Tables[0].NewRow());
            GridMachine.DataSource = gridds;
            GridMachine.DataBind();
            int columncount = GridMachine.Rows[0].Cells.Count;
            GridMachine.Rows[0].Cells.Clear();
            GridMachine.Rows[0].Cells.Add(new TableCell());
            GridMachine.Rows[0].Cells[0].ColumnSpan = columncount;
            GridMachine.Rows[0].Cells[0].Text = "No Record Found";
        }
    }

    protected void GridMachine_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridMachine.EditIndex = e.NewEditIndex;
        GridFill();
    }

    protected void GridMachine_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string ID = ((CheckBox)GridMachine.Rows[e.RowIndex].FindControl("ChkMac")).Text;
        string Query = "Delete from Machine Where MNo='" + Convert.ToString(GridMachine.DataKeys[e.RowIndex].Value.ToString()) + "'";
        string res = Machin.RowDelete(ID, Query);
        if (res == "1")
        {
            GridFill();
        }
    }

    protected void GridMachine_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        string userid = Convert.ToString(GridMachine.DataKeys[e.RowIndex].Value.ToString());
        GridViewRow row = (GridViewRow)GridMachine.Rows[e.RowIndex];
        CheckBox ChkMac = (CheckBox)row.FindControl("ChkMac");
        TextBox lblIp = (TextBox)row.FindControl("txtip0");
        string loc = ((TextBox)row.FindControl("txtloc")).Text;
        GridMachine.EditIndex = -1;
        string sql = "update Machine Set IPAddr='" + lblIp.Text + "', Location = '" + loc + "' where MNo='" + userid + "'";
        string res = Machin.RowUpdate(sql);
        if (res == "1")
        {
            GridFill();
        }
    }

    protected void GridMachine_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridMachine.PageIndex = e.NewPageIndex;
        GridFill();
    }

    protected void GridMachine_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridMachine.EditIndex = -1;
        GridFill();
    }

    protected void ChkMac_CheckedChanged(object sender, EventArgs e)
    {
        string row = ((Label)GridMachine.Rows[1].FindControl("lblMac")).Text;
        ((CheckBox)GridMachine.HeaderRow.FindControl("Chkall")).Checked = false;
    }

    protected void GridMachine_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        foreach (GridViewRow row in GridMachine.Rows)
        {
            CheckBox chk = (CheckBox)row.FindControl("ChkMac");
            if (chk != null & chk.Checked)
            {
                string str;
                str = GridMachine.DataKeys[e.NewSelectedIndex].Value.ToString();
                string strname = row.Cells[3].Text;
            }
        }
    }

    protected void btn_Connect_Click(object sender, EventArgs e)
    {
        try
        {
            
            int count = 0;
            con_new.Open();
              zkemkeeper.CZKEMClass CtrlBioComm = new zkemkeeper.CZKEMClass();
            zkemkeeper.CZKEMClass axCZKEM1 = new zkemkeeper.CZKEMClass();
            string cmd_text = "IF EXISTS (SELECT * FROM sysobjects WHERE Name = 'current_details')" + "DROP TABLE current_details";
            SqlCommand com = new SqlCommand(cmd_text, con_new);
            com.ExecuteNonQuery();
            com = new SqlCommand("create table current_details(machine_num int,enroll_num int,Name varchar(50) ,Days varchar(20) ,VerifyMode int,InOutMode int,year int,month int,day int,hour int,min int,sec int,pn_branchid int,pn_companyid int)", con_new);
            com.ExecuteNonQuery();

            foreach (GridViewRow row in GridMachine.Rows)
            {
                CheckBox chk = (CheckBox)row.FindControl("ChkMac");
                if (chk != null & chk.Checked)
                {
                    count += 1;
                    str = ((Label)row.FindControl("lblMac")).Text;
                    str2 = ((Label)row.FindControl("lblIp")).Text;
           
                    bIsConnected = CtrlBioComm.Connect_Net(str2, 4370);
                    if (bIsConnected == true)
                    {
                        lblhidden.Text = "Connection established!!!";
                        cmd_collect_Click(this, null);
                        btn_disconnect_Click(this, null);
                    }
                    else
                    {
                        lblhidden.Text = "Cannot Create Connection!!!";
                        MacError += str + ", ";
                    }

                }
            }
            if (count == 0)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Cannot connect to Machine # " + MacError + " ');", true);
            }
            SqlCommand cmd = new SqlCommand("select * from current_details where pn_branchid='" + employee.BranchId + "' and pn_companyid='" + employee.CompanyId + "' order by month desc , day desc, min asc ", con_new);
            SqlDataAdapter ada = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            ada.Fill(ds);
            lv_fingerDetails.DataSource = ds;
            lv_fingerDetails.DataBind();
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Data Downloaded Successfully');", true);
            populate_lv();

            if (MacError != "")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('No reader selected to download');", true);
            }
        }
        catch (COMException ce)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Error : No reader found!');", true);
        }
        finally
        {
            con_new.Close();
        }
    }

    protected void btn_disconnect_Click(object sender, EventArgs e)
    {

        zkemkeeper.CZKEMClass CtrlBioComm = new zkemkeeper.CZKEMClass();
        zkemkeeper.CZKEMClass axCZKEM1 = new zkemkeeper.CZKEMClass();
        //bool beep_chk = true;
       // beep_chk = CtrlBioComm.Beep(500);
        CtrlBioComm.Disconnect();
        //CtrlBioComm.EnableDevice(1, true);
        lblhidden.Text = "Disconnected";
    }

    protected void btn_save_Click(object sender, EventArgs e)
    {
        string mno, ip, Loc;
        string res;
        mno = txtMachine.Text;
        ip = txtip.Text;
        Loc = txtlocation.Text;
        if (mno != "" || ip != "" || Loc != "")
        {
            res = Machin.MacAdd(employee.CompanyId, employee.BranchId, mno, ip, Loc);
            if (res == "1")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Saved Successfully');", true);
                txtip.Text = "";
                txtMachine.Text = "";
                txtlocation.Text = "";
                GridFill();
            }
            else if (res == "0")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Error Occured);", true);
            }
        }
        else
        {
           // ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Enter all details);", true);
            ClientScript.RegisterStartupScript(this.Page.GetType(), "alert", "alert('Enter all fields');", true);
        }
    }

    protected void btn_downtemp_Click(object sender, EventArgs e)
    {
        try
        {
            if (btn_Connecttemp.Text == "Connect")
            {
                //MessageBox.Show("Please connect the device first!", "Error");
                return;
            }
            zkemkeeper.CZKEMClass CtrlBioComm = new zkemkeeper.CZKEMClass();
            zkemkeeper.CZKEMClass axCZKEM1 = new zkemkeeper.CZKEMClass();
            string sdwEnrollNumber = "";
            string sName = "";
            string sPassword = "";
            int iPrivilege = 0;
            bool bEnabled = false;

            int idwFingerIndex;
            string sTmpData = "";
            int iTmpLength = 0;
            int iFlag = 0;
            DataSet ds = new DataSet();
            axCZKEM1.EnableDevice(iMachineNumber, false);
            axCZKEM1.ReadAllUserID(iMachineNumber);//read all the user information to the memory
            axCZKEM1.ReadAllTemplate(iMachineNumber);//read all the users' fingerprint templates to the memory
            while (axCZKEM1.SSR_GetAllUserInfo(iMachineNumber, out sdwEnrollNumber, out sName, out sPassword, out iPrivilege, out bEnabled))//get all the users' information from the memory
            {
                for (idwFingerIndex = 0; idwFingerIndex < 10; idwFingerIndex++)
                {
                    if (axCZKEM1.GetUserTmpExStr(iMachineNumber, sdwEnrollNumber, idwFingerIndex, out iFlag, out sTmpData, out iTmpLength))//get the corresponding templates string and length from the memory
                    {
                        ListItem list = new ListItem();

                        list.Text = sdwEnrollNumber;
                        ds.Tables[0].Rows[0][0] = sName;
                        ds.Tables[0].Rows[0][0] = idwFingerIndex.ToString();
                        ds.Tables[0].Rows[0][0] = sTmpData;
                        ds.Tables[0].Rows[0][0] = iPrivilege.ToString();
                        ds.Tables[0].Rows[0][0] = sPassword;

                        //list.SubItems.Add(sName);
                        //list.SubItems.Add(idwFingerIndex.ToString());
                        //list.SubItems.Add(sTmpData);
                        //list.SubItems.Add(iPrivilege.ToString());
                        //list.SubItems.Add(sPassword);
                        if (bEnabled == true)
                        {
                            ds.Tables[0].Rows[0][0] = "true";
                        }
                        else
                        {
                            ds.Tables[0].Rows[0][0] = "false";
                        }
                        ds.Tables[0].Rows[0][0] = iFlag.ToString();
                        //list.SubItems.Add(iFlag.ToString());
                        //lvDownload.Items.Add(list);
                        GvTemp.DataSource = ds.Tables[0];
                        GvTemp.DataBind();
                    }
                }
            }
            axCZKEM1.EnableDevice(iMachineNumber, true);
        }
        catch (Win32Exception we)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('"+we.ErrorCode.ToString()+"');", true);
        }
    }
    protected void btn_Connecttemp_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtiptemp.Text.Trim() == "")
            {
                //MessageBox.Show("IP and Port cannot be null", "Error");
                return;
            }
            int idwErrorCode = 0;
           zkemkeeper.CZKEMClass CtrlBioComm = new zkemkeeper.CZKEMClass();
            zkemkeeper.CZKEMClass axCZKEM1 = new zkemkeeper.CZKEMClass();
            if (btn_Connecttemp.Text == "DisConnect")
            {
                axCZKEM1.Disconnect();
                bIsConnected = false;
                btn_Connecttemp.Text = "Connect";
                btn_Connecttemp.CssClass = "btn btn-success";
                return;
            }

            bIsConnected = axCZKEM1.Connect_Net(txtiptemp.Text, 4370);
            if (bIsConnected == true)
            {
                btn_Connecttemp.CssClass = "btn btn-danger";
                btn_Connecttemp.Text = "DisConnect";
                iMachineNumber = 1;//In fact,when you are using the tcp/ip communication,this parameter will be ignored,that is any integer will all right.Here we use 1.
                axCZKEM1.RegEvent(iMachineNumber, 65535);//Here you can register the realtime events that you want to be triggered(the parameters 65535 means registering all)
            }
            else
            {
                axCZKEM1.GetLastError(ref idwErrorCode);
            }
        }
        catch (Win32Exception we)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('" + we.ErrorCode.ToString() + "');", true);
        }
    }

    protected void GridMachine_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            // loop all data rows
            foreach (DataControlFieldCell cell in e.Row.Cells)
            {
                // check all cells in one row
                foreach (Control control in cell.Controls)
                {
                    // Must use LinkButton here instead of ImageButton
                    // if you are having Links (not images) as the command button.
                    ImageButton button = control as ImageButton;
                    if (button != null && button.CommandName == "Delete")
                        // Add delete confirmation
                        button.OnClientClick = "if (!confirm('Are you sure " +"you want to delete this record?')) return;";
                }
            }
        }
    }
}
