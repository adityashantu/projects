using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace LeaveManagementCPTU.CONTROLTEMPLATES.LeaveManagementCPTU
{
    public partial class ucIndex : UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }
        protected void MyRequests_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/leavemanagementsystem/_layouts/15/LeaveManagementCPTU/MyRequests.aspx");
        }
        protected void NewRequest_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/leavemanagementsystem/_layouts/15/LeaveManagementCPTU/NewRequest.aspx");
        }
        protected void ManagerHome_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/leavemanagementsystem/_layouts/15/LeaveManagementCPTU/ManagerMain.aspx");
        }
        protected void AdminPanel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/leavemanagementsystem/_layouts/15/LeaveManagementCPTU/AdminPanel.aspx");
        }
    }
}
