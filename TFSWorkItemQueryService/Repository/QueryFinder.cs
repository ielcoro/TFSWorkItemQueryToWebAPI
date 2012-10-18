using Microsoft.TeamFoundation.WorkItemTracking.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TFSWorkItemQueryService.Repository
{
    public class QueryFinder : IQueryFinder
    {
        private ITfsContext tfsContext;

        public QueryFinder(ITfsContext tfsContext)
        {
            this.tfsContext = tfsContext;
        }
        
        public QueryDefinition FindQuery(string project, string queryPath, string queryName)
        {
            if (!this.tfsContext.CurrentWorkItemStore.Projects.Contains(project)) throw new ArgumentException("Project: " + project + " does not exist", "project");

            Project currentProject = this.tfsContext.CurrentWorkItemStore.Projects[project];

            string fullQueryPath = project + "/" + queryPath;

            return GetQuery(fullQueryPath, queryName, currentProject.QueryHierarchy) as QueryDefinition;
        }

        private QueryItem GetQuery(string path, string name, IEnumerable<QueryItem> queryItems)
        {
            if (queryItems.Where(x => x.Name == name && x.Path.Contains(path)).Count() == 1)
                return queryItems.Where(x => x.Name == name).Single();
            else
            {
                var nextNode = (from q in queryItems.OfType<QueryFolder>()
                                where path.Contains(q.Path)
                                select q).SingleOrDefault();

                return GetQuery(path, name, nextNode);
            }
        }
    }
}
