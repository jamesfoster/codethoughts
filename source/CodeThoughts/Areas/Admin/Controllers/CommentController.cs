namespace CodeThoughts.Areas.Admin.Controllers
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
		// GET: /Admin/Comment/

		public ActionResult Index()
		{
			IQueryable<Comment> comments = db.Comments.Include(c => c.Post);
			return View(comments.ToList());
		}

		//
		// GET: /Admin/Comment/Details/5

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
		// GET: /Admin/Comment/Create

		public ActionResult Create()
		{
			ViewBag.PostId = new SelectList(db.Posts, "Id", "Title");
			return View();
		}

		//
		// POST: /Admin/Comment/Create

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

		//
		// GET: /Admin/Comment/Edit/5

		public ActionResult Edit(int id = 0)
		{
			Comment comment = db.Comments.Find(id);
			if (comment == null)
			{
				return HttpNotFound();
			}
			ViewBag.PostId = new SelectList(db.Posts, "Id", "Title", comment.PostId);
			return View(comment);
		}

		//
		// POST: /Admin/Comment/Edit/5

		[HttpPost]
		public ActionResult Edit(Comment comment)
		{
			if (ModelState.IsValid)
			{
				db.Entry(comment).State = EntityState.Modified;
				db.SaveChanges();
				return RedirectToAction("Index");
			}
			ViewBag.PostId = new SelectList(db.Posts, "Id", "Title", comment.PostId);
			return View(comment);
		}

		//
		// GET: /Admin/Comment/Delete/5

		public ActionResult Delete(int id = 0)
		{
			Comment comment = db.Comments.Find(id);
			if (comment == null)
			{
				return HttpNotFound();
			}
			return View(comment);
		}

		//
		// POST: /Admin/Comment/Delete/5

		[HttpPost, ActionName("Delete")]
		public ActionResult DeleteConfirmed(int id)
		{
			Comment comment = db.Comments.Find(id);
			db.Comments.Remove(comment);
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