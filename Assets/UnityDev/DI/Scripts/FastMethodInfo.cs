namespace UnityDev.DI
{
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Reflection;

    /// <summary>
    /// Класс обертка информации о методе, с вызовом чеез делегат.
    /// </summary>
    ///NOTE: один из способов увеличить скорость работы,
    ///не вызывая напрямую работу метода через MethodInfo
    public class FastMethodInfo
    {
        private delegate object ReturnValueDelegate(object instance, object[] arguments);
        private delegate void VoidDelegate(object instance, object[] arguments);

        private object instance = default;
        private object[] arguments = default;

        public FastMethodInfo(MethodInfo methodInfo, object obj, params object[] arg)
        {
            instance = obj;
            arguments = arg;
            ParameterExpression instanceExpression = Expression.Parameter(typeof(object), nameof(instance));
            ParameterExpression argumentsExpression = Expression.Parameter(typeof(object[]), nameof(arguments));
            List<Expression> argumentExpressions = new List<Expression>();
            ParameterInfo[] parameterInfos = methodInfo.GetParameters();

            for (int i = 0; i < parameterInfos.Length; ++i)
            {
                argumentExpressions.Add(Expression.Convert(Expression.ArrayIndex(argumentsExpression, Expression.Constant(i)), parameterInfos[i].ParameterType));
            }

            MethodCallExpression callExpression = Expression.Call(!methodInfo.IsStatic ? Expression.Convert(instanceExpression, methodInfo.ReflectedType) : null, methodInfo, argumentExpressions);
            if (callExpression.Type == typeof(void))
            {
                VoidDelegate voidDelegate = Expression.Lambda<VoidDelegate>(callExpression, instanceExpression, argumentsExpression).Compile();
                Delegate = (instance, arguments) => { voidDelegate(instance, arguments); return null; };
            }
            else
            {
                Delegate = Expression.Lambda<ReturnValueDelegate>(Expression.Convert(callExpression, typeof(object)), instanceExpression, argumentsExpression).Compile();
            }
        }

        private ReturnValueDelegate Delegate { get; }

        /// <summary>
        /// Вызов метода
        /// </summary>
        /// <returns></returns>
        public object Invoke() => Delegate(instance, arguments);
    }
}
