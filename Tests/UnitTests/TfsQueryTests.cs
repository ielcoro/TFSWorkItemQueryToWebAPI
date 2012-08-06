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
    public class TfsQueryTests
    {
        [TestMethod]
        public void QueryRepositoryShouldLookUpInWorkItemStoreForQuery()
        {
            using (ShimsContext.Create())
            {
                string project = "SampleProject";
                string queryPath = "SamplePath/Sample/";
                string queryName = "Sample Query";

                var queryFinder = A.Fake<IQueryFinder>();
                var queryDefinition = new ShimQueryDefinition();
                var queryRunner = A.Dummy<IQueryRunner>();

                queryDefinition.QueryTextGet = () => "SELECT System.ID, System.Title from workitems";

                A.CallTo(() => queryFinder.FindQuery(project, queryPath, queryName))
                 .Returns(queryDefinition);

                var repository = new TFSRepository(queryFinder, queryRunner);

                repository.Run(project, queryPath, queryName);

                A.CallTo(() => queryFinder.FindQuery(project, queryPath, queryName))
                 .MustHaveHappened();
            }
        }

        [TestMethod]
        public void QueryRepositoryShouldReturnWorkItemsForSpecifiedQuery()
        {
            using (ShimsContext.Create())
            {
                string project = "SampleProject";
                string queryPath = "SamplePath/Sample";
                string queryName = "Sample Query";
                int desiredWorkItems = 5;

                var queryFinder = A.Fake<IQueryFinder>();
                var queryDefinition = new ShimQueryDefinition();
                var queryRunner = A.Fake<IQueryRunner>();

                queryDefinition.QueryTextGet = () => "SELECT System.ID, System.Title from workitems";

                A.CallTo(() => queryFinder.FindQuery(project, queryPath, queryName))
                 .Returns(queryDefinition);

                A.CallTo(() => queryRunner.RunQuery(A<QueryDefinition>.Ignored))
                 .Returns(CreateWorkItems(desiredWorkItems));

                var repository = new TFSRepository(queryFinder, queryRunner);

                IEnumerable<WorkItem> workItems = repository.Run(project, queryPath, queryName);

                A.CallTo(() => queryRunner.RunQuery(A<QueryDefinition>.Ignored)).MustHaveHappened();

                Assert.AreEqual(desiredWorkItems, workItems.Count());
            }
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
