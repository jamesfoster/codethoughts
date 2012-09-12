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
		// GET: /Post/Details/5

		public ActionResult Details(int id = 0)
		{
			Post post = Posts.Find(id);
			if (post == null)
			{
				return HttpNotFound();
			}
			return View(post);
		}
	}
}