using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.QualityTools.Testing.Fakes;
using Microsoft.TeamFoundation.WorkItemTracking.Client;
using Microsoft.TeamFoundation.WorkItemTracking.Client.Fakes;
using TFSWorkItemQueryService.Repository;

namespace UnitTests.Fakes
{
    public class FakeTfsContext : ITfsContext
    {
        private List<WorkItem> workItems;

        private ShimWorkItemStore workItemStore;

        public FakeTfsContext(IDisposable shimsContext)
        {
            if (shimsContext == null) throw new ArgumentNullException();

            this.workItems = new List<WorkItem>();
            workItemStore = new ShimWorkItemStore();

            workItemStore.GetWorkItemInt32 = (id) => workItems.Where(w => w.Id == id).SingleOrDefault();
        }

        public WorkItemStore CurrentWorkItemStore
        {
            get { return workItemStore; }
        }

        public string CurrentProject { get { return "\"TestProject\""; } }

        public string CurrentUser { get { return "\"Iñaki Elcoro\""; } }

        public void AddWorkItem(WorkItem workItem)
        {
            this.workItems.Add(workItem);
        }

    }
}
