using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace TaskManagement
{
    public partial class WebFormRole : System.Web.UI.Page
    {
        public static int? RId = null;
        public static string con = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                UpdateButton.Visible = false;
                CancleButton.Visible = false;
                sucessLabel.Visible = false;
                Display();
            }
        }
        public void Display()
        {
            using(SqlConnection connection= new SqlConnection(con))
            {
                SqlCommand cmd = new SqlCommand("DisplayRole", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                connection.Open();
                RoleGridView.DataSource = cmd.ExecuteReader();
                RoleGridView.DataBind();
            }
        }
        protected void AddButton_Click(object sender, EventArgs e)
        {
            using(SqlConnection connection= new SqlConnection(con))
            {
                SqlCommand cmd = new SqlCommand("InsertRole", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@RId", DBNull.Value);
                cmd.Parameters.AddWithValue("@RName", RoleNameTextBox.Text);
                connection.Open();
                int result = cmd.ExecuteNonQuery();
                if (result > 0)
                {
                    sucessLabel.Text = "Data is inserted sucessfully into the dataBase";
                    sucessLabel.Visible = true;
                    Display();
                }
                else
                {
                    sucessLabel.Text = "Data could't be inserted into the dataBase";
                    sucessLabel.Visible = true;
                }
                
            }
        }

        protected void RoleGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditCommand")
            {
                var getBtn = e.CommandSource as LinkButton;
                var clickedRow = getBtn.NamingContainer as GridViewRow;
                RoleNameTextBox.Text = clickedRow.Cells[2].Text;
                RId = int.Parse(clickedRow.Cells[1].Text);
                AddButton.Visible = false;
                CancleButton.Visible = true;
                UpdateButton.Visible = true;
            }
            if (e.CommandName == "DeleteCommand")
            {
                using(SqlConnection connection= new SqlConnection(con))
                {
                    SqlCommand cmd = new SqlCommand("DeleteRole", connection);
                    var getBtn = e.CommandSource as LinkButton;
                    var clickedRow = getBtn.NamingContainer as GridViewRow;
                    RId = int.Parse(clickedRow.Cells[1].Text);
                    cmd.Parameters.AddWithValue("@RId", RId);
                    cmd.CommandType = CommandType.StoredProcedure;
                    connection.Open();
                    int result = cmd.ExecuteNonQuery();
                    if (result > 0)
                    {
                        sucessLabel.Text = "Deleted Sucessfully";
                        sucessLabel.Visible = true;
                        RoleNameTextBox.Text = "";
                        RId = null;
                        Display();
                    }
                    else
                    {
                        sucessLabel.Text = "Could't be deleted Sucessfully";
                        sucessLabel.Visible = true;
                    }
                }
            }
        }

        protected void CancleButton_Click(object sender, EventArgs e)
        {
            RoleNameTextBox.Text = "";
            AddButton.Visible = true;
            CancleButton.Visible = false;
            UpdateButton.Visible = false;
            RId = null;
        }

        protected void UpdateButton_Click(object sender, EventArgs e)
        {
            using(SqlConnection connection=new SqlConnection(con))
            {
                SqlCommand cmd = new SqlCommand("InsertRole", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@RId", RId);
                cmd.Parameters.AddWithValue("@RName", RoleNameTextBox.Text);
                connection.Open();
                int result = cmd.ExecuteNonQuery();
                if (result > 0)
                {
                    UpdateButton.Visible = false;
                    CancleButton.Visible = false;
                    AddButton.Visible = true;
                    RoleNameTextBox.Text = "";
                    RId = null;
                    sucessLabel.Text = "Update sucessfully into the dataBase";
                    sucessLabel.Visible = true;
                    Display();
                }
                else
                {
                    UpdateButton.Visible = false;
                    CancleButton.Visible = false;
                    AddButton.Visible = true;
                    RoleNameTextBox.Text = "";
                    RId = null;
                    sucessLabel.Text = "Could't be Update sucessfully into the DataBase";
                    sucessLabel.Visible = true;
                }
            }
        }
    }
}