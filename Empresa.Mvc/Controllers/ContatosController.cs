using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Empresa.Repositorios.SqlServer;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Empresa.Mvc.Controllers
{
    public class ContatosController : Controller
    {
        private EmpresaDbContext _db;// = new EmpresaDbContext();

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }
    }
}
