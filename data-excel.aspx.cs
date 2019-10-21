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
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Collections.ObjectModel;

public partial class data_excel : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string constr = ConfigurationManager.ConnectionStrings["xls"].ConnectionString;
        OleDbConnection con = new OleDbConnection(constr);
        try
        {
            con.Open();
            OleDbCommand cmd = new OleDbCommand("select * from [Sheet1$]", con);
            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            //da.SelectCommand = cmd;
            DataSet ds = new DataSet();
            da.Fill(ds);
            GridView1.DataSource = ds.Tables[0].DefaultView;
            GridView1.DataBind();
        }
        catch (Exception exp)
        {

        }
        finally
        {
            con.Close();
        }
    }
}
