using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TFSWorkItemQueryService.Repository
{
    public class Macro : IMacro
    {
        public Macro (string name)
	    {
            if (String.IsNullOrEmpty(name)) throw new ArgumentException("Name cannot be null or empty", "name");
            this.Name = name;
	    }

        public string Name { get; set; }

        public string GetValue()
        {
            throw new NotImplementedException();
        }
    }
}
