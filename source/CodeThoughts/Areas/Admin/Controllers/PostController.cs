namespace CodeThoughts.Areas.Admin.Controllers
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
		// GET: /Admin/Post/

		public ActionResult Index()
		{
			IQueryable<Post> posts = db.Posts.Include(p => p.Blog);
			return View(posts.ToList());
		}

		//
		// GET: /Admin/Post/Details/5

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
		// GET: /Admin/Post/Create

		public ActionResult Create()
		{
			ViewBag.BlogId = new SelectList(db.Blogs, "Id", "Name");
			return View();
		}

		//
		// POST: /Admin/Post/Create

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
		// GET: /Admin/Post/Edit/5

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
		// POST: /Admin/Post/Edit/5

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
		// GET: /Admin/Post/Delete/5

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
		// POST: /Admin/Post/Delete/5

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