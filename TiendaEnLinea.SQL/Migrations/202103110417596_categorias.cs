namespace TiendaEnLinea.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class categorias : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CategoriaProducto",
                c => new
                    {
                        Codigo = c.Int(nullable: false, identity: true),
                        Nombre = c.String(maxLength: 150),
                        Activa = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Codigo);
            
            AddColumn("dbo.Cliente", "RecibirNotificaciones", c => c.Boolean());
            AddColumn("dbo.Pedido", "IdTipoPago", c => c.Int());
            AddColumn("dbo.Producto", "IdCategoria", c => c.Int());
            CreateIndex("dbo.Producto", "IdCategoria");
            AddForeignKey("dbo.Producto", "IdCategoria", "dbo.CategoriaProducto", "Codigo");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Producto", "IdCategoria", "dbo.CategoriaProducto");
            DropIndex("dbo.Producto", new[] { "IdCategoria" });
            DropColumn("dbo.Producto", "IdCategoria");
            DropColumn("dbo.Pedido", "IdTipoPago");
            DropColumn("dbo.Cliente", "RecibirNotificaciones");
            DropTable("dbo.CategoriaProducto");
        }
    }
}
