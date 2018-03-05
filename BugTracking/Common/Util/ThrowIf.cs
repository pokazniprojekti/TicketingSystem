using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Common.Util
{
    public static class ThrowIf
    {
        #region IsNull check

        /// <summary>
        /// Throws an exception when parameter is null.
        /// </summary>
        /// <param name="parameter">An object to be validated.</param>
        /// <param name="description">Description included in the exception when is an exception is raised.</param>
        /// <exception cref="T:ArgumentNullException">The parameter is null.</exception>
        public static void IsNull(object parameter, string description)
        {
            if (parameter == null)
            {
                throw new ArgumentNullException(description);
            }
        }

        /// <summary>
        /// Throws an exception when specified parameter is null.
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="selector">Expression which will be evaluated</param>
        public static void IsNull<T>(Expression<Func<T>> selector)
        {
            var memberSelector = (MemberExpression)selector.Body;
            var constantSelector = (ConstantExpression)memberSelector.Expression;
            var value = ((FieldInfo)memberSelector.Member).GetValue(constantSelector.Value);

            if (value != null) return;

            var name = ((MemberExpression)selector.Body).Member.Name;
            throw new ArgumentNullException(name);
        }
        #endregion
    }
}
