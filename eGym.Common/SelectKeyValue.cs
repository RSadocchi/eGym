using System;
using System.Collections.Generic;
using System.Text;

namespace eGym.Common
{
    public class SelectKeyValue<TValue>
    {
        public string Text { get; set; } = "";
        public TValue Value { get; set; }


        public SelectKeyValue() { }
        public SelectKeyValue(string _text, TValue _value)
        {
            Text = _text;
            Value = _value;
        }

        public override string ToString()
            => $"{Text} {Value}";
    }

    public class SelectKeyValue : SelectKeyValue<string>
    {
        public SelectKeyValue() : base() { }
        public SelectKeyValue(string _text, string _value) : base(_text, _value) { }
    }
}
