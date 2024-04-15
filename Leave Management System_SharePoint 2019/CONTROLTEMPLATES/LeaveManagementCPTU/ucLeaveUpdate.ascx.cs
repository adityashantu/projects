using LeaveManagementCPTU.Helpers;
using LeaveManagementCPTU;
using Microsoft.SharePoint;
//-------
using Microsoft.SharePoint.Administration.DatabaseProvider;
using Microsoft.SharePoint.Utilities;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Linq;
using System.Collections.Generic;
//
using LeaveManagementCPTU.Service;
using LeaveManagementCPTU.Datas;

/*namespace LeaveManagementCPTU.CONTROLTEMPLATES.LeaveManagementCPTU
{
    public partial class ucLeaveUpdate : UserControl
    {
        string webUrl = SPContext.Current.Web.Url;
        string currentUserEmail = SPContext.Current.Web.CurrentUser.Email;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                using (LeaveManagementCPTUDataContext objDataContext = new LeaveManagementCPTUDataContext(SPContext.Current.Web.Url))
                {
                    
                    if (AdminCheck.Visible == false)
                    {

                        var Check = objDataContext.LMAdmin.FirstOrDefault(x => x.Title.Equals(currentUserEmail));
                        if (Check != null)
                        {
                            AdminCheck.Visible = true;

                            
                            List<LMLeaves> leaveUpdate = new List<LMLeaves>();
                            leaveUpdate = objDataContext.LMLeave.ToList();
                            
                            int earnedLeave = 0;
                            int medLeave = 0;

                            foreach (var item in leaveUpdate)
                            {
                                if (item.Title.Equals("MedicalLeaves"))
                                {
                                    medLeave = Convert.ToInt32(item.Days);
                                }
                                else if (item.Title.Equals("EarnedLeaveIncrease"))
                                {
                                    earnedLeave = Convert.ToInt32(item.Days);
                                }
                            }

                            List<LMEmployeeLeavesMains> myRequestList = new List<LMEmployeeLeavesMains>();
                            myRequestList = objDataContext.LMEmployeeLeavesMain.ToList();

                            DateTime dt = DateTime.Now;

                            if (dt.Month == 1)
                            {
                                foreach (var item in myRequestList)
                                {
                                    item.MedicalTotalLeave = medLeave;
                                    item.MedicalLeaveTaken = 0;
                                }
                            }

                            foreach (var item in myRequestList)
                            {
                                if (item.CasualTotalLeave < (90 - (earnedLeave - 1)))
                                {
                                    item.CasualTotalLeave += earnedLeave;
                                }
                                else if (item.CasualTotalLeave == (90 - (earnedLeave - 1)))
                                {
                                    item.CasualTotalLeave += (earnedLeave - 1);
                                }
                            }

                            objDataContext.SubmitChanges();
                            
                        }
                        else
                        {
                            lblErrMsg.Text = "You Can Not View This Data... You may not be a Admin... If you think this message is wrong, Please notify your SharePoint Admin... ";
                        }
                    }
                }
            }
            
        }
    }
}*/




namespace LeaveManagementCPTU.CONTROLTEMPLATES.LeaveManagementCPTU
{
    public partial class ucLeaveUpdate : UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            using (LeaveManagementCPTUDataContext objDataContext = new LeaveManagementCPTUDataContext(SPContext.Current.Web.Url))
            {
                List<LMLeaves> leaveUpdate = new List<LMLeaves>();
                leaveUpdate = objDataContext.LMLeave.ToList();

                int earnedLeave = 0;
                int medLeave = 0;

                foreach (var item in leaveUpdate)
                {
                    if (item.Title.Equals("MedicalLeaves"))
                    {
                        medLeave = Convert.ToInt32(item.Days);
                    }
                    else if (item.Title.Equals("EarnedLeaveIncrease"))
                    {
                        earnedLeave = Convert.ToInt32(item.Days);
                    }
                }

                List<LMEmployeeLeavesMains> myRequestList = new List<LMEmployeeLeavesMains>();
                myRequestList = objDataContext.LMEmployeeLeavesMain.ToList();

                DateTime dt = DateTime.Now;

                if (dt.Month == 1)
                {
                    foreach (var item in myRequestList)
                    {
                        item.MedicalTotalLeave = medLeave;
                        item.MedicalLeaveTaken = 0;
                    }
                }

                foreach (var item in myRequestList)
                {
                    if (item.CasualTotalLeave < (90 - (earnedLeave - 1)))
                    {
                        item.CasualTotalLeave += earnedLeave;
                    }
                    else if (item.CasualTotalLeave == (90 - (earnedLeave - 1)))
                    {
                        item.CasualTotalLeave += (earnedLeave - 1);
                    }
                }

                objDataContext.SubmitChanges();
            }
        }
    }
}
