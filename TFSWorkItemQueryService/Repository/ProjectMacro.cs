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
        private ITfsContext tfsContext;

        public ProjectMacro(ITfsContext tfsContext)
            : base("Project")
        {
            this.tfsContext = tfsContext;
        }
        
        public override string GetValue(QueryDefinition definition)
        {
            return tfsContext.CurrentProject;
        }
    }
}
