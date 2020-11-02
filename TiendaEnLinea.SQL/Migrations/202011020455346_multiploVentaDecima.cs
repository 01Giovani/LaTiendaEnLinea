namespace TiendaEnLinea.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class multiploVentaDecima : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Producto", "MultiploVenta", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Producto", "MultiploVenta", c => c.Int(nullable: false));
        }
    }
}
