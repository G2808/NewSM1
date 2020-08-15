<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.master" AutoEventWireup="true" CodeBehind="HQ.aspx.cs" Inherits="NewSM1.HQ" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <style>
.borderless tr td {
    border: none !important;
    padding: 0px !important;
}
.table-responsive {
    width: 100%;
    margin-bottom: 15px;
    overflow-y: hidden;
    overflow-x: scroll;
    -ms-overflow-style: -ms-autohiding-scrollbar;
    border: 1px solid #DDD;
    -webkit-overflow-scrolling: touch;
}
.td {
            margin:0;
            padding:0
        }
.rcbInput.radPreventDecorate {
    height: 20px !important;
    
}
        </style>

    <asp:UpdatePanel ID="updatePanel1" runat="server" UpdateMode="Conditional">
      <ContentTemplate>
      <div class=" container-fluid" style="width:100%;margin-top:10px;text-align:center;">
                <div class="row" style="margin-top:0px;text-align:left">
                  <div class="col-lg-12">
                    <h3 class="card-header"><i class="fa fa-globe"></i>  Headquarter </h3>
                  </div>
                </div>
             <asp:Panel ID="Panel_Search" runat="server"  DefaultButton="cmdSearch" >
                                 <div class="row" style="margin-top:10px">
                                     <div class="col-md-3 offset-3">
                                              <asp:TextBox ID="txtSearchCode" runat="server" style="margin-left: 0px"  CssClass="form-control" placeholder="ID"></asp:TextBox>
                                     </div>
                                     <div class="col-md-3">
                                              <asp:TextBox ID="txtSearchStatus" runat="server" style="margin-left: 0px" CssClass="form-control" placeholder="HQ Name"></asp:TextBox>
                                     </div>
                                 </div>
                                <div class="row btn-block" style="margin-top:10px;text-align:center">
                                            <asp:LinkButton ID="cmdSearch" runat="server" Text="Search" CssClass="btn  btn-primary" Width="90px" OnClick="cmdSearch_Click">
                                            </asp:LinkButton>
                                            <asp:LinkButton ID="cmdAdd" runat="server" Text="Add" CssClass="btn btn-primary" Width="90px" OnClick="cmdAdd_Click">
                                            </asp:LinkButton>
                                            <asp:LinkButton ID="cmdExit" runat="server" Text="Exit" CssClass="btn btn-danger" Width="90px" OnClick="cmdExit_Click">
                                            </asp:LinkButton>
                                </div>                
                                         <div class="row btn-block" style="text-align:center">
                                            <asp:UpdateProgress ID="UpdateProgress1" runat="Server" AssociatedUpdatePanelID="UpdatePanel1" DisplayAfter="2">
                                                <ProgressTemplate >
                                                    <asp:Image ID="Image1" runat="server" ImageUrl="~/images/spinner.gif" Width="150px" Height="80px" />
                                                    <b>Loading....</b>
                                                </ProgressTemplate>
                                            </asp:UpdateProgress>
                                              </div>
                                                 <div class=" pre-scrollable" style="min-height:450px;margin-top:10PX">
                                        <telerik:RadGrid ID="radGrid" runat="server" class="table-responsive" RenderMode="Auto" Skin="Bootstrap" AllowPaging="False" OnNeedDataSource="radGrid_NeedDataSource" AutoGenerateColumns="False"   OnItemCommand="radGrid_ItemCommand"  Width="100%" style="overflow:auto" GroupPanelPosition="Top" EnableAriaSupport="True" PageSize="9" Visible="False">                      
                                            <ExportSettings IgnorePaging="true"></ExportSettings>
                                            <MasterTableView CommandItemDisplay="Top">
                                                <CommandItemSettings ShowAddNewRecordButton="False" ShowExportToCsvButton="True" ShowExportToExcelButton="True" ShowRefreshButton="false" />
                                               <Columns>
                                                                <telerik:GridBoundColumn DataField="headquarter_Id" FilterControlAltText="Filter ID column" HeaderText="ID" ReadOnly="True" UniqueName="headquarter_Id">
                                                                    <HeaderStyle Width="20px" />
                                                                </telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn DataField="headquarter_name" FilterControlAltText="Filter sname column" HeaderText="Name" UniqueName="headquarter_name">
                                                                <ItemStyle HorizontalAlign="Left" />
                                                                </telerik:GridBoundColumn>
                                                                <telerik:GridButtonColumn ButtonType="ImageButton" ItemStyle-HorizontalAlign="Center" ImageUrl="~/Images/Edit_New.png" HeaderText="Edit" CommandName="CMDEDIT" FilterControlAltText="Filter column column" UniqueName="column">
                                                                </telerik:GridButtonColumn>
                                                            </Columns>
                                            </MasterTableView>
                                        </telerik:RadGrid>
                           </div>	
             </asp:Panel>
       </div>
      <div class="container-fluid" style="width:100%;text-align:center">
             <asp:Panel ID="Panel_AddEdit" runat="server">
                                    <div class="row" style="margin-top:15px">
                                        <div class="col-md-1 col-md-offset-3 " style="text-align:left" >
                                            <label for="">ID</label>
                                        </div>
                                        <div class="col-md-3">
                                            <asp:TextBox ID="txtID" runat="server" CssClass="form-control rcbInput"></asp:TextBox>
                                        </div>
                                    </div><%--row Short Name--%>
                                    <div class="row" style="margin-top:10px">
                                        <div class="col-md-1 col-md-offset-3 " style="text-align:left" >
                                            <label for="">Name</label>
                                        </div>
                                        <div class="col-md-5">
                                            <asp:TextBox ID="txtName" runat="server" CssClass="form-control rcbInput"></asp:TextBox>
                                        </div>
                                    </div><%--row Short Name--%>
                                    <div class="row" style="margin-top:10px">
                                        <div class="col-md-1 col-md-offset-3 " style="text-align:left" >
                                            <label for="">HRIS HQ</label>
                                        </div>
                                        <div class="col-md-5" style="text-align:left">
                                            <telerik:RadComboBox ID="radHRISHQ" runat="server" Skin="Bootstrap"></telerik:RadComboBox>
                                        </div>
                                    </div><%--row Short Name--%>

                                    <div class="row" style="align-content:center;margin-top:20px">
                                        <asp:Label ID="lblErrorAdd" runat="server" Text="" ForeColor="Red"></asp:Label>
                                    </div><%--row Error--%>
                                    <div class="row btn-block" style="text-align:left" >
                                           <asp:LinkButton ID="cmdUpdate" runat="server" Text="Update" CssClass="btn btn-primary" Width="90px" OnClick="cmdUpdate_Click" >
                                            </asp:LinkButton>
                                           <asp:LinkButton ID="cmdExitAdd" runat="server" Text="Exit" CssClass="btn btn-danger" Width="90px" OnClick="cmdExitAdd_Click" >                                         
                                            </asp:LinkButton>
                                    </div><%--row Buttons--%>
                    </asp:Panel>
       </div>
     <asp:Literal ID="ActFlag" runat="server" Visible="false"></asp:Literal>
  </ContentTemplate>
      <Triggers>
            <asp:PostBackTrigger ControlID="radGrid" /> 
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
