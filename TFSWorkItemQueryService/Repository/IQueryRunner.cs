using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.TeamFoundation.WorkItemTracking.Client;

namespace TFSWorkItemQueryService
{
    public interface IQueryRunner
    {
        IEnumerable<WorkItem> RunQuery(QueryDefinition queryDefinition);
    }
}
