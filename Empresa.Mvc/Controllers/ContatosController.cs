using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Empresa.Repositorios.SqlServer;
using Empresa.Dominio;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Empresa.Mvc.Helpers;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Empresa.Mvc.Controllers
{
    public class ContatosController : Controller
    {
        private readonly EmpresaDbContext _db;// = new EmpresaDbContext();
        private readonly IDataProtector _protectorProvider;

        public ContatosController(EmpresaDbContext db, 
            IDataProtectionProvider protectionProvider, 
            IConfiguration configuracao)
        {
            //this.db = db;
            _db = db;
            _protectorProvider = protectionProvider.CreateProtector(configuracao.GetSection("ChaveCriptografia").Value);
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View(_db.Contatos.OrderBy(c => c.Nome).ToList());
        }

        //[Authorize(Roles = "Admin, Corretor")]
        public IActionResult Create()
        {
            var valores = User.Claims.GetValues("Contato");            

            return View();
        }

        [HttpPost]
        //[Authorize(Roles = "Corretor")]
        //[Authorize(Roles = "Admin")]
        public IActionResult Create(Contato contato)
        {
            var podeCriar = User.HasClaim("Contato", "Criar");

            if (!podeCriar)
            {
                return RedirectToAction("AcessoNegado", "Login");
            }

            contato.Senha = _protectorProvider.Protect(contato.Senha);

            _db.Contatos.Add(contato);
            _db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
