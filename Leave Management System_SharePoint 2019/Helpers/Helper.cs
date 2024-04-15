/*using LeaveManagementCPTU.CONTROLTEMPLATES.LeaveManagementCPTU;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Administration;
using Microsoft.SharePoint.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net.Mail;
using LeaveRequest;
using System.Text;
using System.Threading.Tasks;

namespace LeaveManagementCPTU.Helpers
{
    public class Helper
    {
        static string webUrl = SPContext.Current.Web.Url;

        public static bool CheckUserAuthorization(int requestID)
        {
            try
            {
                bool isApprover = false;

                PendingApprovals objPending = new PendingApprovals();
                using (LeaveManagementCPTUDataContext objDataContext = new LeaveManagementCPTUDataContext(webUrl))
                {

                    objPending = objDataContext.PendingApproval.FirstOrDefault(x => x.RequestID == requestID);

                    ////---Check whether current user is Approver or not--
                    if (objPending != null)
                    {
                        if (SPContext.Current.Web.CurrentUser.ID == objPending.PendingToId)
                        {
                            return isApprover = true;
                        }
                        else
                        {
                            return isApprover = false;
                        }
                    }
                    else
                    {
                        return isApprover = false;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static int? FindNextApprover(string deptName)
        {
            int? nextApproverId = null;
            try
            {
                using (LeaveManagementCPTUDataContext objDataContext = new LeaveManagementCPTUDataContext(SPContext.Current.Web.Url))
                {
                    nextApproverId = objDataContext.DepartmentApprover.Where(x => x.DepartmentName.Equals(deptName)).FirstOrDefault().ApproverId;
                    objDataContext.Dispose();

                }
                return nextApproverId.HasValue ? nextApproverId : null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static void PrepareEmailForRequester(string toEmailAddr, Int32? requestCode, string requestorName, string requestLink, string approverName = null, string approvalStatus = null)
        {
            string emailFrom = "testuser4@xyzlocal.onmicrosoft.com";
            string emailFromName = "Leave Request";
            string eMailSubject = string.Empty;
            string emailBody = string.Empty;

            eMailSubject = "Your Leave Request has been " + approvalStatus;
            emailBody = "Dear Mr. /Ms. " + requestorName + ",</br>" +
                                "Request for Leave has been processed.</br>" +
                                "Request Informtion:</br>" +
                                "Last Approver: " + approverName + "</br>" +
                                "Approval Status: " + approvalStatus + "</br>" +
                                "Request ID: " + requestCode + "</br>" +
                                "Click <a href=" + requestLink + ">here</a> to see details.</br></br>" +
                                "[This is a System Generated Email from Portal. Thus, requesting you not to reply this E-mail.Thank you for using this Portal]";


            SendMailBySharePoint(toEmailAddr, eMailSubject, emailBody);
            //SendMailBySMTP(toEmailAddr, emailFrom, emailFromName, eMailSubject, emailBody);
        }
        public void PrepareEmailForApprover(string toEmailAddr, string requestCode, string requestorName, string approverName, string requestLink)
        {
            string eMailSubject = "Request for Trade Merchandising with ID: " + requestCode + " is waiting for your approval";
            string emailBody = "Dear Mr. /Ms. " + approverName + ",</br>" +
                               "Request for Trade Merchandising is waiting for your approval. Please proceed to continue.</br>" +
                               "Request Information:</br>" +
                               "Request from: " + requestorName + "</br>" +
                                "Request Code: " + requestCode + "</br>" +
                                "Click <a href=" + requestLink + ">here</a> to give approval.</br></br>" +
                                "[This is a System Generated Email from Berger Portal. Thus, requesting you not to reply this E-mail. " +
                                "If you need any further assistance, please login Berger Portal, go to DMS section & go through your desired Process Document. Thank you for using this Portal]";

            SendMailBySharePoint(toEmailAddr, eMailSubject, emailBody);
        }

        ////Send Email By SharePoint
        private static void SendMailBySharePoint(string toEmailAddr, string eMailSubject, string emailBody)
        {
            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate ()
                {
                    string fromEMailAddr = SPAdministrationWebApplication.Local.OutboundMailSenderAddress;
                    using (SPWeb spWeb = new SPSite(webUrl).OpenWeb())
                    {

                        StringDictionary emailHeader = new StringDictionary();

                        emailHeader.Add("from", fromEMailAddr);
                        emailHeader.Add("to", toEmailAddr);
                        emailHeader.Add("subject", eMailSubject);

                        SPUtility.SendEmail(spWeb, emailHeader, emailBody);
                    }
                });
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        ////Send Email By SMTP
        public static bool SendMailBySMTP(string to, string from, string fromName, string subject, string body)
        {
            SmtpClient smtpClient = new SmtpClient();
            MailMessage message = new MailMessage();

            try
            {
                MailAddress fromAddress = new MailAddress(from, fromName);

                //mail server
                smtpClient.Host = "smtp.office365.com"; //xyzlocal.mail.protection.outlook.com
                message.From = fromAddress;
                message.To.Add(to);
                message.Subject = subject;
                message.IsBodyHtml = true;
                message.Body = body;

                smtpClient.Port = 587;

                smtpClient.EnableSsl = false;
                smtpClient.UseDefaultCredentials = false;

                smtpClient.Send(message);
            }
            catch (SmtpException smtpEx)
            {
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

    }
}
*/