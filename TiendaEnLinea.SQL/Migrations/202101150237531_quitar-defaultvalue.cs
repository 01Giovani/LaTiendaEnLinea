namespace TiendaEnLinea.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class quitardefaultvalue : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Pedido", "FechaIngreso", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Pedido", "FechaIngreso", c => c.DateTime(nullable: false));
        }
    }
}
