using eGym.Core.SeedWork;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eGym.Core.Domain
{
    [Table(nameof(Country), Schema = "dbo")]
    public partial class Country : Entity, IAggregateRoot
    {
        #region Db Columns
        [Key] 
        public int Country_ID { get; set; }
        [Column(TypeName = "char(3)")]
        [MaxLength(3)] 
        public string Country_IsoCode { get; set; }
        [Column(TypeName = "char(3)")]
        [MaxLength(3)] 
        public string Country_UICCode { get; set; }
        [MaxLength(3)] 
        public string Country_VIESCode { get; set; }
        [MaxLength(5)] 
        public string Country_CultureInfoCode { get; set; }
        [MaxLength(50)] 
        public string Country_CountryName { get; set; }
        [MaxLength(5)] 
        public string Country_Language { get; set; }
        #endregion

        #region Virtuals

        #endregion

        #region Constructors
        public Country()
        {

        }
        #endregion
    }
}
