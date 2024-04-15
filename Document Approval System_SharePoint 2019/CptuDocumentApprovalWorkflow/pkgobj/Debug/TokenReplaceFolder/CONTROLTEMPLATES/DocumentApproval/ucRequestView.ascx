<%@ Assembly Name="CPTUDocumentApprovalWorkflow, Version=1.0.0.0, Culture=neutral, PublicKeyToken=2eb8c4dfd12a657f" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucRequestView.ascx.cs" Inherits="CPTUDocumentApprovalWorkflow.CONTROLTEMPLATES.DocumentApproval.ucRequestView" %>

<style type="text/css">
    .include-table {
        padding: 0px !important;
    }


    input[type="checkbox"], input[type="radio"] {
        margin-right: 5px;
        margin-bottom: 5px;
        margin-left: 5px;
    }
    
    label {
        margin-bottom: 0px;
        font-weight: 300;
    }
    .btnSubmit input[type="submit"], button {
    min-width: 7em;
    padding: 7px 10px;
    border: 1px solid #009ACE;
    background-color: #009ACE;
    margin-right: 46px;
    margin-bottom: 12px;
    font-family: "Segoe UI","Segoe",Tahoma,Helvetica,Arial,sans-serif;
    font-size: 13px;
    color: #ffffff;
    height: 38px;
    border-radius: 4px;
    font-weight: bold;
    float:right;
   }
    .btnSubmit input[type="submit"]:hover{
	background: #167595;
	color: #fff;
    }
      .btnLoadInfo input[type="submit"], button {
    min-width: 5em;
    padding: 6px 10px;
    border: 1px solid #BECCD1;
    background-color: #f3fafb;
    margin-left: 1px;
    font-family: "Segoe UI","Segoe",Tahoma,Helvetica,Arial,sans-serif;
    font-size: 13px;
    color: #009ACE;
    height: 34px;
    border-radius: 4px;
    font-weight: bold;
   }
    .btnLoadInfo input[type="submit"]:hover{
	/*background: #167595;*/
	color: #167595;
    }
</style>
 <div class="header-title">Document Approval</div>
<div class="col-12 content">           
    <div>
        <table  class="table table-bordered table-sm form-inline" style="width:85%; table-layout: fixed;font-family: ui-sans-serif; font-size: 15px;">                         
             <%--<tr>
                <td colspan="4" style="text-align:center;background: #0072c6;" class="heading">Document Approval</td>
            </tr>--%>
            <tr>
                <td>Request No.:</td>
                <td>                                   
                    <asp:Label ID="txtRequestNo" runat="server" Text=""></asp:Label>
                </td>
             
                <td>Request Date:</td>
                <td>
                    <asp:Label ID="txtRequestDate" runat="server" Text=""></asp:Label>        
                </td>
            </tr>
            <tr>
                <td>Requester Name:</td>
                <td>                                   
                    <asp:Label ID="txtRequesterName" runat="server" Text=""></asp:Label>
                </td>
             
                <td>Email:</td>
                <td>
                    <asp:Label ID="txtRequesterEmail" runat="server" Text=""></asp:Label>        
                </td>
            </tr>
            <tr>
                <td>Department/Division:</td>
                <td>                                   
                    <asp:Label ID="txtRequesterDepartment" runat="server" Text=""></asp:Label>
                </td>
             
                <td>Designation:</td>
                <td>
                    <asp:Label ID="txtRequesterDesignation" runat="server" Text=""></asp:Label>        
                </td>
            </tr>
             <tr>
                <td>Comment:</td>
                <td colspan="3">                                   
                    <asp:Label ID="txtComment" runat="server" Text=""></asp:Label>
                </td>                        
            </tr> 
             <tr>                
                <td colspan="4">                                   
                     <asp:GridView ID="gvAttachments" runat="server" AutoGenerateColumns="false"  CssClass="table table-bordered" >
                    <Columns>
                        <asp:BoundField DataField="ID" HeaderText="ID" Visible="False" />
                        <asp:TemplateField HeaderText="Attachments">
                            <ItemTemplate>                               
                                <a href="<%#Eval("FileUrl")%>" target="_blank"> <%#Eval("FileName")%></a>
                            </ItemTemplate>
                        </asp:TemplateField>                                                                                                 
                    </Columns>
                </asp:GridView>    
                </td>                        
            </tr>
            <tr>
                <asp:Panel ID="pblApprovalHistory" Visible="false" runat="server">
                     <td colspan="4" style="background:#f6f7f8;" >
                      <h6 style="text-align:center">Approval History</h6>
                      <asp:GridView ID="gvApprovalHistory" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered">
                       <Columns>
                            <asp:BoundField DataField="ID" HeaderText="ID" Visible="False" />                                                                                                            
                            <asp:BoundField DataField="ApprovalLevel" HeaderText="Approval Level" > </asp:BoundField> 
                            <asp:BoundField DataField="Approval" HeaderText="Action" > </asp:BoundField>
                            <asp:BoundField DataField="ApprovedBy" HeaderText="Approved By" > </asp:BoundField>
                            <asp:BoundField DataField="ApprovalDate" HeaderText="Approval Date" > </asp:BoundField>   
                            <asp:BoundField DataField="Comment" HeaderText="Comment" > </asp:BoundField>    
                        </Columns>
                     </asp:GridView>
                </td>
                </asp:Panel>
               
            </tr>
        </table>
    </div> 
</div> 
         

