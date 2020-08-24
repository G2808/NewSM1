<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.master" AutoEventWireup="true" CodeBehind="TertiaryStockAndSalesMonthly.aspx.cs" Inherits="NewSM.TertiaryStockAndSalesMonthly" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<script type="text/javascript">
    function ShowPopup() {
        //$("#MyPopup .modal-title").html(title);
        //$("#MyPopup .modal-body").html(body);
        $("#MyPopup").modal("show");
    }
</script>
    <style>
            .RadGrid_Default .rgCommandRow a {
                color: #000000;
                float: right !important;
                margin-left: 1145px;
                position: absolute;
                top: 13px;
                text-decoration: none;
            }
            .RadGrid_Default .rgAdd {
                position: absolute;
                float: right !important;  
                top:13px;
                left:1140px;
            }
    </style>
        <asp:Panel runat="server" ID="Panel_Search">
            <div class="container-fluid" style="text-align:center;margin-top:0px">
        <div class="row justify-content-center" style="margin-top:0px">
        <h4>Stock and Sales</h4>
        </div>
        <asp:UpdatePanel ID="UpdatePanelSearch" runat="server">
           <ContentTemplate>
               <div class="container-fluid">
                    <div class="row">
                        <div class="col-1" style="text-align:left">Year</div>
                        <div class="col-1" style="text-align:left">Month    </div>
                        <div class="col-2" style="text-align:left">City</div>
                        <div class="col-2" style="text-align:left">Head Quarter</div>
                        <div class="col-2" style="text-align:left">Type</div>
                        <div class="col-3" style="text-align:left">Customer</div>
                    </div>
                        <div class="row">
                            <div class="col-1 " style="padding:2px;margin:0">
                                 <asp:DropDownList ID="cmbYear" runat="server"   AutoPostBack="True" CssClass="form-control">
                                  </asp:DropDownList>                  
                            </div>
                            <div class="col-1" style="padding:2px;margin:0">
                                <asp:DropDownList ID="cmbMonth" runat="server"  CssClass="form-control">
                                 </asp:DropDownList>                
                            </div>
                            <div class="col-2" style="padding:2px;margin:0">
                                 <asp:DropDownList ID="cmbHQ" runat="server" Width="100%" AutoPostBack="True" CssClass="form-control" OnSelectedIndexChanged="cmbHQ_SelectedIndexChanged1">
                                 </asp:DropDownList>             
                            </div>
                            <div class="col-2" style="padding:2px;margin:0">
                                 <asp:DropDownList ID="cmbCity" runat="server"  Width="100%" AutoPostBack="True" CssClass="form-control" OnSelectedIndexChanged="cmbCity_SelectedIndexChanged1">
                                  </asp:DropDownList>           
                            </div>
                            <div class="col-2" style="padding:2px;margin:0">
                                 <asp:DropDownList ID="cmbType" runat="server"   Width="100%" AutoPostBack="True" CssClass="form-control">
                                  </asp:DropDownList>        
                            </div>
                            <div class="col-3" style="padding:2px;margin:0">
                                  <asp:DropDownList ID="cmbParty" runat="server" Width="100%" AutoPostBack="True" CssClass="form-control">
                                  </asp:DropDownList>    
                            </div>
                        </div>
                        <div class="row justify-content-center">
                             <asp:Label ID="lblError" runat="server" Text="" ForeColor="Red"></asp:Label>
                        </div>
                </div>
        </ContentTemplate>
        </asp:UpdatePanel>
        <div class="row btn-block" style=" text-align: center; margin-top: 10px; margin-bottom: 10px">
                            <asp:LinkButton ID="btnSearch" runat="server" Text="Search" CssClass="btn  btn-info" Width="90px" OnClick="btnSearch_Click">
                            </asp:LinkButton>
                            <asp:LinkButton ID="btnNew" runat="server" Text="Add" CssClass="btn btn-primary" Width="90px" OnClick="btnNew_Click">
                            </asp:LinkButton>
                            <asp:LinkButton ID="cmdExit" runat="server" Text="Exit" CssClass="btn btn-danger" Width="90px" OnClick="cmdExit_Click">
                            </asp:LinkButton>
                </div>

                    <telerik:RadGrid ID="radData" runat="server" Font-Size="Smaller" class="table-responsive" RenderMode="Auto" Skin="Bootstrap" Width="100%" Style="overflow: auto" EnableAriaSupport="True" PageSize="9" OnDataBound="radData_DataBound" Height="100%" OnItemCommand="radData_ItemCommand" OnNeedDataSource="radData_NeedDataSource" OnPreRender="radData_PreRender"  >
                        <GroupingSettings CollapseAllTooltip="Collapse all groups" />
                        <ExportSettings IgnorePaging="true"></ExportSettings>
                        <MasterTableView CommandItemDisplay="TopAndBottom">
                            <CommandItemSettings ShowAddNewRecordButton="False" ShowExportToCsvButton="True" ShowExportToExcelButton="True" ShowRefreshButton="false" />
                            <Columns>
                                <telerik:GridButtonColumn ButtonType="ImageButton" ItemStyle-HorizontalAlign="Center" ImageUrl="~/Images/Edit_New.png" HeaderText="" CommandName="CMDEDIT" FilterControlAltText="Filter column column" UniqueName="column">
                                </telerik:GridButtonColumn>
                       </Columns> 
                         </MasterTableView>
                        <FilterMenu RenderMode="Auto">
                        </FilterMenu>
                        <HeaderContextMenu RenderMode="Auto">
                        </HeaderContextMenu>
                     </telerik:RadGrid>

    </div>
    </asp:Panel>
        <asp:Panel ID="Panel_Item" runat="server" Visible="false" Width="500px" DefaultButton="Submit" BackColor="White" >  
             <div class="container-fluid" style="margin:auto">
                              <div class=" panel-info">
                                    <div class="panel-heading">
                                                 <h4>
                                                    Add Tertiary Sales
                                                </h4>
                                        </div>
                                    <div class="panel-body">
                                                                 <div class="form-group row">
                                                                    <label for="full_name_id" class="col-sm-2 col-form-label" style="margin-top:5px">Customer</label>
                                                                     <div class="col-sm-10">
                                                                           <asp:TextBox ID="txtParty" runat="server" CssClass="form-control" ReadOnly="True" AutoPostBack="false" ></asp:TextBox>
                                                                     </div>
                                                                </div>   												
                                                                 <div class="form-group row">
                                                                    <label for="full_name_id" class="col-sm-2 col-form-label" style="margin-top:5px">Product</label>
                                                                     <div class="col-sm-10">
                                                                           <asp:TextBox ID="txtProduct" runat="server" CssClass="form-control" ReadOnly="True" AutoPostBack="false"></asp:TextBox>
                                                                     </div>
                                                                </div>   
                                                                 <div class="form-group row">
                                                                    <label for="full_name_id" class="col-sm-2 col-form-label" style="margin-top:5px">Operning</label>
                                                                     <div class="col-sm-10">
                                                                           <asp:TextBox ID="txtOpening" runat="server" CssClass="form-control" ReadOnly="True" AutoPostBack="false" ></asp:TextBox>
                                                                     </div>
                                                                </div>   
                                                                 <div class="form-group row">
                                                                    <label for="full_name_id" class="col-sm-2 col-form-label" style="margin-top:5px">Purchase</label>
                                                                     <div class="col-sm-10">
                                                                           <asp:TextBox ID="txtPurchase" runat="server" CssClass="form-control" ReadOnly="True" AutoPostBack="false" ></asp:TextBox>
                                                                     </div>
                                                                </div>   
                                                                 <div class="form-group row">
                                                                    <label for="full_name_id" class="col-sm-2 col-form-label" style="margin-top:5px">Sales</label>
                                                                     <div class="col-sm-10">
                                                                           <asp:TextBox ID="txtSales" runat="server" CssClass="form-control" ReadOnly="false" AutoPostBack="True" OnTextChanged="txtSales_TextChanged"></asp:TextBox>
                                                                     </div>
                                                                </div> 
                                                                 <div class="form-group row">
                                                                    <label for="full_name_id" class="col-sm-2 col-form-label" style="margin-top:5px">Closing</label>
                                                                     <div class="col-sm-10">
                                                                           <asp:TextBox ID="txtClosing" runat="server" CssClass="form-control" ReadOnly="True" AutoPostBack="false"></asp:TextBox>
                                                                     </div>
                                                                </div>
                                                                <div class="row" style="text-align:center">
                                                                         <asp:Label ID="lblErrorModal" runat="server" Text="" ForeColor="Red" Font-Bold="true"></asp:Label>              
                                                                </div>
                                        </div>
                                    <div class="panel-footer">
                                                    <asp:Button ID="Submit" CssClass="btn btn-info" runat="server" Text="Submit" OnClick="Submit_Click" Width="150px" />
                                                    <asp:Button ID="cmdExitItemAdd" CssClass="btn  btn-danger" runat="server" Width="150px" Text="Exit" OnClick="cmdExitItemAdd_Click" />

                                        </div>
 
                              </div>
              </div>
            </asp:Panel>
        <asp:Panel runat="server" ID="Panel_AddEdit">
                                <asp:TextBox ID="txtType" runat="server" Visible="False" Width="72px"></asp:TextBox>
                                <asp:TextBox ID="txtStockist" runat="server" Visible="False" Width="72px"></asp:TextBox>
                                <asp:TextBox ID="txtUser" runat="server" Visible="False" Width="72px"></asp:TextBox>
                                <asp:TextBox ID="txtHQ" runat="server" Visible="False" Width="72px"></asp:TextBox>
                                <asp:TextBox ID="txtUserID" runat="server" Visible="False" Width="72px"></asp:TextBox>
                                <asp:Literal ID="ActFlag" runat="server" Visible="False"></asp:Literal>
                                <div class="row">
                                    <div class="col-md-12" style="text-align:center">
                                        <asp:Label ID="lblStockist" runat="server" Text="" Font-Bold="true" Font-Size="X-Large"></asp:Label>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12" style="text-align:center">
                                        <asp:Label ID="lblMonthYear" runat="server" Text="" Font-Bold="true" Font-Size="Medium"></asp:Label>
                                    </div>
                                </div>
                                <div class="row" style="text-align:center;margin-top:5px">
                                    <asp:Label ID="lblItemError" runat="server" Text="" Font-Bold="true" ForeColor="red"></asp:Label>
                                </div>
                                 <div class="container-fluid" style="width:100%">
                                     <asp:PlaceHolder ID="phAddItem" runat="server" >
                                     </asp:PlaceHolder>
                                        <table class="table">
                                                      <thead>
                                                        <tr>
                                                          <th scope="col">Products</th>
                                                          <th scope="col">Opening</th>
                                                          <th scope="col">Purchase</th>
                                                          <th scope="col">Sales</th>
                                                          <th scope="col">Closing</th>
                                                        </tr>
                                                      </thead>
                                                      <tbody>
                                                        <tr>
                                                          <th scope="row">DIAM</th>
                                                          <td>
                                                              <asp:TextBox ID="txtOPEDIAM" runat="server" CssClass="form-control" Enabled="false" ></asp:TextBox>
                                                          </td>
                                                          <td>
                                                              <asp:TextBox ID="txtPURDIAM" runat="server" CssClass="form-control" Enabled="false" ></asp:TextBox>
                                                          </td>
                                                          <td>
                                                              <asp:TextBox ID="txtSALDIAM" runat="server" CssClass="form-control" Enabled="true" AutoPostBack="True" CausesValidation="True" OnTextChanged="txtSALDIAM_TextChanged" ></asp:TextBox>
                                                          </td>
                                                          <td>
                                                              <asp:TextBox ID="txtCLODIAM" runat="server" CssClass="form-control" Enabled="false" ></asp:TextBox>
                                                          </td>
                                                        </tr>
                                                        <tr>
                                                          <th scope="row">DMR</th>
                                                          <td>
                                                              <asp:TextBox ID="txtOPEDMR" runat="server" CssClass="form-control" Enabled="false" ></asp:TextBox>
                                                          </td>
                                                          <td>
                                                              <asp:TextBox ID="txtPURDMR" runat="server" CssClass="form-control" Enabled="false" ></asp:TextBox>
                                                          </td>
                                                          <td>
                                                              <asp:TextBox ID="txtSALDMR" runat="server" CssClass="form-control" Enabled="true" AutoPostBack="True" OnTextChanged="txtSALDMR_TextChanged" ></asp:TextBox>
                                                          </td>
                                                          <td>
                                                              <asp:TextBox ID="txtCLODMR" runat="server" CssClass="form-control" Enabled="false" ></asp:TextBox>
                                                          </td>
                                                        <tr>
                                                          <th scope="row">DMR60</th>
                                                          <td>
                                                              <asp:TextBox ID="txtOPEDMR60" runat="server" CssClass="form-control" Enabled="false" ></asp:TextBox>
                                                          </td>
                                                          <td>
                                                              <asp:TextBox ID="txtPURDMR60" runat="server" CssClass="form-control" Enabled="false" ></asp:TextBox>
                                                          </td>
                                                          <td>
                                                              <asp:TextBox ID="txtSALDMR60" runat="server" CssClass="form-control" Enabled="true" AutoPostBack="True" OnTextChanged="txtSALDMR60_TextChanged" ></asp:TextBox>
                                                          </td>
                                                          <td>
                                                              <asp:TextBox ID="txtCLODMR60" runat="server" CssClass="form-control" Enabled="false" ></asp:TextBox>
                                                          </td>
                                                        </tr>
                                                        <tr>
                                                          <th scope="row">DXR60</th>
                                                          <td>
                                                              <asp:TextBox ID="txtOPEDXR60" runat="server" CssClass="form-control" Enabled="false" ></asp:TextBox>
                                                          </td>
                                                          <td>
                                                              <asp:TextBox ID="txtPURDXR60" runat="server" CssClass="form-control" Enabled="false" ></asp:TextBox>
                                                          </td>
                                                          <td>
                                                              <asp:TextBox ID="txtSALDXR60" runat="server" CssClass="form-control" Enabled="true" AutoPostBack="True" OnTextChanged="txtSALDXR60_TextChanged"  ></asp:TextBox>
                                                          </td>
                                                          <td>
                                                              <asp:TextBox ID="txtCLODXR60" runat="server" CssClass="form-control" Enabled="false" ></asp:TextBox>
                                                          </td>
                                                        </tr>
                                                        <tr>
                                                          <th scope="row">DXRMEX</th>
                                                          <td>
                                                              <asp:TextBox ID="txtOPEDXRMEX" runat="server" CssClass="form-control" Enabled="false" ></asp:TextBox>
                                                          </td>
                                                          <td>
                                                              <asp:TextBox ID="txtPURDXRMEX" runat="server" CssClass="form-control" Enabled="false" ></asp:TextBox>
                                                          </td>
                                                          <td>
                                                              <asp:TextBox ID="txtSALDXRMEX" runat="server" CssClass="form-control" Enabled="true" AutoPostBack="True" OnTextChanged="txtSALDXRMEX_TextChanged" ></asp:TextBox>
                                                          </td>
                                                          <td>
                                                              <asp:TextBox ID="txtCLODXRMEX" runat="server" CssClass="form-control" Enabled="false" ></asp:TextBox>
                                                          </td>
                                                        </tr>
                                                        <tr>
                                                          <th scope="row">COV2</th>
                                                          <td>
                                                              <asp:TextBox ID="txtOPECOV2" runat="server" CssClass="form-control" Enabled="false" ></asp:TextBox>
                                                          </td>
                                                          <td>
                                                              <asp:TextBox ID="txtPURCOV2" runat="server" CssClass="form-control" Enabled="false" ></asp:TextBox>
                                                          </td>
                                                          <td>
                                                              <asp:TextBox ID="txtSALCOV2" runat="server" CssClass="form-control" Enabled="true" AutoPostBack="True" OnTextChanged="txtSALCOV2_TextChanged" ></asp:TextBox>
                                                          </td>
                                                          <td>
                                                              <asp:TextBox ID="txtCLOCOV2" runat="server" CssClass="form-control" Enabled="false" ></asp:TextBox>
                                                          </td>
                                                        </tr>
                                                        <tr>
                                                          <th scope="row">COV4</th>
                                                          <td>
                                                              <asp:TextBox ID="txtOPECOV4" runat="server" CssClass="form-control" Enabled="false" ></asp:TextBox>
                                                          </td>
                                                          <td>
                                                              <asp:TextBox ID="txtPURCOV4" runat="server" CssClass="form-control" Enabled="false" ></asp:TextBox>
                                                          </td>
                                                          <td>
                                                              <asp:TextBox ID="txtSALCOV4" runat="server" CssClass="form-control" Enabled="true" AutoPostBack="True" OnTextChanged="txtSALCOV4_TextChanged" ></asp:TextBox>
                                                          </td>
                                                          <td>
                                                              <asp:TextBox ID="txtCLOCOV4" runat="server" CssClass="form-control" Enabled="false" ></asp:TextBox>
                                                          </td>
                                                        </tr>
                                                        <tr>
                                                          <th scope="row">COV8</th>
                                                          <td>
                                                              <asp:TextBox ID="txtOPECOV8" runat="server" CssClass="form-control" Enabled="false" ></asp:TextBox>
                                                          </td>
                                                          <td>
                                                              <asp:TextBox ID="txtPURCOV8" runat="server" CssClass="form-control" Enabled="false" ></asp:TextBox>
                                                          </td>
                                                          <td>
                                                              <asp:TextBox ID="txtSALCOV8" runat="server" CssClass="form-control" Enabled="true" AutoPostBack="True" OnTextChanged="txtSALCOV8_TextChanged" ></asp:TextBox>
                                                          </td>
                                                          <td>
                                                              <asp:TextBox ID="txtCLOCOV8" runat="server" CssClass="form-control" Enabled="false" ></asp:TextBox>
                                                          </td>
                                                        </tr>
													    <tr>
                                                          <th scope="row">COVPL</th>
                                                          <td>
                                                              <asp:TextBox ID="txtOPECOVPL" runat="server" CssClass="form-control" Enabled="false" ></asp:TextBox>
                                                          </td>
                                                          <td>
                                                              <asp:TextBox ID="txtPURCOVPL" runat="server" CssClass="form-control" Enabled="false" ></asp:TextBox>
                                                          </td>
                                                          <td>
                                                              <asp:TextBox ID="txtSALCOVPL" runat="server" CssClass="form-control" Enabled="true" AutoPostBack="True" OnTextChanged="txtSALCOVPL_TextChanged" ></asp:TextBox>
                                                          </td>
                                                          <td>
                                                              <asp:TextBox ID="txtCLOCOVPL" runat="server" CssClass="form-control" Enabled="false" ></asp:TextBox>
                                                          </td>
                                                        </tr>	
                                                        <tr>
                                                          <th scope="row">COVAM</th>
                                                          <td>
                                                              <asp:TextBox ID="txtOPECOVAM" runat="server" CssClass="form-control" Enabled="false" ></asp:TextBox>
                                                          </td>
                                                          <td>
                                                              <asp:TextBox ID="txtPURCOVAM" runat="server" CssClass="form-control" Enabled="false" ></asp:TextBox>
                                                          </td>
                                                          <td>
                                                              <asp:TextBox ID="txtSALCOVAM" runat="server" CssClass="form-control" Enabled="true" AutoPostBack="True" OnTextChanged="txtSALCOVAM_TextChanged" ></asp:TextBox>
                                                          </td>
                                                          <td>
                                                              <asp:TextBox ID="txtCLOCOVAM" runat="server" CssClass="form-control" Enabled="false" ></asp:TextBox>
                                                          </td>
                                                        </tr>														
                                                        <tr>
                                                          <th scope="row">C4AM10</th>
                                                          <td>
                                                              <asp:TextBox ID="txtOPEC4AM10" runat="server" CssClass="form-control" Enabled="false" ></asp:TextBox>
                                                          </td>
                                                          <td>
                                                              <asp:TextBox ID="txtPURC4AM10" runat="server" CssClass="form-control" Enabled="false" ></asp:TextBox>
                                                          </td>
                                                          <td>
                                                              <asp:TextBox ID="txtSALC4AM10" runat="server" CssClass="form-control" Enabled="true" AutoPostBack="True" OnTextChanged="txtSALC4AM10_TextChanged" ></asp:TextBox>
                                                          </td>
                                                          <td>
                                                              <asp:TextBox ID="txtCLOC4AM10" runat="server" CssClass="form-control" Enabled="false" ></asp:TextBox>
                                                          </td>
                                                        </tr>
                                                        <tr>
                                                          <th scope="row">C8AM10</th>
                                                          <td>
                                                              <asp:TextBox ID="txtOPEC8AM10" runat="server" CssClass="form-control" Enabled="false" ></asp:TextBox>
                                                          </td>
                                                          <td>
                                                              <asp:TextBox ID="txtPURC8AM10" runat="server" CssClass="form-control" Enabled="false" ></asp:TextBox>
                                                          </td>
                                                          <td>
                                                              <asp:TextBox ID="txtSALC8AM10" runat="server" CssClass="form-control" Enabled="true" AutoPostBack="True" OnTextChanged="txtSALC8AM10_TextChanged" ></asp:TextBox>
                                                          </td>
                                                          <td>
                                                              <asp:TextBox ID="txtCLOC8AM10" runat="server" CssClass="form-control" Enabled="false" ></asp:TextBox>
                                                          </td>
                                                        </tr>
                                                        <tr>
                                                          <th scope="row">C8AM5</th>
                                                          <td>
                                                              <asp:TextBox ID="txtOPEC8AM5" runat="server" CssClass="form-control" Enabled="false" ></asp:TextBox>
                                                          </td>
                                                          <td>
                                                              <asp:TextBox ID="txtPURC8AM5" runat="server" CssClass="form-control" Enabled="false" ></asp:TextBox>
                                                          </td>
                                                          <td>
                                                              <asp:TextBox ID="txtSALC8AM5" runat="server" CssClass="form-control" Enabled="true" AutoPostBack="True" OnTextChanged="txtSALC8AM5_TextChanged" ></asp:TextBox>
                                                          </td>
                                                          <td>
                                                              <asp:TextBox ID="txtCLOC8AM5" runat="server" CssClass="form-control" Enabled="false" ></asp:TextBox>
                                                          </td>
                                                        </tr>
                                                        <tr>
                                                          <th scope="row">COVPLHD</th>
                                                          <td>
                                                              <asp:TextBox ID="txtOPECOVPLHD" runat="server" CssClass="form-control" Enabled="false" ></asp:TextBox>
                                                          </td>
                                                          <td>
                                                              <asp:TextBox ID="txtPURCOVPLHD" runat="server" CssClass="form-control" Enabled="false" ></asp:TextBox>
                                                          </td>
                                                          <td>
                                                              <asp:TextBox ID="txtSALCOVPLHD" runat="server" CssClass="form-control" Enabled="true" AutoPostBack="True" OnTextChanged="txtSALCOVPLHD_TextChanged" ></asp:TextBox>
                                                          </td>
                                                          <td>
                                                              <asp:TextBox ID="txtCLOCOVPLHD" runat="server" CssClass="form-control" Enabled="false" ></asp:TextBox>
                                                          </td>
                                                        </tr>		
                                                        <tr>
                                                          <th scope="row">DAF500</th>
                                                          <td>
                                                              <asp:TextBox ID="txtOPEDAF500" runat="server" CssClass="form-control" Enabled="false" ></asp:TextBox>
                                                          </td>
                                                          <td>
                                                              <asp:TextBox ID="txtPURDAF500" runat="server" CssClass="form-control" Enabled="false" ></asp:TextBox>
                                                          </td>
                                                          <td>
                                                              <asp:TextBox ID="txtSALDAF500" runat="server" CssClass="form-control" Enabled="true" AutoPostBack="True" OnTextChanged="txtSALDAF500_TextChanged" ></asp:TextBox>
                                                          </td>
                                                          <td>
                                                              <asp:TextBox ID="txtCLODAF500" runat="server" CssClass="form-control" Enabled="false" ></asp:TextBox>
                                                          </td>
                                                        </tr>
                                                        <tr>
                                                          <th scope="row">ARC</th>
                                                          <td>
                                                              <asp:TextBox ID="txtOPEARC" runat="server" CssClass="form-control" Enabled="false" ></asp:TextBox>
                                                          </td>
                                                          <td>
                                                              <asp:TextBox ID="txtPURARC" runat="server" CssClass="form-control" Enabled="false" ></asp:TextBox>
                                                          </td>
                                                          <td>
                                                              <asp:TextBox ID="txtSALARC" runat="server" CssClass="form-control" Enabled="true" AutoPostBack="True" OnTextChanged="txtSALARC_TextChanged" ></asp:TextBox>
                                                          </td>
                                                          <td>
                                                              <asp:TextBox ID="txtCLOARC" runat="server" CssClass="form-control" Enabled="false" ></asp:TextBox>
                                                          </td>
                                                        </tr>
                                                        <tr>
                                                          <th scope="row">NIX</th>
                                                          <td>
                                                              <asp:TextBox ID="txtOPENIX" runat="server" CssClass="form-control" Enabled="false" ></asp:TextBox>
                                                          </td>
                                                          <td>
                                                              <asp:TextBox ID="txtPURNIX" runat="server" CssClass="form-control" Enabled="false" ></asp:TextBox>
                                                          </td>
                                                          <td>
                                                              <asp:TextBox ID="txtSALNIX" runat="server" CssClass="form-control" Enabled="true" AutoPostBack="True" OnTextChanged="txtSALNIX_TextChanged" ></asp:TextBox>
                                                          </td>
                                                          <td>
                                                              <asp:TextBox ID="txtCLONIX" runat="server" CssClass="form-control" Enabled="false" ></asp:TextBox>
                                                          </td>
                                                        </tr>	
                                                        <tr>
                                                          <th scope="row">NSR</th>
                                                          <td>
                                                              <asp:TextBox ID="txtOPENSR" runat="server" CssClass="form-control" Enabled="false" ></asp:TextBox>
                                                          </td>
                                                          <td>
                                                              <asp:TextBox ID="txtPURNSR" runat="server" CssClass="form-control" Enabled="false" ></asp:TextBox>
                                                          </td>
                                                          <td>
                                                              <asp:TextBox ID="txtSALNSR" runat="server" CssClass="form-control" Enabled="true" AutoPostBack="True" OnTextChanged="txtSALNSR_TextChanged" ></asp:TextBox>
                                                          </td>
                                                          <td>
                                                              <asp:TextBox ID="txtCLONSR" runat="server" CssClass="form-control" Enabled="false" ></asp:TextBox>
                                                          </td>
                                                        </tr>		
                                                        <tr>
                                                          <th scope="row">NAM</th>
                                                          <td>
                                                              <asp:TextBox ID="txtOPENAM" runat="server" CssClass="form-control" Enabled="false" ></asp:TextBox>
                                                          </td>
                                                          <td>
                                                              <asp:TextBox ID="txtPURNAM" runat="server" CssClass="form-control" Enabled="false" ></asp:TextBox>
                                                          </td>
                                                          <td>
                                                              <asp:TextBox ID="txtSALNAM" runat="server" CssClass="form-control" Enabled="true" AutoPostBack="True" OnTextChanged="txtSALNAM_TextChanged" ></asp:TextBox>
                                                          </td>
                                                          <td>
                                                              <asp:TextBox ID="txtCLONAM" runat="server" CssClass="form-control" Enabled="false" ></asp:TextBox>
                                                          </td>
                                                        </tr>		
                                                        <tr>
                                                          <th scope="row">NAM25</th>
                                                          <td>
                                                              <asp:TextBox ID="txtOPENAM25" runat="server" CssClass="form-control" Enabled="false" ></asp:TextBox>
                                                          </td>
                                                          <td>
                                                              <asp:TextBox ID="txtPURNAM25" runat="server" CssClass="form-control" Enabled="false" ></asp:TextBox>
                                                          </td>
                                                          <td>
                                                              <asp:TextBox ID="txtSALNAM25" runat="server" CssClass="form-control" Enabled="true" AutoPostBack="True" OnTextChanged="txtSALNAM25_TextChanged" ></asp:TextBox>
                                                          </td>
                                                          <td>
                                                              <asp:TextBox ID="txtCLONAM25" runat="server" CssClass="form-control" Enabled="false" ></asp:TextBox>
                                                          </td>
                                                        </tr>
                                                        <tr>
                                                          <th scope="row">NAM10</th>
                                                          <td>
                                                              <asp:TextBox ID="txtOPENAM10" runat="server" CssClass="form-control" Enabled="false" ></asp:TextBox>
                                                          </td>
                                                          <td>
                                                              <asp:TextBox ID="txtPURNAM10" runat="server" CssClass="form-control" Enabled="false" ></asp:TextBox>
                                                          </td>
                                                          <td>
                                                              <asp:TextBox ID="txtSALNAM10" runat="server" CssClass="form-control" Enabled="true" AutoPostBack="True" OnTextChanged="txtSALNAM10_TextChanged" ></asp:TextBox>
                                                          </td>
                                                          <td>
                                                              <asp:TextBox ID="txtCLONAM10" runat="server" CssClass="form-control" Enabled="false" ></asp:TextBox>
                                                          </td>
                                                        </tr>
                                                        <tr>
                                                          <th scope="row">FLA20</th>
                                                          <td>
                                                              <asp:TextBox ID="txtOPEFLA20" runat="server" CssClass="form-control" Enabled="false" ></asp:TextBox>
                                                          </td>
                                                          <td>
                                                              <asp:TextBox ID="txtPURFLA20" runat="server" CssClass="form-control" Enabled="false" ></asp:TextBox>
                                                          </td>
                                                          <td>
                                                              <asp:TextBox ID="txtSALFLA20" runat="server" CssClass="form-control" Enabled="true" AutoPostBack="True" OnTextChanged="txtSALFLA20_TextChanged" ></asp:TextBox>
                                                          </td>
                                                          <td>
                                                              <asp:TextBox ID="txtCLOFLA20" runat="server" CssClass="form-control" Enabled="false" ></asp:TextBox>
                                                          </td>
                                                        </tr>														
                                                        <tr>
                                                          <th scope="row">FMR</th>
                                                          <td>
                                                              <asp:TextBox ID="txtOPEFMR" runat="server" CssClass="form-control" Enabled="false" ></asp:TextBox>
                                                          </td>
                                                          <td>
                                                              <asp:TextBox ID="txtPURFMR" runat="server" CssClass="form-control" Enabled="false" ></asp:TextBox>
                                                          </td>
                                                          <td>
                                                              <asp:TextBox ID="txtSALFMR" runat="server" CssClass="form-control" Enabled="true" AutoPostBack="True" OnTextChanged="txtSALFMR_TextChanged" ></asp:TextBox>
                                                          </td>
                                                          <td>
                                                              <asp:TextBox ID="txtCLOFMR" runat="server" CssClass="form-control" Enabled="false" ></asp:TextBox>
                                                          </td>
                                                        </tr>
                                                        <tr>
                                                          <th scope="row">STAB</th>
                                                          <td>
                                                              <asp:TextBox ID="txtOPESTAB" runat="server" CssClass="form-control" Enabled="false" ></asp:TextBox>
                                                          </td>
                                                          <td>
                                                              <asp:TextBox ID="txtPURSTAB" runat="server" CssClass="form-control" Enabled="false" ></asp:TextBox>
                                                          </td>
                                                          <td>
                                                              <asp:TextBox ID="txtSALSTAB" runat="server" CssClass="form-control" Enabled="true" AutoPostBack="True" OnTextChanged="txtSALSTAB_TextChanged" ></asp:TextBox>
                                                          </td>
                                                          <td>
                                                              <asp:TextBox ID="txtCLOSTAB" runat="server" CssClass="form-control" Enabled="false" ></asp:TextBox>
                                                          </td>
                                                        </tr>		
                                                        <tr>
                                                          <th scope="row">TLA</th>
                                                          <td>
                                                              <asp:TextBox ID="txtOPETLA" runat="server" CssClass="form-control" Enabled="false" ></asp:TextBox>
                                                          </td>
                                                          <td>
                                                              <asp:TextBox ID="txtPURTLA" runat="server" CssClass="form-control" Enabled="false" ></asp:TextBox>
                                                          </td>
                                                          <td>
                                                              <asp:TextBox ID="txtSALTLA" runat="server" CssClass="form-control" Enabled="true" AutoPostBack="True" OnTextChanged="txtSALTLA_TextChanged" ></asp:TextBox>
                                                          </td>
                                                          <td>
                                                              <asp:TextBox ID="txtCLOTLA" runat="server" CssClass="form-control" Enabled="false" ></asp:TextBox>
                                                          </td>
                                                        </tr>
                                                        <tr>
                                                          <th scope="row">COR5</th>
                                                          <td>
                                                              <asp:TextBox ID="txtOPECOR5" runat="server" CssClass="form-control" Enabled="false" ></asp:TextBox>
                                                          </td>
                                                          <td>
                                                              <asp:TextBox ID="txtPURCOR5" runat="server" CssClass="form-control" Enabled="false" ></asp:TextBox>
                                                          </td>
                                                          <td>
                                                              <asp:TextBox ID="txtSALCOR5" runat="server" CssClass="form-control" Enabled="true" AutoPostBack="True" OnTextChanged="txtSALCOR5_TextChanged" ></asp:TextBox>
                                                          </td>
                                                          <td>
                                                              <asp:TextBox ID="txtCLOCOR5" runat="server" CssClass="form-control" Enabled="false" ></asp:TextBox>
                                                          </td>
                                                        </tr>		
                                                        <tr>
                                                          <th scope="row">COR75</th>
                                                          <td>
                                                              <asp:TextBox ID="txtOPECOR75" runat="server" CssClass="form-control" Enabled="false" ></asp:TextBox>
                                                          </td>
                                                          <td>
                                                              <asp:TextBox ID="txtPURCOR75" runat="server" CssClass="form-control" Enabled="false" ></asp:TextBox>
                                                          </td>
                                                          <td>
                                                              <asp:TextBox ID="txtSALCOR75" runat="server" CssClass="form-control" Enabled="true" AutoPostBack="True" OnTextChanged="txtSALCOR75_TextChanged" ></asp:TextBox>
                                                          </td>
                                                          <td>
                                                              <asp:TextBox ID="txtCLOCOR75" runat="server" CssClass="form-control" Enabled="false" ></asp:TextBox>
                                                          </td>
                                                        </tr>	
                                                        <tr>
                                                          <th scope="row">DAF1000</th>
                                                          <td>
                                                              <asp:TextBox ID="txtOPEDAF1000" runat="server" CssClass="form-control" Enabled="false" ></asp:TextBox>
                                                          </td>
                                                          <td>
                                                              <asp:TextBox ID="txtPURDAF1000" runat="server" CssClass="form-control" Enabled="false" ></asp:TextBox>
                                                          </td>
                                                          <td>
                                                              <asp:TextBox ID="txtSALDAF1000" runat="server" CssClass="form-control" Enabled="true" AutoPostBack="True" OnTextChanged="txtSALDAF1000_TextChanged" ></asp:TextBox>
                                                          </td>
                                                          <td>
                                                              <asp:TextBox ID="txtCLODAF1000" runat="server" CssClass="form-control" Enabled="false" ></asp:TextBox>
                                                          </td>
                                                        </tr>
                                                        <tr>
                                                          <th scope="row">TXAM</th>
                                                          <td>
                                                              <asp:TextBox ID="txtOPETXAM" runat="server" CssClass="form-control" Enabled="false" ></asp:TextBox>
                                                          </td>
                                                          <td>
                                                              <asp:TextBox ID="txtPURTXAM" runat="server" CssClass="form-control" Enabled="false" ></asp:TextBox>
                                                          </td>
                                                          <td>
                                                              <asp:TextBox ID="txtSALTXAM" runat="server" CssClass="form-control" Enabled="true" AutoPostBack="True" OnTextChanged="txtSALTXAM_TextChanged" ></asp:TextBox>
                                                          </td>
                                                          <td>
                                                              <asp:TextBox ID="txtCLOTXAM" runat="server" CssClass="form-control" Enabled="false" ></asp:TextBox>
                                                          </td>
                                                        </tr>														
                                                      </tbody>
                                            </table>
                                 </div>
                                     
                    </asp:Panel>
</asp:Content>
