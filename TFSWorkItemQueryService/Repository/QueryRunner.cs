using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.TeamFoundation.WorkItemTracking.Client;

namespace TFSWorkItemQueryService.Repository
{
    public class QueryRunner : IQueryRunner
    {

        public IEnumerable<WorkItem> RunQuery(QueryDefinition queryDefinition)
        {
            throw new NotImplementedException();
        }
    }
}