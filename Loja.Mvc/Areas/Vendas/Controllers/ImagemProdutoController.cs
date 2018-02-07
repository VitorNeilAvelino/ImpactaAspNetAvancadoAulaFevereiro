using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using Loja.Repositorios.SqlServer.EF;
using System.Data.Entity;

namespace Loja.Mvc.Areas.Vendas.Controllers
{
    public class ImagemProdutoController : Controller
    {
        private readonly LojaDbContext _contexto = new LojaDbContext();

        public ActionResult Index(int produtoId)
        {
            var produto = _contexto.Produtos.Include(p => p.Imagem).Single(p => p.Id == produtoId);

            return File(produto.Imagem.Bytes, produto.Imagem.ContentType);
        }

        public ActionResult Miniatura(int produtoId, int largura = 50, int altura = 50)
        {
            var produto = _contexto.Produtos.Include(p => p.Imagem).Single(p => p.Id == produtoId);

            if (produto.Imagem == null)
            {
                return null;
            }

            return File(ObterMiniatura(produto.Imagem.Bytes, largura, altura), produto.Imagem.ContentType);
        }

        public static byte[] ObterMiniatura(byte[] imagem, int largura, int altura)
        {
            using (var stream = new MemoryStream())
            {
                using (var miniatura = Image.FromStream(new MemoryStream(imagem)).GetThumbnailImage(largura, altura, null, new IntPtr()))
                {
                    miniatura.Save(stream, ImageFormat.Png);
                    return stream.ToArray();
                }
            }
        }

        protected override void Dispose(bool disposing)
        {
            _contexto.Dispose();

            base.Dispose(disposing);
        }
    }
}