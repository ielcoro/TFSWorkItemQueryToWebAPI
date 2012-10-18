using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TFSWorkItemQueryService;
using TFSWorkItemQueryService.Controllers;
using Microsoft.Practices.Unity;
using TFSWorkItemQueryService.Repository;
using System.Linq;

namespace IntegrationTests
{
    [TestClass]
    public class RunQueryControllerTests
    {
        [TestMethod]
        public void UnityInitializesRunQueryController()
        {
            Bootstrapper.Initialise();

            var queryController = Bootstrapper.Container.Resolve<QueryController>();

            Assert.IsNotNull(queryController);
        }

        [TestMethod]
        public void UnityInitializesMacroList()
        {
            Bootstrapper.Initialise();

            var macroParser = Bootstrapper.Container.Resolve<IQueryMacroParser>();

            Assert.IsNotNull(macroParser);
            Assert.AreEqual(2, macroParser.Macros.Count());
            Assert.IsTrue(macroParser.Macros.Where(x => x is MeMacro).Any());
            Assert.IsTrue(macroParser.Macros.Where(x => x is ProjectMacro).Any());
        }

        [TestMethod]
        public void RunQueryControllerTestReturnsWorkItems()
        {
            Assert.Fail("Not Tested");    
        }
    }
}
