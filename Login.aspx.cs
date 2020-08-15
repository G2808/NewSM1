using NewSm1;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;


namespace NewSM1
{
    public partial class Login : System.Web.UI.Page
    {
        private string sConnectionString = WebConfigurationManager.ConnectionStrings["ConnectionStringSM"].ConnectionString;
        DataBase db = new DataBase();

        protected void Page_Load(object sender, EventArgs e)
        {
            Session.RemoveAll();
            if (!IsPostBack) { }

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            if (txtLogin.Text.Trim() == "") { lblError.Text = "Please Enter Login Name"; return; }
            if (txtPassword.Text.Trim() == "") { lblError.Text = "Please enter Password"; return; }
            string cmdString = "SMNEWLogin";
            SqlConnection con = new SqlConnection(sConnectionString);
            SqlCommand cmd = new SqlCommand(cmdString, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@LOGIN", SqlDbType.VarChar).Value = txtLogin.Text;
            cmd.Parameters.Add("@PASSWORD", SqlDbType.VarChar).Value = txtPassword.Text;

            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                reader.Read();
                if (reader.HasRows)
                {

                    Session["CODE"] = reader["CODE"];
                    Session["ROLE"] = reader["ROLE"];
                    Session["NAME"] = reader["NAME"];

                    //  Response.Redirect("BANNERHO.aspx");
                    Response.Redirect("userrights.aspx");
                }

            }
            catch (Exception ex)
            {
                lblError.Text = "Invalid User or Password";
            }
            finally { con.Close(); }

        }

        private int GetCurrentYear()
        {
            int rint = 0;
            SqlConnection con = new SqlConnection(sConnectionString);
            string cmdString = "";
            if (Session["FIELD"].ToString() == "Y")
            {
                cmdString = "select max(lyear) lyear from currentyear where flag='FIELD'";

            }
            if (Session["FIELD"].ToString() == "N")
            {
                cmdString = "select max(lyear) lyear from currentyear where flag='HO'";
            }
            SqlCommand cmd = new SqlCommand(cmdString, con);
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                reader.Read();
                rint = Convert.ToInt16(reader["lyear"].ToString());
            }
            catch { }
            finally { con.Close(); }
            return rint;
        }

        protected void btnForgotPassword_Click(object sender, EventArgs e)
        {
            Session["FORGOTPASSWORD"] = "##LOGINPAGE##";
            Response.Redirect("ForgotPassword.aspx");
        }


    }
}