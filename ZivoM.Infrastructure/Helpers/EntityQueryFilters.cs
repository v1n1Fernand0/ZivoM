using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ZivoM.Helpers
{
    public static class EntityQueryFilters
    {
        public static LambdaExpression CreateIsDeletedFilter(Type entityType)
        {
            var parameter = Expression.Parameter(entityType, "e");
            var propertyMethodInfo = typeof(EF).GetMethod("Property")?.MakeGenericMethod(typeof(bool));
            var isDeletedProperty = Expression.Call(propertyMethodInfo, parameter, Expression.Constant("IsDeleted"));
            var compareExpression = Expression.MakeBinary(ExpressionType.Equal, isDeletedProperty, Expression.Constant(false));
            var lambda = Expression.Lambda(compareExpression, parameter);
            return lambda;
        }
    }
}
