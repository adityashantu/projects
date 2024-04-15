using Microsoft.SharePoint;
using nDocumentApproval;
using PBL_FarmSolution.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace CPTUDocumentApprovalWorkflow.CONTROLTEMPLATES.DocumentApproval
{
    public partial class ucMyRequest : UserControl
    {
        string webUrl = SPContext.Current.Web.Url;
        private DataTable dtRequests = new DataTable();
        string loginName = SPContext.Current.Web.CurrentUser.LoginName;
       
        protected void Page_Load(object sender, EventArgs e)
        {
           
            GetMyRequest(loginName);
        }

        private void GetMyRequest(string loginName)
        {
            int loginID = SPContext.Current.Web.CurrentUser.ID;
            //string currentUserID = UserProfileHelper.GetPropertyValue(loginName, "WorkEmail");
            using (DocumentApprovalDataContext dataContext = new DocumentApprovalDataContext(webUrl))
            {
                List<DocumentApprovalModel> requestDataItems = dataContext.DocumentApprovalWorkflow.Where(x => x.RequesterId == loginID).OrderByDescending(x=>x.Id).ToList();
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

                        drRow["ViewLink"] = @"/sites/dms/_layouts/15/DocumentApproval/RequestView.aspx?ItemID=" + oItem.Id;
                        drRow["ReqLink"] = @"/sites/dms/_layouts/15/DocumentApproval/NewRequest.aspx?ItemID=" + oItem.Id;
                        dtRequests.Rows.Add(drRow);
                    }
                    gvMyrequests.DataSource = dtRequests;
                    gvMyrequests.DataBind();
                }
                else {
                    //msgLabel.Text = "No data available in table...";
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
            dtRequests.Columns.Add("ViewLink", typeof(string));
            dtRequests.Columns.Add("ReqLink", typeof(string));
        }

       
    }
}
