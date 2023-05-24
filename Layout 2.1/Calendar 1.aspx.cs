using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    // change code with your code that you have changed in local machine 
    public partial class WebForm11 : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-2VT3DAG;Initial Catalog=db1;Integrated Security=True");
        protected void Page_Load(object sender, EventArgs e)
        {
            today();
            /*
            if (!IsPostBack)
            {
                today();
            }*/
            //string UserId = Session["Id"] as string;
            //Label1.Text= UserId;
            Session["ID1"] = Session["Id"];
            string UserId = Session["Id"] as string;
            Label1.Text= UserId;
        }


        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("Create_Abs.aspx");
        }

        protected void Calendar1_SelectionChanged(object sender, EventArgs e)
        {

            string SelectDate = Calendar1.SelectedDate.ToString("yyyy-MM-dd");
            Response.Write(SelectDate);

           
            con.Open();
                string query = "SELECT e.FirstName, l.LeaveType FROM Employee e INNER JOIN Leave l ON e.id = l.EMP_ID WHERE  '" + Calendar1.SelectedDate.ToString("yyyy-MM-dd") + "'between l.StartDate AND l.EndDate;";
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader sdr = cmd.ExecuteReader();
                if (sdr.HasRows == true)
                {
                    GridView1.DataSource = sdr;
                    GridView1.DataBind();
                }
                else
                {
                    //Empty data Table
                    DataTable dt = new DataTable();
                    GridView1.DataSource = dt;
                    GridView1.DataBind();
                }
                con.Close();
            
                        
        }    
        
        private void today()
        {
           
            string query = "SELECT e.FirstName,l.LeaveType FROM Employee e INNER JOIN Leave l ON e.id = l.EMP_ID where  '" + Calendar1.TodaysDate.ToString("yyyy-MM-dd") + "'between l.StartDate AND l.EndDate";
            SqlCommand cmd = new SqlCommand(query, con);
            //SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            SqlDataReader sdr = cmd.ExecuteReader();
            if(sdr.HasRows == true)
            {
                GridView1.DataSource = sdr;
                GridView1.DataBind();             
            }
            else
            {
                //Empty data Table
                DataTable dt = new DataTable();
                GridView1.DataSource = dt;
                GridView1.DataBind();
            }
            con.Close();
        }

        protected void Calendar1_DayRender(object sender, DayRenderEventArgs e)
        {
            if ( e.Day.IsWeekend)
            {
                e.Day.IsSelectable = false;
                e.Cell.ToolTip = "Chhuti hai bhai..";
                
            }
            if(e.Day.IsOtherMonth )
            {
                e.Day.IsSelectable = false;
            }

            

        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            string query = "Select Firstname From HolidayTable";
            SqlCommand cmd2 = new SqlCommand(query, con);
            con.Open();
            SqlDataReader sdr2 = cmd2.ExecuteReader();
            if (sdr2.HasRows == true)
            {
                GridView1.DataSource = sdr2;
                GridView1.DataBind();
            }
            // Access the header row of the GridView
            GridViewRow headerRow = GridView1.HeaderRow;
            if (headerRow != null)
            {
                // Modify the header text
                headerRow.Cells[0].Text = "Upcoming Holidays";
                // Replace 0 with the appropriate cell index if you have multiple columns in the header.
            }
            con.Close() ;
        }



    }
}