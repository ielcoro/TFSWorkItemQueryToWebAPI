using FakeItEasy;
using Microsoft.QualityTools.Testing.Fakes;
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
        ITfsContext tfsContextMock;

        [TestInitialize]
        public void Initialize()
        {
            shimsContext = ShimsContext.Create();

            var tfsContext = new FakeTfsContext(shimsContext);
            tfsContextMock = A.Fake<ITfsContext>((o) => o.Wrapping(tfsContext));
        }

        [TestCleanup]
        public void CleanUp()
        {
            shimsContext.Dispose();
        }

        [TestMethod]
        public void QueryFinderLooksForQueryUsingCurrentContext()
        {
            var finder = new QueryFinder(tfsContextMock);

            finder.FindQuery("TestProject", "/", "testQuery");

            
        }   
    }
}
