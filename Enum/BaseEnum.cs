using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace ExpressionParser.Plug
{

    /// <summary>
    /// 当前条件所属类型
    /// </summary>
    public enum Em_AS_ConditionType
    {
        /// <summary>
        /// 并
        /// </summary>
        [Description("并")]
        And = 0,

        /// <summary>
        /// 或
        /// </summary>
        [Description("或")]
        Or = 1
    }

    /// <summary>
    /// 高级搜索条件
    /// </summary>
    public enum Em_AS_Condition
    {
        /// <summary>
        /// 包含
        /// </summary>
        [Description("Include")]
        Include = 0,

        /// <summary>
        /// 等于
        /// </summary>
        [Description("=")]
        Equal = 1,

        /// <summary>
        /// 大于等于
        /// </summary>
        [Description(">=")]
        GtEqual = 2,

        /// <summary>
        /// 大于
        /// </summary>
        [Description(">")]
        Gt = 3,

        /// <summary>
        /// 小于等于
        /// </summary>
        [Description("<=")]
        LtEqual = 4,

        /// <summary>
        /// 小于
        /// </summary>
        [Description("<")]
        Lt = 5,


    }


}
