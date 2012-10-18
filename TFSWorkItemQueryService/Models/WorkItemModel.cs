using Microsoft.TeamFoundation.WorkItemTracking.Client;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace TFSWorkItemQueryService.Repository.Model
{
    public class WorkItemModel
        : DynamicObject
    {
        private IEnumerable<Field> workItemFields;

        public WorkItemModel(WorkItem workItem)
        {
            this.workItemFields = workItem.Fields.OfType<Field>();
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            bool exists = false;

            if (exists = workItemFields.Any(x => x.Name == binder.Name))
            {
                result = (from f in workItemFields
                          where f.Name == binder.Name
                          select f.Name).SingleOrDefault();
            }
            else
                exists = base.TryGetMember(binder, out result);

            return exists;
        }

    }
}
