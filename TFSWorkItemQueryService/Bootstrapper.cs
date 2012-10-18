using System.Web.Http;
using Microsoft.Practices.Unity;
using TFSWorkItemQueryService.Repository;

namespace TFSWorkItemQueryService
{
    public static class Bootstrapper
    {
        public static void Initialise()
        {
            BuildUnityContainer();

            GlobalConfiguration.Configuration.DependencyResolver = new Unity.WebApi.UnityDependencyResolver(Container);
        }

        private static IUnityContainer BuildUnityContainer()
        {
            Container = new UnityContainer();

            // register all your components with the container here
            // e.g. container.RegisterType<ITestService, TestService>();            

            Container.RegisterType<IQueryFinder, QueryFinder>();
            Container.RegisterType<IQueryRunner, QueryRunner>();
            Container.RegisterType<ITfsContext, TfsContext>(new HierarchicalLifetimeManager());
            Container.RegisterType<IQueryMacroParser, MacroParser>();

            return Container;
        }

        public static UnityContainer Container { get; private set; }
    }
}