<%@ Assembly Name="CPTUDocumentApprovalWorkflow, Version=1.0.0.0, Culture=neutral, PublicKeyToken=2eb8c4dfd12a657f" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucMyPendingTask.ascx.cs" Inherits="CPTUDocumentApprovalWorkflow.CONTROLTEMPLATES.DocumentApproval.ucMyPendingTask" %>

  <script type="text/javascript">
      <!--Datatable-->
      $(document).ready(function () {
          $("#gvMyPendingTask").prepend($("<thead></thead>").append($(this).find("tr:first"))).dataTable({
              aaSorting: [[0, 'desc']]
          });
      });
  </script>
 <div class="my-content">
       <div class="p-title"><h6>My Pending Task</h6></div> 
      <span class="text-center"><asp:Label ID="msgLabel" runat="server" Text=""></asp:Label></span>
      <asp:GridView ID="gvMyPendingTask" Class="gvMyRequest"  EnableModelValidation="True" style="clear:none; overflow-y:auto;height: auto" runat="server" ClientIDMode="Static" CssClass="table table-bordered"  AutoGenerateColumns="False" ForeColor="#979797" GridLines="None"  >
            <AlternatingRowStyle BackColor="White" />
            <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" HorizontalAlign="Center"/>
            <HeaderStyle BackColor="#4647B8" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
            <RowStyle HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />               
             <Columns>
                <asp:BoundField DataField="ID" HeaderText="ID" Visible="False" />
                <asp:BoundField DataField="FormLink" HeaderText="Form Link" Visible="False" />  
                <asp:TemplateField HeaderText="Request No.">
                  <ItemTemplate>                 
                    <asp:HyperLink ID="HyperLink1" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"ReqNo") %>' NavigateUrl='<%# Eval("ReqLink", "{0}") %>'></asp:HyperLink>
                  </ItemTemplate>           
                </asp:TemplateField>

                <asp:BoundField DataField="Title" HeaderText="Title" >
                </asp:BoundField> 

                  <asp:BoundField DataField="RequesterName" HeaderText="Requestor Name" >    
                </asp:BoundField> 

                 <asp:BoundField DataField="RequestDate" HeaderText="Request Date" >    
                </asp:BoundField>
                                                                                                                 
                <asp:BoundField DataField="RequestStatus" HeaderText="Request Status" >
                </asp:BoundField>                                                         
            </Columns>                 
     </asp:GridView>   
  </div>
