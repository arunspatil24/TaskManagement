using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace TaskManagement
{
    public partial class WebFormUser : System.Web.UI.Page
    {
        public static string con = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CancleButton.Visible = false;
                UpdateButton.Visible = false;
                sucessLabel.Visible = false;
                Display();
            }
        }
        public void Display()
        {
            using(SqlConnection connection=new SqlConnection(con))
            {
                SqlCommand cmd = new SqlCommand("DisplayUserLogin", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                connection.Open();
                UserLoginGridView.DataSource = cmd.ExecuteReader();
                UserLoginGridView.DataBind();
            }
        }

        protected void AddButton_Click(object sender, EventArgs e)
        {
            using(SqlConnection connection = new SqlConnection(con))
            {
                SqlCommand cmd = new SqlCommand("InsertUserLogin", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UId", DBNull.Value);
                cmd.Parameters.AddWithValue("@UName", UserNameTextBox.Text);
                cmd.Parameters.AddWithValue("@UPassword", PasswordTextBox.Text);
                cmd.Parameters.AddWithValue("@RId", int.Parse(RoleIdTextBox.Text));
                connection.Open();
                int result = cmd.ExecuteNonQuery();
                if (result > 0)
                {
                    sucessLabel.Text = "Data is inserted sucessfully into the database";
                    sucessLabel.Visible = true;
                    Display();
                }
                else
                {
                    sucessLabel.Text = "Data is not inserted into the database due to some problem";
                    sucessLabel.Visible = true;
                }
            }
        }
    }
}