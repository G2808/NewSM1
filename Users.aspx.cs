using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web.Configuration;
using Telerik.Web.UI;

namespace NewSM1
{
    public partial class Users : System.Web.UI.Page
    {
        private string sConnectionStringHR = WebConfigurationManager.ConnectionStrings["ConnectionStringSM"].ConnectionString;


        protected void Page_Load(object sender, EventArgs e)
        {
            //txtHeader.Text = "User Details";
            if (!IsPostBack)
            {

                radData.MasterTableView.NoMasterRecordsText = "No records to Display";
                radData.ExportSettings.HideStructureColumns = true;
                radData.ExportSettings.SuppressColumnDataFormatStrings = false;
                radData.ExportSettings.ExportOnlyData = true;
                radData.ExportSettings.IgnorePaging = true;
                radData.ExportSettings.Excel.Format = GridExcelExportFormat.ExcelML;
                Panel_search.Visible = true; Panel_entry.Visible = false;
                //Panel_AddEdit.Visible = false; Panel_Search.Visible = true;

            }
            radData.EnableAjaxSkinRendering = true;
        }


        protected void FieldToVariable(Int32 vID)
        {
            String cmdString = "select * from smnewuser where CODE =" + vID;
            SqlDataReader reader = getDataReader(cmdString);
            reader.Read();
            if (reader.HasRows)
            {
                txtCode.Text = reader["code"].ToString();
                txtCode.BackColor = Color.LightGray;
                txtCode.Enabled = false;
                txtFirstName.Text = reader["firstname"].ToString();
                txtLastName.Text = reader["lastname"].ToString();
                txtCode.Text = reader["CODE"].ToString();
                txtemail.Text = reader["email"].ToString();
                try
                {
                    radProfile.SelectedValue = reader["profile"].ToString();
                }
                catch { radProfile.SelectedValue = null; }
                finally { }
                txtVISA.Text = reader["visa"].ToString();
                txtPassword.Attributes["VALUE"] = reader["PASSWORD"].ToString();
                txtContact.Text = reader["contact"].ToString();
                reader.Close();
                Panel_search.Visible = false; Panel_entry.Visible = true;
                //Panel_AddEdit.Visible = true; Panel_Search.Visible = false;
            }
        }

        protected void cmdAdd_Click(object sender, EventArgs e)
        {
            ClearFields();
            ActFlag.Text = "Adding";
            lblError.Text = "";
            lblModalTitle.Text = "Add User Details";
            txtCode.Enabled = true;
            txtCode.BackColor = Color.White;
            ClearFields();
            Panel_search.Visible = false; Panel_entry.Visible = true;

        }





        protected void CreateLog(string thekey, int userid, string modulechanged, string theAction)
        {

            SqlConnection con = new SqlConnection(sConnectionStringHR);
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
            SqlConnection con = new SqlConnection(sConnectionStringHR);
            String cmdString = "select * from smnewuser where 1=1 ";
            if (txtSearchCode.Text.Trim() != "") { cmdString = cmdString + " and code = " + txtSearchCode.Text; }
            if (txtSearchName.Text.Trim() != "") { cmdString = cmdString + " and firstname like '%" + txtSearchName.Text + "%'"; }
            cmdString = cmdString + " order by code";
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
            SqlConnection con = new SqlConnection(sConnectionStringHR);
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
            txtCode.Text = "";
            txtSearchName.Text = "";
            txtFirstName.Text = "";
            txtLastName.Text = "";
            txtSearchName.Text = "";
            txtVISA.Text = "";
            txtemail.Text = "";

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
            //BindData();
        }
        protected void radData_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "CMDEDIT")
            {
                GridDataItem item = (GridDataItem)e.Item;
                ActFlag.Text = "Editing";
                Int32 index = Convert.ToInt32(item["code"].Text);
                Session["ID"] = index;
                FieldToVariable(index);
                ActFlag.Text = "Editing";
                lblModalTitle.Text = "Edit User";
                //ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myModal", "$('#myModal').modal();", true);
                //upModal.Update();
            }
            if (e.CommandName == "CMDDELETE")
            {
                GridDataItem item = (GridDataItem)e.Item;
                int index = Convert.ToInt16(item["ID"].Text);
                SqlConnection con = new SqlConnection(sConnectionStringHR);
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
            if (txtCode.Text == "")
            {
                lblError.Text = "User code can't be empty";
                return;

            }
            Int64 vCode = getNumber(txtCode.Text);
            if (vCode < 10000 || vCode > 20000)
            {
                lblError.Text = "Code must be between 10000 and 20000"; return;

            }
            if (txtFirstName.Text == "")
            {
                lblError.Text = "user name can not be empty"; return;
            }
            if (txtemail.Text == "")
            {
                lblError.Text = "Email can not be empty"; return;
            }
            string thekey = "";
            string flag = "";
            string cmdu = "";
            SqlConnection con = new SqlConnection(sConnectionStringHR);
            SqlCommand cmd = new SqlCommand(cmdu, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "AddIISUsers";
            if (ActFlag.Text == "Adding")
            {

                cmd.Parameters.Add("@flag", SqlDbType.VarChar).Value = "Add";
                cmd.Parameters.Add("@code", SqlDbType.VarChar).Value = txtCode.Text;
                cmd.Parameters.Add("@firstname", SqlDbType.VarChar).Value = txtFirstName.Text;
                cmd.Parameters.Add("@lastname", SqlDbType.VarChar).Value = txtLastName.Text;
                cmd.Parameters.Add("@visa", SqlDbType.VarChar).Value = txtVISA.Text;
                cmd.Parameters.Add("@email", SqlDbType.VarChar).Value = txtemail.Text;
                cmd.Parameters.Add("@profile", SqlDbType.VarChar).Value = radProfile.SelectedValue;
                cmd.Parameters.Add("@contact", SqlDbType.VarChar).Value = txtContact.Text;
                cmd.Parameters.Add("@password", SqlDbType.VarChar).Value = txtPassword.Text;
                flag = "Inserted";
            }
            if (ActFlag.Text == "Editing")
            {
                cmd.Parameters.Add("@flag", SqlDbType.VarChar).Value = "Edit";
                cmd.Parameters.Add("@code", SqlDbType.VarChar).Value = txtCode.Text;
                cmd.Parameters.Add("@firstname", SqlDbType.VarChar).Value = txtFirstName.Text;
                cmd.Parameters.Add("@lastname", SqlDbType.VarChar).Value = txtLastName.Text;
                cmd.Parameters.Add("@visa", SqlDbType.VarChar).Value = txtVISA.Text;
                cmd.Parameters.Add("@email", SqlDbType.VarChar).Value = txtemail.Text;
                cmd.Parameters.Add("@profile", SqlDbType.VarChar).Value = radProfile.SelectedValue;
                cmd.Parameters.Add("@contact", SqlDbType.VarChar).Value = txtContact.Text;
                cmd.Parameters.Add("@password", SqlDbType.VarChar).Value = txtPassword.Text;


            }

            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                //CreateLog(thekey, Convert.ToInt32(Session["CODE"]), "Employee", flag);
                ClearFields();
                Panel_search.Visible = true; Panel_entry.Visible = false;
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
                return;
            }
            finally { con.Close(); }

        }

        protected Int64 getNumber(string vNum)
        {
            Int64 retValue = 0;
            try
            {
                retValue = Convert.ToInt64(vNum);
            }
            catch
            {
                retValue = 0;
            }
            return retValue;
        }


        protected void btnExit_Click(object sender, EventArgs e)
        {
            Panel_entry.Visible = false; Panel_search.Visible = true;
        }
    }
}