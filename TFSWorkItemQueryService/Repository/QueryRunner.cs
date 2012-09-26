using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.TeamFoundation.WorkItemTracking.Client;

namespace TFSWorkItemQueryService.Repository
{
    public class QueryRunner : IQueryRunner
    {
        private ITfsContext currentContext;
        private IQueryMacroParser macroParser;

        public QueryRunner(ITfsContext currentContext, IQueryMacroParser macroParser)
        {
            this.currentContext = currentContext;
            this.macroParser = macroParser;
        }

        public IEnumerable<WorkItem> RunQuery(QueryDefinition queryDefinition)
        {
            QueryDefinition parsedQueryDefinition = macroParser.Replace(queryDefinition);

            var query = new Query(currentContext.CurrentWorkItemStore, parsedQueryDefinition.QueryText);

            if (query.IsLinkQuery)
            {
                var queryResults = query.RunLinkQuery();
                
                return from l in queryResults.OfType<WorkItemLinkInfo>()
                       select currentContext.CurrentWorkItemStore.GetWorkItem(l.TargetId);
            }
            else
            {
                var queryResults = query.RunQuery();
                
                return queryResults.OfType<WorkItem>();
            }
        }
    }
}