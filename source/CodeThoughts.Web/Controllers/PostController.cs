namespace CodeThoughts.Controllers
{
	using System.Web.Mvc;
	using Data;
	using Model;

	public class PostController : Controller
	{
		public IPostRepository Posts { get; set; }

		public PostController(IPostRepository postRepository)
		{
			Posts = postRepository;
		}

		//
		// GET: /blog/hello-world

		public ActionResult Show(string id)
		{
			var post = Posts.FindByUrl(id);

			if (post == null)
				return HttpNotFound();

			return View(post);
		}
	}
}