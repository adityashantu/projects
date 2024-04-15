<%@ Assembly Name="LeaveManagementCPTU, Version=1.0.0.0, Culture=neutral, PublicKeyToken=eda72657dcf68580" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucManagerApproval.ascx.cs" Inherits="LeaveManagementCPTU.CONTROLTEMPLATES.LeaveManagementCPTU.ucManagerApproval" %>

<link href="../../../_layouts/15/LeaveManagementCPTU/Design/CSS/toastr.min.css" rel="stylesheet" />
<script src="../../../_layouts/15/LeaveManagementCPTU/Design/SCRIPT/toastr.min.js"></script>

<!DOCTYPE html>
<html lang="en">

<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Document</title>
</head>

<body>
    <asp:Panel ID="ManagerCheck" runat="server" Visible="false">
        <div class="first">
            <div class="sec">
                <fieldset>
                    <legend>Leave Entry</legend>
                    <div class="thirdLeft">
                        <label class="input"><b>Name:</b></label>
                        <br>
                        <asp:TextBox class="LeaveEntryBoxesBig" ID="EmployeeName" runat="server" ClientIDMode="Static"
                            ReadOnly="true" Style="background-color: #dcdddd">
                        </asp:TextBox>
                    </div>
                    <div class="third">
                        <label class="input"><b>Email:</b></label>
                        <br>
                        <asp:TextBox class="LeaveEntryBoxes" ID="EmployeeMail" runat="server" ClientIDMode="Static"
                            ReadOnly="true" Style="background-color: #dcdddd">
                        </asp:TextBox>
                    </div>
                    <div class="thirdLeft">
                        <label class="input"><b>Designation:</b></label>
                        <br>
                        <asp:TextBox class="LeaveEntryBoxesBig" ID="EmployeeDesignation" runat="server"
                            ClientIDMode="Static" ReadOnly="true" Style="background-color: #dcdddd">
                        </asp:TextBox>
                    </div>
                    <div class="third">
                        <label class="input"><b>Department:</b></label>
                        <br>
                        <asp:TextBox class="LeaveEntryBoxes" ID="EmployeeDepartment" runat="server" ClientIDMode="Static"
                            ReadOnly="true" Style="background-color: #dcdddd">
                        </asp:TextBox>
                    </div>
                    <div class="thirdLeft">
                        <label class="input"><b>Line Manager:</b></label>
                        <br>
                        <asp:TextBox class="LeaveEntryBoxesBig" ID="EmployeeManager" runat="server" ClientIDMode="Static"
                            ReadOnly="true" Style="background-color: #dcdddd">
                        </asp:TextBox>
                    </div>
                    <div class="thirdFromTo">
                        <label class="input"><b>From: (DD/MM/YYYY)</b></label>
                        <br>
                        <asp:TextBox class="LeaveEntryDates" ID="txtFromDate" ClientIDMode="Static" runat="server"
                            ReadOnly="true" Style="background-color: #dcdddd">
                        </asp:TextBox>
                    </div>
                    <div class="thirdFromTo">
                        <label class="input"><b>To: (DD/MM/YYYY)</b></label>
                        <br>
                        <asp:TextBox class="LeaveEntryDates" ID="txtToDate" ClientIDMode="Static" runat="server"
                            ReadOnly="true" Style="background-color: #dcdddd">
                        </asp:TextBox>
                    </div>
                    <div class="thirdFromTo">
                        <label class="input"><b>Day Difference:</b></label>
                        <br>
                        <asp:TextBox class="LeaveEntryDates" ID="DayDifference" runat="server" ClientIDMode="Static"
                            ReadOnly="true" Style="background-color: #dcdddd">
                        </asp:TextBox>
                    </div>
                    <div class="thirdFromTo">
                        <label class="input"><b>Leave Type:</b></label>
                        <br>
                        <asp:TextBox class="LeaveEntryDates" ID="ddlLeaveType" runat="server" ClientIDMode="Static"
                            ReadOnly="true" Style="background-color: #dcdddd">
                        </asp:TextBox>
                    </div>
                    <div>
                        <div class="five">
                            <label class="input"><b>Reason:</b></label>
                            <br>
                            <asp:TextBox ID="Reason" ClientIDMode="Static" CssClass="form-control" TextMode="MultiLine"
                                runat="server" ReadOnly="true" Style="background-color: #dcdddd">
                            </asp:TextBox>
                        </div>
                    </div>

                </fieldset>
            </div>

            <div class="for">
                <fieldset>
                    <legend>Leave History</legend>
                    <div class="six">
                        <h3 style='font-weight: bold; font-size: 18px;'><strong>Casual Leave</strong></h3>
                        <table>
                            <tr>
                                <td>
                                    <label for="TotalCasualLeave"><b>Total Leave:</b></label>
                                </td>
                                <td>
                                    <asp:TextBox class="LeaveHistoryBoxes" ID="CasualTotalLeave" runat="server"
                                        ClientIDMode="Static" ReadOnly="true" Style="background-color: #dcdddd">
                                    </asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label for="TotalCasualLeave"><b>Leave Taken:</b></label>
                                </td>
                                <td>
                                    <asp:TextBox class="LeaveHistoryBoxes" ID="CasualLeaveTaken" runat="server"
                                        ClientIDMode="Static" ReadOnly="true" Style="background-color: #dcdddd">
                                    </asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label for="TotalCasualLeave"><b>Leave Remaining:</b></label>
                                </td>
                                <td>
                                    <asp:TextBox class="LeaveHistoryBoxes" ID="CasualLeaveRemaining" runat="server"
                                        ClientIDMode="Static" ReadOnly="true" Style="background-color: #dcdddd">
                                    </asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </div>

                    <div class="six">
                        <h3 style='font-weight: bold; font-size: 18px;'><b>Medical Leave</b></h3>
                        <table>
                            <tr>
                                <td>
                                    <label for="TotalMedicalLeave"><b>Total Leave:</b></label>
                                </td>
                                <td>
                                    <asp:TextBox class="LeaveHistoryBoxes" ID="MedicalTotalLeave" runat="server"
                                        ClientIDMode="Static" ReadOnly="true" Style="background-color: #dcdddd">
                                    </asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label for="TotalMedicalLeave"><b>Leave Taken:</b></label>
                                </td>
                                <td>
                                    <asp:TextBox class="LeaveHistoryBoxes" ID="MedicalLeaveTaken" runat="server"
                                        ClientIDMode="Static" ReadOnly="true" Style="background-color: #dcdddd">
                                    </asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>

                                    <label for="TotalMedicalLeave"><b>Leave Remaining:</b></label>
                                </td>
                                <td>
                                    <asp:TextBox class="LeaveHistoryBoxes" ID="MedicalLeaveRemaining" runat="server"
                                        ClientIDMode="Static" ReadOnly="true" Style="background-color: #dcdddd">
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
                                        ClientIDMode="Static" ReadOnly="true" Style="background-color: #dcdddd">
                                    </asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label for="OthersSpecialLeave"><b>Special Leave:</b></label>
                                </td>
                                <td>
                                    <asp:TextBox class="LeaveHistoryBoxes" ID="OthersSpecialLeave" runat="server"
                                        ClientIDMode="Static" ReadOnly="true" Style="background-color: #dcdddd">
                                    </asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </div>
                </fieldset>

                <div>
                    <div class="five">
                        <asp:TextBox
                            ID="ManagerComment"
                            runat="server"
                            class="LeaveEntryBoxes"
                            ClientIDMode="Static"
                            CssClass="form-control"
                            TextMode="MultiLine"
                            placeholder="Comment" />
                    </div>
                </div>

                <div>
                    <div class="fiveButton">
                        <div style="display: inline-block;">
                            <asp:Button Style="background-color: #0078d4; height: 50px; width: 170px; clear: both; color: white; font-weight: bold; font-size: 15px; text-align: center; vertical-align: middle; cursor: pointer;"
                                ID="btnApprove"
                                runat="server"
                                Text="Approve"
                                OnClientClick="javascript: return validateAndSubmit();"
                                OnClick="btnApprove_Click" />
                        </div>
                        <div style="display: inline-block;">
                            <asp:Button Style="background-color: #0078d4; height: 50px; width: 170px; clear: both; color: white; font-weight: bold; font-size: 15px; text-align: center; vertical-align: middle; cursor: pointer;"
                                ID="btnUnApprove"
                                runat="server"
                                Text="Reject"
                                OnClientClick="javascript: return ValidateRequiredFields();"
                                OnClick="btnUnApprove_Click" />
                        </div>
                    </div>
                </div>

            </div>
        </div>

        <div class="row">
            <div style="text-align: left; color: red; margin-left: 46px;">
                <p>
                    <asp:Label ID="lblErrMsg" runat="server" Text=""></asp:Label>
                </p>
            </div>
        </div>


        <asp:Panel ID="HiddenPanel" runat="server" Visible="false">
            <asp:Label ID="HiddenValue" runat="server" Text=""></asp:Label>
        </asp:Panel>
    </asp:Panel>
</body>

</html>








<style>
    .first {
        display: flex;
        height: 95%;
        margin: 10px;
        padding: 10px;
    }

    .sec {
        height: 100%;
        width: 55%;
        margin: 20px;
    }

    .third {
        width: 40%;
        height: 55px;
        margin: 5px;
        float: left;
        padding: 0;
    }

    .for {
        height: 100%;
        width: 40%;
        margin-top: 20px;
        margin-bottom: 20px;
        margin-right: 20px;
    }

    .five {
        width: 97%;
        height: 80px;
        padding: 0;
        float: left;
        margin: 5px;
    }
    .fiveButton{
        width: 97%;
        height: 40px;
        padding: 0;
        float: left;
        margin: 5px;
    }

    .six {
        width: 45%;
        height: 120px;
        margin: 5px;
        float: left;
        padding: 0;
    }
    .sixOthers {
        width: 45%;
        height: 100px;
        margin: 5px;
        margin-top: 25px;
        float: left;
        padding: 0;
    }

    .thirdLeft {
        width: 55%;
        height: 55px;
        margin: 5px;
        float: left;
        padding: 0;
    }

    .thirdFromTo {
        width: 48%;
        height: 55px;
        margin: 5px;
        float: left;
        padding: 0;
    }

    .LeaveEntryBoxesBig {
        width: 95%;
        Height: 32px;
    }

    .LeaveEntryBoxes {
        width: 100%;
        Height: 32px;
    }

    .LeaveEntryDates {
        width: 95%;
        height: 32px;
    }
    .inputDate{
        width: 98%;
        height: 32px;
    }
   select{
        width: 97%;
        height: 36px;
    }
   textarea {
        width: 98%;
        height: 60px;
    }
   legend {
        font-size: 25px;
        font-weight: bold;
    }
   .LeaveHistoryBoxes {
        width: 50px;
        height: 22px;
        padding: 5px;
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
        var ManagerComment = $("#ManagerComment").val();
        if ((ManagerComment == "") || (ManagerComment == null)) {
            $("#ManagerComment").focus();
            Command: toastr["error"]("Please Type a comment!")
            return false;
        }
    }

    function validateAndSubmit() {
        var leaveType = $("#ddlLeaveType").val();
        var dayDifference = parseInt($("#DayDifference").val());
        if (leaveType == "Casual") {
            var casualValue = parseInt($("#CasualLeaveRemaining").val());
            if (dayDifference > casualValue) {
                toastr["error"]("This Employee Does not have enough Casual Leave...");
                $("#CasualLeaveRemaining").focus();
                return false;
            }
        }
        if (leaveType == "Medical") {
            var medicalValue = parseInt($("#MedicalLeaveRemaining").val());
            if (dayDifference > medicalValue) {
                toastr["error"]("This Employee Does not have enough Medical Leave...");
                $("#CasualLeaveRemaining").focus();
                return false;
            }
        }
        return true;
    }


</script>
