<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucAdminReport.ascx.cs" Inherits="LeaveManagementCPTU.CONTROLTEMPLATES.LeaveManagementCPTU.ucAdminReport" %>



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

        <div style="float:left;">
            <label class="input"><b>Employee Name:</b></label>
            <br>
            <asp:TextBox ID="EmployeeName" runat="server" ClientIDMode="Static"
                ReadOnly="true" Style="background-color: #dcdddd; height:30px; width: 400px; margin-right:10px;">
            </asp:TextBox>
        </div>

        <div class="">
            <label class="input"><b>Employee Email:</b></label>
            <br>
            <asp:TextBox class="" ID="EmployeeMailStatic" runat="server" ClientIDMode="Static"
                ReadOnly="true" Style="background-color: #dcdddd; height:30px; width: 300px; margin-right:10px;">
            </asp:TextBox>
        </div>



        <fieldset>
            <legend style="font-size: 20px; font-weight: bold;">Employee Leaves</legend>
            <div style="float:left; margin-right: 10px;">
                <h3 style='font-weight: bold; font-size: 18px;'><strong>Casual Leave</strong></h3>
                <table>
                    <tr>
                        <td>
                            <label for="TotalCasualLeave"><b>Total Leave:</b></label>
                        </td>
                        <td>
                            <asp:TextBox class="LeaveHistoryBoxes" ID="CasualTotalLeave" runat="server"
                                ClientIDMode="Static" ReadOnly="true" Style="background-color: white; border:hidden; font-size:large; width:25px;">
                            </asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label for="TotalCasualLeave"><b>Leave Taken:</b></label>
                        </td>
                        <td>
                            <asp:TextBox class="LeaveHistoryBoxes" ID="CasualLeaveTaken" runat="server"
                                ClientIDMode="Static" ReadOnly="true" Style="background-color: white; border:hidden; font-size:large; width:25px;">
                            </asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label for="TotalCasualLeave"><b>Leave Remaining:</b></label>
                        </td>
                        <td class="CasualLeaveRemaining">
                            <asp:TextBox class="LeaveHistoryBoxes" ID="CasualLeaveRemaining" runat="server"
                                ClientIDMode="Static" ReadOnly="true" Style="background-color: white; border:hidden; font-size:large; width:25px;">
                            </asp:TextBox>
                        </td>
                    </tr>
                </table>
            </div>

            <div style="float:left; margin-right: 10px;">
                <h3 style='font-weight: bold; font-size: 18px;'><b>Medical Leave</b></h3>
                <table>
                    <tr>
                        <td>
                            <label for="TotalMedicalLeave"><b>Total Leave:</b></label>
                        </td>
                        <td>
                            <asp:TextBox class="LeaveHistoryBoxes" ID="MedicalTotalLeave" runat="server"
                                ClientIDMode="Static" ReadOnly="true" Style="background-color: white; border:hidden; font-size:large; width:25px;">
                            </asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label for="TotalMedicalLeave"><b>Leave Taken:</b></label>
                        </td>
                        <td>
                            <asp:TextBox class="LeaveHistoryBoxes" ID="MedicalLeaveTaken" runat="server"
                                ClientIDMode="Static" ReadOnly="true" Style="background-color: white; border:hidden; font-size:large; width:25px;">
                            </asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>

                            <label for="TotalMedicalLeave"><b>Leave Remaining:</b></label>
                        </td>
                        <td>
                            <asp:TextBox class="LeaveHistoryBoxes" ID="MedicalLeaveRemaining" runat="server"
                                ClientIDMode="Static" ReadOnly="true" Style="background-color: white; border:hidden; font-size:large; width:25px;">
                            </asp:TextBox>
                        </td>
                    </tr>
                </table>
            </div>

            <div class="sixOthers">
                <h3 style='font-weight: bold; font-size: 18px;'>Others</h3>
                <table>
                    <tr>
                        <td>
                            <label for="OthersWithoutPay"><b>Without Pay:</b></label>
                        </td>
                        <td>
                            <asp:TextBox class="LeaveHistoryBoxes" ID="OthersWithoutPay" runat="server"
                                ClientIDMode="Static" ReadOnly="true" Style="background-color: white; border:hidden; font-size:large; width:25px;">
                            </asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label for="OthersSpecialLeave"><b>Special Leave:</b></label>
                        </td>
                        <td>
                            <asp:TextBox class="LeaveHistoryBoxes" ID="OthersSpecialLeave" runat="server"
                                ClientIDMode="Static" ReadOnly="true" Style="background-color: white; border:hidden; font-size:large; width:25px;">
                            </asp:TextBox>
                        </td>
                    </tr>
                </table>
            </div>
        </fieldset>

        <fieldset class="EmployeeReport">
            <legend style="font-size: 20px; font-weight: bold;">Filter</legend>
            <div>

                <div style="display: inline-block;">
                    <label class="input">Status:</label>
                    <br>
                    <asp:DropDownList
                        ID="FilterStatus"
                        runat="server"
                        ClientIDMode="Static"
                        Style="width: 150px; height: 38px; padding: 5px; margin-right: 15px; font-size: 15px;">
                        <asp:ListItem Value="Select" Text="Select"></asp:ListItem>
                        <asp:ListItem Value="Pending" Text="Pending"></asp:ListItem>
                        <asp:ListItem Value="Approved" Text="Approved"></asp:ListItem>
                        <asp:ListItem Value="Unapproved" Text="Unapproved"></asp:ListItem>
                        <asp:ListItem Value="ALL" Text="ALL"></asp:ListItem>
                    </asp:DropDownList>
                </div>

                <div style="display: inline-block;">
                    <label class="input">Month:</label>
                    <br>
                    <asp:DropDownList
                        ID="FilterMonth"
                        runat="server"
                        ClientIDMode="Static"
                        Style="width: 150px; height: 38px; padding: 5px; margin-right: 15px; font-size: 15px;">
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
                        <asp:ListItem Value="ALL" Text="ALL"></asp:ListItem>

                    </asp:DropDownList>
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

            <div>

                <div style="padding-top: 10px;">
                    <table id="Table_Filter" class="auto-style2">
                        <thead>
                            <tr>
                                <th class="auto-style1">Name</th>
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
                            <asp:Repeater ID="filter_repeater" runat="server">
                                <ItemTemplate>
                                    <tr style="text-align: center">
                                        <td>
                                            <%# DataBinder.Eval(Container.DataItem, "RequesterName") %>  
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
                </div>

                <div style="">
                    <span class="text-center">
                        <asp:Label ID="msgEmpty_Filter" runat="server" Text="" Style="color: darkred; font-size: 20px; font: bold;"></asp:Label></span>
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




<style>
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