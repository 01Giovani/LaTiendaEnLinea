namespace TiendaEnLinea.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class descripcionoferta : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Producto", "DescripcionOferta", c => c.String(nullable:true));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Producto", "DescripcionOferta");
        }
    }
}
