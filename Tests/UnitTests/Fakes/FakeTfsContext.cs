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
        private WorkItemStore workItemStore;

        public FakeTfsContext(IDisposable shimsContext)
        {
            if (shimsContext == null) throw new ArgumentNullException();

            workItemStore = new ShimWorkItemStore();
        }

        public WorkItemStore CurrentWorkItemStore
        {
            get { return workItemStore; }
        }
    }
}
