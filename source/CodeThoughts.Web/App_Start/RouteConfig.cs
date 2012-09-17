namespace CodeThoughts
{
	using System.Web.Mvc;
	using System.Web.Routing;
	using Controllers;
	using RestfulRouting;

	public class RouteConfig
	{
		public static void RegisterRoutes(RouteCollection routes)
		{
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

			routes.MapRoutes<Routes>();
		}

		public class Routes : RouteSet
		{
			public override void Map(IMapper map)
			{
#if DEBUG
				map.DebugRoute("routedebug");
#endif
				map.Root<HomeController>(x => x.Index());

				map.Area<BlogController>("", defaultArea =>
					{
						defaultArea.Path("login").To<AccountController>(a => a.Login(null));
						defaultArea.Path("logoff").To<AccountController>(a => a.LogOff());

						defaultArea.Resources<PostController>(posts =>
							{
								posts.As("blog");

								posts.Only("show");
								posts.Resources<CommentController>(c => c.Except("destroy"));
							});
					});

				map.Area<Areas.Admin.Controllers.BlogController>("admin", admin =>
					{
						admin.Root<Areas.Admin.Controllers.BlogController>(b => b.Index());

						admin.Resources<Areas.Admin.Controllers.PostController>(posts =>
							{
								posts.As("blog");

								posts.Member(a => a.Get("delete"));

								posts.Resources<Areas.Admin.Controllers.CommentController>();
							});
					});
			}
		}
	}
}
