using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Web.UI;
using Telerik.Web.UI;

namespace NewSM1
{
    public partial class Period : System.Web.UI.Page
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





        protected void FieldToVariable(string vYear, string vMonth)
        {
            String cmdString = "select * from smperiod where theyear ='" + vYear + "' and themonth= '" + vMonth + "'";
            SqlDataReader reader = getDataReader(cmdString);
            reader.Read();
            if (reader.HasRows)
            {
                radYear.SelectedValue = reader["theyear"].ToString();
                radYear.Enabled = false;
                radMonth.SelectedValue = reader["themonth"].ToString();
                radMonth.Enabled = false;
                radLocked.SelectedValue = reader["locked"].ToString();
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
            lblModalTitle.Text = "Period Details";
            radYear.Enabled = true;
            radMonth.Enabled = true;
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myModal", "$('#myModal').modal();", true);
            upModal.Update();
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
            Response.Redirect("bannerho.aspx");
        }

        protected void cmdClear_Click(object sender, EventArgs e)
        {
            ClearFields();
        }





        protected void BindData()
        {
            SqlConnection con = new SqlConnection(sConnectionString);
            String cmdString = "select * from smperiod where 1=1 ";
            if (txtSearchYear.Text.Trim() != "") { cmdString = cmdString + " and theyear like '" + txtSearchYear.Text + "%'"; }
            if (txtSearchMonth.Text.Trim() != "") { cmdString = cmdString + " and themonth like '" + txtSearchMonth.Text + "%'"; }
            cmdString = cmdString + " order by theyear";
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
            txtSearchMonth.Text = "";
            txtSearchYear.Text = "";


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
                string vYear = item["THEYEAR"].Text;
                string vMonth = item["THEMONTH"].Text;
                Session["THE_YEAR"] = vYear;
                Session["THE_MONTH"] = vMonth;
                FieldToVariable(vYear, vMonth);
                ActFlag.Text = "Editing";
                lblModalTitle.Text = "Edit Period";
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
            if (radYear.SelectedValue.ToString() == "")
            {
                lblError.Text = "Year Can't be empty"; return;
            }
            if (radMonth.SelectedValue.ToString() == "")
            {
                lblError.Text = "Month Can't be empty"; return;
            }
            string thekey = "";
            string flag = "";
            string cmdu = "";
            SqlConnection con = new SqlConnection(sConnectionString);
            SqlCommand cmd = new SqlCommand(cmdu, con);
            cmd.CommandType = CommandType.Text;
            if (ActFlag.Text == "Adding")
            {
                cmd.CommandText = "Insert into smperiod(theyear,themonth,locked,monthsmall) values(@theyear,@themonth,@locked,@monthsmall)";
                cmd.Parameters.Add("@flag", SqlDbType.VarChar).Value = "Add";
                cmd.Parameters.Add("@theyear", SqlDbType.VarChar).Value = radYear.SelectedValue;
                cmd.Parameters.Add("@themonth", SqlDbType.VarChar).Value = radMonth.SelectedValue;
                cmd.Parameters.Add("@locked", SqlDbType.VarChar).Value = radLocked.SelectedValue;
                cmd.Parameters.Add("@monthsmall", SqlDbType.VarChar).Value = radMonth.SelectedItem.Text;


                flag = "Inserted";
            }
            if (ActFlag.Text == "Editing")
            {
                cmd.CommandText = "update smperiod set locked=@lockded where " +
                    " theyear = '" + Session["THE_YEAR"] + " and themonth='" + Session["THE_MONTH"].ToString() + "'";
                cmd.Parameters.Add("@flag", SqlDbType.VarChar).Value = "Edit";
                cmd.Parameters.Add("@locked", SqlDbType.VarChar).Value = radLocked.SelectedValue;
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