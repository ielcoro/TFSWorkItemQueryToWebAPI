using Microsoft.TeamFoundation.WorkItemTracking.Client;
using System;
namespace TFSWorkItemQueryService.Repository
{
    public interface IMacro
    {
        string GetValue(QueryDefinition definition);
        string Name { get; }
    }
}
