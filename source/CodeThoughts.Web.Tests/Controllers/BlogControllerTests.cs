using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using CodeThoughts.Controllers;
using NUnit.Framework;

namespace CodeThoughts.Web.Tests.Controllers
{
	[TestFixture]
	public class BlogControllerTests
	{
		[Test]
		public void IndexReturnsViewResult()
		{
			var controller = new BlogController();

			var result = controller.Index();

			Assert.That(result, Is.AssignableFrom<ViewResult>());
		}
	}
}
