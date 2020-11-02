[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(TiendaEnLinea.Web.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(TiendaEnLinea.Web.App_Start.NinjectWebCommon), "Stop")]

namespace TiendaEnLinea.Web.App_Start
{
    using System;
    using System.Web;
    using Bitworks.Abstract.Logs;
    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;
    using Ninject.Web.Common.WebHost;
    using TiendaEnLinea.Core.Repositories;
    using TiendaEnLinea.Core.Services;
    using TiendaEnLinea.Service;
    using TiendaEnLinea.SQL;
    using TiendaEnLinea.SQL.Repositories;

    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application.
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }

        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }

        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();
                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {

            kernel.Bind<Bitworks.Data.BaseContext>().To<TiendaEnLineaContext>().InRequestScope();

            //REPOS
            kernel.Bind<ICheckoutRepository>().To<CheckoutRepository>().InRequestScope();
            kernel.Bind<IClienteRepository>().To<ClienteRepository>().InRequestScope();
            kernel.Bind<IMediasProductoRepository>().To<MediasProductoRepository>().InRequestScope();
            kernel.Bind<IMultimediaRepository>().To<MultimediaRepository>().InRequestScope();
            kernel.Bind<IPedidoRepository>().To<PedidoRepository>().InRequestScope();
            kernel.Bind<IProductoRepository>().To<ProductoRepository>().InRequestScope();
            kernel.Bind<IProductoPedidoRepository>().To<ProductoPedidoRepository>().InRequestScope();


            //SERVICIOS
            kernel.Bind<IProductoService>().To<ProductoService>().InRequestScope();
            kernel.Bind<IPedidoService>().To<PedidoService>().InRequestScope();

            //LOG
            kernel.Bind<IExtraerInfoExcepcion>().To<Bitworks.Logger.Utils.ExtraerInfoExcepcion>().InSingletonScope();
            kernel.Bind<Bitworks.Abstract.Logs.ILogBitworks>().To<Bitworks.Logger.BitworksLog>().InSingletonScope().WithConstructorArgument("nombreTrace", "BitworksTrace");
        }
    }
}