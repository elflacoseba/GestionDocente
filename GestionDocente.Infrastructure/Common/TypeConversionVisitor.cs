using System.Linq.Expressions;

namespace GestionDocente.Infrastructure.Common
{
    public class TypeConversionVisitor : ExpressionVisitor
    {
        private readonly Type _sourceType;
        private readonly Type _targetType;
        private ParameterExpression? _parameter;

        public TypeConversionVisitor(Type sourceType, Type targetType)
        {
            _sourceType = sourceType;
            _targetType = targetType;
        }

        public Expression<Func<TTarget, bool>> Convert<TSource, TTarget>(Expression<Func<TSource, bool>> expression)
        {
            _parameter = Expression.Parameter(_targetType, expression.Parameters[0].Name);
            var body = Visit(expression.Body);
            return Expression.Lambda<Func<TTarget, bool>>(body, _parameter);
        }

        protected override Expression VisitParameter(ParameterExpression node)
        {
            return _parameter ?? base.VisitParameter(node);
        }

        protected override Expression VisitMember(MemberExpression node)
        {
            if (node.Expression is ParameterExpression paramExpr && paramExpr.Type == _sourceType)
            {
                var memberInfo = _targetType.GetMember(node.Member.Name).FirstOrDefault();
                if (memberInfo != null)
                {
                    return Expression.MakeMemberAccess(_parameter!, memberInfo);
                }
            }
            return base.VisitMember(node);
        }
    }
}