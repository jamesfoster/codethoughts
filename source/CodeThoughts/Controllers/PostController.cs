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

		//
		// GET: /Post/Create

		public ActionResult Create()
		{
			ViewBag.BlogId = new SelectList(db.Blogs, "Id", "Name");
			return View();
		}

		//
		// POST: /Post/Create

		[HttpPost]
		public ActionResult Create(Post post)
		{
			if (ModelState.IsValid)
			{
				db.Posts.Add(post);
				db.SaveChanges();
				return RedirectToAction("Index");
			}

			ViewBag.BlogId = new SelectList(db.Blogs, "Id", "Name", post.BlogId);
			return View(post);
		}

		//
		// GET: /Post/Edit/5

		public ActionResult Edit(int id = 0)
		{
			Post post = db.Posts.Find(id);
			if (post == null)
			{
				return HttpNotFound();
			}
			ViewBag.BlogId = new SelectList(db.Blogs, "Id", "Name", post.BlogId);
			return View(post);
		}

		//
		// POST: /Post/Edit/5

		[HttpPost]
		public ActionResult Edit(Post post)
		{
			if (ModelState.IsValid)
			{
				db.Entry(post).State = EntityState.Modified;
				db.SaveChanges();
				return RedirectToAction("Index");
			}
			ViewBag.BlogId = new SelectList(db.Blogs, "Id", "Name", post.BlogId);
			return View(post);
		}

		//
		// GET: /Post/Delete/5

		public ActionResult Delete(int id = 0)
		{
			Post post = db.Posts.Find(id);
			if (post == null)
			{
				return HttpNotFound();
			}
			return View(post);
		}

		//
		// POST: /Post/Delete/5

		[HttpPost, ActionName("Delete")]
		public ActionResult DeleteConfirmed(int id)
		{
			Post post = db.Posts.Find(id);
			db.Posts.Remove(post);
			db.SaveChanges();
			return RedirectToAction("Index");
		}

		protected override void Dispose(bool disposing)
		{
			db.Dispose();
			base.Dispose(disposing);
		}
	}
}