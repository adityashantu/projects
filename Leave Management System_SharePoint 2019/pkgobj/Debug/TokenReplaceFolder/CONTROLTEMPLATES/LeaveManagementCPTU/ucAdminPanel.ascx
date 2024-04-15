<%@ Assembly Name="LeaveManagementCPTU, Version=1.0.0.0, Culture=neutral, PublicKeyToken=eda72657dcf68580" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucAdminPanel.ascx.cs" Inherits="LeaveManagementCPTU.CONTROLTEMPLATES.LeaveManagementCPTU.ucAdminPanel" %>


<link href="../../../_layouts/15/LeaveManagementCPTU/Design/CSS/toastr.min.css" rel="stylesheet" />
<script src="../../../_layouts/15/LeaveManagementCPTU/Design/SCRIPT/toastr.min.js"></script>




<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Document</title>
</head>

<body>
    <asp:Panel ID="AdminCheck" runat="server" Visible="false">

    <fieldset>
        <legend style="font-size: 30px;"><b>Search User</b></legend>

        <div>

            <div style="display: inline-block;">
                <label class="input">Employee Mail:</label>
                <br>
                <asp:TextBox
                    ID="EmployeeMail"
                    runat="server"
                    ClientIDMode="Static"
                    Style="width: 280px; height: 26px; padding: 5px; margin-right: 15px; font-size: 15px;">
                </asp:TextBox>
            </div>

            <div style="display: inline-block;">
                <asp:Button Style="background-color: #0078d4; padding: 5px; width: 100px; height: 36px; color: white; font-size: 15px; font-weight: bold; cursor: pointer;"
                    ID="LeaveRequest"
                    runat="server"
                    Text="Filter"
                    OnClientClick="javascript: return ValidateRequiredFields();"
                    OnClick="btnFilter_Click" />
            </div>

        </div>

        <div class="row">
        <div style="text-align: left; color: red; margin-left: 46px;">
            <p>
                <asp:Label ID="SearchError" runat="server" Text=""></asp:Label>
            </p>
        </div>
    </div>

    </fieldset>

        

    </asp:Panel>

    <div class="row">
        <div style="text-align: left; color: red; margin-left: 46px;">
            <p>
                <asp:Label ID="lblErrMsg" runat="server" Text=""></asp:Label>
            </p>
        </div>
    </div>
    
</body>

</html>





<script type="text/javascript">
    toastr.options = {
        "closeButton": true,
        "debug": false,
        "newestOnTop": false,
        "progressBar": false,
        "positionClass": "toast-top-center",
        "preventDuplicates": true,
        "onclick": null,
        "showDuration": "300",
        "hideDuration": "1000",
        "timeOut": "5000",
        "extendedTimeOut": "1000",
        "showEasing": "swing",
        "hideEasing": "linear",
        "showMethod": "fadeIn",
        "hideMethod": "fadeOut"
    }

    function ValidateRequiredFields() {

        var EmployeeMail = $("#EmployeeMail").val();
        //var EmployeeID = $("#EmployeeID").val();

        if (EmployeeMail == "" || EmployeeMail == null) {
            $("#EmployeeMail").focus();
            Command: toastr["error"]("Please type Employee Mail!!!");
            return false;
        }
        return true;
    }

</script>
