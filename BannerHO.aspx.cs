using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;

namespace NewSm1
{
    public partial class BannerHO : System.Web.UI.Page
    {
        private string sConnectionString = WebConfigurationManager.ConnectionStrings["ConnectionStringSM"].ConnectionString;
        DataBase db = new DataBase();
        protected void Page_Load(object sender, EventArgs e)
        {
            getData();
        }

        private void getData()
        {
            DataTable table = new DataTable();
            SqlConnection con = new SqlConnection(sConnectionString);
            SqlCommand cmd = new SqlCommand("select sum(value) value from smNewTERTIARYDASHBORD where ", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@startdate", "2019-07-01");
            cmd.Parameters.AddWithValue("@enddate", "2019-07-31");
            cmd.Parameters.AddWithValue("@userid", Session["CODE"].ToString());
            string IsAdmin = "N";
            if (Session["ROLE"].ToString().Trim().ToUpper() == "ADMIN")
            {
                IsAdmin = "Y";
            }
            cmd.Parameters.AddWithValue("@IsAdmin", "Y");
            try
            {
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    lblMonthTertiary.Text = dt.Compute("sum(thevalue)", string.Empty).ToString();
                }
            }
            catch (Exception ex) { }
            finally { con.Close(); }

        }


    }


}