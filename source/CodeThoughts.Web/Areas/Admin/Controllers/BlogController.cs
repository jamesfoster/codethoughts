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
		// GET: /Admin/Blog/

		public ActionResult Index()
		{
			return View(Blogs.All());
		}

		//
		// GET: /Admin/Blog/Details/5

		public ActionResult Details(int id = 0)
		{
			Blog blog = Blogs.Find(id);
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
				Blogs.Add(blog);
				return RedirectToAction("Index");
			}

			return View(blog);
		}

		//
		// GET: /Admin/Blog/Edit/5

		public ActionResult Edit(int id = 0)
		{
			Blog blog = Blogs.Find(id);
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
				Blogs.Update(blog);
				return RedirectToAction("Index");
			}
			return View(blog);
		}

		//
		// GET: /Admin/Blog/Delete/5

		public ActionResult Delete(int id = 0)
		{
			Blog blog = Blogs.Find(id);
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
			Blog blog = Blogs.Find(id);
			Blogs.Delete(blog);
			return RedirectToAction("Index");
		}
	}
}