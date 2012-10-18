using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TFSWorkItemQueryService.Repository;


namespace TFSWorkItemQueryService.Controllers
{
    public class QueryController : ApiController
    {
        public QueryController(TFSRepository repository)
        {

        }
        // GET api/query
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/query/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/query
        public void Post(string value)
        {
        }

        // PUT api/query/5
        public void Put(int id, string value)
        {
        }

        // DELETE api/query/5
        public void Delete(int id)
        {
        }

        public IEnumerable<Microsoft.TeamFoundation.WorkItemTracking.Client.WorkItem> Run(string p1, string p2, string p3)
        {
            throw new NotImplementedException();
        }
    }
}
