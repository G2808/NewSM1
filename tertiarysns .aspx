<%@ Page Language="c#" AutoEventWireup="true" MasterPageFile="~/Admin.master"  CodeBehind="tertiarysns .aspx.cs" Inherits="NewSM.Tertiarysns" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>

<Asp:Content ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
.modal-lg {
    max-width: 600px !important;
    text-align:left;
}
    </style>
<%--    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>--%>
<script type="text/javascript" src='https://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.8.3.min.js'></script>
<script type="text/javascript" src='https://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/3.0.3/js/bootstrap.min.js'></script>
<link rel="stylesheet" href='https://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/3.0.3/css/bootstrap.min.css'
    media="screen" />

<script type="text/javascript">
    function ShowPopup() {
        //$("#MyPopup .modal-title").html(title);
        //$("#MyPopup .modal-body").html(body);
        $("#MyPopup").modal("show");
    }
</script>
    <asp:Button ID="btnShowPopup" runat="server" Text="Show Popup" OnClick="ShowPopup"
        CssClass="btn btn-info btn-lg" />
    <asp:Panel runat="server" ID="Panel_Search">
            <div class="container-fluid" style="text-align:center;margin-top:0px">
        <div class="row justify-content-center" style="margin-top:0px">
        <h4>Stock and Sales</h4>
        </div>
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
        <div class="pre-scrollable" style="margin-top:10px;max-height:450px">
                    <telerik:RadGrid ID="radData" runat="server" class="table-responsive" RenderMode="Auto" Skin="Office2010Blue" Width="100%" Style="overflow: auto" EnableAriaSupport="True" PageSize="9" OnDataBound="radData_DataBound"  >
                        <GroupingSettings CollapseAllTooltip="Collapse all groups" />
                        <ExportSettings IgnorePaging="true"></ExportSettings>
                        <MasterTableView CommandItemDisplay="Top">
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

    </div>
    </asp:Panel>
    <asp:Panel runat="server" ID="Panel_AddEdit">

                <div class="container-fluid" style =" margin-top:10px">
                <asp:TextBox ID="txtType" runat="server" Visible="False" Width="72px"></asp:TextBox>
                <asp:TextBox ID="txtStockist" runat="server" Visible="False" Width="72px"></asp:TextBox>
                <asp:TextBox ID="txtUser" runat="server" Visible="False" Width="72px"></asp:TextBox>
                <asp:TextBox ID="txtHQ" runat="server" Visible="False" Width="72px"></asp:TextBox>
                <asp:TextBox ID="txtUserID" runat="server" Visible="False" Width="72px"></asp:TextBox>
                <asp:Literal ID="ActFlag" runat="server" Visible="False"></asp:Literal>
                </div>
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
                    <asp:Label ID="lblErrorItem" runat="server" Text="" ForeColor="Red"></asp:Label>
                                         <div class="row" style="margin-top:10px">
                                             <div class="col-md-5 col-md-offset-3">
                                                      <asp:TextBox ID="txtSearchCode" runat="server" style="margin-left: 0px"  CssClass="form-control" placeholder="Item"></asp:TextBox>
                                             </div>
                                                 <asp:LinkButton ID="cmdSearchItem" runat="server" Text="Search" CssClass="btn btn-danger" Width="90px" OnClick="cmdSearchItem_Click" >
                                                        <span aria-hidden="true" class="glyphicon glyphicon-refresh"></span>                                              
                                                        Search

                                                   </asp:LinkButton>
                                                   <asp:LinkButton ID="cmdExitItem" runat="server" Text="Exit" CssClass="btn btn-primary" Width="90px" >
                                                      <span aria-hidden="true" class="glyphicon  glyphicon-eject"></span>                                           
                                                       Exit
                                                   </asp:LinkButton>
                                             </div>
                </div>
                <div class ="row" style="margin-top:10px">
                    <div class="col-md-8 col-md-offset-2">
                        <div class="pre-scrollable" style="max-height:450px">
                                                <telerik:RadGrid ID="RadGrid1" runat="server" class="table-responsive" RenderMode="Auto" Skin="Office2007" AllowPaging="False" AutoGenerateColumns="False"   OnItemCommand="radGrid_ItemCommand"  Width="100%" style="overflow:auto" GroupPanelPosition="Top" EnableAriaSupport="True" PageSize="9" Visible="True" Font-Size="X-Small">                      
                                                    <ExportSettings IgnorePaging="true"></ExportSettings>
                                                    <MasterTableView CommandItemDisplay="Top">
                                                        <CommandItemSettings ShowAddNewRecordButton="False" ShowExportToCsvButton="True" ShowExportToExcelButton="True" ShowRefreshButton="false" />
                        <Columns>
                            <telerik:GridBoundColumn DataField="ID" HeaderStyle-Width="80px" HeaderText="id" SortExpression="id"
                                UniqueName="ID" Display="false">
                            </telerik:GridBoundColumn>
                            <telerik:GridNumericColumn DataField="ITEM" HeaderStyle-Width="180px" HeaderText="Product" ReadOnly="true">
                            </telerik:GridNumericColumn>
                            <telerik:GridNumericColumn DataField="OPENING" HeaderStyle-Width="80px" HeaderText="Opening" ReadOnly="true">
                            </telerik:GridNumericColumn>
                            <telerik:GridNumericColumn DataField="PURCHASE" HeaderStyle-Width="80px" HeaderText="Purchase" ReadOnly="true">
                            </telerik:GridNumericColumn>
                            <telerik:GridNumericColumn DataField="SALES" HeaderStyle-Width="80px" HeaderText="Sales" ReadOnly="false">
                            </telerik:GridNumericColumn>
                            <telerik:GridNumericColumn DataField="CLOSING" HeaderStyle-Width="80px" HeaderText="Closing" ReadOnly="true">
                            </telerik:GridNumericColumn>
                            <telerik:GridButtonColumn ButtonType="ImageButton" ItemStyle-HorizontalAlign="Center" ImageUrl="~/Images/Edit_New.png" HeaderText="" CommandName="ADDDATA" FilterControlAltText="Filter column column" UniqueName="column">
                             </telerik:GridButtonColumn>
                        </Columns>
                                                    </MasterTableView>
                                                </telerik:RadGrid>
                            </div>
                        </div>
                </div>
    <div id="MyPopup" class="modal fade" role="dialog" data-backdrop="static">
             <div class="modal-dialog">
        <!-- Modal content-->
        <div class="modal-content">
            <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal">
                    &times;</button>
                <h4 class="modal-title">
                </h4>
            </div>
            <div class="modal-body">
                                     <div class="form-group">
                                        <!-- Full Name -->
                                        <label for="full_name_id" class="control-label">ID</label>
                                        <asp:TextBox ID="txtCode" runat="server" CssClass="form-control" ReadOnly="false" AutoPostBack="False"></asp:TextBox>
                                    </div>         
                                     <div class="form-group">
                                        <!-- Full Name -->
                                        <label for="full_name_id" class="control-label">ID</label>
                                        <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control" ReadOnly="false" AutoPostBack="False"></asp:TextBox>
                                    </div>      
                         <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-danger" data-dismiss="modal">
                    Close</button>
            </div>
        </div>
    </div>
</div>

      </asp:Panel>

<%--</ContentTemplate>
</asp:UpdatePanel>--%>

</Asp:Content>
