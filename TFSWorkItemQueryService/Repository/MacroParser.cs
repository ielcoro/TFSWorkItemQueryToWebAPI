using Microsoft.TeamFoundation.WorkItemTracking.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace TFSWorkItemQueryService.Repository
{
    public class MacroParser : IQueryMacroParser
    {
        public IEnumerable<IMacro> Macros { get; private set; }

        public MacroParser(IEnumerable<IMacro> macros)
        {
            this.Macros = macros;
        }

        public QueryDefinition Replace(QueryDefinition definition)
        {
            string queryText = definition.QueryText;

            foreach (var macro in Macros)
            {
                queryText = queryText.Replace("@" + macro.Name, macro.GetValue(definition));
                queryText = Regex.Replace(queryText, "@" + macro.Name, macro.GetValue(definition),RegexOptions.IgnoreCase);
            }

            return new QueryDefinition(definition.Name, queryText);
        }
    }
}
