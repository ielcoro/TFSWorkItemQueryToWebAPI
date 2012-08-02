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
        const string TFS_ADDRESS = "http://srvfundation:8080/tfs";
        const string QUERY_PATH = "EquipoIE/Shared Queries/Ormazabal/Monitorización de Celdas/Current Sprint/";
        const string QUERY_NAME = "Sprint Backlog";

        [TestMethod]
        public void ExecuteExistingQuery()
        {
            var tfsServer = new TfsTeamProjectCollection(new Uri(TFS_ADDRESS));

            tfsServer.ClientCredentials = new TfsClientCredentials(new WindowsCredential());
            tfsServer.Connect(Microsoft.TeamFoundation.Framework.Common.ConnectOptions.None);

            var workItemStore = tfsServer.GetService<WorkItemStore>();

            var item = GetQuery(QUERY_PATH, QUERY_NAME, workItemStore.Projects["EquipoIE"].QueryHierarchy);

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
