using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using Microsoft.Office.Server;
using Microsoft.Office.Server.UserProfiles;
using nDocumentApproval;
using PBL_FarmSolution.Helper;
using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using CPTUDocumentApprovalWorkflow.Models;
using System.Collections.Generic;
using CPTUDocumentApprovalWorkflow.Service;
using Microsoft.SharePoint.Administration;

namespace CPTUDocumentApprovalWorkflow.CONTROLTEMPLATES.DocumentApproval
{
    public partial class ucNewRequest : UserControl
    {
        string webUrl = SPContext.Current.Web.Url;
        string loginName = SPContext.Current.Web.CurrentUser.LoginName;
        EmailService emailService = new EmailService();
        EmailUserInfo emailUserInfo = new EmailUserInfo();
       
        private DataTable dtReviewer = new DataTable();

        string emailFormat = string.Empty;
        string requestComment = string.Empty;
        string comment = string.Empty;
        string requestNo = string.Empty;

        
        
        protected void Page_Load(object sender, EventArgs e)
        {
            Int32 reqID = Convert.ToInt32(Request.QueryString["ItemID"]);
            ViewState["ItemID"] = reqID;
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(loginName))
                {
                    GetCurrrentUserInfo(loginName);

                    if (reqID > 0)
                    {
                        GetRequestById(reqID);
                    }
                    
                    if (reqID < 1)
                    {
                        string requestNo = txtRequestNo.Text;
                        SetDefaultReviewer(requestNo);
                    }
                }
            }
        }     
        private void GetCurrrentUserInfo(string loginName)
        {
            txtRequestNo.Text = "DA-" + DateTime.Now.ToString("yyyyMMdd-hhmmss");
            txtRequestDate.Text = DateTime.Now.ToString("dd/MM/yyyy hh:mm tt");
            txtRequesterName.Text = UserProfileHelper.GetPropertyValue(loginName, "PreferredName");
            txtRequesterEmail.Text = UserProfileHelper.GetPropertyValue(loginName, "WorkEmail");
            txtRequesterDepartment.Text = UserProfileHelper.GetPropertyValue(loginName, "Department");
            txtRequesterDesignation.Text = UserProfileHelper.GetPropertyValue(loginName, "Title");
          
        }

        private void SetDefaultReviewer(string requestNo)
        {
           
            DAReviewerModel dAReviewerModel = new DAReviewerModel();
            using (DocumentApprovalDataContext dataContext = new DocumentApprovalDataContext(webUrl))
            {
                try
                {
                    var defaultReviewerList = dataContext.DefaultReviewer.ToList();
                    foreach (var user in defaultReviewerList)
                    {
                        dAReviewerModel = new DAReviewerModel();
                        dAReviewerModel.Title = requestNo;
                        dAReviewerModel.ReviewerId = user.NameId;
                        dAReviewerModel.Email = user.Email;
                        dAReviewerModel.Level = 0;
                        dataContext.DAReviewer.InsertOnSubmit(dAReviewerModel);
                        dataContext.SubmitChanges();                     
                    }

                    var listReviwers = dataContext.DAReviewer.Where(x => x.Title == requestNo).ToList();
                    CreateDataTable();
                    DataRow drRow;
                    foreach (DAReviewerModel item in listReviwers)
                    {
                        drRow = dtReviewer.NewRow();
                        drRow["ID"] = item.Id.ToString();
                        drRow["Name"] = item.ReviewerImnName;

                        dtReviewer.Rows.Add(drRow);
                    }

                    gvReviewerList.DataSource = dtReviewer;
                    gvReviewerList.DataBind();                                              
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                
            }
        }
        protected void btnSubmit_Click1(object sender, EventArgs e) { 
            int itemId = Convert.ToInt32(ViewState["ItemID"]);
            string requestNo = txtRequestNo.Text;
            string currentUserName = UserProfileHelper.GetPropertyValue(loginName, "PreferredName");
            int currentUserID = SPContext.Current.Web.CurrentUser.ID;
            DAReviewerModel obReviewer = new DAReviewerModel();
            DAApprovalHistoryModel approvalHistory = new DAApprovalHistoryModel();
            DocumentApprovalModel documentApprovalModel = new DocumentApprovalModel();
            using (DocumentApprovalDataContext dataContext = new DocumentApprovalDataContext(webUrl))
            {
                if (ValidateSubmitRequest())
                {
                    if (itemId > 0)
                    {
                        documentApprovalModel = dataContext.DocumentApprovalWorkflow.Where(x => x.ReqNo == requestNo).SingleOrDefault();
                    }

                    documentApprovalModel.ReqNo = txtRequestNo.Text;
                    documentApprovalModel.RequestDate = DateTime.Now;
                    documentApprovalModel.RequesterName = txtRequesterName.Text;
                    documentApprovalModel.RequesterEmail = txtRequesterEmail.Text;
                    documentApprovalModel.RequesterId = currentUserID;
                    documentApprovalModel.RequesterDepartment = txtRequesterDepartment.Text;
                    documentApprovalModel.RequesterDesignation = txtRequesterDesignation.Text;
                    documentApprovalModel.Comment = txtComment.Text;

                    if (approverPeoplePicker.Entities.Count > 0)
                    {
                        PickerEntity approverPicker = (PickerEntity)approverPeoplePicker.Entities[0];
                        string appvrLoginName = approverPicker.Description;
                        documentApprovalModel.ApproverEmail = UserProfileHelper.GetPropertyValue(appvrLoginName, "WorkEmail");
                        documentApprovalModel.ApproverId = SPContext.Current.Web.EnsureUser(((PickerEntity)approverPeoplePicker.Entities[0]).Description).ID;
                    }

                    // documentApprovalModel.ApproverEmail = SPContext.Current.Web.EnsureUser(((PickerEntity)approverPeoplePicker.Entities[0]).Description).Email;
                    if (itemId == 0)
                    {
                        // if item id is 0 then submit new request
                        dataContext.DocumentApprovalWorkflow.InsertOnSubmit(documentApprovalModel);
                    }
                    dataContext.SubmitChanges();

                    var reviewers = dataContext.DAReviewer.Where(x => x.Title == requestNo).ToList();

                    int level = 1;
                    foreach (var reviwer in reviewers)
                    {
                        obReviewer = reviwer;                        
                        obReviewer.Level = level;
                        dataContext.SubmitChanges();
                        level = level + 1;
                    }


                    var firstReviewer = dataContext.DAReviewer.Where(x => x.Title == requestNo && x.Level == 1).SingleOrDefault();
                    documentApprovalModel.PendingUserId = firstReviewer.ReviewerId;
                    documentApprovalModel.PendingDate = DateTime.Now;
                    documentApprovalModel.RequestStep = 1;
                    documentApprovalModel.RequestStatus = "Reviewer-1";

                  

                    if (itemId > 0) {
                        // Add to Approval Hitory List
                        approvalHistory.ReqNo = requestNo;
                        approvalHistory.Approval = "Resubmitted";
                        approvalHistory.ApprovalLevel = "Initiator";
                        approvalHistory.ApprovedBy = currentUserName;
                        approvalHistory.ApproverID = currentUserID.ToString();
                        approvalHistory.ApprovalDate = DateTime.Now.ToString();                     
                        dataContext.DAApprovalHistory.InsertOnSubmit(approvalHistory);
                    }

                    var attachements = dataContext.DAAttachments.Where(x => x.ReqNo == requestNo).ToList();
                    foreach (var attachment in attachements) {
                        attachment.Title = "Active";
                    }
                    dataContext.SubmitChanges();
                    dataContext.Dispose();



                    #region Send Email Notification
                    emailFormat = "NotifyForApproval";
                    emailUserInfo.Name = firstReviewer.ReviewerImnName;
                    emailUserInfo.Email = firstReviewer.Email;
                    requestNo = documentApprovalModel.ReqNo;
                    requestComment = documentApprovalModel.Comment;
                    string requestLink = webUrl + @"/_layouts/15/DocumentApproval/NewRequest.aspx?ItemID=" + documentApprovalModel.Id;
                    emailService.SendEmail(emailFormat, requestNo, requestComment, requestLink, comment, emailUserInfo, "");
                    #endregion

                    //---Redirect to My Request Page--//
                    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "AlertSubmittedSuccessfully();", true);
                   
                   // Response.Redirect("~/_layouts/15/DocumentApproval/MyRequest.aspx");
                }
              
            }
        }     
        protected void btnSubmitApproval_Click(object sender, EventArgs e)
        {
            string requestNo = txtRequestNo.Text;
            // lblMessage.Text = ViewState["ItemID"].ToString();
            int reqId = Convert.ToInt32(ViewState["RequestID"]);
            double? reqStep = Convert.ToDouble(ViewState["ReqStep"]);
            string currentUserName = UserProfileHelper.GetPropertyValue(loginName, "PreferredName");
            int currentUserID = SPContext.Current.Web.CurrentUser.ID;
            DAReviewerModel obReviewer = new DAReviewerModel();

            using (DocumentApprovalDataContext dataContext = new DocumentApprovalDataContext(webUrl))
            {
                if (ValidateSubmitApproval())
                {
                    var daRequest = dataContext.DocumentApprovalWorkflow.Where(x => x.Id == reqId).SingleOrDefault();
                DocumentApprovalModel documentApprovalModel = new DocumentApprovalModel();
                DAReviewerActionModel dAReviewerActionModel = new DAReviewerActionModel();
                DAApprovalHistoryModel approvalHistory = new DAApprovalHistoryModel();
                documentApprovalModel = daRequest;
                if (reqStep > 0 && reqStep <= 20)
                {
                    reqStep = reqStep + 1;

                    if (ddlReviewerApproval.SelectedValue == "Approved")
                    {
                        dAReviewerActionModel.DAId = documentApprovalModel.Id;
                        dAReviewerActionModel.Action = "Approved";
                        dAReviewerActionModel.ActionDate = DateTime.Now;
                        dAReviewerActionModel.ActionedById = currentUserID;
                        dAReviewerActionModel.Comment = txtReviewerComment.Text;

                        dataContext.DAReviewerAction.InsertOnSubmit(dAReviewerActionModel);
                        dataContext.SubmitChanges();

                        var reviewerList = dataContext.DAReviewer.Where(x => x.Title == requestNo).ToList();
                        var reviewerActionList = dataContext.DAReviewerAction.Where(x => x.DAId == reqId && x.Action == "Approved").ToList();
                        if (reviewerList.Count == reviewerActionList.Count)
                        {
                            documentApprovalModel.PendingUserId = documentApprovalModel.ApproverId;
                            documentApprovalModel.PendingDate = DateTime.Now;
                            documentApprovalModel.RequestStep = 50;
                            documentApprovalModel.RequestStatus = "Approver";
                            

                            emailFormat = "NotifyForApproval";
                            emailUserInfo.Name = documentApprovalModel.ApproverImnName;
                            emailUserInfo.Email = documentApprovalModel.ApproverEmail;

                                
                            }
                        else
                        {
                            var reviewer = dataContext.DAReviewer.Where(x => x.Title == requestNo && x.Level == reqStep).SingleOrDefault();

                            documentApprovalModel.PendingUserId = reviewer.ReviewerId;
                            documentApprovalModel.PendingDate = DateTime.Now;
                            documentApprovalModel.RequestStep = reqStep;
                            documentApprovalModel.RequestStatus = "Reviewer-" + reqStep;
                         

                            emailFormat = "NotifyForApproval";
                            emailUserInfo.Name = reviewer.ReviewerImnName;
                            emailUserInfo.Email = reviewer.Email;
                        }


                        // Add to Approval Hitory List
                        approvalHistory.ReqNo = requestNo;
                        approvalHistory.Approval = ddlReviewerApproval.SelectedValue;
                        approvalHistory.ApprovalLevel = "Reviewer";
                        approvalHistory.ApprovedBy = currentUserName;
                        approvalHistory.ApproverID = currentUserID.ToString();
                        approvalHistory.ApprovalDate = DateTime.Now.ToString();
                        approvalHistory.Comment = txtReviewerComment.Text;
                        dataContext.DAApprovalHistory.InsertOnSubmit(approvalHistory);
                        dataContext.SubmitChanges();


                    }
                    else if (ddlReviewerApproval.SelectedValue == "Returned")
                    {
                          

                            documentApprovalModel.PendingUserId = 0;
                        documentApprovalModel.PendingDate = null;
                        documentApprovalModel.RequestStep = 0;
                        documentApprovalModel.RequestStatus = "Returned";

                        // Add to Approval Hitory List
                        approvalHistory.ReqNo = requestNo;
                        approvalHistory.Approval = ddlReviewerApproval.SelectedValue;
                        approvalHistory.ApprovalLevel = "Reviewer";
                        approvalHistory.ApprovedBy = currentUserName;
                        approvalHistory.ApproverID = currentUserID.ToString();
                        approvalHistory.ApprovalDate = DateTime.Now.ToString();
                        approvalHistory.Comment = txtReviewerComment.Text;
                        dataContext.DAApprovalHistory.InsertOnSubmit(approvalHistory);
                      

                        emailFormat = "Returned";
                        comment = txtReviewerComment.Text;
                        emailUserInfo.Name = documentApprovalModel.RequesterName;
                        emailUserInfo.Email = documentApprovalModel.RequesterEmail;

                            //---Chear DAReviewerAction by ReqId---
                            using (SPSite site = new SPSite(webUrl))
                            {
                                using (SPWeb web = site.OpenWeb())
                                {
                                    SPList list = web.Lists["DAReviewerAction"];

                                    for (int i = list.Items.Count - 1; i >= 0; i--)
                                    {
                                        SPListItem item = list.Items[i];

                                        // Check your condition here and delete the items that match the condition.
                                        if (item["DAId"].ToString() == reqId.ToString())
                                        {
                                            item.Delete();
                                        }
                                    }
                                  
                                }
                            }

                        }
                    else if (ddlReviewerApproval.SelectedValue == "Rejected")
                    {
                        documentApprovalModel.PendingUserId = 0;
                        documentApprovalModel.PendingDate = null;
                        documentApprovalModel.RequestStep = 101;
                        documentApprovalModel.RequestStatus = "Rejected";

                        // Add to Approval Hitory List
                        approvalHistory.ReqNo = requestNo;
                        approvalHistory.Approval = ddlReviewerApproval.SelectedValue;
                        approvalHistory.ApprovalLevel = "Reviewer";
                        approvalHistory.ApprovedBy = currentUserName;
                        approvalHistory.ApproverID = currentUserID.ToString();
                        approvalHistory.ApprovalDate = DateTime.Now.ToString();
                        approvalHistory.Comment = txtReviewerComment.Text;
                        dataContext.DAApprovalHistory.InsertOnSubmit(approvalHistory);

                        emailFormat = "Rejected";
                        comment = txtReviewerComment.Text;
                        emailUserInfo.Name = documentApprovalModel.RequesterName;
                        emailUserInfo.Email = documentApprovalModel.RequesterEmail;
                    }
                    dataContext.SubmitChanges();
                }
                else if (reqStep == 50)
                {
                    if (ddlApproverApproval.SelectedValue == "Approved")
                    {
                        documentApprovalModel.PendingUserId = 0;
                        documentApprovalModel.PendingDate = null;
                        documentApprovalModel.RequestStep = 100;
                        documentApprovalModel.RequestStatus = "Completed";

                        // Add to Approval Hitory List
                        approvalHistory.ReqNo = requestNo;
                        approvalHistory.Approval = ddlApproverApproval.SelectedValue;
                        approvalHistory.ApprovalLevel = "Approver";
                        approvalHistory.ApprovedBy = currentUserName;
                        approvalHistory.ApproverID = currentUserID.ToString();
                        approvalHistory.ApprovalDate = DateTime.Now.ToString();
                        approvalHistory.Comment = txtApproverComment.Text;
                        dataContext.DAApprovalHistory.InsertOnSubmit(approvalHistory);

                        emailFormat = "Completed";
                        comment = txtApproverComment.Text;
                        emailUserInfo.Name = documentApprovalModel.RequesterName;
                        emailUserInfo.Email = documentApprovalModel.RequesterEmail;

                    }
                    else if (ddlApproverApproval.SelectedValue == "Returned")
                    {
                            //---Chear DAReviewerAction by ReqId---
                            using (SPSite site = new SPSite(webUrl))
                            {
                                using (SPWeb web = site.OpenWeb())
                                {
                                    SPList list = web.Lists["DAReviewerAction"];

                                    for (int i = list.Items.Count - 1; i >= 0; i--)
                                    {
                                        SPListItem item = list.Items[i];

                                        // Check your condition here and delete the items that match the condition.
                                        if (item["DAId"].ToString() == reqId.ToString())
                                        {
                                            item.Delete();
                                        }
                                    }
                                    //foreach (SPListItem item in list.Items)
                                    //{
                                    //    // Check your condition here and delete the items that match the condition.
                                    //    if (item["DAId"].ToString() == reqId.ToString())
                                    //    {
                                    //        item.Delete();                                         
                                    //    }
                                    //}
                                    //list.Update();
                                }
                            }

                            documentApprovalModel.PendingUserId = 0;
                        documentApprovalModel.PendingDate = null;
                        documentApprovalModel.RequestStep = 0;
                        documentApprovalModel.RequestStatus = "Returned";

                        // Add to Approval Hitory List
                        approvalHistory.ReqNo = requestNo;
                        approvalHistory.Approval = ddlApproverApproval.SelectedValue;
                        approvalHistory.ApprovalLevel = "Approver";
                        approvalHistory.ApprovedBy = currentUserName;
                        approvalHistory.ApproverID = currentUserID.ToString();
                        approvalHistory.ApprovalDate = DateTime.Now.ToString();
                        approvalHistory.Comment = txtApproverComment.Text;
                        dataContext.DAApprovalHistory.InsertOnSubmit(approvalHistory);

                        emailFormat = "Returned";
                        comment = txtApproverComment.Text;
                        emailUserInfo.Name = documentApprovalModel.RequesterName;
                        emailUserInfo.Email = documentApprovalModel.RequesterEmail;

                       
                    }
                    else if (ddlApproverApproval.SelectedValue == "Rejected")
                    {
                        documentApprovalModel.PendingUserId = 0;
                        documentApprovalModel.PendingDate = null;
                        documentApprovalModel.RequestStep = 101;
                        documentApprovalModel.RequestStatus = "Rejected";

                        // Add to Approval Hitory List
                        approvalHistory.ReqNo = requestNo;
                        approvalHistory.Approval = ddlApproverApproval.SelectedValue;
                        approvalHistory.ApprovalLevel = "Approver";
                        approvalHistory.ApprovedBy = currentUserName;
                        approvalHistory.ApproverID = currentUserID.ToString();
                        approvalHistory.ApprovalDate = DateTime.Now.ToString();
                        approvalHistory.Comment = txtApproverComment.Text;
                        dataContext.DAApprovalHistory.InsertOnSubmit(approvalHistory);

                        emailFormat = "Returned";
                        comment = txtApproverComment.Text;
                        emailUserInfo.Name = documentApprovalModel.RequesterName;
                        emailUserInfo.Email = documentApprovalModel.RequesterEmail;
                    }
                    dataContext.SubmitChanges();
                }
                dataContext.Dispose();

                #region Send Email Notification
                requestNo = documentApprovalModel.ReqNo;
                requestComment = documentApprovalModel.Comment;
                string requestLink = webUrl + @"/_layouts/15/DocumentApproval/NewRequest.aspx?ItemID=" + documentApprovalModel.Id;
                emailService.SendEmail(emailFormat, requestNo, requestComment, requestLink, comment, emailUserInfo, "");
                #endregion
            }

            }
            ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "AlertApprovalSubmittedSuccessfully();", true);
        }      
        private void GetRequestById(int reqID)
        {
            pnlShow.Visible = true;
            pnlShow2.Visible = true;
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
                lblApproverName.Text = request.ApproverImnName;
                LoadAttachmentByReqId(request.ReqNo);
                LoadApprovalHistoryByReqId(request.ReqNo);
                // Load Reviewser in Grid
                var listReviwers = dataContext.DAReviewer.Where(x => x.Title == request.ReqNo).ToList();

                CreateDataTable();
                DataRow drRow;
                foreach (DAReviewerModel item in listReviwers)
                {
                    drRow = dtReviewer.NewRow();
                    drRow["ID"] = item.Id.ToString();
                    drRow["Name"] = item.ReviewerImnName;

                    dtReviewer.Rows.Add(drRow);
                }

                gvReviewerList.DataSource = dtReviewer;
                gvReviewerList.DataBind();

                gvReviewerView.DataSource = dtReviewer;
                gvReviewerView.DataBind();
                //Load Approver in PP
                SPUser approver = SPContext.Current.Web.EnsureUser(request.ApproverImnName);
                PickerEntity objPickerEntity = new PickerEntity();
                objPickerEntity.Key = approver.LoginName;
                approverPeoplePicker.Entities.Add(approverPeoplePicker.ValidateEntity(objPickerEntity));


                ViewState["RequestID"] = request.Id;
                ViewState["RequestNo"] = request.ReqNo;
                ViewState["ReqStep"] = request.RequestStep;


                if (request.RequestStep > 0)
                {
                    btnSubmit.Visible = false;
                    txtComment.Enabled = false;
                    //pnlApprovalHistory.Visible = true;
                    pblUploadAttachment.Visible = false;
                    gvAttachments.Columns[2].Visible = false;
                    gvReviewerList.Columns[2].Visible = false;
                    reviewerPeoplePicker.Visible = false;
                    btnAddReviewer.Visible = false;
                    approverPeoplePicker.Enabled = false;
                    pnlReviewerShow.Visible = false;
                    pnlReviewerViewMode.Visible = true;

                }
                if (request.RequestStep > 0 && request.RequestStep <= 20)
                {
                    var reviewer = dataContext.DAReviewer.Where(x => x.Title == request.ReqNo && x.Level == request.RequestStep).SingleOrDefault();
                    if (reviewer != null)
                    {
                        if (reviewer.ReviewerId == currentUserID)
                        {
                            pnlReviewer.Visible = true;
                        }
                    }
                }

                if (request.RequestStep == 50)
                {
                    if (request.PendingUserId == currentUserID)
                    {
                        pnlApprover.Visible = true;
                    }
                }
            }
        }
        private void LoadApprovalHistoryByReqId(string reqNo)
        {
            using (DocumentApprovalDataContext dataContext = new DocumentApprovalDataContext(webUrl))
            {
                var approvalHistoryList = dataContext.DAApprovalHistory.Where(x => x.ReqNo == reqNo).ToList();
    
                if (approvalHistoryList.Count > 0 ) {
                    pnlApprovalHistory.Visible = true;
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

        #region Reviewer
        protected void btnAddReviewer_Click(object sender, EventArgs e)
        {
            string requestNo = txtRequestNo.Text;
            Reviewer obReviewer = new Reviewer();
            List<Reviewer> listReviwer = new List<Reviewer>();

             try
             {
                if (reviewerPeoplePicker.Entities.Count > 0)
                {
                    bool status = false;
                    PickerEntity reviewerPicker = (PickerEntity)reviewerPeoplePicker.Entities[0];
                    string reviewerEmail = reviewerPicker.Description;
                    obReviewer.Email = UserProfileHelper.GetPropertyValue(reviewerEmail, "WorkEmail");
                    obReviewer.UserId = SPContext.Current.Web.EnsureUser(((PickerEntity)reviewerPeoplePicker.Entities[0]).Description).ID;
                    obReviewer.Name = SPContext.Current.Web.EnsureUser(((PickerEntity)reviewerPeoplePicker.Entities[0]).Description).Name;

                    if (gvReviewerList.Rows.Count > 0)
                    {

                        foreach (GridViewRow row in gvReviewerList.Rows)
                        {

                            string userName = row.Cells[1].Text;


                            if (obReviewer.Name == userName)
                            {
                                // lblMessage.Text = "Same Reviewer Already Exists";
                                status = false;
                                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ExistsReviewer();", true);
                                break;

                            }
                            else
                            {
                                status = true;
                            }

                            //}
                        }
                    }
                    else {
                        status = true;
                    }

                    if (status)
                    {
                        //obReviewer.UserId = SPContext.Current.Web.EnsureUser(((PickerEntity)reviewerPeoplePicker.Entities[0]).Description).ID;
                        //obReviewer.Name = SPContext.Current.Web.EnsureUser(((PickerEntity)reviewerPeoplePicker.Entities[0]).Description).Name;
                        //obReviewer.Email = SPContext.Current.Web.EnsureUser(((PickerEntity)reviewerPeoplePicker.Entities[0]).Description).Email;                               
                        using (DocumentApprovalDataContext dataContext = new DocumentApprovalDataContext(webUrl))
                        {
                            DAReviewerModel approverModel = new DAReviewerModel();
                            approverModel.Title = requestNo;
                            approverModel.ReviewerId = obReviewer.UserId;
                            approverModel.Email = obReviewer.Email;
                            approverModel.Level = 0;

                            dataContext.DAReviewer.InsertOnSubmit(approverModel);
                            dataContext.SubmitChanges();
                            dataContext.Dispose();
                            reviewerPeoplePicker.Entities.Clear();

                            var listReviwers = dataContext.DAReviewer.Where(x => x.Title == requestNo).ToList();
                            CreateDataTable();
                            DataRow drRow;
                            foreach (DAReviewerModel item in listReviwers)
                            {
                                drRow = dtReviewer.NewRow();
                                drRow["ID"] = item.Id.ToString();
                                drRow["Name"] = item.ReviewerImnName;

                                dtReviewer.Rows.Add(drRow);
                            }

                            gvReviewerList.DataSource = dtReviewer;
                            gvReviewerList.DataBind();
                        }
                    }
                }

                   
             
              
                   

                }
                catch (Exception ex)
                {
                    //lblMessage.Text = ex.Message;
                }

        

            //if (ViewState["listReviwer"] != null)
            //{
            //    gvReviewerList.DataSource = (List<Reviewer>)ViewState["listReviwer"];
            //    gvReviewerList.DataBind();
            //}
            //else
            //{
            //    List<Reviewer> reviwers = new List<Reviewer>();
            //    ViewState["Reviwers"] = reviwers;
            //}

        }
        private void CreateDataTable()
        {
            dtReviewer.Columns.Add("ID", typeof(Int32));
            dtReviewer.Columns.Add("Name", typeof(string));
        }
      
        protected void gvReviewerList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "deleteReviewer")
            {
                string requestNo = txtRequestNo.Text;                           
                using (SPSite oSite = new SPSite(webUrl))
                {
                    //Get a Root Web  
                    using (SPWeb oWeb = oSite.OpenWeb())
                    {
                        int itemId = Convert.ToInt32(e.CommandArgument);
                        //Get a Particular List                       
                        SPList list = oWeb.Lists.TryGetList("DAReviewer");
                        // Delete the List item by ID  
                        SPListItem itemToDelete = list.GetItemById(itemId);
                        itemToDelete.Delete();

                        //Load in Grid
                        LoadReviewer(requestNo);
                    }

                }
            }
        }

        private void LoadReviewer(string requestNo)
        {
            using (DocumentApprovalDataContext dataContext = new DocumentApprovalDataContext(webUrl))
            {
                var listReviwers = dataContext.DAReviewer.Where(x => x.Title == requestNo).ToList();
                CreateDataTable();
                DataRow drRow;
                foreach (DAReviewerModel item in listReviwers)
                {
                    drRow = dtReviewer.NewRow();
                    drRow["ID"] = item.Id.ToString();
                    drRow["Name"] = item.ReviewerImnName;

                    dtReviewer.Rows.Add(drRow);
                }

                gvReviewerList.DataSource = dtReviewer;
                gvReviewerList.DataBind();
            }
        }
        #endregion

        #region Attachment
        protected void btnAttachment_Click(object sender, EventArgs e)
        {
            if (fileUploadControl.PostedFile != null)
            {
                string requestNo = txtRequestNo.Text;
                String documentLibraryName = "DAAttachments";
                using (SPSite oSite = new SPSite(SPContext.Current.Site.Url))
                {
                    using (SPWeb oWeb = oSite.OpenWeb())
                    {
                        SPFolder myLibrary = oWeb.Folders[documentLibraryName];
                        Boolean replaceExistingFiles = true;
                        Random generator = new Random();
                        int uniqueId = generator.Next(100000, 999999);
                        String fileName = fileUploadControl.PostedFile.FileName;
                        String uniqueFileName = uniqueId + "-" + fileName  ;
                        // Upload document
                        SPFile spfile = myLibrary.Files.Add(uniqueFileName, fileUploadControl.PostedFile.InputStream, replaceExistingFiles);
                        spfile.Item.Properties["ReqNo"] = requestNo;
                        spfile.Item.Properties["FileName"] = fileName;
                        spfile.Item.SystemUpdate();
                        spfile.Update();
                        // Commit 
                        myLibrary.Update();


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
                                //string file_Name = file.Name;
                                string file_Name = item["FileName"].ToString();
                               
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
           
        }
        protected void gvAttachments_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "deleteAttachment")
            {
                string requestNo = txtRequestNo.Text;
                using (SPSite oSite = new SPSite(SPContext.Current.Site.Url))
                {
                    //Get a Root Web  
                    using (SPWeb oWeb = oSite.RootWeb)
                    {
                        int itemId = Convert.ToInt32(e.CommandArgument);
                        //Get a Particular List  
                        SPList docLib = oWeb.Lists["DAAttachments"];

                        // Delete the List item by ID  
                        SPListItem itemToDelete = docLib.GetItemById(itemId);
                        itemToDelete.Delete();

                        LoadAttachmentByReqId(requestNo);
                    }
                }
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
                            dataRow["FileName"] = item["FileName"];
                            dataRow["FileUrl"] = file_Url;
                            dt.Rows.Add(dataRow);
                        }
                    }

                    gvAttachments.DataSource = dt;
                    gvAttachments.DataBind();
                }
            }
        }
        #endregion

        #region Validation
        private bool ValidateSubmitRequest()
        {
            if (txtComment.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "RequiredComment();", true);
                return false;
            }
            if (gvAttachments.Rows.Count == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "RequiredAttachment();", true);
                return false;
            }
            if (gvReviewerList.Rows.Count == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "RequiredReviewer();", true);
                return false;
            }
            if (approverPeoplePicker.Accounts.Count == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "RequiredApprover();", true);
                return false;
            }
            //if (gvReviewerList.Rows.Count > 0) {
            //    foreach (GridViewRow row in gvReviewerList.Rows) { 

            //    }
            //}
            else
            {
                return true;
            }
        }
        private bool ValidateSubmitApproval()
        {
            bool isReturn = false;
            double? reqStep = Convert.ToDouble(ViewState["ReqStep"]);

            if (reqStep > 0 && reqStep <= 20)
            {
                if (ddlReviewerApproval.SelectedValue == "")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "RequiredApprovalStatus();", true);
                    isReturn = false;
                }
                if (txtReviewerComment.Text == "")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "RequiredComment();", true);
                    isReturn = false;
                }
                else {
                    return isReturn = true;
                }
            }

            if (reqStep == 50)
            {
                if (ddlApproverApproval.SelectedValue == "")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "RequiredApprovalStatus();", true);
                    isReturn = false;
                }
                if (txtApproverComment.Text == "")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "RequiredComment();", true);
                    isReturn = false;
                }
                else
                {
                    return isReturn = true;
                }
            }         
            return isReturn;
        }


        #endregion

        protected void ddlReviewerApproval_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
