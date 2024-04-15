<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Register Src="~/_controltemplates/15/DocumentApproval/ucAllRequests.ascx" TagPrefix="uc1" TagName="ucAllRequests" %>


<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AllRequest.aspx.cs" Inherits="CPTUDocumentApprovalWorkflow.Layouts.DocumentApproval.AllRequest" DynamicMasterPageFile="~masterurl/default.master" %>

<asp:Content ID="PageHead" ContentPlaceHolderID="PlaceHolderAdditionalPageHead" runat="server">
     <link href="../Content/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../Content/css/site.css" rel="stylesheet" />
    <link href="../Content/css/jquery.dataTables.min.css" rel="stylesheet" />
    <script src="../Content/js/jquery.min.js"></script>
    <script src="../Content/js/jquery.dataTables.min.js"></script>
    <link href="../Content/css/jquery-ui.css" rel="stylesheet" />
    <script src="../Content/js/jquery-ui.js"></script>
    <script type="text/javascript">
  
    //// date picker 
    $(function () {
        $("#dtpFromDate").datepicker({
            showOn: "button",
            buttonImage: "../../../_layouts/15/Content/img/calendar.gif",
            buttonImageOnly: true,
            buttonText: "Select from date",
            changeMonth: true,
            changeYear: true,
            //dateFormat: "dd/mm/yy",
            //defaultDate: -30
        });
        $("#dtpToDate").datepicker({
            showOn: "button",
            buttonImage: "../../../_layouts/15/Content/img/calendar.gif",
            buttonImageOnly: true,
            buttonText: "Select to date",
            changeMonth: true,
            changeYear: true,
            //dateFormat: "dd/mm/yy",
            //defaultDate: +0
        });
    });
    </script>

</asp:Content>

<asp:Content ID="Main" ContentPlaceHolderID="PlaceHolderMain" runat="server">
    <uc1:ucAllRequests runat="server" id="ucAllRequests" />
</asp:Content>

<asp:Content ID="PageTitle" ContentPlaceHolderID="PlaceHolderPageTitle" runat="server">
Admin View
</asp:Content>

<asp:Content ID="PageTitleInTitleArea" ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea" runat="server" >

</asp:Content>
