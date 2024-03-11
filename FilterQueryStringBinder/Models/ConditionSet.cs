using FilterQueryStringBinder.Binders;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace FilterQueryStringBinder.Models
{
    [ModelBinder<QueryStringExpressionBinder>]
    public class ConditionSet<T>
    {
        public List<Condition> Filters { get; set; }
    }
}