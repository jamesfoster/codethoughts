namespace MvcHelpers
{
	using System.Collections.Generic;
	using System.ComponentModel;
	using System.Dynamic;
	using System.Linq;
	using System.Web;
	using System.Web.Mvc;

	public static class HtmlHelperExtensions
	{
		public static IHtmlString Ticked(this HtmlHelper helper)
		{
			return new HtmlString("&#9745;");
		}

		public static IHtmlString Unticked(this HtmlHelper helper)
		{
			return new HtmlString("&#9744;");
		}

		public static IHtmlString Todo(this HtmlHelper helper, List<object> todos)
		{
			const string itemTemplate = @"
	<li class=""{0}"">
		<span class=""tick"">{1}</span>
		<span class=""description"">{2}</span>
		<span class=""date"">{3}</span>
		{4}
	</li>";

			var result = "";

			foreach (var todo in todos.Select(t => t.ToDynamic()))
			{
				var expando = (IDictionary<string, object>) todo;

				var inner = "";
				if (expando.ContainsKey("Inner") && todo.Inner != null)
					inner = Todo(helper, todo.Inner).ToHtmlString();

				result += string.Format(itemTemplate,
				                        todo.Complete ? "complete" : "incomplete",
				                        todo.Complete ? helper.Ticked() : helper.Unticked(),
				                        todo.Description,
				                        expando.ContainsKey("Date") ? todo.Date : "",
				                        inner);
			}

			result = string.Format(@"<ol class=""todo"">{0}</ol>", result);

			return new HtmlString(result);
		}
	}

	public static class DynamicExtensions
	{
		public static dynamic ToDynamic(this object value)
		{
			IDictionary<string, object> expando = new ExpandoObject();

			foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(value.GetType()))
				expando.Add(property.Name, property.GetValue(value));

			return expando as ExpandoObject;
		}
	}
}
