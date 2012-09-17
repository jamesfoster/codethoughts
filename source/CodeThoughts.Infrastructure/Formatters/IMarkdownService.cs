namespace CodeThoughts.Infrastructure.Formatters
{
	public interface IMarkdownService
	{
		string ToHtml(string content);
	}
}