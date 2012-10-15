using Microsoft.TeamFoundation.WorkItemTracking.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TFSWorkItemQueryService.Repository
{
    public class ProjectMacro
        : Macro
    {
        private const string SimpleQuote = "\"";
        private ITfsContext tfsContext;

        public ProjectMacro(ITfsContext tfsContext)
            : base("Project")
        {
            this.tfsContext = tfsContext;
        }
        
        public override string GetValue(QueryDefinition definition)
        {
            return String.Format("{0}{1}{0}", SimpleQuote, definition.Project.Name);
        }
    }
}
