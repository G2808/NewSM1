using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;

namespace NewSm1
{
    public partial class Admin : System.Web.UI.MasterPage
    {
        private string sConnectionString = WebConfigurationManager.ConnectionStrings["ConnectionStringSM"].ConnectionString;
        DataBase db = new DataBase();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["CODE"] == null) { Response.Redirect("Login.aspx"); }
                string strSQL;
                if (Session["ROLE"].ToString() != "Y")
                {
                    strSQL = "SELECT menuid,mmenu,target,nullif(submenuid,0) submenuid FROM smmenunew order by seqmain ";
                }
                else
                {
                    strSQL = "select * from smmenunew where submenuid> 0 and menuid in (select menuid from smusermenu where userid ='" + Session["theUserId"].ToString() + "') order by seqsub";
                }
                BindMenu(strSQL);
                lblUserLogin.Text = Session["NAME"].ToString();
            }
        }

        private void BindMenu(string query)
        {

            SqlConnection con = new SqlConnection(sConnectionString);
            string cmdsql = query;
            SqlCommand cmd = new SqlCommand(cmdsql, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            try
            {
                con.Open();
                da.Fill(dt);
                RadMenu1.DataTextField = "mmenu";
                RadMenu1.DataFieldID = "menuid";
                RadMenu1.DataFieldParentID = "submenuid";
                RadMenu1.DataNavigateUrlField = "target";
                //RadMenu1.Attributes["Target"] = "targetWindow";
                RadMenu1.DataSource = dt;
                RadMenu1.DataBind();
            }
            catch (Exception ex) { }

            finally
            {
                con.Close();
            }

        }


    }
}