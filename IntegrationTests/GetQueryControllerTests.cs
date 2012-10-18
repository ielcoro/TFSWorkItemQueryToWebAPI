using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TFSWorkItemQueryService;
using TFSWorkItemQueryService.Controllers;
using Microsoft.Practices.Unity;
using TFSWorkItemQueryService.Repository;
using System.Linq;
using System.Collections.Generic;
using Microsoft.TeamFoundation.WorkItemTracking.Client;
using System.Net.Http;
using System.Net;
using System.Threading.Tasks;

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
                "Proteger con contraseña el directorio de distribución de ClickOnce",
                "Clon del repositorio en srvdesarrollo",
                "Crear manual de instalación",
                "Crear manual de usuario", 
                "Envio de E-mail al generar pedido",
                "Vaciar el carrito de la compra",
                "Cambio de Contraseña"
            };

            var queryController = Bootstrapper.Container.Resolve<QueryController>();

            IEnumerable<WorkItem> workItems = queryController.Run("EquipoIE", "Shared Queries/Erka - Extranet Clientes/", "Product Backlog");

            CollectionAssert.AreEqual(workItems.Select(x => x.Title).ToList(), expectedWorkItems);
        }

        [TestMethod]
        public async Task TestRunQueryFromClientSide()
        {
            var client = new HttpClient();

            var path = WebUtility.UrlEncode("Shared Queries/Erka - Extranet Clientes/");
            var name = WebUtility.UrlEncode("Product Backlog");

            string xml = await client.GetStringAsync(String.Format("http://localhost:8035/api/query/run?teamProject={0}&path={1}&queryName={2}", "EquipoIE", path, name));
        }
    }
}
