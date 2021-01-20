using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eGym.Core.Security.Identity
{
    public class User : Microsoft.AspNetCore.Identity.IdentityUser<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool Disabled { get; set; }
        public Nullable<DateTime> EmailConfirmedDateTime { get; set; }
        public Nullable<DateTime> PhoneNumberConfirmedDateTime { get; set; }
        public string PINHash { get; set; }
        public string Culture { get; set; }
        public Nullable<DateTime> PasswordExpirationDateTime { get; set; }
        public string Avatar { get; set; }
        [NotMapped]
        public string CompleteName { get => $"{FirstName} {LastName}"; }


        public virtual ICollection<PasswordHistory> PasswordHistory { get; set; }


        public User() : base()
        {
            PasswordHistory = new List<PasswordHistory>();
        }

        public static explicit operator Microsoft.AspNetCore.Identity.IdentityUser(User Value)
        {
            return (Microsoft.AspNetCore.Identity.IdentityUser)Value;
        }
    }
}