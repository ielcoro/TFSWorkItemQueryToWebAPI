using Microsoft.TeamFoundation.WorkItemTracking.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TFSWorkItemQueryService.Repository
{
    public class MacroParser : IQueryMacroParser
    {
        IEnumerable<IMacro> macros;

        public MacroParser(IEnumerable<IMacro> macros)
        {
            this.macros = macros;
        }

        public QueryDefinition Replace(QueryDefinition definition)
        {
            string queryText = definition.QueryText;

            foreach (var macro in macros)
            {
                queryText = queryText.Replace("@" + macro.Name, macro.GetValue(definition));
            }

            return new QueryDefinition(definition.Name, queryText);
        }
    }
}
