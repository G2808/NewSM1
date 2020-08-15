<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.master" AutoEventWireup="true" Inherits="NewSM1.PRODUCTTEAMLINK" Codebehind="PRODUCTTEAMLINK.aspx.cs" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:UpdatePanel ID="updatePanel1" runat="server" UpdateMode="Conditional">
      <ContentTemplate>

      <div class="container-fluid" style="margin-top:10px;text-align:center">
                <div class="row" style="margin-top:0px;text-align:left">
                  <div class="col-lg-12">
                    <h3 class="card-header"><i class="fa fa-link"></i>  Assign Product to Team </h3>
                  </div>
                </div>
             <asp:Panel ID="Panel_Search" runat="server"  DefaultButton="cmdSearch" >
                        <div class="card border" style="margin:0;padding:0">
                             <div class="card-body" style="margin:0;padding:0;margin-top:10px">
                                 <div class="row">
                                     <div class="col-md-3 offset-4">
                                              <asp:TextBox ID="txtSearchProduct" runat="server" style="margin-left: 0px"  CssClass="form-control" placeholder="Product Code"></asp:TextBox>
                                     </div>
                                     <div class="col-md-3">
                                              <asp:TextBox ID="txtSearchTeam" runat="server" style="margin-left: 0px" CssClass="form-control" placeholder="Team code"></asp:TextBox>
                                     </div>

                                 </div>

                            </div>
                                 <div class="card-footer btn-block" style="align-content:center;margin-top:10px;margin-bottom:10px">
                                           <asp:LinkButton ID="cmdSearch" runat="server" Text="Search" CssClass="btn btn-primary" Width="90px" OnClick="cmdSearch_Click">
                                           </asp:LinkButton>
                                           <asp:LinkButton ID="cmdAdd" runat="server" Text="Add" CssClass="btn btn-primary" Width="90px" OnClick="cmdAdd_Click">
                                           </asp:LinkButton>
                                           <asp:LinkButton ID="cmdExit" runat="server" Text="Exit" CssClass="btn btn-danger" Width="90px" OnClick="cmdExit_Click">
                                           </asp:LinkButton>
                                     </div>
                             <div>
                    <telerik:RadGrid ID="radData" runat="server" class="table-responsive" RenderMode="Auto" Skin="Office2007" AllowPaging="False" OnNeedDataSource="radData_NeedDataSource" AutoGenerateColumns="False"   OnItemCommand="radData_ItemCommand"  Width="100%" style="overflow:auto" GroupPanelPosition="Top" EnableAriaSupport="True" PageSize="9" Visible="False">                      
                        <ExportSettings IgnorePaging="true"></ExportSettings>
                                            <MasterTableView CommandItemDisplay="Top">
                                                <CommandItemSettings ShowAddNewRecordButton="False" ShowExportToCsvButton="True" ShowExportToExcelButton="True" ShowRefreshButton="false" />
                                               <Columns>
                                                    <telerik:GridBoundColumn DataField="ID" FilterControlAltText="Filter id column" HeaderText="ID" UniqueName="ID">
                                                        <HeaderStyle Width="50px" />
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="TEAMCODE" FilterControlAltText="Filter ID column" HeaderText="Product Name" ReadOnly="True" UniqueName="TEAMCODE">
                                                        <HeaderStyle Width="200px" />
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="PRODUCTCODE" FilterControlAltText="Filter ID column" HeaderText="PRODUCT" ReadOnly="True" UniqueName="PRODUCTCODE" >
                                                        <HeaderStyle Width="200px" />
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="DATEFROM" FilterControlAltText="Filter ID column" HeaderText="FROM" ReadOnly="True" UniqueName="DATEFROM" DataFormatString="{0:yyyy-MM-dd}">
                                                        <HeaderStyle Width="200px" />
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="DATETO" FilterControlAltText="Filter ID column" HeaderText="TO" ReadOnly="True" UniqueName="DATETO" DataFormatString="{0:yyyy-MM-dd}">
                                                        <HeaderStyle Width="200px" />
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridButtonColumn ButtonType="ImageButton" ItemStyle-HorizontalAlign="Center" ImageUrl="~/Images/Edit_New.png" HeaderText="" CommandName="CMDEDIT" FilterControlAltText="Filter column column" UniqueName="column">
                                                     </telerik:GridButtonColumn>
                                                 </Columns>
                                            </MasterTableView>
                                        </telerik:RadGrid>

              </div>
             </asp:Panel>
       </div>
      <div class="container-fluid" style="width:100%;text-align:center">
                    <asp:Panel ID="Panel_AddEdit" runat="server">
                         <div class="card border" style="margin-top:10px">
                                <div class="card-body" style="vertical-align:middle">
                                    <div class="row">
                                        <div class="col-md-2 " style="text-align:left" >
                                            <label for="">Team</label>
                                        </div>
                                        <div class="col-md-5">
                                              <telerik:RadComboBox ID="DDTeam" RenderMode="Lightweight" runat="server"
                                                EmptyMessage="Select Type" HighlightTemplatedItems="true"
                                                EnableLoadOnDemand="true" Filter="StartsWith" Style="width:100% !important;" >
                                                </telerik:RadComboBox>
                                        </div>
                                    </div><%--row team Code--%>
                                    <div class="row" style="margin-top:10px">
                                        <div class="col-md-2 " style="text-align:left" >
                                            <label for="">Product</label>
                                        </div>
                                        <div class="col-md-5" style="text-align:left">
                                              <telerik:RadComboBox ID="ddProduct" RenderMode="Lightweight" runat="server"
                                                EmptyMessage="Select Type" HighlightTemplatedItems="true"
                                                EnableLoadOnDemand="true" Filter="StartsWith" Style="width:100% !important;" >
                                                </telerik:RadComboBox>

                                        </div>
                                    </div><%--row team Name--%>
                                    <div class="row" style="margin-top:10px">
                                        <div class="col-md-2 " style="text-align:left" >
                                            <label for="">From Date</label>
                                        </div>
                                        <div class="col-md-5" style="text-align:left">
                                               <telerik:RadDatePicker ID="radFrom" runat="server" DateInput-DateFormat="yyyy-MM-dd" Skin="Bootstrap">
                                                <Calendar EnableWeekends="True" FastNavigationNextText="&amp;lt;&amp;lt;" Skin="Bootstrap" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False">
                                                </Calendar>
                                                <DateInput DateFormat="yyyy-MM-dd" DisplayDateFormat="yyyy-MM-dd" LabelWidth="40%">
                                                    <EmptyMessageStyle Resize="None" />
                                                    <ReadOnlyStyle Resize="None" />
                                                    <FocusedStyle Resize="None" />
                                                    <DisabledStyle Resize="None" />
                                                    <InvalidStyle Resize="None" />
                                                    <HoveredStyle Resize="None" />
                                                    <EnabledStyle Resize="None" />
                                                </DateInput>
                                    <DatePopupButton HoverImageUrl="" ImageUrl="" />
                                </telerik:RadDatePicker >   
                                        </div>
                                    </div><%--row team Name--%>
                                    <div class="row" style="margin-top:10px">
                                        <div class="col-md-2" style="text-align:left" >
                                            <label for="">To Date</label>
                                        </div>
                                        <div class="col-md-4" style="text-align:left">
                                               <telerik:RadDatePicker ID="radTo" runat="server" DateInput-DateFormat="yyyy-MM-dd" Skin="Bootstrap">
                                    <Calendar EnableWeekends="True" FastNavigationNextText="&amp;lt;&amp;lt;" Skin="Bootstrap" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False">
                                    </Calendar>
                                    <DateInput DateFormat="yyyy-MM-dd" DisplayDateFormat="yyyy-MM-dd" LabelWidth="40%">
                                        <EmptyMessageStyle Resize="None" />
                                        <ReadOnlyStyle Resize="None" />
                                        <FocusedStyle Resize="None" />
                                        <DisabledStyle Resize="None" />
                                        <InvalidStyle Resize="None" />
                                        <HoveredStyle Resize="None" />
                                        <EnabledStyle Resize="None" />
                                    </DateInput>
                                    <DatePopupButton HoverImageUrl="" ImageUrl="" />
                                </telerik:RadDatePicker >   

                                        </div>
                                    </div><%--row team Name--%>


                                    <div class="max-auto" style="align-content:center;margin-top:10px">
                                        <asp:Label ID="lblErrorAdd" runat="server" Text="" ForeColor="Red"></asp:Label>
                                    </div><%--row Error--%>
                                 </div>
                                    <div class="card-footer" style="margin-top:20px;align-content:center">
                                           <asp:LinkButton ID="cmdUpdate" runat="server" Text="Update" CssClass="btn btn-primary    " Width="90px" OnClick="cmdUpdate_Click" >                                     
                                            </asp:LinkButton>
                                           <asp:LinkButton ID="cmdExitAdd" runat="server" Text="Exit" CssClass="btn btn-danger" Width="90px" OnClick="cmdExitAdd_Click" >
                                            </asp:LinkButton>
                                    </div><%--row Buttons--%>

                          </div>
                    </asp:Panel>
       </div>
     <asp:Literal ID="ActFlag" runat="server" Visible="false"></asp:Literal>
  </ContentTemplate>
      <Triggers>
            <asp:PostBackTrigger ControlID="radData" /> 
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
