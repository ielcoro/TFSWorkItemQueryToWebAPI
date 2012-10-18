using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.TeamFoundation.Client;
using Microsoft.TeamFoundation.WorkItemTracking.Client;

namespace TFSWorkItemQueryService.Repository
{
    public class TFSRepository
    {
        private IQueryFinder queryFinder;
        private IQueryRunner queryRunner;

        public TFSRepository(IQueryFinder queryFinder, IQueryRunner queryRunner)
        {
            this.queryFinder = queryFinder;
            this.queryRunner = queryRunner;
        }

        public IEnumerable<WorkItem> Run(string project, string queryPath, string queryName)
        {
            QueryDefinition queryDefinition = queryFinder.FindQuery(project, queryPath, queryName);

            return queryRunner.RunQuery(queryDefinition);
        }
    }
}