using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Web.UI;
using Telerik.Web.UI;

namespace NewSM
{
    partial class Tertiarysns : System.Web.UI.Page
    {
        private string sConnectionString = WebConfigurationManager.ConnectionStrings["ConnectionString1"].ConnectionString;
        private string sConnectionString1 = WebConfigurationManager.ConnectionStrings["ConnectionString1"].ConnectionString;

        private double interimtotal, interimtotal1, intt2, intt3, intt4, intt5, intt6, intt7, intt8, intt9, intt10, intt11, intt12, intt13, intt14, intt15, intt16, intt17, intt18, intt19, intt20, intt21, intt22, intt23, intt24, intt25, intt26, intt27;
        private string strRegion;
        private bool vlocked;

        protected void Page_Load(object sender, System.EventArgs e)
        {
            lblError.Text = "";
            lblErrorItem.Text = "";
            if (!IsPostBack)
            {
                //txtStockist.Text = Session["theRegion"].ToString() ;
                //txtUser.Text = Session["EMPNAME"].ToString();
                //txtUserID.Text = Session["CODE"].ToString();
                //if (Session["IsAdmin"].ToString() == "Y")
                //{
                //}
                //BindHQ();
                //// BindCustomer()
                //bindYear();
                //bindMonth();
                //BindType();
                //BindCustomer();
                Panel_Search.Visible = true; Panel_AddEdit.Visible = false;

            }
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

        protected void RadGrid1_ItemInserted(object sender, Telerik.Web.UI.GridInsertedEventArgs e)
        {

        }

        protected void RadGrid1_ItemUpdated(object sender, Telerik.Web.UI.GridUpdatedEventArgs e)
        {
            GridEditableItem item = (GridEditableItem)e.Item;
            String id = item.GetDataKeyValue("ID").ToString();
            if (e.Exception != null)
            {
                e.KeepInEditMode = true;
                e.ExceptionHandled = true;
                lblErrorItem.Text = "ID " + id + " cannot be updated. Reason: " + e.Exception.Message;
            }
            else
            {
                SqlConnection con = new SqlConnection(sConnectionString);
                SqlCommand cmd = new SqlCommand("smUpdateTertirySalesNew", con);
                cmd.CommandType = CommandType.StoredProcedure;
            }
        }

        protected void UpdateSales()
        {

        }

        protected void RadGrid1_PreRender(object sender, EventArgs e)
        {

        }

        protected void RadGrid1_BatchEditCommand(object sender, Telerik.Web.UI.GridBatchEditingEventArgs e)
        {

        }

        protected void cmdSearchItem_Click(object sender, EventArgs e)
        {
            bindItems();
        }

        protected void radGrid_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "ADDDATA")
            {
                GridDataItem item = (GridDataItem)e.Item;
                ActFlag.Text = "Editing";
                txtCode.Text = item["ID"].Text;
                //txtProduct.Text = item["ITEM"].Text;
                //txtOpening.Text = item["OPENING"].Text;
                //txtPurchase.Text= item["PURCHASE"].Text;
                //txtSales.Text = item["SALES"].Text; ;
                //string index = item["ITEM_CODE"].Text;
                //Session["ITEM_CODE"] = index;
                ActFlag.Text = "Editing";
                ClientScript.RegisterStartupScript(this.GetType(), "MyPopup", "ShowPopup();", true);

                //lblModalTitle.Text = "Add Tertiary Sales";
                //ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myModal", "$('#myModal').modal({backdrop: 'static', keyboard: false});", true);

                //ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myModal", "$('#myModal').modal();", true);
                //upModal.Update();
            }

        }

        protected void Submit_Click(object sender, EventArgs e)
        {

        }

        protected void txtSales_TextChanged(object sender, EventArgs e)
        {
            //decimal calcClosing = Convert.ToDecimal(txtOpening.Text) + Convert.ToDecimal(txtPurchase.Text) - Convert.ToDecimal(txtSales.Text);
            //txtClosing.Text = calcClosing.ToString();
            ClientScript.RegisterStartupScript(this.GetType(), "MyPopup", "ShowPopup();", true);
            return;

        }

        protected void ShowPopup(object sender, EventArgs e)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "MyPopup", "ShowPopup();", true);

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

        protected void cmbParty_SelectedIndexChanged(object sender, System.EventArgs e)
        {
        }

        protected void cmbHQ_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            BindCity();
            BindCustomer();
        }

        protected void btnSearch_Click(object sender, System.EventArgs e)
        {
            vlocked = chkMonth(cmbMonth.SelectedValue, cmbYear.Text);
            //ResetTotal();
            binddata();

        }


        protected void bindItems()
        {
            SqlConnection con = new SqlConnection(sConnectionString);
            if (con.State == ConnectionState.Open)
                con.Close();
            SqlCommand cmd = new SqlCommand("AddNewTertiary", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@tyear", SqlDbType.VarChar));
            cmd.Parameters["@tyear"].Value = cmbYear.SelectedValue;
            cmd.Parameters.Add(new SqlParameter("@tmonth", SqlDbType.VarChar));
            cmd.Parameters["@tmonth"].Value = cmbMonth.SelectedValue;

            cmd.Parameters.Add(new SqlParameter("@cust_code", SqlDbType.VarChar));
            cmd.Parameters["@cust_code"].Value = cmbParty.SelectedValue;
            if (txtSearchCode.Text.Trim() != "")
            {
                cmd.Parameters.Add(new SqlParameter("@item_code", SqlDbType.VarChar));
                cmd.Parameters["@item_code"].Value = txtSearchCode.Text;
            }
            cmd.Parameters.Add(new SqlParameter("@ttype", SqlDbType.Float));
            cmd.Parameters["@ttype"].Value = Convert.ToDecimal(cmbType.SelectedValue);
            con.Open();

            SqlDataAdapter adaptor = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adaptor.Fill(ds, "Tert");
            con.Close();
            RadGrid1.DataSource = ds;
            RadGrid1.DataBind();
        }

        protected void binddata()
        {
            SqlConnection con = new SqlConnection(sConnectionString);
            if (con.State == ConnectionState.Open)
                con.Close();
            SqlCommand cmd = new SqlCommand("gsDisplayTertiaryNew", con);
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
            cmd.Parameters["@ttype"].Value = Val(cmbType.SelectedValue);

            cmd.Parameters.Add(new SqlParameter("@HQ", SqlDbType.Int));
            cmd.Parameters["@HQ"].Value = Val(cmbHQ.SelectedValue);
            con.Open();
            SqlDataAdapter adaptor = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adaptor.Fill(ds, "Tert");
            con.Close();
            radData.DataSource = ds;
            radData.DataBind();
        }

        protected void grdDetails_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                string cmdu;
                SqlConnection con = new SqlConnection(sConnectionString);
                if (con.State == ConnectionState.Open)
                    con.Close();
                cmdu = "update smtertiary set del_flag='Y',user_delete= '" + txtUser.Text + "', delete_date= getdate()" + " where cust_code='" + e.Item.Cells[0].Text + "' and year='" + cmbYear.Text + "' and month='" + cmbMonth.Text + "'";
                try
                {
                    SqlCommand cmd = new SqlCommand(cmdu, con);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    binddata();
                }
                catch (Exception ex)
                {
                    lblError.Text = "Delete Eror:" + ex.Message;
                }
            }
            if (e.CommandName == "Edit")
            {
                if (chkMonth(cmbMonth.SelectedValue, cmbYear.Text))
                {
                    lblError.Text = "This month is locked !! you can only view";
                    return;
                }

                cmbHQ.SelectedValue = e.Item.Cells[33].Text;
                cmbParty.SelectedValue = e.Item.Cells[0].Text;
                cmbType.SelectedValue = e.Item.Cells[2].Text;
                // cmbCity.SelectedValue = e.Item.Cells(3).Text
                enableDisableControl(false);
                //getPrimary();
                //getOpening();
                //getTertiary();
                // getReturns()
                Panel_Search.Visible = false; Panel_AddEdit.Visible = true;

            }
        }

        protected void grdDetails_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
        {
            Int16 fclno;
            fclno = 5;
            if (e.Item.ItemType == System.Web.UI.WebControls.ListItemType.AlternatingItem)
            {
                interimtotal += System.Convert.ToDouble(e.Item.Cells[fclno].Text);
                interimtotal1 += System.Convert.ToDouble(e.Item.Cells[fclno + 1].Text);
                intt2 += System.Convert.ToDouble(e.Item.Cells[fclno + 2].Text);
                intt3 += System.Convert.ToDouble(e.Item.Cells[fclno + 3].Text);
                intt4 += System.Convert.ToDouble(e.Item.Cells[fclno + 4].Text);
                intt5 += System.Convert.ToDouble(e.Item.Cells[fclno + 5].Text);
                intt6 += System.Convert.ToDouble(e.Item.Cells[fclno + 6].Text);
                intt7 += System.Convert.ToDouble(e.Item.Cells[fclno + 7].Text);
                intt8 += System.Convert.ToDouble(e.Item.Cells[fclno + 8].Text);
                intt9 += System.Convert.ToDouble(e.Item.Cells[fclno + 9].Text);
                intt10 += System.Convert.ToDouble(e.Item.Cells[fclno + 10].Text);
                intt11 += System.Convert.ToDouble(e.Item.Cells[fclno + 11].Text);
                intt12 += System.Convert.ToDouble(e.Item.Cells[fclno + 12].Text);
                intt13 += System.Convert.ToDouble(e.Item.Cells[fclno + 13].Text);
                intt14 += System.Convert.ToDouble(e.Item.Cells[fclno + 14].Text);
                intt15 += System.Convert.ToDouble(e.Item.Cells[fclno + 15].Text);
                intt16 += System.Convert.ToDouble(e.Item.Cells[fclno + 16].Text);
                intt17 += System.Convert.ToDouble(e.Item.Cells[fclno + 17].Text);
                intt18 += System.Convert.ToDouble(e.Item.Cells[fclno + 18].Text);
                intt19 += System.Convert.ToDouble(e.Item.Cells[fclno + 19].Text);
                intt20 += System.Convert.ToDouble(e.Item.Cells[fclno + 20].Text);
                intt21 += System.Convert.ToDouble(e.Item.Cells[fclno + 21].Text);
                intt22 += System.Convert.ToDouble(e.Item.Cells[fclno + 22].Text);
                intt23 += System.Convert.ToDouble(e.Item.Cells[fclno + 23].Text);
                intt24 += System.Convert.ToDouble(e.Item.Cells[fclno + 24].Text);
                intt25 += System.Convert.ToDouble(e.Item.Cells[fclno + 25].Text);
                intt26 += System.Convert.ToDouble(e.Item.Cells[fclno + 26].Text);
                intt27 += System.Convert.ToDouble(e.Item.Cells[fclno + 27].Text);
                e.Item.Cells[34].Enabled = !vlocked;
                e.Item.Cells[35].Enabled = !vlocked;
            }
            if (e.Item.ItemType == System.Web.UI.WebControls.ListItemType.Footer)
            {
                e.Item.Cells[0].Text = "TOTAL: ";
                e.Item.Cells[fclno].Text = interimtotal.ToString();
                e.Item.Cells[fclno + 1].Text = interimtotal1.ToString();
                e.Item.Cells[fclno + 2].Text = intt2.ToString();
                e.Item.Cells[fclno + 3].Text = intt3.ToString();
                e.Item.Cells[fclno + 4].Text = intt4.ToString();
                e.Item.Cells[fclno + 5].Text = intt5.ToString();
                e.Item.Cells[fclno + 6].Text = intt6.ToString();
                e.Item.Cells[fclno + 7].Text = intt7.ToString();
                e.Item.Cells[fclno + 8].Text = intt8.ToString();
                e.Item.Cells[fclno + 9].Text = intt9.ToString();
                e.Item.Cells[fclno + 10].Text = intt10.ToString();
                e.Item.Cells[fclno + 11].Text = intt11.ToString();
                e.Item.Cells[fclno + 12].Text = intt12.ToString();
                e.Item.Cells[fclno + 13].Text = intt13.ToString();
                e.Item.Cells[fclno + 14].Text = intt14.ToString();
                e.Item.Cells[fclno + 15].Text = intt15.ToString();
                e.Item.Cells[fclno + 16].Text = intt16.ToString();
                e.Item.Cells[fclno + 17].Text = intt17.ToString();
                e.Item.Cells[fclno + 18].Text = intt18.ToString();
                e.Item.Cells[fclno + 19].Text = intt19.ToString();
                e.Item.Cells[fclno + 20].Text = intt20.ToString();
                e.Item.Cells[fclno + 21].Text = intt21.ToString();
                e.Item.Cells[fclno + 22].Text = intt22.ToString();
                e.Item.Cells[fclno + 23].Text = intt23.ToString();
                e.Item.Cells[fclno + 24].Text = intt24.ToString();
                e.Item.Cells[fclno + 25].Text = intt25.ToString();
                e.Item.Cells[fclno + 26].Text = intt26.ToString();
                e.Item.Cells[fclno + 27].Text = intt27.ToString();
            }

        }




        protected void cmdExit_Click(object sender, System.EventArgs e)
        {
            // Response.Redirect("default.aspx")
            Response.Redirect("banner.aspx");
        }

        protected void enableDisableControl(bool yesno)
        {
            cmbYear.Enabled = yesno;
            cmbMonth.Enabled = yesno;
            cmbParty.Enabled = yesno;
            cmbHQ.Enabled = yesno;
            btnNew.Enabled = yesno;
            btnNew.Visible = yesno;
            btnSearch.Enabled = yesno;
            btnSearch.Visible = yesno;
            btnSearch.Enabled = yesno;
            btnSearch.Visible = yesno;
            cmdExit.Enabled = yesno;
            cmdExit.Visible = yesno;
            cmbCity.Enabled = yesno;
            cmbType.Enabled = yesno;
            //grdDetails.Visible = yesno;
        }

        protected void btnNew_Click(object sender, System.EventArgs e)
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
            if (chkMonth(cmbMonth.SelectedValue, cmbYear.Text))
            {
                lblError.Text = "This month is locked !! you can only view";
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
            bindItems();
        }

        public string getMonth(int vmth)
        {
            switch (vmth)
            {
                case 1:
                    {
                        return "January";
                    }

                case 2:
                    {
                        return "February";
                    }

                case 3:
                    {
                        return "March";
                    }

                case 4:
                    {
                        return "April";
                    }

                default:
                    return "";
            }
        }



        protected void Button1_Click1(object sender, System.EventArgs e)
        {
            ContentType = "application/vnd.ms-excel";
            //Response Charset = "";
            this.EnableViewState = false;
            System.IO.StringWriter tw = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(tw);
            //grdDetails.Columns[0].Visible = false;
            //grdDetails.Columns[1].SortExpression = "";
            //ClearControls(grdDetails);
            //grdDetails.RenderControl(hw);
            //Response.Write(tw.ToString() + " target=_blank");
            //Response.End();
        }
        private bool chkNew(string vmonth, string vyear, int vhq, string vCustomer)
        {
            SqlConnection con = new SqlConnection(sConnectionString);
            if (con.State == ConnectionState.Open)
                con.Close();
            string cmdSelect = "select * from smtertiary where year='" + vyear + "' and month='" + vmonth + "'" + " and hq_id=" + vhq + " and cust_code='" + vCustomer + "'";
            SqlCommand cmd = new SqlCommand(cmdSelect, con);
            SqlDataAdapter adaptor = new SqlDataAdapter(cmd);
            con.Open();
            SqlDataReader reader;
            reader = cmd.ExecuteReader();
            reader.Read();
            if (reader.HasRows)
                return true;
            else
                return false;
        }

        private string getCity(string vCustCode)
        {
            SqlConnection con = new SqlConnection(sConnectionString);
            if (con.State == ConnectionState.Open)
                con.Close();
            string cmdSelect = "select stockist_city from smstockist where stockist_id='" + vCustCode + "'";
            SqlCommand cmd = new SqlCommand(cmdSelect, con);
            SqlDataAdapter adaptor = new SqlDataAdapter(cmd);
            con.Open();
            SqlDataReader reader;
            reader = cmd.ExecuteReader();
            reader.Read();
            if (reader.HasRows)
                return reader["stockist_city"].ToString();
            else
                return "";
        }

        private string getCustType(string vCustCode)
        {
            SqlConnection con = new SqlConnection(sConnectionString);
            if (con.State == ConnectionState.Open)
                con.Close();
            string cmdSelect = "select stockist_type from smstockist where stockist_id='" + vCustCode + "'";
            SqlCommand cmd = new SqlCommand(cmdSelect, con);
            SqlDataAdapter adaptor = new SqlDataAdapter(cmd);
            con.Open();
            SqlDataReader reader;
            reader = cmd.ExecuteReader();
            reader.Read();
            if (reader.HasRows)
                return reader["stockist_type"].ToString();
            else
                return "";
            con.Close();
        }

        private void ClearControls(Control ctrl)
        {
            int i;

            for (i = ctrl.Controls.Count - 1; i <= 0; i += i - 1)
                ClearControls(ctrl.Controls[i]);

            Type ctrlType = ctrl.GetType();
            if (ctrlType.Name != "TableCell")
            {
                if (ctrl.GetType().GetProperty("SelectedItem") != null)
                {
                    LiteralControl literal = new LiteralControl();
                    ctrl.Parent.Controls.Add(literal);

                    try
                    {
                        literal.Text = System.Convert.ToString(ctrl.GetType().GetProperty("SelectedItem").GetValue(ctrl, null/* TODO Change to default(_) if this is not a reference type */));
                    }
                    catch
                    {
                    }

                    ctrl.Parent.Controls.Remove(ctrl);
                }
                else if (ctrl.GetType().GetProperty("Text") != null)
                {
                    LiteralControl literal = new LiteralControl();
                    ctrl.Parent.Controls.Add(literal);
                    literal.Text = System.Convert.ToString(ctrl.GetType().GetProperty("Text").GetValue(ctrl, null/* TODO Change to default(_) if this is not a reference type */));
                    ctrl.Parent.Controls.Remove(ctrl);
                }
            }
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


        protected void cmbCity_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            BindCustomer();
        }

        protected void cmbType_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            BindCustomer();
        }




        protected void cmbYear_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            bindMonth();
        }



        protected void cmdCancelAddEdit_Click(object sender, System.EventArgs e)
        {
            enableDisableControl(true);
            Panel_Search.Visible = true; Panel_AddEdit.Visible = false;

        }

        protected void radData_DataBound(object sender, EventArgs e)
        {
            radData.MasterTableView.GetColumn("stockist_id").Display = false;
            radData.MasterTableView.GetColumn("hq_id").Display = false;
            radData.MasterTableView.GetColumn("stockist_type").Display = false;
        }

        protected void cmbHQ_SelectedIndexChanged1(object sender, EventArgs e)
        {
            BindCity();
        }

        protected void cmbCity_SelectedIndexChanged1(object sender, EventArgs e)
        {
            BindCustomer();
        }




        protected void grdDetails_SelectedIndexChanged(object sender, System.EventArgs e)
        {
        }




        protected Int64 Val(string sText)
        {
            Int64 retVal = 0;
            try
            {
                retVal = Convert.ToInt64(sText);
            }
            catch
            {
                retVal = 0;
            }
            return retVal;
        }


    }
}