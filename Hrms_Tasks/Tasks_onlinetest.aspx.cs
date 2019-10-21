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
using ePayHrms.Login;
using ePayHrms.Connection;
using ePayHrms.Candidate;
using System.IO;
using ePayHrms.Company;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using ePayHrms.BE.Recruitment;
using ePayHrms.Employee;
using System.Data.SqlClient;

public partial class Hrms_Tasks_Default : System.Web.UI.Page
{
    Company company = new Company();
    Employee employee = new Employee();

    Be_Recruitment r = new Be_Recruitment();
    PayRoll pay = new PayRoll();
    Candidate c = new Candidate();

    Collection<Candidate> WorkHistoryList;
    string eid;
    Collection<Company> CompanyList;


    string s_login_role;
    int ddl_i, grk;
    string _path, _Value;
    string s_form = "";
    DataSet ds_userrights;

    #region Properties
    private int Id { get; set; } //this will help you know on each databind event what question to extract from the database. 
    //no stop is required as the event is triggers as many times as the number of questions, assured by the datasourse which has as many values as questions
    #endregion Properties


    protected void Page_Load(object sender, EventArgs e)
    {
        
        results.Visible = false;
        ImageButton1.Visible = false;
        employee.CompanyId = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
        employee.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
        employee.EmployeeId = Convert.ToInt32(Request.Cookies["Login_temp_EmployeeID"].Value);
        pay.CompanyId = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
         pay.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
        c.CompanyID = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
        c.BranchID = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
        s_login_role = Request.Cookies["Login_temp_Role"].Value;
        Label8.Text = (string)Session["Login_Name"] + "!";
        Id = 1;

        if (!IsPostBack)
        {
            CompanyList = company.fn_getCompany();

            if (CompanyList.Count > 0)
            {

                switch (s_login_role)
                {

                    case "a":
                        // hr();
                        break;

                    case "e":

                        break;

                    case "h": Response.Redirect("../Hrms_Employee/Employee_Preview.aspx");
                        break;

                    case "u":
                        s_form = "46";

                        ds_userrights = company.check_Userrights((int)Session["Login_temp_EmployeeID"], s_form);

                        if (ds_userrights.Tables[0].Rows.Count > 0)
                        {
                            //hr();

                        }
                        else
                        {
                            Response.Cookies["Msg_Session"].Value=  "Permission Restricted. Please Contact Administrator.";
                            Response.Redirect("~/Company_Home.aspx");
                        }
                        break;
                    default:
                        Response.Cookies["Msg_Session"].Value=  "Permission Restricted. Please Contact Administrator";
                        Response.Redirect("../Hrms_Master/Common/Common_Home.aspx");
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

    }

    private List<int> GetQuestions(string getCondition)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Connectionstring"]);
        SqlCommand com_test = new SqlCommand();
        SqlDataReader rea_test;
        int question;
        List<int> questions = new List<int>();
        string qsetid, tot, query;
        string date = DateTime.Now.ToShortDateString();
        string sqldate = employee.Convert_ToSqlDatestring(date);
        sqldate = sqldate + " 12:00:00 AM";
        date = "2011/7/15 12:00:00 AM";
        //lbl_Error.Text = sqldate.ToString();
        con.Open();
        com_test = new SqlCommand("select * from test_assign where pn_CompanyID = '" + employee.CompanyId + "' and pn_BranchID = '" + employee.BranchId + "' and pn_QuestID = '"+txt_qsetno.Text+"' and  pn_EmployeeID = '" + employee.EmployeeId + "' and test_date = '"+ sqldate +"' ", con);
        rea_test = com_test.ExecuteReader();
        if (rea_test.Read())
        {
            qsetid = Convert.ToString(rea_test["pn_QuestID"]);
            tot = Convert.ToString(rea_test["total_quest"]);
            lbl_Error.Text = qsetid;
            //string s = "New";
           // questions = new List<int>();
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["Connectionstring"]))
            {
                connection.Open();
                query = "SELECT top "+tot+" sno FROM online where pn_questid='" + qsetid + "' and pn_BranchID = '" + employee.BranchId + "'";
                SqlCommand command = new SqlCommand(query , connection); // remember to add:  "Where condition='" + getCondition + "'"

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    question = reader.GetInt32(reader.GetOrdinal("sno")); 
                    questions.Add(question);
                }

                connection.Close();
            }

            return questions;
        }
        else
        {
            return questions;
        }
        
    }

    private Question GetQuestion(string getCondition)
    {
        Question question = new Question();
        SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Connectionstring"]);
        SqlCommand com_test = new SqlCommand();
        SqlDataReader rea_test1;
        string qsetid1, tot1, query1;
        con.Open();
        com_test = new SqlCommand("select * from test_assign where pn_CompanyID = '" + employee.CompanyId + "' and pn_BranchID = '" + employee.BranchId + "' and  pn_EmployeeID = '" + employee.EmployeeId + "' and pn_QuestID='" + txt_qsetno.Text + "' ", con);
        rea_test1 = com_test.ExecuteReader();
        if (rea_test1.Read())
        {
            qsetid1 = Convert.ToString(rea_test1["pn_QuestID"]);
            tot1 = Convert.ToString(rea_test1["total_quest"]);
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["Connectionstring"]))
            {
                connection.Open();
                query1 = "SELECT top " + tot1 + " sno, Questions, option1, option2, option3, option4, answers FROM online WHERE sno='" + Id + "' and pn_questID = '" + qsetid1 + "'";
                SqlCommand command =
                    new SqlCommand(query1, connection); // remember to add:  "Where condition='" + getCondition + "'"

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    question.QuestionId = reader.GetInt32(reader.GetOrdinal("sno"));
                    question.QuestionToPose = reader.GetString(reader.GetOrdinal("Questions"));
                    question.AnswerOne = reader.GetString(reader.GetOrdinal("option1"));
                    question.AnswerTwo = reader.GetString(reader.GetOrdinal("option2"));
                    question.AnswerThree = reader.GetString(reader.GetOrdinal("option3"));
                    question.AnswerFour = reader.GetString(reader.GetOrdinal("option4"));
                    question.CorrectAnswer = reader.GetString(reader.GetOrdinal("Answers"));
                }

                connection.Close();
            }
            return question;
        }

        else
        {
            return question;
        }
    }

    private class Question
    {
        #region Properties
        public int QuestionId { get; set; }

        public string QuestionToPose { get; set; }

        public string AnswerOne { get; set; }

        public string AnswerTwo { get; set; }

        public string AnswerThree { get; set; }

        public string AnswerFour { get; set; }

        public string CorrectAnswer { get; set; }

        #endregion Properties

        #region .ctors
        public Question()
        {
        }
        #endregion Properties
    } // class declaration


    #region Button events
    public void Row_Editing(object sender, GridViewEditEventArgs e)
    {
        ((Label)GridView1.Rows[e.NewEditIndex].FindControl("ErrorLabel")).Visible = false;
        RadioButtonList button = (RadioButtonList)GridView1.Rows[e.NewEditIndex].Cells[0].FindControl("RadioButtonList1");

        if (button.SelectedIndex >= 0) //bug fixed: user must select an item. An error is signaled when an option is not selected.Yay!
        {
            ((Label)GridView1.Rows[e.NewEditIndex].FindControl("LabelAnswer")).Visible = true;
        }
        else
        {
            ((Label)GridView1.Rows[e.NewEditIndex].FindControl("ErrorLabel")).Visible = true;
        }
        e.Cancel = true;
    }
    public void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Question question = GetQuestion("etc");

            RadioButtonList list = (RadioButtonList)e.Row.Cells[0].FindControl("RadioButtonList1");
            list.Items.Add(new ListItem(question.AnswerOne));
            list.Items.Add(new ListItem(question.AnswerTwo));
            list.Items.Add(new ListItem(question.AnswerThree));
            list.Items.Add(new ListItem(question.AnswerFour));
            list.DataBind();

            ((Label)e.Row.Cells[0].FindControl("LabelQuestion")).Text = question.QuestionId + " . " +
                                                                         question.QuestionToPose;
            ((Label)e.Row.Cells[0].FindControl("LabelAnswer")).Text = "Answer : " + question.CorrectAnswer + "";
            Id++;
            //GridView1.Columns[1].Visible = false;
            //if (e.Row.RowIndex == 1)
            //{
            //    e.Row.Cells[0].Enabled = false;
            //}
            if (list.SelectedIndex == -1)
            {
                GridView1.Columns[1].Visible = false;
            }
        }

        
    }
    #endregion Button events


    protected void btn_qsubmit_Click(object sender, EventArgs e)
    {
        GridView1.DataSource = GetQuestions("etc"); //the point opf this is to know how many questions there are as to know how many databind events to trigger.
        //small performance problem
        GridView1.DataBind();
    }

    //protected void btn_submit_Click(object sender, EventArgs e)
    //{
    //    GridView1.DataSource = GetQuestions("etc"); //the point opf this is to know how many questions there are as to know how many databind events to trigger.
    //    //small performance problem
    //    GridView1.DataBind();
    //}

    protected void txt_qsetno_TextChanged(object sender, EventArgs e)
    {
      
    }
    protected void ImageButton1_Click(object sender, EventArgs e)
    {
        try
        {
            lbl_Error.Text = "";
            lbl_error2.Text = "";
            results.Visible = true;
            int count = 0, cc, total;
            decimal percent;
            string sqlans, answers , qans="";
            SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Connectionstring"]);
            con.Open();
            SqlCommand com_ans;
            com_ans = new SqlCommand("select * from test_assign where pn_CompanyID = '" + employee.CompanyId + "' and pn_BranchID = '" + employee.BranchId + "' and  pn_EmployeeID = '" + employee.EmployeeId + "' and pn_QuestID = '"+txt_qsetno.Text+"' ", con);
            SqlDataReader rea_ans = com_ans.ExecuteReader();
            if (rea_ans.Read())
            {
                qans = Convert.ToString(rea_ans["pn_QuestID"]);
            }
            con.Close();
            SqlCommand com = new SqlCommand();
            SqlDataReader rea;
            con.Open();
            for (int c = 0; c < GridView1.Rows.Count; c++)
            {
                cc = c + 1;
                com = new SqlCommand("Select answers from online where pn_questid = '" + qans + "' and sno = " + cc + " and pn_BranchID = '" + employee.BranchId + "'", con);
                rea = com.ExecuteReader();
                answers = "";
                if (rea.Read())
                {

                    sqlans = Convert.ToString(rea["answers"]);
                    answers = ((RadioButtonList)GridView1.Rows[c].FindControl("RadioButtonList1")).SelectedItem.Text;
                    if (sqlans == answers)
                    {
                        count = count + 1;
                    }
                    rea.Close();

                }
                else
                {
                    lbl_Error.Text = "Error";
                    results.Visible = false;
                    rea.Close();
                }
            }
            lbl_quest.Text = GridView1.Rows.Count.ToString();
            lbl_ans.Text = count.ToString();
            total = GridView1.Rows.Count;
            percent = (count * 100) / total;
            lbl_percent.Text = percent.ToString();
            //lbl_Error.Text = count.ToString();
            GridView1.Columns[1].Visible = true;
            if (count < 22)
            {
                Response.Write("<script language='javascript'>alert('Sorry.. You have scored a least mark. Better luck next time.');</script>");
            }
            else
            {
                Response.Write("<script language='javascript'>alert('Congratulations! You are advanced to the next level.');</script>");
            }
            
        }
        catch (Exception ex)
        {
            results.Visible = false;
            lbl_Error.Text = "Please select the answer for all the questions";
            lbl_error2.Text = "Please select the answer for all the questions";
            ImageButton1.Visible = true;
        }
            
    }
    protected void ImageButton2_Click(object sender, EventArgs e)
    {
        GridView1.DataSource = GetQuestions("etc"); //the point of this is to know how many questions there are as to know how many databind events to trigger.
        //small performance problem
        GridView1.DataBind();
        ImageButton1.Visible = true;
        
    }
}
