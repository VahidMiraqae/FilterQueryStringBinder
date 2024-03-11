using System.ComponentModel;

namespace FilterQueryStringBinder.Models
{
    public enum ConditionOperator
    {
        None = 0b000,
        [Description("~eq~")]
        EQ = 0b0010,
        [Description("~neq~")]
        NEQ = 0b0100,
        [Description("~gt~")]
        GT = 0b1000,
        [Description("~lt~")]
        LT = 0b0001_0000,
        [Description("~gteq~")]
        GTEQ = 0b0010_0000,
        [Description("~lteq~")]
        LTEQ = 0b0100_0000,
        [Description("~ew~")]
        EW = 0b1000_0000,
        [Description("~sw~")]
        SW = 0b0001_0000_0000,
        [Description("")]
        LIKE = 0b0010_1000_0000
    }
}