using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Empresa.Mvc.ViewModels;
using Empresa.Repositorios.SqlServer;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Empresa.Mvc.Controllers
{
    public class LoginController : Controller
    {
        private readonly EmpresaDbContext _db;// = new EmpresaDbContext();
        private readonly IDataProtector _protectorProvider;
        private string _tipoAutenticacao;

        public LoginController(EmpresaDbContext db,
            IDataProtectionProvider protectionProvider,
            IConfiguration configuracao)
        {
            //this.db = db;
            _db = db;
            _protectorProvider = protectionProvider.CreateProtector(configuracao.GetSection("ChaveCriptografia").Value);
            _tipoAutenticacao = configuracao.GetSection("TipoAutenticacao").Value;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(LoginViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var contato = _db.Contatos.Where(c => c.Email == viewModel.Email &&
               _protectorProvider.Unprotect(c.Senha) == viewModel.Senha).SingleOrDefault();

            if (contato == null)
            {
                ModelState.AddModelError("", "Usuário/Senha incorretos.");

                return View(viewModel);
            }

            var claims = new List<Claim> {
                new Claim(ClaimTypes.Name, contato.Nome),
                new Claim(ClaimTypes.Email, contato.Email),

                //new Claim(ClaimTypes.Role, "Admin"),
                new Claim(ClaimTypes.Role, "Vendedor"),
                new Claim("Contato", "Criar")
            };

            var identidade = new ClaimsIdentity(claims, _tipoAutenticacao);
            var principal = new ClaimsPrincipal(identidade);

            HttpContext.Authentication.SignInAsync(_tipoAutenticacao, principal);

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Logout()
        {
            HttpContext.Authentication.SignOutAsync(_tipoAutenticacao);

            return RedirectToAction("Index", "Home");
        }

        public IActionResult AcessoNegado()
        {
            return View();
        }
    }
}