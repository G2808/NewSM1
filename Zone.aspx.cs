using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web.Configuration;
using System.Web.UI;
using Telerik.Web.UI;

namespace NewSM1
{
    public partial class Zone : System.Web.UI.Page
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
            String cmdString = "select * from smzone where zone_id ='" + vID + "'";
            SqlDataReader reader = getDataReader(cmdString);
            reader.Read();
            if (reader.HasRows)
            {
                txtID.Text = reader["zone_id"].ToString();
                txtID.BackColor = Color.LightGray;
                txtID.Enabled = false;
                txtZoneName.Text = reader["descr"].ToString();
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
            txtID.Enabled = true;
            txtID.BorderColor = Color.White;
            lblModalTitle.Text = "Add Region";
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
            String cmdString = "select * from smzone where 1=1 ";
            if (txtSearchShort.Text.Trim() != "") { cmdString = cmdString + " and zone_id like '" + txtSearchShort.Text + "%'"; }
            if (txtSearchName.Text.Trim() != "") { cmdString = cmdString + " and descr like '" + txtSearchName.Text + "%'"; }
            cmdString = cmdString + " order by zone_id";
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
            txtID.Text = "";
            txtSearchName.Text = "";
            txtZoneName.Text = "";


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
                string index = item["zone_id"].Text;
                Session["zone_id"] = index;
                FieldToVariable(index);
                ActFlag.Text = "Editing";
                lblModalTitle.Text = "Edit Zone";
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
            if (txtZoneName.Text == "")
            {
                lblError.Text = "Zone name Can't be empty"; return;
            }
            string thekey = "";
            string flag = "";
            string cmdu = "";
            SqlConnection con = new SqlConnection(sConnectionString);
            SqlCommand cmd = new SqlCommand(cmdu, con);
            cmd.CommandType = CommandType.Text;
            if (ActFlag.Text == "Adding")
            {
                cmd.CommandText = "insert into smzone(id,descr) value(@id,@descr)";
                cmd.Parameters.Add("@id", SqlDbType.VarChar).Value = txtID.Text;
                cmd.Parameters.Add("@descr", SqlDbType.VarChar).Value = txtZoneName.Text;
                flag = "Inserted";
            }
            if (ActFlag.Text == "Editing")
            {
                cmd.CommandText = "update smzone set descr = @descr where zone_id='" + txtID.Text + "'";
                cmd.Parameters.Add("@descr", SqlDbType.VarChar).Value = txtZoneName.Text;
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