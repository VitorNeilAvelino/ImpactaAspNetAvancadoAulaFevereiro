using Loja.Dominio;
using Loja.Mvc.Areas.Vendas.Models;
using Loja.Repositorios.SqlServer.EF;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Loja.Mvc.Helpers
{
    public static class Mapeamento
    {
        public static List<ProdutoViewModel> Mapear(List<Produto> produtos)
        {
            var produtosViewModel = new List<ProdutoViewModel>();

            foreach (var produto in produtos)
            {
                produtosViewModel.Add(Mapear(produto));
            }

            return produtosViewModel;
        } 

        public static Produto Mapear(ProdutoViewModel viewModel, LojaDbContext dbContext/*, HttpPostedFileBase imagemProduto*/)
        {
            var produto = new Produto();

            if (viewModel.Imagem != null && viewModel.Imagem.ContentLength > 0)
            {
                using (var reader = new BinaryReader(viewModel.Imagem.InputStream))
                {
                    produto.Imagem = new ProdutoImagem
                    {
                        Bytes = reader.ReadBytes(viewModel.Imagem.ContentLength),
                        ContentType = viewModel.Imagem.ContentType
                    };
                }
            }

            produto.Categoria = dbContext.Categorias.Find(viewModel.CategoriaId);
            //produto.Descontinuado = viewModel.Descontinuado;
            produto.Estoque = viewModel.Estoque.Value;
            produto.Nome = viewModel.Nome;
            produto.Preco = viewModel.Preco.Value;
            produto.EmLeilao = viewModel.EmLeilao;

            return produto;
        }

        public static ProdutoViewModel Mapear(Produto produto, List<Categoria> categorias = null)
        {
            var viewModel = new ProdutoViewModel();

            viewModel.CategoriaId = produto.Categoria?.Id;
            viewModel.CategoriaNome = produto.Categoria?.Nome;

            if (categorias != null)
            {
                foreach (var categoria in categorias)
                {
                    viewModel.Categorias.Add(new SelectListItem { Text = categoria.Nome, Value = categoria.Id.ToString() });
                }
            }

            viewModel.Estoque = produto.Estoque;
            viewModel.Id = produto.Id;
            viewModel.Nome = produto.Nome;
            viewModel.Preco = produto.Preco;
            viewModel.EmLeilao = produto.EmLeilao;

            return viewModel;
        }

        public static void Mapear(ProdutoViewModel viewModel, Produto produto, LojaDbContext dbContext/*, HttpPostedFileBase imagemProduto*/)
        {
            dbContext.Entry(produto).CurrentValues.SetValues(viewModel);

            produto.Categoria = dbContext.Categorias.Single(c => c.Id == viewModel.CategoriaId);

            if (viewModel.Imagem != null && viewModel.Imagem.ContentLength > 0)
            {
                using (var reader = new BinaryReader(viewModel.Imagem.InputStream))
                {
                    if (produto.Imagem == null)
                    {
                        produto.Imagem = new ProdutoImagem
                        {
                            Bytes = reader.ReadBytes(viewModel.Imagem.ContentLength),
                            ContentType = viewModel.Imagem.ContentType
                        };
                    }
                    else
                    {
                        produto.Imagem.Bytes = reader.ReadBytes(viewModel.Imagem.ContentLength);
                        produto.Imagem.ContentType = viewModel.Imagem.ContentType;
                    }
                }
            }
        }
    }
}