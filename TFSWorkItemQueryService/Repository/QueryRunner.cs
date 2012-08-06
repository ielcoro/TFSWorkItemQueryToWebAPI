using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TFSWorkItemQueryService.Repository
{
    public class QueryRunner
    {
        private ITfsConnectionManager connectionManager;

        public QueryRunner(ITfsConnectionManager connectionManager)
        {
            this.connectionManager = connectionManager;
        }

        public IEnumerable<Microsoft.TeamFoundation.WorkItemTracking.Client.WorkItem> Run(string p)
        {
            throw new NotImplementedException();
        }
    }
}
