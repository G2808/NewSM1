using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web.Configuration;
using System.Web.UI;
using Telerik.Web.UI;

namespace NewSM1
{
    public partial class ITEM : System.Web.UI.Page
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
                //Panel_AddEdit.Visible = false; Panel_Search.Visible = true;

            }
            radData.EnableAjaxSkinRendering = true;
        }





        protected void FieldToVariable(string vID)
        {
            String cmdString = "select * from smitem where item_code ='" + vID + "'";
            SqlDataReader reader = getDataReader(cmdString);
            reader.Read();
            if (reader.HasRows)
            {
                txtCode.Text = reader["ITEM_CODE"].ToString();
                txtCode.BackColor = Color.LightGray;
                txtCode.Enabled = false;
                txtName.Text = reader["item_name"].ToString();
                txtMolecule.Text = reader["item_molecule"].ToString();
                txtSEQ.Text = reader["seq"].ToString();
                radGroup.SelectedValue = reader["item_group"].ToString();
                reader.Close();
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myModal", "$('#myModal').modal();", true);

            }
        }

        protected void cmdAdd_Click(object sender, EventArgs e)
        {
            ClearFields();
            ActFlag.Text = "Adding";
            lblError.Text = "";
            lblModalTitle.Text = "Add Product";
            txtCode.Enabled = true;
            txtCode.BackColor = Color.White;
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myModal", "$('#myModal').modal({backdrop: 'static', keyboard: false});", true);

            //ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myModal", "$('#myModal').modal();", true);
            //upModal.Update();
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
            String cmdString = "select * from smitem where 1=1 ";
            if (txtSearchCode.Text.Trim() != "") { cmdString = cmdString + " and item_code like '" + txtCode.Text + "%'"; }
            if (txtSearchName.Text.Trim() != "") { cmdString = cmdString + " and item_name like '" + txtName.Text + "%'"; }
            cmdString = cmdString + " order by item_code";
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

        protected void BindGroup()
        {
            DataTable dt;
            dt = getDataTable("select CODE,GROUPNAME from SMITEMGROUP union all select 0,'' order by CODE");

            radGroup.DataSource = dt;
            radGroup.DataTextField = "GROUPNAME";
            radGroup.DataValueField = "CODE";
            radGroup.DataBind();
        }

        protected DataTable getDataTable(String cmdString)
        {
            SqlConnection con = new SqlConnection(sConnectionString);
            SqlCommand cmd = new SqlCommand(cmdString, con);
            DataTable ds = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);

            try
            {
                con.Open();
                adapter.Fill(ds);
            }
            catch (Exception ex) { }
            finally { con.Close(); }
            return ds;
        }


        protected void ClearFields()
        {
            txtCode.Text = "";
            txtSearchCode.Text = "";
            txtSearchName.Text = "";
            txtName.Text = "";
            //txtSname.Text = "";
            //txtName.Text = "";

        }

        protected void cmdExitAdd_Click(object sender, EventArgs e)
        {
            ClearFields();
            ActFlag.Text = "";
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
                string index = item["ITEM_CODE"].Text;
                Session["ITEM_CODE"] = index;
                FieldToVariable(index);
                ActFlag.Text = "Editing";
                lblModalTitle.Text = "Edit Product";
                //ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myModal", "$('#myModal').modal();", true);
                //upModal.Update();
            }
            if (e.CommandName == "CMDDELETE")
            {
                GridDataItem item = (GridDataItem)e.Item;
                int index = Convert.ToInt16(item["CODE"].Text);
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
            if (txtCode.Text == "")
            {
                lblError.Text = "Code Can't be empty"; return;
            }
            if (txtName.Text == "")
            {
                lblError.Text = "Name Can't be empty"; return;
            }
            string thekey = "";
            string flag = "";
            string cmdu = "";
            SqlConnection con = new SqlConnection(sConnectionString);
            SqlCommand cmd = new SqlCommand(cmdu, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "AddEditProduct";
            if (ActFlag.Text == "Adding")
            {

                cmd.Parameters.Add("@flag", SqlDbType.VarChar).Value = "Add";
                flag = "Inserted";
            }
            if (ActFlag.Text == "Editing")
            {
                cmd.Parameters.Add("@flag", SqlDbType.VarChar).Value = "Edit";
            }
            cmd.Parameters.Add("@item_code", SqlDbType.VarChar).Value = txtCode.Text;
            cmd.Parameters.Add("@item_name", SqlDbType.VarChar).Value = txtName.Text;
            cmd.Parameters.Add("@item_molecule", SqlDbType.VarChar).Value = txtMolecule.Text;
            cmd.Parameters.Add("@item_group", SqlDbType.VarChar).Value = radGroup.SelectedValue;
            cmd.Parameters.Add("@seq", SqlDbType.VarChar).Value = txtSEQ.Text;
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