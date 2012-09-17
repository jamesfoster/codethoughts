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
		// GET: /admin/blog/

		public ActionResult Index()
		{
			var posts = Posts.All().OrderByDescending(p => p.DateCreated).ToList();
			return View(posts);
		}

		//
		// GET: /admin/blog/hello-world

		public ActionResult Show(string id)
		{
			var post = Posts.FindByUrl(id);

			if (post == null)
				return HttpNotFound();

			return View(post);
		}

		//
		// GET: /admin/blog/New

		public ActionResult New()
		{
			var blogs = Blogs.All();
			var selected = blogs.Select(b => b.Id).FirstOrDefault();

			ViewBag.BlogId = new SelectList(blogs, "Id", "Name", selected);
			return View();
		}

		//
		// POST: /admin/blog/Create

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
			return View("New", post);
		}

		//
		// GET: /admin/blog/hello-world/edit

		public ActionResult Edit(string id)
		{
			var post = Posts.FindByUrl(id);

			if (post == null)
				return HttpNotFound();

			ViewBag.Blog = Blogs.Find(post.BlogId).Name;
			return View(post);
		}

		//
		// PUT: /admin/blog/hello-world

		[HttpPut]
		[ValidateInput(false)]
		public ActionResult Update(Post post)
		{
			if (ModelState.IsValid)
			{
				Posts.Update(post);
				return RedirectToAction("Index");
			}

			var p = Posts.Find(post.Id);
			ViewBag.Blog = Blogs.Find(p.BlogId).Name;

			return View("Edit", post);
		}

		//
		// GET: /admin/blog/hello-world/delete

		public ActionResult Delete(string id)
		{
			var post = Posts.FindByUrl(id);
			
			if (post == null)
				return HttpNotFound();

			return View(post);
		}

		//
		// DELETE: /admin/blog/hello-world

		[HttpDelete]
		public ActionResult Destroy(string id)
		{
			var post = Posts.FindByUrl(id);

			Posts.Delete(post);

			return RedirectToAction("Index");
		}
	}
}