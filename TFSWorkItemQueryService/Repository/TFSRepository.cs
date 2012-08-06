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
        private ITfsConnectionManager connectionManager;

        public TFSRepository(ITfsConnectionManager connectionManager)
        {
            // TODO: Complete member initialization
            this.connectionManager = connectionManager;
        }
    }
}