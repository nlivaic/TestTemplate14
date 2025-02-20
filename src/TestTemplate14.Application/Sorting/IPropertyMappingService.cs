using System.Collections.Generic;
using TestTemplate14.Application.Sorting.Models;

namespace TestTemplate14.Application.Sorting
{
    public interface IPropertyMappingService
    {
        IEnumerable<SortCriteria> Resolve(BaseSortable sortableSource, BaseSortable sortableTarget);
    }
}
