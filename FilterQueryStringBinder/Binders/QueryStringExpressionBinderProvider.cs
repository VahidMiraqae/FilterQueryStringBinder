using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace FilterQueryStringBinder.Binders
{
    public class QueryStringExpressionBinderProvider : IModelBinderProvider
    {
        public IModelBinder? GetBinder(ModelBinderProviderContext context)
        {
            if (context.Metadata.ModelType.GetGenericTypeDefinition() == typeof(ExpressionFilter<>)
                && context.Metadata.BindingSource == BindingSource.Query
                )
            {
                return new QueryStringExpressionBinder();
            }

            return null;
        }
    }
}
