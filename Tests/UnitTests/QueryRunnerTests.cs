using System;
using System.Collections.Generic;
using Microsoft.TeamFoundation.WorkItemTracking.Client;
using Microsoft.TeamFoundation.WorkItemTracking.Client.Fakes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TFSWorkItemQueryService.Repository;
using System.Linq;
using Microsoft.QualityTools.Testing.Fakes;

namespace UnitTests
{
    [TestClass]
    public class QueryRunnerTests
    {
        ShimQueryDefinition queryDefinition;
        IDisposable shimContext;

        [TestInitialize]
        public void Initialize()
        {
            shimContext = ShimsContext.Create();

            queryDefinition = new ShimQueryDefinition();
            queryDefinition.QueryTextGet = () => "SELECT System.ID, System.Title from workitems";
        }

        [TestCleanup]
        public void CleanUp()
        {
            shimContext.Dispose();
        }

        [TestMethod]
        public void QueryRunFromQueryDefinitonShouldReturnWorkItems()
        {
            var queryRunner = new QueryRunner();

            IEnumerable<WorkItem> workItems = queryRunner.RunQuery(queryDefinition);

            Assert.IsTrue(workItems.Any());
        }
    }
}
