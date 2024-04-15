using CPTUDocumentApprovalWorkflow.Models;
using Microsoft.SharePoint;
using nDocumentApproval;
using PBL_FarmSolution.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPTUDocumentApprovalWorkflow.Service
{
    public class TimerJobService
    {
        string emailFormat = string.Empty;
        
        string comment = string.Empty;
        EmailService emailService = new EmailService();
        EmailUserInfo emailUserInfo = new EmailUserInfo();

        internal void AutoApprovalFunc(string webUrl)
        {         
            using (DocumentApprovalDataContext dataContext = new DocumentApprovalDataContext(webUrl))
            {
                var pendingDateReqList = dataContext.DocumentApprovalWorkflow.Where(x=>x.PendingDate != null).ToList();
                               
                foreach (var request in pendingDateReqList) 
                {
                    DateTime todaydate = DateTime.Now.Date;
                    DateTime pendingDate = request.PendingDate.Value.Date;
                    TimeSpan timeDif = todaydate.Subtract(pendingDate);
                    if (timeDif.TotalDays >= 7 && timeDif.TotalDays < 8)
                    {
                        // var daRequest = dataContext.DocumentApprovalWorkflow.Where(x => x.Id == reqId).SingleOrDefault();
                        DocumentApprovalModel documentApprovalModel = new DocumentApprovalModel();
                        DAReviewerActionModel dAReviewerActionModel = new DAReviewerActionModel();
                        DAApprovalHistoryModel approvalHistory = new DAApprovalHistoryModel();
                        documentApprovalModel = request;
                        if (request.RequestStep > 0 && request.RequestStep <= 20)
                        {
                           request.RequestStep = request.RequestStep + 1;

                            dAReviewerActionModel.DAId = request.Id;
                            dAReviewerActionModel.Action = "Approved";
                            dAReviewerActionModel.ActionDate = DateTime.Now;
                            dAReviewerActionModel.ActionedById = request.PendingUserId;
                            dAReviewerActionModel.Comment = "Auto Approved by System.";

                            dataContext.DAReviewerAction.InsertOnSubmit(dAReviewerActionModel);
                            dataContext.SubmitChanges();

                            var reviewerList = dataContext.DAReviewer.Where(x => x.Title == request.ReqNo).ToList();
                            var reviewerActionList = dataContext.DAReviewerAction.Where(x => x.DAId == request.Id && x.Action == "Approved").ToList();
                            if (reviewerList.Count == reviewerActionList.Count)
                            {
                                documentApprovalModel.PendingUserId = documentApprovalModel.ApproverId;
                                documentApprovalModel.RequestStep = 50;
                                documentApprovalModel.RequestStatus = "Approver";

                                emailFormat = "NotifyForApproval";
                                emailUserInfo.Name = documentApprovalModel.ApproverImnName;
                                emailUserInfo.Email = documentApprovalModel.ApproverEmail;
                            }
                            else
                            {
                                var reviewer = dataContext.DAReviewer.Where(x => x.Title == request.ReqNo && x.Level == request.RequestStep).SingleOrDefault();
                                documentApprovalModel.PendingUserId = reviewer.ReviewerId;
                                documentApprovalModel.RequestStep = request.RequestStep;
                                documentApprovalModel.RequestStatus = "Reviewer" + request.RequestStep;

                                emailFormat = "NotifyForApproval";
                                emailUserInfo.Name = reviewer.ReviewerImnName;
                                emailUserInfo.Email = reviewer.Email;
                            }


                            // Add to Approval Hitory List
                            approvalHistory.ReqNo = request.ReqNo;
                            approvalHistory.Approval = "Approved";
                            approvalHistory.ApprovalLevel = "Reviewer";
                            approvalHistory.ApprovedBy = request.PendingUserImnName;
                            approvalHistory.ApproverID = request.PendingUserId.ToString();
                            approvalHistory.ApprovalDate = DateTime.Now.ToString();
                            approvalHistory.Comment = "Auto Approved by System.";
                            dataContext.DAApprovalHistory.InsertOnSubmit(approvalHistory);
                            dataContext.SubmitChanges();                                                                             
                        }
                        else if (request.RequestStep == 50)
                        {
                            documentApprovalModel.PendingUserId = 0;
                            documentApprovalModel.RequestStep = 100;
                            documentApprovalModel.RequestStatus = "Completed";

                            // Add to Approval Hitory List
                            approvalHistory.ReqNo = request.ReqNo;
                            approvalHistory.Approval = "Approved";
                            approvalHistory.ApprovalLevel = "Approver";
                            approvalHistory.ApprovedBy = request.PendingUserImnName;
                            approvalHistory.ApproverID = request.PendingUserId.ToString();
                            approvalHistory.ApprovalDate = DateTime.Now.ToString();
                            approvalHistory.Comment = "Auto Approved by System.";
                            dataContext.DAApprovalHistory.InsertOnSubmit(approvalHistory);

                            emailFormat = "Completed";
                            comment = approvalHistory.Comment;
                            emailUserInfo.Name = documentApprovalModel.RequesterName;
                            emailUserInfo.Email = documentApprovalModel.RequesterEmail;

                            dataContext.SubmitChanges();
                        }
                        dataContext.Dispose();

                        #region Send Email Notification
                        string requestNo = request.ReqNo;
                        string requestTitle = request.Comment;
                        string requestLink = webUrl + @"/_layouts/15/DocumentApproval/NewRequest.aspx?ItemID=" + request.Id;
                        emailService.SendAutoApprovedEmail(emailFormat, requestNo, requestTitle, requestLink, comment, emailUserInfo, "", webUrl);
                        #endregion
                    }
                }
            }
        }
    }
}
