using LeaveManagementCPTU.Helpers;
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using System.Linq;
using Microsoft.SharePoint.Client;

namespace LeaveManagementCPTU.CONTROLTEMPLATES.LeaveManagementCPTU
{
    public partial class ucMyRequests : UserControl
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
                    List<LMLeavesRequests> myRequestList = new List<LMLeavesRequests>();
                    myRequestList = objDataContext.LMLeaveRequest.Where(x => x.Title.Equals(currentUserEmail) && x.LeaveStatus.Equals("Pending")).OrderByDescending(x => x.Id).ToList();

                    ////-- Add datasource for Repeater------------
                    if (myRequestList.Count > 0)
                    {
                        rptdatatable.DataSource = myRequestList;
                        rptdatatable.DataBind();

                        objDataContext.Dispose();
                    }
                    else
                    {
                        rptdatatable.DataSource = null;
                        rptdatatable.DataBind();
                        msgLabel.Text = "No data available in table...";
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

            }
            catch (Exception ex)
            {
                lblErrMsg.Text = "Error From : " + ex.TargetSite + "==>" + ex.Message;
            }
        }

        protected void btnLeaveFilter_Click(object sender, EventArgs e)
        {
            string month = FilterMonth.SelectedValue;
            string status = FilterStatus.SelectedValue;

            string currentUserEmail = SPContext.Current.Web.CurrentUser.Email;
            using (LeaveManagementCPTUDataContext objDataContext = new LeaveManagementCPTUDataContext(SPContext.Current.Web.Url))
            {
                List<LMLeavesRequests> myRequestList = new List<LMLeavesRequests>();
                string currentYear = DateTime.Now.Year.ToString();
                myRequestList = objDataContext.LMLeaveRequest.Where(x => x.Title.Equals(currentUserEmail) && x.LeaveStatus.Equals(status) && (x.DateRequested.ToString().Split(' ')[0].Split('/')[2].Equals(currentYear)) && (x.DateRequested.ToString().Split('/')[1].Equals(month))).OrderByDescending(x => x.Id).ToList();




                ////-- Add datasource for Repeater------------
                if (myRequestList.Count > 0)
                {
                    Filter_errMsg.Text = null;

                    FilteredTable.DataSource = myRequestList;
                    FilteredTable.DataBind();

                    objDataContext.Dispose();
                }
                else
                {
                    FilteredTable.DataSource = null;
                    FilteredTable.DataBind();
                    Filter_errMsg.Text = "No data available in table...";
                }
            }
        }

        protected void btnLeaveRequest_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/leavemanagementsystem/_layouts/15/LeaveManagementCPTU/NewRequest.aspx");  //Response.Redirect("~/_layouts/15/LeaveManagementCPTU/NewRequest.aspx");
        }

        /*protected string RenderActionButton(string status, int itemId)
        {
            // Check if the status is "Pending"
            if (status == "Pending")
            {
                return $"<asp:LinkButton Text='Delete' OnClick='btnDelete_Click' CommandArgument='{itemId}' runat='server' />";
                // Return the HTML markup for the button
                //return $"<asp:Button Text='Delete' OnClick='btnDelete_Click' CommandArgument='{itemId}' runat='server' />";
                //return $"<button OnClick='btnDelete_Click' CommandArgument='{itemId}'>Delete</button>";
                //return "<button OnClick='btnDelete_Click'>Delete</button>"; ;
            }
            else
            {
                // Return an empty string for other statuses
                return "";
            }
        }*/

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            // Get the button that was clicked
            Button btnDelete = (Button)sender;

            // Get the ID passed as a parameter from the button's CommandArgument
            int itemIdToDelete;
            
            if (int.TryParse(btnDelete.CommandArgument, out itemIdToDelete))
            {
                // Call a method to delete the item from SharePoint list
                DeleteItemFromList(itemIdToDelete);
                
            }
        }

        private void DeleteItemFromList(int itemIdToDelete)
        {
            using (LeaveManagementCPTUDataContext objDataContext = new LeaveManagementCPTUDataContext(SPContext.Current.Web.Url))
            {
                List<LMLeavesRequests> myList = new List<LMLeavesRequests>();
                //myList.RemoveAll(item => item.Id == itemIdToDelete);
                LMLeavesRequests itemToDelete = objDataContext.LMLeaveRequest.FirstOrDefault(item => item.Id == itemIdToDelete);
                if (itemToDelete != null)
                {
                    // Mark the item for deletion
                    objDataContext.LMLeaveRequest.DeleteOnSubmit(itemToDelete);

                    // Submit changes to the SharePoint list
                    objDataContext.SubmitChanges();

                    Response.Redirect("~/leavemanagementsystem/_layouts/15/LeaveManagementCPTU/MyRequests.aspx");  //Response.Redirect("~/_layouts/15/LeaveManagementCPTU/MyRequests.aspx");
                }
                else
                {
                    Console.WriteLine("Item not found.");
                }
                //objDataContext.LMLeaveRequest.InsertOnSubmit(myList);

                //objDataContext.SubmitChanges();
            }
        }

    }
}



