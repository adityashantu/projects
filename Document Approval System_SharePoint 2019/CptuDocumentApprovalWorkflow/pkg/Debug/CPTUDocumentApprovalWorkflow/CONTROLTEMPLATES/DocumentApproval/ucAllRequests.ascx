<%@ Assembly Name="CPTUDocumentApprovalWorkflow, Version=1.0.0.0, Culture=neutral, PublicKeyToken=2eb8c4dfd12a657f" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucAllRequests.ascx.cs" Inherits="CPTUDocumentApprovalWorkflow.CONTROLTEMPLATES.DocumentApproval.ucAllRequests" %>

<style type="text/css">
    .form-width {
        width:100%;
        height:28px;
    }
</style>
<script type="text/javascript">
      <!--Datatable-->
      $(document).ready(function () {
          $("#gvAllReqItems").prepend($("<thead></thead>").append($(this).find("tr:first"))).dataTable({
              aaSorting: [[0, 'desc']]
          });
      });
  </script>

 <div class="my-content">
         <div class="p-title"><h6>Admin View</h6></div> 
          <table  class="table table-bordered table-sm " style="width:100%; margin-top:15px; table-layout: fixed;font-family: ui-sans-serif; font-size: 15px; background:#ffffff; ">                        
             <tr>
                <td></td>
                 <td></td>
                 <td></td>
                 <td></td>
             </tr>
              <tr>
                  <td>Request Number:</td>
                  <td>
                        <asp:TextBox ID="txtRequestNumber" runat="server" ClientIDMode="Static" CssClass="form-width"  ></asp:TextBox>  
                  </td>
                  <td>Status:</td>
                  <td>
                      <asp:DropDownList ID="ddlStatus" runat="server" ClientIDMode="Static" CssClass="form-width" >                
                        <asp:ListItem Value="0" Text="--- Select ---" ></asp:ListItem>    
                        <asp:ListItem Value="100" Text="Completed"></asp:ListItem>
                        <asp:ListItem Value="50" Text="In Progress"></asp:ListItem>
                        <asp:ListItem Value="101" Text="Rejected"></asp:ListItem>                                     
                    </asp:DropDownList>  
                  </td>
              </tr>
                 <tr>
                  <td>
                      From Date:
                  </td>
                  <td>
                      <asp:TextBox ID="dtpFromDate" runat="server" ClientIDMode="Static" onkeypress="javascript: return false;" ></asp:TextBox>
                  </td>
                  <td>To Date:</td>
                  <td>
                       <asp:TextBox ID="dtpToDate" runat="server" ClientIDMode="Static" onkeypress="javascript: return false;" ></asp:TextBox>
                  </td>
              </tr>
              <tr>
                  <td colspan="4">
                      <asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_Click" Text="Search" /> 
                      <asp:Button ID="btnReset" runat="server" OnClick="btnReset_Click" Text="Reset" /> 
                  </td>
              </tr>

          </table>
                   
                                                                              
       <span class="text-center"><asp:Label ID="msgLabel" runat="server" Text=""></asp:Label></span>
       <asp:GridView ID="gvAllReqItems" Class="gvMyRequest"  EnableModelValidation="True" style="clear:none; overflow-y:auto;height: auto" runat="server" ClientIDMode="Static" CssClass="table table-bordered"  AutoGenerateColumns="False" ForeColor="#979797" GridLines="None"  >
            <AlternatingRowStyle BackColor="White" />
            <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" HorizontalAlign="Center"/>
            <HeaderStyle BackColor="#4647B8" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
            <RowStyle HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />               
             <Columns>
                <asp:BoundField DataField="ID" HeaderText="ID" Visible="False" />
                <asp:BoundField DataField="FormLink" HeaderText="Form Link" Visible="False" />  
                <asp:TemplateField HeaderText="Request Number">
                  <ItemTemplate>                 
                    <asp:HyperLink ID="HyperLink1" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"ReqNo") %>' NavigateUrl='<%# Eval("ReqLink", "{0}") %>'></asp:HyperLink>
                  </ItemTemplate>           
                </asp:TemplateField>
                <asp:BoundField DataField="Title" HeaderText="Title" >
                </asp:BoundField> 
                 <asp:BoundField DataField="RequestDate" HeaderText="Request Date" >    
                </asp:BoundField>
                 <asp:BoundField DataField="RequesterName" HeaderText="Requester Name" >    
                </asp:BoundField>                                                                                                  
                <asp:BoundField DataField="RequestStatus" HeaderText="Request Status" >
                </asp:BoundField>                                                         
            </Columns>                 
     </asp:GridView> 
  </div>
