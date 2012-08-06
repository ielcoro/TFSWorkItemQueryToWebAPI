using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FakeItEasy;
using TFSWorkItemQueryService.Repository;
using System.Collections.Generic;
using Microsoft.TeamFoundation.WorkItemTracking.Client;
using Microsoft.TeamFoundation.Client;

namespace UnitTests
{
    [TestClass]
    public class TfsQueryTests
    {
        [TestMethod]
        public void TfsQueryExecutorUsesConnectionManagerToRunQueries()
        {
            var connectionManager = A.Fake<ITfsConnectionManager>();
            A.CallTo(() => connectionManager.Connect()).Returns(A.Fake<IConnection>());

            var queryRunner = new QueryRunner(connectionManager);

            IEnumerable<WorkItem> workItems = queryRunner.Run("*");

            A.CallTo(() => connectionManager.Connect()).MustHaveHappened();
        }
    }
}
