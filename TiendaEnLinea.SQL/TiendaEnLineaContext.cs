using Bitworks.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaEnLinea.Core.Model;
using TiendaEnLinea.SQL.Mapping;

namespace TiendaEnLinea.SQL
{
    public class TiendaEnLineaContext : BaseContext
    {
        public TiendaEnLineaContext() : base("Name=TiendaEnLineaContext", TipoGeneracionMappin.Ninguno)
        {
        }

        public DbSet<Beneficiario> Beneficiario { get; set; }
        public DbSet<CheckOut> CheckOut { get; set; }
        public DbSet<Cliente> Cliente { get; set; }
        public DbSet<MediasProducto> MediasProducto { get; set; }
        public DbSet<Multimedia> Multimedia { get; set; }
        public DbSet<Pedido> Pedido { get; set; }
        public DbSet<Producto> Producto { get; set; }
        public DbSet<ProductosPedido> ProductosPedido { get; set; }

        public DbSet<CategoriaProducto> CategoriaProducto { get; set; }

        protected override void OnModelCreating(DbModelBuilder md)
        {
            base.OnModelCreating(md);

            md.Configurations.Add(new BeneficiarioMap());
            md.Configurations.Add(new CheckOutMap());
            md.Configurations.Add(new ClienteMap());
            md.Configurations.Add(new MediasProductoMap());
            md.Configurations.Add(new MultimediaMap());
            md.Configurations.Add(new PedidoMap());
            md.Configurations.Add(new ProductoMap());
            md.Configurations.Add(new ProductoPedidoMap());
            md.Configurations.Add(new CategoriaProductoMap());
        }
        
    }
}
