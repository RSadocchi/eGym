using eGym.Application.Option;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace eGym.Application.Services
{
    public interface IEmailService
    {
        Task<bool> SendEmailAsync(
            string to,
            string subject,
            string body,
            IEnumerable<string> attachments = null,
            IEnumerable<string> attachmentNames = null,
            string from = null,
            string fromName = null,
            string cc = null,
            string bcc = null,
            IEmailServerOptions emailServerOptions = null);
    }
}
