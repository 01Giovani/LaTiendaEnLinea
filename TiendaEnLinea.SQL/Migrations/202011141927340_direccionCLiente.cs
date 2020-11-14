namespace TiendaEnLinea.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class direccionCLiente : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Cliente", "Direccion", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Cliente", "Direccion");
        }
    }
}
