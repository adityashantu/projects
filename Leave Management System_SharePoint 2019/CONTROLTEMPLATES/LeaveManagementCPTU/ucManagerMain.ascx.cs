
using Microsoft.SharePoint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using Microsoft.Office.Server.UserProfiles;
using System.ComponentModel;

namespace LeaveManagementCPTU.CONTROLTEMPLATES.LeaveManagementCPTU
{
    public partial class ucManagerMain : UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string currentUserEmail = SPContext.Current.Web.CurrentUser.Email;
            using (LeaveManagementCPTUDataContext objDataContext = new LeaveManagementCPTUDataContext(SPContext.Current.Web.Url))
            {
                List<LMLeavesRequests> PendingRequestList = new List<LMLeavesRequests>();
                PendingRequestList = objDataContext.LMLeaveRequest.Where(x => x.ApproverMail.Equals(currentUserEmail) && x.LeaveStatus.Equals("Pending")).OrderByDescending(x => x.Id).ToList();


                

                ////-- Add datasource for Repeater------------
                if (PendingRequestList.Count > 0)
                {
                    rptdatatable.DataSource = PendingRequestList;
                    rptdatatable.DataBind();
                    objDataContext.Dispose();
                }
                else
                {
                    rptdatatable.DataSource = null;
                    rptdatatable.DataBind();
                    msgLabel.Text = "No Pending Requests...";
                }
            }
        }

        protected void btnFilter_Click(object sender, EventArgs e)
        {
            //Copy-Paste from page-load
            string currentUserEmail = SPContext.Current.Web.CurrentUser.Email;

            using (LeaveManagementCPTUDataContext objDataContext1 = new LeaveManagementCPTUDataContext(SPContext.Current.Web.Url))
            {
                //List<LMLeavesRequests> FilterRequestList = new List<LMLeavesRequests>();
                //FilterRequestList = objDataContext1.LMLeaveRequest.Where(x => x.ApproverMail.Equals(currentUserEmail) && x.LeaveStatus.Equals("Pending")).OrderByDescending(x => x.Id).ToList();

                ////-- Add datasource for Repeater------------
                /*if (PendingRequestList.Count > 0)
                {
                    rptdatatable.DataSource = PendingRequestList;
                    rptdatatable.DataBind();

                    objDataContext1.Dispose();
                }
                else
                {
                    rptdatatable.DataSource = null;
                    rptdatatable.DataBind();
                    msgLabel.Text = "No data available in table...";
                }*/
            }
            //Copy-paste ends here. 




            string month = FilterMonth.SelectedValue;
            string status = FilterStatus.SelectedValue;
            string mail = EmployeeMail.Text.Trim();
            string currentYear = DateTime.Now.Year.ToString();

            ////With ID Search
            /*int id = 0;
            try
            {
                id = Convert.ToInt32(EmployeeID.Text.Trim());
            }
            catch
            {
                
            }

            using (LeaveManagementCPTUDataContext objDataContext = new LeaveManagementCPTUDataContext(SPContext.Current.Web.Url))
            {
                List<LMLeavesRequests> myFilterList = new List<LMLeavesRequests>();

                if (EmployeeMail.Text.Trim() == "" && EmployeeID.Text.Trim() == "")
                {
                    msgEmpty_Filter.Text = "Please insert Emplpoyee Mail or Employee ID...";
                }
                else if (status == "ALL" && month != "ALL")
                {
                    if (mail == "")
                    {
                        myFilterList = objDataContext.LMLeaveRequest.Where(x => x.ApproverMail.Equals(currentUserEmail) && x.RequesterID==(id) && (x.DateRequested.ToString().Split('/')[0].Equals(month))).OrderByDescending(x => x.Id).ToList();
                    }
                    else
                    {
                        myFilterList = objDataContext.LMLeaveRequest.Where(x => x.ApproverMail.Equals(currentUserEmail) && x.Title.Equals(mail) && (x.DateRequested.ToString().Split('/')[0].Equals(month))).OrderByDescending(x => x.Id).ToList();
                    }
                }
                else if (status != "ALL" && month == "ALL")
                {
                    if (mail == "")
                    {
                        myFilterList = objDataContext.LMLeaveRequest.Where(x => x.ApproverMail.Equals(currentUserEmail) && x.RequesterID == (id) && x.LeaveStatus.Equals(status)).OrderByDescending(x => x.Id).ToList();
                    }
                    else
                    {
                        myFilterList = objDataContext.LMLeaveRequest.Where(x => x.ApproverMail.Equals(currentUserEmail) && x.Title.Equals(mail) && x.LeaveStatus.Equals(status)).OrderByDescending(x => x.Id).ToList();
                    }
                }
                else if (status == "ALL" && month == "ALL")
                {
                    if (EmployeeMail.Text.Trim() == "")
                    {
                        myFilterList = objDataContext.LMLeaveRequest.Where(x => x.ApproverMail.Equals(currentUserEmail) && x.RequesterID == (id)).OrderByDescending(x => x.Id).ToList();
                    }
                    else
                    {
                        myFilterList = objDataContext.LMLeaveRequest.Where(x => x.ApproverMail.Equals(currentUserEmail) && x.Title.Equals(mail)).OrderByDescending(x => x.Id).ToList();
                    }
                }
                else
                {
                    if (EmployeeMail.Text.Trim() == "")
                    {
                        myFilterList = objDataContext.LMLeaveRequest.Where(x => x.ApproverMail.Equals(currentUserEmail) && x.RequesterID == (id) && x.LeaveStatus.Equals(status) && (x.DateRequested.ToString().Split('/')[0].Equals(month))).OrderByDescending(x => x.Id).ToList();
                    }
                    else
                    {
                        myFilterList = objDataContext.LMLeaveRequest.Where(x => x.ApproverMail.Equals(currentUserEmail) && x.Title.Equals(mail) && x.LeaveStatus.Equals(status) && (x.DateRequested.ToString().Split('/')[0].Equals(month))).OrderByDescending(x => x.Id).ToList();
                    }
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
            }*/



            ////WithOut ID Serach
            using (LeaveManagementCPTUDataContext objDataContext = new LeaveManagementCPTUDataContext(SPContext.Current.Web.Url))
            {
                List<LMLeavesRequests> myFilterList = new List<LMLeavesRequests>();

                if (EmployeeMail.Text.Trim() == "")
                {
                    msgEmpty_Filter.Text = "Please insert Emplpoyee Mail";
                }
                else if (status == "ALL" && month != "ALL")
                {
                    myFilterList = objDataContext.LMLeaveRequest.Where(x => x.ApproverMail.Equals(currentUserEmail) && x.Title.Equals(mail) && (x.DateRequested.ToString().Split(' ')[0].Split('/')[2].Equals(currentYear)) && (x.DateRequested.ToString().Split('/')[1].Equals(month))).OrderByDescending(x => x.Id).ToList();
                }
                else if (status != "ALL" && month == "ALL")
                {
                    myFilterList = objDataContext.LMLeaveRequest.Where(x => x.ApproverMail.Equals(currentUserEmail) && x.Title.Equals(mail) && (x.DateRequested.ToString().Split(' ')[0].Split('/')[2].Equals(currentYear)) && x.LeaveStatus.Equals(status)).OrderByDescending(x => x.Id).ToList();

                }
                else if (status == "ALL" && month == "ALL")
                {
                    myFilterList = objDataContext.LMLeaveRequest.Where(x => x.ApproverMail.Equals(currentUserEmail) && x.Title.Equals(mail) && (x.DateRequested.ToString().Split(' ')[0].Split('/')[2].Equals(currentYear))).OrderByDescending(x => x.Id).ToList();

                }
                else
                {
                    myFilterList = objDataContext.LMLeaveRequest.Where(x => x.ApproverMail.Equals(currentUserEmail) && x.Title.Equals(mail) && (x.DateRequested.ToString().Split(' ')[0].Split('/')[2].Equals(currentYear)) && x.LeaveStatus.Equals(status) && (x.DateRequested.ToString().Split('/')[1].Equals(month))).OrderByDescending(x => x.Id).ToList();
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
