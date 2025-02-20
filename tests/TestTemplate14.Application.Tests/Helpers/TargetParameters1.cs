using System.Collections.Generic;
using TestTemplate14.Application.Sorting.Models;

namespace TestTemplate14.Application.Tests.Helpers
{
    public class TargetParameters1
        : BaseSortable<MappingTargetModel1>
    {
        public override IEnumerable<SortCriteria> SortBy { get; set; } = new List<SortCriteria>();
    }
}
