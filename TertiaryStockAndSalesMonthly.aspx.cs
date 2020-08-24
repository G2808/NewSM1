using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace NewSM
{
    public partial class TertiaryStockAndSalesMonthly : System.Web.UI.Page
    {
        private string sConnectionString = WebConfigurationManager.ConnectionStrings["ConnectionStringSM"].ConnectionString;
        private string sConnectionString1 = WebConfigurationManager.ConnectionStrings["ConnectionStringSM"].ConnectionString;
        private bool vlocked;
        private bool isEditMode = true;
        private SqlDataSource SqlDataSource1 = new SqlDataSource();

        protected void Page_Load(object sender, EventArgs e)
        {
            lblError.Text = "";
            if (!IsPostBack)
            {
                txtUser.Text = Session["NAME"].ToString();
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
            SqlCommand cmd = new SqlCommand("smNewDisplayTertiary", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@IsAdmin", Session["ISADMIN"].ToString());
            cmd.Parameters.AddWithValue("@EMPCODE", Session["CODE"].ToString());
            cmd.Parameters.AddWithValue("@City", cmbCity.SelectedValue);
            cmd.Parameters.AddWithValue("@Year", cmbYear.SelectedValue);
            cmd.Parameters.AddWithValue("@Month", cmbMonth.SelectedValue);
            cmd.Parameters.AddWithValue("@Stockist", cmbParty.SelectedValue);
            cmd.Parameters.AddWithValue("@ttype", cmbType.SelectedValue);
            cmd.Parameters.AddWithValue("@HQ", cmbHQ.SelectedValue);
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
            lblStockist.Text = cmbParty.SelectedItem.Text + "(" + cmbParty.SelectedItem.Value + " - " + cmbHQ.SelectedItem.Text + ")";
            lblMonthYear.Text = cmbYear.SelectedItem.Text + " " + cmbMonth.SelectedItem.Text;
            //GetDataItem();
            GetDataOpening(cmbYear.SelectedValue.ToString(), cmbMonth.SelectedValue.ToString(), cmbParty.SelectedValue.ToString(), cmbType.SelectedValue.ToString());
            GetDataPurchase(cmbYear.SelectedValue.ToString(), cmbMonth.SelectedValue.ToString(), cmbParty.SelectedValue.ToString(), cmbType.SelectedValue.ToString());

            GetDataSalesAndClosing(cmbYear.SelectedValue.ToString(),cmbMonth.SelectedValue.ToString(),cmbParty.SelectedValue.ToString(),cmbType.SelectedValue.ToString());
            //GenerateTable(6, 20);
        }


        protected void cmdExit_Click(object sender, EventArgs e)
        {

        }

        protected void GetDataOpening(string vYear,string vMonth,string vStocksit,string vType) 
        {
            string qry = "smNewGetTertiaryOpening";
            DataTable dt = new DataTable();
            SqlConnection con = new SqlConnection(sConnectionString);
            SqlCommand cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("@tyear", vYear);
            cmd.Parameters.AddWithValue("@tmonth", vMonth);
            cmd.Parameters.AddWithValue("@cust_code", vStocksit);
            cmd.Parameters.AddWithValue("@ttype", vType);
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dt);
                txtOPEDIAM.Text = dt.Rows[0]["DIAMOPN"].ToString();
                txtOPEDMR.Text = dt.Rows[0]["DMROPN"].ToString();
                txtOPEDMR60.Text = dt.Rows[0]["DMR60OPN"].ToString();
                txtOPEDXR60.Text = dt.Rows[0]["DXR60OPN"].ToString();
                txtOPEDXRMEX.Text = dt.Rows[0]["DXRMEXOPN"].ToString();
                txtOPECOV2.Text = dt.Rows[0]["COV2OPN"].ToString();
                txtOPECOV4.Text = dt.Rows[0]["COV4OPN"].ToString();
                txtOPECOV8.Text = dt.Rows[0]["COV8OPN"].ToString();
                txtOPECOVPL.Text = dt.Rows[0]["COVPLOPN"].ToString();
                txtOPECOVPLHD.Text = dt.Rows[0]["COVPLHDOPN"].ToString();
                txtOPECOVAM.Text = dt.Rows[0]["COVAMOPN"].ToString();
                txtOPEDAF500.Text = dt.Rows[0]["DAF500OPN"].ToString();
                txtOPEDAF1000.Text = dt.Rows[0]["DAF1000OPN"].ToString();
                txtOPEARC.Text = dt.Rows[0]["ARCOPN"].ToString();
                txtOPENIX.Text = dt.Rows[0]["NIXOPN"].ToString();
                txtOPENSR.Text = dt.Rows[0]["NSROPN"].ToString();
                txtOPENAM.Text = dt.Rows[0]["NAMOPN"].ToString();
                txtOPEFLA20.Text = dt.Rows[0]["FLA20OPN"].ToString();
                txtOPEFMR.Text = dt.Rows[0]["FMROPN"].ToString();

                txtOPESTAB.Text = dt.Rows[0]["STABOPN"].ToString();
                txtOPETLA.Text = dt.Rows[0]["TLAOPN"].ToString();
                txtOPECOR5.Text = dt.Rows[0]["COR5OPN"].ToString();
                txtOPECOR75.Text = dt.Rows[0]["COR75OPN"].ToString();
                txtOPEC4AM10.Text = dt.Rows[0]["C4AM10OPN"].ToString();
                txtOPEC8AM10.Text = dt.Rows[0]["C8AM10OPN"].ToString();
                txtOPEC8AM5.Text = dt.Rows[0]["C8AM5OPN"].ToString();

                txtOPENAM10.Text = dt.Rows[0]["NAM10OPN"].ToString();
                txtOPENAM25.Text = dt.Rows[0]["NAM25OPN"].ToString();
                txtOPETXAM.Text = dt.Rows[0]["TXAMOPN"].ToString();

            }
            catch (Exception ex) { }
            finally { con.Close(); }

        }

        protected void GetDataPurchase(string vYear, string vMonth, string vStocksit, string vType)
        {
            string qry = "smNewGetCustomerPrimary";
            DataTable dt = new DataTable();
            SqlConnection con = new SqlConnection(sConnectionString);
            SqlCommand cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("@tyear", vYear);
            cmd.Parameters.AddWithValue("@tmonth", vMonth);
            cmd.Parameters.AddWithValue("@cust_code", vStocksit);
            cmd.Parameters.AddWithValue("@ttype", vType);
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dt);
                txtPURDIAM.Text = dt.Rows[0]["DIAM"].ToString();
                txtPURDMR.Text = dt.Rows[0]["DMR"].ToString();
                txtPURDMR60.Text = dt.Rows[0]["DMR60"].ToString();
                txtPURDXR60.Text = dt.Rows[0]["DXR60"].ToString();
                txtPURDXRMEX.Text = dt.Rows[0]["DXRMEX"].ToString();
                txtPURCOV2.Text = dt.Rows[0]["COV2"].ToString();
                txtPURCOV4.Text = dt.Rows[0]["COV4"].ToString();
                txtPURCOV8.Text = dt.Rows[0]["COV8"].ToString();
                txtPURCOVPL.Text = dt.Rows[0]["COVPL"].ToString();
                txtPURCOVPLHD.Text = dt.Rows[0]["COVPLHD"].ToString();
                txtPURCOVAM.Text = dt.Rows[0]["COVAM"].ToString();
                txtPURDAF500.Text = dt.Rows[0]["DAF5"].ToString();
                txtPURDAF1000.Text = dt.Rows[0]["DAF1000"].ToString();
                txtPURARC.Text = dt.Rows[0]["ARC"].ToString();
                txtPURNIX.Text = dt.Rows[0]["NIX"].ToString();
                txtPURNSR.Text = dt.Rows[0]["NSR"].ToString();
                txtPURNAM.Text = dt.Rows[0]["NAM"].ToString();
                txtPURFLA20.Text = dt.Rows[0]["FLA20"].ToString();
                txtPURFMR.Text = dt.Rows[0]["FMR"].ToString();

                txtPURSTAB.Text = dt.Rows[0]["STAB"].ToString();
                txtPURTLA.Text = dt.Rows[0]["TLA"].ToString();
                txtPURCOR5.Text = dt.Rows[0]["COR5"].ToString();
                txtPURCOR75.Text = dt.Rows[0]["COR75"].ToString();
                txtPURC4AM10.Text = dt.Rows[0]["C4AM10"].ToString();
                txtPURC8AM10.Text = dt.Rows[0]["C8AM10"].ToString();
                txtPURC8AM5.Text = dt.Rows[0]["C8AM5"].ToString();

                txtPURNAM10.Text = dt.Rows[0]["NAM10"].ToString();
                txtPURNAM25.Text = dt.Rows[0]["NAM25"].ToString();
                txtPURTXAM.Text = dt.Rows[0]["TXAM"].ToString();

            }
            catch (Exception ex) { }
            finally { con.Close(); }

        }

        protected void GetDataSalesAndClosing(string vYear, string vMonth, string vStockist, string vType)
        {
            string qry = "smNewGetTertiarySalesAndClosing";
            DataTable dt = new DataTable();
            SqlConnection con = new SqlConnection(sConnectionString);
            SqlCommand cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("@tyear",vYear);
            cmd.Parameters.AddWithValue("@tmonth", vMonth);
            cmd.Parameters.AddWithValue("@cust_code", vStockist);
            cmd.Parameters.AddWithValue("@ttype", vType);
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dt);

            }
            catch (Exception ex) { }
            finally { con.Close(); }
            txtSALDIAM.Text = dt.Rows[0]["DIAM"].ToString();
            txtSALDMR.Text = dt.Rows[0]["DMR"].ToString();
            txtSALDMR60.Text = dt.Rows[0]["DMR60"].ToString();
            txtSALDXR60.Text = dt.Rows[0]["DXR60"].ToString();
            txtSALDXRMEX.Text = dt.Rows[0]["DXRMEX"].ToString();
            txtSALCOV2.Text = dt.Rows[0]["COV2"].ToString();
            txtSALCOV4.Text = dt.Rows[0]["COV4"].ToString();
            txtSALCOV8.Text = dt.Rows[0]["COV8"].ToString();
            txtSALCOVPL.Text = dt.Rows[0]["COVPL"].ToString();
            txtSALCOVPLHD.Text = dt.Rows[0]["COVPLHD"].ToString();
            txtSALCOVAM.Text = dt.Rows[0]["COVAM"].ToString();
            txtSALDAF500.Text = dt.Rows[0]["DAF5"].ToString();
            txtSALDAF1000.Text = dt.Rows[0]["DAF1000"].ToString();
            txtSALARC.Text = dt.Rows[0]["ARC"].ToString();
            txtSALNIX.Text = dt.Rows[0]["NIX"].ToString();
            txtSALNSR.Text = dt.Rows[0]["NSR"].ToString();
            txtSALNAM.Text = dt.Rows[0]["NAM"].ToString();
            txtSALFLA20.Text = dt.Rows[0]["FLA20"].ToString();
            txtSALFMR.Text = dt.Rows[0]["FMR"].ToString();

            txtSALSTAB.Text = dt.Rows[0]["STAB"].ToString();
            txtSALTLA.Text = dt.Rows[0]["TLA"].ToString();
            txtSALCOR5.Text = dt.Rows[0]["COR5"].ToString();
            txtSALCOR75.Text = dt.Rows[0]["COR75"].ToString();
            txtSALC4AM10.Text = dt.Rows[0]["C4AM10"].ToString();
            txtSALC8AM10.Text = dt.Rows[0]["C8AM10"].ToString();
            txtSALC8AM5.Text = dt.Rows[0]["C8AM5"].ToString();

            txtSALNAM10.Text = dt.Rows[0]["NAM10"].ToString();
            txtSALNAM25.Text = dt.Rows[0]["NAM25"].ToString();
            txtSALTXAM.Text = dt.Rows[0]["TXAM"].ToString();

            //Closing Stock
            txtCLODIAM.Text = dt.Rows[0]["DIAMcl"].ToString();
            txtCLODMR.Text = dt.Rows[0]["DMRcl"].ToString();
            txtCLODMR60.Text = dt.Rows[0]["DMR60cl"].ToString();
            txtCLODXR60.Text = dt.Rows[0]["DXR60cl"].ToString();
            txtCLODXRMEX.Text = dt.Rows[0]["DXRMEXcl"].ToString();
            txtCLOCOV2.Text = dt.Rows[0]["COV2cl"].ToString();
            txtCLOCOV4.Text = dt.Rows[0]["COV4cl"].ToString();
            txtCLOCOV8.Text = dt.Rows[0]["COV8cl"].ToString();
            txtCLOCOVPL.Text = dt.Rows[0]["COVPLcl"].ToString();
            txtCLOCOVPLHD.Text = dt.Rows[0]["COVPLHDcl"].ToString();
            txtCLOCOVAM.Text = dt.Rows[0]["COVAMcl"].ToString();
            txtCLODAF500.Text = dt.Rows[0]["DAF5cl"].ToString();
            txtCLODAF1000.Text = dt.Rows[0]["DAF1000cl"].ToString();
            txtCLOARC.Text = dt.Rows[0]["ARCcl"].ToString();
            txtCLONIX.Text = dt.Rows[0]["NIXcl"].ToString();
            txtCLONSR.Text = dt.Rows[0]["NSRcl"].ToString();
            txtCLONAM.Text = dt.Rows[0]["NAMcl"].ToString();
            txtCLOFLA20.Text = dt.Rows[0]["FLA20cl"].ToString();
            txtCLOFMR.Text = dt.Rows[0]["FMRcl"].ToString();

            txtCLOSTAB.Text = dt.Rows[0]["STABcl"].ToString();
            txtCLOTLA.Text = dt.Rows[0]["TLAcl"].ToString();
            txtCLOCOR5.Text = dt.Rows[0]["COR5cl"].ToString();
            txtCLOCOR75.Text = dt.Rows[0]["COR75cl"].ToString();
            txtCLOC4AM10.Text = dt.Rows[0]["C4AM10cl"].ToString();
            txtCLOC8AM10.Text = dt.Rows[0]["C8AM10cl"].ToString();
            txtCLOC8AM5.Text = dt.Rows[0]["C8AM5cl"].ToString();

            txtCLONAM10.Text = dt.Rows[0]["NAM10cl"].ToString();
            txtCLONAM25.Text = dt.Rows[0]["NAM25cl"].ToString();
            txtCLOTXAM.Text = dt.Rows[0]["TXAMcl"].ToString();
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
               // lblStockist = item["STOCKIST_ID"].Text;
                lblStockist.Text = item["NAME"].Text + "(" + item["ID"].Text + " - " + item["HQNAME"].Text + ")";
                lblMonthYear.Text = cmbYear.SelectedItem.Text + " " + cmbMonth.SelectedItem.Text;
                Panel_Search.Visible = false; Panel_AddEdit.Visible = true; Panel_Item.Visible = false;
                GetDataOpening(cmbYear.SelectedValue.ToString(), cmbMonth.SelectedValue.ToString(), item["ID"].Text, item["TYPE"].Text);
                GetDataPurchase(cmbYear.SelectedValue.ToString(), cmbMonth.SelectedValue.ToString(), item["ID"].Text, item["TYPE"].Text);

                GetDataSalesAndClosing(cmbYear.SelectedValue.ToString(), cmbMonth.SelectedValue.ToString(), item["ID"].Text, item["TYPE"].Text);

            }
        }

        protected void radData_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
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

        //private void GenerateTable(int colsCount, int rowsCount)
        //{
        //    string qry = "smNewGetCustomerPrimary";
        //    DataTable dt = new DataTable();
        //    SqlConnection con = new SqlConnection(sConnectionString);
        //    SqlCommand cmd = new SqlCommand(qry, con);
        //    cmd.Parameters.AddWithValue("@tyear", cmbYear.SelectedValue);
        //    cmd.Parameters.AddWithValue("@tmonth", cmbMonth.SelectedValue);
        //    cmd.Parameters.AddWithValue("@cust_code", cmbParty.SelectedValue);
        //    cmd.Parameters.AddWithValue("@ttype", cmbType.SelectedValue);
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    try 
        //    {
        //        con.Open();
        //        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
        //        adapter.Fill(dt);           
        //    }
        //    catch (Exception ex) { }
        //    finally { con.Close(); }
        //    Table table = new Table();
        //    table.Style["width"] = "100%";
        //    table.Attributes["cssStyle"] = "width=100%";
        //    table.ID = "Table1";
        //    TableRow rowHeader = new TableRow();

        //    TableCell cellheaderproduct = new TableCell();
        //    Label lbl1 = new Label();
        //    lbl1.Text = "Product Name";
        //    cellheaderproduct.Controls.Add(lbl1);
        //    rowHeader.Cells.Add(cellheaderproduct);

        //    TableCell cellheaderOPEN = new TableCell();
        //    Label lblopen = new Label();
        //    lblopen.Text = "Opening";
        //    cellheaderOPEN.Controls.Add(lblopen);
        //    rowHeader.Cells.Add(cellheaderOPEN);

        //    TableCell cellheaderPUR = new TableCell();
        //    Label lblpur = new Label();
        //    lblpur.Text = "Purchase";
        //    cellheaderPUR.Controls.Add(lblpur);
        //    rowHeader.Cells.Add(cellheaderPUR);


        //    TableCell cellheaderSALE = new TableCell();
        //    Label lblsale = new Label();
        //    lblsale.Text = "Sales";
        //    cellheaderSALE.Controls.Add(lblsale);
        //    rowHeader.Cells.Add(cellheaderSALE);


        //    TableCell cellheaderCLOSE = new TableCell();
        //    Label lblClose = new Label();
        //    lblClose.Text = "Closing";
        //    cellheaderCLOSE.Controls.Add(lblClose);
        //    rowHeader.Cells.Add(cellheaderCLOSE);


        //    table.Rows.Add(rowHeader);

        //    foreach (DataRow dr in dt.Rows)
        //    {
        //        TableRow row = new TableRow();
        //        TableCell cellitem = new TableCell();
        //        Label lbl = new Label();
        //        lbl.Text = dr["itemcode"].ToString();
        //        cellitem.Controls.Add(lbl);
        //        row.Cells.Add(cellitem);

        //        TableCell cellOpening = new TableCell();
        //        TextBox tbopn = new TextBox();
        //        tbopn.Enabled = false;
        //        tbopn.ID = "txt" + dr["itemcode"].ToString().Trim() + "OPN";
        //        cellOpening.Controls.Add(tbopn);
        //        row.Cells.Add(cellOpening);

        //        TableCell cellPurchase = new TableCell();
        //        TextBox tbpur = new TextBox();
        //        tbpur.Enabled = false;
        //        tbpur.ID = "txt" + dr["itemcode"].ToString().Trim() + "PUR";
        //        tbpur.Text = dr["quantity"].ToString().Trim();
        //        cellPurchase.Controls.Add(tbpur);
        //        row.Cells.Add(cellPurchase);

        //        TableCell cellSales = new TableCell();
        //        TextBox tbsale = new TextBox();
        //        tbsale.Enabled = true;
        //        tbsale.ID = "txt" + dr["itemcode"].ToString().Trim() + "SALE";
        //        tbsale.TextChanged += new EventHandler(txtSales_TextChanged);
        //        cellSales.Controls.Add(tbsale);
        //        row.Cells.Add(cellSales);

        //        TableCell cellClosing = new TableCell();
        //        TextBox tbClosing = new TextBox();
        //        tbClosing.Enabled = false;
        //        tbClosing.ID = "txt" + dr["itemcode"].ToString().Trim() + "CLOSE";
        //        cellClosing.Controls.Add(tbClosing);
        //        row.Cells.Add(cellClosing);

        //        table.Rows.Add(row);
        //    }
        //    phAddItem.Controls.Add(table);
        //}

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

        protected void radData_PreRender(object sender, EventArgs e)
        {

        }

        protected void txtSALDXR60_TextChanged(object sender, EventArgs e)
        {
            txtCLODXR60.Text = (Convert.ToInt16(txtOPEDXR60.Text) + Convert.ToInt16(txtPURDXR60.Text) - Convert.ToInt16(txtSALDXR60.Text)).ToString();
        }

        protected void txtSALTXAM_TextChanged(object sender, EventArgs e)
        {
            txtSALTXAM.Text = (Convert.ToInt16(txtOPETXAM.Text) + Convert.ToInt16(txtPURTXAM.Text) - Convert.ToInt16(txtSALTXAM.Text)).ToString();

        }

        protected void txtSALDAF1000_TextChanged(object sender, EventArgs e)
        {
            txtSALDAF1000.Text = (Convert.ToInt16(txtOPEDAF1000.Text) + Convert.ToInt16(txtPURDAF1000.Text) - Convert.ToInt16(txtSALDAF1000.Text)).ToString();
        }

        protected void txtSALCOR75_TextChanged(object sender, EventArgs e)
        {
            txtSALCOR75.Text = (Convert.ToInt16(txtOPECOR75.Text) + Convert.ToInt16(txtPURCOR75.Text) - Convert.ToInt16(txtSALCOR75.Text)).ToString();

        }

        protected void txtSALCOR5_TextChanged(object sender, EventArgs e)
        {
            txtSALCOR5.Text = (Convert.ToInt16(txtOPECOR5.Text) + Convert.ToInt16(txtPURCOR5.Text) - Convert.ToInt16(txtSALCOR5.Text)).ToString();
        }

        protected void txtSALTLA_TextChanged(object sender, EventArgs e)
        {
            txtSALTLA.Text = (Convert.ToInt16(txtOPETLA.Text) + Convert.ToInt16(txtPURTLA.Text) - Convert.ToInt16(txtSALTLA.Text)).ToString();

        }

        protected void txtSALSTAB_TextChanged(object sender, EventArgs e)
        {
            txtSALSTAB.Text = (Convert.ToInt16(txtOPESTAB.Text) + Convert.ToInt16(txtPURSTAB.Text) - Convert.ToInt16(txtSALSTAB.Text)).ToString();

        }

        protected void txtSALFMR_TextChanged(object sender, EventArgs e)
        {
            txtSALFMR.Text = (Convert.ToInt16(txtOPEFMR.Text) + Convert.ToInt16(txtPURFMR.Text) - Convert.ToInt16(txtSALFMR.Text)).ToString();

        }

        protected void txtSALFLA20_TextChanged(object sender, EventArgs e)
        {
            txtSALFLA20.Text = (Convert.ToInt16(txtOPEFLA20.Text) + Convert.ToInt16(txtPURFLA20.Text) - Convert.ToInt16(txtSALFLA20.Text)).ToString();

        }

        protected void txtSALNAM10_TextChanged(object sender, EventArgs e)
        {
            txtSALNAM10.Text = (Convert.ToInt16(txtOPENAM10.Text) + Convert.ToInt16(txtPURNAM10.Text) - Convert.ToInt16(txtSALNAM10.Text)).ToString();

        }

        protected void txtSALNAM25_TextChanged(object sender, EventArgs e)
        {
            txtSALNAM25.Text = (Convert.ToInt16(txtOPENAM25.Text) + Convert.ToInt16(txtPURNAM25.Text) - Convert.ToInt16(txtSALNAM25.Text)).ToString();

        }

        protected void txtSALNAM_TextChanged(object sender, EventArgs e)
        {
            txtSALNAM.Text = (Convert.ToInt16(txtOPENAM.Text) + Convert.ToInt16(txtPURNAM.Text) - Convert.ToInt16(txtSALNAM.Text)).ToString();

        }

        protected void txtSALNSR_TextChanged(object sender, EventArgs e)
        {
            txtSALNSR.Text = (Convert.ToInt16(txtOPENSR.Text) + Convert.ToInt16(txtPURNSR.Text) - Convert.ToInt16(txtSALNSR.Text)).ToString();

        }

        protected void txtSALNIX_TextChanged(object sender, EventArgs e)
        {
            txtSALNIX.Text = (Convert.ToInt16(txtOPENIX.Text) + Convert.ToInt16(txtPURNIX.Text) - Convert.ToInt16(txtSALNIX.Text)).ToString();

        }

        protected void txtSALARC_TextChanged(object sender, EventArgs e)
        {
            txtSALARC.Text = (Convert.ToInt16(txtOPEARC.Text) + Convert.ToInt16(txtPURARC.Text) - Convert.ToInt16(txtSALARC.Text)).ToString();

        }

        protected void txtSALDAF500_TextChanged(object sender, EventArgs e)
        {
            txtSALDAF500.Text = (Convert.ToInt16(txtOPEDAF500.Text) + Convert.ToInt16(txtPURDAF500.Text) - Convert.ToInt16(txtSALDAF500.Text)).ToString();

        }

        protected void txtSALCOVPLHD_TextChanged(object sender, EventArgs e)
        {
            txtSALCOVPLHD.Text = (Convert.ToInt16(txtOPECOVPLHD.Text) + Convert.ToInt16(txtPURCOVPLHD.Text) - Convert.ToInt16(txtSALCOVPLHD.Text)).ToString();

        }

        protected void txtSALC8AM5_TextChanged(object sender, EventArgs e)
        {
            txtSALC8AM5.Text = (Convert.ToInt16(txtOPEC8AM5.Text) + Convert.ToInt16(txtPURC8AM5.Text) - Convert.ToInt16(txtSALC8AM5.Text)).ToString();

        }

        protected void txtSALC8AM10_TextChanged(object sender, EventArgs e)
        {
            txtSALC8AM10.Text = (Convert.ToInt16(txtOPEC8AM10.Text) + Convert.ToInt16(txtPURC8AM10.Text) - Convert.ToInt16(txtSALC8AM10.Text)).ToString();

        }

        protected void txtSALC4AM10_TextChanged(object sender, EventArgs e)
        {
            txtSALC4AM10.Text = (Convert.ToInt16(txtOPEC4AM10.Text) + Convert.ToInt16(txtPURC4AM10.Text) - Convert.ToInt16(txtSALC4AM10.Text)).ToString();

        }

        protected void txtSALCOVAM_TextChanged(object sender, EventArgs e)
        {
            txtSALCOVAM.Text = (Convert.ToInt16(txtOPECOVAM.Text) + Convert.ToInt16(txtPURCOVAM.Text) - Convert.ToInt16(txtSALCOVAM.Text)).ToString();

        }

        protected void txtSALCOVPL_TextChanged(object sender, EventArgs e)
        {
            txtSALCOVPL.Text = (Convert.ToInt16(txtOPECOVPL.Text) + Convert.ToInt16(txtPURCOVPL.Text) - Convert.ToInt16(txtSALCOVPL.Text)).ToString();

        }

        protected void txtSALCOV8_TextChanged(object sender, EventArgs e)
        {
            txtSALCOV8.Text = (Convert.ToInt16(txtOPECOV8.Text) + Convert.ToInt16(txtPURCOV8.Text) - Convert.ToInt16(txtSALCOV8.Text)).ToString();

        }

        protected void txtSALCOV4_TextChanged(object sender, EventArgs e)
        {
            txtSALCOV4.Text = (Convert.ToInt16(txtOPECOV4.Text) + Convert.ToInt16(txtPURCOV4.Text) - Convert.ToInt16(txtSALCOV4.Text)).ToString();

        }

        protected void txtSALCOV2_TextChanged(object sender, EventArgs e)
        {
            txtSALCOV2.Text = (Convert.ToInt16(txtOPECOV2.Text) + Convert.ToInt16(txtPURCOV2.Text) - Convert.ToInt16(txtSALCOV2.Text)).ToString();

        }

        protected void txtSALDXRMEX_TextChanged(object sender, EventArgs e)
        {
            txtSALDXRMEX.Text = (Convert.ToInt16(txtOPEDXRMEX.Text) + Convert.ToInt16(txtPURDXRMEX.Text) - Convert.ToInt16(txtSALDXRMEX.Text)).ToString();
        }

        protected void txtSALDMR60_TextChanged(object sender, EventArgs e)
        {
            txtSALDMR60.Text = (Convert.ToInt16(txtOPEDMR60.Text) + Convert.ToInt16(txtPURDMR60.Text) - Convert.ToInt16(txtSALDMR60.Text)).ToString();

        }

        protected void txtSALDMR_TextChanged(object sender, EventArgs e)
        {
            txtSALDMR60.Text = (Convert.ToInt16(txtOPEDMR60.Text) + Convert.ToInt16(txtPURDMR60.Text) - Convert.ToInt16(txtSALDMR60.Text)).ToString();

        }

        protected void txtSALDIAM_TextChanged(object sender, EventArgs e)
        {
            txtSALDIAM.Text = (Convert.ToInt16(txtOPEDIAM.Text) + Convert.ToInt16(txtPURDIAM.Text) - Convert.ToInt16(txtSALDIAM.Text)).ToString();
        }
    }




}