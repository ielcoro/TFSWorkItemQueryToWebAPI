using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.QualityTools.Testing.Fakes;
using Microsoft.TeamFoundation.WorkItemTracking.Client;
using Microsoft.TeamFoundation.WorkItemTracking.Client.Fakes;
using TFSWorkItemQueryService.Repository;

namespace UnitTests.Fakes
{
    public class FakeTfsContext : ITfsContext
    {
        private List<WorkItem> workItems;

        private ShimWorkItemStore workItemStoreFake;

        private ShimProject projectFake;

        private ShimQueryHierarchy hierarchyFake;

        public FakeTfsContext(IDisposable shimsContext)
        {
            if (shimsContext == null) throw new ArgumentNullException();

            this.workItems = new List<WorkItem>();
            workItemStoreFake = new ShimWorkItemStore();

            workItemStoreFake.GetWorkItemInt32 = (id) => workItems.Where(w => w.Id == id).SingleOrDefault();

            CreateProjectCollectionFake();

            CreateQueryHierarchyFake();
        }

        public WorkItemStore CurrentWorkItemStore
        {
            get { return workItemStoreFake; }
        }

        public string CurrentUser { get { return "Iñaki Elcoro"; } }

        #region Fake Tools

        public void AddWorkItem(WorkItem workItem)
        {
            this.workItems.Add(workItem);
        }

        public Project GetCurrentProject()
        {
            return projectFake;
        }

        public QueryHierarchy GetHierarchy()
        {
            return hierarchyFake;
        }

        private void CreateProjectCollectionFake()
        {
            projectFake = new ShimProject();
            projectFake.NameGet = () => "TestProject";

            var projectList = new List<Project>() { projectFake };

            var projectCollectionFake = new ShimProjectCollection();

            projectCollectionFake.Bind(projectList);
            projectCollectionFake.ContainsString = (x) => projectList.Select(p => p.Name).Contains(x);
            projectCollectionFake.ItemGetString = (x) => projectList.FirstOrDefault(p => p.Name == x);

            workItemStoreFake.ProjectsGet = () => projectCollectionFake;
        }

        private void CreateQueryHierarchyFake()
        {
            hierarchyFake = new ShimQueryHierarchy();

            var queryFolderFake = new ShimQueryFolder();
            var queryDefinitionFake = new ShimQueryDefinition();
            
            queryDefinitionFake.QueryTextGet = () => "SELECT System.ID, System.Title from workitems";
            
            var queryList = CreateQueryDefinitionList(queryFolderFake, queryDefinitionFake);

            queryFolderFake = SetupQueryFolderFake(queryFolderFake, queryList);

            hierarchyFake.Bind(new List<QueryFolder>() { queryFolderFake });
            projectFake.QueryHierarchyGet = () => hierarchyFake;
        }

        private ShimQueryFolder SetupQueryFolderFake(ShimQueryFolder queryFolderFake, List<QueryDefinition> queryList)
        {
            queryFolderFake.Bind(queryList);
            
            var queryFolderBaseFake = new ShimQueryItem(queryFolderFake);
            queryFolderBaseFake.NameGet = () => "TestFolder";
            queryFolderBaseFake.ProjectGet = () => projectFake;
            queryFolderBaseFake.PathGet = () => "TestProject/TestFolder";

            return queryFolderFake;
        }

        private List<QueryDefinition> CreateQueryDefinitionList(ShimQueryFolder parent,  ShimQueryDefinition queryDefinitionFake)
        {
            var queryDefinitionBaseFake = new ShimQueryItem(queryDefinitionFake);

            queryDefinitionBaseFake.NameGet = () => "TestQuery";
            queryDefinitionBaseFake.ProjectGet = () => projectFake;
            queryDefinitionBaseFake.ParentGet = () => parent;
            queryDefinitionBaseFake.PathGet = () => "TestProject/TestFolder";

            var queryList = new List<QueryDefinition>() { queryDefinitionFake };
            return queryList;
        }

        #endregion

    }
}
