namespace CodeThoughts.Infrastructure
{
	using ColorCode;
	using ColorCode.Formatting;
	using ColorCode.Styling.StyleSheets;
	using Formatters;
	using Ninject.Modules;

	public class InfrastructureModule : NinjectModule
	{
		public override void Load()
		{
			Kernel.Bind<ICodeColorizer>().To<CodeColorizer>();
			Kernel.Bind<IFormatter>().To<HtmlFormatter>();
			Kernel.Bind<IStyleSheet>().To<DefaultStyleSheet>();
			Kernel.Bind<Tranformers>().ToSelf();
			Kernel.Bind<IMarkdownService>().To<MarkdownService>().InSingletonScope();
		}
	}
}