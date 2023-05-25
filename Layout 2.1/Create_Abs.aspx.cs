using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Reflection.Emit;
using SetOffs1;
//using Microsoft.AspNet.FriendlyUrls;

namespace WebApplication1
{
    // This is the demo for github
     
    public partial class WebForm1 : System.Web.UI.Page
    {
        //Change you code what you have added on local machine
        //
        SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-2VT3DAG;Initial Catalog=db1;Integrated Security=True");

      

        static string b="2";
        int a = Int16.Parse(b);
        DateTime from1;
        DateTime To1;


        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                Calendar1.Visible= false;
                Calendar2.Visible = false;
            }
            // b = Session["ID"]as string;
           

        }
     







    protected void Calendar1_Click(object sender, ImageClickEventArgs e)
        {
           
            Calendar2.Visible = false;
            if (Calendar1.Visible)
            {
                Calendar1.Visible= false;
               
            }
            else
            {
                Calendar1.Visible= true;
               

            }

            Calendar1.Attributes.Add("style", "position:absolute");

        }

        protected void Unnamed_Click1(object sender, ImageClickEventArgs e)
        {
           
            Calendar1.Visible = false;
            if (Calendar2.Visible)
            {
                Calendar2.Visible = false;
            }
            else
            {
                Calendar2.Visible = true;
            }
            Calendar2.Attributes.Add("style", "position:absolute");
            
        }
       

        protected void Calendar1_SelectionChanged(object sender, EventArgs e)
        {
            from.Text=Calendar1.SelectedDate.ToString("dd/MM/yy");
            Calendar1.Visible = false;
             from1 = Convert.ToDateTime(from.Text);


        }

        protected void Calendar2_SelectionChanged(object sender, EventArgs e)
        {
            To.Text = Calendar2.SelectedDate.ToString("dd/MM/yy");
            Calendar2.Visible = false;
            To1 = Convert.ToDateTime(To.Text);



            DateTime startDate = Calendar1.SelectedDate;
            DateTime endDate = Calendar2.SelectedDate;
            TimeSpan difference = endDate - startDate;
            string m = difference.ToString("dd");

            int n = Int16.Parse(m)+1;
            Total_Days.Text = n.ToString();

        }
      
       


        protected void Unnamed_Click2(object sender, EventArgs e)
        {
           if(from.Text=="" || To.Text==""|| Drop.SelectedValue== "")
            {
               
                string script = $@"<script type='text/javascript'>alert('{"* Error : Plese fill all information"}');</script>";
                ClientScript.RegisterStartupScript(this.GetType(), "errorScript", script);
            }
            

            //  SqlCommand cmd =conn.CreateCommand();
            // cmd.CommandType= CommandType.Text;

            //  cmd.CommandText = "insert into ADD values('" + Drop.SelectedValue + "', '" + from + "','" + To + "','" + TextBox1.Text + "' ) ";


            //  cmd.ExecuteNonQuery();
            else {

                string UserId = Session["Id"] as string;

                conn.Open();
                SqlCommand cmd1 = new SqlCommand("select id from Employee where email='"+UserId+"'",conn);
                object result = cmd1.ExecuteScalar();
                if (result != null)
                {
                    a = (int)result; // Type casting the result to int and assigning it to employeeId
                }
                //else{handle the exception later}
                Leave l = new Leave();
                l.LeaveType = Drop.SelectedValue;

                DBConnection cmd = new DBConnection();
                cmd.AddLeave(l);


            //    SqlCommand cmd = new SqlCommand("insert into [Leave]  values('" + a + "','" + Drop.SelectedValue + "', '" + Calendar1.SelectedDate.ToString("yyyy-MM-dd") + "', '" + Calendar2.SelectedDate.ToString("yyyy-MM-dd") +"','" + Total_Days.Text + "','" + TextBox1.Text + "') ", conn);
            //cmd.ExecuteNonQuery();
            //conn.Close();


                
                Response.Redirect("Calendar 1.aspx");
            }

        }

        protected void Calendar1_DayRender(object sender, DayRenderEventArgs e)
        {
            if (e.Day.Date < DateTime.Now.Date) // Replace DateTime.Now with your selected value
            {
                e.Day.IsSelectable = false;
                e.Cell.ForeColor = System.Drawing.Color.Gray; // Change the color to gray to indicate the disabled day
            }
        }

        protected void Calendar2_DayRender(object sender, DayRenderEventArgs e)
        {
            if (e.Day.Date < Calendar1.SelectedDate) // Replace DateTime.Now with your selected value
            {
                e.Day.IsSelectable = false;
                e.Cell.ForeColor = System.Drawing.Color.Gray; // Change the color to gray to indicate the disabled day
            }

        }
        

        
    }
}