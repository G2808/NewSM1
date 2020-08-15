<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.master" AutoEventWireup="true" CodeBehind="Period.aspx.cs" Inherits="NewSM1.Period" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
.modal-lg {
    max-width: 600px !important;
    text-align:left;
}
    </style>
    <asp:UpdatePanel ID="updatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="container-fluid" style="width: 100%; margin-top: 10px; text-align: center">
                <div class="row" style="margin-top:0px;text-align:left">
                  <div class="col-lg-12">
                    <h3 class="card-header"><i class="fa fa-calendar"></i>  Period </h3>
                  </div>
                </div>
                 <div class="row" style="margin-top:10px">
                    <div class="col-md-3  offset-4">
                        <asp:TextBox ID="txtSearchYear" runat="server" CssClass="form-control" placeholder="Year"></asp:TextBox>
                    </div>
                    <div class="col-md-3">
                        <asp:TextBox ID="txtSearchMonth" runat="server"  CssClass="form-control" placeholder="Month"></asp:TextBox>
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
                <div>
                        <div class="row btn-block" style="text-align:center">
                            <asp:UpdateProgress ID="UpdateProgress1" runat="Server" AssociatedUpdatePanelID="UpdatePanel1" DisplayAfter="2">
                                <ProgressTemplate >
                                    <asp:Image ID="Image1" runat="server" ImageUrl="~/images/spinner.gif" Width="150px" Height="80px" />
                                    <b>Loading....</b>
                                </ProgressTemplate>
                            </asp:UpdateProgress>
                        </div>

                    <div class="row" style="margin-top:10px">
                    <telerik:RadGrid ID="radData" runat="server" class="table-responsive" RenderMode="Auto" Skin="Office2010Blue" AllowPaging="false" OnNeedDataSource="radData_NeedDataSource" AutoGenerateColumns="False" OnItemCommand="radData_ItemCommand" Width="100%" Style="overflow: auto" GroupPanelPosition="Top" EnableAriaSupport="True" PageSize="9" Visible="False">
                        <ExportSettings IgnorePaging="true"></ExportSettings>
                        <MasterTableView CommandItemDisplay="Top">
                            <CommandItemSettings ShowAddNewRecordButton="False" ShowExportToCsvButton="True" ShowExportToExcelButton="True" ShowRefreshButton="false" />
                            <Columns>
                                <telerik:GridBoundColumn DataField="theyear" FilterControlAltText="Filter id column" HeaderText="Year" UniqueName="theyear">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="themonth" FilterControlAltText="Filter ID column" HeaderText="Month No" ReadOnly="True" UniqueName="themonth">
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="monthsmall" FilterControlAltText="Filter name column" HeaderText="Month Name" UniqueName="Monthsmall">
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="locked" FilterControlAltText="Filter name column" HeaderText="Status" UniqueName="locked">
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                <telerik:GridButtonColumn ButtonType="ImageButton" ItemStyle-HorizontalAlign="Center" ImageUrl="~/Images/Edit_New.png" HeaderText="" CommandName="CMDEDIT" FilterControlAltText="Filter column column" UniqueName="column">
                                </telerik:GridButtonColumn>
                            </Columns>
                        </MasterTableView>
                    </telerik:RadGrid>
                </div>
                    </div>
            </div>

            <asp:Literal ID="ActFlag" runat="server" Visible="false"></asp:Literal>
            <div class="modal fade" id="myModal" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" data-backdrop="static" data-keyboard="false">
                <div class="modal-dialog modal-lg" style="text-align: left;">
                    <asp:UpdatePanel ID="upModal" runat="server" ChildrenAsTriggers="False" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div class="modal-content">
                                <div class="modal-header">
                                    <h4 class="modal-title">
                                        <asp:Label ID="lblModalTitle" runat="server" Text=""></asp:Label></h4>
                                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>

                                </div>
                                <div class="modal-body">
                                    <div class="form-group">
                                        <label for="full_name_id" class="control-label">Year</label>
                                                        <telerik:RadComboBox ID="radYear" RenderMode="Lightweight" runat="server"
                                                            HighlightTemplatedItems="true"
                                                            EnableLoadOnDemand="true" Filter="StartsWith" Style="width:100% !important;" AutoPostBack="True"   Skin="Bootstrap" Height="120px"  >
                                                            <Items>
                                                                <telerik:RadComboBoxItem runat="server" Text="2011" Value="2011"></telerik:RadComboBoxItem>
                                                                <telerik:RadComboBoxItem runat="server" Text="2012" Value="2012"></telerik:RadComboBoxItem>
                                                                <telerik:RadComboBoxItem runat="server" Text="2013" Value="2013"></telerik:RadComboBoxItem>
                                                                <telerik:RadComboBoxItem runat="server" Text="2014" Value="2014"></telerik:RadComboBoxItem>
                                                                <telerik:RadComboBoxItem runat="server" Text="2015" Value="2015"></telerik:RadComboBoxItem>
                                                                <telerik:RadComboBoxItem runat="server" Text="2016" Value="2016"></telerik:RadComboBoxItem>
                                                                <telerik:RadComboBoxItem runat="server" Text="2017" Value="2017"></telerik:RadComboBoxItem>
                                                                <telerik:RadComboBoxItem runat="server" Text="2018" Value="2018"></telerik:RadComboBoxItem>
                                                                <telerik:RadComboBoxItem runat="server" Text="2019" Value="2019"></telerik:RadComboBoxItem>
                                                                <telerik:RadComboBoxItem runat="server" Text="2020" Value="2020"></telerik:RadComboBoxItem>
                                                                <telerik:RadComboBoxItem runat="server" Text="2021" Value="2021"></telerik:RadComboBoxItem>
                                                                <telerik:RadComboBoxItem runat="server" Text="2022" Value="2022"></telerik:RadComboBoxItem>
                                                                <telerik:RadComboBoxItem runat="server" Text="2023" Value="2023"></telerik:RadComboBoxItem>
                                                                <telerik:RadComboBoxItem runat="server" Text="2024" Value="2024"></telerik:RadComboBoxItem>
                                                                <telerik:RadComboBoxItem runat="server" Text="2025" Value="2025"></telerik:RadComboBoxItem>
                                                                <telerik:RadComboBoxItem runat="server" Text="2026" Value="2026"></telerik:RadComboBoxItem>
                                                                <telerik:RadComboBoxItem runat="server" Text="2027" Value="2027"></telerik:RadComboBoxItem>
                                                                <telerik:RadComboBoxItem runat="server" Text="2028" Value="2028"></telerik:RadComboBoxItem>
                                                                <telerik:RadComboBoxItem runat="server" Text="2029" Value="2029"></telerik:RadComboBoxItem>
                                                                <telerik:RadComboBoxItem runat="server" Text="2030" Value="2030"></telerik:RadComboBoxItem>
                                                                <telerik:RadComboBoxItem runat="server" Text="2031" Value="2031"></telerik:RadComboBoxItem>
                                                                <telerik:RadComboBoxItem runat="server" Text="2032" Value="2032"></telerik:RadComboBoxItem>
                                                                <telerik:RadComboBoxItem runat="server" Text="2033" Value="2033"></telerik:RadComboBoxItem>
                                                                <telerik:RadComboBoxItem runat="server" Text="2034" Value="2034"></telerik:RadComboBoxItem>
                                                                <telerik:RadComboBoxItem runat="server" Text="2035" Value="2035"></telerik:RadComboBoxItem>
                                                                <telerik:RadComboBoxItem runat="server" Text="2036" Value="2036"></telerik:RadComboBoxItem>

                                                            </Items>
                                                        </telerik:RadComboBox>  
                                    </div>
                                    <div class="form-group">
                                        <label for="full_name_id" class="control-label">Month</label>
                                                        <telerik:RadComboBox ID="radMonth" RenderMode="Lightweight" runat="server"
                                                            HighlightTemplatedItems="true"
                                                            EnableLoadOnDemand="true" Filter="StartsWith" Style="width:100% !important;" AutoPostBack="True"   Skin="Bootstrap" Height="120px"  >
                                                            <Items>
                                                                <telerik:RadComboBoxItem runat="server" Text="Jan" Value="1"></telerik:RadComboBoxItem>
                                                                <telerik:RadComboBoxItem runat="server" Text="Feb" Value="2"></telerik:RadComboBoxItem>
                                                                <telerik:RadComboBoxItem runat="server" Text="Mar" Value="3"></telerik:RadComboBoxItem>
                                                                <telerik:RadComboBoxItem runat="server" Text="Apr" Value="4"></telerik:RadComboBoxItem>
                                                                <telerik:RadComboBoxItem runat="server" Text="May" Value="5"></telerik:RadComboBoxItem>
                                                                <telerik:RadComboBoxItem runat="server" Text="Jun" Value="6"></telerik:RadComboBoxItem>
                                                                <telerik:RadComboBoxItem runat="server" Text="Jul" Value="7"></telerik:RadComboBoxItem>
                                                                <telerik:RadComboBoxItem runat="server" Text="Aug" Value="8"></telerik:RadComboBoxItem>
                                                                <telerik:RadComboBoxItem runat="server" Text="Sep" Value="9"></telerik:RadComboBoxItem>
                                                                <telerik:RadComboBoxItem runat="server" Text="Oct" Value="10"></telerik:RadComboBoxItem>
                                                                <telerik:RadComboBoxItem runat="server" Text="Nov" Value="11"></telerik:RadComboBoxItem>
                                                                <telerik:RadComboBoxItem runat="server" Text="Dec" Value="12"></telerik:RadComboBoxItem>

                                                            </Items>
                                                        </telerik:RadComboBox>  

                                    </div>
                                    <div class="form-group">
                                        <label for="full_name_id" class="control-label">Locked</label>
                                                        <telerik:RadComboBox ID="radLocked" RenderMode="Lightweight" runat="server"
                                                            HighlightTemplatedItems="true"
                                                            EnableLoadOnDemand="true" Filter="StartsWith" Style="width:100% !important;" AutoPostBack="True"   Skin="Bootstrap" Height="120px"  >
                                                            <Items>
                                                                <telerik:RadComboBoxItem runat="server" Text="Yes" Value="Y"></telerik:RadComboBoxItem>
                                                                <telerik:RadComboBoxItem runat="server" Text="No" Value="N"></telerik:RadComboBoxItem>
                                                            </Items>
                                                        </telerik:RadComboBox>  

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
