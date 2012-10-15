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
            throw new NotImplementedException();
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
