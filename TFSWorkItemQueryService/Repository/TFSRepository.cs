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
        private IQueryFinder queryFinder;

        public TFSRepository(IQueryFinder queryFinder)
        {
            this.queryFinder = queryFinder;
        }

        public IEnumerable<WorkItem> Run(string project, string queryPath, string queryName)
        {
            queryFinder.FindQuery(project, queryPath, queryName);

            return null;
        }
    }
}