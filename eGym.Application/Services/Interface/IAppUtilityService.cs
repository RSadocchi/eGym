using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace eGym.Application.Services
{
    public interface IAppUtilityService
    {
        void Cookie_Append(string name, string value, int expiringDays, Encoding encoding = null);
        string Cookie_Get(string name, Encoding encoding = null);
        void Cookie_Remove(string name);

        bool ToggleLanguage(string lang);
    }
}
