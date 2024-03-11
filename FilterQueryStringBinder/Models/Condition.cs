namespace FilterQueryStringBinder.Models
{
    public class Condition
    {
        public string PropertyName { get; set; }
        public ConditionOperator Operator { get; set; }
        public object Value { get; set; }
    }
}