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
using ePayHrms.Connection;
using System.IO;
using ePayHrms.Company;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using ePayHrms.Student;
using ePayHrms.Employee;
using System.Globalization;
using System.Data.OleDb;

public partial class Hrms_Employee_Student_Profile : System.Web.UI.Page
{
    SqlConnection myConnection = new SqlConnection(ConfigurationManager.AppSettings["Connectionstring"]);
    SqlCommand cmd = new SqlCommand();
    Employee employee = new Employee();
    Company company = new Company();
    Student student = new Student();
    string s_login_role;
    string s_form = "";
    DataSet ds_userrights;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

            s_login_role = Request.Cookies["Login_temp_Role"].Value;

            if (s_login_role != "e")
            {

                

                student.CompanyId = Convert.ToInt32(Request.Cookies["Login_temp_CompanyID"].Value);
                student.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);

                if (!IsPostBack)
                {
                    switch (s_login_role)
                    {
                        case "a":
                            student.BranchId = Convert.ToInt32(Request.Cookies["preview_BranchID"].Value);
                            admin();
                            break;

                        case "h":
                            student.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
                            hr();
                            break;

                        case "e":
                            student.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
                            hr();
                            break;

                        case "u": s_form = "21";

                            ds_userrights = company.check_Userrights((int)Session["Login_temp_EmployeeID"], s_form);

                            if (ds_userrights.Tables[0].Rows.Count > 0)
                            {
                                student.BranchId = Convert.ToInt32(Request.Cookies["Login_temp_BranchID"].Value);
                                student.StudentId = Convert.ToInt32(Request.Cookies["preview_EmployeeID"].Value);
                            }
                            else
                            {
                                Response.Cookies["Msg_Session"].Value=  "Permission Restricted. Please Contact Administrator";
                                Response.Redirect("Employee_Preview.aspx");
                            }

                            break;

                        default: Response.Cookies["Msg_Session"].Value=  "Permission Restricted. Please Contact Administrator";
                            Response.Redirect("~/Company_Home.aspx");
                            break;
                    }
                }
            }
            else
            {
                Session["emp_menu"] = 0;
                Response.Redirect("~/Company_Home.aspx");
            }

        }
        catch (Exception ex)
        {
            Response.Cookies["Msg_Session"].Value=  "Error Occurred";
            Response.Redirect("~/Company_Home.aspx");
        }
    }

    public void ddl_year_load(DropDownList ddl)
    {
        try
        {
            int cur_yr = DateTime.Now.Year;
            ddl.Items.Clear();
            for (int yr_it = cur_yr - 5; yr_it <= cur_yr + 1; yr_it++)
            {
                ddl.Items.Add(Convert.ToString(yr_it));
            }
            ddl.SelectedItem.Text = cur_yr.ToString();
        }

        catch (Exception ex)
        {
            //lbl_error.Text = "Error";
        }
    }

    public void ddl_year()
    {
        try
        {
            int cur_yr = DateTime.Now.Year;
            txtAcademicYear.Items.Clear();
            for (int yr_it = 2010; yr_it <= cur_yr + 1; yr_it++)
            {
                txtAcademicYear.Items.Add(Convert.ToString(yr_it));
            }
            txtAcademicYear.SelectedItem.Text = DateTime.Now.Year.ToString();
        }

        catch (Exception ex)
        {
            //lbl_error.Text = "Error";
        }
    }

    public void hr()
    {
        ddl_year();
        txtCountry.DataSource = CountryList();
        txtCountry.DataBind();
        txtCountry.SelectedIndex = 46;
        ddl_Student.Items.Clear();
        ddl_year_load(ddl_Year);
        student.AcademicYear = Convert.ToInt32(ddl_Year.SelectedItem.Text);
        Collection<Student> StudentList = student.fn_getStudentList(student);
        if (StudentList.Count > 0)
        {

            for (int ddl_i = -1; ddl_i < StudentList.Count; ddl_i++)
            {

                if (ddl_i == -1)
                {
                    ListItem e_list = new ListItem();

                    e_list.Text = "Select Student";
                    e_list.Value = "se";
                    ddl_Student.Items.Add(e_list);
                }
                else
                {
                    ListItem e_list = new ListItem();
                    e_list.Value = StudentList[ddl_i].RegisterNo.ToString();
                    e_list.Text = StudentList[ddl_i].FullName.ToString();
                    ddl_Student.Items.Add(e_list);
                }
            }
        }
        else
        {
            ListItem e_list = new ListItem();

            e_list.Text = "No Students Available";
            e_list.Value = "se";
            ddl_Student.Items.Add(e_list);

        }
    }

    public void admin()
    {

    }

    public void load()
    {

    }


    public static List<string> CountryList()
    {
        //Creating list
        List<string> CultureList = new List<string>();

        //getting  the specific  CultureInfo from CultureInfo class
        CultureInfo[] getCultureInfo = CultureInfo.GetCultures(CultureTypes.SpecificCultures);

        foreach (CultureInfo getCulture in getCultureInfo)
        {
            //creating the object of RegionInfo class
            RegionInfo GetRegionInfo = new RegionInfo(getCulture.LCID);
            //adding each county Name into the arraylist
            if (!(CultureList.Contains(GetRegionInfo.EnglishName)))
            {
                CultureList.Add(GetRegionInfo.EnglishName);
            }
        }
        //sorting array by using sort method to get countries in order
        CultureList.Sort();
        //returning country list
        return CultureList;
    }

    protected void ddl_Year_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddl_Student.Items.Clear();
        student.AcademicYear = Convert.ToInt32(ddl_Year.SelectedItem.Text);
        Collection<Student> StudentList = student.fn_getStudentList(student);
        if (StudentList.Count > 0)
        {
            for (int ddl_i = -1; ddl_i < StudentList.Count; ddl_i++)
            {

                if (ddl_i == -1)
                {
                    ListItem e_list = new ListItem();

                    e_list.Text = "Select Student";
                    e_list.Value = "se";
                    ddl_Student.Items.Add(e_list);
                }
                else
                {
                    ListItem e_list = new ListItem();
                    e_list.Value = StudentList[ddl_i].RegisterNo.ToString();
                    e_list.Text = StudentList[ddl_i].FullName.ToString();
                    ddl_Student.Items.Add(e_list);
                }
            }
        }
        else
        {
            ListItem e_list = new ListItem();
            e_list.Text = "No Students Available";
            e_list.Value = "se";
            ddl_Student.Items.Add(e_list);
        }
    }

    public void load_student()
    {
        ddl_Student.Items.Clear();
        student.AcademicYear = Convert.ToInt32(ddl_Year.SelectedItem.Text);
        Collection<Student> StudentList = student.fn_getStudentList(student);
        if (StudentList.Count > 0)
        {
            for (int ddl_i = -1; ddl_i < StudentList.Count; ddl_i++)
            {

                if (ddl_i == -1)
                {
                    ListItem e_list = new ListItem();

                    e_list.Text = "Select Student";
                    e_list.Value = "se";
                    ddl_Student.Items.Add(e_list);
                }
                else
                {
                    ListItem e_list = new ListItem();
                    e_list.Value = StudentList[ddl_i].RegisterNo.ToString();
                    e_list.Text = StudentList[ddl_i].FullName.ToString();
                    ddl_Student.Items.Add(e_list);
                }
            }
        }
        else
        {
            ListItem e_list = new ListItem();
            e_list.Text = "No Students Available";
            e_list.Value = "se";
            ddl_Student.Items.Add(e_list);
        }
    }
    protected void btn_save_Click(object sender, EventArgs e)
    {
        if (txtStudentName.Value == "" || txtRegisterNo.Value == "" || txtRollNo.Value == "" || txtDOB.Text == "" || txtAdmissionDate.Text == "" || txtContact.Value == "" || txtContact.Value == "" || txtClassName.SelectedItem.Text == "Select" || txtDepartment.SelectedItem.Text == "Select" || txtReaderID.Value == "")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Enter All mandatory Field!');", true);
            return;
        }
        string _Value = "";        
        save();
        ImageSave();
        _Value = student.Student_Details(student);
        if (_Value != "1")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Added Successfully!');", true);
            Clear();
            load_student();
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Error Occured!');", true);
        }            
    }

    public void ImageSave()
    {
        if (Img_Upload.HasFile)
        {
            string fileName = Path.GetFileName(Img_Upload.PostedFile.FileName);
            Img_Upload.PostedFile.SaveAs(Server.MapPath("~/Images/Students/") + fileName);
            //Response.Redirect(Request.Url.AbsoluteUri);
        }
    }
    public void save()
    {        
        string gender = "";
        student.RollNo = txtRollNo.Value;
        student.RegisterNo = txtRegisterNo.Value;
        student.FirstName = txtStudentName.Value;
        student.DOB = Convert.ToDateTime(txtDOB.Text);        
        gender = rbGender.SelectedItem.Text;
        student.Gender = gender;
        student.AdmissionDate = Convert.ToDateTime(txtAdmissionDate.Text);
        student.ReaderId = Convert.ToInt32(txtReaderID.Value);
        student.AcademicYear = Convert.ToInt32(txtAcademicYear.SelectedItem.Text);
        student.CourseName = txtClassName.SelectedItem.Text;
        student.DepartmentName = txtDepartment.SelectedItem.Text;
        student.Section = txtSection.SelectedItem.Text;
        student.Cyear = ddl_CurrentYear.SelectedItem.Text;
        student.Phone = txtContact.Value;
        student.Residence = txtResidence.SelectedItem.Text;
        student.BloodGroup = txtBloodGroup.SelectedItem.Text;
        student.EmailId = txtEmailID.Value;
        student.Incharge_name = txtFaculty.Value;
        student.Language = txtLanguage.Value;
        student.AdmissionType = txtAdmissiontype.Value;
        student.Amount = Convert.ToInt32(txtInstitution.Value);
        student.Community = txtCommunity.Value;
        student.Religion = txtReligion.Value;
        student.Nationality = txtNationality.Value;
        student.Boarding_point = txtBusDetail.Value;
        student.Bank_Code = txtBankAC.Value;
        student.FatherName = txtFather.Value;
        student.MotherName = txtMother.Value;
        student.AddressLine1 = txtAddress1.Text;
        student.AddressLine2 = txtAddress2.Text;
        student.City = txtCity.Value;
        student.State = txtState.Value;
        student.District = txtDistrict.Value;
        student.Country = txtCountry.SelectedItem.Text;
        student.ph_Residence = txtParentsContact.Value;
        student.status = Convert.ToChar(ddlStatus.Text);
    }
    protected void btn_reset_Click(object sender, EventArgs e)
    {
        Clear();
        txtQuick.Value = "";
    }
    public void Clear()
    {
        txtStudentName.Value = "";
        txtRegisterNo.Value = "";
        txtRollNo.Value = "";
        txtDOB.Text = "";
        rbGender.SelectedIndex = 0;
        txtAdmissionDate.Text = "";
        txtAcademicYear.SelectedIndex = 0;
        txtClassName.SelectedIndex = 0;
        ddl_CurrentYear.SelectedIndex = 0;
        txtDepartment.SelectedIndex = 0;
        txtSection.SelectedIndex = 0;
        txtResidence.SelectedIndex = 0;
        txtContact.Value = "";
        txtBloodGroup.SelectedIndex = 0;
        txtEmailID.Value = "";
        txtFaculty.Value = "";
        txtAdmissiontype.Value = "";
        txtInstitution.Value = "";
        txtCommunity.Value = "";
        txtReligion.Value = "";
        txtNationality.Value = "";
        txtBusDetail.Value = "";
        txtReaderID.Value = "";
        txtBankAC.Value = "";
        txtFather.Value = "";
        txtMother.Value = "";
        txtLanguage.Value = "";
        txtAddress1.Text = "";
        txtAddress2.Text = "";
        txtCity.Value = "";
        txtState.Value = "";
        txtDistrict.Value = "";
        txtCountry.SelectedIndex = 0;
        txtParentsContact.Value = "";
        ddlStatus.SelectedIndex = 0;
    }

    protected void btn_import_Click(object sender, EventArgs e)
    {
        if (FU_Excel.HasFile)
        {
            string excelPath = Server.MapPath("~/Files/") + Path.GetFileName(FU_Excel.PostedFile.FileName);
            FU_Excel.SaveAs(excelPath);

            string conString = string.Empty;
            string extension = Path.GetExtension(FU_Excel.PostedFile.FileName);
            switch (extension)
            {
                case ".xls": //Excel 97-03
                    conString = ConfigurationManager.ConnectionStrings["Excel03ConString"].ConnectionString;
                    break;

                case ".xlsx": //Excel 07 or higher
                    conString = ConfigurationManager.ConnectionStrings["Excel07+ConString"].ConnectionString;
                    break;

                default :
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Only Excel files are allowed to upload');", true);
                    return;
                    break;

            }

            try
            {
                conString = string.Format(conString, excelPath);
                using (OleDbConnection excel_con = new OleDbConnection(conString))
                {
                    excel_con.Open();
                    string sheet1 = excel_con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null).Rows[0]["TABLE_NAME"].ToString();
                    DataTable dtExcelData = new DataTable();

                    //[OPTIONAL]: It is recommended as otherwise the data will be considered as String by default.
                    dtExcelData.Columns.AddRange(new DataColumn[38] { new DataColumn("pn_CompanyID", typeof(int)),
                new DataColumn("pn_BranchID", typeof(int)),
                new DataColumn("RollNo", typeof(string)),
                new DataColumn("RegisterNo", typeof(string)),
                new DataColumn("StudentName", typeof(string)),
                new DataColumn("DateofBirth", typeof(DateTime)),
                new DataColumn("Gender",typeof(string)),
                new DataColumn("AdmissionDate",typeof(DateTime)),
                new DataColumn("Readerid",typeof(int)),
                new DataColumn("AcademicYear",typeof(int)),
                new DataColumn("ClassName", typeof(string)),
                new DataColumn("Department", typeof(string)),
                new DataColumn("Section", typeof(string)),
                new DataColumn("CurrentYear", typeof(string)),
                new DataColumn("Phone", typeof(string)),
                new DataColumn("Place", typeof(string)),
                new DataColumn("BloodGroup", typeof(string)),
                new DataColumn("Emailid", typeof(string)),
                new DataColumn("Facultyadvisor", typeof(string)),
                new DataColumn("MotherTongue", typeof(string)),
                new DataColumn("AdmissionType", typeof(string)),
                new DataColumn("Institutionfees", typeof(int)),
                new DataColumn("Community", typeof(string)),
                new DataColumn("Religion", typeof(string)),
                new DataColumn("Nationality", typeof(string)),
                new DataColumn("Busdetails", typeof(string)),
                new DataColumn("BankAC", typeof(string)),
                new DataColumn("FatherName", typeof(string)),
                new DataColumn("MotherName", typeof(string)),
                new DataColumn("Address1", typeof(string)),
                new DataColumn("Address2", typeof(string)),
                new DataColumn("City", typeof(string)),
                new DataColumn("State", typeof(string)),
                new DataColumn("District", typeof(string)),
                new DataColumn("Country", typeof(string)),
                new DataColumn("ParentsContact", typeof(string)),
                new DataColumn("Status", typeof(char)),
                new DataColumn("Photo", typeof(string))

                 });

                    using (OleDbDataAdapter oda = new OleDbDataAdapter("SELECT * FROM [" + sheet1 + "]", excel_con))
                    {
                        oda.Fill(dtExcelData);
                    }
                    excel_con.Close();

                    string consString = ConfigurationManager.ConnectionStrings["connectionstring"].ConnectionString;
                    using (SqlConnection con = new SqlConnection(consString))
                    {
                        using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))
                        {
                            //Set the database table name
                            sqlBulkCopy.DestinationTableName = "dbo.paym_student";
                            //[OPTIONAL]: Map the Excel columns with that of the database table
                            sqlBulkCopy.ColumnMappings.Add("CollegeID", "pn_CompanyID");
                            sqlBulkCopy.ColumnMappings.Add("BranchID", "pn_BranchID");
                            sqlBulkCopy.ColumnMappings.Add("StudentRollNo", "RollNo");
                            sqlBulkCopy.ColumnMappings.Add("RegisterNo", "RegisterNo");
                            sqlBulkCopy.ColumnMappings.Add("StudentName", "StudentName");
                            sqlBulkCopy.ColumnMappings.Add("DOB", "DateofBirth");
                            sqlBulkCopy.ColumnMappings.Add("Gender", "Gender");
                            sqlBulkCopy.ColumnMappings.Add("DateofAdmission", "AdmissionDate");
                            sqlBulkCopy.ColumnMappings.Add("ReaderID", "Readerid");
                            sqlBulkCopy.ColumnMappings.Add("AcademicYear", "AcademicYear");
                            sqlBulkCopy.ColumnMappings.Add("ClassName", "ClassName");
                            sqlBulkCopy.ColumnMappings.Add("Department", "Department");
                            sqlBulkCopy.ColumnMappings.Add("Section", "Section");
                            sqlBulkCopy.ColumnMappings.Add("ActiveYear", "CurrentYear");
                            sqlBulkCopy.ColumnMappings.Add("PhoneNo", "Phone");
                            sqlBulkCopy.ColumnMappings.Add("Home/Hostel", "Place");
                            sqlBulkCopy.ColumnMappings.Add("BloodGroup", "BloodGroup");
                            sqlBulkCopy.ColumnMappings.Add("Emailid", "EmailID");
                            sqlBulkCopy.ColumnMappings.Add("Facultyadvisor", "FacultyAdvisor");
                            sqlBulkCopy.ColumnMappings.Add("MotherTongue", "MotherTongue");
                            sqlBulkCopy.ColumnMappings.Add("AdmissionType", "AdmissionType");
                            sqlBulkCopy.ColumnMappings.Add("Institutionfees", "InstitutionFee");
                            sqlBulkCopy.ColumnMappings.Add("Community", "Community");
                            sqlBulkCopy.ColumnMappings.Add("Religion", "Religion");
                            sqlBulkCopy.ColumnMappings.Add("Nationality", "Nationality");
                            sqlBulkCopy.ColumnMappings.Add("StudentBusdetails", "BusDetail");
                            sqlBulkCopy.ColumnMappings.Add("BankAccount", "BankAC");
                            sqlBulkCopy.ColumnMappings.Add("FatherName", "FatherName");
                            sqlBulkCopy.ColumnMappings.Add("MotherName", "MotherName");
                            sqlBulkCopy.ColumnMappings.Add("Address1", "Address1");
                            sqlBulkCopy.ColumnMappings.Add("Address2", "Address2");
                            sqlBulkCopy.ColumnMappings.Add("City", "City");
                            sqlBulkCopy.ColumnMappings.Add("State", "State");
                            sqlBulkCopy.ColumnMappings.Add("District", "District");
                            sqlBulkCopy.ColumnMappings.Add("Country", "Country");
                            sqlBulkCopy.ColumnMappings.Add("ParentsContact", "ParentsContact");
                            sqlBulkCopy.ColumnMappings.Add("Status", "Status");
                            sqlBulkCopy.ColumnMappings.Add("Photo", "Photo");
                            con.Open();
                            sqlBulkCopy.WriteToServer(dtExcelData);
                            con.Close();
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Data Imported Successfully!');", true);
                        }
                    }
                }
            }
            catch
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Excel file is not in the correct format');", true);
            }
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Choose Excel file to upload!');", true);
        }
    }
    protected void ddl_Student_SelectedIndexChanged(object sender, EventArgs e)
    {
        Get_StudentInfo(ddl_Student.SelectedValue);
    }

    public void Get_StudentInfo(string reg)
    {
        string gd = "";
        student.AcademicYear = Convert.ToInt32(ddl_Year.SelectedItem.Text);
        student.RegisterNo = reg;
        Collection<Student> StudentList = student.fn_getStudent(student);
        if (StudentList.Count > 0)
        {
            txtStudentName.Value = StudentList[0].FirstName;
            txtRegisterNo.Value = StudentList[0].RegisterNo;
            txtRollNo.Value = StudentList[0].RollNo;
            if (StudentList[0].DOB.ToShortDateString() != "01/01/1900")
            {
                txtDOB.Text = Convert.ToString(StudentList[0].DOB.ToShortDateString());
            }
            gd = StudentList[0].Gender;
            if (gd == "Male")
            {
                rbGender.SelectedIndex = 0;
            }
            else if (gd == "Female")
            {
                rbGender.SelectedIndex = 1;
            }
            else
            {
                rbGender.SelectedIndex = 2;
            }
            if (StudentList[0].AdmissionDate.ToShortDateString() != "01/01/1900")
            {
                txtAdmissionDate.Text = Convert.ToString(StudentList[0].AdmissionDate.ToShortDateString());
            }
            if (StudentList[0].AcademicYear != 0)
            {
                txtAcademicYear.SelectedItem.Text = Convert.ToString(StudentList[0].AcademicYear);
            }
            txtClassName.SelectedValue = StudentList[0].CourseName;
            txtDepartment.SelectedValue = StudentList[0].DepartmentName;
            txtSection.SelectedValue = StudentList[0].Section;
            ddl_CurrentYear.SelectedValue = StudentList[0].Cyear;
            txtContact.Value = StudentList[0].Phone;
            txtResidence.SelectedValue = StudentList[0].Residence;
            txtBloodGroup.SelectedValue = StudentList[0].BloodGroup;
            txtEmailID.Value = StudentList[0].EmailId;
            txtFaculty.Value = StudentList[0].Incharge_name;
            txtAdmissiontype.Value = StudentList[0].AdmissionType;
            if (StudentList[0].Amount != 0)
            {
                txtInstitution.Value = Convert.ToString(StudentList[0].Amount);
            }
            txtCommunity.Value = StudentList[0].Community;
            txtReligion.Value = StudentList[0].Religion;
            txtNationality.Value = StudentList[0].Nationality;
            txtBusDetail.Value = StudentList[0].Boarding_point;
            if (StudentList[0].ReaderId != 0)
            {
                txtReaderID.Value = Convert.ToString(StudentList[0].ReaderId);
            }
            txtBankAC.Value = StudentList[0].Bank_Code;
            txtFather.Value = StudentList[0].FatherName;
            txtMother.Value = StudentList[0].MotherName;
            txtLanguage.Value = StudentList[0].Language;
            txtAddress1.Text = StudentList[0].AddressLine1;
            txtAddress2.Text = StudentList[0].AddressLine2;
            txtCity.Value = StudentList[0].City;
            txtState.Value = StudentList[0].State;
            txtDistrict.Value = StudentList[0].District;
            txtCountry.SelectedItem.Text = StudentList[0].Country;
            txtParentsContact.Value = StudentList[0].ph_Residence;
            if (StudentList[0].status21 == "Y")
            {
                ddlStatus.SelectedIndex = 0;
            }
            else
            {
                ddlStatus.SelectedIndex = 1;
            }
            Img_Student.ImageUrl = "~/Images/Students/" + student.RegisterNo + ".jpg";
        }
        else
        {
            Clear();
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('No Student Available');", true);
        }
    }
    protected void btn_info_Click(object sender, EventArgs e)
    {
        Get_StudentInfo(txtQuick.Value);
    }
}