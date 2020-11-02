namespace TiendaEnLinea.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class nullablePrecioOferta : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Producto", "PrecioOferta", c => c.Decimal(precision: 18, scale: 2,nullable:true));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Producto", "PrecioOferta", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
    }
}
