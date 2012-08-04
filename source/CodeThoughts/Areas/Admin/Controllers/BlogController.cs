namespace CodeThoughts.Areas.Admin.Controllers
{
	using System.Data;
	using System.Linq;
	using System.Web.Mvc;
	using Models;

	public class BlogController : Controller
	{
		readonly BlogContext db = new BlogContext();

		//
		// GET: /Admin/Blog/

		public ActionResult Index()
		{
			return View(db.Blogs.ToList());
		}

		//
		// GET: /Admin/Blog/Details/5

		public ActionResult Details(int id = 0)
		{
			Blog blog = db.Blogs.Find(id);
			if (blog == null)
			{
				return HttpNotFound();
			}
			return View(blog);
		}

		//
		// GET: /Admin/Blog/Create

		public ActionResult Create()
		{
			return View();
		}

		//
		// POST: /Admin/Blog/Create

		[HttpPost]
		public ActionResult Create(Blog blog)
		{
			if (ModelState.IsValid)
			{
				db.Blogs.Add(blog);
				db.SaveChanges();
				return RedirectToAction("Index");
			}

			return View(blog);
		}

		//
		// GET: /Admin/Blog/Edit/5

		public ActionResult Edit(int id = 0)
		{
			Blog blog = db.Blogs.Find(id);
			if (blog == null)
			{
				return HttpNotFound();
			}
			return View(blog);
		}

		//
		// POST: /Admin/Blog/Edit/5

		[HttpPost]
		public ActionResult Edit(Blog blog)
		{
			if (ModelState.IsValid)
			{
				db.Entry(blog).State = EntityState.Modified;
				db.SaveChanges();
				return RedirectToAction("Index");
			}
			return View(blog);
		}

		//
		// GET: /Admin/Blog/Delete/5

		public ActionResult Delete(int id = 0)
		{
			Blog blog = db.Blogs.Find(id);
			if (blog == null)
			{
				return HttpNotFound();
			}
			return View(blog);
		}

		//
		// POST: /Admin/Blog/Delete/5

		[HttpPost, ActionName("Delete")]
		public ActionResult DeleteConfirmed(int id)
		{
			Blog blog = db.Blogs.Find(id);
			db.Blogs.Remove(blog);
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