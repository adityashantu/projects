<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucMyRequests.ascx.cs" Inherits="LeaveManagementCPTU.CONTROLTEMPLATES.LeaveManagementCPTU.ucMyRequests" Debug="true" %>




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

    <div>
        <div style="display: inline-block;">
            <label class="input">Name:</label>
            <br>
            <asp:TextBox
                ID="EmployeeName"
                runat="server"
                ClientIDMode="Static"
                ReadOnly="true"
                Style="width: 320px; height: 30px; padding: 5px; margin-right: 15px; font-size: 14px; background-color: #dcdddd">

            </asp:TextBox>
        </div>

        <div style="display: inline-block;">
            <label class="input">Email:</label>
            <br>
            <asp:TextBox
                ID="EmployeeMail"
                runat="server"
                ClientIDMode="Static"
                ReadOnly="true"
                Style="width: 300px; height: 30px; padding: 5px; margin-right: 15px; font-size: 14px; background-color: #dcdddd">

            </asp:TextBox>
        </div>

        <div style="display: inline-block;">
            <asp:Button Style="background-color: #0078d4; padding: 5px; width: 150px; height: 45px; color: white; font-size: 15px; font-weight: bold; cursor: pointer;"
                ID="LeaveRequest"
                runat="server"
                Text="Leave Request"
                OnClick="btnLeaveRequest_Click" />

        </div>

    </div>









    <div>
        <fieldset class="PendingReuqests">
            <legend style="font-size: 25px; font-weight: bold;">Pending Request</legend>



            <table id="TABLE_MY_REQUEST" class="auto-style2">

                <thead>
                    <tr>
                        <th class="auto-style1">Date Requested</th>
                        <th class="auto-style1">Leave Type</th>
                        <th class="auto-style1">Leave From</th>
                        <th class="auto-style1">Leave To</th>
                        <th class="auto-style1">Day Difference</th>
                        <th class="auto-style1">Reason</th>
                        <th class="auto-style1">Manager</th>
                        <th class="auto-style1">Status</th>
                        <th class="auto-style1"></th>
                    </tr>
                </thead>
                <tbody>

                    <asp:Repeater ID="rptdatatable" runat="server">
                        <ItemTemplate>
                            <tr style="text-align: center">

                                <td>
                                    <%# DataBinder.Eval(Container.DataItem, "DateRequested", "{0:dd/MM/yyyy}") %>
                                </td>
                                <td>
                                    <%# DataBinder.Eval(Container.DataItem, "LeaveType") %>  
                                </td>
                                <td>
                                    <%# DataBinder.Eval(Container.DataItem, "DateFrom", "{0:dd/MM/yyyy}") %>
                                </td>
                                <td>
                                    <%# DataBinder.Eval(Container.DataItem, "DateTo", "{0:dd/MM/yyyy}") %> 
                                </td>
                                <td>
                                    <%# DataBinder.Eval(Container.DataItem, "DayDifference") %>  
                                </td>
                                <td>
                                    <%# DataBinder.Eval(Container.DataItem, "Reason") %>  
                                </td>
                                <td>
                                    <%# DataBinder.Eval(Container.DataItem, "Approver") %>  
                                </td>
                                <td>
                                    <%# DataBinder.Eval(Container.DataItem, "LeaveStatus") %>
                                </td>
                                <td>
                                    <asp:Button Style="background-color: #0078d4; color: white; font-weight: bold; cursor: pointer;" ID="btnDelete" runat="server" Text="Delete" OnClick="btnDelete_Click" CommandArgument='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "Id")) %>' />
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>

                </tbody>
            </table>
            <div>
                <span class="text-center">
                    <asp:Label ID="msgLabel" runat="server" Text="" Style="color: darkred; font-size: 20px; font: bold;"></asp:Label>

                </span>
            </div>

        </fieldset>
    </div>
    <div>
        <fieldset>
            <legend style="font-size: 25px; font-weight: bold;">Filter</legend>

            <div>
                <div style="display: inline-block;">
                    <label class="input">Status:</label>
                    <br>
                    <asp:DropDownList
                        ID="FilterStatus"
                        runat="server"
                        ClientIDMode="Static">
                        <asp:ListItem Value="Select" Text="Select"></asp:ListItem>
                        <asp:ListItem Value="Approved" Text="Approved"></asp:ListItem>
                        <asp:ListItem Value="Unapproved" Text="Unapproved"></asp:ListItem>
                    </asp:DropDownList>
                </div>

                <div style="display: inline-block;">
                    <label class="input">Month:</label>
                    <br>
                    <asp:DropDownList
                        ID="FilterMonth"
                        runat="server"
                        ClientIDMode="Static">
                        <asp:ListItem Value="Select" Text="Select"></asp:ListItem>
                        <asp:ListItem Value="01" Text="January"></asp:ListItem>
                        <asp:ListItem Value="02" Text="February"></asp:ListItem>
                        <asp:ListItem Value="03" Text="March"></asp:ListItem>
                        <asp:ListItem Value="04" Text="April"></asp:ListItem>
                        <asp:ListItem Value="05" Text="May"></asp:ListItem>
                        <asp:ListItem Value="06" Text="June"></asp:ListItem>
                        <asp:ListItem Value="07" Text="July"></asp:ListItem>
                        <asp:ListItem Value="08" Text="August"></asp:ListItem>
                        <asp:ListItem Value="09" Text="September"></asp:ListItem>
                        <asp:ListItem Value="10" Text="October"></asp:ListItem>
                        <asp:ListItem Value="11" Text="November"></asp:ListItem>
                        <asp:ListItem Value="12" Text="December"></asp:ListItem>

                    </asp:DropDownList>
                </div>

                <div style="display: inline-block;">
                    <asp:Button Style="background-color: #0078d4; padding: 5px; width: 150px; height: 45px; color: white; font-size: 15px; font-weight: bold; cursor: pointer;"
                        ID="Filter"
                        runat="server"
                        Text="Filter"
                        OnClientClick="javascript: return ValidateRequiredFields();"
                        OnClick="btnLeaveFilter_Click" />

                </div>
            </div>

            <div>


                <table id="Filtered_Table" class="auto-style2" style="margin-top:15px"> 

                    <thead>
                        <tr>
                            <th class="auto-style1">Leave Type</th>
                            <th class="auto-style1">Leave From</th>
                            <th class="auto-style1">Leave To</th>
                            <th class="auto-style1">Day Difference</th>
                            <th class="auto-style1">Reason</th>
                            <th class="auto-style1">Status</th>
                            <th class="auto-style1">Manager Comment</th>
                        </tr>
                    </thead>
                    <tbody>

                        <asp:Repeater ID="FilteredTable" runat="server">
                            <ItemTemplate>
                                <tr style="text-align: center">
                                    <td>
                                        <%# DataBinder.Eval(Container.DataItem, "LeaveType") %>  
                                    </td>
                                    <td>
                                        <%# DataBinder.Eval(Container.DataItem, "DateFrom", "{0:dd/MM/yyyy}") %>
                                    </td>
                                    <td>
                                        <%# DataBinder.Eval(Container.DataItem, "DateTo", "{0:dd/MM/yyyy}") %>
                                    </td>
                                    <td>
                                        <%# DataBinder.Eval(Container.DataItem, "DayDifference") %>  
                                    </td>
                                    <td>
                                        <%# DataBinder.Eval(Container.DataItem, "Reason") %>  
                                    </td>
                                    <td>
                                        <%# DataBinder.Eval(Container.DataItem, "LeaveStatus") %>
                                    </td>
                                    <td>
                                        <%# DataBinder.Eval(Container.DataItem, "ManagerComment") %>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>

                    </tbody>
                </table>

                <div >
                    <span class="text-center">
                        <asp:Label ID="Filter_errMsg" runat="server" Text="" Style="color: darkred; font-size: 20px; font: bold;"></asp:Label>

                    </span>
                </div>
            </div>
        </fieldset>
    </div>
    <div class="row">
        <div style="text-align: left; color: red; margin-left: 46px;">
            <p>
                <asp:Label ID="lblErrMsg" runat="server" Text=""></asp:Label>
            </p>
        </div>
    </div>



</body>

</html>



<style>
    select {
        width: 262px;
        height: 45px;
        padding: 5px;
        margin-right: 15px;
        font-size: 15px;
    }

    .auto-style2 {
        width: auto;
    }

    table, th, td {
        border: 1px solid black;
        border-collapse: collapse;
        padding-top: 10px;
    }

    th, td {
        padding: 15px;
    }
</style>





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

        var FilterStatus = $("#FilterStatus").val();
        if (FilterStatus == "Select") {
            $("#FilterStatus").focus();
            Command: toastr["error"]("Please Select Status!")
            return false;
        }

        var FilterMonth = $("#FilterMonth").val();
        if (FilterMonth == "Select") {
            $("#FilterMonth").focus();
            Command: toastr["error"]("Please Select Month!")
            return false;
        }

        return true;
    }

</script>
