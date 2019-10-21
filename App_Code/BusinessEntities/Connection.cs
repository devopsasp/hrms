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

namespace ePayHrms.Connection
{
    /// <summary>
    /// Summary description for Connection
    /// </summary>
    public class Connection
    {
        public Connection()
        {
        }

        private SqlConnection _Con;        

        public SqlConnection fn_Connection()
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.AppSettings["Connectionstring"];                       
            return con;          
        }


    }
}