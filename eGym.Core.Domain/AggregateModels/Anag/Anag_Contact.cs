﻿using eGym.Core.SeedWork;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eGym.Core.Domain
{
    [Table(nameof(Anag_Contact), Schema = "dbo")]
    public partial class Anag_Contact : Entity
    {
        [Key]
        public int Cnt_ID { get; set; }

        [Required]
        [MaxLength(150)]
        public string Cnt_Value { get; set; }

        public bool Cnt_DefaultInType { get; set; }

        public string Cnt_Note { get; set; }

        public int Cnt_AnagID { get; set; }

        [Required]
        [Column(TypeName = "nchar(1)")]
        public char Cnt_TypeID { get; set; }

        public Anag_Contact()
        {

        }

        public virtual Anag_Master Anag_Master { get; set; }

        [NotMapped]
        public EN_ContactType EN_ContactType => EN_ContactType.FromID(this.Cnt_TypeID);
    }
}
