using CodeThoughts;

[assembly: WebActivator.PreApplicationStartMethod(typeof(NinjectWebCommon), "Start")]
[assembly: WebActivator.ApplicationShutdownMethodAttribute(typeof(NinjectWebCommon), "Stop")]

namespace CodeThoughts
{
	using System;
	using System.Web;
	using System.Web.Security;
	using Data;
	using Infrastructure;
	using Microsoft.Web.Infrastructure.DynamicModuleHelper;
	using Ninject;
	using Ninject.Activation;
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
			var kernel = new StandardKernel(new NinjectSettings
				{
					LoadExtensions = false
				});
			kernel.Load(new Ninject.Web.Mvc.MvcModule());
			kernel.Load(new InfrastructureModule());

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
			kernel.Bind<ICommentRepository>().To<EfCommentRepository>();

			kernel.Bind<IPostRepository>().To<EfPostRepository>();
			kernel.Bind<IPostRepository>().To<PublishedPostRepository>().When(OnlyShowPublishedPosts);

			kernel.Bind<BlogContext>().ToMethod(ctx => new BlogContext());
		}

		static bool OnlyShowPublishedPosts(IRequest request)
		{
			return request.ParentRequest.Service != typeof (IPostRepository) &&
			       !CanViewUnpublishedPosts();
		}

		static bool CanViewUnpublishedPosts()
		{
			var user = HttpContext.Current.User.Identity;

			return user.IsAuthenticated && Roles.IsUserInRole(user.Name, "admin");
		}
	}
}
