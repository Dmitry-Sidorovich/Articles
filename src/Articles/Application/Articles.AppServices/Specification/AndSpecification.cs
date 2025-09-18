using System.Linq.Expressions;

namespace Articles.AppServices.Specification;

public class AndSpecification<T>: Specification<T>
{
    private readonly Specification<T> _left;
    private readonly Specification<T> _right;

    public AndSpecification(Specification<T> right, Specification<T> left)
    {
        _right = right;
        _left = left;
    }

    public override Expression<Func<T, bool>> ToExpression()
    {
        var leftExpression = _left.ToExpression();
        var rightExpression = _right.ToExpression();
        
        var paramExpression = Expression.Parameter(typeof(T));
        var expressionBody = Expression.AndAlso(leftExpression.Body, rightExpression.Body);

        expressionBody = (BinaryExpression)new ParameterReplacer(paramExpression).Visit(expressionBody);
        
        return Expression.Lambda<Func<T, bool>>(expressionBody, paramExpression);
    }
}