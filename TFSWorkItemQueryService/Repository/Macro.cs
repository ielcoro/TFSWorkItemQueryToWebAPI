using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TFSWorkItemQueryService.Repository
{
    public class Macro
    {
        public Macro (string name)
	    {
            this.Name = name;
	    }

        public string Name { get; set; }
    }
}
