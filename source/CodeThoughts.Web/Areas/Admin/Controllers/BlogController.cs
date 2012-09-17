namespace CodeThoughts.Areas.Admin.Controllers
{
	using System.Web.Mvc;
	using Data;
	using Model;

	public class BlogController : AdminController
	{
		public IBlogRepository Blogs { get; set; }

		public BlogController(IBlogRepository blogRepository)
		{
			Blogs = blogRepository;
		}

		//
		// GET: /admin/

		public ActionResult Index()
		{
			return View(Blogs.All());
		}
	}
}