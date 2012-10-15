using Microsoft.TeamFoundation.WorkItemTracking.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TFSWorkItemQueryService.Repository
{
    public class MeMacro
        : Macro
    {
        private ITfsContext tfsContext;

        public MeMacro(ITfsContext tfsContext)
            : base("Me")
        {
            this.tfsContext = tfsContext;
        }

        public override string GetValue(QueryDefinition definition)
        {
            return this.tfsContext.CurrentUser;
        }
    }
}
