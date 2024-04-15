using Microsoft.SharePoint;
using nDocumentApproval;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Linq;
using System.Data;

namespace CPTUDocumentApprovalWorkflow.CONTROLTEMPLATES.DocumentApproval
{
    public partial class ucRequestView : UserControl
    {
        string webUrl = SPContext.Current.Web.Url;
        protected void Page_Load(object sender, EventArgs e)
        {
            Int32 reqID = Convert.ToInt32(Request.QueryString["ItemID"]);
            ViewState["ItemID"] = reqID;
            if (!IsPostBack)
            {                               
                if (reqID > 0)
                {
                    GetRequestById(reqID);
                }               
            }
        }

        private void GetRequestById(int reqID)
        {          
            int currentUserID = SPContext.Current.Web.CurrentUser.ID;

            using (DocumentApprovalDataContext dataContext = new DocumentApprovalDataContext(webUrl))
            {
                DocumentApprovalModel request = dataContext.DocumentApprovalWorkflow.Where(x => x.Id == reqID).SingleOrDefault();
                txtRequestNo.Text = request.ReqNo;
                txtRequestDate.Text = request.RequestDate.ToString();
                txtRequesterName.Text = request.RequesterName;
                txtRequesterEmail.Text = request.RequesterEmail;
                txtRequesterDepartment.Text = request.RequesterDepartment;
                txtRequesterDesignation.Text = request.RequesterDesignation;
                txtComment.Text = request.Comment;

                LoadAttachmentByReqId(request.ReqNo);
                LoadApprovalHistoryByReqId(request.ReqNo);
                //// Load Reviewser in Grid
                //var listReviwers = dataContext.DAReviewer.Where(x => x.Title == request.ReqNo).ToList();

                //CreateDataTable();
                //DataRow drRow;
                //foreach (DAReviewerModel item in listReviwers)
                //{
                //    drRow = dtReviewer.NewRow();
                //    drRow["ID"] = item.Id.ToString();
                //    drRow["Name"] = item.ReviewerImnName;

                //    dtReviewer.Rows.Add(drRow);
                //}

                //gvReviewerList.DataSource = dtReviewer;
                //gvReviewerList.DataBind();

                ////Load Approver in PP
                //SPUser approver = SPContext.Current.Web.EnsureUser(request.ApproverImnName);
                //PickerEntity objPickerEntity = new PickerEntity();
                //objPickerEntity.Key = approver.LoginName;
                //approverPeoplePicker.Entities.Add(approverPeoplePicker.ValidateEntity(objPickerEntity));


                //ViewState["RequestID"] = request.Id;
                //ViewState["RequestNo"] = request.ReqNo;
                //ViewState["ReqStep"] = request.RequestStep;


                //if (request.RequestStep > 0)
                //{
                //    btnSubmit.Visible = false;
                //    txtComment.Enabled = false;
                //    //pnlApprovalHistory.Visible = true;
                //    pblUploadAttachment.Visible = false;
                //    gvAttachments.Columns[2].Visible = false;
                //    gvReviewerList.Columns[2].Visible = false;
                //    reviewerPeoplePicker.Visible = false;
                //    btnAddReviewer.Visible = false;
                //    approverPeoplePicker.Enabled = false;
                //}
                //if (request.RequestStep > 0 && request.RequestStep <= 20)
                //{
                //    var reviewer = dataContext.DAReviewer.Where(x => x.Title == request.ReqNo && x.Level == request.RequestStep).SingleOrDefault();
                //    if (reviewer != null)
                //    {
                //        if (reviewer.ReviewerId == currentUserID)
                //        {
                //            pnlReviewer.Visible = true;
                //        }
                //    }
                //}

                //if (request.RequestStep == 50)
                //{
                //    if (request.PendingUserId == currentUserID)
                //    {
                //        pnlApprover.Visible = true;
                //    }
                //}
            }
        }
        private void LoadAttachmentByReqId(string requestNo)
        {
            using (SPSite oSite = new SPSite(SPContext.Current.Site.Url))
            {
                using (SPWeb oWeb = oSite.OpenWeb())
                {
                    // custom list for item store and attachment.
                    SPList docLib = oWeb.Lists["DAAttachments"];
                    DataTable dt = new DataTable();
                    dt.Columns.Add("ID", typeof(int));
                    dt.Columns.Add("FileName", typeof(string));
                    dt.Columns.Add("FileUrl", typeof(string));
                    DataRow dataRow;
                    foreach (SPListItem item in docLib.Items)
                    {
                        dataRow = dt.NewRow();
                        int id = Convert.ToInt32(item["ID"]);
                        //string refNo = item["ReqNo"].ToString();
                        if (item["ReqNo"].ToString() == requestNo)
                        {
                            SPFile file = item.File;
                            string file_Url = file.LinkingUrl;
                            string file_Name = file.Name;                          
                            dataRow["ID"] = id;
                            dataRow["FileName"] = file_Name;
                            dataRow["FileUrl"] = file_Url;
                            dt.Rows.Add(dataRow);
                        }
                    }

                    gvAttachments.DataSource = dt;
                    gvAttachments.DataBind();
                }
            }
        }
        private void LoadApprovalHistoryByReqId(string reqNo)
        {
            using (DocumentApprovalDataContext dataContext = new DocumentApprovalDataContext(webUrl))
            {
                var approvalHistoryList = dataContext.DAApprovalHistory.Where(x => x.ReqNo == reqNo).ToList();

                if (approvalHistoryList.Count > 0)
                {
                    pblApprovalHistory.Visible = true;
                    DataTable dt = new DataTable();
                    dt.Columns.Add("ID", typeof(int));
                    dt.Columns.Add("ApprovalLevel", typeof(string));
                    dt.Columns.Add("Approval", typeof(string));
                    dt.Columns.Add("ApprovedBy", typeof(string));
                    dt.Columns.Add("ApprovalDate", typeof(string));
                    dt.Columns.Add("Comment", typeof(string));
                    DataRow dataRow;
                    foreach (DAApprovalHistoryModel item in approvalHistoryList)
                    {
                        dataRow = dt.NewRow();
                        dataRow["ID"] = item.Id;
                        dataRow["ApprovalLevel"] = item.ApprovalLevel;
                        dataRow["Approval"] = item.Approval;
                        dataRow["ApprovedBy"] = item.ApprovedBy;
                        dataRow["ApprovalDate"] = item.ApprovalDate;
                        dataRow["Comment"] = item.Comment;
                        dt.Rows.Add(dataRow);
                    }
                    gvApprovalHistory.DataSource = dt;
                    gvApprovalHistory.DataBind();
                }

            }
        }
    }
}
