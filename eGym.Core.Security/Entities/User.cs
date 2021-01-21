using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eGym.Core.Security.Identity
{
    public class User : Microsoft.AspNetCore.Identity.IdentityUser<int>
    {
        public bool Disabled { get; set; }
        public string PINHash { get; set; }
        [MaxLength(5)]
        public string Culture { get; set; }
        public DateTime? EmailConfirmedDateTime { get; set; }
        public DateTime? PhoneNumberConfirmedDateTime { get; set; }
        public DateTime? PasswordExpirationDateTime { get; set; }
        public DateTime? PrivacyAcceptanceDateTime { get; set; }
        public DateTime? PolicyAcceptanceDateTime { get; set; }
        public string TwoFactorTokenProviders { get; set; }


        public virtual ICollection<PasswordHistory> PasswordHistory { get; set; }
        public virtual ICollection<UserVoucher> UserVouchers { get; set; }


        public User() : base()
        {
            PasswordHistory = new HashSet<PasswordHistory>();
            UserVouchers = new HashSet<UserVoucher>();
        }

        public static explicit operator Microsoft.AspNetCore.Identity.IdentityUser(User Value)
        {
            return (Microsoft.AspNetCore.Identity.IdentityUser)Value;
        }
    }
}