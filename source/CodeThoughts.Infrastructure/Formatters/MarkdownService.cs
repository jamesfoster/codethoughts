namespace CodeThoughts.Infrastructure.Formatters
{
	using MarkdownSharp;

	public class MarkdownService
	{
		protected static Tranformers Tranformers { get; set; }
		protected Markdown Markdown { get; set; }

		public MarkdownService(Tranformers tranformers)
		{
			Tranformers = tranformers;
			Markdown = new Markdown(CreateMarkdownOptions());
		}

		private static MarkdownOptions CreateMarkdownOptions()
		{
			return new MarkdownOptions
			{
				AutoHyperlink = true,
				AutoNewLines = false,
				EncodeProblemUrlCharacters = true,
				LinkEmails = true
			};
		}

		public string ToHtml(string content)
		{
			foreach (var transformer in Tranformers.GetTransformers())
			{
				content = transformer(content);
			}

			return Markdown.Transform(content);
		}
	}
}