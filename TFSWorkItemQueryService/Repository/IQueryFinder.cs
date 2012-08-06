using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TFSWorkItemQueryService.Repository
{
    public interface IQueryFinder
    {
        object FindQuery(string project, string queryPath, string queryName);
    }
}
