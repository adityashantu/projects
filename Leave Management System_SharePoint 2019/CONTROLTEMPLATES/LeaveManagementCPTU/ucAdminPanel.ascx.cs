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
    public partial class ucAdminPanel : UserControl
    {
        string webUrl = SPContext.Current.Web.Url;
        string currentUserEmail = SPContext.Current.Web.CurrentUser.Email;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                using (LeaveManagementCPTUDataContext objDataContext = new LeaveManagementCPTUDataContext(SPContext.Current.Web.Url))
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
                            lblErrMsg.Text = "You Can Not View This Data... You may not be a Admin... If you think this message is wrong, Please notify your SharePoint Admin... ";
                        }
                    }
                }
            }
        }

        protected void btnFilter_Click(object sender, EventArgs e)
        {
            using (LeaveManagementCPTUDataContext objDataContext = new LeaveManagementCPTUDataContext(SPContext.Current.Web.Url))
            {
                var Check = objDataContext.LMEmployeeLeavesMain.FirstOrDefault(x => x.Title.Equals(EmployeeMail.Text));
                if (Check == null)
                {
                    SearchError.Text = "We can not find this user. Please check the Employee Mail again. \nPlease visit the SharePoint Admin if you think you everything is right.";
                }
                else
                {
                    string adminLink = "~/leavemanagementsystem/_layouts/15/LeaveManagementCPTU/AdminReport.aspx?mail=";

                    Response.Redirect(adminLink + EmployeeMail.Text);
                }
            }
        }
    }

}
