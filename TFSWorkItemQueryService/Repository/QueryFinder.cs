using Microsoft.TeamFoundation.WorkItemTracking.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TFSWorkItemQueryService.Repository
{
    public class QueryFinder : IQueryFinder
    {
        public QueryDefinition FindQuery(string project, string queryPath, string queryName)
        {
            throw new NotImplementedException();
        }
    }
}
