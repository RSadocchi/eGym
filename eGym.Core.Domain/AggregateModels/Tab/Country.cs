using eGym.Core.SeedWork;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eGym.Core.Domain
{
    [Table(nameof(Country), Schema = "dbo")]
    public partial class Country : Entity, IAggregateRoot
    {
        #region Db Columns
        [Key] 
        [Column(TypeName = "char(3)")]
        [MaxLength(3)] 
        public string Country_IsoCode { get; set; }
        [MaxLength(50)] 
        public string Country_CountryName { get; set; }
        [MaxLength(3)] 
        public string Country_VIESCode { get; set; }
        [MaxLength(5)] 
        public string Country_Language { get; set; }
        public string Country_CFiscCode { get; set; }
        #endregion

        #region Virtuals
        public virtual ICollection<Anag_Master> Anag_Masters { get; set; }
        public virtual ICollection<Anag_Address> Anag_Addresses { get; set; }
        #endregion

        #region Constructors
        public Country()
        {
            Anag_Masters = new HashSet<Anag_Master>();
            Anag_Addresses = new HashSet<Anag_Address>();
        }
        #endregion
    }
}
