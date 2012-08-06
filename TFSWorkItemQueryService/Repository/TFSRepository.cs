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
        private ITfsContext context;

        public TFSRepository(ITfsContext context)
        {
            // TODO: Complete member initialization
            this.context = context;
        }
    }
}