namespace TiendaEnLinea.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class precioEnDetalle : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProductosPedido", "Precio", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ProductosPedido", "Precio");
        }
    }
}
