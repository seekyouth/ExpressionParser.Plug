using ExpressionParser.Plug.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FineUIPro;
using System.Web.UI;
using System.Linq.Expressions;
using System.ComponentModel;


namespace ExpressionParser.Plug
{
    public  class SelectHelper
    {
        string propName = string.Empty;
        string condition = string.Empty;
        string keyword = string.Empty;
        string conditionType = string.Empty;
        List<Dictionary<string, object>> newAddedList = new List<Dictionary<string, object>>();
        /// <param name="newAddedList">新增数据行</param>
        /// <param name="propName">查询名称</param>
        /// <param name="condition">条件</param>
        /// <param name="keyword">查询值</param>
        /// <param name="conditionType">与其他条件的关系</param>
        public SelectHelper(FineUIPro.Grid grid)
        {
            propName = grid.AllColumns[1].ColumnID;
            condition = grid.AllColumns[2].ColumnID;
            keyword = grid.AllColumns[3].ColumnID;
            conditionType = grid.AllColumns[4].ColumnID;
            newAddedList = grid.GetNewAddedList();
        }
        /// <summary>
        /// 获取查询语句
        /// </summary>
        /// <returns></returns>
        public string GetSelectComplex()
        {
            string sqlwhere = string.Empty;
            StringBuilder sb = new StringBuilder();
            string endrelation = "";//最后的关系
            for (int i = 0; i < newAddedList.Count; i++)
            {
                string propNamevalue = newAddedList[i][propName].ToString();
                string conditionvalue = newAddedList[i][condition].ToString();
                string keywordvalue = newAddedList[i][keyword].ToString();
                string conditionTypevalue = newAddedList[i][conditionType].ToString();
                #region 处理特殊条件
                if (conditionvalue.Contains("in")) keywordvalue = DisposeIn(keywordvalue);//处理 in 
                else if (conditionvalue.Contains("between") && !(i + 1 < newAddedList.Count)) continue;
                else if (conditionvalue.Contains("between")) keywordvalue = DisposeBetween(keywordvalue, newAddedList[i + 1][keyword].ToString());//处理 between 
                else if (conditionvalue.Contains("like")) keywordvalue = DisposeLike(keywordvalue);//处理 like 
                else i++;
                #endregion
                sb.AppendFormat(" {0} {1} '{2}' {3}", propNamevalue, conditionvalue, keywordvalue, conditionTypevalue);
                endrelation = conditionType;
            }
            sqlwhere = sb.ToString();
            sqlwhere = sqlwhere.Remove(sqlwhere.Length - endrelation.Length, endrelation.Length);
            return sqlwhere;
        }

        /// <summary>
        /// 获取查询语句
        /// </summary>
        /// <returns></returns>
        public string GetQueryComplex<T>()
        {
            Expression<Func<T, bool>> expression = ExpressionExt.ConvertToExpression<T>(SetQueryItems());
            return new QueryTranslator().Translate(expression);
        }

        #region 条件特殊处理
        /// <summary>
        /// 处理Between
        /// </summary>
        private string DisposeBetween(string statime, string endtime)
        {
            return string.Format("BETWEEN '{0}' AND '{1}'", statime, endtime);
        }
        /// <summary>
        /// 处理In
        /// 以逗号分隔
        /// </summary>
        private string DisposeIn(string parameter)
        {
            StringBuilder sb = new StringBuilder();
            parameter = parameter.Replace('，', ',');//转义中文逗号
            string[] parameters = parameter.Split(',');
            foreach (var par in parameters)
            {
                sb.AppendFormat("'{0}',", par);
            }
            string where = sb.ToString();
            return where.Remove(where.Length - 1, 1);//移除最后的逗号
        }
        /// <summary>
        /// 处理Like
        /// </summary>
        private string DisposeLike(string parameter)
        {
            return string.Format("'%{0}%'", parameter);
        }

        #endregion
        #region 获取QueryItem集合
        /// <summary>
        /// 获取QueryItem集合
        /// </summary>
        /// <returns></returns>
        public List<QueryItem> GetQueryItems()
        {
            List<QueryItem> qis = SetQueryItems();
            return qis;
        }


        /// <summary>
        /// 获取QueryItem集合
        /// </summary>
        /// <returns></returns>
        private List<QueryItem> SetQueryItems()
        {
            List<QueryItem> qis = new List<QueryItem>();
            foreach (var add in newAddedList)
            {
                QueryItem qi = new QueryItem()
                {
                    PropName = add[propName].ToString(),
                    Condition = QueryItem.GetConditionEnum(add[condition].ToString()),
                    Keyword = add[keyword].ToString(),
                    ConditionType = QueryItem.GetConditionTypeEnum(add[conditionType].ToString().Contains("like")? "Include" : add[conditionType].ToString())
                };
                qis.Add(qi);
            }
            return qis;
        }
        #endregion
        #region 获取字段名和注释
         /// <summary>
        /// 根据对象获取字段名和备注
        /// </summary>
        /// <typeparam name="T">对象实体</typeparam>
        /// <param name="t">对象实例化</param>
        /// <returns></returns>
        public List<ListNameValue> GetModelDesc<T>(T t)
        {
            List<ListNameValue> nameValues =new  List<ListNameValue>();
            Type types = t.GetType();
            foreach (var item in types.GetProperties())
            {
                var v = (DescriptionAttribute[])item.GetCustomAttributes(typeof(DescriptionAttribute), false);
                ListNameValue namevalue = new ListNameValue(item.Name, v[0].Description);
                nameValues.Add(namevalue);
            }
            return nameValues;
        }
        /// <summary>
        /// 根据实体名称获取字段名和备注
        /// </summary>
        /// <param name="modelName">实体名称（带命名空间名称）</param>
        /// <returns></returns>
        public List<ListNameValue> GetModelDesc(string modelName)
        {
            Type t;
            object obj;
            t = Type.GetType(modelName);
            obj = System.Activator.CreateInstance(t);
            return GetModelDesc<object>(obj);
        }
        #endregion
        #region 获取Grid
        private Grid GetGrid(ControlCollection control)
        {
            Grid grid = new Grid();
            for (int i = 0; i < control.Count; i++)
            {
                if (control[i].ID.Contains("grid"))//ID包含值
                {
                    grid = control[i] as Grid;
                    break;
                }
                else if (control[i].Controls.Count > 0) GetGrid(control[i].Controls);
            }
            return grid;
        }
        #endregion
    }
}
