using FilterQueryStringBinder.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel;
using System.Reflection;

namespace FilterQueryStringBinder.Binders
{
    public class QueryStringExpressionBinder : IModelBinder
    {
        private const string TILDA_CHAR = "~";

        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var queries = bindingContext.HttpContext.Request.Query;

            var model = bindingContext.ModelType.GetGenericArguments().Single();
            var validQueries = model.GetTypeInfo().DeclaredProperties
                .Join(queries, a => a.Name, b => b.Key, (a, b) => new { a, b }, StringComparer.OrdinalIgnoreCase);

            var list = new List<Condition>();
            foreach (var item in validQueries)
            {
                foreach (var item1 in item.b.Value)
                {
                    string operatorToken = "~eq~";

                    object value = null;
                    if (!string.IsNullOrWhiteSpace(item1))
                    {
                        bool startsWithTilda = item1.StartsWith(TILDA_CHAR);
                        var ind = item1.IndexOf(TILDA_CHAR, 1);
                        operatorToken = startsWithTilda && ind > 0 ? item1.Substring(0, ind + 1) : "";
                        var valueStr = ind < 0 ? item1 : item1.Substring(ind + 1);
                        value = TypeDescriptor.GetConverter(item.a.PropertyType).ConvertFromString(valueStr);
                    }
                    else if (item.a.PropertyType == typeof(bool))
                    {
                        value = true;
                    }

                    if (!ParserExtensions.TryParse(operatorToken, item.a.PropertyType, out var @operator))
                    {
                        // not a valid operator for this type
                        continue;
                    }

                    var condition = new Condition()
                    {
                        PropertyName = item.a.Name,
                        Operator = @operator,
                        Value = value
                    };

                    list.Add(condition);

                }
            }

            var h = Activator.CreateInstance(bindingContext.ModelType);
            bindingContext.ModelType.GetProperty("Filters").SetValue(h, list);
            bindingContext.Model = h;
            bindingContext.Result = ModelBindingResult.Success(bindingContext.Model);
            return Task.CompletedTask;
        }
    }
}
