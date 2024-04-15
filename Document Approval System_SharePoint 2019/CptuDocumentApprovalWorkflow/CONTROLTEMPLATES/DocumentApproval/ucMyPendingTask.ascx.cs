using Microsoft.SharePoint;
using nDocumentApproval;
using PBL_FarmSolution.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Linq;

namespace CPTUDocumentApprovalWorkflow.CONTROLTEMPLATES.DocumentApproval
{
    public partial class ucMyPendingTask : UserControl
    {
        string webUrl = SPContext.Current.Web.Url;
        private DataTable dtRequests = new DataTable();
       // string loginName = SPContext.Current.Web.CurrentUser.LoginName;
        long loginUserID = SPContext.Current.Web.CurrentUser.ID;
        protected void Page_Load(object sender, EventArgs e)
        {
            GetMyPendingTask(loginUserID);
        }

        private void GetMyPendingTask(long loginUserID)
        {
            //string currentUserEmail = UserProfileHelper.GetPropertyValue(loginName, "WorkEmail");
            using (DocumentApprovalDataContext dataContext = new DocumentApprovalDataContext(webUrl))
            {
                List<DocumentApprovalModel> requestDataItems = dataContext.DocumentApprovalWorkflow.Where(x => x.PendingUserId == loginUserID).OrderByDescending(x => x.Id).ToList();
                if (requestDataItems.Count > 0)
                {
                    CreateDataTable();
                    DataRow drRow;
                    foreach (DocumentApprovalModel oItem in requestDataItems)
                    {
                        drRow = dtRequests.NewRow();
                        drRow["ID"] = oItem.Id.ToString();
                        drRow["ReqNo"] = oItem.ReqNo;
                        drRow["RequestDate"] = oItem.RequestDate;
                        drRow["RequesterName"] = oItem.RequesterName;
                        drRow["RequestStatus"] = oItem.RequestStatus;
                        drRow["Title"] = oItem.Comment;

                        drRow["ReqLink"] = @"/sites/dms/_layouts/15/DocumentApproval/NewRequest.aspx?ItemID=" + oItem.Id;
                        dtRequests.Rows.Add(drRow);
                    }
                    gvMyPendingTask.DataSource = dtRequests;
                    gvMyPendingTask.DataBind();
                }
                else
                {
                    msgLabel.Text = "No data available in table...";
                }
            }
        }

        private void CreateDataTable()
        {
            dtRequests.Columns.Add("ID", typeof(Int32));
            dtRequests.Columns.Add("ReqNo", typeof(string));
            dtRequests.Columns.Add("RequestDate", typeof(string));
            dtRequests.Columns.Add("RequesterName", typeof(string));
            dtRequests.Columns.Add("RequestStatus", typeof(string));
            dtRequests.Columns.Add("Title", typeof(string));
            dtRequests.Columns.Add("ReqLink", typeof(string));
        }
    }
}
