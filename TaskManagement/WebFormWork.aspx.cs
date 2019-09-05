using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace TaskManagement.Pages
{

    public partial class WebFormWork : System.Web.UI.Page
    {
        bool valdate = false;
        public static int? WId = null;
        public static string con = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Cancel.Visible = false;
                Sucess.Visible = false;
                Update.Visible = false;
                Display();
                SqlConnection connection = new SqlConnection(con);
                using (SqlCommand cmd = new SqlCommand("DisplayClientName", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    connection.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();
                    ClientNameDropDown.DataSource = rdr;
                    ClientNameDropDown.DataTextField = "CName";
                    ClientNameDropDown.DataValueField = "CId";
                    ClientNameDropDown.DataBind();
                    ListItem list = new ListItem("--Select--", "-1");
                    ClientNameDropDown.Items.Insert(0, list);
                    connection.Close();
                }
            }
        }
        public void Display()
        {
            using (SqlConnection connection = new SqlConnection(con))
            {
                SqlCommand cmd2 = new SqlCommand("DisplayWork", connection);
                cmd2.CommandType = CommandType.StoredProcedure;
                cmd2.Parameters.AddWithValue("@PId", DBNull.Value);
                connection.Open();
                WorkGrid.DataSource = cmd2.ExecuteReader();
                WorkGrid.DataBind();
            }
        }

        protected void ClientNameDropDown_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(con);
            using (SqlCommand cmd = new SqlCommand("DisplayProject", connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CId", ClientNameDropDown.SelectedValue);
                connection.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                ProjectNameDropDown.DataSource = rdr;
                ProjectNameDropDown.DataTextField = "PName";
                ProjectNameDropDown.DataValueField = "PId";
                ProjectNameDropDown.DataBind();
                ListItem list = new ListItem("--Select--", "-1");
                ProjectNameDropDown.Items.Insert(0, list);
            }
        }

        public bool Valdation()
        {
            if (ClientNameDropDown.SelectedValue == "-1")
            {
                Sucess.Text = "Please Select the client Name ";
                Sucess.Visible = true;
                valdate = false;
                return valdate;
            }
            else
            {
                valdate = true;
            }

            if (ProjectNameDropDown.SelectedValue == "-1")
            {
                Sucess.Text = "Please Select the Project Name ";
                valdate = false;
                Sucess.Visible = true;
                return valdate;
            }
            else
            {
                valdate = true;
            }

            return valdate;

        }
        protected void AddButton_Click(object sender, EventArgs e)
        {
            if (Valdation())
            {
                using (SqlConnection connection = new SqlConnection(con))
                {
                    SqlCommand cmd = new SqlCommand("InsertWork", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    connection.Open();
                    cmd.Parameters.AddWithValue("@PId", ProjectNameDropDown.SelectedValue);
                    cmd.Parameters.AddWithValue("@CId", ClientNameDropDown.SelectedValue);
                    cmd.Parameters.AddWithValue("@WId", DBNull.Value);
                    cmd.Parameters.AddWithValue("@Works", Work.Text);
                    int result = cmd.ExecuteNonQuery();
                    if (result > 0)
                    {
                        Sucess.Text = "The data is sucessfully inserted into the table";
                        Sucess.Visible = true;
                        ClientNameDropDown.SelectedValue = "-1";
                        ProjectNameDropDown.SelectedValue = "-1";
                        Work.Text = "";
                        Display();
                    }
                    else
                    {
                        Sucess.Text = "The Data cannot be inserted into the table";
                        Sucess.Visible = true;
                    }
                }
            }
        }

        protected void Cancel_Click(object sender, EventArgs e)
        {
            ClientNameDropDown.SelectedValue = "-1";
            ProjectNameDropDown.SelectedValue = "-1";
            Work.Text = "";
            Cancel.Visible = false;
            Update.Visible = false;
            AddButton.Visible = true;
        }

        protected void WorkGrid_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if(e.CommandName== "EditWork")
            {
                AddButton.Visible = false;
                Cancel.Visible = true;
                Update.Visible = true;
                var getBtn = e.CommandSource as LinkButton;
                var clickedRow = getBtn.NamingContainer as GridViewRow;
                ClientNameDropDown.SelectedValue = clickedRow.Cells[4].Text;
                ProjectNameDropDown.SelectedValue = clickedRow.Cells[3].Text;
                WId = int.Parse(clickedRow.Cells[1].Text);
                Work.Text = clickedRow.Cells[2].Text;

            }
            if(e.CommandName== "DeleteWork")
            {
                var getBtn = e.CommandSource as LinkButton;
                var clickedRow = getBtn.NamingContainer as GridViewRow;
                WId = int.Parse(clickedRow.Cells[1].Text);
                using(SqlConnection connection=new SqlConnection(con))
                {
                    SqlCommand cmd = new SqlCommand("DeleteWork", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@WId", WId);
                    connection.Open();
                    int result = cmd.ExecuteNonQuery();
                    if (result > 0)
                    {
                        Sucess.Text = "The row is Deleted sucessfully";
                        Sucess.Visible = true;
                        WId = null;
                    }
                    else
                    {
                        Sucess.Text = "The row Could't be deleted ";
                        Sucess.Visible = false;
                    }
                }
                Display();
                AddButton.Visible = true;
            }
        }

        protected void Update_Click(object sender, EventArgs e)
        {
            using(SqlConnection connection= new SqlConnection(con))
            {
                SqlCommand cmd = new SqlCommand("InsertWork", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@PId",ProjectNameDropDown.SelectedValue);
                cmd.Parameters.AddWithValue("@CId",ClientNameDropDown.SelectedValue);
                cmd.Parameters.AddWithValue("@WId",WId);
                cmd.Parameters.AddWithValue("@Works",Work.Text);
                connection.Open();
                int result = cmd.ExecuteNonQuery();
                if (result > 0)
                {
                    Sucess.Text = "The record is updated sucessfully";
                    Sucess.Visible = true;
                    ProjectNameDropDown.SelectedValue = "-1";
                    ClientNameDropDown.SelectedValue = "-1";
                    Work.Text = "";
                    WId = null;
                    AddButton.Visible = true;
                }
                else
                {
                    Sucess.Text = "The record Could't be updated";
                    Sucess.Visible = false;
                }
            }
            
            Display();
        }
    }
}