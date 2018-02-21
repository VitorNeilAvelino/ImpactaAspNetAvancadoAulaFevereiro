using Microsoft.VisualStudio.TestTools.UnitTesting;
using Loja.Mvc.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Loja.Dominio;

namespace Loja.Mvc.Helpers.Tests
{
    [TestClass()]
    public class MapeamentoTests
    {
        [TestMethod()]
        public void MapearTest()
        {
            //Arranje
            var produto = new Produto();
            produto.Ativo = true;
            produto.Categoria = new Categoria { Nome = "Laticínios" };
            produto.EmLeilao = false;
            produto.Estoque = 24;
            produto.Id = 12;
            produto.Nome = "Manteiga";
            produto.Preco = 22.25m;

            //Act
            var viewModel = Mapeamento.Mapear(produto);

            //Assert
            Assert.AreEqual(produto.EmLeilao, viewModel.EmLeilao);
            Assert.AreEqual(produto.Categoria.Nome, viewModel.CategoriaNome);
            Assert.AreEqual(produto.Preco, viewModel.Preco);
        }
    }
}