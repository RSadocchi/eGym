using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace eGym.Core.Security.Identity
{
    public partial class UserVoucher // : Entity, IAggregateRoot
    {
        [Key] public int UV_ID { get; set; }
        public int UV_StatusID { get; set; }
        [StringLength(6)] public string UV_Voucher { get; set; }
        public DateTime UV_VoucherCreation { get; set; }
        public DateTime UV_VoucherExpirationDateTime { get; set; }

        /// <summary>
        /// DATI UTENZA: riferimento anagrafica selezionata in fase di creazione voucher
        /// </summary>
        public int? UV_SentToAnagID { get; set; }
        /// <summary>
        /// DATI UTENZA: email a cui si invia il voucher
        /// </summary>
        public string UV_SentToEmail { get; set; }
        /// <summary>
        /// DATI UTENZA: nome della persona a cui si invia il voucher
        /// </summary>
        public string UV_SentToFirstName { get; set; }
        /// <summary>
        /// DATI UTENZA: cognome della persona a cui si invia il voucher
        /// </summary>
        public string UV_SentToLastName { get; set; }
        /// <summary>
        /// DATI UTENZA: codica fiscale della persona a cui si invia il voucher
        /// </summary>
        public string UV_SentToTaxCode { get; set; }
        /// <summary>
        /// DATI UTENZA: telefono (opzionale in questa fase) della persona a cui si invia il voucher
        /// </summary>
        public string UV_SentToMobile { get; set; }
        /// <summary>
        /// DATI UTENZA: Ruolo per cui si emette il voucher (consultivo o esecutivo)
        /// </summary>
        public string UV_SentToRole { get; set; }
        /// <summary>
        /// DATI UTENZA: Cultura in cui viene inviata la mail del voucher
        /// </summary>
        [MaxLength(5)] public string UV_SentToCulture { get; set; }
        /// <summary>
        /// Riferimento all'anagrafica del customer
        /// </summary>
        public int? UV_AnagID { get; set; } //CHECK: UV_AnagID secondo me non dovrebbe essere nullabile
        /// <summary>
        /// Inizio del processo di registrazione
        /// </summary>
        public DateTime? UV_RegistrationStartedDateTime { get; set; }
        /// <summary>
        /// Processo completato, ma il nuovo user non ha ancora confermato la mail
        /// </summary>
        public DateTime? UV_RegistrationCompletedDateTime { get; set; }
        /// <summary>
        /// Tutte le fasi comoletate, compresa conferma user email
        /// </summary>
        public DateTime? UV_RegistrationClosedDateTime { get; set; }
        public string UV_TemporaryToken { get; set; }
        public DateTime? UV_TemporaryTokenExpirationDateTime { get; set; }
        public int? UV_UserID { get; set; }
        public string UV_UserFirstName { get; set; }
        public string UV_UserLastName { get; set; }
        public string UV_UserEmail { get; set; }
        public string UV_UserMobile { get; set; }
        public DateTime? UV_PrivacyAcceptanceDateTime { get; set; }
        public DateTime? UV_PolicyAcceptanceDateTime { get; set; }
        public int UV_LastModifyByUserID { get; set; }
        public DateTime UV_LastModifyDateTime { get; set; }


        public virtual User User { get; set; }
    }
}
