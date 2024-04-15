using LeaveManagementCPTU.Helpers;
using LeaveManagementCPTU;
using Microsoft.SharePoint;
//-------
using Microsoft.SharePoint.Administration.DatabaseProvider;
using Microsoft.SharePoint.Utilities;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Linq;
using System.Collections.Generic;
//
using LeaveManagementCPTU.Service;
using LeaveManagementCPTU.Datas;

namespace LeaveManagementCPTU.CONTROLTEMPLATES.LeaveManagementCPTU
{
    public partial class ucNewRequest : UserControl
    {

        string webUrl = SPContext.Current.Web.Url;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {

                //--Get Current User's Information----
                LoadCurrentUserInfoFromSPUserProfileService(SPContext.Current.Web.CurrentUser.LoginName);


                string currentUserEmail = SPContext.Current.Web.CurrentUser.Email;
                using (LeaveManagementCPTUDataContext objDataContext = new LeaveManagementCPTUDataContext(SPContext.Current.Web.Url))
                {
                    List<LMEmployeeLeavesMains> myRequestList = new List<LMEmployeeLeavesMains>();
                    myRequestList = objDataContext.LMEmployeeLeavesMain.Where(x => x.Title.Equals(currentUserEmail)).ToList();


                    foreach (var item in myRequestList){
                        if (item.Title.ToString() == currentUserEmail) {
                            //EmployeeID.Text = item.EmployeeID.ToString();

                            MedicalTotalLeave.Text = item.MedicalTotalLeave.ToString();
                            MedicalLeaveTaken.Text = item.MedicalLeaveTaken.ToString();
                            MedicalLeaveRemaining.Text = item.MedicalLeaveRemaining.ToString();

                            CasualTotalLeave.Text = item.CasualTotalLeave.ToString();
                            CasualLeaveTaken.Text = item.CasualLeaveTaken.ToString();
                            CasualLeaveRemaining.Text = item.CasualLeaveRemaining.ToString();

                            OthersWithoutPay.Text = item.LeaveWithoutPay.ToString();
                            OthersSpecialLeave.Text = item.SpecialLeave.ToString();
                        }
                    }
                }
            }
        }

        

        private void LoadCurrentUserInfoFromSPUserProfileService(string loginName)
        {
            try
            {
                EmployeeName.Text = UserProfileHelper.GetPropertyValue(loginName, UserProfileConstants.FullName);
                EmployeeMail.Text = UserProfileHelper.GetPropertyValue(loginName, UserProfileConstants.WorkEmail);
                EmployeeDepartment.Text = UserProfileHelper.GetPropertyValue(loginName, UserProfileConstants.Department);
                EmployeeDesignation.Text = UserProfileHelper.GetPropertyValue(loginName, UserProfileConstants.Designation);
                //manager = UserProfileHelper.GetPropertyValue(loginName, UserProfileConstants.ManagerLoginName);
                //EmployeeManager.Text = UserProfileHelper.GetPropertyValue(loginName, UserProfileConstants.ManagerLoginName).Split('\\').Last();
                hiddenManagerMail.Text = UserProfileHelper.GetPropertyValue(loginName, UserProfileConstants.ManagerLoginName);
                EmployeeManager.Text = UserProfileHelper.GetPropertyValue(hiddenManagerMail.Text, UserProfileConstants.FullName);

                hiddenManagerMail.Text = UserProfileHelper.GetPropertyValue(loginName, UserProfileConstants.ManagerLoginName).Split('\\').Last();
                hiddenManagerMail.Text = UserProfileHelper.GetPropertyValue(hiddenManagerMail.Text, UserProfileConstants.WorkEmail);
                
            }
            catch (Exception ex)
            {
                lblErrMsg.Text = "Error From : " + ex.TargetSite + "==>" + ex.Message;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                using (LeaveManagementCPTUDataContext objDataContext = new LeaveManagementCPTUDataContext(webUrl))
                {
                    LMLeavesRequests objLeaveRequest = new LMLeavesRequests();

                    //user input to the list (From User inout Screen)
                    //objLeaveRequest.RequesterID = Convert.ToInt32(EmployeeID.Text.Trim());
                    objLeaveRequest.Title = EmployeeMail.Text.Trim();
                    objLeaveRequest.RequesterName = EmployeeName.Text.Trim();
                    objLeaveRequest.RequesterDepartment = EmployeeDepartment.Text.Trim();
                    objLeaveRequest.RequesterDesignation = EmployeeDesignation.Text.Trim();
                    objLeaveRequest.DateFrom = Convert.ToDateTime(txtFromDate.Text.Trim());
                    objLeaveRequest.DateTo = Convert.ToDateTime(txtToDate.Text.Trim());
                    objLeaveRequest.LeaveType = ddlLeaveType.SelectedValue.Trim();
                    objLeaveRequest.Reason = Reason.Text.Trim();
                    objLeaveRequest.Approver = EmployeeManager.Text.Trim();
                    objLeaveRequest.ApproverMail = hiddenManagerMail.Text.Trim();



                    //Day Difference Code
                    DateTime startDate = Convert.ToDateTime(txtFromDate.Text.Trim());
                    DateTime endDate = Convert.ToDateTime(txtToDate.Text.Trim());
                    if (endDate < startDate)
                    {
                        lblErrMsg.Text = "End date cannot be before start date.";
                        return;
                    }
                    int dayDifference = CalculateDayDifference(startDate, endDate);

                    hiddenManagerMail.Text = dayDifference.ToString();
                    objLeaveRequest.DayDifference = Convert.ToInt32(hiddenManagerMail.Text);

                    /////////////////




                    objDataContext.LMLeaveRequest.InsertOnSubmit(objLeaveRequest);
                    objDataContext.SubmitChanges();



                    #region Send Email Notification
                    //Fetching data for additional information
                    string fromDate = txtFromDate.Text;
                    string toDate = txtToDate.Text;
                    string comment = Reason.Text;
                    //
                    EmailServiceCPTU emailService = new EmailServiceCPTU();
                    EmailUserInfo emailUserInfo = new EmailUserInfo();
                    emailUserInfo.Name = EmployeeManager.Text.Trim();
                    //hiddenManagerMail.Text = UserProfileHelper.GetPropertyValue(EmployeeManager.Text, UserProfileConstants.WorkEmail);
                    emailUserInfo.Email = objLeaveRequest.ApproverMail; //hiddenManagerMail.Text.Trim();

                    string emailFormat = "NotifyForApproval";

                    emailService.SendEmail(emailFormat, emailUserInfo, fromDate, toDate, comment);
                    #endregion


                    objDataContext.Dispose();
                    Response.Redirect("~/leavemanagementsystem/_layouts/15/LeaveManagementCPTU/MyRequests.aspx");  //Response.Redirect("~/_layouts/15/LeaveManagementCPTU/MyRequests.aspx");


                }
            }
            catch (Exception ex)
            {
                lblErrMsg.Text = "Error From : " + ex.TargetSite + "==>" + ex.Message;
            }
        }

        private int CalculateDayDifference(DateTime startDate, DateTime endDate) 
        {
            int dayDifference = 0;
            for (DateTime date = startDate; date <= endDate; date = date.AddDays(1))
            {
                if (date.DayOfWeek != DayOfWeek.Friday && date.DayOfWeek != DayOfWeek.Saturday)
                {
                    dayDifference++;
                }
            }
            return dayDifference;
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/leavemanagementsystem/_layouts/15/LeaveManagementCPTU/MyRequests.aspx"); //Response.Redirect("~/_layouts/15/LeaveManagementCPTU/MyRequests.aspx");
        }


        }
}
