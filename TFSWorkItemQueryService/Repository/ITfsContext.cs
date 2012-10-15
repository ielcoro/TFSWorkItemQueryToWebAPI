using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.TeamFoundation.WorkItemTracking.Client;

namespace TFSWorkItemQueryService.Repository
{
    public interface ITfsContext
    {
        WorkItemStore CurrentWorkItemStore { get; }

        string CurrentProject { get; }

        string CurrentUser { get; }
    }
}
