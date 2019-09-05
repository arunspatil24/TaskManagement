using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
namespace TaskManagement
{
    public partial class WebFormTaskDetails : System.Web.UI.Page
    {
        public static string con = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            Display();
            if (!IsPostBack)
            {
                CancleButton.Visible = false;
                UpdateButton.Visible = false;
                sucess.Visible = false;
                using(SqlConnection connection= new SqlConnection(con))
                {
                    SqlCommand cmd = new SqlCommand("DisplayClientName", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    connection.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();
                    ClientDropDownList.DataSource = rdr;
                    ClientDropDownList.DataTextField = "CName";
                    ClientDropDownList.DataValueField = "CId";
                    ClientDropDownList.DataBind();
                    ListItem list = new ListItem("--Select--", "-1");
                    ClientDropDownList.Items.Insert(0, list);
                }
            }

        }
        public void Clear()
        {
            ClientDropDownList.SelectedValue = "-1";
            ProjectDropDownList.SelectedValue = "-1";
            WorkDropDownList.SelectedValue = "-1";
            CDate.SelectedDate = DateTime.Today;
            WorkHoursTextBox.Text = "";
            WorkDetailsTextArea.Value = "";
        }

        public void Display()
        {
            using(SqlConnection connection = new SqlConnection(con))
            {
                SqlCommand cmd = new SqlCommand("DisplayTaskDetails", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                connection.Open();
                TaskDetailsGridView.DataSource = cmd.ExecuteReader();
                TaskDetailsGridView.DataBind();

            }
        }

        protected void ClientDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            using(SqlConnection connection= new SqlConnection(con))
            {
                SqlCommand cmd = new SqlCommand("DisplayProject", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CId", ClientDropDownList.SelectedValue);
                connection.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                ProjectDropDownList.DataSource = rdr;
                ProjectDropDownList.DataValueField = "PId";
                ProjectDropDownList.DataTextField = "PName";
                ProjectDropDownList.DataBind();
                ListItem list = new ListItem("--Select--", "-1");
                ProjectDropDownList.Items.Insert(0, list);
            }
        }

        protected void ProjectDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(con))
            {
                SqlCommand cmd = new SqlCommand("DisplayWork", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@PId", ProjectDropDownList.SelectedValue);
                connection.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                WorkDropDownList.DataSource = rdr;
                WorkDropDownList.DataValueField = "WId";
                WorkDropDownList.DataTextField = "Works";
                WorkDropDownList.DataBind();
                ListItem list = new ListItem("--Select--", "-1");
                WorkDropDownList.Items.Insert(0, list);
            }
        }

        protected void ADDButton_Click(object sender, EventArgs e)
        {
            using(SqlConnection connection= new SqlConnection(con))
            {
                SqlCommand cmd = new SqlCommand("InsertTaskDetails", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@TId",DBNull.Value);
                cmd.Parameters.AddWithValue("@CId",ClientDropDownList.SelectedValue);
                cmd.Parameters.AddWithValue("@PId",ProjectDropDownList.SelectedValue);
                cmd.Parameters.AddWithValue("@WId",WorkDropDownList.SelectedValue);
                cmd.Parameters.AddWithValue("@TDate",CDate.SelectedDate);
                cmd.Parameters.AddWithValue("@UserId","0");
                cmd.Parameters.AddWithValue("@THours",int.Parse(WorkHoursTextBox.Text));
                cmd.Parameters.AddWithValue("@Details",WorkDetailsTextArea.Value);
                connection.Open();
                int result = cmd.ExecuteNonQuery();
                if (result > 0)
                {
                    sucess.Text = "The data is inserted sucessfully";
                    sucess.Visible = true;
                    Clear();

                }
                else
                {
                    sucess.Text = "The data could't be uploaded to database";
                    sucess.Visible = true;
                }
               
            }
        }

        protected void TaskDetailsGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditCommand")
            {
                ADDButton.Visible = false;
                CancleButton.Visible = true;
                UpdateButton.Visible = true;
                var getBtn = e.CommandSource as LinkButton;
                var clickedRow = getBtn.NamingContainer as GridViewRow;
                ClientDropDownList.SelectedValue = clickedRow.Cells[2].Text;
                ProjectDropDownList.SelectedValue = clickedRow.Cells[3].Text;
            }
        }
    }
}