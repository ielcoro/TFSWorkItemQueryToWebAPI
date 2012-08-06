using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.TeamFoundation.WorkItemTracking.Client;

namespace TFSWorkItemQueryService.Repository
{
    public class QueryRunner : IQueryRunner
    {
        private ITfsContext currentContext;

        public QueryRunner(ITfsContext currentContext)
        {
            this.currentContext = currentContext;
        }

        public IEnumerable<WorkItem> RunQuery(QueryDefinition queryDefinition)
        {
            var query = new Query(currentContext.CurrentWorkItemStore, queryDefinition.QueryText);

            WorkItemCollection workItems = query.RunQuery();

            return workItems.OfType<WorkItem>();
        }
    }
}