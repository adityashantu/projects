using Microsoft.SharePoint;
using nDocumentApproval;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace CPTUDocumentApprovalWorkflow.CONTROLTEMPLATES.DocumentApproval
{
    public partial class ucAllRequests : UserControl
    {
        string webUrl = SPContext.Current.Web.Url;
        private DataTable dtAllRequests = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            GetAllRequest();
        }

        private void GetAllRequest()
        {
            using (DocumentApprovalDataContext dataContext = new DocumentApprovalDataContext(webUrl))
            {
                List<DocumentApprovalModel> requestDataItems = dataContext.DocumentApprovalWorkflow.OrderByDescending(x => x.Id).ToList();
                if (requestDataItems.Count > 0)
                {
                    CreateDataTable();
                    DataRow drRow;
                    foreach (DocumentApprovalModel oItem in requestDataItems)
                    {
                        drRow = dtAllRequests.NewRow();
                        drRow["ID"] = oItem.Id.ToString();
                        drRow["ReqNo"] = oItem.ReqNo;
                        drRow["RequestDate"] = oItem.RequestDate;
                        drRow["RequesterName"] = oItem.RequesterName;
                        drRow["RequestStatus"] = oItem.RequestStatus;
                        drRow["Title"] = oItem.Comment;

                        drRow["ReqLink"] = @"/sites/dms/_layouts/15/DocumentApproval/NewRequest.aspx?ItemID=" + oItem.Id;
                        dtAllRequests.Rows.Add(drRow);
                    }
                    gvAllReqItems.DataSource = dtAllRequests;
                    gvAllReqItems.DataBind();
                }
                else
                {
                    msgLabel.Text = "No data available in table...";
                }
            }
        }
        private void CreateDataTable()
        {
            dtAllRequests.Columns.Add("ID", typeof(Int32));
            dtAllRequests.Columns.Add("ReqNo", typeof(string));
            dtAllRequests.Columns.Add("RequestDate", typeof(string));
            dtAllRequests.Columns.Add("RequesterName", typeof(string));
            dtAllRequests.Columns.Add("RequestStatus", typeof(string));
            dtAllRequests.Columns.Add("Title", typeof(string));
            dtAllRequests.Columns.Add("ReqLink", typeof(string));
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            using (DocumentApprovalDataContext dataContext = new DocumentApprovalDataContext(webUrl))
            {
                List<DocumentApprovalModel> resultItems = new List<DocumentApprovalModel>();
                resultItems = dataContext.DocumentApprovalWorkflow.ToList();
                
                DateTime fromDate = DateTime.MinValue;
                DateTime toDate = DateTime.MinValue;

                if (dtpFromDate.Text != "") {
                     fromDate = Convert.ToDateTime(dtpFromDate.Text.Trim());
                }
                if (dtpToDate.Text != "") {
                     toDate = Convert.ToDateTime(dtpToDate.Text.Trim());
                }
               
                double reportStatus = Convert.ToDouble(ddlStatus.SelectedValue);
                if (resultItems.Count > 0)
                {
                    if (fromDate != DateTime.MinValue && toDate != DateTime.MinValue) {
                        resultItems = resultItems.Where(x => Convert.ToDateTime(x.RequestDate).Date >= Convert.ToDateTime(fromDate).Date && Convert.ToDateTime(x.RequestDate).Date <= Convert.ToDateTime(toDate).Date).ToList();
                    }
                    
                    if (!string.IsNullOrEmpty(txtRequestNumber.Text.Trim()))
                    {
                        resultItems = resultItems.Where(x => x.ReqNo == txtRequestNumber.Text.Trim()).ToList();
                    }
                    if (reportStatus != 0)
                    {
                        if (reportStatus > 0 && reportStatus < 100) {
                            resultItems = resultItems.Where(x => x.RequestStep > 0 && x.RequestStep < 100).ToList();
                        }
                        else if (reportStatus == 100) {
                            resultItems = resultItems.Where(x => x.RequestStep == 100).ToList();
                        }
                        else if (reportStatus == 101) {
                            resultItems = resultItems.Where(x => x.RequestStep == 101).ToList();
                        }                       
                    }

                    dtAllRequests.Columns.Add("ID", typeof(Int32));
                    dtAllRequests.Columns.Add("ReqNo", typeof(string));
                    dtAllRequests.Columns.Add("RequestDate", typeof(string));
                    dtAllRequests.Columns.Add("RequesterName", typeof(string));
                    dtAllRequests.Columns.Add("RequestStatus", typeof(string));
                    dtAllRequests.Columns.Add("Title", typeof(string));
                    dtAllRequests.Columns.Add("ReqLink", typeof(string));
                    DataRow drRow;
                    foreach (DocumentApprovalModel oItem in resultItems)
                    {
                        drRow = dtAllRequests.NewRow();
                        drRow["ID"] = oItem.Id.ToString();
                        drRow["ReqNo"] = oItem.ReqNo;
                        drRow["RequestDate"] = oItem.RequestDate;
                        drRow["RequesterName"] = oItem.RequesterName;
                        drRow["RequestStatus"] = oItem.RequestStatus;
                        drRow["Title"] = oItem.Comment;

                        drRow["ReqLink"] = @"/_layouts/15/DocumentApproval/NewRequest.aspx?ItemID=" + oItem.Id;
                        dtAllRequests.Rows.Add(drRow);
                    }
                    gvAllReqItems.DataSource = dtAllRequests;
                    gvAllReqItems.DataBind();

                }
                else
                {
                    gvAllReqItems.DataSource = null;
                    gvAllReqItems.DataBind();
                    msgLabel.Text = "No Data Found";
                }
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            txtRequestNumber.Text = "";
            ddlStatus.SelectedValue = null;
            dtpFromDate.Text = null;
            dtpToDate.Text = null;
        }
    }
}
