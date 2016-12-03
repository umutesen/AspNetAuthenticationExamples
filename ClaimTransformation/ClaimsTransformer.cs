using System.Collections.Generic;
using System.Security;
using System.Security.Claims;

namespace ClaimTransformation
{
    public class ClaimsTransformer : ClaimsAuthenticationManager
    {
        public override ClaimsPrincipal Authenticate(string resourceName, ClaimsPrincipal incomingPrincipal)
        {
            var name = incomingPrincipal.Identity.Name;

            if (string.IsNullOrWhiteSpace(name))
            {
                // exception
                throw new SecurityException("Name claim is missing");
            }
            return CreatePrincipal(name);
        }

        private ClaimsPrincipal CreatePrincipal(string name)
        {
            var hasLongName = name.Length > 3;

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, name),
                new Claim("NameLength",hasLongName.ToString())
            };

            var identityCollection = new ClaimsIdentity(claims, "Custom");

            return new ClaimsPrincipal(identityCollection);
        }
    }
}