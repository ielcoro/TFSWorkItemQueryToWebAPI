using Microsoft.TeamFoundation.Client;
using Microsoft.TeamFoundation.WorkItemTracking.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TFSWorkItemQueryService.Repository
{
    public class TfsContext : ITfsContext, IDisposable
    {
        TfsTeamProjectCollection tfsServer;

        public TfsContext()
        {
            tfsServer = new TfsTeamProjectCollection(new Uri(Properties.Settings.Default.TfsAddress));

            tfsServer.ClientCredentials = new TfsClientCredentials(new WindowsCredential());

            this.CurrentWorkItemStore = tfsServer.GetService<WorkItemStore>();
        }

        public WorkItemStore CurrentWorkItemStore { get; private set; }

        public string CurrentUser
        {
            get { return tfsServer.AuthorizedIdentity.DisplayName; }
        }

        public void Dispose()
        {
            if (tfsServer != null)
                tfsServer.Dispose();
        }
    }
}
