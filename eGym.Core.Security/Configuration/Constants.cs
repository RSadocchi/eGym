using eGym.Core.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace eGym.Core.Security
{
    //public class EN_RoleType : Enumeration<short>
    //{
    //    public EN_RoleType() : base() { }
    //    public EN_RoleType(short id, string code) : base(id, code) { }

    //    public static IEnumerable<EN_RoleType> GetAll() => GetAll<EN_RoleType>();
    //    public static EN_RoleType FromID(short id) => FromID<EN_RoleType>(id);
    //    public static EN_RoleType FromCode(string code) => FromCode<EN_RoleType>(code);

    //    public static EN_RoleType Auto = new EN_RoleType(1, nameof(Auto));
    //    public static EN_RoleType SysAdmin = new EN_RoleType(2, nameof(SysAdmin));
    //    public static EN_RoleType Administarator = new EN_RoleType(3, nameof(Administarator));
    //    public static EN_RoleType User = new EN_RoleType(4, nameof(User));
    //}

    public static class Const_RoleTypes
    {
        public const string Auto = "Auto";
        public const string SysAdmin = "SysAdmin";
        public const string Administarator = "Administarator";
        public const string User = "User";

    }

    public static class Const_ClaimTypes
    {
        public const string GOD = "GOD";
        public const string ADMINISTRATOR = "ADMINISTRATOR";
        public const string USER = "USER";
        public const string AUTO = "AUTO";
    }

    public static class Const_ClaimValues
    {
        public const string DefaultValue = "1";
    }
}
