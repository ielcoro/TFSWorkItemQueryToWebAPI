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
            tfsServer.Connect(Microsoft.TeamFoundation.Framework.Common.ConnectOptions.None);

            var workItemStore = tfsServer.GetService<WorkItemStore>();

            var item = GetQuery(Properties.Settings.Default.QueryPath, Properties.Settings.Default.QueryName, workItemStore.Projects["EquipoIE"].QueryHierarchy);

            var text = ((QueryDefinition)item).QueryText;

            var query = new Query(workItemStore, text);

            var results = query.RunLinkQuery();
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
