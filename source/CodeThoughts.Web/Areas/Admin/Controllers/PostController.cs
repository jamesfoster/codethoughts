namespace CodeThoughts.Areas.Admin.Controllers
{
	using System.Web.Mvc;
	using Data;
	using Model;

	public class PostController : AdminController
	{
		public IPostRepository Posts { get; set; }
		public IBlogRepository Blogs { get; set; }

		public PostController(IPostRepository postRepository, IBlogRepository blogRepository)
		{
			Posts = postRepository;
			Blogs = blogRepository;
		}

		//
		// GET: /Admin/Post/

		public ActionResult Index()
		{
			return View(Posts.All());
		}

		//
		// GET: /Admin/Post/Details/5

		public ActionResult Details(int id = 0)
		{
			Post post = Posts.Find(id);
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
			ViewBag.BlogId = new SelectList(Blogs.All(), "Id", "Name");
			return View();
		}

		//
		// POST: /Admin/Post/Create

		[HttpPost]
		public ActionResult Create(Post post)
		{
			if (ModelState.IsValid)
			{
				Posts.Add(post);
				return RedirectToAction("Index");
			}

			ViewBag.BlogId = new SelectList(Blogs.All(), "Id", "Name", post.BlogId);
			return View(post);
		}

		//
		// GET: /Admin/Post/Edit/5

		public ActionResult Edit(int id = 0)
		{
			Post post = Posts.Find(id);
			if (post == null)
			{
				return HttpNotFound();
			}
			ViewBag.BlogId = new SelectList(Blogs.All(), "Id", "Name", post.BlogId);
			return View(post);
		}

		//
		// POST: /Admin/Post/Edit/5

		[HttpPost]
		public ActionResult Edit(Post post)
		{
			if (ModelState.IsValid)
			{
				Posts.Update(post);
				return RedirectToAction("Index");
			}
			ViewBag.BlogId = new SelectList(Blogs.All(), "Id", "Name", post.BlogId);
			return View(post);
		}

		//
		// GET: /Admin/Post/Delete/5

		public ActionResult Delete(int id = 0)
		{
			Post post = Posts.Find(id);
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
			Post post = Posts.Find(id);
			Posts.Delete(post);
			return RedirectToAction("Index");
		}
	}
}