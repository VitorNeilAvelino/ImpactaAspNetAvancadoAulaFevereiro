using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Loja.Dominio;
using Loja.Repositorios.SqlServer.EF;
using Loja.Mvc.Helpers;
using Loja.Mvc.Areas.Vendas.Models;
using System;
using Microsoft.AspNet.SignalR;
using Loja.Mvc.Hubs;

namespace Loja.Mvc.Areas.Vendas.Controllers
{
    public class ProdutosController : Controller
    {
        // ToDo: design pattern Unity of Work.
        private LojaDbContext db = new LojaDbContext();
        private IHubContext _leilaoHub = GlobalHost.ConnectionManager.GetHubContext<LeilaoHub>();

        // GET: Produtos
        public ActionResult Index()
        {
            //throw new Exception("Erro na listagem de produtos.");
            return View(Mapeamento.Mapear(db.Produtos.ToList()));
        }

        // GET: Produtos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Produto produto = db.Produtos.Find(id);
            if (produto == null)
            {
                return HttpNotFound();
            }
            return View(Mapeamento.Mapear(produto));
        }

        // GET: Produtos/Create
        public ActionResult Create()
        {
            return View(Mapeamento.Mapear(new Produto(), db.Categorias.ToList()));
        }

        // POST: Produtos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProdutoViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                db.Produtos.Add(Mapeamento.Mapear(viewModel, db));
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(viewModel);
        }

        // GET: Produtos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Produto produto = db.Produtos.Find(id);

            if (produto == null)
            {
                return HttpNotFound();
            }

            return View(Mapeamento.Mapear(produto, db.Categorias.ToList()));
        }

        // POST: Produtos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProdutoViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var produto = db.Produtos.Find(viewModel.Id);

                Mapeamento.Mapear(viewModel, produto, db);

                //db.Entry(produto).State = EntityState.Modified;
                //db.Entry(produto).CurrentValues.SetValues(viewModel);
                //produto.Categoria = db.Categorias.Find(viewModel.CategoriaId);

                db.SaveChanges();

                _leilaoHub.Clients.All.atualizarOfertas();

                return RedirectToAction("Index");
            }
            return View(viewModel);
        }

        // GET: Produtos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Produto produto = db.Produtos.Find(id);

            if (produto == null)
            {
                return HttpNotFound();
            }

            return View(Mapeamento.Mapear(produto));
        }

        // POST: Produtos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Produto produto = db.Produtos.Find(id);
            db.Produtos.Remove(produto);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
