using System;
using System.Collections.Generic;
using System.Text;

namespace eGym.Common
{
    public class SelectItem<TValue>
    {
        public string Icon { get; set; } = null;
        public string Text { get; set; } = "";
        public TValue Value { get; set; } = default(TValue);
        public bool Disabled { get; set; } = false;
        public bool Selected { get; set; } = false;
        public bool Visible { get; set; } = true;
        public dynamic Group { get; set; }
        public dynamic Data { get; set; }

        public SelectItem() { }
        public SelectItem(string _text, TValue _value)
        {
            Text = _text;
            Value = _value;
        }

        public override string ToString() => $"{Text} {Value}";
    }

    public class SelectItem : SelectItem<string>
    {
        public SelectItem() : base() { }
        public SelectItem(string _text, string _value) : base(_text, _value) { }
    }
}
