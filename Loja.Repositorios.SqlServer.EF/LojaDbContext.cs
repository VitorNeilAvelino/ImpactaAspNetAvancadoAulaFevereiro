using Loja.Dominio;
using Loja.Repositorios.SqlServer.EF.Migrations;
using Loja.Repositorios.SqlServer.EF.ModelConfiguration;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loja.Repositorios.SqlServer.EF
{
    public class LojaDbContext : IdentityDbContext<Usuario>
    {
        public LojaDbContext() : base("name=lojaConnectionString")
        {
            //pág. 191 - estratégias de inicialização.
            //Database.SetInitializer(new LojaDbInitializer());

            Database.SetInitializer(
                new MigrateDatabaseToLatestVersion<LojaDbContext, Configuration>());
        }

        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Categoria> Categorias { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            //modelBuilder.Entity<Usuario>().ToTable("Usuario");
            //modelBuilder.Entity<IdentityRole>().ToTable("Perfil");

            modelBuilder.Configurations.Add(new ProdutoConfiguration());
            modelBuilder.Configurations.Add(new ProdutoImagemConfiguration());
            modelBuilder.Configurations.Add(new CategoriaConfiguration());
        }
    }
}
