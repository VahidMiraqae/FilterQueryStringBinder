namespace FilterQueryStringBinder.Binders
{
    public class QueryStringCondition
    {
        public string PropertyName { get; set; }
        public QueryStringOperator Operator { get; set; }
        public object Value { get; set; }
    }
}