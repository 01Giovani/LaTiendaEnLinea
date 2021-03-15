namespace TiendaEnLinea.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class visitas : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Visita",
                c => new
                    {
                        Codigo = c.Int(nullable: false, identity: true),
                        Ip = c.String(maxLength: 50),
                        FechaIngreso = c.DateTimeOffset(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.Codigo);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Visita");
        }
    }
}
