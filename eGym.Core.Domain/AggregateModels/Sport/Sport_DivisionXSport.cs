using eGym.Core.SeedWork;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eGym.Core.Domain
{
    [Table(nameof(Sport_DivisionXSport), Schema = "conf")]
    public class Sport_DivisionXSport : Entity
    {
        #region Db Columns
        [Key]
        public int DXS_ID { get; set; }
        public int DXS_SportID { get; set; }
        public int DXS_DivisionID { get; set; }
        #endregion

        #region Virtuals
        public virtual Sport_Master Sport_Master { get; set; }
        public virtual Sport_Division Sport_Division { get; set; }
        #endregion

        #region Constructors
        public Sport_DivisionXSport()
        {

        }
        #endregion
    }
}
