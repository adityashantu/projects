<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucIndex.ascx.cs" Inherits="CPTUDocumentApprovalWorkflow.CONTROLTEMPLATES.DocumentApproval.ucIndex" %>
<link href="../../../_layouts/15/Content/fontawesome/css/all.css" rel="stylesheet" />
<style type="text/css">
    .index-title {
      color: #018c01;
      border-bottom: 2px solid #b86c13;
      padding-bottom: 0px;
      width: 80%;
      margin-left: 0px;
    }
    #sideNavBox {
      display: none;
    }
</style>
<div class="index-title"><h5>Welcome to Document Approval System</h5></div>
<div class="my-content" style="width:80%;margin-top:10px;">
      
      <table  class="table table-bordered table-sm form-inline" style="font-family: ui-sans-serif; font-size: 15px;">                                     
            <tr>
                <td><i class="fa-solid fa-plus"></i>&nbsp&nbsp<a href="/sites/dms/_layouts/15/DocumentApproval/NewRequest.aspx">New Request</a></td>                   
            </tr>
            <tr>                
                <td><i class="fa-solid fa-pen-to-square"></i>&nbsp&nbsp<a href="/sites/dms/_layouts/15/DocumentApproval/MyRequest.aspx"  target="_blank">My Requests</a> </td>               
            </tr>
            <tr>                
                <td><i class="fa-solid fa-file"></i>&nbsp&nbsp<a href="/sites/dms/_layouts/15/start.aspx#/DAAttachments/Forms/My%20Documents.aspx"  target="_blank">My Documents</a> </td>               
            </tr>
            <tr>                
                <td><i class="fas fa-tasks"></i>&nbsp&nbsp<a href="/sites/dms/_layouts/15/DocumentApproval/MyPendingTask.aspx"  target="_blank">My Pending Task </a> </td>                 </tr>
           <%-- <tr>                
                <td><i class="fa-solid fa-screwdriver-wrench"></i>&nbsp&nbsp<a href="/_layouts/15/DocumentApproval/AllRequest.aspx" target="_blank">Admin View</a> </td>                 
            </tr>--%>
       </table>
 </div>
         