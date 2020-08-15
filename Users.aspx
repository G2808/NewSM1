<%@ Page Title="" Language="C#" MasterPageFile="Admin.master" AutoEventWireup="true" Inherits="NewSM1.Users" Codebehind="Users.aspx.cs" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script>
        function calendarShown(sender, args) {
            sender._popupBehavior._element.style.zIndex = 10005;
        }
    </script>
    <link href="Css/default.css" rel="stylesheet" />
    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <div class="container-fluid" style="margin-top:0px;margin-left:20px;width:100%" >
        <div class="row" style="margin-top:0px">
          <div class="col-lg-12">
            <h3 class="card-header"><i class="fa fa-user"></i> Users</h3>
          </div>
        </div>

        <asp:UpdatePanel ID="updatePanel1" runat="server" UpdateMode="Conditional">

                <ContentTemplate>
                    <asp:Panel ID="Panel_search" runat="server" DefaultButton="cmdSearch">
                        <div  style="width:100%">

                           <div class="row" >
                                    <div class="col-md-3 col-md-offset-3 col-sm-6 col-sm-offset-3" style="padding-top:5px">
                                        <asp:TextBox ID="txtSearchCode" runat="server" CssClass="form-control" placeholder="Code"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3" style="padding-top:5px">
                                        <asp:TextBox ID="txtSearchName" runat="server"  CssClass="form-control" placeholder="Employee Name" Width="350px"></asp:TextBox>
                                    </div>
                                </div>
                            <div class="card-footer" style="text-align:center;margin-top:5px">
                                       <div class="row btn-block">
                                           <asp:LinkButton ID="cmdSearch" runat="server" Text="Search" CssClass="btn btn-primary" Width="90px" OnClick="cmdSearch_Click">
                                           </asp:LinkButton>                    
                                           <asp:LinkButton ID="cmdAdd" runat="server" Text="Add" CssClass="btn btn-primary" Width="90px" OnClick="cmdAdd_Click">
                                           </asp:LinkButton>              
                                           <asp:LinkButton ID="cmdExit" runat="server" Text="Exit" CssClass="btn btn-danger" Width="90px" OnClick="cmdExit_Click">
                                           </asp:LinkButton>
                                    </div>
                            </div>
                            </div>       
                        <div class="row btn-block" style="text-align:center">
                            <asp:UpdateProgress ID="UpdateProgress1" runat="Server" AssociatedUpdatePanelID="UpdatePanel1" DisplayAfter="2">
                                <ProgressTemplate >
                                    <asp:Image ID="Image1" runat="server" ImageUrl="~/images/spinner.gif" Width="150px" Height="80px" />
                                    <b>Loading....</b>
                                </ProgressTemplate>
                            </asp:UpdateProgress>
                        </div>
                        <div class="panel-body" >
                            <telerik:RadGrid ID="radData" runat="server"  RenderMode="Auto" Skin="Bootstrap" AllowPaging="false" OnNeedDataSource="radData_NeedDataSource" AutoGenerateColumns="False" OnItemCommand="radData_ItemCommand" Style="margin-left:auto !important;margin-right:auto !important:" GroupPanelPosition="Top" EnableAriaSupport="True"  Visible="True" Width="100%">
                                <ExportSettings IgnorePaging="true"></ExportSettings>
                                <MasterTableView CommandItemDisplay="Top">
                                    <CommandItemSettings ShowAddNewRecordButton="False" ShowExportToCsvButton="True" ShowExportToExcelButton="True" ShowRefreshButton="false" />
                                    <Columns>
                                        <telerik:GridBoundColumn DataField="Code" FilterControlAltText="Filter ID column" HeaderText="CODE" ReadOnly="True" UniqueName="CODE">
                                            <ItemStyle HorizontalAlign="Left" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="firstname" FilterControlAltText="Filter ID column" HeaderText="First Name" ReadOnly="True" UniqueName="Firstname">
                                            <ItemStyle HorizontalAlign="Left" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="lastname" FilterControlAltText="Filter ID column" HeaderText="Last Name" ReadOnly="True" UniqueName="lastname">
                                            <ItemStyle HorizontalAlign="Left" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="email" FilterControlAltText="Filter ID column" HeaderText="eMail" ReadOnly="True" UniqueName="email">
                                            <ItemStyle HorizontalAlign="Left" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="visa" FilterControlAltText="Filter ID column" HeaderText="Visa" ReadOnly="True" UniqueName="visa">
                                            <ItemStyle HorizontalAlign="Left" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="profile" FilterControlAltText="Filter ID column" HeaderText="Profile" ReadOnly="True" UniqueName="profile">
                                            <ItemStyle HorizontalAlign="Left" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridButtonColumn ButtonType="ImageButton" ItemStyle-HorizontalAlign="Center" ImageUrl="~/Images/Edit_New.png" HeaderText="" CommandName="CMDEDIT" FilterControlAltText="Filter column column" UniqueName="column">
                                        </telerik:GridButtonColumn>
                                    </Columns>
                                </MasterTableView>
                            </telerik:RadGrid>
                        </div>             
                        <asp:Literal ID="ActFlag" runat="server" Visible="false"></asp:Literal>
                     </asp:Panel>
                        <asp:Panel ID="Panel_entry" runat="server">
                        <div class="panel center-block" style="width:100%; border:0">
                            <asp:UpdatePanel ID="upModal" runat="server" ChildrenAsTriggers="False" UpdateMode="Conditional">
                                <ContentTemplate>
                                        <div class="panel-heading">
                                            <h4 class=" panel-title">
                                                <asp:Label ID="lblModalTitle" runat="server" Text=""></asp:Label></h4>
                                        </div>
                                        <div class=" panel-body" >

                                            <div class="row">
                                                <div class="col-md-2" style="width:150px;text-align:left">
                                                <label for="full_name_id" class="control-label" style="width:40%;margin-top:5px">Code</label>
                                                </div>
                                                <div class="col-md-4" >
                                                    <asp:TextBox ID="txtCode" runat="server" CssClass="form-control" Width="40%" ></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="row" style="margin-top:10px">
                                                <div class="col-md-2" style="width:150px">
                                                <label for="full_name_id" class="control-label" style="width:100%;margin-top:5px">First Name</label>
                                                </div>
                                                <div class="col-md-5">
                                                    <asp:TextBox ID="txtFirstName" runat="server" CssClass="form-control" Width="100%" ></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="row" style="margin-top:10px">
                                                <div class=" col-md-2" style="width:150px">
                                                        <label for="full_name_id" class="control-label" style="width:100%;margin-top:5px">Last Name</label>
                                                </div>
                                                <div class="col-md-5">
                                                    <asp:TextBox ID="txtLastName" runat="server" CssClass="form-control" Width="100%" ></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="row" style="margin-top:10px">
                                                <div class="col-md-2" style="width:150px">
                                                <label for="full_name_id" class="control-label" style="width:100%;margin-top:5px">eMail</label>
                                                </div>
                                                <div class="col-md-8">
                                                    <asp:TextBox ID="txtemail" runat="server" CssClass="form-control" ReadOnly="false"  Width="100%"></asp:TextBox>
                                                </div>
                                                </div>
                                            <div class="row" style="margin-top:10px">
                                                <div class="col-md-2" style="width:150px">
                                                <label for="full_name_id" class="control-label" style="width:50%;margin-top:5px"> Visa </label>
                                                </div>
                                                <div class="col-md-4">
                                                    <asp:TextBox ID="txtVISA" runat="server" CssClass="form-control" ReadOnly="false"  Width="50%"></asp:TextBox>
                                                </div>
                                                </div>
                                            <div class="row" style="margin-top:10px">
                                                <div class="col-md-2" style="width:150px">
                                                <label for="full_name_id" class="control-label" style="width:170px;margin-top:5px">Profile</label>
                                                </div>
                                                <div class="col-md-4">
                                                    <telerik:RadDropDownList ID="radProfile" runat="server" SelectedText="User" SelectedValue="user" Skin="Bootstrap">
                                                        <Items>
                                                            <telerik:DropDownListItem runat="server" Selected="True" Text="USER" Value="USER"></telerik:DropDownListItem>
                                                            <telerik:DropDownListItem runat="server" Text="RESOLVER" Value="RESOLVER"></telerik:DropDownListItem>
                                                            <telerik:DropDownListItem runat="server" Text="ADMIN" Value="ADMIN"></telerik:DropDownListItem>
                                                        </Items>
                                                    </telerik:RadDropDownList>
                                                </div>
                                             </div>
                                            <div class="row" style="margin-top:10px">
                                                <div class="col-md-2" style="width:150px">
                                                <label for="full_name_id" class="control-label" style="width:170px;margin-top:5px">Contact</label>
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:TextBox ID="txtContact" runat="server" CssClass="form-control" ReadOnly="false"  Width="100%"></asp:TextBox>
                                                </div>
                                             </div>
                                            <div class="row" style="margin-top:10px">
                                                <div class="col-md-2" style="width:150px">
                                                <label for="full_name_id" class="control-label" style="width:170px;margin-top:5px">Password</label>
                                                </div>
                                                <div class="col-md-4">
                                                    <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" ReadOnly="false"  Width="200px" TextMode="Password"></asp:TextBox>
                                                </div>
                                             </div>
                                            <div class="row" >
                                                <div class="col-md-12" style="text-align:center">
                                                  <asp:Label ID="lblError" runat="server" Text="" ForeColor="Red"></asp:Label>
                                                    </div>
                                            </div>
                                        </div>
                                        <div class=" panel-footer">
                                            <div class="row center-block">
                                                <div class="col-md-12 text-center">
                                                    <asp:Button ID="btnExit" CssClass="btn btn-danger" runat="server" Text="Exit" OnClick="btnExit_Click" Width="80px"  />
                                                    <asp:Button ID="Submit" CssClass="btn btn-primary" runat="server" Text="Submit" OnClick="Submit_Click" Width="80px" />
                                                    </div>
                                          </div>
                                        </div>

                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="Submit" EventName="Click" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
                </asp:Panel>

                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="radData" EventName="ItemCommand" />
                    <asp:AsyncPostBackTrigger ControlID="Submit" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="btnExit" EventName="Click" />
                </Triggers>

    </asp:UpdatePanel>
    </div>

<!-- Modal -->
</asp:Content>
