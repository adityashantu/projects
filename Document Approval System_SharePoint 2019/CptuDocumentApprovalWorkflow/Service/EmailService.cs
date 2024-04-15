using CPTUDocumentApprovalWorkflow.Models;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Administration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace CPTUDocumentApprovalWorkflow.Service
{
    public class EmailService
    {
        internal void SendEmail(string emailFormat, string requestNo, string requestComment, string requestLink, string comment, EmailUserInfo emailToUser, string ccUsers)
        {          
            try
            {            
                using (SPSite site = new SPSite(SPContext.Current.Site.Url))
                {
                    using (SPWeb web = site.OpenWeb())
                    {
                        string currentUser = SPContext.Current.Web.CurrentUser.Name;
                        string toUserEmail = emailToUser.Email;
                        string fromEMailAddr = SPAdministrationWebApplication.Local.OutboundMailSenderAddress;
                        MailMessage mail = new MailMessage();
                        mail.From = new MailAddress(fromEMailAddr, "Document Approval");
                        mail.To.Add(toUserEmail);
    
                        mail.Subject = GetMailSubject(emailFormat, requestNo);
                        mail.Body = GetEmailBodyTemplate(emailFormat, requestNo, requestComment, requestLink, comment);
                        mail.IsBodyHtml = true;

                        SmtpClient smtp = new SmtpClient(site.WebApplication.OutboundMailServiceInstance.Server.Address);
                        smtp.UseDefaultCredentials = true;
                        smtp.Send(mail);
                    }
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
        }
        internal void SendAutoApprovedEmail(string emailFormat, string requestNo, string requestComment, string requestLink, string comment, EmailUserInfo emailToUser, string ccUsers, string webUrl)
        {
            try
            {
                using (SPSite site = new SPSite(webUrl))
                {
                    using (SPWeb web = site.OpenWeb())
                    {
                        //string currentUser = SPContext.Current.Web.CurrentUser.Name;
                        string toUserEmail = emailToUser.Email;
                        string fromEMailAddr = SPAdministrationWebApplication.Local.OutboundMailSenderAddress;
                        MailMessage mail = new MailMessage();
                        mail.From = new MailAddress(fromEMailAddr, "Document Approval");
                        mail.To.Add(toUserEmail);

                        mail.Subject = GetMailSubject(emailFormat, requestNo);
                        mail.Body = GetEmailBodyTemplate(emailFormat, requestNo, requestComment, requestLink, comment);
                        mail.IsBodyHtml = true;

                        SmtpClient smtp = new SmtpClient(site.WebApplication.OutboundMailServiceInstance.Server.Address);
                        smtp.UseDefaultCredentials = true;
                        smtp.Send(mail);
                    }
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
        }
        private string GetMailSubject(string emailFormat, string requestNo)
        {
            string emailSubject = string.Empty;
            if (emailFormat == "NotifyForApproval")
            {
                emailSubject = "Document Approval Request " + "(" + requestNo + ")";
            }
            if (emailFormat == "Returned")
            {
                emailSubject = "Document Approval Request: Returned Notification " + "(" + requestNo + ")";
            }
            if (emailFormat == "Rejected")
            {
                emailSubject = "Document Approval Request: Rejected Notification " + "(" + requestNo + ")";
            }
            if (emailFormat == "Completed")
            {
                emailSubject = "Document Approval Request: Approved Notification " + "(" + requestNo + ")";
            }
            return emailSubject;
        }
        private string GetEmailBodyTemplate(string emailFormat, string requestNo, string requestComment, string requestLink, string comment)
        {
            string emailBody = string.Empty;
            if (emailFormat == "NotifyForApproval") {
                emailBody = "<!DOCTYPE html> " +
                "<html xmlns=\"http://www.w3.org/1999/xhtml\">" +
                "<head>" +
                    "<title>Document Approval Request</title>" +
                "</head>" +
                "<body style=\"font-family:'Century Gothic'\">" +
                    "<p style=\"font-size:10px;\">" +
                        "<p>" + "Dear Sir/Madam," + "</p>" +
                        "<p>" + "Document approval request is inititated for your approval." + "</p>" +
                        "<b>" + "Request No: "+"</b>" + "<p>"+ requestNo + "," + " </p>" +
                        "<b>" + "Comment: " + "</b>" + "<p>" + requestComment + "," + " </p>" +              
                        "<br />" +
                        "<p>" + "To review request through system link " + "<a href=" + requestLink + ">" + "Click Here." + "</a>" + "<br />" + "</p>" +
                        "Thank You" + "<br />" +
                    //"Email : " + emailTextBox.Text +
                    "</p>" +

                "</body>" +
                "</html>";
            }

            if (emailFormat == "Returned")
            {
                emailBody = "<!DOCTYPE html> " +
                "<html xmlns=\"http://www.w3.org/1999/xhtml\">" +
                "<head>" +
                    "<title>Document Approval Request</title>" +
                "</head>" +
                "<body style=\"font-family:'Century Gothic'\">" +
                    "<p style=\"font-size:10px;\">" +
                        "<p>" + "Dear Sir/Madam," + "</p>" +
                        "<p>" + "Your Document Approval Request has been returned." + "</p>" +
                        "<b>" + "Request No: " + "</b>" + "<p>" + requestNo + "," + " </p>" +                       
                        "<p>" + "Comment: " + comment + "," + "</p>" +
                        "<br />" +
                        "<p>" + "To update request " + "<a href=" + requestLink + ">" + "Click Here" + "</a>" + "<br />" + "</p>" +
                        "Thank You" + "<br />" +
                    //"Email : " + emailTextBox.Text +
                    "</p>" +

                "</body>" +
                "</html>";
            }

            if (emailFormat == "Rejected")
            {
                emailBody = "<!DOCTYPE html> " +
                "<html xmlns=\"http://www.w3.org/1999/xhtml\">" +
                "<head>" +
                    "<title>Document Approval Request</title>" +
                "</head>" +
                "<body style=\"font-family:'Century Gothic'\">" +
                    "<p style=\"font-size:10px;\">" +
                        "<p>" + "Dear Sir/Madam," + "</p>" +
                        "<p>" + "Your Document Approval Request has been rejected." + "</p>" +
                        "<b>" + "Request No: " + "</b>" + "<p>" + requestNo + "," + " </p>" +
                        "<p>" + "Comment: " + comment + "," + "</p>" +
                        "<br />" +
                        "<p>" + "To view details " + "<a href=" + requestLink + ">" + "Click Here" + "</a>" + "<br />" + "</p>" +
                        "Thank You" + "<br />" +
                    //"Email : " + emailTextBox.Text +
                    "</p>" +

                "</body>" +
                "</html>";
            }

            if (emailFormat == "Completed")
            {
                emailBody = "<!DOCTYPE html> " +
                "<html xmlns=\"http://www.w3.org/1999/xhtml\">" +
                "<head>" +
                    "<title>Document Approval Request</title>" +
                "</head>" +
                "<body style=\"font-family:'Century Gothic'\">" +
                    "<p style=\"font-size:10px;\">" +
                        "<p>" + "Dear Sir/Madam," + "</p>" +
                        "<p>" + "Your Document Approval Request has been approved." + "</p>" +
                        "<b>" + "Request No: " + "</b>" + "<p>" + requestNo + "," + " </p>" +                       
                        "<p>" + "Comment: " + comment + "," + "</p>" +
                        "<br />" +
                        "<p>" + "To view approval details " + "<a href=" + requestLink + ">" + "Click Here" + "</a>" + "<br />" + "</p>" +
                        "Thank You" + "<br />" +
                    //"Email : " + emailTextBox.Text +
                    "</p>" +

                "</body>" +
                "</html>";
            }
            return emailBody;
        }
    }
}
