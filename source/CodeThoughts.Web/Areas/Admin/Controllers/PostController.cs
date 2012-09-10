namespace CodeThoughts.Areas.Admin.Controllers
{
	using System.Web.Mvc;
	using Data;
	using Model;
	using System.Linq;

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
			var posts = Posts.All().OrderByDescending(p => p.DateCreated).ToList();
			return View(posts);
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
			var blogs = Blogs.All();
			var selected = blogs.Select(b => b.Id).FirstOrDefault();

			ViewBag.BlogId = new SelectList(blogs, "Id", "Name", selected);
			return View();
		}

		//
		// POST: /Admin/Post/Create

		[HttpPost]
		[ValidateInput(false)]
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
			ViewBag.Blog = Blogs.Find(post.BlogId).Name;
			return View(post);
		}

		//
		// POST: /Admin/Post/Edit/5

		[HttpPost]
		[ValidateInput(false)]
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