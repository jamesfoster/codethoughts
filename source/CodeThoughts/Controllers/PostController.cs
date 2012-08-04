namespace CodeThoughts.Controllers
{
	using System.Data;
	using System.Data.Entity;
	using System.Linq;
	using System.Web.Mvc;
	using Models;

	public class PostController : Controller
	{
		readonly BlogContext db = new BlogContext();

		//
		// GET: /Post/

		public ActionResult Index()
		{
			IQueryable<Post> posts = db.Posts.Include(p => p.Blog);
			return View(posts.ToList());
		}

		//
		// GET: /Post/Details/5

		public ActionResult Details(int id = 0)
		{
			Post post = db.Posts.Find(id);
			if (post == null)
			{
				return HttpNotFound();
			}
			return View(post);
		}

		protected override void Dispose(bool disposing)
		{
			db.Dispose();
			base.Dispose(disposing);
		}
	}
}