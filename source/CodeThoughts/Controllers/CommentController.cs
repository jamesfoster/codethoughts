namespace CodeThoughts.Controllers
{
	using System.Data;
	using System.Data.Entity;
	using System.Linq;
	using System.Web.Mvc;
	using Models;

	public class CommentController : Controller
	{
		readonly BlogContext db = new BlogContext();

		//
		// GET: /Comment/

		public ActionResult Index()
		{
			IQueryable<Comment> comments = db.Comments.Include(c => c.Post);
			return View(comments.ToList());
		}

		//
		// GET: /Comment/Details/5

		public ActionResult Details(int id = 0)
		{
			Comment comment = db.Comments.Find(id);
			if (comment == null)
			{
				return HttpNotFound();
			}
			return View(comment);
		}

		//
		// GET: /Comment/Create

		public ActionResult Create()
		{
			ViewBag.PostId = new SelectList(db.Posts, "Id", "Title");
			return View();
		}

		//
		// POST: /Comment/Create

		[HttpPost]
		public ActionResult Create(Comment comment)
		{
			if (ModelState.IsValid)
			{
				db.Comments.Add(comment);
				db.SaveChanges();
				return RedirectToAction("Index");
			}

			ViewBag.PostId = new SelectList(db.Posts, "Id", "Title", comment.PostId);
			return View(comment);
		}

		protected override void Dispose(bool disposing)
		{
			db.Dispose();
			base.Dispose(disposing);
		}
	}
}