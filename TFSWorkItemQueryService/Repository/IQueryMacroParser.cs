using Microsoft.TeamFoundation.WorkItemTracking.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TFSWorkItemQueryService.Repository
{
    public interface IQueryMacroParser
    {
        QueryDefinition Replace(QueryDefinition definition);
        IEnumerable<IMacro> Macros { get; }
    }
}
