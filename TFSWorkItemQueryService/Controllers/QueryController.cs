using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.TeamFoundation.WorkItemTracking.Client;
using TFSWorkItemQueryService.Repository;


namespace TFSWorkItemQueryService.Controllers
{
    public class QueryController : ApiController
    {
        TFSRepository repository;

        public QueryController(TFSRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public IEnumerable<WorkItem> Run(string teamProject, string path, string queryName)
        {
            return repository.Run(teamProject, path, queryName);
        }
    }
}
