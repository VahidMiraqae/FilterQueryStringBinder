using System.Linq.Expressions;

namespace FilterQueryStringBinder.Binders
{
    public class ExpressionFilter<T>
    {
        public ConditionCollection Conditions { get; set; }
        public Expression<Func<T, bool>> Filter { get; set; }
    }
}
