namespace CodeThoughts.Infrastructure.Formatters
{
	using System;
	using System.Collections.Generic;
	using System.IO;
	using System.Linq;
	using System.Text;
	using System.Text.RegularExpressions;
	using ColorCode;

	public class Tranformers
	{
		private const string CodeBlockMarker = "```";

		private readonly ICodeColorizer _colorizer;
		readonly IFormatter _formatter;
		readonly IStyleSheet _styleSheet;

		private Regex _cSharpCodeBlocksRegExPreTrans;
		private Regex _jsCodeBlocksRegExPreTrans;
		private Regex _htmlCodeBlocksRegExPreTrans;
		private Regex _cssCodeBlocksRegExPreTrans;
		private Regex _xmlCodeBlocksRegExPreTrans;
		private Regex _genericCodeBlocksRegExPreTrans;

		public Func<string, string> LineBreaks { get; set; }

		public Func<string, string> HtmlEncoding { get; set; }

		public Func<string, string> Generic { get; set; }

		public Func<string, string> CSharp { get; set; }

		public Func<string, string> JavaScript { get; set; }

		public Func<string, string> Html { get; set; }

		public Func<string, string> Css { get; set; }

		public Func<string, string> Xml { get; set; }

		public Tranformers(ICodeColorizer colorizer, IFormatter formatter, IStyleSheet styleSheet)
		{
			_colorizer = colorizer;
			_formatter = formatter;
			_styleSheet = styleSheet;
			InitializeTransformers();
		}

		private void InitializeTransformers()
		{
			OnInitializeTranformerRegExs();
			OnInitializeTranformerFuncs();
		}

		protected virtual void OnInitializeTranformerRegExs()
		{
			_cSharpCodeBlocksRegExPreTrans = CodeBlockRegEx("(c#|csharp){1}");
			_jsCodeBlocksRegExPreTrans = CodeBlockRegEx("(js|javascript){1}");
			_htmlCodeBlocksRegExPreTrans = CodeBlockRegEx("html");
			_cssCodeBlocksRegExPreTrans = CodeBlockRegEx("css");
			_xmlCodeBlocksRegExPreTrans = CodeBlockRegEx("xml");
			_genericCodeBlocksRegExPreTrans = CodeBlockRegEx(string.Empty);
		}

		static Regex CodeBlockRegEx(string name)
		{
			const string format = "^{0}([\\s]*){1}(.*?){0}";

			return new Regex(string.Format(format, CodeBlockMarker, name), RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.Compiled | RegexOptions.Singleline);
		}

		protected virtual void OnInitializeTranformerFuncs()
		{
			LineBreaks = mc => mc.Replace("\r\n", "\n");
			HtmlEncoding = mc => mc.Replace("\\<", "&lt;").Replace("\\>", "&gt;");

			CSharp = CodeBlockFunc(_cSharpCodeBlocksRegExPreTrans, Languages.CSharp);
			JavaScript = CodeBlockFunc(_jsCodeBlocksRegExPreTrans, Languages.JavaScript);
			Html = CodeBlockFunc(_htmlCodeBlocksRegExPreTrans, Languages.Html);
			Css = CodeBlockFunc(_cssCodeBlocksRegExPreTrans, Languages.Css);
			Xml = CodeBlockFunc(_xmlCodeBlocksRegExPreTrans, Languages.Xml);
			Generic = CodeBlockFunc(_genericCodeBlocksRegExPreTrans);
		}

		Func<string, string> CodeBlockFunc(Regex regex, ILanguage language = null)
		{
			return mc => regex.Replace(mc, m => FormatAndColorize(m.Value, language));
		}

		protected virtual string FormatAndColorize(string value, ILanguage language = null)
		{
			var stringBuilder = new StringBuilder();
			var lines = value.Split(new[] {"\n"}, StringSplitOptions.None);

			foreach (var str in lines.Where(s => !s.StartsWith(CodeBlockMarker)))
			{
				if (language == null)
					stringBuilder.Append(new string(' ', 4));

				if (str == "\n")
					stringBuilder.AppendLine();
				else
					stringBuilder.AppendLine(str);
			}

			if (language == null)
				return stringBuilder.ToString();

			var writer = new StringWriter();

			_colorizer.Colorize(stringBuilder.ToString().Trim(), language, _formatter, _styleSheet, writer);

			writer.Close();

			return writer.ToString();
		}

		public virtual IEnumerable<Func<string, string>> GetTransformers()
		{
			yield return LineBreaks;

			yield return CSharp;
			
			yield return JavaScript;
			
			yield return Html;
			
			yield return Css;
			
			yield return Xml;
			
			yield return Generic;
			
			yield return HtmlEncoding;
		}
	}
}