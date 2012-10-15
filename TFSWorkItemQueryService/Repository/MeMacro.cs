using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TFSWorkItemQueryService.Repository
{
    public class MeMacro
        : IMacro
    {
        private ITfsContext tfsContext;

        public MeMacro(ITfsContext tfsContext)
        {
            this.tfsContext = tfsContext;
        }

        public string Name { get { return "Me"; } }

        public string GetValue()
        {
            return this.tfsContext.CurrentUser;
        }

    }
}
