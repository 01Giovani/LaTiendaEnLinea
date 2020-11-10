namespace TiendaEnLinea.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class estados_pedido : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Pedido", "IdEstado", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Pedido", "IdEstado");
        }
    }
}
