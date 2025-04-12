using AutoMapper;
using System.Linq.Expressions;

namespace GestionDocente.Infrastructure.Mapping
{
    public class PredicateMappingProfile : Profile
    {
        public PredicateMappingProfile()
        {
            // Create a mapping for Expression<Func<TEntity, bool>> to Expression<Func<TModel, bool>>
            CreateMap(typeof(Expression<>), typeof(Expression<>))
                .ConvertUsing(typeof(PredicateConverter<,>));
        }

        private class PredicateConverter<TEntity, TModel> :
            ITypeConverter<Expression<Func<TEntity, bool>>, Expression<Func<TModel, bool>>>
        {
            public Expression<Func<TModel, bool>> Convert(
                Expression<Func<TEntity, bool>> source,
                ResolutionContext context)
            {
                // Map the body of the predicate from TEntity to TModel
                var parameter = Expression.Parameter(typeof(TModel), source.Parameters[0].Name);
                var body = new ExpressionReplacer(source.Parameters[0], parameter).Visit(source.Body);

                return Expression.Lambda<Func<TModel, bool>>(body!, parameter);
            }

            public Expression<Func<TModel, bool>> Convert(Expression<Func<TEntity, bool>> source, Expression<Func<TModel, bool>> destination, ResolutionContext context)
            {
                // Map the body of the predicate from TEntity to TModel
                var parameter = Expression.Parameter(typeof(TModel), source.Parameters[0].Name);
                var body = new ExpressionReplacer(source.Parameters[0], parameter).Visit(source.Body);
                return Expression.Lambda<Func<TModel, bool>>(body!, parameter);
            }
        }

        private class ExpressionReplacer : ExpressionVisitor
        {
            private readonly Expression _oldExpression;
            private readonly Expression _newExpression;

            public ExpressionReplacer(Expression oldExpression, Expression newExpression)
            {
                _oldExpression = oldExpression;
                _newExpression = newExpression;
            }

            protected override Expression VisitParameter(ParameterExpression node)
            {
                return node == _oldExpression ? _newExpression : base.VisitParameter(node);
            }
        }
    }
}
