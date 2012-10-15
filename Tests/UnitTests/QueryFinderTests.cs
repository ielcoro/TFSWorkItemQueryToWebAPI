using FakeItEasy;
using Microsoft.QualityTools.Testing.Fakes;
using Microsoft.TeamFoundation.WorkItemTracking.Client;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TFSWorkItemQueryService.Repository;
using UnitTests.Fakes;

namespace UnitTests
{
    [TestClass]
    public class QueryFinderTests
    {
        IDisposable shimsContext;
        FakeTfsContext fakeTfsContext;

        [TestInitialize]
        public void Initialize()
        {
            shimsContext = ShimsContext.Create();

            fakeTfsContext = new FakeTfsContext(shimsContext);
        }

        [TestCleanup]
        public void CleanUp()
        {
            shimsContext.Dispose();
        }

        [TestMethod]
        public void QueryFinderReturnsQueryNavigatingFoldersInsideHierarchy()
        {
            string expectedQuery = "SELECT System.ID, System.Title from workitems";
            string expectedQueryName = "TestQuery";
            string expectedQueryFolderName = "TestFolder";

            var finder = new QueryFinder(fakeTfsContext);
            QueryDefinition actual = finder.FindQuery("TestProject", "TestFolder", "TestQuery");

            Assert.AreEqual(expectedQuery, actual.QueryText);
            Assert.AreEqual(expectedQueryName, actual.Name);
            Assert.AreEqual(expectedQueryFolderName, actual.Parent.Name);
        }   
    }
}
