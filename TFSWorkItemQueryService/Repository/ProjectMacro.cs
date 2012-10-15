using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TFSWorkItemQueryService.Repository
{
    public class ProjectMacro
        : IMacro
    {
        public string GetValue()
        {
            return null;
        }

        public string Name
        {
            get
            {
                return "Project";
            }
        }
    }
}
