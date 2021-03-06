﻿using System.Web.Mvc;
using CodeThoughts.Controllers;
using NUnit.Framework;

namespace CodeThoughts.Web.Tests.Controllers
{
	using Data;
	using Moq;

	[TestFixture]
	public class BlogControllerTests
	{
		[Test]
		public void IndexReturnsViewResult()
		{
			var blogRepository = new Mock<IBlogRepository>();

			var controller = new BlogController(blogRepository.Object);

			var result = controller.Index();

			Assert.That(result, Is.AssignableFrom<ViewResult>());
		}
	}
}
