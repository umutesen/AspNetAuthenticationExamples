using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Claims;
using System.Threading;

namespace ClaimsPrincipal
{
    class Program
    {
        static void Main(string[] args)
        {
            SetupPrincipal();

            UsePrincipalLegacy();

            UsePrincipalNew();
        }

        private static void UsePrincipalNew()
        {
            //var cp = Thread.CurrentPrincipal as System.Security.Claims.ClaimsPrincipal;
            var cp = System.Security.Claims.ClaimsPrincipal.Current;

            Debug.WriteLine(cp.FindFirst(ClaimTypes.Email).Value);
        }

        private static void UsePrincipalLegacy()
        {
            var p = Thread.CurrentPrincipal;
            //Debug.WriteLine(p.Identity.IsAuthenticated);
            //Debug.WriteLine(p.IsInRole("Geek"));
        }

        private static void SetupPrincipal()
        {
            //var claim = new Claim("name","umut");

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, "umut"),
                new Claim(ClaimTypes.Email, "umutesen@me.com"),
                new Claim(ClaimTypes.Role, "Geek")
            };

            var id = new ClaimsIdentity(claims, "Console App", ClaimTypes.Name, ClaimTypes.Role);

            //Debug.WriteLine(id.IsAuthenticated);

            var principal = new System.Security.Claims.ClaimsPrincipal(id);

            Thread.CurrentPrincipal = principal;
        }
    }
}
