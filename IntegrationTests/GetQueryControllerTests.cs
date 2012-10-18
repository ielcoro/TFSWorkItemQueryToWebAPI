using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TFSWorkItemQueryService;
using TFSWorkItemQueryService.Controllers;
using Microsoft.Practices.Unity;
using TFSWorkItemQueryService.Repository;
using System.Linq;
using System.Collections.Generic;
using Microsoft.TeamFoundation.WorkItemTracking.Client;

namespace IntegrationTests
{
    [TestClass]
    public class RunQueryControllerTests
    {
        [TestInitialize]
        public void Initialize()
        {
            Bootstrapper.Initialise();
        }

        [TestMethod]
        public void UnityInitializesRunQueryController()
        {
            var queryController = Bootstrapper.Container.Resolve<QueryController>();

            Assert.IsNotNull(queryController);
        }

        [TestMethod]
        public void UnityInitializesMacroList()
        {
            var macroParser = Bootstrapper.Container.Resolve<IQueryMacroParser>();

            Assert.IsNotNull(macroParser);
            Assert.AreEqual(2, macroParser.Macros.Count());
            Assert.IsTrue(macroParser.Macros.Where(x => x is MeMacro).Any());
            Assert.IsTrue(macroParser.Macros.Where(x => x is ProjectMacro).Any());
        }

        [TestMethod]
        public void RunQueryControllerTestReturnsWorkItems()
        {
            var expectedWorkItems = new List<string>()
            {
                "Proteger con contraseña el directio de distribución de ClickOnce",
                "Clon del repositorio en srvdesarrollo",
                "Crear manual de instalación",
                "Crear manual de usuario", 
                "Envio de E-mail al generar el pedido",
                "Vaciar el carrito de la compra",
                "Cambio de Contraseña"
            };
            var queryController = Bootstrapper.Container.Resolve<QueryController>();

            IEnumerable<WorkItem> workItems = queryController.Run("EquipoIE", "Shared Queries/Erka - Extranet Clientes/", "Product Backlog");

            CollectionAssert.AreEqual(workItems.Select(x => x.Title).ToList(), expectedWorkItems);
        }
    }
}
