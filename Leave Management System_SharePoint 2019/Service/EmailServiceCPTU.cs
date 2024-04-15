using LeaveManagementCPTU.Datas; //using CPTUDocumentApprovalWorkflow.Models;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Administration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace LeaveManagementCPTU.Service
{
    public class EmailServiceCPTU
    {
        internal void SendEmail(string emailFormat, EmailUserInfo emailToUser, string fromDate, string toDate, string comment)
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
                        mail.From = new MailAddress(fromEMailAddr, "Leave Management System");
                        mail.To.Add(toUserEmail);

                        mail.Subject = "Leave Management System";
                        mail.Body = GetEmailBodyTemplate(emailFormat, currentUser, fromDate, toDate, comment);
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

        private string GetEmailBodyTemplate(string emailFormat, string currentUser, string fromDate, string toDate, string comment)
        {
            string emailBody = string.Empty;
            if (emailFormat == "NotifyForApproval")
            {
                emailBody = "<!DOCTYPE html> " +
                "<html xmlns=\"http://www.w3.org/1999/xhtml\">" +
                "<head>" +
                    "<title>Leave Managment System</title>" +
                "</head>" +
                "<body style=\"font-family:'Century Gothic'\">" +
                    "<p style=\"font-size:10px;\">" +
                        "<p>" + "Dear Sir/Madam," + "</p>" +
                        "<p>" + "<b>" + currentUser +"</b>" +" applied for a leave request from " + fromDate + " to " + toDate + ". Please visit the Leave Management System from your SharePoint." + "</p>" +
                        
                        "<p>" + "You can also visit this link to review the request. " + "<a href=" + "http://sharepoint.cptu.gov.bd/leavemanagementsystem/_layouts/15/LeaveManagementCPTU/ManagerMain.aspx" + ">" + "Click Here." + "</a>" + "<br />" + "</p>" +
                        "Thank You" + "<br />" +
                        "SharePoint Server" + "<br />" +
                    //"Email : " + emailTextBox.Text +
                    "</p>" +

                "</body>" +
                "</html>";
            }

            else if (emailFormat == "Approve" && (comment == "" || comment == null))
            {
                emailBody = "<!DOCTYPE html> " +
                "<html xmlns=\"http://www.w3.org/1999/xhtml\">" +
                "<head>" +
                    "<title>Leave Managment Syste</title>" +
                "</head>" +
                "<body style=\"font-family:'Century Gothic'\">" +
                    "<p style=\"font-size:10px;\">" +
                        "<p>" + "Dear Sir/Madam," + "</p>" +
                        "<p>" + "One of your Leave Request from " + fromDate + " to " + toDate + " has been approved." + "</p>" +
                        "<p>" + "To check your leave requests, Please visit the link." + "<a href=" + "http://sharepoint.cptu.gov.bd/leavemanagementsystem/_layouts/15/LeaveManagementCPTU/MyRequests.aspx" + ">" + "Click Here" + "</a>" + "<br />" + "</p>" +
                        "Thank You" + "<br />" +
                        "SharePoint Server" + "<br />" +
                    //"Email : " + emailTextBox.Text +
                    "</p>" +

                "</body>" +
                "</html>";
            }

            else if (emailFormat == "Approve" && comment != "") 
            {
                emailBody = "<!DOCTYPE html> " +
                "<html xmlns=\"http://www.w3.org/1999/xhtml\">" +
                "<head>" +
                    "<title>Leave Managment Syste</title>" +
                "</head>" +
                "<body style=\"font-family:'Century Gothic'\">" +
                    "<p style=\"font-size:10px;\">" +
                        "<p>" + "Dear Sir/Madam," + "</p>" +
                        "<p>" + "One of your Leave Request from " + fromDate + " to " + toDate + " has been approved." + "</p>" +
                        "<p>" + "<b>" + "Manager Comment: " + "</b>" + comment + "</p>" +
                        "<p>" + "To check your leave requests, Please visit the link." + "<a href=" + "http://sharepoint.cptu.gov.bd/leavemanagementsystem/_layouts/15/LeaveManagementCPTU/MyRequests.aspx" + ">" + "Click Here" + "</a>" + "<br />" + "</p>" +
                        "Thank You" + "<br />" +
                        "SharePoint Server" + "<br />" +
                    //"Email : " + emailTextBox.Text +
                    "</p>" +

                "</body>" +
                "</html>";
            }

            if (emailFormat == "Unapprove")
            {
                emailBody = "<!DOCTYPE html> " +
                "<html xmlns=\"http://www.w3.org/1999/xhtml\">" +
                "<head>" +
                    "<title>Leave Managment Syste</title>" +
                "</head>" +
                "<body style=\"font-family:'Century Gothic'\">" +
                    "<p style=\"font-size:10px;\">" +
                        "<p>" + "Dear Sir/Madam," + "</p>" +
                        "<p>" + "We are sorry to inform you that one of your Leave Request from " + fromDate + " to " + toDate + " has been Rejected." + "</p>" +
                        "<p>" + "<b>" + "Manager Comment: " + "</b>" + comment + "</p>" +
                        "<p>" + "To Check it out, Please visit the link." + "<a href=" + "http://sharepoint.cptu.gov.bd/leavemanagementsystem/_layouts/15/LeaveManagementCPTU/MyRequests.aspx" + ">" + "Click Here" + "</a>" + "<br />" + "</p>" +
                        "Thank You" + "<br />" +
                        "SharePoint Server" + "<br />" +
                    //"Email : " + emailTextBox.Text +
                    "</p>" +

                "</body>" +
                "</html>";
            }

            return emailBody;
        }



    }
}
