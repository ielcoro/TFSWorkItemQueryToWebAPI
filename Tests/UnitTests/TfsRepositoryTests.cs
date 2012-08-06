using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FakeItEasy;
using TFSWorkItemQueryService.Repository;
using System.Collections.Generic;
using Microsoft.TeamFoundation.WorkItemTracking.Client;
using Microsoft.TeamFoundation.Client;
using System.Linq;
using Microsoft.QualityTools.Testing.Fakes;
using Microsoft.TeamFoundation.WorkItemTracking.Client.Fakes;
using TFSWorkItemQueryService;

namespace UnitTests
{
    [TestClass]
    public class TfsRepositoryTests
    {
        private const string project = "SampleProject";
        private const string queryPath = "SamplePath/Sample/";
        private const string queryName = "Sample Query";

        IQueryFinder queryFinder;
        ShimQueryDefinition queryDefinition;
        IQueryRunner queryRunner;
        IDisposable shimContext;

        [TestInitialize]
        public void Initialize()
        {
            shimContext = ShimsContext.Create();
            queryFinder = A.Fake<IQueryFinder>();
            queryDefinition = new ShimQueryDefinition();
            queryRunner = A.Fake<IQueryRunner>();

            queryDefinition.QueryTextGet = () => "SELECT System.ID, System.Title from workitems";

            A.CallTo(() => queryFinder.FindQuery(project, queryPath, queryName))
                .Returns(queryDefinition);

        }

        [TestCleanup]
        public void CleanUp()
        {
            shimContext.Dispose();
        }

        [TestMethod]
        public void QueryRepositoryShouldLookUpInWorkItemStoreForQuery()
        {

            var repository = new TFSRepository(queryFinder, queryRunner);

            repository.Run(project, queryPath, queryName);

            A.CallTo(() => queryFinder.FindQuery(project, queryPath, queryName))
             .MustHaveHappened();
        }

        [TestMethod]
        public void QueryRepositoryShouldReturnWorkItemsForSpecifiedQuery()
        {
            int desiredWorkItems = 5;

            A.CallTo(() => queryRunner.RunQuery(A<QueryDefinition>.Ignored))
             .Returns(CreateWorkItems(desiredWorkItems));

            var repository = new TFSRepository(queryFinder, queryRunner);

            IEnumerable<WorkItem> workItems = repository.Run(project, queryPath, queryName);

            A.CallTo(() => queryRunner.RunQuery(A<QueryDefinition>.Ignored)).MustHaveHappened();

            Assert.AreEqual(desiredWorkItems, workItems.Count());
        }

        private IEnumerable<WorkItem> CreateWorkItems(int desiredWorkItems)
        {
            List<WorkItem> workItems = new List<WorkItem>();

            for (int i = 0; i < desiredWorkItems; i++)
            {
                workItems.Add(new ShimWorkItem());
            }

            return workItems;
        }
    }
}
