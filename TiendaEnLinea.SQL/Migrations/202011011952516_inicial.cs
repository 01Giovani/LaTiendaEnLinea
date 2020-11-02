namespace TiendaEnLinea.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class inicial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Beneficiario",
                c => new
                    {
                        Codigo = c.Guid(nullable: false),
                        NombreCompleto = c.String(maxLength: 500),
                        Telefono = c.String(maxLength: 25),
                        Direccion = c.String(maxLength: 500),
                        LatitudUbicacion = c.String(maxLength: 20),
                        LongitudUbicacion = c.String(maxLength: 20),
                        IdCliente = c.String(nullable: false, maxLength: 150),
                    })
                .PrimaryKey(t => t.Codigo)
                .ForeignKey("dbo.Cliente", t => t.IdCliente)
                .Index(t => t.IdCliente);
            
            CreateTable(
                "dbo.Cliente",
                c => new
                    {
                        Codigo = c.String(nullable: false, maxLength: 150),
                        NombreCompleto = c.String(maxLength: 500),
                        Telefono = c.String(maxLength: 25),
                        DUI = c.String(maxLength: 20),
                    })
                .PrimaryKey(t => t.Codigo);
            
            CreateTable(
                "dbo.CheckOut",
                c => new
                    {
                        Codigo = c.Int(nullable: false, identity: true),
                        IdPedido = c.Guid(nullable: false),
                        IdDetallePedido = c.Int(nullable: false),
                        Preparado = c.Boolean(nullable: false),
                        Comentario = c.String(maxLength: 500),
                    })
                .PrimaryKey(t => t.Codigo)
                .ForeignKey("dbo.ProductosPedido", t => t.IdDetallePedido)
                .ForeignKey("dbo.Pedido", t => t.IdPedido)
                .Index(t => t.IdPedido)
                .Index(t => t.IdDetallePedido);
            
            CreateTable(
                "dbo.ProductosPedido",
                c => new
                    {
                        Codigo = c.Int(nullable: false, identity: true),
                        IdProducto = c.Guid(nullable: false),
                        IdPedido = c.Guid(nullable: false),
                        Cantidad = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SubTotal = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Modificado = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Codigo)
                .ForeignKey("dbo.Pedido", t => t.IdPedido)
                .ForeignKey("dbo.Producto", t => t.IdProducto)
                .Index(t => t.IdProducto)
                .Index(t => t.IdPedido);
            
            CreateTable(
                "dbo.Pedido",
                c => new
                    {
                        Codigo = c.Guid(nullable: false),
                        FechaIngreso = c.DateTime(nullable: false, defaultValueSql: "getdate()"),
                        IdCliente = c.String(maxLength: 150),
                        IdBeneficiario = c.Guid(),
                        Total = c.Decimal(precision: 18, scale: 2),
                        Despachado = c.Boolean(nullable: false),
                        Comentario = c.String(maxLength: 500),
                        FechaEntregado = c.DateTime(),
                        OrdenEntrega = c.Int(),
                    })
                .PrimaryKey(t => t.Codigo)
                .ForeignKey("dbo.Beneficiario", t => t.IdBeneficiario)
                .ForeignKey("dbo.Cliente", t => t.IdCliente)
                .Index(t => t.IdCliente)
                .Index(t => t.IdBeneficiario);
            
            CreateTable(
                "dbo.Producto",
                c => new
                    {
                        Codigo = c.Guid(nullable: false),
                        Nombre = c.String(maxLength: 500),
                        Descripcion = c.String(maxLength: 500),
                        Precio = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PrecioOferta = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Destacado = c.Boolean(nullable: false),
                        Activo = c.Boolean(nullable: false),
                        MultiploVenta = c.Int(nullable: false),
                        PrefijoVenta = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.Codigo);
            
            CreateTable(
                "dbo.MediasProducto",
                c => new
                    {
                        Codigo = c.Int(nullable: false, identity: true),
                        IdProducto = c.Guid(nullable: false),
                        IdMultimedia = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Codigo)
                .ForeignKey("dbo.Multimedia", t => t.IdMultimedia)
                .ForeignKey("dbo.Producto", t => t.IdProducto)
                .Index(t => t.IdProducto)
                .Index(t => t.IdMultimedia);
            
            CreateTable(
                "dbo.Multimedia",
                c => new
                    {
                        Codigo = c.Guid(nullable: false),
                        NombreArchivo = c.String(maxLength: 150),
                        MimeType = c.String(maxLength: 150),
                        Contenido = c.Binary(),
                    })
                .PrimaryKey(t => t.Codigo);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CheckOut", "IdPedido", "dbo.Pedido");
            DropForeignKey("dbo.CheckOut", "IdDetallePedido", "dbo.ProductosPedido");
            DropForeignKey("dbo.ProductosPedido", "IdProducto", "dbo.Producto");
            DropForeignKey("dbo.MediasProducto", "IdProducto", "dbo.Producto");
            DropForeignKey("dbo.MediasProducto", "IdMultimedia", "dbo.Multimedia");
            DropForeignKey("dbo.ProductosPedido", "IdPedido", "dbo.Pedido");
            DropForeignKey("dbo.Pedido", "IdCliente", "dbo.Cliente");
            DropForeignKey("dbo.Pedido", "IdBeneficiario", "dbo.Beneficiario");
            DropForeignKey("dbo.Beneficiario", "IdCliente", "dbo.Cliente");
            DropIndex("dbo.MediasProducto", new[] { "IdMultimedia" });
            DropIndex("dbo.MediasProducto", new[] { "IdProducto" });
            DropIndex("dbo.Pedido", new[] { "IdBeneficiario" });
            DropIndex("dbo.Pedido", new[] { "IdCliente" });
            DropIndex("dbo.ProductosPedido", new[] { "IdPedido" });
            DropIndex("dbo.ProductosPedido", new[] { "IdProducto" });
            DropIndex("dbo.CheckOut", new[] { "IdDetallePedido" });
            DropIndex("dbo.CheckOut", new[] { "IdPedido" });
            DropIndex("dbo.Beneficiario", new[] { "IdCliente" });
            DropTable("dbo.Multimedia");
            DropTable("dbo.MediasProducto");
            DropTable("dbo.Producto");
            DropTable("dbo.Pedido");
            DropTable("dbo.ProductosPedido");
            DropTable("dbo.CheckOut");
            DropTable("dbo.Cliente");
            DropTable("dbo.Beneficiario");
        }
    }
}
