using eGym.Core.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;

namespace eGym.Core.Localization
{
    public class EN_EditorType : Enumeration<short>
    {
        public EN_EditorType() : base() { }
        public EN_EditorType(short id, string code) : base(id, code) { }

        public static IEnumerable<EN_EditorType> GetAll() => GetAll<EN_EditorType>();
        public static EN_EditorType FromID(short id) => FromID<EN_EditorType>(id);

        public static EN_EditorType Undefined = new EN_EditorType(0, nameof(Undefined));
        public static EN_EditorType Input = new EN_EditorType(1, nameof(Input));
        public static EN_EditorType TextArea = new EN_EditorType(2, nameof(TextArea));
        public static EN_EditorType HtmlEditor = new EN_EditorType(3, nameof(HtmlEditor));
    }
}
