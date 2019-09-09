using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Web.Security;

namespace TaskManagement
{
    public partial class WebFormLoginPage : System.Web.UI.Page
    {
        public static string con = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void LoginButton_Click(object sender, EventArgs e)
        {
           if(Authenticated(TxtUserName.Text,TxtPassword.Text))
            {
                //Response.Redirect("WebFormClients.aspx");
                FormsAuthentication.RedirectFromLoginPage(TxtUserName.Text, false);
            }
        }

        public bool Authenticated(string UserName, string Password)
        {

            using (SqlConnection connection = new SqlConnection(con))
            {
                SqlCommand cmd = new SqlCommand("loginUser", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UName", UserName);
                cmd.Parameters.AddWithValue("@UPassword", Password);
                connection.Open();
                int result = (int)cmd.ExecuteScalar();
                return result == 1;
            }
            
        }
    }
}