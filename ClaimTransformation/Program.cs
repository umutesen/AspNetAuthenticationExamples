using System.IdentityModel.Services;
using System.Security.Principal;
using System.Threading;

namespace ClaimTransformation
{
    class Program
    {
        static void Main(string[] args)
        {

            SetPrincipal();
        }


        private static void SetPrincipal()
        {
            var p =
                new WindowsPrincipal(WindowsIdentity.GetCurrent());

            Thread.CurrentPrincipal =
                FederatedAuthentication.FederationConfiguration.IdentityConfiguration.ClaimsAuthenticationManager.Authenticate("none", p);
        }
    }
}
