using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.TeamFoundation.WorkItemTracking.Client;

namespace TFSWorkItemQueryService.Repository
{
    public class QueryRunner
    {
        private ITfsConnectionManager connectionManager;

        public QueryRunner(ITfsConnectionManager connectionManager)
        {
            this.connectionManager = connectionManager;
        }

        public IEnumerable<WorkItem> Run(string wiql)
        {
            this.connectionManager.Connect();

            return null;
        }
    }
}
