using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace TaskManagement.Pages
{
    public partial class WebFormProject : System.Web.UI.Page
    {
        public static string con = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;
        public static int? clientValueField = null;
        public static int?  ProjectId = null;
        bool valdate;
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                HttpCookie receive = Request.Cookies["ckName"];
                if (receive !=null && receive.Values["ClientName"]!=null && receive.Values["ClientId"]!= null)
                {
                    Label3.Text = receive.Values["ClientName"];
                    Label4.Text = receive.Values["ClientId"];
                }
              
               
                Cancel.Visible = false;
                Sucess.Visible = false;
                ProjectUpdate.Visible = false;
                SqlConnection connection = new SqlConnection(con);
                Display();
                using (SqlCommand cmd = new SqlCommand("DisplayClientName", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    connection.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();
                    ClientName.DataSource = rdr;
                    ClientName.DataTextField = "CName";
                    ClientName.DataValueField = "CId";
                    ClientName.DataBind();
                    ListItem list = new ListItem("--Select--", "-1");
                    ClientName.Items.Insert(0, list);

                }
            }
           
        }

        public bool Valdation()
        {
            
            if (ClientName.SelectedValue == "-1")
            {
                Sucess.Text = "Please Select the client Name ";
                valdate = false;
                return valdate;
            }
            else
            {
                valdate = true;
            }

            if (ProjectName.Text == string.Empty)
            {
                Sucess.Text = "Please enter Project Name";
                valdate = false;
                return valdate;
            }
            else
            {
                valdate = true;
            }
            return valdate;
            
        }

        protected void Add_Click(object sender, EventArgs e)
        {
            if (Valdation())
            {


                using (SqlConnection connection = new SqlConnection(con))
                {
                    SqlCommand cmd = new SqlCommand("ProjectInsert", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@PName", ProjectName.Text);
                    cmd.Parameters.AddWithValue("@CId", ClientName.SelectedValue);
                    cmd.Parameters.AddWithValue("@PId", DBNull.Value);
                    connection.Open();
                    int result = cmd.ExecuteNonQuery();
                    if (result > 0)
                    {
                        Sucess.Text = "Addition of client Data is sucessfull";
                        Sucess.Visible = true;
                        ProjectName.Text = "";

                        ClientName.SelectedValue = "-1";
                        Display();
                    }
                    else
                    {
                        Sucess.Text = "Error in inserting the vaule";
                        Sucess.Visible = true;
                    }
                }
            }
        }
        public void Display()
        {
            using (SqlConnection connection = new SqlConnection(con))
            {
                SqlCommand cmd2 = new SqlCommand("DisplayProject", connection);
                cmd2.CommandType = CommandType.StoredProcedure;
                cmd2.Parameters.AddWithValue("@CId", DBNull.Value);
                connection.Open();
                ProjectGridView.DataSource = cmd2.ExecuteReader();
                ProjectGridView.DataBind();
            }
        }

        protected void ClientName_SelectedIndexChanged(object sender, EventArgs e)
        {
            clientValueField = int.Parse(ClientName.SelectedItem.Value);
        }

        protected void ProjectGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "ProjectEdit")
            {
                Add.Visible = false;
                Cancel.Visible = true;
                ProjectUpdate.Visible = true;
                var getBtn = e.CommandSource as LinkButton;
                var clickedRow = getBtn.NamingContainer as GridViewRow;
                ProjectName.Text=clickedRow.Cells[2].Text;
                ClientName.SelectedValue = clickedRow.Cells[3].Text;
                ProjectId = int.Parse(clickedRow.Cells[1].Text);
            }
            if(e.CommandName== "ProjectDelete")
            {
                var getBtn = e.CommandSource as LinkButton;
                var clickedRow = getBtn.NamingContainer as GridViewRow;
                int? ProjectDeleteId = int.Parse(clickedRow.Cells[1].Text);
                using(SqlConnection connection = new SqlConnection(con))
                {
                    SqlCommand cmd = new SqlCommand("ProjectDelete", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@PId", ProjectDeleteId);
                    connection.Open();
                    int result = cmd.ExecuteNonQuery();
                    if (result > 0)
                    {
                        Sucess.Text = "Project Deleted Sucessfully";
                        Sucess.Visible = true;
                        Display();
                        ProjectDeleteId = null;
                    }
                    else
                    {
                        Sucess.Text = "Project Could't not be delted";
                        Sucess.Visible = false;
                    }
                }
            }
        }
        protected void ProjectUpdate_Click(object sender, EventArgs e)
        {
            if (Valdation())
            {
                using (SqlConnection connection = new SqlConnection(con))
                {
                    SqlCommand cmd = new SqlCommand("ProjectInsert", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@PName", ProjectName.Text);
                    cmd.Parameters.AddWithValue("@CId", ClientName.SelectedValue);
                    cmd.Parameters.AddWithValue("@PId", ProjectId);
                    connection.Open();
                    int result = cmd.ExecuteNonQuery();
                    if (result > 0)
                    {
                        Sucess.Text = "Update of client Data is sucessfull";
                        Sucess.Visible = true;
                        ProjectId = null;
                        Display();
                        ProjectName.Text = "";
                        ClientName.SelectedValue = "-1";
                        Cancel.Visible = false;
                        Add.Visible = true;
                        ProjectUpdate.Visible = false;
                    }
                    else
                    {
                        Sucess.Text = "Error in inserting the vaule";
                        Sucess.Visible = true;
                        Add.Visible = true;
                    }
                }
            }
        }

        protected void Cancel_Click(object sender, EventArgs e)
        {
            ClientName.SelectedValue = "-1";
            ProjectName.Text = "";
            Cancel.Visible = false;
            ProjectUpdate.Visible = false;

        }
    }
}