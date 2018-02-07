namespace Loja.Repositorios.SqlServer.EF.Migrations
{
    using Dominio;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Loja.Repositorios.SqlServer.EF.LojaDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "Loja.Repositorios.SqlServer.EF.LojaDbContext";
        }

        protected override void Seed(LojaDbContext context)
        {
            if (!context.Categorias.Any())
            {
                context.Categorias.AddRange(ObterCategorias());
                context.SaveChanges();
            }

            if (!context.Produtos.Any())
            {
                context.Produtos.AddRange(ObterProdutos(context));
                context.SaveChanges();
            }

            base.Seed(context);
        }

        private IEnumerable<Produto> ObterProdutos(LojaDbContext context)
        {
            var grampeador = new Produto();
            grampeador.Nome = "Grampeador";
            grampeador.Preco = 17.27m;
            grampeador.Estoque = 27;
            grampeador.Ativo = true;
            grampeador.Categoria = context.Categorias
                                            .Single(c => c.Nome == "Papelaria");

            var penDrive = new Produto();
            penDrive.Nome = "Pen Drive";
            penDrive.Preco = 17.32m;
            penDrive.Estoque = 32;
            penDrive.Ativo = true;
            penDrive.Categoria = context.Categorias
                                            .Single(c => c.Nome == "Informática");

            //return new List<Produto> { grampeador, penDrive };
            var produtos = new List<Produto>();
            produtos.Add(grampeador);
            produtos.Add(penDrive);

            return produtos;
        }

        private List<Categoria> ObterCategorias()
        {
            return new List<Categoria> {
                new Categoria { Nome = "Papelaria" },
                new Categoria { Nome = "Informática" },
                new Categoria { Nome = "Perfumaria" }
            };
        }
    }
}
