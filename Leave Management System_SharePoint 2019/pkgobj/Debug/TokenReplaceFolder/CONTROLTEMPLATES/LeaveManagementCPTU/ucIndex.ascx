<%@ Assembly Name="LeaveManagementCPTU, Version=1.0.0.0, Culture=neutral, PublicKeyToken=eda72657dcf68580" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucIndex.ascx.cs" Inherits="LeaveManagementCPTU.CONTROLTEMPLATES.LeaveManagementCPTU.ucIndex" %>



<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Document</title>
</head>
<body>
    <fieldset>
        <legend style="font-size: 30px;"><b>Leave Management System</b></legend>
        <div style="padding: 10px; display: inline-block;">
            <asp:Button Style="background-color: #0078d4; padding: 10px; margin-left: 50px; width: 200px; clear: both; color: white; font-weight: bold; font-size: 15px; text-align: center; vertical-align: middle; cursor: pointer;"
                ID="MyRequest"
                runat="server"
                Text="My Requests"
                OnClick="MyRequests_Click" />
        </div>
        <div style="padding: 10px; display: inline-block;">
            <asp:Button Style="background-color: #0078d4; padding: 10px; margin-left: 50px; width: 200px; clear: both; color: white; font-weight: bold; font-size: 15px; text-align: center; vertical-align: middle; cursor: pointer;"
                ID="NewRequest"
                runat="server"
                Text="New Request"
                OnClick="NewRequest_Click" />
        </div>
        <div style="padding: 10px; display: inline-block;">
            <asp:Button Style="background-color: #0078d4; padding: 10px; margin-left: 50px; width: 200px; clear: both; color: white; font-weight: bold; font-size: 15px; text-align: center; vertical-align: middle; cursor: pointer;"
                ID="ManagerHome"
                runat="server"
                Text="Manager Home"
                OnClick="ManagerHome_Click" />
        </div>
        <div style="padding: 10px; display: inline-block;">
            <asp:Button Style="background-color: #0078d4; padding: 10px; margin-left: 50px; width: 200px; clear: both; color: white; font-weight: bold; font-size: 15px; text-align: center; vertical-align: middle; cursor: pointer;"
                ID="AdminPanel"
                runat="server"
                Text="Admin Panel"
                OnClick="AdminPanel_Click" />
        </div>
    </fieldset>
</body>
</html>
