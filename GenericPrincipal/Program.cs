using System.Diagnostics;
using System.Security.Permissions;
using System.Security.Principal;
using System.Threading;

namespace GenericPrincipal
{
    class Program
    {
        static void Main(string[] args)
        {
            SetupPrincipal();
            UsePrincipal();
        }

        private static void SetupPrincipal()
        {
            // Authenticate client
            var identity = new GenericIdentity("bob");

            // fetch roles
            var roles = new[] { "Development", "Marketing" };

            var principal = new System.Security.Principal.GenericPrincipal(identity, roles);

            Thread.CurrentPrincipal = principal;
        }

        private static void UsePrincipal()
        {
            var p = Thread.CurrentPrincipal;

            Debug.WriteLine(p.Identity.Name);

            Debug.WriteLine(p.IsInRole("Development"));


            if (p.IsInRole("Marketing"))
            {
                // do something
            }
            else
            {
                // access denied
            }

            // throws exception
            new PrincipalPermission(null, "Development").Demand();

            DoDevWork();
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Development")]
        [PrincipalPermission(SecurityAction.Demand, Role = "SomeOther")]
        private static void DoDevWork()
        {
            Debug.WriteLine("You're a dev!");
        }

    }
}
