namespace CodeThoughts.Controllers
{
	using System.Data;
	using System.Linq;
	using System.Web.Mvc;
	using Models;

	public class BlogController : Controller
	{
		readonly BlogContext db = new BlogContext();

		//
		// GET: /Blog/

		public ActionResult Index()
		{
			return View(db.Blogs.ToList());
		}

		//
		// GET: /Blog/Details/5

		public ActionResult Details(int id = 0)
		{
			Blog blog = db.Blogs.Find(id);
			if (blog == null)
			{
				return HttpNotFound();
			}
			return View(blog);
		}

		protected override void Dispose(bool disposing)
		{
			db.Dispose();
			base.Dispose(disposing);
		}
	}
}