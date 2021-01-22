using eGym.Application.Option;
using eGym.Common;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eGym.Application.Services
{
    public class EmailService : IEmailService
    {
        readonly EmailOptions _emailOptions;
        readonly ILogger<EmailService> _logger;

        public EmailService(
            IOptions<EmailOptions> options,
            ILogger<EmailService> logger)
        {
            _emailOptions = options.Value ?? throw new ArgumentNullException(nameof(options));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        #region ///PRIVATE Methods
        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return (addr.Address == email) && email.IsValidEmailAddress();
            }
            catch
            {
                return false;
            }
        }

        private bool IsDebugAllowedEmail(string[] allowedEmails, string email)
        {
            foreach (string allowedEmail in allowedEmails)
                if (allowedEmail.IndexOf("@") == 0)
                {
                    if (email.EndsWith(allowedEmail, StringComparison.OrdinalIgnoreCase))
                        return true;
                }
                else
                {
                    if (email.Equals(allowedEmail, StringComparison.OrdinalIgnoreCase))
                        return true;
                }
            return false;
        }

        private async Task<bool> SendMailkit(
            string from,
            string to,
            string subject,
            string body,
            string mailServerAddress,
            int mailServerPort,
            string userId,
            string userPassword,
            IEnumerable<string> attachments = null,
            IEnumerable<string> attachmentNames = null,
            string fromName = null,
            string cc = null,
            string bcc = null,
            bool useSSL = false,
            bool useTLS = false)
        {
            MimeKit.MimeMessage oMsg = new MimeKit.MimeMessage();
            try
            {
                if (string.IsNullOrWhiteSpace(from))
                    new Exception("No sender");

                //Imposta il mittente
                oMsg.From.Add(new MimeKit.MailboxAddress(fromName, from));

                //Imposta i TO
                if (!string.IsNullOrWhiteSpace(to) && to.IndexOf(";") >= 0)
                    foreach (string eml in to.Split(';').Where(t => !string.IsNullOrWhiteSpace(t)).Distinct())
                        oMsg.To.Add(new MimeKit.MailboxAddress(eml.Replace(" ", ""), eml.Replace(" ", "")));
                else if (!string.IsNullOrWhiteSpace(to))
                    oMsg.To.Add(new MimeKit.MailboxAddress(to.Replace(" ", ""), to.Replace(" ", "")));
                else
                    throw new Exception("No recipients"); //Se non c'è nessun destinatario lancio un'eccezione

                //Imposta a CC
                if (!string.IsNullOrWhiteSpace(cc) && cc.IndexOf(";") >= 0)
                    foreach (string eml in cc.Split(';').Where(t => !string.IsNullOrWhiteSpace(t)).Distinct())
                        oMsg.Cc.Add(new MimeKit.MailboxAddress(eml.Replace(" ", ""), eml.Replace(" ", "")));
                else if (!string.IsNullOrWhiteSpace(cc))
                    oMsg.Cc.Add(new MimeKit.MailboxAddress(cc.Replace(" ", ""), cc.Replace(" ", "")));

                //Imposta i BCC
                if (!string.IsNullOrWhiteSpace(bcc) && bcc.IndexOf(";") >= 0)
                    foreach (string eml in bcc.Split(';').Where(t => !string.IsNullOrWhiteSpace(t)).Distinct())
                        oMsg.Bcc.Add(new MimeKit.MailboxAddress(eml.Replace(" ", ""), eml.Replace(" ", "")));
                else if (!string.IsNullOrWhiteSpace(bcc))
                    oMsg.Bcc.Add(new MimeKit.MailboxAddress(bcc.Replace(" ", ""), bcc.Replace(" ", "")));

                //Imposto oggetto
                oMsg.Subject = subject;

                //Imposto contenuto
                var builder = new MimeKit.BodyBuilder
                {
                    HtmlBody = body
                };

                //imposto gli allegati
                if (attachments?.Count() > 0)
                    foreach (string all in attachments)
                        if (!string.IsNullOrWhiteSpace(all))
                            builder.Attachments.Add(all);

                //rename allegati
                for (int attIndex = 0; attIndex < builder.Attachments.Count; attIndex++)
                    if (builder.Attachments[attIndex].IsAttachment && attachmentNames?.Count() > attIndex)
                        builder.Attachments[attIndex].ContentDisposition.FileName = attachmentNames.ElementAt(attIndex);

                oMsg.Body = builder.ToMessageBody();

                _logger.LogInformation($"{nameof(IEmailService)}.{nameof(SendMailkit)} ==== from {string.Join(";", oMsg.From.Select(t => t.ToString()))} - to {string.Join(";", oMsg.To.Select(t => t.ToString()))} - cc {string.Join(";", oMsg.Cc.Select(t => t.ToString()))} - bcc {string.Join(";", oMsg.Bcc.Select(t => t.ToString()))} ==== subject '{subject}'");

                //Imposto il Server Smtp
                //Metto valori di default da web.config nel caso si passi SmtpClient Nothing
                MailKit.Net.Smtp.SmtpClient client;
                try
                {
                    client = new MailKit.Net.Smtp.SmtpClient();
                    await client
                        .ConnectAsync(
                            mailServerAddress,
                            mailServerPort,
                            useTLS ?
                                MailKit.Security.SecureSocketOptions.StartTls :
                                (useSSL ?
                                    MailKit.Security.SecureSocketOptions.SslOnConnect :
                                    MailKit.Security.SecureSocketOptions.None))
                        .ConfigureAwait(false);
                }
                catch (MailKit.Security.AuthenticationException ex)
                {
                    if (ex.InnerException is System.ComponentModel.Win32Exception)
                    {
                        //Fix per gestione bug .net framework / mailkit
                        //https://github.com/dotnet/corefx/issues/1854#issuecomment-160513245
                        client = new MailKit.Net.Smtp.SmtpClient();
                        await client
                            .ConnectAsync(
                                mailServerAddress,
                                mailServerPort,
                                useTLS ?
                                    MailKit.Security.SecureSocketOptions.StartTls :
                                    (useSSL ?
                                        MailKit.Security.SecureSocketOptions.SslOnConnect :
                                        MailKit.Security.SecureSocketOptions.None))
                            .ConfigureAwait(false);
                    }
                    else { throw; }
                }

                using (client)
                {
                    try
                    {
                        // Note: since we don't have an OAuth2 token, disable
                        // the XOAUTH2 authentication mechanism.
                        client.AuthenticationMechanisms.Remove("XOAUTH2");

                        // Note: only needed if the SMTP server requires authentication
                        if (!string.IsNullOrEmpty(userPassword)) await client.AuthenticateAsync(userId, userPassword);
                        await client.SendAsync(oMsg).ConfigureAwait(false);
                    }
                    catch
                    {
                        throw;
                    }
                    finally
                    {
                        await client.DisconnectAsync(true).ConfigureAwait(false);
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(IEmailService)}.{nameof(SendMailkit)} FATAL EXCEPTION: {ex.Message}", ex);
                throw ex;
            }
            finally { oMsg = null; }
        }
        #endregion


        public async Task<bool> SendEmailAsync(
            string to,
            string subject,
            string body,
            IEnumerable<string> attachments = null,
            IEnumerable<string> attachmentNames = null,
            string from = null,
            string fromName = null,
            string cc = null,
            string bcc = null,
            IEmailServerOptions emailServerOptions = null)
        {
            if (_emailOptions.Debug)
            {
                //Debug
                string debugMailTo = "";
                string debugMailCc = "";
                string debugMailBcc = "";

                if (!string.IsNullOrWhiteSpace(_emailOptions.DebugAllowedAddresses))
                {
                    string[] allowedEmails = _emailOptions.DebugAllowedAddresses.Split(';');

                    if (!string.IsNullOrEmpty(to))
                        foreach (string emlTo in to.Split(';'))
                            if (!string.IsNullOrWhiteSpace(emlTo) && IsDebugAllowedEmail(allowedEmails, emlTo))
                                debugMailTo += ";" + emlTo;

                    if (!string.IsNullOrEmpty(cc))
                        foreach (string emlCC in cc.Split(';'))
                            if (!string.IsNullOrWhiteSpace(emlCC) && IsDebugAllowedEmail(allowedEmails, emlCC))
                                debugMailCc += ";" + emlCC;

                    if (!string.IsNullOrEmpty(bcc))
                        foreach (string emlBcc in bcc.Split(';'))
                            if (!string.IsNullOrWhiteSpace(emlBcc) && IsDebugAllowedEmail(allowedEmails, emlBcc))
                                debugMailBcc += ";" + emlBcc;
                }

                if (string.IsNullOrWhiteSpace(debugMailTo)) 
                    debugMailTo = _emailOptions.DebugDefaultTo;

                if (string.IsNullOrWhiteSpace(debugMailCc)) 
                    debugMailCc = null;

                if (string.IsNullOrWhiteSpace(debugMailBcc))
                {
                    if (!string.IsNullOrWhiteSpace(_emailOptions.DefaultBcc)) 
                        debugMailBcc = _emailOptions.DefaultBcc;
                    else 
                        debugMailBcc = null;
                }

                subject = "DEBUG - " + subject;
                body = "DEBUG - Real To: " + to + " - Real Cc: " + cc + " - Real Bcc: " + bcc + "<br/><br/>" + body;
                to = debugMailTo;
                cc = debugMailCc;
                bcc = debugMailBcc;
            }

            return await SendMailkit(
                from: emailServerOptions?.FromAddress ?? from ?? _emailOptions.FromAddress,
                fromName: emailServerOptions?.FromName ?? fromName ?? _emailOptions.FromName,
                to: to,
                cc: cc,
                bcc: bcc + (emailServerOptions == null && !string.IsNullOrEmpty(_emailOptions.DefaultBcc) ? ";" + _emailOptions.DefaultBcc : ""),
                subject: subject,
                body: body,
                attachments: attachments,
                attachmentNames: attachmentNames,

                mailServerAddress: emailServerOptions?.MailServerAddress ?? _emailOptions.MailServerAddress,
                mailServerPort: emailServerOptions?.MailServerPort ?? _emailOptions.MailServerPort,
                userId: emailServerOptions?.UserId ?? _emailOptions.UserId,
                userPassword: emailServerOptions?.UserPassword ?? _emailOptions.UserPassword,
                useSSL: emailServerOptions?.UseSSL ?? _emailOptions.UseSSL,
                useTLS: emailServerOptions?.UseTLS ?? _emailOptions.UseTLS);
        }

    }
}
