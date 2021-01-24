using AutoMapper;
using Bitworks.Extensiones.Bytes;
using Bitworks.Extensiones.Imagenes;
using Bitworks.Seguridad.MVC.Atributos;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TiendaEnLinea.Core.DTOs.Producto;
using TiendaEnLinea.Core.Model;
using TiendaEnLinea.Core.Services;

namespace TiendaEnLinea.Web.Controllers
{
    public class ProductoController : Controller
    {
        private static IMapper _mapper;
        private IProductoService _productoService;
        public ProductoController(
            IProductoService productoService
            )
        {
            _productoService = productoService;
        }

        static ProductoController()
        {

            var config = new AutoMapper.MapperConfiguration(x =>
            {
                x.CreateMap<Producto, ListaProductoDTO>();
                x.CreateMap<Producto, DetalleProductoDTO>();
                
            });

            _mapper = config.CreateMapper();
        }

        [CaracteristicasAccion("Listado de productos",false,true)]
        [HttpGet]
        public ActionResult Index()
        {
            List<Producto> lista = _productoService.GetProductos();
            List<ListaProductoDTO> productos = _mapper.Map<List<ListaProductoDTO>>(lista);

            return View(productos);
        }

        [CaracteristicasAccion("Detalle de producto",false,true)]
        [HttpGet]
        public ActionResult Detalle(Guid? id = null)
        {

            DetalleProductoDTO model = new DetalleProductoDTO();
            if (id != null) {
                Producto producto = _productoService.GetProducto(id.Value);
                if(producto != null)
                    model= _mapper.Map<DetalleProductoDTO>(producto);
            }

            
            return View(model);
        }

        [CaracteristicasAccion("Guardar producto",false,true)]
        [HttpPost]
        public ActionResult Detalle(DetalleProductoDTO data) {

            decimal precio = decimal.Parse(data.Precio);
            decimal? precioOferta = null;
            if(!string.IsNullOrEmpty(data.PrecioOferta))
                precioOferta = decimal.Parse(data.PrecioOferta);

            decimal multiplo = 1;
            if (!string.IsNullOrEmpty(data.MultiploVenta))
                multiplo = decimal.Parse(data.MultiploVenta);
            
            Producto saved = _productoService.GuardarProducto(new Producto()
            {
                Codigo = data.Codigo ?? Guid.NewGuid(),
                Nombre = data.Nombre,
                Descripcion = data.Descripcion,
                Precio =precio,
                PrecioOferta = precioOferta,
                Destacado = data.Destacado,
                Activo  = data.Activo,
                MultiploVenta = multiplo,
                PrefijoVenta = data.PrefijoVenta,
                DescripcionOferta = data.DescripcionOferta

            });

            return RedirectToAction("Detalle",new { id = saved.Codigo });
        }

        [CaracteristicasAccion("Guardar fotos producto",false,true)]
        [HttpPost]
        public ActionResult GuardarImagen(MultimediaProductoDTO data) {


            Multimedia multimedia = _productoService.GuardarMultimedia(new Multimedia() { 
                 Codigo = Guid.NewGuid(),
                 Contenido = PostedFileBaseToBytes(data.File),
                 MimeType= data.File.ContentType,
                 NombreArchivo = data.File.FileName
            });

            _productoService.GuardarImagen(new MediasProducto() { 
                IdProducto =data.IdProducto,
                IdMultimedia = multimedia.Codigo              
            });

            return RedirectToAction("Detalle",new { id = data.IdProducto });
        }

        private byte[] PostedFileBaseToBytes(HttpPostedFileBase file)
        {
            MemoryStream target = new MemoryStream();
            file.InputStream.CopyTo(target);
            return target.ToArray();
        }

        [CaracteristicasAccion("Eliminar imagen",false,true)]
        [HttpGet]
        public ActionResult EliminarImagen(int id,Guid idProducto)
        {
            _productoService.EliminarImagen(id);

            return RedirectToAction("Detalle", new { id = idProducto });
        }

        [CaracteristicasAccion("Get multimedia",false,false)]
        [OutputCache(Duration = 7200, Location = System.Web.UI.OutputCacheLocation.Client)]
        public ActionResult GetMultimediaConLlave(Guid id)
        {
            Multimedia media = _productoService.GetMultimedia(id);
            if (media != null)
            {
                var r =
               media.Contenido.bwGetImagen().bwCambiarTamanoConRelacionByte(new Size(500, 500), 80, true);

                return File(r, r.bwGetMimeType());
            }  
                            
            else
                throw new HttpException(404, "El archivo seleccionado no existe");

        }
    }
}