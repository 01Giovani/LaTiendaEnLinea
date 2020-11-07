namespace TiendaEnLinea.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class campos_pedido : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Pedido", "Completado", c => c.Boolean(nullable: false));
            AddColumn("dbo.Pedido", "FechaCompletado", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Pedido", "FechaCompletado");
            DropColumn("dbo.Pedido", "Completado");
        }
    }
}
