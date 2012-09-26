using System;
using System.Collections.Generic;
using Microsoft.TeamFoundation.WorkItemTracking.Client;
using Microsoft.TeamFoundation.WorkItemTracking.Client.Fakes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TFSWorkItemQueryService.Repository;
using System.Linq;
using Microsoft.QualityTools.Testing.Fakes;
using UnitTests.Fakes;
using FakeItEasy;

namespace UnitTests
{
    [TestClass]
    public class QueryRunnerTests
    {
        ShimQueryDefinition queryDefinition;
        IDisposable shimContext;
        FakeTfsContext tfsContext;

        [TestInitialize]
        public void Initialize()
        {
            shimContext = ShimsContext.Create();

            tfsContext = new FakeTfsContext(shimContext);
            queryDefinition = new ShimQueryDefinition();
            queryDefinition.QueryTextGet = () => "SELECT System.ID, System.Title from workitems";

            SetupQueryShim(tfsContext);
        }

        /// <summary>
        /// Configures a query shim to simulate Run on TFS Query objects
        /// </summary>
        private void SetupQueryShim(FakeTfsContext tfsContext)
        {
            ShimQuery.ConstructorWorkItemStoreString = (q, ws, wi) =>
            {
                ShimQuery query = new ShimQuery();

                query.QueryStringGet = () => wi;

                q = query;
            };

            var workItem = new ShimWorkItem();
            workItem.IdGet = () => 5;
            workItem.TitleGet = () => "Linked Work Item";

            var workItemLinkInfo = new WorkItemLinkInfo();
            workItemLinkInfo.TargetId = 5;

            tfsContext.AddWorkItem(workItem);
            ShimQuery.AllInstances.IsLinkQueryGet = (q) => true;
            ShimQuery.AllInstances.RunQuery = (q) => new ShimWorkItemCollection();
            ShimQuery.AllInstances.RunLinkQuery = (q) => new List<WorkItemLinkInfo>() {  workItemLinkInfo }.ToArray();
        }

        [TestCleanup]
        public void CleanUp()
        {
            shimContext.Dispose();
        }

        [TestMethod]
        public void QueryRunFromQueryDefinitonShouldCallContext()
        {
            var tfsContextMock = A.Fake<ITfsContext>(x => x.Wrapping(tfsContext));

            var queryRunner = new QueryRunner(tfsContextMock);

            IEnumerable<WorkItem> workItems = queryRunner.RunQuery(queryDefinition);

            A.CallTo(() => tfsContextMock.CurrentWorkItemStore).MustHaveHappened();
        }

        [TestMethod]
        public void QueryRunShouldTransformLinkedQueries()
        {
            var tfsContextMock = A.Fake<ITfsContext>(o => o.Wrapping(tfsContext));

            ShimQuery.AllInstances.IsLinkQueryGet = (q) => true;

            var queryRunner = new QueryRunner(tfsContextMock);

            IEnumerable<WorkItem> workItems = queryRunner.RunQuery(queryDefinition);

            A.CallTo(() => tfsContextMock.CurrentWorkItemStore).MustHaveHappened();

            Assert.IsTrue(workItems.Where(w => w.Title == "Linked Work Item").Any());
        }
    }
}
