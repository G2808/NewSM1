using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;
using Telerik.Web.UI;

namespace NewSM1
{
    public partial class PRODUCTTEAMLINK : System.Web.UI.Page
    {
        private string sConnectionString = WebConfigurationManager.ConnectionStrings["ConnectionStringsm"].ConnectionString;


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                radData.MasterTableView.NoMasterRecordsText = "No records to Display";
                radData.ExportSettings.HideStructureColumns = true;
                radData.ExportSettings.SuppressColumnDataFormatStrings = false;
                radData.ExportSettings.ExportOnlyData = true;
                radData.ExportSettings.IgnorePaging = true;
                radData.ExportSettings.Excel.Format = GridExcelExportFormat.ExcelML;
                Panel_AddEdit.Visible = false; Panel_Search.Visible = true;
                BindTeam();
                BindProduct();
                Session["ID_TEAMPRODUCT"] = 0;


            }
            radData.EnableAjaxSkinRendering = true;
        }


        protected Boolean CheckLinkExist()
        {
            Boolean retval = false;
            SqlConnection con = new SqlConnection(sConnectionString);
            String cmdString = "CheckDateOverlapTeamProduct ";
            SqlCommand cmd = new SqlCommand(cmdString, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@teamcode", SqlDbType.VarChar).Value = DDTeam.SelectedValue;
            cmd.Parameters.Add("@productcode", SqlDbType.VarChar).Value = ddProduct.SelectedValue;
            cmd.Parameters.Add("@d1", SqlDbType.SmallDateTime).Value = radFrom.SelectedDate;
            cmd.Parameters.Add("@d2", SqlDbType.SmallDateTime).Value = radTo.SelectedDate;
            cmd.Parameters.Add("@ID", SqlDbType.BigInt).Value = Convert.ToInt64(Session["ID_TEAMPRODUCT"].ToString());

            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                reader.Read();
                if (reader.HasRows)
                {
                    retval = true;
                    lblErrorAdd.Text = "Link exist as ID:" + reader["ID"].ToString() + " for product " + ddProduct.SelectedValue + " and Team:" + DDTeam.SelectedValue;
                }
                else { retval = false; }

            }
            catch { }
            finally { con.Close(); }
            return retval;
        }



        protected void FieldToVariable(Int32 vID)
        {
            String cmdString = "select * from teamproduct where ID ='" + vID + "'";
            SqlDataReader reader = getDataReader(cmdString);
            reader.Read();
            if (reader.HasRows)
            {
                ddProduct.SelectedValue = reader["productcode"].ToString();
                DDTeam.SelectedValue = reader["teamcode"].ToString();
                radFrom.SelectedDate = Convert.ToDateTime(reader["datefrom"].ToString());
                radTo.SelectedDate = Convert.ToDateTime(reader["dateto"].ToString());
                reader.Close();
                Panel_AddEdit.Visible = true; Panel_Search.Visible = false;
            }
        }

        protected void cmdAdd_Click(object sender, EventArgs e)
        {
            ClearFields();
            ActFlag.Text = "Adding";
            Panel_AddEdit.Visible = true; Panel_Search.Visible = false;
        }


        protected void cmdUpdate_Click(object sender, EventArgs e)
        {
            if (DDTeam.SelectedValue == "") { lblErrorAdd.Text = "Please select Team"; return; }
            if (ddProduct.SelectedValue == "") { lblErrorAdd.Text = "Please select Product"; return; }
            if (radFrom.SelectedDate.ToString() == "") { lblErrorAdd.Text = "Please select Date From"; return; }
            if (radTo.SelectedDate.ToString() == "") { lblErrorAdd.Text = "Please select Date To"; return; }
            if (radFrom.SelectedDate >= radTo.SelectedDate) { lblErrorAdd.Text = "Invalid Period specificed"; return; }
            if (CheckLinkExist()) { return; }

            string thekey = "";
            string flag = "";
            string cmdu = "";
            SqlConnection con = new SqlConnection(sConnectionString);
            SqlCommand cmd = new SqlCommand(cmdu, con);
            if (ActFlag.Text == "Adding")
            {
                cmdu = "insert into TEAMPRODUCT(PRODUCTCODE,TEAMCODE,DATEFROM,DATETO) values (@PRODUCTCODE,@TEAMCODE,@DATEFROM,@DATETO) ";
                flag = "Inserted";
            }

            if (ActFlag.Text == "Editing")
            {
                cmdu = "update TEAMPRODUCT set productcode=@productcode,teamcode=@teamcode,datefrom=@datefrom,dateto=@dateto where id=" + Session["ID_TEAMPRODUCT"];
                thekey = Session["ID_TEAMPRODUCT"].ToString();
                flag = "Edited";
            }
            cmd.Parameters.Add("@productcode", SqlDbType.VarChar).Value = ddProduct.SelectedValue;
            cmd.Parameters.Add("@teamcode", SqlDbType.VarChar).Value = DDTeam.SelectedValue;
            cmd.Parameters.Add("@datefrom", SqlDbType.SmallDateTime).Value = radFrom.SelectedDate;
            cmd.Parameters.Add("@dateto", SqlDbType.SmallDateTime).Value = radTo.SelectedDate;

            cmd.CommandType = CommandType.Text;
            //ActFlag.Text = "";
            try
            {
                con.Open();
                cmd.CommandText = cmdu;
                cmd.ExecuteNonQuery();
                CreateLog(thekey, Convert.ToInt32(Session["LOGINID"]), "TEAMPRODUCT", flag);
                Panel_AddEdit.Visible = false; Panel_Search.Visible = true;

                BindData();
                ClearFields();
            }
            catch (Exception ex)
            {

                lblErrorAdd.Text = "Error Inserting/Updating Record" + ex.Message;
                return;
            }
            finally { con.Close(); }
        }


        protected void CreateLog(string thekey, int userid, string modulechanged, string theAction)
        {

            SqlConnection con = new SqlConnection(sConnectionString);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "ChangeLogAdd";
            cmd.Connection = con;
            cmd.Parameters.Add("@key", SqlDbType.VarChar).Value = thekey;
            cmd.Parameters.Add("@modulechanged", SqlDbType.VarChar).Value = modulechanged;
            cmd.Parameters.Add("@action", SqlDbType.VarChar).Value = theAction;
            cmd.Parameters.Add("@userid", SqlDbType.Int).Value = Convert.ToInt32(Session["LOGINID"]);
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
            }
            catch { }
            finally { con.Close(); }
        }


        protected void cmdSearch_Click(object sender, EventArgs e)
        {
            radData.Visible = true;
            BindData();
        }


        protected void cmdExit_Click(object sender, EventArgs e)
        {
            //Response.Redirect("banner.aspx");
            Response.Redirect("bannerho.aspx");
        }

        protected void cmdClear_Click(object sender, EventArgs e)
        {
            ClearFields();
        }


        protected void BindData()
        {
            SqlConnection con = new SqlConnection(sConnectionString);
            String cmdString = "select id,productcode,teamcode,datefrom,dateto from teamproduct where 1=1 ";
            if (txtSearchProduct.Text.Trim() != "") { cmdString = cmdString + " and productcode like '" + txtSearchProduct.Text + "%'"; }
            if (txtSearchTeam.Text.Trim() != "") { cmdString = cmdString + " and teamcode like '" + txtSearchTeam.Text + "%'"; }

            cmdString = cmdString + " order by productcode";
            try
            {
                SqlDataReader reader = getDataReader(cmdString);
                radData.DataSource = reader;
                radData.DataBind();
                reader.Close();
            }
            catch { }
        }



        protected SqlDataReader getDataReader(String cmdString)
        {
            SqlConnection con = new SqlConnection(sConnectionString);
            SqlCommand cmd = new SqlCommand(cmdString, con);
            SqlDataReader dr = null;
            con.Open();
            try
            {
                dr = cmd.ExecuteReader();
            }
            catch { }
            finally { }
            return dr;
        }



        protected void ClearFields()
        {
            txtSearchProduct.Text = "";
            txtSearchTeam.Text = "";
            radFrom.SelectedDate = null;
            radTo.SelectedDate = null;
            lblErrorAdd.Text = "";
        }

        protected void cmdExitAdd_Click(object sender, EventArgs e)
        {
            Panel_AddEdit.Visible = false; Panel_Search.Visible = true;
            ClearFields();
            ActFlag.Text = "";
            lblErrorAdd.Text = "";
        }
        protected void radData_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            BindData();
        }
        protected void radData_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "CMDEDIT")
            {
                GridDataItem item = (GridDataItem)e.Item;
                ActFlag.Text = "Editing";

                Int32 index = Convert.ToInt32(item["ID"].Text.ToString());
                Session["ID_TEAMPRODUCT"] = index;
                FieldToVariable(index);

            }

        }

        protected void BindProduct()
        {
            SqlConnection con = new SqlConnection(sConnectionString);
            String cmdString = "select code,name from producthr order by code ";
            SqlCommand cmd = new SqlCommand(cmdString, con);
            try
            {
                con.Open();

                ddProduct.DataSource = cmd.ExecuteReader();
                ddProduct.DataTextField = "name";
                ddProduct.DataValueField = "code";
                ddProduct.DataBind();

            }
            catch { }
            finally { con.Close(); }


        }
        protected void BindTeam()
        {
            SqlConnection con = new SqlConnection(sConnectionString);
            String cmdString = "select teamcode,teamname from SMTEAM order by teamcode ";
            SqlCommand cmd = new SqlCommand(cmdString, con);
            try
            {
                con.Open();

                DDTeam.DataSource = cmd.ExecuteReader();
                DDTeam.DataTextField = "teamname";
                DDTeam.DataValueField = "teamcode";
                DDTeam.DataBind();

            }
            catch { }
            finally { con.Close(); }


        }


    }
}