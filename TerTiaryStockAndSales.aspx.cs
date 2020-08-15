using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace NewSM
{
    public partial class TerTiaryStockAndSales : System.Web.UI.Page
    {
        private string sConnectionString = WebConfigurationManager.ConnectionStrings["ConnectionString1"].ConnectionString;
        private string sConnectionString1 = WebConfigurationManager.ConnectionStrings["ConnectionString1"].ConnectionString;
        private bool vlocked;
        private bool isEditMode = true;
        private SqlDataSource SqlDataSource1 = new SqlDataSource();

        protected void Page_Load(object sender, EventArgs e)
        {
            lblError.Text = "";
            if (!IsPostBack)
            {
                txtUser.Text = Session["EMPNAME"].ToString();
                BindHQ();
                // BindCustomer()
                bindYear();
                bindMonth();
                BindType();
                BindCustomer();
                Panel_Search.Visible = true; Panel_AddEdit.Visible = false;

            }
        }

        protected void ShowPopup(object sender, EventArgs e)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "MyPopup", "ShowPopup();", true);

        }

        protected void txtCode_TextChanged(object sender, EventArgs e)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "MyPopup", "ShowPopup();", true);

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            vlocked = chkMonth(cmbMonth.SelectedValue, cmbYear.Text);
            //ResetTotal();

            binddata();
        }

        protected void binddata()
        {
            SqlConnection con = new SqlConnection(sConnectionString);
            if (con.State == ConnectionState.Open)
                con.Close();
            SqlCommand cmd = new SqlCommand("BSgsDisplayTertiary", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@IsAdmin", SqlDbType.VarChar));
            cmd.Parameters["@IsAdmin"].Value = Session["IsAdmin"].ToString();

            cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.VarChar));
            cmd.Parameters["@UserId"].Value = Session["theUserID"];

            cmd.Parameters.Add(new SqlParameter("@City", SqlDbType.VarChar));
            cmd.Parameters["@City"].Value = cmbCity.SelectedValue;

            cmd.Parameters.Add(new SqlParameter("@Year", SqlDbType.VarChar));
            cmd.Parameters["@Year"].Value = cmbYear.SelectedValue;

            cmd.Parameters.Add(new SqlParameter("@Month", SqlDbType.VarChar));
            cmd.Parameters["@Month"].Value = cmbMonth.SelectedValue;

            cmd.Parameters.Add(new SqlParameter("@Stockist", SqlDbType.VarChar));
            cmd.Parameters["@Stockist"].Value = cmbParty.SelectedValue;

            cmd.Parameters.Add(new SqlParameter("@ttype", SqlDbType.Float));
            cmd.Parameters["@ttype"].Value = Convert.ToInt16(cmbType.SelectedValue);

            cmd.Parameters.Add(new SqlParameter("@HQ", SqlDbType.Int));
            cmd.Parameters["@HQ"].Value = Convert.ToInt64(cmbHQ.SelectedValue);
            con.Open();
            SqlDataAdapter adaptor = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adaptor.Fill(dt);
            con.Close();
            radData.DataSource = dt;
            radData.DataBind();
        }



        protected void btnNew_Click(object sender, EventArgs e)
        {
            //ClearFields();
            if (cmbYear.Text == "")
            {
                lblError.Text = "Year is Blank";
                return;
            }
            if (cmbMonth.Text == "")
            {
                lblError.Text = "Month is Blank";
                return;
            }
            if (cmbParty.Text.Trim() == "")
            {
                lblError.Text = "Please Select Party";
                cmbParty.Focus(); return;
            }
            Panel_AddEdit.Visible = true; Panel_Search.Visible = false;
            lblStockist.Text = cmbParty.SelectedItem.Text + "(" + cmbHQ.SelectedItem.Text + ")";
            lblMonthYear.Text = cmbYear.SelectedItem.Text + " " + cmbMonth.SelectedItem.Text;
            bindItemsAdd();

            GenerateTable(6, 20);
        }

        protected void bindItemsAdd()
        {
            //SqlConnection con = new SqlConnection(sConnectionString);
            //if (con.State == ConnectionState.Open)
            //    con.Close();
            //SqlCommand cmd = new SqlCommand("BSAddNewTertiary", con);
            //cmd.CommandType = CommandType.StoredProcedure;


            //cmd.Parameters.Add(new SqlParameter("@tyear", SqlDbType.VarChar));
            //cmd.Parameters["@tyear"].Value = cmbYear.SelectedValue;

            //cmd.Parameters.Add(new SqlParameter("@tmonth", SqlDbType.VarChar));
            //cmd.Parameters["@tmonth"].Value = cmbMonth.SelectedValue;

            //cmd.Parameters.Add(new SqlParameter("@cust_code", SqlDbType.VarChar));
            //cmd.Parameters["@cust_code"].Value = cmbParty.SelectedValue;
            //if (txtSearchCode.Text.Trim() != "")
            //{
            //    cmd.Parameters.Add(new SqlParameter("@item_code", SqlDbType.VarChar));
            //    cmd.Parameters["@item_code"].Value = txtSearchCode.Text;
            //}
            //cmd.Parameters.Add(new SqlParameter("@ttype", SqlDbType.Float));
            //cmd.Parameters["@ttype"].Value = Convert.ToDecimal(cmbType.SelectedValue);
            //con.Open();

            //SqlDataAdapter adaptor = new SqlDataAdapter(cmd);
            //DataTable dt = new DataTable();
            //adaptor.Fill(dt);
            //con.Close();
            isEditMode = true;
            //SqlDataSource1.ID = "SqlDataSource1";
            //this.Page.Controls.Add(SqlDataSource1);
            //SqlDataSource1.SelectParameters.Clear();
            //SqlDataSource1.ConnectionString = sConnectionString;
            //SqlDataSource1.SelectCommand = "BSDisplayTertiary";
            //SqlDataSource1.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
            SqlDataSource2.SelectParameters.Clear();
            SqlDataSource2.SelectParameters.Add("tyear", cmbYear.SelectedValue);
            SqlDataSource2.SelectParameters.Add("tmonth", cmbMonth.SelectedValue);
            SqlDataSource2.SelectParameters.Add("cust_code", cmbParty.SelectedValue);
            SqlDataSource2.SelectParameters.Add("ttype", cmbType.SelectedValue);
            if (txtSearchCode.Text.Trim() != "")
            {
                SqlDataSource2.SelectParameters.Add("item_code", txtSearchCode.Text);
            }
            SqlDataSource2.DataBind();
            RadGrid1.Rebind();
            //SqlDataSource1.SelectParameters.Add("@tyear", DbType.Int32, cmbYear.SelectedValue.ToString());
            //SqlDataSource1.SelectParameters.Add("@tmonth", DbType.Int32, cmbMonth.SelectedValue.ToString());
            //SqlDataSource1.SelectParameters.Add("@cust_code", DbType.String, cmbParty.SelectedValue.ToString());
            //SqlDataSource1.SelectParameters.Add("@ttype", DbType.Decimal, cmbType.SelectedValue.ToString());
            //SqlDataSource1.DataBind();
            //RadGrid1.DataSource = SqlDataSource1;
            //RadGrid1.DataBind();
            //SqlDataSource2.DataBind();
            //RadGrid1.DataBind();
        }

        protected void cmdExit_Click(object sender, EventArgs e)
        {

        }

        protected void cmbHQ_SelectedIndexChanged1(object sender, EventArgs e)
        {
            BindCity();
            BindCustomer();
        }

        protected void cmbCity_SelectedIndexChanged1(object sender, EventArgs e)
        {

        }

        protected void radData_DataBound(object sender, EventArgs e)
        {

        }

        protected void fillmonth()
        {
            Int16 i;
            for (i = 1; i <= 12; i++)
                cmbMonth.Items.Add(System.Convert.ToString(i));

            cmbYear.Text = System.Convert.ToString(DateTime.Now.Year);
            cmbMonth.Text = System.Convert.ToString(DateTime.Now.Year);
        }


        protected void BindCustomer()
        {
            if (cmbHQ.SelectedItem.Text.Trim() == "")
            {
                cmbParty.Items.Clear();
                return;
            }
            SqlConnection con = new SqlConnection(sConnectionString);
            if (con.State == System.Data.ConnectionState.Closed)
                con.Close();
            con.Open();
            string cmdDatalist = " select stk.stockist_id,stk.stockist_name" + " from smstockist stk " + " inner join smlinkhqstockist lhs on lhs.stockist_id=stk.stockist_id ";
            if (Session["IsAdmin"].ToString() == "N")
                cmdDatalist = cmdDatalist + " inner join smlinkhqemp lhe on lhe.hqid = lhs.headquarter_id" + " where lhe.empid='" + txtUserID.Text + "' and lhe.hqid=" + cmbHQ.SelectedValue;
            else if (cmbHQ.SelectedItem.Text != "")
                cmdDatalist = cmdDatalist + " where lhs.headquarter_id=" + cmbHQ.SelectedValue;
            // If cmbType.SelectedItem.Text.ToString <> "" Then
            cmdDatalist = cmdDatalist + " and stk.stockist_type=" + cmbType.SelectedValue;
            // End If
            if (cmbCity.Text.Trim() != "")
                cmdDatalist = cmdDatalist + " and stk.stockist_city='" + cmbCity.SelectedItem.Text + "'";
            SqlCommand cmdCust = new SqlCommand(cmdDatalist, con);
            cmbParty.Items.Clear();
            cmbParty.DataSource = cmdCust.ExecuteReader();
            cmbParty.DataTextField = "stockist_name";
            cmbParty.DataValueField = "stockist_id";
            cmbParty.DataBind();
            cmbParty.Items.Add("");
            cmbParty.SelectedValue = "";
            con.Close();
        }

        protected void BindCity()
        {
            if (cmbHQ.SelectedItem.Text.Trim() == "")
            {
                cmbCity.Items.Clear();
                return;
            }
            SqlConnection con = new SqlConnection(sConnectionString);
            if (con.State == System.Data.ConnectionState.Closed)
                con.Close();
            con.Open();

            string cmdDatalist = "select distinct stk.stockist_city from smstockist stk " + " inner join smlinkhqstockist ls on ls.stockist_id=stk.stockist_id " + " where ls.headquarter_id=" + cmbHQ.SelectedItem.Value + " order by stk.stockist_city ";

            SqlCommand cmdDept = new SqlCommand(cmdDatalist, con);
            cmbCity.Items.Clear();

            cmbCity.DataSource = cmdDept.ExecuteReader();
            cmbCity.DataTextField = "stockist_city";
            cmbCity.DataValueField = "stockist_city";
            cmbCity.DataBind();
            cmbCity.Items.Add("");
            cmbCity.SelectedValue = "";
            con.Close();
        }

        protected void BindType()
        {
            SqlConnection con = new SqlConnection(sConnectionString);
            if (con.State == System.Data.ConnectionState.Closed)
                con.Close();
            con.Open();

            string cmdDatalist = "select tt.type,tt.typedesc from smtype tt";
            if (Session["IsAdmin"].ToString() == "N")
                cmdDatalist = cmdDatalist + " inner join smlinkemptype smt on smt.typeid=tt.type where smt.empid='" + txtUserID.Text + "'";

            SqlCommand cmdDept = new SqlCommand(cmdDatalist, con);
            cmbType.Items.Clear();

            cmbType.DataSource = cmdDept.ExecuteReader();
            cmbType.DataTextField = "typedesc";
            cmbType.DataValueField = "type";

            cmbType.DataBind();
            con.Close();
        }

        protected void BindHQ()
        {
            SqlConnection con = new SqlConnection(sConnectionString);
            if (con.State == System.Data.ConnectionState.Closed)
                con.Close();
            con.Open();
            string cmdDatalist;
            if (Session["IsAdmin"].ToString() == "Y")
                cmdDatalist = "select hq.headquarter_id,hq.headquarter_name from smhq hq";
            else
            {
                cmdDatalist = "select distinct hq.headquarter_id,hq.headquarter_name from smhq hq " + " inner join smlinkhqemp lhq on lhq.hqid=hq.headquarter_id where lhq.empid='" + txtUserID.Text + "' ";
                cmdDatalist = cmdDatalist + " order by hq.headquarter_name";
            }
            SqlCommand cmdDept = new SqlCommand(cmdDatalist, con);
            cmbHQ.Items.Clear();

            cmbHQ.DataSource = cmdDept.ExecuteReader();
            cmbHQ.DataTextField = "headquarter_name";
            cmbHQ.DataValueField = "headquarter_id";
            cmbHQ.DataBind();
            cmbHQ.Items.Add("");
            cmbHQ.SelectedValue = "0";
            con.Close();
        }
        protected void bindMonth()
        {
            if (cmbYear.Text == "")
                return;
            SqlConnection cn = new SqlConnection(sConnectionString);
            if (cn.State == ConnectionState.Open)
                cn.Close();
            cn.Open();
            string sqlMonth = "select distinct cast(themonth as numeric),monthsmall,themonth," + " cast(themonth as numeric) month, cast(theyear as numeric)" + " from smPeriod where theyear='" + cmbYear.Text + "' order by cast(theyear as numeric) desc,cast(themonth as numeric)  desc";

            // Dim sqlMonth As String = "select themonth,monthsmall,cast(themonth as numeric) from smPeriod where theyear='" & cmbYear.Text & "' order by cast(themonth as numeric) desc"
            cmbMonth.Items.Clear();
            SqlCommand cmdMonth = new SqlCommand(sqlMonth, cn);
            cmbMonth.DataSource = cmdMonth.ExecuteReader();
            cmbMonth.DataTextField = "Monthsmall";
            cmbMonth.DataValueField = "theMonth";
            cmbMonth.DataBind();
            cn.Close();
        }

        protected void bindYear()
        {
            SqlConnection cn = new SqlConnection(sConnectionString);
            if (cn.State == System.Data.ConnectionState.Open)
                cn.Close();
            cn.Open();
            string sqlYear = "select distinct theyear,cast(theyear as numeric) from smPeriod order by cast(theyear as numeric) desc";
            cmbMonth.Items.Clear();
            SqlCommand cmdYear = new SqlCommand(sqlYear, cn);
            cmbYear.DataSource = cmdYear.ExecuteReader();
            cmbYear.DataTextField = "theyear";
            cmbYear.DataValueField = "theyear";
            cmbYear.DataBind();
            cn.Close();
        }
        private bool chkMonth(string vMonth, string vyear)
        {
            SqlConnection con = new SqlConnection(sConnectionString1);
            if (con.State == ConnectionState.Open)
                con.Close();
            string cmdSelect = "select locked from smPeriod where theyear='" + vyear + "' and themonth='" + vMonth + "'";
            SqlCommand cmd = new SqlCommand(cmdSelect, con);
            SqlDataAdapter adaptor = new SqlDataAdapter(cmd);
            con.Open();
            SqlDataReader reader;
            reader = cmd.ExecuteReader();
            reader.Read();
            if (reader.HasRows)
            {
                if (reader["locked"].ToString() == "Y")
                    return true;
                else
                    return false;
            }
            else
                return false;

        }

        protected void cmdSearchItem_Click(object sender, EventArgs e)
        {
            bindItemsAdd();

        }

        protected void RadGrid1_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "ADDDATA")
            {
                GridDataItem item = (GridDataItem)e.Item;
                ActFlag.Text = "Editing";
                //txtCode.Text = item["ID"].Text;
                txtProduct.Text = item["ITEM_CODE"].Text;
                txtOpening.Text = item["OPENING"].Text;
                txtPurchase.Text = item["PURCHASE"].Text;
                txtSales.Text = item["SALES"].Text; ;
                txtClosing.Text = item["CLOSING"].Text; ;
                txtParty.Text = cmbParty.SelectedItem.Text;
                txtHQ.Text = cmbHQ.SelectedItem.Text;
                //string index = item["ITEM_CODE"].Text;
                //Session["ITEM_CODE"] = index;
                lblErrorModal.Text = "";
                ActFlag.Text = "Editing";
                vlocked = chkMonth(cmbMonth.SelectedValue, cmbYear.Text);
                if (chkMonth(cmbMonth.SelectedValue, cmbYear.Text))
                {
                    lblItemError.Text = "This month is locked !! you can only view";
                    return;
                }
                decimal vClosing = Convert.ToDecimal(txtOpening.Text) + Convert.ToDecimal(txtPurchase.Text) - Convert.ToDecimal(txtSales.Text);
                if (vClosing <= 0)
                {
                    lblItemError.Text = "No Stock available for Sale";
                    return;
                }

                Panel_Search.Visible = false; Panel_AddEdit.Visible = false; Panel_Item.Visible = true;

            }

        }

        protected void Submit_Click(object sender, EventArgs e)
        {
            decimal vClosing = Convert.ToDecimal(txtOpening.Text) + Convert.ToDecimal(txtPurchase.Text) - Convert.ToDecimal(txtSales.Text);
            if (vClosing < 0)
            {
                lblErrorModal.Text = "No stock for sales quantity " + txtSales.Text;
                return;
            }
            else
            {
                UpdateTertiary();
            }
        }

        protected void UpdateTertiary()
        {
            double clStock;
            string cmdu;
            DateTime vDate;
            vDate = Convert.ToDateTime(cmbYear.Text.Trim() + "-" + cmbMonth.Text.Trim() + "-01");
            clStock = Convert.ToDouble(txtOpening.Text) + Convert.ToDouble(txtPurchase.Text) - Convert.ToDouble(txtSales.Text);
            if (clStock < 0)
            {
                lblErrorModal.Text = "Can't Update Negative stock for " + txtProduct.Text;
                return;
            }
            Int16 vtype;
            string vcity;

            if (Convert.ToInt16(cmbType.SelectedValue) == 0)
            {
                vtype = Convert.ToInt16(getCustType(cmbParty.SelectedValue.ToString()));
            }
            else
            {
                vtype = Convert.ToInt16(cmbType.SelectedValue);
            }
            if (cmbCity.Text == "")
            {
                vcity = getCity(cmbParty.SelectedValue.ToString());
            }
            else
            { vcity = cmbCity.SelectedValue; }
            //If record exist delete them for the stockist
            string queryDel = " delete from smtertiary  where  year='" + cmbYear.Text + "' and cust_code='" + cmbParty.SelectedValue + "'" +
            " and month='" + cmbMonth.Text + "' and HQ_id=" + cmbHQ.SelectedValue.ToString() + " and type=" + cmbType.SelectedValue;
            SqlConnection conDel = new SqlConnection(sConnectionString);
            SqlCommand cmdDel = new SqlCommand(queryDel, conDel);
            try
            {
                conDel.Open();
                cmdDel.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

            }
            finally { conDel.Close(); }


            if (Convert.ToDouble(txtSales.Text) != 0 || Convert.ToDouble(clStock) != 0)
            {
                cmdu = "insert into smtertiary (cust_code,item_code,year,month,quantity,hq_id,city,type,user_add,add_date,stock,tran_date)" +
                " values (@cust_code,@item_code,@year,@month,@quantity,@hq_id,@city,@type,@user_add,getdate(),@stock,@tran_date)";
                SqlConnection con = new SqlConnection(sConnectionString);
                SqlCommand cmd = new SqlCommand(cmdu, con);
                cmd.Parameters.Add("@cust_code", SqlDbType.Text).Value = cmbParty.SelectedValue;
                cmd.Parameters.Add("@item_code", SqlDbType.Text).Value = txtProduct.Text.Trim();
                cmd.Parameters.Add("@year", SqlDbType.Int).Value = cmbYear.SelectedValue;
                cmd.Parameters.Add("@month", SqlDbType.Int).Value = cmbMonth.SelectedValue;
                cmd.Parameters.Add("@quantity", SqlDbType.Float).Value = Convert.ToDouble(txtSales.Text);
                cmd.Parameters.Add("@hq_id", SqlDbType.BigInt).Value = cmbHQ.SelectedValue;
                cmd.Parameters.Add("@city", SqlDbType.Text).Value = vcity;
                cmd.Parameters.Add("@type", SqlDbType.SmallInt).Value = vtype;
                cmd.Parameters.Add("@user_add", SqlDbType.Text).Value = txtUser.Text;
                cmd.Parameters.Add("@stock", SqlDbType.Float).Value = Convert.ToDecimal(txtClosing.Text);
                cmd.Parameters.Add("@tran_date", SqlDbType.SmallDateTime).Value = Convert.ToDateTime(vDate);
                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    bindItemsAdd();
                    binddata();
                    Panel_AddEdit.Visible = true; Panel_Search.Visible = false; Panel_Item.Visible = false;
                }
                catch (Exception ex)
                {
                    lblErrorModal.Text = ex.Message;
                }
                finally { con.Close(); }


            }

        }

        protected string getCustType(string vCustCode)
        {
            SqlConnection con = new SqlConnection(sConnectionString);
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            string cmdSelect = "select stockist_type from smstockist where stockist_id='" + vCustCode + "'";
            SqlCommand cmd = new SqlCommand(cmdSelect, con);
            SqlDataAdapter adaptor = new SqlDataAdapter(cmd);
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            if (reader.HasRows)
            {
                return reader["stockist_type"].ToString();
            }
            else { return ""; }
        }

        protected string getCity(string vCustCode)
        {
            SqlConnection con = new SqlConnection(sConnectionString);
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            string cmdSelect = "select stockist_city from smstockist where stockist_id = '" + vCustCode + "'";
            SqlCommand cmd = new SqlCommand(cmdSelect, con);
            SqlDataAdapter adaptor = new SqlDataAdapter(cmd);
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            if (reader.HasRows)
            {
                return reader["stockist_city"].ToString();
            }
            else { return ""; }
        }


        protected void cmdExitItem_Click(object sender, EventArgs e)
        {
            Panel_Search.Visible = true; Panel_AddEdit.Visible = false; Panel_Item.Visible = false;

        }

        protected void radData_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "CMDEDIT")
            {
                GridDataItem item = (GridDataItem)e.Item;
                cmbParty.SelectedValue = item["STOCKIST_ID"].Text;
                lblStockist.Text = cmbParty.SelectedItem.Text + "(" + cmbHQ.SelectedItem.Text + ")";
                lblMonthYear.Text = cmbYear.SelectedItem.Text + " " + cmbMonth.SelectedItem.Text;
                bindItemsAdd();
                Panel_Search.Visible = false; Panel_AddEdit.Visible = true; Panel_Item.Visible = false;

            }
        }

        protected void radData_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {

        }

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            isEditMode = true;
            //GridView1.EditIndex = e.NewEditIndex;
            bindItemsAdd();
        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            //Label id = GridView1.Rows[e.RowIndex].FindControl("lbl_ID") as Label;
            //TextBox txtQuantity = GridView1.Rows[e.RowIndex].FindControl("txt_sales") as TextBox;
            SqlConnection con = new SqlConnection(sConnectionString);
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            con.Open();
            //updating the record  
            string UpdateQuery = "BSAddTertiary";
            SqlCommand cmd = new SqlCommand(UpdateQuery, con);
            cmd.CommandType = CommandType.StoredProcedure;
            //cmd.Parameters.Add("@id", SqlDbType.BigInt).Value = Convert.ToInt32(id.Text);
            //cmd.Parameters.Add("@sales", SqlDbType.Float).Value = Convert.ToDecimal(txtQuantity.Text);
            cmd.Parameters.Add("@userid", SqlDbType.Int).Value = Convert.ToInt32(Session["CODE"].ToString());
            cmd.ExecuteNonQuery();
            con.Close();
            //Setting the EditIndex property to -1 to cancel the Edit mode in Gridview  
            //GridView1.EditIndex = -1;
            //Call ShowData method for displaying updated data  
            bindItemsAdd();
        }

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            //GridView1.EditIndex = -1;
            bindItemsAdd();
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void cmdExitItemAdd_Click(object sender, EventArgs e)
        {
            Panel_AddEdit.Visible = true; Panel_Item.Visible = false; Panel_Search.Visible = false;

        }

        protected void txtSales_TextChanged(object sender, EventArgs e)
        {
            decimal vClosing = Convert.ToDecimal(txtOpening.Text) + Convert.ToDecimal(txtPurchase.Text) - Convert.ToDecimal(txtSales.Text);
            txtClosing.Text = vClosing.ToString();
        }

        protected bool IsInEditMode
        {
            get { return this.isEditMode; }
            set { this.isEditMode = value; }
        }

        protected void RadGrid1_ItemCreated(object sender, GridItemEventArgs e)
        {
            if (!Page.IsPostBack && e.Item is GridEditableItem)
            {
                e.Item.Edit = true;
            }

        }

        protected void RadGrid1_PreRender(object sender, EventArgs e)
        {

        }

        protected void RadGrid1_ItemDeleted(object sender, GridDeletedEventArgs e)
        {

        }

        protected void RadGrid1_ItemInserted(object sender, GridInsertedEventArgs e)
        {

        }

        protected void RadGrid1_ItemUpdated(object sender, GridUpdatedEventArgs e)
        {
            GridEditableItem item = (GridEditableItem)e.Item;
            //String id = item.GetDataKeyValue("ID").ToString();
            //double Sales = Convert.ToDouble(item["SALES"].ToString());
            if (e.Exception != null)
            {
                e.KeepInEditMode = true;
                e.ExceptionHandled = true;
                //NotifyUser("Product with ID " + id + " cannot be updated. Reason: " + e.Exception.Message);
            }
            else
            {
                //NotifyUser("Product with ID " + id + " is updated!");
                //item["CLOSING"].Text = ( Convert.ToDecimal(item["CLOSING"].Text) - Convert.ToDecimal(item["SALES"].Text)).ToString();
            }


        }

        private void GenerateTable(int colsCount, int rowsCount)
        {
            string qry = "select item_code from smitem order by seq";
            DataTable dt;
            dt = getDataTable(qry);
            Table table = new Table();
            table.Style["width"] = "100%";
            table.Attributes["cssStyle"] = "width=100%";
            table.ID = "Table1";
            TableRow rowHeader = new TableRow();

            TableCell cellheaderproduct = new TableCell();
            Label lbl1 = new Label();
            lbl1.Text = "Product Name";
            cellheaderproduct.Controls.Add(lbl1);
            rowHeader.Cells.Add(cellheaderproduct);

            TableCell cellheaderOPEN = new TableCell();
            Label lblopen = new Label();
            lblopen.Text = "Opening";
            cellheaderOPEN.Controls.Add(lblopen);
            rowHeader.Cells.Add(cellheaderOPEN);

            TableCell cellheaderPUR = new TableCell();
            Label lblpur = new Label();
            lblpur.Text = "Purchase";
            cellheaderPUR.Controls.Add(lblpur);
            rowHeader.Cells.Add(cellheaderPUR);


            TableCell cellheaderSALE = new TableCell();
            Label lblsale = new Label();
            lblsale.Text = "Sales";
            cellheaderSALE.Controls.Add(lblsale);
            rowHeader.Cells.Add(cellheaderSALE);

            TableCell cellheaderCLOSE = new TableCell();
            Label lblClose = new Label();
            lblClose.Text = "Closing";
            cellheaderCLOSE.Controls.Add(lblClose);
            rowHeader.Cells.Add(cellheaderCLOSE);


            table.Rows.Add(rowHeader);

            foreach (DataRow dr in dt.Rows)
            {
                TableRow row = new TableRow();
                TableCell cellitem = new TableCell();
                Label lbl = new Label();
                lbl.Text = dr["item_code"].ToString();
                cellitem.Controls.Add(lbl);
                row.Cells.Add(cellitem);

                TableCell cellOpening = new TableCell();
                TextBox tbopn = new TextBox();
                tbopn.Enabled = false;
                tbopn.ID = "txt" + dr["item_code"].ToString().Trim() + "OPN";
                cellOpening.Controls.Add(tbopn);
                row.Cells.Add(cellOpening);

                TableCell cellPurchase = new TableCell();
                TextBox tbpur = new TextBox();
                tbpur.Enabled = false;
                tbpur.ID = "txt" + dr["item_code"].ToString().Trim() + "PUR";
                cellPurchase.Controls.Add(tbpur);
                row.Cells.Add(cellPurchase);

                TableCell cellSales = new TableCell();
                TextBox tbsale = new TextBox();
                tbsale.Enabled = true;
                tbsale.ID = "txt" + dr["item_code"].ToString().Trim() + "SALE";
                cellSales.Controls.Add(tbsale);
                row.Cells.Add(cellSales);

                TableCell cellClosing = new TableCell();
                TextBox tbClosing = new TextBox();
                tbClosing.Enabled = false;
                tbClosing.ID = "txt" + dr["item_code"].ToString().Trim() + "CLOSE";
                cellClosing.Controls.Add(tbClosing);
                row.Cells.Add(cellClosing);

                table.Rows.Add(row);
            }
            phAddItem.Controls.Add(table);
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


    }




}