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

            ShimWorkItemCollection collection = SetupWorkItemCollection(workItem);

            ShimQuery.AllInstances.IsLinkQueryGet = (q) => false;
            ShimQuery.AllInstances.RunQuery = (q) => collection;
            ShimQuery.AllInstances.RunLinkQuery = (q) => new List<WorkItemLinkInfo>() { workItemLinkInfo }.ToArray();
        }

        private ShimWorkItemCollection SetupWorkItemCollection(params ShimWorkItem[] workItems)
        {
            var collection = new ShimWorkItemCollection();

            collection.Bind(workItems.Select(w => w.Instance));

            return collection;
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
            var queryMacroParser = A.Fake<IQueryMacroParser>();

            var queryRunner = new QueryRunner(tfsContextMock, queryMacroParser);

            IEnumerable<WorkItem> workItems = queryRunner.RunQuery(queryDefinition);

            A.CallTo(() => tfsContextMock.CurrentWorkItemStore).MustHaveHappened();
        }

        [TestMethod]
        public void QueryRunShouldTransformLinkedQueries()
        {
            var tfsContextMock = A.Fake<ITfsContext>(o => o.Wrapping(tfsContext));

            ShimQuery.AllInstances.IsLinkQueryGet = (q) => true;
            var queryMacroParser = A.Fake<IQueryMacroParser>();

            var queryRunner = new QueryRunner(tfsContextMock, queryMacroParser);

            IEnumerable<WorkItem> workItems = queryRunner.RunQuery(queryDefinition);

            A.CallTo(() => tfsContextMock.CurrentWorkItemStore).MustHaveHappened();

            Assert.IsTrue(workItems.Where(w => w.Title == "Linked Work Item").Any());
        }

        [TestMethod]
        public void QueryRunShouldRunStandardQueries()
        {
            var tfsContextMock = A.Fake<ITfsContext>(o => o.Wrapping(tfsContext));
            var queryMacroParser = A.Fake<IQueryMacroParser>();

            var queryRunner = new QueryRunner(tfsContextMock, queryMacroParser);

            IEnumerable<WorkItem> workItems = queryRunner.RunQuery(queryDefinition);

            A.CallTo(() => tfsContextMock.CurrentWorkItemStore).MustHaveHappened();

            Assert.IsTrue(workItems.Where(w => w.Title == "Linked Work Item").Any());
        }

        [TestMethod]
        public void QueryRunnerShouldCallToReplaceMacrosBeforeRunningQueries()
        {
            //Maybe is a smeel you can really assert that the code is using the right version of the query?
            var tfsContextMock = A.Fake<ITfsContext>(o => o.Wrapping(tfsContext));
            var macroParserMock = A.Fake<IQueryMacroParser>();

            var resultQuery = new ShimQueryDefinition();

            resultQuery.QueryTextGet = () => "SELECT System.ID, System.Title from workitems WHERE Project = \"TestProject\"";

            A.CallTo(() => macroParserMock.Replace(A<QueryDefinition>.Ignored))
             .Returns(resultQuery);
            
            var query = "SELECT System.ID, System.Title from workitems WHERE Project = @Project";
            var queryRunner = new QueryRunner(tfsContextMock, macroParserMock);

            var queryWithMacros = new ShimQueryDefinition();

            queryWithMacros.QueryTextGet = () => query;

            IEnumerable<WorkItem> workItems = queryRunner.RunQuery(queryDefinition);

            A.CallTo(() => macroParserMock.Replace(A<QueryDefinition>.Ignored)).MustHaveHappened();
            A.CallTo(() => tfsContextMock.CurrentWorkItemStore).MustHaveHappened();

            Assert.IsTrue(workItems.Where(w => w.Title == "Linked Work Item").Any());
        }
    }
}
