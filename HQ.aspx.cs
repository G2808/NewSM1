using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using Telerik.Web.UI;

namespace NewSM1
{

    public partial class HQ : System.Web.UI.Page
    {
        private string sConnectionString = ConfigurationManager.ConnectionStrings["ConnectionStringsm"].ConnectionString.ToString();


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                Panel_AddEdit.Visible = false; Panel_Search.Visible = true;
                radGrid.MasterTableView.NoMasterRecordsText = "No records to Display";
                radGrid.ExportSettings.HideStructureColumns = true;
                radGrid.ExportSettings.SuppressColumnDataFormatStrings = false;
                radGrid.ExportSettings.ExportOnlyData = true;
                radGrid.ExportSettings.IgnorePaging = true;
                radGrid.ExportSettings.Excel.Format = GridExcelExportFormat.ExcelML;
                Panel_Search.Visible = true;
                Panel_AddEdit.Visible = false;


            }
            //lblError.Text = "";
            radGrid.EnableAjaxSkinRendering = true;
        }



        protected void FieldToVariable(int vID)
        {
            String cmdString = "select * from smhq where headquarter_id =" + vID;
            DataTable ds = getDataTable(cmdString);
            if (ds.Rows.Count > 0)
            {
                txtID.Text = ds.Rows[0]["headquarter_id"].ToString();
                txtName.Text = ds.Rows[0]["headquarter_name"].ToString();
                txtID.Enabled = false;
            }
        }

        protected void cmdAdd_Click(object sender, EventArgs e)
        {
            Panel_Search.Visible = false;
            Panel_AddEdit.Visible = true;
            txtID.Enabled = true;
            ActFlag.Text = "Adding";
        }


        protected void cmdUpdate_Click(object sender, EventArgs e)
        {
            if (txtName.Text.Trim() == "") { lblErrorAdd.Text = " Name can't be blank"; return; }
            string cmdu = "";
            string thekey = "";
            string flag = "";
            SqlConnection con = new SqlConnection(sConnectionString);
            SqlCommand cmd = new SqlCommand(cmdu, con);
            if (ActFlag.Text == "Adding")
            {
                cmdu = "insert into smhq(headquarter_name) values (@headquarter_name) ";
                flag = "Inserted";
                //lblError.Text = "Record Inserted";
            }
            if (ActFlag.Text == "Editing")
            {
                cmdu = "update smhq set headquarter_name = @headquarter_name  where id=" + Session["IDHQ"];
                flag = "Edited";
            }
            cmd.Parameters.Add("@headquarter_name", SqlDbType.VarChar).Value = txtName.Text;
            cmd.CommandType = CommandType.Text;

            try
            {
                con.Open();
                cmd.CommandText = cmdu;
                cmd.ExecuteNonQuery();
                BindData();
                Panel_Search.Visible = true; Panel_AddEdit.Visible = false;
                ClearFields();
            }
            catch
            {

                //lblErrorAdd.Text = "Error Inserting/Updating Record";
                return;
            }
            finally { con.Close(); }

            ClearFields();

        }



        protected void cmdSearch_Click(object sender, EventArgs e)
        {
            radGrid.Visible = true;
            BindData();
        }


        protected void cmdExit_Click(object sender, EventArgs e)
        {
            Response.Redirect("bannerHO.aspx");
        }

        protected void cmdClear_Click(object sender, ImageClickEventArgs e)
        {
            ClearFields();
        }





        protected void BindData()
        {
            SqlConnection con = new SqlConnection(sConnectionString);
            String cmdString = "select * from  smhq where 1=1 ";
            if (txtSearchCode.Text.Trim() != "") { cmdString = cmdString + " and headquarter_id = " + txtSearchCode.Text; }
            if (txtSearchStatus.Text.Trim() != "") { cmdString = cmdString + " and headquarter_name like '%" + txtSearchStatus.Text + "%'"; }
            cmdString = cmdString + " order by headquarter_id";
            try
            {

                radGrid.DataSource = getDataTable(cmdString);
                radGrid.DataBind();
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
            finally { con.Close(); }
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
            catch { }
            finally { con.Close(); }
            return ds;
        }



        protected void ClearFields()
        {
            txtSearchCode.Text = "";
            txtName.Text = "";
            lblErrorAdd.Text = "";
        }

        protected void radGrid_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void cmdExitAdd_Click(object sender, EventArgs e)
        {
            ClearFields();
            Panel_Search.Visible = true;
            Panel_AddEdit.Visible = false;
            ActFlag.Text = "";
            lblErrorAdd.Text = "";

        }
        protected void radGrid_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            BindData();
        }

        protected void radGrid_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "CMDEDIT")
            {
                GridDataItem item = (GridDataItem)e.Item;
                ActFlag.Text = "Editing";
                int index = Convert.ToInt16(item["HEADQUARTER_ID"].Text);
                Session["IDHQ"] = index;
                Panel_AddEdit.Visible = true; Panel_Search.Visible = false;
                FieldToVariable(index);

            }
        }
    }
}



