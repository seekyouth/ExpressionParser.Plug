using ExpressionParser.Plug;
using ExpressionParser.Plug.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Newtonsoft.Json;

namespace System.Linq.Expressions
{
    public static class ExpressionExt
    {
        public static Expression<Func<T, bool>> True<T>() { return f => true; }
        public static Expression<Func<T, bool>> False<T>() { return f => false; }

        public static IQueryable<T> WhereIf<T>(this IQueryable<T> source, Expression<Func<T, bool>> predicate, bool condition)
        {
            return condition ? source.Where(predicate) : source;
        }
        public static IQueryable<T> WhereIf<T>(this IQueryable<T> source, Expression<Func<T, int, bool>> predicate, bool condition)
        {
            return condition ? source.Where(predicate) : source;
        }

        public static IEnumerable<T> WhereIf<T>(this IEnumerable<T> source, Func<T, bool> predicate, bool condition)
        {
            return condition ? source.Where(predicate) : source;
        }

        public static IEnumerable<T> WhereIf<T>(this IEnumerable<T> source, Func<T, int, bool> predicate, bool condition)
        {
            return condition ? source.Where(predicate) : source;
        }

        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> expr1, Expression<Func<T, bool>> expr2)
        {
            return Expression.Lambda<Func<T, bool>>(Expression.AndAlso(expr1.Body, expr2.Body), expr1.Parameters);
        }

        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> expr1, Expression<Func<T, bool>> expr2)
        {
            return Expression.Lambda<Func<T, bool>>(Expression.OrElse(expr1.Body, expr2.Body), expr1.Parameters);
        }


        /// <summary>
        /// 恒为真
        /// </summary>
        /// <returns>表达式</returns>
        public static Expression EqualTrueCompare()
        {
            var type = typeof(string);
            var pRef = Expression.Constant(true);
            var constantReference = Expression.Constant(true);
            var be = Expression.Equal(pRef, constantReference);
            return be;
        }

        /// <summary>
        /// 包含
        /// </summary>
        /// <typeparam name="TSource">数据类型</typeparam>
        /// <param name="pe">左侧表达式参数</param>
        /// <param name="property">属性</param>
        /// <param name="value">值</param>
        /// <returns>表达式</returns>
        public static Expression IncludeCompare<TSource>(ParameterExpression pe, string property, object value)
        {
            try
            {
                var type = typeof(TSource);
                object val = null;
                Type dyType = GetValue(type, property, value, out val);
                if (val == null) return null;
                var propertyReference = Expression.Property(pe, property);
                var constantReference = Expression.Constant(val, dyType);
                var be = Expression.Call(propertyReference, typeof(string).GetMethod("Contains"), constantReference);
                return be;
            }
            catch (Exception e)
            {
                //LogHelper.WriteLog("表达式树生成错误，此条件将被忽略=>" + e.Message, e);
                return null;
            }
        }

        /// <summary>
        /// 等于
        /// </summary>
        /// <typeparam name="TSource">数据类型</typeparam>
        /// <param name="pe">左侧表达式参数</param>
        /// <param name="property">属性</param>
        /// <param name="value">值</param>
        /// <returns>表达式</returns>
        public static Expression EqualCompare<TSource>(ParameterExpression pe, string property, object value)
        {
            try
            {
                var type = typeof(TSource);
                object val = null;
                Type dyType = GetValue(type, property, value, out val);
                if (val == null) return null;
                var propertyReference = Expression.Property(pe, property);
                var constantReference = Expression.Constant(val, dyType);
                BinaryExpression be = Expression.Equal(propertyReference, constantReference);
                return be;
            }
            catch (Exception e)
            {
                //LogHelper.WriteLog("表达式树生成错误，此条件将被忽略=>" + e.Message, e);
                return null;
            }
        }


        /// <summary>
        /// 像
        /// </summary>
        /// <typeparam name="TSource">数据类型</typeparam>
        /// <param name="pe">左侧表达式参数</param>
        /// <param name="property">属性</param>
        /// <param name="value">值</param>
        /// <returns>表达式</returns>
        public static Expression LikeCompare<TSource>(ParameterExpression pe, string property, object value)
        {
            try
            {
                var type = typeof(TSource);
                object val = null;
                Type dyType = GetValue(type, property, value, out val);
                if (val == null) return null;
                var propertyReference = Expression.Property(pe, property);
                var constantReference = Expression.Constant(val, dyType);
                BinaryExpression be = Expression.Equal(propertyReference, constantReference);
                return be;
            }
            catch (Exception e)
            {
                //LogHelper.WriteLog("表达式树生成错误，此条件将被忽略=>" + e.Message, e);
                return null;
            }
        }

        /// <summary>
        /// 大于等于
        /// </summary>
        /// <typeparam name="TSource">数据类型</typeparam>
        /// <param name="pe">左侧表达式参数</param>
        /// <param name="property">属性</param>
        /// <param name="value">值</param>
        /// <returns>表达式</returns>
        public static Expression GtEqualCompare<TSource>(ParameterExpression pe, string property, object value)
        {
            try
            {
                var type = typeof(TSource);
                object val = null;
                Type dyType = GetValue(type, property, value, out val);
                if (val == null) return null;
                var propertyReference = Expression.Property(pe, property);
                var constantReference = Expression.Constant(val, dyType);
                BinaryExpression be = Expression.GreaterThanOrEqual(propertyReference, constantReference);
                return be;
            }
            catch (Exception e)
            {
                //LogHelper.WriteLog("表达式树生成错误，此条件将被忽略=>" + e.Message, e);
                return null;
            }
        }

        /// <summary>
        /// 大于
        /// </summary>
        /// <typeparam name="TSource">数据类型</typeparam>
        /// <param name="pe">左侧表达式参数</param>
        /// <param name="property">属性</param>
        /// <param name="value">值</param>
        /// <returns>表达式</returns>
        public static Expression GtCompare<TSource>(ParameterExpression pe, string property, object value)
        {
            try
            {
                var type = typeof(TSource);
                object val = null;
                Type dyType = GetValue(type, property, value, out val);
                if (val == null) return null;
                var propertyReference = Expression.Property(pe, property);
                var constantReference = Expression.Constant(val, dyType);
                BinaryExpression be = Expression.GreaterThan(propertyReference, constantReference);
                return be;
            }
            catch (Exception e)
            {
                //LogHelper.WriteLog("表达式树生成错误，此条件将被忽略=>" + e.Message, e);
                return null;
            }
        }

        /// <summary>
        /// 小于等于
        /// </summary>
        /// <typeparam name="TSource">数据类型</typeparam>
        /// <param name="pe">左侧表达式参数</param>
        /// <param name="property">属性</param>
        /// <param name="value">值</param>
        /// <returns>表达式</returns>
        public static Expression LtEqualCompare<TSource>(ParameterExpression pe, string property, object value)
        {
            try
            {
                var type = typeof(TSource);
                object val = null;
                Type dyType = GetValue(type, property, value, out val);
                if (val == null) return null;
                var propertyReference = Expression.Property(pe, property);
                var constantReference = Expression.Constant(val, dyType);
                BinaryExpression be = Expression.LessThanOrEqual(propertyReference, constantReference);
                return be;
            }
            catch (Exception e)
            {
                // LogHelper.WriteLog("表达式树生成错误，此条件将被忽略=>" + e.Message, e);
                return null;
            }
        }

        /// <summary>
        /// 小于
        /// </summary>
        /// <typeparam name="TSource">数据类型</typeparam>
        /// <param name="pe">左侧表达式参数</param>
        /// <param name="property">属性</param>
        /// <param name="value">值</param>
        /// <returns>表达式</returns>
        public static Expression LtCompare<TSource>(ParameterExpression pe, string property, object value)
        {
            try
            {
                var type = typeof(TSource);
                object val = null;
                Type dyType = GetValue(type, property, value, out val);
                if (val == null) return null;
                var propertyReference = Expression.Property(pe, property);
                var constantReference = Expression.Constant(val, dyType);
                BinaryExpression be = Expression.LessThan(propertyReference, constantReference);
                return be;
            }
            catch (Exception e)
            {
                // LogHelper.WriteLog("表达式树生成错误，此条件将被忽略=>" + e.Message, e);
                return null;
            }
        }


        /// <summary>
        /// 根据实体类动态转换值类型,并输出转换后的值
        /// </summary>
        /// <param name="type">实体类型</param>
        /// <param name="propName">属性名</param>
        /// <param name="value">原始值</param>
        /// <param name="oVal">输出值</param>
        /// <returns>该属性对应的数据类型</returns>
        public static Type GetValue(Type type, string propName, object value, out object oVal)
        {
            Type objType = null;
            object objValue = null;
            foreach (var pi in type.GetProperties())
            {
                if (pi.Name == propName)
                {
                    objType = pi.PropertyType;
                    string typeStr = string.Empty;
                    bool isNullType = IsNullableType(pi.PropertyType);
                    if (isNullType)
                    {
                        typeStr = pi.PropertyType.GetGenericArguments()[0].Name.ToLower();
                    }
                    else
                    {
                        typeStr = pi.PropertyType.Name.ToLower();
                    }
                    switch (typeStr)
                    {
                        case "string":
                            objValue = value + "";
                            break;
                        case "boolean":
                            bool tempbool;
                            bool isBoolean = bool.TryParse(value + "", out tempbool);
                            if (isBoolean)
                            {
                                objValue = tempbool;
                            }
                            else
                            {
                                objValue = null;
                            }
                            break;
                        case "datetime":
                            DateTime tempDateTime;
                            bool isDateTime = DateTime.TryParse(value + "", out tempDateTime);
                            if (isDateTime)
                            {
                                objValue = tempDateTime;
                            }
                            else
                            {
                                objValue = null;
                            }
                            break;
                        case "int16":
                        case "int32":
                        case "int64":
                            int tempint = 0;
                            bool isInt = int.TryParse(value + "", out tempint);
                            if (isInt)
                            {
                                objValue = tempint;
                            }
                            else
                            {
                                objValue = null;
                            }
                            break;
                        case "double":
                            double tempDouble = 0;
                            var tempStrDouble = value + "";
                            bool isDouble = double.TryParse(tempStrDouble, out tempDouble);
                            if (isDouble)
                            {
                                objValue = tempDouble;
                            }
                            else
                            {
                                objValue = null;
                            }
                            break;
                        case "decimal":
                            decimal tempDecimal = 0;
                            var tempStr = value + "";
                            bool isdecimal = decimal.TryParse(tempStr, out tempDecimal);
                            if (isdecimal)
                            {
                                objValue = tempDecimal;
                            }
                            else
                            {
                                objValue = null;
                            }
                            break;
                        default:

                            break;
                    }
                }
            }
            oVal = objValue;
            return objType;
        }

        /// <summary>
        /// 判定是否为可空类型
        /// </summary>
        /// <param name="theType">类型</param>
        /// <returns></returns>
        public static bool IsNullableType(Type theType)
        {
            return (theType.IsGenericType && theType.GetGenericTypeDefinition() == typeof(Nullable<>));
        }


        /// <summary>
        /// 将list的json数据转换成表达式树
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="parameter">json数据</param>
        /// <returns></returns>
        public static Expression<Func<TSource, bool>> ConvertToExpression<TSource>(string parameter)
        {
            parameter = System.Web.HttpUtility.UrlDecode(parameter, System.Text.ASCIIEncoding.UTF8);
            return ConvertToExpression<TSource>(JsonConvert.DeserializeObject<List<QueryItem>>(parameter));
        }


        /// <summary>
        /// 将高级查询转换成对应的表达式树
        /// </summary>
        /// <typeparam name="TSource">类型</typeparam>
        /// <param name="conditions">高级查询条件集合</param>
        /// <returns>对象数据类型的表达式树</returns>
        public static Expression<Func<TSource, bool>> ConvertToExpression<TSource>(this List<QueryItem> conditions)
        {
            var type = typeof(TSource);
            var pe = Expression.Parameter(type, "p");

            Expression exp = null;
            if (conditions == null)
            {
                exp = EqualTrueCompare();
                return Expression.Lambda<Func<TSource, bool>>(exp, pe);
            }
            //并：生成交集的条件
            conditions.Where(p => p.ConditionType == Em_AS_ConditionType.And).ToList().ForEach(p =>
            {
                Expression temp = GetExpressionTemp<TSource>(p, pe);
                if (exp == null)
                {
                    if (temp != null)
                    {
                        exp = temp;
                    }
                }
                else
                {
                    if (temp != null)
                    {
                        exp = Expression.And(exp, temp);
                    }
                }
            });

            //或：生成并集的条件
            conditions.Where(p => p.ConditionType == Em_AS_ConditionType.Or).ToList().ForEach(p =>
            {
                Expression temp = GetExpressionTemp<TSource>(p, pe);
                if (exp == null)
                {
                    if (temp != null)
                    {
                        exp = temp;
                    }
                }
                else
                {
                    if (temp != null)
                    {
                        exp = Expression.Or(exp, temp);
                    }
                }
            });
            if (exp == null)
            {
                exp = EqualTrueCompare();
            }

            return Expression.Lambda<Func<TSource, bool>>(exp, pe);
        }

        private static Expression GetExpressionTemp<TSource>(QueryItem p, ParameterExpression pe)
        {
            Expression temp = null;
            switch (p.Condition)
            {
                case Em_AS_Condition.Include:
                    temp = IncludeCompare<TSource>(pe, p.PropName, p.Keyword);
                    break;
                case Em_AS_Condition.GtEqual:
                    temp = GtEqualCompare<TSource>(pe, p.PropName, p.Keyword);
                    break;
                case Em_AS_Condition.Gt:
                    temp = GtCompare<TSource>(pe, p.PropName, p.Keyword);
                    break;
                case Em_AS_Condition.LtEqual:

                    temp = LtEqualCompare<TSource>(pe, p.PropName, p.Keyword);
                    break;
                case Em_AS_Condition.Lt:
                    temp = LtCompare<TSource>(pe, p.PropName, p.Keyword);
                    break;
                case Em_AS_Condition.Equal:
                    temp = EqualCompare<TSource>(pe, p.PropName, p.Keyword);
                    break;
                default:
                    break;
            }
            return temp;
        }
    }
}
