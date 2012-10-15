using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TFSWorkItemQueryService.Repository
{
    public class UserMacro
        : IMacro
    {
        private ITfsContext tfsContext;

        public UserMacro(ITfsContext tfsContext)
        {
            this.tfsContext = tfsContext;
        }

        public string Name { get { return "User"; } }

        public string GetValue()
        {
            return this.tfsContext.CurrentUser;
        }

    }
}
