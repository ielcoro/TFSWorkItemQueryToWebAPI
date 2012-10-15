using Microsoft.TeamFoundation.WorkItemTracking.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TFSWorkItemQueryService.Repository
{
    public class QueryFinder : IQueryFinder
    {
        private ITfsContext tfsContextMock;

        public QueryFinder(ITfsContext tfsContextMock)
        {
            // TODO: Complete member initialization
            this.tfsContextMock = tfsContextMock;
        }
        public QueryDefinition FindQuery(string project, string queryPath, string queryName)
        {
            throw new NotImplementedException();
        }
    }
}
