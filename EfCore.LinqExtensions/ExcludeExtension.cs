using System;
using System.Linq;
using System.Linq.Expressions;

namespace EfCore.LinqExtensions {
  public static class ExcludeExtension {
    
    /// <summary>
    /// Creates a projection which excludes members from given expressions
    /// </summary>
    /// <typeparam name="TSource">Source type</typeparam>
    /// <typeparam name="TMember">Excluded member type</typeparam>
    /// <param name="collection">Source queryable</param>
    /// <param name="expressions">Multiple expression with same member type can be passed</param>
    /// <returns>Projection of the original source type excluding defined members</returns>
    public static IQueryable<TSource> Exclude<TSource, TMember>(this IQueryable<TSource> collection, params Expression<Func<TSource, TMember>>[] expressions) where TSource : class {

      var excludedMembers = expressions.Select(x => x.Body)
        .Cast<MemberExpression>()
        .Select(x => x.Member)
        .ToList();

      var sourceType = typeof(TSource);
      var properties = sourceType.GetProperties().Where(x => x.CanWrite && !excludedMembers.Contains(x)).ToArray();
      var sourceParam = Expression.Parameter(typeof(TSource), "p");
      var memberBindings = properties.Select(targetProperty => 
        Expression.Bind(targetProperty, Expression.Property(sourceParam, sourceType.GetProperty(targetProperty.Name))));

      var memberInit = Expression.MemberInit(Expression.New(typeof(TSource)), memberBindings);

      return collection.Select(Expression.Lambda<Func<TSource, TSource>>(memberInit, sourceParam));
    }

  }
}
