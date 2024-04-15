using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using LeaveManagementCPTU.Helpers;
using System.Linq;
using Microsoft.SharePoint;
using System.Text.RegularExpressions;
using LeaveManagementCPTU.Service;
using LeaveManagementCPTU.Datas;

namespace LeaveManagementCPTU.CONTROLTEMPLATES.LeaveManagementCPTU
{
    public partial class ucManagerApproval : UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!this.Page.IsPostBack)
                {
                    if (Page.Request.QueryString.Count > 0)
                    {
                        Int32 requestID = Convert.ToInt32(Page.Request.QueryString["Id"]);
                        ViewState["requestID"] = requestID;


                        using (LeaveManagementCPTUDataContext objDataContext = new LeaveManagementCPTUDataContext(SPContext.Current.Web.Url))
                        {
                            if (ManagerCheck.Visible == false)
                            {
                                var Check = objDataContext.LMLeaveRequest.Where(x => x.Id == requestID).FirstOrDefault();
                                if(Check.LeaveStatus.Equals("Approved") || Check.LeaveStatus.Equals("Approved"))
                                {
                                    lblErrMsg.Text = "You Can Not View This Data...";
                                }
                                else if (Check.Approver.Equals(UserProfileHelper.GetPropertyValue((SPContext.Current.Web.CurrentUser.LoginName), UserProfileConstants.FullName).Trim()))
                                {
                                    ManagerCheck.Visible = true;
                                }
                                else
                                {
                                    lblErrMsg.Text = "You Can Not View This Data...";
                                }
                            }



                            var LeaveRequestDetails = objDataContext.LMLeaveRequest.Where(x => x.Id == requestID).FirstOrDefault();
                            if (LeaveRequestDetails != null)
                            {
                                EmployeeName.Text = LeaveRequestDetails.RequesterName;
                                EmployeeMail.Text = LeaveRequestDetails.Title;
                                //EmployeeID.Text = LeaveRequestDetails.RequesterID.ToString();
                                EmployeeManager.Text = LeaveRequestDetails.Approver;
                                EmployeeDepartment.Text = LeaveRequestDetails.RequesterDepartment; //UserProfileHelper.GetPropertyValue(EmployeeName.Text, UserProfileConstants.Department);
                                EmployeeDesignation.Text = LeaveRequestDetails.RequesterDesignation; //UserProfileHelper.GetPropertyValue(EmployeeName.Text, UserProfileConstants.Designation);
                                txtFromDate.Text = LeaveRequestDetails.DateFrom.ToString().Split(' ')[0];
                                txtToDate.Text = LeaveRequestDetails.DateTo.ToString().Split(' ')[0];
                                DayDifference.Text = LeaveRequestDetails.DayDifference.ToString();
                                ddlLeaveType.Text = LeaveRequestDetails.LeaveType.ToString();
                                Reason.Text = LeaveRequestDetails.Reason;

                                /*Match match = Regex.Match(LeaveRequestDetails.Reason.ToString(), "<div[^>]*>(.*?)</div>");

                                if (match.Success)
                                {
                                    Reason.Text = match.Groups[1].Value.ToString();
                                }*/


                                var LeaveDetails = objDataContext.LMEmployeeLeavesMain.Where(x => x.Title == LeaveRequestDetails.Title).FirstOrDefault();
                                CasualTotalLeave.Text = LeaveDetails.CasualTotalLeave.ToString();
                                CasualLeaveTaken.Text = LeaveDetails.CasualLeaveTaken.ToString();
                                CasualLeaveRemaining.Text = LeaveDetails.CasualLeaveRemaining.ToString();

                                MedicalTotalLeave.Text = LeaveDetails.MedicalTotalLeave.ToString();
                                MedicalLeaveTaken.Text = LeaveDetails.MedicalLeaveTaken.ToString();
                                MedicalLeaveRemaining.Text = LeaveDetails.MedicalLeaveRemaining.ToString();

                                OthersWithoutPay.Text = LeaveDetails.LeaveWithoutPay.ToString();
                                OthersSpecialLeave.Text = LeaveDetails.SpecialLeave.ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrMsg.Text = "Error From : " + ex.TargetSite + "==>" + ex.Message;
            }
        }
        protected void btnApprove_Click(object sender, EventArgs e)
        {
            try
            {
                using (LeaveManagementCPTUDataContext objDataContext = new LeaveManagementCPTUDataContext(SPContext.Current.Web.Url))
                {
                    Int32 requestID = Convert.ToInt32(Page.Request.QueryString["Id"]);
                    ViewState["requestID"] = requestID;

                    LMLeavesRequests LeaveAppprove = objDataContext.LMLeaveRequest.Where(x => x.Id == requestID).FirstOrDefault();
                    LMEmployeeLeavesMains EmployeeData = objDataContext.LMEmployeeLeavesMain.Where(x => x.Title == LeaveAppprove.Title).FirstOrDefault();

                    LeaveAppprove.LeaveStatus = "Approved";
                    LeaveAppprove.ManagerComment = ManagerComment.Text.Trim();


                    if (ddlLeaveType.Text.Equals("Casual"))
                    {
                        HiddenValue.Text = (EmployeeData.CasualLeaveTaken + LeaveAppprove.DayDifference).ToString();
                        EmployeeData.CasualLeaveTaken = Convert.ToInt32(HiddenValue.Text);
                        HiddenValue.Text = (EmployeeData.CasualLeaveRemaining - LeaveAppprove.DayDifference).ToString();
                        EmployeeData.CasualLeaveRemaining = Convert.ToInt32(HiddenValue.Text);
                    }
                    else if (ddlLeaveType.Text.Equals("Medical"))
                    {
                        HiddenValue.Text = (EmployeeData.MedicalLeaveTaken + LeaveAppprove.DayDifference).ToString();
                        EmployeeData.MedicalLeaveTaken = Convert.ToInt32(HiddenValue.Text);
                        HiddenValue.Text = (EmployeeData.MedicalLeaveRemaining - LeaveAppprove.DayDifference).ToString();
                        EmployeeData.MedicalLeaveRemaining = Convert.ToInt32(HiddenValue.Text);
                    }
                    else if (ddlLeaveType.Text.Equals("Special Leave"))
                    {
                        HiddenValue.Text = (EmployeeData.SpecialLeave + LeaveAppprove.DayDifference).ToString();
                        EmployeeData.SpecialLeave = Convert.ToInt32(HiddenValue.Text);
                    }
                    else if (ddlLeaveType.Text.Equals("Leave Without Pay"))
                    {
                        HiddenValue.Text = (EmployeeData.LeaveWithoutPay + LeaveAppprove.DayDifference).ToString();
                        EmployeeData.LeaveWithoutPay = Convert.ToInt32(HiddenValue.Text);
                    }


                    objDataContext.SubmitChanges();



                    #region Send Email Notification
                    //Fetching data for additional information
                    string fromDate = txtFromDate.Text;
                    string toDate = txtToDate.Text;
                    string comment = ManagerComment.Text;
                    //
                    EmailServiceCPTU emailService = new EmailServiceCPTU();
                    EmailUserInfo emailUserInfo = new EmailUserInfo();
                    emailUserInfo.Name = EmployeeName.Text.Trim(); //EmployeeManager.Text.Trim();
                    //HiddenValue.Text = UserProfileHelper.GetPropertyValue(EmployeeManager.Text, UserProfileConstants.WorkEmail);
                    emailUserInfo.Email = EmployeeMail.Text.Trim();//HiddenValue.Text.Trim();

                    string emailFormat = "Approve";

                    emailService.SendEmail(emailFormat, emailUserInfo, fromDate, toDate, comment);
                    #endregion




                    objDataContext.Dispose();

                    Response.Redirect("~/leavemanagementsystem/_layouts/15/LeaveManagementCPTU/ManagerMain.aspx"); //Response.Redirect("~/_layouts/15/LeaveManagementCPTU/ManagerMain.aspx");
                }
            }
            catch (Exception ex)
            {
                lblErrMsg.Text = "Error From : " + ex.TargetSite + "==>" + ex.Message;
            }
        }

        protected void btnUnApprove_Click(object sender, EventArgs e)
        {
            try
            {
                using (LeaveManagementCPTUDataContext objDataContext = new LeaveManagementCPTUDataContext(SPContext.Current.Web.Url))
                {
                    Int32 requestID = Convert.ToInt32(Page.Request.QueryString["Id"]);
                    ViewState["requestID"] = requestID;

                    LMLeavesRequests LeaveAppprove = objDataContext.LMLeaveRequest.Where(x => x.Id == requestID).FirstOrDefault();

                    LeaveAppprove.LeaveStatus = "Unapproved";
                    LeaveAppprove.ManagerComment = ManagerComment.Text.Trim();


                    objDataContext.SubmitChanges();



                    #region Send Email Notification
                    //Fetching data for additional information
                    string fromDate = txtFromDate.Text;
                    string toDate = txtToDate.Text;
                    string comment = ManagerComment.Text;
                    //
                    EmailServiceCPTU emailService = new EmailServiceCPTU();
                    EmailUserInfo emailUserInfo = new EmailUserInfo();
                    emailUserInfo.Name = EmployeeName.Text.Trim();//EmployeeManager.Text.Trim();
                    //HiddenValue.Text = UserProfileHelper.GetPropertyValue(EmployeeManager.Text, UserProfileConstants.WorkEmail);
                    emailUserInfo.Email = EmployeeMail.Text.Trim();//HiddenValue.Text.Trim();

                    string emailFormat = "Unapprove";

                    emailService.SendEmail(emailFormat, emailUserInfo, fromDate, toDate, comment);
                    #endregion



                    objDataContext.Dispose();

                    Response.Redirect("~/leavemanagementsystem/_layouts/15/LeaveManagementCPTU/ManagerMain.aspx");  //Response.Redirect("~/_layouts/15/LeaveManagementCPTU/ManagerMain.aspx");
                }
            }
            catch (Exception ex)
            {
                lblErrMsg.Text = "Error From : " + ex.TargetSite + "==>" + ex.Message;
            }
        }

        /*protected void btnBack_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("~/leavemanagementsystem/_layouts/15/LeaveManagementCPTU/ManagerMain.aspx");  //Response.Redirect("~/_layouts/15/LeaveManagementCPTU/ManagerMain.aspx");
            }
            catch (Exception ex)
            {
                lblErrMsg.Text = "Error From : " + ex.TargetSite + "==>" + ex.Message;
            }
        }*/
    }
}
