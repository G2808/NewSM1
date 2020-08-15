<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.master" AutoEventWireup="true" CodeBehind="TerTiaryStockAndSales.aspx.cs" Inherits="NewSM.TerTiaryStockAndSales" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<%--<script type="text/javascript" src='https://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.8.3.min.js'></script>
<script type="text/javascript" src='https://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/3.0.3/js/bootstrap.min.js'></script>
<link rel="stylesheet" href='https://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/3.0.3/css/bootstrap.min.css'
    media="screen" />--%>
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
                <div class="row"  >
                    <div class="col-xs-1" style="padding:0;margin:0">
                        <asp:Label ID="Label12" runat="server" Text="Year" ></asp:Label>  
            
                    </div>
                    <div class="col-xs-1" style="padding:0;margin:0">
                        <asp:Label ID="Label13" runat="server" Text="Month" ></asp:Label>  
              
                    </div>
                    <div class="col-xs-2" style="padding:0;margin:0">
                        <asp:Label ID="Label14" runat="server" Text="Headquarter" ></asp:Label>  
        
                    </div>
                    <div class="col-xs-2" style="padding:0;margin:0">
                        <asp:Label ID="Label15" runat="server" Text="City" ></asp:Label>  
         
                    </div>
                    <div class="col-xs-2" style="padding:0;margin:0">
                        <asp:Label ID="Label16" runat="server" Text="Type" ></asp:Label>  
       
                    </div>
                    <div class="col-xs-3" style="padding:0;margin:0">
                        <asp:Label ID="Label17" runat="server" Text="Party" ></asp:Label>  
  
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-1 " style="padding:2px;margin:0">
                         <asp:DropDownList ID="cmbYear" runat="server"   AutoPostBack="True" CssClass="form-control">
                          </asp:DropDownList>                  
                    </div>
                    <div class="col-xs-1" style="padding:2px;margin:0">
                        <asp:DropDownList ID="cmbMonth" runat="server"  CssClass="form-control">
                         </asp:DropDownList>                
                    </div>
                    <div class="col-xs-2" style="padding:2px;margin:0">
                         <asp:DropDownList ID="cmbHQ" runat="server" Width="100%" AutoPostBack="True" CssClass="form-control" OnSelectedIndexChanged="cmbHQ_SelectedIndexChanged1">
                         </asp:DropDownList>             
                    </div>
                    <div class="col-xs-2" style="padding:2px;margin:0">
                         <asp:DropDownList ID="cmbCity" runat="server"  Width="100%" AutoPostBack="True" CssClass="form-control" OnSelectedIndexChanged="cmbCity_SelectedIndexChanged1">
                          </asp:DropDownList>           
                    </div>
                    <div class="col-xs-2" style="padding:2px;margin:0">
                         <asp:DropDownList ID="cmbType" runat="server"   Width="100%" AutoPostBack="True" CssClass="form-control">
                          </asp:DropDownList>        
                    </div>
                    <div class="col-xs-3" style="padding:2px;margin:0">
                          <asp:DropDownList ID="cmbParty" runat="server" Width="400px" AutoPostBack="True" CssClass="form-control">
                          </asp:DropDownList>    
                    </div>
                </div>
                <div>
                <div class="row justify-content-center">
                     <asp:Label ID="lblError" runat="server" Text="" ForeColor="Red"></asp:Label>
                </div>
    </div>
        </ContentTemplate>
        </asp:UpdatePanel>
        <div class="row" style="align-content: center; margin-top: 10px; margin-bottom: 10px">
                    <div class="col-md-2 col-md-offset-3">
                            <asp:LinkButton ID="btnSearch" runat="server" Text="Search" CssClass="btn  btn-primary" Width="100%" OnClick="btnSearch_Click">
                            </asp:LinkButton>
                    </div>
                    <div class="col-md-2">
                            <asp:LinkButton ID="btnNew" runat="server" Text="Add" CssClass="btn btn-primary" Width="100%" OnClick="btnNew_Click">
                            </asp:LinkButton>
                        </div>
                     <div class="col-md-2">
                            <asp:LinkButton ID="cmdExit" runat="server" Text="Exit" CssClass="btn btn-primary" Width="100%" OnClick="cmdExit_Click">
                            </asp:LinkButton>
                         </div>
                </div>

                    <telerik:RadGrid ID="radData" runat="server" class="table-responsive" RenderMode="Auto" Skin="Office2010Blue" Width="100%" Style="overflow: auto" EnableAriaSupport="True" PageSize="9" OnDataBound="radData_DataBound" Height="100%" OnItemCommand="radData_ItemCommand" OnNeedDataSource="radData_NeedDataSource"  >
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
        <asp:Panel runat="server" ID="Panel_AddEdit" DefaultButton="cmdSearchItem">
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
                                <div class="row" style="margin-top:10px">
                                                         <div class="row" style="margin-top:10px">
                                                             <div class="col-md-5 col-md-offset-3">
                                                                      <asp:TextBox ID="txtSearchCode" runat="server" style="margin-left: 0px"  CssClass="form-control" placeholder="Item"></asp:TextBox>
                                                             </div>
                                                                 <asp:LinkButton ID="cmdSearchItem" runat="server" Text="Search" CssClass="btn btn-danger" Width="90px" OnClick="cmdSearchItem_Click" >
                                                                        <span aria-hidden="true" class="glyphicon glyphicon-refresh"></span>                                              
                                                                        Search

                                                                   </asp:LinkButton>
                                                                   <asp:LinkButton ID="cmdExitItem" runat="server" Text="Exit" CssClass="btn btn-primary" Width="90px" OnClick="cmdExitItem_Click" >
                                                                      <span aria-hidden="true" class="glyphicon  glyphicon-eject"></span>                                           
                                                                       Exit
                                                                   </asp:LinkButton>
                                                             </div>
                                </div>
                                <div class="row" style="text-align:center;margin-top:5px">
                                    <asp:Label ID="lblItemError" runat="server" Text="" Font-Bold="true" ForeColor="red"></asp:Label>
                                </div>
                                 <div class="pre-scrollable" style="margin-top:10px;max-height:450px">
                                        <div id="demo" class="demo-container no-bg">
                                            <telerik:RadGrid RenderMode="Lightweight" ID="RadGrid1" runat="server" AllowAutomaticDeletes="True" Skin="Office2010Blue"
                                                AllowAutomaticInserts="True" OnItemDeleted="RadGrid1_ItemDeleted" OnItemInserted="RadGrid1_ItemInserted"
                                                OnItemUpdated="RadGrid1_ItemUpdated" OnPreRender="RadGrid1_PreRender" AllowAutomaticUpdates="True" AllowPaging="False"
                                                 AutoGenerateColumns="False" DataSourceID="SqlDataSource2" OnItemCreated="RadGrid1_ItemCreated" >
                                                <GroupingSettings CollapseAllTooltip="Collapse all groups" />
                                                <MasterTableView AutoGenerateColumns="False" CommandItemDisplay="Top" DataKeyNames="ID" EditMode="Batch" HorizontalAlign="NotSet" DataSourceID="SqlDataSource2">
                                                    <BatchEditingSettings EditType="Cell" HighlightDeletedRows="true" />
                                                    <CommandItemSettings ShowAddNewRecordButton="false" />
                                                    <Columns>
                                                        <telerik:GridBoundColumn DataField="ID" Display="false" HeaderStyle-Width="210px" HeaderText="ID" SortExpression="ID" UniqueName="ID">
                                                            <HeaderStyle Width="210px" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="CUST_CODE" HeaderStyle-Width="210px" HeaderText="CUST_CODE" ReadOnly="true" UniqueName="CUST_CODE">
                                                            <HeaderStyle Width="210px" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="CITY" HeaderStyle-Width="210px" HeaderText="CITY" ReadOnly="true" UniqueName="CITY">
                                                            <HeaderStyle Width="210px" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="ITEM_CODE" HeaderStyle-Width="110px" HeaderText="ITEM_CODE" ReadOnly="true" UniqueName="ITEM_CODE">
                                                            <HeaderStyle Width="110px" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="OPENING" HeaderStyle-Width="110px" HeaderText="OPENING" ReadOnly="true" UniqueName="OPENING">
                                                            <HeaderStyle Width="110px" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="PURCHASE" HeaderStyle-Width="110px" HeaderText="PURCHASE" ReadOnly="true" UniqueName="PURCHASE">
                                                            <HeaderStyle Width="110px" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridTemplateColumn DataField="SALES" HeaderStyle-Width="80px" HeaderText="SALES" ItemStyle-BackColor="LightYellow" SortExpression="SALES" UniqueName="TemplateColumn">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSales" runat="server" Text='<%# Eval("Sales") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <span>
                                                                <telerik:RadNumericTextBox ID="tbSales" runat="server" RenderMode="Lightweight" Width="55px">
                                                                </telerik:RadNumericTextBox>
                                                                </span>
                                                            </EditItemTemplate>
                                                            <HeaderStyle Width="80px" />
                                                            <ItemStyle BackColor="LightYellow" />
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridBoundColumn DataField="CLOSING" HeaderStyle-Width="110px" HeaderText="CLOSING" ReadOnly="true" UniqueName="CLOSING">
                                                            <HeaderStyle Width="110px" />
                                                        </telerik:GridBoundColumn>
                                                    </Columns>
                                                </MasterTableView>
                                                <FilterMenu RenderMode="Lightweight">
                                                </FilterMenu>
                                                <HeaderContextMenu RenderMode="Lightweight">
                                                </HeaderContextMenu>
                                            </telerik:RadGrid>
                                            <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString1 %>" SelectCommand="BSDisplayTertiary" SelectCommandType="StoredProcedure" UpdateCommand="BSAddTertiary" UpdateCommandType="StoredProcedure">
                                                <SelectParameters>
                                                    <asp:Parameter Name="tyear" Type="Int32" />
                                                    <asp:Parameter Name="tmonth" Type="Int32" />
                                                    <asp:Parameter Name="cust_code" Type="String" />
                                                    <asp:Parameter Name="ttype" Type="Int32" />
                                                </SelectParameters>
                                                <UpdateParameters>
                                                    <asp:Parameter Name="id" Type="Decimal" />
                                                    <asp:Parameter Name="sales" Type="Decimal" />
                                                    <asp:Parameter Name="userid" Type="Int32" />
                                                </UpdateParameters>
                                            </asp:SqlDataSource>
                                        </div>
<%--                                       <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="6" OnRowCancelingEdit="GridView1_RowCancelingEdit"  Width="100%"  
                                           CssClass="table table-hover table-striped"
                                            OnRowEditing="GridView1_RowEditing" OnRowUpdating="GridView1_RowUpdating">  
                                            <Columns>  
                                            <asp:TemplateField HeaderText="ID" Visible="false" >  
                                                <ItemTemplate>  
                                                    <asp:Label ID="lbl_ID" runat="server" Text='<%#Eval("ID") %>'></asp:Label>  
                                                </ItemTemplate>  
                                            </asp:TemplateField>  
                                            <asp:TemplateField HeaderText="Product">  
                                                <ItemTemplate>  
                                                    <asp:Label ID="lbl_ITEM" runat="server" Text='<%#Eval("ITEM_CODE") %>'></asp:Label>  
                                                </ItemTemplate>  
                            <%--                    <EditItemTemplate>  
                                                    <asp:TextBox ID="txt_Name" runat="server" Text='<%#Eval("Name") %>'></asp:TextBox>  
                                                </EditItemTemplate> --%> 
                                            </asp:TemplateField>  
                                            <asp:TemplateField HeaderText="Customer">  
                                                <ItemTemplate>  
                                                    <asp:Label ID="lbl_cust" runat="server" Text='<%#Eval("CUST_CODE") %>'></asp:Label>  
                                                </ItemTemplate>  
                                            </asp:TemplateField> 
                                            <asp:TemplateField HeaderText="CITY">  
                                                <ItemTemplate>  
                                                    <asp:Label ID="lbl_city" runat="server" Text='<%#Eval("CITY") %>'></asp:Label>  
                                                </ItemTemplate>  
                                            </asp:TemplateField> 
                                            <asp:TemplateField HeaderText="Opening">  
                                                <ItemTemplate>  
                                                    <asp:Label ID="lbl_opening" runat="server" Text='<%#Eval("OPENING") %>'></asp:Label>  
                                                </ItemTemplate>  
                                            </asp:TemplateField>  
                                            <asp:TemplateField HeaderText="Purchase">  
                                                <ItemTemplate>  
                                                    <asp:Label ID="lbl_purchase" runat="server" Text='<%#Eval("PURCHASE") %>'></asp:Label>  
                                                </ItemTemplate>  
                                            </asp:TemplateField>  
                                            <asp:TemplateField HeaderText="Sales">  
                                                <ItemTemplate>  
                                                    <asp:Label ID="lbl_SALES" Visible='<%# !(bool) IsInEditMode %>'  runat="server" Text='<%#Eval("SALES") %>'></asp:Label>  
                                                </ItemTemplate>  
                                                <EditItemTemplate>  
                                                    <asp:TextBox ID="txt_sales"  Visible='<%# IsInEditMode %>'  runat="server" Text='<%#Eval("SALES") %>' CssClass="form-control"></asp:TextBox>  
                                                </EditItemTemplate>  
                                            </asp:TemplateField>  
                                            <asp:TemplateField HeaderText="CLOSING">  
                                                <ItemTemplate>  
                                                    <asp:Label ID="lbl_closing" runat="server" Text='<%#Eval("CLOSING") %>'></asp:Label>  
                                                </ItemTemplate>  
                                            </asp:TemplateField>  
                                            <asp:TemplateField>  
                                                <ItemTemplate>  
                                                    <asp:Button ID="btn_Edit" runat="server" Text="Edit" CommandName="Edit" CssClass="btn btn-primary" />  
                                                </ItemTemplate>  
                                                <EditItemTemplate>  
                                                    <asp:Button ID="btn_Update" runat="server" Text="Update" CommandName="Update" CssClass="btn  btn-success"/>  
                                                    <asp:Button ID="btn_Cancel" runat="server" Text="Cancel" CommandName="Cancel" CssClass="btn btn-danger"/>  
                                                </EditItemTemplate>  
                                            </asp:TemplateField>  
                                        </Columns>  
                                        <HeaderStyle BackColor="#663300" ForeColor="#ffffff"/>  
                                        <RowStyle BackColor="#e7ceb6"/>  
                               </asp:GridView>  --%>
                                 </div>
                                 <div class="container-fluid pre-scrollable" style="width:100%">
                                     <asp:PlaceHolder ID="phAddItem" runat="server" >
                                     </asp:PlaceHolder>
                                 </div>
                                     "
                    </asp:Panel>
</asp:Content>
