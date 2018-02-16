using Loja.Mvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Loja.Mvc.Filters
{
    public class AuthorizeRole : AuthorizeAttribute
    {
        public AuthorizeRole(params Perfil[] perfis)
        {
            foreach (var perfil in perfis)
            {
                Roles += perfil + ",";
            }

            //Roles = string.Join(",", perfis.Select(p => Enum.GetName(p.GetType(), p)));
        }
    }
}