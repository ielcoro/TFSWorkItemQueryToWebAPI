using System;
namespace TFSWorkItemQueryService.Repository
{
    public interface IMacro
    {
        string GetValue();
        string Name { get; set; }
    }
}
