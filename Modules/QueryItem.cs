using ExpressionParser.Plug;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExpressionParser.Plug.Modules
{
    /// <summary>
    /// 高级查询
    /// </summary>
    public class QueryItem
    {
        /// <summary>
        /// 属性名称
        /// </summary>
        public string PropName { get; set; }

        /// <summary>
        /// 条件
        /// </summary>
        public Em_AS_Condition Condition { get; set; }

        /// <summary>
        /// 关键字
        /// </summary>
        public string Keyword { get; set; }

        /// <summary>
        /// 这组条件与其它条件的关系
        /// </summary>
        public Em_AS_ConditionType ConditionType { get; set; }



        public static Em_AS_Condition GetConditionEnum(string condition)
        {
            foreach (Em_AS_Condition item in Enum.GetValues(typeof(Em_AS_Condition)))
            {
                if (item.GetDescription() == condition)
                {
                    return (Em_AS_Condition)item.GetHashCode();
                }
            }
            return (Em_AS_Condition)(-1);
        }


        public static Em_AS_ConditionType GetConditionTypeEnum(string condition)
        {
            foreach (Em_AS_ConditionType item in Enum.GetValues(typeof(Em_AS_ConditionType)))
            {
                if (item.GetDescription() == condition)
                {
                    return (Em_AS_ConditionType)item.GetHashCode();
                }
            }
            return (Em_AS_ConditionType)(-1);
        }
    }
}
