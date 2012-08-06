using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FakeItEasy;
using TFSWorkItemQueryService.Repository;
using System.Collections.Generic;
using Microsoft.TeamFoundation.WorkItemTracking.Client;
using Microsoft.TeamFoundation.Client;
using System.Linq;

namespace UnitTests
{
    [TestClass]
    public class TfsQueryTests
    {
        [TestMethod]
        public void PassingAQueryPathAndAQueryNameShouldReturnOneWorkItem()
        {
            string queryPath = "/Sample/TestQuery";
            string queryName = "One Work Item";

            var repository = new TFSRepository();

            IEnumerable<WorkItem> workItems = repository.Run(queryPath, queryName);

            Assert.IsTrue(workItems.Count() == 1);
        }

    }
}
