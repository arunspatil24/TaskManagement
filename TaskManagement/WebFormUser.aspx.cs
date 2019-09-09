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
        public static int? UId = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CancleButton.Visible = false;
                UpdateButton.Visible = false;
                sucessLabel.Visible = false;
                Display();
                DropDownBinding();
            }
           
        }

        public void DropDownBinding()
        {
            using (SqlConnection connection = new SqlConnection(con))
            {
                SqlCommand cmd = new SqlCommand("DisplayRole", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                connection.Open();
                RoleDropDownList.DataSource = cmd.ExecuteReader();
                RoleDropDownList.DataTextField = "RName";
                RoleDropDownList.DataValueField = "RId";
                RoleDropDownList.DataBind();
                ListItem list = new ListItem("--select--","");
                RoleDropDownList.Items.Insert(0, list);
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

        public void Add()
        {
            using (SqlConnection connection = new SqlConnection(con))
            {
                SqlCommand cmd = new SqlCommand("InsertUserLogin", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                
                if (UId == null)
                {
                    cmd.Parameters.AddWithValue("@UId", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@UId", UId);
                }
                cmd.Parameters.AddWithValue("@UName", UserNameTextBox.Text);
                cmd.Parameters.AddWithValue("@UPassword", PasswordTextBox.Text);
                cmd.Parameters.AddWithValue("@RId", int.Parse(RoleDropDownList.SelectedValue));
                connection.Open();
                int result = cmd.ExecuteNonQuery();
                if (result > 0)
                {
                    sucessLabel.Text = "Data is inserted sucessfully into the database";
                    sucessLabel.Visible = true;
                    Display();
                    CancleButton.Visible = false;
                    UpdateButton.Visible = false;
                    UId = null;
                    Response.Redirect(Request.Url.AbsoluteUri);
                }
                else
                {
                    sucessLabel.Text = "Data is not inserted into the database due to some problem";
                    sucessLabel.Visible = true;
                    UId = null;
                    CancleButton.Visible = false;
                    UpdateButton.Visible = false;
                }
            }
           
        }
        protected void AddButton_Click(object sender, EventArgs e)
        {
            UId = null;
            Add();
        }

        public void Clear()
        {
            UId = null;
            UserNameTextBox.Text = "";
            PasswordTextBox.Text = "";
            RoleDropDownList.SelectedValue = "-1";
        }
        protected void CancleButton_Click(object sender, EventArgs e)
        {
            CancleButton.Visible = false;
            UpdateButton.Visible = false;
            AddButton.Visible = true;
            Clear();
        }

        protected void UserLoginGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditCommand")
            {
                AddButton.Visible = false;
                CancleButton.Visible = true;
                UpdateButton.Visible = true;
                var getBtn = e.CommandSource as LinkButton;
                var clickedRow = getBtn.NamingContainer as GridViewRow;
                UId = int.Parse(clickedRow.Cells[1].Text);
                UserNameTextBox.Text = clickedRow.Cells[2].Text;
                PasswordTextBox.Text = clickedRow.Cells[3].Text;
                DropDownBinding();
                RoleDropDownList.SelectedValue =clickedRow.Cells[4].Text;
            }
            if (e.CommandName == "DeleteCommand")
            {
                var getBtn = e.CommandSource as LinkButton;
                var clickedRow = getBtn.NamingContainer as GridViewRow;
                UId = int.Parse(clickedRow.Cells[1].Text);
                using(SqlConnection connection=new SqlConnection(con))
                {
                    SqlCommand cmd = new SqlCommand("DeleteUserLogin", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UId", UId);
                    connection.Open();
                    int result = cmd.ExecuteNonQuery();
                    if (result > 0)
                    {
                        UId = null;
                        Display();
                        sucessLabel.Text = "The record is deleted sucessfully";
                        sucessLabel.Visible = true;
                    }
                    else
                    {
                        UId = null;
                        sucessLabel.Text = "The Record could't be deleted sucessfully";
                        sucessLabel.Visible = true;
                    }
                }
            }
        }

        protected void UpdateButton_Click(object sender, EventArgs e)
        {
            Add();
            AddButton.Visible = true;
        }
    }
}