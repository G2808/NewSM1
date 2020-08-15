using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web.Configuration;
using System.Web.UI;
using Telerik.Web.UI;

namespace NewSM1
{
    public partial class Team : System.Web.UI.Page
    {
        private string sConnectionString = WebConfigurationManager.ConnectionStrings["ConnectionStringSM"].ConnectionString;


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
                //Panel_AddEdit.Visible = false; Panel_Search.Visible = true;

            }
            radData.EnableAjaxSkinRendering = true;
        }





        protected void FieldToVariable(string vID)
        {
            String cmdString = "select * from smteam where teamid =" + vID;
            SqlDataReader reader = getDataReader(cmdString);
            reader.Read();
            if (reader.HasRows)
            {
                txtID.Text = reader["TEAMID"].ToString();
                txtID.BackColor = Color.LightGray;
                teamName.Text = reader["TEAMNAME"].ToString();
                teamShortName.Text = reader["TEAMSHORTNAME"].ToString();
                reader.Close();
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myModal", "$('#myModal').modal();", true);

                //Panel_AddEdit.Visible = true; Panel_Search.Visible = false;
            }
        }

        protected void cmdAdd_Click(object sender, EventArgs e)
        {
            ClearFields();
            ActFlag.Text = "Adding";
            lblError.Text = "";
            lblModalTitle.Text = "Add Team";
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myModal", "$('#myModal').modal({backdrop: 'static', keyboard: false});", true);

            //ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myModal", "$('#myModal').modal();", true);
            //upModal.Update();
        }


        protected void UpdateRecord()
        {
            string thekey = "";
            string flag = "";
            string cmdu = "";
            SqlConnection con = new SqlConnection(sConnectionString);
            SqlCommand cmd = new SqlCommand(cmdu, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "AddEditTeam'";
            if (ActFlag.Text == "Adding")
            {

                cmd.Parameters.Add("@flag", SqlDbType.VarChar).Value = "Add";
                cmd.Parameters.Add("@TeamShortName", SqlDbType.VarChar).Value = teamShortName.Text;
                cmd.Parameters.Add("@TeamName", SqlDbType.VarChar).Value = teamName.Text;
                flag = "Inserted";
            }
            if (ActFlag.Text == "Editing")
            {
                cmd.Parameters.Add("@flag", SqlDbType.VarChar).Value = "Edit";
                cmd.Parameters.Add("@teamname", SqlDbType.VarChar).Value = teamName.Text;
                cmd.Parameters.Add("@teamshortname", SqlDbType.VarChar).Value = teamShortName.Text;
                cmd.Parameters.Add("@ID", SqlDbType.Int).Value = Session["TEAM_ID"];
            }

            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                //CreateLog(thekey, Convert.ToInt32(Session["CODE"]), "Employee", flag);
                BindData();
                ClearFields();
            }
            catch (Exception ex)
            {

                return;
            }
            finally { con.Close(); }

            //ClearFields();

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
            cmd.Parameters.Add("@userid", SqlDbType.Int).Value = Convert.ToInt32(Session["CODE"]);
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
            Response.Redirect("BannerHO.aspx");
        }

        protected void cmdClear_Click(object sender, EventArgs e)
        {
            ClearFields();
        }





        protected void BindData()
        {
            SqlConnection con = new SqlConnection(sConnectionString);
            String cmdString = "select * from smteam where 1=1 ";
            if (txtSearchShort.Text.Trim() != "") { cmdString = cmdString + " and teamshortname like '" + txtSearchShort.Text + "%'"; }
            if (txtSearchName.Text.Trim() != "") { cmdString = cmdString + " and teamname like '" + txtSearchName.Text + "%'"; }
            cmdString = cmdString + " order by teamshortname";
            try
            {
                SqlDataReader reader = getDataReader(cmdString);
                radData.DataSource = reader;
                radData.DataBind();
                reader.Close();
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
            }
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
            txtSearchShort.Text = "";
            txtSearchName.Text = "";
            teamShortName.Text = "";
            teamName.Text = "";
            //txtSname.Text = "";
            //txtName.Text = "";





        }

        protected void cmdExitAdd_Click(object sender, EventArgs e)
        {
            //Panel_AddEdit.Visible = false; Panel_Search.Visible = true;
            ClearFields();
            ActFlag.Text = "";
            //lblErrorAdd.Text = "";
            //txtSname.Enabled = true;
            //txtSname.BackColor = Color.White;
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
                string index = item["TEAMID"].Text;
                Session["TEAM_ID"] = index;
                FieldToVariable(index);
                ActFlag.Text = "Editing";
                lblModalTitle.Text = "Edit Team";
                //ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myModal", "$('#myModal').modal();", true);
                //upModal.Update();
            }
            if (e.CommandName == "CMDDELETE")
            {
                GridDataItem item = (GridDataItem)e.Item;
                int index = Convert.ToInt16(item["ID"].Text);
                SqlConnection con = new SqlConnection(sConnectionString);
                SqlCommand cmd = new SqlCommand();
                try
                {
                    cmd.Connection = con;
                    con.Open();
                    //cmd.CommandText = "update departments set deleted='Y',editdate='" + System.DateTime.Now + "' where id=" + index;
                    cmd.ExecuteNonQuery();
                    BindData();
                    radData.Rebind();
                }
                catch { }
                finally { con.Close(); }
            }
        }

        protected void Submit_Click(object sender, EventArgs e)
        {
            if (teamShortName.Text == "")
            {
                lblError.Text = "Short name Can't be empty"; return;
            }
            if (teamName.Text == "")
            {
                lblError.Text = "Name Can't be empty"; return;
            }
            string thekey = "";
            string flag = "";
            string cmdu = "";
            SqlConnection con = new SqlConnection(sConnectionString);
            SqlCommand cmd = new SqlCommand(cmdu, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "AddEditTeam";
            if (ActFlag.Text == "Adding")
            {

                cmd.Parameters.Add("@flag", SqlDbType.VarChar).Value = "Add";
                cmd.Parameters.Add("@TeamShortName", SqlDbType.VarChar).Value = teamShortName.Text;
                cmd.Parameters.Add("@TeamName", SqlDbType.VarChar).Value = teamName.Text;
                flag = "Inserted";
            }
            if (ActFlag.Text == "Editing")
            {
                cmd.Parameters.Add("@flag", SqlDbType.VarChar).Value = "Edit";
                cmd.Parameters.Add("@teamname", SqlDbType.VarChar).Value = teamName.Text;
                cmd.Parameters.Add("@teamshortname", SqlDbType.VarChar).Value = teamShortName.Text;
                cmd.Parameters.Add("@ID", SqlDbType.Int).Value = Session["TEAM_ID"];
            }

            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                //CreateLog(thekey, Convert.ToInt32(Session["CODE"]), "Employee", flag);
                BindData();
                ClearFields();
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myModal", "$('#myModal').modal('hide');", true);
                upModal.Update();
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
                return;
            }
            finally { con.Close(); }




        }
    }
}