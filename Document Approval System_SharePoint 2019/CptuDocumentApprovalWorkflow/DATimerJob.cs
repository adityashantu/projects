using CPTUDocumentApprovalWorkflow.Service;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Administration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPTUDocumentApprovalWorkflow
{
    public class DATimerJob : SPJobDefinition
    {
        public DATimerJob() : base()
        {
        }

        public DATimerJob(string jobName, SPService service) : base(jobName, service, null, SPJobLockType.None)
        {
            this.Title = "DA Timer Job";
        }


        public DATimerJob(string jobName, SPWebApplication webapp) : base(jobName, webapp, null, SPJobLockType.ContentDatabase)
        {
            this.Title = "DA Timer Job";
        }

        public override void Execute(Guid contentDbId)
        {
            try {
                //For Root Site
                SPWebApplication webApp = this.Parent as SPWebApplication;
                //string webUrl = webApp.Sites[0].RootWeb.Url;
                //For Sub Site
                string subSiteName = "/dms";
                string webUrl = webApp.Sites[0].RootWeb.Url + subSiteName;

                SPList taskList = webApp.Sites[0].RootWeb.Lists["TimerLists"];
                SPListItem newTask = taskList.Items.Add();
                newTask["Title"] = "Job runs at " + DateTime.Now.ToString();
                newTask.Update();
                
                TimerJobService timerJobService = new TimerJobService();
                timerJobService.AutoApprovalFunc(webUrl);
            }
            catch (Exception ex) {
            }
        }
    }
}
