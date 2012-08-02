using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.TeamFoundation.Client;
using Microsoft.TeamFoundation.WorkItemTracking.Client;

namespace TFSWorkItemQueryService.Repository
{
    public class TFSRepository
    {
        private TfsTeamProjectCollection Connect(Uri tfsAddress)
        {
            var tfsServer = new TfsTeamProjectCollection(tfsAddress);

            tfsServer.ClientCredentials = new TfsClientCredentials(new WindowsCredential());
            tfsServer.Connect(Microsoft.TeamFoundation.Framework.Common.ConnectOptions.IncludeServices);

            return tfsServer;
        }

        private IEnumerable<WorkItemLink> ExecuteQuery(Uri tfsAddress, string teamProject, string workItemQuery)
        {
            var tfsServer = Connect(tfsAddress);

            var workItemStore = tfsServer.GetService<WorkItemStore>();

            
        }
    }
}