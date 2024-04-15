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
    public partial class ucAdminReport : UserControl
    {
        string webUrl = SPContext.Current.Web.Url;
        string currentUserEmail = SPContext.Current.Web.CurrentUser.Email;

        string EmployeeMainSearched = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.Request.QueryString.Count > 0)
            {
                string EmpMail = Page.Request.QueryString["mail"];
                ViewState["mail"] = EmpMail;

                EmployeeMainSearched = EmpMail;

                using (LeaveManagementCPTUDataContext objDataContext = new LeaveManagementCPTUDataContext(SPContext.Current.Web.Url))
                {
                    if (AdminCheck.Visible == false)
                    {
                        if (AdminCheck.Visible == false)
                        {
                            var Check = objDataContext.LMAdmin.FirstOrDefault(x => x.Title.Equals(currentUserEmail));
                            if (Check != null)
                            {
                                AdminCheck.Visible = true;
                            }
                            else
                            {
                                lblErrMsg.Text = "You Can Not View This Data... You may no be a Admin... If you think this message is wrong, Please notify your SharePoint Admin... ";
                            }
                        }
                    }

                    var adminReportDetails = objDataContext.LMEmployeeLeavesMain.Where(x => x.Title == EmpMail).FirstOrDefault();
                    EmployeeName.Text = adminReportDetails.EmployeeName;
                    EmployeeMailStatic.Text = adminReportDetails.Title;

                    CasualTotalLeave.Text = adminReportDetails.CasualTotalLeave.ToString();
                    CasualLeaveTaken.Text = adminReportDetails.CasualLeaveTaken.ToString();
                    CasualLeaveRemaining.Text = adminReportDetails.CasualLeaveRemaining.ToString();

                    MedicalTotalLeave.Text = adminReportDetails.MedicalTotalLeave.ToString();
                    MedicalLeaveTaken.Text = adminReportDetails.MedicalLeaveTaken.ToString();
                    MedicalLeaveRemaining.Text = adminReportDetails.MedicalLeaveRemaining.ToString();

                    OthersSpecialLeave.Text = adminReportDetails.SpecialLeave.ToString();
                    OthersWithoutPay.Text = adminReportDetails.LeaveWithoutPay.ToString();
                }
            }
        }

        protected void btnFilter_Click(object sender, EventArgs e)
        {
            string currentUserEmail = SPContext.Current.Web.CurrentUser.Email;


            string month = FilterMonth.SelectedValue;
            string status = FilterStatus.SelectedValue;
            string mail = EmployeeMainSearched;
            string currentYear = DateTime.Now.Year.ToString();


            using (LeaveManagementCPTUDataContext objDataContext = new LeaveManagementCPTUDataContext(SPContext.Current.Web.Url))
            {
                List<LMLeavesRequests> myFilterList = new List<LMLeavesRequests>();

                if (status == "ALL" && month != "ALL")
                {
                    myFilterList = objDataContext.LMLeaveRequest.Where(x => x.Title.Equals(mail) && (x.DateRequested.ToString().Split(' ')[0].Split('/')[2].Equals(currentYear)) && (x.DateRequested.ToString().Split('/')[1].Equals(month))).OrderByDescending(x => x.Id).ToList();
                }
                else if (status != "ALL" && month == "ALL")
                {
                    myFilterList = objDataContext.LMLeaveRequest.Where(x => x.Title.Equals(mail) && (x.DateRequested.ToString().Split(' ')[0].Split('/')[2].Equals(currentYear)) && x.LeaveStatus.Equals(status)).OrderByDescending(x => x.Id).ToList();

                }
                else if (status == "ALL" && month == "ALL")
                {
                    myFilterList = objDataContext.LMLeaveRequest.Where(x => x.Title.Equals(mail) && (x.DateRequested.ToString().Split(' ')[0].Split('/')[2].Equals(currentYear))).OrderByDescending(x => x.Id).ToList();

                }
                else
                {
                    myFilterList = objDataContext.LMLeaveRequest.Where(x => x.Title.Equals(mail) && (x.DateRequested.ToString().Split(' ')[0].Split('/')[2].Equals(currentYear)) && x.LeaveStatus.Equals(status) && (x.DateRequested.ToString().Split('/')[1].Equals(month))).OrderByDescending(x => x.Id).ToList();
                }

                if (myFilterList.Count > 0)
                {
                    msgEmpty_Filter.Text = null;

                    filter_repeater.DataSource = myFilterList;
                    filter_repeater.DataBind();

                    objDataContext.Dispose();
                }
                else if (myFilterList.Count == 0)
                {
                    filter_repeater.DataSource = null;
                    filter_repeater.DataBind();
                    msgEmpty_Filter.Text = "No data available in table...";

                    msgEmpty_Filter.Dispose();
                }
            }
        }
    }
}
