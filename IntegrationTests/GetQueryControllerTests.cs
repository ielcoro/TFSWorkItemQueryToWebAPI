using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TFSWorkItemQueryService;
using TFSWorkItemQueryService.Controllers;
using Microsoft.Practices.Unity;

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
        public void RunQueryControllerTestReturnsWorkItems()
        {
            Assert.Fail("Not Tested");    
        }
    }
}
