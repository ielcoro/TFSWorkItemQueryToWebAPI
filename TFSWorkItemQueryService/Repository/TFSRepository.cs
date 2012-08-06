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

        public IEnumerable<WorkItem> Run(string queryPath, string queryName)
        {
            throw new NotImplementedException();
        }
    }
}