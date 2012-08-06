using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.TeamFoundation.WorkItemTracking.Client;

namespace TFSWorkItemQueryService.Repository
{
    public interface IQueryFinder
    {
        QueryDefinition FindQuery(string project, string queryPath, string queryName);
    }
}
