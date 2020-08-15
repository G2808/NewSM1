using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web.Configuration;
using System.Web.UI;
using Telerik.Web.UI;

namespace NewSM1
{
    public partial class stockist : System.Web.UI.Page
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
                BindStockistType();
                //Panel_AddEdit.Visible = false; Panel_Search.Visible = true;

            }
            radData.EnableAjaxSkinRendering = true;
        }

        protected void FieldToVariable(string vID)
        {
            String cmdString = "select * from smstockist where stockist_id ='" + vID + "'";
            SqlDataReader reader = getDataReader(cmdString);
            reader.Read();
            if (reader.HasRows)
            {
                txtCode.Text = reader["stockist_id"].ToString();
                txtCode.BackColor = Color.LightGray;
                txtName.Text = reader["stockist_name"].ToString();
                txtCity.Text = reader["stockist_city"].ToString();
                radType.SelectedValue = reader["stockist_type"].ToString();
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
            lblModalTitle.Text = "Add Stockist";
            txtCode.Enabled = true;
            txtCode.ReadOnly = false;
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
            String cmdString = "select s.stockist_id,s.stockist_name,s.stockist_city, st.typedesc stockist_type from smstockist s  " +
                " left join smtype st on st.type=s.stockist_type";
            if (txtSearchCode.Text.Trim() != "") { cmdString = cmdString + " and s.stockist_id like '%" + txtSearchCode.Text + "%'"; }
            if (txtSearchName.Text.Trim() != "") { cmdString = cmdString + " and s.stockist_name like '%" + txtSearchName.Text + "%'"; }
            if (txtSearchCity.Text.Trim() != "") { cmdString = cmdString + " and s.stockist_city like '%" + txtSearchCity.Text + "%'"; }
            if (txtSearchType.Text.Trim() != "") { cmdString = cmdString + " and st.typedesc like '%" + txtSearchCity.Text + "%'"; }

            cmdString = cmdString + " order by s.stockist_id";
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


        protected void BindStockistType()
        {
            DataTable dt;
            dt = getDataTable("select type,typedesc from smtype  order by type");

            radType.DataSource = dt;
            radType.DataTextField = "typedesc";
            radType.DataValueField = "type";
            radType.DataBind();
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
            catch (Exception ex) { }
            finally { }
            return dr;
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
            txtSearchCode.Text = "";
            txtSearchName.Text = "";
            txtCity.Text = "";
            txtName.Text = "";
            txtCode.Text = "";
            txtName.Text = "";


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
                string index = item["stockist_id"].Text;
                Session["STOCKIST_ID"] = index;
                FieldToVariable(index);
                ActFlag.Text = "Editing";
                lblModalTitle.Text = "Edit Stockist";
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
            if (txtCode.Text == "")
            {
                lblError.Text = "Stockist code can't be blank"; return;
            }
            if (txtName.Text == "")
            {
                lblError.Text = "Stockist Name Can't be empty"; return;
            }
            string thekey = "";
            string flag = "";
            string cmdu = "";
            SqlConnection con = new SqlConnection(sConnectionString);

            SqlCommand cmd = new SqlCommand(cmdu, con);

            if (ActFlag.Text == "Adding")
            {
                cmdu = "insert into smstockist(stockist_id,stockist_name,stockist_city,stockist_type) " +
                           "values (@stockist_id,@stockist_name,@stockist_city,@stockist_type)";
                cmd.Parameters.Add("@stockist_id", SqlDbType.VarChar).Value = txtCode.Text;
                cmd.Parameters.Add("@stockist_nam", SqlDbType.VarChar).Value = txtName.Text;
                cmd.Parameters.Add("@stockist_city", SqlDbType.Int).Value = txtCity.Text;
                cmd.Parameters.Add("@subgroupid", SqlDbType.Int).Value = radType.SelectedValue;

                flag = "Inserted";
            }
            if (ActFlag.Text == "Editing")
            {
                cmdu = "update smstockist set stockist_name=@stockist_name,stockist_city=@stockist_city,stockist_type=@stockist_type where stockist_id='" + Session["STOCKIST_ID"].ToString() + "'";
                cmd.Parameters.Add("@stockist_name", SqlDbType.VarChar).Value = txtName.Text;
                cmd.Parameters.Add("@stockist_city", SqlDbType.Int).Value = txtCity.Text;
                cmd.Parameters.Add("@stockist_type", SqlDbType.Int).Value = radType.SelectedValue.ToString();
            }

            try
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = cmdu;
                con.Open();
                cmd.ExecuteNonQuery();
                //CreateLog(thekey, Convert.ToInt32(Session["CODE"]), "Employee", flag);
                BindData();
                ClearFields();
                txtCode.Enabled = false;
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