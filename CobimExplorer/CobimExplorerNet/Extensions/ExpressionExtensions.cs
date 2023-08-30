﻿using System.Linq.Expressions;
using System.Reflection;

namespace CobimExplorerNet.Extensions
{
    public static class ExpressionExtensions
    {
        public static MemberInfo GetMemberInfo(this Expression expression)
        {
            LambdaExpression lambdaExpression = (LambdaExpression)expression;
            return lambdaExpression.Body is UnaryExpression body ? (body.Operand as MemberExpression).Member : (lambdaExpression.Body as MemberExpression).Member;
        }
    }
}
