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

                queryDefinition.QueryTextGet = () => "SELECT System.ID, System.Title from workitems";

                A.CallTo(() => queryFinder.FindQuery(project, queryPath, queryName))
                 .Returns(queryDefinition);

                var repository = new TFSRepository(queryFinder);

                repository.Run(project, queryPath, queryName);

                A.CallTo(() => queryFinder.FindQuery(project, queryPath, queryName))
                 .MustHaveHappened();
            }
        }

        
    }
}
