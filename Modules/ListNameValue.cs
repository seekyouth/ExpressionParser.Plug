using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExpressionParser.Plug.Modules
{
    /// <summary>
    /// 键值类
    /// </summary>
    public class ListNameValue
    {
        private string _value;

        public string Value
        {
            get { return _value; }
            set { _value = value; }
        }
        private string _name;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        public ListNameValue(string value, string name)
        {
            _value = value;
            _name = name;
        }
    }
}
