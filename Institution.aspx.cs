using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web.Configuration;
using System.Web.UI;
using Telerik.Web.UI;

namespace NewSM1
{
    public partial class Institution : System.Web.UI.Page
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
                BindGroup();
                BindSubGroup();
                //Panel_AddEdit.Visible = false; Panel_Search.Visible = true;

            }
            radData.EnableAjaxSkinRendering = true;
        }





        protected void FieldToVariable(Int32 vID)
        {
            String cmdString = "select * from sminstitution where inst_id =" + vID;
            SqlDataReader reader = getDataReader(cmdString);
            reader.Read();
            if (reader.HasRows)
            {
                txtCode.Text = reader["INST_ID"].ToString();
                txtCode.BackColor = Color.LightGray;
                txtName.Text = reader["descr"].ToString();
                txtFullName.Text = reader["inst_fullname"].ToString();
                radGroup.SelectedValue = reader["groupid"].ToString();
                radSubGroup.SelectedValue = reader["subgroupid"].ToString();

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
            lblModalTitle.Text = "Add Institution";
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
            Response.Redirect("bannerHO.aspx");
        }

        protected void cmdClear_Click(object sender, EventArgs e)
        {
            ClearFields();
        }





        protected void BindData()
        {
            SqlConnection con = new SqlConnection(sConnectionString);
            String cmdString = "select i.inst_id,i.descr,i.inst_fullname,ig.name igroup,isg.descr subgroup from sminstitution i  " +
                    " left join sminstitutiongroup ig on ig.id=i.groupid " +
                    " left join sminstitutionsubgroup isg on isg.id=i.subgroupid  where 1=1 ";
            if (txtSearchCode.Text.Trim() != "") { cmdString = cmdString + " and descr like '%" + txtSearchCode.Text + "%'"; }
            if (txtSearchName.Text.Trim() != "") { cmdString = cmdString + " and inst_fullname like '%" + txtSearchName.Text + "%'"; }
            cmdString = cmdString + " order by i.descr";
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

        protected void BindGroup()
        {
            DataTable dt;
            dt = getDataTable("select id,name from smInstitutiongroup union all select 0,'' order by id");

            radGroup.DataSource = dt;
            radGroup.DataTextField = "name";
            radGroup.DataValueField = "id";
            radGroup.DataBind();
        }

        protected void BindSubGroup()
        {
            DataTable dt;
            dt = getDataTable("select id,descr from smInstitutionsubgroup union all select 0,'' order by id");

            radSubGroup.DataSource = dt;
            radSubGroup.DataTextField = "descr";
            radSubGroup.DataValueField = "id";
            radSubGroup.DataBind();
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
            txtSearchGroup.Text = "";
            txtSearchName.Text = "";
            txtName.Text = "";
            txtSearchSubGroup.Text = "";
            txtSearchGroup.Text = "";



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
                Int32 index = Convert.ToInt32(item["INST_ID"].Text);
                Session["INSTITUTION_ID"] = index;
                FieldToVariable(index);
                ActFlag.Text = "Editing";
                lblModalTitle.Text = "Edit Institution";
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
            if (txtName.Text == "")
            {
                lblError.Text = "Short name Can't be empty"; return;
            }
            if (txtFullName.Text == "")
            {
                lblError.Text = "Name Can't be empty"; return;
            }
            string thekey = "";
            string flag = "";
            string cmdu = "";
            SqlConnection con = new SqlConnection(sConnectionString);

            SqlCommand cmd = new SqlCommand(cmdu, con);

            if (ActFlag.Text == "Adding")
            {
                cmdu = "insert into sminstitution(descr,inst_fullname,groupid,subgroupid) " +
                           "values (@descr,@inst_fullname,@groupid,@subgroupid)";
                cmd.Parameters.Add("@descr", SqlDbType.VarChar).Value = txtName.Text;
                cmd.Parameters.Add("@inst_fullname", SqlDbType.VarChar).Value = txtFullName.Text;
                cmd.Parameters.Add("@groupid", SqlDbType.Int).Value = radGroup.SelectedValue;
                cmd.Parameters.Add("@subgroupid", SqlDbType.Int).Value = radSubGroup.SelectedValue;

                flag = "Inserted";
            }
            if (ActFlag.Text == "Editing")
            {
                cmdu = "update sminstitution set descr=@descr,inst_fullname=@inst_fullname,groupid=@groupid,subgroupid=@subgroupid where ID=" + Session["INSTITUTION_ID"];
                cmd.Parameters.Add("@descr", SqlDbType.VarChar).Value = txtName;
                cmd.Parameters.Add("@inst_fullname", SqlDbType.VarChar).Value = txtFullName.Text;
                cmd.Parameters.Add("@groupid", SqlDbType.Int).Value = radGroup.SelectedValue.ToString();
                cmd.Parameters.Add("@subgroupid", SqlDbType.Int).Value = radSubGroup.SelectedValue.ToString();
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