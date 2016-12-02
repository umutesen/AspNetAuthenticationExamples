using System.Diagnostics;
using System.Security.Principal;

namespace WindowsPrincipal
{
    class Program
    {
        static void Main(string[] args)
        {
            var identity = WindowsIdentity.GetCurrent();
            Debug.WriteLine(identity.Name);

            var account = new NTAccount(identity.Name);
            var sid = account.Translate(typeof(SecurityIdentifier));
            Debug.WriteLine(sid);

            foreach (var group in identity.Groups.Translate(typeof(NTAccount)))
            {
                Debug.WriteLine(group);
            }


            var principal = new System.Security.Principal.WindowsPrincipal(identity);

            var localAdmins = new SecurityIdentifier(WellKnownSidType.BuiltinAdministratorsSid, null);
            var domainAdmin = new SecurityIdentifier(WellKnownSidType.AccountDomainAdminsSid, identity.User.AccountDomainSid);
            var user = new SecurityIdentifier(WellKnownSidType.BuiltinUsersSid, null);

            Debug.WriteLine(principal.IsInRole(localAdmins));
            Debug.WriteLine(principal.IsInRole(domainAdmin));
            Debug.WriteLine(principal.IsInRole(user));
        }
    }
}
