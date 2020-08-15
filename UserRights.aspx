
<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.master" AutoEventWireup="true" Inherits="NewSm1.UserRights" CodeBehind="UserRights.aspx.cs" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <script type="text/javascript">
        $(document).ready(function () {
            var tab = document.getElementById('<%= hidTAB.ClientID%>').value;
        $('#myTabs a[href="' + tab + '"]').tab('show');
    });
    </script>
    <style>
        box box-body {
            min-height: 500px;
        }
    .modal-lg {
    max-width: 90% !important;
    text-align:left;
}
    </style>
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <div class=" card">
                <div class=" card-header">
                    <h5>Link Tables</h5>
                </div>
            </div>
            <div class=" card-body">
                <telerik:RadTabStrip ID="RadTabStrip1" runat="server" Skin="Bootstrap" MultiPageID="RadMultiPage1">
                    <Tabs>
                        <telerik:RadTab runat="server" Text="Zone- Region" PageViewID="ZoneRegion"></telerik:RadTab>
                        <telerik:RadTab runat="server" Text="Region - HQ" PageViewID="RegionHQ"></telerik:RadTab>
                        <telerik:RadTab runat="server" Text="HQ - Stockist" PageViewID="HQStockist"></telerik:RadTab>
                        <telerik:RadTab runat="server" Text="Employee - HQ" PageViewID="EMPHQ"></telerik:RadTab>

                    </Tabs>
                </telerik:RadTabStrip>
            </div>
            <telerik:RadMultiPage ID="RadMultiPage1" runat="server">
                <telerik:RadPageView ID="ZoneRegion" runat="server">
                    <div class="card">
                        <div class="card-header" style="text-align: center">
                            <label>Select Zone</label><br />
                            <telerik:RadComboBox ID="ddZone" runat="server" Skin="Bootstrap" Height="300px"></telerik:RadComboBox>
                            <asp:Button ID="btnAssign" runat="server" Text="Select" Width="100px" CssClass="btn-primary" OnClick="btnAssign_Click" />
                            <asp:Button ID="btnExit" runat="server" Text="Exit" Width="100px" CssClass="btn-danger" OnClick="btnExit_Click" />
                        </div>
                        <div class="card-body">
                            <div class="row">
                                <div class="col-md-5">
                                    <asp:ListBox ID="lstZoneYes" SelectionMode="Multiple" Width="100%" runat="server" Style="min-height: 200px"></asp:ListBox>
                                </div>
                                <div class="col-md-2" style="text-align: center; margin-top: 30px">
                                    <asp:Button ID="btnAssignRegion" runat="server" Text="<<<<" CssClass="btn btn-primary" OnClick="btnAssignRegion_Click" />
                                    <br />
                                    <br />
                                    <asp:Button ID="btnRemoveRegion" runat="server" Text=">>>>" CssClass="btn btn-danger" OnClick="btnRemoveRegion_Click" />
                                </div>
                                <div class="col-md-5">
                                    <asp:ListBox ID="lstZoneNo" Width="100%" SelectionMode="Multiple" runat="server" Style="min-height: 200px"></asp:ListBox>
                                </div>
                            </div>
                        </div>
                    </div>
                </telerik:RadPageView>
                <telerik:RadPageView ID="RegionHQ" runat="server">
                    <div class="card">
                        <div class="card-header" style="text-align: center">
                            <label>Select Region</label><br />
                            <telerik:RadComboBox ID="ddRegion" runat="server" Skin="Bootstrap" Width="300px" Height="150px"></telerik:RadComboBox>
                            <asp:Button ID="btnAssignHQ" runat="server" Text="Select" Width="100px" CssClass="btn-primary" OnClick="btnAssignHQ_Click" />
                            <asp:Button ID="btnExitHQ" runat="server" Text="Exit" Width="100px" CssClass="btn-danger" />
                        </div>
                        <div class="card-body">
                            <div class="row">
                                <div class="col-md-5">
                                    <asp:ListBox ID="lstHQYes" SelectionMode="Single" Width="100%" runat="server" Style="min-height: 200px"></asp:ListBox>
                                </div>
                                <div class="col-md-2" style="text-align: center; margin-top: 10px">
                                    <asp:TextBox ID="txtPercentage" runat="server" CssClass="form-control" Text="100" Style="text-align: center"></asp:TextBox><br />

                                    <asp:Button ID="btnAddHQ" runat="server" Text="<<<<" CssClass="btn btn-primary" OnClick="btnAddHQ_Click" />
                                    <br />
                                    <br />
                                    <asp:Button ID="btnRemoveHQ" runat="server" Text=">>>>" CssClass="btn btn-danger" OnClick="btnRemoveHQ_Click" />
                                </div>
                                <div class="col-md-5">
                                    <asp:ListBox ID="lstHQNo" Width="100%" SelectionMode="Single" runat="server" Style="min-height: 200px"></asp:ListBox>
                                </div>
                            </div>
                            <div class="row" style="text-align: center">
                                <asp:Label ID="lblErrorRegionHQ" runat="server" Text="" ForeColor="Red"></asp:Label>
                            </div>
                        </div>
                    </div>
                </telerik:RadPageView>
                <telerik:RadPageView ID="HQStockist" runat="server">
                    <div class="card">
                        <div class="card-header" style="text-align: center">
                            <label>Select Headquarter</label><br />
                            <telerik:RadComboBox ID="ddHQ" runat="server" Skin="Bootstrap" Width="300px" Height="150px"></telerik:RadComboBox>
                            <asp:Button ID="btnAssignStokist" runat="server" Text="Select" Width="100px" CssClass="btn-primary" OnClick="btnAssignStockist_Click" />
                            <asp:Button ID="btnExitStockist" runat="server" Text="Exit" Width="100px" CssClass="btn-danger" OnClick="btnExitStockist_Click" />
                        </div>
                        <div class="card-body">
                            <div class="row">
                                <div class="col-md-5">
                                    <asp:ListBox ID="lstStockistYes" SelectionMode="Single" Width="100%" runat="server" Style="min-height: 200px"></asp:ListBox>
                                </div>
                                <div class="col-md-2" style="text-align: center; margin-top: 10px">
                                    <asp:Button ID="btnAddStockist" runat="server" Text="<<<<" CssClass="btn btn-primary" OnClick="btnAddStockist_Click" />
                                    <br />
                                    <br />
                                    <asp:Button ID="btnRemoveStockist" runat="server" Text=">>>>" CssClass="btn btn-danger" OnClick="btnRemoveStockist_Click" />
                                    <br />
                                    <br />
                                    <asp:Button ID="btnMapStockistHQ" runat="server" Text="Map" CssClass="btn  btn-info" Width="60px" OnClick="btnMapStockistHQ_Click"  />

                                </div>
                                <div class="col-md-5">
                                    <asp:ListBox ID="lstStockistNo" Width="100%" SelectionMode="Single" runat="server" Style="min-height: 200px"></asp:ListBox>
                                </div>
                            </div>
                            <div class="row" style="text-align: center">
                                <asp:Label ID="lblErrorRegionStokist" runat="server" Text="" ForeColor="Red"></asp:Label>
                            </div>
                        </div>
                    </div>
                </telerik:RadPageView>
                <telerik:RadPageView ID="EMPHQ" runat="server">
                    <div class="card">
                        <div class="card-header" style="text-align: center">
                            <label>Select Employee</label><br />
                            <telerik:RadComboBox ID="ddEmployee" runat="server" Skin="Bootstrap" Width="600px" Height="150px" Filter="Contains"></telerik:RadComboBox>
                            <asp:Button ID="btnEmpHQ" runat="server" Text="Select" Width="100px" CssClass="btn-primary" OnClick="btnEmpHQ_Click" />
                            <asp:Button ID="btnEmpExitHQ" runat="server" Text="Exit" Width="100px" CssClass="btn-danger" OnClick="btnEmpExitHQ_Click" />
                        </div>
                        <div class="card-body">
                            <div class="row">
                                <div class="col-md-5">
                                    <asp:ListBox ID="lstEmpHQYes" SelectionMode="Single" Width="100%" runat="server" Style="min-height: 200px"></asp:ListBox>
                                </div>
                                <div class="col-md-2" style="text-align: center; margin-top: 10px">
                                    <asp:TextBox ID="txtEmpPercentage" runat="server" CssClass="form-control" Text="100" Style="text-align: center"></asp:TextBox><br />

                                    <asp:Button ID="btnAddEmpHQ" runat="server" Text="<<<<" CssClass="btn btn-primary" OnClick="btnAddEmpHQ_Click" />
                                    <br />
                                    <br />
                                    <asp:Button ID="btnRemoveEmpHQ" runat="server" Text=">>>>" CssClass="btn btn-danger" OnClick="btnRemoveEmpHQ_Click" />
                                    <br />
                                    <br />
                                    <asp:Button ID="btnMapEmpHQ" runat="server" Text="Map" CssClass="btn  btn-info" Width="60px" OnClick="btnMapEmpHQ_Click" />
                                </div>
                                <div class="col-md-5">
                                    <asp:ListBox ID="lstEmpHQNo" Width="100%" SelectionMode="Single" runat="server" Style="min-height: 200px"></asp:ListBox>
                                </div>
                            </div>
                            <div class="row" style="text-align: center">
                                <asp:Label ID="lblErrorEmpHQ" runat="server" Text="" ForeColor="Red"></asp:Label>
                            </div>
                        </div>
                    </div>
                </telerik:RadPageView>
            </telerik:RadMultiPage>

        </ContentTemplate>
    </asp:UpdatePanel>
<%--            THis is for Headquarter and Employee Link Map--%>
            <div class="modal fade" id="myModal" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" data-backdrop="static" data-keyboard="false">
                <div class="modal-dialog modal-lg" style="text-align: left;">
                    <asp:UpdatePanel ID="upModal" runat="server" ChildrenAsTriggers="False" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div class="modal-content">
                                <div class="modal-header">
                                    <h4 class="modal-title">
                                        Employee - Headquarter Link

                                    </h4>
                                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                                </div>
                                <div class="modal-body">
                                    <div class="pre-scrollable">
                                        <telerik:RadGrid ID="RadGrid1" runat="server" Skin="Bootstrap" AutoGenerateColumns="False"
                                            AllowFilteringByColumn="True" OnNeedDataSource="RadGrid1_NeedDataSource" CellSpacing="-1" GridLines="Both">
                                            <ClientSettings>
                                                <Scrolling AllowScroll="True" UseStaticHeaders="True"></Scrolling>
                                            </ClientSettings>
                                            <MasterTableView>
                                                <Columns>
                                                   
                                                    <telerik:GridBoundColumn DataField="positionid" HeaderText="ID" UniqueName="positionid"    
                                                        
                                                        FilterControlAltText="Filter positionid column">
                                                         <ItemStyle Width="20px" /> 
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="positionname" HeaderText="Position Name"                                        UniqueName="positionname"  
                                                        FilterControlAltText="Filter positionname column">

                                                    </telerik:GridBoundColumn>
                                                             <telerik:GridBoundColumn DataField="headquarter_id" HeaderText="HQID"                                              UniqueName="headquarter_id"
                                                                 FilterControlAltText="Filter positionname column">
                                                         
                                                    </telerik:GridBoundColumn>  
                                                             <telerik:GridBoundColumn DataField="linkhq" HeaderText="Headquarter"                                              UniqueName="linkhq"
                                                                 FilterControlAltText="Filter positionname column">
                                                         
                                                    </telerik:GridBoundColumn>  
                                                    <telerik:GridBoundColumn DataField="name" HeaderText="Employee Name" UniqueName="name"                         FilterControlAltText="Filter Name column">
                                                         
                                                    </telerik:GridBoundColumn>                                                    
                                                </Columns>
                                            </MasterTableView>
                                        </telerik:RadGrid>
                                    </div>
                                </div>
                                <div class="modal-footer">
                                    <button class="btn btn-info" data-dismiss="modal" aria-hidden="true">Close</button>
                                </div>
                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="RadGrid1" EventName="Load" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>

                <div class="modal fade" id="myModal1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" data-backdrop="static" data-keyboard="false">
                <div class="modal-dialog modal-lg" style="text-align: left;">
                    <asp:UpdatePanel ID="upModal1" runat="server" ChildrenAsTriggers="False" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div class="modal-content">
                                <div class="modal-header">
                                    <h4 class="modal-title">
                                        Stokist - Headquarter Link
                                    </h4>
                                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                                </div>
                                <div class="modal-body">
                                    <div class="pre-scrollable">
                                        <telerik:RadGrid ID="radMapStockistHQ" runat="server" Skin="Bootstrap" AutoGenerateColumns="False"
                                            AllowFilteringByColumn="True"  CellSpacing="-1" GridLines="Both" OnNeedDataSource="radMapStockistHQ_NeedDataSource">
                                            <ClientSettings>
                                                <Scrolling AllowScroll="True" UseStaticHeaders="True"></Scrolling>
                                            </ClientSettings>
                                            <MasterTableView>
                                                <Columns>
                                                   
                                                   <telerik:GridBoundColumn DataField="stockist_id" HeaderText="Code" UniqueName="stockist_id"    
                                                        FilterControlAltText="Filter positionid column">
                                                         <ItemStyle Width="120px" />
                                                         <HeaderStyle Width="120px" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="stockist_name" HeaderText="Customer Name"   UniqueName="stockist_name"  
                                                        FilterControlAltText="Filter positionname column">
                                                    </telerik:GridBoundColumn>
                                                             <telerik:GridBoundColumn DataField="typedesc" HeaderText="Type" UniqueName="typedesc"
                                                                 FilterControlAltText="Filter positionname column">
                                                                 <ItemStyle Width="120px" />                                                         
                                                                 <HeaderStyle Width="120px" />
                                                                 
                                                    </telerik:GridBoundColumn>  
                                                             <telerik:GridBoundColumn DataField="stockist_city" HeaderText="City" UniqueName="stockist_city"
                                                                 FilterControlAltText="Filter positionname column">
                                                            </telerik:GridBoundColumn>  
                                                    <telerik:GridBoundColumn DataField="headquarter_name" HeaderText="Headquarter" UniqueName="headquarter_name"                         
                                                        FilterControlAltText="Filter positionname column">           
                                                    </telerik:GridBoundColumn>       
                                                    <telerik:GridBoundColumn DataField="headquarter_id" HeaderText="HQID" UniqueName="headquarter_id"                    
                                                        FilterControlAltText="Filter positionname column">    
                                                        <HeaderStyle Width="60px" />
                                                    </telerik:GridBoundColumn>                                                       
                                                </Columns>
                                            </MasterTableView>
                                        </telerik:RadGrid>
                                    </div>
                                </div>
                                <div class="modal-footer">
                                    <button class="btn btn-info" data-dismiss="modal" aria-hidden="true">Close</button>
                                </div>
                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="RadGrid1" EventName="Load" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>

    <asp:Literal ID="ActFlag" runat="server" Visible="false"></asp:Literal>
    <asp:HiddenField ID="hidTAB" runat="server" />
    <script src="js/jquery-3.5.1.min.js"></script>
    <script src="js/bootstrap.min.js"></script>
</asp:Content>

