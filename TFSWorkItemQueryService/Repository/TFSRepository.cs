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

        public TFSRepository(IQueryFinder queryFinder)
        {
            this.queryFinder = queryFinder;
        }

        public TFSRepository(IQueryFinder queryFinder, IQueryRunner queryRunner)
        {
            // TODO: Complete member initialization
            this.queryFinder = queryFinder;
            this.queryRunner = queryRunner;
        }

        public IEnumerable<WorkItem> Run(string project, string queryPath, string queryName)
        {
            queryFinder.FindQuery(project, queryPath, queryName);

            return null;
        }
    }
}