using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace Empresa.Mvc.Helpers
{
    public static class ClaimHelper
    {
        public static List<string> GetValues(this IEnumerable<Claim> claims, string tipo)
        {
            return claims.Where(c => c.Type == tipo).Select(c => c.Value).ToList();
        }
    }
}