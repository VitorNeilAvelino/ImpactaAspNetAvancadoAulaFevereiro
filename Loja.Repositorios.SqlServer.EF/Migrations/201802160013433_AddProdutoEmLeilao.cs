namespace Loja.Repositorios.SqlServer.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddProdutoEmLeilao : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Produto", "EmLeilao", c => c.Boolean(nullable: false, defaultValue: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Produto", "EmLeilao");
        }
    }
}
