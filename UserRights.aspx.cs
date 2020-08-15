using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Web.UI;
using Telerik.Web.UI;

namespace NewSm1
{
    public partial class UserRights : System.Web.UI.Page
    {
        private string sConnectionString = WebConfigurationManager.ConnectionStrings["ConnectionStringSM"].ConnectionString;
        DataBase db = new DataBase();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindZone();
                BindRegion();
                BindHQ();
                BindEmployee();

                RadTabStrip1.SelectedIndex = 1;
                RadMultiPage1.SelectedIndex = 1;
                lblErrorRegionHQ.Text = "";
            }
        }

        protected void BindZone()
        {
            SqlConnection con = new SqlConnection(sConnectionString);
            SqlCommand cmd = new SqlCommand();
            DataTable dt;
            dt = db.ReturnDataTable("select zone_id,descr from smzone order by descr");

            ddZone.DataSource = dt;
            ddZone.DataTextField = "descr";
            ddZone.DataValueField = "zone_id";
            ddZone.DataBind();
        }
        protected void BindRegion()
        {
            SqlConnection con = new SqlConnection(sConnectionString);
            SqlCommand cmd = new SqlCommand();
            DataTable dt;
            dt = db.ReturnDataTable("select region_id,region_longname from smregion order by region_longname");
            ddRegion.DataSource = dt;
            ddRegion.DataTextField = "region_longname";
            ddRegion.DataValueField = "region_id";
            ddRegion.DataBind();
        }


        protected void BindHQ()
        {
            SqlConnection con = new SqlConnection(sConnectionString);
            SqlCommand cmd = new SqlCommand();
            DataTable dt;
            dt = db.ReturnDataTable("select headquarter_id,headquarter_name from smhq1 order by headquarter_name");
            ddHQ.DataSource = dt;
            ddHQ.DataTextField = "headquarter_name";
            ddHQ.DataValueField = "headquarter_id";
            ddHQ.DataBind();
        }

        protected void BindEmployee()
        {
            SqlConnection con = new SqlConnection(sConnectionString);
            SqlCommand cmd = new SqlCommand();
            DataTable dt;
            dt = db.ReturnDataTable("select FIRSTNAME+' '+lastname+' ('+positionname +')' Position,positionid from smNewUser where positionname is not null and FIELD = 'Y' and DESIGGROUPID IN('SBE', 'MR') and positionactive='Y' order by  FIRSTNAME");

            ddEmployee.DataSource = dt;
            ddEmployee.DataTextField = "position";
            ddEmployee.DataValueField = "positionid";
            ddEmployee.DataBind();
        }
        //protected void BindMenuNo()
        //{
        //    OracleConnection con = new OracleConnection(sConnectionString);
        //    string cmdString = "select menuid,mmenu from gsmenunew where menuid not in (select menuid from gsusermenunew where userid='" + Session["UID_RIGHTS"].ToString() + "')";
        //    OracleCommand cmd = new OracleCommand(cmdString, con);
        //    try
        //    {
        //        con.Open();
        //        lstMenuNo.DataSource = cmd.ExecuteReader();
        //        lstMenuNo.DataTextField = "mmenu";
        //        lstMenuNo.DataValueField = "menuid";
        //        lstMenuNo.DataBind();
        //    }
        //    catch { }
        //    finally { con.Close(); }

        //}

        //protected void BindMenuYes()
        //{
        //    OracleConnection con = new OracleConnection(sConnectionString);
        //    string cmdString = "select menuid,usermenu from gsusermenunew where userid='" + Session["UID_RIGHTS"].ToString() + "'";
        //    OracleCommand cmd = new OracleCommand(cmdString, con);
        //    try
        //    {
        //        con.Open();
        //        lstMenuYes.DataSource = cmd.ExecuteReader();
        //        lstMenuYes.DataTextField = "usermenu";
        //        lstMenuYes.DataValueField = "menuid";
        //        lstMenuYes.DataBind();
        //    }
        //    catch { }
        //    finally { con.Close(); }

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




        protected void cmdAssign_Click(object sender, EventArgs e)
        {
            hidTAB.Value = "#tab1";
            RadTabStrip1.SelectedIndex = 0;
            RadMultiPage1.SelectedIndex = 0;
        }

        protected void btnReturn_Click(object sender, EventArgs e)
        {


        }
        protected void ListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void BindZoneNo()
        {
            SqlConnection con = new SqlConnection(sConnectionString);
            string cmdString = "select region_id,region_longname from smregion where region_id not in (select region_id from smNewLinkZoneRegion where zone_id =" + ddZone.SelectedValue;
            SqlCommand cmd = new SqlCommand(cmdString, con);
            try
            {
                con.Open();
                lstZoneNo.DataSource = cmd.ExecuteReader();
                lstZoneNo.DataTextField = "DESCR";
                lstZoneNo.DataValueField = "FLD_VALUE";
                lstZoneNo.DataBind();
            }
            catch { }
            finally { con.Close(); }

        }

        protected void cmdExit_Click(object sender, EventArgs e)
        {
            Response.Redirect("banner.aspx");
        }
        protected void cmdReturn_Click(object sender, EventArgs e)
        {
        }

        protected void btnAssign_Click(object sender, EventArgs e)
        {
            BindZoneYesNo();
        }


        protected void BindZoneYesNo()
        {
            string sqlYes = "select region_id,region_longname from smregion where region_id in " +
            "(select region_id from smnewlinkzoneregion where zone_id in (" + ddZone.SelectedValue + ")) order by region_longname";
            string sqlNo = "select region_id,region_longname from smregion where region_id not in " +
            "(select region_id from smnewlinkzoneregion where zone_id in (" + ddZone.SelectedValue + ")) order by region_longname";
            DataTable dtYes = db.ReturnDataTable(sqlYes);
            lstZoneYes.DataSource = dtYes;
            lstZoneYes.DataTextField = "region_longname";
            lstZoneYes.DataValueField = "region_id";
            lstZoneYes.DataBind();
            DataTable dtNo = db.ReturnDataTable(sqlNo);
            lstZoneNo.DataSource = dtNo;
            lstZoneNo.DataTextField = "region_longname";
            lstZoneNo.DataValueField = "region_id";
            lstZoneNo.DataBind();
        }
        protected void BindHQYesNo()
        {
            string sqlYes = "select hq.headquarter_id,trim(hq.headquarter_name) + ' ('+Convert(varchar(10),lhq.salesper) +'%)' headquarter_name " +
            " from smhq1 hq inner join smnewlinkregionhq lhq on lhq.headquarter_id=hq.headquarter_id and lhq.region_id=" + ddRegion.SelectedValue +
            " where hq.headquarter_id in " +
            "(select headquarter_id from smnewlinkregionhq where region_id in (" + ddRegion.SelectedValue + ")) order by hq.headquarter_name";
            string sqlNo = "select headquarter_id,headquarter_name from smhq1 where headquarter_id not in " +
            "(select headquarter_id from smnewlinkregionhq where region_id in (" + ddRegion.SelectedValue + ")) " +
            " and headquarter_id not in (select headquarter_id from smnewlinkregionhq group by headquarter_Id having sum(salesper) = 100) order by headquarter_name";
            DataTable dtYes = db.ReturnDataTable(sqlYes);
            lstHQYes.DataSource = dtYes;
            lstHQYes.DataTextField = "headquarter_name";
            lstHQYes.DataValueField = "headquarter_id";
            lstHQYes.DataBind();
            DataTable dtNo = db.ReturnDataTable(sqlNo);
            lstHQNo.DataSource = dtNo;
            lstHQNo.DataTextField = "headquarter_name";
            lstHQNo.DataValueField = "headquarter_id";
            lstHQNo.DataBind();
        }

        protected void btnExit_Click(object sender, EventArgs e)
        {
            Response.Redirect("Bannerho.aspx");
        }

        protected void BindStockistYesNo()
        {
            string sqlYes = "select stockist_id,stockist_name +' (City: ' + Stockist_city +')'" + " stockist_name  from smstockist where stockist_id in " +
            "(select stockist_id from smnewLinkHQStockist where headquarter_id in (" + ddHQ.SelectedValue + ")) order by stockist_name";
            string sqlNo = "select stockist_id,stockist_name + '(City:'+ Stockist_city +')' stockist_name from smstockist where stockist_id not in " +
            "(select stockist_id from smnewLinkHQStockist) order by stockist_name";
            DataTable dtYes = db.ReturnDataTable(sqlYes);
            lstStockistYes.DataSource = dtYes;
            lstStockistYes.DataTextField = "stockist_name";
            lstStockistYes.DataValueField = "stockist_id";
            lstStockistYes.DataBind();
            DataTable dtNo = db.ReturnDataTable(sqlNo);
            lstStockistNo.DataSource = dtNo;
            lstStockistNo.DataTextField = "stockist_name";
            lstStockistNo.DataValueField = "stockist_id";
            lstStockistNo.DataBind();
        }

        protected void BindEMPHQYesNo()
        {
            string sqlYes = "select hq.headquarter_id,trim(hq.headquarter_name) + ' ('+Convert(varchar(10),lhq.salesper) +'%)' headquarter_name " +
            " from smhq1 hq inner join smnewlinkhqemp lhq on lhq.hqid=hq.headquarter_id and lhq.POSITIONID=" + ddEmployee.SelectedValue +
            " where hq.headquarter_id in " +
            "(select hqid from smnewlinkhqemp where POSITIONID in (" + ddEmployee.SelectedValue + ")) order by hq.headquarter_name";
            string sqlNo = "select headquarter_id,headquarter_name from smhq1 where headquarter_id not in " +
            "(select hqid from smnewlinkhqemp where POSITIONID in (" + ddEmployee.SelectedValue + ")) order by headquarter_name ";
            DataTable dtYes = db.ReturnDataTable(sqlYes);
            lstEmpHQYes.DataSource = dtYes;
            lstEmpHQYes.DataTextField = "headquarter_name";
            lstEmpHQYes.DataValueField = "headquarter_id";
            lstEmpHQYes.DataBind();
            DataTable dtNo = db.ReturnDataTable(sqlNo);
            lstEmpHQNo.DataSource = dtNo;
            lstEmpHQNo.DataTextField = "headquarter_name";
            lstEmpHQNo.DataValueField = "headquarter_id";
            lstEmpHQNo.DataBind();
        }

        protected void btnRemoveRegion_Click(object sender, EventArgs e)
        {
            string condstring = "";
            if (lstZoneYes.SelectedIndex >= 0)
            {
                for (int i = 0; i <= lstZoneYes.Items.Count - 1; i++)
                {
                    if (lstZoneYes.Items[i].Selected == true)
                    {
                        if (condstring == "") { condstring = lstZoneYes.Items[i].Value; } else { condstring = condstring + "," + lstZoneYes.Items[i].Value; }
                    }
                }
                SqlConnection con = new SqlConnection(sConnectionString);

                string cmdstring = "delete from smnewlinkzoneregion where zone_id=" + ddZone.SelectedValue + " and region_id in (" + condstring + ")";
                try
                {
                    SqlCommand cmd = new SqlCommand(cmdstring, con);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    BindZoneYesNo();
                }
                catch { }
                finally { con.Close(); }
            }
        }


        protected void btnAssignRegion_Click(object sender, EventArgs e)
        {
            string condstring = "";
            if (lstZoneNo.SelectedIndex >= 0)
            {
                for (int i = 0; i <= lstZoneNo.Items.Count - 1; i++)
                {
                    if (lstZoneNo.Items[i].Selected == true)
                    {
                        if (condstring == "") { condstring = lstZoneNo.Items[i].Value; } else { condstring = condstring + "," + lstZoneNo.Items[i].Value; }

                    }
                }
                SqlConnection con = new SqlConnection(sConnectionString);

                string cmdstring = "insert into smnewlinkzoneregion(zone_id,region_id) select " + ddZone.SelectedValue + ",region_id from smregion where region_id in (" + condstring + ")";
                try
                {
                    SqlCommand cmd = new SqlCommand(cmdstring, con);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    BindZoneYesNo();
                }
                catch { }
                finally { con.Close(); }
            }

        }

        protected void btnAssignHQ_Click(object sender, EventArgs e)
        {
            lblErrorRegionHQ.Text = "";
            BindHQYesNo();
        }

        protected void btnAddHQ_Click(object sender, EventArgs e)
        {
            string condstring = "";
            if (lstHQNo.SelectedIndex >= 0)
            {
                for (int i = 0; i <= lstHQNo.Items.Count - 1; i++)
                {
                    if (lstHQNo.Items[i].Selected == true)
                    {
                        if (condstring == "") { condstring = lstHQNo.Items[i].Value; } else { condstring = condstring + "," + lstHQNo.Items[i].Value; }

                    }
                }
                SqlConnection con = new SqlConnection(sConnectionString);
                if (Convert.ToInt32(txtPercentage.Text) == 0 || txtPercentage.Text == "")
                {
                    lblErrorRegionHQ.Text = "Sales percentage cant be zero or blank"; return;
                }
                if (CheckSalesPercentage(condstring))
                {
                    string cmdstring = "insert into smnewlinkregionhq(region_id,salesper,headquarter_id) select " + ddRegion.SelectedValue + "," + txtPercentage.Text + ", headquarter_id from smhq1 where headquarter_id in (" + condstring + ")";
                    try
                    {
                        SqlCommand cmd = new SqlCommand(cmdstring, con);
                        con.Open();
                        cmd.ExecuteNonQuery();
                        BindHQYesNo();
                    }
                    catch { }
                    finally { con.Close(); }
                }
            }
        }

        protected bool CheckSalesPercentage(string vcondstring)
        {
            bool retval = true;
            string sqlString = "select isnull(sum(salesper) ,0) as per from smnewlinkRegionHQ where headquarter_id in (" + vcondstring + ")";
            SqlDataReader reader = db.ReturnDataReader(sqlString);
            reader.Read();

            if (reader.HasRows)
            {
                Decimal AddedPercentage = Convert.ToDecimal(reader["per"].ToString());
                Decimal AddingPercentage = Convert.ToDecimal(txtPercentage.Text);
                Decimal BalanceCanAdd = 100 - AddedPercentage;
                if (AddedPercentage + AddingPercentage > 100)
                {
                    lblErrorRegionHQ.Text = "Can't Add you have left with only " + BalanceCanAdd.ToString() + "% You are trying to add " + txtPercentage.Text + "%";
                    retval = false;
                }
                else
                {
                    retval = true;
                }
            }
            return retval;

        }

        protected void btnRemoveHQ_Click(object sender, EventArgs e)
        {
            string condstring = "";
            if (lstHQYes.SelectedIndex >= 0)
            {
                for (int i = 0; i <= lstHQYes.Items.Count - 1; i++)
                {
                    if (lstHQYes.Items[i].Selected == true)
                    {
                        if (condstring == "") { condstring = lstHQYes.Items[i].Value; } else { condstring = condstring + "," + lstHQYes.Items[i].Value; }
                    }
                }
                SqlConnection con = new SqlConnection(sConnectionString);

                string cmdstring = "delete from smnewlinkregionhq where region_id=" + ddRegion.SelectedValue + " and headquarter_id in (" + condstring + ")";
                try
                {
                    SqlCommand cmd = new SqlCommand(cmdstring, con);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    BindHQYesNo();
                }
                catch { }
                finally { con.Close(); }
            }
        }

        protected void btnAssignStockist_Click(object sender, EventArgs e)
        {
            BindStockistYesNo();
        }

        protected void btnExitStockist_Click(object sender, EventArgs e)
        {
            Response.Redirect("BannerHo.aspx");
        }

        protected void btnAddStockist_Click(object sender, EventArgs e)
        {
            string condstring = "";
            if (lstStockistNo.SelectedIndex >= 0)
            {
                for (int i = 0; i <= lstStockistNo.Items.Count - 1; i++)
                {
                    if (lstStockistNo.Items[i].Selected == true)
                    {
                        if (condstring == "") { condstring = "'" + lstStockistNo.Items[i].Value + "'"; } else { condstring = condstring + ",'" + lstStockistNo.Items[i].Value + "'"; }
                    }
                }
                SqlConnection con = new SqlConnection(sConnectionString);

                string cmdstring = "insert into smnewLinkHQStockist(headquarter_id,stockist_id) select " + ddHQ.SelectedValue + ",stockist_id from smstockist where stockist_id in (" + condstring + ")";
                try
                {
                    SqlCommand cmd = new SqlCommand(cmdstring, con);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    BindStockistYesNo();
                }
                catch (Exception ex) { }
                finally { con.Close(); }
            }
        }

        protected void btnRemoveStockist_Click(object sender, EventArgs e)
        {
            string condstring = "";
            if (lstStockistYes.SelectedIndex >= 0)
            {
                for (int i = 0; i <= lstStockistYes.Items.Count - 1; i++)
                {
                    if (lstStockistYes.Items[i].Selected == true)
                    {
                        if (condstring == "") { condstring = "'" + lstStockistYes.Items[i].Value.Trim() + "'"; } else { condstring = condstring + ",'" + lstStockistYes.Items[i].Value.Trim() + "'"; }
                    }
                }
                SqlConnection con = new SqlConnection(sConnectionString);

                string cmdstring = "delete from smnewlinkhqstockist where headquarter_id=" + ddHQ.SelectedValue + " and stockist_id in (" + condstring + ")";
                try
                {
                    SqlCommand cmd = new SqlCommand(cmdstring, con);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    BindStockistYesNo();
                }
                catch { }
                finally { con.Close(); }
            }
        }

        protected void btnEmpHQ_Click(object sender, EventArgs e)
        {
            BindEMPHQYesNo();
        }

        protected void btnEmpExitHQ_Click(object sender, EventArgs e)
        {

        }

        protected void btnAddEmpHQ_Click(object sender, EventArgs e)
        {
            string condstring = "";
            if (lstEmpHQNo.SelectedIndex >= 0)
            {
                for (int i = 0; i <= lstEmpHQNo.Items.Count - 1; i++)
                {
                    if (lstEmpHQNo.Items[i].Selected == true)
                    {
                        if (condstring == "") { condstring = lstEmpHQNo.Items[i].Value; } else { condstring = condstring + "," + lstEmpHQNo.Items[i].Value; }

                    }
                }
                SqlConnection con = new SqlConnection(sConnectionString);
                if (Convert.ToInt32(txtPercentage.Text) == 0 || txtPercentage.Text == "")
                {
                    lblErrorRegionHQ.Text = "Sales percentage cant be zero or blank"; return;
                }
                string cmdstring = "insert into smnewlinkhqemp(positionid,salesper,hqid) select " + ddEmployee.SelectedValue + "," + txtEmpPercentage.Text + ", headquarter_id from smhq1 where headquarter_id in (" + condstring + ")";
                try
                {
                    SqlCommand cmd = new SqlCommand(cmdstring, con);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    BindEMPHQYesNo();
                }
                catch (Exception EX) { }
                finally { con.Close(); }

            }
        }

        protected void btnRemoveEmpHQ_Click(object sender, EventArgs e)
        {
            string condstring = "";
            if (lstEmpHQYes.SelectedIndex >= 0)
            {
                for (int i = 0; i <= lstEmpHQYes.Items.Count - 1; i++)
                {
                    if (lstEmpHQYes.Items[i].Selected == true)
                    {
                        if (condstring == "") { condstring = lstEmpHQYes.Items[i].Value; } else { condstring = condstring + "," + lstEmpHQYes.Items[i].Value; }
                    }
                }
                SqlConnection con = new SqlConnection(sConnectionString);

                string cmdstring = "delete from smnewlinkhqemp where positionid=" + ddEmployee.SelectedValue + " and hqid in (" + condstring + ")";
                try
                {
                    SqlCommand cmd = new SqlCommand(cmdstring, con);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    BindEMPHQYesNo();
                }
                catch { }
                finally { con.Close(); }
            }
        }

        protected void btnMapEmpHQ_Click(object sender, EventArgs e)
        {



            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myModal", "$('#myModal').modal();", true);
            upModal.Update();
        }

        protected void ModalSubmit_Click(object sender, EventArgs e)
        {

        }

        protected void RadGrid1_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            //if (RadGrid1.Columns.Count == 0) 
            //{
            //    RadGrid1.MasterTableView.DataKeyNames = new string[] { "positionid" };
            //    RadGrid1.AllowPaging = true;
            //    RadGrid1.MasterTableView.AutoGenerateColumns = false;
            //    RadGrid1.PageSize = 15;
            //    Telerik.Web.UI.GridBoundColumn boundColumn;
            //    boundColumn = new GridBoundColumn();
            //    RadGrid1.MasterTableView.Columns.Add(boundColumn);
            //    boundColumn.DataField = "PositionID";
            //    boundColumn.HeaderText = "ID";
            //    boundColumn.ItemStyle.Width = Unit.Pixel(90);
            //    boundColumn = new GridBoundColumn();
            //    RadGrid1.MasterTableView.Columns.Add(boundColumn);
            //    boundColumn.DataField = "positionname";
            //    boundColumn.HeaderText = "Pos Name";
            //    boundColumn = new GridBoundColumn();
            //    RadGrid1.MasterTableView.Columns.Add(boundColumn);
            //    boundColumn.DataField = "linkhq";
            //    boundColumn.HeaderText = "HQ";
            //    boundColumn = new GridBoundColumn();
            //    RadGrid1.MasterTableView.Columns.Add(boundColumn);
            //    boundColumn.DataField = "Name";
            //    boundColumn.HeaderText = "Employee";


            //}
            RadGrid1.AllowPaging = false;
            string Sql = "select distinct PU.positionid,PU.positionname,u.FIRSTNAME+' '+u.lastname Name,hq1.headquarter_Name LinkHQ from smnewuser pu " +
              "  left join smnewuser u on u.positionid = pu.positionid and u.ACTIVE = 'Y' " +
              "  left join smNewLinkHQEMP le on le.POSITIONID = pu.positionid " +
              "  left join smHQ1 hq1 on hq1.headquarter_Id = le.HQID " +
              "  where pu.positionid is not null and pu.positionactive = 'Y' and " +
              " pu.FIELD = 'Y'  and PU.DESIGGROUPID in ('MR', 'SBE') order by hq1.headquarter_name";
            DataTable dt = new DataTable();
            dt = getDataTable(Sql);
            RadGrid1.DataSource = dt;
        }

        protected void radMapStockistHQ_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            RadGrid1.AllowPaging = false;
            string Sql = "	select s.stockist_id,s.Stockist_Name,ty.typedesc,s.Stockist_City " +
            ", hq.headquarter_Name,hq.headquarter_Id "+
            " from smstockist s " +
            " left join  smnewLinkHQStockist l on s.stockist_id = l.stockist_id "+
            " left join smtype ty on ty.type = s.stockist_type " +
            " left join smHQ1 hq on hq.headquarter_Id = l.headquarter_id";
            DataTable dt = new DataTable();
            dt = getDataTable(Sql);
            radMapStockistHQ.DataSource = dt;
        }

        protected void btnMapStockistHQ_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myModal1", "$('#myModal1').modal();", true);
            upModal1.Update();
        }
    }
}