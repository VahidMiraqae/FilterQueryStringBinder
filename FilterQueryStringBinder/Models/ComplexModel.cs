using System.Linq.Expressions;

namespace FilterQueryStringBinder.Models
{
    public class ComplexModel
    {
        public Condition Int { get; set; }



        public string String { get; set; }
        public IEnumerable<string> StringCollection { get; set; }
        public int? NullableInt { get; set; }
        public double Double { get; set; }
        public decimal? NullableDecimal { get; set; }
        public DateTime DateTime { get; set; }
        public DateTime NullableDateTime { get; set; }
        public DateOnly DateOnly { get; set; }
        public TimeOnly TimeOnly { get; set; }
        public DateOnly? NullableDateOnly { get; set; }
        public TimeOnly? NullableTimeOnly { get; set; }
        public char Char { get; set; }
        public char? NullableChar { get; set; }
        
    }
}
