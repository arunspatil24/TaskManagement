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
        public static int? TId = null;
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
            DateTextBox.Text = "";
            TId = null;
        }

        public void ClientDropDown()
        {
            using (SqlConnection connection = new SqlConnection(con))
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

        public void ProjectDropDown()
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
            ClientDropDown();
        }

        protected void ProjectDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            ProjectDropDown();
        }
        public void Add()
        {
            using (SqlConnection connection = new SqlConnection(con))
            {
                SqlCommand cmd = new SqlCommand("InsertTaskDetails", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                if (TId == null)
                {
                    cmd.Parameters.AddWithValue("@TId", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@TId", TId);
                }
                cmd.Parameters.AddWithValue("@CId", ClientDropDownList.SelectedValue);
                cmd.Parameters.AddWithValue("@PId", ProjectDropDownList.SelectedValue);
                cmd.Parameters.AddWithValue("@WId", WorkDropDownList.SelectedValue);
                cmd.Parameters.AddWithValue("@TDate", CDate.SelectedDate);
                cmd.Parameters.AddWithValue("@UserId", "0");
                cmd.Parameters.AddWithValue("@THours", int.Parse(WorkHoursTextBox.Text));
                cmd.Parameters.AddWithValue("@Details", WorkDetailsTextArea.Value);
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
        protected void ADDButton_Click(object sender, EventArgs e)
        {
            TId = null;
            Add();
            Display();
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
                ClientDropDown();
                ProjectDropDownList.SelectedValue = clickedRow.Cells[3].Text;
                ProjectDropDown();
                WorkDropDownList.SelectedValue = clickedRow.Cells[4].Text;
                DateTextBox.Text = clickedRow.Cells[5].Text;
                WorkHoursTextBox.Text = clickedRow.Cells[7].Text;
                WorkDetailsTextArea.InnerText = clickedRow.Cells[8].Text;
                TId = int.Parse(clickedRow.Cells[1].Text);
            }
            if (e.CommandName == "DeleteCommand")
            {
                var getBtn = e.CommandSource as LinkButton;
                var clickedRow = getBtn.NamingContainer as GridViewRow;
                TId = int.Parse(clickedRow.Cells[1].Text);
                using(SqlConnection connection=new SqlConnection(con))
                {
                    SqlCommand cmd = new SqlCommand("DeleteTaskDetails", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@TId", TId);
                    int result = cmd.ExecuteNonQuery();
                    if (result > 0)
                    {
                        sucess.Text = "Data deleted sucessfully";
                        sucess.Visible = true;
                        Display();
                        TId = null;
                    }
                    else
                    {
                        sucess.Text = "Data could't be deleted sucessfully";
                        sucess.Visible = true;
                        TId = null;
                    }
                }
            }
        }

        protected void CDate_SelectionChanged(object sender, EventArgs e)
        {
            DateTextBox.Text = CDate.SelectedDate.ToShortDateString();
        }

        protected void UpdateButton_Click(object sender, EventArgs e)
        {
            Add();
            CancleButton.Visible = false;
            UpdateButton.Visible = false;
            ADDButton.Visible = true;
            Display();
        }

        protected void CancleButton_Click(object sender, EventArgs e)
        {
            Clear();
            ADDButton.Visible = true;
            CancleButton.Visible = false;
            UpdateButton.Visible = false;
        }
    }
}