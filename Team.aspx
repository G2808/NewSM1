<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.master" AutoEventWireup="true" Codebehind="Team.aspx.cs" Inherits="NewSM1.Team"  %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<style>


/*.modal {
    text-align: center;
}


.modal::before {
    content: "";      
    display: inline-block;
    height: 100%;    
    margin-right: -4px;
    vertical-align: middle;
}

.modal-dialog { 
    display: inline-block;  
    text-align: left;   
    vertical-align: middle;

}*/

.modal-lg {
    max-width: 400px !important;
    text-align:left;
}

</style>

    <asp:UpdatePanel ID="updatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="container-fluid" style="width: 100%; margin-top: 10px; text-align:center">
                <div class="row" style="margin-top:0px;text-align:left">
                  <div class="col-lg-12">
                    <h3 class="card-header"><i class="fa fa-users"></i> Team</h3>
                  </div>
                </div>
                <div class="row" style="margin-top:10px;text-align:center">
                    <div class="col-md-2  offset-4">
                        <asp:TextBox ID="txtSearchShort" runat="server" CssClass="form-control" placeholder="Team Short Name"></asp:TextBox>
                    </div>
                    <div class="col-md-3">
                        <asp:TextBox ID="txtSearchName" runat="server"  CssClass="form-control" placeholder="Team Name"></asp:TextBox>
                    </div>

                </div>
                <div class="row btn-block" style="text-align:center;margin-top:10px">
                            <asp:LinkButton ID="cmdSearch" runat="server" Text="Search" CssClass="btn  btn-primary" Width="90px" OnClick="cmdSearch_Click">
                            </asp:LinkButton>
                            <asp:LinkButton ID="cmdAdd" runat="server" Text="Add" CssClass="btn btn-primary" Width="90px" OnClick="cmdAdd_Click">
                            </asp:LinkButton>
                            <asp:LinkButton ID="cmdExit" runat="server" Text="Exit" CssClass="btn btn-danger" Width="90px" OnClick="cmdExit_Click">
                            </asp:LinkButton>
                </div>
                <div>
                    <telerik:RadGrid ID="radData" runat="server" class="table-responsive" RenderMode="Auto" Skin="Bootstrap" AllowPaging="false" OnNeedDataSource="radData_NeedDataSource" AutoGenerateColumns="False" OnItemCommand="radData_ItemCommand" Width="100%" Style="overflow: auto" GroupPanelPosition="Top" EnableAriaSupport="True" PageSize="9" Visible="False">
                        <ExportSettings IgnorePaging="true"></ExportSettings>
                        <MasterTableView CommandItemDisplay="Top">
                            <CommandItemSettings ShowAddNewRecordButton="False" ShowExportToCsvButton="True" ShowExportToExcelButton="True" ShowRefreshButton="false" />
                            <Columns>
                                <telerik:GridBoundColumn DataField="TEAMID" FilterControlAltText="Filter id column" HeaderText="ID" UniqueName="TEAMID">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="TEAMSHORTNAME" FilterControlAltText="Filter ID column" HeaderText="Short Name" ReadOnly="True" UniqueName="TEAMSHORTNAME">
                                    <HeaderStyle Width="20px" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="teamname" FilterControlAltText="Filter name column" HeaderText="NAME" UniqueName="teamname">
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                <telerik:GridButtonColumn ButtonType="ImageButton" ItemStyle-HorizontalAlign="Center" ImageUrl="~/Images/Edit_New.png" HeaderText="" CommandName="CMDEDIT" FilterControlAltText="Filter column column" UniqueName="column">
                                </telerik:GridButtonColumn>
                            </Columns>
                        </MasterTableView>
                    </telerik:RadGrid>
                </div>
            </div>

            <asp:Literal ID="ActFlag" runat="server" Visible="false"></asp:Literal>
             <div class="modal fade" id="myModal" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" data-backdrop="static" data-keyboard="false" >
                <div class="modal-dialog modal-lg ">
                    <asp:UpdatePanel ID="upModal" runat="server" ChildrenAsTriggers="False" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div class="modal-content modal-lg">
                                <div class="modal-header">
                                    <h4 class="modal-title">
                                        <asp:Label ID="lblModalTitle" runat="server" Text=""></asp:Label></h4>
                                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>

                                </div>
                                <div class="modal-body">
                                    <div class="form-group">
                                        <!-- Full Name -->
                                        <label for="full_name_id" class="control-label">ID</label>
                                        <asp:TextBox ID="txtID" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <!-- Full Name -->
                                        <label for="full_name_id" class="control-label">Team Short Name</label>
                                        <asp:TextBox ID="teamShortName" runat="server" CssClass="form-control" ReadOnly="false"></asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <!-- Full Name -->
                                        <label for="full_name_id" class="control-label">Team Name</label>
                                        <asp:TextBox ID="teamName" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>

                                    <asp:Label ID="lblError" runat="server" Text="" ForeColor="Red"></asp:Label>
                                </div>
                                <div class="modal-footer">
                                    <button class="btn btn-info" data-dismiss="modal" aria-hidden="true">Close</button>
                                    <asp:Button ID="Submit" CssClass="btn btn-info" runat="server" Text="Submit" OnClick="Submit_Click" />
                                </div>
                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="Submit" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>

        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="radData" EventName="ItemCommand" />
     
        </Triggers>

    </asp:UpdatePanel>

</asp:Content>
