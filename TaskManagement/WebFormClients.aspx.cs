using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace TaskManagement
{
    
    public partial class WebFormClients : System.Web.UI.Page
    {
        public static string con = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;
        public static int? Id = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                Sucess.Visible = false;
                error.Visible = false;
                HideButtons();
                Display();
            }
            
        }

        public void HideButtons()
        {
            ClientUpdate.Visible = false;
            ClientCancel.Visible = false;
        }
        public void ShowButtons()
        {
            ClientUpdate.Visible = true;
            ClientCancel.Visible = true;
        }
        protected void ClientAdd_Click(object sender, EventArgs e)
        {
            if (ClientName.Text == string.Empty)
            {
                error.Text = "Please fill the Client Name";
                error.Visible = true;
            }
            else
            {

                using (SqlConnection connection = new SqlConnection(con))
                {
                    SqlCommand cmd = new SqlCommand("ClientInsert", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CId", DBNull.Value);
                    cmd.Parameters.AddWithValue("@CName", ClientName.Text);
                    connection.Open();
                    int result = cmd.ExecuteNonQuery();
                    if (result > 0)
                    {
                        Sucess.Text = "Addition of client Data is sucessfull";
                        Sucess.Visible = true;
                        Display();
                        clearValues();
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
            using(SqlConnection connection = new SqlConnection(con))
            {
                SqlCommand cmd2 = new SqlCommand("DisplayClientName", connection);
                cmd2.CommandType = CommandType.StoredProcedure;
                connection.Open();
                var data = cmd2.ExecuteReader();
                //ClientData.DataSource = cmd2.ExecuteReader();
                //ClientData.DataBind();
                ClientData.DataSource = data;
                ClientData.DataBind();
            }
        }

        protected void ClientData_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        protected void ClientData_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if(e.CommandName== "EditBtn")
            {
                var getBtn = e.CommandSource as LinkButton;
                var clickedRow = getBtn.NamingContainer as GridViewRow;
                Id = int.Parse(clickedRow.Cells[1].Text);
                ClientName.Text=clickedRow.Cells[2].Text;
                ShowButtons();
                ClientAdd.Visible = false;

            }
            if(e.CommandName == "DelBtn")
            {
                var getBtn = e.CommandSource as LinkButton;
                var clickedRow = getBtn.NamingContainer as GridViewRow;
                int? DeleteId = int.Parse(clickedRow.Cells[1].Text);

                using (SqlConnection connection = new SqlConnection(con))
                {
                    SqlCommand cmd = new SqlCommand("DeleteClient", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    connection.Open();
                    cmd.Parameters.AddWithValue("@CId", DeleteId);
                    int result =cmd.ExecuteNonQuery();
                    if (result > 0)
                    {
                        Sucess.Text = "Data is sucessfull Deleted";
                        Sucess.Visible = true;
                        Display();
                        clearValues();
                        DeleteId = null;
                    }
                    else
                    {
                        Sucess.Text = "Error in Deleting the vaule";
                        Sucess.Visible = true;
                    }
                }
            }
        }

        protected void ClientUpdate_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(con))
            {
                SqlCommand cmd = new SqlCommand("ClientInsert", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CName", ClientName.Text);
                cmd.Parameters.AddWithValue("@CId",Id);
                connection.Open();
                int result = cmd.ExecuteNonQuery();
                if (result > 0)
                {
                    Sucess.Text = "Client Data is sucessfully updated";
                    Sucess.Visible = true;
                    clearValues();
                    Display();
                    ClientAdd.Visible = true;
                    HideButtons();
                }
                else
                {
                    Sucess.Text = "Error in inserting the vaule";
                    Sucess.Visible = true;
                    ClientAdd.Visible = true;
                    HideButtons();
                }
            }
        }

        public void clearValues()
        {
            ClientName.Text = "";
            Id = null;
         
        }

        protected void ClientCancel_Click(object sender, EventArgs e)
        {
            clearValues();
            HideButtons();
            ClientAdd.Visible = true;
        }

        protected void BtnLoad_Click(object sender, EventArgs e)
        {
            //ViewState["ClientName"] = ClientName.Text;
            // HiddenField1.Value = ClientName.Text;
            //ClientName.Text = "";
            //Response.Redirect("WebFormProject.aspx?ClientName="+ClientName.Text);
            HttpCookie cookie = new HttpCookie("ckName");
            cookie.Values.Add("ClientName", ClientName.Text);
            cookie.Values.Add("ClientId", "1");
            // cookie.Expires.AddSeconds(5);
            cookie.Expires = DateTime.Now.AddSeconds(5);
            Response.Cookies.Add(cookie);
            Response.Redirect("WebFormProject.aspx");
        }

        protected void ReloadBtn_Click(object sender, EventArgs e)
        {
            // ClientName.Text = Convert.ToString(ViewState["ClientName"]);
            //ClientName.Text = HiddenField1.Value;
        }
    }
}