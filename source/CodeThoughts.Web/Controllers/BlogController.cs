namespace CodeThoughts.Controllers
{
	using System.Web.Mvc;
	using Models;

	public class BlogController : Controller
	{
		public IBlogRepository Blogs { get; set; }

		public BlogController(IBlogRepository blogRepository)
		{
			Blogs = blogRepository;
		}

		//
		// GET: /Blog/

		public ActionResult Index()
		{
			return View(Blogs.All());
		}

		//
		// GET: /Blog/Details/5

		public ActionResult Details(int id = 0)
		{
			Blog blog = Blogs.Find(id);
			if (blog == null)
			{
				return HttpNotFound();
			}
			return View(blog);
		}
	}
}