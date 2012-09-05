using CodeThoughts;

[assembly: WebActivator.PreApplicationStartMethod(typeof(NinjectWebCommon), "Start")]
[assembly: WebActivator.ApplicationShutdownMethodAttribute(typeof(NinjectWebCommon), "Stop")]

namespace CodeThoughts
{
	using System;
	using System.Web;
	using Data;
	using Microsoft.Web.Infrastructure.DynamicModuleHelper;
	using Ninject;
	using Ninject.Web.Common;

	public static class NinjectWebCommon
	{
		static readonly Bootstrapper bootstrapper = new Bootstrapper();

		/// <summary>
		/// Starts the application
		/// </summary>
		public static void Start()
		{
			DynamicModuleUtility.RegisterModule(typeof (OnePerRequestHttpModule));
			DynamicModuleUtility.RegisterModule(typeof (NinjectHttpModule));
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
		static IKernel CreateKernel()
		{
			var kernel = new StandardKernel();
			kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
			kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

			RegisterServices(kernel);
			return kernel;
		}

		/// <summary>
		/// Load your modules or register your services here!
		/// </summary>
		/// <param name="kernel">The kernel.</param>
		static void RegisterServices(IKernel kernel)
		{
			kernel.Bind<IBlogRepository>().To<EfBlogRepository>();
			kernel.Bind<IPostRepository>().To<EfPostRepository>();
			kernel.Bind<ICommentRepository>().To<EfCommentRepository>();

			kernel.Bind<BlogContext>().ToMethod(ctx => new BlogContext());
		}
	}
}