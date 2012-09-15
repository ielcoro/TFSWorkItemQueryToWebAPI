using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.TeamFoundation.Client;
using Microsoft.TeamFoundation.WorkItemTracking.Client;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TFSApiTests
{
    [TestClass]
    public class QueryExecutionTests
    {
        [TestMethod]
        public void ExecuteExistingQuery()
        {

            var tfsServer = new TfsTeamProjectCollection(new Uri(Properties.Settings.Default.TfsAddress));

            tfsServer.ClientCredentials = new TfsClientCredentials(new WindowsCredential());
 
            var workItemStore = tfsServer.GetService<WorkItemStore>();

            var query = GetRunnableQuery(workItemStore);

            List<WorkItem> workItems = Run(workItemStore, query);

            Assert.IsTrue(workItems.Any());
        }

        private Query GetRunnableQuery(WorkItemStore workItemStore)
        {
            var item = GetQuery(Properties.Settings.Default.QueryPath, Properties.Settings.Default.QueryName, workItemStore.Projects[Properties.Settings.Default.TeamProject].QueryHierarchy);

            var text = ((QueryDefinition)item).QueryText;

            var query = new Query(workItemStore, ReplaceMacros(text));
            return query;
        }

        private List<WorkItem> Run(WorkItemStore workItemStore, Query query)
        {
            List<WorkItem> workItems;
            if (query.IsLinkQuery)
            {
                workItems = RunLinkQuery(workItemStore, query);
            }
            else
            {
                workItems = RunQuery(query);
            }
            return workItems;
        }

        private List<WorkItem> RunQuery(Query query)
        {
            var queryResults = query.RunQuery();

            return queryResults.Cast<WorkItem>().ToList();
        }

        private List<WorkItem> RunLinkQuery(WorkItemStore workItemStore, Query query)
        {
            var queryResults = query.RunLinkQuery();

            return queryResults.Select(i => workItemStore.GetWorkItem(i.TargetId)).ToList();
        }

        private TfsTeamProjectCollection ConnectToTfs()
        {
            var tfsServer = new TfsTeamProjectCollection(new Uri(Properties.Settings.Default.TfsAddress));

            tfsServer.ClientCredentials = new TfsClientCredentials(new WindowsCredential());
            tfsServer.Connect(Microsoft.TeamFoundation.Framework.Common.ConnectOptions.None);
            return tfsServer;
        }

        private string ReplaceMacros(string queryText)
        {
            return queryText.Replace("@project", "'" + Properties.Settings.Default.TeamProject + "'");
        }

        private QueryItem GetQuery(string path, string name, IEnumerable<QueryItem> queryItems)
        {
            if (queryItems.Where(x => x.Name == name).Count() == 1)
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
