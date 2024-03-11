using Microsoft.Extensions.Primitives;
using System.ComponentModel;
using System.Numerics;
using System.Reflection;

namespace FilterQueryStringBinder.Models
{
    public static class ParserExtensions
    {

        private static Dictionary<Type, ConditionOperator> _x = new()
        {
            {typeof(INumber<>),ConditionOperator.EQ | ConditionOperator.NEQ | ConditionOperator.GT | ConditionOperator.LT | ConditionOperator.GTEQ | ConditionOperator.LTEQ },
            {typeof(string), ConditionOperator.EQ | ConditionOperator.NEQ | ConditionOperator.SW | ConditionOperator.EW | ConditionOperator.LIKE },
            {typeof(bool), ConditionOperator.EQ | ConditionOperator.NEQ }
        };
        private static Dictionary<string, ConditionOperator> _hh;

        static ParserExtensions()
        {
            _hh = typeof(ConditionOperator).GetTypeInfo().DeclaredFields
                .Select(a => new { Token = a.GetCustomAttribute<DescriptionAttribute>()?.Description, Field = a })
                .Where(a => a.Token is not null)
                .ToDictionary(a => a.Token, a => (ConditionOperator)a.Field.GetValue(null));
        }

        public static bool TryParse(string value, Type type, out ConditionOperator @operator)
        {
            if (!IsConditionableType(type, out Type generalType))
            {
                @operator = ConditionOperator.None;
                return false;
            }

            if (!_hh.TryGetValue(value, out @operator)) 
            {
                @operator = ConditionOperator.None;
                return false;
            }

            if (@operator == ConditionOperator.LIKE && (generalType == typeof(INumber<>) || generalType == typeof(bool)))
            {
                @operator = ConditionOperator.EQ;
            }

            var parsed = (_x[generalType] & @operator) != 0;
            @operator = !parsed ? ConditionOperator.None : @operator;
            return parsed;
        }

        public static bool IsConditionableType(Type type, out Type generalType)
        {
            generalType = NumberType(type) ?? DateTimeType(type) ?? TextType(type) ?? BooleanType(type);
            return generalType is not null;
        }

        private static Type? NumberType(Type type)
        {
            return type.GetInterface("INumber`1")?.GetGenericTypeDefinition();
        }

        private static Type? DateTimeType(Type type)
        {
            if (type == typeof(DateTime) || type == typeof(DateOnly) || type == typeof(TimeOnly))
            {
                return typeof(INumber<>);
            }
            return null;
        }

        private static Type? TextType(Type type)
        {
            if (type == typeof(string))
            {
                return typeof(string);
            }
            return null;
        }

        private static Type? BooleanType(Type type)
        {
            if (type == typeof(bool))
            {
                return typeof(bool);
            }
            return null;
        }

    }
}
